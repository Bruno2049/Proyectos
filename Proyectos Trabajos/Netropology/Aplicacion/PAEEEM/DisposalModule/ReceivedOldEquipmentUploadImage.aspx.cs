using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DisposalModule
{
    public partial class ReceivedOldEquipmentUploadImage : System.Web.UI.Page
    {
        private const string DEFAULT_SELECTED_ITEM = "";
        private class FilterCondition
        {
            public FilterCondition()
            { }
            private string _program;
            private string _supplier;
            private string _supplierType;
            private string _technology;

            public string Program
            {
                get { return this._program; }
                set { this._program = value; }
            }
            public string Supplier
            {
                get { return this._supplier; }
                set { this._supplier = value; }
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
        }

        #region Define Global variable
        public string DisposalCenterID
        {
            get
            {
                return ViewState["DisposalID"] == null ? "0" : ViewState["DisposalID"].ToString();
            }
            set
            {
                ViewState["DisposalID"] = value;
            }
        }

        public string DisposalCenterType
        {
            get
            {
                return ViewState["DisposalCenterType"] == null ? "" : ViewState["DisposalCenterType"].ToString();
            }
            set
            {
                ViewState["DisposalCenterType"] = value;
            }
        }
        #endregion

        #region Initialize Data When Page Loaded
        /// <summary>
        /// Init default filter conditions when page first loaded
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //check session status
                    if (null == Session["UserInfo"])
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }
                    //cache disposal center type and id for later use
                    DisposalCenterID = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento.ToString();
                    string UserType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;

                    if (UserType == GlobalVar.DISPOSAL_CENTER)
                    {
                        DisposalCenterType = "M";
                    }
                    else
                    {
                        DisposalCenterType = "B";
                    }
                  
                    //Init program drop down list                
                    InitializePrograms();
                    //Init suppliers
                    InitializeSuppliers(DEFAULT_SELECTED_ITEM);
                    //Init technology
                    InitializeTechnologies(DEFAULT_SELECTED_ITEM);
                    //Init credit request drop down list
                    InitializeCredits(DEFAULT_SELECTED_ITEM);
                    //Init old products list
                    InitializeReceivedOldProducts(DEFAULT_SELECTED_ITEM);                  
                    //Hidden aspnetPager
                    AspNetPager.Visible = false;
                    //set default date for date time controls
                    //txtFromDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    DataTable data = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetOldestReceivedProductsWithoutImage(drpProgram.SelectedValue, int.Parse(DisposalCenterID), DisposalCenterType);
                    string ultimo = data != null && data.Rows.Count > 0 ? data.Rows[0]["Dt_Fecha_Recepcion"].ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(ultimo))
                    {
                        DateTime date = Convert.ToDateTime(ultimo);
                        txtFromDate.Text = date.ToString("yyyy-MM-dd");
                        txtToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.panel, typeof(Page), "LoadedError", "alert('" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// Init programs
        /// </summary>
        private void InitializePrograms()
        {
            DataTable dtProgram = null;
            dtProgram = CAT_PROGRAMADal.ClassInstance.GetPrograms();
            if (dtProgram != null)
            {
                drpProgram.DataSource = dtProgram;
                drpProgram.DataTextField = "Dx_Nombre_Programa";
                drpProgram.DataValueField = "ID_Prog_Proy";
                drpProgram.DataBind();
                drpProgram.Items.Insert(0, new ListItem(""));
            }
        }
        /// <summary>
        /// Init credit request list
        /// </summary>
        private void InitializeCredits(string selectedItem)
        {
            DataTable credits = null;   
            FilterCondition FilterValue = new FilterCondition();
            FilterValue=GetFilterValue();
            //retrieve the program and disposal center related credit requests          
            credits = K_CREDITODal.ClassInstance.GetCreditByProgramAndDisposalAndSupplierAndTechnology(FilterValue.Program,
                                                DisposalCenterID, DisposalCenterType, FilterValue.Supplier, FilterValue.SupplierType, FilterValue.Technology);
            if (credits != null)
            {
                drpCredit.DataSource = credits;
                drpCredit.DataTextField = "No_Credito";
                drpCredit.DataValueField = "No_Credito";
                drpCredit.DataBind();
                drpCredit.Items.Insert(0, new ListItem(""));
                if (selectedItem!="")
                {
                    ListItem item = drpCredit.Items.FindByValue(selectedItem);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        /// <summary>
        /// Init old equipment internal Code list
        /// </summary>
        private void InitializeReceivedOldProducts(string selectedItem)
        {
            DataTable oldProducts = null;
            FilterCondition FilterValue = new FilterCondition();
            //Get partial Filter value
            FilterValue=GetFilterValue();
            string credit = (this.drpCredit.SelectedIndex == 0 || this.drpCredit.SelectedIndex == -1) ? "" : this.drpCredit.SelectedValue;       
            //retrieve the related old products to specific credit request           
            oldProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetReceivedOldProductsInternalCodeByFilter(
                                                    credit, FilterValue.Supplier, FilterValue.SupplierType, FilterValue.Technology, FilterValue.Program, DisposalCenterID, DisposalCenterType);
            if (oldProducts != null)
            {
                drpInteralCode.DataSource = oldProducts;
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
        /// <summary>
        /// Init suppliers
        /// </summary>
        private void InitializeSuppliers(string selectedItem)
        {
            DataTable suppliers = null;
            suppliers = CAT_PROVEEDORDal.ClassInstance.GetDisposalCenterRelatedSuppliers(int.Parse(DisposalCenterID), DisposalCenterType);

            if (suppliers != null)
            {
                drpDistributor.DataSource = suppliers;
                drpDistributor.DataTextField = "Dx_Nombre_Comercial";
                drpDistributor.DataValueField = "Id_Proveedor";
                drpDistributor.DataBind();
                drpDistributor.Items.Insert(0, new ListItem(""));
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

        /// <summary>
        /// Init technology list
        /// </summary>
        private void InitializeTechnologies(string selectedItem)
        {
            DataTable technologies = null;
            string program = (drpProgram.SelectedIndex == -1 || drpProgram.SelectedIndex == 0) ? "" : drpProgram.SelectedValue;

            technologies = CAT_TECNOLOGIADAL.ClassInstance.GetTechnologyWithProgramandDisposalCenter(program, int.Parse(DisposalCenterID), DisposalCenterType);
            if (technologies != null)
            {
                drpTechnology.DataSource = technologies;
                drpTechnology.DataTextField = "Dx_Nombre_General";
                drpTechnology.DataValueField = "Cve_Tecnologia";
                drpTechnology.DataBind();
                drpTechnology.Items.Insert(0, new ListItem(""));
                if (selectedItem != "")
                {
                    ListItem item = drpTechnology.Items.FindByValue(selectedItem);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        #endregion

        #region "Button Action"
        /// <summary>
        /// Search the old products list with some conditions
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CacheFilterConditions();
            RefreshGridViewData();
            AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Cache the selected filter conditions
        /// </summary>
        private void CacheFilterConditions()
        {
            Session["CurrentProgramRecivedImage"] = drpProgram.SelectedIndex;
            Session["CurrentCreditRecivedImage"] = drpCredit.SelectedIndex;
            Session["CurrentInternalCodeRecivedImage"] = drpInteralCode.SelectedIndex;
            Session["CurrentSupplierRecivedImage"] = drpDistributor.SelectedIndex;
            Session["CurrentTypeRecivedImage"] = drpType.SelectedIndex;
            Session["CurrentTechnologyRecivedImage"] = drpTechnology.SelectedIndex;
            Session["CurrentFromDateRecivedImage"] = txtFromDate.Text.Trim();
            Session["CurrentToDateRecivedImage"] = txtToDate.Text.Trim();
        }
        /// <summary>
        /// Refresh the grid view control
        /// </summary>
        private void RefreshGridViewData()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                int PageCount = 0;
                DataTable oldProductsReceived = null;
                //filter conditions
                FilterCondition FilterValue = new FilterCondition();
                //Get partial Filter value
                FilterValue=GetFilterValue();
                string credit = (this.drpCredit.SelectedIndex == 0 || this.drpCredit.SelectedIndex == -1) ? "" : this.drpCredit.SelectedValue;       
                string type = this.drpType.SelectedValue;
                string internalCode = (this.drpInteralCode.SelectedIndex == 0 || this.drpInteralCode.SelectedIndex == -1) ? "" : this.drpInteralCode.SelectedValue.ToString();
                string fromDate = this.txtFromDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtFromDate.Text.Trim();
                string toDate = this.txtToDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtToDate.Text.Trim();

                //retrieve old products with the filter conditions
                oldProductsReceived = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetRecoveryProductsForImage(FilterValue.Program, credit, internalCode.Trim(), FilterValue.Supplier.Trim(), FilterValue.SupplierType, type, FilterValue.Technology, fromDate, toDate,
                                                                                                                                      int.Parse(DisposalCenterID), DisposalCenterType, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
                bool emptyRows = false;
                if (oldProductsReceived != null)
                {
                    if (oldProductsReceived.Rows.Count == 0)
                    {
                        emptyRows = true;
                        oldProductsReceived.Rows.Add(oldProductsReceived.NewRow());

                    }

                    //bind old products to grid view control
                    this.AspNetPager.RecordCount = PageCount;
                    this.grdReceivedOldEquipment.DataSource = oldProductsReceived;
                    this.grdReceivedOldEquipment.DataBind();
                }

                //hide the button columns when row is empty
                if (emptyRows)
                {
                    Image imgIcon = grdReceivedOldEquipment.Rows[0].FindControl("imgFoto") as Image;                                       
                    if (imgIcon != null)
                    {
                        imgIcon.Visible = false;
                    }

                    FileUpload fldSelect = grdReceivedOldEquipment.Rows[0].FindControl("fldSelect") as FileUpload;
                    if (fldSelect != null)
                    {
                        fldSelect.Visible = false;
                    }

                    Button btnUpload = grdReceivedOldEquipment.Rows[0].FindControl("btnUpLaod") as Button;
                    if (btnUpload != null)
                    {
                        btnUpload.Visible = false;
                    }
                    // added by coco 2012-02-24
                    LinkButton linkShowImage = grdReceivedOldEquipment.Rows[0].FindControl("lkbShowImage") as LinkButton;
                    if (linkShowImage != null)
                    {
                        linkShowImage.Visible = false;
                    }
                    //end add
                }
                //visible bottom grid view control
                AspNetPager.Visible = true;
            }
        }

        private FilterCondition GetFilterValue()
        {
            FilterCondition FilterValue = new FilterCondition();
           FilterValue.Program = (this.drpProgram.SelectedIndex == 0 || this.drpProgram.SelectedIndex == -1) ? "" : this.drpProgram.SelectedValue;
           FilterValue.Supplier = (this.drpDistributor.SelectedIndex == 0 || this.drpDistributor.SelectedIndex == -1) ? "" : this.drpDistributor.SelectedValue.Substring(0, this.drpDistributor.SelectedValue.IndexOf('-'));
           FilterValue.SupplierType = "";
            if (this.drpDistributor.SelectedIndex != 0 && this.drpDistributor.SelectedIndex != -1)
            {
                if (this.drpDistributor.SelectedItem.Text.Contains("(MATRIZ)"))
                {
                    FilterValue.SupplierType = "S";
                }
                else
                {
                    FilterValue.SupplierType = "S_B";
                }
            }

            FilterValue.Technology = (this.drpTechnology.SelectedIndex == 0 || this.drpTechnology.SelectedIndex == -1) ? "" : this.drpTechnology.SelectedValue;
            return FilterValue;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        /// <summary>
        /// Determine the action columns' status when grid view data bound
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void grdReceivedOldEquipment_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < grdReceivedOldEquipment.Rows.Count; i++)
            {
                string IsUpload = grdReceivedOldEquipment.DataKeys[i][1].ToString();

                Image Img = grdReceivedOldEquipment.Rows[i].FindControl("imgFoto") as Image;
                FileUpload btnSelect = grdReceivedOldEquipment.Rows[i].FindControl("fldSelect") as FileUpload;
                Button btnUpload = grdReceivedOldEquipment.Rows[i].FindControl("btnUpLaod") as Button;
                LinkButton linkShowImage = grdReceivedOldEquipment.Rows[i].FindControl("lkbShowImage") as LinkButton; // added by coco 2012-02-24

                if (IsUpload == "0")//photograph is not uploaded
                {
                    Img.ImageUrl = "~/DisposalModule/images/Image2.png";
                    linkShowImage.Visible = false;//add by coco 20120224
                }
                else if (IsUpload == "1")
                {
                    Img.ImageUrl = "~/DisposalModule/images/Image1.png";
                    linkShowImage.Visible = true;//add by coco 20120224
                    btnSelect.Visible = false;
                    btnUpload.Visible = false;
                }
            }
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                RefreshGridViewData();
            }
        }

        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (IsPostBack)
            {
                //setup filter conditions for data refreshing
                this.drpProgram.SelectedIndex = Session["CurrentProgramRecivedImage"] != null ? (int)Session["CurrentProgramRecivedImage"] : 0;
                this.drpCredit.SelectedIndex = Session["CurrentCreditRecivedImage"] != null ? (int)Session["CurrentCreditRecivedImage"] : 0;
                this.drpInteralCode.SelectedIndex = Session["CurrentInternalCodeRecivedImage"] != null ? (int)Session["CurrentInternalCodeRecivedImage"] : 0;
                this.drpDistributor.SelectedIndex = Session["CurrentSupplierRecivedImage"] != null ? (int)Session["CurrentSupplierRecivedImage"] : 0;
                this.drpType.SelectedIndex = Session["CurrentTypeRecivedImage"] != null ? (int)Session["CurrentTypeRecivedImage"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrentTechnologyRecivedImage"] != null ? (int)Session["CurrentTechnologyRecivedImage"] : 0;
                this.txtFromDate.Text = Session["CurrentFromDateRecivedImage"] != null ? (string)Session["CurrentFromDateRecivedImage"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtToDate.Text = Session["CurrentToDateRecivedImage"] != null ? (string)Session["CurrentToDateRecivedImage"] : DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnUpLaod_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewRow gridViewRow = (GridViewRow)((Button)sender).NamingContainer;
                int RowIndex = gridViewRow.RowIndex;

                DataTable data = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetOldestReceivedProductsWithoutImage(drpProgram.SelectedValue, int.Parse(DisposalCenterID), DisposalCenterType);
                string ultimo = data != null && data.Rows.Count > 0 ? data.Rows[0]["Id_Folio"].ToString() : string.Empty;
                string current = grdReceivedOldEquipment.Rows[RowIndex].Cells[1].Text; // Fecha Recepción

                if (!string.IsNullOrEmpty(ultimo) && current.CompareTo(ultimo) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.panel, typeof(Page), "Error"
                        , "alert('La secuencia de carga no es la correcta! Existe un equipo con mayor antigüedad del "
                        + ((DateTime)data.Rows[0]["Dt_Fecha_Recepcion"]).ToString("yyyy-MM-dd") + "');", true);
                    return;
                }

                Image img = grdReceivedOldEquipment.Rows[RowIndex].FindControl("imgFoto") as Image;
                FileUpload btnFldSelect = (FileUpload)grdReceivedOldEquipment.Rows[RowIndex].FindControl("fldSelect");
                string creditId = grdReceivedOldEquipment.DataKeys[RowIndex][0].ToString();
                //add by coco 2012-04-12
                bool NotUploadCount = K_CREDITO_SUSTITUCIONBLL.ClassInstance.IsAllowUploadReceiptionOldEquipment(DisposalCenterID, DisposalCenterType, creditId);
                if (!NotUploadCount)
                {
                    ScriptManager.RegisterStartupScript(this.panel, typeof(Page), "Warning", "alert('Existen equipos pendientes de cargar fotografía');", true);
                    return;
                }
                //end add
                string strFilePath = "\\OldEquipmentReceiptionImage\\" + creditId + "\\";

                //check if folder is exist, if not create it
                if (!Directory.Exists(Server.MapPath(strFilePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(strFilePath));
                }

                //check if file has been uploaded
                if (btnFldSelect.HasFile)
                {
                    string ImageExtend = "BMP、JPG、JPEG、PNG、GIF";
                    string FileExtend = btnFldSelect.FileName.Substring(btnFldSelect.FileName.LastIndexOf(".") + 1).ToUpper();
                    //check upload file format, if it is photograph
                    if (ImageExtend.Contains(FileExtend))
                    {                    
                        string strFileName = btnFldSelect.FileName;
                        //upload file to server                      
                        LsUtility.ScalingAndUploadImage(btnFldSelect.PostedFile.InputStream, Server.MapPath(strFilePath) + strFileName, 800, 800);//edit by coco 2012-03-29
                         //btnFldSelect.SaveAs(Server.MapPath(strFilePath) + strFileName);

                        //update database field to reference to photograph path
                        K_CREDITO_SUSTITUCIONModel instance = new K_CREDITO_SUSTITUCIONModel();
                        instance.Id_Credito_Sustitucion = int.Parse(creditId);
                        instance.Dx_Imagen_Recepcion = strFilePath + strFileName;

                        //updated by tina 2012-07-12
                        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                        {
                            int result = 0;
                            result = K_CREDITO_SUSTITUCIONDAL.ClassInstance.UpLoadImageForReceiveOldEquipment(instance);
                            result += K_CREDITO_SUSTITUCION_EXTENSIONDal.ClassInstance.Insert_K_CREDITO_SUSTITUCION_EXTENSION(int.Parse(creditId));

                            //hide file upload button, and refresh logo to photo exist
                            if (result > 0)
                            {
                                //add by coco 20120224
                                LinkButton linkShowImage = grdReceivedOldEquipment.Rows[RowIndex].FindControl("lkbShowImage") as LinkButton;
                                linkShowImage.Visible = true;
                                //end add
                                img.ImageUrl = "~/DisposalModule/images/Image1.png";
                                ((Button)sender).Visible = false;
                                btnFldSelect.Visible = false;
                            }
                            scope.Complete();
                        }
                        //end                        
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.panel, typeof(Page), "ImageUploadException", "alert('" + ex.Message + "');", true);
            }
        }
        //add by coco 20120224
        protected void lkbShowImage_Click(object sender, EventArgs e)
        {
            LinkButton linkShowImage = (LinkButton)sender;
            GridViewRow gridViewRow = (GridViewRow)linkShowImage.NamingContainer;
            int RowIndex = gridViewRow.RowIndex;

            DataTable dtOldEquipment = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetOldEquipmentInfoByID(grdReceivedOldEquipment.DataKeys[RowIndex][0].ToString());

            string imgPath = string.Empty;
            if (dtOldEquipment != null && dtOldEquipment.Rows.Count > 0)
            {
                imgPath = dtOldEquipment.Rows[0]["Dx_Imagen_Recepcion"].ToString().Replace("\\", "/");
            }
            if (imgPath != string.Empty)
            {
                //edit by coco 2012-03-27
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowImage", "var centerwin = window.open('ShowImage.aspx?ImagePath=" + imgPath + "','','width=800,height=800,top =0,toolbar=no,menubar=no,scrollbars=auto, resizable=no,location=no, status=no');", true);
            }
        }      
        //end add
        #endregion

        #region Controls Changed Events Handler
        /// <summary>
        /// Refresh credit request and technology list
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
        {           
            InitializeCredits(drpCredit.SelectedValue);
            InitializeTechnologies(drpTechnology.SelectedValue);
            InitializeReceivedOldProducts(drpInteralCode.SelectedValue);
        }

        /// <summary>
        /// Refresh old products list
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void drpCredit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeReceivedOldProducts(drpInteralCode.SelectedValue);
        }
        /// <summary>
        /// Supplier changed Refresh credit and Inner Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCredits(drpCredit.SelectedValue);
            InitializeReceivedOldProducts(drpInteralCode.SelectedValue);
        }
        /// <summary>
        /// Technology changed Refresh credit and Inner Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCredits(drpCredit.SelectedValue);
            InitializeReceivedOldProducts(drpInteralCode.SelectedValue);
        }
        #endregion      
    }
}
