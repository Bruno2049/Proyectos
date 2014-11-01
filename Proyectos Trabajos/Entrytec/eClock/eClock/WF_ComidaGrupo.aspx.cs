using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_ComidaGrupo : System.Web.UI.Page
{
    CeC_Sesion Sesion = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.Redirige("WF_Comida.aspx?Parametros=AGRUPACION&Agrupacion=" + Sesion.eClock_Agrupacion, true);
    }
}