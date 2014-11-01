using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.CirculoCredito;
using System.IO;

namespace PAEEEM.CirculoCredito
{
    public partial class VisorImagenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var credit = Request["Id"];
                var tipo = Request["Tipo"];
                var referen = Request["Referencia"];
                var foto = (List<TemporalPDFs>)Session["CirculoCreditTemporal"];
                var consulta = (List<ConsultaPaquetes>)Session["ConsultaPaquete"];
                var revision = (List<ConsultaPaquetes>)Session["LstDatosRevision"];
                if (referen == "carga")
                {
                    if (tipo == "carta")
                    {
                        
                        var t = foto.FindAll(c => c.nocredit == credit).ToList();
                        if (t != null)
                        {

                            var str = new MemoryStream();
                            str.Write(t.Single().carta, 0, t.Single().carta.Length);
                            //var bit = new Bitmap(str);



                            Response.Clear();
                            Response.Buffer = true;
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=" + "Carta");
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(t.Single().carta);
                            Response.End();
                            //Response.ContentType = type;
                            //bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                        }
                        else
                        {
                            circuloVentana.RadAlert("No es el archivo solicitado", 300, 150, "Visor de Archivos", null);
                        }
                    }
                    else if (tipo == "acta")
                    {

                        var t = foto.FindAll(c => c.nocredit == credit).ToList();
                        if (t != null)
                        {

                            var str = new MemoryStream();
                            str.Write(t.Single().acta, 0, t.Single().acta.Length);
                            //var bit = new Bitmap(str);



                            Response.Clear();
                            Response.Buffer = true;
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=" + "Carta");
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(t.Single().acta);
                            Response.End();
                            //Response.ContentType = type;
                            //bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                        }
                        else
                        {
                            circuloVentana.RadAlert("No es el archivo solicitado", 300, 150, "Visor de Archivos", null);
                        }
                    }
 
                }
                else if (referen == "consulta")
                {
                    if (tipo == "carta")
                    {

                        var l = consulta==null?revision.ToList():consulta.ToList();
                        var t = l.FindAll(o => o.noCredit == credit).ToList();
                        //var r= revision.FindAll(o => o.noCredit == credit).ToList();
                        if ( t!= null)
                        {

                            var str = new MemoryStream();
                            str.Write(t.Single().carta, 0, t.Single().carta.Length);
                            //var bit = new Bitmap(str);



                            Response.Clear();
                            Response.Buffer = true;
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=" + "Carta");
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(t.Single().carta);
                            Response.End();
                            //Response.ContentType = type;
                            //bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                        }

                        else
                        {
                            circuloVentana.RadAlert("No es el archivo solicitado", 300, 150, "Visor de Archivos", null);
                        }
                    }
                    else if (tipo == "acta")
                    {
                        var l = consulta == null ? revision.ToList() : consulta.ToList();
                        var t = l.FindAll(o => o.noCredit == credit).ToList();
                        if (t != null)
                        {

                            var str = new MemoryStream();
                            str.Write(t.Single().acta, 0, t.Single().acta.Length);
                            //var bit = new Bitmap(str);



                            Response.Clear();
                            Response.Buffer = true;
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=" + "Carta");
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(t.Single().acta);
                            Response.End();
                            //Response.ContentType = type;
                            //bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                        }
                        else
                        {
                            circuloVentana.RadAlert("No es el archivo solicitado", 300, 150, "Visor de Archivos", null);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                circuloVentana.RadAlert(ex.Message, 300, 150, "Visor de Imagenes", null);
            }
        }
    }
}