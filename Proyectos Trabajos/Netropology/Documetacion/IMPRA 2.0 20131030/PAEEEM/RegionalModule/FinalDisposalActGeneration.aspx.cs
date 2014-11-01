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
    public partial class FinalDisposalActGeneration : System.Web.UI.Page
    {
        // Added by tina 2012-2-16
        #region Global Variables
        /// <summary>
        /// property
        /// </summary>
        private string MaterialRecoveryActNumber
        {
            get 
            {
                return ViewState["MaterialRecoveryActNumber"] == null ? "" : ViewState["MaterialRecoveryActNumber"].ToString();
            }
            set
            {
                ViewState["MaterialRecoveryActNumber"] = value;
            }
        }
        private string UnableEquipmentActNumber
        {
            get
            {
                return ViewState["UnableEquipmentActNumber"] == null ? "" : ViewState["UnableEquipmentActNumber"].ToString();
            }
            set
            {
                ViewState["UnableEquipmentActNumber"] = value;
            }
        }
        private string FromDate
        {
            get
            {
                return ViewState["FromDate"] == null ? "" : ViewState["FromDate"].ToString();
            }
            set
            {
                ViewState["FromDate"] = value;
            }
        }
        private string ToDate
        {
            get
            {
                return ViewState["ToDate"] == null ? "" : ViewState["ToDate"].ToString();
            }
            set
            {
                ViewState["ToDate"] = value;
            }
        }
        private string Program
        {
            get
            {
                return ViewState["Program"] == null ? "" : ViewState["Program"].ToString();
            }
            set
            {
                ViewState["Program"] = value;
            }
        }
        private string Technology
        {
            get
            {
                return ViewState["Technology"] == null ? "" : ViewState["Technology"].ToString();
            }
            set
            {
                ViewState["Technology"] = value;
            }
        }
        private string DisposalCenter // added by tina 2012-02-27
        {
            get
            {
                return ViewState["DisposalCenter"] == null ? "" : ViewState["DisposalCenter"].ToString();
            }
            set
            {
                ViewState["DisposalCenter"] = value;
            }
        }
        #endregion

        #region Initialize Components
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
                //Initialize components         
                InitializeComponents();

                btnSave.Enabled = false;
            }
        }
        /// <summary>
        /// Initial Drop Down list
        /// </summary>
        private void InitializeComponents()
        {
            ShoworHideRegisterAction(); 
            InitializePrograms();
            //InitializeDateControls();
            // updated by tina 2012-02-27
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                if (UserModel.Tipo_Usuario == GlobalVar.REGIONAL_OFFICE || UserModel.Tipo_Usuario == GlobalVar.ZONE_OFFICE)
                {
                    InitializeDisposalCenters();
                }
            }
            InitializeTechnologies();
            // end

            // RSA called again after technologies have been set
            InitializeDateControls();
        }
        /// <summary>
        /// set registry button display or not
        /// </summary>
        private void ShoworHideRegisterAction()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                //if the login user is disposal center operator then disappears the register button
                if (UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER || UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER_BRANCH)
                {
                    btnSave.Visible = false;
                    // added by tina 2012-02-27
                    lblDisposal.Visible = false;
                    drpDisposal.Visible = false;
                    // end
                }
            }
        }
        /// <summary>
        /// Initial Program dropdownlist
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
        /// Initial Technology DropDownList
        /// </summary>
        private void InitializeTechnologies()
        {
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            DataTable technologies = null;
            string program = drpProgram.SelectedIndex == -1 ? "" : drpProgram.SelectedValue;
            // added by tina 2012-02-27
            string disposalType = "";
            int disposalId = 0;
            if (this.drpDisposal.SelectedIndex != -1)
            {
                if (this.drpDisposal.Text.Contains("Matriz")) // updated by tina 2012-02-29
                {
                    disposalType = "M";
                }
                else
                {
                    disposalType = "B";
                }
                disposalId = Convert.ToInt32(drpDisposal.SelectedValue.Substring(0, drpDisposal.SelectedValue.IndexOf('-')));
            }
            // end

            if (UserModel != null)
            {
                if (UserModel.Tipo_Usuario == GlobalVar.REGIONAL_OFFICE || UserModel.Tipo_Usuario == GlobalVar.ZONE_OFFICE) // regional user or zone user
                {
                    technologies = CAT_TECNOLOGIADAL.ClassInstance.GetTechnologyWithProgramandDisposalCenter(program, disposalId, disposalType); // updated by tina 2012-02-27
                }
                else // disposal user
                {
                    if (program != "")
                    {
                        technologies = CAT_TECNOLOGIADAL.ClassInstance.GetTechnologyWithProgramandDisposalCenter(program, UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
                    }
                    else
                    {
                        technologies = CAT_TECNOLOGIADAL.ClassInstance.GetDisposalCenterRelatedTechnology(UserModel.Id_Departamento, UserModel.Tipo_Usuario == GlobalVar.DISPOSAL_CENTER ? "M" : "B");
                    }
                }

                if (technologies != null)
                {
                    drpTechnology.DataSource = technologies;
                    drpTechnology.DataTextField = "Dx_Nombre_General";
                    drpTechnology.DataValueField = "Cve_Tecnologia";
                    drpTechnology.DataBind();
                }
            }
        }
        /// <summary>
        /// set default date for date time controls
        /// </summary>
        private void InitializeDateControls()
        {
            string fromDate = "";
            if (drpActType.SelectedValue == "0")
            {
                fromDate = K_ACTA_RECUPERACIONDal.ClassInstance.GetFromDateForFinalActOrSupervision(drpTechnology.SelectedValue);
            }
            else
            {
                fromDate = K_ACTA_INHABILITACIONDal.ClassInstance.GetFromDateForFinalActOrUnable(drpTechnology.SelectedValue);
            }
            txtFromDate.Text = fromDate != "" ? fromDate : DateTime.Now.ToString("yyyy-MM-dd");

            txtToDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        }
        // added by tina 2012-02-27
        /// <summary>
        /// Initial Disposal Center DropDownList
        /// </summary>
        private void InitializeDisposalCenters()
        {
            DataTable dtDisposalCenter = null;
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
            if (UserModel != null)
            {
                dtDisposalCenter = CAT_CENTRO_DISPDAL.ClassInstance.GetDisposalCenterAndBranchByUser(UserModel.Tipo_Usuario == GlobalVar.REGIONAL_OFFICE ? "R" : "Z", UserModel.Id_Departamento);
                if (dtDisposalCenter != null)
                {
                    this.drpDisposal.DataSource = dtDisposalCenter;
                    this.drpDisposal.DataTextField = "Dx_Razon_Social";
                    this.drpDisposal.DataValueField = "Id_Centro_Disp";
                    this.drpDisposal.DataBind();
                }
            }
        }
        // end
        #endregion

        #region Refresh conditions when filter changed
        protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeTechnologies();
        }

        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fromDate = "";
            if (drpActType.SelectedValue == "0")
            {
                fromDate = K_ACTA_RECUPERACIONDal.ClassInstance.GetFromDateForFinalActOrSupervision(drpTechnology.SelectedValue);
            }
            else
            {
                fromDate = K_ACTA_INHABILITACIONDal.ClassInstance.GetFromDateForFinalActOrUnable(drpTechnology.SelectedValue);
            }
            txtFromDate.Text = fromDate != "" ? fromDate : DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion

        /// <summary>
        /// Display final disposal act format selected
        /// </summary>
        /// <param name="sender">Target Object Triggered Event</param>
        /// <param name="e">Event Argument</param>
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            
            US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;

            // Added by Tina 2012-2-16
            if (IsFilterConditionChanged())
            {
                GenerateActNumber();
                SaveFilterCondition();
            }

            //: Call reporting service to display the associated format file by selected act type
            if (drpActType.SelectedValue != "0") // Report of Materials Recovery
            {
                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=ActaCircunst_ResyMat_Recuperados&MaterialRecoveryActNumber=" + MaterialRecoveryActNumber + "&FI=" + txtFromDate.Text + "&FF=" + txtToDate.Text + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Recuperados_Preview&FI=" + txtFromDate.Text + "&FF=" + txtToDate.Text + "&Act=" + UserModel.Id_Usuario + "&CD=" + Convert.ToInt32(drpDisposal.SelectedValue.Substring(0, drpDisposal.SelectedValue.IndexOf('-'))) + "&Tec=" + drpTechnology.SelectedValue + "&Prog=" + drpProgram.SelectedValue + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            }
            else // Report of Old Equipment Disabled
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=ActaCircunst_InhabyDestruccion_Preview&FI=" + txtFromDate.Text + "&FF=" + txtToDate.Text + "&Act=" + UserModel.Id_Usuario + "&CD=" + Convert.ToInt32(drpDisposal.SelectedValue.Substring(0, drpDisposal.SelectedValue.IndexOf('-'))) + "&Tec=" + drpTechnology.SelectedValue + "&Prog=" + drpProgram.SelectedValue + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            }

            btnSave.Enabled = true;
        }

        /// <summary>
        /// Save the act related data
        /// </summary>
        /// <param name="sender">Target Object Triggered Event</param>
        /// <param name="e">Event Argument</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (drpDisposal.SelectedIndex == -1){ return; }

            int result = 0;
            if (drpActType.SelectedValue != "0")
            {
                //Get the supervision products for saving
                DataTable dtSupervisionProducts = GetSupervisionProducts();
                if (dtSupervisionProducts != null && dtSupervisionProducts.Rows.Count > 0)
                {
                    if (IsFilterConditionChanged())
                    {
                        GenerateActNumber();
                        SaveFilterCondition();
                    }

                    ArrayList arrListRecuperacion = null;
                    K_ACTA_RECUPERACIONModel modelRecupercion = null;
                    //get data for saving
                    arrListRecuperacion = GetRecuperacions(dtSupervisionProducts);
                    modelRecupercion = GetMaterialRecoveryData();
                    modelRecupercion.Id_Acta_Recuperacion = MaterialRecoveryActNumber;

                    //save in batch
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew))
                    {
                        result = K_ACTA_RECUPERACIONDal.ClassInstance.Insert_K_ACTA_RECUPERACION(modelRecupercion);
                        result += K_RECUPERACIONBLL.ClassInstance.UpdateFinalActID(arrListRecuperacion, modelRecupercion.Id_Acta_Recuperacion);

                        scope.Complete();
                    }
                }
            }
            else
            {
                DataTable dtUnableProductsForAct = GetUnableProductsForAct();
                if (dtUnableProductsForAct != null && dtUnableProductsForAct.Rows.Count > 0)
                {
                    if (IsFilterConditionChanged())
                    {
                        GenerateActNumber();
                        SaveFilterCondition();
                    }

                    ArrayList arrListInhabilitacion = null;
                    K_ACTA_INHABILITACIONModel modelInhabilitation = null;
                    //get data for saving
                    arrListInhabilitacion = GetInhabilitacions(dtUnableProductsForAct);
                    modelInhabilitation = GetEquipmentUnableData();
                    modelInhabilitation.Id_Acta_Inhabilitacion = UnableEquipmentActNumber;

                    //save in batch
                    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew))
                    {
                        result += K_ACTA_INHABILITACIONDal.ClassInstance.Insert_K_ACTA_INHABILITACION(modelInhabilitation);
                        result += K_INHABILITACIONBLL.ClassInstance.UpdateFinalActID(arrListInhabilitacion, modelInhabilitation.Id_Acta_Inhabilitacion); // added by Tina 2012-02-20

                        scope.Complete();
                    }
                }
            }

            if (result >= 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveSucess", "confirm('El acta circunstanciada ha sido guardada exitosamente.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveError", "confirm('No existen equipos para incluir en acta circunstanciada.');", true);
            }
        }

        /// <summary>
        /// Return Main menu
        /// </summary>
        /// <param name="sender">Target Object Triggered Event</param>
        /// <param name="e">Event Argument</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../default.aspx");
        }

        // added by tina 2012-02-27
        protected void drpDisposal_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeTechnologies();
        }

        #region Private Methods
        /// <summary>
        /// get data for save
        /// </summary>
        /// <returns></returns>
        private DataTable GetSupervisionProducts()
        {
            DataTable dtSupervisionProducts = null;
            try
            {
                US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
                if (UserModel != null)
                {
                    //filter conditions
                    string program = this.drpProgram.SelectedIndex == -1 ? "" : this.drpProgram.SelectedValue;
                    string technology = this.drpTechnology.SelectedIndex == -1 ? "" : this.drpTechnology.SelectedValue;
                    string fromDate = txtFromDate.Text.Trim();
                    string toDate = txtToDate.Text.Trim();
                    //Get UserID and UserType
                    int relatedRoleId = UserModel.Id_Departamento;

                    // updated by tina 2012-02-27
                    string disposalType = "";
                    string disposalId = "";
                    if (this.drpDisposal.SelectedIndex != -1)
                    {
                        if (this.drpDisposal.Text.Contains("Matriz")) // updated by tina 2012-02-29
                        {
                            disposalType = "M";
                        }
                        else
                        {
                            disposalType = "B";
                        }
                        disposalId = drpDisposal.SelectedValue.Substring(0, drpDisposal.SelectedValue.IndexOf('-'));
                    }

                    dtSupervisionProducts = K_RECUPERACION_PRODUCTODal.ClassInstance.GetSupervisionProducts(program, technology, fromDate, toDate, relatedRoleId, UserModel.Tipo_Usuario == GlobalVar.REGIONAL_OFFICE ? "R" : "Z", disposalId, disposalType);
                    // end
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "RegisterException", "alert('Generación Acta Circunstanciada Fail:" + ex.Message + "');", true);
            }
            return dtSupervisionProducts;
        }

        private DataTable GetUnableProductsForAct() // added by Tina 2012-02-20
        {
            DataTable dtUnableProductsForAct = null;
            try
            {
                US_USUARIOModel UserModel = Session["UserInfo"] as US_USUARIOModel;
                if (UserModel != null)
                {
                    //filter conditions
                    string program = this.drpProgram.SelectedIndex == -1 ? "" : this.drpProgram.SelectedValue;
                    string technology = this.drpTechnology.SelectedIndex == -1 ? "" : this.drpTechnology.SelectedValue;
                    string fromDate = txtFromDate.Text.Trim();
                    string toDate = txtToDate.Text.Trim();
                    //Get UserID and UserType
                    int relatedRoleId = UserModel.Id_Departamento;

                    // updated by tina 2012-02-27
                    string disposalType = "";
                    string disposalId = "";
                    if (this.drpDisposal.SelectedIndex != -1)
                    {
                        if (this.drpDisposal.Text.Contains("Matriz")) // updated by tina 2012-02-29
                        {
                            disposalType = "M";
                        }
                        else
                        {
                            disposalType = "B";
                        }
                        disposalId = drpDisposal.SelectedValue.Substring(0, drpDisposal.SelectedValue.IndexOf('-'));
                    }

                    dtUnableProductsForAct = K_INHABILITACION_PRODUCTODal.ClassInstance.GetUnableProductsForAct(program, technology, fromDate, toDate, relatedRoleId, UserModel.Tipo_Usuario == GlobalVar.REGIONAL_OFFICE ? "R" : "Z", disposalId, disposalType);
                    // end
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.panel, typeof(Page), "RegisterException", "alert('Generación Acta Circunstanciada Fail:" + ex.Message + "');", true);
            }
            return dtUnableProductsForAct;
        }

        private K_ACTA_RECUPERACIONModel GetMaterialRecoveryData()
        {
            K_ACTA_RECUPERACIONModel modelRecupercion = new K_ACTA_RECUPERACIONModel();
            
            //get recuperación data            
            if (txtFromDate.Text.Trim() != "")
            {
                modelRecupercion.Dt_Fe_Inicio_Recup = Convert.ToDateTime(txtFromDate.Text.Trim());
            }
            if (txtToDate.Text.Trim() != "")
            {
                modelRecupercion.Dt_Fe_Fin_Recup = Convert.ToDateTime(txtToDate.Text.Trim());
            }

            modelRecupercion.Dt_Fe_Creacion = DateTime.Now.Date;

            return modelRecupercion;
        }

        private K_ACTA_INHABILITACIONModel GetEquipmentUnableData()
        {
            K_ACTA_INHABILITACIONModel modelInhabilitation = new K_ACTA_INHABILITACIONModel();

            //get inhabilitación data            
            if (txtFromDate.Text.Trim() != "")
            {
                modelInhabilitation.Dt_Fe_Inicio_Inhabilita = Convert.ToDateTime(txtFromDate.Text.Trim());
            }

            if (txtToDate.Text.Trim() != "")
            {
                modelInhabilitation.Dt_Fe_Fin_Inhabilita = Convert.ToDateTime(txtToDate.Text.Trim());
            }
            modelInhabilitation.Dt_Fe_Creacion = DateTime.Now.Date;

            return modelInhabilitation;
        }

        private ArrayList GetRecuperacions(DataTable dtSupervisionProducts)
        {
            ArrayList arrListRecuperacion = new ArrayList();
            foreach (DataRow row in dtSupervisionProducts.Rows)
            {
                arrListRecuperacion.Add(row["Id_Recuperacion"].ToString());
            }

            return arrListRecuperacion;
        }

        private ArrayList GetInhabilitacions(DataTable dtUnableProductsForAct) // added by Tina 2012-02-20
        {
            ArrayList arrListInhabilitacion = new ArrayList();
            foreach (DataRow row in dtUnableProductsForAct.Rows)
            {
                arrListInhabilitacion.Add(row["Id_Inhabilitacion"].ToString());
            }

            return arrListInhabilitacion;
        }

        // added by tina 2012-2-16
        private void GenerateActNumber()
        {
            int consequenceNumber = 0;
            string materialRecoveryActNumber, unableEquipmentActNumber;

            //Get sequence number of acts
            consequenceNumber = LsUtility.GetNumberSequence("FINIALACT");

            //get technology short name code
            CAT_TECNOLOGIAModel modelTechnology = CAT_TECNOLOGIADAL.ClassInstance.Get_CAT_TECNOLOGIAByPKID(drpTechnology.SelectedValue);
            string technologyShortName = modelTechnology.Dx_Cve_CC;

            //build material recovery act number and unable equipment act number separately
            materialRecoveryActNumber = DateTime.Now.ToString("yyyyMMdd") + "-" + technologyShortName + string.Format("{0:D3}", consequenceNumber) + "-R";
            unableEquipmentActNumber = DateTime.Now.ToString("yyyyMMdd") + "-" + technologyShortName + string.Format("{0:D3}", consequenceNumber) + "-I";

            MaterialRecoveryActNumber = materialRecoveryActNumber;
            UnableEquipmentActNumber = unableEquipmentActNumber;
        }

        private bool IsFilterConditionChanged()
        {
            bool result = false;

            // updated by tina 2012-02-27
            if (FromDate != txtFromDate.Text || ToDate != txtToDate.Text || Program != drpProgram.SelectedValue || Technology != drpTechnology.SelectedValue || DisposalCenter != drpDisposal.SelectedValue)
            {
                result = true;
            }

            return result;
        }

        private void SaveFilterCondition()
        {
            FromDate = txtFromDate.Text.Trim();
            ToDate = txtToDate.Text.Trim();
            Program = drpProgram.SelectedValue;
            Technology = drpTechnology.SelectedValue;
            DisposalCenter = drpDisposal.SelectedValue; // added by tina 2012-02-27
        }

        protected void drpActType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fromDate = "";
            if (drpActType.SelectedValue == "0")
            {
                fromDate = K_ACTA_RECUPERACIONDal.ClassInstance.GetFromDateForFinalActOrSupervision(drpTechnology.SelectedValue);
            }
            else
            {
                fromDate = K_ACTA_INHABILITACIONDal.ClassInstance.GetFromDateForFinalActOrUnable(drpTechnology.SelectedValue);
            }
            txtFromDate.Text = fromDate != "" ? fromDate : DateTime.Now.ToString("yyyy-MM-dd");
        }
         // end
        #endregion
    }
}
