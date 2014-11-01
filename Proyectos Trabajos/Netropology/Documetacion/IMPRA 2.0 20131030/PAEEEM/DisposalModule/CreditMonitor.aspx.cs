using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DisposalModule
{
    public partial class CreditMonitor : System.Web.UI.Page
    {
        #region Page Laod
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
                //get Current User DisposalID and UserType
                int DisposalID = 0;
                string userType = "";
                DisposalID = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
                if (userType == GlobalVar.DISPOSAL_CENTER_BRANCH)
                {
                    userType = "B";
                }
                else
                {
                    userType = "M";
                }
                //Init date control
                this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Session is not null to load default data                
                InitDropDownList(DisposalID,userType);
                LoadGridViewData();
            }
        }
        /// <summary>
        /// Init drop down list data
        /// </summary>
        private void InitDropDownList(int DisposalID,string UserType)
        {
            InitDistributor(DisposalID,UserType);
            InitTechnology(DisposalID, UserType);
        }
        /// <summary>
        /// Load Distributor
        /// </summary>
        private void InitDistributor(int DisposalID, string UserType)
        {
            DataTable dtDistributor = CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORbyDisposalID(DisposalID, UserType);
            if (dtDistributor != null && dtDistributor.Rows.Count > 0)
            {
                this.drpDistributor.DataSource = dtDistributor;
                this.drpDistributor.DataTextField = "Dx_Razon_Social";
                this.drpDistributor.DataValueField = "Id_Proveedor";                       
                this.drpDistributor.DataBind();
                this.drpDistributor.Items.Insert(0, new ListItem("", "-1"));
                //for (int i = 0; i < dtDistributor.Rows.Count; i++)
                //{
                //    drpDistributor.Items[i + 1].Attributes.Add("Title", dtDistributor.Rows[i]["Id_Proveedor"].ToString());
                //}
            }          
        }
        /// <summary>
        /// Load technology
        /// </summary>
        private void InitTechnology(int DisposalID, string UserType)
        {
            DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgramAndDisposalID(Global.PROGRAM.ToString(),DisposalID,UserType);
            if (dtTechnology != null)
            {
                this.drpTechnology.DataSource = dtTechnology;
                this.drpTechnology.DataTextField = "Dx_Nombre_Particular";
                this.drpTechnology.DataValueField = "Cve_Tecnologia";
                this.drpTechnology.DataBind();
                this.drpTechnology.Items.Insert(0, new ListItem("", "-1"));
            }
        }
        /// <summary>
        /// Load default data when page load
        /// </summary>
        private void LoadGridViewData()
        {
            int PageCount = 0;
            int DisposalID = 0;
            string userType = "";
            DataTable dtCredits = null;
            //Get Current User
            if (Session["UserInfo"] != null)
            {
                DisposalID = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }
            bool Flag = false;
            dtCredits = K_CREDITODal.ClassInstance.Get_CreditRequestForDisposal(DisposalID, userType,-1,"",-1, string.Empty, 1, this.AspNetPager.PageSize, out PageCount);
            if (dtCredits != null)
            {
                if (dtCredits.Rows.Count == 0)
                {
                    dtCredits.Rows.Add(dtCredits.NewRow());
                    Flag = true;
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grdCredit.DataSource = dtCredits;
                this.grdCredit.DataBind();
                //hidden radiobutton
                if (Flag)
                {
                    RadioButton Select = grdCredit.Rows[0].FindControl("RadioButton1") as RadioButton;
                    if (Select != null)
                    {
                        Select.Visible = false;
                    }                  
                }                   
            }
        }
        #endregion

        /// <summary>
        /// Provider selected Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGridView();
            Session["CurrentDistributor"] = this.drpDistributor.SelectedIndex;
        }
        /// <summary>
        /// Technology selected Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGridView();
            Session["CurrentTechnology"] = this.drpTechnology.SelectedIndex;
        }
        /// <summary>
        /// Refresh grid data
        /// </summary>
        private void RefreshDataGridView()
        {
            int PageCount = 0;
            int DisposalID = 0;
            string userType = "";
            DataTable dtCredits = null;

            if (Session["UserInfo"] != null)
            {
                DisposalID = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }
            int ProviderID = -1;
            if (this.drpDistributor.SelectedIndex != 0 && this.drpDistributor.SelectedIndex != -1)
            {
                string[] Provide = drpDistributor.SelectedValue.Split('-');
                if (Provide.Length > 0)
                {
                    ProviderID = (Provide[0] == "" )? 0 : Convert.ToInt32(Provide[0]);
                }
            }           
            int Technology = (this.drpTechnology.SelectedIndex == 0 || this.drpTechnology.SelectedIndex == -1) ?-1 : Convert.ToInt32(this.drpTechnology.SelectedValue);
            string ProviderType = "";
            if (ProviderID > 0)
            {
                if (this.drpDistributor.SelectedItem.Text.IndexOf("(BRANCH)") > 0)
                {
                    ProviderType = GlobalVar.SUPPLIER_BRANCH;
                }
                else
                {
                    ProviderType = GlobalVar.SUPPLIER;
                }
            }

            dtCredits = K_CREDITODal.ClassInstance.Get_CreditRequestForDisposal(DisposalID, userType,ProviderID, ProviderType,Technology, string.Empty, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);
            bool Flag = false;
            if (dtCredits != null)
            {
                if (dtCredits.Rows.Count == 0)
                {
                    dtCredits.Rows.Add(dtCredits.NewRow());
                    Flag = true;
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grdCredit.DataSource = dtCredits;
                this.grdCredit.DataBind();
                //hidden radiobutton
                if (Flag)
                {
                    RadioButton Select = grdCredit.Rows[0].FindControl("RadioButton1") as RadioButton;
                    if (Select != null)
                    {
                        Select.Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// Refresh Data When Pager Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                RefreshDataGridView();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanging(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.drpDistributor.SelectedIndex = Session["CurrentDistributor"] != null ? (int)Session["CurrentDistributor"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrentTechnology"] != null ? (int)Session["CurrentTechnology"] : 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnScanCredit_Click(object sender, EventArgs e)
        {
            bool blIsSelect = false;
            string BarCode = "";
            for (int i = 0; i < grdCredit.Rows.Count; i++)
            {
                blIsSelect = ((RadioButton)grdCredit.Rows[i].FindControl("RadioButton1")).Checked;
                if (blIsSelect)
                {
                    BarCode = grdCredit.Rows[i].Cells[0].Text;
                    break;
                }
            }
            DataTable dtCredits = K_CREDITODal.ClassInstance.GetCreditsBarCodeByNoCredit(BarCode);
            if (dtCredits != null && dtCredits.Rows.Count > 0)
            {
                BarCode = dtCredits.Rows[0]["Barcode_Solicitud"].ToString();
            }
            else
            {
                BarCode = "";
            }
            if (!blIsSelect)
            {
                Response.Redirect("CreditRequestScaned.aspx");
            }
            else
            {
                Response.Redirect("ProductListForDisposal.aspx?BarCode=" + Convert.ToBase64String(Encoding.Default.GetBytes(BarCode)).Replace("+", "%2B"));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCredit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rb = (RadioButton)e.Row.FindControl("RadioButton1");
                if (rb != null)
                {
                    rb.Attributes.Add("onclick", "judge(this)");
                }
            }
        }
    }
}
