using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Reflection.Emit;
using System.Globalization;


public partial class eClockEmpleado : System.Web.UI.Page
{
   

    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion == null)
            return;
        
    }

    protected void Btn_Usuario_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("eClockEmpleado.aspx");
    }
    protected void Btn_Principal_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("eClockEmpleado.aspx");
    }
    protected void Btn_Salir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.CierraSesion();
        Sesion.Redirige("WF_Login.aspx");
    }
}
