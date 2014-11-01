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

/// <summary>
/// Descripción breve de Cec_Incidencias
/// </summary>
public class Cec_Incidencias
{
    public Cec_Incidencias()
    {

    }
    public static int CreaIncidencia(int TipoIncidenciaID, string PersonasDiariosIDs, string IncidenciaComentario, CeC_Sesion Sesion)
    {
        string[] sPersonasDiariosIDs = CeC.ObtenArregoSeparador(PersonasDiariosIDs, ",");
        if (sPersonasDiariosIDs.Length <= 0)
            return -1;

        int PersonaDiarioID = CeC.Convierte2Int(sPersonasDiariosIDs[0]);
        int TipoIncidencia_R_ID = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_ID_DS(TipoIncidenciaID, PersonaDiarioID);

        if (TipoIncidencia_R_ID > 0)
        {
            return CeC_IncidenciasRegla.AsignaIncidencia(TipoIncidencia_R_ID, PersonasDiariosIDs, IncidenciaComentario, Sesion.SESION_ID);
        }
        else
        {
            int IncidenciaID = -1;
            ///Borrar Incidencia
            if (TipoIncidenciaID == -1)
                IncidenciaID = -1;
            else
                IncidenciaID = CreaIncidencia(TipoIncidenciaID, IncidenciaComentario, Sesion.SESION_ID);

            if (IncidenciaID >= 0)
            {

                foreach (string sPersonaDiarioID in sPersonasDiariosIDs)
                {
                    try
                    {
                        PersonaDiarioID = CeC.Convierte2Int(sPersonaDiarioID);
                        AsignaIncidencia(PersonaDiarioID, IncidenciaID, Sesion.SESION_ID);
                    }
                    catch { }
                }
            }
            return IncidenciaID;
        }
    }
    /// <summary>
    /// Crea una nueva incidencia
    /// </summary>
    /// <param name="TipoIncidenciaID">Identificador del Tipo de Incidencia</param>
    /// <param name="IncidenciaComentario">Comentario para la Incidencia</param>
    /// <param name="SesionID">Identificador de la Sesion</param>
    /// <returns>Identificador de la Incidencia(Autonumerico)</returns>
    public static int CreaIncidencia(int TipoIncidenciaID, string IncidenciaComentario, int SesionID)
    {
        if (TipoIncidenciaID <= 0)
            return -1;
        if (SesionID < 0)
            return -2;
        // Genera un Autonumerico para la Incidencia.
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
    public static int AsignaIncidencia(DateTime FechaInicial, DateTime FechaFinal, int Persona_ID, int IncidenciaID)
    {
        return AsignaIncidencia(FechaInicial, FechaFinal, Persona_ID, IncidenciaID, 0);
    }
    public static int AsignaIncidencia(DateTime FechaInicial, DateTime FechaFinal, int Persona_ID, int IncidenciaID, int Sesion_ID)
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
        int R = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET INCIDENCIA_ID = " +
            IncidenciaID + " WHERE PERSONA_ID = " + Persona_ID + " AND PERSONA_DIARIO_FECHA >=" +
            CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(FechaFinal));
        if (R > 0)
        {
            if (Sesion_ID > 0)
            {

                int Persona_Link_ID = CeC_Empleados.ObtenPersona_Link_ID(Persona_ID);
                string Descripcion = "";
                Descripcion += "NoEmpleado = " + Persona_Link_ID + ", Fecha Desde = " + FechaInicial + " Hasta =" + FechaFinal.AddDays(-1) + ", IncidenciaID = " + IncidenciaID;
                CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Justificacion Dia", Persona_ID, Descripcion, Sesion_ID);
            }

            return 1;
        }
        return -9999;
    }
    public static int AsignaIncidencia(int Persona_Diario_ID, int IncidenciaID)
    {
        return AsignaIncidencia(Persona_Diario_ID, IncidenciaID, 0);
    }
    public static string ObtenIncidenciaDesc(int IncidenciaID)
    {
        //     CeC_BD.EjecutaEscalarString("SELECT 
        return "";
    }
    public static int AsignaIncidencia(int Persona_Diario_ID, int IncidenciaID, int Sesion_ID)
    {
        if (Persona_Diario_ID <= 0)
            return -2;
        if (IncidenciaID < 0)
            return -3;
        CeC_IncidenciasInventario.CorrigeMovimiento(Persona_Diario_ID, Sesion_ID, 0);
        int R = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET INCIDENCIA_ID = " +
                IncidenciaID + " WHERE PERSONA_DIARIO_ID = " + Persona_Diario_ID);
        if (R > 0)
        {
            if (Sesion_ID > 0)
            {

                int Persona_Link_ID = CeC_Empleados.ObtenPersona_Link_ID(CeC_Asistencias.ObtenPersonaID(Persona_Diario_ID));
                string Descripcion = "";
                Descripcion += "NoEmpleado = " + Persona_Link_ID + ", Fecha = " + CeC_Asistencias.ObtenFecha(Persona_Diario_ID).ToShortDateString() + ", IncidenciaID = " + IncidenciaID;
                CeC_Sesion.SAgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION, "Justificacion Dia", Persona_Diario_ID, Descripcion, Sesion_ID);
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
    public static int ObtenTipoIncidenciaID(int Suscripcion_ID, string Nombre)
    {
        string ADD = " TIPO_INCIDENCIA_ID IN (";
        ADD += " SELECT     EC_AUTONUM.AUTONUM_TABLA_ID AS TIPO_INCIDENCIA_ID " +
                "FROM         EC_AUTONUM " +
                " WHERE     (EC_AUTONUM.AUTONUM_TABLA = 'EC_TIPO_INCIDENCIAS') AND (SUSCRIPCION_ID  = " + Suscripcion_ID + "))";

        return CeC_BD.EjecutaEscalarInt("SELECT TIPO_INCIDENCIA_ID FROM EC_TIPO_INCIDENCIAS WHERE TIPO_INCIDENCIA_NOMBRE = '" + CeC_BD.ObtenParametroCadena(Nombre) + "' AND " + ADD);
    }

    public static int ObtenTipoIncidenciaEXID(int Suscripcion_ID, string IDExtTXT)
    {
        return CeC_BD.EjecutaEscalarInt("SELECT TIPO_INCIDENCIAS_EX_ID FROM EC_TIPO_INCIDENCIAS_EX WHERE TIPO_INCIDENCIAS_EX_TXT = '" + CeC_BD.ObtenParametroCadena(IDExtTXT) + "' AND " + CeC_Autonumerico.ValidaSuscripcion("EC_TIPO_INCIDENCIAS_EX", "TIPO_INCIDENCIAS_EX_ID", Suscripcion_ID));
    }

    public static int ObtenTipoIncidenciaID(string Abreviatura)
    {
        try 
        {
            return CeC.Convierte2Int(CeC_BD.EjecutaEscalar("SELECT TIPO_INCIDENCIA_ID  FROM EC_TIPO_INCIDENCIAS WHERE TIPO_INCIDENCIA_ABR = '" + Abreviatura + "'"));
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return 0;
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

    public static int TipoIncidenciaAgrega(CeC_Sesion Sesion, string Nombre, string Abreviatura)
    {
        return TipoIncidenciaAgrega(Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, Nombre, Abreviatura);
    }
    public static int ObtenTotalTipoIncidencias(int SuscripcionID, bool Borrados)
    {
        DataSet Ds = ObtenTiposIncidencias(SuscripcionID, Borrados);
        return Ds.Tables[0].Rows.Count;
    }
    public static void CrearTiposIncidenciasPredeterminados(int Suscripcion_ID)
    {
        TipoIncidenciaAgrega(0, Suscripcion_ID, "Vacaciones", "V");
        TipoIncidenciaAgrega(0, Suscripcion_ID, "Permiso con Goce", "PG");
        TipoIncidenciaAgrega(0, Suscripcion_ID, "Permiso sin Goce", "PS");
        TipoIncidenciaAgrega(0, Suscripcion_ID, "Tiempo por Tiempo", "TT");


    }

    public static void InsertaTipoIncidencia()
    { }
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
    public static bool QuitaIncidencia(int IncidenciaID)
    {
        if (IncidenciaID <= 0)
            return false;
        if (CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET INCIDENCIA_ID = 0 WHERE INCIDENCIA_ID = " + IncidenciaID) > 0)
            return true;
        return false;
    }
    public static int ObtenTotalAplicadas(int IncidenciaID)
    {
        return 0;
    }

}
