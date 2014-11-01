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


/// <summary>
/// Descripción breve de CMd_Adam5SQL
/// </summary>
public class CMd_Adam5SQL : CMd_Base
{

    CeC_BD m_BD = null;
    public CMd_Adam5SQL()
    {
        if (m_BD == null)
            CadenaConexion = CadenaConexion;

    }

    /// <summary>
    /// Obtiene el nombre del módulo
    /// </summary>
    /// <returns></returns>
    public override string LeeNombre()
    {
        return "Conector con Adam V5 SQL";
    }

    int m_SuscripcionID = 2;
    [Description("Indica la suscripción que se usará para sincronizar a los empleados")]
    [DisplayNameAttribute("m_SuscripcionID")]
    public int SuscripcionID
    {
        get { return m_SuscripcionID; }
        set { m_SuscripcionID = value; }
    }

    string m_HashTablaEmpleados = "";
    [Description("Contiene un hash para conocer si los datos de empleados han cambiado")]
    [DisplayNameAttribute("HashTablaEmpleados")]
    public string HashTablaEmpleados
    {
        get { return m_HashTablaEmpleados; }
        set { m_HashTablaEmpleados = value; }
    }

    string m_CadenaConexion = "Provider=MSDAORA;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.0.73.189)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=adam)));User Id=adam;Password=adam55;";
    [Description("Contiene la cadena de conexión a la base de datos de Adam")]
    [DisplayNameAttribute("CadenaConexion")]
    public string CadenaConexion
    {
        get { return m_CadenaConexion; }
        set
        {
            m_CadenaConexion = value;
            m_BD = new CeC_BD(m_CadenaConexion);
            //CIsLog2.AgregaLog("Cadena de conexión Adam " + m_CadenaConexion);

        }
    }

    /// <summary>
    /// Indica se se usará la incidencia 50, indispensable en Grupo Miro
    /// </summary>
    bool m_UsaIncidencia50 = true;
    [Description("Contiene la cadena de conexión a la base de datos de Adam")]
    [DisplayNameAttribute("UsaIncidencia50 ")]
    public bool UsaIncidencia50
    {
        get { return m_UsaIncidencia50; }
        set
        {
            m_UsaIncidencia50 = value;
        }
    }
    /// <summary>
    /// Indica si guardara la asignacion de turnos en Adam
    /// </summary>
    bool m_UsaTurnos = false;
    [Description("Indica si guardara la asignacion de turnos en Adam")]
    [DisplayNameAttribute("UsaTurnos ")]
    public bool UsaTurnos
    {
        get { return m_UsaTurnos; }
        set
        {
            m_UsaTurnos = value;
        }
    }

