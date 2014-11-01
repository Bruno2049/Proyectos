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

namespace PAEEEM.RegionalModule
{
    public partial class SupervisionDateRegistry : System.Web.UI.Page
    {
        #region Define Global variable
        public int UserID
        {
            get
            {
                return ViewState["UserID"] == null ? 0 : int.Parse(ViewState["UserID"].ToString());
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }
        public string UserType
        {
            get
            {
                return ViewState["UserType"] == null ? "" : ViewState["UserType"].ToString();
            }
            set
            {
                ViewState["UserType"] = value;
            }
        }
        public string userType
        {
            get
            {
                return ViewState["userType"] == null ? "" : ViewState["userType"].ToString();
            }
            set
            {
                ViewState["userType"] = value;
            }
        }
        #endregion

        #region  Initialize Components
        /// <summary>
        /// Init default filter conditions when page first loaded
        /// </summary>
        /// <param name="sender">Event Raise Target Object</param>
        /// <param name="e">Event Argument</param>
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
                //Add by coco 2011-11-24            
                InitializeProperty();
                //end add
                //Initialize selection options    
                InitializePrograms();
                InitializeTechnologies();                
                InitializeDateControls();
                //Initialize CAyD dropdownlist
                InitializeDrpCAyD();
                //hide the bottom section
                AspNetPager.Visible = false;
                this.Registry.Visible = false;
            }
        }

