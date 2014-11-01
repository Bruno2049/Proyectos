using System;
using System.Globalization;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.BussinessLayer;
using PAEEEM.Captcha;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.Credito;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using PAEEEM.LogicaNegocios.TarifaSubEstaciones;

namespace PAEEEM.RegionalModule
{
    public partial class CreditAuthorization : Page
    {
        const string DxCveCcSe = "SE";
        private const int Nc = 0;

        /// <summary>
        /// Init controls when page first load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

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

            btn_Aceptar.Enabled = false;

            ////add by @l3x////
            DDLMotivo.Attributes.Add("onchange", "ValidarCbxMotivo()");
            TexObserv.Attributes.Add("onkeyup", "ValidarObservaciones(this, 300);");
            TexMoti.Attributes.Add("onkeyup", "ValidarMotivo(this, 300);");

            DDLMotivoRechazo.Attributes.Add("onchange", "ValidarCbxMotivoRecha()");
            TexObservRechazo.Attributes.Add("onkeyup", "ValidarObservacionesRecha(this, 300);");
            TexMotiRechazo.Attributes.Add("onkeyup", "ValidarMotivoRecha(this, 300);");
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
 //                   btnPendiente.Enabled = false;
            //     }
            // }
        }
        /// <summary>
        /// Init filter options
        /// </summary>
        private void InitDropDownOptions()
        {
            if (Session["UserInfo"] == null) return;

            var filter = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
            var roleType = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;

            InitialDdlDistribuidor(roleType, filter);
            InitProveedorBranches(roleType, filter);
            InitialDdlEstatus();
            InitialDdlTecno();
        }
        /// <summary>
        /// Load default grid view list
        /// </summary>
        private void InitDefaultData()
        {
            int pageCount;
            int filter = 0, roleType = 0;
            //Changed by Jerry 2011/08/04
            if (Session["UserInfo"] != null)
            {
                filter = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                roleType = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;
            }

            var dtApproveCredit = K_CREDITODal.ClassInstance.GetCredits(roleType, filter, string.Empty, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);

            //Check if there is no record
            var bIsOne = false;
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
            gvCredit.Columns[4].Visible = !bIsOne;
        }

        /// <summary>
        /// Init proveedor branches
        /// </summary>
        private void InitProveedorBranches(int role, int filter)
        {
            int proveedor;

            int.TryParse(ddlDistribuidor.SelectedValue, out proveedor);
            var dtBranches = role == (int) UserRole.ZONE
                ? SupplierBrancheDal.ClassInstance.GetSupplierBranch(proveedor /*researved, not used*/, filter)
                : SupplierBrancheDal.ClassInstance.GetSupplierBranchesWithRegion(filter, proveedor
                    /*researved, not used*/);

            if (dtBranches == null) return;

            drpBranch.DataSource = dtBranches;
            drpBranch.DataTextField = "Dx_Nombre_Comercial";
            drpBranch.DataValueField = "Id_Branch";
            drpBranch.DataBind();
            drpBranch.Items.Insert(0, new ListItem("", "0"));
            drpBranch.SelectedIndex = 0;
        }

        /// <summary>
        /// Load technology
        /// </summary>
        private void InitialDdlTecno()
        {
            //Changed by Jerry 2011/08/05
            var dtTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgram(Global.PROGRAM.ToString(CultureInfo.InvariantCulture));
            if (dtTechnology == null) return;
            ddlTecno.DataSource = dtTechnology;
            ddlTecno.DataTextField = "Dx_Nombre_Particular";
            ddlTecno.DataValueField = "Cve_Tecnologia";
            ddlTecno.DataBind();
            ddlTecno.Items.Insert(0, new ListItem("", "0"));
            ddlTecno.SelectedIndex = 0;
        }

        /// <summary>
        /// Load estado
        /// </summary>
        private void InitialDdlEstatus()
        {
            var dtCreditStatus = CatEstatusDal.ClassInstance.GetCreditEstatus();
            if (dtCreditStatus == null) return;
            ddlEstatus.DataSource = dtCreditStatus;
            ddlEstatus.DataTextField = "Dx_Estatus_Credito";
            ddlEstatus.DataValueField = "Cve_Estatus_Credito";
            ddlEstatus.DataBind();
            ddlEstatus.Items.Insert(0, new ListItem("", "0"));
        }
        /// <summary>
        /// Load proveedor
        /// </summary>
        private void InitialDdlDistribuidor(int role, int filter)
        {
            var providerList = role == (int)UserRole.ZONE ? CAT_PROVEEDORDal.ClassInstance.GetProveedorWithZone(filter) : CAT_PROVEEDORDal.ClassInstance.GetProveedorWithRegion(filter);

            if (providerList == null) return;
            ddlDistribuidor.DataSource = providerList;
            ddlDistribuidor.DataTextField = "Dx_Nombre_Comercial";
            ddlDistribuidor.DataValueField = "Id_Proveedor";
            ddlDistribuidor.DataBind();
            ddlDistribuidor.Items.Insert(0, new ListItem("", "0"));
            ddlDistribuidor.SelectedIndex = 0;
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
        private void BindGridView()
        {
            int pageCount;
            int filter = 0, roleType = 0;
            //Changed by Jerry 2011/08/04
            if (Session["UserInfo"] != null)
            {
                filter = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
                roleType = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;
            }
            #region Build Search Conditions
            int technology;
            int.TryParse(ddlTecno.SelectedValue, out technology);

            int statusId;
            int.TryParse(ddlEstatus.SelectedValue, out statusId);

            int distributorId;
            int.TryParse(ddlDistribuidor.SelectedValue, out distributorId);

            int branchId;
            int.TryParse(drpBranch.SelectedValue, out branchId);
            #endregion

            var dtApproveCredit = K_CREDITODal.ClassInstance.GetCredits(roleType, filter, distributorId, branchId, statusId, technology, string.Empty, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out pageCount);
           
            //Check if there is no record
            var bIsOne = false;
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
            gvCredit.Columns[4].Visible = !bIsOne;
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
                BindGridView();
            }
        }

