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

public partial class WF_EnvioAutomaticoMails : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        
        // Permisos****************************************

        string[] Permiso = new string[10];
        /*Permiso[0] = "S";
        Permiso[1] = "S.Configuracion";
        Permiso[2] = "S.Configuracion.Edicion";
        Permiso[3] = "S.Configuracion.Nuevo";
        Permiso[4] = "S.Configuracion.Borrar";*/
        ///Solo uso de esta restriccion
       
        if (Sesion.PERFIL_ID !=1)
        {
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "FR_Main.htm", "", "");
            CBActivar.Visible = false;
            TxtDias.Visible = false;
            FECHA.Visible = false;
            WebPanel2.Visible = false;
            return;
        }
        if (!IsPostBack)
        {
            Sesion.TituloPagina = "Envio Automático de Reporte de Asistencia por Mail";
            Sesion.DescripcionPagina = "Puede configurar el sistema para que envie un mail con el reporte de asistencia a cada usuario, " +
                "a partir de la fecha que especifique, cada determinados días. Por ejemplo, si necesita un reporte cada lunes, selecciona el " +
                "lunes más próximo y en el campo de días guarda el número 7, en caso de no necesitar el módulo de envio automático, " +
                "deseleccione la opción dando click en la casilla";

            //CBActivar.Checked = CMd_Mails
            try
            {
                CBActivar.Checked = CeC_Config.MODULO_ENVIO_AUTOMATICO_MAILS;
                TxtDias.Value = CeC_Config.ENV_AUT_MAILS_DIAS;
                FECHA.SelectedDate = CeC_Config.ENV_AUT_MAILS_DESDE.Date;
                CeC_Config.ENV_AUT_MAILS_RUTA_TEMP = HttpRuntime.AppDomainAppPath;

            }
            catch (Exception ex) { }
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Envio Automático de Mails", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

        CeC_Config.MODULO_ENVIO_AUTOMATICO_MAILS = CBActivar.Checked;
        //CeC_BD.EjecutaComando("UPDATE EC_CONFIG_USUARIO SET CONFIG_USUARIO_VALOR = '"+CBActivar.Checked.ToString()+"' WHERE CONFIG_USUARIO_VARIABLE LIKE 'CMd_Mails.Habilitado'");
        CeC_Config.ENV_AUT_MAILS_DIAS = Convert.ToInt32(TxtDias.Value);
        CeC_Config.ENV_AUT_MAILS_DESDE = FECHA.SelectedDate;
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_EnvioAutomaticoMails.aspx");
    }
}