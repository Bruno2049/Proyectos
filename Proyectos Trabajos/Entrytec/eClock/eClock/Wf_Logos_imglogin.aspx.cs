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

public partial class Wf_Logos_imglogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(System.IO.File.ReadAllBytes(HttpRuntime.AppDomainAppPath + "\\Imagenes\\imglogin.jpg"));
        }
        catch
        {
            try
            {
                byte[] Ima = CeC_BD.ObtenImagen("imglogin");
                if (Ima != null)
                {
                    Response.ContentType = "image/Jpeg";
                    Response.BinaryWrite(Ima);
                }
            }
            catch { }
        }
    }
}