        protected void AspNetPager_PageChanging(object sender, EventArgs e)
        {
            if (!IsPostBack) return;
            ddlEstatus.SelectedIndex = Session["AuthorizeCurrentStatus"] != null ? (int)Session["AuthorizeCurrentStatus"] : 0;
            ddlTecno.SelectedIndex = Session["AuthorizeCurrentTechnology"] != null ? (int)Session["AuthorizeCurrentTechnology"] : 0;
            ddlDistribuidor.SelectedIndex = Session["AuthorizeCurrentProveedor"] != null ? (int)Session["AuthorizeCurrentProveedor"] : 0;
            drpBranch.SelectedIndex = Session["AuthorizeCurrentProveedorBranch"] != null ? (int)Session["AuthorizeCurrentProveedorBranch"] : 0;
        }
        /// <summary>
        /// Enrevision the selected credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRevision_Click(object sender, EventArgs e)
        {
            var creditNumber="";//Changed by Jerry 2011/08/12
            int iCount = 0, iSucess = 0;
            string strMsg;
            bool bIsSelected;

            #region Check If Have Records Be Seleted

            for (var i = 0; i < gvCredit.Rows.Count; i++)
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
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Enrevision The Seleted Credits

            for (var i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (!bIsSelected) continue;

                creditNumber = gvCredit.DataKeys[i].Value.ToString();//Changed by Jerry 2011/08/12

                //Edit by Jerry 2011/08/03
                // RSA columna extra
                if (gvCredit.Rows[i].Cells[5].Text.ToUpper() != "POR ENTREGAR" || !IsReceived(creditNumber)) continue;

                var iResult = K_CREDITODal.ClassInstance.EnRevisionCredit(creditNumber, (int)CreditStatus.ENREVISION, DateTime.Now, Session["UserName"].ToString(), DateTime.Now.Date);

                if (iResult <= 0) continue;

                iSucess++;

                /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO EN REVISION EN SOLICITUD DE CREDITO*/
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                    "SOLICITUD DE CREDITO", "EN REVISION", creditNumber,
                    "", "", "", "");

                //Update schedule jobs
                // RSA columna extra
                if (gvCredit.Rows[i].Cells[5].Text.ToUpper() == "PENDIENTE")
                {
                    ScheduleJobsDal.ClassInstance.CanceledScheduleJob(creditNumber,ParameterHelper.strCon_DBLsWebApp);//Changed by Jerry 2012/04/13
                }
            }

