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
using PAEEEM.Helpers;
using PAEEEM.Entities;

namespace PAEEEM
{
    public partial class CrediticialHistroyReview : System.Web.UI.Page
    {
        #region Define Global Variables
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
                    //Changed by Jerry 2011/08/12
                    CreditNumber = System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["CreditNumber"].ToString().Replace("%2B", "+")));
                    txtCredito.Text = CreditNumber;
                }

                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                
                // Init Page Data
                InitDefaultData();
                InitMopValidation();
                EnableControl(false);
                int ResultCount = K_CREDITO_SUSTITUCIONDAL.ClassInstance.GetCountDtFeachReciveptionIsnull_Supplier(CreditNumber);
                if (ResultCount != 0)
                {
                    btnBoletaBajaEficiencia.Visible = true;
                }
                InitButtons();
            }
        }

        private void InitButtons()
        {
            if (Credit_Status == (int)CreditStatus.CANCELADO || Credit_Status == (int)CreditStatus.RECHAZADO
                || Credit_Status == (int)CreditStatus.BENEFICIARIO_CON_ADEUDOS || Credit_Status == (int)CreditStatus.Calificación_MOP_no_válida
                || Credit_Status == (int)CreditStatus.TARIFA_FUERA_DE_PROGRAMA)
            {
                btnDisplayCreditCheckList.Enabled = false;
                btnDisplayCreditContract.Enabled = false;
                btnDisplayEquipmentAct.Enabled = false;
                btnDisplayGuaranteeEndorsement.Enabled = false;
                btnDisplayPromissoryNote.Enabled = false;
                btnDisplayGuarantee.Enabled = false;
                btnDisplayQuota1.Enabled = false;
                btnDisplayDisposalBonusReceipt.Enabled = false;
                Button1.Enabled = false;
                BtnAmortPag.Enabled = false;
                btnDisplayReceiptToSettle.Enabled = false;
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
                Tipo_Sociedad = int.Parse(dtCredito.Rows[0]["Cve_Tipo_Sociedad"].ToString());

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
            //GridViewDetail.Columns.Add("Unit", Type.GetType("System.String")); // Comment by Tina 2011/09/01
            GridViewDetail.Columns.Add("DisposalCenter", Type.GetType("System.String"));
            // Added by Tina 2011/09/01
            GridViewDetail.Columns.Add("TypeOfProduct", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Model", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Marca", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("KeyNumber", Type.GetType("System.String"));
            // End
            GridViewDetail.Columns.Add("Status", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Capacidad", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Antiguedad", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("DisposalType", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Color", Type.GetType("System.String"));
            GridViewDetail.Columns.Add("Peso", Type.GetType("System.String"));

            K_TECNOLOGIA_MATERIALDAL ktm = new K_TECNOLOGIA_MATERIALDAL();

            TechnologyGroupNumber = 0;
            DataTable dtCreditoSustitucion = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONByNo_Credito(CreditNumber);
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
                        //row["Unit"] = dataRow["Unit"] != null ? dataRow["Unit"].ToString() : ""; // Comment by Tina 2011/09/01
                        //row["DisposalCenter"] = dataRow["DisposalCenter"] != DBNull.Value ? dataRow["DisposalCenter"].ToString() : ""; // Update by Tina 2011/09/01
                        // Added by Tina 2011/09/01
                        row["TypeOfProduct"] = dataRow["TypeOfProduct"] != DBNull.Value ? dataRow["TypeOfProduct"].ToString() : "";
                        row["Model"] = dataRow["Model"] != DBNull.Value ? dataRow["Model"].ToString() : "";
                        row["Marca"] = dataRow["Marca"] != DBNull.Value ? dataRow["Marca"].ToString() : "";
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
                        GridViewDetail.Rows.Add(row);
                    }
                }
            }
            if (TechnologyGroupNumber == 0)
            {
                TechnologyGroupNumber = 0; // Update by Tina 2011/09/01

                row = GridViewDetail.NewRow();
                row["Technology"] = "";
                //row["Unit"] = "";// Comment by Tina 2011/09/01
                row["DisposalCenter"] = "";
                // Added by Tina 2011/09/01
                row["TypeOfProduct"] = "";
                row["Model"] = "";
                row["Marca"] = "";
                row["KeyNumber"] = "";
                // End
                row["Status"] = "0";
                row["Capacidad"] = "";
                row["Antiguedad"] = "";
                row["DisposalType"] = "";
                row["Color"] = "";
                row["Peso"] = "";
                GridViewDetail.Rows.Add(row);
            }
            //Binding
            this.gdvReplacement.DataSource = GridViewDetail;
            this.gdvReplacement.DataBind();
            #endregion
        }
        private void InitMopValidation()
        {
            DataTable dt = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONByNo_CreditoFolio(CreditNumber);
            DataTable dt2 = K_CREDITO_SUSTITUCIONDAL.ClassInstance.Get_K_CREDITO_SUSTITUCIONByNo_Credito(CreditNumber);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value && Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                //btnDisplayEquipmentAct.Enabled = true;
                //btnDisplayDisposalBonusReceipt.Enabled = true;
                //btnDisplayReceiptToSettle.Enabled = true;
                btnBoletaBajaEficiencia.Enabled = true;
            }

            if (dt2.Rows.Count > 0 && dt2.Rows[0][0] != DBNull.Value && Convert.ToInt16(dt2.Rows[0][0]) > 0)
            {
                btnDisplayEquipmentAct.Enabled = true;
                btnDisplayDisposalBonusReceipt.Enabled = true;
                btnDisplayReceiptToSettle.Enabled = true;
                //btnBoletaBajaEficiencia.Enabled = true;
            }
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

        //added by tina 2012-07-18
        private void BindModelDropDownList(DropDownList drpModelo, DataTable dtModelo)
        {
            drpModelo.DataSource = dtModelo;
            drpModelo.DataTextField = "Dx_Modelo_Producto";
            drpModelo.DataValueField = "Dx_Modelo_Producto";
            drpModelo.DataBind();
            drpModelo.Items.Insert(0, "");
        }
        //end

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
                DropDownList drpProductType = gdvReplacement.Rows[0].FindControl("drpProductType") as DropDownList; // updated by tina 2012/04/13
                //updated by tina 2012-07-18
                //TextBox txtModelo = gdvReplacement.Rows[0].FindControl("txtModelo") as TextBox;
                //TextBox txtMarca = gdvReplacement.Rows[0].FindControl("txtMarca") as TextBox;
                DropDownList drpModelo = gdvReplacement.Rows[0].FindControl("drpModelo") as DropDownList;
                DropDownList drpMarca = gdvReplacement.Rows[0].FindControl("drpMarca") as DropDownList;
                //end
                // update by Tina 2011/11/22
                DropDownList drpCapacidad = gdvReplacement.Rows[0].FindControl("drpCapacidad") as DropDownList;
                TextBox txtAntiguedad = gdvReplacement.Rows[0].FindControl("txtAntiguedad") as TextBox;
                TextBox txtColor = gdvReplacement.Rows[0].FindControl("txtColor") as TextBox;
                TextBox txtPeso = gdvReplacement.Rows[0].FindControl("txtPeso") as TextBox;
                drpTechnology.Enabled = false;
                drpDisposalCenter.Enabled = false;
                drpProductType.Enabled = false; // updated by tina 2012/04/13
                //updated by tina 2012-07-18
                //txtModelo.Enabled = false;
                //txtMarca.Enabled = false;
                drpModelo.Enabled = false;
                drpMarca.Enabled = false;
                //end
                drpCapacidad.Enabled = false;
                txtAntiguedad.Enabled = false;
                txtColor.Enabled = false;
                txtPeso.Enabled = false;
                // end
                // added by tina 2012/04/13
                Label lblPesoUnit = gdvReplacement.Rows[0].FindControl("lblPesoUnit") as Label;
                lblPesoUnit.Text = "";
                // end
            }
            else
            {
                for (int i = 0; i < TechnologyGroupNumber; i++)
                {
                    DropDownList drpTechnology = gdvReplacement.Rows[i].FindControl("drpTechnology") as DropDownList;
                    DropDownList drpDisposalCenter = gdvReplacement.Rows[i].FindControl("drpDisposalCenter") as DropDownList;
                    //TextBox txtUnidades = gdvReplacement.Rows[i].FindControl("txtUnidades") as TextBox;// Comment by Tina 2011/09/01
                    // Added by Tina 2011/09/01
                    DropDownList drpProductType = gdvReplacement.Rows[i].FindControl("drpProductType") as DropDownList; // updated by tina 2012/04/13
                    //updated by tina 2012-07-18
                    //TextBox txtModelo = gdvReplacement.Rows[i].FindControl("txtModelo") as TextBox;
                    //TextBox txtMarca = gdvReplacement.Rows[i].FindControl("txtMarca") as TextBox;
                    DropDownList drpModelo = gdvReplacement.Rows[i].FindControl("drpModelo") as DropDownList;
                    DropDownList drpMarca = gdvReplacement.Rows[i].FindControl("drpMarca") as DropDownList;
                    //end update
                    // End
                    // added  by tina 2011/11/22
                    DropDownList drpCapacidad = gdvReplacement.Rows[i].FindControl("drpCapacidad") as DropDownList;
                    TextBox txtAntiguedad = gdvReplacement.Rows[i].FindControl("txtAntiguedad") as TextBox;
                    TextBox txtColor = gdvReplacement.Rows[i].FindControl("txtColor") as TextBox;
                    TextBox txtPeso = gdvReplacement.Rows[i].FindControl("txtPeso") as TextBox;
                    // End
                    // added by tina 2012/04/13
                    Label lblPesoUnit = gdvReplacement.Rows[i].FindControl("lblPesoUnit") as Label;
                    // end

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
                        // Comment by Tina 2011/08/24
                        //if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Status"].ToString()) && GridViewDetail.Rows[i]["Status"].ToString() == "1")
                        //{
                        //    drpTechnology.Enabled = false;
                        //}
                        //else
                        //{
                        //    drpTechnology.Enabled = true;
                        //}
                        // Added by Tina 2011/09/05
                        drpTechnology.ToolTip = drpTechnology.SelectedItem.Text; 
                        // End
                    }
                    if (drpDisposalCenter != null)
                    {
                        DataTable DisposalCenterDatatable = null;
                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Status"].ToString()) && GridViewDetail.Rows[i]["Status"].ToString() == "1")
                        {
                            US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];

                            DisposalCenterDatatable = CAT_CENTRO_DISPBLL.ClassInstance.Get_CAT_CENTRO_DISPByTECHNOLOGY(2, drpTechnology.SelectedValue.ToString(),UserModel.Id_Usuario);// Comment by Tina 2011/08/24
                            if (DisposalCenterDatatable != null)
                            {
                                drpDisposalCenter.DataSource = DisposalCenterDatatable;
                                drpDisposalCenter.DataTextField = "Dx_Razon_Social";
                                drpDisposalCenter.DataValueField = "Id_Centro_Disp";
                                drpDisposalCenter.DataBind();
                                drpDisposalCenter.Items.Insert(0, "");
                            }
                        }
                        // Comment by Tina 2011/08/24
                        //else
                        //{
                        //    DisposalCenterDatatable = CAT_CENTRO_DISPBLL.ClassInstance.Get_All_CAT_CENTRO_DISP(0);
                        //    if (DisposalCenterDatatable != null)
                        //    {
                        //        drpDisposalCenter.DataSource = DisposalCenterDatatable;
                        //        drpDisposalCenter.DataTextField = "Dx_Razon_Social";
                        //        drpDisposalCenter.DataValueField = "Id_Centro_Disp";
                        //        drpDisposalCenter.DataBind();
                        //        drpDisposalCenter.Items.Insert(0, "");
                        //    }
                        //}                 

                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["DisposalCenter"].ToString()))
                        {
                            drpDisposalCenter.SelectedValue = GridViewDetail.Rows[i]["DisposalCenter"].ToString();
                        }
                        else
                        {
                            drpDisposalCenter.Items.Insert(0, GridViewDetail.Rows[i]["DisposalCenter"].ToString());
                            drpDisposalCenter.SelectedIndex = 0;
                        }
                        // Added by Tina 2011/09/05
                        drpDisposalCenter.ToolTip = drpDisposalCenter.SelectedItem.Text; 
                        // End
                    }

                    // Comment by Tina 2011/09/01
                    //if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Unit"].ToString()))
                    //{
                    //    txtUnidades.Text = GridViewDetail.Rows[i]["Unit"].ToString();
                    //}
                    // Added by Tina 2011/09/01
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
                        drpProductType.ToolTip = drpProductType.SelectedItem.Text;
                    }
                    // end
                    //updated by tina 2012-07-18
                    //if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Model"].ToString()))
                    //{
                    //    txtModelo.Text = GridViewDetail.Rows[i]["Model"].ToString();
                    //}
                    //if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Marca"].ToString()))
                    //{
                    //    txtMarca.Text = GridViewDetail.Rows[i]["Marca"].ToString();
                    //}
                    if (drpModelo != null)
                    {
                        DataTable dtDrpModelo = CAT_PRODUCTODal.ClassInstance.GetProductModeloByProductType(drpProductType.SelectedValue);
                        if (dtDrpModelo != null)
                        {
                            BindModelDropDownList(drpModelo, dtDrpModelo);
                        }
                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Model"].ToString()))
                        {
                            ListItem itmSelected = drpModelo.Items.FindByValue(GridViewDetail.Rows[i]["Model"].ToString());
                            if (itmSelected != null)
                                itmSelected.Selected = true;
                            else
                            {
                                drpModelo.Items.Insert(0, GridViewDetail.Rows[i]["Model"].ToString());
                                drpModelo.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            drpModelo.SelectedIndex = 0;
                        }
                        foreach (ListItem item in drpModelo.Items)
                        {
                            item.Attributes.Add("Title", item.Text);
                        }
                        drpModelo.ToolTip = drpModelo.SelectedItem.Text;
                    }
                    if (drpMarca != null)
                    {
                        DataTable dtdrpMarca = CAT_MARCADal.ClassInstance.Get_ALL_CAT_MARCADal();
                        if (dtdrpMarca != null)
                        {
                            drpMarca.DataSource = dtdrpMarca;
                            drpMarca.DataTextField = "Dx_Marca";
                            drpMarca.DataValueField = "Cve_Marca";
                            drpMarca.DataBind();
                            drpMarca.Items.Insert(0, "");
                        }
                        if (!string.IsNullOrEmpty(GridViewDetail.Rows[i]["Marca"].ToString()))
                        {
                            ListItem itmSelected = drpMarca.Items.FindByValue(GridViewDetail.Rows[i]["Marca"].ToString());
                            if (itmSelected != null)
                                itmSelected.Selected = true;
                            else
                            {
                                drpMarca.Items.Insert(0, GridViewDetail.Rows[i]["Marca"].ToString());
                                drpMarca.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            drpMarca.SelectedIndex = 0;
                        }
                        foreach (ListItem item in drpMarca.Items)
                        {
                            item.Attributes.Add("Title", item.Text);
                        }
                        drpMarca.ToolTip = drpMarca.SelectedItem.Text;
                    }
                    //end update
                    // End
                    // added by Tina 2011/11/22
                    if (drpCapacidad != null)
                    {
                        DataTable dtCapacidad = CAT_CAPACIDAD_SUSTITUCIONDal.ClassInstance.Get_CapacidaByTechnology(Convert.ToInt32(drpTechnology.SelectedValue)); // updated by tina 2012/04/13
                        if (dtCapacidad != null)
                        {
                            drpCapacidad.DataSource = dtCapacidad;
                            drpCapacidad.DataTextField = "No_Capacidad"; // updated by tina 2012/04/13
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
                    // end
                    // added by tina 2012/04/13
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

        #region Print Buttons
        protected void btnDisplayCreditRequest_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Solicitud de Credito&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
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
                /*&& Tipo_Sociedad != (int)CompanyType.PERSONAFISICACONACTIVIDADEMPRESARIAL*/)  // RSA persona moral
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=ContratoCreditoPF&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else if (Tipo_Sociedad == (int)CompanyType.REPECO)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=ContratoCreditoREPECO&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=ContratoCreditoPM&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

            
         //   ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Solicitud de Credito&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayEquipmentAct_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Acta Entrega-Recepcion&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayCreditRequest1_Click(object sender, EventArgs e)
        {
            if (Tipo_Sociedad != (int)CompanyType.PERSONAFISICA && Tipo_Sociedad != (int)CompanyType.REPECO
                /*&& Tipo_Sociedad != (int)CompanyType.PERSONAFISICACONACTIVIDADEMPRESARIAL*/)  // RSA persona moral
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=AutorizacionConsultaBuroPM&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=AutorizacionConsulta Buro&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

         }

        protected void btnDisplayPromissoryNote_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Pagare&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnAmortPag_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=TablaAmortizacionPagare&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

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
        #endregion

        #region Return Buttons
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Salir();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../RegionalModule/CreditReview.aspx?creditno=" + CreditNumber + "&statusid=" + Credit_Status + "&Flag="+Request.QueryString["Flag"]);                        
        }
        #endregion

        protected void btnBoletaBajaEficiencia_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Boleta_EquipoBajaEficiencia&CreditNumber=" + CreditNumber + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

        }
    }
}
