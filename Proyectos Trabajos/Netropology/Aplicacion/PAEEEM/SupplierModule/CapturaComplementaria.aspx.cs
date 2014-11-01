using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.SolicitudCredito;

namespace PAEEEM.SupplierModule
{
    public partial class CapturaComplementaria : Page
    {
        #region Define Global variable

        public string UserId
        {
            get { return ViewState["UserId"] == null ? "" : ViewState["UserId"].ToString(); }
            set { ViewState["UserId"] = value; }
        }

        public string IdDepartment
        {
            get { return ViewState["IdDepartment"] == null ? "" : ViewState["IdDepartment"].ToString(); }
            set { ViewState["IdDepartment"] = value; }
        }

        public string CreditNo
        {
            get { return ViewState["CreditNo"] == null ? "0" : ViewState["CreditNo"].ToString(); }
            set { ViewState["CreditNo"] = value; }
        }

        public string TipoUser
        {
            get { return ViewState["TipoUser"] == null ? "" : ViewState["TipoUser"].ToString(); }
            set { ViewState["TipoUser"] = value; }
        }

        public string UserName
        {
            get { return ViewState["UserName"] == null ? "" : ViewState["UserName"].ToString(); }
            set { ViewState["UserName"] = value; }
        }

        public string UserEmail
        {
            get { return ViewState["UserEmail"] == null ? "" : ViewState["UserEmail"].ToString(); }
            set { ViewState["UserEmail"] = value; }
        }

        private string CreditNumber
        {
            get { return ViewState["CreditNumber"] != null ? ViewState["CreditNumber"].ToString() : ""; }
            set { ViewState["CreditNumber"] = value; }
        }

        public DataTable ProgramDt
        {
            get { return ViewState["ProgramDt"] == null ? null : ViewState["ProgramDt"] as DataTable; }
            set { ViewState["ProgramDt"] = value; }
        }

        public bool GuardoDatosPyme
        {
            get
            {
                return ViewState["GuardoDatosPyme"] == null
                    ? default(Boolean)
                    : Boolean.Parse(ViewState["GuardoDatosPyme"].ToString());
            }
            set { ViewState["GuardoDatosPyme"] = value; }
        }

        public bool CargaDatosValidHistorialCred
        {
            get
            {
                return ViewState["CargaDatosValidHistorialCred"] == null
                    ? default(Boolean)
                    : Boolean.Parse(ViewState["CargaDatosValidHistorialCred"].ToString());
            }
            set { ViewState["CargaDatosValidHistorialCred"] = value; }
        }

        public bool Pyme
        {
            get { return ViewState["Pyme"] == null ? default(Boolean) : Boolean.Parse(ViewState["Pyme"].ToString()); }
            set { ViewState["Pyme"] = value; }
        }

        public CLI_Cliente DatosCliente
        {
            get { return (CLI_Cliente) ViewState["DatosCliente"]; }
            set { ViewState["DatosCliente"] = value; }
        }

        //public K_DATOS_PYME NewdatosPyMe
        //{
        //    get { return (K_DATOS_PYME)ViewState["NewdatosPyMe"]; }
        //    set { ViewState["NewdatosPyMe"] = value; }
        //}

        public int StatusCredito //reemplace por CreditStatus
        {
            get { return ViewState["StatusCredito"] == null ? 1 : int.Parse(ViewState["StatusCredito"].ToString()); }
            set { ViewState["StatusCredito"] = value; }
        }

        //public DatosCredito CreditDetail
        //{
        //    get
        //    {
        //        return ViewState["CreditDetail"] == null ? null : ViewState["CreditDetail"] as DatosCredito;
        //    }
        //    set
        //    {
        //        ViewState["CreditDetail"] = value;
        //    }
        //}
        public int IdProgProy
        {
            get { return ViewState["ID_Prog_Proy"] == null ? 1 : int.Parse(ViewState["ID_Prog_Proy"].ToString()); }
            set { ViewState["ID_Prog_Proy"] = value; }
        }

