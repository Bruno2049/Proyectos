using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using System.Transactions;

namespace PAEEEM
{
    public partial class CreditMonitor : System.Web.UI.Page
    {
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
                this.literalFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Session is not null to load default data                
                InitDropDownList();
                LoadGridViewData();
                Session["CurrentStatusCreditMonitor"] = 0;
                Session["CurrentRazonCreditMonitor"] = 0;
                Session["CurrentDateCreditMonitor"] = 0;
                Session["CurrenttechnologyCreditMonitor"] = 0;    
            }
        }
        /// <summary>
        /// Init drop down list data
        /// </summary>
        private void InitDropDownList()
        {
            InitStatus();
            InitTechnology();
            InitRazonSocial();
            InitPendienteDate();
        }
        /// <summary>
        /// Load status options
        /// </summary>
        private void InitStatus()
        {
            DataTable dtCreditStatus = CatEstatusDal.ClassInstance.GetCreditEstatus();
            if (dtCreditStatus != null)
            {
                this.drpEstatus.DataSource = dtCreditStatus;
                this.drpEstatus.DataTextField = "Dx_Estatus_Credito";
                this.drpEstatus.DataValueField = "Cve_Estatus_Credito";
                this.drpEstatus.DataBind();
                this.drpEstatus.Items.Insert(0, new ListItem("", "-1"));
            }
        }
        /// <summary>
        /// Load technology
        /// </summary>
        private void InitTechnology()
        {
            DataTable dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgram(Global.PROGRAM.ToString());//Changed by Jerry 2011-08-08
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
        /// Load credit Pending Date and P. Física o razón social
        /// </summary>
        private void InitRazonSocial()
        {
            int proveedorId = 0;
            string userType = "";
            if (Session["UserInfo"] != null)
            {
                proveedorId = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }
            DataTable dtRazonSocial = K_CREDITODal.ClassInstance.GetRazonSocial(proveedorId, userType);
            if (dtRazonSocial != null)
            {
                this.drpRazonSocial.DataSource = dtRazonSocial;
                this.drpRazonSocial.DataTextField = "Dx_Razon_Social";
                this.drpRazonSocial.DataBind();
                this.drpRazonSocial.Items.Insert(0, new ListItem(""));
            }
        }
        /// <summary>
        /// Load credit pendiente date
        /// </summary>
        private void InitPendienteDate()
        {
            int proveedorId = 0;
            string userType = "";
            if (Session["UserInfo"] != null)
            {
                proveedorId = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }
            DataTable dtCreditPendienteDate = K_CREDITODal.ClassInstance.GetPendienteDate(proveedorId, userType);
            if (dtCreditPendienteDate != null)
            {
                this.drpFechaDate.DataSource = dtCreditPendienteDate;
                this.drpFechaDate.DataTextField = "Dt_Fecha_Pendiente";                
                this.drpFechaDate.DataBind();
                this.drpFechaDate.Items.Insert(0, new ListItem(""));
            }
        }
        /// <summary>
        /// Load default data when page load
        /// </summary>
        private void LoadGridViewData()
        { 
            int PageCount=0;
            int proveedorId = 0;
            string userType = "";
            DataTable dtCredits = null;            

            if (Session["UserInfo"] != null)
            {
                proveedorId = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }

            dtCredits = K_CREDITODal.ClassInstance.GetCredits(proveedorId, userType, string.Empty, 1, this.AspNetPager.PageSize, out PageCount);
            if (dtCredits != null)
            {
                if (dtCredits.Rows.Count == 0)
                {
                    dtCredits.Rows.Add(dtCredits.NewRow());
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grdCredit.DataSource = dtCredits;
                this.grdCredit.DataBind();
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
                RefreshGridData();
            }
        }

        protected void AspNetPager_PageChanging(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.drpEstatus.SelectedIndex = Session["CurrentStatusCreditMonitor"] != null ? (int)Session["CurrentStatusCreditMonitor"] : 0;
                this.drpRazonSocial.SelectedIndex = Session["CurrentRazonCreditMonitor"] != null ? (int)Session["CurrentRazonCreditMonitor"] : 0;
                this.drpFechaDate.SelectedIndex = Session["CurrentDateCreditMonitor"] != null ? (int)Session["CurrentDateCreditMonitor"] : 0;
                this.drpTechnology.SelectedIndex = Session["CurrenttechnologyCreditMonitor"] != null ? (int)Session["CurrenttechnologyCreditMonitor"] : 0;
            }                   
        }
        /// <summary>
        /// Refresh grid data
        /// </summary>
        private void RefreshGridData()
        {
            int PageCount = 0;
            int proveedorId = 0;
            string userType = "";
            DataTable dtCredits = null;

            if (Session["UserInfo"] != null)
            {
                proveedorId = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                userType = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
            }

            string PendingDate = (this.drpFechaDate.SelectedIndex == 0 || this.drpFechaDate.SelectedIndex == -1)? "" : this.drpFechaDate.SelectedItem.Text;
            string RazonSocial = (this.drpRazonSocial.SelectedIndex == 0 || this.drpRazonSocial.SelectedIndex == -1)? "" : this.drpRazonSocial.SelectedItem.Text;
            int Status = (this.drpEstatus.SelectedIndex == 0  ||  this.drpEstatus.SelectedIndex == -1)? -1 : Convert.ToInt32(this.drpEstatus.SelectedValue);
            int Technology = (this.drpTechnology.SelectedIndex == 0 || this.drpTechnology.SelectedIndex == -1) ? -1 : Convert.ToInt32(this.drpTechnology.SelectedValue);
            
            dtCredits = K_CREDITODal.ClassInstance.GetCredits(proveedorId, userType, PendingDate, Status, RazonSocial, Technology, string.Empty, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out PageCount);

            if (dtCredits != null)
            {
                if (dtCredits.Rows.Count == 0)
                {
                    dtCredits.Rows.Add(dtCredits.NewRow());
                }
                //Bind to grid view
                this.AspNetPager.RecordCount = PageCount;
                this.grdCredit.DataSource = dtCredits;
                this.grdCredit.DataBind();
            }
        }
        /// <summary>
        /// Do the action when command button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Validate", StringComparison.OrdinalIgnoreCase))
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument);
                string CreditNumber = this.grdCredit.DataKeys[RowIndex].Value.ToString();
                Response.Redirect("ValidateCrediticialHistroy.aspx?CreditNumber=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(CreditNumber)).Replace("+", "%2B"));
            }
        }
        /// <summary>
        /// Pass Row Index to CommandArgument
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkValidate = (LinkButton)e.Row.FindControl("linkValidate");
                if(linkValidate != null)
                {
                    linkValidate.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }
        /// <summary>
        /// Refresh grid data when fecha date changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpFechaDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
            //
            Session["CurrentDateCreditMonitor"] = this.drpFechaDate.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Refresh grid data when status changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
            //
            Session["CurrentStatusCreditMonitor"] = this.drpEstatus.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Refresh grid data when razón social changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpRazonSocial_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
            //
            Session["CurrentRazonCreditMonitor"] = this.drpRazonSocial.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Refresh grid data when technology changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
            //
            Session["CurrenttechnologyCreditMonitor"] = this.drpTechnology.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Hide validation button when empty line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in this.grdCredit.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow && (this.grdCredit.DataKeys[0].Value.ToString() == "" || 
                    !row.Cells[6].Text.Equals("PENDIENTE", StringComparison.OrdinalIgnoreCase) && !row.Cells[6].Text.Equals("POR ENTREGAR", StringComparison.OrdinalIgnoreCase)))
                {
                    row.FindControl("linkValidate").Visible = false;
                }
                // RSA 20130813 If user Distribuidor and credit Pendiente without MOP then enable edition button
                // and add warning about loosing edition capabilities to linkValidate
                else if (row.Cells[6].Text.Equals("PENDIENTE", StringComparison.OrdinalIgnoreCase)
                    && string.IsNullOrEmpty(((HiddenField)row.FindControl("hdMOP")).Value)
                    && ((US_USUARIOModel)Session["UserInfo"]).Id_Rol == 3)
                {
                    ((LinkButton)row.FindControl("linkValidate")).OnClientClick
                        = "return confirm('Confirmar Realizar Validación e Integración del Expediente\\n\\nNO podrá editar el crédito después de hacer la consulta crediticia');";
                    row.FindControl("btnEdit").Visible = true;
                }
                // END
            }
        }
        /// <summary>
        /// Add new credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddCredit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Captcha/valida.aspx");
        }

        /// <summary>
        /// Edit an existing credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button btnEdit = (Button)sender;
            string credit = btnEdit.CommandArgument;

            Response.Redirect("CreditRequest.aspx?Token=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(credit)));
        }
        /// <summary>
        /// Cancel selected credits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelCredit_Click(object sender, EventArgs e)
        {
            string CreditNumber;
            int iCount = 0, iSucess = 0;
            string strMsg = "";
            bool bIsSelected = false;

            #region Check If Have Records Be Seleted

            for (int i = 0; i < grdCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)grdCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    iCount++;
                }
            }

            if (iCount == 0)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPleaseSelectOne") as string;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Cancel The Selected Credits

            for (int i = 0; i < grdCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)grdCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (bIsSelected)
                {
                    CreditNumber = grdCredit.DataKeys[i].Value.ToString();

                    int iResult = 0;
                    if (this.grdCredit.Rows[i].Cells[6].Text.ToUpper() == "POR ENTREGAR"
                        || this.grdCredit.Rows[i].Cells[6].Text.ToUpper() == "PENDIENTE")
                    {
                        decimal iRequestAmount = 0;
                        iRequestAmount = this.grdCredit.Rows[i].Cells[2].Text == "" ? 0 : decimal.Parse(this.grdCredit.Rows[i].Cells[5].Text.Substring(1));
                        using (TransactionScope scope = new TransactionScope())
                        {
                            iResult = K_CREDITODal.ClassInstance.CancelCredit(CreditNumber, Session["UserName"].ToString());
                            iResult += CAT_PROGRAMADal.ClassInstance.IncreaseCurrentAmount(Global.PROGRAM, iRequestAmount);
                            scope.Complete();
                        }
                        // End

                        if (iResult > 0)
                        {
                            iSucess++;
                        }
                    }
                }
            }
            //if all had been updated successfully
            if (iCount > 0 && iCount == iSucess)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSSuccessToCancel") as string;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CancelSuccess", "alert('" + strMsg + "');", true);
            }
            else
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToCancel") as string;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CancelFailed", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            LoadGridViewData();
        }
    }
}
