using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;



/// <summary>
/// Descripción breve de CeC_EnvioReportes
/// </summary>
public class CeC_EnvioReportes
{
    public static DateTime ultimaEjecucion;
    public static CeC_Sesion Sesion;

    public CeC_EnvioReportes()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Establece la siguente ejecución del envio de reportes
    /// </summary>
    /// <param name="Reporte_ID">Identificador de la regla de envio</param>
    /// <param name="Edicion">Indica si es una edición o creación(True), o actualización(false)</param>
    /// <returns></returns>
    public static bool SigEjecucion(decimal Reporte_ID, bool Edicion)
    {
        DS_EnvioReportes dS_EnvReportes = new DS_EnvioReportes();
        DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter TA_EnvReporte = new DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter();
        TA_EnvReporte.FillByEnvReporteID(dS_EnvReportes.EC_ENV_REPORTES, Reporte_ID);
        DS_EnvioReportes.EC_ENV_REPORTESRow Fila = (DS_EnvioReportes.EC_ENV_REPORTESRow)dS_EnvReportes.EC_ENV_REPORTES.Rows[0];
        DateTime FechaCrea = Fila.ENV_REPORTE_FECHAHORAC;
        DateTime UltFecha = Fila.ENV_REPORTE_FECHAHORA;
        DateTime SigFecha = Fila.ENV_REPORTE_FECHAHORAE;
        int Periodo = Convert.ToInt16(Fila.ENV_REPORTE_C_DIAS);
        if (Edicion)
            SigFecha = FechaCrea;
        UltFecha = SigFecha;
        switch (Periodo)
        {
            case -1:
                if (SigFecha.Day < 16)
                    SigFecha = SigFecha.AddDays(15);
                else
                {
                    SigFecha = SigFecha.AddMonths(1);
                    SigFecha = SigFecha.AddDays(-15);
                }
                break;
            case -2:
                SigFecha = SigFecha.AddMonths(1);
                break;
            default:
                SigFecha = SigFecha.AddDays(Periodo);
                break;
        }
        TA_EnvReporte.UpdateUltySigEnvios(UltFecha, SigFecha, Reporte_ID);
        return true;
    }

    /// <summary>
    /// Edita una regla de envio de reportes o crea una nueva
    /// </summary>
    /// <param name="EnvReporteID">Identificador de la regla a editar. Si es -1 crea una nueva regla</param>
    /// <param name="UsuarioID">Identificador del usuario a quien será enviado el reporte</param>
    /// <param name="ReporteID">Identificador del tipo de reporte a enviar</param>
    /// <param name="FechaI">Fecha en que se creó o ejecutará la regla de envio</param>
    /// <param name="Intervalo">Intervalo de fechas que crearán el reporte</param>
    /// <param name="Periodo">Periodo de envio del reporte</param>
    /// <param name="TipoEnvio">Tipo de envio: una vez o periódico</param>
    /// <param name="Descrip">Descripción de la regla de envio</param>
    /// <returns></returns>
    public static decimal EditarEnv(decimal EnvReporteID, decimal UsuarioID, decimal ReporteID, DateTime FechaI, int Intervalo, int Periodo, int TipoEnvio, string Descrip, CeC_Sesion sesion)
    {
        Sesion = sesion;
        ultimaEjecucion = DateTime.Now;
        DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter TA_EnvReportes = new DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter();
        if (EnvReporteID == -1)
        {
            decimal ID = Convert.ToDecimal(CeC_Autonumerico.GeneraAutonumerico("EC_ENV_REPORTES", "ENV_REPORTE_ID"));
            TA_EnvReportes.NuevoEnvio(ID, UsuarioID, ("").Trim(), 0, ReporteID, Descrip.Trim(), FechaI, Periodo, Intervalo, 0, TipoEnvio);
            SigEjecucion(ID, true);
            EjecutarCadaHora();
            return ID;
        }
        else
        {
            TA_EnvReportes.EditarEnv(UsuarioID, "", 0, ReporteID, Descrip, FechaI, Periodo, Intervalo, 0, TipoEnvio, EnvReporteID);
            SigEjecucion(EnvReporteID, true);
            EjecutarCadaHora();
            return EnvReporteID;
        }
    }

