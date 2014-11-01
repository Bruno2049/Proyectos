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

public partial class MasterPage : System.Web.UI.MasterPage
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this.Page);
        WCBotonesEncabezado1.Sesion = Sesion;
        //img_Icono.ImageUrl = "~/Imagenes.Main/IconosPagina/" + Sesion.Pagina_Actual + ".ico.png";
        //LTitulo.Text = Sesion.TituloPagina;
        //LDescripcion.Text = Sesion.DescripcionPagina;
    }
    protected void WCBotonesEncabezado1_Load(object sender, EventArgs e)
    {
        
    }
}
