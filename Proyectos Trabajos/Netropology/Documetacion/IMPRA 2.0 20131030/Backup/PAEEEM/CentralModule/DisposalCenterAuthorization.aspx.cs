using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.CentralModule
{
    public partial class DisposalCenterAuthorization : System.Web.UI.Page
    {
        #region Initialize Components
        /// <summary>
        ///  Init Default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (null == Session["UserInfo"])
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }
                    //Clear Filter
                    Session["CurrentRegionalProductAuthorization"] = 0;
                    Session["CurrentTipoDisposalAuthorization"] = 0;
                    Session["CurrentZoneProductAuthorization"] = 0;
                    Session["CurrentStatusProductAuthorization"] = 0;
                    Session["CurrentCentroAcopioAuthorization"] = 0;
                    //Initialize regional dropdownlist
                    InitializeDrpRegional();
                    //Initialize zone dropdownlist
                    InitializeDrpZona();
                    //Initialize Status dropdownlist
                    InitializeDrpStatus();
                    //Initialize CAyD dropdownlist
                    InitializeDrpCAyD();
                    //Bind data for gridview
                    BindDataGridView();
                    //Initial date
                    lblFecha.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "LoadException", "alert('" + ex.Message + "');", true);
                }
            }
        }
        /// Initialize Regional Dropdownlist
        /// </summary>
        private void InitializeDrpRegional()
        {
            DataTable dtRegional = CatZonaDal.ClassInstance.GetAllRegion();
            if (dtRegional != null && dtRegional.Rows.Count > 0)
            {
                drpRegional.DataSource = dtRegional;
                drpRegional.DataValueField = "Cve_Region";
                drpRegional.DataTextField = "Dx_Nombre_Region";
                drpRegional.DataBind();
            }
            drpRegional.Items.Insert(0, new ListItem("", ""));
            drpRegional.SelectedIndex = 0;
        }
        /// <summary>
        /// <summary>
        /// Initialize Zone Dropdownlist
        /// </summary>
        private void InitializeDrpZona()
        {
            DataTable dtZone;
            if (string.IsNullOrEmpty(drpRegional.SelectedValue))
                dtZone = CatZonaDal.ClassInstance.GetAllZone();
            else
                dtZone = CatZonaDal.ClassInstance.GetZonaWithRegional(Convert.ToInt32(drpRegional.SelectedValue));
            if (dtZone != null && dtZone.Rows.Count > 0)
            {
                drpZona.DataSource = dtZone;
                drpZona.DataValueField = "Cve_Zona";
                drpZona.DataTextField = "Dx_Nombre_Zona";
                drpZona.DataBind();
            }
            drpZona.Items.Insert(0, new ListItem("", ""));
            drpZona.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Status dropdownlist
        /// </summary>
        private void InitializeDrpStatus()
        {
            DataTable dtStatus = CAT_ESTATUS_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterEstatus();
            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                drpEstatus.DataSource = dtStatus;
                drpEstatus.DataValueField = "Cve_Estatus_Centro_Disp";
                drpEstatus.DataTextField = "Dx_Estatus_Centro_Disp";
                drpEstatus.DataBind();
            }
            drpEstatus.Items.Insert(0, new ListItem("", ""));
            drpEstatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Initialize Status dropdownlist
        /// </summary>
        private void InitializeDrpCAyD()
        {
            DataTable dtCAyD = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranch();
            if (dtCAyD != null && dtCAyD.Rows.Count > 0)
            {
                drpCAyD.DataSource = dtCAyD;
                drpCAyD.DataValueField = "Id_Centro_Disp";
                drpCAyD.DataTextField = "Dx_Razon_Social";
                drpCAyD.DataBind();
            }
            drpCAyD.Items.Insert(0, new ListItem("", ""));
            drpCAyD.SelectedIndex = 0;
        }
        #endregion

        #region Grid View Events
        /// <summary>
        /// Init grid view during the page first load
        /// </summary>    
        private void BindDataGridView()
        {
            try
            {
                //obtain products list 
                int PageCount = 0;
                DataTable Disposal = null;
                Boolean emptyRow = false;
                //filter conditions             
                string TipoOfDisposal = (this.drpTipo.SelectedIndex == 0 || this.drpTipo.SelectedIndex == -1) ? "" : this.drpTipo.SelectedValue;
                string Zone = (this.drpZona.SelectedIndex == 0 || this.drpZona.SelectedIndex == -1) ? "" : this.drpZona.SelectedValue;
                int Estatus = (this.drpEstatus.SelectedIndex == 0 || this.drpEstatus.SelectedIndex == -1) ? 0 : int.Parse(this.drpEstatus.SelectedValue);
                string CAyD = (this.drpCAyD.SelectedIndex == 0 || this.drpCAyD.SelectedIndex == -1) ? "" : this.drpCAyD.SelectedValue;
                string Regional = (this.drpRegional.SelectedIndex == 0 || this.drpRegional.SelectedIndex == -1) ? "" : this.drpRegional.SelectedValue;
                string idCD = string.Empty;

                if (!string.IsNullOrEmpty(CAyD))
                {
                    string[] partes = CAyD.Split('-');
                    if (partes.Length > 1)
                    {
                        idCD = partes[0];
                        TipoOfDisposal = partes[1] == "(Matriz)" ? "M" : "B";
                        this.drpTipo.SelectedIndex = 0;
                    }
                }

                Disposal = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalAndBranchWithZoneAndStatus(Zone, TipoOfDisposal, Estatus, idCD, Regional, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out PageCount);

                if (Disposal != null)
                {
                    if (Disposal.Rows.Count == 0)
                    {
                        emptyRow = true;
                        Disposal.Rows.Add(Disposal.NewRow());
                    }
                    //Bind to grid view
                    this.AspNetPager.RecordCount = PageCount;
                    this.grvDisposalCenter.DataSource = Disposal;
                    this.grvDisposalCenter.DataBind();
                }

                //hide the checkbox and Edit button when the row is empty
                if (emptyRow)
                {
                    CheckBox ckbSelect = grvDisposalCenter.Rows[0].FindControl("ckbSelect") as CheckBox;
                    if (ckbSelect != null)
                    {
                        ckbSelect.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "GridViewInitError",
                                                                        "alert('Excepción que se produce durante la vista de cuadrícula de inicialización:" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvDisposalCenter_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < grvDisposalCenter.Rows.Count; i++)
            {
                int Status = 0;
                Status = grvDisposalCenter.DataKeys[i][1].ToString() != "" ? int.Parse(grvDisposalCenter.DataKeys[i][1].ToString()) : 0;
                CheckBox ckbSelect = grvDisposalCenter.Rows[i].FindControl("ckbSelect") as CheckBox;
                if (Status == (int)ProviderStatus.CANCELADO)
                {
                    ckbSelect.Visible = false;
                }
            }
        }
        /// <summary>
        /// Changed page 
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
            }
        }

        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (IsPostBack)
            {
                //setup filter conditions for data refreshing                
                this.drpRegional.SelectedIndex = Session["CurrentRegionalProductAuthorization"] != null ? (int)Session["CurrentRegionalProductAuthorization"] : 0;
                this.drpTipo.SelectedIndex = Session["CurrentTipoDisposalAuthorization"] != null ? (int)Session["CurrentTipoDisposalAuthorization"] : 0;
                this.drpZona.SelectedIndex = Session["CurrentZoneProductAuthorization"] != null ? (int)Session["CurrentZoneProductAuthorization"] : 0;
                this.drpEstatus.SelectedIndex = Session["CurrentStatusProductAuthorization"] != null ? (int)Session["CurrentStatusProductAuthorization"] : 0;
                this.drpCAyD.SelectedIndex = Session["CurrentCentroAcopioAuthorization"] != null ? (int)Session["CurrentCentroAcopioAuthorization"] : 0;
            }
        }
        #endregion

        #region Filter Changed Events
        /// <summary>
        /// regional change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // RSA 2012-10-02 update zone selector, try to preserve it's selection
                string zona = drpZona.SelectedValue;
                InitializeDrpZona();
                try { drpZona.SelectedValue = zona; }   // try to keep previous selection
                catch (Exception) { }                   // if it fails, use the default empty selection
                Session["CurrentZoneProductAuthorization"] = drpZona.SelectedIndex;

                BindDataGridView();
                Session["CurrentRegionalProductAuthorization"] = drpRegional.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }
        /// <summary>
        /// zone change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
                Session["CurrentZoneProductAuthorization"] = drpZona.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }
        /// <summary>
        /// Type change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
                Session["CurrentTipoDisposalAuthorization"] = drpTipo.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }
        /// <summary>
        /// Status change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
                Session["CurrentStatusProductAuthorization"] = drpEstatus.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }

        /// <summary>
        /// Status change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCAyD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
                Session["CurrentCentroAcopioAuthorization"] = drpCAyD.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }
        #endregion

        #region Button Action
        /// <summary>
        /// Active Disposal Center And Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActive_Click(object sender, EventArgs e)
        {
            string DisposalList = "";
            string DisposalBranch = "";
            // only if its Status is "Pendiente". The status will change to "Activo".
            GetSelectSuppliserIDList(out DisposalList, out DisposalBranch, (int)DisposalCenterStatus.PENDIENTE);
            int Result = 0;
            if (DisposalList != "" || DisposalBranch != "")
            {
                if (DisposalList != "")
                {
                    Result = CAT_CENTRO_DISPDAL.ClassInstance.UpdateDisposalStatus(DisposalList, (int)DisposalCenterStatus.ACTIVO);
                }
                if (DisposalBranch != "")
                {
                    Result += CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.UpdateDisposalBranchStatus(DisposalBranch, (int)DisposalCenterStatus.ACTIVO);
                }
            }
            else
            {
                ClearGridViewChecked((int)DisposalCenterStatus.PENDIENTE);
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione CAyD pendientes.');", true);
            }
            if (Result > 0)
            {
                BindDataGridView();
            }
        }
        /// <summary>
        /// Get Select Disposal Center
        /// </summary>
        /// <param name="SupplierList"></param>
        /// <param name="BranchList"></param>
        /// <param name="Status"></param>
        private void GetSelectSuppliserIDList(out string DisposalList, out string DisposalBranch, int Status)
        {
            DisposalList = "";
            DisposalBranch = "";
            for (int i = 0; i < grvDisposalCenter.Rows.Count; i++)
            {
                CheckBox ckbSelect = grvDisposalCenter.Rows[i].FindControl("ckbSelect") as CheckBox;
                if (Status == 0)// Cancel Supplier and Branch
                {
                    if (ckbSelect.Checked)
                    {
                        if (grvDisposalCenter.Rows[i].Cells[4].Text.ToUpper() == "MATRIZ") // updated by tina 2012-02-29
                        {
                            DisposalList += grvDisposalCenter.DataKeys[i][0].ToString() + ",";
                        }
                        else
                        {
                            DisposalBranch += grvDisposalCenter.DataKeys[i][0].ToString() + ",";
                        }
                    }
                }
                else//Active and Desactive Supplier and Branch
                {
                    if (ckbSelect.Checked && int.Parse(grvDisposalCenter.DataKeys[i][1].ToString()) == Status)
                    {
                        if (grvDisposalCenter.Rows[i].Cells[4].Text.ToUpper() == "MATRIZ") // updated by tina 2012-02-29
                        {
                            DisposalList += grvDisposalCenter.DataKeys[i][0].ToString() + ",";
                        }
                        else
                        {
                            DisposalBranch += grvDisposalCenter.DataKeys[i][0].ToString() + ",";
                        }
                    }
                }
            }
            DisposalList = DisposalList.TrimEnd(new char[] { ',' });
            DisposalBranch = DisposalBranch.TrimEnd(new char[] { ',' });
        }
        /// <summary>
        /// DesActvie Disposal Center And Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeActive_Click(object sender, EventArgs e)
        {
            string DisposalList = "";
            string DisposalBranch = "";
            // only if its Status is "Active". The status will change to "InActivo".
            GetSelectSuppliserIDList(out DisposalList, out DisposalBranch, (int)DisposalCenterStatus.ACTIVO);
            int Result = 0;
            if (DisposalList != "" || DisposalBranch != "")
            {
                if (DisposalList != "")
                {
                    Result = CAT_CENTRO_DISPDAL.ClassInstance.UpdateDisposalStatus(DisposalList, (int)DisposalCenterStatus.INACTIVO);
                }
                if (DisposalBranch != "")
                {
                    Result += CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.UpdateDisposalBranchStatus(DisposalBranch, (int)DisposalCenterStatus.INACTIVO);
                }
            }
            else
            {
                ClearGridViewChecked((int)DisposalCenterStatus.ACTIVO);
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione CAyD Activo.');", true);
            }
            if (Result > 0)
            {
                BindDataGridView();
            }
        }
        /// <summary>
        /// Cancel Disposal Center And Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string DisposalList = "";
            string DisposalBranch = "";
           
            GetSelectSuppliserIDList(out DisposalList, out DisposalBranch, 0);
            int Result = 0;
            if (DisposalList != "" || DisposalBranch != "")
            {
                if (DisposalList != "")
                {
                    Result = CAT_CENTRO_DISPDAL.ClassInstance.UpdateDisposalStatus(DisposalList, (int)DisposalCenterStatus.CANCELADO);
                }
                if (DisposalBranch != "")
                {
                    Result += CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.UpdateDisposalBranchStatus(DisposalBranch, (int)DisposalCenterStatus.CANCELADO);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione CAyD Pendiente.');", true);
            }
            if (Result > 0)
            {
                BindDataGridView();
            }
        }
        private void ClearGridViewChecked(int Status)
        {
            for (int i = 0; i < grvDisposalCenter.Rows.Count; i++)
            {
                CheckBox ckbSelect = grvDisposalCenter.Rows[i].FindControl("ckbSelect") as CheckBox;
                if (ckbSelect.Checked && int.Parse(grvDisposalCenter.DataKeys[i][1].ToString()) != Status)
                {
                    ckbSelect.Checked = false;
                }
            }
        }
        /// <summary>
        /// Reactive Supplier and Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReActive_Click(object sender, EventArgs e)
        {
            string DisposalList = "";
            string DisposalBranch = "";
            // only if its Status is "Inactive". The status will change to "Reactive".
            GetSelectSuppliserIDList(out DisposalList, out DisposalBranch, (int)DisposalCenterStatus.INACTIVO);
            int Result = 0;
            if (DisposalList != "" || DisposalBranch != "")
            {
                if (DisposalList != "")
                {
                    Result = CAT_CENTRO_DISPDAL.ClassInstance.UpdateDisposalStatus(DisposalList, (int)DisposalCenterStatus.ACTIVO);
                }
                if (DisposalBranch != "")
                {
                    Result += CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.UpdateDisposalBranchStatus(DisposalBranch, (int)DisposalCenterStatus.ACTIVO);
                }
            }
            else
            {
                ClearGridViewChecked((int)DisposalCenterStatus.INACTIVO);
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione CAyD Inactivo.');", true);
            }
            if (Result > 0)
            {
                BindDataGridView();
            }
        }
        #endregion
    }
}
