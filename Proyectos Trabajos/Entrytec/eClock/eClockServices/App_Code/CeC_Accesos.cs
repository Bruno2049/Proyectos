using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Reflection;
using System.Data.OleDb;
/// <summary>
/// Descripción breve de CeC_Accesos
/// </summary>
public class CeC_Accesos : CeC_Tabla
{
    int m_Acceso_Id = 0;
    [Description("Identificador unico de registro")]
    [DisplayNameAttribute("Acceso_Id")]
    public int Acceso_Id { get { return m_Acceso_Id; } set { m_Acceso_Id = value; } }
    int m_Persona_Id = 0;
    [Description("Indica de que persona se refiere el acceso")]
    [DisplayNameAttribute("Persona_Id")]
    public int Persona_Id { get { return m_Persona_Id; } set { m_Persona_Id = value; } }
    int m_Terminal_Id = 0;
    [Description("Terminal en la que ocurrio dicho acceso, si es 0 significa que fue una justificacion")]
    [DisplayNameAttribute("Terminal_Id")]
    public int Terminal_Id { get { return m_Terminal_Id; } set { m_Terminal_Id = value; } }
    DateTime m_Acceso_Fechahora = CeC_BD.FechaNula;
    [Description("Fecha y hora en la que ocurrio el evento")]
    [DisplayNameAttribute("Acceso_Fechahora")]
    public DateTime Acceso_Fechahora { get { return m_Acceso_Fechahora; } set { m_Acceso_Fechahora = value; } }
    int m_Tipo_Acceso_Id = 0;
    [Description("Indica el tipo de acceso")]
    [DisplayNameAttribute("Tipo_Acceso_Id")]
    public int Tipo_Acceso_Id { get { return m_Tipo_Acceso_Id; } set { m_Tipo_Acceso_Id = value; } }
    bool m_Acceso_Calculado = false;
    [Description("Indica si el acceso se ha calculado para asistencia")]
    [DisplayNameAttribute("Acceso_Calculado")]
    public bool Acceso_Calculado { get { return m_Acceso_Calculado; } set { m_Acceso_Calculado = value; } }

    public CeC_Accesos(CeC_Sesion Sesion)
        : base("EC_ACCESOS", "ACCESO_ID", Sesion)
    {

    }
    public CeC_Accesos(int AccesoID, CeC_Sesion Sesion)
        : base("EC_ACCESOS", "ACCESO_ID", Sesion)
    {
        Carga(AccesoID.ToString(), Sesion);
    }