            if (iCount > 0 && iCount == iSucess)
            {
                strMsg   = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSSuccessToRevision") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "updateRevisionSucess", "alert('" + strMsg + "');", true);

                ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", "window.open('PrintForm2.aspx?ReportName=Acuse_Recibo&CreditNumber=" + creditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            }
            else
            {
               strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToRevision") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "updateRevisionFail", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            BindGridView();
        }
        /// <summary>
        /// Autorizado credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAutorizado_Click(object sender, EventArgs e)
        {
            int iCount = 0, iSucess = 0;
            string strMsg;
            bool bIsSelected;
            var iCanceled = 0;
            var iSkiped = 0;

            #region Check If Have Records Be Seleted

            for (var i = 0; i < gvCredit.Rows.Count; i++)
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
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Autorizado The Selected Credits

            var error = string.Empty;
            for (var i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (!bIsSelected) continue;

                var creditNumber = gvCredit.DataKeys[i].Value.ToString();//Changed by Jerry 2011/08/12
                var cveEstatus = int.Parse(gvCredit.DataKeys[i][1].ToString());

                var resultValid = !K_CREDITO_SUSTITUCIONDAL.ClassInstance.IsSustitucionFolioRequired(creditNumber);

                // RSA columna extra
                //if (this.gvCredit.Rows[i].Cells[5].Text.ToUpper() == "EN REVISION" && ResultValid)//could authorize credits with status en revision
                //if (this.gvCredit.Rows[i].Cells[5].Text.ToUpper() == "EN REVISION" && ResultValid)
                if (cveEstatus == (int)CreditStatus.PRE_AUTORIZADO && resultValid)
                {
                    // RSA 20130830 Check if this credit is still valid
                    var creditState = CancelInvalidCredit(creditNumber, ref error);
                    if (!creditState.Equals("VALID"))
                    {
                        // Couldn't be validated, it was either skiped because a communication problem with SICOM,
                        // or canceled if it was invalid
                        if (creditState.Equals("CANCELED"))
                            iCanceled++;
                        else
                            iSkiped++;
                    }
                    else
                    {
                        var iResult = K_CREDITODal.ClassInstance.AprobarCredit(creditNumber, (int)CreditStatus.AUTORIZADO, DateTime.Now, Session["UserName"].ToString(), DateTime.Now.Date);

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
            // RSA 20130830 Inform of any canceled credit
            if (iCanceled > 0)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSInvalidToAutorizado") as string;
                if (strMsg != null)
                {
                    strMsg = string.Format(strMsg, iCanceled);
                    if (!string.IsNullOrEmpty(error))
                        strMsg += "\\r\\n" + error.Replace("<br />", "\r\n").Replace("\r\n", "\\r\\n");
                    ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "updateRevisionSucess", "alert('" + strMsg + "');", true);
                }
            }
            //If all had been update successfully
            else if (iCount > 0 && iCount == iSucess)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSSuccessToAutorizado") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "updateRevisionSucess", "alert('" + strMsg + "');", true);
            }
            else
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToAutorizado") as string;
                if (!string.IsNullOrEmpty(error))
                    strMsg += "\\r\\n" + error.Replace("<br />", "\r\n").Replace("\r\n", "\\r\\n");
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "updateRevisionFail", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            BindGridView();
        }
        /// <summary>
        /// Rechazado credit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRechazado_Click(object sender, EventArgs e)
        {
            int iCount = 0, iSucess = 0;
            string strMsg;
            bool bIsSelected;

            #region Check If Have Records Be Seleted

            for (var i = 0; i < gvCredit.Rows.Count; i++)
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
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Rechazado The Selected Credits

            for (var i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (!bIsSelected) continue;

                var creditNumber = gvCredit.DataKeys[i].Value.ToString();//Changed by Jerry 2011/08/12

                // RSA columna extra
                if (gvCredit.Rows[i].Cells[5].Text.ToUpper() != "EN REVISION") continue;

                // update by Tina 2011/08/12
                var iRequestAmount = gvCredit.Rows[i].Cells[2].Text == "" ? 0 : decimal.Parse(gvCredit.Rows[i].Cells[2].Text.Substring(1));
                int iResult;
                using (var scope = new TransactionScope())
                {
                    iResult = K_CREDITODal.ClassInstance.RechazarCredit(creditNumber, (int)CreditStatus.RECHAZADO, DateTime.Now, Session["UserName"].ToString(), DateTime.Now.Date);//K_CREDITOBll.ClassInstance.Update_CreditStatus(instance);
                    iResult += CAT_PROGRAMADal.ClassInstance.IncreaseCurrentAmount(Global.PROGRAM, iRequestAmount);
                    scope.Complete();
                }
                // End

                if (iResult > 0)
                {
                    iSucess++;
                }
            }
            //if all had been updated successfully
            if (iCount > 0 && iCount == iSucess)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSSuccessToRechazado") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "updateRevisionSucess", "alert('" + strMsg + "');", true);
            }
            else
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToRechazado") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "updateRevisionFail", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            BindGridView();
        }

        #region Refresh Grid View
        /// <summary>
        /// Refresh grid view data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDistribuidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
            //Added by Jerry 2011/08/09
            Session["AuthorizeCurrentProveedor"] = ddlDistribuidor.SelectedIndex;
            AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Refresh grid view data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTecno_SelectedIndexChanged(object sender, EventArgs e)
        { 
            //Added by Jerry 2011/08/09
            Session["AuthorizeCurrentTechnology"] = ddlTecno.SelectedIndex;
            BindGridView();
           
            //this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Refresh grid view data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
            //Added by Jerry 2011/08/09
            Session["AuthorizeCurrentStatus"] = ddlEstatus.SelectedIndex;
            //this.AspNetPager.GoToPage(1);
        }
        /// <summary>
        /// Refresh grid view when branch changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
            //Added by Jerry 2011/08/09
            Session["AuthorizeCurrentProveedorBranch"] = drpBranch.SelectedIndex;
            AspNetPager.GoToPage(1);
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

            int iCount = 0, iSucess = 0;
            string strMsg;
            bool bIsSelected;
            

            #region Check If Have Records Be Seleted

            for (var i = 0; i < gvCredit.Rows.Count; i++)
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
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
                return;
            }
            #endregion

            #region Rechazado The Selected Credits

