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

public partial class WF_Wizarde : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.PERFIL_ID != 1)
        {
            BGuardarCambios.Visible = false;
            WebPanel1.Visible = false;
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");

        }
        if (!IsPostBack)
        {
            Sesion.TituloPagina = "Selección de imágenes";
            Sesion.DescripcionPagina = "Seleccione las imágenes que desea almacenar en el sistema";
            CBFirma.Checked = CeC_Config.FirmaActiva;
            CBFotografia.Checked = CeC_Config.FotografiaActiva;
            CBHuella.Checked = CeC_Config.HuellaActiva;
            this.Master.FindControl("WC_Menu1").FindControl("mnu_Main").Visible = !Convert.ToBoolean(Sesion.EsWizard);
            this.Master.FindControl("WCBotonesEncabezado1").Visible = !Convert.ToBoolean(Sesion.EsWizard);
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asistente de Configuración", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            CeC_Config.FirmaActiva = CBFirma.Checked;
            CeC_Config.FotografiaActiva = CBFotografia.Checked;
            CeC_Config.HuellaActiva = CBHuella.Checked;
            //Sesion.Redirige("WF_Importacion.aspx");
            LCorrecto.Text = "Los cambios se han guardado correctamente";
        }
        catch
        {
            LError.Text = "No se han podido guardar los cambios";
        }
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Main.aspx");
        //Page_Load(sender, e);
    }
}
