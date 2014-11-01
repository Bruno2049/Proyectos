using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.LogicaNegocios.ModuloCentral;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;

namespace PAEEEM.CentralModule
{
    public partial class HostoricoProveedor : System.Web.UI.Page
    {


        private List<DatHistoricoProveedores> LstDatosHistorico
        {
            get
            {
                return ViewState["LstDatosHistorico"] == null
                           ? new List<DatHistoricoProveedores>()
                           : ViewState["LstDatosHistorico"] as List<DatHistoricoProveedores>;
            }
            set { ViewState["LstDatosHistorico"] = value; }
        }


        private void BindRegionZonaEstatus()
        {
            RadCbxRegion.DataSource = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catRegion();
            RadCbxRegion.DataTextField = "Dx_Nombre_Region";
            RadCbxRegion.DataValueField = "Cve_Region";
            RadCbxRegion.DataBind();

            RadCbxRegion.Items.Insert(0, new RadComboBoxItem ("Seleccione","0"));
            RadCbxRegion.SelectedIndex = 0;

            RadCbxZona.Items.Insert(0, new RadComboBoxItem("Seleccione región","0"));

            RadCbxStatus.DataSource = HistoricoProveedor.catstatus();
            RadCbxStatus.DataTextField = "Dx_Estatus_Proveedor";
            RadCbxStatus.DataValueField = "Cve_Estatus_Proveedor";
            RadCbxStatus.DataBind();

            RadCbxStatus.Items.Insert(0, new RadComboBoxItem("Seleccione", "0"));

        }

        private void ValidarBotones()
        {
            if (RadTxtDistNC.Text == "" && RadTxtDistRS.Text == "" && RadCbxRegion.SelectedValue == "0" && RadCbxZona.SelectedValue == "0" && RadCbxStatus.SelectedValue == "0")
            {
                btnBuscar.Enabled = false;
                btnLimpiar.Enabled = false;
            }
            else
            {
                btnBuscar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRegionZonaEstatus();

                RadTxtDistNC.Attributes.Add("onblur", "EnableDisable()");
                RadTxtDistNC.Attributes.Add("onKeyUp", "EnableDisable()");

                RadTxtDistRS.Attributes.Add("onblur", "EnableDisable()");
                RadTxtDistRS.Attributes.Add("onKeyUp", "EnableDisable()");

            }
            
        }

        protected void rgHistoricoProveedor_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            LstDatosHistorico = HistoricoProveedor.histoProveedor(RadTxtDistNC.Text, RadTxtDistRS.Text, Convert.ToInt32(RadCbxRegion.SelectedValue), Convert.ToInt32(RadCbxZona.SelectedValue), Convert.ToInt32(RadCbxStatus.SelectedValue));
            rgHistoricoProveedor.DataSource = LstDatosHistorico;
        }

        protected void RadCbxRegion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCbxRegion.SelectedValue == "0")
            {
                RadCbxZona.Items.Clear();
                RadCbxZona.Items.Insert(0,new RadComboBoxItem ("Seleccione región","0"));
                RadCbxZona.SelectedIndex = 0;
            }
            else
            {
                RadCbxZona.DataSource = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZona().FindAll(o => o.Cve_Region == int.Parse(RadCbxRegion.SelectedValue));
                RadCbxZona.DataTextField = "Dx_Nombre_Zona";
                RadCbxZona.DataValueField = "Cve_Zona";
                RadCbxZona.DataBind();

                RadCbxZona.Items.Insert(0,new RadComboBoxItem("Seleccione","0"));
                RadCbxZona.SelectedIndex = 0;
            }
            ValidarBotones();
           
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LstDatosHistorico = HistoricoProveedor.histoProveedor(RadTxtDistNC.Text, RadTxtDistRS.Text, Convert.ToInt32(RadCbxRegion.SelectedValue), Convert.ToInt32(RadCbxZona.SelectedValue), Convert.ToInt32(RadCbxStatus.SelectedValue));
            rgHistoricoProveedor.DataSource = LstDatosHistorico;
            rgHistoricoProveedor.DataBind();
            ValidarBotones();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            RadTxtDistNC.Text = "";
            RadTxtDistRS.Text = "";
            RadCbxRegion.SelectedIndex = 0;
            RadCbxZona.Items.Clear();
            RadCbxZona.Items.Insert(0, new RadComboBoxItem("Seleccione región", "0"));
            RadCbxStatus.SelectedIndex = 0;

            btnLimpiar.Enabled = false;
            btnBuscar.Enabled = false;
          //  rgHistoricoProveedor.Columns.Clear();
            ValidarBotones();
        }


        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //for (int i = 0; i < Props.Length; i++)
            //{
            //    dataTable.Columns.Add(Props[i].Name);
            //}
            dataTable.Columns.Add("region", typeof(System.String));
            dataTable.Columns.Add("zona", typeof(System.String));
            dataTable.Columns.Add("idProveedor", typeof(System.Int32));
            dataTable.Columns.Add("tipo", typeof(System.String));
            dataTable.Columns.Add("NomNC", typeof(System.String));
            dataTable.Columns.Add("NomRS", typeof(System.String));
            dataTable.Columns.Add("Status", typeof(System.String));
            dataTable.Columns.Add("fechaEstatus", typeof(System.DateTime));
            dataTable.Columns.Add("motivo", typeof(System.String));
            dataTable.Columns.Add("usuario", typeof(System.String));
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
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
            DataTable dt = ToDataTable(LstDatosHistorico.ToList());
            ExportToPdf(dt);
        }
        protected void imgExportaExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = ToDataTable(LstDatosHistorico.ToList());
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

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }


  


    }
}