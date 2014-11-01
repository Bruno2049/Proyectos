using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_EM_Retardos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("WF_EM_Faltas.aspx?Parametros=" + this.Request.Params["Parametros"].ToString(), true);
    }
}
