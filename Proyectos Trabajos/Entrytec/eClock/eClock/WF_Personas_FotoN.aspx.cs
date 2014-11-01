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

public partial class WF_Personas_FotoN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            CeC_Sesion Sesion;
            Sesion = CeC_Sesion.Nuevo(this);
            CeC_SesionBD SesionBD = new CeC_SesionBD(Sesion.SESION_ID);
            byte[] Ima = SesionBD.FotoNueva;


            if (Ima != null)
            {
                Response.ContentType = "image/Jpeg";
                Response.BinaryWrite(Ima);
            }
        }
        catch
        {
        }
    }
}