    public static DateTime ObtenFechaHora(int Acceso_ID)
    {
        return CeC_BD.EjecutaEscalarDateTime("SELECT ACCESO_FECHAHORA FROM EC_ACCESOS WHERE ACCESO_ID = " + Acceso_ID);
    }
    public bool Nuevo(int PersonaId, int TerminalId, DateTime AccesoFechahora, int TipoAccesoId, bool AccesoCalculado,
        CeC_Sesion Sesion)
    {
        return Actualiza(-9999, PersonaId, TerminalId, AccesoFechahora, TipoAccesoId, AccesoCalculado, Sesion);

    }
    /// <summary>
    /// Actualiza los accesos de en la base de datos
    /// </summary>
    /// <param name="AccesoId">Identificador del acceso a actualizarr</param>
    /// <param name="PersonaId">Identificador de la persona que genera el acceso</param>
    /// <param name="TerminalId">Identificador de la terminal que genera el acceso</param>
    /// <param name="AccesoFechahora">Fecha y hora del acceso a ser actualizado</param>
    /// <param name="TipoAccesoId">Identificador del acceso a ser actualizado</param>
    /// <param name="AccesoCalculado">Calculara o no el acceso</param>
    /// <param name="Sesion">Sesion actual</param>
    /// <returns>Si fue actulizado(True) o no (False)</returns>
    public bool Actualiza(int AccesoId, int PersonaId, int TerminalId, DateTime AccesoFechahora, int TipoAccesoId, bool AccesoCalculado,
CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(AccesoId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Acceso_Id = AccesoId; Persona_Id = PersonaId; Terminal_Id = TerminalId; Acceso_Fechahora = AccesoFechahora; Tipo_Acceso_Id = TipoAccesoId; Acceso_Calculado = AccesoCalculado;

            if (Guarda(Sesion))
            {
                CeC_Asistencias.CambioAccesosPendientes();
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Obtine un DataSet con los datos de acceso de entrada y salida de una persona en una terminal.
    /// </summary>
    /// <param name="Persona_ID">Identificador único de la persona</param>
    /// <param name="Agrupacion">Agrupación seleccionada</param>
    /// <param name="FechaInicial">Fecha de inicio de la consulta</param>
    /// <param name="FechaFinal">Fecha de fin de la consulta</param>
    /// <param name="Usuario_ID">Identificador único del Usuario que tiene iniciada Sesión</param>
    /// <param name="OrdenarPor">Criterio de ordenamiento</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>DataSet con No.Empleado, Nombre del Empleado, Fecha y Hora de Acceso, Tipo de Acceso, Terminal donde se registro el acceso</returns>
    public static DataSet ObtenEntradaSalidaDS(int Persona_ID, string TerminalAgrupacion, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, int Usuario_ID, CeC_Sesion Sesion)
    {
        string where = null;
        if (TerminalAgrupacion == "MostrarTodasTerminales")
        {
            where = " WHERE ";
        }
        else
        {
            where = " WHERE EC_TERMINALES.TERMINAL_AGRUPACION = '" + TerminalAgrupacion + "' AND ";
        }
        string OrdenarPor = "TERMINAL_AGRUPACION, EC_PERSONAS.PERSONA_LINK_ID, EC_ACCESOS.ACCESO_FECHAHORA";
        string Qry = "SELECT    EC_TERMINALES.TERMINAL_AGRUPACION, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE,  EC_ACCESOS.ACCESO_FECHAHORA, " +
                              " EC_TIPO_ACCESOS.TIPO_ACCESO_ID, EC_TERMINALES.TERMINAL_NOMBRE" +
                        " FROM    EC_TIPO_ACCESOS INNER JOIN " +
                                " EC_ACCESOS ON EC_TIPO_ACCESOS.TIPO_ACCESO_ID = EC_ACCESOS.TIPO_ACCESO_ID INNER JOIN " +
                                " EC_PERSONAS ON EC_ACCESOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN " +
                                " EC_TERMINALES ON EC_ACCESOS.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID " +
                        where +
            //" WHERE EC_TERMINALES.TERMINAL_AGRUPACION = '" + TerminalAgrupacion + "' AND " +
                        " (EC_ACCESOS.ACCESO_FECHAHORA >= @FECHA_INICIAL@) AND  " +
                        " (EC_ACCESOS.ACCESO_FECHAHORA < @FECHA_FINAL@) AND EC_PERSONAS." +
                        CeC_Asistencias.ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, true) +
                        CeC_Config.MostrarESTerminalesAsistencia +
                        " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Usuario_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Obtiene Los ids de las personas accesadas
    /// </summary>
    /// <param name="AccesosIds">Cadena con los id´s de los accesos</param>
    /// <returns>Cadena con los id´s de las personas accesadas</returns>
    public static string ObtenPersonasDiarioIDS(string AccesosIds)
    {
        string Qry = "SELECT PERSONA_DIARIO_ID FROM EC_PERSONAS_DIARIO WHERE ACCESO_E_ID IN (" + AccesosIds + ") OR ACCESO_S_ID  IN  (" + AccesosIds + ") OR ACCESO_CS_ID  IN  (" + AccesosIds + ") OR ACCESO_CR_ID  IN (" + AccesosIds + ")";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return "";
        string PersonasIDs = "";
        foreach (DataRow DR in DS.Tables[0].Rows)
        {
            PersonasIDs = CeC.AgregaSeparador(PersonasIDs, CeC.Convierte2String(DR["PERSONA_DIARIO_ID"]), ",");
        }
        return PersonasIDs;
    }
    /// <summary>
    /// Funcion que borra los accesos 
    /// </summary>
    /// <param name="AccesosIds">Cadena de con los id´s de los accesos a borrar</param>
    /// <returns>Si fueron borrados(True) si no (False)</returns>
    public static bool BorrarAccesos(string AccesosIds)
    {
        string PersonasDiarioIDS = ObtenPersonasDiarioIDS(AccesosIds);

        string Qry = "UPDATE EC_ACCESOS SET PERSONA_ID = 0 WHERE ACCESO_ID IN (" + AccesosIds + ")";
        if (CeC_BD.EjecutaComando(Qry) <= 0)
            return false;

        CeC_Asistencias.RecalculaAsistencia(PersonasDiarioIDS, "");
        return true;
    }
    /// <summary>
    /// Funcion que guarda los registros accesos
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona que accede</param>
    /// <param name="TerminalID">Identificador de la terminal que registra</param>
    /// <param name="TipoAccesoID">Identificador del tipo de acceso</param>
    /// <param name="FechaHora">Fecha y hora del acceso</param>
    /// <returns></returns>
    public static int AgregarAcceso(int PersonaID, int TerminalID, int TipoAccesoID, DateTime FechaHora)
    {
        return AgregarAcceso(PersonaID, TerminalID, TipoAccesoID, FechaHora, 0, 0);
    }
    /// <summary>
    /// Funcion que se encarga de consultar si tiene derecho a comida
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona consultada</param>
    /// <param name="Fecha">Fecha</param>
    /// <returns>Un entero que define si tiene o no derecho</returns>
    public static int TienePrimeraComida(int PersonaID, DateTime Fecha)
    {
        CIsLog2.AgregaLog("TienePrimeraComida " + PersonaID);
        return CeC_BD.EjecutaEscalarInt("SELECT TIPO_COMIDA_ID FROM EC_PERSONAS_COMIDA WHERE PERSONA_COMIDA_FECHA BETWEEN " + CeC_BD.SqlFecha(Fecha) + " AND " + CeC_BD.SqlFecha(Fecha.AddDays(1)) + " AND PERSONA_ID = " + PersonaID.ToString());
    }
    /// <summary>
    /// Fuincion que guarda un registro con la fecha de la comida
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona</param>
    /// <param name="TipoComidaID">Identificador del tipo de de comida</param>
    /// <param name="TipoCobroID">Identificador del tipo de cobro que tendra la comida</param>
    /// <param name="Fecha">Fecha de la comida</param>
    /// <returns>Si guardo(True) o si no (False)</returns>
    public static bool AgregarComidaFecha(int PersonaID, int TipoComidaID, int TipoCobroID, DateTime Fecha)
    {
        CIsLog2.AgregaLog("AgregarComida " + PersonaID + " " + TipoComidaID + " " + TipoCobroID);
        decimal Costo = CeC_BD.EjecutaEscalarDecimal("SELECT TIPO_COMIDA_COSTO FROM EC_TIPO_COMIDA WHERE TIPO_COMIDA_ID = " + TipoComidaID.ToString() + "");
        if (CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_COMIDA(PERSONA_COMIDA_ID,PERSONA_ID,TIPO_COMIDA_ID,TIPO_COBRO_ID,PERSONA_COMIDA_FECHA,SESION_ID,TIPO_COMIDA_COSTOA) VALUES(" +
            CeC_Autonumerico.GeneraAutonumerico("PERSONA_COMIDA_ID", "EC_PERSONAS_COMIDA").ToString() + "," + PersonaID.ToString() + "," + TipoComidaID.ToString() + "," + TipoCobroID.ToString() + "," + CeC_BD.SqlFecha(Fecha) + ",0," + Costo.ToString() + ")") > 0)
            return true;
        return false;
    }
    /// <summary>
    /// Funcion que agrega fecha de la comida
    /// </summary>
    /// <param name="PersonaID">Identificador de la persona</param>
    /// <param name="TipoCobroID">Identificador del tipo de cobro</param>
    /// <param name="Fecha">Fecha a agregar</param>
    /// <returns></returns>
    public static bool AgregarComidaFecha(int PersonaID, int TipoCobroID, DateTime Fecha)
    {
        bool Primera = Convert.ToBoolean(TienePrimeraComida(PersonaID, Fecha) > 0);
        return AgregarComidaFecha(PersonaID, Primera ? 2 : 1, TipoCobroID, Fecha);
    }
    /// <summary>
    /// Guarda un registro de Acceso en el Sistema
    /// </summary>
    /// <param name="PersonaID">ID de la Persona a la que corresponde el Acceso</param>
    /// <param name="TerminalID">ID de la Terminal en que se registro el Acceso</param>
    /// <param name="TipoAccesoID">Tipo de Acceso</param>
    /// <param name="FechaHora">Fecha y Hora del Acceso</param>
    /// <param name="SesionID">ID de Sesion</param>
    /// <param name="SuscripcionID">ID de la Suscripcion</param>
    /// <returns>Regresa el ID el Acceso generado</returns>
    public static int AgregarAcceso(int PersonaID, int TerminalID, int TipoAccesoID, DateTime FechaHora, int SesionID, int SuscripcionID)
    {
        int AccesoID = CeC_BD.EjecutaEscalarInt("SELECT ACCESO_ID FROM EC_ACCESOS WHERE PERSONA_ID = " + PersonaID.ToString() +
            " AND TERMINAL_ID =   " + TerminalID.ToString() + " AND ACCESO_FECHAHORA = " + CeC_BD.SqlFechaHora(FechaHora) +
            " AND TIPO_ACCESO_ID = " + TipoAccesoID.ToString() + "");
        if (AccesoID > 0)
            return AccesoID;
        CeC_Terminales_Param Parm = new CeC_Terminales_Param(TerminalID);
        int ACCESO_CALCULADO = 0;// Parm.TERMINAL_ASISTENCIA ? 0 : 1;
        AccesoID = CeC_Autonumerico.GeneraAutonumerico("EC_ACCESOS", "ACCESO_ID", SesionID, SuscripcionID);
        string qry = "INSERT INTO EC_ACCESOS(ACCESO_ID,PERSONA_ID,TERMINAL_ID,ACCESO_FECHAHORA,TIPO_ACCESO_ID,ACCESO_CALCULADO) VALUES("
            + AccesoID.ToString() + "," + PersonaID.ToString() + "," + TerminalID.ToString() + "," + CeC_BD.SqlFechaHora(FechaHora) + "," +
            TipoAccesoID.ToString() + "," + ACCESO_CALCULADO + ")";
        int ret = CeC_BD.EjecutaComando(qry);
        if (ret > 0)
        {
            if (Parm.TERMINAL_COMIDA)
            {
                AgregarComidaFecha(PersonaID, 0, FechaHora);
                //eClock.WS_Monedero WSMonedero = new eClock.WS_Monedero();

                //bool Primera = Convert.ToBoolean(WSMonedero.TienePrimeraComida(PersonaID));
                //if (Primera == true)
                //    WSMonedero.AgregarComidaFecha(PersonaID, 2, 0, FechaHora);
                //else
                //    WSMonedero.AgregarComidaFecha(PersonaID, 1, 0, FechaHora);

            }
            if (ACCESO_CALCULADO == 0)
                CeC_Asistencias.CambioAccesosPendientes();
            return AccesoID;
        }
        else
            return AccesoID;
    }
    /// <summary>
    /// Guarda un registro de Acceso al sistema
    /// </summary>
    /// <param name="TerminalID">ID de la Terminal en que ocurrio la checada</param>
    /// <param name="Llave">Contenido del campo llave. Por ejemplo PERSONA_LINK_ID</param>
    /// <param name="FechaHora">Fecha y Hora del Acceso</param>
    /// <param name="TAcceso">Tipo de Acceso</param>
    /// <param name="SesionID">ID de la Sesion</param>
    /// <param name="SuscripcionID">ID de la Suscripcion</param>
    /// <param name="AgregarInmediatamente">TRUE Indica si se debe agregar la checada al instante. FALSE manda la checada a Terminales_DExtras </param>
    /// <returns>TRUE si se agrego el registro correctamente. FALSE en caso de error al agregar el registro.</returns>
    public static bool AgregaChecada(int TerminalID, string Llave, DateTime FechaHora, int TAcceso, int SesionID, int SuscripcionID, bool AgregarInmediatamente)
    {

        int Persona_ID = 0;
        if (AgregarInmediatamente)
        {
            Persona_ID = CeC_Personas.ObtenPersonaIDdCampoLlave(TerminalID, Llave);
            if (Persona_ID <= 0)
                Persona_ID = CeC_Personas.ObtenPersonaIDdCampoAdicional(TerminalID, Llave);
            //                Persona_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_TERMINALES WHERE PERSONA_TERMINAL_CAMPO_A = '" + Llave + "' AND TERMINAL_ID = " + TerminalID);

            if (Persona_ID > 0)
                if (AgregarAcceso(Persona_ID, TerminalID, Convert.ToInt32(TAcceso), FechaHora, SesionID, SuscripcionID) > 0)
                    return true;
        }
        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-MX");

        bool Ret = CeC_Terminales_DExtras.AgregaDExtra(TerminalID, 1, SesionID, SuscripcionID, Llave, FechaHora.ToString(culture) + ";" + Convert.ToInt32(TAcceso).ToString(), null);
        CeC_Asistencias.CambioAccesosNAsign();
        return Ret;
    }
    /// <summary>
    /// Procesa los accesos viejos
    /// </summary>
    /// <returns></returns>
    public static int ProcesaAccesosViejos()
    {
        int Accesos = 0;
        /*     
             OleDbConnection Conexion = null;
             try
             {

                 DS_AsistenciasN DS = new DS_AsistenciasN();
                 DS_AsistenciasNTableAdapters.EC_TERMINALES_DEXTRASTableAdapter TA = new DS_AsistenciasNTableAdapters.EC_TERMINALES_DEXTRASTableAdapter();
                 TA.Fill(DS.EC_TERMINALES_DEXTRAS, 1);

                 // DR = (OleDbDataReader)CeC_BD.EjecutaReader(
                 //     "SELECT TERMINALES_DEXTRAS_ID, TERMINALES_DEXTRAS_TEXTO1, TERMINALES_DEXTRAS_TEXTO2,TERMINAL_ID " +
                 //    " FROM EC_TERMINALES_DEXTRAS WHERE TIPO_TERM_DEXTRAS_ID =" +
                 //Convert.ToInt32(Tipo_DEXTRAS.Accesos) + " AND TERMINAL_ID = " + TERMINAL_ID + " and TERMINALES_DEXTRAS_TEXTO1 in('1','5','6','1165') ORDER BY  TERMINALES_DEXTRAS_TEXTO2");
                 //                                    Convert.ToInt32(Tipo_DEXTRAS.Accesos) + " AND TERMINAL_ID = " + TERMINAL_ID + " and TERMINALES_DEXTRAS_TEXTO1 in('1','5','6','14','15','19','23','160','2149','515','1165') ORDER BY  TERMINALES_DEXTRAS_TEXTO2");
                 //                    Convert.ToInt32(Tipo_DEXTRAS.Accesos) + " AND TERMINAL_ID = " + TERMINAL_ID + " and TO_DATE(substr(TERMINALES_DEXTRAS_TEXTO2,3,10),'DD/MM/YYYY') > to_DATE('01/01/2006','DD/MM/YYYY') ORDER BY  TERMINALES_DEXTRAS_TEXTO2");
                 //    Convert.ToInt32(Tipo_DEXTRAS.Accesos) + " \n ORDER BY  TERMINALES_DEXTRAS_TEXTO2", out Conexion);
                 for (int i = 0; i < DS.EC_TERMINALES_DEXTRAS.Rows.Count; i++)
                 {
                     try
                     {
                         string ID = DS.EC_TERMINALES_DEXTRAS[i].TERMINALES_DEXTRAS_TEXTO1.ToString();

                         string Extra = DS.EC_TERMINALES_DEXTRAS[i].TERMINALES_DEXTRAS_TEXTO2.ToString();
                         int TERMINAL_ID = Convert.ToInt32(DS.EC_TERMINALES_DEXTRAS[i].TERMINAL_ID);

                         // string ID = DR.GetValue(1).ToString();
                         // string Extra = DR.GetValue(2).ToString();
                         // int TERMINAL_ID = Convert.ToInt32(DR.GetValue(3));
                         int Pos = Extra.IndexOf(';');
                         //                        CIsLog.AgregaDebug("ProcesaAccesosViejos ID = {0:G}, Extra = {1:G}", ID, Extra);
                         int TAcceso = Convert.ToInt32(Extra.Substring(Pos + 1));
                         System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-MX");

                         DateTime FechaHora = Convert.ToDateTime(Extra.Substring(0, Pos), culture);
                         CeC.Sleep(0);

                         int Persona_ID = CeC_Personas.ObtenPersonaIDdCampoLlave(TERMINAL_ID, ID);
                         int SuscripcionID = 0;
                         ///Obtiene la suscripcion, comentado para mejorar velocidad
                         //SuscripcionID = CeC_Personas.ObtenSuscripcionID(Persona_ID);
                         if (Persona_ID > 0)
                         {
                             if (AgregarAcceso(Persona_ID, TERMINAL_ID, TAcceso, FechaHora, 0, Persona_ID) > 0)
                             {
                                 CeC.Sleep(0);
                                 CIsLog2.AgregaDebug("BorraTERMINALES_DEXTRAS TeraminalDExtrasID = {2:G} ID = {0:G}, Extra = {1:G}", ID, Extra, DS.EC_TERMINALES_DEXTRAS[i].TERMINALES_DEXTRAS_ID);
                                 CeC_Terminales_DExtras.Borra(CeC.Convierte2Int(DS.EC_TERMINALES_DEXTRAS[i].TERMINALES_DEXTRAS_ID));
                                 Accesos++;

                             }
                         }
                         else
                             if (FechaHora < DateTime.Now.AddMonths(-1))
                             {
                                 ///Se borran los registros anteriores a 2 meses pero se pasan antes a una tabla de respaldos
                                 CeC_Terminales_DExtras.PasaARespaldo(CeC.Convierte2Int(DS.EC_TERMINALES_DEXTRAS[i].TERMINALES_DEXTRAS_ID));
                             }

                     }
                     catch (Exception ex)
                     {
                         CIsLog2.AgregaError("TERMINALES_DEXTRAS_ID " + DS.EC_TERMINALES_DEXTRAS[i].TERMINALES_DEXTRAS_ID + ex.Message);
                     }
                     CeC.Sleep(5);
                 }
                 //            DR.Close();

             }
             catch (Exception ex)
             {
                 CIsLog2.AgregaError(ex);
             }
             try
             {
                 //Conexion.Close();
                 //Conexion.Dispose();
             }
             catch (Exception ex)
             {
                 CIsLog2.AgregaError(ex);
             }*/

        string Qry = "SELECT	EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_ID, EC_V_PERSONAS_TERMINALES.TERMINAL_ID, EC_V_PERSONAS_TERMINALES.PERSONA_ID,  \n" +
                " 	EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO2, EC_TERMINALES_DEXTRAS.TIPO_TERM_DEXTRAS_ID, EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO1 \n" +
                " FROM	EC_V_PERSONAS_TERMINALES INNER JOIN \n" +
                " 	EC_TERMINALES_DEXTRAS ON EC_V_PERSONAS_TERMINALES.DATO_LLAVE = EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO1 AND  \n" +
                " 	EC_V_PERSONAS_TERMINALES.TERMINAL_ID = EC_TERMINALES_DEXTRAS.TERMINAL_ID \n" +
                " WHERE	(EC_TERMINALES_DEXTRAS.TIPO_TERM_DEXTRAS_ID = 1) \n" +
                " \n ORDER BY EC_TERMINALES_DEXTRAS.TERMINALES_DEXTRAS_TEXTO2";
        int TTimeOutAnt = CeC_BD.TimeOutSegundos;
        CeC_BD.TimeOutSegundos = 3600;
        DataSet DS = CeC_BD.EjecutaDataSet(Qry);
        CeC_BD.TimeOutSegundos = TTimeOutAnt;
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            int TERMINALES_DEXTRAS_ID = 0;
            int PersonaID = 0;
            int TERMINAL_ID = 0;
            string TERMINALES_DEXTRAS_TEXTO1 = "";
            string TERMINALES_DEXTRAS_TEXTO2 = "";

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                try
                {
                    TERMINALES_DEXTRAS_ID = CeC.Convierte2Int(DR["TERMINALES_DEXTRAS_ID"]);
                    PersonaID = CeC.Convierte2Int(DR["PERSONA_ID"]);
                    TERMINAL_ID = CeC.Convierte2Int(DR["TERMINAL_ID"]);
                    TERMINALES_DEXTRAS_TEXTO1 = CeC.Convierte2String(DR["TERMINALES_DEXTRAS_TEXTO1"]);
                    TERMINALES_DEXTRAS_TEXTO2 = CeC.Convierte2String(DR["TERMINALES_DEXTRAS_TEXTO2"]);

                    string Extra = TERMINALES_DEXTRAS_TEXTO2;
                    // string ID = DR.GetValue(1).ToString();
                    // string Extra = DR.GetValue(2).ToString();
                    // int TERMINAL_ID = Convert.ToInt32(DR.GetValue(3));
                    int Pos = Extra.IndexOf(';');
                    //                        CIsLog.AgregaDebug("ProcesaAccesosViejos ID = {0:G}, Extra = {1:G}", ID, Extra);
                    int TAcceso = Convert.ToInt32(Extra.Substring(Pos + 1));
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-MX");

                    DateTime FechaHora = Convert.ToDateTime(Extra.Substring(0, Pos), culture);
                    CeC.Sleep(0);

                    if (AgregarAcceso(PersonaID, TERMINAL_ID, TAcceso, FechaHora) > 0)
                    {
                        CeC.Sleep(0);
                        CIsLog2.AgregaDebug("BorraTERMINALES_DEXTRAS TeraminalDExtrasID = {2:G} ID = {0:G}, Extra = {1:G}", PersonaID, Extra, TERMINALES_DEXTRAS_ID);
                        CeC_Terminales_DExtras.Borra(TERMINALES_DEXTRAS_ID);
                        Accesos++;

                    }


                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError("TERMINALES_DEXTRAS_ID " + TERMINALES_DEXTRAS_ID + " " + TERMINALES_DEXTRAS_TEXTO2 + " " + ex.Message);
                }
            }
        }

        return Accesos;
    }
    /// <summary>
    /// El campo llave para accesos es ACCESO_ID
    /// </summary>
    /// <param name="Persona_ID">dejar en blanco si no se conoce la persona</param>
    /// <param name="Agrupacion">Dejar en blanco si no se conoce la agrupacion</param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static DataSet ObtenAccesos(bool MuestraAgrupacion, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string TerminalesIDs, string TipoAccesosIds, int Usuario_ID)
    {
        string Campos = "";
        string OrdenarPor = "";
        if (MuestraAgrupacion)
            Campos += "AGRUPACION_NOMBRE,";
        if (Persona_ID > 0)
        {
            Campos += "ACCESO_FECHAHORA, TIPO_ACCESO_NOMBRE, TERMINAL_NOMBRE, ACCESO_CALCULADO,TIPO_ACCESO_ID";
            OrdenarPor = "ACCESO_FECHAHORA";
        }
        else
        {
            Campos += "PERSONA_LINK_ID, PERSONA_NOMBRE, ACCESO_FECHAHORA, TIPO_ACCESO_NOMBRE, TERMINAL_NOMBRE, ACCESO_CALCULADO,TIPO_ACCESO_ID";
            OrdenarPor = "ACCESO_FECHAHORA";
        }

        string Qry = " SELECT     ACCESO_ID,  " + Campos + " " +
        " FROM " + ObtenVistaAccesos(FechaInicial, FechaFinal, TerminalesIDs, TipoAccesosIds) +
        " WHERE 1=1 " +
        CeC_Asistencias.ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, false) +
        " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        return DS;
    }