        public int TipoSociedad
        {
            get { return ViewState["Tipo_Sociedad"] == null ? 1 : int.Parse(ViewState["Tipo_Sociedad"].ToString()); }
            set { ViewState["Tipo_Sociedad"] = value; }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                //var parseo = (ParseoTrama) Session["PARSEO_TRAMA"];

                ////*** Enable editing, read credit number if any *******
                //// **********Manda llamar CreditMonitor*****************************
                //if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                //{
                //    //***** a token exits, try and retrieve the existing credit information ******
                //    CreditNumber =System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"]));

                //  var dtAllCreditProperties = GetAllCreditProperties(CreditNumber);
                //if (IsEditionAllowed(dtAllCreditProperties))
                // {
                //        ShowCreditNumber(lblCredito1, txtCredito1);
                //        //Muestra No de credito en otras pantallas
                //        //ShowCreditNumber(lblCredito2, txtCredito2);
                //        //ShowCreditNumber(lblCredito3, txtCredito3);
                //        //ShowCreditNumber(lblCredito4, txtCredito4);

                //Session["ValidRPU"] = dtAllCreditProperties.Rows[0]["No_RPU"].ToString();
                Session["ValidRPU"] = "1234566";
                //        imgAlta1.Visible = false;
                //        // imgAlta2.Visible = false;
                //        imgEdit1.Visible = true;
                //        // imgEdit2.Visible = true;
                //}
                //    else
                //    {
                //        Response.Redirect("CreditMonitor.aspx");
                //    }
                //}

                var user = (US_USUARIOModel) Session["UserInfo"];
                UserId = user.Id_Usuario.ToString(CultureInfo.InvariantCulture);
                InitDepartmentProperty();
                TipoUser = user.Tipo_Usuario;
                Session["UserName"] = user.Nombre_Usuario;
                UserEmail = user.CorreoElectronico;

                //lbbNowdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ProgramDt =
                    CAT_PROGRAMABLL.ClassInstance.Get_CAT_PROGRAMAbyPk(
                        Global.PROGRAM.ToString(CultureInfo.InvariantCulture)); //Changed by Jerry 2011/08/08


                // ProgramDt = AccesoDatos.SolicitudCredito.CREDITO_DAL.getDatosCat_Programa(Convert.ToInt16(Global.PROGRAM.ToString(CultureInfo.InvariantCulture)));

                //if(Session["ValidaRPU"] == null)
                //{
                //    Response.Redirect("../Login/Login.aspx");
                //}
            }
        }

        private DataTable GetAllCreditProperties(string creditNumber)
        {
            // validate that the credit exists, its status is Pendiente, and the auxiliar informartion exists            
            // and has no mop yet, only then continue with the edition, otherwise return to the monitor
            var dtCredit = K_CREDITODal.ClassInstance.GetCreditsReview(creditNumber);
            if (dtCredit == null || dtCredit.Rows.Count <= 0) return null;
            var result = dtCredit;
            result.Columns.Add("Dx_Nombres");
            result.Columns.Add("Dx_Apellido_Paterno");
            result.Columns.Add("Dx_Apellido_Materno");
            result.Columns.Add("Dt_Nacimiento_Fecha");
            result.Columns.Add("No_MOP");

            var dtAuxiliar = CAT_AUXILIARDal.ClassInstance.Get_CAT_AUXILIARByCreditNo(creditNumber);
            if (dtAuxiliar == null || dtAuxiliar.Rows.Count <= 0) return result;
            result.Rows[0]["Dx_Nombres"] = dtAuxiliar.Rows[0]["Dx_Nombres"];
            result.Rows[0]["Dx_Apellido_Paterno"] = dtAuxiliar.Rows[0]["Dx_Apellido_Paterno"];
            result.Rows[0]["Dx_Apellido_Materno"] = dtAuxiliar.Rows[0]["Dx_Apellido_Materno"];
            result.Rows[0]["Dt_Nacimiento_Fecha"] = dtAuxiliar.Rows[0]["Dt_Nacimiento_Fecha"];
            result.Rows[0]["No_MOP"] = dtAuxiliar.Rows[0]["No_MOP"];
            return result;
        }

        private bool IsEditionAllowed(DataTable dtAllCreditData)
        {
            var result = dtAllCreditData != null
                         &&
                         CreditStatus.PENDIENTE ==
                         (CreditStatus)
                             Enum.Parse(typeof (CreditStatus), dtAllCreditData.Rows[0]["Cve_Estatus_Credito"].ToString())
                         && string.IsNullOrEmpty(dtAllCreditData.Rows[0]["No_MOP"].ToString());

            return result;
        }

        private void ShowCreditNumber(Label lblCredito, TextBox txtCredito)
        {
            lblCredito.Visible = true;
            txtCredito.Visible = true;
            txtCredito.Text = CreditNumber;
        }

