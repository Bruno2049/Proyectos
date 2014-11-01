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

public partial class WF_DatosEmpresa : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Datos de la Empresa";
        Sesion.DescripcionPagina = "Ingrese los datos de su empresa";
        if (Sesion.SUSCRIPCION_ID != 1 || !Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Configuracion))
        {
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            return;
        }
        if (!IsPostBack)
        {
            TxtDireccion.Text = Sesion.ConfiguraSuscripcion.CompaniaDireccion;
            TxtNombre.Text = Sesion.ConfiguraSuscripcion.CompaniaNombre;
            TxtURL.Text = Sesion.ConfiguraSuscripcion.CompaniaURL;
            TxtTelefono.Text = Sesion.ConfiguraSuscripcion.CompaniaTelefono;
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Datos de la Empresa", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            Sesion.ConfiguraSuscripcion.CompaniaDireccion = TxtDireccion.Text;
            Sesion.ConfiguraSuscripcion.CompaniaNombre = TxtNombre.Text;
            Sesion.ConfiguraSuscripcion.CompaniaURL = TxtURL.Text;
            Sesion.ConfiguraSuscripcion.CompaniaTelefono = TxtTelefono.Text;
            Lbl_Error.Text = "";
            Lbl_Correcto.Text = "Datos guardados correctamente.";
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            Lbl_Error.Text = "Hubo errores al guardar los datos. Revise el Log para mayor información.";
            Lbl_Correcto.Text = "";
        }
    }
    protected void BGuardarCambios0_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            CeC_Campos.gAsignaEtiquetasPredeterminadas();
            Lbl_Error.Text = "";
            Lbl_Correcto.Text = "Finalizado: Campos inicializados.";
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            Lbl_Error.Text = "Hubo errores al inicializar los campos. Revise el Log para mayor información.";
            Lbl_Correcto.Text = "";
        }
    }
}
