using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PAEEEM.DisposalModule;
using PAEEEM.LogicaNegocios.ModuloCentral;
using Telerik.Web.UI;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.CentralModule
{
    public partial class NoSujetoCredito : System.Web.UI.Page
    {
        
        //public List<MotivosCredito> Motivos
        public IEnumerable<MotivosCredito> Motivos
        {
            set { Session["motivos"] = value; }
            get { return (IEnumerable<MotivosCredito>)Session["motivos"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            imgExportaExcel.Visible = false;
            imgExportaPDF.Visible = false;
            btnBuscar.Enabled = true;
            txtRPU.Attributes.Add("onKeyUp", "Validarfiltros()");
            txtRPU.Attributes.Add("onchange", "Validarfiltros()");
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
            
        }

        private void llenarGrid()
        {
            string rpu = txtRPU.Text;
            NoCredito NC = new NoCredito();
            int con = 0;
            #region
            //var cc = NC.ConsultaCancelaciones(rpu);
            //foreach (var item in cc)
            //{
            //    con++;
            //    item.NoIntentos = con.ToString();
            //}
            //var pi = NC.ConsultaPresupuestoInversion(rpu);
            //foreach (var item in pi)
            //{
            //    con++;
            //    item.NoIntentos = con.ToString();
            //}
            //var cv = NC.ConsultaValidacionRpu(rpu);
            //foreach (var item in cv)
            //{
            //    con++;
            //    item.NoIntentos = con.ToString();
            //}
            //var ccMop = NC.ConsultaCrediticiaMopNoValido(rpu);
            //foreach (var item in ccMop)
            //{
            //    con++;
            //    item.NoIntentos = con.ToString();
            //}
            //var ecc = NC.ExcedeConsultasCrediticias(rpu);
            //foreach (var item in ecc)
            //{
            //    con++;
            //    item.NoIntentos = con.ToString();
            //}
            
            //Motivos = cc.Concat(pi).Concat(cv).Concat(ccMop).Concat(ecc);

#endregion
            var cons = NC.Consulta(rpu);

            var EA = cons.Where(c => c.Secuencia_E_Alta > 0).GroupBy(a => a.Secuencia_E_Alta).Select(b => b.FirstOrDefault());
            var EB = cons.Where(c => c.Secuencia_E_Baja > 0).GroupBy(a => a.Secuencia_E_Baja).Select(b => b.FirstOrDefault());
            var equipos = EA.Concat(EB).Distinct().ToList();
            var res = cons.Where(c => c.Secuencia_E_Alta == 0&&c.Secuencia_E_Baja==0);
            
            //var final=res.Concat(equipos).ToList();
            var final = equipos.Concat(res).ToList();
            foreach (var motivosCredito in final)
            {
                con++;
                motivosCredito.NoIntentos = con.ToString();
                if (motivosCredito.Causa == "PRESUPUESTO DE INVERSIÓN")
                {
                    foreach (var c in cons)
                    {
                        if (c.Secuencia_E_Alta == motivosCredito.Secuencia_E_Alta &&
                            c.Secuencia_E_Baja == motivosCredito.Secuencia_E_Baja)
                            c.NoIntentos = con.ToString();
                    }
                }

            }
            Motivos = cons;
            


            if (Motivos.ToList().Count > 0)
            {
                imgExportaExcel.Visible = false;
                imgExportaPDF.Visible = true;
                tit1.Visible = true;
                tit2.Visible = true;
                RadGrid1.DataSource = Motivos;
                RadGrid1.DataBind();
                var equiposA = new List<EquiposAlta>();
                var equiposB = new List<EquiposBaja>();

                if (EA != null)
                {
                    foreach (var item in EA)
                    {
                        var EATemp = NC.EquiposAltas(rpu, item.Secuencia_E_Alta, item.NoIntentos);

                        foreach (var b in EATemp)
                        {
                            equiposA.Add(b);
                        }
                    }

                    RadGrid3.DataSource = equiposA;
                    RadGrid3.DataBind();
                }

                if (EB != null)
                {
                    foreach (var item in EB)
                    {

                        var EBTemp = NC.EquiposBajas(rpu, item.Secuencia_E_Baja, item.NoIntentos);
                        foreach (var a in EBTemp)
                        {

                            equiposB.Add(a);
                        }
                    }
                    RadGrid2.DataSource = equiposB;
                    RadGrid2.DataBind();
                }
            }
            else
            {
                imgExportaExcel.Visible = false;
                imgExportaPDF.Visible = false;
                tit1.Visible = false;
                tit2.Visible = false;
                RadWindowManager1.RadAlert("No se encontraron resultados para el RPU ingresado", 300, 150, "Busqueda",
                    null);
            }

        }
    

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //foreach (PropertyInfo prop in Props)
            //{
            //    //Setting column names as Property names
            //    dataTable.Columns.Add(prop.Name);
            //}

            ////for (int i = 0; i < Props.Length - 2; i++)
            ////{
            ////    dataTable.Columns.Add(Props[i].Name);
            ////}
            
            dataTable.Columns.Add("NoIntentos", typeof(System.String));
            dataTable.Columns.Add("NombreRazonSocial", typeof(System.String));
            dataTable.Columns.Add("Causa", typeof(System.String));
            dataTable.Columns.Add("Motivo", typeof(System.String));
            dataTable.Columns.Add("Datos", typeof(System.String));
            dataTable.Columns.Add("Fecha", typeof(System.DateTime));
            dataTable.Columns.Add("Zona", typeof(System.String));
            dataTable.Columns.Add("Region", typeof(System.String));
            dataTable.Columns.Add("Distribuidor", typeof(System.String));
            dataTable.Columns.Add("IdTrama", typeof(System.String));
            foreach (T item in items)
            {
                var values = new object[Props.Length-2];
                for (int i = 0; i < Props.Length-2; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public void ExportToPdf(DataTable ExDataTable) //Datatable 
        {
            //Here set page size as A4

            Document pdfDoc = new Document(PageSize.A2, 5f, 5f, 5f, 5f); //(PageSize.A4, 10, 10, 10, 10);

            try
            {
                PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                //Set Font Properties for PDF File
                Font fnt = FontFactory.GetFont("Times New Roman", 7);
                DataTable dt = ExDataTable;

                if (dt != null)
                {

                    PdfPTable PdfTable = new PdfPTable(dt.Columns.Count);
                    PdfPCell PdfPCell = null;

                    //Here we create PDF file tables

                    for (int rows = 0; rows < dt.Rows.Count; rows++)
                    {
                        if (rows == 0)
                        {
                            for (int column = 0; column < dt.Columns.Count; column++)
                            {
                                PdfPCell =
                                    new PdfPCell(new Phrase(new Chunk(dt.Columns[column].ColumnName.ToString(), fnt)));
                                PdfTable.AddCell(PdfPCell);
                            }
                        }
                        for (int column = 0; column < dt.Columns.Count; column++)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), fnt)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }

                    // Finally Add pdf table to the document 
                    pdfDoc.Add(PdfTable);
                }

                pdfDoc.Close();

                Response.ContentType = "application/pdf";

                //Set default file Name as current datetime
                Response.AddHeader("content-disposition", "attachment; filename=Motivos.pdf");

                System.Web.HttpContext.Current.Response.Write(pdfDoc);

                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void imgExportaPDF_Click(object sender, ImageClickEventArgs e)
        {
            //llenarGrid();
            DataTable dt = ToDataTable(Motivos.ToList());
            ExportToPdf(dt);
        }

        protected void imgExportaExcel_OnClick(object sender, ImageClickEventArgs e)
        {
            DataTable dt = ToDataTable(Motivos.ToList());
            //dt = city.GetAllCity();//your datatable
            string attachment = "attachment; filename=Motivos.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }


        protected void RadGrid1_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ExportToExcel")
            {
                RadGrid1.ExportSettings.ExportOnlyData = false;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.OpenInNewWindow = true;
                RadGrid1.ExportSettings.FileName = "Consulta_Solicitudes_" + txtRPU.Text;
                RadGrid1.MasterTableView.ExportToCSV();
            }

            if (e.CommandName == "ExportToPdf")
            {
                RadGrid1.ExportSettings.ExportOnlyData = false;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.OpenInNewWindow = true;
                RadGrid1.ExportSettings.FileName = "Consulta_Solicitudes_" + txtRPU.Text;
                RadGrid1.ExportSettings.Pdf.PaperSize = GridPaperSize.Letter;
                RadGrid1.MasterTableView.ExportToPdf();
            }
        }
    }
}