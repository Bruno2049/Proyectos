using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DisposalModule
{
    public partial class GasRecoveryRegistry : System.Web.UI.Page
    {
        private const string DEFAULT_SELECTED_ITEM = "";
        private class FilterCondition
        {
            public FilterCondition()
            { }
            private string _program;
            private string _supplierId;
            private string _supplierType;
            private string _technology;
            private int _disposalId;
            private string _disposalType;
            private string _material;
            private int _order;

            public string Program
            {
                get { return this._program; }
                set { this._program = value; }
            }
            public string SupplierId
            {
                get { return this._supplierId; }
                set { this._supplierId = value; }
            }
            public string SupplierType
            {
                get { return this._supplierType; }
                set { this._supplierType = value; }
            }
            public string Technology
            {
                get { return this._technology; }
                set { this._technology = value; }
            }
            public int DisposalId
            {
                get { return this._disposalId; }
                set { this._disposalId = value; }
            }
            public string DisposalType
            {
                get { return this._disposalType; }
                set { this._disposalType = value; }
            }
            public string Material
            {
                get { return this._material; }
                set { this._material = value; }
            }
            public int Order
            {
                get { return this._order; }
                set { this._order = value; }
            }
        }

        //added by tina 2012-08-09
        private string EquipmentNotRegistryAllMeterial
        {
            get { return ViewState["EquipmentNotRegistryAllMeterial"] == null ? "" : ViewState["EquipmentNotRegistryAllMeterial"].ToString(); }
            set { ViewState["EquipmentNotRegistryAllMeterial"] = value; }
        }
        //end added

        #region Initialize Components
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //check if session is null, if true return to login screen
                if (null == Session["UserInfo"])
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                //Setup current date to date control
                this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

                //added by tina 2012-08-09
                GetEquipmentNotRegistryAllMeterialOverTime();
                //end added

                //Initialize wizard pages                
                InitializeFirstPageComponents();

                InitializeSecondPageComponents();
            }
        }

        //added by tina 2012-08-09
        private void GetEquipmentNotRegistryAllMeterialOverTime()
        {
            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];

            if (UserModel != null)
            {
                int disposalID = 0;
                string disposalType = "";
                disposalID = UserModel.Id_Departamento;
                disposalType = UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B";

                DataTable dtEquipmentNotRegistryAllMeterialOverTime = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetEquipmentNotRegistryAllMeterialBeforeTimeLine(disposalID, disposalType);
                if (dtEquipmentNotRegistryAllMeterialOverTime != null && dtEquipmentNotRegistryAllMeterialOverTime.Rows.Count > 0)
                {
                    foreach (DataRow row in dtEquipmentNotRegistryAllMeterialOverTime.Rows)
                    {
                        EquipmentNotRegistryAllMeterial += row["Id_Credito_Sustitucion"].ToString() + ",";
                    }
                }
                EquipmentNotRegistryAllMeterial = EquipmentNotRegistryAllMeterial.TrimEnd(',');
            }
        }
        //end

        /// <summary>
        /// Init drop down list data 
        /// </summary>
        private void InitializeFirstPageComponents()
        {
            InitializePrograms();

            InitializeTechnology(DEFAULT_SELECTED_ITEM);

            //setup default register date
            this.txtRecoveryDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void InitializeSecondPageComponents()
        {
            InitializeSuppliers(DEFAULT_SELECTED_ITEM);

            InitializeCredits(DEFAULT_SELECTED_ITEM);

            InitializeOldProducts(DEFAULT_SELECTED_ITEM);

            VisualizeAndEnableControls(false);

            this.txtReceiptFromDate.Text = this.txtReceiptToDate.Text = this.txtInhabilitacionFromDate.Text = this.txtInhabilitacionToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void VisualizeAndEnableControls(bool visible)
        {
            drpProgram1.Enabled = visible;
            drpTechnology1.Enabled = visible;

            AspNetPager.Visible = visible;
            btnRegistry.Visible = visible;
        }

        private void InitializePrograms()
        {
            DataTable programs = CAT_PROGRAMADal.ClassInstance.GetPrograms();

            if (programs != null)
            {
                drpProgram.DataSource = programs;
                drpProgram.DataTextField = "Dx_Nombre_Programa";
                drpProgram.DataValueField = "ID_Prog_Proy";
                drpProgram.DataBind();

                drpProgram1.DataSource = programs;
                drpProgram1.DataTextField = "Dx_Nombre_Programa";
                drpProgram1.DataValueField = "ID_Prog_Proy";
                drpProgram1.DataBind();

                drpProgram1.SelectedValue = drpProgram.SelectedValue;
            }
        }

        private void InitializeTechnology(string selectedItem)
        {
            DataTable technologies = null;
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            if (UserModel != null)
            {
                string program = (drpProgram.SelectedIndex == -1 || drpProgram.SelectedIndex == 0) ? "" : drpProgram.SelectedValue;
                if (program != "")
                {
                    technologies = CAT_TECNOLOGIADAL.ClassInstance.GetTechnologyWithProgramandDisposalCenter(program, UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
                }
                else
                {
                    technologies = CAT_TECNOLOGIADAL.ClassInstance.GetDisposalCenterRelatedTechnology(UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
                }

                if (technologies != null)
                {
                    drpTechnology.DataSource = technologies;
                    drpTechnology.DataTextField = "Dx_Nombre_General";
                    drpTechnology.DataValueField = "Cve_Tecnologia";
                    drpTechnology.DataBind();

                    drpTechnology1.DataSource = technologies;
                    drpTechnology1.DataTextField = "Dx_Nombre_General";
                    drpTechnology1.DataValueField = "Cve_Tecnologia";
                    drpTechnology1.DataBind();

                    if (selectedItem != "")
                    {
                        ListItem item = drpTechnology.Items.FindByValue(selectedItem);
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                        //refresh materials based upon technology
                        InitializeMaterialTypes(drpMaterialType.SelectedValue);
                    }
                    else
                    {
                        InitializeMaterialTypes(DEFAULT_SELECTED_ITEM);
                    }

                    drpTechnology1.SelectedValue = drpTechnology.SelectedValue;
                }

                ////refresh materials based upon technology
                //if (drpTechnology.SelectedIndex != -1)
                //{
                //    InitializeMaterialTypes(drpMaterialType.SelectedValue);
                //}
            }
        }

        private void InitializeMaterialTypes(string selectedItem)
        {
            //clear old materials list
            drpMaterialType.Items.Clear();

            DataTable materials = null;
            string technology = drpTechnology.SelectedIndex == -1 ? "" : drpTechnology.SelectedValue;
            if (technology != "")
            {
                materials = CAT_RESIDUO_MATERIALDal.ClassInstance.GetMaterialTypeByTechnology(technology);
            }
            
            if (materials != null && materials.Rows.Count > 0)
            {
                panelMaterial.Visible = true;
                btnRecovery.Visible = true;

                drpMaterialType.DataSource = materials;
                drpMaterialType.DataTextField = "Dx_Residuo_Material_Gral";
                drpMaterialType.DataValueField = "Cve_Residuo_Material";
                drpMaterialType.DataBind();

                if (selectedItem != "")
                {
                    ListItem item = drpMaterialType.Items.FindByValue(selectedItem);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                    InitializeGasTypes(drpGasType.SelectedValue);
                }
                else
                {
                    InitializeGasTypes(DEFAULT_SELECTED_ITEM);
                }

                if (drpMaterialType.SelectedValue == "0") // Gas Refrigerante
                {
                    lblGasType.Visible = true;
                    drpGasType.Visible = true;

                    lblMaterialName.Text = drpMaterialType.SelectedItem.Text;
                    lblUnit.Text = "Kgs";

                    //InitializeGasTypes();
                }
                else
                {
                    lblGasType.Visible = false;
                    drpGasType.Visible = false;

                    lblMaterialName.Text = drpMaterialType.SelectedItem.Text;

                    DataTable materialsWithTechnology = CAT_RESIDUO_MATERIALDal.ClassInstance.GetMaterialByPk(drpMaterialType.SelectedValue);

                    if (materialsWithTechnology.Rows.Count > 0)
                    {
                        lblUnit.Text = materialsWithTechnology.Rows[0]["Dx_Unidades"].ToString();
                    }
                }

                //Change material label
                RefreshMaterialLabelName();
            }
            else
            {
                panelMaterial.Visible = false;
                btnRecovery.Visible = false;
            }
        }

        private void InitializeGasTypes(string selectedItem)
        {
            DataTable gasTypes = null;
            string technology = (drpTechnology.SelectedIndex == -1) ? "" : drpTechnology.SelectedValue;

            gasTypes = CAT_RESIDUO_MATERIALDal.ClassInstance.GetGasTypeByTechnology(technology);
            if (gasTypes != null)
            {
                drpGasType.DataSource = gasTypes;
                drpGasType.DataTextField = "Dx_Residuo_Material_Gral";
                drpGasType.DataValueField = "Cve_Residuo_Material";
                drpGasType.DataBind();

                if (selectedItem != "")
                {
                    ListItem item = drpGasType.Items.FindByValue(selectedItem);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        private void InitializeCredits(string selectedItem)
        {
            FilterCondition filterConditions = GetFilterConditions();

            //updated by tina 2012-08-09
            DataTable credits = K_CREDITODal.ClassInstance.GetCreditsWithTechnologyAndMaterialAndProgramAndSupplier(filterConditions.Program, filterConditions.Technology, filterConditions.Material,
                            filterConditions.Order, filterConditions.SupplierId, filterConditions.SupplierType, filterConditions.DisposalId, filterConditions.DisposalType, EquipmentNotRegistryAllMeterial);
            //end updated
            if (credits != null)
            {
                drpCredit.DataSource = credits;
                drpCredit.DataTextField = "No_Credito";
                drpCredit.DataValueField = "No_Credito";
                drpCredit.DataBind();
                drpCredit.Items.Insert(0, new ListItem(""));

                if (selectedItem != "")
                {
                    ListItem item = drpCredit.Items.FindByValue(selectedItem);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        private void InitializeOldProducts(string selectedItem)
        {
            FilterCondition filterConditions = GetFilterConditions();

            string credit = (drpCredit.SelectedIndex == -1 || drpCredit.SelectedIndex == 0) ? "" : drpCredit.SelectedValue;

            //updated by tina 2012-08-09
            DataTable oldProductInternalCode = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetReceivedOldProductsInternalCodeForMaterialRegistry(credit, filterConditions.Program, 
                                                            filterConditions.Technology,filterConditions.Material, filterConditions.Order, filterConditions.SupplierId, filterConditions.SupplierType, 
                                                            filterConditions.DisposalId, filterConditions.DisposalType,EquipmentNotRegistryAllMeterial);
            //updated by tina 2012-08-09
            if (oldProductInternalCode != null)
            {
                drpInteralCode.DataSource = oldProductInternalCode;
                drpInteralCode.DataTextField = "Id_Folio";
                drpInteralCode.DataValueField = "Id_Folio";
                drpInteralCode.DataBind();
                drpInteralCode.Items.Insert(0, new ListItem(""));

                if (selectedItem != "")
                {
                    ListItem item = drpInteralCode.Items.FindByValue(selectedItem);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        private void InitializeSuppliers(string selectedItem)
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                DataTable suppliers = CAT_PROVEEDORDal.ClassInstance.GetDisposalCenterRelatedSuppliers(UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
                if (suppliers != null)
                {
                    drpDistributor.DataSource = suppliers;
                    drpDistributor.DataTextField = "Dx_Nombre_Comercial";
                    drpDistributor.DataValueField = "Id_Proveedor";
                    drpDistributor.DataBind();
                    drpDistributor.Items.Insert(0, new ListItem(""));
                }

                if (selectedItem != "")
                {
                    ListItem item = drpDistributor.Items.FindByValue(selectedItem);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        private FilterCondition GetFilterConditions()
        {
            FilterCondition filterConditions = new FilterCondition();
            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];

            if (UserModel != null)
            {
                filterConditions.Program = this.drpProgram.SelectedIndex == -1 ? "" : this.drpProgram.SelectedValue;
                filterConditions.SupplierId = (this.drpDistributor.SelectedIndex == 0 || this.drpDistributor.SelectedIndex == -1) ? "" : this.drpDistributor.SelectedValue.Substring(0, this.drpDistributor.SelectedValue.IndexOf('-'));
                filterConditions.SupplierType = "";
                if (this.drpDistributor.SelectedIndex != 0 && this.drpDistributor.SelectedIndex != -1)
                {
                    if (this.drpDistributor.SelectedItem.Text.Contains("(SUPPLIER)"))
                    {
                        filterConditions.SupplierType = "S";
                    }
                    else
                    {
                        filterConditions.SupplierType = "S_B";
                    }
                }

                filterConditions.Technology = this.drpTechnology.SelectedIndex == -1 ? "" : this.drpTechnology.SelectedValue;
                filterConditions.DisposalId = UserModel.Id_Departamento;
                filterConditions.DisposalType = UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B";

                filterConditions.Material = drpMaterialType.SelectedIndex == -1 ? "-1" : drpMaterialType.SelectedValue == "0" ? drpGasType.SelectedValue : drpMaterialType.SelectedValue;
                int order = 1;
                if (drpMaterialType.SelectedValue != "0")
                {
                    order = K_TECNOLOGIA_RESIDUO_MATERIALDal.ClassInstance.GetOrderByTechnologyAndMaterial(drpTechnology.SelectedValue, filterConditions.Material);
                }
                filterConditions.Order = order;
            }

            return filterConditions;
        }

        #endregion

        #region Controls Changing Events
        protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeTechnology(drpTechnology.SelectedValue);
            InitializeCredits(drpCredit.SelectedValue);
            InitializeOldProducts(drpInteralCode.SelectedValue);
            //InitMaterialType();
            drpProgram1.SelectedValue = drpProgram.SelectedValue;
            drpTechnology1.SelectedValue = drpTechnology.SelectedValue;
        }

        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeMaterialTypes(drpMaterialType.SelectedValue);
            drpTechnology1.SelectedValue = drpTechnology.SelectedValue;
            InitializeCredits(drpCredit.SelectedValue);
            InitializeOldProducts(drpInteralCode.SelectedValue);
        }

        protected void drpMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMaterialLabelName();

            if (drpMaterialType.SelectedValue == "0") // Gas Refrigerante
            {
                lblGasType.Visible = true;
                drpGasType.Visible = true;

                lblMaterialName.Text = drpMaterialType.SelectedItem.Text;
                lblUnit.Text = "Kgs";

                InitializeGasTypes(drpGasType.SelectedValue);
            }
            else
            {
                lblGasType.Visible = false;
                drpGasType.Visible = false;

                lblMaterialName.Text = drpMaterialType.SelectedItem.Text;

                DataTable materials = CAT_RESIDUO_MATERIALDal.ClassInstance.GetMaterialByPk(drpMaterialType.SelectedValue);

                if (materials.Rows.Count > 0)
                {
                    lblUnit.Text = materials.Rows[0]["Dx_Unidades"].ToString();
                }
            }

            InitializeCredits(drpCredit.SelectedValue);
            InitializeOldProducts(drpInteralCode.SelectedValue);
        }

        protected void drpGasType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCredits(drpCredit.SelectedValue);
            InitializeOldProducts(drpInteralCode.SelectedValue);
        }

        protected void drpCredit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeOldProducts(drpInteralCode.SelectedValue);
        }

        protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCredits(drpCredit.SelectedValue);
            InitializeOldProducts(drpInteralCode.SelectedValue);
        }

        #endregion

        #region Button Clicks
        protected void btnNext_Click(object sender, EventArgs e)
        {
            DataTable dtRecovery = null;
            string material = drpMaterialType.SelectedValue == "0" ? drpGasType.SelectedValue : drpMaterialType.SelectedValue;
            string recoveryDate = txtRecoveryDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : txtRecoveryDate.Text.Trim();
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            dtRecovery = K_RECUPERACIONDal.ClassInstance.GetTodayRecordByTechnologyAndMaterial(drpTechnology.SelectedValue, material, recoveryDate, UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
            double weight = 0;
            double.TryParse(txtWeight.Text, out weight);

            if (DateTime.Parse(recoveryDate) > DateTime.Now)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DateError",
                    "alert('La fecha de recuperación no puede ser mayor al día de hoy');", true);

                return;
            }
            if (weight <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page),
                                            "Warning", "alert('  ¡ Debe capturar el peso de recuperación correspondiente!');", true);
            }
            else if (dtRecovery == null || dtRecovery.Rows.Count == 0)
            {
                //InitializeSecondPageComponents();
                this.wizardPages.MoveTo(this.Registry);
                ChangeTitle();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page),
                                            "Warning", "alert('  ¡ cada registro de recuperación de material se puede hacer sólo una vez al día');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../default.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CacheFilterConditions();
            ClearCachedProducts();
            LoadGridViewData();
            this.AspNetPager.GoToPage(1);
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            grdOldEquipment.Visible = false;
            btnRegistry.Visible = false;
            this.AspNetPager.Visible = false;
            this.wizardPages.MoveTo(this.CaptureWeight);
            ChangeTitle();
        }

        protected void btnRegistry_Click(object sender, EventArgs e)
        {
            RememberOldSelectedProducts();

            if (drpTechnology.SelectedIndex != -1 && drpMaterialType.SelectedIndex != -1)
            {
                if (Session["MaterialRegistrySelectProducts"] != null && ((ArrayList)Session["MaterialRegistrySelectProducts"]).Count > 0)
                {
                    int result = 0;
                    int Id_Recuperacion = 0;
                    ArrayList listProducts = new ArrayList();
                    listProducts = (ArrayList)Session["MaterialRegistrySelectProducts"];

                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                    {
                        if (drpTechnology.SelectedIndex != -1 && drpMaterialType.SelectedIndex != -1)
                        {
                            K_RECUPERACIONModel model = GetData();

                            result = K_RECUPERACIONDal.ClassInstance.Insert_K_RECUPERACION(model, out Id_Recuperacion);
                        }

                        result += K_RECUPERACION_PRODUCTOBLL.ClassInstance.Insert_K_RECUPERACION_PRODUCTO(Id_Recuperacion, listProducts);

                        scope.Complete();
                    }

                    if (result > 0)
                    {
                        ClearCachedProducts();
                        LoadGridViewData();
                        this.AspNetPager.GoToPage(1);
                        //added by tina 2012-08-09
                        GetEquipmentNotRegistryAllMeterialOverTime();
                        //end added
                    }
                }
            }
        }

        #endregion

        #region Events
        protected void grdOldEquipment_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in this.grdOldEquipment.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox ckbSelect = (CheckBox)row.FindControl("ckbSelect");
                    if (this.grdOldEquipment.DataKeys[row.RowIndex][0].ToString() == "")//no record
                    {
                        ckbSelect.Visible = false;
                    }
                }
            }
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadGridViewData();
                ReCheckSelectedProducts();
            }
        }

        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (IsPostBack)
            {
                RememberOldSelectedProducts();

                this.drpProgram.SelectedIndex = Session["CurrentProgramMaterialRegistry"] != null ? (int)Session["CurrentProgramMaterialRegistry"] : 0;
                this.drpCredit.SelectedIndex = Session["CurrentCreditMaterialRegistry"] != null ? (int)Session["CurrentCreditMaterialRegistry"] : 0;
                this.drpInteralCode.SelectedIndex = Session["CurrentInternalCodeMaterialRegistry"] != null ? (int)Session["CurrentInternalCodeMaterialRegistry"] : 0;
                this.drpDistributor.SelectedIndex = Session["CurrentSupplierMaterialRegistry"] != null ? (int)Session["CurrentSupplierMaterialRegistry"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrentTechnologyMaterialRegistry"] != null ? (int)Session["CurrentTechnologyMaterialRegistry"] : 0;
                this.txtReceiptFromDate.Text = Session["CurrentReceiptFromDateMaterialRegistry"] != null ? (string)Session["CurrentReceiptFromDateMaterialRegistry"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtReceiptToDate.Text = Session["CurrentReceiptToDateMaterialRegistry"] != null ? (string)Session["CurrentReceiptToDateMaterialRegistry"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtInhabilitacionFromDate.Text = Session["CurrentInhabilitacionFromDateMaterialRegistry"] != null ? (string)Session["CurrentInhabilitacionFromDateMaterialRegistry"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtInhabilitacionToDate.Text = Session["CurrentInhabilitacionToDateMaterialRegistry"] != null ? (string)Session["CurrentInhabilitacionToDateMaterialRegistry"] : DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        #endregion

        #region Private Methods
        private K_RECUPERACIONModel GetData()
        {
            K_RECUPERACIONModel model = new K_RECUPERACIONModel();
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            if (UserModel != null)
            {
                string weightMaterial = txtWeight.Text.Trim() == "" ? "0" : txtWeight.Text.Trim();
                model.No_Material = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(weightMaterial)));
                model.Dt_Fecha_Recuperacion = txtRecoveryDate.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(txtRecoveryDate.Text.Trim());
                model.Cve_Tecnologia = Convert.ToInt32(drpTechnology.SelectedValue);
                model.Cve_Residuo_Material = Convert.ToInt32(drpMaterialType.SelectedValue == "0" ? drpGasType.SelectedValue : drpMaterialType.SelectedValue);
                model.Id_Centro_Disp = UserModel.Id_Departamento;
                if (UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER)
                {
                    model.Fg_Tipo_Centro_Disp = "M";
                }
                else
                {
                    model.Fg_Tipo_Centro_Disp = "B";
                }
            }

            return model;
        }

        private void CacheFilterConditions()
        {
            Session["CurrentProgramMaterialRegistry"] = drpProgram.SelectedIndex;
            Session["CurrentCreditMaterialRegistry"] = drpCredit.SelectedIndex;
            Session["CurrentInternalCodeMaterialRegistry"] = drpInteralCode.SelectedIndex;
            Session["CurrentSupplierMaterialRegistry"] = drpDistributor.SelectedIndex;
            Session["CurrentTechnologyMaterialRegistry"] = drpTechnology.SelectedIndex;
            Session["CurrentReceiptFromDateMaterialRegistry"] = txtReceiptFromDate.Text.Trim();
            Session["CurrentReceiptToDateMaterialRegistry"] = txtReceiptToDate.Text.Trim();
            Session["CurrentInhabilitacionFromDateMaterialRegistry"] = txtInhabilitacionFromDate.Text.Trim();
            Session["CurrentInhabilitacionToDateMaterialRegistry"] = txtInhabilitacionToDate.Text.Trim();
        }

        private void LoadGridViewData()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            if (UserModel != null)
            {
                int PageCount = 0;
                DataTable dtProducts = null;

                FilterCondition filterConditions = GetFilterConditions();

                string credit = (this.drpCredit.SelectedIndex == 0 || this.drpCredit.SelectedIndex == -1) ? "" : this.drpCredit.SelectedValue;
                string internalCode = (this.drpInteralCode.SelectedIndex == 0 || this.drpInteralCode.SelectedIndex == -1) ? "" : this.drpInteralCode.SelectedValue.Substring(0, 6);
                string receiptFromDate = this.txtReceiptFromDate.Text.Trim();
                string receiptToDate = this.txtReceiptToDate.Text.Trim();
                string inhabilitacionFromDate = this.txtInhabilitacionFromDate.Text.Trim();
                string inhabilitacionToDate = this.txtInhabilitacionToDate.Text.Trim();
                string recoveryDate = this.txtRecoveryDate.Text.Trim();

                //updated by tina 2012-07-12 //updated by tina 2012-08-09              
                if (drpMaterialType.SelectedValue == "0" || filterConditions.Order == 1)
                {
                    dtProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetGasRecoveryProducts(filterConditions.Program, credit, internalCode, filterConditions.SupplierId, filterConditions.SupplierType,
                                      filterConditions.Technology, receiptFromDate, receiptToDate, inhabilitacionFromDate, inhabilitacionToDate, Convert.ToInt32(filterConditions.Material),
                                      filterConditions.DisposalId, filterConditions.DisposalType, "", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount, EquipmentNotRegistryAllMeterial, recoveryDate);
                }
                else
                {
                    if (filterConditions.Order == 2)
                    {

                        dtProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetRecoveryProducts(filterConditions.Program, credit, internalCode, filterConditions.SupplierId, filterConditions.SupplierType,
                                          filterConditions.Technology, receiptFromDate, receiptToDate, inhabilitacionFromDate, inhabilitacionToDate, Convert.ToInt32(filterConditions.Material),
                                          filterConditions.DisposalId, filterConditions.DisposalType, "", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount, EquipmentNotRegistryAllMeterial, recoveryDate);
                    }
                    else if (filterConditions.Order > 2)
                    {
                        dtProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetRecoveryProducts(filterConditions.Program, credit, internalCode, filterConditions.SupplierId, filterConditions.SupplierType,
                                          filterConditions.Technology, receiptFromDate, receiptToDate, inhabilitacionFromDate, inhabilitacionToDate, Convert.ToInt32(filterConditions.Material),
                                          filterConditions.Order, filterConditions.DisposalId, filterConditions.DisposalType, "", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount, EquipmentNotRegistryAllMeterial, recoveryDate);
                    }
                }
                //end updated

                grdOldEquipment.Visible = true;

                if (dtProducts != null)
                {
                    btnRegistry.Visible = true;
                    if (dtProducts.Rows.Count == 0)
                    {
                        btnRegistry.Enabled = false;
                        dtProducts.Rows.Add(dtProducts.NewRow());
                    }
                    else
                    {
                        btnRegistry.Enabled = true;
                    }

                    //bind
                    this.AspNetPager.RecordCount = PageCount;
                    this.grdOldEquipment.DataSource = dtProducts;
                    this.grdOldEquipment.DataBind();
                }

                AspNetPager.Visible = true;
            }
        }

        private void ClearCachedProducts()
        {
            Session["MaterialRegistrySelectProducts"] = null;
        }

        /// <summary>
        /// remember the selected products
        /// </summary>
        private void RememberOldSelectedProducts()
        {
            ArrayList arrListProducts = new ArrayList();

            foreach (GridViewRow Row in grdOldEquipment.Rows)
            {
                string oldProductInnerCode = this.grdOldEquipment.DataKeys[Row.RowIndex][0].ToString();

                bool result = ((CheckBox)Row.FindControl("ckbSelect")).Checked;

                if (Session["MaterialRegistrySelectProducts"] != null)
                {
                    arrListProducts = (ArrayList)Session["MaterialRegistrySelectProducts"];
                }

                if (result)
                {
                    // save selected product code
                    if (!arrListProducts.Contains(oldProductInnerCode))
                    {
                        arrListProducts.Add(oldProductInnerCode);
                    }
                }
                else
                {
                    if (arrListProducts.Contains(oldProductInnerCode))
                    {
                        arrListProducts.Remove(oldProductInnerCode);
                    }
                }

                if (arrListProducts != null && arrListProducts.Count > 0)
                {
                    Session["MaterialRegistrySelectProducts"] = arrListProducts;
                }
            }
        }

        /// <summary>
        /// repopulate checkbox status
        /// </summary>
        private void ReCheckSelectedProducts()
        {
            ArrayList arrListProducts = (ArrayList)Session["MaterialRegistrySelectProducts"];

            if (arrListProducts != null && arrListProducts.Count > 0)
            {
                foreach (GridViewRow Row in grdOldEquipment.Rows)
                {
                    string productCode = this.grdOldEquipment.DataKeys[Row.RowIndex][0].ToString();
                    if (arrListProducts.Contains(productCode))
                    {
                        CheckBox ckbSelect = (CheckBox)Row.FindControl("ckbSelect");
                        ckbSelect.Checked = true;
                    }
                }
            }
        }       

        private void ChangeTitle()
        {
            if (this.wizardPages.ActiveStepIndex == 0)
            {
                lblTitle.Text = "REGISTRO DE RECUPERACION DE RESIDUOS: ";
            }
            else
            {
                lblTitle.Text = "REGISTRO DE EQUIPOS CON RECUPERACION DE ";
            }
        }

        private void RefreshMaterialLabelName()
        { 
            switch (drpMaterialType.SelectedItem.Text)
            {
                case "Gas Refrigerante":
                    literalMaterial.Text = "GAS";
                    break;
                case "Aceite":
                    literalMaterial.Text = "ACEITE";
                    break;
                case "Cobre":
                    literalMaterial.Text = "COBRE";
                    break;
                case "Aluminio":
                    literalMaterial.Text = "ALUMINIO";
                    break;
                case "Fierro":
                    literalMaterial.Text = "FIERRO";
                    break;
                case "Otros Materiales":
                    literalMaterial.Text = "OTROS MATERIALES";
                    break;
            }
        }
        #endregion
    }
}
