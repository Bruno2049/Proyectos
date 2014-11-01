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
using System.Globalization;

/// <summary>
/// Conector a Oracle Business Suite (Oracle BS)
/// </summary>
public class CMd_OracleBS : CMd_Base
{
    CeC_BD m_BD;
    // Variables Miembro
    // DataSet a una conexión Oracle BS
    private DS_CMd_OracleBS m_DS_Oracle = new DS_CMd_OracleBS();
    // Variable que almacenará el archivo plano a exportar desde Oracle BS
    private DS_CMd_OracleBS.ArchivoPlanoDataTable m_DTArchivoPlano;
    // Variable que almacena el formato de cultura para presentar la fecha
    CultureInfo m_Culture = new CultureInfo("en-US");

    public CMd_OracleBS()
    {
        //
        // Se inicializa automáticamente el archivo plano que se va a exportar
        m_DTArchivoPlano = m_DS_Oracle.ArchivoPlano;
        //
    }

    /// <summary>
    /// Obtiene el nombre del módulo
    /// </summary>
    /// <returns></returns>
    public override string LeeNombre()
    {
        return "Integración Ver. 1.1 OracleBS";
    }

    string m_CadenaConexion = "Provider=MSDAORA;Data Source=prod;Persist Security Info=True;Password=consultor;User ID=consultor";
    [Description("Contiene la cadena de conexión a la base de datos de oracle bussines suite")]
    [DisplayNameAttribute("CadenaConexion")]
    public string CadenaConexion
    {
        get { return m_CadenaConexion; }
        set
        {
            m_CadenaConexion = value;
            m_BD = new CeC_BD(m_CadenaConexion);
            CIsLog2.AgregaLog("ActualizaEmpleados CMd_OracleBS:" + CadenaConexion);
        }
    }

    int m_DiasARestarPAsistencia = 7;
    [Description("Contiene los dias que se restaran al periodo de nomina para tomarlo como asistencias")]
    [DisplayNameAttribute("DiasARestarPAsistencia")]

    public int DiasARestarPAsistencia
    {
        get { return m_DiasARestarPAsistencia; }
        set { m_DiasARestarPAsistencia = value; }
    }

    bool m_ActEmpleadosAut = true;
    [Description("Indica si se actualizarán los empleados de manera automática ")]
    [DisplayNameAttribute("ActEmpleadosAut")]
    public bool ActEmpleadosAut
    {
        get { return m_ActEmpleadosAut; }
        set { m_ActEmpleadosAut = value; }
    }

    bool m_UsaTablaInter = true;
    [Description("Indica si se usará tabla intermedia interfaz soft&soulware ")]
    [DisplayNameAttribute("UsaTablaInter")]
    public bool UsaTablaInter
    {
        get { return m_UsaTablaInter; }
        set { m_UsaTablaInter = value; }
    }

    public static string ObtenQryEmpleados()
    {
        string Qry = System.IO.File.ReadAllText(HttpRuntime.AppDomainAppPath + "eC_OracleBS.sql");
        return (Qry);
    }
    public DataSet ObtenEmpleados()
    {
        return (DataSet)m_BD.lEjecutaDataSet(ObtenQryEmpleados());
    }
    public DataSet ObtenEmpleado(int Persona_Link_id)
    {
        return (DataSet)m_BD.lEjecutaDataSet(ObtenQryEmpleados() + " PEO.EMPLOYEE_NUMBER = '" + Persona_Link_id + "'");
    }

    public DataSet ObtenEmpleadosCambios()
    {
        string Qry = ObtenQryEmpleados();
        if (UsaTablaInter)
        {
            Qry = Qry.Replace("WHERE 1=1", ", APPS.ECLOCK_MODIF_EMP WHERE 1=1");
            Qry += " AND APPS.ECLOCK_MODIF_EMP.CLAVE_EMPL = PEO.EMPLOYEE_NUMBER AND APPS.ECLOCK_MODIF_EMP.ESTADO=1 ";
        }
        DataSet DS = (DataSet)m_BD.lEjecutaDataSet(Qry);
        return DS;
    }
    /// <summary>
    /// esta función será ejecutada en la clase de asistencias una instante
    /// despues de generar las faltas, y una vez cada hora
    /// </summary>
    /// <returns></returns>
    public override bool EjecutarUnaVezCadaHora()
    {
        return ActualizaEmpleados(-1, false);

    }
    public bool EmpleadoActualizado(string CLAVE_EMPL)
    {
        if (m_BD.lEjecutaComando("UPDATE APPS.ECLOCK_MODIF_EMP SET ESTADO = 0 WHERE CLAVE_EMPL = '" + CLAVE_EMPL + "'") > 0)
            return true;
        return false;
    }
    public override bool ActualizaEmpleados(int SuscripcionID, bool Manual)
    {
        if (SuscripcionID < 0)
            return false;

        if (!Manual && !ActEmpleadosAut)
            return false;
        CIsLog2.AgregaLog("ActualizaEmpleados(int SuscripcionID = " + SuscripcionID + ", bool Manual" + Manual + ")");
        DataSet DSEmpleados = ObtenEmpleadosCambios();
        int Registros = CeC_Empleados.ImportaRegistros(DSEmpleados, true, SuscripcionID, 0);
        if (Registros < 0)
            return false;
        m_BD.lEjecutaComando("UPDATE APPS.ECLOCK_MODIF_EMP SET ESTADO = 0 ");
        return true;
    }

    public DataSet ObtenTiposIncidenciasOracle()
    {
        return (DataSet)m_BD.lEjecutaDataSet("select petf.ELEMENT_TYPE_ID,petf.ELEMENT_NAME,petf.REPORTING_NAME,petf.DESCRIPTION from pay_element_types_f petf");
    }

