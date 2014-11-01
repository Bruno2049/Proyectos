using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.LogicaNegocios.MRV;

namespace PAEEEM.MRV
{
    public partial class VisorImagenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var id = Request.QueryString["Id"];
                var tipo = Request.QueryString["IdT"];
                
                if (tipo == "1")
                {
                    var foto = new CuestionarioLN().ObtenFotoCuestionario(int.Parse(id));

                    if (foto != null)
                    {
                        var str = new MemoryStream();
                        str.Write(foto.Foto, 0, foto.Foto.Length);
                        var bit = new Bitmap(str);

                        Response.ContentType = "image/jpeg";
                        bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        rwmVentana.RadAlert("No existe la imagen solicitada", 300, 150, "Visor de Imagenes", null);
                    }
                }
                
                if (tipo == "2")
                {
                    var foto = new MedicionesLN().ObtenFotoMedicion(int.Parse(id));

                    if (foto != null)
                    {
                        var type = "";
                        var str = new MemoryStream();
                        str.Write(foto.Foto, 0, foto.Foto.Length);
                        //var bit = new Bitmap(str);

                        switch (foto.Extension)
                        {
                            case ".xls":
                                type = "application/vnd.ms-excel";
                                break;

                            case ".xlsx":
                                type = "application/vnd.ms-excel";

                                break;
                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = type;
                        Response.AddHeader("content-disposition", "attachment;filename=" + foto.Nombre); 
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(foto.Foto);
                        Response.End();
                        //Response.ContentType = type;
                        //bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        rwmVentana.RadAlert("No el archivo solicitado", 300, 150, "Visor de Archivos", null);
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