    /// <summary>
    /// Duplica la regla de envio seleccionada
    /// </summary>
    /// <param name="ReporteID">Identificador de la regla a duplicar</param>
    /// <returns></returns>
    public static bool DuplicarEnv(decimal ReporteID)
    {
        decimal ID = Convert.ToDecimal(CeC_Autonumerico.GeneraAutonumerico("EC_ENV_REPORTES", "ENV_REPORTE_ID"));
        DS_EnvioReportes dS_EnvReportes = new DS_EnvioReportes();
        DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter TA_EnvReporte = new DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter();
        TA_EnvReporte.FillByEnvReporteID(dS_EnvReportes.EC_ENV_REPORTES, ReporteID);
        DS_EnvioReportes.EC_ENV_REPORTESRow Fila = (DS_EnvioReportes.EC_ENV_REPORTESRow)dS_EnvReportes.EC_ENV_REPORTES.Rows[0];

        TA_EnvReporte.Insert(ID, Fila.USUARIO_ID, Fila.ENV_REPORTE_EMAIL, Fila.FORMATO_REP_ID, Fila.REPORTE_ID,
            Fila.ENV_REPORTE_DESCRIPCION, Fila.ENV_REPORTE_FECHAHORA, Fila.ENV_REPORTE_C_DIAS, Fila.ENV_REPORTE_DIAS_INI,
            Fila.ENV_REPORTE_EUVEZ, Fila.ENV_REPORTE_FECHAHORAE, Fila.ENV_REPORTE_DIAS_FIN, Fila.ENV_REPORTE_FECHAHORAC);
        return true;
    }

    public static void ReglaPredeterminda()
    {
        string hoy = DateTime.Now.DayOfWeek.ToString().Substring(0, 2);
        DateTime proxLunes;
        switch (hoy)
        {
            case "Mo":
                proxLunes = DateTime.Now.Date.AddDays(7);
                break;
            case "Tu":
                proxLunes = DateTime.Now.Date.AddDays(6);
                break;
            case "We":
                proxLunes = DateTime.Now.Date.AddDays(5);
                break;
            case "Th":
                proxLunes = DateTime.Now.Date.AddDays(4);
                break;
            case "Fr":
                proxLunes = DateTime.Now.Date.AddDays(3);
                break;
            case "Sa":
                proxLunes = DateTime.Now.Date.AddDays(2);
                break;
            case "Su":
                proxLunes = DateTime.Now.Date.AddDays(1);
                break;
        }
        DS_EnvioReportes dS_EnvReportes = new DS_EnvioReportes();
        DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter TA_EnvReporte = new DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter();

        //TA_EnvReporte.Insert(0, 1, "", 0, 1,"Envio cada Lunes del Reporte de Asistencia Diaria de la Semana Anterior",
        //    "0", 7, 7,0, proxLunes, 1, DateTime.Now);
    }

