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

public partial class WF_TurnoNuevo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CeC_Sesion Sesion;
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.WF_Turnos_TURNO_ID = -1;
        Sesion.Redirige("WF_TurnosEdicion.aspx");
    }
}
