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

public partial class WF_Activacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    enum TipoActivacion
    {
        Telefono, Internet
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        TipoActivacion Tipo = new TipoActivacion();

/*        IsProtectServer.CeT_PS Cs = new IsProtectServer.CeT_PS();
        Cs.Directorio = HttpRuntime.AppDomainAppPath;
        Tipo = (TipoActivacion)RBTActiv.SelectedIndex;
        if (txtLlave.Text.Length == 19)
        {
            try
            {
                Cs.LlaveProducto = txtLlave.Text;
                switch (Tipo)
                {
                    case TipoActivacion.Telefono:
                        this.Response.Redirect("WF_ActivTelef.aspx", false);
                        break;
                    case TipoActivacion.Internet:
                        try
                        {
                            Cs.Activar();
                            if (Cs.Coinciden(IsProtectServer.CeT_PS.Productos.eClock))
                            {
                                this.Response.Redirect("WF_Login.aspx", false);
                                CeC_BD.IniciaAplicacion();
                            }
                            else LError.Text = "Clave de Producto No Valida";
                        }
                        catch (Exception ex) 
                        {
                            LError.Text = "No hay conexión a internet, intente la activación por Telefono"; 
                        }
                        //Sesion.Redirige("WF_Login.aspx");
                        //Sesion.CierraSesion();
                        break;
                }
            }
            catch (Exception ex) 
            {
                LError.Text = HttpRuntime.AppDomainAppPath   + " " + ex.Message; CIsLog2.AgregaError(ex); Cs.Directorio = HttpRuntime.AppDomainAppPath; 
            }
        }*/
    }
}