        private void InitDepartmentProperty()
        {
            if (Session["UserInfo"] == null) return;
            var user = (US_USUARIOModel) Session["UserInfo"];

            if (user.Tipo_Usuario == GlobalVar.SUPPLIER_BRANCH)
            {
                var model =
                    CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORByBranchID(
                        user.Id_Departamento.ToString(CultureInfo.InvariantCulture));
                IdDepartment = model.Id_Proveedor.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                IdDepartment = user.Id_Departamento.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void wizardPages_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                //Validar Paso Pyme
                if (e.CurrentStepIndex == 0) //ValidaPyMe
                {
                    wucValidaPyme.Page.Validate();

                    if (wucValidaPyme.Page.IsValid)
                    {
                        if (!GuardoDatosPyme)
                        {
                            var startNext = wizardPages.FindControl("StartNavigationTemplateContainerID").FindControl("btnStartNext") as Button;

                            Session["NewdatosPyMe"] = new K_DATOS_PYME();
                            Session["NewdatosPyMe"] = wucValidaPyme.NewDatosPyme();
                            var newdatosPyMe = (K_DATOS_PYME) Session["NewdatosPyMe"];

                            //Valida Pyme
                            Pyme = CatalogosSolicitud.ValidaClasificacionPyme(newdatosPyMe);
                            newdatosPyMe.Cve_Es_Pyme = Convert.ToInt32(Pyme);
                            newdatosPyMe.Adicionado_Por = UserName;

                            //Guarda Datos Pyme
                            var datosPyme = SolicitudCreditoAcciones.GuarDatosPyme(newdatosPyMe);

                            GuardoDatosPyme = true;

                            if (datosPyme.Cve_Es_Pyme == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError", "alert('Su empresa no cumple con las características de PyME');", true);

                                wucValidaPyme.EnableDatos();

                                if (startNext != null) startNext.Enabled = false;
                                e.Cancel = true;
                            }
                            else
                            {
                                wucValidaPyme.EnableDatos();
                            }
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }
                if (e.CurrentStepIndex == 1) //Presupuesto de Inversion
                {
                    Session["CreditNumber"] = "PAEEMDA01A1111";
                    Session["IdCliente"] = 8;
                    var tipoPersona = (DropDownList)wucValidaPyme.FindControl("DDXTipoPersona");
                    Session["TipoPersona"] = tipoPersona.SelectedValue;

                    wucCapturaBasica.CargaDatosPyme();
                }

                if (e.CurrentStepIndex == 2) //Captura Basica
                {
                    var txtTelCliente = (TextBox)wucCapturaBasica.FindControl("TxttelefonoFiscal");
                    var txtNomCliente = (TextBox)wucCapturaBasica.FindControl("TxtNombrePFisica");
                    var txtApellidoPatCliente = (TextBox)wucCapturaBasica.FindControl("TxtApellidoPaterno");
                    var txtApellidoMatCliente = (TextBox)wucCapturaBasica.FindControl("TxtApellidoMaterno");
                    var txtEmailCliente = (TextBox)wucCapturaBasica.FindControl("TxtEmail");
                    Session["NombreCliente"] = txtNomCliente.Text;
                    Session["ApellidoPatCliente"] = txtApellidoPatCliente.Text;
                    Session["ApellidoMatCliente"] = txtApellidoMatCliente.Text;
                    Session["EmailCliente"] = txtEmailCliente.Text;
                    Session["TelefonoCliente"] = txtTelCliente.Text;

                    wucCapturaBasica.Page.Validate();
                    if (wucCapturaBasica.Page.IsValid)
                    {
                        //*******************************************
                        var infoCliente = wucCapturaBasica.GetObjetoCliente();
                        var newdatosPyMe = (K_DATOS_PYME)Session["NewdatosPyMe"];

                        infoCliente.IdCliente = Convert.ToInt16(Session["IdCliente"]);
                        //infoCliente.DatosCliente.Nombre_Comercial = newdatosPyMe.Dx_Nombre_Comercial;
                        //infoCliente.DatosCliente.RPU = newdatosPyMe.No_RPU;
                        //infoCliente.DatosCliente.Ventas_Mes = newdatosPyMe.Prom_Vtas_Mensuales;
                        //infoCliente.DatosCliente.Gastos_Mes = newdatosPyMe.Tot_Gastos_Mensuales;
                        //infoCliente.DatosCliente.Numero_Empleados = newdatosPyMe.No_Empleados;
                        //infoCliente.DatosCliente.Cve_Sector = newdatosPyMe.Cve_Sector_Economico;
                        //infoCliente.DatosCliente.Tipo_Industria = (int)newdatosPyMe.Cve_Tipo_Industria;

                        //**NOTA** ELiminar condicion de existe idcliente
                        //**En este punto ya debe de existir el cliente
                        var existeIdCliente = SolicitudCreditoAcciones.ObtenCredito(Session["CreditNumber"].ToString());

                        if (existeIdCliente.IdCliente != null)
                        {
                            infoCliente.IdCliente = (int) existeIdCliente.IdCliente;
                            infoCliente.DatosCliente.IdCliente = infoCliente.IdCliente;
                            SolicitudCreditoAcciones.ActualizaDatosInfoGeneral(infoCliente);
                        }
                        else
                        {
                            var insertaDatosCliente = SolicitudCreditoAcciones.InsertaCliente(infoCliente.DatosCliente);

                            if (insertaDatosCliente != null)
                            {
                                var direccionNegocio = infoCliente.DireccionesCliente[0];
                                direccionNegocio.IdCliente = insertaDatosCliente.IdCliente;
                                direccionNegocio.IdTipoDomicilio = 1;
                                Captura.InsertaDirecciones(direccionNegocio);

                                var direccionFiscal = infoCliente.DireccionesCliente[1];
                                direccionFiscal.IdCliente = insertaDatosCliente.IdCliente;
                                direccionFiscal.IdTipoDomicilio = 2;
                                Captura.InsertaDirecciones(direccionFiscal);

                            }
                        }
                        if (!CargaDatosValidHistorialCred)
                        {
                            if (LoadPage())
                                CargaDatosValidHistorialCred = true;
                        }

                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }

                if (e.CurrentStepIndex == 3) //Validacion Crediticia
                {
                    //if (Page.IsValid)
                    //{
                    //    if (!ValidateMop())
                    //    {
                    //        e.Cancel = true;
                    //        //Salir();
                    //        return;
                    //    }
                    //}

                }
                if (e.CurrentStepIndex == 4) //Captura complementaria
                {
                    //wucInformacionComplementaria.Page.Validate();
                    //if (wucInformacionComplementaria.Page.IsValid)
                    //{

                    //    var DatosCompCliente = wucInformacionComplementaria.GuardaInfoCompCliente();
                    //    // ******FALTAAAAAAAAAAA***Guardar Horarios de operacion
                    //}
                    //else
                    //{
                    //    e.Cancel = true;
                    //}

                }

            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                    "alert('" + ex.Message.Replace('\'', '"') + "');", true);
                new LsApplicationException(this, "Credit Request", ex, true);
            }

        }

        protected void wizardPages_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnCancel3_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Login/Login.aspx");
            //Response.Redirect("CreditMonitor.aspx");
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Login/Login.aspx");
            //Response.Redirect("CreditMonitor.aspx");
        }

        protected void BtnCacel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Login/Login.aspx");
            //Response.Redirect("CreditMonitor.aspx");
        }

