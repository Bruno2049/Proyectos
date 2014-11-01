using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entities;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.RegionalModule
{
    public partial class DisposalCenterMonitor : System.Web.UI.Page
    {
        #region Initialize Components
        /// <summary>
        /// Init default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                //Setup default date for date display control
                this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

                //Initialize filter controls                
                InitializeFilterComponents();

                //First load grid view data when page loaded
                LoadGridViewData();

                //Clear filter conditions
                ClearFilterConditions();
            }
        }

        /// <summary>
        /// Init drop down list data
        /// </summary>
        private void InitializeFilterComponents()
        {
            InitializeZoneOptions();
            InitializeDisposalCenterStatus();
        }

        /// <summary>
        /// init zone
        /// </summary>
        private void InitializeZoneOptions()
        {
            DataTable zones = null;
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            //bind the zones to zone filter control
            if (UserModel != null)
            {
                zones = CatZonaDal.ClassInstance.GetZonaWithRegional(UserModel.Id_Departamento);
                if (zones != null)
                {
                    this.drpZone.DataSource = zones;
                    this.drpZone.DataTextField = "Dx_Nombre_Zona";
                    this.drpZone.DataValueField = "Cve_Zona";
                    this.drpZone.DataBind();
                    this.drpZone.Items.Insert(0, new ListItem(""));
                }
            }
        }

        /// <summary>
        /// init status
        /// </summary>
        private void InitializeDisposalCenterStatus()
        {
            DataTable statuses  = CAT_ESTATUS_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterEstatus();
            if (statuses != null)
            {
                this.drpEstatus.DataSource = statuses;
                this.drpEstatus.DataTextField = "Dx_Estatus_Centro_Disp";
                this.drpEstatus.DataValueField = "Cve_Estatus_Centro_Disp";
                this.drpEstatus.DataBind();
                this.drpEstatus.Items.Insert(0, new ListItem(""));
            }
        }

        /// <summary>
        /// clear filter conditions
        /// </summary>
        private void ClearFilterConditions()
        {
            Session["CurrentZoneDisposalMonitor"] = 0;
            Session["CurrentTypeDisposalMonitor"] = 0;
            Session["CurrentEstatusDisposalMonitor"] = 0;
        }
        #endregion

        #region Grid View Control Events
        /// <summary>
        /// Load default data when page load
        /// </summary>
        private void LoadGridViewData()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                int PageCount = 0;
                DataTable disposalCenters = null;

                string zone = (this.drpZone.SelectedIndex == 0 || this.drpZone.SelectedIndex == -1) ? "" : this.drpZone.SelectedValue;
                string type = (this.drpType.SelectedIndex == 0 || this.drpType.SelectedIndex == -1) ? "" : this.drpType.SelectedValue;
                int status = (this.drpEstatus.SelectedIndex == 0 || this.drpEstatus.SelectedIndex == -1) ? 0 : Convert.ToInt32(this.drpEstatus.SelectedValue);

                //retrieve the disposal centers belong to this specific regional
                disposalCenters = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranchWithZoneAndStatus(UserModel.Id_Departamento, zone, type, status, "Dx_Razon_Social"/*sort string*/, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
                
                if (disposalCenters != null)
                {
                    if (disposalCenters.Rows.Count == 0)
                    {
                        disposalCenters.Rows.Add(disposalCenters.NewRow());
                    }

                    //Bind to grid view
                    this.AspNetPager.RecordCount = PageCount;
                    this.grdDisposalCenters.DataSource = disposalCenters;
                    this.grdDisposalCenters.DataBind();
                }
            }
        }

        /// <summary>
        /// Hide link button when status is invalid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdDisposalCenters_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gridViewRow in this.grdDisposalCenters.Rows)
            {
                //Update by Mark 2012-07-06
                //LinkButton linkEdit = (LinkButton)gridViewRow.FindControl("linkEdit"); 
                Button btnEdit = (Button)gridViewRow.FindControl("btnEdit");
                //End by Mark
                //determine whether to display edit button by status, just status Pending, Active and Inactive could be edited
                if (btnEdit != null)
                {
                    if (gridViewRow.RowType == DataControlRowType.DataRow &&
                       (gridViewRow.Cells[3].Text.Replace("&nbsp;", "") == GlobalVar.PENDING_DISPOSAL_CENTER || 
                       gridViewRow.Cells[3].Text.Replace("&nbsp;", "") == GlobalVar.ACTIVE_DISPOSAL_CENTER || 
                       gridViewRow.Cells[3].Text.Replace("&nbsp;", "") == GlobalVar.INACTIVE_DISPOSAL_CENTER))
                    {
                        btnEdit.Visible = true;
                        btnEdit.CommandArgument = gridViewRow.Cells[0].Text.Replace("&nbsp;", "") + ";" + gridViewRow.Cells[5].Text.Replace("&nbsp;", "");
                    }
                    else
                    {
                        btnEdit.Visible = false;
                    }
                }
                //Update by Mark 2010-07-06
                //LinkButton linkAssignTechnology = (LinkButton)gridViewRow.FindControl("linkAssignTechnology");
                Button btnAssignTechnology = (Button)gridViewRow.FindControl("btnAssignTechnology");
                //End by Mark
                //determine whether to display assign button by status, just the status Active could be assigned the technology
                if (btnAssignTechnology != null)
                {
                    if (gridViewRow.RowType == DataControlRowType.DataRow && gridViewRow.Cells[3].Text.Replace("&nbsp;", "") == GlobalVar.ACTIVE_DISPOSAL_CENTER)
                    {
                        btnAssignTechnology.Visible = true;
                        btnAssignTechnology.CommandArgument = gridViewRow.Cells[0].Text.Replace("&nbsp;", "") + ";" + gridViewRow.Cells[5].Text.Replace("&nbsp;", "");
                    }
                    else
                    {
                        btnAssignTechnology.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Do the action when command button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdDisposalCenters_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] cmdArgs = e.CommandArgument.ToString().Split(';');
            string disposalCenterId = cmdArgs[0];
            string disposalCenterType = cmdArgs[1];
            
            //Jump to disposal center edition screen
            if (e.CommandName.Equals("edit", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("DisposalCenterEdit.aspx?DisposalID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(disposalCenterId)).Replace("+", "%2B") +
                                                                               "&Type=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(disposalCenterType)).Replace("+", "%2B"));
            }

            //Jump to technology assignment screen
            if (e.CommandName.Equals("assign", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("AssignTechnologiesToDisposal.aspx?DisposalID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(disposalCenterId)).Replace("+", "%2B") +
                                                                               "&Type=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(disposalCenterType)).Replace("+", "%2B"));
            }
        }
        #endregion

        #region Controls Changed Events
        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            //retrieve cached filter conditions before page changing
            if (IsPostBack)
            {                
                this.drpZone.SelectedIndex = Session["CurrentZoneDisposalMonitor"] != null ? (int)Session["CurrentZoneDisposalMonitor"] : 0;
                this.drpType.SelectedIndex = Session["CurrentTypeDisposalMonitor"] != null ? (int)Session["CurrentTypeDisposalMonitor"] : 0;
                this.drpEstatus.SelectedIndex = Session["CurrentEstatusDisposalMonitor"] != null ? (int)Session["CurrentEstatusDisposalMonitor"] : 0;
            }
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadGridViewData();
            }
        }

        protected void drpZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentZoneDisposalMonitor"] = drpZone.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentTypeDisposalMonitor"] = drpType.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }

        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentEstatusDisposalMonitor"] = drpEstatus.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }

        #endregion

        #region Button Clicked
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("DisposalCenterEdit.aspx");
        }
        #endregion
    }
}