    public DataSet ObtenTiposNominaOracle()
    {
        return (DataSet)m_BD.lEjecutaDataSet("SELECT PAYROLL_ID,  PAYROLL_NAME from apps.pay_all_payrolls_f");
    }

    public DataSet ObtenTiposNominaTiemposOracle(int PAYROLL_ID)
    {
        return (DataSet)m_BD.lEjecutaDataSet("SELECT TIME_PERIOD_ID,  PERIOD_NAME,PERIOD_NUM,PERIOD_TYPE,START_DATE,END_DATE, YEAR_NUMBER from apps.per_time_periods WHERE PAYROLL_ID = " + PAYROLL_ID);
    }

    public override bool ActualizaTiposNomina(int SuscripcionID)
    {

        DataSet DSTiposNominaOracle = ObtenTiposNominaOracle();
        if (DSTiposNominaOracle == null || DSTiposNominaOracle.Tables.Count < 1 || DSTiposNominaOracle.Tables[0].Rows.Count < 1)
            return false;
        foreach (DataRow DR in DSTiposNominaOracle.Tables[0].Rows)
        {
            try
            {
                int PAYROLL_ID = Convert.ToInt32(DR["PAYROLL_ID"]);
                int TipoNominaID = CeC_TiposNomina.ObtenTipoNominaIDDeTipoNominaIDEx(Convert.ToString(PAYROLL_ID), SuscripcionID);
                if (TipoNominaID < 0)
                {
                    string PAYROLL_NAME = Convert.ToString(DR["PAYROLL_NAME"]);
                    int PeriodoNID = CeC_Periodos_N.AgregaPeriodoN(PAYROLL_NAME, "", 0, CeC_BD.FechaNula, "SELECT PERSONA_ID FROM EC_PERSONAS_DATOS WHERE TIPO_NOMINA = '" + PAYROLL_NAME + "'", 10, 0, SuscripcionID);
                    if (PeriodoNID > 0)
                    {
                        DataSet DSTiemposNomina = ObtenTiposNominaTiemposOracle(PAYROLL_ID);
                        if (DSTiemposNomina == null || DSTiemposNomina.Tables.Count < 1 || DSTiemposNomina.Tables[0].Rows.Count < 1)
                            return false;
                        foreach (DataRow DRTiempo in DSTiemposNomina.Tables[0].Rows)
                        {
                            try
                            {
                                int TIME_PERIOD_ID = Convert.ToInt32(DRTiempo["TIME_PERIOD_ID"]);
                                DateTime Inicio = Convert.ToDateTime(DRTiempo["START_DATE"]);
                                DateTime Fin = Convert.ToDateTime(DRTiempo["END_DATE"]);
                                int YEAR_NUMBER = Inicio.Year;//= Convert.ToInt32(DRTiempo["YEAR_NUMBER"]);
                                int PeriodoNo = CeC.Convierte2Int(DRTiempo["PERIOD_NUM"]);
                                CeC_Periodos.AgregaPeriodo(PeriodoNID, 0, Inicio, Fin, Inicio.AddDays(-DiasARestarPAsistencia), Fin.AddDays(-DiasARestarPAsistencia), YEAR_NUMBER, PeriodoNo, 0, SuscripcionID);
                            }
                            catch { }
                        }
                        CeC_TiposNomina.Agraga(PeriodoNID, PAYROLL_NAME, PAYROLL_ID.ToString(), 0, SuscripcionID);
                    }
                }
            }
            catch { }
        }
        return true;
    }

