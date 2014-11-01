using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.CentralModule
{
    public partial class ProductMonitor : Page
    {
        #region Initialize Components
        /// <summary>
        /// Init Default Data When page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
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
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "LoadException", "alert('" + ex.Message + "');", true);
            }
        }

        private void LoadSession()
        {
            if (Session["CurrentPageIndex"] != null)
            {
                drpManufacture.SelectedIndex = Session["CurrentManufactureProductAuthorization"] != null ? (int)Session["CurrentManufactureProductAuthorization"] : 0;
                drpTechnology.SelectedIndex = Session["CurrentTechnologyProductAuthorization"] != null ? (int)Session["CurrentTechnologyProductAuthorization"] : 0;
                drpTipoProduct.SelectedIndex = Session["CurrentTipoProductAuthorization"] != null ? (int)Session["CurrentTipoProductAuthorization"] : 0;
                drpMarca.SelectedIndex = Session["CurrentMarcaProductAuthorization"] != null ? (int)Session["CurrentMarcaProductAuthorization"] : 0;
                drpStatus.SelectedIndex = Session["CurrentStatusProductAuthorization"] != null ? (int)Session["CurrentStatusProductAuthorization"] : 0;
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
            var dtManufacture = CAT_FABRICANTEDal.ClassInstance.Get_All_CAT_FABRICANTE();

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
            var dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIA();

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
            var technology = (drpTechnology.SelectedIndex == 0 || drpTechnology.SelectedIndex == -1) ? "" : drpTechnology.SelectedValue;

            var dtTipoProduct = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(technology);
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
            var dtMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_MARCADal();

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
            var dtStatus = CAT_ESTATUS_PRODUCTODal.ClassInstance.Get_All_CAT_ESTATUS_PRODUCTO();

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
                int pageCount;
                var emptyRow = false;

                //filter conditions
                var manufacture = (drpManufacture.SelectedIndex == 0 || drpManufacture.SelectedIndex == -1) ? "" : drpManufacture.SelectedValue;
                var technology = (drpTechnology.SelectedIndex == 0 || drpTechnology.SelectedIndex == -1) ? "" : drpTechnology.SelectedValue;
                var tipoOfProduct = (drpTipoProduct.SelectedIndex == 0 || drpTipoProduct.SelectedIndex == -1) ? "" : drpTipoProduct.SelectedValue;
                var marca = (drpMarca.SelectedIndex == 0 || drpMarca.SelectedIndex == -1) ? "" : drpMarca.SelectedValue;
                var estatus = (drpStatus.SelectedIndex == 0 || drpStatus.SelectedIndex == -1) ? "" : drpStatus.SelectedValue;

                var products = CAT_PRODUCTODal.ClassInstance.GetProductsList(manufacture, technology, tipoOfProduct, marca, estatus, "", AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);

                if (products != null)
                {
                    //Check if no row exist, insert a new data row
                    if (products.Rows.Count == 0)
                    {
                        emptyRow = true;
                        products.Rows.Add(products.NewRow());
                    }

                    //Bind to grid view
                    AspNetPager.RecordCount = pageCount;
                    grvProductMonitor.DataSource = products;
                    grvProductMonitor.DataBind();
                    if (Session["CurrentPageIndex"] != null)
                    {
                        var page = (Session["CurrentPageIndex"] == null) ? 1 : int.Parse(Session["CurrentPageIndex"].ToString());
                        AspNetPager.GoToPage(page);
                        Session["CurrentPageIndex"] = null;
                    }
                }

                //hide the checkbox and Edit button when the row is empty
                if (!emptyRow) return;
                var ckbSelect = grvProductMonitor.Rows[0].FindControl("ckbSelect") as CheckBox;
                if (ckbSelect != null)
                {
                    ckbSelect.Visible = false;
                }

                var btnEdit = grvProductMonitor.Rows[0].FindControl("btnEdit") as Button;
                if (btnEdit != null)
                {
                    btnEdit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "GridViewInitError",
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
            if (!IsPostBack) return;
            //setup filter conditions for data refreshing
            drpManufacture.SelectedIndex = Session["CurrentManufactureProductAuthorization"] != null ? (int)Session["CurrentManufactureProductAuthorization"] : 0;
            drpTechnology.SelectedIndex = Session["CurrentTechnologyProductAuthorization"] != null ? (int)Session["CurrentTechnologyProductAuthorization"] : 0;
            drpTipoProduct.SelectedIndex =  Session["CurrentTipoProductAuthorization"] != null ? (int) Session["CurrentTipoProductAuthorization"] : 0;
            drpMarca.SelectedIndex = Session["CurrentMarcaProductAuthorization"] != null ? (int)Session["CurrentMarcaProductAuthorization"] : 0;
            drpStatus.SelectedIndex = Session["CurrentStatusProductAuthorization"] != null ? (int)Session["CurrentStatusProductAuthorization"] : 0;
        }

        protected void grvProductMonitor_DataBound(object sender, EventArgs e)
        {
            for (var i = 0; i < grvProductMonitor.Rows.Count; i++)
            {
                var dataKey = grvProductMonitor.DataKeys[i];
                var key = grvProductMonitor.DataKeys[i];
                if (key == null) continue;
                var status = dataKey != null && dataKey[1].ToString() != "" ? int.Parse(key[1].ToString()) : 0;

                var btnEdit = grvProductMonitor.Rows[i].FindControl("btnEdit") as Button;
                var ckbSelect = grvProductMonitor.Rows[i].FindControl("ckbSelect") as CheckBox;

                if (btnEdit == null || ckbSelect == null || status != (int) ProductStatus.CANCELADO) continue;
                btnEdit.Visible = false;
                ckbSelect.Visible = false;
            }
        }
        #endregion

        #region Filter Changed Events

        protected void drpManufacture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            BindDataGridView();
            Session["CurrentManufactureProductAuthorization"] = drpManufacture.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        protected void drpMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            BindDataGridView();
            Session["CurrentMarcaProductAuthorization"] = drpMarca.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            //Refresh product type list
            InitializeProductTypes();
            //Refresh grid view data
            BindDataGridView();
            Session["CurrentTechnologyProductAuthorization"] = drpTechnology.SelectedIndex;
            Session["CurrentTipoProductAuthorization"] = drpTipoProduct.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            BindDataGridView();
            Session["CurrentStatusProductAuthorization"] = drpStatus.SelectedIndex;
            AspNetPager.GoToPage(1);
        }

        protected void drpTipoProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            var typeProduct = (drpTipoProduct.SelectedIndex == 0 || drpTipoProduct.SelectedIndex == -1) ? "" : drpTipoProduct.SelectedValue;
            if (typeProduct != "")
            {
                var technology = CAT_TIPO_PRODUCTODal.ClassInstance.Get_TechnologyByPk(typeProduct);
                drpTechnology.SelectedValue= technology;
                Session["CurrentTechnologyProductAuthorization"] = drpTechnology.SelectedIndex;
            }
            BindDataGridView();
            Session["CurrentTipoProductAuthorization"] = drpTipoProduct.SelectedIndex;
            AspNetPager.GoToPage(1);
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
            var gridViewRow = (GridViewRow) ((Button) sender).NamingContainer;
            Session["CurrentManufactureProductAuthorization"] = drpManufacture.SelectedIndex;
            Session["CurrentMarcaProductAuthorization"] = drpMarca.SelectedIndex;
            Session["CurrentTechnologyProductAuthorization"] = drpTechnology.SelectedIndex;
            Session["CurrentStatusProductAuthorization"] = drpStatus.SelectedIndex;
            Session["CurrentTipoProductAuthorization"] = drpTipoProduct.SelectedIndex;
            Session["CurrentPageIndex"] = AspNetPager.CurrentPageIndex;
            var dataKey = grvProductMonitor.DataKeys[gridViewRow.RowIndex];
            if (dataKey != null)
            {
                Session["ProductId"] = dataKey[0];
                Response.Redirect("AddAndEditProduct.aspx?ProductID=" + grvProductMonitor.DataKeys[gridViewRow.RowIndex][0]);
            }
        }

        /// <summary>
        /// Active Selected Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnActive_Click(object sender, EventArgs e)
        {
            var productIds = GetCheckedItems(GlobalVar.ACTIVATE);
            char[] delimiterChars = {','};
            if (!string.IsNullOrEmpty(productIds))
            {
                var idAfectados = productIds.Split(delimiterChars);
                foreach (var id in idAfectados)
                {
                    var originalInfoProduct = AccesoDatos.Log.CatProducto.ObtienePorId(Convert.ToInt32(productIds));
                    var result = CAT_PRODUCTODal.ClassInstance.UpdateProductStatus(id, (int) ProductStatus.ACTIVO);
                    if (result > 0)
                    {
                        /*INSERTAR EVENTO REACTIVAR DEL TIPO DE PROCESOS PRODUCTOS EN LOG*/
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "PRODUCTOS", "REACTIVAR", originalInfoProduct.Dx_Nombre_Producto,
                            "MOtivos??", "", "Cve_Estatus_Producto: " + originalInfoProduct.Cve_Estatus_Producto,
                            "Cve_Estatus_Producto: " + ProductStatus.ACTIVO);
                    }
                }
                BindDataGridView();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "SelectProduct","alert('Por favor seleccione un producto.');", true);
            }
        }
        /// <summary>
        /// Desactive Selected Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeActive_Click(object sender, EventArgs e)
        {
            var productIds = GetCheckedItems(GlobalVar.DEACTIVATE);
            char[] delimiterChars = { ',' };
            if (!string.IsNullOrEmpty(productIds))
            {
                var idAfectados = productIds.Split(delimiterChars);
                foreach (var id in idAfectados)
                {
                    var originalInfoProduct = AccesoDatos.Log.CatProducto.ObtienePorId(Convert.ToInt32(productIds));
                    var result = CAT_PRODUCTODal.ClassInstance.UpdateProductStatus(id, (int) ProductStatus.INACTIVO);
                    if (result > 0)
                    {
                        /*INSERTAR EVENTO INACTIVAR DEL TIPO DE PROCESOS PRODUCTOS EN LOG*/
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "PRODUCTOS", "INACTIVAR", originalInfoProduct.Dx_Nombre_Producto,
                            "MOtivos??", "", "Cve_Estatus_Producto: " + originalInfoProduct.Cve_Estatus_Producto,
                            "Cve_Estatus_Producto: " + Convert.ToString((int) ProductStatus.INACTIVO));
                    }
                }
                BindDataGridView();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "SelectProduct","alert('Por favor seleccione un producto.');", true);
            }
        }
        /// <summary>
        /// Cancel Selected Products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            var productIds = GetCheckedItems(GlobalVar.CANCEL);
            char[] delimiterChars = { ',' };
            if (!string.IsNullOrEmpty(productIds))
            {
                var idAfectados = productIds.Split(delimiterChars);
                foreach (var id in idAfectados)
                {
                    var originalInfoProduct = AccesoDatos.Log.CatProducto.ObtienePorId(Convert.ToInt32(productIds));
                    var result = CAT_PRODUCTODal.ClassInstance.UpdateProductStatus(id,(int) ProductStatus.CANCELADO);
                    if (result > 0)
                    {
                        /*INSERTAR EVENTO CANCELADO DEL TIPO DE PROCESOS PRODUCTOS EN LOG*/
                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]),
                            "PRODUCTOS", "CANCELADO", id.ToString(CultureInfo.InvariantCulture),
                            "MOtivos??", "", "Cve_Estatus_Producto: " + originalInfoProduct.Cve_Estatus_Producto,
                            "Cve_Estatus_Producto: " + Convert.ToString((int) ProductStatus.CANCELADO));
                    }
                }
                BindDataGridView();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "SelectProduct", "alert('Por favor seleccione un producto.');", true);
            }
        }
        #endregion

        #region Private Methods

        private string GetCheckedItems(string action)
        {
            var productIds = "";

            for (var i = 0; i < grvProductMonitor.Rows.Count; i++)
            {
                var ckbSelect = grvProductMonitor.Rows[i].FindControl("ckbSelect") as CheckBox;
                var dataKey = grvProductMonitor.DataKeys[i];
                if (dataKey == null) continue;
                var productStatus = int.Parse(dataKey[1].ToString());
                var productId = dataKey[0].ToString();

                if (ckbSelect != null && !ckbSelect.Checked) continue;
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
                }
            }

            productIds = productIds.TrimEnd(new[] { ',' });

            return productIds;
        }
        #endregion
    }
}
