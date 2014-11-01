using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.LogicaNegocios.Tarifas;
using Wuqi.Webdiyer;

namespace PAEEEM.CentralModule
{
    public partial class HistoricoTarifas : Page
    {
        private List<DetalleTarifa> _listTarifa1 = new List<DetalleTarifa>();

        public string Fecha
        {
            get { return ViewState["Fecha"] == null ? "" : ViewState["Fecha"].ToString(); }
            set { ViewState["Fecha"] = value; }
        }

        public string Mes
        {
            get { return ViewState["Mes"] == null ? "" : ViewState["Mes"].ToString(); }
            set { ViewState["Mes"] = value; }
        }
        
        public string Año
        {
            get { return ViewState["Año"] == null ? " ": ViewState["Año"].ToString();}
            set { ViewState["Año"] = value; }
        }

        public int Tarifa
        {
            get { return ViewState["Tarifa"] == null ? 0 : int.Parse(ViewState["Tarifa"].ToString()); }
            set { ViewState["Tarifa"] = value; }
        }

        public int Region
        {
            get { return ViewState["Region"] == null ? 0 : int.Parse(ViewState["Region"].ToString()); }
            set { ViewState["Region"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }
            LlenaTarifa();
            LlenaMes();
            LlenaAnio();
            LlenaRegiones();
            div_Tarifa_02.Visible = false;
            div_Tarifa_03.Visible = false;
            div_Tarifa_HM.Visible = false;
            div_Tarifa_OM.Visible = false;
            tbButton.Visible = false;
        }

        private void LlenaRegiones()
        {
            var dtRegiones = RegistrarTarifas.Regiones();
            if (dtRegiones == null) return;
            drpRegiones.DataSource = dtRegiones;
            drpRegiones.DataTextField = "Descripcion";
            drpRegiones.DataValueField = "id_region";
            drpRegiones.DataBind();
            drpRegiones.Items.Insert(0, "Seleccione");
            drpRegiones.SelectedIndex = 0;
        }

        private void LlenaAnio()
        {
            var anioActual = DateTime.Now.Year;
            drpAnio.Items.Insert(0, "Seleccione");

            for (var a = 0; a < 12; a ++)
            {
                drpAnio.Items.Add(anioActual.ToString(CultureInfo.InvariantCulture));
                anioActual = anioActual - 1;
            }
        }

        private void LlenaMes()
        {
            drpMes.Items.Insert(0, "Seleccione");
            drpMes.Items.Insert(1, "ENERO");
            drpMes.Items.Insert(2, "FEBRERO");
            drpMes.Items.Insert(3, "MARZO");
            drpMes.Items.Insert(4, "ABRIL");
            drpMes.Items.Insert(5, "MAYO");
            drpMes.Items.Insert(6, "JUNIO");
            drpMes.Items.Insert(7, "JULIO");
            drpMes.Items.Insert(8, "AGOSTO");
            drpMes.Items.Insert(9, "SEPTIEMBRE");
            drpMes.Items.Insert(10, "OCTUBRE");
            drpMes.Items.Insert(11, "NOVIEMBRE");
            drpMes.Items.Insert(12, "DICIEMBRE");
        }

        private void LlenaTarifa()
        {
            var dtTarifa = RegistrarTarifas.TiposTarifa();
            if (dtTarifa == null) return;
            drpTipoTarifa.DataSource = dtTarifa;
            drpTipoTarifa.DataTextField = "Dx_Tarifa";
            drpTipoTarifa.DataValueField = "Cve_Tarifa";
            drpTipoTarifa.DataBind();
            drpTipoTarifa.Items.Insert(0, "Seleccione");
            drpTipoTarifa.SelectedIndex = 0;
        }

    
        protected void AspNetPager_PageChanging(object src, PageChangingEventArgs e)
        {
            if (!IsPostBack) return;
            //setup filter conditions for data refreshing                
            drpRegiones.SelectedIndex = Session["CurrentRegion"] != null ? (int)Session["CurrentRegion"] : 0;
            drpAnio.SelectedIndex = Session["CurrentAnio"] != null ? (int)Session["CurrentAnio"] :0 ;
            drpMes.SelectedIndex = Session["CurrentMes"] != null ? (int)Session["CurrentMes"] : 0;
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindGridViewData(gvTarifa_HM, Año, Mes, Region);
            }
        }

