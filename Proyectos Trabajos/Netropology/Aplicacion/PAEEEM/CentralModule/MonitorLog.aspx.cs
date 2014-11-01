using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using PAEEEM.LogicaNegocios.LOG;
using Wuqi.Webdiyer;

namespace PAEEEM.CentralModule
{
    public partial class MonitorLog : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                //BindDataGridView();
            }
            else
            {
                LlenaTipoProcesos();
                //LlenaTareasProcesos();
                LLenaRoles();
                tbExportar.Visible = false;
            }
        }

        private void LLenaRoles()
        {
            var dtRoles = LnMonitorLog.Roles();
            if (dtRoles == null) return;
            drpRoles.DataSource = dtRoles;
            drpRoles.DataTextField = "NOMBRE_ROL";
            drpRoles.DataValueField = "ID_ROL";
            drpRoles.DataBind();
            drpRoles.Items.Insert(0, "Seleccione");
            drpRoles.SelectedIndex = 0;
        }

        private void LlenaTipoProcesos()
        {
            var dtProceso = LnMonitorLog.TipoProcesos();
            if (dtProceso == null) return;
            drpTipoProceso.DataSource = dtProceso;
            drpTipoProceso.DataTextField = "DESCRIPCION";
            drpTipoProceso.DataValueField = "IDTIPOPROCESO";
            drpTipoProceso.DataBind();
            drpTipoProceso.Items.Insert(0, "Seleccione");
            drpTipoProceso.SelectedIndex = 0;
        }

        private void LlenaTareasProcesos()
        {
            var dtTareas = LnMonitorLog.TareasProcesos(drpTipoProceso.SelectedIndex);
            //var dTable = dtTareas.Select(o => o.DESCRIPCION).Distinct();
            //drpTareas.DataSource = dTable.ToList();
            drpTareas.DataSource = dtTareas;
            drpTareas.DataBind();
            drpTareas.Items.Insert(0, "Seleccione");
            drpTareas.SelectedIndex = 0;
        }

        protected void drpTipoProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dtTareas = LnMonitorLog.TareasProcesos(drpTipoProceso.SelectedIndex);
            drpTareas.DataSource = dtTareas;
            drpTareas.DataTextField = "DESCRIPCION";
            drpTareas.DataValueField = "IDTAREA";
            drpTareas.DataBind();
            drpTareas.Items.Insert(0, "Seleccione");
            drpTareas.SelectedIndex = 0;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BindDataGridView();
        }

        protected void BindDataGridView()
        {
            int pageCount;
            if (txtFdesde.Value != "")
                if (txtFhasta.Value == "")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "Error",
                        "alert('Capture la Fecha Fin')", true);
                    txtFhasta.Focus();
                    return;
                }

            if (txtFhasta.Value != "")
                if (txtFdesde.Value == "")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(Page), "Error",
                        "alert('Capture la Fecha Inicio')", true);
                    txtFdesde.Focus();
                    return;
                }
            //string fechaInicio, string fechaFin, int idRol, string nameUser,
            //int idTarea, int idProceso, string sortBy, int pageIndex, int pageSize, out int pageCount
            var dtRateAndCost = DataAccessLayer.CentralModule.Log.DBConsulta_Log(
                txtFdesde.Value == "" ? "" : txtFdesde.Value,
                txtFhasta.Value == "" ? "" : txtFhasta.Value,
                drpRoles.SelectedIndex,
                txtUsuario.Text == "" ? "" : txtUsuario.Text,
                drpTareas.SelectedValue == "Seleccione" ? 0 : Convert.ToInt32(drpTareas.SelectedValue),
                drpTipoProceso.SelectedIndex,
                "",
                AspNetPager.CurrentPageIndex,
                AspNetPager.PageSize,
                out pageCount);
            AspNetPager.RecordCount = pageCount;

            if (dtRateAndCost != null && dtRateAndCost.Rows.Count == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = @"No se encontró información con los filtros seleccionados.";
                tbExportar.Visible = false;
            }
            else
            {
                tbExportar.Visible = true;
                lblMensaje.Visible = false;
            }

            //Bind grid view data
            gvReporte.DataSource = dtRateAndCost;
            gvReporte.DataBind();
        }

        protected void imgExportaExcel_Click(object sender, ImageClickEventArgs e)
        {
            const string header = "Content-Disposition";
            const string ruta = "attachment;filename=ReporteLOG.xls";

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var htw = new HtmlTextWriter(sw);

            var page = new Page();
            var frm = new HtmlForm();

            gvReporte.Parent.Controls.Add(frm);
            gvReporte.AllowPaging = false;
            gvReporte.EnableViewState = false;

            BindDataGridViewExportar();
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(gvReporte);
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
            using (var sw = new StringWriter())
            {
                using (var hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    gvReporte.AllowPaging = false;

                    BindDataGridViewExportar();

                    gvReporte.RenderControl(hw);
                    var sr = new StringReader(sw.ToString());
                    var pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    var htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteLog.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void BindDataGridViewExportar()
        {
            //string fech.aInicio, string fechaFin,string nameUser, int? idRol,  int? idProceso,string descTarea
            var dtRateAndCost = LnMonitorLog.Consulta_Log(txtFdesde.Value == "" ? null : txtFdesde.Value,
                txtFhasta.Value == "" ? null : txtFhasta.Value,
                txtUsuario.Text == "" ? null : txtUsuario.Text,
                drpRoles.SelectedIndex == 0 ? (int?) null : drpRoles.SelectedIndex,
                drpTipoProceso.SelectedIndex == 0 ? (int?) null : drpTipoProceso.SelectedIndex,
                drpTareas.SelectedIndex == 0 ? null : drpTareas.SelectedValue);

            if (dtRateAndCost == null) return;
            
            gvReporte.DataSource = dtRateAndCost;
            gvReporte.DataBind();
        }

        protected void AspNetPager_PageChanging(object src, PageChangingEventArgs e)
        {
            if (!IsPostBack) return;
            //setup filter conditions for data refreshing  
            txtFdesde.Value = Session["FechaDesde"] != null ? (string)Session["FechaDesde"] : "";
            txtFhasta.Value = Session["FechaHasta"] != null ? (string)Session["FechaHasta"] : "";
            txtUsuario.Text = Session["Usuario"] != null ? (string)Session["Usuario"] : "";
            txtSucursal.Text = Session["Sucursal"] != null ? (string)Session["Sucursal"] : "";
            drpEmpresa.SelectedIndex = Session["Empresa"] != null ? (int)Session["Empresa"] : 0;
            drpTipoProceso.SelectedIndex = Session["Proceso"] != null ? (int)Session["Proceso"] : 0;
            drpTareas.SelectedIndex = Session["Proceso"] != null ? (int)Session["Proceso"] : 0;
            drpRoles.SelectedIndex = Session["Rol"] != null ? (int)Session["Rol"] : 0;
            drpRegion.SelectedIndex = Session["Region"] != null ? (int)Session["Region"] : 0;
            DrpZona.SelectedIndex = Session["Zona"] != null ? (int)Session["Zona"] : 0;
            txtNumSolicitud.Text = Session["NumSolicitud"] != null ? (string)Session["NumSolicitud"] : "";
            txtRPU.Text = Session["RPU"] != null ? (string)Session["RPU"] : "";
            txtNota.Text = Session["Nota"] != null ? (string)Session["Nota"] : "";
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
            }
        }

    }
}