    public bool EnviaHorasExtras(DateTime FechaNomInicio, CeC_ConfigSuscripcion Config, DateTime FechaInicial, DateTime FechaFinal, int PeriodoID, string SQLPersonas)
    {
        string QryDiasHorasExtras = "";
        if(!CeC_BD.EsOracle)
        {
            //QryDiasHorasExtras = ", sum(IIF (PERSONA_D_HE_SIMPLE>0,1, 0)) as NoDias_HE_SIMPLE, sum(IIF (PERSONA_D_HE_DOBLE>0,1, 0)) as NoDias_HE_DOBLE, sum(IIF (PERSONA_D_HE_TRIPLE>0,1, 0)) as NoDias_HE_TRIPLE";
            QryDiasHorasExtras = ", sum(case when PERSONA_D_HE_SIMPLE>0 then 1 else 0 end) as NoDias_HE_SIMPLE, sum(case when PERSONA_D_HE_DOBLE>0 then 1 else 0 end) as NoDias_HE_DOBLE, sum(case when PERSONA_D_HE_TRIPLE>0 then 1 else 0 end) as NoDias_HE_TRIPLE";
        }
        else
        {
            QryDiasHorasExtras = ", sum(DECODE(PERSONA_D_HE_SIMPLE, 0, 0, 1)) as NoDias_HE_SIMPLE, sum(DECODE(PERSONA_D_HE_DOBLE, 0, 0, 1)) as NoDias_HE_DOBLE, sum(DECODE(PERSONA_D_HE_TRIPLE, 0, 0, 1)) as NoDias_HE_TRIPLE";
        }
        string Qry = "SELECT     EC_PERSONAS.PERSONA_LINK_ID, EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIAS_EX_ID, SUM(EC_PERSONAS_D_HE.PERSONA_D_HE_SIMPLE)  \n" +
     "                      AS PERSONA_D_HE_SIMPLE, SUM(EC_PERSONAS_D_HE.PERSONA_D_HE_DOBLE) AS PERSONA_D_HE_DOBLE,  \n" +
     "                      SUM(EC_PERSONAS_D_HE.PERSONA_D_HE_TRIPLE) AS PERSONA_D_HE_TRIPLE \n" + QryDiasHorasExtras +
     " FROM         EC_PERSONAS INNER JOIN \n" +
     "                      EC_PERSONAS_DIARIO ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DIARIO.PERSONA_ID INNER JOIN \n" +
     "                      EC_PERSONAS_D_HE ON EC_PERSONAS_DIARIO.PERSONA_D_HE_ID = EC_PERSONAS_D_HE.PERSONA_D_HE_ID INNER JOIN \n" +
     "                      EC_TIPO_INCIDENCIAS ON EC_PERSONAS_D_HE.TIPO_INCIDENCIA_ID = EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID LEFT OUTER JOIN \n" +
     "                      EC_TIPO_INCIDENCIAS_EX_INC ON EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID = EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIA_ID \n" +
    " WHERE PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(FechaFinal.AddDays(1)) +
    " AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" + SQLPersonas + ")\n" +
     " GROUP BY EC_PERSONAS.PERSONA_LINK_ID, EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIAS_EX_ID ";
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return false;
        try
        {
            string IncHESimple = Config.TipoIncidenciaExHoraExtraSimple.ToString();
            string IncHEDoble = Config.TipoIncidenciaExHoraExtraDoble.ToString();
            string IncHETripe = Config.TipoIncidenciaExHoraExtraTriple.ToString();

            foreach (DataRow DR in DS.Tables[0].Rows)
            {

                try
                {
                    int PersonaLinkID = Convert.ToInt32(DR["PERSONA_LINK_ID"]);
                    string TipoIncEx = CeC.Convierte2String(DR["TIPO_INCIDENCIAS_EX_ID"]);
                    double PERSONA_D_HE_SIMPLE = CeC.Convierte2Double(DR["PERSONA_D_HE_SIMPLE"]);
                    double PERSONA_D_HE_DOBLE = CeC.Convierte2Double(DR["PERSONA_D_HE_DOBLE"]);
                    double PERSONA_D_HE_TRIPLE = CeC.Convierte2Double(DR["PERSONA_D_HE_TRIPLE"]);
                    int NoDias_HE_SIMPLE = CeC.Convierte2Int(DR["NoDias_HE_SIMPLE"]);
                    int NoDias_HE_DOBLE = CeC.Convierte2Int(DR["NoDias_HE_DOBLE"]);
                    int NoDias_HE_TRIPLE = CeC.Convierte2Int(DR["NoDias_HE_TRIPLE"]);


                    if (TipoIncEx == "")
                    {
                        if (PERSONA_D_HE_SIMPLE > 0)
                            AgregaIncidenciaOracle(PersonaLinkID, IncHESimple, PERSONA_D_HE_SIMPLE, FechaNomInicio, DateTime.Now, DateTime.Now, NoDias_HE_SIMPLE.ToString(), DateTime.Now, "", 0, false, PeriodoID);
                        if (PERSONA_D_HE_DOBLE > 0)
                            AgregaIncidenciaOracle(PersonaLinkID, IncHEDoble, PERSONA_D_HE_DOBLE, FechaNomInicio, DateTime.Now, DateTime.Now, NoDias_HE_DOBLE.ToString(), DateTime.Now, "", 0, false, PeriodoID);
                        if (PERSONA_D_HE_TRIPLE > 0)
                            AgregaIncidenciaOracle(PersonaLinkID, IncHETripe, PERSONA_D_HE_TRIPLE, FechaNomInicio, DateTime.Now, DateTime.Now, NoDias_HE_TRIPLE.ToString(), DateTime.Now, "", 0, false, PeriodoID);

                    }
                    else
                        AgregaIncidenciaOracle(PersonaLinkID, TipoIncEx, PERSONA_D_HE_SIMPLE, FechaNomInicio, DateTime.Now, DateTime.Now, "", DateTime.Now, "", 0, false, PeriodoID);

                }
                catch
                { }
            }
            return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    public bool BorraIncidencias(DateTime FechaNomInicio, int PeriodoID)
    {
        try
        {
            if (m_BD.lEjecutaComando("DELETE APPS.ECLOCK_INCIDENCIAS_EMPL WHERE FECHA_APLICACION=" + m_BD.lSqlFecha(FechaNomInicio) + " AND PERIODO=" + PeriodoID + "") > 0)
                return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    public bool EnviaPrimaDominical(DateTime FechaInicial, DateTime FechaFinal, int PeriodoID, string SQLPersonas, CeC_ConfigSuscripcion Config, DateTime FechaNomInicio)
    {
        string Qry = "SELECT PERSONA_LINK_ID, count(*) as NO_DOMINGOS_T, min(ACCESO_FECHAHORA) as ACCESO_FECHAHORA FROM (" +
            "SELECT PERSONA_LINK_ID, count(PERSONA_LINK_ID) as NO_DOMINGOS_T,min(ACCESO_FECHAHORA) as ACCESO_FECHAHORA FROM EC_ACCESOS,EC_PERSONAS WHERE EC_ACCESOS.PERSONA_ID=EC_PERSONAS.PERSONA_ID AND (DATEPART(weekday, ACCESO_FECHAHORA) = 7) " +
                " AND ACCESO_FECHAHORA>=" + CeC_BD.SqlFecha(FechaInicial) + " AND ACCESO_FECHAHORA<=" + CeC_BD.SqlFecha(FechaFinal) + " AND EC_PERSONAS.PERSONA_ID IN (" + SQLPersonas +
                ") AND EC_PERSONAS.PERSONA_ID IN (" + Config.TipoIncidenciaExPrimaDominical_Filtro +
                ") GROUP BY PERSONA_LINK_ID, DATEPART(dayofyear, ACCESO_FECHAHORA)"
                + ")t GROUP BY PERSONA_LINK_ID";
        int TipoIncidenciaExPrimaDominical = Config.TipoIncidenciaExPrimaDominical;
        if (TipoIncidenciaExPrimaDominical <= 0)
            return false;
        try
        {
            DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                return false;
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                int PersonaLinkID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
                int DomingosTrabajados = CeC.Convierte2Int(DR["NO_DOMINGOS_T"]);
                DateTime FechaMin = CeC.Convierte2DateTime(DR["ACCESO_FECHAHORA"]);
                AgregaIncidenciaOracle(PersonaLinkID, TipoIncidenciaExPrimaDominical.ToString(), DomingosTrabajados, FechaNomInicio, FechaMin, FechaMin, "", FechaNomInicio, "", 0, false, PeriodoID);
            }
            //
            return true;
        }
        catch { }
        return false;
    }
    public bool AgregaIncidenciaOracle(double UNIDAD, DateTime FECHA_APLICACION,
    DateTime FECHA_INICIO_REAL, DateTime FECHA_FIN_REAL, DataRow DR)
    {

        string Incidencia = CeC.Convierte2String(DR["TIPO_INCIDENCIAS_EX_TXT"]);
        if (Incidencia.Trim() == "")
            return false;
        DS_CMd_OracleBS.ArchivoPlanoRow DR_APlano = m_DTArchivoPlano.NewArchivoPlanoRow();
        DR_APlano.NUM_EMPLEADO = CeC.Convierte2String(DR["PERSONA_LINK_ID"]);
        DR_APlano.NOMBRE_INCIDENCIA = Incidencia;
        DR_APlano.FECHA_APLICA = CeC.Convierte2String(FECHA_APLICACION.ToString("d-MMM-yyyy", m_Culture));
        DR_APlano.FECHA_INICIO = CeC.Convierte2String(FECHA_INICIO_REAL.ToString("d-MMM-yyyy", m_Culture));
        DR_APlano.TOTAL_DIAS = CeC.Convierte2String(UNIDAD);
        DR_APlano.FECHA_FIN = CeC.Convierte2String(FECHA_FIN_REAL.ToString("d-MMM-yyyy", m_Culture));

        string Comentario = CeC.Convierte2String(DR["INCIDENCIA_COMENTARIO"]);

        string[] sCampos = CeC_Campos_Inc_R.ObtenCampos(Comentario);
        if (sCampos != null)
        {
            foreach (string sCampo in sCampos)
            {
                try
                {
                    CeC_Campos_Inc_R Campo = new CeC_Campos_Inc_R(sCampo, null);
                    DR_APlano[Campo.Campo_Inc_R_Dest] = Convert.ChangeType(CeC_Campos_Inc_R.ObtenValor(Comentario, sCampo), DR_APlano[Campo.Campo_Inc_R_Dest].GetType());
                }
                catch (Exception Ex)
                {
                    CIsLog2.AgregaError(Ex);
                }
            }
        }
        m_DTArchivoPlano.AddArchivoPlanoRow(DR_APlano);
        return true;
    }

    public bool EnviaIncidencias2(DateTime FechaInicial, DateTime FechaFinal, int PeriodoID, string SQLPersonas)
    {
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(ObtenQryIncidenciaseClock(FechaInicial, FechaFinal, SQLPersonas));
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return false;

        DateTime FechaNomInicio = CeC_Periodos.ObtenPeriodoNomInicio(PeriodoID);
        //BorraIncidencias(FechaNomInicio, PeriodoID);
        int SuscripcionID = CeC_Personas.ObtenSuscripcionID(CeC.Convierte2Int(DS.Tables[0].Rows[0]["PERSONA_ID"]));
        CeC_ConfigSuscripcion Config = new CeC_ConfigSuscripcion(SuscripcionID);
        try
        {
            string IncidenciasExIDRetardo = Config.TipoIncidenciaSExRetardos;
            int PersonaLinkID_Ant = 0;
            string TipoIncEx_Ant = "";
            DateTime FechaInc_Ant = CeC_BD.FechaNula;
            DateTime FechaAsisInicial = CeC_BD.FechaNula;
            DateTime FechaAsisFinal = CeC_BD.FechaNula;
            int NoInc = 0;
            string INCIDENCIA_COMENTARIO_Ant = "";
            DataRow DR_Ant = null;
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                try
                {

                    int PersonaLinkID = CeC.Convierte2Int(DR["PERSONA_LINK_ID"]);
                    string TipoIncEx = CeC.Convierte2String(DR["TIPO_INCIDENCIAS_EX_ID"]);
                    string INCIDENCIA_COMENTARIO = CeC.Convierte2String(DR["INCIDENCIA_COMENTARIO"]);
                    DateTime FechaInc = Convert.ToDateTime(DR["PERSONA_DIARIO_FECHA"]);

                    if (CeC.ExisteEnSeparador(IncidenciasExIDRetardo, TipoIncEx, ","))
                    {
                        double Horas = 0;
                        TimeSpan TSHoras = CeC_BD.DateTime2TimeSpan(CeC.Convierte2DateTime(DR["PERSONA_DIARIO_TDE"]));
                        Horas = TSHoras.TotalHours;
                        AgregaIncidenciaOracle(Horas, FechaNomInicio, FechaInc, FechaInc, DR);
                        continue;
                    }

                    if (PersonaLinkID != PersonaLinkID_Ant || TipoIncEx_Ant != TipoIncEx || Math.Abs(FechaInc_Ant.Subtract(FechaInc).Days) > 1)
                    {
                        if (NoInc > 0)
                            AgregaIncidenciaOracle(NoInc, FechaNomInicio, FechaAsisInicial, FechaAsisFinal, DR_Ant);
                        NoInc = 0;
                        FechaAsisInicial = FechaInc;
                        PersonaLinkID_Ant = PersonaLinkID;
                        TipoIncEx_Ant = TipoIncEx;
                        FechaInc_Ant = FechaInc;
                    }
                    NoInc++;
                    FechaAsisFinal = FechaInc;
                    FechaInc_Ant = FechaInc;
                    DR_Ant = DR;
                }
                catch { }
            }
            AgregaIncidenciaOracle(NoInc, FechaNomInicio, FechaAsisInicial, FechaAsisFinal, DR_Ant);

        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return false;
        }

        EnviaPrimaDominical(FechaInicial, FechaFinal, PeriodoID, SQLPersonas, Config, FechaNomInicio);
        EnviaHorasExtras(FechaNomInicio, Config, FechaInicial, FechaFinal, PeriodoID, SQLPersonas);
        CeC_Exportacion Exp = new CeC_Exportacion();


        Exp.GenerarArchivo(m_DS_Oracle, ",");
        Exp.GuardaTemporal("CMd_OracleBS.tmp");
        return CeC_Periodos.CambiaEstado(PeriodoID, CeC_Periodos.EDO_PERIODO.Cerrado);

    }
    public override bool EnviaIncidencias(DateTime FechaInicial, DateTime FechaFinal, int PeriodoID, string SQLPersonas)
    {
        if (!UsaTablaInter)
            return EnviaIncidencias2(FechaInicial, FechaFinal, PeriodoID, SQLPersonas);


        string Qry = ObtenQryIncidenciaseClockV5(FechaInicial, FechaFinal, SQLPersonas);

        /*"SELECT     EC_PERSONAS_DIARIO.PERSONA_ID,EC_PERSONAS_DATOS.PERSONA_LINK_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA,  " +
                "                      EC_TIPO_INCIDENCIAS_EX_INC_SIS.TIPO_INCIDENCIAS_EX_ID,PERSONA_D_HE_ID, EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID, PERSONA_DIARIO_TDE, " +
                "                      EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIAS_EX_ID AS TIPO_INCIDENCIAS_EX_ID_INC, EC_PERSONAS_DIARIO.INCIDENCIA_ID, INCIDENCIA_COMENTARIO " +
                "FROM         EC_PERSONAS_DATOS INNER JOIN " +
                " EC_PERSONAS_DIARIO ON EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS_DIARIO.PERSONA_ID LEFT OUTER JOIN " +
                " EC_TIPO_INCIDENCIAS_EX_INC INNER JOIN " +
                " EC_INCIDENCIAS ON EC_TIPO_INCIDENCIAS_EX_INC.TIPO_INCIDENCIA_ID = EC_INCIDENCIAS.TIPO_INCIDENCIA_ID ON  " +
                " EC_PERSONAS_DIARIO.INCIDENCIA_ID = EC_INCIDENCIAS.INCIDENCIA_ID LEFT OUTER JOIN " +
                " EC_TIPO_INCIDENCIAS_EX_INC_SIS ON EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID = EC_TIPO_INCIDENCIAS_EX_INC_SIS.TIPO_INC_SIS_ID " +
                " WHERE PERSONA_DIARIO_FECHA >= " + CeC_BD.SqlFecha(FechaInicial) + " AND PERSONA_DIARIO_FECHA < " + CeC_BD.SqlFecha(FechaFinal.AddDays(1)) +
                " AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" + SQLPersonas + ")" +
                " \n ORDER BY EC_PERSONAS_DATOS.PERSONA_LINK_ID,EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA ";
        */
        DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);
        if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
            return false;

        DateTime FechaNomInicio = CeC_Periodos.ObtenPeriodoNomInicio(PeriodoID);
        BorraIncidencias(FechaNomInicio, PeriodoID);
        int SuscripcionID = CeC_Personas.ObtenSuscripcionID(Convert.ToInt32(DS.Tables[0].Rows[0]["PERSONA_ID"]));
        CeC_ConfigSuscripcion Config = new CeC_ConfigSuscripcion(SuscripcionID);
        try
        {
            string IncidenciasExIDRetardo = Config.TipoIncidenciaSExRetardos;
            int PersonaLinkID_Ant = 0;
            string TipoIncEx_Ant = "";
            DateTime FechaInc_Ant = CeC_BD.FechaNula;
            DateTime FechaAsisInicial = CeC_BD.FechaNula;
            DateTime FechaAsisFinal = CeC_BD.FechaNula;
            int NoInc = 0;
            string INCIDENCIA_COMENTARIO_Ant = "";
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                try
                {

                    int PersonaLinkID = Convert.ToInt32(DR["PERSONA_LINK_ID"]);
                    string TipoIncEx = CeC.Convierte2String(DR["TIPO_INCIDENCIAS_EX_ID"]);
                    int INCIDENCIA_ID = Convert.ToInt32(DR["INCIDENCIA_ID"]);
                    string INCIDENCIA_COMENTARIO = CeC.Convierte2String(DR["INCIDENCIA_COMENTARIO"]);
                    if (PersonaLinkID == 77975)
                        PersonaLinkID = 77975;
                    if (PersonaLinkID == 19658)
                        PersonaLinkID = 19658;
                    if (PersonaLinkID == 36072)
                        PersonaLinkID = 36072;
                    DateTime FechaInc = Convert.ToDateTime(DR["PERSONA_DIARIO_FECHA"]);
                    // int PersonasDHEID = Convert.ToInt32(DR["PERSONA_D_HE_ID"]);

                    if (CeC.ExisteEnSeparador(IncidenciasExIDRetardo, TipoIncEx, ","))
                    {
                        double Horas = 0;
                        TimeSpan TSHoras = CeC_BD.DateTime2TimeSpan(CeC.Convierte2DateTime(DR["PERSONA_DIARIO_TDE"]));
                        Horas = TSHoras.TotalHours;
                        AgregaIncidenciaOracle(PersonaLinkID, TipoIncEx, Horas, FechaNomInicio, FechaInc, FechaInc, "", CeC_BD.FechaNula, "", 0, true, PeriodoID, INCIDENCIA_COMENTARIO);
                        continue;
                    }

                    string Configuracion = CeC.Convierte2String(DR["TIPO_INCIDENCIAS_EX_PARAM"]);
                    if (Configuracion != "")
                    {
                        AgregaIncidenciaOracle(PersonaLinkID, TipoIncEx, NoInc, FechaNomInicio, FechaInc, FechaInc, "", CeC_BD.FechaNula, "", 0, false, PeriodoID, INCIDENCIA_COMENTARIO, DR);
                        continue;
                    }


                    if (PersonaLinkID != PersonaLinkID_Ant || TipoIncEx_Ant != TipoIncEx || Math.Abs(FechaInc_Ant.Subtract(FechaInc).Days) > 1)
                    {
                        if (NoInc > 0)
                            AgregaIncidenciaOracle(PersonaLinkID_Ant, TipoIncEx_Ant, NoInc, FechaNomInicio, FechaAsisInicial, FechaAsisFinal, "", DateTime.Now, "", 0, false, PeriodoID, INCIDENCIA_COMENTARIO_Ant);
                        NoInc = 0;
                        FechaAsisInicial = FechaInc;
                        PersonaLinkID_Ant = PersonaLinkID;
                        TipoIncEx_Ant = TipoIncEx;
                        FechaInc_Ant = FechaInc;
                        INCIDENCIA_COMENTARIO_Ant = INCIDENCIA_COMENTARIO;
                    }
                    NoInc++;
                    FechaAsisFinal = FechaInc;
                    FechaInc_Ant = FechaInc;

                }
                catch { }
            }
            AgregaIncidenciaOracle(PersonaLinkID_Ant, TipoIncEx_Ant, NoInc, FechaNomInicio, FechaAsisInicial, FechaAsisFinal, "", DateTime.Now, "", 0, false, PeriodoID, INCIDENCIA_COMENTARIO_Ant);

        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return false;
        }
        EnviaPrimaDominical(FechaInicial, FechaFinal, PeriodoID, SQLPersonas, Config, FechaNomInicio);
        EnviaHorasExtras(FechaNomInicio, Config, FechaInicial, FechaFinal, PeriodoID, SQLPersonas);

        return CeC_Periodos.CambiaEstado(PeriodoID, CeC_Periodos.EDO_PERIODO.Cerrado);

    }
    public bool AgregaIncidenciaOracle(int CLAVE_EMPL, string ELEMENTO, double UNIDAD, DateTime FECHA_APLICACION,
    DateTime FECHA_INICIO_REAL, DateTime FECHA_FIN_REAL, string FOLIO_INCAPACIDAD, DateTime FECHA_INCAPACIDAD,
    string TIPO_INCAPACIDAD, int IMPORTE, bool Sumar, int PeriodoID)
    {
        return AgregaIncidenciaOracle(CLAVE_EMPL, ELEMENTO, UNIDAD, FECHA_APLICACION, FECHA_INICIO_REAL, FECHA_FIN_REAL, FOLIO_INCAPACIDAD,
            FECHA_INCAPACIDAD, TIPO_INCAPACIDAD, IMPORTE, Sumar, PeriodoID, "", null);
    }
    public bool AgregaIncidenciaOracle(int CLAVE_EMPL, string ELEMENTO, double UNIDAD, DateTime FECHA_APLICACION,
        DateTime FECHA_INICIO_REAL, DateTime FECHA_FIN_REAL, string FOLIO_INCAPACIDAD, DateTime FECHA_INCAPACIDAD,
        string TIPO_INCAPACIDAD, int IMPORTE, bool Sumar, int PeriodoID,
        string INCIDENCIA_COMENTARIO)
    {
        return AgregaIncidenciaOracle(CLAVE_EMPL, ELEMENTO, UNIDAD, FECHA_APLICACION, FECHA_INICIO_REAL, FECHA_FIN_REAL, FOLIO_INCAPACIDAD,
            FECHA_INCAPACIDAD, TIPO_INCAPACIDAD, IMPORTE, Sumar, PeriodoID, INCIDENCIA_COMENTARIO, null);
    }
    public bool AgregaIncidenciaOracle(int CLAVE_EMPL, string ELEMENTO, double UNIDAD, DateTime FECHA_APLICACION,
        DateTime FECHA_INICIO_REAL, DateTime FECHA_FIN_REAL, string FOLIO_INCAPACIDAD, DateTime FECHA_INCAPACIDAD,
        string TIPO_INCAPACIDAD, int IMPORTE, bool Sumar, int PeriodoID,
        string INCIDENCIA_COMENTARIO, DataRow DR)
    {
        //Establecemos la información cultural para el formato de fecha que se guarda
        if (ELEMENTO == "")
            return false;
        if (ELEMENTO.Trim() == "0")
            return false;

        //       string Qry = "INSERT INTO APPS.ECLOCK_INCIDENCIAS_EMPL "+
        //          "(CLAVE_EMPL,ELEMENTO, ELEMENTO, UNIDAD, FECHA_APLICACION, FOLIO_INCAPACIDAD, FECHA_INCAPACIDAD, TIPO_INCAPACIDAD, IMPORTE"
        if (UsaTablaInter)
        {
            if (IncapacidadEsTipo(ELEMENTO))
                return false;
            UNIDAD = Math.Round(UNIDAD, 1);

            if (DR != null)
            {
                string Configuracion = CeC.Convierte2String(DR["TIPO_INCIDENCIAS_EX_PARAM"]);
                if (Configuracion != "")
                {
                    string[] sCampos = CeC_Campos_Inc_R.ObtenCampos(Configuracion);
                    foreach (string sCampo in sCampos)
                    {
                        if (sCampo == "INCIDENCIA_COMENTARIO")
                        {
                            INCIDENCIA_COMENTARIO = CeC.Convierte2String(ObtenValorCampo(CeC_Campos_Inc_R.ObtenValor(Configuracion, sCampo), DR));
                        }

                        if (sCampo == "UNIDAD")
                        {
                            UNIDAD = CeC.Convierte2Double(ObtenValorCampo(CeC_Campos_Inc_R.ObtenValor(Configuracion, sCampo), DR), UNIDAD);
                        }

                    }

                }
            }
            //Pareche para el envio de numero de dias de horas extras
            if (FOLIO_INCAPACIDAD != null && FOLIO_INCAPACIDAD != "")
                INCIDENCIA_COMENTARIO = FOLIO_INCAPACIDAD;
            string Qry ="";
            try
            {
                Qry = "INSERT INTO APPS.ECLOCK_INCIDENCIAS_EMPL " +
                    "(CLAVE_EMPL, ELEMENTO, UNIDAD, FECHA_APLICACION,FOLIO_INCAPACIDAD,FECHA_INICIO_REAL,FECHA_FIN_REAL,PERIODO) VALUES (" +
                    CLAVE_EMPL + ",'" + ELEMENTO + "'," + UNIDAD + "," + m_BD.lSqlFecha(FECHA_APLICACION) + ",'" + CeC_BD.ObtenParametroCadena(INCIDENCIA_COMENTARIO) + "'" +
                    "," + m_BD.lSqlFecha(FECHA_INICIO_REAL) + "," + m_BD.lSqlFecha(FECHA_FIN_REAL) + "," + PeriodoID + ")";
                if (m_BD.lEjecutaComando(Qry) > 0)
                    return true;

                Qry = "INSERT INTO APPS.ECLOCK_INCIDENCIAS_EMPL " +
                "(CLAVE_EMPL, ELEMENTO, UNIDAD, FECHA_APLICACION,FECHA_INICIO_REAL,FECHA_FIN_REAL,PERIODO) VALUES (" +
                CLAVE_EMPL + ",'" + ELEMENTO + "'," + UNIDAD + "," + m_BD.lSqlFecha(FECHA_APLICACION) +
                "," + m_BD.lSqlFecha(FECHA_INICIO_REAL) + "," + m_BD.lSqlFecha(FECHA_FIN_REAL) + "," + PeriodoID + ")";
                if (m_BD.lEjecutaComando(Qry) > 0)
                    return true;
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError("AgregaIncidenciaOracle " + Qry);
                //CIsLog2.AgregaError(ex);
            }
        }
        else
        {
            string TIPO_INCIDENCIAS_EX_TXT = ObtenTipoIncEx_TXT(ELEMENTO);
            DS_CMd_OracleBS.ArchivoPlanoRow DR_APlano = m_DTArchivoPlano.NewArchivoPlanoRow();
            DR_APlano.NUM_EMPLEADO = CeC.Convierte2String(CLAVE_EMPL);
            DR_APlano.NOMBRE_INCIDENCIA = TIPO_INCIDENCIAS_EX_TXT;
            DR_APlano.FECHA_APLICA = CeC.Convierte2String(FECHA_APLICACION.ToString("d-MMM-yyyy", m_Culture));
            DR_APlano.FECHA_INICIO = CeC.Convierte2String(FECHA_INICIO_REAL.ToString("d-MMM-yyyy", m_Culture));
            DR_APlano.TOTAL_DIAS = CeC.Convierte2String(UNIDAD);
            DR_APlano.FECHA_FIN = CeC.Convierte2String(FECHA_FIN_REAL.ToString("d-MMM-yyyy", m_Culture));
            m_DTArchivoPlano.AddArchivoPlanoRow(DR_APlano);

        }
        return false;

    }

