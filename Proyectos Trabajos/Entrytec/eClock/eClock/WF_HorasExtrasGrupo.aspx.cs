using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_HorasExtrasGrupo : System.Web.UI.Page
{
    CeC_Sesion Sesion = null;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.Parametros.Length > 0)
            Sesion.eClock_Agrupacion = Sesion.Parametros;
        Sesion.Redirige("WF_HorasExtras.aspx?Parametros=AGRUPACION&Agrupacion=" + Sesion.eClock_Agrupacion, true);

    }
}
