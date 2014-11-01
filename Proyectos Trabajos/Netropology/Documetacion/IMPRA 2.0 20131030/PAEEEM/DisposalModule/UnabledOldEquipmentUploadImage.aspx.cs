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
    public partial class UnabledOldEquipmentUploadImage : System.Web.UI.Page
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

        // added by tina 2012/04/12
        public string SearchType
        {
            get
            {
                return ViewState["SearchType"] == null ? "" : ViewState["SearchType"].ToString();
            }
            set
            {
                ViewState["SearchType"] = value;
            }
        }
        // end
        #endregion

        #region  Initialize Data When Page Loaded
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
                    InitPrograms();
                    //Init credit request drop down list
                    //Init suppliers
                    InitSuppliers(DEFAULT_SELECTED_ITEM);
                    //Init technology
                    InitTechnologies(DEFAULT_SELECTED_ITEM);
                    InitCredits(DEFAULT_SELECTED_ITEM);
                    //Init old products list
                    InitOldProducts(DEFAULT_SELECTED_ITEM);                    
                    //Hidden aspnetPager
                    AspNetPager.Visible = false;
                    //set default date for date time controls
                    txtFromDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    //set default inhabilication date
                    txtInFromDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtInToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
        private void InitPrograms()
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
        private void InitCredits(string selectedItem)
        {
            DataTable credits = null;
            FilterCondition FilterValue = new FilterCondition();
            FilterValue=GetFilterValue();
            //retrieve the program and disposal center related credit requests           
            credits = K_CREDITODal.ClassInstance.GetCreditByProgramAndDisposalAndSupplierAndTechnology(FilterValue.Program, DisposalCenterID,
                                                                                                            DisposalCenterType, FilterValue.Supplier, FilterValue.SupplierType, FilterValue.Technology);
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

        private FilterCondition GetFilterValue()
        {
            FilterCondition FilterValue = new FilterCondition();
            FilterValue.Program = (drpProgram.SelectedIndex == -1 || drpProgram.SelectedIndex == 0) ? "" : drpProgram.SelectedValue;
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
            FilterValue.Technology = (this.drpTechnology.SelectedIndex == -1 || drpTechnology.SelectedIndex == 0) ? "" : drpTechnology.SelectedValue;
            return FilterValue;
        }
        /// <summary>
        /// Init old equipment internal Code list
        /// </summary>
        private void InitOldProducts(string selectedItem)
        {
            DataTable oldProducts = null;         
            //Get partial Filter value
            FilterCondition FilterValue = new FilterCondition();
            FilterValue=GetFilterValue();
            string credit = (drpCredit.SelectedIndex == -1 || drpCredit.SelectedIndex == 0) ? "" : drpCredit.SelectedValue;

            //retrieve the related old products to specific credit request         
            oldProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetReceivedOldProductsInternalCodeByFilter(credit, FilterValue.Supplier, FilterValue.SupplierType,
                                                                                    FilterValue.Technology, FilterValue.Program, DisposalCenterID, DisposalCenterType);
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
        private void InitSuppliers(string selectedItem)
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
        private void InitTechnologies(string selectedItem)
        {
            DataTable technologies = null;
            string program = (drpProgram.SelectedIndex == -1 || drpProgram.SelectedIndex == 0) ? "" : drpProgram.SelectedValue;
            //get technology with or without program
            if (program != "")
            {
                technologies = CAT_TECNOLOGIADAL.ClassInstance.GetTechnologyWithProgramandDisposalCenter(program, int.Parse(DisposalCenterID), DisposalCenterType);
            }
            else
            {
                technologies = CAT_TECNOLOGIADAL.ClassInstance.GetDisposalCenterRelatedTechnology(int.Parse(DisposalCenterID), DisposalCenterType);
            }
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

        #region Button Action
        /// <summary>
        /// Search the old products list with some conditions
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchType = "Search"; // added by tina 2012/04/12
            CacheFilterConditions();
            RefreshGridViewData();
            AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Cache the selected filter conditions
        /// </summary>
        private void CacheFilterConditions()
        {
            Session["CurrentProgramUnableImage"] = drpProgram.SelectedIndex;
            Session["CurrentCreditUnableImage"] = drpCredit.SelectedIndex;
            Session["CurrentInternalCodeUnableImage"] = drpInteralCode.SelectedIndex;
            Session["CurrentSupplierUnableImage"] = drpDistributor.SelectedIndex;
            Session["CurrentTypeUnableImage"] = drpType.SelectedIndex;
            Session["CurrentTechnologyUnableImage"] = drpTechnology.SelectedIndex;
            Session["CurrentFromDateUnableImage"] = txtFromDate.Text.Trim();
            Session["CurrentToDateUnableImage"] = txtToDate.Text.Trim();
            Session["CurrentInFromDateUnableImage"] = txtInFromDate.Text.Trim();
            Session["CurrentInToDateUnableImage"] = txtInToDate.Text.Trim();
        }
        /// <summary>
        /// Refresh the grid view control
        /// </summary>
        private void RefreshGridViewData() // updated by tina 2012/04/12
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                int PageCount = 0;
                DataTable oldProductsReceived = null;
                //filter conditions
                FilterCondition FilterValue = new FilterCondition();
                //Get partial Filter Value
                FilterValue = GetFilterValue();
                // updated by tina 2012/04/12
                if (SearchType == "Search")
                {
                    string credit = (this.drpCredit.SelectedIndex == 0 || this.drpCredit.SelectedIndex == -1) ? "" : this.drpCredit.SelectedValue;
                    string internalCode = (this.drpInteralCode.SelectedIndex == 0 || this.drpInteralCode.SelectedIndex == -1) ? "" : this.drpInteralCode.SelectedValue.ToString();
                    string type = this.drpType.SelectedValue;
                    string fromDate = this.txtFromDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtFromDate.Text.Trim();
                    string toDate = this.txtToDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtToDate.Text.Trim();
                    string InfromDate = this.txtInFromDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtInFromDate.Text.Trim();
                    string IntoDate = this.txtInToDate.Text.Trim() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : this.txtInToDate.Text.Trim();
                    //retrieve old products with the filter conditions
                    oldProductsReceived = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetInHabilitacionProductsForImage(FilterValue.Program, credit, internalCode.Trim(), FilterValue.Supplier.Trim(), FilterValue.SupplierType, type, FilterValue.Technology, fromDate, toDate,
                                                                                                                                          int.Parse(DisposalCenterID), DisposalCenterType, InfromDate, IntoDate, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
                }
                else
                {
                    oldProductsReceived = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetInHabilitacionProductsForImage(FilterValue.Program, "", "", "", "", "0", FilterValue.Technology, "", "",
                                                                                                                                          int.Parse(DisposalCenterID), DisposalCenterType, "", "", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
                }
                // end
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

                    FileUpload btnSelect = grdReceivedOldEquipment.Rows[0].FindControl("fldSelect") as FileUpload;
                    if (btnSelect != null)
                    {
                        btnSelect.Visible = false;
                    }

                    Button btnUpload = grdReceivedOldEquipment.Rows[0].FindControl("btnUpLaod") as Button;
                    if (btnUpload != null)
                    {
                        btnUpload.Visible = false;
                    }

                    // added by tina 2012-02-24
                    LinkButton linkShowImage = grdReceivedOldEquipment.Rows[0].FindControl("linkShowImage") as LinkButton;
                    if (linkShowImage != null)
                    {
                        linkShowImage.Visible = false;
                    }
                }
                //visible bottom grid view control
                AspNetPager.Visible = true;
            }
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
                // Button btnSelect = grdReceivedOldEquipment.Rows[i].FindControl("btnSelect") as Button;
                FileUpload btnSelect = grdReceivedOldEquipment.Rows[i].FindControl("fldSelect") as FileUpload;
                Button btnUpload = grdReceivedOldEquipment.Rows[i].FindControl("btnUpLaod") as Button;
                //btnUpload.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this.cmdUpload, ""));

                LinkButton linkShowImage = grdReceivedOldEquipment.Rows[i].FindControl("linkShowImage") as LinkButton; // added by tina 2012-02-24

                if (IsUpload == "0")//identify photograph is not exist
                {
                    Img.ImageUrl = "~/DisposalModule/images/Image2.png";
                    linkShowImage.Visible = false; // added by tina 2012-02-24
                }
                else if (IsUpload == "1")
                {
                    Img.ImageUrl = "~/DisposalModule/images/Image1.png";
                    btnSelect.Visible = false;
                    btnUpload.Visible = false;
                    linkShowImage.Visible = true; // added by tina 2012-02-24
                }
            }
        }
        /// <summary>
        /// Return Main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
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
                this.drpProgram.SelectedIndex = Session["CurrentProgramUnableImage"] != null ? (int)Session["CurrentProgramUnableImage"] : 0;
                this.drpCredit.SelectedIndex = Session["CurrentCreditUnableImage"] != null ? (int)Session["CurrentCreditUnableImage"] : 0;
                this.drpInteralCode.SelectedIndex = Session["CurrentInternalCodeUnableImage"] != null ? (int)Session["CurrentInternalCodeUnableImage"] : 0;
                this.drpDistributor.SelectedIndex = Session["CurrentSupplierUnableImage"] != null ? (int)Session["CurrentSupplierUnableImage"] : 0;
                this.drpType.SelectedIndex = Session["CurrentTypeUnableImage"] != null ? (int)Session["CurrentTypeUnableImage"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrentTechnologyUnableImage"] != null ? (int)Session["CurrentTechnologyUnableImage"] : 0;
                this.txtFromDate.Text = Session["CurrentFromDateUnableImage"] != null ? (string)Session["CurrentFromDateUnableImage"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtToDate.Text = Session["CurrentToDateUnableImage"] != null ? (string)Session["CurrentToDateUnableImage"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtInFromDate.Text = Session["CurrentInFromDateUnableImage"] != null ? (string)Session["CurrentInFromDateUnableImage"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtInToDate.Text = Session["CurrentInToDateUnableImage"] != null ? (string)Session["CurrentInToDateUnableImage"] : DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// Upload Image Inhabilitación for Old equipment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpLaod_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnSelect = (Button)sender;
                GridViewRow gridViewRow = (GridViewRow)btnSelect.NamingContainer;
                int RowIndex = gridViewRow.RowIndex;

                DataTable data = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetOldestInHabilitacionProductsWithoutImage(drpProgram.SelectedValue, int.Parse(DisposalCenterID), DisposalCenterType);
                string ultimo = data != null && data.Rows.Count > 0 ? data.Rows[0]["Id_Folio"].ToString() : string.Empty;
                string current = grdReceivedOldEquipment.Rows[RowIndex].Cells[1].Text; // Fecha inhabilitación

                if (!string.IsNullOrEmpty(ultimo) && current.CompareTo(ultimo) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.panel, typeof(Page), "Error"
                        , "alert('La secuencia de carga no es la correcta! Existe un equipo con mayor antigüedad del "
                        + ((DateTime)data.Rows[0]["Dt_Fecha_Inhabilitacion"]).ToString("yyyy-MM-dd") + "');", true);
                    return;
                }

                
                string IDcredit = grdReceivedOldEquipment.DataKeys[RowIndex][0].ToString();
                //add by coco 2012-04-12
                if (!K_CREDITO_SUSTITUCIONDAL.ClassInstance.ReceiptionOldEquipmentIsUploadImage(DisposalCenterID, DisposalCenterType, IDcredit))
                {
                    ScriptManager.RegisterStartupScript(this.panel, typeof(Page), "UploadImage", "alert('La imagen del equipo no ha sido cargada en la recepción');", true);
                    return;
                }
                else if(!K_CREDITO_SUSTITUCIONBLL.ClassInstance.IsAllowUploadUnableOldEquipment(DisposalCenterID,DisposalCenterType,IDcredit))
                {
                    ScriptManager.RegisterStartupScript(this.panel, typeof(Page), "UploadImage", "alert('Han pasao más de tres días desde que el equipo fue recibido.');", true);
                    return;
                }
                //end add     
                Image img = grdReceivedOldEquipment.Rows[RowIndex].FindControl("imgFoto") as Image;
                //Button btn = grdReceivedOldEquipment.Rows[RowIndex].FindControl("btnSelect") as Button;
                FileUpload btnFldSelect = (FileUpload)grdReceivedOldEquipment.Rows[RowIndex].FindControl("fldSelect");
                string strFilePath = "\\OldEquipmentInhabilitacionImage\\" + IDcredit + "\\";

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
                       // btnFldSelect.SaveAs(Server.MapPath(strFilePath) + strFileName);
                        
                        //update database field to reference to photograph path
                        K_CREDITO_SUSTITUCIONModel instance = new K_CREDITO_SUSTITUCIONModel();
                        instance.Id_Credito_Sustitucion = int.Parse(IDcredit);
                        instance.Dx_Imagen_Inhabilitacion = strFilePath + strFileName;

                        int resulut = 0;
                        resulut = K_CREDITO_SUSTITUCIONDAL.ClassInstance.UpLoadImageForInHabilitacionOldEquipment(instance);

                        //hide file upload button, and refresh logo to photo exist
                        if (resulut > 0)
                        {
                            LinkButton linkShowImage = grdReceivedOldEquipment.Rows[RowIndex].FindControl("linkShowImage") as LinkButton; // added by tina 2012-02-24
                            linkShowImage.Visible = true;
                            img.ImageUrl = "~/DisposalModule/images/Image1.png";
                            btnSelect.Visible = false;
                            btnFldSelect.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.panel, typeof(Page), "UploadImage", "alert('" + ex.Message + "');", true);
            }
        }

        // added by tina 2012-02-24
        protected void linkShowImage_Click(object sender, EventArgs e)
        {
            LinkButton linkShowImage = (LinkButton)sender;
            GridViewRow gridViewRow = (GridViewRow)linkShowImage.NamingContainer;
            int RowIndex = gridViewRow.RowIndex;

            DataTable dtOldEquipment = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetOldEquipmentInfoByID(grdReceivedOldEquipment.DataKeys[RowIndex][0].ToString());

            string imgPath = string.Empty;
            if (dtOldEquipment != null && dtOldEquipment.Rows.Count > 0)
            {
                imgPath = dtOldEquipment.Rows[0]["Dx_Imagen_Inhabilitacion"].ToString().Replace("\\","/");
            }
            if (imgPath != string.Empty)
            {
                //edit by coco 2012-03-27 //centerwin.moveTo(screen.width/2-300, screen.height/2-300);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowImage", "var centerwin = window.open('ShowImage.aspx?ImagePath=" + imgPath + "','','width=800,height=800,top=0,toolbar=no,menubar=no,scrollbars=auto, resizable=no,location=no, status=no');", true);
            }
        }
        // end

        // added by tina 2012/04/12
        protected void btnFindAll_Click(object sender, EventArgs e)
        {
            SearchType = "FindAll";
            CacheFilterConditions();
            RefreshGridViewData();
            AspNetPager.GoToPage(1);
        }
        // end
        #endregion

        #region Controls Changed Events Handler
        /// <summary>
        /// Refresh credit request and technology list
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCredits(drpCredit.SelectedValue);
            InitTechnologies(drpTechnology.SelectedValue);
            InitOldProducts(drpInteralCode.SelectedValue);
        }
        /// <summary>
        /// Refresh old products list
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void drpCredit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitOldProducts(drpInteralCode.SelectedValue);
        }
        /// <summary>
        /// Supplier changed Refresh credit and Inner Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCredits(drpCredit.SelectedValue);          
            InitOldProducts(drpInteralCode.SelectedValue);
        }
        /// <summary>
        /// Technology changed Refresh credit and Inner Code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCredits(drpCredit.SelectedValue);
            InitOldProducts(drpInteralCode.SelectedValue);
        }
        #endregion  
    }
}