        private void InitializeProperty()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                UserID = UserModel.Id_Departamento;
                UserType = UserModel.Tipo_Usuario;
                switch (UserType)
                {
                    case GlobalVar.DISPOSAL_CENTER:
                        userType = "M";
                        break;
                    case GlobalVar.DISPOSAL_CENTER_BRANCH:
                        userType = "B";
                        break;
                    case GlobalVar.REGIONAL_OFFICE:
                        userType = "R";
                        break;
                    case GlobalVar.ZONE_OFFICE:
                        userType = "Z";
                        break;
                    default: userType = "";
                        break;
                }
            }
        }
        /// <summary>
        /// //set default date for date time controls
        /// </summary>
        private void InitializeDateControls()
        {
            //Initial date: the first old product reception date, or last old product reception date of last final act
            string fromDate = K_ACTA_RECUPERACIONDal.ClassInstance.GetFromDateForFinalActOrSupervision(drpTechnology.SelectedValue);
            txtFromDate.Text = fromDate != "" ? fromDate : DateTime.Now.ToString("yyyy-MM-dd");

            //Final date: current date minus 1 day
            txtToDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtInSpect.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// Initial Program
        /// </summary>
        private void InitializePrograms()
        {
            DataTable programs = CAT_PROGRAMADal.ClassInstance.GetPrograms();

            if (programs != null)
            {
                drpProgram.DataSource = programs;
                drpProgram.DataTextField = "Dx_Nombre_Programa";
                drpProgram.DataValueField = "ID_Prog_Proy";
                drpProgram.DataBind();
            }
        }
        /// <summary>
        /// Load technology dropdownlist
        /// </summary>
        private void InitializeTechnologies()
        {
            string program = drpProgram.SelectedIndex == -1 ? "" : drpProgram.SelectedValue;
            //edit by coco 2011-11-24
            DataTable technologies = null;
            if (UserType == GlobalVar.DISPOSAL_CENTER || UserType == GlobalVar.DISPOSAL_CENTER_BRANCH)
            {
                technologies = CAT_TECNOLOGIADAL.ClassInstance.GetTechnologyWithProgramandDisposalCenter(program, UserID, userType);
            }
            else
            {
                technologies = CAT_TECNOLOGIADAL.ClassInstance.GetTechnologyWithProgramandDisposalCenter(program, 0, "");
            }
            //end edit
            if (technologies != null)
            {
                drpTechnology.DataSource = technologies;
                drpTechnology.DataTextField = "Dx_Nombre_General";
                drpTechnology.DataValueField = "Cve_Tecnologia";
                drpTechnology.DataBind();
            }
        }

        /// <summary>
        /// Initialize Status dropdownlist
        /// </summary>
        private void InitializeDrpCAyD()
        {
            int count;
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            DataTable dtCAyD = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranchWithZoneAndStatus(0, UserModel.Id_Departamento.ToString(), string.Empty, 0, "Dx_Nombre_Comercial", 1, 99999, out count);


            foreach (DataRow dr in dtCAyD.Rows)
            {
                dr["Dx_Nombre_Comercial"] = dr["Dx_Nombre_Comercial"].ToString() + "-(" + dr["Tipo_Centro_Disp"].ToString() + ")";
            }

            if (dtCAyD != null && dtCAyD.Rows.Count > 0)
            {
                drpCAyD.DataSource = dtCAyD;
                drpCAyD.DataValueField = "Id_Centro_Disp";
                drpCAyD.DataTextField = "Dx_Nombre_Comercial";
                drpCAyD.DataBind();
            }
            drpCAyD.Items.Insert(0, new ListItem("", ""));
            drpCAyD.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// Search data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Cache filter conditions for later use
            CacheFilterConditions();
            //refresh grid view data
            RefreshGridViewData();
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Save the selected filter conditions
        /// </summary>
        private void CacheFilterConditions()
        {
            Session["SupervisionCurrentProgram"] = drpProgram.SelectedIndex;
            Session["SupervisionCurrentTechnology"] = drpTechnology.SelectedIndex;
            Session["SupervisionCurrentFromDate"] = txtFromDate.Text.Trim();
            Session["SupervisionCurrentToDate"] = txtToDate.Text.Trim();
        }
        /// <summary>
        /// Initial Gridview
        /// </summary>
        private void RefreshGridViewData()
        {            
            int PageCount = 0;
            DataTable oldReceivedProducts = null;
            string program = (this.drpProgram.SelectedIndex == -1) ? "" : this.drpProgram.SelectedValue;
            string technology = (this.drpTechnology.SelectedIndex == -1) ? "" : this.drpTechnology.SelectedValue;
            string CAyD = (this.drpCAyD.SelectedIndex == 0 || this.drpCAyD.SelectedIndex == -1) ? "" : this.drpCAyD.SelectedValue;
            string CAyDDesc = (this.drpCAyD.SelectedIndex == 0 || this.drpCAyD.SelectedIndex == -1) ? "" : this.drpCAyD.SelectedItem.Text;
            string fromDate = this.txtFromDate.Text.Trim();
            string toDate = this.txtToDate.Text.Trim();
            string TipoOfDisposal = string.Empty;

            if (!string.IsNullOrEmpty(CAyD))
            {
                string[] partes = CAyDDesc.Split('-');
                if (partes.Length > 1)
                {
                    TipoOfDisposal = partes[1] == "(Matriz)" ? "M" : "B";
                }
            }
            
            //edit by coco 2011-11-24
            if (UserType == GlobalVar.DISPOSAL_CENTER_BRANCH || UserType == GlobalVar.DISPOSAL_CENTER)
            {
                oldReceivedProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetRecoveryProductsForSupervisionView(program, technology, fromDate, toDate, UserID, userType,                                                     
                    this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
            }
            else
            {
                oldReceivedProducts = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetRecoveryProductsForSupervision(program, technology, fromDate, toDate, UserID, userType,                                                                        
                    CAyD, TipoOfDisposal, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
            }
            //end edit
            bool emptRow = false;
            if (oldReceivedProducts != null && oldReceivedProducts.Rows.Count == 0)
            {
                oldReceivedProducts.Rows.Add(oldReceivedProducts.NewRow());
                emptRow = true;
            }

            //Bind to grid view
            this.AspNetPager.RecordCount = PageCount;
            this.grdReceivedOldEquipment.DataSource = oldReceivedProducts;
            this.grdReceivedOldEquipment.DataBind();

            // updated by tina 2012-03-13
            this.AspNetPager.Visible = true;
            if (UserType == GlobalVar.REGIONAL_OFFICE || UserType == GlobalVar.ZONE_OFFICE)
            {
                //if row is empty, hide the checkbox
                if (emptRow)
                {
                    ((CheckBox)grdReceivedOldEquipment.Rows[0].FindControl("ckbSelect")).Visible = false;
                    this.Registry.Visible = false;
                    this.txtInSpect.Visible = false;
                }
                else
                {
                    this.Registry.Visible = true;
                    this.txtInSpect.Visible = true;
                }
            }
            else
            {
                this.grdReceivedOldEquipment.Columns[14].Visible = false;
                this.Registry.Visible = false;
                this.txtInSpect.Visible = false;
            }
        }
       
        /// <summary>
        /// return main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../default.aspx");
        }
        /// <summary>
        /// Registry button action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegistry_Click(object sender, EventArgs e)
        {
            try
            {
                RememberOldSelectedProducts();
                string creditString = "";

                if (Session["ProductList"] != null)
                {
                    ArrayList list = (ArrayList)Session["ProductList"];
                    for (int i = 0; i < list.Count; i++)
                    {
                        creditString += list[i].ToString() + ",";
                    }

                    char[] paras = new char[] { ',' };
                    creditString = creditString.TrimEnd(paras);
                }

                int Result = K_RECUPERACION_PRODUCTODal.ClassInstance.UpdateK_Recuperacion_Product(creditString, txtInSpect.Text.Trim());

                if (Result > 0)
                {
                    RefreshGridViewData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "RegistryDate", "alert(' Registry date fail:" + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                RefreshGridViewData();
                //edit by coco 2011-11-24
                if (UserType == GlobalVar.REGIONAL_OFFICE || UserType == GlobalVar.ZONE_OFFICE)
                {
                    RePopulateSelectedProducts();
                }
               //end edit
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            if (IsPostBack)
            {
                //edit by coco 2011-11-24
                if (UserType == GlobalVar.REGIONAL_OFFICE || UserType == GlobalVar.ZONE_OFFICE)
                {
                    //Cache selected records
                    RememberOldSelectedProducts();
                }
                //end edit
                //Cache filter conditions
                this.drpProgram.SelectedIndex = Session["SupervisionCurrentProgram"] != null ? (int)Session["SupervisionCurrentProgram"] : 0;
                this.drpTechnology.SelectedIndex = Session["SupervisionCurrentTechnology"] != null ? (int)Session["SupervisionCurrentTechnology"] : 0;
                this.txtFromDate.Text = Session["SupervisionCurrentFromDate"] != null ? (string)Session["SupervisionCurrentFromDate"] : DateTime.Now.ToString("yyyy-MM-dd");
                this.txtToDate.Text = Session["SupervisionCurrentToDate"] != null ? (string)Session["SupervisionCurrentToDate"] : DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// Program dropdownlist changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeTechnologies();
        }
        /// <summary>
        /// repopulate checkbox status
        /// </summary>
        private void RePopulateSelectedProducts()
        {
            ArrayList listProducts = null;

            if (Session["ProductList"] != null)
            {
                listProducts = (ArrayList)Session["ProductList"];
            }

            if (listProducts != null)
            {
                for (int i = 0; i < grdReceivedOldEquipment.Rows.Count; i++)
                {
                    string creditSusId = grdReceivedOldEquipment.DataKeys[i][0].ToString();

                    if (listProducts.Contains(creditSusId))
                    {
                        ((CheckBox)grdReceivedOldEquipment.Rows[i].FindControl("ckbSelect")).Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// remember the selected products
        /// </summary>
        private void RememberOldSelectedProducts()
        {
            ArrayList arrListCredits = new ArrayList();

            if (Session["ProductList"] != null)
            {
                arrListCredits = (ArrayList)Session["ProductList"];
            }

            for (int i = 0; i < grdReceivedOldEquipment.Rows.Count; i++)
            {
                CheckBox ckb = grdReceivedOldEquipment.Rows[i].FindControl("ckbSelect") as CheckBox;

                if (ckb.Checked)
                {
                    if (!arrListCredits.Contains(grdReceivedOldEquipment.DataKeys[i][0].ToString()))
                    {
                        arrListCredits.Add(grdReceivedOldEquipment.DataKeys[i][0].ToString());
                    }
                }
                else
                {
                    if (arrListCredits.Contains(grdReceivedOldEquipment.DataKeys[i][0].ToString()))
                    {
                        arrListCredits.Remove(grdReceivedOldEquipment.DataKeys[i][0].ToString());
                    }
                }
            }

            if (arrListCredits != null && arrListCredits.Count > 0)
            {
                Session["ProductList"] = arrListCredits;
            }
        }
        /// <summary>
        /// Refresh from and to date when technology changed
        /// </summary>
        /// <param name="sender">Target Object</param>
        /// <param name="e">Event Argument</param>
        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            //refresh the initial date when technology changed
            string fromDate = K_ACTA_RECUPERACIONDal.ClassInstance.GetFromDateForFinalActOrSupervision(drpTechnology.SelectedValue);

            txtFromDate.Text = fromDate != "" ? fromDate : DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Status change refresh datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCAyD_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}