        protected void IgualaDatos(bool valueCheck)
        {
            //var txtTelCliente = (TextBox)wucCapturaBasica.FindControl("TxttelefonoFiscal");
            //var txtNomCliente = (TextBox)wucCapturaBasica.FindControl("TxtNombrePFisica");
            //var txtApellidoPatCliente = (TextBox)wucCapturaBasica.FindControl("TxtApellidoPaterno");
            //var txtApellidoMatCliente = (TextBox)wucCapturaBasica.FindControl("TxtApellidoMaterno");
            //var txtEmailCliente = (TextBox)wucCapturaBasica.FindControl("TxtEmail");

            //var txtNomRepLegal = (TextBox)wucInformacionComplementaria.FindControl("TxtNombreRepLegal");
            //var txtApellidoPatRepLegal =
            //    (TextBox)wucInformacionComplementaria.FindControl("TxtApPaternoRepLegal");
            //var txtApellidoMatRepLegal =
            //    (TextBox)wucInformacionComplementaria.FindControl("TxtApMaternoRepLegal");
            //var txtEmailRepLegal = (TextBox)wucInformacionComplementaria.FindControl("TxtEmailRepLegal");
            //var txtTelRepLegal = (TextBox)wucInformacionComplementaria.FindControl("TxtTelefonoRepLegal");

            // txtNomRepLegal.Text = txtNomCliente.Text;
        }

