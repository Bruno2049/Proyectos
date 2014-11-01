using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades.CirculoCredito;
using System.IO;

namespace PAEEEM.CentralModule
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
                var pdfs = (List<TemporalPDFs>)Session["Listapdf"];
                if (referen == "carga")
                {
                    if (tipo == "carta")
                    {
                    
                        
                        var t = pdfs.FindAll(c => c.nocredit == credit).ToList();
                        if (t != null)
                        {

                            var str = new MemoryStream();
                            str.Write(t.Single().acta, 0, t.Single().acta.Length);

                            Response.Clear();
                            Response.Buffer = true;
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition", "attachment;filename=" + "Poder Notarial.pdf");
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.BinaryWrite(t.Single().acta);
                            Response.End();
                          
                        }
                    }
                }
                var id = Request.QueryString["Id"];

                if (id == "1")
                {

                    var foto = (byte[])Session["pdf1"];
                    //var str = new MemoryStream();


                    //str.Write(foto, 0, foto.Length);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "PODER NOTARIAL.pdf");
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(foto);
                    Response.End();
                }
                else if (id == "2")
                {
                    var foto = (byte[])Session["pdf2"];
                    //var str = new MemoryStream();


                    //str.Write(foto, 0, foto.Length);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "ACTA CONSTITUTIVA.pdf");
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(foto);
                    Response.End();
                }
                else if (id == "4")
                {
                    var foto = (byte[])Session["pdf_Pod_Not_Temp"];                  

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "PODER NOTARIAL.pdf");
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(foto);
                    Response.End();
                }
                else if (id == "5")
                {
                    var foto = (byte[])Session["pdf_ActaConst_Temp"];

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "ACTA CONSTITUTIVA.pdf");
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(foto);
                    Response.End();
                }
                else if(id=="3")
                {
                    var foto = (byte[])Session["pdf_notarial"];
                    //var str = new MemoryStream();


                    //str.Write(foto, 0, foto.Length);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "PODER NOTARIAL SUC_FIS.pdf");
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(foto);
                    Response.End();
                }
            }
            catch (Exception m)
            {
                rwmVentana.RadAlert("ERROR: " + m.Message, 300, 150, "Datos Generales", null);

            }
        
        
        }
        
    }
}