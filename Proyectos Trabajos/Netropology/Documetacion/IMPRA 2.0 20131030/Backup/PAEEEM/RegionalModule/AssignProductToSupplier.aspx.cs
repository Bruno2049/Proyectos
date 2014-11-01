using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;

namespace PAEEEM.RegionalModule
{
    public partial class AssignProductToSupplier : System.Web.UI.Page
    {
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        private int SupplierID
        {
            get
            {
                return ViewState["SupplierID"] == null ? 0 : Convert.ToInt32(ViewState["SupplierID"].ToString());
            }
            set
            {
                ViewState["SupplierID"] = value;
            }
        }
        public DataTable GridViewDetail
        {
            get
            {
                return ViewState["GridViewDetail"] == null ? null : ViewState["GridViewDetail"] as DataTable;
            }
            set
            {
                ViewState["GridViewDetail"] = value;
            }
        }
        public int ProductGroupNumber
        {
            get
            {
                return ViewState["ProductGroupNumber"] == null ? 0 : int.Parse(ViewState["ProductGroupNumber"].ToString());
            }
            set
            {
                ViewState["ProductGroupNumber"] = value;
            }
        }
        public string DeletedProduct
        {
            get
            {
                return ViewState["DeletedProduct"] == null ? "" : ViewState["DeletedProduct"].ToString();
            }
            set
            {
                ViewState["DeletedProduct"] = value;
            }
        }
        public ArrayList SelectedProduct
        {
            get
            {
                return ViewState["SelectedProduct"] == null ? null : ViewState["SelectedProduct"] as ArrayList;
            }
            set
            {
                ViewState["SelectedProduct"] = value;
            }
        }
        //add by coco 2012-05-30
        public ArrayList SelectedProductUnitPrice
        {
            get
            {
                return ViewState["SelectedProductUnitPrice"] == null ? null : ViewState["SelectedProductUnitPrice"] as ArrayList;
            }
            set
            {
                ViewState["SelectedProductUnitPrice"] = value;
            }
        }
        //end add
        #endregion