        #region Validacion Informacion Crediticia

        public bool LoadPage()
        {
            try
            {
                txtFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                CreditNumber = Session["CreditNumber"].ToString();
                txtCreditoNum.Text = CreditNumber;
                //DataTable dtCredito = K_CREDITODal.ClassInstance.GetCredits(CreditNumber);
                Session["CreditDetail"] = new DatosCredito();
                Session["CreditDetail"] = CREDITO_DAL.ClassInstance.GetDatosCredito(CreditNumber);
                var creditDetail = (DatosCredito) Session["CreditDetail"];


                IdProgProy = (int) creditDetail.IdProgProy;
                StatusCredito = creditDetail.CveEstatusCredito;
                TipoSociedad = (int) creditDetail.CveTipoSociedad;
                txtRazonSocial.Text = creditDetail.RazonSocial ??
                                      creditDetail.Nombre + " " + creditDetail.ApPaterno + " " + creditDetail.ApMaterno;
                txtRequestAmount.Text = string.Format("{0:C2}", creditDetail.MontoSolicitado);
                txtCreditYearsNumber.Text = creditDetail.NoPlazoPago.ToString();
                txtPaymentPeriod.Text = creditDetail.PeriodoPago;


                txtRequestAmount.Enabled = false;
                txtCreditYearsNumber.Enabled = false;
                txtPaymentPeriod.Enabled = false;

                InitMopValidation();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void InitMopValidation()
        {
            var startNext =
                wizardPages.FindControl("StartNavigationTemplateContainerID").FindControl("btnStartNext") as Button;
            var creditDetail = (DatosCredito) Session["CreditDetail"];

            switch (StatusCredito)
            {
                case (int) CreditStatus.PENDIENTE:
                    if (creditDetail != null)
                    {
                        var noMop = creditDetail.NoMop.ToString();

                        if (string.IsNullOrEmpty(noMop.ToString(CultureInfo.InvariantCulture)))
                        {
                            var symbol = new[] {'-'};
                            var creditNum = CreditNumber.Substring(CreditNumber.LastIndexOfAny(symbol) + 1);

                            while (creditNum.Substring(0, 1) == "0")
                            {
                                creditNum = creditNum.Substring(1);
                            }
                            {
                                btnConsultaCrediticia.Attributes.Add("onclick",
                                    "if (confirm('Confirmar realizar Consulta Crediticia\\n\\nNO podrá editar el crédito después de hacer la consulta crediticia')) {this.style.display ='none'; } else { return false; }");
                            }
                            lblImporte.Visible = true;
                            txtContratoImporte.Visible = true;
                            revContratoImporte.Visible = true;
                            btnConsultaCrediticia.Visible = true;
                            if (startNext != null) startNext.Enabled = false;
                        }
                        else
                        {
                            // no row, or row with non empty cell
                            lblImporte.Visible = false;
                            txtContratoImporte.Visible = false;
                            revContratoImporte.Visible = false;
                            btnConsultaCrediticia.Visible = false;

                            var valid = ValidateMop();

                            if (startNext != null) startNext.Enabled = valid;
                            lblMopInvalido.Visible = !valid;
                        }
                    }
                    else
                    {
                        lblImporte.Visible = false;
                        txtContratoImporte.Visible = false;
                        revContratoImporte.Visible = false;
                        btnConsultaCrediticia.Visible = false;
                        if (startNext != null) startNext.Enabled = false;
                    }
                    break;
                case (int) CreditStatus.Calificación_MOP_no_válida:
                case (int) CreditStatus.TARIFA_FUERA_DE_PROGRAMA:
                case (int) CreditStatus.BENEFICIARIO_CON_ADEUDOS:
                case (int) CreditStatus.CANCELADO:
                case (int) CreditStatus.RECHAZADO:
                    lblImporte.Visible = false;
                    txtContratoImporte.Visible = false;
                    revContratoImporte.Visible = false;
                    btnConsultaCrediticia.Visible = false;
                    if (startNext != null) startNext.Enabled = false;
                    lblMopInvalido.Visible = (StatusCredito == (int) CreditStatus.Calificación_MOP_no_válida);
                    break;
                default:
                    lblImporte.Visible = false;
                    txtContratoImporte.Visible = false;
                    revContratoImporte.Visible = false;
                    btnConsultaCrediticia.Visible = false;
                    if (startNext != null) startNext.Enabled = true;
                    break;
            }
        }


        private bool ValidateMop()
        {
            var creditDetail = (DatosCredito) Session["CreditDetail"];
            var valid = false;
            //var dtAuxiliar = CAT_AUXILIARDal.ClassInstance.Get_CAT_AUXILIARByCreditNo(CreditNumber);//Consulta Cat_Auxiliar
            //if (dtAuxiliar == null || dtAuxiliar.Rows.Count == 0)
            //    return false;
            var dtProgram = AccesoDatos.SolicitudCredito.CREDITO_DAL.getDatosCat_Programa(IdProgProy);
            //var dtProgram = CAT_PROGRAMADal.ClassInstance.Get_All_CAT_PROGRAMAByPK(ID_Prog_Proy.ToString());
            var califmop = dtProgram.No_Calif_MOP ?? 4;
            //var califmop = dtProgram != null && dtProgram.Rows.Count > 0 &&
            //               dtProgram.Rows[0]["No_Calif_MOP"] != DBNull.Value
            //    ? int.Parse(dtProgram.Rows[0]["No_Calif_MOP"].ToString())
            //    : 4;
            if (creditDetail != null)
            {
                //if (CreditDetail != null && CreditDetail.Rows.Count > 0
                //    && !string.IsNullOrEmpty(CreditDetail.Rows[0]["Dt_Fecha_Calificación_MOP_no_válida"].ToString()))
                //    return false;
                if (!string.IsNullOrEmpty(creditDetail.FechaCalificacionMopNoValida.ToString()))
                    return false;
                //if (string.IsNullOrEmpty(dtAuxiliar.Rows[0]["No_MOP"].ToString()))
                //    return false;
                if (string.IsNullOrEmpty(creditDetail.NoMop.ToString()))
                    return false;

                if (StatusCredito == (int) CreditStatus.Calificación_MOP_no_válida)
                    return false;

                var mop = creditDetail.NoMop.ToString(); // .Rows[0]["No_MOP"].ToString();
                valid = (int.Parse(mop) >= 0 && int.Parse(mop) <= califmop);

            }
            else
            {
                return false;
            }
            return valid;
        }


        private bool IsIntegerNumberValid(string strMatch)
        {
            const string patrn = "^-?\\d+$";
            return Regex.IsMatch(strMatch, patrn, RegexOptions.IgnoreCase);
        }



        protected void btnDisplayCreditRequest_Click(object sender, EventArgs e)
        {

        }

        protected void btnDisplayPaymentSchedule_Click(object sender, EventArgs e)
        {

        }

        protected void btnDisplayQuota_Click(object sender, EventArgs e)
        {

        }

        protected void btnDisplayRequest_Click(object sender, EventArgs e)
        {

        }

        protected void btnSalirValidate_Click(object sender, EventArgs e)
        {

        }

        protected void btnConsultaCrediticia_Click(object sender, EventArgs e)
        {
            var creditDetail = (DatosCredito) Session["CreditDetail"];
            var mop = string.Empty;
            var folio = string.Empty;
            var message = string.Empty;
            var califmop = 0; // the No_Calif_MOP in CAT_PROGRAMA table
            var isValid = false;
            int result;
            var retry = false;

            var instance = new CAT_AUXILIAREntity();

            var appstate = new LsApplicationState(HttpContext.Current.Application);

            if (!string.IsNullOrEmpty(hiddenfield.Value))
            {
                if (hiddenfield.Value != "Cancel")
                {
                    if (Page.IsValid)
                    {
                        instance.No_Credito = CreditNumber;
                        var symbol = new[] {'-'};
                        var creditNum = CreditNumber.Substring(CreditNumber.LastIndexOfAny(symbol) + 1);

                        while (creditNum.Substring(0, 1) == "0")
                        {
                            creditNum = creditNum.Substring(1);
                        }
                        if (IsIntegerNumberValid(hiddenfield.Value))
                        {
                            mop = hiddenfield.Value;
                            instance.No_MOP = mop;
                        }
                        else
                        {
                            try
                            {
                                //** Temporal for validation testing
                                if (TipoSociedad == (int) CompanyType.MORAL)
                                {
                                    if (!appstate.DebugMode.Equals("false") && !appstate.DebugMode.Equals("pm"))
                                    {
                                        //** use the last two digits of the amount
                                        //** if only one is provided throw an exception to simulate web service not responding
                                        if (txtContratoImporte.Text.Length > 0)
                                            mop = txtContratoImporte.Text.Substring(txtContratoImporte.Text.Length - 2,
                                                2);
                                        else
                                            mop = "04";
                                        folio = "1";
                                    }
                                    else
                                    {
                                        if (creditDetail != null)
                                        {

                                            var pm = new PMHelper
                                            {
                                                Producto = "001",
                                                Rfc = creditDetail.RFC, //** CreditDetail.Rows[0]["DX_RFC"].ToString(),
                                                Nombres = creditDetail.RazonSocial,
                                                //** CreditDetail.Rows[0]["Dx_Razon_Social"].ToString(),
                                                Direccion1 = creditDetail.Calle + " " + creditDetail.NumExt,
                                                //**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Calle"].ToString() +" " + CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Num"].ToString(),
                                                CodigoPostal = creditDetail.CodigoPostal,
                                                //**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_CP"].ToString(),
                                                Colonia = creditDetail.Colonia,
                                                //**CreditDetail.Rows[0]["Dx_Domicilio_Fisc_Colonia"].ToString(),
                                                Ciudad = creditDetail.DelegMunicipio,
                                                //** CreditDetail.Rows[0]["Dx_Deleg_Municipio"].ToString(),
                                                Estado = creditDetail.CvePm,
                                                //** CreditDetail.Rows[0]["Dx_Cve_PM"].ToString(),
                                                Pais = "MX"
                                            };

                                            pm.ConsultarPersonaMoral();

                                            mop = pm.Mop;
                                            folio = pm.Folio;


                                        }
                                    }
                                }
                                else
                                {
                                    if (!appstate.DebugMode.Equals("false"))
                                    {
                                        //**use the last two digits of the amount
                                        //**if only one is provided throw an exception to simulate web service not responding
                                        if (txtContratoImporte.Text.Length > 0)
                                            mop = txtContratoImporte.Text.Substring(txtContratoImporte.Text.Length - 2,
                                                2);
                                        else
                                            mop = "04";
                                        folio = "1";
                                    }
                                    else
                                    {
                                        var helper = new CCHelper
                                        {
                                            Calle = creditDetail.Calle,
                                            ColoniaPoblacion = creditDetail.Colonia,
                                            CP = creditDetail.CodigoPostal,
                                            DelegacionMunicipio = creditDetail.DelegMunicipio,
                                            Estado = creditDetail.CveCc,
                                            ImporteContrato = Convert.ToSingle(creditDetail.MontoSolicitado),
                                            Numero = creditDetail.NumExt,
                                            RFC = creditDetail.RFC,
                                            Nombres = creditDetail.Nombre,
                                            ApellidoPaterno = creditDetail.ApPaterno,
                                            ApellidoMaterno = creditDetail.ApMaterno,
                                            FechaNacimiento = Convert.ToDateTime(creditDetail.FecNacimiento),
                                            NumeroInterior = "",
                                            Ciudad = creditDetail.DelegMunicipio,
                                            NumeroFirma = CreditNumber
                                        };

                                        //**helper.Sexo = Convert.ToInt32(CreditDetail.Rows[0]["Fg_Edo_Civil_Repre_Legal"]) == 1
                                        // **       ? "Masculino"
                                        // **       : "Femenino";

                                        mop = helper.ConsultaCirculo();
                                        folio = helper.folio;
                                    }
                                }
                                {
                                    instance.No_MOP = mop;
                                    instance.Ft_Folio = folio;
                                }
                            }
                            catch (Exception)
                            {
                                mop = hiddenfield.Value;
                                instance.No_MOP = mop;
                                retry = true;
                            }
                        }

                        if (retry)
                        {
                            // RSA 20130508 it is not valid, but it ain't invalid either, set appropriate message
                            message = GetGlobalResourceObject("DefaultResource", "MOPErrorConnection") as string;
                        }
                        else if (IsIntegerNumberValid(mop) || hiddenfield.Value != "mop")
                        {
                            // var dtProgram = CAT_PROGRAMADal.ClassInstance.Get_All_CAT_PROGRAMAByPK(ID_Prog_Proy.ToString());
                            var dtProgram = AccesoDatos.SolicitudCredito.CREDITO_DAL.getDatosCat_Programa(IdProgProy);


                            if (dtProgram != null)
                            {
                                if (dtProgram.No_Calif_MOP != null)
                                    califmop = (int) dtProgram.No_Calif_MOP;
                                        // dtProgram.Rows[0]["No_Calif_MOP"] != DBNull.Value ? int.Parse(dtProgram.Rows[0]["No_Calif_MOP"].ToString()) : 4;


                                switch (int.Parse(mop))
                                {
                                    case 96:
                                        message =
                                            string.Format(
                                                GetGlobalResourceObject("DefaultResource", "MOPError96") as string,
                                                califmop, mop);
                                        break;
                                    case 99:
                                        message =
                                            string.Format(
                                                GetGlobalResourceObject("DefaultResource", "MOPError99") as string,
                                                califmop, mop);
                                        break;
                                    default:
                                        if (int.Parse(mop) >= 0 && int.Parse(mop) <= califmop)
                                        {
                                            //RSA 20130508 use message from resource file
                                            message =
                                                string.Format(
                                                    GetGlobalResourceObject("DefaultResource", "MOPOK") as string,
                                                    califmop, mop);
                                            isValid = true;
                                        }
                                        else if (int.Parse(mop) > califmop)
                                        {
                                            //RSA 20130508 use message from resource file
                                            // message = "Invalido: El MOP obtenido es > " + califmop + " pero distinto de 96 y 99";
                                            message =
                                                string.Format(
                                                    GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as
                                                        string, califmop, mop);
                                        }
                                        else
                                        {
                                            //RSA 20130508 use message from resource file
                                            //message = "Invalido: Con el MOP Obtenido no se pudo asignar la Tasa automáticamente.";
                                            message =
                                                string.Format(
                                                    GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as
                                                        string, califmop, mop);
                                        }
                                        break;
                                }
                            }
                        }
                            //invalid
                        else
                        {
                            //RSA 20130508 use message from resource file
                            message =
                                string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string,
                                    califmop, mop);
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/SupplierModule/CreditMonitor.aspx");
                }
            }
                // invalid
            else
            {
                // RSA 20130508 use message from resource file
                message = string.Format(GetGlobalResourceObject("DefaultResource", "MOPErrorInvalid") as string,
                    califmop, mop);
            }

            var startNext =
                wizardPages.FindControl("StartNavigationTemplateContainerID").FindControl("StartNextButton") as Button;
            if (isValid)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valid", "alert('" + message + "');", true);
                result = CAT_AUXILIARDal.ClassInstance.Update_CAT_AUXILIARByCredito(instance);

                if (result <= 0) return;

                lblImporte.Visible = false;
                txtContratoImporte.Visible = false;
                revContratoImporte.Visible = false;
                btnConsultaCrediticia.Visible = false;
                if (startNext != null) startNext.Enabled = true;
            }
            else
            {
                using (var scope = new TransactionScope())
                {
                    result = CAT_AUXILIARDal.ClassInstance.Update_CAT_AUXILIARByCredito(instance);
                    if (!retry)
                        result += K_CREDITODal.ClassInstance.CalificacionMopNoValidCredit(CreditNumber,
                            (int) CreditStatus.Calificación_MOP_no_válida, DateTime.Now, Session["UserName"].ToString(),
                            DateTime.Now);
                    scope.Complete();
                }
                StatusCredito = (int) CreditStatus.Calificación_MOP_no_válida;

                if (!string.IsNullOrEmpty(message))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "InValid",
                        "alert('" + message.Replace('\'', '"') + "');", true);
                    lblMopInvalido.Visible = true;
                    lblMopInvalido.Text = message;
                }

                if (retry)
                {
                    lblImporte.Visible = true;
                    txtContratoImporte.Visible = true;
                    revContratoImporte.Visible = true;
                    btnConsultaCrediticia.Visible = true;
                    if (startNext != null) startNext.Enabled = false;
                }
                else
                {
                    lblImporte.Visible = false;
                    txtContratoImporte.Visible = false;
                    revContratoImporte.Visible = false;
                    btnConsultaCrediticia.Visible = false;
                    if (startNext != null) startNext.Enabled = false;
                }
            }
        }

        #endregion


        private void Salir()
        {
            var userModel = (US_USUARIOModel) Session["UserInfo"];

            switch (userModel.Id_Rol)
            {
                case 3:
                    Response.Redirect("../SupplierModule/CreditMonitor.aspx");
                    break;
                case 6:
                case 2:
                case 1:
                    Response.Redirect("../RegionalModule/CreditAuthorization.aspx");
                    break;
                default:
                    Response.Redirect("../Default.aspx");
                    break;
            }
        }
    }
}