            for (var i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (!bIsSelected) continue;

                var creditNumber = gvCredit.DataKeys[i].Value.ToString();//Changed by Jerry 2011/08/12

                //if (this.gvCredit.Rows[i].Cells[3].Text.ToUpper() != "PENDIENTE" && this.gvCredit.Rows[i].Cells[3].Text.ToUpper() != "BENEFICIARIO CON ADEUDOS" && this.gvCredit.Rows[i].Cells[3].Text.ToUpper() != "TARIFA FUERA DE PROGRAMA")
                // RSA columna extra
                if (gvCredit.Rows[i].Cells[5].Text.ToUpper() == "PENDIENTE" ||
                    gvCredit.Rows[i].Cells[5].Text.ToUpper() == "BENEFICIARIO CON ADEUDOS" ||
                    gvCredit.Rows[i].Cells[5].Text.ToUpper() == "TARIFA FUERA DE PROGRAMA" ||
                    gvCredit.Rows[i].Cells[5].Text.ToUpper() == "RECHAZADO" ||
                    gvCredit.Rows[i].Cells[5].Text.ToUpper() == "CALIFICACION MOP NO VALIDA") continue;

                // update by Tina 2011/08/12
                var iRequestAmount = gvCredit.Rows[i].Cells[2].Text == "" ? 0 : decimal.Parse(gvCredit.Rows[i].Cells[2].Text.Substring(1));
                int iResult;
                using (var scope = new TransactionScope())
                {
                    iResult = K_CREDITODal.ClassInstance.PendienteCredit(creditNumber, (int)CreditStatus.PENDIENTE, DateTime.Now.Date, Session["UserName"].ToString(), DateTime.Now.Date);//K_CREDITOBll.ClassInstance.Update_CreditStatus(instance);
                    iResult += CAT_PROGRAMADal.ClassInstance.IncreaseCurrentAmount(Global.PROGRAM, iRequestAmount);
                    scope.Complete();
                }
                // End

                if (iResult <= 0) continue;

                iSucess++;
                //Update batch email table
                var jobEntity = new ScheduleJobEntity
                {
                    Credit_No = creditNumber,
                    Create_Date = DateTime.Now.Date,
                    Email_Body =
                        HttpContext.GetGlobalResourceObject("DefaultResource", "Email25DaysPendingBody") as string,
                    Email_Title =
                        HttpContext.GetGlobalResourceObject("DefaultResource", "Email25DaysPendingTitle") as string,
                    Job_Status = GlobalVar.WAITING_FOR_PROCESS
                };


                if (Session["UserInfo"] != null)
                {
                    jobEntity.Supplier_Name = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;
                    jobEntity.Supplier_Email = ((US_USUARIOModel)Session["UserInfo"]).CorreoElectronico;
                }
                //insert data
                ScheduleJobsDal.ClassInstance.AddScheduleJob(jobEntity);
            }
            //if all had been updated successfully
            if (iCount > 0 && iCount == iSucess)
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPendienteCreditSuccess") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "PendienteSuccess", "alert('" + strMsg + "');", true);
            }
            else
            {
                strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPendienteFailed") as string;
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "PendienteFailed", "alert('" + strMsg + "');", true);
            }
            #endregion

            //Refresh grid view data
            BindGridView();
        }
        /// <summary>
        /// Change the credit to status pendiente
        /// </summary>
        //protected void btnCancelar_Click(object sender, EventArgs e)
        //{
        //    string CreditNumber;
        //    int iCount = 0, iSucess = 0;
        //    string strMsg = "";
        //    bool bIsSelected = false;

        //    #region Check If Have Records Be Seleted

        //    for (int i = 0; i < gvCredit.Rows.Count; i++)
        //    {
        //        bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
        //        if (bIsSelected)
        //        {
        //            iCount++;
        //        }
        //    }

        //    if (iCount == 0)
        //    {
        //        strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgPleaseSelectOne") as string;
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "pleaseSelsectone", "alert('" + strMsg + "');", true);
        //        return;
        //    }
        //    #endregion

        //    #region Cancel The Selected Credits

        //    for (int i = 0; i < gvCredit.Rows.Count; i++)
        //    {
        //        bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
        //        NC = i;
        //        if (bIsSelected)
        //        {
        //            CreditNumber = gvCredit.DataKeys[i].Value.ToString();

        //            int iResult = 0;
        //            if (this.gvCredit.Rows[i].Cells[5].Text.ToUpper() == "EN REVISION")
        //            {
        //                decimal iRequestAmount = 0;
        //                iRequestAmount = this.gvCredit.Rows[i].Cells[2].Text == "" ? 0 : decimal.Parse(this.gvCredit.Rows[i].Cells[2].Text.Substring(1));
        //                using (TransactionScope scope = new TransactionScope())
        //                {
        //                    iResult = K_CREDITODal.ClassInstance.CancelCredit(CreditNumber, Session["UserName"].ToString());
        //                    iResult += CAT_PROGRAMADal.ClassInstance.IncreaseCurrentAmount(Global.PROGRAM, iRequestAmount);
        //                    scope.Complete();
        //                }
        //                // End

        //                if (iResult > 0)
        //                {
        //                    iSucess++;
        //                }
        //            }
        //        }
        //    }
        //    //if all had been updated successfully
        //    if (iCount > 0 && iCount == iSucess)
        //    {
        //        strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSSuccessToCancel") as string;
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "CancelSuccess", "alert('" + strMsg + "');", true);
        //    }
        //    else
        //    {
        //        strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgUSFailToCancel") as string;
        //        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "CancelFailed", "alert('" + strMsg + "');", true);
        //    }
        //    #endregion

        //    //Refresh grid view data
        //    BindGridView(AspNetPager.CurrentPageIndex, AspNetPager.PageSize);
        //}
        /// <summary>
        /// Check whether the Old product is received 
        /// </summary>
        /// <returns></returns>
        private static Boolean IsReceived(string creditNum)
        {
            var received = !K_CREDITO_SUSTITUCIONDAL.ClassInstance.IsOldProductCharactersRequired(creditNum);

            return received;
        }

        // RSA 20131003 if it's SE then do not validate RPU, it may has been discontiued already
        private static bool IsSe(string creditNum)
        {
            var result = false;

            var creditProductDt = K_CREDITO_PRODUCTODal.ClassInstance.get_K_Credit_ProductByCreditNo(creditNum);
            // for SE Rows.Count must be 1, if count is different then it cannot be SE
            if (creditProductDt == null || creditProductDt.Rows.Count != 1) return false;

            var techId = creditProductDt.Rows[0]["Technology"].ToString();

            // get all technologies
            var technology = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgramAndDxCveCC(Global.PROGRAM.ToString(CultureInfo.InvariantCulture), 0, string.Empty);
            // identify technology and check if it is SE
            if (technology == null || technology.Rows.Count <= 0) return false;

            for (var j = 0; j < technology.Rows.Count; j++)
            {
                if (!technology.Rows[j]["Cve_Tecnologia"].ToString().Equals(techId)) continue;

                result = technology.Rows[j]["Dx_Cve_CC"].ToString() == DxCveCcSe;
                break;
            }

            return result;
        }

        /// <summary>
        /// Cancel credits that are not valid anymore
        /// </summary>
        /// <param name="creditNum"></param>
        /// <param name="errorMsg"></param>
        /// <returns>Returns the credits status: VALID, SKIPED, CANCELED</returns>
        private static string CancelInvalidCredit(string creditNum, ref string errorMsg)
        {
            var result = string.Empty;
            var statusFlag = "Exist";
            var error = string.Empty;

            var validator = new CodeValidator();

            try
            {
                var dtCredit = K_CREDITODal.ClassInstance.GetCreditsReview(creditNum);
                var serviceCode = dtCredit.Rows[0]["No_RPU"].ToString();

                // RSA 20131003 if SE do not validate RPU, it has been descontiued
                if (IsSe(creditNum))
                {
                    result = "VALID";
                }
                else
                {
                    string[] causa;
                    if (validator.ValidateServiceCode(serviceCode, out error, ref statusFlag,out causa))
                    {
                        result = "VALID";
                    }
                        // communication problem, we don't know whether it is valid or invalid
                    else if (statusFlag.Equals("W"))
                    {
                        result = "SKIPED";
                    }
                }

                #region Cambio día 28 de Julio Solicitado por Sistemas
                //else // it is invalid CANCEL it
                //{
                //    decimal iRequestAmount = 0;
                //    float valor = 0;

                //    // Read requested amount by this credit
                //    if (float.TryParse(dtCredit.Rows[0]["Mt_Monto_Solicitado"].ToString(), out valor))
                //        iRequestAmount = (decimal)valor;

                //    using (TransactionScope scope = new TransactionScope())
                //    {
                //        // Change credti status to Cancelado, and return the amount to the program
                //      K_CREDITODal.ClassInstance.CancelCredit(CreditNum, Session["UserName"].ToString());
                //   CAT_PROGRAMADal.ClassInstance.IncreaseCurrentAmount(Global.PROGRAM, iRequestAmount);
                //        RegionalDal.ClassInstance.LogCreditCanceled(CreditNum, Session["UserName"].ToString(), error);

                //        // log operation
                //        scope.Complete();
                //    }

                //    result = "CANCELED";
                //}
                #endregion
            }
            catch (Exception)
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

        protected void ddlEstatus_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindGridView();
            //Added by Jerry 2011/08/09
            Session["AuthorizeCurrentStatus"] = ddlEstatus.SelectedIndex;
            //this.AspNetPager.GoToPage(1);
        }

        protected void ckbSelect_CheckedChanged(object sender, EventArgs e)
        {
            
            var chk = (CheckBox)sender;
            var row = (GridViewRow)chk.Parent.Parent;

            var textoLink = row.Cells[0].Controls[0] as HyperLink;
            var credito = textoLink.Text;

            var estatus = row.Cells[5].Text;

            var cveEstatus = gvCredit.DataKeys[row.RowIndex][1].ToString();
            var CveEstatus = Int32.Parse(cveEstatus);
            var idRol = ((US_USUARIOModel)Session["UserInfo"]).Id_Rol;
            var idUsuario = ((US_USUARIOModel)Session["UserInfo"]).Id_Usuario;

            var combo = (DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones");

            if (chk.Checked == false)
            {
                foreach (GridViewRow item in gvCredit.Rows)
                {
                    gvCredit.Rows[item.DataItemIndex].Enabled = true;

                    ((DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).Enabled = false;
                    ((DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).Items.Clear();
                    ((DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).Items.Insert(0, "Elige Opcion");
                    btn_Aceptar.Enabled = false;
                }
            }

            else
            {

                foreach (GridViewRow item in gvCredit.Rows)
                {
                    if (row.DataItemIndex == item.DataItemIndex)
                    {
                        gvCredit.Rows[item.DataItemIndex].Enabled = true;
                        ((DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).Enabled = true;                        
                        btn_Aceptar.Enabled = true;
                    }

                    else
                    {
                        gvCredit.Rows[item.DataItemIndex].Enabled = false;
                    }
                }

                var lista = ConsultaCredito.ObtenAcciones(CveEstatus, idRol);
                var listaAccionesUsuario = ConsultaCredito.ObtenAccionesUsuario(CveEstatus, idUsuario);

                if (lista.Count > 0)
                {
                    if (listaAccionesUsuario.Count > 0)
                    {
                        foreach (var accionUsuario in listaAccionesUsuario)
                        {
                            if (lista.FindAll(me => me.ID_Acciones == accionUsuario.IdAccion).Count > 0)
                            {
                                var accionRol = lista.Find(me => me.ID_Acciones == accionUsuario.IdAccion);

                                if (!accionUsuario.EstatusAccion)
                                    lista.Remove(accionRol);
                                else
                                {
                                    var accion = new CAT_ACCIONES();
                                    accion.ID_Acciones = Convert.ToByte(accionUsuario.IdAccion);
                                    accion.Nombre_Accion = accionUsuario.NombreAccion;
                                    accion.Estatus = accionUsuario.EstatusAccion;

                                    lista.Add(accion);
                                }

                                if (accionUsuario.EstatusAccion == accionRol.Estatus)
                                    lista.Remove(accionRol);
                            }
                            else
                            {
                                if (accionUsuario.EstatusAccion)
                                {
                                    var accion = new CAT_ACCIONES();
                                    accion.ID_Acciones = Convert.ToByte(accionUsuario.IdAccion);
                                    accion.Nombre_Accion = accionUsuario.NombreAccion;
                                    accion.Estatus = accionUsuario.EstatusAccion;

                                    lista.Add(accion);
                                }
                            }
                        }
                    }

                    if (SubEstaciones.ClassInstance.EsSubestaciones(credito))
                    {
                        if (SubEstaciones.ClassInstance.HayRegistroDeNuevoRPUDist(credito))
                        {
                            if (SubEstaciones.ClassInstance.HayRegistroDeNuevoRPUJefeZona(credito))
                            {
                                var registro = SubEstaciones.ClassInstance.RPUActivo(credito);

                                if (registro)
                                {
                                    lista.RemoveAll(me => me.ID_Acciones == 8);
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                                                                        "El RPU se encuentra Activo",
                                                                        " alert('El RPU ya esta activo');", true);
                                }
                                else
                                {
                                    lista.RemoveAll(me => me.ID_Acciones == 8);
                                    lista.RemoveAll(me => me.ID_Acciones == 12);
                                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof (Page),
                                                                        "El RPU no se encuentra Activo",
                                                                        " alert('El RPU aun no esta activo');", true);
                                }
                            }
                        }
                        else
                        {
                            lista.RemoveAll(me => me.ID_Acciones == 8);
                            lista.RemoveAll(me => me.ID_Acciones == 12);
                        }
                    }
                    else
                    {
                        lista.RemoveAll(me => me.ID_Acciones == 8);
                    }

                    combo.DataSource = lista;
                    combo.DataValueField = "ID_Acciones";
                    combo.DataTextField = "Nombre_Accion";
                    combo.DataBind();
                    combo.Items.Insert(0, "Elegir Opcion");
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.DataSource = listaAccionesUsuario.FindAll(me => me.EstatusAccion);
                    combo.DataValueField = "IdAccion";
                    combo.DataTextField = "NombreAccion";
                    combo.DataBind();

                    combo.Items.Insert(0, "Elegir Opcion");
                    combo.SelectedIndex = 0;
                }
            }

        }

        protected void gvCredit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_Aceptar_Click(object sender, EventArgs e)
        {
            GridViewRow row = null;

            foreach (GridViewRow item in gvCredit.Rows)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }

            var textoLink = row.Cells[0].Controls[0] as HyperLink;
            var credito = textoLink.Text;

            if (SubEstaciones.ClassInstance.EsSubestaciones(credito) && (((DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).SelectedValue == "8"))
            {
                //if (((DropDownList)grdCredit.Rows[Row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).SelectedItem.Text == "Editar RPU" )
                //{
                //    //Enviar credito a Editar RPU
                //    Response.Redirect("CapturaDatosSolicitud.aspx?Token=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(Credito)));
                //}
                Response.Redirect("CambiarRPUZona.aspx?Token=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(credito)));
                //enviar credito y Cambiar RPU                
            }

            else switch (((DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "2":
                    Response.Redirect("CapturaDatosSolicitud.aspx?Token=" + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(credito)));
                    break;
                case "12":
                    btnRevision_Click(sender, e);
                    break;
                case "14":
                    btnAutorizado_Click(sender, e);
                    break;
                case "13":
                    btnRechazado_Click(sender, e);
                    break;
                default:
                    if (((DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).SelectedItem.Text == @"Pendiente")
                    {
                        btnPendiente_Click(sender, e);
                    }

                    else switch (((DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).SelectedValue)
                    {
                        case "15"://9
                        {
                            var textoLink1 = row.Cells[Nc].Controls[0] as HyperLink;
                            var creditNumber = textoLink1.Text;
                            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", "window.open('PrintForm2.aspx?ReportName=Acuse_Recibo&CreditNumber=" + creditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
                        }
                            break;
                        case "16":
                            PreAutorizaCredito();
                            break;
                        case "10":
                            Response.Redirect("~/MRV/AdmonMRV.aspx?Token=" +
                                              Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(credito)));
                            break;
                    }
                    break;
            }
        }

        protected void btn_NuevaSolicitud_Click(object sender, EventArgs e)
        {

        }

        protected void LSB_Acciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = null;
            var user = (US_USUARIOModel)Session["UserInfo"];
            foreach (GridViewRow item in gvCredit.Rows)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }
            switch (((DropDownList)gvCredit.Rows[row.DataItemIndex].Cells[6].FindControl("LSB_Acciones")).SelectedValue)
            {
                case "7":
                    RadWindow1.OpenerElementID = null;
                    modalPopup.OpenerElementID = btn_Aceptar.ClientID;
                    DDLMotivo.DataSource = CAT_MotivosCancelRechaza.cat_motivos(user.Id_Rol, 7);
                    DDLMotivo.DataTextField = "motivo";
                    DDLMotivo.DataValueField = "id_Motivo";
                    DDLMotivo.DataBind();
                    DDLMotivo.Items.Insert(0, "Seleccione");
                    DDLMotivo.SelectedIndex = 0;
                    break;
                case "13":
                    modalPopup.OpenerElementID = null;
                    RadWindow1.OpenerElementID = btn_Aceptar.ClientID;
                    DDLMotivoRechazo.DataSource = CAT_MotivosCancelRechaza.cat_motivos(user.Id_Rol, 5);
                    DDLMotivoRechazo.DataTextField = "motivo";
                    DDLMotivoRechazo.DataValueField = "id_Motivo";
                    DDLMotivoRechazo.DataBind();
                    DDLMotivoRechazo.Items.Insert(0, "Seleccione");
                    DDLMotivoRechazo.SelectedIndex = 0;
                    break;
                default:
                    RadWindow1.OpenerElementID = null;
                    modalPopup.OpenerElementID = null;
                    break;
            }
        }

        private CANCELAR_RECHAZAR CancelarRechazar(string tipo)
        {
            GridViewRow row = null;

            foreach (GridViewRow item in gvCredit.Rows)
            {
                if (((CheckBox)item.FindControl("ckbSelect")).Checked)
                {
                    row = item;
                }
            }
            var textoLink = row.Cells[0].Controls[0] as HyperLink;
            var credito = textoLink.Text;

            var user = (US_USUARIOModel)Session["UserInfo"];
            var datos = new CANCELAR_RECHAZAR();

            switch (tipo)
            {
                case "Cancelar":
                    datos.No_Credito = credito;
                    datos.ID_MOTIVO = Convert.ToByte(DDLMotivo.SelectedValue);
                    datos.DESCRIPCION_MOTIVO = TexMoti.Text == "" ? null : TexMoti.Text;
                    datos.OBSERVACION = TexObserv.Text == "" ? null : TexObserv.Text;
                    datos.ADICIONADO_POR = user.Nombre_Usuario;
                    datos.FECHA_ADICION = DateTime.Now;
                    break;
                case "Rechazar":
                    datos.No_Credito = credito;
                    datos.ID_MOTIVO = Convert.ToByte(DDLMotivoRechazo.SelectedValue);
                    datos.DESCRIPCION_MOTIVO = TexMotiRechazo.Text == "" ? null : TexMotiRechazo.Text;
                    datos.OBSERVACION = TexObservRechazo.Text == "" ? null : TexObservRechazo.Text;
                    datos.ADICIONADO_POR = user.Nombre_Usuario;
                    datos.FECHA_ADICION = DateTime.Now;
                    break;
            }
            return datos;
        }

  

        protected void btnFinalizarRechazo_Click(object sender, EventArgs e)
        {
            int iCount = 0, iSucess = 0;
            bool bIsSelected;

            #region Check If Have Records Be Seleted

            for (var i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    iCount++;
                }
            }
            #endregion

            #region Cancel The Selected Credits

            for (var i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (!bIsSelected) continue;

                var creditNumber = gvCredit.DataKeys[i].Value.ToString();

                var iResult = 0;

                using (var scope = new TransactionScope())
                {
                   var creditoLog =CancelarRechazar(((DropDownList) gvCredit.Rows[i].Cells[6].FindControl("LSB_Acciones")).SelectedItem.Text);

                    if (SolicitudCreditoAcciones.Agregarcancel(creditoLog) != null)
                    {
                        iResult = K_CREDITODal.ClassInstance.RechazarCredit(creditNumber, Session["UserName"].ToString());
                        if (iResult > 0)
                        {
                            var motivos =
                                SolicitudCreditoAcciones.ObtenEstatusCreditoCancelarRechazar(creditoLog.ID_MOTIVO);
                            switch (motivos.Cve_Estatus_Credito)
                            {
                                case 5: //RECHAZADO
                                    /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO RECHAZADO EN SOLICITUD DE CREDITO*/
                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                                        Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                                        "SOLICITUD DE CREDITO", "RECHAZADO", creditoLog.No_Credito,
                                        motivos.ID_MOTIVO == 40
                                             ? motivos.DESCRIPCION + ": " + creditoLog.DESCRIPCION_MOTIVO
                                            : motivos.DESCRIPCION + ": " + creditoLog.OBSERVACION, "", "", "");
                                    break;

                                case 7: //CANCELADO
                                    /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO CANCELADO EN SOLICITUD DE CREDITO*/
                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                                        Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                                        "SOLICITUD DE CREDITO", "CANCELADO", creditoLog.No_Credito,
                                        motivos.ID_MOTIVO == 39
                                           ? motivos.DESCRIPCION + ": " + creditoLog.DESCRIPCION_MOTIVO
                                            : motivos.DESCRIPCION + ": " + creditoLog.OBSERVACION, "", "", "");
                                    break;
                            }
                            scope.Complete();
                        }
                    }
                }
                if (iResult > 0)
                {
                    iSucess++;
                }
            }

            //if all had been updated successfully
            if (iCount > 0 && iCount == iSucess)
            {
                RadWindowManager1.RadAlert("La Solicitud se ha Rechazado correctamente", 300, 150, "Cancelar Rechazar",
                    null);
            }
            else
            {

                RadWindowManager1.RadAlert("!! La Solicitud NO se ha Rechazado !!", 300, 150, "Cancelar Rechazar", null);
            }

            #endregion

            //Refresh grid view data
            BindGridView();


        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            int iCount = 0, iSucess = 0,corre=0;
            //string strMsg = "";
            bool bIsSelected;

            #region Check If Have Records Be Seleted

            for (var i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                if (bIsSelected)
                {
                    iCount++;
                }
            }
            #endregion

            #region Cancel The Selected Credits

            for (var i = 0; i < gvCredit.Rows.Count; i++)
            {
                bIsSelected = ((CheckBox) gvCredit.Rows[i].FindControl("ckbSelect")).Checked;

                if (!bIsSelected) continue;

                var creditNumber = gvCredit.DataKeys[i].Value.ToString();

                var iResult = 0;
                //   if (this.gvCredit.Rows[i].Cells[5].Text.ToUpper() == "EN REVISION")
                //{
                var textoLink = gvCredit.Rows[i].Cells[0].Controls[0] as HyperLink;
                var credito = textoLink.Text;
                var razon = gvCredit.Rows[i].Cells[1].Text;
                var iRequestAmount = gvCredit.Rows[i].Cells[2].Text == ""
                    ? 0
                    : decimal.Parse(gvCredit.Rows[i].Cells[2].Text.Substring(1));
                using (var scope = new TransactionScope())
                {
                   var creditoLog =
                        CancelarRechazar(
                            ((DropDownList) gvCredit.Rows[i].Cells[6].FindControl("LSB_Acciones")).SelectedItem.Text);
                   if (SolicitudCreditoAcciones.Agregarcancel(creditoLog) != null)
                    {
                        SolicitudCreditoAcciones.IncrementarFonDisponibleEincentivo(iRequestAmount, credito);
                        iResult = K_CREDITODal.ClassInstance.CancelCredit(creditNumber, Session["UserName"].ToString());
                        if (iResult > 0)
                        {
                            try
                            {
                                var dat = SolicitudCreditoAcciones.DatosMotivo(credito);
                                MailUtility.MotivoCanceAdistribuidor("MotivoCancelacion.html", dat[0].addres,
                                    dat[0].usuario,
                                    credito, dat[0].rpu, razon,
                                    DDLMotivo.SelectedValue == "39" ? TexMoti.Text : DDLMotivo.SelectedItem.Text);
                            }
                            catch (Exception)
                            {

                                corre++;
                            }

                            var motivos =
                                SolicitudCreditoAcciones.ObtenEstatusCreditoCancelarRechazar(creditoLog.ID_MOTIVO);
                            switch (motivos.Cve_Estatus_Credito)
                            {
                                case 5: //RECHAZADO
                                    /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO RECHAZADO EN SOLICITUD DE CREDITO*/
                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                                        Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                                        "SOLICITUD DE CREDITO", "RECHAZADO", creditoLog.No_Credito,
                                        motivos.ID_MOTIVO == 40
                                            ? motivos.DESCRIPCION + ": " + creditoLog.DESCRIPCION_MOTIVO
                                            : motivos.DESCRIPCION + ": " + creditoLog.OBSERVACION, "", "", "");
                                    break;

                                case 7: //CANCELADO
                                    /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO CANCELADO EN SOLICITUD DE CREDITO*/
                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                                        Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                                        "SOLICITUD DE CREDITO", "CANCELADO", creditoLog.No_Credito,
                                        motivos.ID_MOTIVO == 39 ? motivos.DESCRIPCION + ": " + creditoLog.DESCRIPCION_MOTIVO
                                            : motivos.DESCRIPCION + ": " + creditoLog.OBSERVACION, "", "", "");
                                    break;
                            }
                            scope.Complete();
                        }
                    }
                }

                if (iResult > 0)
                {
                    iSucess++;
                }

            }
            //if all had been updated successfully
            if (iCount > 0 && iCount == iSucess)
            {
                RadWindowManager1.RadAlert("La solicitud se ha Cancelado correctamente", 300, 150, "Cancelar Rechazar", null);
                if (corre > 0)
                {
                    RadWindowManager1.RadAlert("PROBLEMAS CON EL CORREO!!", 300, 150, "Send email.", null);
                }
            }
            else
            {
                RadWindowManager1.RadAlert("!! La solicitud no se ha Cancelado !!", 300, 150, "Cancelar Rechazar", null);
            }
            #endregion

            //Refresh grid view data
            BindGridView();


        }

        protected void PreAutorizaCredito()
        {
            rwmVentana.RadConfirm(
                    "¿Esta seguro de Enviar la solicitud al estatus Pre Autorizado?",
                    "confirmCallBackFn", 300, 100, null, "Cambio de Estatus");
        }

        protected void hidBtnPreAutorizar_Click(object sender, EventArgs e)
        {
            var noCredito = "";

            #region Verificar que hay un registro seleccionado y Actualizar Estatus

            try
            {
                for (var i = 0; i < gvCredit.Rows.Count; i++)
                {
                    var bIsSelected = ((CheckBox)gvCredit.Rows[i].FindControl("ckbSelect")).Checked;
                    if (bIsSelected)
                    {
                        noCredito = gvCredit.DataKeys[i].Value.ToString();
                    }
                }

                var credito = blCredito.Obtener(noCredito);

                if (credito == null) return;

                var user = (US_USUARIOModel)Session["UserInfo"];

                credito.Cve_Estatus_Credito = Convert.ToByte(CreditStatus.PRE_AUTORIZADO);
                credito.Fecha_Pre_Autorizado = DateTime.Now.Date;
                credito.Fecha_Ultmod = DateTime.Now.Date;
                credito.Usr_Ultmod = user.Nombre_Usuario;

                if (!blCredito.Actualizar(credito)) return;

                /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO PRE-AUTORIZADO EN SOLICITUD DE CREDITO*/
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                    "SOLICITUD DE CREDITO", "CAMBIO DE ESTATUS A PRE-AUTORIZADO", credito.No_Credito,
                    "", "", "", "");

                BindGridView();
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "updateRevisionSucess",
                                                    "alert('Ocurrió un problema al Pre Autorizar la solicitud');", true);
            }


            #endregion
        }
    }
}
