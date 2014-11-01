using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using PAEEEM.Entidades.ModuloCentral;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using Control = System.Web.UI.Control;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace PAEEEM.SolicitudCredito
{
    public partial class DatosCredito : System.Web.UI.Page
    {


        public string NoCredito { set; get; }
        ////int Filter = 0;
        ////int RoleType = 0;
        public List<DatosConsulta> _creditos
        {
            set { Session["_creditos"] = value; }
            get { return (List<DatosConsulta>) Session["_creditos"]; }
        }

        public int Filter
        {
            set { Session["Filter"] = value; }
            get { return (int) Session["Filter"]; }
        }

        public int RoleType
        {
            set { Session["RoleType"] = value; }
            get { return (int) Session["RoleType"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                llenarDRP();
                atributos();
                btnLimpiar.Enabled = false;
                btnBuscar.Enabled = false;
                imgExportaExcel.Visible = false;
                imgExportaPDF.Visible = false;
                if (_creditos != null)
                {
                    llenarGrid();
                    AspNetPager2.Visible = true;
                }
            }
        }

        private void atributos()
        {
            txtSolicitud.Attributes.Add("onKeyUp", "Validarfiltros()");
            txtSolicitud.Attributes.Add("onchange", "Validarfiltros()");
            txtRPU.Attributes.Add("onKeyUp", "Validarfiltros()");
            txtRFC.Attributes.Add("onKeyUp", "Validarfiltros()");
            drpEstatus.Attributes.Add("onKeyUp", "Validarfiltros()");
            drpRegion.Attributes.Add("onKeyUp", "Validarfiltros()");
            drpZona.Attributes.Add("onKeyUp", "Validarfiltros()");
            txtDistrRS.Attributes.Add("onKeyUp", "Validarfiltros()");
            txtDistrNC.Attributes.Add("onKeyUp", "Validarfiltros()");
            rdpFechaInicio.Attributes.Add("onKeyUp", "Validarfiltros()");
            rdpFechaFin.Attributes.Add("onKeyUp", "Validarfiltros()");
            txtRPU.Attributes.Add("onchange", "Validarfiltros()");
            txtRFC.Attributes.Add("onchange", "Validarfiltros()");
            txtDistrRS.Attributes.Add("onchange", "Validarfiltros()");
            txtDistrNC.Attributes.Add("onchange", "Validarfiltros()");
        }

        private void llenarGrid()
        {

            if (_creditos.Count() > 0)
            {
                //gvresultadosBusqueda.DataSource = _creditos;
                //gvresultadosBusqueda.DataBind();
                gvresultadosBusqueda.DataSource = _creditos;
                gvresultadosBusqueda.DataBind();

                var rownum = 1;

                foreach (var VarGlob in _creditos)
                {
                    VarGlob.Rownum = rownum;
                    rownum++;
                }

                AspNetPager2.RecordCount = _creditos.Count;

                imgExportaPDF.Visible = true;
                imgExportaExcel.Visible = true;
            }
            else
            {
                imgExportaPDF.Visible = false;
                imgExportaExcel.Visible = false;
                ScriptManager.RegisterStartupScript(this, typeof (Page), "AlertInform",
                    "alert('No se encontró ninguna coincidencia, por favor intente con otro criterio');",
                    true);
            }
        }

        private void llenarDRP()
        {
            if (Session["UserInfo"] != null)
            {
                Filter = ((US_USUARIOModel) Session["UserInfo"]).Id_Departamento;
                RoleType = ((US_USUARIOModel) Session["UserInfo"]).Id_Rol;
            }

            if (Filter >= 0)
            {
                if (RoleType == (int) UserRole.ZONE)
                {
                    //zona
                    var zon = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZona(Filter);
                    int reg = (int) zon.FirstOrDefault().Cve_Region;
                    drpRegion.DataSource = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catRegion(reg);
                    drpRegion.DataTextField = "Dx_Nombre_Region";
                    drpRegion.DataValueField = "Cve_Region";
                    drpRegion.DataBind();
                    drpRegion.SelectedIndex = 0;

                    drpZona.DataSource = zon;
                    drpZona.DataTextField = "Dx_Nombre_Zona";
                    drpZona.DataValueField = "Cve_Zona";
                    drpZona.DataBind();
                    drpZona.SelectedIndex = 0;

                }
                else if (RoleType == (int) UserRole.REGIONAL)
                {
                    //region
                    drpRegion.DataSource = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catRegion(Filter);
                    drpRegion.DataTextField = "Dx_Nombre_Region";
                    drpRegion.DataValueField = "Cve_Region";
                    drpRegion.DataBind();
                    drpRegion.SelectedIndex = 0;

                    drpZona.DataSource =
                        LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZona()
                            .FindAll(reg => reg.Cve_Region == int.Parse(drpRegion.SelectedValue));
                    drpZona.DataTextField = "Dx_Nombre_Zona";
                    drpZona.DataValueField = "Cve_Zona";
                    drpZona.DataBind();
                    drpZona.Items.Insert(0, new ListItem("Seleccione una Region", "0"));
                    drpZona.SelectedIndex = 0;

                }
                else if (RoleType == (int) UserRole.CENTRALOFFICE)
                {
                    drpRegion.DataSource = LogicaNegocios.ModuloCentral.CatalogosRegionZona.catRegion();
                    drpRegion.DataTextField = "Dx_Nombre_Region";
                    drpRegion.DataValueField = "Cve_Region";
                    drpRegion.DataBind();
                    drpRegion.Items.Insert(0, new ListItem("Seleccione", "0"));
                    drpRegion.SelectedIndex = 0;

                    drpZona.Items.Insert(0, new ListItem("Seleccione una Region", "0"));
                    drpZona.SelectedIndex = 0;
                }
            }

            drpEstatus.DataSource = LogicaNegocios.Credito.ConsultaCredito.EstatusCredito();
            drpEstatus.DataTextField = "Dx_Estatus_Credito";
            drpEstatus.DataValueField = "Cve_Estatus_Credito";
            drpEstatus.DataBind();
            drpEstatus.Items.Insert(0, new ListItem("Seleccione", "0"));
            drpEstatus.SelectedIndex = 0;

        }

        private void limpiar()
        {
            txtSolicitud.Text = "";
            txtRPU.Text = "";
            txtRFC.Text = "";
            drpEstatus.SelectedIndex = 0;
            drpEstatus.SelectedIndex = 0;
            drpRegion.SelectedIndex = 0;
            txtDistrRS.Text = "";
            txtDistrNC.Text = "";
            drpZona.SelectedIndex = 0;
            rdpFechaInicio.SelectedDate = null;
            rdpFechaFin.SelectedDate = null;
            btnBuscar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        private void consultar()
        {
            string noCredito = txtSolicitud.Text;
            string rpu = txtRPU.Text;
            string rfc = txtRFC.Text;
            string fs = drpEstatus.SelectedItem.Text;
            int estatus = int.Parse(drpEstatus.SelectedValue);
            int region = int.Parse(drpRegion.SelectedItem.Value);
            string distrRS = txtDistrRS.Text;
            string distrNC = txtDistrNC.Text;
            int zona = int.Parse(drpZona.SelectedItem.Value);
            DateTime? FechaINI = null;
            DateTime? FechaFIN = null;
            try
            {
                FechaINI = (DateTime) rdpFechaInicio.SelectedDate.Value;
            }
            catch (Exception)
            {
                FechaINI = null;
            }
            try
            {
                FechaFIN = (DateTime) rdpFechaFin.SelectedDate.Value;
            }
            catch (Exception)
            {
                FechaFIN = null;
            }
            _creditos = LogicaNegocios.Credito.ConsultaCredito.DatosBusquedaSolicitudes(noCredito, rpu, rfc, estatus,
                region, distrRS, distrNC, zona, FechaINI, FechaFIN);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            consultar();
            llenarGrid();
            AspNetPager2.Visible = true;
            //NoCredito = txtSolicitud.Text;
            //cargarDatos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void gvresultadosBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Buscar")
            {
                NoCredito = e.CommandArgument.ToString();
            }
        }

        protected void AspNetPager2_PageChanged(object sender, EventArgs e)
        {

            CargaGridCustomizablesPlantilla2();
        }

        protected void drpRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSolicitud.Text != "" || txtRPU.Text != "" || txtRFC.Text != "" || drpEstatus.SelectedValue != "0" ||
                drpRegion.SelectedValue != "0" || drpZona.SelectedValue != "0" || txtDistrRS.Text != "" ||
                txtDistrNC.Text != "")
            {
                btnBuscar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
                btnLimpiar.Enabled = false;
            }
            if (RoleType != (int) UserRole.ZONE)
            {
                drpZona.DataSource =
                    LogicaNegocios.ModuloCentral.CatalogosRegionZona.catZona()
                        .FindAll(reg => reg.Cve_Region == int.Parse(drpRegion.SelectedValue));
                drpZona.DataTextField = "Dx_Nombre_Zona";
                drpZona.DataValueField = "Cve_Zona";
                drpZona.DataBind();
                drpZona.Items.Insert(0, new ListItem("Seleccione", "0"));
                drpZona.SelectedIndex = 0;
            }
        }

        protected void drpEstatus_TextChanged(object sender, EventArgs e)
        {
            if (txtSolicitud.Text != "" || txtRPU.Text != "" || txtRFC.Text != "" || drpEstatus.SelectedValue != "0" ||
                drpRegion.SelectedValue != "0" || drpZona.SelectedValue != "0" || txtDistrRS.Text != "" ||
                txtDistrNC.Text != "")
            {
                btnBuscar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
                btnLimpiar.Enabled = false;
            }

        }

        protected void drpZona_TextChanged(object sender, EventArgs e)
        {
            if (txtSolicitud.Text != "" || txtRPU.Text != "" || txtRFC.Text != "" || drpEstatus.SelectedValue != "0" ||
                drpRegion.SelectedValue != "0" || drpZona.SelectedValue != "0" || txtDistrRS.Text != "" ||
                txtDistrNC.Text != "")
            {
                btnBuscar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
                btnLimpiar.Enabled = false;
            }
        }

        protected void rdpFechaInicio_SelectedDateChanged(object sender,
            Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (rdpFechaInicio.SelectedDate.HasValue || txtSolicitud.Text != "" || txtRPU.Text != "" ||
                txtRFC.Text != "" || drpEstatus.SelectedValue != "0" || drpRegion.SelectedValue != "0" ||
                drpZona.SelectedValue != "0" || txtDistrRS.Text != "" || txtDistrNC.Text != "")
            {
                btnBuscar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
                btnLimpiar.Enabled = false;
            }
        }

        protected void rdpFechaFin_SelectedDateChanged(object sender,
            Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (rdpFechaFin.SelectedDate.HasValue || txtSolicitud.Text != "" || txtRPU.Text != "" || txtRFC.Text != "" ||
                drpEstatus.SelectedValue != "0" || drpRegion.SelectedValue != "0" || drpZona.SelectedValue != "0" ||
                txtDistrRS.Text != "" || txtDistrNC.Text != "")
            {
                btnBuscar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
                btnLimpiar.Enabled = false;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void imgExportaExcel_Click(object sender, ImageClickEventArgs e)
        {
            const string header = "Content-Disposition";
            const string ruta = "attachment;filename=Solicitudes.xls";

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var htw = new HtmlTextWriter(sw);

            var page = new Page();
            var frm = new HtmlForm();

            gvresultadosBusqueda.Parent.Controls.Add(frm);
            gvresultadosBusqueda.AllowPaging = false;
            gvresultadosBusqueda.EnableViewState = false;

            llenarGrid();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvresultadosBusqueda);
            frm.RenderControl(htw);

            page.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";

            Response.AddHeader(header, ruta);

            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void imgExportaPDF_Click(object sender, ImageClickEventArgs e)
        {
            llenarGrid();
            
            DataTable dt = ToDataTable(_creditos);
            ExportToPdf(dt);
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof (T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof (T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //foreach (PropertyInfo prop in Props)
            //{
            //    //Setting column names as Property names
            //    dataTable.Columns.Add(prop.Name);
            //}

            ////for (int i = 0; i < Props.Length - 2; i++)
            ////{
            ////    dataTable.Columns.Add(Props[i].Name);
            ////}

            dataTable.Columns.Add("SOLICITUD", typeof(System.String));
            dataTable.Columns.Add("RPU", typeof(System.String));
            dataTable.Columns.Add("NOMBRE/RAZÓN SOCIAL", typeof(System.String));
            dataTable.Columns.Add("RFC", typeof(System.String));
            dataTable.Columns.Add("MONTO DE FINANCIAMIENTO", typeof(System.String));
            dataTable.Columns.Add("ESTATUS", typeof(System.String));
            dataTable.Columns.Add("FECHA ESTATUS", typeof(System.DateTime));
            dataTable.Columns.Add("DISTRIBUIDOR RAZÓN SOCIAL", typeof(System.String));
            dataTable.Columns.Add("DISTRIBUIDOR NOMBRE COMERCIAL", typeof(System.String));
            dataTable.Columns.Add("REGION", typeof(System.String));
            dataTable.Columns.Add("ZONA", typeof(System.String));
            foreach (T item in items)
            {
                var values = new object[Props.Length - 2];
                for (int i = 0; i < Props.Length - 2; i++)
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
                Response.AddHeader("content-disposition", "attachment; filename=Solicitudes.pdf");

                System.Web.HttpContext.Current.Response.Write(pdfDoc);

                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        
        protected void CargaGridCustomizablesPlantilla2()
        {
            var paginaActual = AspNetPager2.CurrentPageIndex;
            var tamanioPagina = AspNetPager2.PageSize;


            gvresultadosBusqueda.DataSource = _creditos.FindAll(
                me =>
                    me.Rownum >= (((paginaActual - 1)*tamanioPagina) + 1) &&
                    me.Rownum <= (paginaActual*tamanioPagina));

            gvresultadosBusqueda.DataBind();
        }

    }
}