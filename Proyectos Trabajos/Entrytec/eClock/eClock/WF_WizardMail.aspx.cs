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

public partial class WF_WizardMail : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.PERFIL_ID != 1)
        {
            BGuardarCambios.Visible = false;
            Webpanel1.Visible =WebPanel2.Visible= false;
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            txtCorreo.Text = CeC_BD.EjecutaEscalarString("SELECT USUARIO_EMAIL FROM EC_USUARIOS WHERE USUARIO_ID =" + Sesion.USUARIO_ID);
        }
        Sesion.TituloPagina = "Asistente de Configuracion";
        Sesion.DescripcionPagina = "Datos Personales y Soporte Tecnico";
        try
        {
            this.Master.FindControl("WC_Menu1").FindControl("mnu_Main").Visible = !Convert.ToBoolean(Sesion.EsWizard);
            this.Master.FindControl("WCBotonesEncabezado1").Visible = !Convert.ToBoolean(Sesion.EsWizard);
        }
        catch { }
        //Agregar Módulo Log
        if (!IsPostBack)
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asistente de Configuración", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CeC_BD.EjecutaComando("UPDATE EC_USUARIOS SET USUARIO_EMAIL = '" +txtCorreo.Text+"' WHERE USUARIO_ID =" + Sesion.USUARIO_ID);
        Sesion.Redirige("WF_OtrosProductos.aspx");
    }
}
