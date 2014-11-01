using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EntryTec;
using System.Data.OleDb;
using System.Web.Mail;
using System.Net.Mail;
using eClock;
using System.Threading;
using System.Collections.Generic;

/// <summary>
/// Descripción breve de Cec_Incidencias
/// </summary>
public class Cec_Incidencias
{
    public Cec_Incidencias()
    {

    }
    /// <summary>
    /// Asigna una incidencia a un empleado en un rango de dias seleccionado. Si no existe
    /// </summary>
    /// <param name="TipoIncidenciaID">ID del Tipo de Incidencia.</param>
    /// <param name="PersonasDiariosIDs">ID de los registros de Personas Diario (registros seleccionados)</param>
    /// <param name="IncidenciaComentario">Comentario sobre la incidencia asignada</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>ID de la Incidencia creada</returns>
    public static int AsignaIncidencia(int TipoIncidenciaID, string PersonasDiariosIDs, string IncidenciaComentario, CeC_Sesion Sesion)
    {
        string[] sPersonasDiariosIDs = CeC.ObtenArregoSeparador(PersonasDiariosIDs, ",");
        if (sPersonasDiariosIDs.Length <= 0)
            return -1;
        bool Validar = false;
        if (Sesion != null)
            if (!CeC_Periodos.PuedeModificarBloqueados(Sesion))
                Validar = true;

        if (TipoIncidenciaID == 0)
        {
            int Borrados = 0;
            foreach (string sPersonaDiarioID in sPersonasDiariosIDs)
            {
                try
                {
                    int PersonaDiarioID = CeC.Convierte2Int(sPersonaDiarioID);
                    if (Validar)
                        if (!CeC_Periodos.PuedeModificar(PersonaDiarioID))
                        {
                            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Justificacion", PersonaDiarioID, "No puede modificar días bloqueados");
                            continue;
                        }
                    AsignaIncidencia(PersonaDiarioID, 0, Sesion);
                    Borrados++;
                }
                catch { }
            }
            return Borrados;
        }

        eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencias = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS();
        TipoIncidencias.TIPO_INCIDENCIA_ID = TipoIncidenciaID;
        TipoIncidencias = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TIPO_INCIDENCIAS>(CeC_Tablas.ObtenDatos("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", TipoIncidencias, Sesion));

        int NoIncidencias = 0;

        if (TipoIncidencias.TIPO_INCIDENCIA_REGLAS)
        {
            DataSet DSEC_TIPO_INCIDENCIAS_R = null;
            if (TipoIncidencias.TIPO_INCIDENCIA_REGLAS)
                DSEC_TIPO_INCIDENCIAS_R = CeC_IncidenciasRegla.ObtenEC_TIPO_INCIDENCIAS_R(TipoIncidenciaID);

            string PersonasIds = eClockBase.CeC.PersonasDiarioIDs2PersonaIDs(PersonasDiariosIDs);
            string[] sPersonasIds = eClockBase.CeC.ObtenArregoSeparador(PersonasIds, ",");
            foreach (string sPersonaID in sPersonasIds)
            {
                string PersonaDiarioIDXPersona = "";
                int PersonaId = CeC.Convierte2Int(sPersonaID);
                int PersonaDiarioID = 0;

                foreach (string sPersonaDiarioID in sPersonasDiariosIDs)
                {
                    PersonaDiarioID = CeC.Convierte2Int(sPersonaDiarioID);
                    if (Validar)
                        if (!CeC_Periodos.PuedeModificar(PersonaDiarioID))
                        {
                            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Justificacion", PersonaDiarioID, "No puede modificar días bloqueados");
                            continue;
                        }
                    if (eClockBase.CeC.PersonaDiarioID2PersonaID(PersonaDiarioID) == PersonaId)
                        PersonaDiarioIDXPersona = CeC.AgregaSeparador(PersonaDiarioIDXPersona, PersonaDiarioID.ToString(), ",");
                }

                if (PersonaDiarioIDXPersona != "")
                {
                    CeC_IncidenciasRegla TipoIncidenciaR = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_DesdePersonaID(DSEC_TIPO_INCIDENCIAS_R, PersonaId);
                    if(CeC_IncidenciasRegla.AsignaIncidencia(TipoIncidencias, TipoIncidenciaR, PersonaDiarioIDXPersona, IncidenciaComentario, Sesion)> 0)
                        NoIncidencias ++;
                }
            }
            EnviaMailIncidencia(TipoIncidencias, PersonasDiariosIDs, IncidenciaComentario, Sesion);
            return NoIncidencias;
        }
        else
        {
            int IncidenciaID = -1;
            ///Borrar Incidencia
            if (TipoIncidenciaID == -1)
                IncidenciaID = -1;
            else
                IncidenciaID = CreaIncidencia(TipoIncidenciaID, IncidenciaComentario, Sesion);

            if (IncidenciaID >= 0)
            {

                foreach (string sPersonaDiarioID in sPersonasDiariosIDs)
                {
                    try
                    {
                        int PersonaDiarioID = CeC.Convierte2Int(sPersonaDiarioID);
                        if (Validar)
                            if (!CeC_Periodos.PuedeModificar(PersonaDiarioID))
                            {
                                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Justificacion", PersonaDiarioID, "No puede modificar días bloqueados");
                                continue;
                            }
                        AsignaIncidencia(PersonaDiarioID, IncidenciaID, Sesion);
                    }
                    catch { }
                }
                EnviaMailIncidencia(TipoIncidencias, PersonasDiariosIDs, IncidenciaComentario, Sesion);
            }
            return IncidenciaID;
        }
    }