    public static string ObtenTipoIncEx_TXT(string TIPO_INCIDENCIAS_EX_ID)
    {
        return CeC_BD.EjecutaEscalarString("SELECT TIPO_INCIDENCIAS_EX_TXT FROM EC_TIPO_INCIDENCIAS_EX where TIPO_INCIDENCIAS_EX_ID = " + TIPO_INCIDENCIAS_EX_ID);
    }



    public override bool ActualizaTiposIncidencias(int SuscripcionID)
    {
        ActualizaTiposNomina(SuscripcionID);
        DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter EC_TipoInc_Ex_TA = new DS_CMd_BaseTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter();
        DataSet DSTiposIncidenciasOracle = ObtenTiposIncidenciasOracle();
        if (DSTiposIncidenciasOracle == null || DSTiposIncidenciasOracle.Tables.Count < 1 || DSTiposIncidenciasOracle.Tables[0].Rows.Count < 1)
            return false;
        DS_CMd_Base.EC_TIPO_INCIDENCIAS_EXDataTable EC_TipoIncEx = EC_TipoInc_Ex_TA.GetData();
        foreach (DataRow DR in DSTiposIncidenciasOracle.Tables[0].Rows)
        {
            try
            {
                DS_CMd_Base.EC_TIPO_INCIDENCIAS_EXRow TipoInc = null;
                int ElementTypeID = Convert.ToInt32(DR["ELEMENT_TYPE_ID"]);
                TipoInc = EC_TipoIncEx.FindByTIPO_INCIDENCIAS_EX_ID(ElementTypeID);
                if (TipoInc == null)
                {
                    TipoInc = EC_TipoIncEx.NewEC_TIPO_INCIDENCIAS_EXRow();
                    TipoInc.TIPO_INCIDENCIAS_EX_ID = ElementTypeID;
                    TipoInc.TIPO_FALTA_EX_ID = 0;
                }

                string ELEMENT_NAME = Convert.ToString(DR["ELEMENT_NAME"]);
                if (ELEMENT_NAME.Length > 255)
                    TipoInc.TIPO_INCIDENCIAS_EX_TXT = ELEMENT_NAME.Substring(0, 255);
                else
                    TipoInc.TIPO_INCIDENCIAS_EX_TXT = ELEMENT_NAME;
                TipoInc.TIPO_INCIDENCIAS_EX_NOMBRE = Convert.ToString(DR["REPORTING_NAME"]);
                string Param = "";
                TipoInc.TIPO_INCIDENCIAS_EX_PARAM = Param;

                if (TipoInc.RowState == DataRowState.Detached)
                {
                    EC_TipoIncEx.AddEC_TIPO_INCIDENCIAS_EXRow(TipoInc);
                }
            }
            catch (Exception exc)
            {
                CIsLog2.AgregaError(exc);
            }
        }

        EC_TipoInc_Ex_TA.Update(EC_TipoIncEx);
        return true;
    }
    public bool IncapacidadEsTipo(string ELEMENTO)
    {
        try
        {
            return m_BD.lEjecutaEscalarBool("SELECT MIN(ELEMENTO) FROM APPS.ECLOCK_INCAPACIDADES_EMP WHERE ELEMENTO = " + ELEMENTO, false);
        }
        catch { };
        return false;
    }
    public bool IncapacidadActualizada(int ID_INCAPACIDAD_ORACLE)
    {
        try
        {
            if (m_BD.lEjecutaComando("UPDATE  APPS.ECLOCK_INCAPACIDADES_EMP SET STATUS = 0 WHERE ID_INCAPACIDAD_ORACLE = " + ID_INCAPACIDAD_ORACLE) > 0)
                return true;
        }
        catch { };
        return false;

    }
    public bool IncapacidadAsignaIncidenciaID(int ID_INCAPACIDAD_ORACLE, int IncidenciaID)
    {
        try
        {
            if (m_BD.lEjecutaComando("UPDATE  APPS.ECLOCK_INCAPACIDADES_EMP SET ID_INCAPACIDAD_ECLOCK = " + IncidenciaID + " WHERE ID_INCAPACIDAD_ORACLE = " + ID_INCAPACIDAD_ORACLE) > 0)
                return true;
        }
        catch { };
        return false;

    }

