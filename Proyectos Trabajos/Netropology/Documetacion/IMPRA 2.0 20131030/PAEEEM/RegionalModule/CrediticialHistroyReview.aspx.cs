using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;

namespace PAEEEM
{
    public partial class CrediticialHistroyReview : System.Web.UI.Page
    {
        #region Define Global Variables
        public int CreditNumber
        {
            get
            {
                return ViewState["CreditNumber"] == null ? 1 : int.Parse(ViewState["CreditNumber"].ToString());
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
        public int TechnologyGroupNumber
        {
            get
            {
                return ViewState["TechnologyGroupNumber"] == null ? 1 : int.Parse(ViewState["TechnologyGroupNumber"].ToString());
            }
            set
            {
                ViewState["TechnologyGroupNumber"] = value;
            }
        }
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                if (Request.QueryString["CreditNumber"] != null && Request.QueryString["CreditNumber"].ToString() != "")
                {
                    CreditNumber = Convert.ToInt32(System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["CreditNumber"].ToString().Replace("%2B", "+"))));
                }

                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                
                // Init Page Data
                InitDefaultData();
                EnableControl(false);
            }
        }

        private void EnableControl(bool bEnabled)
        {
            txtRequestAmount.Enabled = bEnabled;
            txtCreditYearsNumber.Enabled = bEnabled;
            txtPaymentPeriod.Enabled = bEnabled;
            txtRepresentative_LegalDocumentNumber.Enabled = bEnabled;
            txtRepresentative_LegalDocumentFecha.Enabled = bEnabled;
            txtRepresentative_NotariesProfessionName.Enabled = bEnabled;
            txtRepresentative_NotariesProfessionNumber.Enabled = bEnabled;
            drpRepresentative_Estado.Enabled = bEnabled;
            drpRepresentative_OfficeorMunicipality.Enabled = bEnabled;
            txtApplicant_LegalDocumentNumber.Enabled = bEnabled;
            txtApplicant_LegalDocumentFecha.Enabled = bEnabled;
            txtApplicant_NotariesProfessionName.Enabled = bEnabled;
            txtApplicant_NotariesProfessionNumber.Enabled = bEnabled;
            drpApplicant_Estado.Enabled = bEnabled;
            drpApplicant_OfficeOrMunicipality.Enabled = bEnabled;
            txtGuarantee_LegalDocumentNumber.Enabled = bEnabled;
            txtGuarantee_LegalDocumentFecha.Enabled = bEnabled;
            txtGuarantee_NotariesProfessionName.Enabled = bEnabled;
            txtGuarantee_NotariesProfessionNumber.Enabled = bEnabled;
            drpGuarantee_Estado.Enabled = bEnabled;
            drpGuarantee_OfficeOrMunicipality.Enabled = bEnabled;
            txtPropertyEncumbrancesFecha.Enabled = bEnabled;
            txtPropertyEncumbrancesName.Enabled = bEnabled;
            txtMarriageNumber.Enabled = bEnabled;
            txtCitizenRegisterOffice.Enabled = bEnabled;
            radAcquisition.Enabled = bEnabled;
            radReplacement.Enabled = bEnabled;
            gdvReplacement.Enabled = bEnabled;
        }

        /// <summary>
        /// Init Validate step Default Data
        /// </summary>
        private void InitDefaultData()
        {
            #region Init Validate Data
            DataTable dtCredito = K_CREDITODal.ClassInstance.GetCredits(CreditNumber);
            if (dtCredito != null && dtCredito.Rows.Count > 0)
            {
                ID_Prog_Proy = int.Parse(dtCredito.Rows[0]["ID_Prog_Proy"].ToString());
                Credit_Status = int.Parse(dtCredito.Rows[0]["Cve_Estatus_Credito"].ToString());
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
            }
            #endregion

            #region Init Integrate Data
            EstadoBind();
            DelegMunicipioBind();

            if (dtCredito != null && dtCredito.Rows.Count > 0)
            {
                txtRepresentative_LegalDocumentNumber.Text = dtCredito.Rows[0]["Dx_No_Escritura_Poder"].ToString();
                if (dtCredito.Rows[0]["Dt_Fecha_Poder"] != null && dtCredito.Rows[0]["Dt_Fecha_Poder"].ToString() != "")
                {
                    txtRepresentative_LegalDocumentFecha.Text = Convert.ToDateTime(dtCredito.Rows[0]["Dt_Fecha_Poder"].ToString()).ToString("yyyy-MM-dd");
                }
                txtRepresentative_NotariesProfessionName.Text = dtCredito.Rows[0]["Dx_Nombre_Notario_Poder"].ToString();
                txtRepresentative_NotariesProfessionNumber.Text = dtCredito.Rows[0]["Dx_No_Notario_Poder"].ToString();
                if (dtCredito.Rows[0]["Cve_Estado_Poder"] != null)
                {
                    drpRepresentative_Estado.SelectedValue = dtCredito.Rows[0]["Cve_Estado_Poder"].ToString();
                    RepresentativeDelegMunicipioBind();// Filter Deleg_Municipio_Poder by Estado_Poder
                }
                if (dtCredito.Rows[0]["Cve_Deleg_Municipio_Poder"] != null)
                {
                    drpRepresentative_OfficeorMunicipality.SelectedValue = dtCredito.Rows[0]["Cve_Deleg_Municipio_Poder"].ToString();
                }
                txtApplicant_LegalDocumentNumber.Text = dtCredito.Rows[0]["Dx_No_Escritura_Acta"].ToString();
                if (dtCredito.Rows[0]["Dt_Fecha_Acta"] != null && dtCredito.Rows[0]["Dt_Fecha_Acta"].ToString() != "")
                {
                    txtApplicant_LegalDocumentFecha.Text = Convert.ToDateTime(dtCredito.Rows[0]["Dt_Fecha_Acta"].ToString()).ToString("yyyy-MM-dd");
                }
                txtApplicant_NotariesProfessionName.Text = dtCredito.Rows[0]["Dx_Nombre_Notario_Acta"].ToString();
                txtApplicant_NotariesProfessionNumber.Text = dtCredito.Rows[0]["Dx_No_Notario_Acta"].ToString();
                if (dtCredito.Rows[0]["Cve_Estado_Acta"] != null)
                {
                    drpApplicant_Estado.SelectedValue = dtCredito.Rows[0]["Cve_Estado_Acta"].ToString();
                    ApplicantDelegMunicipioBind();// Filter Deleg_Municipio_Acta by Estado_Acta
                }
                if (dtCredito.Rows[0]["Cve_Deleg_Municipio_Acta"] != null)
                {
                    drpApplicant_OfficeOrMunicipality.SelectedValue = dtCredito.Rows[0]["Cve_Deleg_Municipio_Acta"].ToString();
                }
                txtGuarantee_LegalDocumentNumber.Text = dtCredito.Rows[0]["Dx_No_Escritura_Aval"].ToString();
                if (dtCredito.Rows[0]["Dt_Fecha_Escritura_Aval"] != null && dtCredito.Rows[0]["Dt_Fecha_Escritura_Aval"].ToString() != "")
                {
                    txtGuarantee_LegalDocumentFecha.Text = Convert.ToDateTime(dtCredito.Rows[0]["Dt_Fecha_Escritura_Aval"].ToString()).ToString("yyyy-MM-dd");
                }
                txtGuarantee_NotariesProfessionName.Text = dtCredito.Rows[0]["Dx_Nombre_Notario_Escritura_Aval"].ToString();
                txtGuarantee_NotariesProfessionNumber.Text = dtCredito.Rows[0]["Dx_No_Notario_Escritura_Aval"].ToString();
                if (dtCredito.Rows[0]["Cve_Estado_Escritura_Aval"] != null)
                {
                    drpGuarantee_Estado.SelectedValue = dtCredito.Rows[0]["Cve_Estado_Escritura_Aval"].ToString();
                    GuaranteeDelegMunicipioBind();// Filter Deleg_Municipio_Escritura_Aval by Estado_Escritura_Aval
                }
                if (dtCredito.Rows[0]["Cve_Deleg_Municipio_Escritura_Aval"] != null)
                {
                    drpGuarantee_OfficeOrMunicipality.SelectedValue = dtCredito.Rows[0]["Cve_Deleg_Municipio_Escritura_Aval"].ToString();
                }
                if (dtCredito.Rows[0]["Dt_Fecha_Gravamen"] != null && dtCredito.Rows[0]["Dt_Fecha_Gravamen"].ToString() != "")
                {
                    txtPropertyEncumbrancesFecha.Text = Convert.ToDateTime(dtCredito.Rows[0]["Dt_Fecha_Gravamen"].ToString()).ToString("yyyy-MM-dd");
                }
                txtPropertyEncumbrancesName.Text = dtCredito.Rows[0]["Dx_Emite_Gravamen"].ToString();
                txtMarriageNumber.Text = dtCredito.Rows[0]["Dx_Num_Acta_Matrimonio_Aval"].ToString();
                txtCitizenRegisterOffice.Text = dtCredito.Rows[0]["Dx_Registro_Civil_Mat_Aval"].ToString();

                if (dtCredito.Rows[0]["Fg_Adquisicion_Sust"].ToString() == "2")
                {
                    radAcquisition.Checked = false;
                    radReplacement.Checked = true;
                }
                else
                {
                    radAcquisition.Checked = true;
                    radReplacement.Checked = false;
                }
            }
            #endregion

            #region Init Replacement Data
            GridViewDetail = new DataTable();
            DataRow row;

            GridViewDetail.Columns.Add("Technology", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Unit", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("DisposalCenter", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Status", Type.GetType("System.String"));

            DataTable dtCreditoSustitucion = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONByNo_Credito(CreditNumber);
            if (dtCreditoSustitucion != null && dtCreditoSustitucion.Rows.Count > 0)
            {
                TechnologyGroupNumber = dtCreditoSustitucion.Rows.Count;
                //Init data source
                foreach (DataRow dataRow in dtCreditoSustitucion.Rows)
                {
                    row = GridViewDetail.NewRow();

                    row["Technology"] = dataRow["Technology"].ToString();
                    row["Unit"] = dataRow["Unit"] != null ? dataRow["Unit"].ToString() : "";
                    row["DisposalCenter"] = dataRow["DisposalCenter"] != null ? dataRow["DisposalCenter"].ToString() : "";
                    row["Status"] = "1";
                    GridViewDetail.Rows.Add(row);
                }
            }
            else
            {
                TechnologyGroupNumber = 1;

                row = GridViewDetail.NewRow();
                row["Technology"] = "";
                row["Unit"] = "";
                row["DisposalCenter"] = "";
                row["Status"] = "0";
                GridViewDetail.Rows.Add(row);
            }
            //Binding
            this.gdvReplacement.DataSource = GridViewDetail;
            this.gdvReplacement.DataBind();
            #endregion
        }

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

        /// <summary>
        /// databound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gdvReplacement_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < TechnologyGroupNumber; i++)
            {
                DropDownList drpTechnology = gdvReplacement.Rows[i].FindControl("drpTechnology") as DropDownList;
                DropDownList drpDisposalCenter = gdvReplacement.Rows[i].FindControl("drpDisposalCenter") as DropDownList;
                TextBox txtUnidades = gdvReplacement.Rows[i].FindControl("txtUnidades") as TextBox;

                if (drpTechnology != null)
                {
                    // Update by Tina 2011/08/03
                    DataTable TechnologyDataTable = CAT_TECNOLOGIABLL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgram(ID_Prog_Proy.ToString());
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

                    if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Status"].ToString()) && GridViewDetail.Rows[i]["Status"].ToString() == "1")
                    {
                        drpTechnology.Enabled = false;
                    }
                    else
                    {
                        drpTechnology.Enabled = true;
                    }
                }
                if (drpDisposalCenter != null)
                {
                    DataTable DisposalCenterDatatable = null;
                    if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Status"].ToString()) && GridViewDetail.Rows[i]["Status"].ToString() == "1")
                    {
                        DisposalCenterDatatable = CAT_CENTRO_DISPBLL.ClassInstance.Get_CAT_CENTRO_DISPByTECHNOLOGY(0, int.Parse(drpTechnology.SelectedValue));
                        if (DisposalCenterDatatable != null)
                        {
                            drpDisposalCenter.DataSource = DisposalCenterDatatable;
                            drpDisposalCenter.DataTextField = "Dx_Razon_Social";
                            drpDisposalCenter.DataValueField = "Id_Centro_Disp";
                            drpDisposalCenter.DataBind();
                            drpDisposalCenter.Items.Insert(0, "");
                        }
                    }
                    else
                    {
                        DisposalCenterDatatable = CAT_CENTRO_DISPBLL.ClassInstance.Get_All_CAT_CENTRO_DISP(0);
                        if (DisposalCenterDatatable != null)
                        {
                            drpDisposalCenter.DataSource = DisposalCenterDatatable;
                            drpDisposalCenter.DataTextField = "Dx_Razon_Social";
                            drpDisposalCenter.DataValueField = "Id_Centro_Disp";
                            drpDisposalCenter.DataBind();
                            drpDisposalCenter.Items.Insert(0, "");
                        }
                    }                 

                    if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["DisposalCenter"].ToString()))
                    {
                        drpDisposalCenter.SelectedValue = GridViewDetail.Rows[i]["DisposalCenter"].ToString();
                    }
                    else
                    {
                        drpDisposalCenter.SelectedIndex = 0;
                    }
                }

                if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Unit"].ToString()))
                {
                    txtUnidades.Text = GridViewDetail.Rows[i]["Unit"].ToString();
                }
            }
        }

        #region Print Buttons
        protected void btnDisplayCreditRequest_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Autorización Consulta Buro&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayQuota_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Presupuesto de Inversión&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayPaymentSchedule_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Tabla de Amortización&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayCreditCheckList_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Check List Expediente&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayCreditContract_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Solicitud de Crédito&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayEquipmentAct_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Acta Entrega-Recepción&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayCreditRequest1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Autorización Consulta Buro&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayPromissoryNote_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Pagaré&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayGuaranteeEndorsement_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Endoso en Garantía&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayQuota1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Presupuesto de Inversión&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayGuarantee_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Carta Compromiso Aval&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayReceiptToSettle_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Equipo Baja Eficiencia&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayDisposalBonusReceipt_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Recibo de Chatarrización&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayRepaymentSchedule_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Tabla de Amortización&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        #endregion

        #region Return Buttons
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Flag"] != null && Request.QueryString["Flag"].ToString() == "M")
            {
                Response.Redirect("../SupplierModule/CreditMonitor.aspx");
            }
            else
            {
                Response.Redirect("../RegionalModule/CreditAuthorization.aspx");
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../RegionalModule/CreditReview.aspx?creditno=" + CreditNumber + "&statusid=" + Credit_Status + "&Flag="+Request.QueryString["Flag"]);                        
        }
        #endregion
    }
}