    public static bool EnviaMailIncidencia(eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencia, string PersonasDiariosIDs, string IncidenciaComentario, CeC_Sesion Sesion)
    {
        if (TipoIncidencia.TIPO_INCIDENCIA_MOMENTO != null && TipoIncidencia.TIPO_INCIDENCIA_MOMENTO != "")
        {
            string PersonasIds = eClockBase.CeC.PersonasDiarioIDs2PersonaIDs(PersonasDiariosIDs);
            string[] sPersonasIds = eClockBase.CeC.ObtenArregoSeparador(PersonasIds, ",");
            string Cuerpo;
            Cuerpo = " Se ha asignado la incidencia " + TipoIncidencia.TIPO_INCIDENCIA_NOMBRE + " a:" + eClockBase.CeC.SaltoLineaHtml;


            foreach (string sPersonaID in sPersonasIds)
            {
                int PersonaID = CeC.Convierte2Int(sPersonaID);
                eClockBase.Modelos.Personas.Model_Datos PersonaDatos = CeC_Personas.ObtenDatosPersonaModelo(PersonaID);
                Cuerpo += PersonaDatos.PERSONA_NOMBRE + " (" + PersonaDatos.PERSONA_LINK_ID + ") para las fechas " + eClockBase.CeC.ObtenDiasTexto(eClockBase.CeC.PersonasDiarioIDs2Fechas(PersonasDiariosIDs, PersonaID)) + " Datos: " + IncidenciaComentario + eClockBase.CeC.SaltoLineaHtml;
            }
            CeC_Mails.EnviaMensaje("", TipoIncidencia.TIPO_INCIDENCIA_MOMENTO, "", TipoIncidencia.TIPO_INCIDENCIA_NOMBRE, Cuerpo, 0, null, Sesion);
            return true;
        }
        return false;
    }
    /// <summary>
    /// Crea una nueva incidencia
    /// </summary>
    /// <param name="TipoIncidenciaID">ID del Tipo de Incidencia.</param>
    /// <param name="IncidenciaComentario">Comentario sobre la incidencia asignada</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>ID de la Incidencia creada</returns>
    public static int CreaIncidencia(int TipoIncidenciaID, string IncidenciaComentario, CeC_Sesion Sesion)
    {
        if (TipoIncidenciaID <= 0)
            return -1;
        int SesionID = 0;
        if (Sesion != null)
            if (Sesion.SESION_ID < 0)
                return -2;
            else
                SesionID = Sesion.SESION_ID;
        int IncidenciaID = CeC_Autonumerico.GeneraAutonumerico("EC_INCIDENCIAS", "INCIDENCIA_ID");
        int R = CeC_BD.EjecutaComando("INSERT INTO EC_INCIDENCIAS (INCIDENCIA_ID, TIPO_INCIDENCIA_ID, INCIDENCIA_COMENTARIO, INCIDENCIA_FECHAHORA, SESION_ID) VALUES(" +
        IncidenciaID + ", " + TipoIncidenciaID + ", '" + IncidenciaComentario + "', " + CeC_BD.SqlFechaHora(DateTime.Now) + ", " + SesionID + ")");
        if (R > 0)
        {
            if (SesionID > 0)
            {
                string Descripcion = "";
                Descripcion += "TipoIncidenciaID = " + TipoIncidenciaID + ", Tipo = " + ObtenTipoIncidenciaNombre(TipoIncidenciaID) + ", Comentario = " + IncidenciaComentario;
                CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO, "Justificacion", IncidenciaID, Descripcion, SesionID);
            }

            return IncidenciaID;
        }
        return -9999;
    }
    public static string Obten_Tipo_Inc(DateTime FechaHoraEntrada, DateTime FechaHoraSalida, int Tipo_Inc_ID, string Tipo_Inc, string JustificaComent)
    {
        if (JustificaComent.Length > 0)
            return "";
        if (FechaHoraSalida < FechaHoraEntrada.AddMinutes(30))
            FechaHoraSalida = CeC_BD.FechaNula;
        switch (Tipo_Inc_ID)
        {
            case 1: return Tipo_Inc;
        }

        if (FechaHoraEntrada == CeC_BD.FechaNula && FechaHoraSalida == CeC_BD.FechaNula)
        {
            switch (Tipo_Inc_ID)
            {
                case 10: return Tipo_Inc;
                case 11: return Tipo_Inc;
                case 0: if (DateTime.Now.Date < FechaHoraEntrada.Date) return Tipo_Inc; break;
            }
        }

        int Inc_ID = 1;
        if (FechaHoraEntrada == CeC_BD.FechaNula && FechaHoraSalida == CeC_BD.FechaNula)
            Inc_ID = 12;
        else
            if (FechaHoraEntrada == CeC_BD.FechaNula || FechaHoraSalida == CeC_BD.FechaNula)
                Inc_ID = 13;
        return Obten_Tipo_Inc(Inc_ID);

    }
    public static string Obten_Tipo_Inc(int ID)
    {
        switch (ID)
        {
            case 1:
                return "Asistencia Normal";
                break;
            case 12:
                return "Falta";
                break;
            case 13:
                return "Falta Checada";
                break;

        }
        return "";
    }
    /// <summary>
    /// Asigna una Incidencia a un empleado en el perdiodo de Fechas indicado
    /// </summary>
    /// <param name="FechaInicial">Fecha de Inicio de la incidencia</param>
    /// <param name="FechaFinal">Fecha de Fin de la incidencia(Puede ser el mismo que la Fecha Inicial, pero no menor)</param>
    /// <param name="Persona_ID">ID de la Persona a la que se asignará la incidencia</param>
    /// <param name="IncidenciaID">ID de la Incidencia a asignar</param>
    /// <returns>Registros afectados. Error -1</returns>
    public static int AsignaIncidencia(DateTime FechaInicial, DateTime FechaFinal, int Persona_ID, int IncidenciaID)
    {
        return AsignaIncidencia(FechaInicial, FechaFinal, Persona_ID, IncidenciaID, null);
    }
    /// <summary>
    /// Asigna una Incidencia a un empleado en el perdiodo de Fechas indicado
    /// </summary>
    /// <param name="FechaInicial">Fecha de Inicio de la incidencia</param>
    /// <param name="FechaFinal">Fecha de Fin de la incidencia(Puede ser el mismo que la Fecha Inicial, pero no menor)</param>
    /// <param name="Persona_ID">ID de la Persona a la que se asignará la incidencia</param>
    /// <param name="IncidenciaID">ID de la Incidencia a asignar</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>Registros afectados. Error -1</returns>
    public static int AsignaIncidencia(DateTime FechaInicial, DateTime FechaFinal, int Persona_ID, int IncidenciaID, CeC_Sesion Sesion)
    {
        if (FechaFinal < FechaInicial)
            return -1;
        if (Persona_ID <= 0)
            return -2;
        if (IncidenciaID <= 0)
            return -3;

        FechaInicial = FechaInicial.Date;
        FechaFinal = FechaFinal.Date.AddDays(1);
        CeC_Asistencias.GeneraPrevioPersonaDiario(Persona_ID, FechaInicial, FechaFinal);
        CeC_IncidenciasInventario.CorrigeMovimientos(Sesion, Persona_ID, FechaInicial, FechaFinal.AddDays(-1));
        int R = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET INCIDENCIA_ID = " +
            IncidenciaID + " WHERE PERSONA_ID = " + Persona_ID + " AND PERSONA_DIARIO_FECHA >=" +
            CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(FechaFinal));
        if (R > 0)
        {
            if (Sesion != null && Sesion.SESION_ID > 0)
            {

                int Persona_Link_ID = CeC_Empleados.ObtenPersona_Link_ID(Persona_ID);
                string Descripcion = "";
                Descripcion += "NoEmpleado = " + Persona_Link_ID + ", Fecha Desde = " + FechaInicial + " Hasta =" + FechaFinal.AddDays(-1) + ", IncidenciaID = " + IncidenciaID;
                CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Justificacion Dia", Persona_ID, Descripcion, Sesion.SESION_ID);
            }

            return 1;
        }
        return -9999;
    }
    /// <summary>
    /// Asigna una Incidencia a un empleado en el periodo
    /// </summary>
    /// <param name="Persona_Diario_ID">ID de los registros de Persona Diario(dias seleccionados)</param>
    /// <param name="IncidenciaID">ID de la Incidencia a asignar</param>
    /// <returns>Registros afectados. Error -1</returns>
    public static int AsignaIncidencia(int Persona_Diario_ID, int IncidenciaID)
    {
        return AsignaIncidencia(Persona_Diario_ID, IncidenciaID, null);
    }
    /// <summary>
    /// Asigna una Incidencia a un empleado en el perdiodo de Fechas indicado
    /// </summary>
    /// <param name="Sesion">Variable se Sesion</param>
    /// <param name="DatosIncidencia">DatosIncidencia[0] FechaInicial
    /// DatosIncidencia[1] FechaFinal
    /// DatosIncidencia[2] Persona_ID, 
    /// DatosIncidencia[3] IncidenciaID</param>
    /// <returns>Lista con los registros asignados. Registros con error son -1</returns>
    public static string AsignaIncidencia(CeC_Sesion Sesion, string DatosIncidencia)
    {
        string[] Filas = CeC.ObtenArregoSeparador(DatosIncidencia, ",");
        string[] DatosIncidenciaAsignar = CeC.ObtenArregoSeparador(DatosIncidencia, ",");
        foreach (string Fila in Filas)
        {
            AsignaIncidencia(CeC.Convierte2DateTime(DatosIncidencia[0]), CeC.Convierte2DateTime(DatosIncidencia[1]), CeC.Convierte2Int(DatosIncidencia[2]), CeC.Convierte2Int(DatosIncidencia[3]), Sesion);
        }
        return "";
    }
    public static string ObtenIncidenciaDesc(int IncidenciaID)
    {
        //     CeC_BD.EjecutaEscalarString("SELECT 
        return "";
    }
    /// <summary>
    /// Asigna una Incidencia a un empleado en el periodo
    /// </summary>
    /// <param name="Persona_Diario_ID">ID de los registros de Persona Diario(dias seleccionados)</param>
    /// <param name="IncidenciaID">ID de la Incidencia a asignar</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>Registros afectados. Error -1</returns>
    public static int AsignaIncidencia(int Persona_Diario_ID, int IncidenciaID, CeC_Sesion Sesion)
    {
        if (Persona_Diario_ID <= 0)
            return -2;
        if (IncidenciaID < -1 )
            return -3;
        CeC_IncidenciasInventario.CorrigeMovimiento(Sesion, Persona_Diario_ID, 0);
        int R = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET INCIDENCIA_ID = " +
                IncidenciaID + " WHERE PERSONA_DIARIO_ID = " + Persona_Diario_ID);
        if (R > 0)
        {
            if (Sesion.SESION_ID > 0)
            {

                int Persona_Link_ID = CeC_Empleados.ObtenPersona_Link_ID(CeC_Asistencias.ObtenPersonaID(Persona_Diario_ID));
                string Descripcion = "";
                Descripcion += "NoEmpleado = " + Persona_Link_ID + ", Fecha = " + CeC_Asistencias.ObtenFecha(Persona_Diario_ID).ToShortDateString() + ", IncidenciaID = " + IncidenciaID;
                CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Justificacion Dia", Persona_Diario_ID, Descripcion, Sesion.SESION_ID);
            }


            return 1;
        }
        return -9999;
    }
    public static string ObtenTipoIncidenciaNombre(int TipoIncidenciaID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT TIPO_INCIDENCIA_NOMBRE FROM EC_TIPO_INCIDENCIAS WHERE TIPO_INCIDENCIA_ID = " + TipoIncidenciaID);
    }
    public static DataSet ObtenTiposIncidencias(int SuscripcionID, bool MostrarBorrados)
    {
        return ObtenTiposIncidencias(SuscripcionID, MostrarBorrados, false);

    }
    public static DataSet ObtenTiposIncidencias(int SuscripcionID, bool MostrarBorrados, bool SoloConReglas)
    {
        string Borrado = "";
        if (!MostrarBorrados)
            Borrado = " AND (TIPO_INCIDENCIA_BORRADO = 0)";

        string ADD = " TIPO_INCIDENCIA_ID IN (";
        ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TIPO_INCIDENCIA_ID " +
                "FROM         EC_AUTONUM " +
                " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TIPO_INCIDENCIAS') AND (SUSCRIPCION_ID  = " + SuscripcionID + "))";

        if (SoloConReglas)
        {
            ADD += " AND TIPO_INCIDENCIA_ID IN (SELECT TIPO_INCIDENCIA_ID FROM EC_TIPO_INCIDENCIAS_R WHERE TIPO_INCIDENCIA_R_BORRADO = 0) ";
        }
        return (DataSet)
            CeC_BD.EjecutaDataSet(
                @"SELECT TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE, TIPO_INCIDENCIA_ABR, TIPO_INCIDENCIA_BORRADO FROM EC_TIPO_INCIDENCIAS WHERE " + ADD + Borrado);
    }
    public static DataSet ObtenTiposIncidenciasMenu(int SuscripcionID)
    {
        string ADD = " TIPO_INCIDENCIA_ID IN (";
        ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TIPO_INCIDENCIA_ID " +
                "FROM         EC_AUTONUM " +
                " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TIPO_INCIDENCIAS') AND (SUSCRIPCION_ID  = " + SuscripcionID + "))  AND (TIPO_INCIDENCIA_BORRADO = 0)";

        if (CeC_BD.EsOracle)
            return (DataSet)
    CeC_BD.EjecutaDataSet(
        @"SELECT TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE || ' (' || TIPO_INCIDENCIA_ABR || ')' as TIPO_INCIDENCIA_NOMBRE FROM EC_TIPO_INCIDENCIAS WHERE " + ADD);

        return (DataSet)
            CeC_BD.EjecutaDataSet(
                @"SELECT TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE + ' (' + TIPO_INCIDENCIA_ABR + ')' as TIPO_INCIDENCIA_NOMBRE FROM EC_TIPO_INCIDENCIAS WHERE " + ADD);
    }
    public static DataSet ObtenTiposIncidenciasSistemaMenu()
    {
        if (CeC_BD.EsOracle)
            return (DataSet)
    CeC_BD.EjecutaDataSet(
        @"SELECT TIPO_INC_SIS_ID, TIPO_INC_SIS_NOMBRE || ' (' || TIPO_INC_SIS_ABR || ')' as TIPO_INC_SIS_NOMBRE FROM EC_TIPO_INC_SIS WHERE TIPO_INC_SIS_ID > 0");

        return (DataSet)
            CeC_BD.EjecutaDataSet(
                @"SELECT TIPO_INC_SIS_ID, TIPO_INC_SIS_NOMBRE + ' (' + TIPO_INC_SIS_ABR + ')' as TIPO_INC_SIS_NOMBRE FROM EC_TIPO_INC_SIS WHERE TIPO_INC_SIS_ID > 0 ");
    }
    public static int ObtenTipoIncidenciaExID(int Persona_ID, DateTime Fecha)
    {
        string Qry = "";
        Qry = "SELECT TIPO_INCIDENCIAS_EX_ID FROM EC_PERSONAS_DIARIO,EC_TIPO_INCIDENCIAS_EX_INC,eC_INCIDENCIAS WHERE " +
                "EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIA_ID = EC_INCIDENCIAS.TIPO_INCIDENCIA_ID  " +
                "AND EC_PERSONAS_DIARIO.INCIDENCIA_ID = EC_INCIDENCIAS.INCIDENCIA_ID  " +
                "AND PERSONA_ID = " + Persona_ID + " AND PERSONA_DIARIO_FECHA= " + CeC_BD.SqlFecha(Fecha);
        int IncEx = CeC_BD.EjecutaEscalarInt(Qry);
        if (IncEx > 0)
            return IncEx;
        Qry = "SELECT TIPO_INCIDENCIAS_EX_ID from EC_PERSONAS_DIARIO, EC_TIPO_INCIDENCIAS_EX_INC_SIS WHERE " +
              "EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID = EC_TIPO_INCIDENCIAS_EX_INC_SIS.TIPO_INC_SIS_ID " +
              "AND PERSONA_ID = " + Persona_ID + " AND PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(Fecha);
        IncEx = CeC_BD.EjecutaEscalarInt(Qry);
        return IncEx;
    }

    public static string ObtenTipoIncidenciaExTXT(int TipoIncidenciaExID)
    {
        if (TipoIncidenciaExID <= 0)
            return "";
        return CeC_BD.EjecutaEscalarString("SELECT TIPO_INCIDENCIAS_EX_TXT FROM  EC_TIPO_INCIDENCIAS_EX WHERE TIPO_INCIDENCIAS_EX_ID = " + TipoIncidenciaExID);
    }
    public static int ObtenTipoIncidenciaExID(string TipoIncidenciaExTXT)
    {
        if (TipoIncidenciaExTXT.Length <= 0)
            return 0;
        return CeC_BD.EjecutaEscalarInt("SELECT TIPO_INCIDENCIAS_EX_ID FROM  EC_TIPO_INCIDENCIAS_EX WHERE TIPO_INCIDENCIAS_EX_TXT = '" + TipoIncidenciaExTXT + "'");
    }
    public static int ObtenTipoIncidenciaID_PersonaDiarioID(int PersonaDiarioID)
    {
        string Qry = "SELECT TIPO_INCIDENCIA_ID " +
       "FROM  EC_PERSONAS_DIARIO INNER JOIN " +
       "EC_INCIDENCIAS ON EC_PERSONAS_DIARIO.INCIDENCIA_ID = EC_INCIDENCIAS.INCIDENCIA_ID " +
       "WHERE EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = " + PersonaDiarioID;
        return CeC_BD.EjecutaEscalarInt(Qry);
    }
    public static int ObtenTipoIncidenciaID(int TipoIncidenciaExID)
    {
        if (TipoIncidenciaExID <= 0)
            return 0;
        return CeC_BD.EjecutaEscalarInt("SELECT     TIPO_INCIDENCIA_ID " +
                " FROM EC_TIPO_INCIDENCIAS_EX_INC WHERE TIPO_INCIDENCIAS_EX_ID = " + TipoIncidenciaExID);
    }
    public static int TieneIncidencia(int Persona_ID, DateTime FechaHora)
    {
        string qry = "SELECT INCIDENCIA_ID FROM ( " +
            "select PERSONA_DIARIO_ID,PERSONA_DIARIO_FECHA , " +
            " PERSONA_DIARIO_FECHA + (turno_dia_hemin - " + CeC_BD.SqlFechaNula() + ") AS HEMIN, " +
            " PERSONA_DIARIO_FECHA + (TURNO_DIA_HSMAX - " + CeC_BD.SqlFechaNula() + ") AS HSMAX " +
            " from EC_PERSONAS_diario, EC_TURNOS_DIA where  EC_PERSONAS_diario.TURNO_DIA_ID= EC_TURNOS_DIA.TURNO_DIA_ID " +
            " AND EC_PERSONAS_diario.persona_ID  = " + Persona_ID + ")t where " + CeC_BD.SqlFechaHora(FechaHora);
        return CeC_BD.EjecutaEscalarInt(qry);
    }
    public static bool TipoIncidenciaBorra(int Tipo_Incidencia_ID)
    {
        int R = CeC_BD.EjecutaComando("UPDATE EC_TIPO_INCIDENCIAS SET TIPO_INCIDENCIA_BORRADO = 1 WHERE TIPO_INCIDENCIA_ID = " + Tipo_Incidencia_ID);
        if (R > 0)
            return true;
        return false;
    }
    public static bool TipoIncidenciaActualiza(int Tipo_Incidencia_ID, string Nombre, string Abreviatura, bool Borrar)
    {
        int Borrado = 0;
        if (Borrar)
            Borrado = 1;
        int R = CeC_BD.EjecutaComando("UPDATE EC_TIPO_INCIDENCIAS SET TIPO_INCIDENCIA_BORRADO = "
            + Borrado + ", TIPO_INCIDENCIA_NOMBRE = '" + CeC_BD.ObtenParametroCadena(Nombre)
            + "' , TIPO_INCIDENCIA_ABR='" + CeC_BD.ObtenParametroCadena(Abreviatura) + "' WHERE TIPO_INCIDENCIA_ID = " + Tipo_Incidencia_ID);
        if (R > 0)
            return true;
        return false;

    }
    /// <summary>
    /// Haciendo una solicitud ala base de datos
    /// obtiene el ID del tipo de incidencia seleccionado
    /// para posteriormente ser asignado al empleado.
    /// </summary>
    /// <param name="Suscripcion_ID"></param>
    /// <param name="Nombre"></param>
    /// <returns></returns>
    public static int ObtenTipoIncidenciaID(int Suscripcion_ID, string Nombre)
    {
        string ADD = " TIPO_INCIDENCIA_ID IN (";
        ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TIPO_INCIDENCIA_ID " +
                "FROM         EC_AUTONUM " +
                " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TIPO_INCIDENCIAS') AND (SUSCRIPCION_ID  = " + Suscripcion_ID + "))";

        return CeC_BD.EjecutaEscalarInt("SELECT TIPO_INCIDENCIA_ID FROM EC_TIPO_INCIDENCIAS WHERE TIPO_INCIDENCIA_NOMBRE = '" + CeC_BD.ObtenParametroCadena(Nombre) + "' AND " + ADD);
    }
    /// <summary>
    /// 
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public static int ObtenTipoIncidenciaEXID(int Suscripcion_ID, string IDExtTXT)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT TIPO_INCIDENCIAS_EX_ID FROM EC_TIPO_INCIDENCIAS_EX WHERE TIPO_INCIDENCIAS_EX_TXT = '" + CeC_BD.ObtenParametroCadena(IDExtTXT) + "' AND " + CeC_Autonumerico.ValidaSuscripcion("EC_TIPO_INCIDENCIAS_EX", "TIPO_INCIDENCIAS_EX_ID", Suscripcion_ID));
    }

    public static int TipoIncidenciaExAgrega(int Sesion_ID, int Suscripcion_ID, string IDExtTXT, string Nombre, string Descripcion, string Parametros, int TIPO_FALTA_EX_ID)
    {
        int R = ObtenTipoIncidenciaEXID(Suscripcion_ID, IDExtTXT);
        if (R > 0)
            return R;
        if (IDExtTXT.Length <= 0)
            return -999;
        int Tipo_IncidenciaEx_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TIPO_INCIDENCIAS_EX", "TIPO_INCIDENCIAS_EX_ID", Sesion_ID, Suscripcion_ID);
        string Qry = "INSERT INTO EC_TIPO_INCIDENCIAS_EX (" +
            "TIPO_INCIDENCIAS_EX_ID, TIPO_INCIDENCIAS_EX_TXT, TIPO_INCIDENCIAS_EX_NOMBRE, TIPO_INCIDENCIAS_EX_DESC," +
            " TIPO_INCIDENCIAS_EX_PARAM, TIPO_FALTA_EX_ID, TIPO_INCIDENCIAS_EX_BORRADO) VALUES(" +
            Tipo_IncidenciaEx_ID + ",'" + CeC_BD.ObtenParametroCadena(IDExtTXT) + "','" + CeC_BD.ObtenParametroCadena(Nombre) + "','" + CeC_BD.ObtenParametroCadena(Descripcion) +
            "','" + CeC_BD.ObtenParametroCadena(Parametros) + "'," + TIPO_FALTA_EX_ID + ",0)";
        R = CeC_BD.EjecutaComando(Qry);
        if (R > 0)
        {
            CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO, "Tipo de Incidencias Externas", Tipo_IncidenciaEx_ID, "Nombre = " + Nombre + ", IDExtTXT = " + IDExtTXT, Sesion_ID);
            return Tipo_IncidenciaEx_ID;
        }
        return -1;
    }
    /// <summary>
    /// Agrega una nueva incidencia
    /// </summary>
    /// <param name="Sesion_ID">ID de Sesion</param>
    /// <param name="Suscripcion_ID">Identificador de la Suscripcion</param>
    /// <param name="Nombre">Nombre de la incidencia</param>
    /// <param name="Abreviatura">Abreviatura de la incidencia</param>
    /// <returns>Registros insertados con exito. Error -1</returns>
    public static int TipoIncidenciaAgrega(int Sesion_ID, int Suscripcion_ID, string Nombre, string Abreviatura)
    {
        int R = ObtenTipoIncidenciaID(Suscripcion_ID, Nombre);
        if (R > 0)
            return R;

        if (Abreviatura.Length > 2)
            Abreviatura = Abreviatura.Substring(0, 2);
        int Tipo_Incidencia_ID = CeC_Autonumerico.GeneraAutonumerico("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", Sesion_ID, Suscripcion_ID);
        R = CeC_BD.EjecutaComando("INSERT INTO EC_TIPO_INCIDENCIAS (TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE, TIPO_INCIDENCIA_ABR, TIPO_INCIDENCIA_BORRADO) VALUES(" +
           Tipo_Incidencia_ID + ", '" + CeC_BD.ObtenParametroCadena(Nombre) + "', '" + CeC_BD.ObtenParametroCadena(Abreviatura) + "', 0)");
        if (R > 0)
        {
            CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO, "Tipo de Incidencias", Tipo_Incidencia_ID, "Nombre = " + Nombre + ", Abreviatura = " + Abreviatura, Sesion_ID);
            return Tipo_Incidencia_ID;
        }
        return -1;
    }
    /// <summary>
    /// Agrega una nueva incidencia
    /// </summary>
    /// <param name="Sesion_ID">ID de Sesion</param>
    /// <param name="Nombre">Nombre de la incidencia</param>
    /// <param name="Abreviatura">Abreviatura de la incidencia</param>
    /// <returns>Registros insertados con exito. Error -1</returns>
    public static int TipoIncidenciaAgrega(CeC_Sesion Sesion, string Nombre, string Abreviatura)
    {
        return TipoIncidenciaAgrega(Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, Nombre, Abreviatura);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public static int ObtenTotalTipoIncidencias(int SuscripcionID, bool Borrados)
    {
        DataSet Ds = ObtenTiposIncidencias(SuscripcionID, Borrados);
        return Ds.Tables[0].Rows.Count;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public static void CrearTiposIncidenciasPredeterminados(int Suscripcion_ID)
    {
        TipoIncidenciaAgrega(0, Suscripcion_ID, "Vacaciones", "V");
        TipoIncidenciaAgrega(0, Suscripcion_ID, "Permiso con Goce", "PG");
        TipoIncidenciaAgrega(0, Suscripcion_ID, "Permiso sin Goce", "PS");
        TipoIncidenciaAgrega(0, Suscripcion_ID, "Tiempo por Tiempo", "TT");


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public static void InsertaTipoIncidencia()
    { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public static string ObtenPersonasDiarioIDs(string PendientesPorValidar, int TipoIncidenciaRID)
    {
        string Ret = "";
        try
        {
            string[] Valores = PendientesPorValidar.Split(new char[] { ',' });
            for (int Cont = 0; Cont < Valores.Length; Cont += 2)
            {
                if (Convert.ToInt32(Valores[Cont]) == TipoIncidenciaRID)
                    Ret = CeC.AgregaSeparador(Ret, Valores[Cont + 1], ",");
            }
        }
        catch { }
        return Ret;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public static bool QuitaIncidencia(int IncidenciaID)
    {
        if (IncidenciaID <= 0)
            return false;
        if (CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET INCIDENCIA_ID = 0 WHERE INCIDENCIA_ID = " + IncidenciaID) > 0)
            return true;
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public static int ObtenTotalAplicadas(int IncidenciaID)
    {
        return 0;
    }
    public static eClockBase.Modelos.Incidencias.Model_StatusHoras ObtenStatusHoras(int PersonaDiarioID, CeC_Sesion Sesion)
    {
        try
        {
            eClockBase.Modelos.Incidencias.Model_StatusHoras Status = new eClockBase.Modelos.Incidencias.Model_StatusHoras();

            string Qry = "SELECT TURNO_DIA_HE, TURNO_DIA_HS, EC_V_ASISTENCIAS.ACCESO_E, EC_V_ASISTENCIAS.ACCESO_S " +
                "FROM EC_V_ASISTENCIAS INNER JOIN " +
                "EC_V_TURNOS_DIA_PD_ID ON EC_V_ASISTENCIAS.PERSONA_DIARIO_ID = EC_V_TURNOS_DIA_PD_ID.PERSONA_DIARIO_ID " +
                "WHERE     (EC_V_ASISTENCIAS.PERSONA_DIARIO_ID = " + PersonaDiarioID + ")";
            DataSet DS = CeC_BD.EjecutaDataSet(Qry);
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = DS.Tables[0].Rows[0];
                Status.Turno_Entrada = CeC.Convierte2DateTime(DR["TURNO_DIA_HE"]);
                Status.Turno_Salida = CeC.Convierte2DateTime(DR["TURNO_DIA_HS"]);
                Status.Entrada = CeC.Convierte2DateTime(DR["ACCESO_E"]);
                Status.Salida = CeC.Convierte2DateTime(DR["ACCESO_S"]);
            }

            Qry = "SELECT ACCESO_MIN,ACCESO_MAX" +
            " FROM EC_V_PERSONAS_DIARIO_MINMAX WHERE PERSONA_DIARIO_ID = " + PersonaDiarioID + "";
            DS = CeC_BD.EjecutaDataSet(Qry);
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = DS.Tables[0].Rows[0];
                Status.PrimerChecada = CeC.Convierte2DateTime(DR["ACCESO_MIN"]);
                Status.UltimaChecada = CeC.Convierte2DateTime(DR["ACCESO_MAX"]);
            }
            return Status;
        }
        catch { }
        return null;
    }

    public static List<eClockBase.Modelos.Incidencias.Model_StatusHoras> ObtenStatusHoras(string PersonasDiariosIDs, CeC_Sesion Sesion)
    {
        List<eClockBase.Modelos.Incidencias.Model_StatusHoras> Status = new List<eClockBase.Modelos.Incidencias.Model_StatusHoras>();
        string[] sPersonasDiariosIDs = CeC.ObtenArregoSeparador(PersonasDiariosIDs, ",");
        foreach (string sPersonaDiarioID in sPersonasDiariosIDs)
        {
            eClockBase.Modelos.Incidencias.Model_StatusHoras StatusHora = ObtenStatusHoras(CeC.Convierte2Int(sPersonaDiarioID), Sesion);
            if (StatusHora != null)
                Status.Add(StatusHora);
        }
        return Status;
    }

    public static eClockBase.Modelos.Incidencias.Model_StatusRegla ObtenStatusReglaHoras(int TipoIncidenciaID, int PersonaDiarioID, decimal NoHoras, CeC_Sesion Sesion)
    {
        eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencias = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS();
        TipoIncidencias.TIPO_INCIDENCIA_ID = TipoIncidenciaID;
        TipoIncidencias = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TIPO_INCIDENCIAS>(CeC_Tablas.ObtenDatos("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", TipoIncidencias, Sesion)); List<eClockBase.Modelos.Incidencias.Model_StatusRegla> Statuss = new List<eClockBase.Modelos.Incidencias.Model_StatusRegla>();
        DataSet DSEC_TIPO_INCIDENCIAS_R = null;
        //List<eClockBase.
        if (TipoIncidencias.TIPO_INCIDENCIA_REGLAS)
        {
            DSEC_TIPO_INCIDENCIAS_R = CeC_IncidenciasRegla.ObtenEC_TIPO_INCIDENCIAS_R(TipoIncidenciaID);
        }

        bool Cargar = true;
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_ID,COUNT(PERSONA_DIARIO_ID) AS NOINC FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID = " + PersonaDiarioID + " GROUP BY PERSONA_ID");
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            Cargar = false;
        if (Cargar)
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                eClockBase.Modelos.Incidencias.Model_StatusRegla StatusRegla = new eClockBase.Modelos.Incidencias.Model_StatusRegla();
                int PersonaID = CeC.Convierte2Int(DR["PERSONA_ID"]);
                eClockBase.Modelos.Personas.Model_Datos PersonaDatos = CeC_Personas.ObtenDatosPersonaModelo(PersonaID);
                StatusRegla.PersonaID = PersonaID;
                StatusRegla.PersonaLinkID = PersonaDatos.PERSONA_LINK_ID;
                StatusRegla.PersonaNombre = PersonaDatos.PERSONA_NOMBRE;
                StatusRegla.Agrupacion = PersonaDatos.AGRUPACION_NOMBRE;



                CeC_IncidenciasRegla TipoIncidenciaR = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_DesdePersonaID(DSEC_TIPO_INCIDENCIAS_R, PersonaID);
                if (TipoIncidenciaR != null)
                {
                    int TipoIncidenciaRIDInv = TipoIncidenciaR.TIPO_INCIDENCIA_R_ID;
                    if (TipoIncidenciaR.TIPO_INCIDENCIA_R_ID_INV > 0)
                        TipoIncidenciaRIDInv = TipoIncidenciaR.TIPO_INCIDENCIA_R_ID_INV;
                    decimal Descuento = NoHoras;
                    decimal Saldo = CeC_IncidenciasInventario.ObtenSaldo(PersonaID, TipoIncidenciaRIDInv);
                    decimal SaldoPosterior = Saldo - Descuento;

                    StatusRegla.FechaDesde = CeC_IncidenciasRegla.sObtenFechaInicia(TipoIncidenciaR.TIPO_INCIDENCIA_R_FIQRY, PersonaID);
                    StatusRegla.Saldo = Saldo;
                    StatusRegla.AConsumir = Descuento;
                    StatusRegla.Inventario = TipoIncidenciaR.TIPO_INCIDENCIA_R_INV;
                    StatusRegla.ValorMinimo = TipoIncidenciaR.TIPO_INCIDENCIA_R_CRED;
                    StatusRegla.TIPO_UNIDAD_ID = TipoIncidenciaR.TIPO_UNIDAD_ID;
                    StatusRegla.TIPO_REDONDEO_ID = TipoIncidenciaR.TIPO_REDONDEO_ID;
                    StatusRegla.Sumar = TipoIncidenciaR.TIPO_INCIDENCIA_R_SUMAR;

                    if (SaldoPosterior < TipoIncidenciaR.TIPO_INCIDENCIA_R_CRED)
                        StatusRegla.Permitido = false;
                    else
                        StatusRegla.Permitido = true;
                }
                else
                    StatusRegla.Permitido = false;
                return (StatusRegla);
            }

        return null;
    }

    public static List<eClockBase.Modelos.Incidencias.Model_StatusRegla> ObtenStatusRegla(int TipoIncidenciaID, string PersonasDiariosIDs, CeC_Sesion Sesion)
    {
        eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencias = new eClockBase.Modelos.Model_TIPO_INCIDENCIAS();
        TipoIncidencias.TIPO_INCIDENCIA_ID = TipoIncidenciaID;
        TipoIncidencias = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TIPO_INCIDENCIAS>(CeC_Tablas.ObtenDatos("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", TipoIncidencias, Sesion));
        List<eClockBase.Modelos.Incidencias.Model_StatusRegla> Statuss = new List<eClockBase.Modelos.Incidencias.Model_StatusRegla>();
        DataSet DSEC_TIPO_INCIDENCIAS_R = null;
        if (TipoIncidencias.TIPO_INCIDENCIA_REGLAS)
            DSEC_TIPO_INCIDENCIAS_R = CeC_IncidenciasRegla.ObtenEC_TIPO_INCIDENCIAS_R(TipoIncidenciaID);


        bool Cargar = true;
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet("SELECT PERSONA_ID,COUNT(PERSONA_DIARIO_ID) AS NOINC FROM EC_PERSONAS_DIARIO WHERE PERSONA_DIARIO_ID IN (" + PersonasDiariosIDs + ") GROUP BY PERSONA_ID");
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            Cargar = false;
        if (Cargar)
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                eClockBase.Modelos.Incidencias.Model_StatusRegla StatusRegla = new eClockBase.Modelos.Incidencias.Model_StatusRegla();
                int PersonaID = CeC.Convierte2Int(DR["PERSONA_ID"]);
                eClockBase.Modelos.Personas.Model_Datos PersonaDatos = CeC_Personas.ObtenDatosPersonaModelo(PersonaID);
                StatusRegla.PersonaID = PersonaID;
                StatusRegla.PersonaLinkID = PersonaDatos.PERSONA_LINK_ID;
                StatusRegla.PersonaNombre = PersonaDatos.PERSONA_NOMBRE;
                StatusRegla.Agrupacion = PersonaDatos.AGRUPACION_NOMBRE;



                CeC_IncidenciasRegla TipoIncidenciaR = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_DesdePersonaID(DSEC_TIPO_INCIDENCIAS_R, PersonaID);
                if (TipoIncidenciaR != null)
                {
                    int TipoIncidenciaRIDInv = TipoIncidenciaR.TIPO_INCIDENCIA_R_ID;
                    if (TipoIncidenciaR.TIPO_INCIDENCIA_R_ID_INV > 0)
                        TipoIncidenciaRIDInv = TipoIncidenciaR.TIPO_INCIDENCIA_R_ID_INV;

                    int NOInc = CeC.Convierte2Int(DR["NOINC"]);
                    decimal Descuento = NOInc;
                    decimal Saldo = CeC_IncidenciasInventario.ObtenSaldo(PersonaID, TipoIncidenciaRIDInv);
                    decimal SaldoPosterior = Saldo - Descuento;

                    StatusRegla.FechaDesde = CeC_IncidenciasRegla.sObtenFechaInicia(TipoIncidenciaR.TIPO_INCIDENCIA_R_FIQRY, PersonaID);
                    StatusRegla.Saldo = Saldo;
                    StatusRegla.AConsumir = Descuento;
                    StatusRegla.Inventario = TipoIncidenciaR.TIPO_INCIDENCIA_R_INV;
                    StatusRegla.ValorMinimo = TipoIncidenciaR.TIPO_INCIDENCIA_R_CRED;
                    StatusRegla.TIPO_UNIDAD_ID = TipoIncidenciaR.TIPO_UNIDAD_ID;
                    StatusRegla.TIPO_REDONDEO_ID = TipoIncidenciaR.TIPO_REDONDEO_ID;
                    StatusRegla.Sumar = TipoIncidenciaR.TIPO_INCIDENCIA_R_SUMAR;

                    if (SaldoPosterior < TipoIncidenciaR.TIPO_INCIDENCIA_R_CRED)
                        StatusRegla.Permitido = false;
                    else
                        StatusRegla.Permitido = true;
                }
                else
                    StatusRegla.Permitido = false;
                Statuss.Add(StatusRegla);
            }

        return Statuss;
    }

    public static int Resumen()
    {
        int UltimoIncidenciaID = CeC_Config.IncidenciasResumenUltimoIncidenciaID;
        string Qry = "SELECT	EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID, EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_NOMBRE,  \n" +
" 	EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ABR, EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_RESUMEN,  \n" +
" 	EC_INCIDENCIAS.INCIDENCIA_COMENTARIO, EC_INCIDENCIAS.INCIDENCIA_EXTRAS, EC_USUARIOS.USUARIO_NOMBRE,  \n" +
" 	EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_INCIDENCIAS.INCIDENCIA_ID, \n" +
"  EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE  \n" +
" FROM	EC_INCIDENCIAS INNER JOIN \n" +
" 	EC_TIPO_INCIDENCIAS ON EC_INCIDENCIAS.TIPO_INCIDENCIA_ID = EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID INNER JOIN \n" +
" 	EC_PERSONAS_DIARIO ON EC_INCIDENCIAS.INCIDENCIA_ID = EC_PERSONAS_DIARIO.INCIDENCIA_ID INNER JOIN \n" +
" 	EC_PERSONAS ON EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN \n" +
" 	EC_SESIONES ON EC_INCIDENCIAS.SESION_ID = EC_SESIONES.SESION_ID INNER JOIN \n" +
" 	EC_USUARIOS ON EC_SESIONES.USUARIO_ID = EC_USUARIOS.USUARIO_ID \n" +
" WHERE	(EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID > " + UltimoIncidenciaID + ") AND (EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_RESUMEN IS NOT NULL AND  \n" +
" 	EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_RESUMEN <> '') \n" +
" \n ORDER BY EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID, EC_PERSONAS.PERSONA_NOMBRE";
        int Enviados = 0;
        DataSet DS = CeC_BD.EjecutaDataSet(Qry);
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            int TIPO_INCIDENCIA_ID_Anterior = -1;
            string Titulo = "";
            string Cuerpo = "";
            string eMails = "";
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                Enviados++;
                int TIPO_INCIDENCIA_ID = CeC.Convierte2Int(DR["TIPO_INCIDENCIA_ID"]);
                string TIPO_INCIDENCIA_NOMBRE = CeC.Convierte2String(DR["TIPO_INCIDENCIA_NOMBRE"]);
                
                string TIPO_INCIDENCIA_ABR = CeC.Convierte2String(DR["TIPO_INCIDENCIA_ABR"]);
                string TIPO_INCIDENCIA_RESUMEN = CeC.Convierte2String(DR["TIPO_INCIDENCIA_RESUMEN"]);
                string INCIDENCIA_COMENTARIO = CeC.Convierte2String(DR["INCIDENCIA_COMENTARIO"]);
                string INCIDENCIA_EXTRAS = CeC.Convierte2String(DR["INCIDENCIA_EXTRAS"]);
                string USUARIO_NOMBRE = CeC.Convierte2String(DR["USUARIO_NOMBRE"]);
                DateTime PERSONA_DIARIO_FECHA = CeC.Convierte2DateTime(DR["PERSONA_DIARIO_FECHA"]);
                int INCIDENCIA_ID = CeC.Convierte2Int(DR["INCIDENCIA_ID"]);
                int PERSONA_LINK_ID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
                string PERSONA_NOMBRE = CeC.Convierte2String(DR["PERSONA_NOMBRE"]);


                if (INCIDENCIA_ID > UltimoIncidenciaID)
                    UltimoIncidenciaID = INCIDENCIA_ID;

                if (TIPO_INCIDENCIA_ID != TIPO_INCIDENCIA_ID_Anterior)
                {
                    Cuerpo += "</table>";
                    if (TIPO_INCIDENCIA_ID_Anterior > 0)
                        CeC_Mails.EnviarMail(eMails, Titulo, Cuerpo);
                    TIPO_INCIDENCIA_ID_Anterior = TIPO_INCIDENCIA_ID;

                    Titulo = "Resumen de incidencias de tipo " + TIPO_INCIDENCIA_NOMBRE;
                    eMails = TIPO_INCIDENCIA_RESUMEN;
                    Cuerpo = "Al momento (" + DateTime.Now.ToString() + ") " + CeC.SaltoLineaHtml;
                    Cuerpo += "Se las siguientes personas se les asigno una incidencia de tipo " + TIPO_INCIDENCIA_NOMBRE + ":";
                    Cuerpo += "<table style=\"width:100%;\">";
                    Cuerpo += "<tr>";
                    Cuerpo += "<td>Empleado</td>";
                    Cuerpo += "<td>Fecha</td>";
                    Cuerpo += "<td>Datos</td>";
                    Cuerpo += "</tr>";
                }
                Cuerpo += "<tr>";
                Cuerpo += "<td>" + PERSONA_NOMBRE + " (" + CeC.Convierte2String(PERSONA_LINK_ID) + ")</td>";
                Cuerpo += "<td>" + PERSONA_DIARIO_FECHA.ToShortDateString() + "</td>";
                Cuerpo += "<td>" + INCIDENCIA_COMENTARIO + "</td>";
                Cuerpo += "</tr>";
            }
            Cuerpo += "</table>";
            if (TIPO_INCIDENCIA_ID_Anterior > 0)
                CeC_Mails.EnviarMail(eMails, Titulo, Cuerpo);
            CeC_Config.IncidenciasResumenUltimoIncidenciaID = UltimoIncidenciaID;
        }

        return Enviados;
    }


    

}
