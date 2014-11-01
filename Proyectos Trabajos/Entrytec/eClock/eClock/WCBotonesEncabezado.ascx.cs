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

public partial class WCBotonesEncabezado : System.Web.UI.UserControl
{
    public CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
 
    
    }
    protected void Btn_Salir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this.Page);
        Sesion.CierraSesion();
        Sesion.Redirige("WF_Login.aspx");
    }
    protected void Btn_Usuario_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.WF_Usuarios_USUARIO_ID= Sesion.USUARIO_ID;

        Sesion.Redirige("eClock.aspx?Parametros=WF_UsuarioE.aspx");
    }
    protected void Btn_Principal_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this.Page);
        Sesion.Redirige("eClock.aspx");
    }
}

