using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;
using PAEEEM.Entities;

namespace PAEEEM.DisposalModule
{
    public partial class GeneratorFinalAct : System.Web.UI.Page
    {
        #region Define Global variable
        public string DisposalName
        {
            get
            {
                return ViewState["DisposalName"] == null ? "" : ViewState["DisposalName"].ToString();
            }
            set
            {
                ViewState["DisposalName"] = value;
            }
        }
        #endregion
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
                    DisposalName = CAT_CENTRO_DISP_SUCURSALDAL.ClassInstance.GetDisposalCenterBranchNameByDisposalID(DisposalID);
                }
                else
                {
                    userType = "M";
                    DisposalName = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterNameByDisposalID(DisposalID);
                }
                //Init date control
                this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Session is not null to load default data                
                InitDropDownList(DisposalID, userType);
                LoadGridViewData(DisposalID,userType);
            }
        }
        /// <summary>
        /// Initial Create Date Dropdownlist
        /// </summary>
        /// <param name="DisposalID"></param>
        /// <param name="UserType"></param>
        private void InitDropDownList(int DisposalID, string UserType)
        {
            DataTable dt = K_FOLIODal.ClassInstance.Get_AllFOLIO(DisposalID, UserType);
            if (dt != null)
            {
                drpFrom.DataSource = dt;
                drpFrom.DataTextField = "FOLIO";
                drpFrom.DataValueField = "FOLIO";
                drpFrom.DataBind();
                drpFrom.Items.Insert(0, new ListItem("", ""));

                drpTo.DataSource = dt;
                drpTo.DataTextField = "FOLIO";
                drpTo.DataValueField = "FOLIO";
                drpTo.DataBind();
                drpTo.Items.Insert(0, new ListItem("", ""));
            }
        }
        /// <summary>
        /// Load default data when page load
        /// </summary>
        private void LoadGridViewData(int DisposalID,string UserType)
        {
            int PageCount = 0;          
            DataTable dtPartialAct = null;                    
            dtPartialAct =K_CORTE_PARCIALDAL.ClassInstance.GetPartialCutsForFinialAct(DisposalID, UserType, "","",1, this.AspNetPager.PageSize, out PageCount);
            if (dtPartialAct != null)
            {
                if (dtPartialAct.Rows.Count == 0)
                {
                    dtPartialAct.Rows.Add(dtPartialAct.NewRow());                   
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grvPartialAct.DataSource = dtPartialAct;
                this.grvPartialAct.DataBind();              
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
                this.drpFrom.SelectedIndex = Session["CurrentCreateDate"] != null ? (int)Session["CurrentCreateDate"] : 0;               
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCreateDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGridView();
            Session["CurrentCreateDate"] = this.drpFrom.SelectedIndex;
        }
        /// <summary>
        /// Refresh grid data
        /// </summary>
        private void RefreshDataGridView()
        {
            int PageCount = 0;
            int DisposalID = 0;
            string userType = "";         

            if (Session["UserInfo"] != null)
            {
                DisposalID = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }
            if (userType == GlobalVar.DISPOSAL_CENTER)
            {
                userType = "M";
            }
            else
            {
                userType = "B";
            }
           string FromPeso ="";
           string ToPeso = "";
           if (this.drpFrom.SelectedIndex != 0 && drpFrom.SelectedIndex != -1)
           {
               FromPeso = drpFrom.SelectedValue.ToString();
           }
           if (this.drpTo.SelectedIndex != 0 && drpTo.SelectedIndex != -1)
           {
               ToPeso = drpTo.SelectedValue.ToString();
           }
           DataTable dtPartialAct = null;
           dtPartialAct = K_CORTE_PARCIALDAL.ClassInstance.GetPartialCutsForFinialAct(DisposalID, userType,"","", this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);

           if (dtPartialAct != null)
            {
                if (dtPartialAct.Rows.Count == 0)
                {
                    dtPartialAct.Rows.Add(dtPartialAct.NewRow());                 
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grvPartialAct.DataSource = dtPartialAct;
                this.grvPartialAct.DataBind();              
            }
        }
        /// <summary>
        /// Get Disposal Center Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvPartialAct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grvPartialAct.Rows[e.Row.RowIndex].Cells[2].Text = DisposalName;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGeneratorAct_Click(object sender, EventArgs e)
        {
            int DisposalID = 0;
            string userType = "";

            if (Session["UserInfo"] != null)
            {
                DisposalID = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }
            if (userType == GlobalVar.DISPOSAL_CENTER)
            {
                userType = "M";
            }
            else
            {
                userType = "B";
            }       
            //Count Product Numbers
            string ProductNum = "0";
            DataTable dtPartial = K_CORTE_PARCIALDAL.ClassInstance.Get_PartialAct_ForFinial(DisposalID, userType);
            if (dtPartial != null)
            {
                for (int i = 0; i < dtPartial.Rows.Count; i++)
                { 
                
                }
            }
            K_ACTA_CIRCUNSTANCIADAEntity FinialActEntity = new K_ACTA_CIRCUNSTANCIADAEntity();
            FinialActEntity.Codigo = DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + Convert.ToInt32(LsUtility.GetNumberSequence("FINIALACT")).ToString().PadLeft(4, '0');
            FinialActEntity.Dt_Fecha_Creacion = DateTime.Now;
            FinialActEntity.Fg_Tipo_Centro_Disp = userType;
            FinialActEntity.ID_Centro_Disp = DisposalID;
            FinialActEntity.Num_Productos = "100";

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DisposedProductsMonitor.aspx");
        }
    }
}
