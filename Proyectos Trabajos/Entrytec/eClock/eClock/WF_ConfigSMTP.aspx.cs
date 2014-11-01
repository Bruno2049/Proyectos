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

public partial class WF_ConfigSMTP : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    bool Valida()
    {
        if (Sesion.SUSCRIPCION_ID != 1 || !Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Configuracion))
        {
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            return false;
        }
        return true;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Configuración del SMTP";
        Sesion.DescripcionPagina = "Configure el Servicio de Correo Electrónico del Sistema";
        if (!Valida())
            return;
        if (!IsPostBack)
        {
            txtServidorCorreo.Text = CeC_Config.SevidorCorreo;
            txtServidorSMTP.Text = CeC_Config.SevidorSMTP;
            txtServidorSMTPPuerto.Text = CeC_Config.SevidorSMTPPuerto.ToString();
            txtSMTPNombre.Text = CeC_Config.ServidorSMTPNombreUsuario;
            txtSMTPPass.Text = CeC_Config.ServidorSMTPPass;
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Configuración SMTP", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            if (!Valida())
                return;

            CeC_Config.SevidorCorreo = txtServidorCorreo.Text;
            CeC_Config.SevidorSMTP = txtServidorSMTP.Text;
            CeC_Config.SevidorSMTPPuerto = Convert.ToInt32(txtServidorSMTPPuerto.Text);
            CeC_Config.ServidorSMTPNombreUsuario = txtSMTPNombre.Text;
            CeC_Config.ServidorSMTPPass = txtSMTPPass.Text;
            LOperacion.Text = "Datos guardados satisfactoriamente";
        }
        catch (Exception ex)
        {
            LOperacion.Text = ("Ha Ocurrido Un Error al Guardar la Configuracion");
        }
    }
}
