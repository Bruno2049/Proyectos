using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.CentralModule
{
    public partial class ProductMonitor : System.Web.UI.Page
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
                try
                {
                    if (null == Session["UserInfo"])
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }
                    //Initialize Manufacture Dropdownlist
                    InitializeManufacturers();
                    //Initialize Technology dropdownlist
                    InitializeTechnologies();
                    //Initialize Tipo Of Products dropdownlist
                    InitializeProductTypes();
                    //Initialize Marca dropdownlist
                    InitializeBrands();
                    //Initialize Status dropdownlist
                    InitializeStatuses();

                    LoadSession();
                    //Bind data for gridview
                    BindDataGridView();
                    //Setup default date to current date
                    lblFecha.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "LoadException", "alert('" + ex.Message + "');", true);
                }
            }
        }

        private void LoadSession()
        {
            if (Session["CurrentPageIndex"] != null)
            {
                this.drpManufacture.SelectedIndex = Session["CurrentManufactureProductAuthorization"] != null ? (int)Session["CurrentManufactureProductAuthorization"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrentTechnologyProductAuthorization"] != null ? (int)Session["CurrentTechnologyProductAuthorization"] : 0;
                this.drpTipoProduct.SelectedIndex = Session["CurrentTipoProductAuthorization"] != null ? (int)Session["CurrentTipoProductAuthorization"] : 0;
                this.drpMarca.SelectedIndex = Session["CurrentMarcaProductAuthorization"] != null ? (int)Session["CurrentMarcaProductAuthorization"] : 0;
                this.drpStatus.SelectedIndex = Session["CurrentStatusProductAuthorization"] != null ? (int)Session["CurrentStatusProductAuthorization"] : 0;
            }
            else
            {
                Session["CurrentManufactureProductAuthorization"] = 0;
                Session["CurrentTechnologyProductAuthorization"] = 0;
                Session["CurrentTipoProductAuthorization"] = 0;
                Session["CurrentMarcaProductAuthorization"] = 0;
                Session["CurrentStatusProductAuthorization"] = 0;
            }
        }
        /// <summary>
        /// Initialize Manufacture Dropdownlist
        /// </summary>
        private void InitializeManufacturers()
        {
            DataTable dtManufacture = CAT_FABRICANTEDal.ClassInstance.Get_All_CAT_FABRICANTE();

            if (dtManufacture != null && dtManufacture.Rows.Count > 0)
            {
                drpManufacture.DataSource = dtManufacture;
                drpManufacture.DataValueField = "Cve_Fabricante";
                drpManufacture.DataTextField = "Dx_Nombre_Fabricante";
                drpManufacture.DataBind();
            }
            drpManufacture.Items.Insert(0, new ListItem("", ""));
            drpManufacture.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Technology dropdownlist
        /// </summary>
        private void InitializeTechnologies()
        {
            DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIA();

            if (dtTechnology != null && dtTechnology.Rows.Count > 0)
            {
                drpTechnology.DataSource = dtTechnology;
                drpTechnology.DataValueField = "Cve_Tecnologia";
                drpTechnology.DataTextField = "Dx_Nombre_General";
                drpTechnology.DataBind();
            }
            drpTechnology.Items.Insert(0, new ListItem("", ""));
            drpTechnology.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Tipo Of Products dropdownlist
        /// </summary>
        private void InitializeProductTypes()
        {
            string Technology = (this.drpTechnology.SelectedIndex == 0 || this.drpTechnology.SelectedIndex == -1) ? "" : this.drpTechnology.SelectedValue.ToString();

            DataTable dtTipoProduct = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(Technology);
            if (dtTipoProduct != null && dtTipoProduct.Rows.Count > 0)
            {
                drpTipoProduct.DataSource = dtTipoProduct;
                drpTipoProduct.DataTextField = "Dx_Tipo_Producto";
                drpTipoProduct.DataValueField = "Ft_Tipo_Producto";
                drpTipoProduct.DataBind();
            }

            drpTipoProduct.Items.Insert(0, new ListItem("", ""));
            drpTipoProduct.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Marca dropdownlist
        /// </summary>
        private void InitializeBrands()
        {
            DataTable dtMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_MARCADal();

            if (dtMarca != null && dtMarca.Rows.Count > 0)
            {
                drpMarca.DataSource = dtMarca;
                drpMarca.DataValueField = "Cve_Marca";
                drpMarca.DataTextField = "Dx_Marca";
                drpMarca.DataBind();
            }

            drpMarca.Items.Insert(0, new ListItem("", ""));
            drpMarca.SelectedIndex = 0;
        }
        /// <summary>
        /// Initialize Status dropdownlist
        /// </summary>
        private void InitializeStatuses()
        {
            DataTable dtStatus = CAT_ESTATUS_PRODUCTODal.ClassInstance.Get_All_CAT_ESTATUS_PRODUCTO();

            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                drpStatus.DataSource = dtStatus;
                drpStatus.DataValueField = "Cve_Estatus_Producto";
                drpStatus.DataTextField = "Dx_Estatus_Producto";
                drpStatus.DataBind();
            }
            drpStatus.Items.Insert(0, new ListItem("", ""));
            drpStatus.SelectedIndex = 0;
        }
        #endregion

        #region Grid View Events

        private void BindDataGridView()
        {
            try
            {
                //obtain products list 
                int PageCount = 0;
                DataTable Products = null;
                bool emptyRow = false;

                //filter conditions
                string Manufacture = (this.drpManufacture.SelectedIndex == 0 || this.drpManufacture.SelectedIndex == -1) ? "" : this.drpManufacture.SelectedValue;
                string Technology = (this.drpTechnology.SelectedIndex == 0 || this.drpTechnology.SelectedIndex == -1) ? "" : this.drpTechnology.SelectedValue;
                string TipoOfProduct = (this.drpTipoProduct.SelectedIndex == 0 || this.drpTipoProduct.SelectedIndex == -1) ? "" : this.drpTipoProduct.SelectedValue;
                string Marca = (this.drpMarca.SelectedIndex == 0 || this.drpMarca.SelectedIndex == -1) ? "" : this.drpMarca.SelectedValue;
                string Estatus = (this.drpStatus.SelectedIndex == 0 || this.drpStatus.SelectedIndex == -1) ? "" : this.drpStatus.SelectedValue;

                Products = CAT_PRODUCTODal.ClassInstance.GetProductsList(Manufacture, Technology, TipoOfProduct, Marca, Estatus, "", AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out PageCount);

                if (Products != null)
                {
                    //Check if no row exist, insert a new data row
                    if (Products.Rows.Count == 0)
                    {
                        emptyRow = true;
                        Products.Rows.Add(Products.NewRow());
                    }

                    //Bind to grid view
                    this.AspNetPager.RecordCount = PageCount;
                    this.grvProductMonitor.DataSource = Products;
                    this.grvProductMonitor.DataBind();
                    if (Session["CurrentPageIndex"] != null)
                    {
                        int Page = (Session["CurrentPageIndex"] == null) ? 1 : int.Parse(Session["CurrentPageIndex"].ToString());
                        AspNetPager.GoToPage(Page);
                        Session["CurrentPageIndex"] = null;
                    }
                }

                //hide the checkbox and Edit button when the row is empty
                if (emptyRow)
                {
                    CheckBox ckbSelect = grvProductMonitor.Rows[0].FindControl("ckbSelect") as CheckBox;
                    if (ckbSelect != null)
                    {
                        ckbSelect.Visible = false;
                    }

                    Button btnEdit = grvProductMonitor.Rows[0].FindControl("btnEdit") as Button;
                    if (btnEdit != null)
                    {
                        btnEdit.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "GridViewInitError",
                                                                        "alert('Excepción que se produce durante la vista de cuadrícula de inicialización:" + ex.Message + "');", true);
            }
        }

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
                this.drpManufacture.SelectedIndex = Session["CurrentManufactureProductAuthorization"] != null ? (int)Session["CurrentManufactureProductAuthorization"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrentTechnologyProductAuthorization"] != null ? (int)Session["CurrentTechnologyProductAuthorization"] : 0;
                this.drpTipoProduct.SelectedIndex =  Session["CurrentTipoProductAuthorization"] != null ? (int) Session["CurrentTipoProductAuthorization"] : 0;
                this.drpMarca.SelectedIndex = Session["CurrentMarcaProductAuthorization"] != null ? (int)Session["CurrentMarcaProductAuthorization"] : 0;
                this.drpStatus.SelectedIndex = Session["CurrentStatusProductAuthorization"] != null ? (int)Session["CurrentStatusProductAuthorization"] : 0;
            }
        }

        protected void grvProductMonitor_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < grvProductMonitor.Rows.Count; i++)
            {
                int status = grvProductMonitor.DataKeys[i][1].ToString() != "" ? int.Parse(grvProductMonitor.DataKeys[i][1].ToString()) : 0;

                Button btnEdit = grvProductMonitor.Rows[i].FindControl("btnEdit") as Button;
                CheckBox ckbSelect = grvProductMonitor.Rows[i].FindControl("ckbSelect") as CheckBox;

                if (btnEdit != null && ckbSelect != null && status == (int)ProductStatus.CANCELADO)
                {
                    btnEdit.Visible = false;
                    ckbSelect.Visible = false;
                }
            }
        }
        #endregion

        #region Filter Changed Events

        protected void drpManufacture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
                Session["CurrentManufactureProductAuthorization"] = this.drpManufacture.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }

        protected void drpMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
                Session["CurrentMarcaProductAuthorization"] = this.drpMarca.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }

        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //Refresh product type list
                InitializeProductTypes();
                //Refresh grid view data
                BindDataGridView();
                Session["CurrentTechnologyProductAuthorization"] = this.drpTechnology.SelectedIndex;
                Session["CurrentTipoProductAuthorization"] = this.drpTipoProduct.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }

        protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindDataGridView();
                Session["CurrentStatusProductAuthorization"] = this.drpStatus.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }

        protected void drpTipoProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string TypeProduct = (drpTipoProduct.SelectedIndex == 0 || drpTipoProduct.SelectedIndex == -1) ? "" : drpTipoProduct.SelectedValue.ToString();
                if (TypeProduct != "")
                {
                    string Technology = CAT_TIPO_PRODUCTODal.ClassInstance.Get_TechnologyByPk(TypeProduct);
                    drpTechnology.SelectedValue= Technology;
                    Session["CurrentTechnologyProductAuthorization"] = this.drpTechnology.SelectedIndex;
                }
                BindDataGridView();
                 Session["CurrentTipoProductAuthorization"] = this.drpTipoProduct.SelectedIndex;
                AspNetPager.GoToPage(1);
            }
        }
        #endregion

        #region Button Events
        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddAndEditProduct.aspx");
        }
        /// <summary>
        /// Edit Product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)((Button)sender).NamingContainer;
            Session["CurrentManufactureProductAuthorization"] = this.drpManufacture.SelectedIndex;
            Session["CurrentMarcaProductAuthorization"] = this.drpMarca.SelectedIndex;
            Session["CurrentTechnologyProductAuthorization"] = this.drpTechnology.SelectedIndex;
            Session["CurrentStatusProductAuthorization"] = this.drpStatus.SelectedIndex;
             Session["CurrentTipoProductAuthorization"] = this.drpTipoProduct.SelectedIndex;
            Session["CurrentPageIndex"] = this.AspNetPager.CurrentPageIndex;
            Response.Redirect("AddAndEditProduct.aspx?ProductID=" + grvProductMonitor.DataKeys[gridViewRow.RowIndex][0]);
        }
        /// <summary>
        /// Active Selected Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActive_Click(object sender, EventArgs e)
        {
            string productIds = GetCheckedItems(GlobalVar.ACTIVATE);

            if (!string.IsNullOrEmpty(productIds))
            {
                int result = CAT_PRODUCTODal.ClassInstance.UpdateProductStatus(productIds, (int)ProductStatus.ACTIVO);

                if (result > 0)
                {
                    BindDataGridView();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor seleccione un producto.');", true);
            }
        }
        /// <summary>
        /// Desactive Selected Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeActive_Click(object sender, EventArgs e)
        {
            string productIds = GetCheckedItems(GlobalVar.DEACTIVATE);

            if (!string.IsNullOrEmpty(productIds))
            {
                int Result = CAT_PRODUCTODal.ClassInstance.UpdateProductStatus(productIds, (int)ProductStatus.INACTIVO);

                if (Result > 0)
                {
                    BindDataGridView();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor seleccione un producto.');", true);
            }
        }
        /// <summary>
        /// Cancel Selected Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string productIds = GetCheckedItems(GlobalVar.CANCEL);

            if (!string.IsNullOrEmpty(productIds))
            {
                int Result = CAT_PRODUCTODal.ClassInstance.UpdateProductStatus(productIds, (int)ProductStatus.CANCELADO);
                if (Result > 0)
                {
                    BindDataGridView();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "SelectProduct", "alert('Por favor seleccione un producto.');", true);
            }
        }
        #endregion

        #region Private Methods

        private string GetCheckedItems(string action)
        {
            string productIds = "";

            for (int i = 0; i < grvProductMonitor.Rows.Count; i++)
            {
                CheckBox ckbSelect = grvProductMonitor.Rows[i].FindControl("ckbSelect") as CheckBox;
                int productStatus = int.Parse(grvProductMonitor.DataKeys[i][1].ToString());
                string productId = grvProductMonitor.DataKeys[i][0].ToString();

                if (ckbSelect.Checked)
                {
                    switch (action)
                    {
                        case "active":
                            if (productStatus != (int)ProductStatus.ACTIVO)
                            {
                                productIds += productId + ",";
                            }
                            break;
                        case "inactive":
                            if (productStatus != (int)ProductStatus.INACTIVO)
                            {
                                productIds += productId + ",";
                            }
                            break;
                        case "cancel":
                            productIds += productId + ",";
                            break;
                        default:
                            break;
                    }
                }
            }

            productIds = productIds.TrimEnd(new char[] { ',' });

            return productIds;
        }
        #endregion
    }
}
