using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// WF_EnvioReportesEdicion crea una nueva regla de envio de reportes o edita una existente
/// </summary>
public partial class WF_EnvioReportesEdicion : System.Web.UI.Page
{
    protected DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter DA_EnvReportes = new DS_EnvioReportesTableAdapters.EC_ENV_REPORTESTableAdapter();
    protected DS_EnvioReportes dS_EnvReportes1;
    DS_EnvioReportes.EC_ENV_REPORTESRow Fila;
    DS_EnvioReportes.EC_REPORTESRow FilaRep;
    CeC_Sesion Sesion;
    CeC_EnvioReportes EnvReportes = new CeC_EnvioReportes();

    /// <summary>
    /// Carga los datos de la regla de envio en el Web Form
    /// </summary>
    /// <param name="EnvReporteID">Identificador el envio a cargar los datos en el Web Form</param>
    private void CargarDatos(decimal EnvReporteID)
    {
        DA_EnvReportes.FillByEnvReporteID(dS_EnvReportes1.EC_ENV_REPORTES, EnvReporteID);
        Fila = (DS_EnvioReportes.EC_ENV_REPORTESRow)
                dS_EnvReportes1.EC_ENV_REPORTES.Rows[0];
        WT_DESCRIPCION.Text = Fila.ENV_REPORTE_DESCRIPCION;
        CeC_Grid.SeleccionaID(WC_USUARIO, Sesion.WF_ENV_REPORTE_USUARIO_ID);
        CeC_Grid.SeleccionaID(WC_REPORTE, Sesion.WF_ENV_REPORTE_REPORTE_ID);
        int Intervalo = Convert.ToInt16(Fila.ENV_REPORTE_DIAS_INI);
        switch (Intervalo)
        {
            case 0:
                optMes.Checked = true;
                break;
            case 15:
                optQuincena.Checked = true;
                break;
            default:
                optDias.Checked = true;
                WT_UltimosDias.Text = Intervalo.ToString();
                break;
        }
        if (Fila.ENV_REPORTE_EUVEZ == 1)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>UnaVez();</script>");
            TE.Text = "0";
            WDT_FECHAE.Value = Fila.ENV_REPORTE_FECHAHORAE.Date.ToString();
            WDT_HORAE.Text = Fila.ENV_REPORTE_FECHAHORAE.TimeOfDay.ToString();
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>Periodo();</script>");
            TE.Text = "1";
            WDT_FECHAI.Value = Fila.ENV_REPORTE_FECHAHORAC.Date.ToString();
            WDT_HORAI.Text = Fila.ENV_REPORTE_FECHAHORAC.TimeOfDay.ToString();
            int Periodo = Convert.ToInt16(Fila.ENV_REPORTE_C_DIAS);
            switch (Periodo)
            {
                case -1:
                    optCadaQuincena.Checked = true;
                    break;
                case -2:
                    optCadaMes.Checked = true;
                    break;
                default:
                    optCadaNDias.Checked = true;
                    WT_DiasEnvio.Text = Periodo.ToString();
                    break;
            }
            lbl_UltEjecucion.Text = Fila.ENV_REPORTE_FECHAHORA.ToString();
            lbl_SigEjecucion.Text = Fila.ENV_REPORTE_FECHAHORAE.ToString();
        }     
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        dS_EnvReportes1 = new DS_EnvioReportes();
        Sesion = CeC_Sesion.Nuevo(this);
        CMd_Sicoss Sicoss = new CMd_Sicoss();
        if (!IsPostBack)
        {
            string PermisoFinal = CeC_Sesion.LeeStringSesion(this, "PermisosComparacion");
            
            /********************************
             Titulo y Descripción del WebForm
             ********************************/ 
            Sesion.TituloPagina = "Envio de Reportes-Edición";
            Sesion.DescripcionPagina = "Ingrese los datos de la Regla para el Envio de Reportes";

            /********
             Permisos
             ********/
            string[] Permiso = new string[4];
            Permiso[0] = eClock.CEC_RESTRICCIONES.S0Envio0Reportes0Nuevo;
            Permiso[1] = eClock.CEC_RESTRICCIONES.S0Envio0Reportes0Editar;
            Permiso[2] = eClock.CEC_RESTRICCIONES.S0Envio0Reportes0Borrar;

            if (!Sesion.Acceso(Permiso, CIT_Perfiles.Acceso(Sesion.PERFIL_ID, this)))
            {
                CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
                return;
            }

            /*****************
             Inicializa Combos    
             *****************/
            CeC_Grid.AsignaCatalogo(WC_USUARIO, 39);
            CeC_Grid.AplicaFormato(WC_USUARIO);
            WC_USUARIO.DataValueField = "USUARIO_ID";
            CeC_Grid.AsignaCatalogo(WC_REPORTE, 34);
            CeC_Grid.AplicaFormato(WC_REPORTE);

            if (!Sicoss.Habilitado)
            {
                for (int i = 0; i < WC_REPORTE.Rows.Count; i++)
                {
                    if (WC_REPORTE.Rows[i].Cells[1].Value.ToString() == "Asistencia Sicoss")
                    {
                        WC_REPORTE.Rows[i].Hidden = true;
                        break;
                    }
                }
                
            }

            DA_EnvReportes.FillByEnvReporteID(dS_EnvReportes1.EC_ENV_REPORTES, Sesion.WF_ENV_REPORTE_ID);
            if (dS_EnvReportes1.EC_ENV_REPORTES.Count > 0)
            {
                Fila = (DS_EnvioReportes.EC_ENV_REPORTESRow)dS_EnvReportes1.EC_ENV_REPORTES.Rows[0];
                CargarDatos(Convert.ToDecimal(Sesion.WF_ENV_REPORTE_ID));
            }
            else
            {
                Fila = dS_EnvReportes1.EC_ENV_REPORTES.NewEC_ENV_REPORTESRow();
                WDT_FECHAE.Value = DateTime.Now.Date;
                WDT_FECHAI.Value = DateTime.Now.Date;
            }

            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Edición de Envio de Reportes", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        /*************************************
         Declaración y asignación de variables
         *************************************/
        DateTime FechaIni;
        int Periodo;
        int Intervalo;
        decimal Resp;
        int TipoEnvio = Convert.ToInt16(TE.Text);
        string Descrip = WT_DESCRIPCION.Text;
        string reporte;
        LError.Text = LCorrecto.Text = "";
        if (WC_REPORTE.SelectedCell == null)
        {
            LError.Text = "Seleccione el Reporte a Enviar";
            return;
        }
        else
            reporte = WC_REPORTE.SelectedCell.Value.ToString();
        if (WC_USUARIO.SelectedIndex == -1)
        {
            LError.Text = "Seleccione el Usuario a quien se Enviará el Reporte";
            return;
        }
        else
            Sesion.WF_ENV_REPORTE_USUARIO_ID = Convert.ToInt16(WC_USUARIO.DataValue);
        dS_EnvReportes1 = new DS_EnvioReportes();
        DS_EnvioReportesTableAdapters.EC_REPORTESTableAdapter TA_EnvReporte = new DS_EnvioReportesTableAdapters.EC_REPORTESTableAdapter();
        TA_EnvReporte.Fill(dS_EnvReportes1.EC_REPORTES, reporte);
        FilaRep = (DS_EnvioReportes.EC_REPORTESRow)dS_EnvReportes1.EC_REPORTES.Rows[0];
        Sesion.WF_ENV_REPORTE_REPORTE_ID = Convert.ToInt16(FilaRep.REPORTE_ID);

        /**********************************************************
         Verifica si seleccionarón fechas futuras o actual
         **********************************************************/
        if (TipoEnvio == 1)
        {
            if (Convert.ToDateTime(WDT_FECHAE.Value) < DateTime.Now.Date)
            {
                LError.Text = "La Fecha de Ejecución debe ser la Acual o Futura";
                return;
            }
        }
        else
        {
            if (Convert.ToDateTime(WDT_FECHAI.Value) < DateTime.Now.Date)
            {
                LError.Text = "La Fecha de Inicio debe ser la Acual o Futura";
                return;
            }
        }

        /**********************************************************
         Verifica si se enviará una vez o periódicamente el reporte
         **********************************************************/
        if (TipoEnvio == 0)
        {
            FechaIni = Convert.ToDateTime(WDT_FECHAI.Value.ToString()).Add(Convert.ToDateTime(WDT_HORAI.Text).TimeOfDay);
            if (optCadaNDias.Checked)
                Periodo = Convert.ToInt16(WT_DiasEnvio.Text);
            else if (optCadaQuincena.Checked)
                Periodo = -1;
            else
                Periodo = -2;
        }
        else
        {
            FechaIni = Convert.ToDateTime(WDT_FECHAE.Value).Add(Convert.ToDateTime(WDT_HORAE.Value).TimeOfDay);
            Periodo = 0;
        }

        /********************************************
         Verifica que días se incluirán en el reporte
         ********************************************/
        if (optDias.Checked)
            Intervalo = Convert.ToInt16(WT_UltimosDias.Text);
        else if (optQuincena.Checked)
            Intervalo = -1;
        else
            Intervalo = -2;

        Resp = CeC_EnvioReportes.EditarEnv(Convert.ToDecimal(Sesion.WF_ENV_REPORTE_ID), Convert.ToDecimal(Sesion.WF_ENV_REPORTE_USUARIO_ID),
               Convert.ToDecimal(Sesion.WF_ENV_REPORTE_REPORTE_ID), FechaIni, Intervalo, Periodo, TipoEnvio, Descrip, Sesion);
        CargarDatos(Resp);
        if (Resp != null)
            LCorrecto.Text = "Los Datos se Han Guardado Correctamente";
        else
            LError.Text = "Los Datos no se Han Podido Guardar";
    }

    protected void btn_Deshacer_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = LCorrecto.Text = "";
        CargarDatos(Sesion.WF_ENV_REPORTE_ID);
    }

    protected void btn_Regresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_EnvioReportes.aspx");
    }
}