        protected void AspNetPagerOM_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindGridViewData(gvTarifa_OM, Año, Mes, Region);
            }
        }

        protected void AspNetPager03_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindGridViewData(gvTarifa_03, Año, Mes, Region);
            }
        }

        protected void AspNetPager02_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindGridViewData(gvTarifa_02, Año, Mes, Region);
            }
        }

        private void LlenaGridExportar(GridView tipoTarifa, string fecha, int region)
        {
            _listTarifa1 = RegistrarTarifas.RetornaDatosTarifas(Tarifa, fecha, region);
                    tipoTarifa.DataSource = _listTarifa1;
                    tipoTarifa.DataBind();
        }

        private void BindGridViewData(GridView tipoTarifa, string año, string mes, int region)
        {
            int pageCount;
            lblMensaje.Text = @"";
            tipoTarifa.Visible = true;
            DataTable dt = null;
            switch (Tarifa)
            {
                case 1: //tarifa 02
                    dt = DataAccessLayer.Tarifas.DHistoricoTarifas.HistoricoTarifa02(año, mes, "",
                        AspNetPager02.CurrentPageIndex, AspNetPager02.PageSize, out pageCount);
                    AspNetPager02.RecordCount = pageCount;
                    break;
                case 2: //tarifa 03
                    dt = DataAccessLayer.Tarifas.DHistoricoTarifas.HistoricoTarifa03(año, mes, "",
                        AspNetPager03.CurrentPageIndex, AspNetPager03.PageSize, out pageCount);
                    AspNetPager03.RecordCount = pageCount;
                    break;
                case 3: //Tarifa OM
                    dt = DataAccessLayer.Tarifas.DHistoricoTarifas.HistoricoTarifaOm(region, año, mes, "",
                        AspNetPagerOM.CurrentPageIndex, AspNetPagerOM.PageSize, out pageCount);
                    AspNetPagerOM.RecordCount = pageCount;
                    break;
                case 4: //Tarifa HM
                    dt = DataAccessLayer.Tarifas.DHistoricoTarifas.HistoricoTarifaHm(region, año, mes, "",
                        AspNetPagerHM.CurrentPageIndex, AspNetPagerHM.PageSize, out pageCount);
                    AspNetPagerHM.RecordCount = pageCount;
                    break;
            }

            //Check if no row exist, insert a new data row
            if (dt != null && dt.Rows.Count == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = @"No se encontró información con los filtros seleccionados.";
                tbButton.Visible = false;
            }
            else
            {
                tbButton.Visible = true;
                lblMensaje.Visible = false;
            }

            tipoTarifa.DataSource = dt;
            tipoTarifa.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Tarifa = drpTipoTarifa.SelectedIndex;
            Año = drpAnio.SelectedIndex == 0 ? "" : drpAnio.SelectedValue;
            Mes = drpMes.SelectedValue == "Seleccione" ? "" : drpMes.SelectedValue;
            Region = drpRegiones.SelectedIndex;

            Fecha = (Mes == "Seleccione" ? "" : Mes) + "" + (drpAnio.SelectedIndex == 0 ? "" : "-" + drpAnio.SelectedValue);
            switch (Tarifa)
            {
                case 1: //Tarifa 02
                    BindGridViewData(gvTarifa_02, Año, Mes, Region);
                    div_Tarifa_02.Visible = true;
                    div_Tarifa_03.Visible = false;
                    div_Tarifa_OM.Visible = false;
                    div_Tarifa_HM.Visible = false;
                    break;
                case 2: //Tarifa 03
                    BindGridViewData(gvTarifa_03, Año, Mes, Region);
                    div_Tarifa_03.Visible = true;
                    div_Tarifa_02.Visible = false;
                    div_Tarifa_OM.Visible = false;
                    div_Tarifa_HM.Visible = false;
                    break;
                case 3: //Tarifa OM
                    BindGridViewData(gvTarifa_OM, Año, Mes, Region);
                    div_Tarifa_OM.Visible = true;
                    div_Tarifa_03.Visible = false;
                    div_Tarifa_02.Visible = false;
                    div_Tarifa_HM.Visible = false;
                    break;
                case 4: //Tarifa HM
                    BindGridViewData(gvTarifa_HM, Año, Mes, Region);
                    div_Tarifa_HM.Visible = true;
                    div_Tarifa_03.Visible = false;
                    div_Tarifa_OM.Visible = false;
                    div_Tarifa_02.Visible = false;
                    break;
            }
        }

        protected void drpTipoTarifa_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnBuscar.Enabled = true;
            if (drpTipoTarifa.SelectedIndex == 3 || drpTipoTarifa.SelectedIndex == 4)
            {
                lblRegion.Visible = true;
                drpRegiones.Visible = true;
            }
            else
            {
                lblRegion.Visible = false;
                drpRegiones.Visible = false;
            }
        }

        protected void drpMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            switch (Tarifa)
            {
                case 1:
                    //BindGridViewData(gvTarifa_02, Año, Mes, Region);
                    Session["CurrentMes"] = drpMes.SelectedIndex;
                    //AspNetPager02.GoToPage(1);
                    break;
                case 2:
                    //BindGridViewData(gvTarifa_03, Año, Mes, Region);
                    Session["CurrentMes"] = drpMes.SelectedIndex;
                    //AspNetPager03.GoToPage(1);
                    break;
                case 3:
                    //BindGridViewData(gvTarifa_OM, Año, Mes, Region);
                    Session["CurrentMes"] = drpMes.SelectedIndex;
                   // AspNetPagerOM.GoToPage(1);
                    break;
                case 4:
                    //BindGridViewData(gvTarifa_HM, Año, Mes, Region);
                    Session["CurrentMes"] = drpMes.SelectedIndex;
                   // AspNetPagerHM.GoToPage(1);
                    break;
            }
        }

        protected void drpAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            switch (Tarifa)
            {
                case 1:
                    //BindGridViewData(gvTarifa_02, Año, Mes, Region);
                    Session["CurrentAnio"] = drpAnio.SelectedIndex;
                    //AspNetPager02.GoToPage(1);
                    break;
                case 2:
                   // BindGridViewData(gvTarifa_03, Año, Mes, Region);
                    Session["CurrentAnio"] = drpAnio.SelectedIndex;
                    //AspNetPager03.GoToPage(1);
                    break;
                case 3:
                   // BindGridViewData(gvTarifa_OM, Año, Mes, Region);
                    Session["CurrentAnio"] = drpAnio.SelectedIndex;
                   // AspNetPagerOM.GoToPage(1);
                    break;
                case 4:
                   // BindGridViewData(gvTarifa_HM, Año, Mes, Region);
                    Session["CurrentAnio"] = drpAnio.SelectedIndex;
                   // AspNetPagerHM.GoToPage(1);
                    break;
            }
        }

        protected void drpRegiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            switch (Tarifa)
            {
                case 3:
                   // BindGridViewData(gvTarifa_OM, Año, Mes, Region);
                    Session["CurrentRegion"] = drpRegiones.SelectedIndex;
                   // AspNetPagerOM.GoToPage(1);
                    break;
                case 4:
                    //BindGridViewData(gvTarifa_HM, Año, Mes, Region);
                    Session["CurrentRegion"] = drpRegiones.SelectedIndex;
                    //AspNetPagerHM.GoToPage(1);
                    break;
            }
        }
        #region PageIndexChanging Eliminar
        ////protected void PageIndexChanging(object sender, GridViewPageEventArgs gridViewPageEventArgs)
        ////{
        ////    var tarifa = drpTipoTarifa.SelectedIndex;
        ////    var año = drpAnio.SelectedValue;
        ////    var mes = drpMes.SelectedValue;
        ////    var region = drpRegiones.SelectedIndex;

        ////    var fecha = (mes == "Seleccione" ? "" : mes) + "-" + (año == "Seleccione" ? "" : año);
        ////    gvTarifa_OM.PageIndex = gridViewPageEventArgs.NewPageIndex;
        ////    BindGridViewData(gvTarifa_OM, tarifa, fecha, region);
        ////}

        ////protected void PageIndexChangingHm(object sender, GridViewPageEventArgs gridViewPageEventArgs)
        ////{
        ////    var tarifa = drpTipoTarifa.SelectedIndex;
        ////    var año = drpAnio.SelectedValue;
        ////    var mes = drpMes.SelectedValue;
        ////    var region = drpRegiones.SelectedIndex;

        ////    var fecha = (mes == "Seleccione" ? "" : mes) + "-" + (año == "Seleccione" ? "" : año);
        ////    gvTarifa_HM.PageIndex = gridViewPageEventArgs.NewPageIndex;
        ////    BindGridViewData(gvTarifa_HM, tarifa, fecha, region);
        ////}

        ////protected void PageIndexChanging02(object sender, GridViewPageEventArgs gridViewPageEventArgs)
        ////{
        ////    var tarifa = drpTipoTarifa.SelectedIndex;
        ////    var año = drpAnio.SelectedValue;
        ////    var mes = drpMes.SelectedValue;
        ////    var region = drpRegiones.SelectedIndex;

        ////    var fecha = (mes == "Seleccione" ? "" : mes) + "-" + (año == "Seleccione" ? "" : año);
        ////    gvTarifa_02.PageIndex = gridViewPageEventArgs.NewPageIndex;
        ////    BindGridViewData(gvTarifa_02, tarifa, fecha, region);
        ////}

        ////protected void PageIndexChanging03(object sender, GridViewPageEventArgs gridViewPageEventArgs)
        ////{
        ////    var tarifa = drpTipoTarifa.SelectedIndex;
        ////    var año = drpAnio.SelectedValue;
        ////    var mes = drpMes.SelectedValue;
        ////    var region = drpRegiones.SelectedIndex;

        ////    var fecha = (mes == "Seleccione" ? "" : mes) + "-" + (año == "Seleccione" ? "" : año);
        ////    gvTarifa_03.PageIndex = gridViewPageEventArgs.NewPageIndex;
        ////    BindGridViewData(gvTarifa_03, tarifa, fecha, region);
        ////}
        #endregion
       
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void imgExportaExcel_Click(object sender, ImageClickEventArgs e)
        {
            var grid = drpTipoTarifa.SelectedIndex == 1
               ? gvTarifa_02
               : drpTipoTarifa.SelectedIndex == 2
                   ? gvTarifa_03
                   : drpTipoTarifa.SelectedIndex == 3
                       ? gvTarifa_OM
                       : drpTipoTarifa.SelectedIndex == 4 ? gvTarifa_HM : gvTarifa_02;

            var nameTarifa = drpTipoTarifa.SelectedIndex == 1
               ? "Tarifa_02"
               : drpTipoTarifa.SelectedIndex == 2
                   ? "Tarifa_03"
                   : drpTipoTarifa.SelectedIndex == 3
                       ? "Tarifa_OM"
                       : drpTipoTarifa.SelectedIndex == 4 ? "Tarifa_HM" : "Tarifa_02";


            const string header = "Content-Disposition";
            var ruta = "attachment;filename=ReporteHistorico" + nameTarifa  +".xls";

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var htw = new HtmlTextWriter(sw);

            var page = new Page();
            var frm = new HtmlForm();

            grid.Parent.Controls.Add(frm);
            grid.AllowPaging = false;
            grid.EnableViewState = false;
           
            //BindGridViewData(grid, Año, Mes, Region);
            LlenaGridExportar(grid, Fecha, Region);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(grid);
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
            var grid = drpTipoTarifa.SelectedIndex == 1
               ? gvTarifa_02
               : drpTipoTarifa.SelectedIndex == 2
                   ? gvTarifa_03
                   : drpTipoTarifa.SelectedIndex == 3
                       ? gvTarifa_OM
                       : drpTipoTarifa.SelectedIndex == 4 ? gvTarifa_HM : gvTarifa_02;

            var nameTarifa = drpTipoTarifa.SelectedIndex == 1
              ? "Tarifa_02"
              : drpTipoTarifa.SelectedIndex == 2
                  ? "Tarifa_03"
                  : drpTipoTarifa.SelectedIndex == 3
                      ? "Tarifa_OM"
                      : drpTipoTarifa.SelectedIndex == 4 ? "Tarifa_HM" : "Tarifa_02";

            using (var sw = new StringWriter())
            {
                using (var hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    grid.AllowPaging = false;
                    LlenaGridExportar(grid, Fecha, Region);

                    grid.RenderControl(hw);
                    var sr = new StringReader(sw.ToString());
                    var pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    var htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=HistoricoTarifa_" + nameTarifa + "_Fecha_Aplicable" + Fecha + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
    }
}