    public override bool RecibeIncidencias(DateTime FechaInicial, DateTime FechaFinal, string SQLPersonas)
    {
        try
        {
            //Status 0: no modificado, Status 1=Modificado, Status 2=Eliminado
            CIsLog2.AgregaLog("RecibeIncidencias " + SQLPersonas);
            string QRY = "SELECT CLAVE_EMPL, ID_INCAPACIDAD_ORACLE, FECHA_INI_INC, FECHA_FINAL_INC,FOLIO,ELEMENTO,CONTROL_INC,TIPO_INCAPACIDAD,ID_INCAPACIDAD_ECLOCK,CONSECUENCIA,STATUS FROM APPS.ECLOCK_INCAPACIDADES_EMP WHERE STATUS >= 1";
            DataSet DS = (DataSet)m_BD.lEjecutaDataSet(QRY);
            if (DS == null || DS.Tables.Count < 1 || DS.Tables[0].Rows.Count < 1)
                return false;
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                int Persona_Link_ID = CeC.Convierte2Int(DR["CLAVE_EMPL"]);
                int TipoIncidenciaEx = CeC.Convierte2Int(DR["ELEMENTO"]);
                DateTime FechaInicialInc = CeC.Convierte2DateTime(DR["FECHA_INI_INC"]);
                DateTime FechaFinalInc = CeC.Convierte2DateTime(DR["FECHA_FINAL_INC"]);
                int IncidenciaID = CeC.Convierte2Int(DR["ID_INCAPACIDAD_ECLOCK"]);
                string Comentario = CeC.Convierte2String(DR["CONTROL_INC"]) + "->" + CeC.Convierte2String(DR["CONSECUENCIA"]);
                int ID_INCAPACIDAD_ORACLE = CeC.Convierte2Int(DR["ID_INCAPACIDAD_ORACLE"]);
                if (IncidenciaID > 0)
                {
                    Cec_Incidencias.QuitaIncidencia(IncidenciaID);
                }
                if (CeC.Convierte2Int(DR["STATUS"]) == 1)
                {
                    int PersonaID = CeC_Empleados.ObtenPersonaID(Persona_Link_ID);
                    if (PersonaID > 0)
                    {
                        int TipoIncidenciaID = Cec_Incidencias.ObtenTipoIncidenciaID(TipoIncidenciaEx);
                        if (TipoIncidenciaID > 0)
                        {
                            IncidenciaID = Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, Comentario, null);
                            CeC_IncidenciasInventario.CorrigeMovimientos(null, PersonaID, FechaInicialInc, FechaFinalInc);
                            Cec_Incidencias.AsignaIncidencia(FechaInicialInc, FechaFinalInc, PersonaID, IncidenciaID);
                            IncapacidadAsignaIncidenciaID(ID_INCAPACIDAD_ORACLE, IncidenciaID);
                        }
                    }
                }
                IncapacidadActualizada(ID_INCAPACIDAD_ORACLE);
            }
            return true;
        }
        catch (Exception exc)
        {
            CIsLog2.AgregaError(exc);
        }
        return false;
    }
}