    /// <summary>
    /// El campo llave para accesos es ACCESO_ID
    /// </summary>
    /// <param name="Persona_ID">dejar en blanco si no se conoce la persona</param>
    /// <param name="Agrupacion">Dejar en blanco si no se conoce la agrupacion</param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="Usuario_ID"></param>
    /// <returns></returns>
    public static DataSet ObtenAccesosV5(bool MuestraAgrupacion, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string TerminalesIDs, string TipoAccesosIds, int Usuario_ID)
    {
        string Campos = "";
        string OrdenarPor = "";
        if (MuestraAgrupacion)
            Campos += "AGRUPACION_NOMBRE,";

        Campos += "PERSONA_LINK_ID, PERSONA_NOMBRE, ACCESO_FECHAHORA, TIPO_ACCESO_NOMBRE, TERMINAL_NOMBRE, ACCESO_CALCULADO,TIPO_ACCESO_ID";
        OrdenarPor = "ACCESO_FECHAHORA";


        string Qry = " SELECT     ACCESO_ID,  " + Campos + " " +
        " FROM " + ObtenVistaAccesos(FechaInicial, FechaFinal, TerminalesIDs, TipoAccesosIds) +
        " WHERE 1=1 " +
        CeC_Asistencias.ValidaAgrupacion(Persona_ID, Usuario_ID, Agrupacion, false) +
        " \n ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        return DS;
    }
    /// <summary>
    /// Obtiene la coleccion o vista de los accesos en un intervalo de tiempo
    /// </summary>
    /// <param name="FechaInicial">Fecha inicial del periodo</param>
    /// <param name="FechaFinal">Fecha final del periodo</param>
    /// <param name="TerminalesIDs">Cadena con los identificadores de las terminales a consultar</param>
    /// <param name="TipoAccesosIds">Cadena con los identificadores de los tipos de accesos a consultar</param>
    /// <returns></returns>
    public static string ObtenVistaAccesos(DateTime FechaInicial, DateTime FechaFinal, string TerminalesIDs, string TipoAccesosIds)
    {
        string QryTerminales = TerminalesIDs == null || TerminalesIDs.Length < 1 ? "" : " AND TERMINAL_ID IN ( " + TerminalesIDs + ") ";
        string QryTipoAccesos = TipoAccesosIds == null || TipoAccesosIds.Length < 1 ? "" : " AND TIPO_ACCESO_ID IN ( " + TipoAccesosIds + ") ";

        string Vista = "(SELECT ACCESO_ID " +
        ",PERSONA_ID " +
        ",SUSCRIPCION_ID " +
        ",PERSONA_LINK_ID " +
        ",PERSONA_NOMBRE " +
        ",TERMINAL_NOMBRE " +
        ",TIPO_ACCESO_NOMBRE " +
        ",ACCESO_FECHAHORA " +
        ",ACCESO_CALCULADO " +
        ",AGRUPACION_NOMBRE,TIPO_ACCESO_ID " +
        "FROM (" +
        "SELECT     ACCESOS.ACCESO_ID, EC_PERSONAS.PERSONA_ID, EC_PERSONAS.SUSCRIPCION_ID, EC_PERSONAS.PERSONA_LINK_ID,  " +
        "EC_PERSONAS.PERSONA_NOMBRE, EC_TERMINALES.TERMINAL_NOMBRE, EC_TIPO_ACCESOS.TIPO_ACCESO_NOMBRE,  " +
        "ACCESOS.ACCESO_FECHAHORA, ACCESOS.ACCESO_CALCULADO, EC_PERSONAS.AGRUPACION_NOMBRE,ACCESOS.TIPO_ACCESO_ID " +
        "FROM         EC_PERSONAS INNER JOIN " +
        "(SELECT     ACCESO_ID, PERSONA_ID, TERMINAL_ID, ACCESO_FECHAHORA, TIPO_ACCESO_ID, ACCESO_CALCULADO " +
        "FROM          EC_ACCESOS " +
        " WHERE     ACCESO_FECHAHORA >= @FECHA_INICIAL@ AND  " +
        " ACCESO_FECHAHORA < @FECHA_FINAL@ " + QryTerminales + QryTipoAccesos;

        if (CeC_BD.EsOracle)
            Vista += ") ACCESOS ON  ";
        else
            Vista += ") AS ACCESOS ON  ";

        Vista += "EC_PERSONAS.PERSONA_ID = ACCESOS.PERSONA_ID INNER JOIN " +
        "EC_TIPO_ACCESOS ON ACCESOS.TIPO_ACCESO_ID = EC_TIPO_ACCESOS.TIPO_ACCESO_ID INNER JOIN " +
        "EC_TERMINALES ON ACCESOS.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID " +
        ")";
        Vista = CeC_BD.AsignaParametro(Vista, "FECHA_INICIAL", FechaInicial);
        Vista = CeC_BD.AsignaParametro(Vista, "FECHA_FINAL", FechaFinal);
        if (!CeC_BD.EsOracle)
            Vista += "ta";
        Vista += ")";
        if (!CeC_BD.EsOracle)
            Vista += "V_Accesos";

        return Vista;

    }
    /// <summary>
    /// Obtiene el ID del Tipo de Acceso para Anviz
    /// </summary>
    /// <param name="TipoAcceso">Valor del Tipo de Acceso (I,O)</param>
    /// <returns>0	No definido
    /// 1	Correcto
    /// 2	Entrada
    /// 3	Salida
    /// 4	Salida a Comer
    /// 5	Regreso de comer
    /// 6	Incorrecto
    /// 7	Regreso Descanso
    /// 8	Regreso Llamada</returns>
    public static int ObtenTipoAccesoAnviz(string TipoAcceso)
    {
        int TipoAccesoID = 0;
        switch (TipoAcceso)
        {
            case "I":
                TipoAccesoID = 2;
                break;
            case "O":
                TipoAccesoID = 3;
                break;
            default:
                TipoAccesoID = 1;
                break;
        }
        return TipoAccesoID;
    }
}