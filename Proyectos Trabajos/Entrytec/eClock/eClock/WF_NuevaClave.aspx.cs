using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_NuevaClave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LCorrecto.Text = "";
        LError.Text = "";
    }
    protected void WebImageButton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        int IDUsuario = CeC_Usuarios.ObtenUsuarioID(Tbx_Usuario.Text);
        if (IDUsuario <= 0)
        {
            LError.Text = "Usuario no exixte";
            return;
        }
        if (CeC_Usuarios.EnviaPassword(IDUsuario))
        {
            LCorrecto.Text = "Se ha enviado su clave de acceso via email";
            return;
        }
        LError.Text = "No se pudo obtener su clave";
        return;
    }
}
