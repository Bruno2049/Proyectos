using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using System.Data;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;
using PAEEEM.Captcha;

namespace PAEEEM
{
    public partial class CreditAuthorization : System.Web.UI.Page
    {
        const string DxCveCC_SE = "SE";

        /// <summary>
        /// Init controls when page first load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }
                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                InitButtonsByRole();
                //Init drop down list options
                InitDropDownOptions();                //Changed by Jerry 2011/08/04
                //Bind grid view control
                InitDefaultData();
                //Clear filter conditions
                ClearFilterConditions();
            }
        }
        /// <summary>
        /// Init Buttons by Role
        /// </summary>
        private void InitButtonsByRole()
        {
            // RSA 20130703  (2013 jul 03) Always disable button "Pendiente"
            // int RoleType = 0;
            // if (Session["UserInfo"] != null)
            // {
            //     RoleType = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;

            //     if (RoleType == (int)UserRole.ZONE
            //         || RoleType == (int)UserRole.REGIONAL)
            //     {
                    btnPendiente.Enabled = false;
            //     }
            // }
        }
        /// <summary>
        /// Init filter options
        /// </summary>
        private void InitDropDownOptions()
        {
            int Filter = 0;
            int RoleType = 0;
            if (Session["UserInfo"] != null)
            {
                Filter = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                RoleType = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;

                InitialDDLDistribuidor(RoleType, Filter);
                InitProveedorBranches(RoleType, Filter);
                InitialDDLEstatus();
                InitialDDLTecno();
            }
        }
        /// <summary>
        /// Load default grid view list
        /// </summary>
        private void InitDefaultData()
        {
            int pageCount = 0;
            int Filter = 0, RoleType = 0;
            DataTable dtApproveCredit  = null;
            //Changed by Jerry 2011/08/04
            if (Session["UserInfo"] != null)
            {
                Filter = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                RoleType = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;
            }

            dtApproveCredit = K_CREDITODal.ClassInstance.GetCredits(RoleType, Filter, string.Empty, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out pageCount);

            //Check if there is no record
            bool bIsOne = false;
            if (dtApproveCredit.Rows.Count < 1)
            {
                dtApproveCredit.Rows.Add(dtApproveCredit.NewRow());
                bIsOne = true;
            }
            //Bind grid view data
            gvCredit.DataSource = dtApproveCredit;
            AspNetPager.RecordCount = pageCount;
            gvCredit.DataBind();
            //Check if show CheckBox at the right of the row
            if (bIsOne)
            {
                gvCredit.Columns[4].Visible = false;
            }
            else
            {
                gvCredit.Columns[4].Visible = true;
            }
        }
        /// <summary>
        /// Init proveedor branches
        /// </summary>
        private void InitProveedorBranches(int role, int filter)
        {             
            DataTable dtBranches = null;
            int proveedor;

            int.TryParse(this.ddlDistribuidor.SelectedValue, out proveedor);
            if (role == (int)UserRole.ZONE)
            {
                dtBranches = SupplierBrancheDal.ClassInstance.GetSupplierBranch(proveedor/*researved, not used*/, filter);
            }
            else
            {
                dtBranches = SupplierBrancheDal.ClassInstance.GetSupplierBranchesWithRegion(filter, proveedor/*researved, not used*/);
            }
                        
            if (dtBranches != null)
            {
                this.drpBranch.DataSource = dtBranches;
                this.drpBranch.DataTextField = "Dx_Nombre_Comercial";
                this.drpBranch.DataValueField = "Id_Branch";
                this.drpBranch.DataBind();
                this.drpBranch.Items.Insert(0, new ListItem("","0"));
                this.drpBranch.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Load technology
        /// </summary>
        private void InitialDDLTecno()
        {
            //Changed by Jerry 2011/08/05
            DataTable dtTechnology = null;

            dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgram(Global.PROGRAM.ToString());//Changed by Jerry 2011/08/08
            if (dtTechnology != null)
            {
                ddlTecno.DataSource = dtTechnology;
                ddlTecno.DataTextField = "Dx_Nombre_Particular";
                ddlTecno.DataValueField = "Cve_Tecnologia";
                ddlTecno.DataBind();
                ddlTecno.Items.Insert(0, new ListItem("", "0"));
                ddlTecno.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Load estado
        /// </summary>
        private void InitialDDLEstatus()
        {
            List<CAT_ESTATUS_CREDITOModel> creStatusList = CAT_ESTATUS_CREDITOBll.ClassInstance.Get_AllCAT_ESTATUS_CREDITO();
            if (creStatusList.Count > 0)
            {
                ddlEstatus.DataSource = creStatusList;
                ddlEstatus.DataTextField = "Dx_Estatus_Credito";
                ddlEstatus.DataValueField = "Cve_Estatus_Credito";
                ddlEstatus.DataBind();
                ddlEstatus.Items.Insert(0, new ListItem("", "0"));
                ddlEstatus.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Load proveedor
        /// </summary>
        private void InitialDDLDistribuidor(int role, int filter)
        {
            DataTable providerList = null;
            if (role == (int)UserRole.ZONE)
            {
                providerList = CAT_PROVEEDORDal.ClassInstance.GetProveedorWithZone(filter);
            }
            else
            {
                providerList = CAT_PROVEEDORDal.ClassInstance.GetProveedorWithRegion(filter);
            }

            if (providerList != null)
            {
                ddlDistribuidor.DataSource = providerList;
                ddlDistribuidor.DataTextField = "Dx_Nombre_Comercial";
                ddlDistribuidor.DataValueField = "Id_Proveedor";
                ddlDistribuidor.DataBind();
                ddlDistribuidor.Items.Insert(0, new ListItem("", "0"));
                ddlDistribuidor.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// clear filter conditions
        /// </summary>
        private void ClearFilterConditions()
        {
            Session["AuthorizeCurrentStatus"] = 0;
            Session["AuthorizeCurrentTechnology"] = 0;
            Session["AuthorizeCurrentProveedor"] = 0;
            Session["AuthorizeCurrentProveedorBranch"] = 0;
        }

        /// <summary>
        /// Bind grid view data
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        private void BindGridView(int pageIndex, int pageSize)
        {
            int pageCount = 0;
            int Filter = 0, RoleType = 0;
            DataTable dtApproveCredit = null;
            //Changed by Jerry 2011/08/04
            if (Session["UserInfo"] != null)
            {
                Filter = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                RoleType = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;
            }
            #region Build Search Conditions
            int Technology = 0;
            int.TryParse(ddlTecno.SelectedValue, out Technology);

            int StatusID = 0;
            int.TryParse(ddlEstatus.SelectedValue, out StatusID);

            int DistributorID = 0;
            int.TryParse(ddlDistribuidor.SelectedValue, out DistributorID);

            int BranchID = 0;
            int.TryParse(this.drpBranch.SelectedValue, out BranchID);
            #endregion

            dtApproveCredit = K_CREDITODal.ClassInstance.GetCredits(RoleType, Filter, DistributorID, BranchID, StatusID, Technology, string.Empty, this.AspNetPager.CurrentPageIndex, this.AspNetPager.PageSize, out pageCount);
           
            //Check if there is no record
            bool bIsOne = false;
            if (dtApproveCredit.Rows.Count < 1)
            {
                dtApproveCredit.Rows.Add(dtApproveCredit.NewRow());
                bIsOne = true;
            }
            //Bind grid view data
            gvCredit.DataSource = dtApproveCredit;
            AspNetPager.RecordCount = pageCount;
            gvCredit.DataBind();
            //Check if show CheckBox at the right of the row
            if (bIsOne)
            {
                gvCredit.Columns[4].Visible = false;
            }
            else
            {
                gvCredit.Columns[4].Visible = true;
            }
        }
        /// <summary>
        /// Refresh grid view data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
            }
        }

        protected void AspNetPager_PageChanging(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.ddlEstatus.SelectedIndex = Session["AuthorizeCurrentStatus"] != null ? (int)Session["AuthorizeCurrentStatus"] : 0;
                this.ddlTecno.SelectedIndex = Session["AuthorizeCurrentTechnology"] != null ? (int)Session["AuthorizeCurrentTechnology"] : 0;
                this.ddlDistribuidor.SelectedIndex = Session["AuthorizeCurrentProveedor"] != null ? (int)Session["AuthorizeCurrentProveedor"] : 0;
                this.drpBranch.SelectedIndex = Session["AuthorizeCurrentProveedorBranch"] != null ? (int)Session["AuthorizeCurrentProveedorBranch"] : 0;
            }
        }
        /// <summary>
        /// Enrevision the selected credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRevision_Click(object sender, EventArgs e)
        {
            string CreditNumber;//Changed by Jerry 2011/08/12
            int iCount = 0, iSucess = 0;
            string strMsg ="";
            bool bIsSelected = false;

            #region Check If Have Records Be Seleted

            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    iCount++;
                }
            }
            //If no selected record show message
            if (iCount == 0)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPleaseSelectOne") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Enrevision The Seleted Credits

            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    CreditNumber = gvCredit.DataKeys[i].Value.ToString();//Changed by Jerry 2011/08/12

                    int iResult = 0;
                    //Edit by Jerry 2011/08/03
                    // RSA columna extra
                    if (/*this.gvCredit.Rows[i].Cells[3].Text.ToUpper() == "PENDIENTE" || */this.gvCredit.Rows[i].Cells[5].Text.ToUpper() == "POR ENTREGAR" && IsReceived(CreditNumber))//just to do en revision for por entregar credits
                    {
                        iResult = K_CREDITODal.ClassInstance.EnRevisionCredit(CreditNumber, (int)CreditStatus.ENREVISION, DateTime.Now, Session["UserName"].ToString(), DateTime.Now.Date);//K_CREDITOBll.ClassInstance.Update_CreditStatus(instance);

                        if (iResult > 0)
                        {
                            iSucess++;
                            //Update schedule jobs
                            // RSA columna extra
                            if (this.gvCredit.Rows[i].Cells[5].Text.ToUpper() == "PENDIENTE")
                            {
                                ScheduleJobsDal.ClassInstance.CanceledScheduleJob(CreditNumber,ParameterHelper.strCon_DBLsWebApp);//Changed by Jerry 2012/04/13
                            }
                        }
                    }
                }
            }

            if (iCount > 0 && iCount == iSucess)
            {
                strMsg   = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSSuccessToRevision") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "updateRevisionSucess", "alert('" + strMsg + "');", true);
            }
            else
            {
               strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToRevision") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "updateRevisionFail", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
        }
        /// <summary>
        /// Autorizado credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAutorizado_Click(object sender, EventArgs e)
        {
            string CreditNumber;///Changed by Jerry 2011/08/12
            int iCount = 0, iSucess = 0;
            string strMsg = "";
            bool bIsSelected = false;
            int iCanceled = 0, iSkiped = 0;

            #region Check If Have Records Be Seleted

            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    iCount++;
                }
            }

            if (iCount == 0)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPleaseSelectOne") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Autorizado The Selected Credits

            string error = string.Empty;
            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (bIsSelected)
                {
                    CreditNumber = gvCredit.DataKeys[i].Value.ToString();//Changed by Jerry 2011/08/12

                    bool ResultValid = !K_CREDITO_SUSTITUCIONDAL.ClassInstance.IsSustitucionFolioRequired(CreditNumber);
                
                    int iResult = 0;
                    // RSA columna extra
                    if (this.gvCredit.Rows[i].Cells[5].Text.ToUpper() == "EN REVISION" && ResultValid)//could authorize credits with status en revision
                    {
                        // RSA 20130830 Check if this credit is still valid
                        string CreditState = CancelInvalidCredit(CreditNumber, ref error);
                        if (!CreditState.Equals("VALID"))
                        {
                            // Couldn't be validated, it was either skiped because a communication problem with SICOM,
                            // or canceled if it was invalid
                            if (CreditState.Equals("CANCELED"))
                                iCanceled++;
                            else
                                iSkiped++;
                        }
                        else
                        {
                            iResult = K_CREDITODal.ClassInstance.AprobarCredit(CreditNumber, (int)CreditStatus.AUTORIZADO, DateTime.Now, Session["UserName"].ToString(), DateTime.Now.Date);//K_CREDITOBll.ClassInstance.Update_CreditStatus(instance);

                            if (iResult > 0)
                            {
                                iSucess++;
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(error))
                    {
                        error = HttpContext.GetGlobalResourceObject("DefaultResource", "InvalidStatus") as string;
                    }
                }
            }
            // RSA 20130830 Inform of any canceled credit
            if (iCanceled > 0)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSInvalidToAutorizado") as string;
                strMsg = string.Format(strMsg, iCanceled);
                if (!string.IsNullOrEmpty(error))
                    strMsg += "\\r\\n" + error.Replace("<br />", "\r\n").Replace("\r\n", "\\r\\n");
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "updateRevisionSucess", "alert('" + strMsg + "');", true);
            }
            //If all had been update successfully
            else if (iCount > 0 && iCount == iSucess)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSSuccessToAutorizado") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "updateRevisionSucess", "alert('" + strMsg + "');", true);
            }
            else
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToAutorizado") as string;
                if (!string.IsNullOrEmpty(error))
                    strMsg += "\\r\\n" + error.Replace("<br />", "\r\n").Replace("\r\n", "\\r\\n");
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "updateRevisionFail", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
        }
        /// <summary>
        /// Rechazado credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRechazado_Click(object sender, EventArgs e)
        {
            string CreditNumber;//Changed by Jerry 2011/08/12
            int iCount = 0, iSucess = 0;
            string strMsg = "";
            bool bIsSelected = false;

            #region Check If Have Records Be Seleted

            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    iCount++;
                }
            }

            if (iCount == 0)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPleaseSelectOne") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Rechazado The Selected Credits

            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (bIsSelected)
                {
                    CreditNumber = gvCredit.DataKeys[i].Value.ToString();//Changed by Jerry 2011/08/12

                    int iResult = 0;
                    // RSA columna extra
                    if (this.gvCredit.Rows[i].Cells[5].Text.ToUpper() == "EN REVISION")//could rechazado credits with status en revision
                    {
                        // update by Tina 2011/08/12
                        decimal iRequestAmount = 0;
                        iRequestAmount = this.gvCredit.Rows[i].Cells[2].Text == "" ? 0 : decimal.Parse(this.gvCredit.Rows[i].Cells[2].Text.Substring(1));
                        using (TransactionScope scope = new TransactionScope())
                        {
                            iResult = K_CREDITODal.ClassInstance.RechazarCredit(CreditNumber, (int)CreditStatus.RECHAZADO, DateTime.Now, Session["UserName"].ToString(), DateTime.Now.Date);//K_CREDITOBll.ClassInstance.Update_CreditStatus(instance);
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
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSSuccessToRechazado") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "updateRevisionSucess", "alert('" + strMsg + "');", true);
            }
            else
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToRechazado") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "updateRevisionFail", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
        }

        #region Refresh Grid View
        /// <summary>
        /// Refresh grid view data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDistribuidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
            //Added by Jerry 2011/08/09
            Session["AuthorizeCurrentProveedor"] = this.ddlDistribuidor.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Refresh grid view data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTecno_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
            //Added by Jerry 2011/08/09
            Session["AuthorizeCurrentTechnology"] = this.ddlTecno.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Refresh grid view data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
            //Added by Jerry 2011/08/09
            Session["AuthorizeCurrentStatus"] = this.ddlEstatus.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Refresh grid view when branch changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
            //Added by Jerry 2011/08/09
            Session["AuthorizeCurrentProveedorBranch"] = this.drpBranch.SelectedIndex;
            this.AspNetPager.GoToPage(1);
        }
        #endregion
        /// <summary>
        /// Change the credit to status pendiente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPendiente_Click(object sender, EventArgs e)
        {
            // RSA 20130703  (2013 jul 03) Always disabled
            return;
            string CreditNumber;//Changed by Jerry 2011/08/12
            int iCount = 0, iSucess = 0;
            string strMsg = "";
            bool bIsSelected = false;

            #region Check If Have Records Be Seleted

            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    iCount++;
                }
            }

            if (iCount == 0)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPleaseSelectOne") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Rechazado The Selected Credits

            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (bIsSelected)
                {
                    CreditNumber = gvCredit.DataKeys[i].Value.ToString();//Changed by Jerry 2011/08/12

                    int iResult = 0;
                    //if (this.gvCredit.Rows[i].Cells[3].Text.ToUpper() != "PENDIENTE" && this.gvCredit.Rows[i].Cells[3].Text.ToUpper() != "BENEFICIARIO CON ADEUDOS" && this.gvCredit.Rows[i].Cells[3].Text.ToUpper() != "TARIFA FUERA DE PROGRAMA")
                    // RSA columna extra
                    if (this.gvCredit.Rows[i].Cells[5].Text.ToUpper() != "PENDIENTE" && this.gvCredit.Rows[i].Cells[5].Text.ToUpper() != "BENEFICIARIO CON ADEUDOS" && this.gvCredit.Rows[i].Cells[5].Text.ToUpper() != "TARIFA FUERA DE PROGRAMA"
                        && this.gvCredit.Rows[i].Cells[5].Text.ToUpper() != "RECHAZADO" && this.gvCredit.Rows[i].Cells[5].Text.ToUpper() != "CALIFICACION MOP NO VALIDA")
                    {
                        // update by Tina 2011/08/12
                        decimal iRequestAmount = 0;
                        iRequestAmount = this.gvCredit.Rows[i].Cells[2].Text == "" ? 0 : decimal.Parse(this.gvCredit.Rows[i].Cells[2].Text.Substring(1));
                        using (TransactionScope scope = new TransactionScope())
                        {
                            iResult = K_CREDITODal.ClassInstance.PendienteCredit(CreditNumber, (int)CreditStatus.PENDIENTE, DateTime.Now.Date, Session["UserName"].ToString(), DateTime.Now.Date);//K_CREDITOBll.ClassInstance.Update_CreditStatus(instance);
                            iResult += CAT_PROGRAMADal.ClassInstance.IncreaseCurrentAmount(Global.PROGRAM, iRequestAmount);
                            scope.Complete();
                        }
                        // End

                        if (iResult > 0)
                        {
                            iSucess++;
                            //Update batch email table
                            ScheduleJobEntity JobEntity = new ScheduleJobEntity();
                            JobEntity.Credit_No = CreditNumber;
                            JobEntity.Create_Date = DateTime.Now.Date;
                            JobEntity.Email_Body = HttpContext.GetGlobalResourceObject("DefaultResource", "Email25DaysPendingBody") as string;
                            JobEntity.Email_Title = HttpContext.GetGlobalResourceObject("DefaultResource", "Email25DaysPendingTitle") as string;
                            JobEntity.Job_Status = GlobalVar.WAITING_FOR_PROCESS;
                            if (Session["UserInfo"] != null)
                            {
                                JobEntity.Supplier_Name = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;
                                JobEntity.Supplier_Email = ((US_USUARIOModel)Session["UserInfo"]).CorreoElectronico;
                            }
                            //insert data
                            ScheduleJobsDal.ClassInstance.AddScheduleJob(JobEntity);
                        }
                    }
                }
            }
            //if all had been updated successfully
            if (iCount > 0 && iCount == iSucess)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPendienteCreditSuccess") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "PendienteSuccess", "alert('" + strMsg + "');", true);
            }
            else
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPendienteFailed") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "PendienteFailed", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
        }
        /// <summary>
        /// Change the credit to status pendiente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string CreditNumber;
            int iCount = 0, iSucess = 0;
            string strMsg = "";
            bool bIsSelected = false;

            #region Check If Have Records Be Seleted

            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    iCount++;
                }
            }

            if (iCount == 0)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPleaseSelectOne") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Cancel The Selected Credits

            for (int i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (bIsSelected)
                {
                    CreditNumber = gvCredit.DataKeys[i].Value.ToString();

                    int iResult = 0;
                    if (this.gvCredit.Rows[i].Cells[5].Text.ToUpper() == "EN REVISION")
                    {
                        decimal iRequestAmount = 0;
                        iRequestAmount = this.gvCredit.Rows[i].Cells[2].Text == "" ? 0 : decimal.Parse(this.gvCredit.Rows[i].Cells[2].Text.Substring(1));
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
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "CancelSuccess", "alert('" + strMsg + "');", true);
            }
            else
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToCancel") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "CancelFailed", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
        }
        /// <summary>
        /// Check whether the Old product is received 
        /// </summary>
        /// <param name="CreditSusID">substitution record number</param>
        /// <returns></returns>
        private Boolean IsReceived(string CreditNum)
        {
            bool Received = false;

            Received = !K_CREDITO_SUSTITUCIONDAL.ClassInstance.IsOldProductCharactersRequired(CreditNum);

            return Received;
        }

        // RSA 20131003 if it's SE then do not validate RPU, it may has been discontiued already
        private bool IsSE(string CreditNum)
        {
            bool result = false;

            DataTable CreditProductDt = K_CREDITO_PRODUCTODal.ClassInstance.get_K_Credit_ProductByCreditNo(CreditNum);
            // for SE Rows.Count must be 1, if count is different then it cannot be SE
            if (CreditProductDt != null && CreditProductDt.Rows.Count == 1)
            {
                string techId = CreditProductDt.Rows[0]["Technology"].ToString();

                // get all technologies
                DataTable Technology = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgramAndDxCveCC(Global.PROGRAM.ToString(), 0, string.Empty);
                // identify technology and check if it is SE
                if (Technology != null && Technology.Rows.Count > 0)
                {
                    for (int j = 0; j < Technology.Rows.Count; j++)
                    {
                        if (Technology.Rows[j]["Cve_Tecnologia"].ToString().Equals(techId))
                        {
                            result = Technology.Rows[j]["Dx_Cve_CC"].ToString() == DxCveCC_SE;
                            break;
                        }
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// Cancel credits that are not valid anymore
        /// </summary>
        /// <param name="CreditNum"></param>
        /// <returns>Returns the credits status: VALID, SKIPED, CANCELED</returns>
        private string CancelInvalidCredit(string CreditNum, ref string errorMsg)
        {
            string result = string.Empty;
            string statusFlag = "Exist";
            string error = string.Empty;
            string serviceCode = string.Empty;

            CodeValidator validator = new CodeValidator();
            DataTable dtCredit;

            try
            {
                dtCredit = K_CREDITODal.ClassInstance.GetCreditsReview(CreditNum);
                serviceCode = dtCredit.Rows[0]["No_RPU"].ToString();

                // RSA 20131003 if SE do not validate RPU, it has been descontiued
                if (IsSE(CreditNum))
                {
                    result = "VALID";
                }
                else if (validator.ValidateServiceCode(serviceCode, out error, ref statusFlag))
                {
                    result = "VALID";
                }
                // communication problem, we don't know whether it is valid or invalid
                else if (statusFlag.Equals("W"))
                {
                    result = "SKIPED";
                }
                else // it is invalid CANCEL it
                {
                    decimal iRequestAmount = 0;
                    float valor = 0;

                    // Read requested amount by this credit
                    if (float.TryParse(dtCredit.Rows[0]["Mt_Monto_Solicitado"].ToString(), out valor))
                        iRequestAmount = (decimal)valor;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        // Change credti status to Cancelado, and return the amount to the program
                        K_CREDITODal.ClassInstance.CancelCredit(CreditNum, Session["UserName"].ToString());
                        CAT_PROGRAMADal.ClassInstance.IncreaseCurrentAmount(Global.PROGRAM, iRequestAmount);
                        RegionalDal.ClassInstance.LogCreditCanceled(CreditNum, Session["UserName"].ToString(), error);

                        // log operation
                        scope.Complete();
                    }

                    result = "CANCELED";
                }
            }
            catch (Exception ex)
            {
                // if anything goes grong, SKIP it, (it is neither valid nor invalid)
                result = "SKIPED";
            }

            if (!string.IsNullOrEmpty(error))
            {
                // errorMsg = error;
                // errorMsg = string.Format(HttpContext.GetGlobalResourceObject("DefaultResource", "SICOMValidation") as string
                errorMsg = string.Format(HttpContext.GetGlobalResourceObject("DefaultResource", "SICOMValidation").ToString(), error);
            }

            return result;
        }
    }
}
