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

public partial class WF_AsistenciasGrupo : System.Web.UI.Page
{


    CeC_Sesion Sesion = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.Redirige("WF_AsistenciasEmp.aspx?Parametros=AGRUPACION&Agrupacion=" + Sesion.eClock_Agrupacion,true);
    }


}
