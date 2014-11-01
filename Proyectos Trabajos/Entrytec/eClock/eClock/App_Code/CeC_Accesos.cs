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
                        " ORDER BY " + OrdenarPor;
        Qry = CeC_BD.AsignaParametro(Qry, "USUARIO_ID", Usuario_ID);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_INICIAL", FechaInicial);
        Qry = CeC_BD.AsignaParametro(Qry, "FECHA_FINAL", FechaFinal);
        Qry = CeC_BD.AsignaParametro(Qry, "AGRUPACION_NOMBRE", Agrupacion + "%");
        return (DataSet)CeC_Asistencias.EjecutaDataSet(Qry, Sesion);
    }

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
    public static bool BorrarAccesos(string AccesosIds)
    {
        string PersonasDiarioIDS = ObtenPersonasDiarioIDS(AccesosIds);

        string Qry = "UPDATE EC_ACCESOS SET PERSONA_ID = 0 WHERE ACCESO_ID IN (" + AccesosIds + ")";
        if (CeC_BD.EjecutaComando(Qry) <= 0)
            return false;

        CeC_Asistencias.RecalculaAsistencia(PersonasDiarioIDS, "");
        return true;
    }
    public static int AgregarAcceso(int PersonaID, int TerminalID, int TipoAccesoID, DateTime FechaHora)
    {
        return AgregarAcceso(PersonaID, TerminalID, TipoAccesoID, FechaHora, 0, 0);
    }
    public static int AgregarAcceso(int PersonaID, int TerminalID, int TipoAccesoID, DateTime FechaHora, int SesionID, int SuscripcionID)
    {
        int AccesoID = CeC_BD.EjecutaEscalarInt("SELECT ACCESO_ID FROM EC_ACCESOS WHERE PERSONA_ID = " + PersonaID.ToString() +
            " AND TERMINAL_ID =   " + TerminalID.ToString() + " AND ACCESO_FECHAHORA = " + CeC_BD.SqlFechaHora(FechaHora) +
            " AND TIPO_ACCESO_ID = " + TipoAccesoID.ToString() + "");
        if (AccesoID > 0)
            return AccesoID;
        AccesoID = CeC_Autonumerico.GeneraAutonumerico("EC_ACCESOS", "ACCESO_ID", SesionID, SuscripcionID);
        string qry = "INSERT INTO EC_ACCESOS(ACCESO_ID,PERSONA_ID,TERMINAL_ID,ACCESO_FECHAHORA,TIPO_ACCESO_ID) VALUES(" + AccesoID.ToString() + "," + PersonaID.ToString() + "," + TerminalID.ToString() + "," + CeC_BD.SqlFechaHora(FechaHora) + "," + TipoAccesoID.ToString() + ")";
        int ret = CeC_BD.EjecutaComando(qry);
        if (ret > 0)
        {
            CeC_Terminales_Param Parm = new CeC_Terminales_Param(TerminalID);
            if (Parm.TERMINAL_COMIDA)
            {
                eClock.WS_Monedero WSMonedero = new eClock.WS_Monedero();

                bool Primera = Convert.ToBoolean(WSMonedero.TienePrimeraComida(PersonaID));
                if (Primera == true)
                    WSMonedero.AgregarComidaFecha(PersonaID, 2, 0, FechaHora);
                else
                    WSMonedero.AgregarComidaFecha(PersonaID, 1, 0, FechaHora);

            }
            CeC_Asistencias.CambioAccesosPendientes();
            return AccesoID;
        }
        else
            return AccesoID;
    }

    public static bool AgregaChecada(int TerminalID, string Llave, DateTime FechaHora, int TAcceso, int SesionID, int SuscripcionID, bool AgregarInmediatamente)
    {

        int Persona_ID = 0;
        if (AgregarInmediatamente)
        {
            Persona_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_TERMINALES WHERE PERSONA_TERMINAL_CAMPO_L = '" + Llave + "' AND TERMINAL_ID = " + TerminalID);
            if (Persona_ID <= 0)
                Persona_ID = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS_TERMINALES WHERE PERSONA_TERMINAL_CAMPO_A = '" + Llave + "' AND TERMINAL_ID = " + TerminalID);

            if (Persona_ID > 0)
                if (AgregarAcceso(Persona_ID, TerminalID, Convert.ToInt32(TAcceso), FechaHora, SesionID, SuscripcionID) > 0)
                    return true;
        }
        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-MX");

        bool Ret = CeC_Terminales_DExtras.AgregaDExtra(TerminalID, 1, SesionID, SuscripcionID, Llave, FechaHora.ToString(culture) + ";" + Convert.ToInt32(TAcceso).ToString(), null);
        CeC_Asistencias.CambioAccesosNAsign();
        return Ret;
    }


    public static int ProcesaAccesosViejos()
    {
        int Accesos = 0;
        OleDbConnection Conexion = null;
        try
        {

            DS_AsistenciasN DS = new DS_AsistenciasN();
            DS_AsistenciasNTableAdapters.EC_TERMINALES_DEXTRASTableAdapter TA = new DS_AsistenciasNTableAdapters.EC_TERMINALES_DEXTRASTableAdapter();
            TA.Fill(DS.EC_TERMINALES_DEXTRAS, 1);


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
                    string sFechaHora = Extra.Substring(0, Pos);
                    sFechaHora = sFechaHora.Replace(". ", "");
                    sFechaHora = sFechaHora.Replace(".", "");
                    //sFechaHora = sFechaHora.Replace("p m", "pm");
                    DateTime FechaHora = Convert.ToDateTime(sFechaHora, culture);
                    CeC.Sleep(0);
                    CeC_Terminales_Param Param = new CeC_Terminales_Param(TERMINAL_ID);
                    string CampoLlave = Param.TERMINAL_CAMPO_LLAVE;
                    if (ID == "9920534")
                        ID = ID;
                    int Persona_ID = CeC_Personas.ObtenPersonaID(TERMINAL_ID, CampoLlave, ID);

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
                    /*                    else
                                            if(FechaHora < DateTime.Now.AddDays(-10))
                                                BorraTERMINALES_DEXTRAS(DR.GetValue(0).ToString());*/

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
        }

        return Accesos;
    }

}