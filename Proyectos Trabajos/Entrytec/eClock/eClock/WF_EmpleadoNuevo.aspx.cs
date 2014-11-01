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

public partial class WF_EmpleadoNuevo : System.Web.UI.Page
{
    protected int Persona_ID;
    protected CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.WF_Empleados_PERSONA_ID = -1;
        Sesion.WF_EmpleadosBus_Query = "solo para que pase el query";
        Sesion.Redirige("WF_EmpleadosEd.aspx");
    }
}