    int m_BorradosDesde = 30;
    [Description("Contiene los días que restara a la fecha para no cargar a los empleados antes de esa fecha")]
    [DisplayNameAttribute("BorradosDesde")]
    public int BorradosDesde
    {
        get { return m_BorradosDesde; }
        set { m_BorradosDesde = value; }
    }
    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez al día
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezAlDia()
    {
        try
        {
            // ActualizaTiposIncidencias(m_SuscripcionID);
            ActualizaEmpleados(m_SuscripcionID, false);

            return true;
        }
        catch
        {
        }
        return false;
    }
    private string ObtenCadenaMax(string Cadena, int MaxLen)
    {
        Cadena = Cadena.Trim();
        if (Cadena.Length > MaxLen)
            Cadena = Cadena.Substring(0, MaxLen);
        return Cadena;
    }
    public override bool ActualizaTiposIncidencias(int SuscripcionID)
    {
        string InExIDS = "";
        string InIDS = "";
        try
        {

            DS_CMd_Adam5SQLTableAdapters.IncidenciasTableAdapter Adam_TA = new DS_CMd_Adam5SQLTableAdapters.IncidenciasTableAdapter();
            Adam_TA.Connection.ConnectionString = CadenaConexion;
            DS_CMd_Adam5SQL.IncidenciasDataTable Adam_Incidencias = Adam_TA.GetData();

            foreach (DS_CMd_Adam5SQL.IncidenciasRow Adam_Incidencia in Adam_Incidencias)
            {
                try
                {
                    string Param = "";
                    if (!Adam_Incidencia.IsSOLICITUD_REF_01Null())
                        Param += "[referencia_01]" + "'" + Adam_Incidencia.SOLICITUD_REF_01.Trim() + "'" + "T" + "L10" + "|";
                    if (!Adam_Incidencia.IsSOLICITUD_REF_02Null())
                        Param += "[referencia_02]" + "'" + Adam_Incidencia.SOLICITUD_REF_02.Trim() + "'" + "T" + "L10" + "|";
                    if (!Adam_Incidencia.IsSOLICITUD_REF_03Null())
                        Param += "[referencia_03]" + "'" + Adam_Incidencia.SOLICITUD_REF_03.Trim() + "'" + "T" + "L10" + "|";
                    if (!Adam_Incidencia.IsSOLICITUD_REF_04Null())
                        Param += "[referencia_04]" + "'" + Adam_Incidencia.SOLICITUD_REF_04.Trim() + "'" + "T" + "L10" + "|";
                    if (!Adam_Incidencia.IsSOLICITUD_VAR_01Null() && Adam_Incidencia.SOLICITUD_VAR_01.Trim() != "N/A" && Adam_Incidencia.SOLICITUD_VAR_01.Trim() != "NO APLICA")
                        Param += "[variable_01]" + "'" + Adam_Incidencia.SOLICITUD_VAR_01.Trim() + "'" + "D" + "L15.6" + "|";
                    if (!Adam_Incidencia.IsSOLICITUD_VAR_02Null() && Adam_Incidencia.SOLICITUD_VAR_02.Trim() != "N/A" && Adam_Incidencia.SOLICITUD_VAR_02.Trim() != "NO APLICA")
                        Param += "[variable_02]" + "'" + Adam_Incidencia.SOLICITUD_VAR_02.Trim() + "'" + "D" + "L15.6" + "|";
                    if (!Adam_Incidencia.IsSOLICITUD_VAR_03Null() && Adam_Incidencia.SOLICITUD_VAR_03.Trim() != "N/A" && Adam_Incidencia.SOLICITUD_VAR_03.Trim() != "NO APLICA")
                        Param += "[variable_03]" + "'" + Adam_Incidencia.SOLICITUD_VAR_03.Trim() + "'" + "D" + "L15.6" + "|";
                    if (!Adam_Incidencia.IsSOLICITUD_VAR_04Null() && Adam_Incidencia.SOLICITUD_VAR_04.Trim() != "N/A" && Adam_Incidencia.SOLICITUD_VAR_04.Trim() != "NO APLICA")
                        Param += "[variable_04]" + "'" + Adam_Incidencia.SOLICITUD_VAR_04.Trim() + "'" + "D" + "L15.6" + "|";

                    Cec_Incidencias.TipoIncidenciaExAgrega(0, SuscripcionID, Adam_Incidencia.INCIDENCIA_KP.ToString(),
                        Adam_Incidencia.DESCRIPCION_KP.Trim(), Adam_Incidencia.COMPANIA.Trim(), Param, 0);
                }
                catch (Exception exc)
                {
                    CIsLog2.AgregaError(exc);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }

    public override string ObtenQryIncidenciaseClock(DateTime Desde, DateTime Hasta, string QryPersonasID)
    {

        string Qry = System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "eC_Adam5Sql_Inc_eClock.sql");
        Qry += " WHERE PERSONA_BORRADO = 0 AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" + QryPersonasID +
            ") AND PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(Desde) + " AND PERSONA_DIARIO_FECHA <= " +
            CeC_BD.SqlFecha(Hasta);
        return Qry;
    }
    public string ObtenQryEmpleados()
    {

        string Qry = System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "eC_Adam5Sql.sql");
        Qry = CeC_BD.AsignaParametroFecha(Qry, "FECHA_MINIMA_BAJA", DateTime.Now.AddDays(-BorradosDesde));
        return Qry;
    }
    public DataSet ObtenEmpleadosAdam()
    {
        //CIsLog2.AgregaLog("Cadena de conexión Adam " + m_BD.CeC_BD_CadenaConexion);
        return (DataSet)m_BD.lEjecutaDataSet(ObtenQryEmpleados());
    }
    public static bool m_ActualizaTurnos = false;
    public override bool ActualizaTurnos(int SuscripcionID, string Nomina)
    {
        string l_turno = "";
        string l_turnoPredeterminado = " Predeterminado";
        string TurnoAdam = "";
        string TurnoeClock = "";
        try
        {
            //if (!m_ActualizaTurnos)
            //    return false;
            if (SuscripcionID < 0)
                return false;
            if (!m_UsaTurnos)
                return false;
            m_ActualizaTurnos = true;
            CIsLog2.AgregaLog("Actualizando Turnos en Sistema de Nomina: " + m_BD.lEsOracle);
            DateTime Fecha = DateTime.Now;
            int Dia = (int)Fecha.DayOfWeek;
            // Inicia en Lunes y una semana anterior
            if (Dia > 1)
                Fecha = DateTime.Today.AddDays(-Dia + 1);
            //.Add(new TimeSpan(-Dia + 1, 0, 0, 0));
            else
                Fecha = DateTime.Today.AddDays(-Dia - 6);
            //Fecha.Add(new TimeSpan(-Dia - 6, 0, 0, 0));

            DataSet DSPersonasTurnoVista =
                (DataSet)CeC_BD.EjecutaDataSet(" SELECT distinct(EC_V_ASISTENCIAS.PERSONA_LINK_ID), EC_V_ASISTENCIAS.TURNO, EC_PERSONAS_DATOS.TIPO_NOMINA " +
                                               " FROM EC_V_ASISTENCIAS INNER JOIN EC_PERSONAS_DATOS ON EC_V_ASISTENCIAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS.PERSONA_LINK_ID " +
                                               " WHERE EC_V_ASISTENCIAS.PERSONA_DIARIO_FECHA = " + CeC_BD.SqlFecha(Fecha) +
                " AND EC_V_ASISTENCIAS.PERSONA_ID IN (SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0 AND SUSCRIPCION_ID = " + SuscripcionID + ")" +
                " AND EC_PERSONAS_DATOS.TIPO_NOMINA = '" + Nomina + "'");

            //DataSet DSPersonasTurno =
            //    (DataSet)CeC_BD.EjecutaDataSet(" SELECT EC_PERSONAS_DATOS.PERSONA_LINK_ID AS PERSONA_LINK_ID, EC_TURNOS.TURNO_NOMBRE AS TURNO, EC_PERSONAS_DATOS.TIPO_NOMINA AS NOMINA " +
            //                                   " FROM EC_PERSONAS INNER JOIN EC_TURNOS ON EC_TURNOS.TURNO_ID=EC_PERSONAS.TURNO_ID " +
            //                                   " INNER JOIN EC_PERSONAS_DATOS ON EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS.PERSONA_LINK_ID " +
            //                                   " WHERE TURNO_NOMBRE <> 'No Asignado' AND PERSONA_BORRADO = 0 AND SUSCRIPCION_ID = " + SuscripcionID
            //                                   " AND EC_PERSONAS_DATOS.TIPO_NOMINA AS NOMINA='" + Nomina + "'");
            if (DSPersonasTurnoVista == null || DSPersonasTurnoVista.Tables.Count < 1 || DSPersonasTurnoVista.Tables[0].Rows.Count < 1)
            {
                CIsLog2.AgregaError("La tabla no contiene datos. Verifique lo siguiente: " +
                                    "\nEl Tipo de Nómina tenga empleados." +
                                    "\nLa suscrición este activa y sea válida.");
                return false;
            }
            //// obtener el turno id con obtencolumna separador turno / 1 y actualizar el turno en adam
            foreach (DataRow DR_eClock in DSPersonasTurnoVista.Tables[0].Rows)
            {
                string personaLinkID = DR_eClock["PERSONA_LINK_ID"].ToString();
                l_turno = CeC.ObtenColumnaSeparador(DR_eClock["TURNO"].ToString(), "/", 1);
                if (l_turno != "" || DR_eClock["TURNO"].ToString() == l_turnoPredeterminado)
                {
                    TurnoAdam = m_BD.lEjecutaEscalarString("SELECT TURNO FROM TRABAJADORES_GRALES " +
                        " WHERE TRABAJADOR = " + DR_eClock["PERSONA_LINK_ID"].ToString());
                    if (DR_eClock["TURNO"].ToString() == l_turnoPredeterminado)
                    {
                        TurnoeClock = CeC_BD.EjecutaEscalarString(" SELECT EC_TURNOS.TURNO_NOMBRE " +
                                                   " FROM EC_PERSONAS INNER JOIN EC_TURNOS ON EC_TURNOS.TURNO_ID=EC_PERSONAS.TURNO_ID " +
                                                   " INNER JOIN EC_PERSONAS_DATOS ON EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS.PERSONA_LINK_ID " +
                                                   " WHERE TURNO_NOMBRE <> 'No Asignado' AND PERSONA_BORRADO = 0 AND SUSCRIPCION_ID = " + SuscripcionID +
                                                   " AND EC_PERSONAS_DATOS.PERSONA_LINK_ID = " + DR_eClock["PERSONA_LINK_ID"].ToString());
                        l_turno = CeC.ObtenColumnaSeparador(TurnoeClock, "/", 1);
                    }
                    if (TurnoAdam != l_turno)
                    {
                        if (m_BD.lEjecutaComando(" UPDATE TRABAJADORES_GRALES " +
                                                    " SET TURNO = '" + l_turno.ToString() + "'" +
                                                    " WHERE TRABAJADORES_GRALES.TRABAJADOR = " + DR_eClock["PERSONA_LINK_ID"].ToString() +
                                                    " AND TRABAJADORES_GRALES.SIT_TRABAJADOR = 1", true) > 0)
                        {
                            CIsLog2.AgregaLog("Se actualizo el turno del trabajador: " + DR_eClock["PERSONA_LINK_ID"].ToString());
                            continue;
                        }
                        else
                        {
                            CIsLog2.AgregaError("Error al actualizar el turno del trabajador: " + DR_eClock["PERSONA_LINK_ID"].ToString() + ". Verifique que este activo en el sistema de Nómina.");
                            continue;
                        }
                    }
                    else
                    {
                        CIsLog2.AgregaLog("Lo turnos iguales en eClock y el sistema de Nomina para el empleado: " + DR_eClock["PERSONA_LINK_ID"].ToString() + ". Nada que actualizar");
                    }
                }
                continue;
            }
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }

    public static bool m_ActualizandoEmpleados = false;
    public override bool ActualizaEmpleados(int SuscripcionID, bool Manual)
    {
        try
        {
            if (!Manual)
                return false;
            //Quitar
            //return true;
            if (SuscripcionID <= 0)
                return false;
            if (m_ActualizandoEmpleados)
                return false;
            m_ActualizandoEmpleados = true;
            CIsLog2.AgregaLog("ActualizaEmpleados Adam:" + m_BD.lEsOracle);

            DataSet DSEmpleados = ObtenEmpleadosAdam();

            string Hash = "";
            Hash = CeC_BD.CalculaHash(DSEmpleados.GetXml());
            if (Hash == m_HashTablaEmpleados)
            {
                m_ActualizandoEmpleados = false;
                return false;
            }
            DSEmpleados.WriteXml(HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + "DSEmpleados" + DateTime.Now.ToString("hhMMss") + ".xml");
            HashTablaEmpleados = Hash;
            GuardaParametros();
            int Registros = 0;
            try
            {
                Registros = CeC_Empleados.ImportaRegistros(DSEmpleados, true, SuscripcionID, 0);
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError(ex);
            }
            m_ActualizandoEmpleados = false;
            if (Registros < 0)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    public decimal ObtenNoHoras(DateTime Fecha)
    {
        return Convert.ToDecimal(Fecha.Hour) + Convert.ToDecimal(Fecha.Minute) / Convert.ToDecimal(60.0);
    }
    public int ExisteTipoIncidenciaKP(string Compania, short Incidencia_KP)
    {
        return m_BD.lEjecutaEscalarInt("SELECT  COUNT(*) FROM INCIDENCIAS_KP_DEF WHERE COMPANIA = '" + Compania + "' AND INCIDENCIA_KP = " + Incidencia_KP);
        if (Compania == "004" || Compania == "001")
            return 0;
        return 1;
    }
    public bool BorraIncidencia(string Compania, string Trabajador, DateTime FechaIncidencia)
    {
        if (m_BD.lEjecutaComando("DELETE FROM INCIDENCIAS_KP WHERE COMPANIA = '" + Compania + "' AND  INCIDENCIA_KP <> 50 AND TRABAJADOR = " + Trabajador + " AND FECHA_INCIDENCIA = " + m_BD.lSqlFecha(FechaIncidencia)) > 0)
            return true;
        return false;
    }
    public bool BorraIncidencia(string Compania, string Trabajador, DateTime FechaIncidencia, short Incidencia_KP)
    {
        if (m_BD.lEjecutaComando("DELETE FROM INCIDENCIAS_KP WHERE COMPANIA = '" + Compania + "' AND TRABAJADOR = " + Trabajador + " AND FECHA_INCIDENCIA = " + m_BD.lSqlFecha(FechaIncidencia) + " AND  INCIDENCIA_KP = " + Incidencia_KP) > 0)
            return true;
        return false;
    }
    public bool EnviaIncidencia50(DateTime FechaInicial, DateTime FechaFinal, int PeriodoID, string SQLPersonas)
    {
        try
        {
            DateTime FechaOriginal = FechaFinal;
            FechaFinal = FechaFinal.AddDays(1);
            string Qry = "";
            Qry = "SELECT EC_PERSONAS_DATOS.PERSONA_LINK_ID, EC_PERSONAS_DATOS.COMPANIA, " +
                    " PERSONA_NOMBRE, INC_50_CUENTA(EC_PERSONAS.PERSONA_ID, " +
                    CeC_BD.SqlFecha(FechaInicial) + "," + CeC_BD.SqlFecha(FechaFinal) + ", TIPO_NOMINA,COMPANIA )AS ASISTENCIAS, " +
                    " INC_50_HORASXTRABAJAR(EC_PERSONAS.PERSONA_ID, " +
                    CeC_BD.SqlFecha(FechaInicial) + "," + CeC_BD.SqlFecha(FechaFinal) + ", TIPO_NOMINA,COMPANIA )AS HORASXTRABAJAR, " +
                    " INC_50_HORASTRABAJADAS(EC_PERSONAS.PERSONA_ID, " +
                    CeC_BD.SqlFecha(FechaInicial) + "," + CeC_BD.SqlFecha(FechaFinal) + ", TIPO_NOMINA,COMPANIA )AS HORASTRABAJADAS, " +
                    "EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_ID, EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_TXT, " +
                    "EC_TIPO_INCIDENCIAS_EX.TIPO_FALTA_EX_ID, EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_PARAM " +
                    "FROM EC_PERSONAS, EC_PERSONAS_DATOS,EC_TIPO_INCIDENCIAS_EX " +
                    "WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID AND " +
                    "EC_PERSONAS.PERSONA_ID IN (" + SQLPersonas + ") AND EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_TXT = '50'";

            DS_CMd_Adam5SQLTableAdapters.incidencias_kpTableAdapter TA_Adam = new DS_CMd_Adam5SQLTableAdapters.incidencias_kpTableAdapter();
            TA_Adam.Connection.ConnectionString = CadenaConexion;
            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
            Qry = "SELECT EC_PERSONAS_DATOS.PERSONA_LINK_ID, EC_PERSONAS_DATOS.COMPANIA, " +
                    " PERSONA_NOMBRE, " +
                    " 1.0 AS ASISTENCIAS, " +
                    " 1.0 AS HORASXTRABAJAR, " +
                    " 1.0 AS HORASTRABAJADAS, " +
                    "EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_ID, EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_TXT, " +
                    "EC_TIPO_INCIDENCIAS_EX.TIPO_FALTA_EX_ID, EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_PARAM " +
                    "FROM EC_PERSONAS, EC_PERSONAS_DATOS,EC_TIPO_INCIDENCIAS_EX " +
                    "WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID AND " +
                    "EC_PERSONAS.PERSONA_ID IN (" + SQLPersonas + ") AND EC_TIPO_INCIDENCIAS_EX.TIPO_INCIDENCIAS_EX_TXT = '50'";

            DS_CMd_Adam5SQL.incidencias_kpDataTable DT_INC = new DS_CMd_Adam5SQL.incidencias_kpDataTable();

            foreach (DataRow Fila in DS.Tables[0].Rows)
            {
                DS_CMd_Adam5SQL.incidencias_kpRow Inc = DT_INC.Newincidencias_kpRow();
                if (CeC.Convierte2String(Fila["COMPANIA"], "NULL") == "NULL")
                    continue;
                Inc.compania = CeC.Convierte2String(Fila["COMPANIA"]);
                Inc.tra_compania = CeC.Convierte2String(Fila["COMPANIA"]);
                Inc.trabajador = CeC.Convierte2String(Fila["PERSONA_LINK_ID"]).PadLeft(10);
                Inc.fecha_incidencia = FechaOriginal;
                Inc.secuencia = 1;
                Inc.sit_incidencia = 1;
                Inc.variable_01 = 0;
                Inc.variable_02 = 0;
                Inc.variable_03 = 0;
                Inc.variable_04 = 0;
                Inc.referencia_01 = "";
                Inc.referencia_02 = "";
                Inc.referencia_03 = "";
                Inc.referencia_04 = "";
                string Comentario = "";

                if (CeC.Convierte2Int(Fila["TIPO_INCIDENCIAS_EX_ID"]) <= 0)
                    continue;
                Inc.incidencia_kp = CeC.Convierte2Short(Fila["TIPO_INCIDENCIAS_EX_TXT"]);

                if (ExisteTipoIncidenciaKP(Inc.compania.Trim(), Inc.incidencia_kp) <= 0)
                    continue;
                BorraIncidencia(Inc.compania, Inc.trabajador, Inc.fecha_incidencia, Inc.incidencia_kp);

                //Asigna los valores constantes de interfaz
                try
                {
                    string Configuracion = CeC.Convierte2String(Fila["TIPO_INCIDENCIAS_EX_PARAM"]);
                    string[] sCampos = CeC_Campos_Inc_R.ObtenCampos(Configuracion);
                    foreach (string sCampo in sCampos)
                    {
                        bool Paso = true;
                        try
                        {
                            Inc[sCampo] = Convert.ChangeType(ObtenValorCampo(CeC_Campos_Inc_R.ObtenValor(Configuracion, sCampo), Fila), DT_INC.Columns[sCampo].DataType);
                        }
                        catch (Exception Ex)
                        {
                            Paso = false;
                            CIsLog2.AgregaError(Ex);
                        }
                        try
                        {
                            if (!Paso)
                                Inc[sCampo] = Convert.ChangeType(ObtenValorCampo(CeC_Campos_Inc_R.ObtenValor(Configuracion, sCampo), Fila), DT_INC.Columns[sCampo].DataType);
                        }
                        catch (Exception Ex)
                        {
                            Paso = false;
                            CIsLog2.AgregaError(Ex);
                        }
                    }
                }
                catch { }

                try
                {
                    string QryInsert =
                        "INSERT INTO incidencias_kp (compania, trabajador, incidencia_kp, fecha_incidencia, secuencia, referencia_01, " +
                        " referencia_02, referencia_03, tra_compania, referencia_04, variable_01, variable_02, variable_03, variable_04, sit_incidencia) VALUES (";

                    QryInsert += "'" + Inc.compania + "', '" + Inc.trabajador + "', '" + Inc.incidencia_kp + "', " + CeC_BD.SqlFecha(Inc.fecha_incidencia) + ", " + Inc.secuencia + ",'" + Inc.referencia_01 + "','"
                        + Inc.referencia_02 + "', '" + Inc.referencia_03 + "', '" + Inc.tra_compania + "', '" + Inc.referencia_04 + "', " + Inc.variable_01 + ", " +
                        Inc.variable_02 + ", " + Inc.variable_03 + ", " + Inc.variable_04 + ", " + Inc.sit_incidencia + ")";
                    CIsLog2.AgregaLog(QryInsert);
                    m_BD.lEjecutaComando(QryInsert);
                    /*
                    TA_Adam.Insert(Inc.compania, Inc.trabajador, Inc.incidencia_kp, Inc.fecha_incidencia, Inc.secuencia,
                        Inc.referencia_01, Inc.referencia_02, Inc.referencia_03, Inc.tra_compania, Inc.referencia_04, Inc.variable_01,
                        Inc.variable_02, Inc.variable_03, Inc.variable_04, Inc.sit_incidencia);*/

                }
                catch (Exception Ex)
                {
                    CIsLog2.AgregaError(Ex);
                }
            }
            return true;
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return false;
    }

    public override bool EnviaIncidencias(DateTime FechaInicial, DateTime FechaFinal, int PeriodoID, string SQLPersonas)
    {
        try
        {
            if (UsaIncidencia50)
                if (!EnviaIncidencia50(FechaInicial, FechaFinal, PeriodoID, SQLPersonas))
                    return false;

            DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter TA = new DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter();
            DS_CMd_Base.EC_TIPO_INCIDENCIAS_EXDataTable DT = TA.GetDataFaltaEx();
            DS_CMd_Adam5SQLTableAdapters.incidencias_kpTableAdapter TA_Adam = new DS_CMd_Adam5SQLTableAdapters.incidencias_kpTableAdapter();
            TA_Adam.Connection.ConnectionString = CadenaConexion;
            DS_CMd_Adam5SQL.incidencias_kpDataTable DT_INC = new DS_CMd_Adam5SQL.incidencias_kpDataTable();

            DataSet DSPersonaDiario = (DataSet)CeC_BD.EjecutaDataSet(ObtenQryIncidenciaseClock(FechaInicial, FechaFinal, SQLPersonas));
            if (DSPersonaDiario == null || DSPersonaDiario.Tables.Count < 1 || DSPersonaDiario.Tables[0].Rows.Count < 1)
                return false;
            DS_CMd_Adam5SQLTableAdapters.Persona_DiarioTableAdapter TA_PE = new DS_CMd_Adam5SQLTableAdapters.Persona_DiarioTableAdapter();
            // DS_CMd_Adam5SQL.Persona_DiarioDataTable DT_PE = TA_PE.GetData(FechaInicial, FechaFinal.AddDays(1));

            foreach (DataRow Fila in DSPersonaDiario.Tables[0].Rows)
            {
                DS_CMd_Adam5SQL.incidencias_kpRow Inc = DT_INC.Newincidencias_kpRow();
                if (CeC.Convierte2String(Fila["COMPANIA"], "NULL") == "NULL")
                    continue;
                Inc.compania = CeC.Convierte2String(Fila["COMPANIA"]);
                Inc.tra_compania = CeC.Convierte2String(Fila["COMPANIA"]);
                Inc.trabajador = CeC.Convierte2String(Fila["PERSONA_LINK_ID"]).PadLeft(10);
                Inc.fecha_incidencia = CeC.Convierte2DateTime(Fila["PERSONA_DIARIO_FECHA"]);
                Inc.secuencia = 1;
                Inc.sit_incidencia = 1;
                Inc.variable_01 = 0;
                Inc.variable_02 = 0;
                Inc.variable_03 = 0;
                Inc.variable_04 = 0;
                Inc.referencia_01 = "";
                Inc.referencia_02 = "";
                Inc.referencia_03 = "";
                Inc.referencia_04 = "";
                string Comentario = "";

                Comentario = CeC.Convierte2String(Fila["INCIDENCIA_COMENTARIO"]);
                if (CeC.Convierte2Int(Fila["TIPO_INCIDENCIAS_EX_ID"]) <= 0)
                    continue;
                Inc.incidencia_kp = CeC.Convierte2Short(Fila["TIPO_INCIDENCIAS_EX_TXT"]);

                if (ExisteTipoIncidenciaKP(Inc.compania.Trim(), Inc.incidencia_kp) <= 0)
                    continue;
                BorraIncidencia(Inc.compania, Inc.trabajador, Inc.fecha_incidencia);
                string[] sCampos = CeC_Campos_Inc_R.ObtenCampos(Comentario);
                if (sCampos != null)
                {
                    foreach (string sCampo in sCampos)
                    {
                        try
                        {
                            CeC_Campos_Inc_R Campo = new CeC_Campos_Inc_R(sCampo, null);
                            Inc[Campo.Campo_Inc_R_Dest] = Convert.ChangeType(CeC_Campos_Inc_R.ObtenValor(Comentario, sCampo), Inc[Campo.Campo_Inc_R_Dest].GetType());
                        }
                        catch (Exception Ex)
                        {
                            CIsLog2.AgregaError(Ex);
                        }
                    }
                }
                //Asigna los valores constantes de interfaz
                try
                {
                    string Configuracion = CeC.Convierte2String(Fila["TIPO_INCIDENCIAS_EX_PARAM"]);
                    sCampos = CeC_Campos_Inc_R.ObtenCampos(Configuracion);
                    foreach (string sCampo in sCampos)
                    {
                        bool Paso = true;
                        try
                        {
                            Inc[sCampo] = Convert.ChangeType(ObtenValorCampo(CeC_Campos_Inc_R.ObtenValor(Configuracion, sCampo), Fila), DT_INC.Columns[sCampo].DataType);
                        }
                        catch (Exception Ex)
                        {
                            Paso = false;
                            CIsLog2.AgregaError(Ex);
                        }
                        try
                        {
                            if (!Paso)
                                Inc[sCampo] = Convert.ChangeType(ObtenValorCampo(CeC_Campos_Inc_R.ObtenValor(Configuracion, sCampo), Fila), DT_INC.Columns[sCampo].DataType);
                        }
                        catch (Exception Ex)
                        {
                            Paso = false;
                            CIsLog2.AgregaError(Ex);
                        }
                    }
                }
                catch { }


                /*     if (!Fila.IsPERSONA_DIARIO_TTNull() && Fila.PERSONA_DIARIO_TT != CeC_BD.FechaNula)
                         Inc.variable_01 = ObtenNoHoras(Fila.PERSONA_DIARIO_TT);
                     else
                         Inc.variable_01 = ObtenNoHoras(Fila.HorarioTiempo);
                 */
                try
                {
                    //DT_INC.Addincidencias_kpRow(Inc);
                    string QryInsert = "INSERT INTO incidencias_kp (compania, trabajador, incidencia_kp, fecha_incidencia, secuencia, referencia_01, " +
                        " referencia_02, referencia_03, tra_compania, referencia_04, variable_01, variable_02, variable_03, variable_04, sit_incidencia) VALUES (";
                    QryInsert += "'" + Inc.compania + "', '" + Inc.trabajador + "', '" + Inc.incidencia_kp + "', " + CeC_BD.SqlFecha(Inc.fecha_incidencia) + ", " + Inc.secuencia + ",'" + Inc.referencia_01 + "','"
                        + Inc.referencia_02 + "', '" + Inc.referencia_03 + "', '" + Inc.tra_compania + "', '" + Inc.referencia_04 + "', " + Inc.variable_01 + ", " +
                        Inc.variable_02 + ", " + Inc.variable_03 + ", " + Inc.variable_04 + ", " + Inc.sit_incidencia + ")";
                    CIsLog2.AgregaLog(QryInsert);
                    m_BD.lEjecutaComando(QryInsert);
                    //?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
                    /*
                                        TA_Adam.Insert(Inc.compania, Inc.trabajador, Inc.incidencia_kp, Inc.fecha_incidencia, Inc.secuencia,
                                            Inc.referencia_01, Inc.referencia_02, Inc.referencia_03, Inc.tra_compania, Inc.referencia_04, Inc.variable_01,
                                            Inc.variable_02, Inc.variable_03, Inc.variable_04, Inc.sit_incidencia);
                                        */
                }
                catch (Exception Ex)
                {
                    CIsLog2.AgregaError(Ex);
                }
            }
            //TA_Adam.Update(DT_INC);
            CeC_Periodos.CambiaEstado(PeriodoID, CeC_Periodos.EDO_PERIODO.Cerrado);
            return true;
        }
        catch (Exception Ex)
        {
            CIsLog2.AgregaError(Ex);
        }
        return false;
    }

    /// <summary>
    /// Actualiza los periodos Nombre, pero para adam se llaman Tipos de Nomina
    /// </summary>
    /// <param name="SuscripcionID"></param>
    /// <returns></returns>
    public bool ActualizaPeriodosNombre(int SuscripcionID)
    {
        DataSet DS_TiposNomina = (DataSet)m_BD.lEjecutaDataSet("SELECT TIPO_NOMINA, DESCRIPCION FROM TIPOS_NOMINA WHERE TIPO_NOMINA IN (SELECT TIPO_NOMINA FROM PROCESOS_CP_TEST)");
        if (DS_TiposNomina == null || DS_TiposNomina.Tables.Count < 1 || DS_TiposNomina.Tables[0].Rows.Count < 1)
            return false;
        foreach (DataRow DR in DS_TiposNomina.Tables[0].Rows)
        {
            try
            {
                string TipoNomina = CeC.Convierte2String(DR["TIPO_NOMINA"]);
                int PeriodoNID = CeC_Periodos_N.AgregaPeriodoN(TipoNomina,
                    CeC.Convierte2String(DR["DESCRIPCION"]),
                    0, CeC_BD.FechaNula, "", DiasARestarPAsistencia, 0, SuscripcionID);
                if (PeriodoNID > 0)
                {
                    DataSet DSTiemposNomina = (DataSet)m_BD.lEjecutaDataSet("select ID_CALENDARIO, ANIO, PERIODO, FECHA_INICIO, FECHA_TERMINO from CALENDARIO_PROCESOS WHERE TIPO_NOMINA = '" + TipoNomina + "'");
                    if (DSTiemposNomina == null || DSTiemposNomina.Tables.Count < 1 || DSTiemposNomina.Tables[0].Rows.Count < 1)
                        return false;
                    foreach (DataRow DRTiempo in DSTiemposNomina.Tables[0].Rows)
                    {
                        try
                        {
                            int TIME_PERIOD_ID = Convert.ToInt32(DRTiempo["ID_CALENDARIO"]);
                            DateTime Inicio = Convert.ToDateTime(DRTiempo["FECHA_INICIO"]);
                            DateTime Fin = Convert.ToDateTime(DRTiempo["FECHA_TERMINO"]);
                            int YEAR_NUMBER = Convert.ToInt32(DRTiempo["ANIO"]);
                            int PeriodoNo = CeC.Convierte2Int(DRTiempo["PERIODO"]);
                            CeC_Periodos.AgregaPeriodo(PeriodoNID, 0, Inicio, Fin, Inicio.AddDays(-DiasARestarPAsistencia), Fin.AddDays(-DiasARestarPAsistencia), YEAR_NUMBER, PeriodoNo, 0, SuscripcionID);
                        }
                        catch { }
                    }


                }
            }
            catch { }
        }
        return true;
    }
    int m_DiasARestarPAsistencia = 7;
    [Description("Contiene los dias que se restaran al periodo de nomina para tomarlo como asistencias")]
    [DisplayNameAttribute("DiasARestarPAsistencia")]
    public int DiasARestarPAsistencia
    {
        get { return m_DiasARestarPAsistencia; }
        set { m_DiasARestarPAsistencia = value; }
    }

    public DataSet ObtenTiposNomina()
    {
        return (DataSet)m_BD.lEjecutaDataSet("SELECT CLASE_NOMINA, DESCRIPCION FROM CLASES_NOMINA");
    }


    /// <summary>
    /// Actualiza los tipos de nomina, pero en adam los manejaremos como Clase de Nomina
    /// </summary>
    /// <param name="SuscripcionID"></param>
    /// <returns></returns>
    public override bool ActualizaTiposNomina(int SuscripcionID)
    {
        //Para Adam en realidad estará actualizando los tipos de nomina
        //ActualizaPeriodosNombre(SuscripcionID);

        DataSet DSTiposNomina = ObtenTiposNomina();
        if (DSTiposNomina == null || DSTiposNomina.Tables.Count < 1 || DSTiposNomina.Tables[0].Rows.Count < 1)
        {
            CIsLog2.AgregaLog("No se pudieron obtener los tipos de nomina");
            return false;
        }
        CIsLog2.AgregaLog("ActualizaTiposNomina(int SuscripcionID=" + SuscripcionID + ") " + DSTiposNomina.Tables[0].Rows.Count + " Tipos de nomina");
        foreach (DataRow DR in DSTiposNomina.Tables[0].Rows)
        {
            try
            {
                string CLASE_NOMINA = CeC.Convierte2String(DR["CLASE_NOMINA"]);
                string DESCRIPCION = CeC.Convierte2String(DR["DESCRIPCION"]);

                int PeriodoNID = CeC_Periodos_N.AgregaPeriodoN(CLASE_NOMINA,
                    DESCRIPCION,
                    0, CeC_BD.FechaNula, "", DiasARestarPAsistencia, 0, SuscripcionID);
                CIsLog2.AgregaLog("PeriodoNID = " + PeriodoNID + " CLASE_NOMINA=" + CLASE_NOMINA + " DESCRIPCION=" + DESCRIPCION);
                if (PeriodoNID > 0)
                {
                    string Filtro = "tipo_nomina = 'SG'";
                    if (CLASE_NOMINA.Trim() == "CO")
                        Filtro = "tipo_nomina = 'NM'";

                    if (CLASE_NOMINA.Trim() == "EQ")
                        Filtro = "tipo_nomina = 'NQ'";

                    if (CLASE_NOMINA.Trim() == "SE")
                        Filtro = "tipo_nomina = 'S3'";

                    if (CLASE_NOMINA.Trim() == "SI")
                        Filtro = "tipo_nomina = 'S1'";

                    string Qry = "select ID_CALENDARIO, ANIO, PERIODO, FECHA_INICIO, FECHA_TERMINO from CALENDARIO_PROCESOS WHERE " + Filtro + "  AND anio > 2011   order by anio,periodo";
                    DataSet DSTiemposNomina = (DataSet)m_BD.lEjecutaDataSet(Qry);
                    if (DSTiemposNomina == null || DSTiemposNomina.Tables.Count < 1 || DSTiemposNomina.Tables[0].Rows.Count < 1)
                        return false;
                    foreach (DataRow DRTiempo in DSTiemposNomina.Tables[0].Rows)
                    {
                        try
                        {
                            int TIME_PERIOD_ID = Convert.ToInt32(DRTiempo["ID_CALENDARIO"]);
                            DateTime Inicio = Convert.ToDateTime(DRTiempo["FECHA_INICIO"]);
                            DateTime Fin = Convert.ToDateTime(DRTiempo["FECHA_TERMINO"]);
                            int YEAR_NUMBER = Convert.ToInt32(DRTiempo["ANIO"]);
                            int PeriodoNo = CeC.Convierte2Int(DRTiempo["PERIODO"]);
                            CeC_Periodos.AgregaPeriodo(PeriodoNID, 0, Inicio, Fin, Inicio.AddDays(-DiasARestarPAsistencia), Fin.AddDays(-DiasARestarPAsistencia), YEAR_NUMBER, PeriodoNo, 0, SuscripcionID);
                        }
                        catch { }
                    }


                }
                CeC_TiposNomina.Agraga(PeriodoNID, CLASE_NOMINA.Trim(), DESCRIPCION.Trim(), 0, SuscripcionID);
            }
            catch (Exception EX) { CIsLog2.AgregaError(EX); }
        }
        return true;
    }


}
