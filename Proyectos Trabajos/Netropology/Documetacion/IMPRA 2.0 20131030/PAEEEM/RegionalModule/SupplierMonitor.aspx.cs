using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM
{
    public partial class SupplierMonitor : System.Web.UI.Page
    {
        #region Initialize Components
        /// <summary>
        /// Init Default Data When page Load
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

                //Init date control
                this.lblFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
            InitializeZone();
            InitializeSupplierStatus();
        }

        /// <summary>
        /// Initial zone by regional
        /// </summary>
        /// <param name="RegionalID"></param>
        private void InitializeZone()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                DataTable zones = CatZonaDal.ClassInstance.GetZonaWithRegional(UserModel.Id_Departamento);

                if (zones != null && zones.Rows.Count > 0)
                {
                    drpZona.DataSource = zones;
                    drpZona.DataTextField = "Dx_Nombre_Zona";
                    drpZona.DataValueField = "Cve_Zona";
                    drpZona.DataBind();
                    drpZona.Items.Insert(0, new ListItem("", ""));
                }
            }
        }

        /// <summary>
        /// Initial Provider Estatus
        /// </summary>
        private void InitializeSupplierStatus()
        {
            DataTable status =  CAT_ESTATUS_PROVEEDORDal.ClassInstance.Get_Provider_Estatus();
            if (status != null && status.Rows.Count > 0)
            {
                drpEstatus.DataSource = status;
                drpEstatus.DataTextField = "Dx_Estatus_Proveedor";
                drpEstatus.DataValueField = "Cve_Estatus_Proveedor";
                drpEstatus.DataBind();
                drpEstatus.Items.Insert(0, new ListItem("", ""));
            }
        }

        /// <summary>
        /// clear filter conditions
        /// </summary>
        private void ClearFilterConditions()
        {
            Session["CurrentZoneSupplierMonitor"] = 0;
            Session["CurrentTipoSupplierMonitor"] = 0;
            Session["CurrentEstatusSupplierMonitor"] = 0;
        }
        #endregion

        #region Grid View Control Events
        DataTable technologies = null;
        private void LoadGridViewData()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                int PageCount = 0;
                DataTable dtSupplier = null;
                //Filter conditions
                string Zone = (this.drpZona.SelectedIndex == 0 || drpZona.SelectedIndex == -1) ? "" : drpZona.SelectedValue;
                string Tipo = (this.drpTipo.SelectedIndex == 0 || drpTipo.SelectedIndex == -1) ? "" : drpTipo.SelectedValue;
                string Estatus = (this.drpEstatus.SelectedIndex == 0 || drpEstatus.SelectedIndex == -1) ? "" : drpEstatus.SelectedValue;                

                //bool Flag = false;
                dtSupplier = CAT_PROVEEDORDal.ClassInstance.Get_Provider(Zone, Tipo, Estatus, UserModel.Id_Departamento, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
                if (dtSupplier != null)
                {
                    if (dtSupplier.Rows.Count == 0)
                    {
                        dtSupplier.Rows.Add(dtSupplier.NewRow());
                    }

                    technologies = CAT_TECNOLOGIADAL.ClassInstance.Get_ALL_Material_Technology_Provider();
                    //Bind to grid view
                    this.AspNetPager.RecordCount = PageCount;
                    this.grvSupplierMonitor.DataSource = dtSupplier;
                    this.grvSupplierMonitor.DataBind();
                }
            }
        }

        protected void grvSupplierMonitor_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gridViewRow in grvSupplierMonitor.Rows)
            {
                Button btnEdit = gridViewRow.FindControl("btnEdit") as Button;
                Button btnAssignProduct = gridViewRow.FindControl("btnAssignProduct") as Button;
                Button btnAssignDisposal = gridViewRow.FindControl("btnAssignDisposal") as Button;

                //Visible or invisible edit button
                if (btnEdit != null)
                {
                    if (gridViewRow.RowType == DataControlRowType.DataRow &&
                       (gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.PENDING_SUPPLIER || 
                       gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.ACTIVE_SUPPLIER || 
                       gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.INACTIVE_SUPPLIER))
                    {
                        btnEdit.Visible = true;
                        btnEdit.CommandArgument = grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][0].ToString() + ";" + grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][1].ToString();
                    }
                    else
                    {
                        btnEdit.Visible = false;
                    }
                }

                //Visible or invisible assign product button
                if (btnAssignProduct != null)
                {
                    if (gridViewRow.RowType == DataControlRowType.DataRow && 
                        gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.ACTIVE_SUPPLIER && 
                        grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][1].ToString().ToUpper() == "PROVEEDOR")
                    {
                        btnAssignProduct.Visible = true;
                        btnAssignProduct.CommandArgument = grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][0].ToString();
                    }
                    else
                    {
                        btnAssignProduct.Visible = false;
                    }
                }

                //Visible or invisible assign disposal center button
                if (btnAssignDisposal != null)
                {
                    if (gridViewRow.RowType == DataControlRowType.DataRow && gridViewRow.Cells[4].Text.Replace("&nbsp;", "").ToUpper() == GlobalVar.ACTIVE_SUPPLIER)
                    {
                        string id = grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][0].ToString();
                        string type = grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][1].ToString();
                        DataRow[] t = technologies.Select("Id_Proveedor=" + id + " AND Tipo='" + type + "'");
                        if (t != null && t.Length > 0)
                        {
                            btnAssignDisposal.Visible = true;
                            btnAssignDisposal.CommandArgument = grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][0].ToString() + ";" + grvSupplierMonitor.DataKeys[gridViewRow.RowIndex][1].ToString();
                        }
                        else
                            btnAssignDisposal.Visible = false;
                    }
                    else
                    {
                        btnAssignDisposal.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region Controls Changed Events
        /// <summary>
        /// Refresh Data When Pager Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadGridViewData();
            }
        }
        /// <summary>
        /// Get Filter Conditions From Cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanging(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.drpZona.SelectedIndex = Session["CurrentZoneSupplierMonitor"] != null ? (int)Session["CurrentZoneSupplierMonitor"] : 0;
                this.drpTipo.SelectedIndex = Session["CurrentTipoSupplierMonitor"] != null ? (int)Session["CurrentTipoSupplierMonitor"] : 0;
                this.drpEstatus.SelectedIndex = Session["CurrentEstatusSupplierMonitor"] != null ? (int)Session["CurrentEstatusSupplierMonitor"] : 0;
            }
        }
        /// <summary>
        /// drpZone Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentZoneSupplierMonitor"] = this.drpZona.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Supplier Type Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentTipoSupplierMonitor"] = this.drpTipo.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Estatus Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridViewData();
            Session["CurrentEstatusSupplierMonitor"] = this.drpEstatus.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        #endregion

        #region Button Clicked
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupplierAdd.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btnEdit = (Button)sender;
            string[] cmdArgs = btnEdit.CommandArgument.ToString().Split(';');

            if (cmdArgs.Length >= 2)
            {
                Response.Redirect("SupplierAdd.aspx?SupplierID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[0])).Replace("+", "%2B") +
                                                                                   "&Type=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[1])).Replace("+", "%2B"));
            }
        }

        protected void btnAssignProduct_Click(object sender, EventArgs e)
        {
            Button btnAssignProduct = (Button)sender;
            
            Response.Redirect("AssignProductToSupplier.aspx?SupplierID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(btnAssignProduct.CommandArgument)).Replace("+", "%2B"));
        }

        protected void btnAssignDisposal_Click(object sender, EventArgs e)
        {
            Button btnAssignDisposal = (Button)sender;
            string[] cmdArgs = btnAssignDisposal.CommandArgument.ToString().Split(';');

            if (cmdArgs.Length >= 2)
            {
                Response.Redirect("AssignDisposalToSupplier.aspx?SupplierID=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[0])).Replace("+", "%2B") +
                                                                                   "&Type=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(cmdArgs[1])).Replace("+", "%2B"));
            }
        }
        #endregion
    }
}
