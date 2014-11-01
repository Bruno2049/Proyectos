using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Pdf;
using PAEEEM.LogicaNegocios.Validacion_RFC_L;
using PAEEEM.Entidades;
using System.IO;

namespace PAEEEM.CentralModule
{
    public partial class VisorPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack || string.IsNullOrEmpty(Request.QueryString["Token"])) return;

            var ID_Validacion = Request.QueryString["Token"];
            System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"]));
            var Reg = Validacion_RFC_L.ClassInstance.TraeRegistro(Convert.ToInt32(ID_Validacion));

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(Reg.Comprobante);
            Response.End();
        }
    }
}