using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Solicitudes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CeC_BD.IniciaAplicacion();
            if (Request.Params["A"] != null)
            {
                string A = Request.Params["A"].ToString();
                int R = CeC_Solicitudes.Autoriza(this, A, true);
                if (R <= 0)
                    Lbl_Msg.Text = "No se pudo autorizar la solicitud (" + R + ")" ;
                else
                    Lbl_Msg.Text = "Solicitud Autorizada";
            }

            if (Request.Params["D"] != null)
            {
                string D = Request.Params["D"].ToString();
                int R = CeC_Solicitudes.Deniega(this, D);
                if (R <= 0)
                    Lbl_Msg.Text = "No se pudo denegar la solicitud (" + R + ")" ;
                else
                    Lbl_Msg.Text = "Solicitud denegada";
            }


        }
        catch { }

    }
}