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
    public partial class SupplierAuthorization : System.Web.UI.Page
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
                    Session["CurrentRegionalAuthorization"] = 0;
                    Session["CurrentTipoSupplierAuthorization"] = 0;
                    Session["CurrentZoneAuthorization"] = 0;
                    Session["CurrentStatusSupplierAuthorization"] = 0;
                    //Initialize regional dropdownlist
                    InitializeDrpRegional();
                    //Initialize zone dropdownlist
                    InitializeDrpZona();
                    //Initialize Status dropdownlist
                    InitializeDrpStatus();
                    //Bind data for gridview
                    BindDataGridView();
                    lblFecha.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "LoadException", "alert('" + ex.Message + "');", true);
                }
            }
        }
        /// <summary>
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
            DataTable dtStatus = CAT_ESTATUS_PROVEEDORDal.ClassInstance.Get_Provider_Estatus();
            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                drpEstatus.DataSource = dtStatus;
                drpEstatus.DataValueField = "Cve_Estatus_Proveedor";
                drpEstatus.DataTextField = "Dx_Estatus_Proveedor";
                drpEstatus.DataBind();
            }
            drpEstatus.Items.Insert(0, new ListItem("", ""));
            drpEstatus.SelectedIndex = 0;
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
                DataTable Supplier = null;
                Boolean emptyRow = false;
                //filter conditions             
                string TipoOfSupplier = (this.drpTipo.SelectedIndex == 0 || this.drpTipo.SelectedIndex == -1) ? "" : this.drpTipo.SelectedValue;
                string Zone = (this.drpZona.SelectedIndex == 0 || this.drpZona.SelectedIndex == -1) ? "" : this.drpZona.SelectedValue;
                string Estatus = (this.drpEstatus.SelectedIndex == 0 || this.drpEstatus.SelectedIndex == -1) ? "" : this.drpEstatus.SelectedValue;
                string Regional = (this.drpRegional.SelectedIndex == 0 || this.drpRegional.SelectedIndex == -1) ? "" : this.drpRegional.SelectedValue;

                Supplier = CAT_PROVEEDORDal.ClassInstance.Get_Provider_ForAuthorization(Zone, TipoOfSupplier, Estatus, Regional, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out PageCount);

                if (Supplier != null)
                {
                    if (Supplier.Rows.Count == 0)
                    {
                        emptyRow = true;
                        Supplier.Rows.Add(Supplier.NewRow());
                    }
                    //Bind to grid view
                    this.AspNetPager.RecordCount = PageCount;
                    this.grvSupplier.DataSource = Supplier;
                    this.grvSupplier.DataBind();
                }

                //hide the checkbox and Edit button when the row is empty
                if (emptyRow)
                {
                    CheckBox ckbSelect = grvSupplier.Rows[0].FindControl("ckbSelect") as CheckBox;
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
        protected void grvSupplier_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < grvSupplier.Rows.Count; i++)
            {
                int Status = 0;
                Status = grvSupplier.DataKeys[i][1].ToString() != "" ? int.Parse(grvSupplier.DataKeys[i][1].ToString()) : 0;
                CheckBox ckbSelect = grvSupplier.Rows[i].FindControl("ckbSelect") as CheckBox;
                if (Status == (int)ProviderStatus.CANCELADO)
                {
                    ckbSelect.Visible = false;
                }
                //Change Supplier Type 
                if (grvSupplier.DataKeys[i][2].ToString() == "M")
                {
                    grvSupplier.Rows[i].Cells[3].Text = GlobalVar.SUPPLIER_M.ToString();
                }
                else if (grvSupplier.DataKeys[i][2].ToString() == "B")
                {
                    grvSupplier.Rows[i].Cells[3].Text = GlobalVar.SUPPLIER_B.ToString();
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
                this.drpRegional.SelectedIndex = Session["CurrentRegionalAuthorization"] != null ? (int)Session["CurrentRegionalAuthorization"] : 0;
                this.drpTipo.SelectedIndex = Session["CurrentTipoSupplierAuthorization"] != null ? (int)Session["CurrentTipoSupplierAuthorization"] : 0;
                this.drpZona.SelectedIndex = Session["CurrentZoneAuthorization"] != null ? (int)Session["CurrentZoneAuthorization"] : 0;
                this.drpEstatus.SelectedIndex = Session["CurrentStatusSupplierAuthorization"] != null ? (int)Session["CurrentStatusSupplierAuthorization"] : 0;
            }
        }
        #endregion

        #region button action
        /// <summary>
        /// Active Supplier and Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActive_Click(object sender, EventArgs e)
        {
            string SupplierList = "";
            string SupplierBranch = "";
            // only if its Status is "Pendiente". The status will change to "Activo".
            GetSelectSuppliserIDList(out SupplierList, out SupplierBranch, (int)ProviderStatus.PENDIENTE);
            int Result = 0;
            if (SupplierList != "" || SupplierBranch != "")
            {
                if (SupplierList != "")
                {
                    Result = CAT_PROVEEDORDal.ClassInstance.UpdateProviderStatus(SupplierList, (int)ProviderStatus.ACTIVO);
                }
                if (SupplierBranch != "")
                {
                    Result += SupplierBrancheDal.ClassInstance.UpdateProviderBranchStatus(SupplierBranch, (int)ProviderStatus.ACTIVO);
                }
            }
            else
            {
                ClearGridViewChecked((int)ProviderStatus.PENDIENTE);
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione Proveedor pendientes.');", true);
            }
            if (Result > 0)
            {
                BindDataGridView();
            }
        }
        /// <summary>
        /// Get Selected Product list
        /// </summary>
        /// <returns></returns>
        private void GetSelectSuppliserIDList(out string SupplierList, out string BranchList, int Status)
        {
            SupplierList = "";
            BranchList = "";
            for (int i = 0; i < grvSupplier.Rows.Count; i++)
            {
                CheckBox ckbSelect = grvSupplier.Rows[i].FindControl("ckbSelect") as CheckBox;
                if (Status == 0)// Cancel Supplier and Branch
                {
                    if (ckbSelect.Checked)
                    {
                        if (grvSupplier.DataKeys[i][2].ToString() == "M")
                        {
                            SupplierList += grvSupplier.DataKeys[i][0].ToString() + ",";
                        }
                        else
                        {
                            BranchList += grvSupplier.DataKeys[i][0].ToString() + ",";
                        }
                    }
                }
                else//Active and Desactive Supplier and Branch
                {
                    if (ckbSelect.Checked && int.Parse(grvSupplier.DataKeys[i][1].ToString()) == Status)
                    {
                        if (grvSupplier.DataKeys[i][2].ToString() == "M")
                        {
                            SupplierList += grvSupplier.DataKeys[i][0].ToString() + ",";
                        }
                        else
                        {
                            BranchList += grvSupplier.DataKeys[i][0].ToString() + ",";
                        }
                    }
                }
            }
            SupplierList = SupplierList.TrimEnd(new char[] { ',' });
            BranchList = BranchList.TrimEnd(new char[] { ',' });
        }
        /// <summary>
        /// Deactivation Supplier and Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeActive_Click(object sender, EventArgs e)
        {
            string SupplierList = "";
            string SupplierBranch = "";
            int Result = 0;
            GetSelectSuppliserIDList(out SupplierList, out SupplierBranch, (int)ProviderStatus.ACTIVO);
            if (SupplierList != "" || SupplierBranch != "")
            {
                if (SupplierList != "")
                {
                    Result = CAT_PROVEEDORDal.ClassInstance.UpdateProviderStatus(SupplierList, (int)ProviderStatus.INACTIVO);
                }
                if (SupplierBranch != "")
                {
                    Result += SupplierBrancheDal.ClassInstance.UpdateProviderBranchStatus(SupplierBranch, (int)ProviderStatus.INACTIVO);
                }
            }
            else
            {
                ClearGridViewChecked((int)ProviderStatus.ACTIVO);
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione Proveedor ACTIVO.');", true);
            }
            if (Result > 0)
            {
                BindDataGridView();
            }
        }
        /// <summary>
        /// Cancel Supplier and Branch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string SupplierList = "";
            string SupplierBranch = "";
            int Result = 0;
            GetSelectSuppliserIDList(out SupplierList, out SupplierBranch, 0);
            if (SupplierList != "" || SupplierBranch != "")
            {
                if (SupplierList != "")
                {
                    Result = CAT_PROVEEDORDal.ClassInstance.UpdateProviderStatus(SupplierList, (int)ProviderStatus.CANCELADO);
                }
                if (SupplierBranch != "")
                {
                    Result += SupplierBrancheDal.ClassInstance.UpdateProviderBranchStatus(SupplierBranch, (int)ProviderStatus.CANCELADO);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione Proveedor.');", true);
            }
            if (Result > 0)
            {
                BindDataGridView();
            }
        }
        private void ClearGridViewChecked(int Status)
        {
            for (int i = 0; i < grvSupplier.Rows.Count; i++)
            {
                CheckBox ckbSelect = grvSupplier.Rows[i].FindControl("ckbSelect") as CheckBox;
                if (ckbSelect.Checked && int.Parse(grvSupplier.DataKeys[i][1].ToString()) != Status)
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
            string SupplierList = "";
            string SupplierBranch = "";
            int Result = 0;
            GetSelectSuppliserIDList(out SupplierList, out SupplierBranch, (int)ProviderStatus.INACTIVO);
            if (SupplierList != "" || SupplierBranch != "")
            {
                if (SupplierList != "")
                {
                    Result = CAT_PROVEEDORDal.ClassInstance.UpdateProviderStatus(SupplierList, (int)ProviderStatus.ACTIVO);
                }
                if (SupplierBranch != "")
                {
                    Result += SupplierBrancheDal.ClassInstance.UpdateProviderBranchStatus(SupplierBranch, (int)ProviderStatus.ACTIVO);
                }
            }
            else
            {
                ClearGridViewChecked((int)ProviderStatus.INACTIVO);
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor, seleccione Proveedor INACTIVO.');", true);
            }
            if (Result > 0)
            {
                BindDataGridView();
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
                Session["CurrentZoneAuthorization"] = drpZona.SelectedIndex;

                BindDataGridView();
                Session["CurrentRegionalAuthorization"] = drpRegional.SelectedIndex;
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
                Session["CurrentZoneAuthorization"] = drpZona.SelectedIndex;
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
                Session["CurrentTipoSupplierAuthorization"] = drpTipo.SelectedIndex;
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
                Session["CurrentStatusSupplierAuthorization"] = drpEstatus.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }
        #endregion
    }
}
