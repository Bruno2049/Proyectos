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

public partial class WF_ActivTelef : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
/*        IsProtectServer.CeT_PS PS = new IsProtectServer.CeT_PS();
        PS.Directorio = HttpRuntime.AppDomainAppPath;
        LProducto.Text = PS.LlaveProducto;
        LTelefono.Text= PS.MaquinaID;*/
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
    /*    IsProtectServer.CeT_PS PS = new IsProtectServer.CeT_PS();
        PS.Directorio = HttpRuntime.AppDomainAppPath;
        PS.Activacion = txtActivTelef.Text;
        if (!PS.Coinciden(IsProtectServer.CeT_PS.Productos.eClock))
            this.Response.Redirect("WF_ActivTelef.aspx");
        else
        {
            this.Response.Redirect("WF_Login.aspx");
            CeC_BD.IniciaAplicacion();
    //    Sesion.CierraSesion();
        }*/
        this.Response.Redirect("WF_Login.aspx");
    }
}