    /// <summary>
    /// Envia el o los reportes programados
    /// </summary>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static int EnviarReporte()
    {
        DS_EnvioReportes dS_EnvReportes = new DS_EnvioReportes();
        DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter TA_EnvReporte = new DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter();
        DataSet ds = new DataSet();
        /*        ReportClass ReporteCR = null;
                CeC_Reportes.REPORTE TipoReporte = new CeC_Reportes.REPORTE();
                int reporte;
                int reportesEnviados = 0;
                string usuarioMail;
                int fechaIni;
                DateTime fechaEje;
                TA_EnvReporte.FillBorrado(dS_EnvReportes.EC_ENV_REPORTES, 0);
                foreach (DS_EnvioReportes.EC_ENV_REPORTESRow Fila in dS_EnvReportes.EC_ENV_REPORTES)
                {
                    if (Fila.ENV_REPORTE_FECHAHORAE <= DateTime.Now)
                    {
                        reporte = Convert.ToInt16(Fila.REPORTE_ID);
                        switch (reporte)
                        {
                            case 0:
                                TipoReporte = CeC_Reportes.REPORTE.AsistenciasCC;
                                break;
                            case 1:
                                TipoReporte = CeC_Reportes.REPORTE.AsistenciaDiaria;
                                break;
                            case 2:
                                TipoReporte = CeC_Reportes.REPORTE.AsistenciaMens;
                                break;
                            case 3:
                                TipoReporte = CeC_Reportes.REPORTE.AsistenciaMensCC;
                                break;
                            case 4:
                                TipoReporte = CeC_Reportes.REPORTE.AsistenciaSicoss;
                                break;
                            case 5:
                                TipoReporte = CeC_Reportes.REPORTE.GraficasGrupo1;
                                break;
                            case 6:
                                TipoReporte = CeC_Reportes.REPORTE.GraficasGrupo2;
                                break;
                            case 7:
                                TipoReporte = CeC_Reportes.REPORTE.GraficasGrupo3;
                                break;
                            case 8:
                                TipoReporte = CeC_Reportes.REPORTE.GraficosPersona;
                                break;
                        }
                        ds = (DataSet)CeC_BD.EjecutaDataSet("SELECT USUARIO_ID, USUARIO_EMAIL " +
                                      "FROM EC_USUARIOS WHERE (USUARIO_EMAIL IS NOT NULL) AND (USUARIO_EMAIL <> '') " +
                                      "AND (USUARIO_BORRADO = 0) AND (USUARIO_ID = " + Fila.USUARIO_ID + ")");
                        usuarioMail = ds.Tables[0].Rows[0][1].ToString();
                        fechaIni = Convert.ToInt16(Fila.ENV_REPORTE_DIAS_INI);
                        fechaEje = Fila.ENV_REPORTE_FECHAHORAE;
                        switch (fechaIni)
                        {
                            case -2:
                                Sesion.WF_EmpleadosFil_FechaI = fechaEje;
                                Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddMonths(-1);
                                Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddDays(1 - fechaEje.Day);
                                Sesion.WF_EmpleadosFil_FechaF = Sesion.WF_EmpleadosFil_FechaI.AddMonths(1);
                                Sesion.WF_EmpleadosFil_FechaF = Sesion.WF_EmpleadosFil_FechaF.AddDays(-1);
                                break;
                            case -1:
                                if (fechaEje.Day > 15)
                                {
                                    Sesion.WF_EmpleadosFil_FechaI = fechaEje;
                                    Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddDays(1 - fechaEje.Day);
                                    Sesion.WF_EmpleadosFil_FechaF = Sesion.WF_EmpleadosFil_FechaI.AddDays(14);
                                }
                                else
                                {
                                    Sesion.WF_EmpleadosFil_FechaI = fechaEje;
                                    Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddDays(16 - fechaEje.Day);
                                    Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddMonths(-1);
                                    Sesion.WF_EmpleadosFil_FechaF = fechaEje;
                                    Sesion.WF_EmpleadosFil_FechaF = Sesion.WF_EmpleadosFil_FechaF.AddDays(-fechaEje.Day);
                                }
                                break;
                            default:
                                Sesion.WF_EmpleadosFil_FechaI = fechaEje;
                                Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddDays(-fechaIni);
                                Sesion.WF_EmpleadosFil_FechaF = fechaEje.AddDays(-1);
                                break;
                        }
                        string filtro = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0";

                        return 0;

                        if (SigEjecucion(Fila.ENV_REPORTE_ID, false))
                            reportesEnviados++;
                        if (Fila.ENV_REPORTE_EUVEZ == 1)
                            TA_EnvReporte.BorrarEnvio(Fila.ENV_REPORTE_ID);
                    }
                }
                return reportesEnviados;*/
        return -1;
    }

    /// <summary>
    /// Verifica cada hora la fecha de ejecucion de las reglas de envio de reportes
    /// y envia si la fecha y hora se ha cumplido
    /// </summary>
    public static void EjecutarCadaHora()
    {
        string qry = "SELECT ENV_REPORTE_FECHAHORAE FROM EC_ENV_REPORTES WHERE ENV_REPORTE_BORRADO = 0";
        DataSet ds = new DataSet();
        OleDbConnection conn = new OleDbConnection(CeC_BD.CadenaConexion());
        OleDbDataAdapter cmd = new OleDbDataAdapter(qry, conn);
        DateTime[] fechaEje;
        while (CeC_BD.EjecutaEscalar(qry) != null)
        {
            cmd.Fill(ds);
            fechaEje = new DateTime[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                fechaEje[i] = Convert.ToDateTime(ds.Tables[0].Rows[i][0]);
            for (int i = 0; i < fechaEje.Length; i++)
            {
                if (fechaEje[i] <= ultimaEjecucion)
                {
                    EnviarReporte();
                    ultimaEjecucion = ultimaEjecucion.AddHours(1);
                }
            }
            CeC.Sleep(3600000);
            ds.Clear();
        }
    }
}