        #region Initialize Components
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                //Init date control
                this.literalFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                if (Request.QueryString["SupplierID"] != null && Request.QueryString["SupplierID"].ToString() != "")
                {
                    SupplierID = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["SupplierID"].ToString().Replace("%2B", "+"))));
                    //Init default data
                    InitDefaultData();
                }
            }
        }

        private void InitDefaultData()
        {
            InitHeaderData();
            LoadGridViewData();
        }

        private void InitHeaderData()
        {
            CAT_PROVEEDORModel model = null;
            model = CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORByPKID(SupplierID.ToString());

            if (model != null)
            {
                txtSociaNamel.Text = model.Dx_Razon_Social;
                txtBusinessName.Text = model.Dx_Nombre_Comercial;
            }
        }

        private void LoadGridViewData()
        {
            GridViewDetail = new DataTable();
            DataRow row;

            GridViewDetail.Columns.Add("Technology", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Marca", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Product", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Model", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Status", Type.GetType("System.String"));
            //add by coco 2012-05-30
            GridViewDetail.Columns.Add("MaxPrice", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("unitPrice", Type.GetType("System.String"));
            //end add
            GridViewDetail.Columns.Add("ProductName", Type.GetType("System.String"));//added by tina 2012-07-18
            GridViewDetail.Columns.Add("Ft_Tipo_Producto", Type.GetType("System.String"));//added by tina 2012-07-18

            bool emptyRow = false;//added by tina 2012/04/27
            DataTable dtAssignedProducts = null;
            dtAssignedProducts = K_PROVEEDOR_PRODUCTODal.ClassInstance.Get_K_PROVEEDOR_PRODUCTO_ByProviderID(SupplierID);
            if (dtAssignedProducts != null && dtAssignedProducts.Rows.Count > 0)
            {
                ProductGroupNumber = dtAssignedProducts.Rows.Count;
                foreach (DataRow dataRow in dtAssignedProducts.Rows)
                {
                    row = GridViewDetail.NewRow();
                    row["Technology"] = dataRow["Cve_Tecnologia"].ToString();
                    row["Marca"] = dataRow["Cve_Marca"].ToString();
                    row["Product"] = dataRow["Cve_Producto"].ToString();
                    row["Model"] = dataRow["Dx_Modelo_Producto"].ToString();
                    row["Status"] = "1";
                    //add by coco 2012-05-30
                    row["MaxPrice"] = dataRow["Mt_Precio_Max"].ToString();
                    row["unitPrice"] = dataRow["Mt_Precio_Unitario"].ToString();
                    //end add
                    row["ProductName"] = dataRow["Dx_Nombre_Producto"].ToString();//added by tina 2012-07-18
                    row["Ft_Tipo_Producto"] = dataRow["Ft_Tipo_Producto"].ToString();
                    GridViewDetail.Rows.Add(row);
                }
            }
            else
            {
                emptyRow = true;//added by tina 2012/04/27
                ProductGroupNumber = 1;
                row = GridViewDetail.NewRow();
                row["Technology"] = "";
                row["Marca"] = "";
                row["Product"] = "";
                row["Model"] = "";
                row["Status"] = "0";
                //add by coco 2012-05-30
                row["MaxPrice"] = "";
                row["unitPrice"] = "";
                //end add
                row["ProductName"] = "";//added by tina 2012-07-18
                row["Ft_Tipo_Producto"] = "";
                GridViewDetail.Rows.Add(row);
            }
            this.grdProducts.DataSource = GridViewDetail;
            this.grdProducts.DataBind();
            //added by tina 2012/04/27
            if (emptyRow)
            {
                this.grdProducts.Columns[6].Visible = false;//edit by coco 2012-05-30
            }
            else
            {
                this.grdProducts.Columns[6].Visible = true;//edit by coco 2012-05-30
            }
            //end
        }
        #endregion

        #region Grid View Control Events
        protected void grdProducts_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < ProductGroupNumber; i++)
            {
                DropDownList drpTechnology = grdProducts.Rows[i].FindControl("drpTechnology") as DropDownList;
                DropDownList drpMarca = grdProducts.Rows[i].FindControl("drpMarca") as DropDownList;
                DropDownList drpProduct = grdProducts.Rows[i].FindControl("drpProduct") as DropDownList;
                //updated by tina 2012-07-18
                //Label lblModel = grdProducts.Rows[i].FindControl("lblModel") as Label;
                DropDownList drpModel = grdProducts.Rows[i].FindControl("drpModel") as DropDownList;
                Label lblProduct = grdProducts.Rows[i].FindControl("lblProduct") as Label;
                //end                
                //add by coco 2012-05-30
                Label lblMaxPrice = grdProducts.Rows[i].FindControl("lblMaxPrice") as Label;
                TextBox txtUnitPrice = grdProducts.Rows[i].FindControl("txtUnit") as TextBox;
                //end add

                if (drpTechnology != null)
                {
                    DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIA();
                    if (dtTechnology != null)
                    {
                        drpTechnology.DataSource = dtTechnology;
                        drpTechnology.DataTextField = "Dx_Nombre_Particular";
                        drpTechnology.DataValueField = "Cve_Tecnologia";
                        drpTechnology.DataBind();
                        drpTechnology.Items.Insert(0, "");
                    }

                    if (GridViewDetail.Rows[i]["Technology"].ToString() != "")
                    {
                        drpTechnology.SelectedValue = GridViewDetail.Rows[i]["Technology"].ToString();
                    }
                }

                if (drpMarca != null)
                {
                    DataTable dtMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_MARCADal();
                    if (dtMarca != null)
                    {
                        drpMarca.DataSource = dtMarca;
                        drpMarca.DataTextField = "Dx_Marca";
                        drpMarca.DataValueField = "Cve_Marca";
                        drpMarca.DataBind();
                        drpMarca.Items.Insert(0, "");

                        if (GridViewDetail.Rows[i]["Marca"].ToString() != "")
                        {
                            drpMarca.SelectedValue = GridViewDetail.Rows[i]["Marca"].ToString();
                        }
                    }
                }

                if (drpProduct != null)
                {
                    //updated by tina 2012-07-18
                    DataTable dtProduct = CAT_PRODUCTODal.ClassInstance.GetProductNameByTechnologyAndMarca
                                                    (GridViewDetail.Rows[i]["Technology"].ToString(), GridViewDetail.Rows[i]["Marca"].ToString());
                    //end
                    if (dtProduct != null)
                    {
                        drpProduct.DataSource = dtProduct;
                        drpProduct.DataTextField = "Dx_Nombre_Producto";
                        drpProduct.DataValueField = "Ft_Tipo_Producto";//updated by tina 2012-07-18
                        drpProduct.DataBind();
                        drpProduct.Items.Insert(0, "");

                        if (GridViewDetail.Rows[i]["Ft_Tipo_Producto"].ToString() != "")
                        {
                            drpProduct.SelectedValue = GridViewDetail.Rows[i]["Ft_Tipo_Producto"].ToString();
                        }
                    }
                }
                //comment by tina 2012-07-18
                //if (lblModel != null)
                //{
                //    lblModel.Text = GridViewDetail.Rows[i]["Model"].ToString();
                //}
                //end comment

                //added by tina 2012-07-18
                if (drpModel != null)
                {
                    DataTable dtModel = CAT_PRODUCTODal.ClassInstance.GetModelByProductName(GridViewDetail.Rows[i]["Ft_Tipo_Producto"].ToString());
                    if (dtModel != null)
                    {
                        drpModel.DataSource = dtModel;
                        drpModel.DataTextField = "Dx_Modelo_Producto";
                        drpModel.DataValueField = "Dx_Modelo_Producto";
                        drpModel.DataBind();
                        drpModel.Items.Insert(0, "");

                        if (GridViewDetail.Rows[i]["Model"].ToString() != "")
                        {
                            drpModel.SelectedValue = GridViewDetail.Rows[i]["Model"].ToString();
                        }
                    }
                }
                if (lblProduct != null)
                {
                    if (GridViewDetail.Rows[i]["Product"].ToString() != "")
                    {
                        lblProduct.Text = GridViewDetail.Rows[i]["Product"].ToString();
                    }
                }
                //end add

                //add by coco 2012-05-30
                if (lblMaxPrice != null)
                {
                    lblMaxPrice.Text = GridViewDetail.Rows[i]["MaxPrice"].ToString();
                }
                if (txtUnitPrice != null)
                {
                    txtUnitPrice.Text = GridViewDetail.Rows[i]["unitPrice"].ToString();
                }
                //end add
                if (GridViewDetail.Rows[i]["Status"].ToString() == "1")
                {
                    drpTechnology.Enabled = false;
                    drpMarca.Enabled = false;
                    drpProduct.Enabled = false;
                    txtUnitPrice.Enabled = false;//add by coco 2012-05-30
                    drpModel.Enabled = false;//added by tina 2012-07-18
                }
            }
        }

        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
            RefreshProduct(row);
        }

        protected void drpMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
            RefreshProduct(row);
        }

        protected void drpProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
            DropDownList drpProduct = (DropDownList)row.FindControl("drpProduct");
            //Label lblModel = (Label)row.FindControl("lblModel");//comment by tina 2012-07-18
            Label lblMaxPrice = row.FindControl("lblMaxPrice") as Label; //add by coco 2012-05-30
            //added by tina 2012-07-18
            DropDownList drpModel = (DropDownList)row.FindControl("drpModel");
            Label lblProduct = (Label)row.FindControl("lblProduct");
            TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnit") as TextBox;
            //end

            //updated by tina 2012-07-18
            string productName = (drpProduct.SelectedIndex == -1 || drpProduct.SelectedIndex == 0) ? "" : drpProduct.SelectedValue;
            BindModelDropDownList(productName, drpModel);
            if (lblProduct != null)
            {
                lblProduct.Text = "";
            }
            if (lblMaxPrice != null)
            {
                lblMaxPrice.Text = "0";
            }
            if (txtUnitPrice != null)
            {
                txtUnitPrice.Text = "";
            }
            //end            
        }

        //added by tina 2012-07-18
        protected void drpModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
            DropDownList drpModel = (DropDownList)row.FindControl("drpModel");
            Label lblMaxPrice = row.FindControl("lblMaxPrice") as Label;            
            Label lblProduct = (Label)row.FindControl("lblProduct");
            TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnit") as TextBox;
            DropDownList drpTechnology = (DropDownList)row.FindControl("drpTechnology") as DropDownList;
            DropDownList drpMarca = (DropDownList)row.FindControl("drpMarca") as DropDownList;
            DropDownList drpProduct = (DropDownList)row.FindControl("drpProduct") as DropDownList;

            string model = (drpModel.SelectedIndex == -1 || drpModel.SelectedIndex == 0) ? "" : drpModel.SelectedValue;
            if (model != "")
            {
                DataTable productInfo = CAT_PRODUCTODal.ClassInstance.GetProductInfoByModel(model);
                if (lblProduct != null)
                {
                    lblProduct.Text = productInfo.Rows[0]["Cve_Producto"].ToString();
                }
                if (lblMaxPrice != null)
                {
                    lblMaxPrice.Text = productInfo.Rows[0]["Mt_Precio_Max"].ToString();
                }
                if (drpTechnology != null)
                {
                    if (drpTechnology.Items.FindByValue(productInfo.Rows[0]["Cve_Tecnologia"].ToString()) != null)
                        drpTechnology.SelectedValue = productInfo.Rows[0]["Cve_Tecnologia"].ToString();
                }
                if (drpMarca != null)
                {
                    if (drpMarca.Items.FindByValue(productInfo.Rows[0]["Cve_Marca"].ToString()) != null)
                        drpMarca.SelectedValue = productInfo.Rows[0]["Cve_Marca"].ToString();
                }
                if (drpProduct != null)
                {
                    if (drpProduct.Items.FindByValue(productInfo.Rows[0]["Ft_Tipo_Producto"].ToString()) != null)
                        drpProduct.SelectedValue = productInfo.Rows[0]["Ft_Tipo_Producto"].ToString();
                }
                // Adjust options on selectors cascading from left to right, preserve selection if posible, reset price if model changed
                RefreshProduct(row);
            }
            else
            {
                if (lblProduct != null)
                {
                    lblProduct.Text = "";
                }
                if (lblMaxPrice != null)
                {
                    lblMaxPrice.Text = "0";
                }
                if (txtUnitPrice != null)
                {
                    txtUnitPrice.Text = "";
                }
            }
        }
        //end

        //add by coco 2012-05-30
        protected void txtUnit_Onchanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
            Label lblMaxPrice = row.FindControl("lblMaxPrice") as Label;
            TextBox txtUnitPrice = row.FindControl("txtUnit") as TextBox;
            if (lblMaxPrice != null && txtUnitPrice != null && lblMaxPrice.Text != "")
            {
                if (ConvertStringToDecimal(txtUnitPrice.Text) > ConvertStringToDecimal(lblMaxPrice.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "UnilPrice", "alert('El precio ingresado debe ser igual o menor al precio máximo');", true);
                    txtUnitPrice.Text = "";
                    txtUnitPrice.Focus();
                }
                else if (ConvertStringToDecimal(txtUnitPrice.Text) <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "UnilPriceCero", "alert('El precio ingresado debe ser mayor que cero.');", true);
                    txtUnitPrice.Text = "";
                    txtUnitPrice.Focus();
                }
            }
        }
        private decimal ConvertStringToDecimal(string PriceValue)
        {
            try
            {
                return decimal.Parse(PriceValue);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        //end add
        #endregion

        #region Button Clicked
        protected void btnNewProduct_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                SaveGridViewDetail();

                ProductGroupNumber += 1;

                DataRow newrow;
                newrow = GridViewDetail.NewRow();
                newrow["Technology"] = "";
                newrow["Marca"] = "";
                newrow["Product"] = "";
                newrow["Model"] = "";
                newrow["Status"] = "0";
                //add by coco 2012-05-30
                newrow["MaxPrice"] = "";
                newrow["unitPrice"] = "";
                //end add
                newrow["ProductName"] = "";//added by tina 2012-07-18
                newrow["Ft_Tipo_Producto"] = "";
                GridViewDetail.Rows.Add(newrow);

                grdProducts.DataSource = GridViewDetail;
                grdProducts.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Invalid", "alert('El precio ingresado debe ser mayor que cero e igual o menor al precio máximo');", true);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int rowIndex = row.RowIndex;

            DropDownList drpModel = (DropDownList)row.FindControl("drpModel");
            Label lblProduct = (Label)row.FindControl("lblProduct");//added by tina 2012-07-18

            if (!drpModel.Enabled)
            {
                if (DeletedProduct == "")
                {
                    DeletedProduct = lblProduct.Text;//updated by tina 2012-07-18
                }
                else
                {
                    DeletedProduct += "," + lblProduct.Text;//updated by tina 2012-07-18
                }
            }

            SaveGridViewDetail();

            GridViewDetail.Rows.RemoveAt(rowIndex);
            ProductGroupNumber -= 1;

            grdProducts.DataSource = GridViewDetail;
            grdProducts.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveGridViewDetail();
                GetSelectedProduct();

                K_PROVEEDOR_PRODUCTOEntity model = new K_PROVEEDOR_PRODUCTOEntity();
                int result = 0;
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    if (SelectedProduct != null && SelectedProduct.Count > 0)
                    {
                        result = K_PROVEEDOR_PRODUCTOBLL.ClassInstance.Insert_K_PROVEEDOR_PRODUCTO(SupplierID, SelectedProduct, 1, DateTime.Now, SelectedProductUnitPrice);//edit by coco 2012-05-30
                        if(result >0)
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "savesuccess", "alert('La asignación de productos fue exitosa.');", true);
                        else
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "savefail", "alert('La asignación de productos falló, verifique.');", true);

                    }

                    if (DeletedProduct != "")
                    {
                        result += K_PROVEEDOR_PRODUCTODal.ClassInstance.Delete_K_PROVEEDOR_PRODUCTO(SupplierID, DeletedProduct);
                    }

                    scope.Complete();
                }
                if (result > 0)
                {
                    LoadGridViewData();
                    //add by coco 2010-05-30
                    SelectedProductUnitPrice = null;
                    SelectedProduct = null;
                    DeletedProduct = null;
                    //end add
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupplierMonitor.aspx");
        }
        #endregion

        #region Private Methods
        private void SaveGridViewDetail()
        {
            for (int i = 0; i < ProductGroupNumber; i++)
            {
                DataRow oldrow;
                oldrow = GridViewDetail.Rows[i];

                DropDownList drpTechnology = grdProducts.Rows[i].FindControl("drpTechnology") as DropDownList;
                DropDownList drpMarca = grdProducts.Rows[i].FindControl("drpMarca") as DropDownList;
                DropDownList drpProduct = grdProducts.Rows[i].FindControl("drpProduct") as DropDownList;
                //updated by tina 2012-07-18
                //Label lblModel = grdProducts.Rows[i].FindControl("lblModel") as Label;
                DropDownList drpModel = grdProducts.Rows[i].FindControl("drpModel") as DropDownList;
                Label lblProduct = grdProducts.Rows[i].FindControl("lblProduct") as Label;
                //end
                //add by coco 2012-05-30
                Label lblMaxPrice = grdProducts.Rows[i].FindControl("lblMaxPrice") as Label;
                TextBox txtUnitPrice = grdProducts.Rows[i].FindControl("txtUnit") as TextBox;
                //end add

                oldrow["Technology"] = drpTechnology.SelectedIndex != -1 ? drpTechnology.SelectedValue.ToString() : "";
                oldrow["Marca"] = drpMarca.SelectedIndex != -1 ? drpMarca.SelectedValue.ToString() : "";
                //updated by tina 2012-07-18
                //oldrow["Product"] = drpProduct.SelectedIndex != -1 ? drpProduct.SelectedValue.ToString() : "";
                //oldrow["Model"] = lblModel.Text;
                oldrow["ProductName"] = drpProduct.SelectedIndex != -1 ? drpProduct.SelectedItem.Text : "";
                oldrow["Ft_Tipo_Producto"] = drpProduct.SelectedIndex != -1 ? drpProduct.SelectedValue.ToString() : "";
                oldrow["Product"] = lblProduct.Text;
                oldrow["Model"] = drpModel.SelectedIndex != -1 ? drpModel.SelectedValue.ToString() : "";
                //end

                if (!drpModel.Enabled)
                {
                    oldrow["Status"] = "1";
                }
                else
                {
                    oldrow["Status"] = "0";
                }

                //add by coco 2012-05-30
                oldrow["MaxPrice"] = lblMaxPrice.Text;
                oldrow["unitPrice"] = txtUnitPrice.Text;
                //end add
            }
        }

        private void RefreshProduct(GridViewRow row)
        {
            DropDownList drpTechnology = (DropDownList)row.FindControl("drpTechnology");
            DropDownList drpMarca = (DropDownList)row.FindControl("drpMarca");
            DropDownList drpProduct = (DropDownList)row.FindControl("drpProduct");
            //updated by tina 2012-07-18
            //Label lblModel = (Label)row.FindControl("lblModel");
            DropDownList drpModel = (DropDownList)row.FindControl("drpModel");
            Label lblProduct = (Label)row.FindControl("lblProduct");
            Label lblMaxPrice = (Label)row.FindControl("lblMaxPrice") as Label;
            TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnit") as TextBox;

            string value = drpModel.Text;
            BindMarcaDropDownList(drpTechnology.SelectedValue, drpMarca);
            BindProductNameDropDownList(drpTechnology.SelectedValue, drpMarca.SelectedValue, drpProduct);
            BindModelByTechnologyAndMarca(drpTechnology.SelectedValue, drpMarca.SelectedValue, drpModel);
            //BindModelDropDownList("", drpModel);
            if (value != drpModel.Text)
            {
                drpProduct.SelectedIndex = 0;
                lblProduct.Text = "";
                lblMaxPrice.Text = "0";
                txtUnitPrice.Text = "";
            }
            //end
        }

        // RSA 2013-02-18 Ini
        private void BindMarcaDropDownList(string technology, DropDownList drpMarca)
        {
            string value = drpMarca.SelectedValue;
            DataTable dtMarca = CAT_PRODUCTODal.ClassInstance.GetMarcaByTechnology(technology);
            if (dtMarca != null)
            {
                drpMarca.DataSource = dtMarca;
                drpMarca.DataTextField = "Dx_Marca";
                drpMarca.DataValueField = "Cve_Marca";
                drpMarca.DataBind();
                drpMarca.Items.Insert(0, "");
                if (drpMarca.Items.FindByValue(value) != null)
                    drpMarca.SelectedValue = value;
            }
        }
        private void BindModelByTechnologyAndMarca(string technology, string marca, DropDownList drpModel)
        {
            string value = drpModel.SelectedValue;
            DataTable dtModel = CAT_PRODUCTODal.ClassInstance.GetModelByTechnologyAndMarca(technology, marca);
            if (dtModel != null)
            {
                drpModel.DataSource = dtModel;
                drpModel.DataTextField = "Dx_Modelo_Producto";
                drpModel.DataValueField = "Dx_Modelo_Producto";
                drpModel.DataBind();
                drpModel.Items.Insert(0, "");
                if (drpModel.Items.FindByValue(value) != null)
                    drpModel.SelectedValue = value;
            }
        }
        // RSA Fin

        //added by tina 2012-07-18
        private void BindProductNameDropDownList(string technology, string marca, DropDownList drpProduct)
        {
            string value = drpProduct.SelectedValue;
            DataTable dtProductName = CAT_PRODUCTODal.ClassInstance.GetProductNameByTechnologyAndMarca(technology, marca);
            if (dtProductName != null)
            {
                drpProduct.DataSource = dtProductName;
                drpProduct.DataTextField = "Dx_Nombre_Producto";
                drpProduct.DataValueField = "Ft_Tipo_Producto";
                drpProduct.DataBind();
                drpProduct.Items.Insert(0, "");
                if (drpProduct.Items.FindByValue(value) != null)
                    drpProduct.SelectedValue = value;
            }
        }

        private void BindModelDropDownList(string productName, DropDownList drpModel)
        {
            DataTable dtModel = CAT_PRODUCTODal.ClassInstance.GetModelByProductName(productName);
            if (dtModel != null)
            {
                drpModel.DataSource = dtModel;
                drpModel.DataTextField = "Dx_Modelo_Producto";
                drpModel.DataValueField = "Dx_Modelo_Producto";
                drpModel.DataBind();
                drpModel.Items.Insert(0, "");
            }
        }
        //end

        private void GetSelectedProduct()
        {
            if (SelectedProduct == null)
            {
                SelectedProduct = new ArrayList();
            }
            //add by coco 2012-05-30
            if (SelectedProductUnitPrice == null)
            {
                SelectedProductUnitPrice = new ArrayList();
            }
            //end add
            for (int i = 0; i < GridViewDetail.Rows.Count; i++)
            {
                if (GridViewDetail.Rows[i]["Status"].ToString() == "1") // have assigned
                {
                    continue;
                }
                else
                {
                    string productId = GridViewDetail.Rows[i]["Product"].ToString();
                    if (productId != "")
                    {
                        int count = 0;
                        for (int j = 0; j < i; j++)
                        {
                            if (productId == GridViewDetail.Rows[j]["Product"].ToString())
                            {
                                break;
                            }
                            else
                            {
                                count++;
                            }
                        }
                        if (count == i)
                        {
                            //edit by coco 2012-05-30
                            string UnitPrice = GridViewDetail.Rows[i]["unitPrice"].ToString();
                            SelectedProduct.Add(productId);
                            SelectedProductUnitPrice.Add(UnitPrice);
                            //end edit
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        #endregion 
    }
}
