using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services.Description;
using System.Xml.Linq;
using System.Transactions;
using System.Text.RegularExpressions;
using PAEEEM.BussinessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM
{
    public partial class ValidateCrediticialHistory : System.Web.UI.Page
    {
        #region Define Global Variables
        public int TechnologyGroupNumber
        {
            get
            {
                return ViewState["TechnologyGroupNumber"] == null ? 0 : int.Parse(ViewState["TechnologyGroupNumber"].ToString());
            }
            set
            {
                ViewState["TechnologyGroupNumber"] = value;
            }
        }
        public string CreditNumber
        {
            get
            {
                return ViewState["CreditNumber"] == null ? "" : ViewState["CreditNumber"].ToString();//Changed by Jerry 2011/08/12
            }
            set
            {
                ViewState["CreditNumber"] = value;
            }
        }
        public int ID_Prog_Proy
        {
            get
            {
                return ViewState["ID_Prog_Proy"] == null ? 1 : int.Parse(ViewState["ID_Prog_Proy"].ToString());
            }
            set
            {
                ViewState["ID_Prog_Proy"] = value;
            }
        }
        public DataTable GridViewDetail
        {
            get
            {
                return ViewState["GridViewDetail"] == null ? null : ViewState["GridViewDetail"] as DataTable;
            }
            set
            {
                ViewState["GridViewDetail"] = value;
            }
        }
        public DataTable CreditDetail
        {
            get
            {
                return ViewState["CreditDetail"] == null ? null : ViewState["CreditDetail"] as DataTable;
            }
            set
            {
                ViewState["CreditDetail"] = value;
            }
        }
        public decimal RequestAmount
        {
            get
            {
                return ViewState["RequestAmount"] == null ? 0 : decimal.Parse(ViewState["RequestAmount"].ToString());
            }
            set
            {
                ViewState["RequestAmount"] = value;
            }
        }
        // Add by Tina 2011/08/03
        public int Credit_Status
        {
            get
            {
                return ViewState["Credit_Status"] == null ? 1 : int.Parse(ViewState["Credit_Status"].ToString());
            }
            set
            {
                ViewState["Credit_Status"] = value;
            }
        }
        // End
        // Comment by Tina 2011/08/09
        // Add by Tina 2011/08/04
        public int Tipo_Sociedad
        {
            get
            {
                return ViewState["Tipo_Sociedad"] == null ? 1 : int.Parse(ViewState["Tipo_Sociedad"].ToString());
            }
            set
            {
                ViewState["Tipo_Sociedad"] = value;
            }
        }
        // End
        // Add by Tina 2011/08/08
        public Dictionary<string,string[]> Dic_Auxiliar
        {
            get
            {
                return ViewState["Dic_Auxiliar"] == null ? new Dictionary<string, string[]>() : (Dictionary<string,string[]>)ViewState["Dic_Auxiliar"];
            }
            set
            {
                ViewState["Dic_Auxiliar"] = value;
            }
        }
        // End
        // Added by Tina 2011/08/24
        public string Requested_Technology
        {
            get
            {
                return ViewState["Requested_Technology"] == null ? "" : ViewState["Requested_Technology"].ToString();
            }
            set
            {
                ViewState["Requested_Technology"] = value;
            }
        }
        // End
        public bool Request_Saved
        {
            get
            {
                return ViewState["Request_Saved"] == null ? false : Convert.ToBoolean(ViewState["Request_Saved"]);
            }
            set
            {
                ViewState["Request_Saved"] = value;
            }
        }
        #endregion

        #region Load Related Events
        /// <summary>
        /// Init default data when page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["CreditNumber"] != null && Request.QueryString["CreditNumber"].ToString() != "")
                {
                    //Changed by Jerry 2011/08/12
                    CreditNumber = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["CreditNumber"].ToString().Replace("%2B", "+")));
                }
                // Add by Tina 2011/08/11
                if (Request.QueryString["Flag"] != null && Request.QueryString["Flag"].ToString() != "")
                {
                    //Button startPrevious = wizardPages.FindControl("StartNavigationTemplateContainerID").FindControl("StartPreviousButton") as Button;
                    //startPrevious.Visible = true;
                    Credit_Status = (int)CreditStatus.PENDIENTE;
                }
                // End
                // Button for Preboleta (once the disposal center user received the old equipments) FRR
                //DataTable dt = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONByNo_CreditoFolio(CreditNumber);
                //if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value && Convert.ToInt16(dt.Rows[0][0]) > 0)
                //{
                //    btnPrint.Visible = true;

                //}
               


                //END
                // Initial default data
                InitDefaultData();

                // 2013-05-17 Do not allow to make changes, if it's already in "Por Entregar", go straight to Print page
                if (Credit_Status == (int)CreditStatus.PORENTREGAR)
                {
                    lblTitle.Text = "IMPRIMIR EXPEDIENTE";

                    wizardPages.ActiveStepIndex = 3;
                    // Button btnPrev = (Button)wizardPages.FindControl("FinishNavigationTemplateContainerID$FinishPreviousButton");
                    // btnPrev.Enabled = false;
                    // btnPrev.Visible = false;
                }
            }
            // Update by Tina 2011/08/03
            InitMopValidation();
            if (Request_Saved)
                gdvReplacement.Enabled = false;
            // End
        }
        /// <summary>
        /// Initial default data
        /// </summary>
        private void InitDefaultData()
        {
            // Step Validate
            InitValidateStepDefaultData();
            // Step Integrate
            InitIntegrateStepDefaultData();
            // Step Replacement
            InitReplacementStepDefaultData();
        }
        /// <summary>
        /// Do mop validation initialization
        /// </summary>
        private void InitMopValidation()
        {
            Button startNext = wizardPages.FindControl("StartNavigationTemplateContainerID").FindControl("StartNextButton") as Button;

            DataTable dt = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONByNo_CreditoFolio(CreditNumber);
            DataTable dt2 = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONByNo_Credito(CreditNumber);

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value && Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                btnPrint.Visible = true;
                //btnDisplayEquipmentAct.Enabled = true;
                //btnDisplayDisposalBonusReceipt.Enabled  = true;
                //btnDisplayReceiptToSettle.Enabled = true;
                btnPrint.Enabled = true;
            }

            if (dt2.Rows.Count > 0 && dt2.Rows[0][0] != DBNull.Value && Convert.ToInt16(dt2.Rows[0][0]) > 0)
            {
                //btnPrint.Visible = true;
                btnDisplayEquipmentAct.Enabled = true;
                btnDisplayDisposalBonusReceipt.Enabled = true;
                btnDisplayReceiptToSettle.Enabled = true;
                //btnPrint.Enabled = true;
            }


            if (Credit_Status == (int)CreditStatus.PENDIENTE // Update by Tina 2011/08/09
                /*&& (Tipo_Sociedad != (int)CompanyType.PERSONAFISICA || Tipo_Sociedad != (int)CompanyType.REPECO)*/)
            {
                DataTable dtAuxiliar = CAT_AUXILIARDal.ClassInstance.Get_CAT_AUXILIARByCreditNo(CreditNumber.ToString());
                if (dtAuxiliar != null)
                {
                    if (dtAuxiliar.Rows.Count > 0 && string.IsNullOrEmpty(dtAuxiliar.Rows[0]["No_MOP"].ToString()))// Update by Tina 2011/08/09
                    {
                        char[] Symbol = new char[] { '-'};
                        string CreditNum = CreditNumber.Substring(CreditNumber.LastIndexOfAny(Symbol) + 1);//Changed by Tina 2011/08/12 2011/08/24
                        // Added by Tina 2011/08/24
                        while (CreditNum.Substring(0, 1) == "0")
                        {
                            CreditNum = CreditNum.Substring(1);
                        }
                        // End
                        //if (!CCHelper.tempMops.Keys.Contains<string>(CreditNumber.ToString()))
                        //{
                        //    btnConsultaCrediticia.Attributes.Add("onclick", "var value;  if(confirm('Confirmar realizar Consulta Crediticia')){value = window.showModalDialog('EnterMop.aspx','','dialogWidth:400px;dialogHeight:240px;toolbar:no; resizable:no;status:no;');" +
                        //                                                                   "if(value!=null) { document.getElementById('" + hiddenfield.ClientID + "').innerText=value;} " +
                        //                                                                   "else { document.getElementById('" + hiddenfield.ClientID + "').innerText= 'Cancel'; }}");
                        //}
                        //else
                        {
                            btnConsultaCrediticia.Attributes.Add("onclick", "if (confirm('Confirmar realizar Consulta Crediticia\\n\\nNO podrá editar el crédito después de hacer la consulta crediticia')) {this.style.display ='none'; } else { return false; }");
                        }
                        lblImporte.Visible = true;
                        txtContratoImporte.Visible = true;
                        revContratoImporte.Visible = true;
                        btnConsultaCrediticia.Visible = true;
                        startNext.Enabled = false;
                    }
                    else
                    {
                        // no row, or row with non empty cell
                        lblImporte.Visible = false;
                        txtContratoImporte.Visible = false;
                        revContratoImporte.Visible = false;
                        btnConsultaCrediticia.Visible = false;

                        bool valid = ValidateMOP();

                        startNext.Enabled = valid;
                        //lblMopInvalido.Visible = (int.Parse(mop) > 4);
                        lblMopInvalido.Visible = !valid;
                    }
                    // Add by Tina 2011/08/08
                    if (dtAuxiliar.Rows.Count > 0)
                    {
                        Dic_Auxiliar = new Dictionary<string, string[]>()
                                        {
                                            {CreditNumber.ToString(), 
                                                               new string[]{dtAuxiliar.Rows[0]["Dx_Nombres"].ToString(),dtAuxiliar.Rows[0]["Dx_Apellido_Paterno"].ToString(),
                                                                                  dtAuxiliar.Rows[0]["Dx_Apellido_Materno"].ToString(),dtAuxiliar.Rows[0]["Dt_Nacimiento_Fecha"].ToString(),
                                                                                  "",dtAuxiliar.Rows[0]["Dx_Ciudad"].ToString()}
                                                                                  }
                                        };
                    }
                    else
                    {
                        Dic_Auxiliar = new Dictionary<string, string[]>()
                                        {
                                            {CreditNumber.ToString(), new string[]{"","","","","",""} }
                                        };
                    }
                    // End
                }
                else
                {
                    lblImporte.Visible = false;
                    txtContratoImporte.Visible = false;
                    revContratoImporte.Visible = false;
                    btnConsultaCrediticia.Visible = false;
                    startNext.Enabled = false;
                }
                // End
            }
            //else if (Credit_Status == (int)CreditStatus.PENDIENTE && Tipo_Sociedad != (int)CompanyType.PERSONAFISICA
            //    /*&& Tipo_Sociedad != (int)CompanyType.PERSONAFISICACONACTIVIDADEMPRESARIAL*/)   // RSA persona moral
            //{
            //    btnConsultaCrediticia.Attributes.Add("onclick", "var value;  if(confirm('Confirmar realizar Consulta Crediticia')){value = window.showModalDialog('EnterMop.aspx','','dialogWidth:400px;dialogHeight:240px;toolbar:no; resizable:no;status:no;');" +
            //                                                                   "if(value!=null) { document.getElementById('" + hiddenfield.ClientID + "').innerText=value;} " +
            //                                                                   "else { document.getElementById('" + hiddenfield.ClientID + "').innerText= 'Cancel'; }}");
            //}
            else if (Credit_Status == (int)CreditStatus.RECHAZADO || Credit_Status == (int)CreditStatus.CANCELADO 
                || Credit_Status == (int)CreditStatus.BENEFICIARIO_CON_ADEUDOS || Credit_Status == (int)CreditStatus.TARIFA_FUERA_DE_PROGRAMA
                || Credit_Status == (int)CreditStatus.Calificación_MOP_no_válida)
            {
                // no permite avanzar
                lblImporte.Visible = false;
                txtContratoImporte.Visible = false;
                revContratoImporte.Visible = false;
                btnConsultaCrediticia.Visible = false;
                startNext.Enabled = false;
                lblMopInvalido.Visible = (Credit_Status == (int)CreditStatus.Calificación_MOP_no_válida);
            }
            else //if (Credit_Status != (int)CreditStatus.PENDIENTE)
            {
                //PORENTREGAR = 2,
                //ENREVISION = 3,
                //AUTORIZADO = 4,
                //PARAFINANZAS = 6,

                // si avanza                
                lblImporte.Visible = false;
                txtContratoImporte.Visible = false;
                revContratoImporte.Visible = false;
                btnConsultaCrediticia.Visible = false;
                startNext.Enabled = true;
            }
        }

        private bool ValidateMOP()
        {
            bool valid = false;
            DataTable dtAuxiliar = CAT_AUXILIARDal.ClassInstance.Get_CAT_AUXILIARByCreditNo(CreditNumber.ToString());
            // if no row default to invalid value -1, otherwise use the cell value
            if (dtAuxiliar == null || dtAuxiliar.Rows.Count == 0)
                valid = false;
            else if (CreditDetail != null && CreditDetail.Rows.Count > 0 
                && !string.IsNullOrEmpty(CreditDetail.Rows[0]["Dt_Fecha_Calificación_MOP_no_válida"].ToString()))
                valid = false;
            else if (string.IsNullOrEmpty(dtAuxiliar.Rows[0]["No_MOP"].ToString()))
                valid = false;
            else if (Credit_Status == (int)CreditStatus.Calificación_MOP_no_válida)
                valid = false;
            else
            {
                DataTable dtProgram = CAT_PROGRAMADal.ClassInstance.Get_All_CAT_PROGRAMAByPK(ID_Prog_Proy.ToString());
                int califmop = dtProgram != null && dtProgram.Rows.Count > 0 && dtProgram.Rows[0]["No_Calif_MOP"] != DBNull.Value ? int.Parse(dtProgram.Rows[0]["No_Calif_MOP"].ToString()) : 4;
                string mop = dtAuxiliar.Rows[0]["No_MOP"].ToString();
                valid = (int.Parse(mop) >= 0 && int.Parse(mop) <= califmop);
            }

            return valid;
        }
        #endregion

        #region Load Steps Data
        /// <summary>
        /// Init Validate step Default Data
        /// </summary>
        private void InitValidateStepDefaultData()
        {
            this.txtFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            this.txtCreditoNum.Text = CreditNumber;//Changed by Tina 2011/08/12

            DataTable dtCredito = K_CREDITODal.ClassInstance.GetCredits(CreditNumber);
            CreditDetail = dtCredito;
            if (dtCredito != null && dtCredito.Rows.Count > 0)
            {
                ID_Prog_Proy = int.Parse(dtCredito.Rows[0]["ID_Prog_Proy"].ToString());
                // Add by Tina 2011/08/03
                Credit_Status = int.Parse(dtCredito.Rows[0]["Cve_Estatus_Credito"].ToString());
                // End
                // Comment by Tina 2011/08/09
                // Add by Tina 2011/08/04
                Tipo_Sociedad = int.Parse(dtCredito.Rows[0]["Cve_Tipo_Sociedad"].ToString());
                // End
                this.txtRazonSocial.Text = dtCredito.Rows[0]["Dx_Razon_Social"].ToString();
                if (!Convert.IsDBNull(dtCredito.Rows[0]["Mt_Monto_Solicitado"]))
                {
                    this.txtRequestAmount.Text = string.Format("{0:C2}", dtCredito.Rows[0]["Mt_Monto_Solicitado"]);
                }
                if (!Convert.IsDBNull(dtCredito.Rows[0]["No_Plazo_Pago"] != null))
                {
                    this.txtCreditYearsNumber.Text = dtCredito.Rows[0]["No_Plazo_Pago"].ToString();
                }
                if (!Convert.IsDBNull(dtCredito.Rows[0]["Dx_Periodo_Pago"]) && dtCredito.Rows[0]["Dx_Periodo_Pago"].ToString() != "")
                {
                    this.txtPaymentPeriod.Text = dtCredito.Rows[0]["Dx_Periodo_Pago"].ToString();
                }
                this.radAcquisition.Checked = false;
                this.radReplacement.Checked = true;
            }
            this.txtRequestAmount.Enabled = false;
            this.txtCreditYearsNumber.Enabled = false;
            this.txtPaymentPeriod.Enabled = false;

            // Comment by Tina 2011/08/03
            //Button startNext = wizardPages.FindControl("StartNavigationTemplateContainerID").FindControl("StartNextButton") as Button;
            //startNext.Enabled = false;
            // End
        }

        /// <summary>
        /// Init Integrate step Default Data
        /// </summary>
        private void InitIntegrateStepDefaultData()
        {
            EstadoBind();
            // Comment by Tina 2011/08/17
            //DelegMunicipioBind();

            if (CreditDetail != null && CreditDetail.Rows.Count > 0 /*&& Convert.ToInt32(CreditDetail.Rows[0]["Cve_Estatus_Credito"].ToString()) == (int)CreditStatus.PORENTREGAR*/)
            {
                txtRepresentative_LegalDocumentNumber.Text = CreditDetail.Rows[0]["Dx_No_Escritura_Poder"].ToString();
                if (CreditDetail.Rows[0]["Dt_Fecha_Poder"] != null && CreditDetail.Rows[0]["Dt_Fecha_Poder"].ToString() != "")
                {
                    txtRepresentative_LegalDocumentFecha.Text = Convert.ToDateTime(CreditDetail.Rows[0]["Dt_Fecha_Poder"].ToString()).ToString("yyyy-MM-dd");
                }
                txtRepresentative_NotariesProfessionName.Text = CreditDetail.Rows[0]["Dx_Nombre_Notario_Poder"].ToString();
                txtRepresentative_NotariesProfessionNumber.Text = CreditDetail.Rows[0]["Dx_No_Notario_Poder"].ToString();
                if (CreditDetail.Rows[0]["Cve_Estado_Poder"] != null)
                {
                    drpRepresentative_Estado.SelectedValue = CreditDetail.Rows[0]["Cve_Estado_Poder"].ToString();
                    // Add by Tina 2011/08/08
                    // Comment by Tina 2011/08/17
                    //RepresentativeDelegMunicipioBind();// Filter Deleg_Municipio_Poder by Estado_Poder
                    // End
                }
                // Comment by Tina 2011/08/17
                //if (CreditDetail.Rows[0]["Cve_Deleg_Municipio_Poder"] != null)
                //{
                //    drpRepresentative_OfficeorMunicipality.SelectedValue = CreditDetail.Rows[0]["Cve_Deleg_Municipio_Poder"].ToString();
                //}
                txtApplicant_LegalDocumentNumber.Text = CreditDetail.Rows[0]["Dx_No_Escritura_Acta"].ToString();
                if (CreditDetail.Rows[0]["Dt_Fecha_Acta"] != null && CreditDetail.Rows[0]["Dt_Fecha_Acta"].ToString() != "")
                {
                    txtApplicant_LegalDocumentFecha.Text = Convert.ToDateTime(CreditDetail.Rows[0]["Dt_Fecha_Acta"].ToString()).ToString("yyyy-MM-dd");
                }
                txtApplicant_NotariesProfessionName.Text = CreditDetail.Rows[0]["Dx_Nombre_Notario_Acta"].ToString();
                txtApplicant_NotariesProfessionNumber.Text = CreditDetail.Rows[0]["Dx_No_Notario_Acta"].ToString();
                if (CreditDetail.Rows[0]["Cve_Estado_Acta"] != null)
                {
                    drpApplicant_Estado.SelectedValue = CreditDetail.Rows[0]["Cve_Estado_Acta"].ToString();
                    // Add by Tina 2011/08/08
                    // Comment by Tina 2011/08/17
                    //ApplicantDelegMunicipioBind();// Filter Deleg_Municipio_Acta by Estado_Acta
                    // End
                }
                // Comment by Tina 2011/08/17
                //if (CreditDetail.Rows[0]["Cve_Deleg_Municipio_Acta"] != null)
                //{
                //    drpApplicant_OfficeOrMunicipality.SelectedValue = CreditDetail.Rows[0]["Cve_Deleg_Municipio_Acta"].ToString();
                //}
                txtGuarantee_LegalDocumentNumber.Text = CreditDetail.Rows[0]["Dx_No_Escritura_Aval"].ToString();
                if (CreditDetail.Rows[0]["Dt_Fecha_Escritura_Aval"] != null && CreditDetail.Rows[0]["Dt_Fecha_Escritura_Aval"].ToString() != "")
                {
                    txtGuarantee_LegalDocumentFecha.Text = Convert.ToDateTime(CreditDetail.Rows[0]["Dt_Fecha_Escritura_Aval"].ToString()).ToString("yyyy-MM-dd");
                }
                txtGuarantee_NotariesProfessionName.Text = CreditDetail.Rows[0]["Dx_Nombre_Notario_Escritura_Aval"].ToString();
                // Update by Tina 2011/08/04
                txtGuarantee_NotariesProfessionNumber.Text = CreditDetail.Rows[0]["Dx_No_Notario_Escritura_Aval"].ToString();
                // End
                if (CreditDetail.Rows[0]["Cve_Estado_Escritura_Aval"] != null)
                {
                    drpGuarantee_Estado.SelectedValue = CreditDetail.Rows[0]["Cve_Estado_Escritura_Aval"].ToString();
                    // Add by Tina 2011/08/08
                    // Comment by Tina 2011/08/17
                    //GuaranteeDelegMunicipioBind();// Filter Deleg_Municipio_Escritura_Aval by Estado_Escritura_Aval
                    // End
                }
                // Comment by Tina 2011/08/17
                //if (CreditDetail.Rows[0]["Cve_Deleg_Municipio_Escritura_Aval"] != null)
                //{
                //    drpGuarantee_OfficeOrMunicipality.SelectedValue = CreditDetail.Rows[0]["Cve_Deleg_Municipio_Escritura_Aval"].ToString();
                //}
                if (CreditDetail.Rows[0]["Dt_Fecha_Gravamen"] != null && CreditDetail.Rows[0]["Dt_Fecha_Gravamen"].ToString() != "")
                {
                    txtPropertyEncumbrancesFecha.Text = Convert.ToDateTime(CreditDetail.Rows[0]["Dt_Fecha_Gravamen"].ToString()).ToString("yyyy-MM-dd");
                }
                txtPropertyEncumbrancesName.Text = CreditDetail.Rows[0]["Dx_Emite_Gravamen"].ToString();
                txtMarriageNumber.Text = CreditDetail.Rows[0]["Dx_Num_Acta_Matrimonio_Aval"].ToString();
                txtCitizenRegisterOffice.Text = CreditDetail.Rows[0]["Dx_Registro_Civil_Mat_Aval"].ToString();

                //if (CreditDetail.Rows[0]["Fg_Adquisicion_Sust"].ToString() == "2") ///RSA value substitution
                {
                    radAcquisition.Checked = false;
                    radReplacement.Checked = true;
                }
                //else
                //{
                //    radAcquisition.Checked = true;
                //    radReplacement.Checked = false;
                //}

                // Add by Tina 2011/08/17
                DelegMunicipioBind();
                if (CreditDetail.Rows[0]["Cve_Deleg_Municipio_Poder"] != null)
                {
                    drpRepresentative_OfficeorMunicipality.SelectedValue = CreditDetail.Rows[0]["Cve_Deleg_Municipio_Poder"].ToString();
                }
                if (CreditDetail.Rows[0]["Cve_Deleg_Municipio_Acta"] != null)
                {
                    drpApplicant_OfficeOrMunicipality.SelectedValue = CreditDetail.Rows[0]["Cve_Deleg_Municipio_Acta"].ToString();
                }
                if (CreditDetail.Rows[0]["Cve_Deleg_Municipio_Escritura_Aval"] != null)
                {
                    drpGuarantee_OfficeOrMunicipality.SelectedValue = CreditDetail.Rows[0]["Cve_Deleg_Municipio_Escritura_Aval"].ToString();
                }
            }
        }
        /// <summary>
        /// Init Replacement step Default Data
        /// </summary>
        private void InitReplacementStepDefaultData()
        {
            GridViewDetail = new DataTable();
            DataRow row;

            // Comment by Tina 2011/08/31
            //GridViewDetail.Columns.Add("Technology", Type.GetType("System.String"));
            //GridViewDetail.Columns.Add("Unit", Type.GetType("System.String"));
            //GridViewDetail.Columns.Add("DisposalCenter", Type.GetType("System.String"));
            //GridViewDetail.Columns.Add("Status", Type.GetType("System.String"));

            // Added by Tina 2011/08/31
            GridViewDetail.Columns.Add("Technology", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("DisposalCenter", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("TypeOfProduct", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Model", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Marca", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("SerialNumber", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("KeyNumber", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Status", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Capacidad", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Antiguedad", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("DisposalType", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Color", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Peso", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Id_Pre_Folio", Type.GetType("System.String")); // updated by tina 2012-02-29
            // End
            K_TECNOLOGIA_MATERIALDAL ktm = new K_TECNOLOGIA_MATERIALDAL();

            TechnologyGroupNumber = 0;
            DataTable dtCreditoSustitucion = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONByNo_Credito(txtCreditoNum.Text);
            if (dtCreditoSustitucion != null && dtCreditoSustitucion.Rows.Count > 0)
            {
                //Init data source
                foreach (DataRow dataRow in dtCreditoSustitucion.Rows)
                {
                    // RSA 20120814 Ignore technologies with GetMaterialMaxOrderByTechnology = 0
                    if (ktm.GetMaterialMaxOrderByTechnology(Convert.ToInt32(dataRow["Technology"].ToString())) > 0)
                    {
                        TechnologyGroupNumber++;
                        row = GridViewDetail.NewRow();

                        row["Technology"] = dataRow["Technology"].ToString();
                        //row["Unit"] = dataRow["Unit"] != null ? dataRow["Unit"].ToString() : ""; // Comment by Tina 2011/08/31
                        //row["DisposalCenter"] = dataRow["DisposalCenter"] != DBNull.Value ? dataRow["DisposalCenter"].ToString() : ""; // Update by Tina 2011/08/31
                        // Added by Tina 2011/08/31
                        row["TypeOfProduct"] = dataRow["TypeOfProduct"] != DBNull.Value ? dataRow["TypeOfProduct"].ToString() : "";
                        row["Model"] = dataRow["Model"] != DBNull.Value ? dataRow["Model"].ToString() : "";
                        row["Marca"] = dataRow["Marca"] != DBNull.Value ? dataRow["Marca"].ToString() : "";
                        row["SerialNumber"] = dataRow["SerialNumber"] != DBNull.Value ? dataRow["SerialNumber"].ToString() : "";
                        row["KeyNumber"] = dataRow["KeyNumber"] != DBNull.Value ? dataRow["KeyNumber"].ToString() : "";
                        // End
                        row["Status"] = "1";
                        row["Capacidad"] = dataRow["Capacidad"] != DBNull.Value ? dataRow["Capacidad"].ToString() : "";
                        row["Antiguedad"] = dataRow["Antiguedad"] != DBNull.Value ? dataRow["Antiguedad"].ToString() : "";
                        row["DisposalType"] = dataRow["DisposalType"] != DBNull.Value ? dataRow["DisposalType"].ToString() : "";
                        if (dataRow["DisposalType"] != DBNull.Value && dataRow["DisposalType"].ToString() == "M")
                        {
                            row["DisposalCenter"] = dataRow["DisposalCenter"] != DBNull.Value ? dataRow["DisposalCenter"].ToString() + "-" + "(Matriz)" : ""; // updated by tina 2012-02-29
                        }
                        else
                        {
                            row["DisposalCenter"] = dataRow["DisposalCenter"] != DBNull.Value ? dataRow["DisposalCenter"].ToString() + "-" + "(Sucursal)" : ""; // updated by tina 2012-02-29
                        }
                        row["Color"] = dataRow["Color"] != DBNull.Value ? dataRow["Color"].ToString() : "";
                        row["Peso"] = dataRow["Peso"] != DBNull.Value ? dataRow["Peso"].ToString() : "";

                        row["Id_Pre_Folio"] = dataRow["Id_Pre_Folio"] != DBNull.Value ? dataRow["Id_Pre_Folio"].ToString() : ""; // updated tina 2012-02-29
                        GridViewDetail.Rows.Add(row);
                    }
                }
            }
            else
            {
                // Comment by Tina 2011/08/31
                //TechnologyGroupNumber = 1;

                //row = GridViewDetail.NewRow();
                //row["Technology"] = "";
                //row["Unit"] = "";
                //row["DisposalCenter"] = "";
                //row["Status"] = "0";
                //GridViewDetail.Rows.Add(row);

                // Added by Tina 2011/08/31
                DataTable dtTechnology = K_CREDITO_PRODUCTODal.ClassInstance.get_RequestTechnologyAmountByCredit(txtCreditoNum.Text, Global.PROGRAM);
                if (dtTechnology != null && dtTechnology.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dtTechnology.Rows)
                    {
                        // RSA 20120814 Ignore technologies with GetMaterialMaxOrderByTechnology = 0
                        if (ktm.GetMaterialMaxOrderByTechnology(Convert.ToInt32(dataRow["Technology"].ToString())) > 0)
                        {
                            TechnologyGroupNumber += int.Parse(dataRow["Amount"].ToString());
                            for (int i = 0; i < int.Parse(dataRow["Amount"].ToString()); i++)
                            {
                                row = GridViewDetail.NewRow();

                                row["Technology"] = dataRow["Technology"].ToString();
                                row["DisposalCenter"] = "";
                                row["TypeOfProduct"] = "";
                                row["Model"] = "";
                                row["Marca"] = "";
                                row["SerialNumber"] = "";
                                row["KeyNumber"] = "";
                                row["Status"] = "0";
                                row["Capacidad"] = "";
                                row["Antiguedad"] = "";
                                row["DisposalType"] = "";
                                row["Color"] = "";
                                row["Peso"] = "";
                                row["Id_Pre_Folio"] = ""; // updated by tina 2012-02-29
                                GridViewDetail.Rows.Add(row);
                            }
                        }
                    }
                }

                // End
            }

            if (TechnologyGroupNumber == 0)
            {
                row = GridViewDetail.NewRow();
                row["Technology"] = "";
                //row["Unit"] = "";// Comment by Tina 2011/08/31
                row["DisposalCenter"] = "";
                // Added by Tina 2011/08/31
                row["TypeOfProduct"] = "";
                row["Model"] = "";
                row["Marca"] = "";
                row["SerialNumber"] = "";
                row["KeyNumber"] = "";
                // End
                row["Status"] = "0";
                row["Capacidad"] = "";
                row["Antiguedad"] = "";
                row["DisposalType"] = "";
                row["Color"] = "";
                row["Peso"] = "";
                row["Id_Pre_Folio"] = ""; // updated by tina 2012-02-29
                GridViewDetail.Rows.Add(row);
            }

            //Binding
            // Update by Tina 2011/09/01
            if (radReplacement.Checked)
            {
                this.gdvReplacement.DataSource = GridViewDetail;
            }
            else
            {
                DataTable tempGridViewDetail = GridViewDetail.Clone();
                TechnologyGroupNumber = 0;

                row = tempGridViewDetail.NewRow();
                row["Technology"] = "";
                row["DisposalCenter"] = "";
                row["TypeOfProduct"] = "";
                row["Model"] = "";
                row["Marca"] = "";
                row["SerialNumber"] = "";
                row["KeyNumber"] = "";
                row["Status"] = "0";
                row["Capacidad"] = "";
                row["Antiguedad"] = "";
                row["DisposalType"] = "";
                row["Color"] = "";
                row["Peso"] = "";
                row["Id_Pre_Folio"] = ""; // updated by tina 2012-02-29
                tempGridViewDetail.Rows.Add(row);
                this.gdvReplacement.DataSource = tempGridViewDetail;
            }
            // End
            this.gdvReplacement.DataBind();
            //Init controls' status
            if (this.radReplacement.Checked)
            {
                this.gdvReplacement.Enabled = true;
                //this.btnAddRecord.Enabled = true;// Comment by Tina 2011/08/31
            }
            else
            {
                this.gdvReplacement.Enabled = false;
                //this.btnAddRecord.Enabled = false;// Comment by Tina 2011/08/31
            }
        }
        #endregion

        #region Load Estado Data
        /// <summary>
        /// Init estado controls
        /// </summary>
        private void EstadoBind()
        {
            DataTable dtEstado = CAT_ESTADOBLL.ClassInstance.Get_All_CAT_ESTADO();
            //Init estado controls
            if (dtEstado != null && dtEstado.Rows.Count > 0)
            {
                //Init representative estado
                drpRepresentative_Estado.DataSource = dtEstado;
                drpRepresentative_Estado.DataTextField = "Dx_Nombre_Estado";
                drpRepresentative_Estado.DataValueField = "Cve_Estado";
                drpRepresentative_Estado.DataBind();
                drpRepresentative_Estado.Items.Insert(0, "");
                drpRepresentative_Estado.SelectedIndex = 0;
                //Init applicant estado
                drpApplicant_Estado.DataSource = dtEstado;
                drpApplicant_Estado.DataTextField = "Dx_Nombre_Estado";
                drpApplicant_Estado.DataValueField = "Cve_Estado";
                drpApplicant_Estado.DataBind();
                drpApplicant_Estado.Items.Insert(0, "");
                drpApplicant_Estado.SelectedIndex = 0;
                //Init guarantee estado
                drpGuarantee_Estado.DataSource = dtEstado;
                drpGuarantee_Estado.DataTextField = "Dx_Nombre_Estado";
                drpGuarantee_Estado.DataValueField = "Cve_Estado";
                drpGuarantee_Estado.DataBind();
                drpGuarantee_Estado.Items.Insert(0, "");
                drpGuarantee_Estado.SelectedIndex = 0;
            }
        }
        #endregion

        #region Load Deleg Municipio Controls
        /// <summary>
        /// Init deleg municipio controls
        /// </summary>
        private void DelegMunicipioBind()
        {
            RepresentativeDelegMunicipioBind();
            ApplicantDelegMunicipioBind();
            GuaranteeDelegMunicipioBind();
        }
        private void RepresentativeDelegMunicipioBind()
        {
            int Cve_Estado = (drpRepresentative_Estado.SelectedIndex == 0 || drpRepresentative_Estado.SelectedIndex == -1) ? -1 : int.Parse(drpRepresentative_Estado.SelectedValue);

            DataTable dtOfficeorMunicipality = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Cve_Estado);
            //Init deleg municipio controls
            if (dtOfficeorMunicipality != null)// Update by jerry 2011/08/08
            {
                //Init representative deleg municipio
                drpRepresentative_OfficeorMunicipality.DataSource = dtOfficeorMunicipality;
                drpRepresentative_OfficeorMunicipality.DataTextField = "Dx_Deleg_Municipio";
                drpRepresentative_OfficeorMunicipality.DataValueField = "Cve_Deleg_Municipio";
                drpRepresentative_OfficeorMunicipality.DataBind();
                drpRepresentative_OfficeorMunicipality.Items.Insert(0, "");
                drpRepresentative_OfficeorMunicipality.SelectedIndex = 0;
            }
        }
        private void ApplicantDelegMunicipioBind()
        {
            int Cve_Estado = (drpApplicant_Estado.SelectedIndex == 0 || drpApplicant_Estado.SelectedIndex == -1) ? -1 : int.Parse(drpApplicant_Estado.SelectedValue);

            DataTable dtOfficeorMunicipality = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Cve_Estado);
            //Init deleg municipio controls
            if (dtOfficeorMunicipality != null)// Update by jerry 2011/08/08
            {
                //Init applicant deleg municipio
                drpApplicant_OfficeOrMunicipality.DataSource = dtOfficeorMunicipality;
                drpApplicant_OfficeOrMunicipality.DataTextField = "Dx_Deleg_Municipio";
                drpApplicant_OfficeOrMunicipality.DataValueField = "Cve_Deleg_Municipio";
                drpApplicant_OfficeOrMunicipality.DataBind();
                drpApplicant_OfficeOrMunicipality.Items.Insert(0, "");
                drpApplicant_OfficeOrMunicipality.SelectedIndex = 0;
            }
        }
        private void GuaranteeDelegMunicipioBind()
        {
            int Cve_Estado = (drpGuarantee_Estado.SelectedIndex == 0 || drpGuarantee_Estado.SelectedIndex == -1) ? -1 : int.Parse(drpGuarantee_Estado.SelectedValue);

            DataTable dtOfficeorMunicipality = CAT_DELEG_MUNICIPIOBLL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Cve_Estado);
            //Init deleg municipio controls
            if (dtOfficeorMunicipality != null)// Update by jerry 2011/08/08
            {
                //Init guarantee deleg municipio
                drpGuarantee_OfficeOrMunicipality.DataSource = dtOfficeorMunicipality;
                drpGuarantee_OfficeOrMunicipality.DataTextField = "Dx_Deleg_Municipio";
                drpGuarantee_OfficeOrMunicipality.DataValueField = "Cve_Deleg_Municipio";
                drpGuarantee_OfficeOrMunicipality.DataBind();
                drpGuarantee_OfficeOrMunicipality.Items.Insert(0, "");
                drpGuarantee_OfficeOrMunicipality.SelectedIndex = 0;
            }
        }
        #endregion

        #region Actions in Step One
        #endregion

        #region Actions in Step Two
        /// <summary>
        /// Refresh deleg municipio when estado selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpRepresentative_Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            RepresentativeDelegMunicipioBind();
        }

        protected void drpApplicant_Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplicantDelegMunicipioBind();
        }

        protected void drpGuarantee_Estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            GuaranteeDelegMunicipioBind();
        }
        #endregion

        #region Actions in Step Three
        /// <summary>
        /// radAcquisition checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void radAcquisition_CheckedChanged(object sender, EventArgs e)
        {
            if (radAcquisition.Checked)
            {
                // Comment by Tina 2011/08/31
                //DataTable tempGridViewDetail = new DataTable();

                //tempGridViewDetail.Columns.Add("Technology", Type.GetType("System.String"));
                //tempGridViewDetail.Columns.Add("Unit", Type.GetType("System.String"));
                //tempGridViewDetail.Columns.Add("DisposalCenter", Type.GetType("System.String"));
                //tempGridViewDetail.Columns.Add("Status", Type.GetType("System.String"));

                //foreach (DataRow dr in GridViewDetail.Rows)
                //{
                //    if (dr["Status"].ToString() == "1")
                //    {
                //        tempGridViewDetail.ImportRow(dr);
                //    }
                //}

                //if (tempGridViewDetail.Rows.Count > 0)
                //{
                //    TechnologyGroupNumber = tempGridViewDetail.Rows.Count;
                //}
                //else
                //{
                //    DataRow row = tempGridViewDetail.NewRow();
                //    row["Technology"] = "";
                //    row["Unit"] = "";
                //    row["DisposalCenter"] = "";
                //    row["Status"] = "0";
                //    tempGridViewDetail.Rows.Add(row);
                //    TechnologyGroupNumber = 1;
                //}

                //GridViewDetail.Rows.Clear();
                //GridViewDetail = tempGridViewDetail;

                //gdvReplacement.DataSource = tempGridViewDetail;
                //gdvReplacement.DataBind();

                gdvReplacement.Enabled = false;
                //btnAddRecord.Enabled = false; 

                // Added by Tina 2011/09/01
                DataTable tempGridViewDetail = new DataTable();
                DataRow row;

                tempGridViewDetail.Columns.Add("Technology", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("DisposalCenter", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("TypeOfProduct", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("Model", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("Marca", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("SerialNumber", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("KeyNumber", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("Status", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("Capacidad", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("Antiguedad", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("DisposalType", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("Color", Type.GetType("System.String"));
                tempGridViewDetail.Columns.Add("Peso", Type.GetType("System.String"));

                TechnologyGroupNumber = 0;

                row = tempGridViewDetail.NewRow();
                row["Technology"] = "";
                row["DisposalCenter"] = "";
                row["TypeOfProduct"] = "";
                row["Model"] = "";
                row["Marca"] = "";
                row["SerialNumber"] = "";
                row["KeyNumber"] = "";
                row["Status"] = "0";
                row["Capacidad"] = "";
                row["Antiguedad"] = "";
                row["DisposalType"] = "";
                row["Color"] = "";
                row["Peso"] = "";
                tempGridViewDetail.Rows.Add(row);

                gdvReplacement.DataSource = tempGridViewDetail;
                gdvReplacement.DataBind();
                // End
            }
        }
        /// <summary>
        /// radReplacement checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void radReplacement_CheckedChanged(object sender, EventArgs e)
        {
            if (radReplacement.Checked)
            {
                gdvReplacement.Enabled = true;
                //btnAddRecord.Enabled = true;// Comment by Tina 2011/08/31

                // Added by Tina 2011/09/01
                TechnologyGroupNumber = GridViewDetail.Rows.Count;
                gdvReplacement.DataSource = GridViewDetail;
                gdvReplacement.DataBind();
                // End
            }
        }

        // Add by Tina 2011/08/05
        /// <summary>
        /// get Disposal Center by Technology
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpTechnology = (DropDownList)sender;
            GridViewRow Row = (GridViewRow)drpTechnology.NamingContainer;
            int Index = Row.RowIndex;
            DropDownList drpDisposalCenter = (DropDownList)gdvReplacement.Rows[Index].FindControl("drpDisposalCenter");
            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];

            if (drpTechnology.SelectedIndex == 0 || drpTechnology.SelectedIndex == -1)
            {
                drpDisposalCenter.DataSource = CAT_CENTRO_DISPBLL.ClassInstance.Get_CAT_CENTRO_DISPByTECHNOLOGY(2, Requested_Technology, UserModel.Id_Usuario);// Update by Tina 2011/08/24
            }
            else
            {
                drpDisposalCenter.DataSource = CAT_CENTRO_DISPBLL.ClassInstance.Get_CAT_CENTRO_DISPByTECHNOLOGY(2, drpTechnology.SelectedValue.ToString(), UserModel.Id_Usuario );// Update by Tina 2011/08/24
            }

            drpDisposalCenter.DataTextField = "Dx_Razon_Social";
            drpDisposalCenter.DataValueField = "Id_Centro_Disp";
            drpDisposalCenter.DataBind();
            drpDisposalCenter.Items.Insert(0, "");
        }

        // Added by Tina 2011/09/05
        /// <summary>
        /// Set the tooltip value of the Disposal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDisposalCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gdvReplacement.Rows.Count; i++)
            {
                DropDownList drpDisposalCenter1 = (DropDownList)gdvReplacement.Rows[i].FindControl("drpDisposalCenter");
                ListItemCollection items = drpDisposalCenter1.Items;

                for (int j = 0; j < items.Count; j++)
                {
                    ListItem item = items[j];
                    item.Attributes["title"] = item.Text;
                }
                drpDisposalCenter1.ToolTip = drpDisposalCenter1.SelectedItem.Text; // added by tina 2012/04/13
            }
            DropDownList drpDisposalCenter = (DropDownList)sender;
            drpDisposalCenter.ToolTip = drpDisposalCenter.SelectedItem.Text; // added by Tina 2011/11/22
        }
        // End

        // added by tina 2012/04/12
        /// <summary>
        /// Set the tooltip value of the product type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gdvReplacement.Rows.Count; i++)
            {
                DropDownList drpProductType1 = (DropDownList)gdvReplacement.Rows[i].FindControl("drpProductType");
                ListItemCollection items = drpProductType1.Items;

                for (int j = 0; j < items.Count; j++)
                {
                    ListItem item = items[j];
                    item.Attributes["Title"] = item.Text;
                }
                drpProductType1.ToolTip = drpProductType1.SelectedItem.Text; // added by tina 2012/04/13
            }
            DropDownList drpProductType = (DropDownList)sender;
            drpProductType.ToolTip = drpProductType.SelectedItem.Text;

            //add by coco 2012-07-17
            GridViewRow Row = (GridViewRow)drpProductType.NamingContainer;
            //TextBox  textModelo = (TextBox)gdvReplacement.Rows[Row.RowIndex].FindControl("TextModelo");
            //DataTable dtModelo = CAT_PRODUCTODal.ClassInstance.GetProductModeloByProductType(drpProductType.SelectedValue);
            //BindDrpModelo(drpModelo, dtModelo);
            //end add
        }
        //add by coco 2012-07-17
        //private static void BindDrpModelo(DropDownList drpModelo, DataTable dtModelo)
        //{
        //    drpModelo.DataSource = dtModelo;
        //    drpModelo.DataTextField = "Dx_Modelo_Producto";
        //    drpModelo.DataValueField = "Dx_Modelo_Producto";
        //    drpModelo.DataBind();
        //    drpModelo.Items.Insert(0, "");
        //}
        //end add
        // end
        #endregion

        #region Actions in Last Step
        #endregion

        #region Button Actions
        // Add by Tina 2011/08/11
        /// <summary>
        /// go to credit review page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StartPreviousButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../RegionalModule/CreditReview.aspx?creditno=" + CreditNumber + "&statusid=" + Credit_Status + "&Flag=V");
        }
        // End

        // Comment by Tina 2011/08/31
        ///// <summary>
        ///// Adds a new record to enter the new product group to dispose
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnAddRecord_Click(object sender, EventArgs e)
        //{
        //    SaveGridViewDetail();

        //    TechnologyGroupNumber += 1;

        //    DataRow NewRow = GridViewDetail.NewRow();
        //    NewRow["Technology"] = "";
        //    NewRow["Unit"] = "";
        //    NewRow["DisposalCenter"] = "";
        //    NewRow["Status"] = "0";

        //    GridViewDetail.Rows.Add(NewRow);

        //    gdvReplacement.DataSource = GridViewDetail;
        //    gdvReplacement.DataBind();
        //}

        /// <summary>
        /// Previous
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wizardPages_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            switch (e.CurrentStepIndex)
            {
                case 1:
                    lblTitle.Text = "VALIDAR HISTORIAL CREDITICIO";
                    break;
                case 2:
                    lblTitle.Text = "INTEGRAR EXPEDIENTE DEL CREDITO";
                    Button btnNext = (Button)wizardPages.FindControl("StepNavigationTemplateContainerID$StepNextButton");
                    btnNext.Text = "Siguiente";
                    break;
                case 3:
                    lblTitle.Text = "ADQUISICION O SUSTITUCION";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Consulta Crediticia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConsultaCrediticia_Click(object sender, EventArgs e)
        {
            string mop = string.Empty;
            string folio = string.Empty;
            string message = string.Empty;
            int califmop = 0;    // the No_Calif_MOP in CAT_PROGRAMA table
            bool IsValid = false;
            int result = 0;
            bool retry = false;

            CAT_AUXILIAREntity instance = new CAT_AUXILIAREntity();

            LsApplicationState appstate = new LsApplicationState(HttpContext.Current.Application);

            if (!string.IsNullOrEmpty(hiddenfield.Value))
            {
                if (hiddenfield.Value != "Cancel")
                {
                    if (Page.IsValid)
                    {
                        instance.No_Credito = CreditNumber;
                        char[] Symbol = new char[] { '-' };
                        string CreditNum = CreditNumber.Substring(CreditNumber.LastIndexOfAny(Symbol) + 1);//Changed by Tina 2011/08/12 2011/08/24
                        // Added by Tina 2011/08/24
                        while (CreditNum.Substring(0, 1) == "0")
                        {
                            CreditNum = CreditNum.Substring(1);
                        }
                        // End

                        if (IsIntegerNumberValid(hiddenfield.Value))
                        {
                            mop = hiddenfield.Value;
                            instance.No_MOP = mop;
                        }
                        else
                        {
                            // if (CCHelper.tempMops.Keys.Contains<string>(CreditNumber.ToString()))
                            try
                            {
                                //FRR 2011

                                //DataTable dtCreditoAuxiliar = CAT_AUXILIARdal.ClassInstance.Get_CAT_AUXILIARByCreditNo(CreditNumber);
                                //CreditDetailAuxiliar = dtCreditoAuxiliar;
                                //if (dtCreditoAuxiliar != null && dtCreditoAuxiliar.Rows.Count > 0)
                                //{
                                //    ID_Nombres = int.Parse(dtCreditoAuxiliar.Rows[0]["Dx_Nombres"].ToString());

                                //}
                                //Temporal for validation testing
                                if (Tipo_Sociedad == (int)CompanyType.MORAL)
                                {
                                    if (!appstate.DebugMode.Equals("false") && !appstate.DebugMode.Equals("pm"))
                                    {
                                        // use the last two digits of the amount
                                        // if only one is provided throw an exception to simulate web service not responding
                                        if (txtContratoImporte.Text.Length > 0)
                                            mop = txtContratoImporte.Text.Substring(txtContratoImporte.Text.Length - 2, 2);
                                        else
                                            mop = "04";
                                        folio = "1";
                                    }
                                    else
                                    {
                                        PMHelper pm = new PMHelper();

                                        pm.Producto = "001";
                                        pm.Rfc = CreditDetail.Rows[0]["DX_RFC"].ToString();
                                        pm.Nombres = CreditDetail.Rows[0]["Dx_Razon_Social"].ToString();
                                        pm.Direccion1 = CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Calle"].ToString()
                                            + " " + CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Num"].ToString();
                                        pm.CodigoPostal = CreditDetail.Rows[0]["Dx_Domicilio_Fisc_CP"].ToString();
                                        pm.Colonia = CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Colonia"].ToString();
                                        pm.Ciudad = CreditDetail.Rows[0]["Dx_Deleg_Municipio"].ToString();
                                        pm.Estado = CreditDetail.Rows[0]["Dx_Cve_PM"].ToString();
                                        pm.Pais = "MX";

                                        pm.ConsultarPersonaMoral();

                                        mop = pm.Mop;
                                        folio = pm.Folio;
                                    }
                                }
                                else
                                {
                                    if (!appstate.DebugMode.Equals("false"))
                                    {
                                        // use the last two digits of the amount
                                        // if only one is provided throw an exception to simulate web service not responding
                                        if (txtContratoImporte.Text.Length > 0)
                                            mop = txtContratoImporte.Text.Substring(txtContratoImporte.Text.Length - 2, 2);
                                        else
                                            mop = "04";
                                        folio = "1";
                                    }
                                    else
                                    {
                                        CCHelper helper = new CCHelper();

                                    helper.Calle = CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Calle"].ToString();
                                    helper.ColoniaPoblacion = CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Colonia"].ToString();
                                    helper.CP = CreditDetail.Rows[0]["Dx_Domicilio_Fisc_CP"].ToString();
                                    helper.DelegacionMunicipio = CreditDetail.Rows[0]["Dx_Deleg_Municipio"].ToString();
                                    helper.Estado = CreditDetail.Rows[0]["Dx_Cve_CC"].ToString();
                                    helper.ImporteContrato = Convert.ToSingle(CreditDetail.Rows[0]["Mt_Monto_Solicitado"]);
                                    helper.Numero = CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Num"].ToString();
                                    helper.RFC = CreditDetail.Rows[0]["DX_RFC"].ToString();
                                    helper.Sexo = Convert.ToInt32(CreditDetail.Rows[0]["Fg_Edo_Civil_Repre_Legal"]) == 1 ? "Masculino" : "Femenino";

                                    helper.Nombres = Dic_Auxiliar[CreditNumber.ToString()][0];
                                    helper.ApellidoPaterno = Dic_Auxiliar[CreditNumber.ToString()][1];
                                    helper.ApellidoMaterno = Dic_Auxiliar[CreditNumber.ToString()][2];
                                    helper.FechaNacimiento = Convert.ToDateTime(Dic_Auxiliar[CreditNumber.ToString()][3]);
                                    helper.NumeroInterior = Dic_Auxiliar[CreditNumber.ToString()][4];
                                    helper.Ciudad = Dic_Auxiliar[CreditNumber.ToString()][5];
                                    helper.NumeroFirma = CreditNumber.ToString();

                                    mop = helper.ConsultaCirculo().ToString();
                                    folio = helper.folio;
                                }
                                }
                                //mop = "0";
                                //folio = "1";



                                //if (Tipo_Sociedad != (int)CompanyType.PERSONAFISICA
                                //    && Tipo_Sociedad != (int)CompanyType.PERSONAFISICACONACTIVIDADEMPRESARIAL)
                                //{
                                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pregunta", "var value;  if(confirm('El usuario no se encuentra en el Círculo de Crédito, ¿desea capturar el MOP?')){value = window.showModalDialog('EnterMop.aspx','','dialogWidth:400px;dialogHeight:240px;toolbar:no; resizable:no;status:no;');" +
                                //                                   "if(value!=null) { document.getElementById('" + hiddenfield.ClientID + "').innerText=value;} " +
                                //                                   "else { document.getElementById('" + hiddenfield.ClientID + "').innerText= 'Cancel'; }}", true);
                                //    mop = "mop";
                                //    instance.No_MOP = mop;
                                //    instance.Ft_Folio = folio;
                                //    retry = true;
                                //}
                                //else
                                {
                                    instance.No_MOP = mop;
                                    instance.Ft_Folio = folio;
                                }
                            }
                            //else
                            catch (Exception ex)
                            {
                                // RSA 20130508 do not display detailed message, "retry" flag is set, a message for it is set later
                                // message = ex.Message.Replace("\r", "\\r").Replace("\n", "\\n");
                                mop = hiddenfield.Value;
                                instance.No_MOP = mop;
                                retry = true;
                            }
                        }

                        if (retry)
                        {
                            // RSA 20130508 it is not valid, but it ain't invalid either, set appropriate message
                            message = GetGlobalResourceObject("DefaultResource", "MOPErrorConnection") as string;
                            IsValid = false;
                        }
                        else if (IsIntegerNumberValid(mop) || hiddenfield.Value != "mop")
                        {
                            DataTable dtProgram = CAT_PROGRAMADal.ClassInstance.Get_All_CAT_PROGRAMAByPK(ID_Prog_Proy.ToString());
                            if (dtProgram != null && dtProgram.Rows.Count > 0)
                            {
                                // Update by Tina 2011/08/09
                                califmop = dtProgram.Rows[0]["No_Calif_MOP"] != DBNull.Value ? int.Parse(dtProgram.Rows[0]["No_Calif_MOP"].ToString()) : 4;
                                // End

                                if (int.Parse(mop) == 96)
                                {
                                    // RSA 20130508 use message from resource file
                                    // message = "Invalido: El MOP obtenido es 96 - 'Industrial con cuenta o atraso grave', rechace ó cancele la solicitud.";
                                    message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPError96") as string, califmop, mop);
                                    IsValid = false;
                                }
                                else if (int.Parse(mop) == 99)
                                {
                                    // RSA 20130508 use message from resource file
                                    // message = "Invalido: El MOP obtenido es 99 - 'Industrial con deuda parcial o total sin recuperar y/o fraude', rechace ó cancele la solicitud.";
                                    message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPError99") as string, califmop, mop);
                                    IsValid = false;
                                }
                                else if (int.Parse(mop) >= 0 && int.Parse(mop) <= califmop)
                                {
                                    // RSA 20130508 use message from resource file
                                    message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPOK") as string, califmop, mop);
                                    IsValid = true;
                                }
                                else if (int.Parse(mop) > califmop)
                                {
                                    // RSA 20130508 use message from resource file
                                    // message = "Invalido: El MOP obtenido es > " + califmop + " pero distinto de 96 y 99";
                                    message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string, califmop, mop);
                                    IsValid = false;
                                }
                                else
                                {
                                    // RSA 20130508 use message from resource file
                                    // message = "Invalido: Con el MOP Obtenido no se pudo asignar la Tasa automáticamente.";
                                    message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string, califmop, mop);
                                    IsValid = false;
                                }
                            }
                        }
                        // invalid
                        else
                        {
                            // RSA 20130508 use message from resource file
                            message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string, califmop, mop);
                            IsValid = false;
                        }
                    }
                }
                else
                {
                    Response.Redirect("CreditMonitor.aspx");
                }
            }
            // invalid
            else
            {
                // RSA 20130508 use message from resource file
                message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string, califmop, mop);
                IsValid = false;
            }

            Button startNext = wizardPages.FindControl("StartNavigationTemplateContainerID").FindControl("StartNextButton") as Button;
            if (IsValid)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valid", "alert('" + message + "');", true);

                result = CAT_AUXILIARDal.ClassInstance.Update_CAT_AUXILIARByCredito(instance);
                // Add by Tina 2011/08/08
                if (result > 0)
                {
                    // Update by Tina 2011/08/04

                    lblImporte.Visible = false;
                    txtContratoImporte.Visible = false;
                    revContratoImporte.Visible = false;
                    btnConsultaCrediticia.Visible = false;
                    startNext.Enabled = true;
                    // End
                }
                // End
            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    result = CAT_AUXILIARDal.ClassInstance.Update_CAT_AUXILIARByCredito(instance);
                    if (!retry)
                        result += K_CREDITODal.ClassInstance.CalificacionMopNoValidCredit(CreditNumber, (int)CreditStatus.Calificación_MOP_no_válida, DateTime.Now, Session["UserName"].ToString(), DateTime.Now);
                    scope.Complete();
                }
               // Ad by Tina 2011/08/11
                Credit_Status = (int)CreditStatus.Calificación_MOP_no_válida;
                // End
                if (!string.IsNullOrEmpty(message))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "InValid", "alert('" + message.Replace('\'', '"') + "');", true);//comment by Jerry 2011/08/04
                    lblMopInvalido.Visible = true;
                    lblMopInvalido.Text = message;
                }
                //Response.Redirect("CreditMonitor.aspx");

                if (retry)
                {
                    lblImporte.Visible = true;
                    txtContratoImporte.Visible = true;
                    revContratoImporte.Visible = true;
                    btnConsultaCrediticia.Visible = true;
                    startNext.Enabled = false;
                }
                else
                {
                    lblImporte.Visible = false;
                    txtContratoImporte.Visible = false;
                    revContratoImporte.Visible = false;
                    btnConsultaCrediticia.Visible = false;
                    startNext.Enabled = false;
                }
            }
        }

        /// <summary>
        ///  perform the saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wizardPages_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (Page.IsValid) // updated by tina 2012-02-29
            {
                if (!ValidateMOP())
                {
                    // wizardPages.ActiveStepIndex = 0;
                    e.Cancel = true;
                    Salir();
                    return;
                }

                Button btnNext;
                switch (e.CurrentStepIndex)
                {
                    case 0:
                        // Add by Tina 2011/08/11
                        if (Credit_Status == (int)CreditStatus.PENDIENTE)
                        {
                            if (CheckCurrentAmountEnough())
                            {
                                lblTitle.Text = "INTEGRAR EXPEDIENTE DEL CREDITO";
                            }
                            else
                            {
                                string strMsg = HttpContext.GetGlobalResourceObject("DefaultResource", "msgAmountNotEnough") as string;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "CheckError", "alert('" + strMsg + "');", true);
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            lblTitle.Text = "INTEGRAR EXPEDIENTE DEL CREDITO";
                        }
                        // End
                        break;
                    case 1:
                        lblTitle.Text = "ADQUISICION O SUSTITUCION";
                        // RSA 20120814 skip list if it doesn't apply
                        if (TechnologyGroupNumber == 0)
                            goto case 2;
                        else
                            gdvReplacement_DataBound(new object(), new EventArgs()); // added by tina 2012/04/13
                            btnNext = (Button)wizardPages.FindControl("StepNavigationTemplateContainerID$StepNextButton");
                            btnNext.Text = "Guardar";
                        break;
                    case 2:
                        lblTitle.Text = "IMPRIMIR EXPEDIENTE";

                        //SaveGridViewDetail();// Comment by Tina 2011/08/31

                        EntidadCredito creditomodel = new EntidadCredito();
                        List<K_CREDITO_SUSTITUCIONModel> listUpdate = new List<K_CREDITO_SUSTITUCIONModel>();
                        List<K_CREDITO_SUSTITUCIONModel> listAdd = new List<K_CREDITO_SUSTITUCIONModel>();

                        LoadIntegrateDataFromUI(out creditomodel);
                        // Update by Tina 2011/09/01
                        if (radReplacement.Checked)
                        {
                            bool isComplete = LoadReplacementDataFromUI(out listUpdate, out listAdd);
                            if (!isComplete)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('Los campos son obligatorios');", true);
                                e.Cancel = true;
                                return;
                            }
                        }

                        using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                        {

                            int iResult = K_CREDITODal.ClassInstance.UpdateCredit(creditomodel);
                            // Added by Tina 2011/09/01
                            if (!radReplacement.Checked)
                            {
                                iResult += K_CREDITO_SUSTITUCIONDAL.ClassInstance.Delete_K_CREDITO_SUSTITUCIONByCredit(txtCreditoNum.Text);
                            }
                            // End

                            if (listAdd.Count > 0)
                            {
                                iResult += K_CREDITO_SUSTITUCIONBLL.ClassInstance.Insert_K_CREDITO_SUSTITUCION(listAdd);
                            }

                            if (listUpdate.Count > 0)
                            {
                                iResult += K_CREDITO_SUSTITUCIONBLL.ClassInstance.Update_K_CREDITO_SUSTITUCION(listUpdate);
                            }

                            //update h_schedule_jobs table
                            ScheduleJobsDal.ClassInstance.CanceledScheduleJob(CreditNumber, ParameterHelper.strCon_DBLsWebApp);//Changed by Jerry 2012/04/13
                            // Add by Tina 2011/08/12
                            //update the current amount of the program
                            CAT_PROGRAMADal.ClassInstance.SubtractCurrentAmount(ID_Prog_Proy, RequestAmount);
                            //submit the transaction
                            scope.Complete();
                        }
                        this.wizardPages.MoveTo(this.Print);
                        btnNext = (Button)wizardPages.FindControl("StepNavigationTemplateContainerID$StepNextButton");
                        btnNext.Text = "Siguiente";
                        Request_Saved = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// databound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gdvReplacement_DataBound(object sender, EventArgs e)
        {
            // Added by Tina 2011/08/31
            if (TechnologyGroupNumber == 0)
            {
                DropDownList drpTechnology = gdvReplacement.Rows[0].FindControl("drpTechnology") as DropDownList;
                DropDownList drpDisposalCenter = gdvReplacement.Rows[0].FindControl("drpDisposalCenter") as DropDownList;
                DropDownList drpProductType = gdvReplacement.Rows[0].FindControl("drpProductType") as DropDownList; // updated by tina 2012/04/12
                //updated by tina 2012-07-18
                //TextBox txtModelo = gdvReplacement.Rows[0].FindControl("txtModelo") as TextBox;
                //TextBox txtMarca = gdvReplacement.Rows[0].FindControl("txtMarca") as TextBox;
                TextBox TextModelo = gdvReplacement.Rows[0].FindControl("TextModelo") as TextBox;
                TextBox TextMarca = gdvReplacement.Rows[0].FindControl("TextMarca") as TextBox;
                //end
                DropDownList drpCapacidad = gdvReplacement.Rows[0].FindControl("drpCapacidad") as DropDownList;
                TextBox txtAntiguedad = gdvReplacement.Rows[0].FindControl("txtAntiguedad") as TextBox;
                TextBox txtColor = gdvReplacement.Rows[0].FindControl("txtColor") as TextBox;
                TextBox txtPeso = gdvReplacement.Rows[0].FindControl("txtPeso") as TextBox;
                drpTechnology.Enabled = false;
                drpDisposalCenter.Enabled = false;
                drpProductType.Enabled = false;
                //updated by tina 2012-07-18
                //txtModelo.Enabled = false;
                //txtMarca.Enabled = false;
                TextModelo.Enabled = false;
                TextMarca.Enabled = false;
                //end
                drpCapacidad.Enabled = false;
                txtAntiguedad.Enabled = false;
                txtColor.Enabled = false;
                txtPeso.Enabled = false;
                // added by tina 2012/04/12
                Label lblPesoUnit = gdvReplacement.Rows[0].FindControl("lblPesoUnit") as Label;
                lblPesoUnit.Text = "";
                // end
            }
            else
            {
                for (int i = 0; i < TechnologyGroupNumber; i++)
                {
                    // Added by Tina 2011/08/24
                    Requested_Technology = string.Empty;
                    // End

                    DropDownList drpTechnology = gdvReplacement.Rows[i].FindControl("drpTechnology") as DropDownList;
                    DropDownList drpDisposalCenter = gdvReplacement.Rows[i].FindControl("drpDisposalCenter") as DropDownList;
                    //TextBox txtUnidades = gdvReplacement.Rows[i].FindControl("txtUnidades") as TextBox;// Comment by Tina 2011/08/31
                    // Added by Tina 2011/08/31
                    DropDownList drpProductType = gdvReplacement.Rows[i].FindControl("drpProductType") as DropDownList; // updated by tina 2012/04/12
                    //edit by coco 2012-07-17
                    //TextBox txtModelo = gdvReplacement.Rows[i].FindControl("txtModelo") as TextBox;
                    //TextBox txtMarca = gdvReplacement.Rows[i].FindControl("txtMarca") as TextBox;
                    TextBox TextModelo = gdvReplacement.Rows[i].FindControl("TextModelo") as TextBox;
                    TextBox TextMarca = gdvReplacement.Rows[i].FindControl("TextMarca") as TextBox;
                    //end edit by coco
                    DropDownList drpCapacidad = gdvReplacement.Rows[i].FindControl("drpCapacidad") as DropDownList;
                    TextBox txtAntiguedad = gdvReplacement.Rows[i].FindControl("txtAntiguedad") as TextBox;
                    TextBox txtColor = gdvReplacement.Rows[i].FindControl("txtColor") as TextBox;
                    TextBox txtPeso = gdvReplacement.Rows[i].FindControl("txtPeso") as TextBox;
                    // End
                    // added by tina 2012/04/12
                    Label lblPesoUnit = gdvReplacement.Rows[i].FindControl("lblPesoUnit") as Label;
                    // end

                    if (drpTechnology != null)
                    {
                        // Update by Tina 2011/08/03
                        // Update by Tina 2011/08/24
                        //DataTable TechnologyDataTable = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgram(ID_Prog_Proy.ToString());
                        DataTable TechnologyDataTable = K_CREDITO_PRODUCTODal.ClassInstance.get_All_RequestTechnologyByCredit(txtCreditoNum.Text, Global.PROGRAM);
                        // End
                        if (TechnologyDataTable != null)
                        {
                            drpTechnology.DataSource = TechnologyDataTable;
                            drpTechnology.DataTextField = "Dx_Nombre_Particular";
                            drpTechnology.DataValueField = "Cve_Tecnologia";
                            drpTechnology.DataBind();
                            drpTechnology.Items.Insert(0, "");
                        }

                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Technology"].ToString()))
                        {
                            drpTechnology.SelectedValue = GridViewDetail.Rows[i]["Technology"].ToString();
                        }
                        else
                        {
                            drpTechnology.SelectedIndex = 0;
                        }
                        drpTechnology.Enabled = false;
                        // Comment by Tina 2011/08/31
                        //if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Status"].ToString()) && GridViewDetail.Rows[i]["Status"].ToString() == "1")
                        //{
                        //    drpTechnology.Enabled = false;
                        //}
                        //else
                        //{
                        //    drpTechnology.Enabled = true;
                        //}
                        // Added by Tina 2011/08/24
                        if (TechnologyDataTable != null && TechnologyDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow dr in TechnologyDataTable.Rows)
                            {
                                Requested_Technology += Requested_Technology + dr["Cve_Tecnologia"].ToString() + ",";
                            }
                            if (Requested_Technology != string.Empty)
                            {
                                Requested_Technology = Requested_Technology.TrimEnd(',');
                            }
                        }
                        // End
                        drpTechnology.ToolTip = drpTechnology.SelectedItem.Text; // Added by Tina 2011/09/05
                    }
                    
                    if (drpDisposalCenter != null)
                    {
                        // Update by Tina 2011/08/05  2011/08/24
                        DataTable DisposalCenterDatatable = null;
                        US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];

                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Status"].ToString()) && GridViewDetail.Rows[i]["Status"].ToString() == "1")
                        {
                            DisposalCenterDatatable = CAT_CENTRO_DISPBLL.ClassInstance.Get_CAT_CENTRO_DISPByTECHNOLOGY(2, drpTechnology.SelectedValue, UserModel.Id_Usuario );
                        }
                        else
                        {
//                            DisposalCenterDatatable = CAT_CENTRO_DISPBLL.ClassInstance.Get_CAT_CENTRO_DISPByTECHNOLOGY(0, Requested_Technology);
                            DisposalCenterDatatable = CAT_CENTRO_DISPBLL.ClassInstance.Get_CAT_CENTRO_DISPByTECHNOLOGY(2, drpTechnology.SelectedValue, UserModel.Id_Usuario);

                        }
                        if (DisposalCenterDatatable != null)
                        {
                            drpDisposalCenter.DataSource = DisposalCenterDatatable;
                            drpDisposalCenter.DataTextField = "Dx_Razon_Social";
                            drpDisposalCenter.DataValueField = "Id_Centro_Disp";
                            drpDisposalCenter.DataBind();
                            drpDisposalCenter.Items.Insert(0, "");
                        }
                        // End  

                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["DisposalCenter"].ToString()))
                        {
                            //drpDisposalCenter.SelectedValue = GridViewDetail.Rows[i]["DisposalCenter"].ToString();
                            if (GridViewDetail.Rows[i]["DisposalCenter"].ToString() != "")
                            {
                                if (GridViewDetail.Rows[i]["DisposalType"].ToString() == "M")
                                {
                                    foreach (ListItem item in drpDisposalCenter.Items)
                                    {
                                        if (item.Text.Contains("(Matriz)") && item.Value == GridViewDetail.Rows[i]["DisposalCenter"].ToString()) // updated by tina 2012-02-29
                                        {
                                            item.Selected = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (ListItem item in drpDisposalCenter.Items)
                                    {
                                        if (item.Text.Contains("(Sucursal)") && item.Value == GridViewDetail.Rows[i]["DisposalCenter"].ToString()) // updated by tina 2012-02-29
                                        {
                                            item.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                drpDisposalCenter.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            drpDisposalCenter.SelectedIndex = 0;
                        }

                        foreach (ListItem item in drpDisposalCenter.Items)
                        {
                            item.Attributes.Add("title", item.Text);
                        }
                        drpDisposalCenter.ToolTip = drpDisposalCenter.SelectedItem.Text;
                    }

                    // Comment by Tina 2011/08/31
                    //if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Unit"].ToString()))
                    //{
                    //    txtUnidades.Text = GridViewDetail.Rows[i]["Unit"].ToString();
                    //}

                    // Added by Tina 2011/08/31

                    // updated by tina 2012/04/12
                    if (drpProductType != null)
                    {
                        DataTable dtProductType = CAT_TIPO_PRODUCTODal.ClassInstance.Get_CAT_TIPO_PRODUCTOByTechnology(drpTechnology.SelectedValue);
                        if (dtProductType != null)
                        {
                            drpProductType.DataSource = dtProductType;
                            drpProductType.DataTextField = "Dx_Tipo_Producto";
                            drpProductType.DataValueField = "Ft_Tipo_Producto";
                            drpProductType.DataBind();
                            drpProductType.Items.Insert(0, "");
                        }
                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["TypeOfProduct"].ToString()))
                        {
                            drpProductType.SelectedValue = GridViewDetail.Rows[i]["TypeOfProduct"].ToString();
                        }
                        else
                        {
                            drpProductType.SelectedIndex = 0;
                        }
                        foreach (ListItem item in drpProductType.Items)
                        {
                            item.Attributes.Add("Title", item.Text);
                        }
                        drpProductType.ToolTip = drpProductType.SelectedItem.Text;
                    }
                    // end
                    //edit by coco 2012-07-17
                    //if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Model"].ToString()))
                    //{
                    //    txtModelo.Text = GridViewDetail.Rows[i]["Model"].ToString();
                    //}
                    //if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Marca"].ToString()))
                    //{
                    //    txtMarca.Text = GridViewDetail.Rows[i]["Marca"].ToString();
                    //}
                    // End
                    
                    //Load embedded model records
                    if (TextModelo != null)
                    {
                        //DataTable dtDrpModelo = CAT_PRODUCTODal.ClassInstance.GetProductModeloByProductType(drpProductType.SelectedValue);
                        //if (dtDrpModelo != null)
                        //{
                        //    BindDrpModelo(drpModelo, dtDrpModelo);
                        //}
                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Model"].ToString()))
                        {
                            TextModelo.Text = GridViewDetail.Rows[i]["Model"].ToString();
                            //ListItem itmSelected = drpModelo.Items.FindByValue(GridViewDetail.Rows[i]["Model"].ToString());
                            //if (itmSelected != null)
                            //    itmSelected.Selected = true;
                        }
                        else
                        {
                            TextModelo.Text = "";
                            //drpModelo.SelectedIndex = 0;
                        }
                        //foreach (ListItem item in drpModelo.Items)
                        //{
                        //    item.Attributes.Add("Title", item.Text);
                        //}
                        //drpModelo.ToolTip = drpModelo.SelectedItem.Text;
                    }

                    //Load embedded marca records
                    if (TextMarca != null)
                    {
                        //DataTable dtdrpMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_MARCADal();
                        //if (dtdrpMarca != null)
                        //{
                        //    drpMarca.DataSource = dtdrpMarca;
                        //    drpMarca.DataTextField = "Dx_Marca";
                        //    drpMarca.DataValueField = "Cve_Marca";
                        //    drpMarca.DataBind();
                        //    drpMarca.Items.Insert(0, "");
                        //}
                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Marca"].ToString()))
                        {
                            TextMarca.Text = GridViewDetail.Rows[i]["Marca"].ToString();
                            //ListItem itmSelected = drpMarca.Items.FindByValue(GridViewDetail.Rows[i]["Marca"].ToString());
                            //if (itmSelected != null)
                            //    itmSelected.Selected = true;
                        }
                        else
                        {
                            TextMarca.Text = "";
                            //drpMarca.SelectedIndex = 0;
                        }
                        //foreach (ListItem item in drpMarca.Items)
                        //{
                        //    item.Attributes.Add("Title", item.Text);
                        //}
                        //drpMarca.ToolTip = drpMarca.SelectedItem.Text;
                    }
                    //end edit by coco
                    if (drpCapacidad != null)
                    {
                        DataTable dtCapacidad = CAT_CAPACIDAD_SUSTITUCIONDal.ClassInstance.Get_CapacidaByTechnology(Convert.ToInt32(drpTechnology.SelectedValue)); // updated by tina 2012/04/12
                        if (dtCapacidad != null)
                        {
                            drpCapacidad.DataSource = dtCapacidad;
                            drpCapacidad.DataTextField = "No_Capacidad";
                            drpCapacidad.DataValueField = "Cve_Capacidad_Sust";
                            drpCapacidad.DataBind();
                            drpCapacidad.Items.Insert(0, "");
                        }
                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Capacidad"].ToString()))
                        {
                            drpCapacidad.SelectedValue = GridViewDetail.Rows[i]["Capacidad"].ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Antiguedad"].ToString()))
                    {
                        txtAntiguedad.Text = GridViewDetail.Rows[i]["Antiguedad"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Color"].ToString()))
                    {
                        txtColor.Text = GridViewDetail.Rows[i]["Color"].ToString();
                    }
                    if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Peso"].ToString()))
                    {
                        txtPeso.Text = GridViewDetail.Rows[i]["Peso"].ToString();
                    }
                    // added by tina 2012/04/12
                    if (drpTechnology.SelectedItem.Text.ToLower().Contains("aire acondicionado"))
                    {
                        lblPesoUnit.Text = "Tons";
                    }
                    else
                    {
                        lblPesoUnit.Text = "Kg";
                    }
                   // end
                }
            }
        }
        #endregion

        #region Private Methods

        // Comment by Tina 2011/08/31
        ///// <summary>
        ///// Save the GridView Data Before Create a new row
        ///// </summary>
        //private void SaveGridViewDetail()
        //{
        //    for (int i = 0; i < TechnologyGroupNumber; i++)
        //    {
        //        DataRow OldRow;
        //        OldRow = GridViewDetail.Rows[i];

        //        DropDownList drpTechnology = gdvReplacement.Rows[i].FindControl("drpTechnology") as DropDownList;
        //        DropDownList drpDisposalCenter = gdvReplacement.Rows[i].FindControl("drpDisposalCenter") as DropDownList;
        //        TextBox txtUnidades = gdvReplacement.Rows[i].FindControl("txtUnidades") as TextBox;

        //        OldRow["Technology"] = drpTechnology.SelectedIndex != -1 ? drpTechnology.SelectedValue.ToString() : "";
        //        OldRow["Unit"] = txtUnidades.Text.Trim();
        //        OldRow["DisposalCenter"] = drpDisposalCenter.SelectedIndex != -1 ? drpDisposalCenter.SelectedValue.ToString() : "";

        //        if (!drpTechnology.Enabled)
        //        {
        //            OldRow["Status"] = "1";
        //        }
        //        else
        //        {
        //            OldRow["Status"] = "0";
        //        }
        //    }
        //}

        /// <summary>
        /// get integrate screen data
        /// </summary>
        /// <param name="creditomodel"></param>
        private void LoadIntegrateDataFromUI(out EntidadCredito creditomodel)
        {
            creditomodel = new EntidadCredito();
            creditomodel.No_Credito = txtCreditoNum.Text;//Changed by Jerry 2011/08/12
            creditomodel.Cve_Estatus_Credito = (int)CreditStatus.PORENTREGAR;
            creditomodel.Dx_No_Escritura_Poder = txtRepresentative_LegalDocumentNumber.Text.Trim();

            if (txtRepresentative_LegalDocumentFecha.Text.Trim() != "")
            {
                creditomodel.Dt_Fecha_Poder = Convert.ToDateTime(txtRepresentative_LegalDocumentFecha.Text.Trim());
            }

            creditomodel.Dx_Nombre_Notario_Poder = txtRepresentative_NotariesProfessionName.Text.Trim();
            creditomodel.Dx_No_Notario_Poder = txtRepresentative_NotariesProfessionNumber.Text.Trim();

            if (drpRepresentative_Estado.SelectedIndex != -1 && drpRepresentative_Estado.SelectedIndex != 0)
            {
                creditomodel.Cve_Estado_Poder = int.Parse(drpRepresentative_Estado.SelectedValue);
            }

            if (drpRepresentative_OfficeorMunicipality.SelectedIndex != -1 && drpRepresentative_OfficeorMunicipality.SelectedIndex != 0)
            {
                creditomodel.Cve_Deleg_Municipio_Poder = int.Parse(drpRepresentative_OfficeorMunicipality.SelectedValue);
            }

            creditomodel.Dx_No_Escritura_Acta = txtApplicant_LegalDocumentNumber.Text.Trim();

            if (txtApplicant_LegalDocumentFecha.Text.Trim() != "")
            {
                creditomodel.Dt_Fecha_Acta = Convert.ToDateTime(txtApplicant_LegalDocumentFecha.Text.Trim());
            }

            creditomodel.Dx_Nombre_Notario_Acta = txtApplicant_NotariesProfessionName.Text.Trim();
            creditomodel.Dx_No_Notario_Acta = txtApplicant_NotariesProfessionNumber.Text.Trim();

            if (drpApplicant_Estado.SelectedIndex != -1 && drpApplicant_Estado.SelectedIndex != 0)
            {
                creditomodel.Cve_Estado_Acta = int.Parse(drpApplicant_Estado.SelectedValue);
            }

            if (drpApplicant_OfficeOrMunicipality.SelectedIndex != -1 && drpApplicant_OfficeOrMunicipality.SelectedIndex != 0)
            {
                creditomodel.Cve_Deleg_Municipio_Acta = int.Parse(drpApplicant_OfficeOrMunicipality.SelectedValue);
            }

            creditomodel.Dx_No_Escritura_Aval = txtGuarantee_LegalDocumentNumber.Text.Trim();

            if (txtGuarantee_LegalDocumentFecha.Text.Trim() != "")
            {
                creditomodel.Dt_Fecha_Escritura_Aval = Convert.ToDateTime(txtGuarantee_LegalDocumentFecha.Text.Trim());
            }

            creditomodel.Dx_Nombre_Notario_Escritura_Aval = txtGuarantee_NotariesProfessionName.Text.Trim();
            // Update by Tina 2011/08/04
            creditomodel.Dx_No_Notario_Escritura_Aval = txtGuarantee_NotariesProfessionNumber.Text.Trim();
            // End
            if (drpGuarantee_Estado.SelectedIndex != -1 && drpGuarantee_Estado.SelectedIndex != 0)
            {
                creditomodel.Cve_Estado_Escritura_Aval = int.Parse(drpGuarantee_Estado.SelectedValue);
            }

            if (drpGuarantee_OfficeOrMunicipality.SelectedIndex != -1 && drpGuarantee_OfficeOrMunicipality.SelectedIndex != 0)
            {
                creditomodel.Cve_Deleg_Municipio_Escritura_Aval = int.Parse(drpGuarantee_OfficeOrMunicipality.SelectedValue);
            }

            if (txtPropertyEncumbrancesFecha.Text.Trim() != "")
            {
                creditomodel.Dt_Fecha_Gravamen = Convert.ToDateTime(txtPropertyEncumbrancesFecha.Text.Trim());
            }

            creditomodel.Dx_Emite_Gravamen = txtPropertyEncumbrancesName.Text.Trim();
            creditomodel.Dx_Num_Acta_Matrimonio_Aval = txtMarriageNumber.Text.Trim();
            creditomodel.Dx_Registro_Civil_Mat_Aval = txtCitizenRegisterOffice.Text.Trim();
            // 2013-05-17 Preserve date if it already exists
            if (CreditDetail.Rows[0]["Dt_Fecha_Por_entregar"] != DBNull.Value)
                creditomodel.Dt_Fecha_Por_entregar = (DateTime)CreditDetail.Rows[0]["Dt_Fecha_Por_entregar"];
            else
                creditomodel.Dt_Fecha_Por_entregar = DateTime.Now;

            if (radAcquisition.Checked)
            {
                creditomodel.Fg_Adquisicion_Sust = 1;
            }
            else
            {
                creditomodel.Fg_Adquisicion_Sust = 2;
            }
            // Ad by Tina 2011/08/11
            Credit_Status = (int)CreditStatus.PORENTREGAR;
            // End

            // RSA 2012-12-13
            creditomodel.Dx_Usr_Ultmod = Session["UserName"].ToString();
        }

        private bool IsIntegerNumberValid(string strMatch)
        {
            string patrn = "^-?\\d+$";
            return Regex.IsMatch(strMatch, patrn, RegexOptions.IgnoreCase);
        }

        // Comment by Tina 2011/08/31
        ///// <summary>
        ///// check if the Technology is selected
        ///// </summary>
        //private bool CheckTechnologySelected(string Technology, int Num)
        //{
        //    bool result = false;
        //    for (int i = 0; i < Num; i++)
        //    {
        //        if (Technology != "" && GridViewDetail.Rows[i]["Technology"].ToString() == Technology)
        //        {
        //            result = true;
        //            break;
        //        }
        //    }
        //    return result;
        //}

        /// <summary>
        /// get replacement screen data
        /// </summary>
        /// <param name="listUpdate"></param>
        /// <param name="listAdd"></param>
        private bool LoadReplacementDataFromUI(out List<K_CREDITO_SUSTITUCIONModel> listUpdate, out List<K_CREDITO_SUSTITUCIONModel> listAdd)
        {
            listUpdate = new List<K_CREDITO_SUSTITUCIONModel>();
            listAdd = new List<K_CREDITO_SUSTITUCIONModel>();

            Dictionary<string, int> dicNumberForEachTechnologySelected = new Dictionary<string, int>();
            bool isComplete = true;

            for (int i = 0; i < gdvReplacement.Rows.Count; i++)
            {
                DropDownList drpTechnology = gdvReplacement.Rows[i].FindControl("drpTechnology") as DropDownList;
                DropDownList drpDisposalCenter = gdvReplacement.Rows[i].FindControl("drpDisposalCenter") as DropDownList;
                //TextBox txtUnidades = gdvReplacement.Rows[i].FindControl("txtUnidades") as TextBox; // Comment by Tina 2011/08/31
                // Added by Tina 2011/08/31
                DropDownList drpProductType = gdvReplacement.Rows[i].FindControl("drpProductType") as DropDownList; // updated by tina 2012/04/12
                //edit by coco 2012-07-17
                //TextBox txtModelo = gdvReplacement.Rows[i].FindControl("txtModelo") as TextBox;
                //TextBox txtMarca = gdvReplacement.Rows[i].FindControl("txtMarca") as TextBox;
                TextBox TextModelo = gdvReplacement.Rows[i].FindControl("TextModelo") as TextBox ;
                TextBox  TextMarca = gdvReplacement.Rows[i].FindControl("TextMarca") as TextBox ;
                //end edit by coco
                DropDownList drpCapacidad = gdvReplacement.Rows[i].FindControl("drpCapacidad") as DropDownList;
                TextBox txtAntiguedad = gdvReplacement.Rows[i].FindControl("txtAntiguedad") as TextBox;
                TextBox txtColor = gdvReplacement.Rows[i].FindControl("txtColor") as TextBox;
                TextBox txtPeso = gdvReplacement.Rows[i].FindControl("txtPeso") as TextBox;
                // End
                //if (!CheckTechnologySelected(GridViewDetail.Rows[i]["Technology"].ToString(), i))
                //{
                // for update
                if (GridViewDetail.Rows[i]["Id_Pre_Folio"].ToString() != "") // updated by tina 2012-02-29
                {
                    K_CREDITO_SUSTITUCIONModel modelUpdate = new K_CREDITO_SUSTITUCIONModel();
                    // Comment by Tina 2011/08/31
                    //modelUpdate.No_Credito = txtCreditoNum.Text;//Changed by Jerry 2011/08/12
                    //modelUpdate.Cve_Tecnologia = int.Parse(drpTechnology.SelectedValue.ToString());

                    //if (!string.IsNullOrEmpty(txtUnidades.Text.Trim()))
                    //{
                    //    modelUpdate.No_Unidades = int.Parse(txtUnidades.Text.Trim());
                    //}

                    if (drpDisposalCenter.SelectedIndex != -1 && drpDisposalCenter.SelectedIndex != 0)
                    {
                        modelUpdate.Id_Centro_Disp = int.Parse(drpDisposalCenter.SelectedValue.ToString().Substring(0, drpDisposalCenter.SelectedValue.IndexOf('-')));
                    }
                    else
                        isComplete = false;
                    modelUpdate.Dt_Fecha_Credito_Sustitucion = DateTime.Now.Date;

                    // Added by Tina 2011/08/31

                    // updated by tina 2012/04/12
                    if (drpProductType.SelectedIndex != -1 && drpProductType.SelectedIndex != 0)
                    {
                        modelUpdate.Dx_Tipo_Producto = Convert.ToInt32(drpProductType.SelectedValue);
                    }
                    else
                        isComplete = false;
                    // end
                    //edit by coco 2012-07-17
                    //if (drpModelo.SelectedIndex > 0)
                    //{
                    if (!string.IsNullOrEmpty(TextModelo.Text.Trim()))
                        modelUpdate.Dx_Modelo_Producto = TextModelo.Text ;
                    else
                        isComplete = false;
                    //}
                    //if (drpMarca.SelectedIndex > 0)
                    //{
                    if (!string.IsNullOrEmpty(TextMarca.Text.Trim()))
                        modelUpdate.Dx_Marca = TextMarca.Text;
                    else
                        isComplete = false;
                    //}
                    //end edit by coco
                    //if (!string.IsNullOrEmpty(txtCapacidad.Text.Trim()))
                    //{
                    //    modelUpdate.Ft_Capacidad = txtCapacidad.Text.Trim();
                    //}
                    if (drpCapacidad.SelectedIndex != 0 && drpCapacidad.SelectedIndex != -1)
                    {
                        modelUpdate.Cve_Capacidad_Sust = Convert.ToInt32(drpCapacidad.SelectedValue);
                    }
                    else
                        isComplete = false;
                    if (!string.IsNullOrEmpty(txtAntiguedad.Text.Trim()))
                    {
                        modelUpdate.Dx_Antiguedad = txtAntiguedad.Text.Trim();
                    }
                    else
                        isComplete = false;
                    if (drpDisposalCenter.SelectedIndex != 0 && drpDisposalCenter.SelectedIndex != -1)
                    {
                        if (drpDisposalCenter.SelectedItem.Text.Contains("(Matriz)")) // updated by tina 2012-02-29
                        {
                            modelUpdate.Fg_Tipo_Centro_Disp = "M";
                        }
                        else
                        {
                            modelUpdate.Fg_Tipo_Centro_Disp = "B";
                        }
                    }
                    if (!string.IsNullOrEmpty(txtColor.Text.Trim()))
                    {
                        modelUpdate.Dx_Color = txtColor.Text.Trim();
                    }
                    else
                        isComplete = false;
                    if (!string.IsNullOrEmpty(txtPeso.Text.Trim()))
                    {
                        modelUpdate.No_Peso = txtPeso.Text.Trim();
                    }
                    else
                        isComplete = false;
                    modelUpdate.Id_Pre_Folio = GridViewDetail.Rows[i]["Id_Pre_Folio"].ToString(); // updated by tina 2012-02-29
                    // End

                    if (isComplete)
                    {
                        listUpdate.Add(modelUpdate);
                    }
                }
                // for add
                else
                {
                    if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Technology"].ToString()))
                    {
                        K_CREDITO_SUSTITUCIONModel modelAdd = new K_CREDITO_SUSTITUCIONModel();
                        modelAdd.No_Credito = txtCreditoNum.Text;//Changed by Jerry 2011/08/12
                        modelAdd.Cve_Tecnologia = int.Parse(drpTechnology.SelectedValue.ToString());
                        // Comment by Tina 2011/08/31
                        //if (!string.IsNullOrEmpty(txtUnidades.Text.Trim()))
                        //{
                        //    modelAdd.No_Unidades = int.Parse(txtUnidades.Text.Trim());
                        //}
                        if (drpDisposalCenter.SelectedIndex != -1 && drpDisposalCenter.SelectedIndex != 0)
                        {
                            modelAdd.Id_Centro_Disp = int.Parse(drpDisposalCenter.SelectedValue.ToString().Substring(0, drpDisposalCenter.SelectedValue.IndexOf('-')));
                        }
                        else
                            isComplete = false;
                        modelAdd.Dt_Fecha_Credito_Sustitucion = DateTime.Now.Date;

                        // Added by Tina 2011/08/31

                        // updated by tina 2012/04/12
                        if (drpProductType.SelectedIndex != -1 && drpProductType.SelectedIndex != 0)
                        {
                            modelAdd.Dx_Tipo_Producto = Convert.ToInt32(drpProductType.SelectedValue);
                        }
                        else
                            isComplete = false;
                        // end
                        //edit by coco 2012-07-17
                        //if (drpModelo.SelectedIndex > 0)
                        //{
                        if (!string.IsNullOrEmpty(TextModelo.Text.Trim()))
                            modelAdd.Dx_Modelo_Producto = TextModelo.Text ;
                        else
                            isComplete = false;
                        //}
                        //if (dMarca.SelectedIndex > 0)
                        //{
                        if (!string.IsNullOrEmpty(TextMarca.Text.Trim()))
                            modelAdd.Dx_Marca = TextMarca.Text;
                        else
                            isComplete = false;
                        //}
                        //end edit by coco
                        // End
                        //if (!string.IsNullOrEmpty(txtCapacidad.Text.Trim()))
                        //{
                        //    modelAdd.Ft_Capacidad = txtCapacidad.Text.Trim();
                        //}
                        if (drpCapacidad.SelectedIndex != 0 && drpCapacidad.SelectedIndex != -1)
                        {
                            modelAdd.Cve_Capacidad_Sust = Convert.ToInt32(drpCapacidad.SelectedValue);
                        }
                        else
                            isComplete = false;
                        if (!string.IsNullOrEmpty(txtAntiguedad.Text.Trim()))
                        {
                            modelAdd.Dx_Antiguedad = txtAntiguedad.Text.Trim();
                        }
                        else
                            isComplete = false;
                        if (drpDisposalCenter.SelectedIndex != 0 && drpDisposalCenter.SelectedIndex != -1)
                        {
                            if (drpDisposalCenter.SelectedItem.Text.Contains("(Matriz)")) // updated by tina 2012-02-29
                            {
                                modelAdd.Fg_Tipo_Centro_Disp = "M";
                            }
                            else
                            {
                                modelAdd.Fg_Tipo_Centro_Disp = "B";
                            }
                        }
                        if (!string.IsNullOrEmpty(txtColor.Text.Trim()))
                        {
                            modelAdd.Dx_Color = txtColor.Text.Trim();
                        }
                        else
                            isComplete = false;
                        if (!string.IsNullOrEmpty(txtPeso.Text.Trim()))
                        {
                            modelAdd.No_Peso = txtPeso.Text.Trim();
                        }
                        else
                            isComplete = false;

                        CAT_TECNOLOGIAModel tecModel = new CAT_TECNOLOGIAModel();
                        tecModel = CAT_TECNOLOGIADAL.ClassInstance.Get_CAT_TECNOLOGIAByPKID(drpTechnology.SelectedValue);
                        //modelAdd.Id_Pre_Folio = txtCreditoNum.Text + "-" + LsUtility.GetNumberSequence("PRODUCTO") + "-" + tecModel.Dx_Cve_CC;
                        int consecutiveNumber = 0;
                        if (dicNumberForEachTechnologySelected.Keys.Contains(drpTechnology.SelectedValue))
                        {
                            consecutiveNumber = dicNumberForEachTechnologySelected[drpTechnology.SelectedValue] + 1;
                            dicNumberForEachTechnologySelected[drpTechnology.SelectedValue] = consecutiveNumber;
                        }
                        else
                        {
                            consecutiveNumber = 1;
                            dicNumberForEachTechnologySelected.Add(drpTechnology.SelectedValue, consecutiveNumber);
                        }
                        modelAdd.Id_Pre_Folio = txtCreditoNum.Text + "-" + consecutiveNumber.ToString() + "-" + tecModel.Dx_Cve_CC;
                        if (isComplete)
                        {
                            GridViewDetail.Rows[i]["Id_Pre_Folio"] = txtCreditoNum.Text + "-" + consecutiveNumber.ToString() + "-" + tecModel.Dx_Cve_CC;// added by tina 2012-02-29
                            listAdd.Add(modelAdd);
                        }
                    }
                }
                //}
            }

            return isComplete;
        }

        // Add by Tina 2011/08/11
        /// <summary>
        /// check if amount is enough to process current applied credit
        /// </summary>
        /// <returns></returns>
        private bool CheckCurrentAmountEnough()
        {
            bool result = false;
            decimal iCurrentAmount = 0;
            decimal iRequestAmount = 0;
            DataTable dtProgram = CAT_PROGRAMADal.ClassInstance.Get_All_CAT_PROGRAMAByPK(ID_Prog_Proy.ToString());
            if (dtProgram != null && dtProgram.Rows.Count > 0)
            {
                iCurrentAmount = dtProgram.Rows[0]["Mt_Fondo_Disponible"] == DBNull.Value ? 0 : decimal.Parse(dtProgram.Rows[0]["Mt_Fondo_Disponible"].ToString());
                iRequestAmount = txtRequestAmount.Text.Trim() == "" ? 0 : decimal.Parse(txtRequestAmount.Text.Trim().Substring(1));
                if (iCurrentAmount - iRequestAmount >= 0)
                {
                    RequestAmount = iRequestAmount;
                    result = true;
                }
            }
            return result;
        }
        // End
        #endregion

        #region Cancel Buttons
        private void Salir()
        {
            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];

            // if (Request.QueryString["Flag"] != null && Request.QueryString["Flag"].ToString() == "M")
            if (UserModel.Id_Rol == 3)
            {
                Response.Redirect("../SupplierModule/CreditMonitor.aspx");
            }
            else if (UserModel.Id_Rol == 1 || UserModel.Id_Rol == 2 || UserModel.Id_Rol == 6)
            {
                Response.Redirect("../RegionalModule/CreditAuthorization.aspx");
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        protected void btnSalirValidate_Click(object sender, EventArgs e)
        {
            Salir();
        }

        protected void btnSalirReplacement_Click(object sender, EventArgs e)
        {
            Salir();
        }

        protected void btnSalirIntegrate_Click(object sender, EventArgs e)
        {
            Salir();
        }

        protected void btnSalirPrint_Click(object sender, EventArgs e)
        {
            Salir();
        }
        #endregion

        #region Print Buttons
        protected void btnDisplayCreditRequest_Click(object sender, EventArgs e)
        {
            if (Tipo_Sociedad != (int)CompanyType.PERSONAFISICA
                && Tipo_Sociedad != (int)CompanyType.REPECO)  // RSA persona moral no, persona repeco si se checa
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=AutorizacionConsultaBuroPM&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=AutorizacionConsulta Buro&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

        }

        protected void btnDisplayQuota_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Presupuesto de Inversion&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayPaymentSchedule_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Tabla de Amortizacion&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayCreditCheckList_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Check List Expediente&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayCreditContract_Click(object sender, EventArgs e)
        {
            if (Tipo_Sociedad == (int)CompanyType.PERSONAFISICA
                /*&& Tipo_Sociedad != (int)CompanyType.PERSONAFISICACONACTIVIDADEMPRESARIAL*/)   // RSA persona moral
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=ContratoCreditoPF&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else if (Tipo_Sociedad == (int)CompanyType.REPECO)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=ContratoCreditoREPECO&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=ContratoCreditoPM&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        
        }

        protected void btnAmortPag_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=TablaAmortizacionPagare&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

        }
        protected void btnDisplayEquipmentAct_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Acta Entrega-Recepcion&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayCreditRequest1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Solicitud de Credito&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayPromissoryNote_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Pagare&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayGuaranteeEndorsement_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Endoso en Garantia&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayQuota1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Presupuesto de Inversion&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayGuarantee_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Carta Compromiso Aval&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayReceiptToSettle_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=EquipoBajaEficiencia&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayDisposalBonusReceipt_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Recibo de Chatarrizacion&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayRepaymentSchedule_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Tabla de Amortizacion&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayRequest_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Solicitud de Credito&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Boleta_EquipoBajaEficiencia&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }


        #endregion

        /*
        // Add by Tina 2011/08/03
        /// <summary>
        /// get Technology by Disposal Center 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpDisposalCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpDisposalCenter = (DropDownList)sender;
            GridViewRow row = (GridViewRow)drpDisposalCenter.Parent.Parent;
            int index = row.RowIndex;
            DropDownList drpTechnology = (DropDownList)gdvReplacement.Rows[index].FindControl("drpTechnology");
            if (drpDisposalCenter.SelectedIndex == 0 || drpDisposalCenter.SelectedIndex == -1)
            {
                drpTechnology.DataSource = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgram(ID_Prog_Proy.ToString());
            }
            else
            {
                drpTechnology.DataSource = CAT_TECNOLOGIABLL.ClassInstance.Get_CAT_TECNOLOGIAByProgramAndDisposal(ID_Prog_Proy, int.Parse(drpDisposalCenter.SelectedValue.ToString()));
            }
            drpTechnology.DataTextField = "Dx_Nombre_Particular";
            drpTechnology.DataValueField = "Cve_Tecnologia";
            drpTechnology.DataBind();
            drpTechnology.Items.Insert(0, "");
        }*/
        // End
    }
}
