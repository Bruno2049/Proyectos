using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.SupplierModule.Controls
{
    public partial class WucValidacionCrediticia : UserControl
    {
        public string CreditNumber
        {
            get
            {
                return ViewState["CreditNumber"] == null ? "" : ViewState["CreditNumber"].ToString();
            }
            set { ViewState["CreditNumber"] = value; }
        }

        public int CreditStatus
        {
            get { return ViewState["CreditStatus"] == null ? 1 : int.Parse(ViewState["CreditStatus"].ToString()); }
            set { ViewState["CreditStatus"] = value; }
        }
        public DatosCredito CreditDetail
        {
            get
            {
                return ViewState["CreditDetail"] == null ? null : ViewState["CreditDetail"] as DatosCredito;
            }
            set
            {
                ViewState["CreditDetail"] = value;
            }
        }
        public int IdProgProy
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
        public int TipoSociedad
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //    if (Request.QueryString["CreditNumber"] != null && Request.QueryString["CreditNumber"] != "")
                //    {
                //        CreditNumber =
                //            System.Text.Encoding.Default.GetString(
                //                Convert.FromBase64String(Request.QueryString["CreditNumber"].Replace("%2B", "+")));
                //    }
                //    if (Request.QueryString["Flag"] != null && Request.QueryString["Flag"] != "")
                //    {
                //        CreditStatus = (int)Helpers.CreditStatus.PENDIENTE;
                //    }
                
                //    // Initial default data
                 // InitDefaultData();
                txtFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                
                #region CodigoAnterior
                //if (dtCredito != null & > 0)
                //{
                //    ID_Prog_Proy = int.Parse(dtCredito.Rows[0]["ID_Prog_Proy"].ToString());
                //    CreditStatus = int.Parse(dtCredito.Rows[0]["Cve_Estatus_Credito"].ToString());
                //    Tipo_Sociedad = int.Parse(dtCredito.Rows[0]["Cve_Tipo_Sociedad"].ToString()); //Tipo de persona
                //    this.txtRazonSocial.Text = dtCredito.Rows[0]["Dx_Razon_Social"].ToString();
                //    if (!Convert.IsDBNull(dtCredito.Rows[0]["Mt_Monto_Solicitado"]))
                //    {
                //        this.txtRequestAmount.Text = string.Format("{0:C2}", dtCredito.Rows[0]["Mt_Monto_Solicitado"]);
                //    }
                //    if (!Convert.IsDBNull(dtCredito.Rows[0]["No_Plazo_Pago"] != null))
                //    {
                //        this.txtCreditYearsNumber.Text = dtCredito.Rows[0]["No_Plazo_Pago"].ToString();
                //    }
                //    if (!Convert.IsDBNull(dtCredito.Rows[0]["Dx_Periodo_Pago"]) && dtCredito.Rows[0]["Dx_Periodo_Pago"].ToString() != "")
                //    {
                //        this.txtPaymentPeriod.Text = dtCredito.Rows[0]["Dx_Periodo_Pago"].ToString();
                //    }
                //    this.radAcquisition.Checked = false;
                //    this.radReplacement.Checked = true;
                //}
                //this.txtRequestAmount.Enabled = false;
                //this.txtCreditYearsNumber.Enabled = false;
                //this.txtPaymentPeriod.Enabled = false;
                #endregion
                
                //    // 2013-05-17 Do not allow to make changes, if it's already in "Por Entregar", go straight to Print page
                //    if (CreditStatus == (int)Helpers.CreditStatus.PORENTREGAR)
                //    {
                //        //lblTitle.Text = @"IMPRIMIR EXPEDIENTE";
                //        //wizardPages.ActiveStepIndex = 3;
                //        // Button btnPrev = (Button)wizardPages.FindControl("FinishNavigationTemplateContainerID$FinishPreviousButton");
                //        // btnPrev.Enabled = false;
                //        // btnPrev.Visible = false;
                //    }
                //}

                // InitMopValidation();

            }
        }

        public bool LoadPage()
        {
            try
            {
                CreditNumber = Session["CreditNumber"].ToString();
                txtCreditoNum.Text = CreditNumber;
                //DataTable dtCredito = K_CREDITODal.ClassInstance.GetCredits(CreditNumber);
                var dtCredito = AccesoDatos.SolicitudCredito.CREDITO_DAL.ClassInstance.GetDatosCredito(CreditNumber);
                CreditDetail = dtCredito;

                if (CreditDetail!=null)
                {
                    IdProgProy = (int) CreditDetail.IdProgProy;
                    CreditStatus = CreditDetail.CveEstatusCredito;
                    TipoSociedad = (int) CreditDetail.CveTipoSociedad;
                    txtRazonSocial.Text = CreditDetail.RazonSocial;
                    txtRequestAmount.Text = string.Format("{0:C2}", CreditDetail.MontoSolicitado);
                    txtCreditYearsNumber.Text = CreditDetail.NoPlazoPago.ToString();
                    txtPaymentPeriod.Text = CreditDetail.NoPlazoPago.ToString();
                    
                }

                txtRequestAmount.Enabled = false;
                txtCreditYearsNumber.Enabled = false;
                txtPaymentPeriod.Enabled = false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void InitDefaultData()
        {
            // Step Validate
            InitValidateStepDefaultData();

            //InitIntegrateStepDefaultData(); // Llena Datos del representante Legal
            //InitReplacementStepDefaultData(); // Llena Datos de los equipos de sustitucion
        }

        //private bool InitMopValidation()
        //{
        //   // Button startNext = wizardPages.FindControl("StartNavigationTemplateContainerID").FindControl("StartNextButton") as Button;
        //    var valorButtonNextWizzard = false;
        //    var NoMop = 0;
        //    if (CreditStatus == (int)Helpers.CreditStatus.PENDIENTE)
        //    {
        //        if (CreditDetail != null)
        //        {
        //            foreach (var valor in CreditDetail)
        //            {
        //                NoMop = valor.NoMop;
        //            }

        //            if (string.IsNullOrEmpty(NoMop.ToString(CultureInfo.InvariantCulture)))
        //            {
        //                var symbol = new char[] { '-' };
        //                var creditNum = CreditNumber.Substring(CreditNumber.LastIndexOfAny(symbol) + 1);
        //                //Changed by Tina 2011/08/12 2011/08/24

        //                while (creditNum.Substring(0, 1) == "0")
        //                {
        //                    creditNum = creditNum.Substring(1);
        //                }
        //                {
        //                    btnConsultaCrediticia.Attributes.Add("onclick",
        //                        "if (confirm('Confirmar realizar Consulta Crediticia\\n\\nNO podrá editar el crédito después de hacer la consulta crediticia')) {this.style.display ='none'; } else { return false; }");
        //                }
        //                lblImporte.Visible = true;
        //                txtContratoImporte.Visible = true;
        //                revContratoImporte.Visible = true;
        //                btnConsultaCrediticia.Visible = true;
        //                valorButtonNextWizzard = false;
        //                 // startNext.Enabled = false; ver funcionamiento
        //            }
        //            else
        //            {
        //                // no row, or row with non empty cell
        //                lblImporte.Visible = false;
        //                txtContratoImporte.Visible = false;
        //                revContratoImporte.Visible = false;
        //                btnConsultaCrediticia.Visible = false;

        //                var valid = ValidateMop();

        //               // startNext.Enabled = valid;
        //                valorButtonNextWizzard = valid;
        //                lblMopInvalido.Visible = !valid;
        //            }
        //        }
        //        else
        //        {
        //            lblImporte.Visible = false;
        //            txtContratoImporte.Visible = false;
        //            revContratoImporte.Visible = false;
        //            btnConsultaCrediticia.Visible = false;
        //            valorButtonNextWizzard = false;
        //           // startNext.Enabled = false;
        //        }
        //    }
        //    else if (CreditStatus == (int)Helpers.CreditStatus.RECHAZADO ||
        //             CreditStatus == (int)Helpers.CreditStatus.CANCELADO
        //             || CreditStatus == (int)Helpers.CreditStatus.BENEFICIARIO_CON_ADEUDOS ||
        //             CreditStatus == (int)Helpers.CreditStatus.TARIFA_FUERA_DE_PROGRAMA
        //             || CreditStatus == (int)Helpers.CreditStatus.Calificación_MOP_no_válida)
        //    {
        //         //no permite avanzar
        //        lblImporte.Visible = false;
        //        txtContratoImporte.Visible = false;
        //        revContratoImporte.Visible = false;
        //        btnConsultaCrediticia.Visible = false;
        //        valorButtonNextWizzard = false;
        //        //startNext.Enabled = false;
        //        lblMopInvalido.Visible = (CreditStatus == (int)Helpers.CreditStatus.Calificación_MOP_no_válida);
        //    }
        //    else //if (Credit_Status != (int)CreditStatus.PENDIENTE)
        //    {
        //        //PORENTREGAR = 2,
        //        //ENREVISION = 3,
        //        //AUTORIZADO = 4,
        //        //PARAFINANZAS = 6,

        //        // si avanza                
        //        lblImporte.Visible = false;
        //        txtContratoImporte.Visible = false;
        //        revContratoImporte.Visible = false;
        //        btnConsultaCrediticia.Visible = false;
        //        valorButtonNextWizzard = true;
        //      //  startNext.Enabled = true;
        //    }
        //    return valorButtonNextWizzard;
        //}

        private void InitValidateStepDefaultData()
        {
            txtFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //txtCreditoNum.Text = CreditNumber;

            // DataTable dtCredito = K_CREDITODal.ClassInstance.GetCredits(CreditNumber);
            var dtCredito = AccesoDatos.SolicitudCredito.CREDITO_DAL.ClassInstance.GetDatosCredito(CreditNumber);
            CreditDetail = dtCredito;

            if (CreditDetail != null)
            {
                //foreach (var valor in dtCredito)
                //{
                //    IdProgProy = valor.IdProgProy;
                //    CreditStatus = valor.CveEstatusCredito;
                //    TipoSociedad = valor.CveTipoSociedad;
                //    txtRazonSocial.Text = valor.RazonSocial;
                //    txtRequestAmount.Text = string.Format("{0:C2}", valor.MontoSolicitado);
                //    txtCreditYearsNumber.Text = valor.NoPlazoPago.ToString(CultureInfo.InvariantCulture);
                //    txtPaymentPeriod.Text = valor.NoPlazoPago.ToString(CultureInfo.InvariantCulture);
                //}
            }
            //txtRequestAmount.Enabled = false;
            //    txtCreditYearsNumber.Enabled = false;
            //    txtPaymentPeriod.Enabled = false;
            

            #region CodigoAnterior
            //if (dtCredito != null & > 0)
            //{
            //    ID_Prog_Proy = int.Parse(dtCredito.Rows[0]["ID_Prog_Proy"].ToString());
            //    CreditStatus = int.Parse(dtCredito.Rows[0]["Cve_Estatus_Credito"].ToString());
            //    Tipo_Sociedad = int.Parse(dtCredito.Rows[0]["Cve_Tipo_Sociedad"].ToString()); //Tipo de persona
            //    this.txtRazonSocial.Text = dtCredito.Rows[0]["Dx_Razon_Social"].ToString();
            //    if (!Convert.IsDBNull(dtCredito.Rows[0]["Mt_Monto_Solicitado"]))
            //    {
            //        this.txtRequestAmount.Text = string.Format("{0:C2}", dtCredito.Rows[0]["Mt_Monto_Solicitado"]);
            //    }
            //    if (!Convert.IsDBNull(dtCredito.Rows[0]["No_Plazo_Pago"] != null))
            //    {
            //        this.txtCreditYearsNumber.Text = dtCredito.Rows[0]["No_Plazo_Pago"].ToString();
            //    }
            //    if (!Convert.IsDBNull(dtCredito.Rows[0]["Dx_Periodo_Pago"]) && dtCredito.Rows[0]["Dx_Periodo_Pago"].ToString() != "")
            //    {
            //        this.txtPaymentPeriod.Text = dtCredito.Rows[0]["Dx_Periodo_Pago"].ToString();
            //    }
            //    this.radAcquisition.Checked = false;
            //    this.radReplacement.Checked = true;
            //}
            //this.txtRequestAmount.Enabled = false;
            //this.txtCreditYearsNumber.Enabled = false;
            //this.txtPaymentPeriod.Enabled = false;
            #endregion
        }

        //private bool ValidateMop()
        //{
        //    var valid = false;
        //    //var dtAuxiliar = CAT_AUXILIARDal.ClassInstance.Get_CAT_AUXILIARByCreditNo(CreditNumber);//Consulta Cat_Auxiliar
        //    //if (dtAuxiliar == null || dtAuxiliar.Rows.Count == 0)
        //    //    return false;
        //    var dtProgram = AccesoDatos.SolicitudCredito.CREDITO_DAL.getDatosCat_Programa(IdProgProy);
        //    //var dtProgram = CAT_PROGRAMADal.ClassInstance.Get_All_CAT_PROGRAMAByPK(ID_Prog_Proy.ToString());
        //    var califmop = dtProgram.No_Calif_MOP ?? 4;
        //    //var califmop = dtProgram != null && dtProgram.Rows.Count > 0 &&
        //    //               dtProgram.Rows[0]["No_Calif_MOP"] != DBNull.Value
        //    //    ? int.Parse(dtProgram.Rows[0]["No_Calif_MOP"].ToString())
        //    //    : 4;
        //    if (CreditDetail.Count > 0)
        //    {
        //        foreach (var valor in CreditDetail)
        //        {
        //            //if (CreditDetail != null && CreditDetail.Rows.Count > 0
        //            //    && !string.IsNullOrEmpty(CreditDetail.Rows[0]["Dt_Fecha_Calificación_MOP_no_válida"].ToString()))
        //            //    return false;
        //            if (string.IsNullOrEmpty(valor.FechaCalificacionMopNoValida.ToString(CultureInfo.InvariantCulture)))
        //                return false;
        //            //if (string.IsNullOrEmpty(dtAuxiliar.Rows[0]["No_MOP"].ToString()))
        //            //    return false;
        //            if (string.IsNullOrEmpty(valor.NoMop.ToString(CultureInfo.InvariantCulture)))
        //                return false;

        //            if (CreditStatus == (int)Helpers.CreditStatus.Calificación_MOP_no_válida)
        //                return false;

        //            var mop = valor.NoMop.ToString(CultureInfo.InvariantCulture); // .Rows[0]["No_MOP"].ToString();
        //            valid = (int.Parse(mop) >= 0 && int.Parse(mop) <= califmop);
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return valid;
        //}

        //protected void btnConsultaCrediticia_Click(object sender, EventArgs e)
        //{
        //    var mop = string.Empty;
        //    var folio = string.Empty;
        //    var message = string.Empty;
        //    var califmop = 0; // the No_Calif_MOP in CAT_PROGRAMA table
        //    var isValid = false;
        //    var result = 0;
        //    var retry = false;

        //    var instance = new CAT_AUXILIAREntity();

        //    var appstate = new LsApplicationState(HttpContext.Current.Application);

        //    if (!string.IsNullOrEmpty(hiddenfield.Value))
        //    {
        //        if (hiddenfield.Value != "Cancel")
        //        {
        //            if (Page.IsValid)
        //            {
        //                instance.No_Credito = CreditNumber;
        //                var symbol = new[] { '-' };
        //                var creditNum = CreditNumber.Substring(CreditNumber.LastIndexOfAny(symbol) + 1);

        //                while (creditNum.Substring(0, 1) == "0")
        //                {
        //                    creditNum = creditNum.Substring(1);
        //                }
        //                if (IsIntegerNumberValid(hiddenfield.Value))
        //                {
        //                    mop = hiddenfield.Value;
        //                    instance.No_MOP = mop;
        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                       //** Temporal for validation testing
        //                        if (TipoSociedad == (int)CompanyType.MORAL)
        //                        {
        //                            if (!appstate.DebugMode.Equals("false") && !appstate.DebugMode.Equals("pm"))
        //                            {
        //                                //** use the last two digits of the amount
        //                                //** if only one is provided throw an exception to simulate web service not responding
        //                                if (txtContratoImporte.Text.Length > 0)
        //                                    mop = txtContratoImporte.Text.Substring(txtContratoImporte.Text.Length - 2,
        //                                        2);
        //                                else
        //                                    mop = "04";
        //                                folio = "1";
        //                            }
        //                            else
        //                            {
        //                                if (CreditDetail.Count > 0)
        //                                {
        //                                    foreach (var valor in CreditDetail)
        //                                    {
        //                                        var pm = new PMHelper
        //                                        {
        //                                            Producto = "001",
        //                                            Rfc = valor.RFC, //** CreditDetail.Rows[0]["DX_RFC"].ToString(),
        //                                            Nombres = valor.RazonSocial, //** CreditDetail.Rows[0]["Dx_Razon_Social"].ToString(),
        //                                            Direccion1 = valor.Calle + " " + valor.NumExt,//**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Calle"].ToString() +" " + CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Num"].ToString(),
        //                                            CodigoPostal = valor.CodigoPostal, //**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_CP"].ToString(),
        //                                            Colonia = valor.Colonia, //**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Colonia"].ToString(),
        //                                            Ciudad = valor.DelegMunicipio,//** CreditDetail.Rows[0]["Dx_Deleg_Municipio"].ToString(),
        //                                            Estado = valor.CvePm,//** CreditDetail.Rows[0]["Dx_Cve_PM"].ToString(),
        //                                            Pais = "MX"
        //                                        };

        //                                        pm.ConsultarPersonaMoral();

        //                                        mop = pm.Mop;
        //                                        folio = pm.Folio;
        //                                    }

        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (!appstate.DebugMode.Equals("false"))
        //                            {
        //                                 //**use the last two digits of the amount
        //                                 //**if only one is provided throw an exception to simulate web service not responding
        //                                if (txtContratoImporte.Text.Length > 0)
        //                                    mop = txtContratoImporte.Text.Substring(txtContratoImporte.Text.Length - 2,
        //                                        2);
        //                                else
        //                                    mop = "04";
        //                                folio = "1";
        //                            }
        //                            else
        //                            {
        //                                var helper = new CCHelper();

        //                                foreach (var valor in CreditDetail)
        //                                {

        //                                    helper.Calle = valor.Calle; //**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Calle"].ToString();
        //                                    helper.ColoniaPoblacion = valor.Colonia; //**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Colonia"].ToString();
        //                                    helper.CP = valor.CodigoPostal; //**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_CP"].ToString();
        //                                    helper.DelegacionMunicipio = valor.DelegMunicipio; //**CreditDetail.Rows[0]["Dx_Deleg_Municipio"].ToString();
        //                                    helper.Estado = valor.DxCveCc;//**CreditDetail.Rows[0]["Dx_Cve_CC"].ToString();
        //                                    helper.ImporteContrato = Convert.ToSingle(valor.MontoSolicitado);//**Convert.ToSingle(CreditDetail.Rows[0]["Mt_Monto_Solicitado"]);
        //                                    helper.Numero = valor.NumExt; //**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Num"].ToString();
        //                                    helper.RFC = valor.RFC; // **CreditDetail.Rows[0]["DX_RFC"].ToString();
        //                                    //**helper.Sexo = Convert.ToInt32(CreditDetail.Rows[0]["Fg_Edo_Civil_Repre_Legal"]) == 1
        //                                    // **       ? "Masculino"
        //                                    // **       : "Femenino";

        //                                    helper.Nombres = valor.Nombre; // **Dic_Auxiliar[CreditNumber.ToString()][0];
        //                                    helper.ApellidoPaterno = valor.ApPaterno; //**Dic_Auxiliar[CreditNumber.ToString()][1];
        //                                    helper.ApellidoMaterno = valor.ApMaterno; //**Dic_Auxiliar[CreditNumber.ToString()][2];
        //                                    helper.FechaNacimiento = Convert.ToDateTime(valor.FecNacimiento); //** Convert.ToDateTime(Dic_Auxiliar[CreditNumber.ToString()][3]);
        //                                    helper.NumeroInterior = ""; //**Dic_Auxiliar[CreditNumber.ToString()][4];
        //                                    helper.Ciudad = valor.DelegMunicipio;// **Dic_Auxiliar[CreditNumber.ToString()][5];
        //                                    helper.NumeroFirma = CreditNumber;

        //                                }
        //                                mop = helper.ConsultaCirculo();
        //                                folio = helper.folio;
        //                            }
        //                        }
        //                        {
        //                            instance.No_MOP = mop;
        //                            instance.Ft_Folio = folio;
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        mop = hiddenfield.Value;
        //                        instance.No_MOP = mop;
        //                        retry = true;
        //                    }
        //                }

        //                if (retry)
        //                {
        //                    // RSA 20130508 it is not valid, but it ain't invalid either, set appropriate message
        //                    message = GetGlobalResourceObject("DefaultResource", "MOPErrorConnection") as string;
        //                }
        //                else if (IsIntegerNumberValid(mop) || hiddenfield.Value != "mop")
        //                {
        //                   // var dtProgram = CAT_PROGRAMADal.ClassInstance.Get_All_CAT_PROGRAMAByPK(ID_Prog_Proy.ToString());
        //                    var dtProgram = AccesoDatos.SolicitudCredito.CREDITO_DAL.getDatosCat_Programa(IdProgProy);


        //                    if (dtProgram != null)
        //                    {
        //                        if (dtProgram.No_Calif_MOP != null)
        //                            califmop = (int)dtProgram.No_Calif_MOP;// dtProgram.Rows[0]["No_Calif_MOP"] != DBNull.Value ? int.Parse(dtProgram.Rows[0]["No_Calif_MOP"].ToString()) : 4;


        //                        switch (int.Parse(mop))
        //                        {
        //                            case 96:
        //                                message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPError96") as string, califmop, mop);
        //                                break;
        //                            case 99:
        //                                message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPError99") as string, califmop, mop);
        //                                break;
        //                            default:
        //                                if (int.Parse(mop) >= 0 && int.Parse(mop) <= califmop)
        //                                {
        //                                     //RSA 20130508 use message from resource file
        //                                    message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPOK") as string, califmop, mop);
        //                                    isValid = true;
        //                                }
        //                                else if (int.Parse(mop) > califmop)
        //                                {
        //                                     //RSA 20130508 use message from resource file
        //                                    // message = "Invalido: El MOP obtenido es > " + califmop + " pero distinto de 96 y 99";
        //                                    message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string, califmop, mop);
        //                                }
        //                                else
        //                                {
        //                                     //RSA 20130508 use message from resource file
        //                                     //message = "Invalido: Con el MOP Obtenido no se pudo asignar la Tasa automáticamente.";
        //                                    message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string, califmop, mop);
        //                                }
        //                                break;
        //                        }
        //                    }
        //                }
        //                 //invalid
        //                else
        //                {
        //                     //RSA 20130508 use message from resource file
        //                    message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string, califmop, mop);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect("~/SupplierModule/CreditMonitor.aspx");
        //        }
        //    }
        //    // invalid
        //    else
        //    {
        //        // RSA 20130508 use message from resource file
        //        message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string, califmop, mop);
        //    }

        //   // Button startNext = wizardPages.FindControl("StartNavigationTemplateContainerID").FindControl("StartNextButton") as Button;
        //    if (isValid)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Valid", "alert('" + message + "');", true);
        //        result = CAT_AUXILIARDal.ClassInstance.Update_CAT_AUXILIARByCredito(instance);

        //        if (result <= 0) return;

        //        lblImporte.Visible = false;
        //        txtContratoImporte.Visible = false;
        //        revContratoImporte.Visible = false;
        //        btnConsultaCrediticia.Visible = false;
        //        //startNext.Enabled = true;
        //    }
        //    else
        //    {
        //        using (var scope = new TransactionScope())
        //        {
        //            result = CAT_AUXILIARDal.ClassInstance.Update_CAT_AUXILIARByCredito(instance);
        //            if (!retry)
        //                result += K_CREDITODal.ClassInstance.CalificacionMopNoValidCredit(CreditNumber, (int)Helpers.CreditStatus.Calificación_MOP_no_válida, DateTime.Now, Session["UserName"].ToString(), DateTime.Now);
        //            scope.Complete();
        //        }
        //        CreditStatus = (int)Helpers.CreditStatus.Calificación_MOP_no_válida;

        //        if (!string.IsNullOrEmpty(message))
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "InValid", "alert('" + message.Replace('\'', '"') + "');", true);
        //            lblMopInvalido.Visible = true;
        //            lblMopInvalido.Text = message;
        //        }

        //        if (retry)
        //        {
        //            lblImporte.Visible = true;
        //            txtContratoImporte.Visible = true;
        //            revContratoImporte.Visible = true;
        //            btnConsultaCrediticia.Visible = true;
        //         //   startNext.Enabled = false;
        //        }
        //        else
        //        {
        //            lblImporte.Visible = false;
        //            txtContratoImporte.Visible = false;
        //            revContratoImporte.Visible = false;
        //            btnConsultaCrediticia.Visible = false;
        //          //   startNext.Enabled = false;
        //        }
        //    }
        //}

        //private bool IsIntegerNumberValid(string strMatch)
        //{
        //    const string patrn = "^-?\\d+$";
        //    return Regex.IsMatch(strMatch, patrn, RegexOptions.IgnoreCase);
        //}

        //protected void btnDisplayCreditRequest_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnDisplayPaymentSchedule_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnDisplayQuota_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnDisplayRequest_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnSalirValidate_Click(object sender, EventArgs e)
        //{

        //}
    }
}