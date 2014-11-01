using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.Vendedores;

namespace PAEEEM.Vendedores
{
    public partial class VisorImagenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var id = Request.QueryString["Id"];
                var tipo = Request.QueryString["Tipo"];

                if (tipo == "1")
                {
                    var foto = (TempImagen) Session["ImagenTemp"];

                    if (foto != null)
                    {
                        var str = new MemoryStream();
                        str.Write(foto.Imagen, 0, foto.Imagen.Length);
                        var bit = new Bitmap(str);

                        Response.ContentType = "image/jpeg";
                        bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        rwmVentana.RadAlert("No existe la imagen solicitada", 300, 150, "Visor de Imagenes", null);
                    }
                }
                else if (tipo == "2")
                {
                    var fotos=(List<TempImagen>) Session["ImagenesTemp"];
                    var foto = fotos.FindAll(me => me.CURP == id).FirstOrDefault();

                    if (foto != null)
                    {
                        var str = new MemoryStream();
                        str.Write(foto.Imagen, 0, foto.Imagen.Length);
                        var bit = new Bitmap(str);

                        Response.ContentType = "image/jpeg";
                        bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        rwmVentana.RadAlert("No existe la imagen solicitada", 300, 150, "Visor de Imagenes", null);
                    }
                }
            }

            catch (Exception ex)
            {
                rwmVentana.RadAlert(ex.Message, 300, 150, "Visor de Imagenes", null);
            }
        }
    }
}