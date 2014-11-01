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

public partial class WF_Terminal_Imagen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            CeC_Sesion Sesion;
            Sesion = CeC_Sesion.Nuevo(this);
            byte[] Imagen = CeC_Terminales_DExtras.ObtenBin(Sesion.WF_Terminales_TERMINALES_ID, 114);
            if (Imagen != null)
            {
                Response.ContentType = "image/Bmp";
                Response.BinaryWrite(Imagen);
            }
        }
        catch
        {
        }
    }
}