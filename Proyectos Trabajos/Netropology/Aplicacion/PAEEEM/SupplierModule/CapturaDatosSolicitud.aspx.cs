using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.BussinessLayer;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using PAEEEM.LogicaNegocios.Trama;
using Telerik.Web.UI;
using PAEEEM.LogicaNegocios.ModuloCentral;
using PAEEEM.LogicaNegocios.Validacion_RFC_L;
using PAEEEM.Entidades.Validacion_RFC_E;
using System.IO;


namespace PAEEEM.SupplierModule
{
    public partial class CapturaDatosSolicitud : Page
    {

        #region Define Global variable

        public string UserId
        {
            get { return ViewState["UserId"] == null ? "" : ViewState["UserId"].ToString(); }
            set { ViewState["UserId"] = value; }
        }

        public int IdProveedor
        {
            get { return ViewState["IdProveedor"] == null ? 0 : (int) ViewState["IdProveedor"]; }
            set { ViewState["IdProveedor"] = value; }
        }

        public int IdSucursal
        {
            get { return ViewState["IdSucursal"] == null ? 0 : (int) ViewState["IdSucursal"]; }
            set { ViewState["IdSucursal"] = value; }
        }

        public short IdNegocio
        {
            get { return ViewState["IdNegocio"] == null ? (short) 0 : (short) ViewState["IdNegocio"]; }
            set { ViewState["IdNegocio"] = value; }
        }

        public string NumeroCredito
        {
            get { return ViewState["NumeroCredito"] as string; }
            set { ViewState["NumeroCredito"] = value; }
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

        public bool EditaPyme
        {
            get
            {
                return ViewState["EditaPyme"] == null
                    ? default(Boolean)
                    : Boolean.Parse(ViewState["EditaPyme"].ToString());
            }
            set { ViewState["EditaPyme"] = value; }
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

        public int IdCliente
        {
            get { return (int) ViewState["IdCliente"]; }
            set { ViewState["IdCliente"] = value; }
        }

        public int IdCredSustitucion
        {
            get
            {
                return ViewState["IdCredSustitucion"] == null ? 1 : int.Parse(ViewState["IdCredSustitucion"].ToString());
            }
            set { ViewState["IdCredSustitucion"] = value; }
        }

        public int CveTecnologia
        {
            get { return ViewState["CveTecnologia"] == null ? 1 : int.Parse(ViewState["CveTecnologia"].ToString()); }
            set { ViewState["CveTecnologia"] = value; }
        }

        public bool MopValido
        {
            get
            {
                return ViewState["MopValido"] == null
                    ? default(Boolean)
                    : Boolean.Parse(ViewState["MopValido"].ToString());
            }
            set { ViewState["MopValido"] = value; }
        }

        public CLI_Cliente DatosCliente
        {
            get { return (CLI_Cliente) ViewState["DatosCliente"]; }
            set { ViewState["DatosCliente"] = value; }
        }


        public int StatusCredito //reemplace por CreditStatus
        {
            get { return ViewState["StatusCredito"] == null ? 1 : int.Parse(ViewState["StatusCredito"].ToString()); }
            set { ViewState["StatusCredito"] = value; }
        }

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

        public int IdCredProducto
        {
            get
            {
                return ViewState["IdCredSustitucion"] == null ? 1 : int.Parse(ViewState["IdCredSustitucion"].ToString());
            }
            set { ViewState["IdCredSustitucion"] = value; }
        }

        public int Tipo_Sociedad
        {
            get { return ViewState["Tipo_Sociedad"] == null ? 1 : int.Parse(ViewState["Tipo_Sociedad"].ToString()); }
            set { ViewState["Tipo_Sociedad"] = value; }
        }

        public List<GridEquiposBaja> LstDatosEquiposBajas
        {
            set
            {
                ViewState["LstDatosEquiposBajas"] = value;
            }
            get
            {
                return (List<GridEquiposBaja>)ViewState["LstDatosEquiposBajas"];
            }
        }

        public string Grupo
        {
            get { return ViewState["Grupo"] == null ? "" : ViewState["Grupo"].ToString(); }
            set { ViewState["Grupo"] = value; }
        }


        #endregion

        //**cambio prueba
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }

            /********************************************************
            *  SE REGISTRAN LOS EVENTOS DE PARA EL UPLOAD FILE     *
            ********************************************************/
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!IsPostBack)
            {
                var parseo = (ParseoTrama) Session["PARSEO_TRAMA"];
                LlenarDdxEstado(DDXEstado, DDXMunicipio, DDXColonia);
                LlenarDDxGiroEmpresa();
                LLenaDDxSectorEconomico();
                LlenarDDxTipoPersona();

                if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                {
                    var noCredito =
                        System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["Token"]));
                    var credito = SolicitudCreditoAcciones.ObtenCredito(noCredito);
                    Session["IdCliente"] = credito.IdCliente.ToString();
                    var cliente = SolicitudCreditoAcciones.ObtenCliente(credito.Id_Proveedor ?? 0,
                        credito.Id_Branch ?? 0, credito.IdCliente ?? 0);
                    Session["ValidRPU"] = credito.RPU;

                    NumeroCredito = noCredito;
                    IdProveedor = cliente.Id_Proveedor;
                    IdSucursal = cliente.Id_Branch;
                    IdNegocio = credito.IdNegocio ?? 1;

                    IniciaValidacionMop();
                   CargaDatosPymeCaptura(cliente);
                }
                else if (Request.QueryString["TokenR"] != null && Request.QueryString["TokenR"] != "")
                {
                    var noCreditoAnterior =
                        System.Text.Encoding.Default.GetString(Convert.FromBase64String(Request.QueryString["TokenR"]));

                    var credito = SolicitudCreditoAcciones.ObtenCredito(noCreditoAnterior);
                    Session["IdCliente"] = credito.IdCliente.ToString();
                    var cliente = SolicitudCreditoAcciones.ObtenCliente(credito.Id_Proveedor ?? 0,
                        credito.Id_Branch ?? 0, credito.IdCliente ?? 0);
                    Session["ValidRPU"] = credito.RPU;

                    IdProveedor = cliente.Id_Proveedor;
                    IdSucursal = cliente.Id_Branch;
                    IdNegocio = credito.IdNegocio ?? 1;

                    CargaDatosPymeCaptura(cliente);
                }
                else
                    InitDepartmentProperty();

                /*----------LLENA COMBOS VALIDACION PYME-------------*/

                TxtNoRPU.Text = Session["ValidRPU"].ToString();
                lbbNowdate.Text = DateTime.Now.ToString("yyyy-MM-dd");


                var user = (US_USUARIOModel) Session["UserInfo"];
                UserId = user.Id_Usuario.ToString(CultureInfo.InvariantCulture);
                //InitDepartmentProperty();
                TipoUser = user.Tipo_Usuario;
                Session["UserName"] = user.Nombre_Usuario;
                Session["idUser"] = user.Id_Usuario;
                UserEmail = user.CorreoElectronico;

                ProgramDt =
                    CAT_PROGRAMABLL.ClassInstance.Get_CAT_PROGRAMAbyPk(
                        Global.PROGRAM.ToString(CultureInfo.InvariantCulture));
                IdProgProy = Convert.ToInt32(ProgramDt.Rows[0]["ID_Prog_Proy"]);
                if (Request.QueryString["Token"] != null && Request.QueryString["Token"] != "")
                {
                    divEditar.Visible = true;
                }
                else
                {
                    divEditar.Visible = false;
                }
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
            txtCredito.Text = NumeroCredito;
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
                IdProveedor = model.Id_Proveedor;
                IdSucursal = user.Id_Departamento;
            }
            else
            {
                IdProveedor = user.Id_Departamento;
                IdSucursal = 0;
            }
        }

        protected void wizardPages_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                #region Validar Paso Pyme
                if (e.CurrentStepIndex == 0) //ValidaPyMe
                {
                    var razonSocial = DDXTipoPersona.SelectedValue == "1"
                                          ? TxtNombrePFisicaVPyME.Text + " " + TxtApellidoPaternoVPyME.Text + " " +
                                            TxtApellidoMaternoVPyME.Text
                                          : TxtRazonSocialVPyME.Text;

                    if (NumeroCredito != null)
                    {
                        #region Edicion Datos Pyme
                        if (EditaPyme)
                        {
                            Page.Validate();

                            if (Page.IsValid)
                            {
                                if (DDXTipoPersona.SelectedIndex == 1)
                                {
                                    if (!ValidaEdad())
                                    {
                                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                            "alert('La edad del cliente no cumple con los requisitos para otorgarle el crédito');",
                                            true);
                                        e.Cancel = true;
                                        return;
                                    }
                                    if (
                                        !ValidateRfc("Selector", TxtNombrePFisicaVPyME.Text.ToUpper(),
                                            TxtApellidoPaternoVPyME.Text.ToUpper(),
                                            TxtApellidoMaternoVPyME.Text.ToUpper(), TxtFechaNacimientoVPyME.Text,
                                            TxtRFCFisicaVPyME.Text.ToUpper()))
                                    {

                                        //Valifacion RFC
                                        var strMsg =
                                            HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateRFCPersona")
                                                as
                                                string;
                                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                            "alert('" + strMsg + "');", true);
                                        e.Cancel = true;
                                        return;
                                    }
                                }

                                if (DDXTipoPersona.SelectedIndex == 2)
                                {
                                    if (
                                        !ValidateRfc("Selector", TxtRazonSocialVPyME.Text.ToUpper(), "", "",
                                            TxtFechaConstitucionVPyME.Text,
                                            TxtRFCMoralVPyME.Text.ToUpper()))
                                    {
                                        var strMsg =
                                            HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateRFCPersona")
                                                as
                                                string;
                                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                            "alert('" + strMsg + "');", true);
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                                ActualizaPyme(e);
                            }
                            else
                            {
                                e.Cancel = true;
                                return;
                            }
                        } 

                        #endregion
                        else
                        {
                            EnableDatos();
                            divEditar.Visible = true;

                            TxtRPUPresupuesto.Text = Session["ValidRPU"].ToString();
                            var infoTarifa = wuAltaBajaEquipos1.InicializaUserControl(NumeroCredito, razonSocial);

                            if (infoTarifa != null)
                            {
                                if (!infoTarifa.AnioFactValido || !infoTarifa.PeriodosValidos)
                                {
                                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                                                            "alert('La facturación del usuario no cumple con las condiciones de este programa');",
                                                                            true);
                                }
                            }
                        }
                    }
                    else
                    {
                        Page.Validate();

                        if (Page.IsValid)
                        {
                            if (DDXTipoPersona.SelectedIndex == 1)
                            {
                                if (!ValidaEdad())
                                {
                                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                        "alert('La edad del cliente no cumple con los requisitos para otorgarle el crédito');",
                                        true);
                                    e.Cancel = true;
                                    return;
                                }
                                if (
                                    !ValidateRfc("Selector", TxtNombrePFisicaVPyME.Text.ToUpper(),
                                        TxtApellidoPaternoVPyME.Text.ToUpper(),
                                        TxtApellidoMaternoVPyME.Text.ToUpper(), TxtFechaNacimientoVPyME.Text,
                                        TxtRFCFisicaVPyME.Text.ToUpper()))
                                {
                                    var strMsg =
                                        HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateRFCPersona") as
                                            string;
                                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                        "alert('" + strMsg + "');", true);
                                    e.Cancel = true;
                                    return;
                                }
                            }

                            if (DDXTipoPersona.SelectedIndex == 2)
                            {
                                if (
                                    !ValidateRfc("Selector", TxtRazonSocialVPyME.Text.ToUpper(), "", "",
                                        TxtFechaConstitucionVPyME.Text,
                                        TxtRFCMoralVPyME.Text.ToUpper()))
                                {
                                    var strMsg =
                                        HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateRFCPersona") as
                                            string;
                                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                        "alert('" + strMsg + "');", true);
                                    e.Cancel = true;
                                    return;
                                }
                            }
                            var startNext =
                                wizardPages.FindControl("StartNavigationTemplateContainerID")
                                    .FindControl("btnStartNext") as Button;

                            Session["NewdatosPyMe"] = new K_DATOS_PYME();
                            Session["NewdatosPyMe"] = NewDatosPyme();
                            var newdatosPyMe = (K_DATOS_PYME) Session["NewdatosPyMe"];

                            /*Valida Pyme*/
                            var pyme = CatalogosSolicitud.ValidaClasificacionPyme(newdatosPyMe);
                            newdatosPyMe.Cve_Es_Pyme = Convert.ToInt32(pyme);
                            newdatosPyMe.Adicionado_Por = Session["UserName"].ToString();
                            newdatosPyMe.Fecha_Adicion = DateTime.Now;

                            /* Guarda Datos Pyme*/
                            var datosPyme = SolicitudCreditoAcciones.GuarDatosPyme(newdatosPyMe);

                            /*INSERTAR EVENTO CLASIFICACION PyME EN LOG*/
                             Session["Cve_Pyme"] = datosPyme.Cve_Dato_Pyme;
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                                "CLASIFICACION PyME", Session["ValidRPU"].ToString(),
                                "", "Cve_Dato_Pyme: " + Session["Cve_Pyme"], "", "");

                            GuardoDatosPyme = true;

                            divEditar.Visible = true;

                            if (datosPyme.Cve_Es_Pyme == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                    "alert('Su empresa no cumple con las características de PyME');",
                                    true);

                                EnableDatos();
                                divEditar.Visible = false;

                                if (startNext != null) startNext.Enabled = false;
                                e.Cancel = true;
                            }
                            else
                            {
                                EnableDatos();
                                divEditar.Visible = true;

                                TxtRPUPresupuesto.Text = Session["ValidRPU"].ToString();
                                var infoTarifa = wuAltaBajaEquipos1.InicializaUserControl(NumeroCredito, razonSocial);

                                if (infoTarifa != null)
                                {
                                    if (!infoTarifa.AnioFactValido || !infoTarifa.PeriodosValidos)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                            "alert('La facturación del usuario no cumple con las condiciones de este programa');",
                                            true);
                                        if (startNext != null) startNext.Enabled = false;
                                        e.Cancel = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                        } //aqui
                    }
                }

                #endregion

                #region Presupuesto de Inversion

                if (e.CurrentStepIndex == 1) //Presupuesto de Inversion
                {
                    var startNext =
                            wizardPages.FindControl("StartNavigationTemplateContainerID")
                                .FindControl("btnStartNext") as Button;

                    if (NumeroCredito != null && wuAltaBajaEquipos1.EsEdicion)
                    {
                        #region Edicion Presupuesto Inversion

                        if (wuAltaBajaEquipos1.ValidaEquiposAltayBaja())
                        {

                            var resultado = wuAltaBajaEquipos1.RealizaCalculosSimulador();

                            if (resultado.CapacidadPagoValue && resultado.ConsumoNegativoValue &&
                                resultado.NuevaFacturaNegativaValue && resultado.PsrValue && resultado.ValidacionValue)
                            {
                                if (wuAltaBajaEquipos1.ValidaMontoMaximo())
                                {
                                    if (wuAltaBajaEquipos1.EditaPresupuestoInversion())
                                    {
                                        /*INSERTAR EVENTO PRESUPUESTO DE INVERSION EN LOG*/
                                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                                            "EDICION PRESUPUESTO DE INVERSION", NumeroCredito,
                                            "", "", "", "");

                                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                                                                "alert('Se actualizó la solicitud de crédito: " +
                                                                                NumeroCredito + "');",
                                                                                true);
                                        var validaDirecciones =
                                            SolicitudCreditoAcciones.ValidaDirecciones(IdProveedor, IdSucursal,
                                                                                       Convert.ToInt32(
                                                                                           Session["IdCliente"]),
                                                                                       IdNegocio);

                                        if (validaDirecciones)
                                            CargaDatosCliente();
                                        else
                                            CargaDatosCapturaBasica();
                                    }
                                    else
                                    {
                                        if (startNext != null) startNext.Enabled = false;
                                        e.Cancel = true;

                                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                                                                "alert('Ocurrió un problema al actualizar los datos de la solicitud');",
                                                                                true);
                                    }
                                }
                                else
                                {
                                    if (startNext != null) startNext.Enabled = false;
                                    e.Cancel = true;

                                    var topeMaximoXRFC = Convert.ToDecimal(
                                        new ParametrosGlobales().ObtienePorCondicion(
                                            p => p.IDPARAMETRO == 4 && p.IDSECCION == 16).VALOR);

                                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                                                            "alert('La razón social del cliente excede su financiamiento máximo de " +
                                                                            topeMaximoXRFC.ToString("C2") + "');",
                                                                            true);
                                }
                            }
                            else
                            {
                                if (startNext != null) startNext.Enabled = false;
                                e.Cancel = true;

                                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                                                        "alert('No se puede otorgar el crédito con las condiciones establecidas');",
                                                                        true);
                            }
                        }
                        else
                        {
                            if (startNext != null) startNext.Enabled = false;
                            e.Cancel = true;

                            ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                                                    "alert('No se han asignado todos los equipos de Alta Eficiencia');",
                                                                    true);
                        }

                        #endregion
                    }
                    else if (NumeroCredito != null && wuAltaBajaEquipos1.ExistenEquiposAlta(NumeroCredito))
                    {
                        if (ValidaMontoMaximo())
                        {
                            var validaDirecciones =
                                SolicitudCreditoAcciones.ValidaDirecciones(IdProveedor, IdSucursal,
                                    Convert.ToInt32(Session["IdCliente"]), IdNegocio);

                            if (validaDirecciones)
                                CargaDatosCliente();
                            else
                                CargaDatosCapturaBasica();
                        }
                        else
                        {
                            if (startNext != null) startNext.Enabled = false;
                            e.Cancel = true;

                            var topeMaximoXRFC = Convert.ToDecimal(
                                new ParametrosGlobales().ObtienePorCondicion(
                                    p => p.IDPARAMETRO == 4 && p.IDSECCION == 16).VALOR);

                            ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                                                    "alert('La razón social del cliente excede su financiamiento máximo de " +
                                                                    topeMaximoXRFC.ToString("C2") + "');",
                                                                    true);
                        }
                    }
                    else
                    {
                        #region Presupuesto de Inversion

                        decimal? montodisp = new DatosMontos().montoDisponiblePrograma();
                        decimal montoMinimoTotal = new DatosMontos().montoMinimoPrograma();

                        decimal montoMinimoIncentivo = new DatosMontos().montoMinimoIncentivo();
                        decimal? montoDisponibleIncentivo = new DatosMontos().montoDisponibleIncentivo();

                        if (montodisp > montoMinimoTotal)
                        {
                            if (montoDisponibleIncentivo > montoMinimoIncentivo)
                            {
                                if (wuAltaBajaEquipos1.ValidaEquiposAltayBaja())
                                {

                                    var resultado = wuAltaBajaEquipos1.RealizaCalculosSimulador();

                                    if (resultado.CapacidadPagoValue && resultado.ConsumoNegativoValue &&
                                        resultado.NuevaFacturaNegativaValue && resultado.PsrValue &&
                                        resultado.ValidacionValue)
                                    {
                                        if (wuAltaBajaEquipos1.ValidaMontoMaximo())
                                        {
                                            //TODO INSERTAR CREDITO
                                            var cliente = ObtenClienteBasico();
                                            CLI_Negocio negocio = null;

                                            if (wuAltaBajaEquipos1.CrearClienteONegocio(IdProveedor, IdSucursal,
                                                IdNegocio, cliente))
                                            {
                                                IdNegocio = wuAltaBajaEquipos1.IdNegocio;
                                                if (wuAltaBajaEquipos1.InsertaCredito())
                                                {
                                                    NumeroCredito = wuAltaBajaEquipos1.NumeroCredito;

                                                    /*INSERTAR EVENTO PRESUPUESTO DE INVERSION EN LOG*/
                                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                                                        Convert.ToInt16(Session["IdDepartamento"]),
                                                        "SOLICITUD DE CREDITO",
                                                        "PRESUPUESTO DE INVERSION", NumeroCredito,
                                                        "", "Cve_Dato_Pyme: " + Session["Cve_Pyme"].ToString(), "", "");

                                                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page),
                                                        "NextError",
                                                        "alert('Se generó la solicitud de crédito: " +
                                                        NumeroCredito + "');",
                                                        true);
                                                    var validaDirecciones =
                                                        SolicitudCreditoAcciones.ValidaDirecciones(IdProveedor,
                                                            IdSucursal,
                                                            Convert.ToInt32(
                                                                Session["IdCliente"]),
                                                            IdNegocio);

                                                    if (validaDirecciones)
                                                        CargaDatosCliente();
                                                    else
                                                        CargaDatosCapturaBasica();
                                                }
                                                else
                                                {
                                                    if (startNext != null) startNext.Enabled = false;
                                                    e.Cancel = true;

                                                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page),
                                                        "NextError",
                                                        "alert('Ocurrió un problema al generar la solicitud de crédito');",
                                                        true);
                                                }
                                            }
                                            else
                                            {
                                                if (startNext != null) startNext.Enabled = false;
                                                e.Cancel = true;

                                                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page),
                                                    "NextError",
                                                    "alert('Ocurrió un problema al guardar los datos del cliente');",
                                                    true);
                                            }
                                        }
                                        else
                                        {
                                            if (startNext != null) startNext.Enabled = false;
                                            e.Cancel = true;

                                            var topeMaximoXRFC = Convert.ToDecimal(
                                                new ParametrosGlobales().ObtienePorCondicion(
                                                    p => p.IDPARAMETRO == 4 && p.IDSECCION == 16).VALOR);

                                            ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                                "alert('La razón social del cliente excede su financiamiento máximo de " +
                                                topeMaximoXRFC.ToString("C2") + "');",
                                                true);
                                        }
                                    }
                                    else
                                    {
                                        if (startNext != null) startNext.Enabled = false;
                                        e.Cancel = true;

                                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                            "alert('No se puede otorgar el crédito con las condiciones establecidas');",
                                            true);
                                    }
                                }
                                else
                                {
                                    if (startNext != null) startNext.Enabled = false;
                                    e.Cancel = true;

                                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                        "alert('No se han asignado todos los equipos de Alta Eficiencia');",
                                        true);
                                }
                            }
                            else
                            {
                                if (startNext != null) startNext.Enabled = false;
                                e.Cancel = true;

                                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                                                            "alert('Por el momento solo podrá registrar solicitudes de tecnologías consideradas como adquisición.');",
                                                                            true);
                            }
                        }
                        else
                        {
                            if (startNext != null) startNext.Enabled = false;
                            e.Cancel = true;

                            ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                                                        "alert('Por el momento no se puede registrar esta solicitud debido a que los recursos del programa se encuentran comprometidos');",
                                                                        true);
                        }
                        #endregion
                    }
                }

                #endregion

                #region Captura Basica

                if (e.CurrentStepIndex == 2) //Captura Basica
                {
                     decimal? montodisp = new DatosMontos().montoDisponiblePrograma();
                        decimal montoMinimoTotal = new DatosMontos().montoMinimoPrograma();

                        decimal montoMinimoIncentivo = new DatosMontos().montoMinimoIncentivo();
                        decimal? montoDisponibleIncentivo = new DatosMontos().montoDisponibleIncentivo();

                    if (montodisp > montoMinimoTotal)
                    {
                        if (montoDisponibleIncentivo > montoMinimoIncentivo)
                        {
                            Page.Validate();
                            if (Page.IsValid)
                            {
                                GuardaDatosInfoGeneral();
                                if (!CargaDatosValidHistorialCred)
                                {
                                    if (CargaDatosValidacionCrediticia())
                                        CargaDatosValidHistorialCred = true;
                                }
                                else
                                {
                                    InitMopValidation();
                                }
                            }
                            else
                            {
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                                                            "alert('Por el momento solo podrá registrar solicitudes de tecnologías consideradas como adquisición.');",
                                                                            true);
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                                                        "alert('Por el momento no se puede registrar esta solicitud debido a que los recursos del programa se encuentran comprometidos');",
                                                                        true);
                    }
                }

                #endregion

                #region Validacion Crediticia

                if (e.CurrentStepIndex == 3) //Validacion Crediticia
                {
                    //if (Page.IsValid)
                    //{
                    //if (!ValidateMop())
                    //    {
                    //        e.Cancel = true;
                    //         //Salir();
                    //        return;
                    //    }

                    //}
                    var existenReferencias =
                        SolicitudCreditoAcciones.ValidaReferenciasCliente(IdProveedor, IdSucursal,
                            Convert.ToInt32(Session["IdCliente"]), IdNegocio);

                    if (existenReferencias)
                        CargaDatosComplementariosCliente();
                    else
                        CargaDatosCapturaComp();
                }

                #endregion

                #region Captura complementaria

                if (e.CurrentStepIndex == 4) //Captura complementaria
                {
                    Page.Validate();
                    if (Page.IsValid)
                    {
                      var  mensaje = ValidaDatosHorariosNegocio();
                        if (mensaje.Equals(string.Empty))
                        {
                            if (ValidarHorarioOperacion(hlabor1Negocio, DDXInicioLunes,
                                hlabor2Negocio, DDXInicioMartes,
                                hlabor3Negocio, DDXInicioMiercoles,
                                hlabor4Negocio, DDXInicioJueves,
                                hlabor5Negocio, DDXInicioViernes,
                                hlabor6Negocio, DDXInicioSabado,
                                hlabor7Negocio, DDXInicioDomingo))
                            {

                                if (ValidaObligadoSolidario())
                                {
                                    if (GuardaInfoCompCliente())
                                    {
                                        CargaDatosBajaEquiposCComp();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                            "alert('Error al tratar de guardar los datos');",
                                            true);
                                        e.Cancel = true;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                                        "alert('Seleccione el Tipo de Persona del Obligado Solidario');",
                                        true);
                                    e.Cancel = true;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                                                               "alert('Horario de operación incompleto. Verifique.');",
                                                                               true);
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            if (!mensaje.Equals(string.Empty))
                                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "NextError",
                                    string.Format("alert('{0}');", mensaje), true);
                            e.Cancel = true;
                        }

                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }

                #endregion

                #region Captura complementaria  Baja de Equipos

                if (e.CurrentStepIndex == 5) // BAJA DE EQUIPOS
                {
                    if (HidRequiereCAyD.Value.Equals("Ok"))
                    {
                        Page.Validate("ValidaCAyD");
                        if (Page.IsValid)
                        {
                            ActualizaCAyD();
                        }
                        else e.Cancel = true;
                    }

                    foreach (GridViewRow grdRow in grdEquiposBaja.Rows)
                    {
                        var chkDatosCompletos =
                            ((CheckBox) grdEquiposBaja.Rows[grdRow.RowIndex].FindControl("ckbSelect"));

                        var capturoTodos = chkDatosCompletos.Checked;
                        if (capturoTodos) continue;
                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                            "alert('Debe de capturar los datos para todos los equipos. Verifique');", true);
                        e.Cancel = true;
                    }

                    var credito = SolicitudCreditoAcciones.ObtenCredito(NumeroCredito);

                    if (credito.Fecha_Genera_Documentacion == null)
                    {
                        credito.Fecha_Genera_Documentacion = DateTime.Now;

                        var fechaGeneraDoc = OpEquiposAbEficiencia.ActualizaCredito(credito);

                        if (fechaGeneraDoc)
                             Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                                            Convert.ToInt16(Session["IdRolUserLogueado"]),
                                                            Convert.ToInt16(Session["IdDepartamento"]),
                                                            "SOLICITUD DE CREDITO",
                                                            "GENERACIÓN DE DOCUMENTACIÓN", NumeroCredito,
                                                            "", Convert.ToString(credito.Fecha_Genera_Documentacion), "", "");
                        else
                            ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                           "alert('Se produjo un error al tratar de inserta la Fecha de Generación de Documentación');", true);
                        e.Cancel = true;
                        
                    }
                   
                        // Metodo inicial de AltaBaja de equipos
                        IniciaCapturaCompAltaEquipos();
                    

                }

                #endregion
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                    "alert('" + ex.Message.Replace('\'', '"') + "');", true);
                new LsApplicationException(this, "Credit Request", ex, true);
            }

        }


        public bool ValidaMontoMaximo()
        {
            var montoTotal = Math.Round(new OpEquiposAbEficiencia().ValidaMontoMaximoCredito(DDXTipoPersona.SelectedValue == "1" ? TxtRFCFisicaVPyME.Text : TxtRFCMoralVPyME.Text), 2);
            var topeMaximoXRFC = Convert.ToDecimal(
                  new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 4 && p.IDSECCION == 16).VALOR);

            if (montoTotal <= topeMaximoXRFC)
                return true;

            return false;
        }

        private bool GuardaDatosInfoGeneral()
        {
           /*******************************************/
            var insert = false;
            var infoCliente = GetObjetoCliente();

            //Datos iniciales a insertar para el Cliente
            var newdatosPyMe = NewDatosPyme(); 

            var negocio = infoCliente.DatosCliente.CLI_Negocio.First();
            negocio.Nombre_Comercial = newdatosPyMe.Dx_Nombre_Comercial;
            negocio.Ventas_Mes = newdatosPyMe.Prom_Vtas_Mensuales;
            negocio.Gastos_Mes = newdatosPyMe.Tot_Gastos_Mensuales;
            negocio.Numero_Empleados = newdatosPyMe.No_Empleados;
            negocio.Cve_Sector = (byte?) newdatosPyMe.Cve_Sector_Economico;
            if (newdatosPyMe.Cve_Tipo_Industria != null)
                negocio.Cve_Tipo_Industria = newdatosPyMe.Cve_Tipo_Industria;

            foreach (var direccion in infoCliente.DireccionesCliente)
            {
                direccion.RPU = newdatosPyMe.No_RPU;
            }

            var existeIdCliente = SolicitudCreditoAcciones.ObtenCliente(infoCliente.DatosCliente.Id_Proveedor,
                infoCliente.DatosCliente.Id_Branch, infoCliente.IdCliente);
            if (existeIdCliente != null)
            {
                infoCliente.IdCliente = existeIdCliente.IdCliente;
                infoCliente.DatosCliente.IdCliente = infoCliente.IdCliente;
                SolicitudCreditoAcciones.ActualizaDatosInfoGeneral(infoCliente);

                /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO CAPTURA INFORMACIÓN GENERAL EN SOLICITUD DE CREDITO*/
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                    "SOLICITUD DE CREDITO", "CAPTURA INFORMACIÓN GENERAL", NumeroCredito,
                    "", "", "", "");

                insert = true;
            }
            else
            {
                var insertaDatosCliente =
                    SolicitudCreditoAcciones.InsertaCliente(infoCliente.DatosCliente);

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

                    /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO CAPTURA INFORMACIÓN GENERAL EN SOLICITUD DE CREDITO*/
                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                        Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                        "SOLICITUD DE CREDITO", "CAPTURA INFORMACIÓN GENERAL", NumeroCredito,
                        "", "", "", "");

                    insert =  true;
                }
            }
            return insert;
        }

        protected void wizardPages_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            
        }




        protected void btnValidarIntegrar_Click(object sender, EventArgs e)
        {
           string mensaje = string.Empty;

            if (!ValidaDatosFachada())
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                    "alert('Seleccione la fotografía de la fachada del equipo.');",
                    true);
                return;
            }

            foreach (GridViewRow grdRow in grdEquiposAlta.Rows)
            {
                var chkDatosCompletos =
                    ((CheckBox)grdEquiposAlta.Rows[grdRow.RowIndex].FindControl("ckbSelect"));

                var capturoTodos = chkDatosCompletos.Checked;
                if (capturoTodos) continue;
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                    "alert('Debe de capturar los datos para todos los equipos. Verifique');", true);
                return;
            }


            var credito = SolicitudCreditoAcciones.ObtenCredito(NumeroCredito);
            credito.Fecha_Por_entregar = DateTime.Now;
            credito.Cve_Estatus_Credito = 2; //POR ENTREGAR
            credito.Usr_Ultmod = Session["UserName"].ToString();

            var cambioEstatus = OpEquiposAbEficiencia.ActualizaCredito(credito);

            if (cambioEstatus)
            {
                /*INSERTAR EVENTO CAMBIOS DEL TIPO DE PROCESO VALIDAR E INTEGRAR EN SOLICITUD DE CREDITO*/
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                    Convert.ToInt16(Session["IdDepartamento"]), //idRegionUsuario,idZona
                    "SOLICITUD DE CREDITO", "VALIDAR E INTEGRAR", NumeroCredito,
                    "", "", "", "");

                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                    "('Solicitud Finalizada. Con éxito. " +
                    NumeroCredito + "');",
                    true);
                Salir();
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session["IdCliente"] = null;
            Session["ValidRPU"] = null;
            Salir();
        }

        protected void BtnCacel_Click(object sender, EventArgs e)
        {
            Salir();
        }

     
        #region Validacion Informacion Crediticia

        protected void IniciaValidacionMop()
        {
            var credito = CREDITO_DAL.ClassInstance.GetDatosCredito(NumeroCredito);
            Session["CreditDetail"] = credito;
            MopValido = ValidateMop();
        }

        public bool CargaDatosValidacionCrediticia()
        {
            try
            {
                txtFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtCreditoNumHistorialCred.Text = NumeroCredito;
                Session["CreditDetail"] = new DatosCredito();
                Session["CreditDetail"] = CREDITO_DAL.ClassInstance.GetDatosCredito(NumeroCredito);
                var creditDetail = (DatosCredito)Session["CreditDetail"];


                if (creditDetail.IdProgProy != null) IdProgProy = (int)creditDetail.IdProgProy;
                StatusCredito = creditDetail.CveEstatusCredito;
                TipoSociedad = DDXTipoPersona.SelectedIndex; 
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
            var steptNext =
                wizardPages.FindControl("StepNavigationTemplateContainerID").FindControl("btnStepNext") as Button;
            var creditDetail = (DatosCredito)Session["CreditDetail"];

            switch (StatusCredito)
            {
                case (int)CreditStatus.PENDIENTE:
                    if (creditDetail != null)
                    {
                        var noMop = creditDetail.NoMop.ToString();

                        if (string.IsNullOrEmpty(noMop.ToString(CultureInfo.InvariantCulture)))
                        {
                            var symbol = new[] { '-' };
                            var creditNum = NumeroCredito.Substring(NumeroCredito.LastIndexOfAny(symbol) + 1);

                            while (creditNum.Substring(0, 1) == "0")
                            {
                                creditNum = creditNum.Substring(1);
                            }
                            //{
                            //    btnConsultaCrediticia.Attributes.Add("onclick",
                            //        "if (confirm('Confirma realizar Consulta Crediticia\\n\\n NO podrá editar el crédito después de hacer la consulta crediticia')) {this.style.display ='none'; } else { return false; }");
                            //}
                            lblImporte.Visible = true;
                            txtContratoImporte.Visible = true;
                            revContratoImporte.Visible = true;
                            btnConsultaCrediticia.Visible = true;
                            if (steptNext != null) steptNext.Enabled = false; //Maritza eliminar al validar Consulta Crediticia
                        }
                        else
                        {
                            // no row, or row with non empty cell
                            lblImporte.Visible = false;
                            txtContratoImporte.Visible = false;
                            revContratoImporte.Visible = false;
                            btnConsultaCrediticia.Visible = false;

                            var valid = ValidateMop();
                            MopValido = valid;

                            if (steptNext != null) steptNext.Enabled = valid;
                            lblMopInvalido.Visible = !valid;

                            BloqueaCamposCliente(!valid);
                        }
                    }
                    else
                    {
                        lblImporte.Visible = false;
                        txtContratoImporte.Visible = false;
                        revContratoImporte.Visible = false;
                        btnConsultaCrediticia.Visible = false;
                        if (steptNext != null) steptNext.Enabled = false;
                    }
                    break;
                case (int)CreditStatus.Calificación_MOP_no_válida:
                case (int)CreditStatus.TARIFA_FUERA_DE_PROGRAMA:
                case (int)CreditStatus.BENEFICIARIO_CON_ADEUDOS:
                case (int)CreditStatus.CANCELADO:
                case (int)CreditStatus.RECHAZADO:
                    lblImporte.Visible = false;
                    txtContratoImporte.Visible = false;
                    revContratoImporte.Visible = false;
                    btnConsultaCrediticia.Visible = false;
                    if (steptNext != null) steptNext.Enabled = false;
                    lblMopInvalido.Visible = (StatusCredito == (int)CreditStatus.Calificación_MOP_no_válida);
                    break;
                default:
                    lblImporte.Visible = false;
                    txtContratoImporte.Visible = false;
                    revContratoImporte.Visible = false;
                    btnConsultaCrediticia.Visible = false;
                    if (steptNext != null) steptNext.Enabled = true;
                    break;
            }
        }


        private bool ValidateMop()
        {
            var creditDetail = (DatosCredito)Session["CreditDetail"];
            var valid = false;
            var dtProgram = CREDITO_DAL.getDatosCat_Programa(IdProgProy);
            var califmop = dtProgram.No_Calif_MOP ?? 4;

            if (creditDetail != null)
            {
                if (!string.IsNullOrEmpty(creditDetail.FechaCalificacionMopNoValida.ToString()))
                    return false;

                if (string.IsNullOrEmpty(creditDetail.NoMop.ToString()))
                    return false;

                if (StatusCredito == (int)CreditStatus.Calificación_MOP_no_válida)
                    return false;

                var mop = creditDetail.NoMop.ToString(); 
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
            if (TipoSociedad != (int)CompanyType.PERSONAFISICA
                && TipoSociedad != (int)CompanyType.REPECO)  // RSA persona moral no, persona repeco si se checa
                ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=AutorizacionConsultaBuroPM&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=AutorizacionConsulta Buro&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

                //ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=AutorizacionConsulta Buro&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayPaymentSchedule_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Tabla de Amortizacion&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        protected void btnDisplayQuota_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", "window.open('PrintForm.aspx?ReportName=Presupuesto de Inversion&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }



        protected void btnConsultaCrediticia_Click(object sender, EventArgs e)
        {
            var datosCred = CreCredito.ObtieneCredito(NumeroCredito);
            var noConsultaCrediticia = VariablesGlobales.ObtienePorId(3, 17);
            int consultas = 0;
            var consultasRFCRPU = CREDITO_DAL.ClassInstance.consultasCrediticias(datosCred.No_Credito).ToList();
            for (int i = 0; i < consultasRFCRPU.Count(); i++)
            {
                consultas += consultasRFCRPU[i].no_consultasCrediticias;
            }


            if (consultas == 0)
            {
                HiddenButton2_Click(sender, new EventArgs());
            }
            else if (consultas < Convert.ToInt32(noConsultaCrediticia.VALOR))
            {
                var num=(1+consultas).ToString(CultureInfo.InvariantCulture);
                RadWindowManager1.RadConfirm("“USTED PUEDE REALIZAR HASTA "+noConsultaCrediticia.VALOR+" CONSULTAS Y ESTÁ POR EJECUTAR LA NÚMERO: "+num+".¿Desea Continuar?", "confirmCallBackFn2", 500, 100, null, "Consulta Crediticia");
            }
            else if (consultas == (Convert.ToInt32(noConsultaCrediticia.VALOR) - 1))
            {
                RadWindowManager1.RadConfirm("“ES SU ÚLTIMA CONSULTA”. ¿Desea Continuar?", "confirmCallBackFn2", 500, 100, null, "Consulta Crediticia");
            }
            else if (consultas >= Convert.ToInt32(noConsultaCrediticia.VALOR))
            {
                /*INSERTAR EVENTO CONSULTA CREDITICIA EN LOG*/
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                    "EXCEDE NÚMERO DE CONSULTAS CREDITICIAS", NumeroCredito,
                    noConsultaCrediticia.VALOR, "", "", "");

                RadWindowManager1.RadAlert("YA CUENTA CON EL NÚMERO DE CONSULTAS CREDITICIAS PERMITIDAS", 500, 100, "Consulta Crediticia", null);
            }
        }


        protected void HiddenButton2_Click(object sender, EventArgs e)
        {
            var creditDetail = (DatosCredito)Session["CreditDetail"];
            var mop = string.Empty;
            var folio = string.Empty;
            var message = string.Empty;
            var califmop = 0; // the No_Calif_MOP in CAT_PROGRAMA table
            var isValid = false;
            var result = false;
            var retry = false;

            var instance = new CAT_AUXILIAREntity();

            var appstate = new LsApplicationState(HttpContext.Current.Application);

            if (!string.IsNullOrEmpty(hiddenfield.Value))
            {
                if (hiddenfield.Value != "Cancel")
                {
                    if (Page.IsValid)
                    {
                        instance.No_Credito = NumeroCredito;
                        var symbol = new[] { '-' };
                        var creditNum = NumeroCredito.Substring(NumeroCredito.LastIndexOfAny(symbol) + 1);

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
                                #region Consulta Persona Moral

                                if (TipoSociedad == (int)CompanyType.MORAL)
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

                                            PMHelper pm;
                                            pm = new PMHelper
                                            {
                                                Producto = "001",
                                                Rfc = creditDetail.RFC,
                                                Nombres = creditDetail.RazonSocial,
                                                Direccion1 = creditDetail.Calle + " " + creditDetail.NumExt,
                                                CodigoPostal = creditDetail.CodigoPostal,
                                                Colonia = creditDetail.Colonia,
                                                Ciudad = creditDetail.DelegMunicipio,
                                                Estado = creditDetail.CvePm,
                                                Pais = "MX"
                                            };


                                            #region Consulta Real  COMENTADA
                                            //pm.ConsultarPersonaMoral();
                                            //mop = pm.Mop;
                                            //folio = pm.Folio;
                                            #endregion

                                            #region Consulta Simulada
                                            var consulta = SolicitudCreditoAcciones.ObtenConsultaPruebas();
                                            mop = consulta.Mop;
                                            folio = consulta.Folio;
                                            #endregion

                                            /*INSERTAR EVENTO CONSULTA CREDITICIA EN LOG*/
                                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                                Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                                                "CONSULTA CREDITICIA", NumeroCredito,
                                                "", "", "", "");
                                        }
                                    }
                                }

                                #endregion

                                #region Persona Física

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
                                            NumeroFirma = NumeroCredito
                                        };

                                        #region Consulta Real COMENTADA
                                        //mop = helper.ConsultaCirculo();
                                        //folio = helper.folio;
                                        #endregion

                                        #region Consulta Simulada
                                        var consulta = SolicitudCreditoAcciones.ObtenConsultaPruebas();
                                        mop = consulta.Mop;
                                        folio = consulta.Folio;
                                        #endregion

                                        /*INSERTAR EVENTO CONSULTA CREDITICIA EN LOG*/
                                        Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                            Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                                            "CONSULTA CREDITICIA", NumeroCredito,
                                            "", "", "", "");
                                    }
                                }

                                #endregion
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
                            var dtProgram = CREDITO_DAL.getDatosCat_Programa(IdProgProy);


                            if (dtProgram != null)
                            {
                                if (dtProgram.No_Calif_MOP != null)
                                    califmop = (int)dtProgram.No_Calif_MOP;

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
                wizardPages.FindControl("StepNavigationTemplateContainerID").FindControl("btnStepNext") as Button;

            var stepPre =
                wizardPages.FindControl("StepNavigationTemplateContainerID").FindControl("btnStepPre") as Button;

            if (isValid)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Valid", "alert('" + message + "');", true);
                var datosCred = CreCredito.ObtieneCredito(NumeroCredito);

                    //////
                if (Equals(instance.No_MOP, "00"))
                    instance.No_MOP = "0";

                datosCred.No_MOP = Convert.ToByte(instance.No_MOP.ToString() == "mop" ? null : instance.No_MOP);
                datosCred.Folio_Consulta = (string)instance.Ft_Folio;
                datosCred.Fecha_Consulta = DateTime.Now;
                //add by @l3x//
                datosCred.No_Consultas_Crediticias =datosCred.No_Consultas_Crediticias == null ? (byte)1 : Convert.ToByte(datosCred.No_Consultas_Crediticias+1);


                result = OpEquiposAbEficiencia.ActualizaCredito(datosCred);

                if (!result) return;
                lblImporte.Visible = false;
                txtContratoImporte.Visible = false;
                revContratoImporte.Visible = false;
                btnConsultaCrediticia.Visible = false;
                BloqueaCamposCliente(false);
                if (ddxMismoDom_Fiscal_Negocio.SelectedValue == "NO")
                {
                    ddxMismoDom_Fiscal_Negocio.Enabled = true;
                    panelMismoDom_Fiscal_Negocio.Enabled = true;
                }
                if (startNext != null) startNext.Enabled = true;
              
            }
            else
            {
                if (!retry)
                {
                    StatusCredito = (int)CreditStatus.Calificación_MOP_no_válida;

                    using (var scope = new TransactionScope())
                    {
                        // result = CAT_AUXILIARDal.ClassInstance.Update_CAT_AUXILIARByCredito(instance);
                        var datosCred = CreCredito.ObtieneCredito(NumeroCredito);
                        if (Equals(instance.No_MOP, "00"))
                            instance.No_MOP = "0";

                        datosCred.No_MOP = Convert.ToByte(instance.No_MOP.ToString() == "mop" ? null : instance.No_MOP);
                        datosCred.Folio_Consulta = (string)instance.Ft_Folio;
                        datosCred.Cve_Estatus_Credito = Convert.ToByte(StatusCredito);
                        datosCred.Fecha_Consulta = DateTime.Now;
                        //add by @l3x//
                        datosCred.No_Consultas_Crediticias =datosCred.No_Consultas_Crediticias == null ? (byte)1 : Convert.ToByte(datosCred.No_Consultas_Crediticias+1);
                        result = OpEquiposAbEficiencia.ActualizaCredito(datosCred);

                        if (!retry)
                            K_CREDITODal.ClassInstance.CalificacionMopNoValidCredit(NumeroCredito,
                                                                                    (int)
                                                                                    CreditStatus
                                                                                        .Calificación_MOP_no_válida,
                                                                                    DateTime.Now,
                                                                                    Session["UserName"].ToString(),
                                                                                    DateTime.Now);
                        scope.Complete();
                    }
                }


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
                    if (stepPre != null) stepPre.Enabled = false;

                    btnDisplayCreditRequest.Enabled = false;
                    btnDisplayPaymentSchedule.Enabled = false;
                    btnDisplayQuota.Enabled = false;
                }
                else
                {
                    lblImporte.Visible = false;
                    txtContratoImporte.Visible = false;
                    revContratoImporte.Visible = false;
                    btnConsultaCrediticia.Visible = false;
                    if (startNext != null) startNext.Enabled = false;
                    if (stepPre != null) stepPre.Enabled = false;

                    btnDisplayCreditRequest.Enabled = false;
                    btnDisplayPaymentSchedule.Enabled = false;
                    btnDisplayQuota.Enabled = false;
                }
            }
        }

        #endregion

        #region ValidaPyME

        #region llenar Catalogos

        protected void LlenarDdxEstado(DropDownList estado,DropDownList municipio, DropDownList colonia)
        {
            var lstEstado = CatalogosSolicitud.ObtenCatEstadosRepublica();
            if (lstEstado == null) return;
            estado.DataSource = lstEstado;
            estado.DataValueField = "Cve_Estado";
            estado.DataTextField = "Dx_Nombre_Estado";
            estado.DataBind();

            estado.Items.Insert(0, "Seleccione");
            estado.SelectedIndex = 0;

            municipio.Items.Insert(0, "Seleccione");
            municipio.SelectedIndex = 0;

            colonia.Items.Insert(0, "Seleccione");
            colonia.SelectedIndex = 0;
        }

        protected void LlenarDDxMunicipo(int idEstado,DropDownList cmbMunicipio,DropDownList cmbColonia)
        {
            try
            {
                cmbMunicipio.Items.Clear();
                cmbColonia.Items.Clear();

                var lstMunicipio = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

                if (lstMunicipio == null) return;
                cmbMunicipio.DataSource = lstMunicipio;
                cmbMunicipio.DataValueField = "Cve_Deleg_Municipio";
                cmbMunicipio.DataTextField = "Dx_Deleg_Municipio";
                cmbMunicipio.DataBind();

                cmbMunicipio.Items.Insert(0, "Seleccione");
                cmbMunicipio.SelectedIndex = 0;
                cmbColonia.Items.Insert(0, "Seleccione");
                cmbColonia.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                
            }

        }

        protected void LlenarDDxColonias(int idEstado, int idMunicipio, DropDownList cmbColonia, DropDownList cmbColoniaHidden)
        {
            try
            {
                cmbColonia.Items.Clear();
                cmbColoniaHidden.Items.Clear();

                var lstColonia = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

                if (lstColonia == null) return;
                cmbColonia.DataSource = lstColonia;
                cmbColonia.DataValueField = "Cve_CP";
                cmbColonia.DataTextField = "Dx_Colonia";
                cmbColonia.DataBind();

                cmbColonia.Items.Insert(0, "Seleccione");
                cmbColonia.SelectedIndex = 0;

                cmbColoniaHidden.DataSource = lstColonia;
                cmbColoniaHidden.DataValueField = "Cve_CP";
                cmbColoniaHidden.DataTextField = "Codigo_Postal";
                cmbColoniaHidden.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void LLenaDDxSectorEconomico()
        {
            DDXSector.Items.Clear();

            var lstSector = CatalogosSolicitud.ObtenCatSectorEconomico();
            if (lstSector == null) return;
            DDXSector.DataSource = lstSector;
            DDXSector.DataValueField = "Cve_Sector";
            DDXSector.DataTextField = "Dx_Sector";
            DDXSector.DataBind();

            DDXSector.Items.Insert(0, "Seleccione");
            DDXSector.SelectedIndex = 0;
        }

        protected void LlenarDDxGiroEmpresa()
        {
            var lstGiroEmpresa = CatalogosSolicitud.ObtenCatTipoIndustrias();

            if (lstGiroEmpresa == null) return;
            DDXGiroEmpresa.DataSource = lstGiroEmpresa;
            DDXGiroEmpresa.DataValueField = "Cve_Tipo_Industria";
            DDXGiroEmpresa.DataTextField = "DESCRIPCION_SCIAN";
            DDXGiroEmpresa.DataBind();

            DDXGiroEmpresa.Items.Insert(0, "Seleccione");
            DDXGiroEmpresa.SelectedIndex = 0;
        }
        #endregion

        #region Eventos Controles

        protected void TxtCP_TextChanged(object sender, EventArgs e)
        {
            string sScript;
            if (TxtCP.Text.Length == 5)
            {
                var lstColonia = CatalogosSolicitud.ObtenColoniasXCp(TxtCP.Text);

                if (lstColonia.Count > 0)
                {
                    var colonia = lstColonia.FirstOrDefault(me => me.CodigoPostal == TxtCP.Text);

                    if (colonia != null)
                        DDXEstado.SelectedValue = colonia.CveEstado.ToString(CultureInfo.InvariantCulture);
                    if (colonia != null)
                    {
                        LlenarDDxMunicipo(colonia.CveEstado,DDXMunicipio,DDXColonia);
                        DDXMunicipio.SelectedValue = colonia.CveDelegMunicipio.ToString();
                    }
                    DDXColonia.DataSource = lstColonia;
                    DDXColonia.DataValueField = "CveCP";
                    DDXColonia.DataTextField = "DxColonia";
                    DDXColonia.DataBind();

                    DDXColonia.Items.Insert(0, "Seleccione");
                    DDXColonia.SelectedIndex = 0;

                    DDXColoniaHidden.DataSource = lstColonia;
                    DDXColoniaHidden.DataValueField = "CveCP";
                    DDXColoniaHidden.DataTextField = "CodigoPostal";
                    DDXColoniaHidden.DataBind();
                }
                else
                {
                    DDXEstado.SelectedIndex = 0;
                    DDXMunicipio.Items.Clear();
                    DDXMunicipio.Items.Insert(0, "Seleccione");
                    DDXMunicipio.SelectedIndex = 0;
                    DDXColonia.Items.Clear();
                    DDXColonia.Items.Insert(0, "Seleccione");
                    DDXColonia.SelectedIndex = 0;

                    sScript =
                        "<script language='JavaScript'>alert('No se encuentra el Código Postal, contactar al Agente FIDE.');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);

                    TxtCP.Focus();
                }
            }
            else
            {
                sScript =
                    "<script language='JavaScript'>alert('El Código Postal debe ser de 5 dígitos. Verifique');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);

                TxtCP.Focus();
            }

        }

        protected void DDXTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDXTipoPersona.SelectedValue == "1")
            {
                divRFCFisica.Visible = true;
                divRFCMoral.Visible = false;
            }

            if (DDXTipoPersona.SelectedValue == "2")
            {
                divRFCFisica.Visible = false;
                divRFCMoral.Visible = true;
            }
        }
        protected void DDXEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipo(int.Parse(DDXEstado.SelectedValue),DDXMunicipio,DDXColonia);
        }

        protected void LlenarDDxTipoPersona()
        {
            var lstTipoPersona = CatalogosSolicitud.ObtenCatTipoSociedad();

            if (lstTipoPersona == null) return;
            DDXTipoPersona.DataSource = lstTipoPersona;
            DDXTipoPersona.DataValueField = "Cve_Tipo_Sociedad";
            DDXTipoPersona.DataTextField = "Dx_Tipo_Sociedad";
            DDXTipoPersona.DataBind();

            DDXTipoPersona.Items.Insert(0, "Seleccione");
            DDXTipoPersona.SelectedIndex = 0;
        }
        
        protected void DDXMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxColonias(int.Parse(DDXEstado.SelectedValue),
                int.Parse(DDXMunicipio.SelectedValue), DDXColonia, DDXColoniaHidden);
        }

        protected void DDXColonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXColoniaHidden.SelectedValue = DDXColonia.SelectedValue;
            TxtCP.Text = DDXColoniaHidden.SelectedItem.Text;
        }
        
        #endregion
        
        #region Metodos Publicos

        protected bool ValidaEdad()
        {
            var fechaHoy = DateTime.Today;
            var fechaNacimiento = Convert.ToDateTime(TxtFechaNacimientoVPyME.Text);
            var edad = Edad(fechaNacimiento);

            return (edad >= 18) && (edad < 66);
        }

        protected int Edad(DateTime fechaNacimiento)
        {
            var fechaHoy = DateTime.Today;
            var edad = (fechaHoy.Year - fechaNacimiento.Year) - 1;

            if (fechaHoy.Month + 1 - fechaNacimiento.Month < 0) //+ 1 porque los meses empiezan en 0 
                return edad;

            if (fechaHoy.Month + 1 - fechaNacimiento.Month > 0)
                return (edad + 1);

            //entonces es que eran iguales. miro los dias 
            //si resto los dias y me da menor que 0 entonces no ha cumplido años. Si da mayor o igual si ha cumplido 
            if (fechaHoy.Day - fechaNacimiento.Day >= 0)
                return (edad + 1);

            return edad;
        }
        
        private bool ValidateRfc(string type, string name, string last, string mother, string birth, string rfc)
        {
            var flag = false;
         
            if (type.ToUpper().Equals("SELECTOR"))
                {
                    type = (CompanyType)Enum.Parse(typeof(CompanyType), DDXTipoPersona.SelectedValue) == CompanyType.MORAL
                        ? "Moral"
                        : "Fisica";
                }

            switch (type.ToUpper())
            {
                case "MORAL":
                    var str1 = K_CREDITODal.ClassInstance.GenerateRFCMoral(name, birth, rfc);
                    flag = rfc.Equals(str1);
                    break;
                case "FISICA":
                    var str2 = K_CREDITODal.ClassInstance.GenerateRFCInmoral(name, last, mother, birth);
                    flag = rfc.Equals(str2);
                    break;
                case "AVAL":
                    var str3 = K_CREDITODal.ClassInstance.GenerateRFCInmoral(name, last, mother, birth);
                    flag = rfc.StartsWith(str3.Substring(0, 10));
                    break;
            }
            if (!flag && (name.Contains("Ñ") || last.Contains("Ñ") || mother.Contains("Ñ")))
                flag = ValidateRfc(type, name.Replace("Ñ", "&"), last.Replace("Ñ", "&"), mother.Replace("Ñ", "&"), birth, rfc);
            return flag;
        }

        public K_DATOS_PYME NewDatosPyme()
        {
            var datosPyme = new K_DATOS_PYME
            {
                No_RPU = Session["ValidRPU"].ToString(),
                Cve_Tipo_Industria = (DDXGiroEmpresa.SelectedIndex == 0 ||
                                      DDXGiroEmpresa.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXGiroEmpresa.SelectedValue),
                Cve_Sector_Economico = (DDXSector.SelectedIndex == 0 ||
                                        DDXSector.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXSector.SelectedValue),
                Dx_Nombre_Comercial = TxtNombreComercial.Text.ToUpper(),
                No_Empleados = (TxtNoEmpleados.Text == "") ? 0 : int.Parse(TxtNoEmpleados.Text),
                Tot_Gastos_Mensuales = (TxtGastosMensuales.Text == "")
                    ? 0
                    : decimal.Parse(TxtGastosMensuales.Text),
                Prom_Vtas_Mensuales = (TxtVentasAnuales.Text == "") ? 0 : decimal.Parse(TxtVentasAnuales.Text),
                Cve_Es_Pyme = 0,
                Cve_CP = (DDXColonia.SelectedIndex == 0 || DDXColonia.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXColonia.SelectedValue),
                Codigo_Postal = TxtCP.Text,
                Cve_Deleg_Municipio = (DDXMunicipio.SelectedIndex == 0 || DDXMunicipio.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXMunicipio.SelectedValue),
                Cve_Estado = (DDXEstado.SelectedIndex == 0 || DDXEstado.SelectedIndex == -1)
                    ? 0
                    : int.Parse(DDXEstado.SelectedValue),
                Cve_Tipo_Sociedad = (byte?)DDXTipoPersona.SelectedIndex,
                RFC = DDXTipoPersona.SelectedIndex == 1 ? TxtRFCFisicaVPyME.Text.ToUpper() : TxtRFCMoralVPyME.Text.ToUpper()
            };

            return datosPyme;
        }

        protected void CargaDatosPymeCaptura(CLI_Cliente cliCliente)
        {
            //var datosPyme = SolicitudCreditoAcciones.BuscaDatosPyme(Session["ValidRPU"].ToString());
            //if (datosPyme != null)
            //{
            var negocio = Captura.ObtenNegocioCliente(IdProveedor, IdSucursal, cliCliente.IdCliente, IdNegocio);
            
                DDXGiroEmpresa.SelectedValue = negocio.Cve_Tipo_Industria.ToString();
                DDXSector.SelectedValue = negocio.Cve_Sector.ToString();
                TxtNombreComercial.Text = negocio.Nombre_Comercial;
                TxtNoEmpleados.Text = negocio.Numero_Empleados.ToString();
                TxtGastosMensuales.Text = Convert.ToDecimal(negocio.Gastos_Mes).ToString("#.##");
            TxtVentasAnuales.Text = Convert.ToDecimal(negocio.Ventas_Mes).ToString("#.##");

            var direccionNegocio = Captura.ObtenDireccionCliente(IdProveedor, IdSucursal, cliCliente.IdCliente,
                IdNegocio,1);
            TxtCP.Text = direccionNegocio.CP;

            DDXEstado.SelectedValue = direccionNegocio.Cve_Estado.ToString();
            LlenarDDxMunicipo((int)direccionNegocio.Cve_Estado, DDXMunicipio, DDXColonia);
            DDXMunicipio.SelectedValue = direccionNegocio.Cve_Deleg_Municipio.ToString();
            LlenarDDxColonias((int)direccionNegocio.Cve_Estado, (int)direccionNegocio.Cve_Deleg_Municipio, DDXColonia, DDXColoniaHidden);
            DDXColonia.SelectedValue = direccionNegocio.CVE_CP.ToString();

                DDXTipoPersona.SelectedValue = cliCliente.Cve_Tipo_Sociedad.ToString();

                if (cliCliente.Cve_Tipo_Sociedad == 1)
                {
                    var fechaNac = cliCliente.Fec_Nacimiento.Value.Date;
                    TxtNombrePFisicaVPyME.Text = cliCliente.Nombre;
                    TxtApellidoPaternoVPyME.Text = cliCliente.Ap_Paterno;
                    TxtApellidoMaternoVPyME.Text = cliCliente.Ap_Materno;
                    TxtFechaNacimientoVPyME.Text = fechaNac.Year.ToString(CultureInfo.InvariantCulture) + @"-" +
                                                   fechaNac.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') +
                                                   "-" + fechaNac.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

                    TxtRFCFisicaVPyME.Text = cliCliente.RFC;
                    divRFCFisica.Visible = true;
                    divRFCMoral.Visible = false;
                }
                else
                {
                    var fechaCons = cliCliente.Fec_Nacimiento.Value.Date;
                    TxtRazonSocialVPyME.Text = cliCliente.Razon_Social;
                    TxtRFCMoralVPyME.Text = cliCliente.RFC;
                    TxtFechaConstitucionVPyME.Text = fechaCons.Year.ToString(CultureInfo.InvariantCulture) + @"-" +
                                                   fechaCons.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') +
                                                   @"-" + fechaCons.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                    divRFCMoral.Visible = true;
                    divRFCFisica.Visible = false;
                }
            EnableDatos();
            if (NumeroCredito != null)
            {
                btnEditarValidacionPyme.Visible = !new OpEquiposAbEficiencia().EsReasignacion(NumeroCredito);
            }
        }

        public void EnableDatos()
        {
            DDXGiroEmpresa.Enabled = false;
            DDXSector.Enabled = false;
            TxtNombreComercial.Enabled = false;
            TxtNoEmpleados.Enabled = false;
            TxtGastosMensuales.Enabled = false;
            TxtVentasAnuales.Enabled = false;
            DDXColonia.Enabled = false;
            TxtCP.Enabled = false;
            DDXMunicipio.Enabled = false;
            DDXMunicipio.Enabled = false;
            DDXEstado.Enabled = false;
            DDXTipoPersona.Enabled = false;
            TxtRFCFisicaVPyME.Enabled = false;
            TxtRFCMoralVPyME.Enabled = false;
            TxtNombrePFisicaVPyME.Enabled = false;
            TxtApellidoPaternoVPyME.Enabled = false;
            TxtApellidoMaternoVPyME.Enabled = false;
            TxtRazonSocialVPyME.Enabled = false;
            TxtFechaNacimientoVPyME.Enabled = false;
            TxtFechaConstitucionVPyME.Enabled = false;
        }

        public K_DATOS_PYME ObtenNuevosDatosPyme()
        {
            var datosPymeActual = SolicitudCreditoAcciones.BuscaDatosPyme(Session["ValidRPU"].ToString());
            Session["datosPymeActual"] = datosPymeActual;

            var newDatosPyme = NewDatosPyme();
            newDatosPyme.Cve_Dato_Pyme = datosPymeActual.Cve_Dato_Pyme;
            newDatosPyme.Adicionado_Por = datosPymeActual.Adicionado_Por;
            newDatosPyme.Fecha_Adicion = datosPymeActual.Fecha_Adicion;

            Session["newDatosPyme"] = newDatosPyme;
            return newDatosPyme;
        }
        
        public void ActualizaPyme(WizardNavigationEventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                var razonSocial = DDXTipoPersona.SelectedValue == "1"
                                          ? TxtNombrePFisicaVPyME.Text + " " + TxtApellidoPaternoVPyME.Text + " " +
                                            TxtApellidoMaternoVPyME.Text
                                          : TxtRazonSocialVPyME.Text;

                if (DDXTipoPersona.SelectedIndex == 1)
                {
                    if (!ValidaEdad())
                    {
                        ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                            "alert('La edad del cliente no cumple con los requisitos para otorgarle el crédito');",
                            true);
                        e.Cancel = true;
                        return;
                    }
                    if (
                        !ValidateRfc("Selector", TxtNombrePFisicaVPyME.Text.ToUpper(),
                            TxtApellidoPaternoVPyME.Text.ToUpper(),
                            TxtApellidoMaternoVPyME.Text.ToUpper(), TxtFechaNacimientoVPyME.Text,
                            TxtRFCFisicaVPyME.Text.ToUpper()))
                    {
                        //Validacion RFC
                        var strMsg =
                            HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateRFCPersona") as
                                string;
                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                            "alert('" + strMsg + "');", true);
                        e.Cancel = true;
                        return;
                    }
                }
                if (DDXTipoPersona.SelectedIndex == 2)
                {
                    if (
                        !ValidateRfc("Selector", TxtRazonSocialVPyME.Text.ToUpper(), "", "",
                            TxtFechaConstitucionVPyME.Text,
                            TxtRFCMoralVPyME.Text.ToUpper()))
                    {
                        var strMsg =
                            HttpContext.GetGlobalResourceObject("DefaultResource", "ValidateRFCPersona") as
                                string;
                        ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                            "alert('" + strMsg + "');", true);
                        e.Cancel = true;
                        return;

                    }
                }
                var startNext =
                    wizardPages.FindControl("StartNavigationTemplateContainerID")
                        .FindControl("btnStartNext") as Button;

                var newDatosPyme = ObtenNuevosDatosPyme();

                /*Valida Pyme*/
                var esPyme = CatalogosSolicitud.ValidaClasificacionPyme(newDatosPyme);
                newDatosPyme.Cve_Es_Pyme = Convert.ToInt32(esPyme);
               // newDatosPyme.Adicionado_Por = Session["UserName"].ToString();
                newDatosPyme.Modificado_Por = Session["UserName"].ToString();
                newDatosPyme.Fecha_Modificacion = DateTime.Now;

                var actualizaPyme = SolicitudCreditoAcciones.ActualizaDatosPyme(newDatosPyme);

                /*INSERTAR EVENTOEDITA DATOS CLASIFICACION PYME EN LOG*/
                var cambios = Insertlog.GetCambiosDatos(Session["datosPymeActual"], Session["newDatosPyme"]);
                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                    Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                    "EDITA DATOS CLASIFICACION PYME",NumeroCredito,
                    "", "Datos PyME", cambios[0], cambios[1]);

                if (esPyme)
                {
                    EnableDatos();
                    divEditar.Visible = true;

                    var datosClientesAnterior = Captura.ObtenCliente(IdProveedor, IdSucursal, Convert.ToInt32(Session["IdCliente"].ToString()));

                    var clienteActualizado = Captura.ObtenCliente(IdProveedor,IdSucursal,Convert.ToInt32(Session["IdCliente"].ToString()));

                    clienteActualizado.Cve_Tipo_Sociedad = Byte.Parse(DDXTipoPersona.SelectedValue);
                    if (clienteActualizado.Cve_Tipo_Sociedad == 1)
                    {
                        clienteActualizado.Nombre = TxtNombrePFisicaVPyME.Text.ToUpper();
                        clienteActualizado.Ap_Paterno = TxtApellidoPaternoVPyME.Text.ToUpper();
                        clienteActualizado.Ap_Materno = TxtApellidoMaternoVPyME.Text.ToUpper();
                        clienteActualizado.RFC = TxtRFCFisicaVPyME.Text.ToUpper();
                        clienteActualizado.Fec_Nacimiento = Convert.ToDateTime(TxtFechaNacimientoVPyME.Text);
                    }
                    else if (clienteActualizado.Cve_Tipo_Sociedad == 2)
                    {
                        clienteActualizado.Razon_Social = TxtRazonSocialVPyME.Text;
                        clienteActualizado.Fec_Nacimiento = Convert.ToDateTime(TxtFechaConstitucionVPyME.Text);
                        clienteActualizado.RFC = TxtRFCMoralVPyME.Text.ToUpper();
                    }

                    Captura.ActualizaCliente(clienteActualizado);

                    /*INSERTAR EVENTO EDITA DATOS CLASIFICACION PYME EN LOG*/
                    var cambiosCliente = Insertlog.GetCambiosDatos(datosClientesAnterior, clienteActualizado);
                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                        Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                        "EDITA DATOS CLASIFICACION PYME", NumeroCredito,
                        "", "Datos Cliente", cambiosCliente[0], cambiosCliente[1]);


                var validaDirecciones =
                            SolicitudCreditoAcciones.ValidaDirecciones(IdProveedor, IdSucursal,
                                Convert.ToInt32(Session["IdCliente"]), IdNegocio);

                    if (validaDirecciones)
                    {
                        var cliente = SolicitudCreditoAcciones.ObtenClienteComplejo(IdProveedor, IdSucursal,
                                                                                    Convert.ToInt32(Session["IdCliente"]),
                                                                                    IdNegocio);

                        var domicilioNegocioAnterior = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 1);
                        var domicilioFiscalAnterior = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 2);

                        var domicilioNegocio = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 1);
                        var domicilioFiscal = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 2);

                       if (newDatosPyme.Cve_CP != domicilioNegocio.CVE_CP)
                        {
                            if (newDatosPyme.Cve_Deleg_Municipio != domicilioNegocio.Cve_Deleg_Municipio)
                            {
                                new OpEquiposAbEficiencia().EliminaPresupuestoInversion(NumeroCredito);
                            }

                            if (domicilioFiscal != null && (domicilioNegocio.CVE_CP == domicilioFiscal.CVE_CP))
                            {
                                domicilioFiscal.Cve_Estado = newDatosPyme.Cve_Estado;
                                domicilioFiscal.Cve_Deleg_Municipio = newDatosPyme.Cve_Deleg_Municipio;
                                domicilioFiscal.CVE_CP = newDatosPyme.Cve_CP;
                                domicilioFiscal.Colonia = newDatosPyme.Cve_CP.ToString();
                                domicilioFiscal.CP = newDatosPyme.Codigo_Postal;
                                domicilioFiscal.Calle = "";
                                domicilioFiscal.Num_Int = "";
                                domicilioFiscal.Num_Ext = "";
                                domicilioFiscal.Referencia_Dom = "";
                                domicilioFiscal.Cve_Tipo_Propiedad = null;

                                Captura.ActualizaDireccion(domicilioFiscal);

                                /*INSERTAR EVENTO EDITA DATOS CLASIFICACION PYME EN LOG*/
                                var cambiosDomicilioFiscal = Insertlog.GetCambiosDatos(domicilioFiscalAnterior, domicilioFiscal);
                                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                                    "EDITA DATOS CLASIFICACION PYME", NumeroCredito,
                                    "", "Datos Domicilio Fiscal", cambiosDomicilioFiscal[0], cambiosDomicilioFiscal[1]);
                            }

                            domicilioNegocio.Cve_Estado = newDatosPyme.Cve_Estado;
                            domicilioNegocio.Cve_Deleg_Municipio = newDatosPyme.Cve_Deleg_Municipio;
                            domicilioNegocio.CVE_CP = newDatosPyme.Cve_CP;
                            domicilioNegocio.Colonia = newDatosPyme.Cve_CP.ToString();
                            domicilioNegocio.CP = newDatosPyme.Codigo_Postal;
                            domicilioNegocio.Calle = "";
                            domicilioNegocio.Num_Int = "";
                            domicilioNegocio.Num_Ext = "";
                            domicilioNegocio.Referencia_Dom = "";
                            domicilioNegocio.Cve_Tipo_Propiedad = null;

                            Captura.ActualizaDireccion(domicilioNegocio);

                            /*INSERTAR EVENTO EDITA DATOS CLASIFICACION PYME EN LOG*/
                            var cambiosDomicilioNegocio = Insertlog.GetCambiosDatos(domicilioNegocioAnterior, domicilioNegocio);
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                                "EDITA DATOS CLASIFICACION PYME", NumeroCredito,
                                "", "Datos Domicilio Negocio ", cambiosDomicilioNegocio[0], cambiosDomicilioNegocio[1]);

                        }
                    }
                    
                    TxtRPUPresupuesto.Text = Session["ValidRPU"].ToString();
                    var infoTarifa = wuAltaBajaEquipos1.InicializaUserControl(NumeroCredito, razonSocial);

                    if (infoTarifa != null)
                    {
                        if (!infoTarifa.AnioFactValido || !infoTarifa.PeriodosValidos)
                        {
                            ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                "alert('La facturación del usuario no cumple con las condiciones de este programa');",
                                true);
                            if (startNext != null) startNext.Enabled = false;
                            e.Cancel = true;
                        }
                    }

                    EditaPyme = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                    "alert('Su empresa no cumple con las características de PyME');",
                                    true);

                    EnableDatos();
                    divEditar.Visible = false;

                    if (startNext != null) startNext.Enabled = false;
                    e.Cancel = true;
                    EditaPyme = false;
                }
            }
        }
        
        #endregion
        #endregion

        #region CAPTURA BASICA    
        
        #region Metodos Protegidos

        protected void CargaDatosCliente()
        {
            var cliente = SolicitudCreditoAcciones.ObtenClienteComplejo(IdProveedor, IdSucursal, Convert.ToInt32(Session["IdCliente"]), IdNegocio);

            TipoSociedad = int.Parse(cliente.DatosCliente.Cve_Tipo_Sociedad.ToString());
            TxtNumeroServicioRPUBasica.Text = Session["ValidRPU"].ToString();
            txtNumeroCreditoBasica.Text = NumeroCredito;

            LlenaDDxTipoIdentificacionCBas();
            LLenarDdxAcreditacionCBas();
            //DDXTipoPersonaCBas.SelectedValue = cliente.DatosCliente.Cve_Tipo_Sociedad.ToString();
            var negocio = cliente.DatosCliente.CLI_Negocio.First();
            if (cliente.DatosCliente.Cve_Tipo_Sociedad == 1)
            {
                TxtRFCFisicaCBas.Enabled = false;
                TxtNombrePFisicaCBas.Enabled = false;
                TxtApellidoPaternoCBas.Enabled = false;
                TxtApellidoMaternoCBas.Enabled = false;
                TxtFechaNacimietoCBas.Enabled = false;
                
                var fechaNacimiento = cliente.DatosCliente.Fec_Nacimiento.Value.Date;

                TxtApellidoPaternoCBas.Text = cliente.DatosCliente.Ap_Paterno;
                TxtApellidoMaternoCBas.Text = cliente.DatosCliente.Ap_Materno;
                TxtNombrePFisicaCBas.Text = cliente.DatosCliente.Nombre;
                LlenarDDxSexoCBas();
                if (cliente.DatosCliente.Genero != null && cliente.DatosCliente.Genero != 0)
                {
                    DDXSexoCBas.SelectedValue = cliente.DatosCliente.Genero.ToString();                    
                }
                TxtFechaNacimietoCBas.Text = fechaNacimiento.Year.ToString() + "-" + fechaNacimiento.Month.ToString().PadLeft(2, '0') +
                                           "-" + fechaNacimiento.Day.ToString().PadLeft(2, '0');
                TxtRFCFisicaCBas.Text = cliente.DatosCliente.RFC;
                TxtCURPCBas.Text = cliente.DatosCliente.CURP;
                LlenarLisBoxEstadoCivilCBas();
                if (cliente.DatosCliente.Cve_Estado_Civil != null)
                {
                    RBEstadoCivilCBas.SelectedValue = cliente.DatosCliente.Cve_Estado_Civil.ToString();
                    if (RBEstadoCivilCBas.SelectedValue == "1")
                    {
                        DDXRegimenMatrimonialCBas.Enabled = false;
                        RFV7.Enabled = false;
                        PanelActaMatrimonio.Visible = false;
                    }
                    else if (RBEstadoCivilCBas.SelectedValue == "2")
                    {
                        DDXRegimenMatrimonialCBas.Enabled = true;
                        RFV7.Enabled = true;
                        PanelActaMatrimonio.Visible = true;
                    }
                }
                //if (cliente.DatosCliente.Cve_Estado_Civil != 1)
                //{
                    LLenarDDxRegimenMatrimonialCBas();
                    DDXRegimenMatrimonialCBas.SelectedValue = cliente.DatosCliente.IdRegimenConyugal == null || cliente.DatosCliente.IdRegimenConyugal == 0
                    ? "Seleccione"
                    : cliente.DatosCliente.IdRegimenConyugal.ToString();
                DDXRegimenMatrimonialCBas.Enabled = cliente.DatosCliente.Cve_Estado_Civil != 1;
                //}
                if (negocio.IdTipoAcreditacion != null)
                {
                    DDXOcupacionCBas.SelectedValue = negocio.IdTipoAcreditacion.ToString();
                }
                if (negocio.IdTipoIdentificacion != null)
                {
                    DDXTipoIdentificaFisicaCBas.SelectedValue = negocio.IdTipoIdentificacion.ToString();
                }
                TxtNroIdentificacionCBas.Text = negocio.Numero_Identificacion;
                TxtEmailCBas.Text = cliente.DatosCliente.email;

                PanelPersonaFisica.Visible = true;
                PanelPersonaMoral.Visible = false;
            }

            if (cliente.DatosCliente.Cve_Tipo_Sociedad == 2)
            {
                    TxtRFCMoralCBas.Enabled = false;
                    TxtRazonSocialCBas.Enabled = false;
                    TxtFechaConstitucionCBas.Enabled = false;
               
                var fechaCons = cliente.DatosCliente.Fec_Nacimiento.Value.Date;

                TxtRazonSocialCBas.Text = cliente.DatosCliente.Razon_Social;
                TxtFechaConstitucionCBas.Text = fechaCons.Year.ToString() + "-" + fechaCons.Month.ToString().PadLeft(2, '0') +
                                           "-" + fechaCons.Day.ToString().PadLeft(2, '0');
                TxtRFCMoralCBas.Text = cliente.DatosCliente.RFC.ToUpper();
                if (negocio.IdTipoIdentificacion != null)
                {
                    DDXTipoIdentificaMoralCBas.SelectedValue = negocio.IdTipoIdentificacion.ToString();
                }
                TxtNumeroIdentMoralCBas.Text = negocio.Numero_Identificacion;
                TxtEmailMoralCBas.Text = cliente.DatosCliente.email;
                if (negocio.IdTipoAcreditacion != null)
                {
                    DDXAcreditaCBas.SelectedValue = negocio.IdTipoAcreditacion.ToString();
                }

                PanelPersonaFisica.Visible = false;
                PanelPersonaMoral.Visible = true;
                PanelActaMatrimonio.Visible = false;
            }

            LlenarDdxEstado(DDXEstadoCBas, DDXMunicipioCBas, DDXColoniaCBas);
            LlenarDDxTipoPropiedadCBas();
            LLenaddxMismoDomFiscal_Negocio();
            LlenarDdxEstado(DDXEstadoFiscalCBas, DDXMunicipioFiscalCBas, DDXColoniaFiscalCBas);

            var domicilioNegocio = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 1);
            if (domicilioNegocio != null)
            {
                TxtCalleCBas.Text = domicilioNegocio.Calle;
                TxtNumeroExteriorCBas.Text = domicilioNegocio.Num_Ext;
                TxtNumeroInteriorCBas.Text = domicilioNegocio.Num_Int;
                TxtCPCBas.Text = domicilioNegocio.CP;
                DDXEstadoCBas.SelectedValue = domicilioNegocio.Cve_Estado.ToString();
                LlenarDDxMunicipo((int)domicilioNegocio.Cve_Estado, DDXMunicipioCBas, DDXColoniaCBas);
                DDXMunicipioCBas.SelectedValue = domicilioNegocio.Cve_Deleg_Municipio.ToString();
                LlenarDDxColonias((int)domicilioNegocio.Cve_Estado, (int)domicilioNegocio.Cve_Deleg_Municipio, DDXColoniaCBas, DDXColoniaFiscalHiddenCBas);
                DDXColoniaCBas.SelectedValue = domicilioNegocio.CVE_CP.ToString();
                TxtTelefonoCBas.Text = domicilioNegocio.Telefono_Local;
                if (domicilioNegocio.Cve_Tipo_Propiedad != null)
                {
                    DDXTipoPropiedadCBas.SelectedValue = domicilioNegocio.Cve_Tipo_Propiedad.ToString();
                }
                txtReferenciaDomNegocioMoral.Text = domicilioNegocio.Referencia_Dom;
            }

            panelMismoDom_Fiscal_Negocio.Visible = false;

            var domicilioFiscal = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 2);
            if (domicilioFiscal != null)
            {
                ddxMismoDom_Fiscal_Negocio.SelectedValue = domicilioFiscal.CVE_CP == domicilioNegocio.CVE_CP
                                                               ? "SI"
                                                               : "NO";
                if (ddxMismoDom_Fiscal_Negocio.SelectedValue == "NO")
                {
                    panelMismoDom_Fiscal_Negocio.Visible = true;
                }
                TxtCalleFiscalCBas.Text = domicilioFiscal.Calle;
                TxtExteriorFiscalCBas.Text = domicilioFiscal.Num_Ext;
                TxtInteriorFiscalCBas.Text = domicilioFiscal.Num_Int;
                TxtCPFiscalCBas.Text = domicilioFiscal.CP;
                DDXEstadoFiscalCBas.SelectedValue = domicilioFiscal.Cve_Estado.ToString();
                LlenarDDxMunicipo((int)domicilioFiscal.Cve_Estado, DDXMunicipioFiscalCBas, DDXColoniaFiscalCBas);
                DDXMunicipioFiscalCBas.SelectedValue = domicilioFiscal.Cve_Deleg_Municipio.ToString();
                LlenarDDxColonias((int)domicilioFiscal.Cve_Estado, (int)domicilioFiscal.Cve_Deleg_Municipio, DDXColoniaFiscalCBas, DDXColoniaFiscalHiddenCBas);
                DDXColoniaFiscalCBas.SelectedValue = domicilioFiscal.CVE_CP.ToString();
                TxttelefonoFiscalCBas.Text = domicilioFiscal.Telefono_Oficina;
                if (domicilioFiscal.Cve_Tipo_Propiedad != null)
                {
                    DDXTipoPropiedadFiscalCBas.SelectedValue = domicilioFiscal.Cve_Tipo_Propiedad.ToString();
                }
                txtReferenciaDomFiscalMoral.Text = domicilioFiscal.Referencia_Dom;
            }

            BloqueaCamposCliente(!MopValido);
            BloqueaDireccionFiscal();

            if (ddxMismoDom_Fiscal_Negocio.SelectedValue == "NO")
            {
                ddxMismoDom_Fiscal_Negocio.Enabled = true;
                panelMismoDom_Fiscal_Negocio.Enabled = true;
            }

        }

        public bool ValidaObligadoSolidario()
        {
            if (RBListTipoPersona.SelectedValue == "")
                return false;

            return true;
        }

        #endregion

        #region Eventos Controles
        
        protected void DDXEstadoFiscalCBas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LlenarDDxMunicipoFiscalCBas(int.Parse(DDXEstadoFiscalCBas.SelectedValue));
            LlenarDDxMunicipo(int.Parse(DDXEstadoFiscalCBas.SelectedValue),DDXMunicipioFiscalCBas,DDXColoniaFiscalCBas);
        }

        protected void DDXMunicipioFiscalCBas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LlenarDDxColoniaFiscalCBas(int.Parse(DDXEstadoFiscalCBas.SelectedValue), int.Parse(DDXMunicipioFiscalCBas.SelectedValue));
            LlenarDDxColonias(int.Parse(DDXEstadoFiscalCBas.SelectedValue), int.Parse(DDXMunicipioFiscalCBas.SelectedValue),DDXColoniaFiscalCBas,DDXColoniaFiscalHiddenCBas);
        }

        protected void DDXColoniaFiscalCBas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXColoniaFiscalHiddenCBas.SelectedValue = DDXColoniaFiscalCBas.SelectedValue;
            TxtCPFiscalCBas.Text = DDXColoniaFiscalHiddenCBas.SelectedItem.Text;
        }

        protected void ddxMismoDom_Fiscal_Negocio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddxMismoDom_Fiscal_Negocio.SelectedValue == "SI")
            {
                panelMismoDom_Fiscal_Negocio.Visible = false;
                DDXEstadoFiscalCBas.SelectedValue = DDXEstadoCBas.SelectedValue;
                LlenarDDxMunicipo(int.Parse(DDXEstadoFiscalCBas.SelectedValue), DDXMunicipioFiscalCBas, DDXColoniaFiscalCBas);
                DDXMunicipioFiscalCBas.SelectedValue = DDXMunicipioCBas.SelectedValue;
                LlenarDDxColonias(int.Parse(DDXEstadoFiscalCBas.SelectedValue), int.Parse(DDXMunicipioFiscalCBas.SelectedValue), DDXColoniaFiscalCBas, DDXColoniaFiscalHiddenCBas);
                DDXColoniaFiscalCBas.SelectedValue = DDXColoniaCBas.SelectedValue;
                TxtCPFiscalCBas.Text = TxtCPCBas.Text;
                TxtCalleFiscalCBas.Text = TxtCalleCBas.Text;
                TxttelefonoFiscalCBas.Text = TxtTelefonoCBas.Text;
                TxtExteriorFiscalCBas.Text = TxtNumeroExteriorCBas.Text;
                TxtInteriorFiscalCBas.Text = TxtNumeroInteriorCBas.Text;
                DDXTipoPropiedadFiscalCBas.SelectedValue = DDXTipoPropiedadCBas.SelectedValue;
                txtReferenciaDomFiscalMoral.Text = txtReferenciaDomNegocioMoral.Text.ToUpper();
            }
            else
            {
                panelMismoDom_Fiscal_Negocio.Visible = true;
                DDXEstadoFiscalCBas.SelectedIndex = 0;
                DDXMunicipioFiscalCBas.Items.Clear();
                DDXColoniaFiscalCBas.Items.Clear();
                TxtCPFiscalCBas.Text = ""; 
                DDXTipoPropiedadFiscalCBas.SelectedIndex = 0; 
                TxtCalleFiscalCBas.Text = "";
                TxttelefonoFiscalCBas.Text = "";
                TxtExteriorFiscalCBas.Text = "";
                TxtInteriorFiscalCBas.Text = "";
                txtReferenciaDomFiscalMoral.Text = "";
            }
        }
        
        protected void RBEstadoCivilCBas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBEstadoCivilCBas.SelectedValue == "1")
            {
                DDXRegimenMatrimonialCBas.Enabled = false;
                DDXRegimenMatrimonialCBas.SelectedValue = "Seleccione";
                RFV7.Enabled = false;
                PanelActaMatrimonio.Visible = false;
            }

            if (RBEstadoCivilCBas.SelectedValue == "2")
            {
                DDXRegimenMatrimonialCBas.Enabled = true;
                RFV7.Enabled = true;
                PanelActaMatrimonio.Visible = true;
            }
        }

        #endregion

        #region LLenar catalogos

        protected void LLenaddxMismoDomFiscal_Negocio()
        {
            ddxMismoDom_Fiscal_Negocio.Items.Clear();
            ddxMismoDom_Fiscal_Negocio.Items.Insert(0, "Seleccione");
            ddxMismoDom_Fiscal_Negocio.Items.Insert(1, "SI"); 
            ddxMismoDom_Fiscal_Negocio.Items.Insert(2, "NO");
            ddxMismoDom_Fiscal_Negocio.SelectedIndex = 0;
        }
       
        //protected void LlenarCatalogoEstadosCBas()
        //{
        //    var lstEstado = CatalogosSolicitud.ObtenCatEstadosRepublica();
        //    if (lstEstado == null) return;
        //    DDXEstadoCBas.DataSource = lstEstado;
        //    DDXEstadoCBas.DataValueField = "Cve_Estado";
        //    DDXEstadoCBas.DataTextField = "Dx_Nombre_Estado";
        //    DDXEstadoCBas.DataBind();

        //    DDXEstadoCBas.Items.Insert(0, "Seleccione");
        //    DDXEstadoCBas.SelectedIndex = 0;

        //    DDXEstadoFiscalCBas.DataSource = lstEstado;
        //    DDXEstadoFiscalCBas.DataValueField = "Cve_Estado";
        //    DDXEstadoFiscalCBas.DataTextField = "Dx_Nombre_Estado";
        //    DDXEstadoFiscalCBas.DataBind();

        //    DDXEstadoFiscalCBas.Items.Insert(0, "Seleccione");
        //    DDXEstadoFiscalCBas.SelectedIndex = 0;
        //}

       //protected void LlenarDDxMunicipoCBas(int idEstado)
       // {
       //     try
       //     {
       //         DDXMunicipioCBas.Items.Clear();

       //         var lstMunicipio = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

       //         if (lstMunicipio == null) return;
       //         DDXMunicipioCBas.DataSource = lstMunicipio;
       //         DDXMunicipioCBas.DataValueField = "Cve_Deleg_Municipio";
       //         DDXMunicipioCBas.DataTextField = "Dx_Deleg_Municipio";
       //         DDXMunicipioCBas.DataBind();

       //         DDXMunicipioCBas.Items.Insert(0, "Seleccione");
       //         DDXMunicipioCBas.SelectedIndex = 0;
       //     }
       //     catch (Exception ex)
       //     {

       //     }

       // }

        //protected void LlenarDDxMunicipoFiscalCBas(int idEstado)
        //{
        //    try
        //    {
        //        DDXMunicipioFiscalCBas.Items.Clear();

        //        var lstMunicipio = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

        //        if (lstMunicipio == null) return;
        //        DDXMunicipioFiscalCBas.DataSource = lstMunicipio;
        //        DDXMunicipioFiscalCBas.DataValueField = "Cve_Deleg_Municipio";
        //        DDXMunicipioFiscalCBas.DataTextField = "Dx_Deleg_Municipio";
        //        DDXMunicipioFiscalCBas.DataBind();

        //        DDXMunicipioFiscalCBas.Items.Insert(0, "Seleccione");
        //        DDXMunicipioFiscalCBas.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        //protected void LlenarDDxColoniasCBas(int idEstado, int idMunicipio)
        //{
        //    try
        //    {
        //        DDXColoniaCBas.Items.Clear();

        //        var lstColonia = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

        //        if (lstColonia == null) return;
        //        DDXColoniaCBas.DataSource = lstColonia;
        //        DDXColoniaCBas.DataValueField = "Cve_CP";
        //        DDXColoniaCBas.DataTextField = "Dx_Colonia";
        //        DDXColoniaCBas.DataBind();

        //        DDXColoniaCBas.Items.Insert(0, "Seleccione");
        //        DDXColoniaCBas.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
                
        //    }

        //}

        //protected void LlenarDDxColoniaFiscalCBas(int idEstado, int idMunicipio)
        //{
        //    try
        //    {
        //        DDXColoniaFiscalCBas.Items.Clear();
        //        DDXColoniaFiscalHiddenCBas.Items.Clear();

        //        var lstColonia = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

        //        if (lstColonia == null) return;
        //        DDXColoniaFiscalCBas.DataSource = lstColonia;
        //        DDXColoniaFiscalCBas.DataValueField = "Cve_CP";
        //        DDXColoniaFiscalCBas.DataTextField = "Dx_Colonia";
        //        DDXColoniaFiscalCBas.DataBind();

        //        DDXColoniaFiscalCBas.Items.Insert(0, "Seleccione");
        //        DDXColoniaFiscalCBas.SelectedIndex = 0;

        //        DDXColoniaFiscalHiddenCBas.DataSource = lstColonia;
        //        DDXColoniaFiscalHiddenCBas.DataValueField = "Cve_CP";
        //        DDXColoniaFiscalHiddenCBas.DataTextField = "Codigo_Postal";
        //        DDXColoniaFiscalHiddenCBas.DataBind();
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        protected void LlenarDDxSexoCBas()
        {
            var lstSexo = CatalogosSolicitud.ObtenCatSexos();

            if (lstSexo == null) return;
            DDXSexoCBas.DataSource = lstSexo;
            DDXSexoCBas.DataValueField = "FG_Sexo";
            DDXSexoCBas.DataTextField = "Dx_Sexo";
            DDXSexoCBas.DataBind();

            DDXSexoCBas.Items.Insert(0, "Seleccione");
            DDXSexoCBas.SelectedIndex = 0;
        }

        //protected void LlenarDDxSexoCComp()
        //{
        //    var lstSexo = CatalogosSolicitud.ObtenCatSexos();

        //    if (lstSexo == null) return;
        //    DDXSexoCComp.DataSource = lstSexo;
        //    DDXSexoCComp.DataValueField = "FG_Sexo";
        //    DDXSexoCComp.DataTextField = "Dx_Sexo";
        //    DDXSexoCComp.DataBind();

        //    DDXSexoCComp.Items.Insert(0, "Seleccione");
        //    DDXSexoCComp.SelectedIndex = 0;
        //}

        protected void LLenarDDxRegimenMatrimonialCBas()
        {
            var lstregimen = CatalogosSolicitud.ObtenCatRegimenConyugal();

            if (lstregimen == null) return;
            DDXRegimenMatrimonialCBas.DataSource = lstregimen;
            DDXRegimenMatrimonialCBas.DataValueField = "IdRegimenConyugal";
            DDXRegimenMatrimonialCBas.DataTextField = "RegimenConyugal";
            DDXRegimenMatrimonialCBas.DataBind();

            DDXRegimenMatrimonialCBas.Items.Insert(0, "Seleccione");
            DDXRegimenMatrimonialCBas.SelectedIndex = 0;
        }

        protected void LLenarDdxAcreditacionCBas()
        {
            var lstAcreditacion = CatalogosSolicitud.ObtenCatTipoAcreditacion();

            if (lstAcreditacion == null) return;
            DDXOcupacionCBas.DataSource = lstAcreditacion;
            DDXOcupacionCBas.DataValueField = "IdTipoAcreditacion";
            DDXOcupacionCBas.DataTextField = "TipoAcreditacion";
            DDXOcupacionCBas.DataBind();

            DDXOcupacionCBas.Items.Insert(0,"Seleccione");
            DDXOcupacionCBas.SelectedIndex = 0;

            DDXAcreditaCBas.DataSource = lstAcreditacion;
            DDXAcreditaCBas.DataValueField = "IdTipoAcreditacion";
            DDXAcreditaCBas.DataTextField = "TipoAcreditacion";
            DDXAcreditaCBas.DataBind();

            DDXAcreditaCBas.Items.Insert(0, "Seleccione");
            DDXAcreditaCBas.SelectedIndex = 0;
        }

        protected void LlenaDDxTipoIdentificacionCBas()
        {
            var lstTipoIdentifica = CatalogosSolicitud.ObtenCatIdentificacion();

            if (lstTipoIdentifica == null) return;
            DDXTipoIdentificaFisicaCBas.DataSource = lstTipoIdentifica;
            DDXTipoIdentificaFisicaCBas.DataValueField = "IdTipoIdentificacion";
            DDXTipoIdentificaFisicaCBas.DataTextField = "TipoIdentificacion";
            DDXTipoIdentificaFisicaCBas.DataBind();

            DDXTipoIdentificaFisicaCBas.Items.Insert(0, "Seleccione");
            DDXTipoIdentificaFisicaCBas.SelectedIndex = 0;

            DDXTipoIdentificaMoralCBas.DataSource = lstTipoIdentifica;
            DDXTipoIdentificaMoralCBas.DataValueField = "IdTipoIdentificacion";
            DDXTipoIdentificaMoralCBas.DataTextField = "TipoIdentificacion";
            DDXTipoIdentificaMoralCBas.DataBind();

            DDXTipoIdentificaMoralCBas.Items.Insert(0, "Seleccione");
            DDXTipoIdentificaMoralCBas.SelectedIndex = 0;
        }

        protected void LlenarDDxTipoPropiedadCBas()
        {
            var lstTipoPrpiedad = CatalogosSolicitud.ObtenCatTipoPropiedad();
            DDXTipoPropiedadCBas.Items.Clear();
            DDXTipoPropiedadFiscalCBas.Items.Clear();
            
            if (lstTipoPrpiedad == null) return;
            DDXTipoPropiedadCBas.DataSource = lstTipoPrpiedad;
            DDXTipoPropiedadCBas.DataValueField = "Cve_Tipo_Propiedad";
            DDXTipoPropiedadCBas.DataTextField = "Dx_Tipo_Propiedad";
            DDXTipoPropiedadCBas.DataBind();

            DDXTipoPropiedadCBas.Items.Insert(0, "Seleccione");
            DDXTipoPropiedadCBas.SelectedIndex = 0;

            DDXTipoPropiedadFiscalCBas.DataSource = lstTipoPrpiedad;
            DDXTipoPropiedadFiscalCBas.DataValueField = "Cve_Tipo_Propiedad";
            DDXTipoPropiedadFiscalCBas.DataTextField = "Dx_Tipo_Propiedad";
            DDXTipoPropiedadFiscalCBas.DataBind();

            DDXTipoPropiedadFiscalCBas.Items.Insert(0, "Seleccione");
            DDXTipoPropiedadFiscalCBas.SelectedIndex = 0;
        }

        protected void LlenarLisBoxEstadoCivilCBas()
        {
            var lstEstadoCivil = CatalogosSolicitud.ObtenCatEstadoCivil();

            if (lstEstadoCivil == null) return;
            RBEstadoCivilCBas.DataSource = lstEstadoCivil;
            RBEstadoCivilCBas.DataValueField = "Cve_Estado_Civil";
            RBEstadoCivilCBas.DataTextField = "Dx_Estado_Civil";
            RBEstadoCivilCBas.DataBind();

            RBEstadoCivilCBas.Items.Insert(0, "Seleccione");
            RBEstadoCivilCBas.SelectedIndex = 0;
        }

        //protected void LlenarDDxTipoPersonaCBas()
        //{
        //    var lstTipoPersona = CatalogosSolicitud.ObtenCatTipoSociedad();

        //    if (lstTipoPersona == null) return;
        //    DDXTipoPersonaCBas.DataSource = lstTipoPersona;
        //    DDXTipoPersonaCBas.DataValueField = "Cve_Tipo_Sociedad";
        //    DDXTipoPersonaCBas.DataTextField = "Dx_Tipo_Sociedad";
        //    DDXTipoPersonaCBas.DataBind();

        //    DDXTipoPersonaCBas.Items.Insert(0, "Seleccione");
        //    DDXTipoPersonaCBas.SelectedIndex = 0;
        //}
        
        #endregion                     

        #region Metodos Publicos

        //protected bool InsertaHorariosNegocio(byte tipoHorario)
        //{
        //    var band = false;
        //    var lstHorarios = new List<CLI_HORARIOS_OPERACION>();
        //    CLI_HORARIOS_OPERACION horario;

        //    if (ChkLunes.Checked)
        //    {
        //        horario = new CLI_HORARIOS_OPERACION
        //        {
        //            No_Credito = NumeroCredito,
        //            IDTIPOHORARIO = tipoHorario,
        //            ID_DIA_OPERACION = 1, //Lunes
        //            Hora_Inicio = ChkLunes.Checked ? DDXInicioLunes.SelectedItem.Text : "",
        //            ////Hora_Fin = ChkLunes.Checked ? DDXFinLunes.SelectedItem.Text : "",
        //            IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldLunes.Value)

        //        };
        //        lstHorarios.Add(horario);
        //    }

        //    if (ChkMartes.Checked)
        //    {
        //        horario = new CLI_HORARIOS_OPERACION
        //        {
        //            No_Credito = NumeroCredito,
        //            IDTIPOHORARIO = tipoHorario,
        //            ID_DIA_OPERACION = 2, //Martes
        //            Hora_Inicio = ChkMartes.Checked ? DDXInicioMartes.SelectedItem.Text : "",
        //            //Hora_Fin = ChkMartes.Checked ? DDXFinMartes.SelectedItem.Text : "",
        //            IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldMartes.Value)
        //        };
        //        lstHorarios.Add(horario);
        //    }

        //    if (ChkMiercoles.Checked)
        //    {
        //        horario = new CLI_HORARIOS_OPERACION
        //        {
        //            No_Credito = NumeroCredito,
        //            IDTIPOHORARIO = tipoHorario,
        //            ID_DIA_OPERACION = 3, //Miercoles
        //            Hora_Inicio = ChkMiercoles.Checked ? DDXInicioMiercoles.SelectedItem.Text : "",
        //            //Hora_Fin = ChkMiercoles.Checked ? DDXFinMiercoles.SelectedItem.Text : "",
        //            IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldMiercoles.Value)
        //        };
        //        lstHorarios.Add(horario);
        //    }

        //    if (ChkJueves.Checked)
        //    {
        //        horario = new CLI_HORARIOS_OPERACION
        //        {
        //            No_Credito = NumeroCredito,
        //            IDTIPOHORARIO = tipoHorario,
        //            ID_DIA_OPERACION = 4, //Jueves
        //            Hora_Inicio = ChkJueves.Checked ? DDXInicioJueves.SelectedItem.Text : "",
        //            //Hora_Fin = ChkJueves.Checked ? DDXFinJueves.SelectedItem.Text : "",
        //            IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldJueves.Value)
        //        };
        //        lstHorarios.Add(horario);
        //    }

        //    if (ChkViernes.Checked)
        //    {
        //        horario = new CLI_HORARIOS_OPERACION
        //        {
        //            No_Credito = NumeroCredito,
        //            IDTIPOHORARIO = tipoHorario,
        //            ID_DIA_OPERACION = 5, //Viernes
        //            Hora_Inicio = ChkViernes.Checked ? DDXInicioViernes.SelectedItem.Text : "",
        //            //Hora_Fin = ChkViernes.Checked ? DDXFinViernes.SelectedItem.Text : "",
        //            IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldViernes.Value)
        //        };
        //        lstHorarios.Add(horario);
        //    }

        //    if (ChkSabado.Checked)
        //    {
        //        horario = new CLI_HORARIOS_OPERACION
        //        {
        //            No_Credito = NumeroCredito,
        //            IDTIPOHORARIO = tipoHorario,
        //            ID_DIA_OPERACION = 6, //Sabado
        //            Hora_Inicio = ChkSabado.Checked ? DDXInicioSabado.SelectedItem.Text : "",
        //            //Hora_Fin = ChkSabado.Checked ? DDXFinSabado.SelectedItem.Text : "",
        //            IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldSabado.Value)
        //        };
        //        lstHorarios.Add(horario);
        //    }

        //    if (ChkDomingo.Checked)
        //    {
        //        horario = new CLI_HORARIOS_OPERACION
        //        {
        //            No_Credito = NumeroCredito,
        //            IDTIPOHORARIO = tipoHorario,
        //            ID_DIA_OPERACION = 7, //Domingo
        //            Hora_Inicio = ChkDomingo.Checked ? DDXInicioDomingo.SelectedItem.Text : "",
        //            //Hora_Fin = ChkDomingo.Checked ? DDXFinDomingo.SelectedItem.Text : "",
        //            IDHORASTRABAJADAS = Convert.ToDecimal(HiddenFieldDomingo.Value)
        //        };
        //        lstHorarios.Add(horario);
        //    }

        //    var horasOperacionTotal = new H_OPERACION_TOTAL
        //    {
        //        No_Credito = NumeroCredito,
        //        IDTIPOHORARIO = tipoHorario,
        //        HORAS_SEMANA = Convert.ToDouble(TxtHorasSemanaCComp.Text),
        //        SEMANAS_AÑO = Convert.ToDouble(TxtSemanasAnioCComp.Text),
        //        HORAS_AÑO = Convert.ToDouble(TxtHorasAnioCComp.Text)
        //    };

        //    if (SolicitudCreditoAcciones.ActualizaHorarioOperacion_IdCredSust(lstHorarios, horasOperacionTotal))
        //        band = true;

        //    return band;
        //}

        public void CargaDatosCapturaBasica()
        {
            panelMismoDom_Fiscal_Negocio.Visible = false;
            TxtNumeroServicioRPUBasica.Text = Session["ValidRPU"].ToString();
            txtNumeroCreditoBasica.Text = NumeroCredito;

            LLenaddxMismoDomFiscal_Negocio();
           
                LlenarDdxEstado(DDXEstadoCBas, DDXEstadoFiscalCBas, DDXColoniaCBas);
                LlenarDdxEstado(DDXEstadoFiscalCBas,DDXMunicipioFiscalCBas,DDXColoniaFiscalCBas);
            
          //LlenarCatalogoEstadosCBas();
            LlenarDDxSexoCBas();
            LLenarDDxRegimenMatrimonialCBas();
            LLenarDdxAcreditacionCBas();
            LlenaDDxTipoIdentificacionCBas();
            //LlenaDDxTipoIdentificacionCBas();
            LlenarDDxTipoPropiedadCBas();
            LlenarLisBoxEstadoCivilCBas();
            //LlenarDDxTipoPersonaCBas();
            //DDXTipoPersonaCBas.Enabled = false;

            var datosPyme = SolicitudCreditoAcciones.BuscaDatosPyme(Session["ValidRPU"].ToString());

            if (datosPyme != null)
            {
                if (DDXTipoPersona.SelectedIndex == 1)
                {
                    PanelPersonaFisica.Visible = true;
                    PanelPersonaMoral.Visible = false;
                }
                else
                {
                    PanelPersonaMoral.Visible = true;
                    PanelPersonaFisica.Visible = false;
                }

                //DDXTipoPersonaCBas.SelectedIndex = Convert.ToInt32(datosPyme.Cve_Tipo_Sociedad);
                if (datosPyme.Cve_Tipo_Sociedad == 1)
                {
                    TxtRFCFisicaCBas.Text = datosPyme.RFC;
                    TxtNombrePFisicaCBas.Text = TxtNombrePFisicaVPyME.Text;
                    TxtApellidoPaternoCBas.Text = TxtApellidoPaternoVPyME.Text;
                    TxtApellidoMaternoCBas.Text = TxtApellidoMaternoVPyME.Text;
                    TxtFechaNacimietoCBas.Text = TxtFechaNacimientoVPyME.Text;

                    TxtRFCFisicaCBas.Enabled = false;
                    TxtNombrePFisicaCBas.Enabled = false;
                    TxtApellidoPaternoCBas.Enabled = false;
                    TxtApellidoMaternoCBas.Enabled = false;
                    TxtFechaNacimietoCBas.Enabled = false;

                }
                else
                {
                    TxtRFCMoralCBas.Text = datosPyme.RFC.ToUpper(); 
                    TxtRazonSocialCBas.Text = TxtRazonSocialVPyME.Text;
                    TxtFechaConstitucionCBas.Text = TxtFechaConstitucionVPyME.Text;

                    TxtRFCMoralCBas.Enabled = false;
                    TxtRazonSocialCBas.Enabled = false;
                    TxtFechaConstitucionCBas.Enabled = false;
                }
                DDXEstadoCBas.SelectedValue = datosPyme.Cve_Estado.ToString();
                if (datosPyme.Cve_Estado != null)
                {
                    //LlenarDDxMunicipoCBas((int)datosPyme.Cve_Estado);
                    LlenarDDxMunicipo((int)datosPyme.Cve_Estado,DDXMunicipioCBas,DDXColoniaCBas);
                    DDXMunicipioCBas.SelectedValue = datosPyme.Cve_Deleg_Municipio.ToString();
                    if (datosPyme.Cve_Deleg_Municipio != null)
                        //LlenarDDxColoniasCBas((int)datosPyme.Cve_Estado, (int)datosPyme.Cve_Deleg_Municipio);
                        LlenarDDxColonias((int)datosPyme.Cve_Estado, (int)datosPyme.Cve_Deleg_Municipio,DDXColoniaCBas,DDXColoniaFiscalCBas);
                }
                DDXColoniaCBas.SelectedValue = datosPyme.Cve_CP.ToString();
                TxtCPCBas.Text = datosPyme.Codigo_Postal;

                BloqueaDireccionFiscal();
            }
        }
        //private Boolean ValidateCurp(string rfc, string curp)
        //{
        //    var result = false;

        //    var persona = (CompanyType)Enum.Parse(typeof(CompanyType), DDXTipoPersonaCBas.SelectedValue);

        //    // if it is persona Moral then there is no CURP return true
        //    // or if it is not Moral, then validate that curp starts with the first part of rfc (no homoclave)
        //    if (persona == CompanyType.MORAL || curp.StartsWith(rfc.Substring(0, rfc.Length - 3)))
        //    {
        //        result = true;
        //    }

        //    return result;
        //}

        public Cliente ObtenCliente()
        {
            var cliente = new Cliente
            {
                DatosCliente =
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdCliente = IdCliente,
                    Cve_Tipo_Sociedad = Byte.Parse(DDXTipoPersona.SelectedValue)
                }
            };
            var negocio = new CLI_Negocio
            {
                Id_Proveedor = IdProveedor,
                Id_Branch = IdSucursal,
                IdCliente = IdCliente
            };
            cliente.DatosCliente.CLI_Negocio = new List<CLI_Negocio> { negocio };

            if (DDXTipoPersona.SelectedValue == "1")
            {
                cliente.DatosCliente.Ap_Paterno = TxtApellidoPaternoCBas.Text;
                cliente.DatosCliente.Ap_Materno = TxtApellidoMaternoCBas.Text;
                cliente.DatosCliente.Nombre = TxtNombrePFisicaCBas.Text;
                cliente.DatosCliente.Genero = byte.Parse(DDXSexoCBas.SelectedValue);
                cliente.DatosCliente.Fec_Nacimiento = Convert.ToDateTime(TxtFechaNacimietoCBas.Text);
                cliente.DatosCliente.RFC = TxtRFCFisicaCBas.Text;
                cliente.DatosCliente.CURP = TxtCPCBas.Text;
                cliente.DatosCliente.Cve_Estado_Civil = Byte.Parse(RBEstadoCivilCBas.SelectedValue);
                cliente.DatosCliente.IdRegimenConyugal = Byte.Parse(DDXRegimenMatrimonialCBas.SelectedValue);
                cliente.DatosCliente.email = TxtEmailCBas.Text;

                negocio.IdTipoAcreditacion = Byte.Parse(DDXOcupacionCBas.SelectedValue);
                negocio.IdTipoIdentificacion = Byte.Parse(DDXTipoIdentificaFisicaCBas.SelectedValue);
                negocio.Numero_Identificacion = TxtNroIdentificacionCBas.Text;
            }

            if (DDXTipoPersona.SelectedValue == "2")
            {
                cliente.DatosCliente.Razon_Social = TxtRazonSocialCBas.Text;
                cliente.DatosCliente.RFC = TxtRFCMoralCBas.Text.ToUpper();
                cliente.DatosCliente.Fec_Nacimiento = Convert.ToDateTime(TxtFechaConstitucionCBas.Text);
                cliente.DatosCliente.email = TxtEmailMoralCBas.Text;

                negocio.IdTipoIdentificacion = Byte.Parse(DDXTipoIdentificaMoralCBas.SelectedValue);
                negocio.Numero_Identificacion = TxtNumeroIdentMoralCBas.Text;
                negocio.IdTipoAcreditacion = Byte.Parse(DDXAcreditaCBas.SelectedValue);
            }

            var rpu = Session["ValidRPU"].ToString();
            var domicilioNegocio = new DIR_Direcciones
            {
                Id_Proveedor = IdProveedor,
                Id_Branch = IdSucursal,
                IdCliente = IdCliente,
                IdTipoDomicilio = 1,
                RPU = rpu,
                CVE_CP = int.Parse(DDXColoniaCBas.SelectedValue),
                Colonia = DDXColoniaCBas.SelectedValue,
                Calle = TxtCalleCBas.Text,
                Num_Ext = TxtNumeroExteriorCBas.Text,
                CP = TxtCPCBas.Text,
                Cve_Estado = Convert.ToByte(DDXEstadoCBas.SelectedValue),
                Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipioCBas.SelectedValue),
                Telefono_Local = TxtTelefonoCBas.Text,
                Cve_Tipo_Propiedad = int.Parse(DDXTipoPropiedadCBas.Text),
                Referencia_Dom = txtReferenciaDomNegocioMoral.Text
            };


            var domicilioFiscal = new DIR_Direcciones
            {
                Id_Proveedor = IdProveedor,
                Id_Branch = IdSucursal,
                IdCliente = IdCliente,
                IdTipoDomicilio = 2,
                RPU = rpu,
                CVE_CP = int.Parse(DDXColoniaFiscalCBas.SelectedValue),
                Colonia = DDXColoniaCBas.SelectedValue,
                Calle = TxtCalleFiscalCBas.Text,
                Num_Ext = TxtExteriorFiscalCBas.Text,
                CP = TxtCPFiscalCBas.Text,
                Cve_Estado = Convert.ToByte(DDXEstadoFiscalCBas.SelectedValue),
                Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipioFiscalCBas.SelectedValue),
                Telefono_Local = TxttelefonoFiscalCBas.Text,
                Cve_Tipo_Propiedad = int.Parse(DDXTipoPropiedadFiscalCBas.SelectedValue)
            };

            cliente.DireccionesCliente.Add(domicilioNegocio);
            cliente.DireccionesCliente.Add(domicilioFiscal);

            return cliente;
        }

        public CLI_Cliente ObtenClienteBasico()
        {
            var cliente = new CLI_Cliente
            {
                Id_Proveedor = IdProveedor,
                Id_Branch = IdSucursal,
                //IdCliente = IdCliente,
                Cve_Tipo_Sociedad = Byte.Parse(DDXTipoPersona.SelectedValue)
            };
            var negocio = new CLI_Negocio
            {
                Id_Proveedor = IdProveedor,
                Id_Branch = IdSucursal,
                //IdCliente = IdCliente,
            };
            cliente.CLI_Negocio = new List<CLI_Negocio> { negocio };

            if (cliente.Cve_Tipo_Sociedad == 1)
            {
                cliente.Nombre = TxtNombrePFisicaVPyME.Text.ToUpper();
                cliente.Ap_Paterno = TxtApellidoPaternoVPyME.Text.ToUpper();
                cliente.Ap_Materno = TxtApellidoMaternoVPyME.Text.ToUpper();
                //cliente.Genero = byte.Parse(DDXSexoCBas.SelectedValue);
                cliente.Fec_Nacimiento = Convert.ToDateTime(TxtFechaNacimientoVPyME.Text);
                //cliente.RFC = TxtRFCFisicaCBas.Text;
                //cliente.CURP = TxtCPCBas.Text;
                //cliente.Cve_Estado_Civil = Byte.Parse(RBEstadoCivilCBas.SelectedValue);
                //cliente.IdRegimenConyugal = Byte.Parse(DDXRegimenMatrimonialCBas.SelectedValue);
                //cliente.email = TxtEmailCBas.Text;

                //negocio.IdTipoAcreditacion = Byte.Parse(DDXOcupacionCBas.SelectedValue);
                //negocio.IdTipoIdentificacion = Byte.Parse(DDXTipoIdentificaFisicaCBas.SelectedValue);
                //negocio.Numero_Identificacion = TxtNroIdentificacionCBas.Text;
            }
            else if (cliente.Cve_Tipo_Sociedad == 2)
            {
                cliente.Razon_Social = TxtRazonSocialVPyME.Text;
                cliente.Fec_Nacimiento = Convert.ToDateTime(TxtFechaConstitucionVPyME.Text);
                cliente.RFC = TxtRFCMoralVPyME.Text;
                //cliente.email = TxtEmailMoralCBas.Text;

                //negocio.IdTipoIdentificacion = Byte.Parse(DDXTipoIdentificaMoralCBas.SelectedValue);
                //negocio.Numero_Identificacion = TxtNumeroIdentMoralCBas.Text;
                //negocio.IdTipoAcreditacion = Byte.Parse(DDXAcreditaCBas.SelectedValue);
            }

            var rpu = Session["ValidRPU"].ToString();
            var domicilioNegocio = new DIR_Direcciones
            {
                Id_Proveedor = IdProveedor,
                Id_Branch = IdSucursal,
                //IdCliente = IdCliente,
                IdTipoDomicilio = 1,
                RPU = rpu,
                CP = TxtCP.Text,
                CVE_CP = int.Parse(DDXColonia.SelectedValue),
                Colonia = DDXColonia.SelectedValue,
                //Calle = TxtCalleCBas.Text,
                //Num_Ext = TxtNumeroExteriorCBas.Text,
                Cve_Estado = Convert.ToByte(DDXEstado.SelectedValue),
                Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipio.SelectedValue),
                //Telefono_Local = TxtTelefonoCBas.Text,
                //Cve_Tipo_Propiedad = int.Parse(DDXTipoPropiedadCBas.Text),
                //Referencia_Dom = txtReferenciaDomNegocioMoral.Text
            };

            //var domicilioFiscal = new DIR_Direcciones
            //{
            //    Id_Proveedor = IdProveedor,
            //    Id_Branch = IdSucursal,
            //    IdCliente = IdCliente,
            //    IdTipoDomicilio = 2,
            //    RPU = rpu,
            //    CVE_CP = int.Parse(DDXColoniaFiscalCBas.SelectedValue),
            //    Colonia = DDXColoniaCBas.SelectedValue,
            //    Calle = TxtCalleFiscalCBas.Text,
            //    Num_Ext = TxtExteriorFiscalCBas.Text,
            //    CP = TxtCPFiscalCBas.Text,
            //    Cve_Estado = Convert.ToByte(DDXEstadoFiscalCBas.SelectedValue),
            //    Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipioFiscalCBas.SelectedValue),
            //    Telefono_Local = TxttelefonoFiscalCBas.Text,
            //    Cve_Tipo_Propiedad = int.Parse(DDXTipoPropiedadFiscalCBas.SelectedValue)
            //};

            negocio.DIR_Direcciones.Add(domicilioNegocio);
            //negocio.DIR_Direcciones.Add(domicilioFiscal);

            return cliente;
        }

        public void GuardaCliente()
        {
            var cliente = ObtenCliente();
            var newDatosCliente = SolicitudCreditoAcciones.ActualizaDatosInfoGeneral(cliente);
            if (newDatosCliente)
            {
                InsertaHorarios(1, hlabor1Negocio, DDXInicioLunes,
                    hlabor2Negocio, DDXInicioMartes,
                    hlabor3Negocio, DDXInicioMiercoles,
                    hlabor4Negocio, DDXInicioJueves,
                    hlabor5Negocio, DDXInicioViernes,
                    hlabor6Negocio, DDXInicioSabado,
                    hlabor7Negocio, DDXInicioDomingo,Convert.ToDouble(TxtHorasSemana.Text),Convert.ToDouble(noSemanasNegocio.Text),Convert.ToDouble(TxtHorasAnio.Text)); //NEGOCIO
            }
        }

        protected void BtnGuardaTermporal_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            //var validaDirecciones =
            //        SolicitudCreditoAcciones.ValidaDirecciones(Convert.ToInt32(Session["IdCliente"]));

            //if (!validaDirecciones)
            //{
            ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                !GuardaDatosInfoGeneral()
                    ? "alert('Error al tratar de guardar los datos.');"
                    : "alert('Los datos se guardaron con éxito.');", true);
             
            //}
        }

        public Cliente GetObjetoCliente()
        {
           var getDatosCliente = new Cliente
            {
                IdCliente = Convert.ToInt32(Session["IdCliente"]),
                DireccionesCliente = new List<DIR_Direcciones>()
            };

            getDatosCliente.DatosCliente = new CLI_Cliente
            {
                Id_Proveedor = IdProveedor,
                Id_Branch = IdSucursal,
                IdCliente = getDatosCliente.IdCliente,
                Cve_Tipo_Sociedad = Convert.ToByte(DDXTipoPersona.SelectedValue),
                Nombre = DDXTipoPersona.SelectedValue == "1" ? TxtNombrePFisicaCBas.Text.ToUpper() : null,
                Ap_Paterno = DDXTipoPersona.SelectedValue == "1" ? TxtApellidoPaternoCBas.Text.ToUpper() : null,
                Ap_Materno = DDXTipoPersona.SelectedValue == "1" ? TxtApellidoMaternoCBas.Text.ToUpper() : null,
                Fec_Nacimiento = DDXTipoPersona.SelectedValue == "1" ? Convert.ToDateTime(TxtFechaNacimietoCBas.Text) : Convert.ToDateTime(TxtFechaConstitucionCBas.Text),
                Razon_Social = DDXTipoPersona.SelectedValue == "2" ? TxtRazonSocialCBas.Text.ToUpper() : null,
                Genero =
                    DDXTipoPersona.SelectedValue == "1" ? Convert.ToByte(DDXSexoCBas.SelectedValue) : Convert.ToByte(0),
                RFC = DDXTipoPersona.SelectedValue == "1" ? TxtRFCFisicaCBas.Text.ToUpper() : TxtRFCMoralCBas.Text.ToUpper(),
                CURP = DDXTipoPersona.SelectedValue == "1" ? TxtCURPCBas.Text.ToUpper() : "",
                Cve_Estado_Civil = (byte?) (DDXTipoPersona.SelectedValue == "1" ? Byte.Parse(RBEstadoCivilCBas.SelectedValue) : (int?)null),
                IdRegimenConyugal =
                    RBEstadoCivilCBas.SelectedValue == "2"
                        ? Convert.ToByte(DDXRegimenMatrimonialCBas.SelectedValue)
                        : (byte?)null,
                email = DDXTipoPersona.SelectedValue == "1" ? TxtEmailCBas.Text.ToUpper() : TxtEmailMoralCBas.Text.ToUpper(),
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1,
                CLI_Negocio = new List<CLI_Negocio>
                {
                    new CLI_Negocio
                    {
                        Id_Proveedor = IdProveedor,
                        Id_Branch = IdSucursal,
                        IdCliente = getDatosCliente.IdCliente,
                        IdNegocio = IdNegocio,
                        IdTipoAcreditacion = DDXTipoPersona.SelectedValue == "1" ? Convert.ToByte(DDXOcupacionCBas.SelectedValue) : Convert.ToByte(DDXAcreditaCBas.SelectedValue),
                        IdTipoIdentificacion = DDXTipoPersona.SelectedValue == "1" ? Convert.ToByte(DDXTipoIdentificaFisicaCBas.SelectedValue) : Convert.ToByte(DDXTipoIdentificaMoralCBas.SelectedValue),
                        Numero_Identificacion = DDXTipoPersona.SelectedValue == "1" ? TxtNroIdentificacionCBas.Text.ToUpper() : TxtNumeroIdentMoralCBas.Text.ToUpper(),
                        Cve_Tipo_Industria = Convert.ToInt32(DDXGiroEmpresa.SelectedValue) ,// DE DONDE SE TOMA ESTE VALOR
                        Fecha_Adicion = DateTime.Now,
                        AdicionadoPor = Session["UserName"].ToString(),
                        Estatus = 1,
                    }
                }
            };

            var rpu = Session["ValidRPU"].ToString();
            var direcciones = new DIR_Direcciones
            {
                Id_Proveedor = IdProveedor,
                Id_Branch = IdSucursal,
                IdCliente = getDatosCliente.IdCliente,
                IdNegocio = IdNegocio,
                IdTipoDomicilio = 1, // negocio
                IdDomicilio = 0, //?????DE DONDE SE TOMA
                RPU = rpu,
                CP = TxtCPCBas.Text.ToUpper(),
                Cve_Estado = Convert.ToByte(DDXEstadoCBas.SelectedValue),
                Cve_Deleg_Municipio =Convert.ToInt16(DDXMunicipioCBas.SelectedValue),
                Colonia = DDXColoniaCBas.SelectedItem.Text,
                Calle = TxtCalleCBas.Text.ToUpper(),
                Telefono_Local = TxtTelefonoCBas.Text.ToUpper(),
                Num_Ext = TxtNumeroExteriorCBas.Text.ToUpper(),
                Num_Int = TxtNumeroInteriorCBas.Text.ToUpper() == "" ? null : TxtNumeroInteriorCBas.Text.ToUpper(),
                Referencia_Dom = txtReferenciaDomNegocioMoral.Text.ToUpper() == "" ? null : txtReferenciaDomNegocioMoral.Text.ToUpper(),
                Cve_Tipo_Propiedad = int.Parse(DDXTipoPropiedadCBas.SelectedValue),
                CVE_CP = (DDXColoniaCBas.SelectedIndex == 0 || DDXColoniaCBas.SelectedIndex == -1)
                      ? 0
                      : int.Parse(DDXColoniaCBas.SelectedValue),
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1
            };

            getDatosCliente.DireccionesCliente.Add(direcciones);

            direcciones = new DIR_Direcciones
            {
                Id_Proveedor = IdProveedor,
                Id_Branch = IdSucursal,
                IdCliente = getDatosCliente.IdCliente,
                IdNegocio = IdNegocio,
                IdTipoDomicilio = 2, //Fiscal
                IdDomicilio = 0, // DE DOND SE TOMA???
                RPU = rpu,
                CP = TxtCPFiscalCBas.Text.ToUpper(),
                Cve_Estado =Convert.ToByte(DDXEstadoFiscalCBas.SelectedValue),
                Cve_Deleg_Municipio = Convert.ToInt16(DDXMunicipioFiscalCBas.SelectedValue),
                Colonia = DDXColoniaFiscalCBas.SelectedItem.Text,
                Calle = TxtCalleFiscalCBas.Text.ToUpper(),
                Telefono_Oficina = TxttelefonoFiscalCBas.Text.ToUpper(),
                Num_Ext = TxtExteriorFiscalCBas.Text.ToUpper(),
                Num_Int = TxtInteriorFiscalCBas.Text.ToUpper() == "" ? null : TxtInteriorFiscalCBas.Text.ToUpper(),
                Referencia_Dom = txtReferenciaDomFiscalMoral.Text.ToUpper() == "" ? null : txtReferenciaDomFiscalMoral.Text.ToUpper(),
                Cve_Tipo_Propiedad = (DDXTipoPropiedadFiscalCBas.SelectedIndex == 0 || DDXTipoPropiedadFiscalCBas.SelectedIndex == -1)
                                ? Convert.ToInt16(DDXTipoPropiedadCBas.SelectedValue) : Convert.ToInt16(DDXTipoPropiedadFiscalCBas.SelectedValue),
                CVE_CP = (DDXColoniaFiscalCBas.SelectedIndex == 0 || DDXColoniaFiscalCBas.SelectedIndex == -1)
                      ? 0
                      : int.Parse(DDXColoniaFiscalCBas.SelectedValue),
                Fecha_Adicion = DateTime.Now,
                AdicionadoPor = Session["UserName"].ToString(),
                Estatus = 1
            };

            getDatosCliente.DireccionesCliente.Add(direcciones);

            return getDatosCliente;
        }


        #endregion

        #endregion

        #region CAPTURA COMPLEMENTARIA

        public void CargaDatosCapturaComp()
        { 
            LLenaHorarios();
            LlenarCatalogoEstadosCComp();
            //DesHabilitarHorarios(); 
            LlenaDDxSexoCComp();
            LlenaDdxRepLegal();
            if (DDXTipoPersona.SelectedValue == "2")
            {
                PanelActaMatrimonio.Visible = false;
                ddxRepLegal.SelectedValue = "NO";
                ddxRepLegal.Enabled = false;
            }
            // PanelPersonaFisica.Visible = false; 
            //  PanelPersonaMoral.Visible  = false;


        }

        #region Llenar Catalogos

        private void LlenaDdxRepLegal()
        {
            ddxRepLegal.Items.Clear();
            ddxRepLegal.Items.Insert(0, "Seleccione");
            ddxRepLegal.Items.Insert(1, "SI");
            ddxRepLegal.Items.Insert(2, "NO");
            ddxRepLegal.SelectedIndex = 0;
        }
        protected void LLenaHorarios()
        {
            var listHorarios = CatalogosSolicitud.ObtenHorariosTrabajo();

            DDXInicioLunes.DataSource = listHorarios;
            DDXInicioLunes.DataValueField = "CveValorCatalogo";
            DDXInicioLunes.DataTextField = "DescripcionCatalogo";
            DDXInicioLunes.DataBind();
            DDXInicioLunes.Items.Insert(0, "Seleccione");
            DDXInicioLunes.SelectedIndex = 0;

            DDXInicioMartes.DataSource = listHorarios;
            DDXInicioMartes.DataValueField = "CveValorCatalogo";
            DDXInicioMartes.DataTextField = "DescripcionCatalogo";
            DDXInicioMartes.DataBind();
            DDXInicioMartes.Items.Insert(0, "Seleccione");
            DDXInicioMartes.SelectedIndex = 0;

            DDXInicioMiercoles.DataSource = listHorarios;
            DDXInicioMiercoles.DataValueField = "CveValorCatalogo";
            DDXInicioMiercoles.DataTextField = "DescripcionCatalogo";
            DDXInicioMiercoles.DataBind();
            DDXInicioMiercoles.Items.Insert(0, "Seleccione");
            DDXInicioMiercoles.SelectedIndex = 0;

            DDXInicioJueves.DataSource = listHorarios;
            DDXInicioJueves.DataValueField = "CveValorCatalogo";
            DDXInicioJueves.DataTextField = "DescripcionCatalogo";
            DDXInicioJueves.DataBind();
            DDXInicioJueves.Items.Insert(0, "Seleccione");
            DDXInicioJueves.SelectedIndex = 0;

            DDXInicioViernes.DataSource = listHorarios;
            DDXInicioViernes.DataValueField = "CveValorCatalogo";
            DDXInicioViernes.DataTextField = "DescripcionCatalogo";
            DDXInicioViernes.DataBind();
            DDXInicioViernes.Items.Insert(0,  "Seleccione");
            DDXInicioViernes.SelectedIndex = 0;

            DDXInicioSabado.DataSource = listHorarios;
            DDXInicioSabado.DataValueField = "CveValorCatalogo";
            DDXInicioSabado.DataTextField = "DescripcionCatalogo";
            DDXInicioSabado.DataBind();
            DDXInicioSabado.Items.Insert(0,  "Seleccione");
            DDXInicioSabado.SelectedIndex = 0;

            DDXInicioDomingo.DataSource = listHorarios;
            DDXInicioDomingo.DataValueField = "CveValorCatalogo";
            DDXInicioDomingo.DataTextField = "DescripcionCatalogo";
            DDXInicioDomingo.DataBind();
            DDXInicioDomingo.Items.Insert(0,  "Seleccione");
            DDXInicioDomingo.SelectedIndex = 0;

        }

        protected void DesHabilitarHorarios()
        {
            DDXInicioLunes.Attributes.Add("disabled", "true");

            DDXInicioMartes.Attributes.Add("disabled", "true");

            DDXInicioMiercoles.Attributes.Add("disabled", "true");

            DDXInicioJueves.Attributes.Add("disabled", "true");

            DDXInicioViernes.Attributes.Add("disabled", "true");

            DDXInicioSabado.Attributes.Add("disabled", "true");

            DDXInicioDomingo.Attributes.Add("disabled", "true");
        }

        protected void LlenarCatalogoEstadosCComp()
        {
            var lstEstado = CatalogosSolicitud.ObtenCatEstadosRepublica();
            if (lstEstado == null) return;
            DDXEstadoOS.DataSource = lstEstado;
            DDXEstadoOS.DataValueField = "Cve_Estado";
            DDXEstadoOS.DataTextField = "Dx_Nombre_Estado";
            DDXEstadoOS.DataBind();

            DDXEstadoOS.Items.Insert(0, "Seleccione");
            DDXEstadoOS.SelectedIndex = 0;

            DDXEstadoPN.DataSource = lstEstado;
            DDXEstadoPN.DataValueField = "Cve_Estado";
            DDXEstadoPN.DataTextField = "Dx_Nombre_Estado";
            DDXEstadoPN.DataBind();

            DDXEstadoPN.Items.Insert(0, "Seleccione");
            DDXEstadoPN.SelectedIndex = 0;

            DDXEstadoAC.DataSource = lstEstado;
            DDXEstadoAC.DataValueField = "Cve_Estado";
            DDXEstadoAC.DataTextField = "Dx_Nombre_Estado";
            DDXEstadoAC.DataBind();

            DDXEstadoAC.Items.Insert(0, "Seleccione");
            DDXEstadoAC.SelectedIndex = 0;

            DDXEstadoPNRL.DataSource = lstEstado;
            DDXEstadoPNRL.DataValueField = "Cve_Estado";
            DDXEstadoPNRL.DataTextField = "Dx_Nombre_Estado";
            DDXEstadoPNRL.DataBind();

            DDXEstadoPNRL.Items.Insert(0, "Seleccione");
            DDXEstadoPNRL.SelectedIndex = 0;

            DDXEstadoClienteAC.DataSource = lstEstado;
            DDXEstadoClienteAC.DataValueField = "Cve_Estado";
            DDXEstadoClienteAC.DataTextField = "Dx_Nombre_Estado";
            DDXEstadoClienteAC.DataBind();

            DDXEstadoClienteAC.Items.Insert(0, "Seleccione");
            DDXEstadoClienteAC.SelectedIndex = 0;

            DDXEstadoClienteAC.DataSource = lstEstado;
            DDXEstadoClienteAC.DataValueField = "Cve_Estado";
            DDXEstadoClienteAC.DataTextField = "Dx_Nombre_Estado";
            DDXEstadoClienteAC.DataBind();

            DDXEstadoClienteAC.Items.Insert(0, "Seleccione");
            DDXEstadoClienteAC.SelectedIndex = 0;
        }

        protected void LlenarDDxMunicipoCComp(int idEstado, DropDownList ddxMunicipio)
        {
            try
            {
                ddxMunicipio.Items.Clear();

                var lstMunicipio = CatalogosSolicitud.ObtenDelegMunicipios(idEstado);

                if (lstMunicipio == null) return;
                ddxMunicipio.DataSource = lstMunicipio;
                ddxMunicipio.DataValueField = "Cve_Deleg_Municipio";
                ddxMunicipio.DataTextField = "Dx_Deleg_Municipio";
                ddxMunicipio.DataBind();

                ddxMunicipio.Items.Insert(0, "Seleccione");
                ddxMunicipio.SelectedIndex = 0;
                ddxMunicipio.Focus();
            }
            catch (Exception ex)
            {

            }

        }

        protected void LlenarDDxColoniasCComp(int idEstado, int idMunicipio, DropDownList ddxColonia,
            DropDownList ddxcoloniaHidden)
        {
            try
            {
                ddxColonia.Items.Clear();

                var lstColonia = CatalogosSolicitud.ObtenCatCodigoPostals(idEstado, idMunicipio);

                if (lstColonia == null) return;
                ddxColonia.DataSource = lstColonia;
                ddxColonia.DataValueField = "Cve_CP";
                ddxColonia.DataTextField = "Dx_Colonia";
                ddxColonia.DataBind();

                ddxColonia.Items.Insert(0, "Seleccione");
                ddxColonia.SelectedIndex = 0;

                ddxcoloniaHidden.DataSource = lstColonia;
                ddxcoloniaHidden.DataValueField = "Cve_CP";
                ddxcoloniaHidden.DataTextField = "Codigo_Postal";
                ddxcoloniaHidden.DataBind();

                ddxcoloniaHidden.Items.Insert(0, "Seleccione");
                ddxcoloniaHidden.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }

        }

        protected void LlenaDDxSexoCComp()
        {
            var lstGenero = CatalogosSolicitud.ObtenCatSexos();
            if (lstGenero == null) return;
            DDXSexoCComp.DataSource = lstGenero;
            DDXSexoCComp.DataValueField = "Fg_Sexo";
            DDXSexoCComp.DataTextField = "Dx_Sexo";
            DDXSexoCComp.DataBind();

            DDXSexoCComp.Items.Insert(0, "Seleccione");
            DDXSexoCComp.SelectedIndex = 0;
        }
     
        #endregion

        #region Eventos

        protected void RBListTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBListTipoPersona.SelectedValue == "1")
            {
                PanelPersonaMoralCComp.Visible = false;
                PanelPersonaFisicaCComp.Visible = true;
            }
            if (RBListTipoPersona.SelectedValue == "2")
            {
                PanelPersonaMoralCComp.Visible = true;
                PanelPersonaFisicaCComp.Visible = false;
            }
        }

        protected void DDXEstadoPNRL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipoCComp(int.Parse(DDXEstadoPNRL.SelectedValue), DDXMunicipioPNRL);
        }

        protected void DDXEstadoClienteAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipoCComp(int.Parse(DDXEstadoClienteAC.SelectedValue), DDXMunicipipClienteAC);
        }

        protected void DDXEstadoPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipoCComp(int.Parse(DDXEstadoPN.SelectedValue), DDXMunicipioPN);
        }

        protected void DDXEstadoAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDDxMunicipoCComp(int.Parse(DDXEstadoAC.SelectedValue), DDXMunicipioAC);
        }

        protected void DDXEstadoOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXMunicipioOS.Items.Clear();
            DDXColoniaOS.Items.Clear();
            LlenarDDxMunicipoCComp(int.Parse(DDXEstadoOS.SelectedValue), DDXMunicipioOS);
            TxtCPOS.Text = "";
        }

        protected void DDXMunicipioOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXColoniaOS.Items.Clear();
            LlenarDDxColoniasCComp(int.Parse(DDXEstadoOS.SelectedValue), int.Parse(DDXMunicipioOS.SelectedValue),
                DDXColoniaOS, DDXColoniaOSHidden);
            TxtCPOS.Text = "";
        }

        protected void DDXColoniaOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDXColoniaOSHidden.SelectedValue = DDXColoniaOS.SelectedValue;
            TxtCPOS.Text = DDXColoniaOSHidden.SelectedItem.Text;
        }
        
        protected void TxtCPFiscalCBas_TextChanged(object sender, EventArgs e)
        {
            string sScript;
            if (TxtCPFiscalCBas.Text.Length == 5)
            {
                var lstColonia = CatalogosSolicitud.ObtenColoniasXCp(TxtCPFiscalCBas.Text);

                if (lstColonia.Count > 0)
                {
                    var colonia = lstColonia.FirstOrDefault(me => me.CodigoPostal == TxtCPFiscalCBas.Text);

                    if (colonia != null)
                        DDXEstadoFiscalCBas.SelectedValue = colonia.CveEstado.ToString(CultureInfo.InvariantCulture);
                    if (colonia != null)
                    {
                        //LlenarDDxMunicipoFiscalCBas(colonia.CveEstado);
                        LlenarDDxMunicipo(colonia.CveEstado,DDXMunicipioFiscalCBas,DDXColoniaFiscalCBas);
                        DDXMunicipioFiscalCBas.SelectedValue = colonia.CveDelegMunicipio.ToString();
                    }
                    DDXColoniaFiscalCBas.DataSource = lstColonia;
                    DDXColoniaFiscalCBas.DataValueField = "CveCP";
                    DDXColoniaFiscalCBas.DataTextField = "DxColonia";
                    DDXColoniaFiscalCBas.DataBind();

                    DDXColoniaFiscalCBas.Items.Insert(0, "Seleccione");
                    DDXColoniaFiscalCBas.SelectedIndex = 0;

                    DDXColoniaFiscalHiddenCBas.DataSource = lstColonia;
                    DDXColoniaFiscalHiddenCBas.DataValueField = "CveCP";
                    DDXColoniaFiscalHiddenCBas.DataTextField = "CodigoPostal";
                    DDXColoniaFiscalHiddenCBas.DataBind();
                }
                else
                {
                    DDXEstadoFiscalCBas.SelectedIndex = 0;
                    DDXMunicipioFiscalCBas.Items.Clear();
                    DDXMunicipioFiscalCBas.Items.Insert(0, "Seleccione");
                    DDXMunicipioFiscalCBas.SelectedIndex = 0;
                    DDXColoniaFiscalCBas.Items.Clear();
                    DDXColoniaFiscalCBas.Items.Insert(0, "Seleccione");
                    DDXColoniaFiscalCBas.SelectedIndex = 0;

                    sScript =
                        "<script language='JavaScript'>alert('No se encuentra el Código Postal, contactar al Agente FIDE.');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);

                    TxtCP.Focus();
                }
            }
            else
            {
                sScript =
                    "<script language='JavaScript'>alert('El Código Postal debe ser de 5 dígitos. Verifique');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);

                TxtCP.Focus();
            }
        }

        protected void TxtCPOS_TextChanged(object sender, EventArgs e)
        {
            string sScript;
            if (TxtCPOS.Text.Length == 5)
            {
                var lstColonia = CatalogosSolicitud.ObtenColoniasXCp(TxtCPOS.Text);

                if (lstColonia.Count > 0)
                {
                    var colonia = lstColonia.FirstOrDefault(me => me.CodigoPostal == TxtCPOS.Text);

                    if (colonia != null)
                        DDXEstadoOS.SelectedValue = colonia.CveEstado.ToString(CultureInfo.InvariantCulture);
                    if (colonia != null)
                    {
                        LlenarDDxMunicipoCComp(colonia.CveEstado, DDXMunicipioOS);
                        DDXMunicipioOS.SelectedValue = colonia.CveDelegMunicipio.ToString();
                    }
                    DDXColoniaOS.DataSource = lstColonia;
                    DDXColoniaOS.DataValueField = "CveCP";
                    DDXColoniaOS.DataTextField = "DxColonia";
                    DDXColoniaOS.DataBind();

                    DDXColoniaOS.Items.Insert(0, "Seleccione");
                    DDXColoniaOS.SelectedIndex = 0;

                    DDXColoniaOSHidden.DataSource = lstColonia;
                    DDXColoniaOSHidden.DataValueField = "CveCP";
                    DDXColoniaOSHidden.DataTextField = "CodigoPostal";
                    DDXColoniaOSHidden.DataBind();
                }
                else
                {
                    DDXEstadoOS.SelectedIndex = 0;
                    DDXMunicipioOS.Items.Clear();
                    DDXMunicipioOS.Items.Insert(0, "Seleccione");
                    DDXMunicipioOS.SelectedIndex = 0;
                    DDXColoniaOS.Items.Clear();
                    DDXColoniaOS.Items.Insert(0, "Seleccione");
                    DDXColoniaOS.SelectedIndex = 0;

                    sScript =
                        "<script language='JavaScript'>alert('No se encuentra el Código Postal, contactar al Agente FIDE.');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);


                    TxtCP.Focus();
                }
            }
            else
            {
                sScript =
                    "<script language='JavaScript'>alert('El Código Postal debe ser de 5 dígitos. Verifique');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensaje", sScript, false);

                TxtCP.Focus();
            }
        }


        //protected void ChkRepLegal_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ChkRepLegal.Checked)
        //    {
        //        TxtNombreRepLegal.Text = TxtNombrePFisicaCBas.Text.ToUpper();
        //        TxtApPaternoRepLegal.Text = TxtApellidoPaternoCBas.Text.ToUpper();
        //        TxtApMaternoRepLegal.Text = TxtApellidoMaternoCBas.Text.ToUpper();
        //        TxtEmailRepLegal.Text = TxtEmailCBas.Text.ToUpper();
        //        TxtTelefonoRepLegal.Text = TxttelefonoFiscalCBas.Text.ToUpper();
        //    }
        //    else
        //    {
        //        TxtNombreRepLegal.Text = "";
        //        TxtApPaternoRepLegal.Text = "";
        //        TxtApMaternoRepLegal.Text = "";
        //        TxtEmailRepLegal.Text = "";
        //        TxtTelefonoRepLegal.Text = "";
        //    }
        //}

        protected void BtnGuardarComplementaria_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
            if (ValidarHorarioOperacion(hlabor1Negocio, DDXInicioLunes,
                hlabor2Negocio, DDXInicioMartes,
                hlabor3Negocio, DDXInicioMiercoles,
                hlabor4Negocio, DDXInicioJueves,
                hlabor5Negocio, DDXInicioViernes,
                hlabor6Negocio, DDXInicioSabado,
                hlabor7Negocio, DDXInicioDomingo))

            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                    !GuardaInfoCompCliente()
                        ? "alert('Error al tratar de guardar los datos.');"
                        : "alert('Los datos se guardaron con éxito.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof (Page), "NextError",
                    "alert('Horario de operación incompleto. Verifique.');",
                    true);
            }
        }

        #endregion

       #region Metodos Protegidos


        protected void CargaHorariosOperacion(List<CLI_HORARIOS_OPERACION> lisHorariosOperacio)
        {
            //foreach (var VARIABLE in COLLECTION)
            //{

            //}
        }

        #endregion

        #region Metodos Publicos

        public Cliente ObtenInfoComplementariaCliente()
        {
            var idCliente = Convert.ToInt32(Session["IdCliente"]);

            var cliente = new Cliente
            {
                IdCliente = idCliente,
                DatosCliente = new CLI_Cliente
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdCliente = idCliente,
                    Cve_Estado_Civil = Convert.ToByte(DDXTipoPersona.SelectedValue) == 1 ? Byte.Parse(RBEstadoCivilCBas.SelectedValue) : (byte?)null,
                    CLI_Negocio = new List<CLI_Negocio>{ new CLI_Negocio
                    {
                        Id_Proveedor = IdProveedor,
                        Id_Branch = IdSucursal,
                        IdNegocio = IdNegocio,
                        IdCliente = idCliente,
                    }}
                },
                ReferenciasNotariales = new List<CLI_Referencias_Notariales>(),
                DireccionesCliente = new List<DIR_Direcciones>(),
                //REPRESENTANTE LEGAL
                RepresentanteLegal = new CLI_Ref_Cliente
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    IdCliente = idCliente,
                    IdTipoReferencia = 1,
                    Cve_Tipo_Sociedad = Convert.ToByte(DDXTipoPersona.SelectedIndex),
                    Nombre = TxtNombreRepLegal.Text.ToUpper(),
                    Ap_Paterno = TxtApPaternoRepLegal.Text.ToUpper(),
                    Ap_Materno = TxtApMaternoRepLegal.Text.ToUpper(),
                    email = TxtEmailRepLegal.Text.ToUpper(),
                    Telefono_Local = TxtTelefonoRepLegal.Text,
                    Fecha_Adicion = DateTime.Now,
                    AdicionadoPor = Session["UserName"].ToString(),
                    Estatus = 1
                }
            };

            //OBLIGADO SOLIDARIO PERSONA FISICA
            if (RBListTipoPersona.SelectedValue == "1")
            {
                var domObligadoSolidario = new DIR_Direcciones
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    IdCliente = idCliente,
                    IdTipoDomicilio = 3,
                    CVE_CP = int.Parse(DDXColoniaOS.SelectedValue),
                    Colonia = DDXColoniaOS.SelectedItem.Text,
                    CP = TxtCPOS.Text,
                    Cve_Estado =Convert.ToByte(DDXEstadoOS.SelectedValue),
                    Cve_Deleg_Municipio =Convert.ToInt16(DDXMunicipioOS.SelectedValue),
                    Calle = TxtCalleOS.Text.ToUpper(),
                    Num_Ext = TxtNoExteriorOS.Text,
                    Telefono_Local = TxtTelefonoOS.Text,
                    Fecha_Adicion = DateTime.Now,
                    AdicionadoPor = Session["UserName"].ToString(),
                    Estatus = 1
                };
                cliente.DireccionesCliente.Add(domObligadoSolidario);

                cliente.ObligadoSolidario = new CLI_Ref_Cliente
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    IdCliente = idCliente,
                    IdTipoReferencia = 2,
                    Cve_Tipo_Sociedad = Byte.Parse(RBListTipoPersona.SelectedValue),
                    Nombre = TxtNombrePFOS.Text.ToUpper(),
                    Ap_Paterno = TxtApPaternoOS.Text.ToUpper(),
                    Ap_Materno = TxtApMaternoOS.Text.ToUpper(),
                    Genero = Byte.Parse(DDXSexoCComp.SelectedValue),
                    Fec_Nacimiento = Convert.ToDateTime(TxtFechaNacimietoOS.Text),
                    RFC = TxtRFCOS.Text.ToUpper(),
                    CURP = TxtCURPOS.Text.ToUpper(),
                    Telefono_Local = TxtTelefonoOS.Text,
                    Fecha_Adicion = DateTime.Now,
                    AdicionadoPor = Session["UserName"].ToString(),
                    Estatus = 1
                };
            }

            //OBLIGADO SOLIDARIO PERSONA MORAL
            if (RBListTipoPersona.SelectedValue == "2")
            {
                cliente.ObligadoSolidario = new CLI_Ref_Cliente
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    IdCliente = idCliente,
                    IdTipoReferencia = 2,
                    Razon_Social = TxtRazonSocialOS.Text.ToUpper(),
                    Cve_Tipo_Sociedad = Byte.Parse(RBListTipoPersona.SelectedValue),
                    Fecha_Adicion = DateTime.Now,
                    AdicionadoPor = Session["UserName"].ToString(),
                    Estatus = 1
                };

                //REPRESENTANTE LEGAL DEL OBLIGADO SOLIDARIO
                cliente.RepLegalObligadoSolidario = new CLI_Ref_Cliente
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    IdCliente = idCliente,
                    IdTipoReferencia = 3,
                    Nombre = TxtNombreRepLegalOS.Text.ToUpper(),
                    Ap_Paterno = TxtApPaternoRepLegalOS.Text.ToUpper(),
                    Ap_Materno = TxtApMaternoRepLegalOS.Text.ToUpper(),
                    email = TxtEmailRepLegalOS.Text.ToUpper(),
                    Fecha_Adicion = DateTime.Now,
                    AdicionadoPor = Session["UserName"].ToString(),
                    Estatus = 1,
                    Telefono_Local = TxtTelefonoRLOS.Text
                };

                var poderRepLegalObligadoSolidario = new CLI_Referencias_Notariales
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    IdCliente = idCliente,
                    IdTipoReferencia = 4,
                    Numero_Escritura = TxtNoEscrituraPN.Text,
                    Fecha_Escritura = Convert.ToDateTime(TxtFechaEscrituraPN.Text),
                    Nombre_Notario = TxtNombreNotarioPN.Text.ToUpper(),
                    Estado = Byte.Parse(DDXEstadoPN.SelectedValue),
                    Municipio = short.Parse(DDXMunicipioPN.SelectedValue),
                    Numero_Notaria = TxtNotariaPN.Text
                };
                cliente.ReferenciasNotariales.Add(poderRepLegalObligadoSolidario);

                var actaConsObligadoSolidario = new CLI_Referencias_Notariales
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    IdCliente = idCliente,
                    IdTipoReferencia = 5,
                    Numero_Escritura = TxtNumeroEscrituraAC.Text,
                    Fecha_Escritura = Convert.ToDateTime(TxtFechaAC.Text),
                    Nombre_Notario = TxtNombreNotarioAC.Text.ToUpper(),
                    Estado = Byte.Parse(DDXEstadoAC.SelectedValue),
                    Municipio = short.Parse(DDXMunicipioAC.SelectedValue),
                    Numero_Notaria = TxtNotariaAC.Text
                };
                cliente.ReferenciasNotariales.Add(actaConsObligadoSolidario);
            }


            if (ddxRepLegal.SelectedValue == "NO")
            {
                var poderRepresentanteLegal = new CLI_Referencias_Notariales
                    {
                        Id_Proveedor = IdProveedor,
                        Id_Branch = IdSucursal,
                        IdNegocio = IdNegocio,
                        IdCliente = idCliente,
                        IdTipoReferencia = 6,
                        Numero_Escritura = TxtNumeroEscrituraPNCliente.Text,
                        Fecha_Escritura = Convert.ToDateTime(TxtPNCliente.Text),
                        Nombre_Notario = TxtNomNotarioPNCliente.Text.ToUpper(),
                        Estado = Byte.Parse(DDXEstadoPNRL.SelectedValue),
                        Municipio = short.Parse(DDXMunicipioPNRL.SelectedValue),
                        Numero_Notaria = TxtNotariaPNRL.Text
                    };

                cliente.ReferenciasNotariales.Add(poderRepresentanteLegal);
            }

            if (PanelActaConstitutiva.Visible)
            {
                var actaConstitutiva = new CLI_Referencias_Notariales
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    IdCliente = idCliente,
                    IdTipoReferencia = 7,
                    Numero_Escritura = TxtNoEscrituraClienteAC.Text,
                    Fecha_Escritura = Convert.ToDateTime(TxtFechaClienteAC.Text),
                    Nombre_Notario = TxtNomNotarioClienteAC.Text.ToUpper(),
                    Estado = Byte.Parse(DDXEstadoClienteAC.SelectedValue),
                    Municipio = short.Parse(DDXMunicipipClienteAC.SelectedValue),
                    Numero_Notaria = TxtNotariaClienteAC.Text
                };
                cliente.ReferenciasNotariales.Add(actaConstitutiva);
            }

            if (PanelActaMatrimonio.Visible)
            {
                var actaMatrimonio = new CLI_Referencias_Notariales
                {
                    Id_Proveedor = IdProveedor,
                    Id_Branch = IdSucursal,
                    IdNegocio = IdNegocio,
                    IdCliente = idCliente,
                    IdTipoReferencia = 8,
                    Numero_Escritura = TxtNumeroActaMat.Text,
                    Nombre_Notario = txtNombreConyuge.Text.ToUpper(),
                    Numero_Notaria = TxtRegistroCivil.Text
                };
                cliente.ReferenciasNotariales.Add(actaMatrimonio);
            }

            return cliente;
        }

        public bool GuardaInfoCompCliente()
        {
            var cliente = ObtenInfoComplementariaCliente();
            var datosInfoCompCliente = SolicitudCreditoAcciones.ActualizaInfoComplementaria(cliente,
                Convert.ToInt16(Session["IdUserLogueado"]),
                Convert.ToInt16(Session["IdRolUserLogueado"]), Convert.ToInt16(Session["IdDepartamento"]));
            return datosInfoCompCliente != null && InsertaHorarios(1, hlabor1Negocio, DDXInicioLunes,
                    hlabor2Negocio, DDXInicioMartes,
                    hlabor3Negocio, DDXInicioMiercoles,
                    hlabor4Negocio, DDXInicioJueves,
                    hlabor5Negocio, DDXInicioViernes,
                    hlabor6Negocio, DDXInicioSabado,
                    hlabor7Negocio, DDXInicioDomingo, Convert.ToDouble(TxtHorasSemana.Text), Convert.ToDouble(noSemanasNegocio.Text), Convert.ToDouble(TxtHorasAnio.Text)); //NEGOCIO
            
        }

        protected void CargaDatosComplementariosCliente()
        {
            var cliente = SolicitudCreditoAcciones.ObtenClienteComplejo(IdProveedor, IdSucursal, Convert.ToInt32(Session["IdCliente"]), IdNegocio);

            PanelActaConstitutiva.Visible = false;
            PanelPersonaFisicaCComp.Visible = false;
            PanelPersonaMoralCComp.Visible = false;

            //ddxRepLegal.SelectedValue = cliente.DatosCliente.
            if (cliente.RepresentanteLegal != null)
            {
                TxtNombreRepLegal.Text = cliente.RepresentanteLegal.Nombre;
                TxtApPaternoRepLegal.Text = cliente.RepresentanteLegal.Ap_Paterno;
                TxtApMaternoRepLegal.Text = cliente.RepresentanteLegal.Ap_Materno;
                TxtEmailRepLegal.Text = cliente.RepresentanteLegal.email;
                TxtTelefonoRepLegal.Text = cliente.RepresentanteLegal.Telefono_Local;

                LlenaDdxRepLegal();
                ddxRepLegal.SelectedValue = cliente.RepresentanteLegal.Nombre == cliente.DatosCliente.Nombre ? "SI" : "NO";

                if (ddxRepLegal.SelectedValue == "SI")
                    BloqueaPoderNotarialRepLegal(false);
            }

            if (cliente.DatosCliente.Cve_Tipo_Sociedad == 1)
            {
                if (cliente.ObligadoSolidario != null)
                {
                    RBListTipoPersona.SelectedValue = cliente.ObligadoSolidario.Cve_Tipo_Sociedad.ToString();
                    if (RBListTipoPersona.SelectedValue == "1")
                    {
                        TxtNombrePFOS.Text = cliente.ObligadoSolidario.Nombre;
                        TxtApPaternoOS.Text = cliente.ObligadoSolidario.Ap_Paterno;
                        TxtApMaternoOS.Text = cliente.ObligadoSolidario.Ap_Materno;
                        LlenaDDxSexoCComp();
                        DDXSexoCComp.SelectedValue = cliente.ObligadoSolidario.Genero.ToString();
                        TxtFechaNacimietoOS.Text = cliente.ObligadoSolidario.Fec_Nacimiento.Value.Date == null
                            ? null
                            : cliente.ObligadoSolidario.Fec_Nacimiento.Value.Year.ToString() + "-" +
                              cliente.ObligadoSolidario.Fec_Nacimiento.Value.Month.ToString().PadLeft(2, '0') +
                              "-" + cliente.ObligadoSolidario.Fec_Nacimiento.Value.Day.ToString().PadLeft(2, '0');
                        TxtRFCOS.Text = cliente.ObligadoSolidario.RFC;
                        TxtCURPOS.Text = cliente.ObligadoSolidario.CURP;
                        TxtTelefonoOS.Text = cliente.ObligadoSolidario.Telefono_Local;
                        //}
                        var direccion = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 3);
                        if (direccion != null)
                        {
                            TxtCPOS.Text = direccion.CP;
                            LlenarDdxEstado(DDXEstadoOS, DDXMunicipioOS, DDXColoniaOS);
                            DDXEstadoOS.SelectedValue = direccion.Cve_Estado.ToString();
                            LlenarDDxMunicipo((int) direccion.Cve_Estado, DDXMunicipioOS, DDXColoniaOS);
                            DDXMunicipioOS.SelectedValue = direccion.Cve_Deleg_Municipio.ToString();
                            LlenarDDxColonias((int) direccion.Cve_Estado, (int) direccion.Cve_Deleg_Municipio,
                                DDXColoniaOS,
                                DDXColoniaHidden);
                            DDXColoniaOS.SelectedValue = direccion.CVE_CP.ToString();
                            TxtCalleOS.Text = direccion.Calle;
                            TxtNoExteriorOS.Text = direccion.Num_Ext;
                            TxtTelefonoOS.Text = direccion.Telefono_Local;
                        }
                        PanelPersonaFisicaCComp.Visible = true;
                    }
                    else if (RBListTipoPersona.SelectedValue == "2")
                    {
                        TxtRazonSocialOS.Text = cliente.ObligadoSolidario.Razon_Social;
                        if (cliente.RepLegalObligadoSolidario != null)
                        {
                            TxtNombreRepLegalOS.Text = cliente.RepLegalObligadoSolidario.Nombre;
                            TxtApPaternoRepLegalOS.Text = cliente.RepLegalObligadoSolidario.Ap_Paterno;
                            TxtApMaternoRepLegalOS.Text = cliente.RepLegalObligadoSolidario.Ap_Materno;
                            TxtEmailRepLegalOS.Text = cliente.RepLegalObligadoSolidario.email;
                            TxtTelefonoRLOS.Text = cliente.RepresentanteLegal.Telefono_Local;
                            var poderRepLegalObligadoSolidario =
                                cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 4);

                            if (poderRepLegalObligadoSolidario != null)
                            {
                                var fechaPN = poderRepLegalObligadoSolidario.Fecha_Escritura.Value.Date;

                                TxtNoEscrituraPN.Text = poderRepLegalObligadoSolidario.Numero_Escritura;
                                TxtFechaEscrituraPN.Text = fechaPN.Year.ToString() + "-" +
                                                           fechaPN.Month.ToString().PadLeft(2, '0') +
                                                           "-" + fechaPN.Day.ToString().PadLeft(2, '0');
                                TxtNombreNotarioPN.Text = poderRepLegalObligadoSolidario.Nombre_Notario;
                                LlenarDdxEstado(DDXEstadoPN, DDXMunicipioPN, DDXColoniaOSHidden);
                                DDXEstadoPN.SelectedValue = poderRepLegalObligadoSolidario.Estado.ToString();
                                LlenarDDxMunicipo((int) poderRepLegalObligadoSolidario.Estado, DDXMunicipioPN,
                                    DDXColoniaOSHidden);
                                DDXMunicipioPN.SelectedValue = poderRepLegalObligadoSolidario.Municipio.ToString();
                                TxtNotariaPN.Text = poderRepLegalObligadoSolidario.Numero_Notaria;
                            }

                            var actaConsObligadoSolidario =
                                cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 5);

                            if (actaConsObligadoSolidario != null)
                            {
                                var fechaEscritura = actaConsObligadoSolidario.Fecha_Escritura.Value.Date;

                                TxtNumeroEscrituraAC.Text = actaConsObligadoSolidario.Numero_Escritura;
                                TxtFechaAC.Text = fechaEscritura.Year.ToString() + "-" +
                                                  fechaEscritura.Month.ToString().PadLeft(2, '0') +
                                                  "-" + fechaEscritura.Day.ToString().PadLeft(2, '0');
                                TxtNombreNotarioAC.Text = actaConsObligadoSolidario.Nombre_Notario;
                                LlenarDdxEstado(DDXEstadoAC, DDXMunicipioAC, DDXColoniaOSHidden);
                                DDXEstadoAC.SelectedValue = actaConsObligadoSolidario.Estado.ToString();
                                LlenarDDxMunicipo((int) actaConsObligadoSolidario.Estado, DDXMunicipioAC,
                                    DDXColoniaOSHidden);
                                DDXMunicipioAC.SelectedValue = actaConsObligadoSolidario.Municipio.ToString();
                                TxtNotariaAC.Text = actaConsObligadoSolidario.Numero_Notaria;
                            }
                        }
                        PanelPersonaMoralCComp.Visible = true;
                    }
                }

            }
            if (cliente.DatosCliente.Cve_Tipo_Sociedad == 2)
            {
                ddxRepLegal.SelectedValue = "NO";
                ddxRepLegal.Enabled = false;
                if (cliente.ObligadoSolidario != null)
                {
                    TxtRazonSocialOS.Text = cliente.ObligadoSolidario.Razon_Social;
                    RBListTipoPersona.SelectedValue = cliente.ObligadoSolidario.Cve_Tipo_Sociedad.ToString();
                    if (RBListTipoPersona.SelectedValue == "1")
                    {
                        var fechaNac = cliente.ObligadoSolidario.Fec_Nacimiento.Value.Date;
                        TxtNombrePFOS.Text = cliente.ObligadoSolidario.Nombre;
                        TxtApPaternoOS.Text = cliente.ObligadoSolidario.Ap_Paterno;
                        TxtApMaternoOS.Text = cliente.ObligadoSolidario.Ap_Materno;
                        LlenaDDxSexoCComp();
                        DDXSexoCComp.SelectedValue = cliente.ObligadoSolidario.Genero.ToString();
                        TxtFechaNacimietoOS.Text = fechaNac.Year.ToString() + "-" +
                                                   fechaNac.Month.ToString().PadLeft(2, '0') +
                                                   "-" + fechaNac.Day.ToString().PadLeft(2, '0');
                        TxtRFCOS.Text = cliente.ObligadoSolidario.RFC;
                        TxtCURPOS.Text = cliente.ObligadoSolidario.CURP;
                        TxtTelefonoOS.Text = cliente.ObligadoSolidario.Telefono_Local;
                        var direccion = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 3);
                        if (direccion != null)
                        {
                            TxtCPOS.Text = direccion.CP;
                            LlenarDdxEstado(DDXEstadoOS, DDXMunicipioOS, DDXColoniaOS);
                            DDXEstadoOS.SelectedValue = direccion.Cve_Estado.ToString();
                            LlenarDDxMunicipo((int)direccion.Cve_Estado, DDXMunicipioOS, DDXColoniaOS);
                            DDXMunicipioOS.SelectedValue = direccion.Cve_Deleg_Municipio.ToString();
                            LlenarDDxColonias((int)direccion.Cve_Estado, (int)direccion.Cve_Deleg_Municipio, DDXColoniaOS, DDXColoniaHidden);
                            DDXColoniaOS.SelectedValue = direccion.CVE_CP.ToString();
                            TxtCalleOS.Text = direccion.Calle;
                            TxtNoExteriorOS.Text = direccion.Num_Ext;
                            TxtTelefonoOS.Text = direccion.Telefono_Local;
                        }
                        PanelPersonaFisicaCComp.Visible = true;
                    }
                    else if (RBListTipoPersona.SelectedValue == "2")
                    {
                        if (cliente.RepLegalObligadoSolidario != null)
                        {
                            TxtNombreRepLegalOS.Text = cliente.RepLegalObligadoSolidario.Nombre;
                            TxtApPaternoRepLegalOS.Text = cliente.RepLegalObligadoSolidario.Ap_Paterno;
                            TxtApMaternoRepLegalOS.Text = cliente.RepLegalObligadoSolidario.Ap_Materno;
                            TxtEmailRepLegalOS.Text = cliente.RepLegalObligadoSolidario.email;
                            TxtTelefonoRLOS.Text = cliente.RepresentanteLegal.Telefono_Local;
                            var poderRepLegalObligadoSolidario =
                                cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 4);

                            if (poderRepLegalObligadoSolidario != null)
                            {
                                var fechaPN = poderRepLegalObligadoSolidario.Fecha_Escritura.Value.Date;

                                TxtNoEscrituraPN.Text = poderRepLegalObligadoSolidario.Numero_Escritura;
                                TxtFechaEscrituraPN.Text = fechaPN.Year.ToString() + "-" + fechaPN.Month.ToString().PadLeft(2, '0') +
                                                       "-" + fechaPN.Day.ToString().PadLeft(2, '0');
                                TxtNombreNotarioPN.Text = poderRepLegalObligadoSolidario.Nombre_Notario;
                                LlenarDdxEstado(DDXEstadoPN, DDXMunicipioPN, DDXColoniaOSHidden);
                                DDXEstadoPN.SelectedValue = poderRepLegalObligadoSolidario.Estado.ToString();
                                LlenarDDxMunicipo((int)poderRepLegalObligadoSolidario.Estado, DDXMunicipioPN, DDXColoniaOSHidden);
                                DDXMunicipioPN.SelectedValue = poderRepLegalObligadoSolidario.Municipio.ToString();
                                TxtNotariaPN.Text = poderRepLegalObligadoSolidario.Numero_Notaria;
                            }

                            var actaConsObligadoSolidario =
                                cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 5);

                            if (actaConsObligadoSolidario != null)
                            {
                                var fechaEscritura = actaConsObligadoSolidario.Fecha_Escritura.Value.Date;

                                TxtNumeroEscrituraAC.Text = actaConsObligadoSolidario.Numero_Escritura;
                                TxtFechaAC.Text = fechaEscritura.Year.ToString() + "-" + fechaEscritura.Month.ToString().PadLeft(2, '0') +
                                                       "-" + fechaEscritura.Day.ToString().PadLeft(2, '0');
                                TxtNombreNotarioAC.Text = actaConsObligadoSolidario.Nombre_Notario;
                                LlenarDdxEstado(DDXEstadoAC, DDXMunicipioAC, DDXColoniaOSHidden);
                                DDXEstadoAC.SelectedValue = actaConsObligadoSolidario.Estado.ToString();
                                LlenarDDxMunicipo((int)actaConsObligadoSolidario.Estado, DDXMunicipioAC, DDXColoniaOSHidden);
                                DDXMunicipioAC.SelectedValue = actaConsObligadoSolidario.Municipio.ToString();
                                TxtNotariaAC.Text = actaConsObligadoSolidario.Numero_Notaria;
                            }
                        }
                        PanelPersonaMoralCComp.Visible = true;
                    }
                }

                var actaConstitutiva = cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 7);
                if (actaConstitutiva != null)
                {
                    var fechaActa = actaConstitutiva.Fecha_Escritura.Value.Date;

                    TxtNoEscrituraClienteAC.Text = actaConstitutiva.Numero_Escritura;
                    TxtFechaClienteAC.Text = fechaActa.Year.ToString() + "-" + fechaActa.Month.ToString().PadLeft(2, '0') +
                                               "-" + fechaActa.Day.ToString().PadLeft(2, '0');
                    TxtNomNotarioClienteAC.Text = actaConstitutiva.Nombre_Notario;
                    LlenarDdxEstado(DDXEstadoClienteAC, DDXMunicipipClienteAC, DDXColoniaOSHidden);
                    DDXEstadoClienteAC.SelectedValue = actaConstitutiva.Estado.ToString();
                    LlenarDDxMunicipo((int)actaConstitutiva.Estado, DDXMunicipipClienteAC, DDXColoniaOSHidden);
                    DDXMunicipipClienteAC.SelectedValue = actaConstitutiva.Municipio.ToString();
                    TxtNotariaClienteAC.Text = actaConstitutiva.Numero_Notaria;
                    PanelActaConstitutiva.Visible = true;
                }
            }

            var poderRepresentanteLegal = cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 6);

            if (poderRepresentanteLegal != null)
            {
                var fechaPNCliente = poderRepresentanteLegal.Fecha_Escritura.Value.Date;

                TxtNumeroEscrituraPNCliente.Text = poderRepresentanteLegal.Numero_Escritura;
                TxtPNCliente.Text = fechaPNCliente.Year.ToString() + "-" + fechaPNCliente.Month.ToString().PadLeft(2, '0') +
                                           "-" + fechaPNCliente.Day.ToString().PadLeft(2, '0');
                TxtNomNotarioPNCliente.Text = poderRepresentanteLegal.Nombre_Notario;
                LlenarDdxEstado(DDXEstadoPNRL, DDXMunicipioPNRL, DDXColoniaOSHidden);
                DDXEstadoPNRL.SelectedValue = poderRepresentanteLegal.Estado.ToString();
                LlenarDDxMunicipo((int)poderRepresentanteLegal.Estado, DDXMunicipioPNRL, DDXColoniaOSHidden);
                DDXMunicipioPNRL.SelectedValue = poderRepresentanteLegal.Municipio.ToString();
                TxtNotariaPNRL.Text = poderRepresentanteLegal.Numero_Notaria;
            }

            if (cliente.DatosCliente.Cve_Estado_Civil == 2)
            {
            var actaMatrimonio = cliente.ReferenciasNotariales.FirstOrDefault(me => me.IdTipoReferencia == 8);

            if (actaMatrimonio != null)
            {
                TxtNumeroActaMat.Text = actaMatrimonio.Numero_Escritura;
                txtNombreConyuge.Text = actaMatrimonio.Nombre_Notario;
                TxtRegistroCivil.Text = actaMatrimonio.Numero_Notaria;
            }
            }
            else
            {
                PanelActaMatrimonio.Visible = false;
            }

            #region Horarios

            LLenaHorarios();

            var noCredito = NumeroCredito;

            for (byte a = 0; a <= 7; a++)
            {
                
                var existeHorario = CapturaSolicitud.ObtenHorariosOperacionPorDiaOperacion(noCredito, 1, a);
                if (existeHorario == null) continue;

                switch (a)
                {
                    case 1:
                        DDXInicioLunes.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor1Negocio.Text = existeHorario.Horas_Laborables.ToString();
                        break;
                    case 2:
                        DDXInicioMartes.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor2Negocio.Text = existeHorario.Horas_Laborables.ToString();
                        break;
                    case 3:
                        DDXInicioMiercoles.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor3Negocio.Text = existeHorario.Horas_Laborables.ToString();
                        break;
                    case 4:
                        DDXInicioJueves.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor4Negocio.Text = existeHorario.Horas_Laborables.ToString();
                        break;
                    case 5:
                        DDXInicioViernes.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor5Negocio.Text = existeHorario.Horas_Laborables.ToString();
                        break;
                    case 6:
                        DDXInicioSabado.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor6Negocio.Text = existeHorario.Horas_Laborables.ToString();
                        break;
                    case 7:
                        DDXInicioDomingo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor7Negocio.Text = existeHorario.Horas_Laborables.ToString();
                        break;
                }
            }

            var totHorasOperacion = CapturaSolicitud.ObtenTotalHorariosOperacion_negocio(noCredito);
            if (totHorasOperacion == null) return;
            TxtHorasSemana.Text = totHorasOperacion.HORAS_SEMANA.ToString();
            noSemanasNegocio.Text = totHorasOperacion.SEMANAS_AÑO.ToString();
            TxtHorasAnio.Text = totHorasOperacion.HORAS_AÑO.ToString();

            #endregion
        }

        private string ValidaDatosHorariosNegocio()
        {
            string mensaje = string.Empty;
            //Valida los horarios por dia
            decimal hrsSemanal = TxtHorasSemana.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasSemana.Text);

            if (hrsSemanal < 1) mensaje = "Debe capturar el horario de operación del negocio, de por lo menos un día.";

            //Validar los Horarios totales
            decimal hrsAnioTotal = TxtHorasAnio.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasAnio.Text);

            if (hrsAnioTotal < 1) mensaje += "Debe capturar el número de semanas de operación del negocio.";

            return mensaje;
        }
      
        #endregion

        #endregion

        #region BAJA EQUIPOS CAPTURA COMPLEMENTARIA
        //¡¡
        #region Llenar Catalogos

        protected void LLenaHorariosEquiposBaja()
        {
            var listHorarios = CatalogosSolicitud.ObtenHorariosTrabajo();

            DDXInicioLunesBajaEquipo.DataSource = listHorarios;
            DDXInicioLunesBajaEquipo.DataValueField = "CveValorCatalogo";
            DDXInicioLunesBajaEquipo.DataTextField = "DescripcionCatalogo";
            DDXInicioLunesBajaEquipo.DataBind();
            DDXInicioLunesBajaEquipo.Items.Insert(0, "Seleccione");
            DDXInicioLunesBajaEquipo.SelectedIndex = 0;

            DDXInicioMartesBajaEquipo.DataSource = listHorarios;
            DDXInicioMartesBajaEquipo.DataValueField = "CveValorCatalogo";
            DDXInicioMartesBajaEquipo.DataTextField = "DescripcionCatalogo";
            DDXInicioMartesBajaEquipo.DataBind();
            DDXInicioMartesBajaEquipo.Items.Insert(0, "Seleccione");
            DDXInicioMartesBajaEquipo.SelectedIndex = 0;

            DDXInicioMiercolesBajaEquipo.DataSource = listHorarios;
            DDXInicioMiercolesBajaEquipo.DataValueField = "CveValorCatalogo";
            DDXInicioMiercolesBajaEquipo.DataTextField = "DescripcionCatalogo";
            DDXInicioMiercolesBajaEquipo.DataBind();
            DDXInicioMiercolesBajaEquipo.Items.Insert(0, "Seleccione");
            DDXInicioMiercolesBajaEquipo.SelectedIndex = 0;

            DDXInicioJuevesBajaEquipo.DataSource = listHorarios;
            DDXInicioJuevesBajaEquipo.DataValueField = "CveValorCatalogo";
            DDXInicioJuevesBajaEquipo.DataTextField = "DescripcionCatalogo";
            DDXInicioJuevesBajaEquipo.DataBind();
            DDXInicioJuevesBajaEquipo.Items.Insert(0, "Seleccione");
            DDXInicioJuevesBajaEquipo.SelectedIndex = 0;

            DDXInicioViernesBajaEquipo.DataSource = listHorarios;
            DDXInicioViernesBajaEquipo.DataValueField = "CveValorCatalogo";
            DDXInicioViernesBajaEquipo.DataTextField = "DescripcionCatalogo";
            DDXInicioViernesBajaEquipo.DataBind();
            DDXInicioViernesBajaEquipo.Items.Insert(0, "Seleccione");
            DDXInicioViernesBajaEquipo.SelectedIndex = 0;

            DDXInicioSabadoBajaEquipo.DataSource = listHorarios;
            DDXInicioSabadoBajaEquipo.DataValueField = "CveValorCatalogo";
            DDXInicioSabadoBajaEquipo.DataTextField = "DescripcionCatalogo";
            DDXInicioSabadoBajaEquipo.DataBind();
            DDXInicioSabadoBajaEquipo.Items.Insert(0, "Seleccione");
            DDXInicioSabadoBajaEquipo.SelectedIndex = 0;

            DDXInicioDomingoBajaEquipo.DataSource = listHorarios;
            DDXInicioDomingoBajaEquipo.DataValueField = "CveValorCatalogo";
            DDXInicioDomingoBajaEquipo.DataTextField = "DescripcionCatalogo";
            DDXInicioDomingoBajaEquipo.DataBind();
            DDXInicioDomingoBajaEquipo.Items.Insert(0, "Seleccione");
            DDXInicioDomingoBajaEquipo.SelectedIndex = 0;

            hlabor1BajaEquipo.Value = null;
            hlabor2BajaEquipo.Value = null;
            hlabor3BajaEquipo.Value = null;
            hlabor4BajaEquipo.Value = null;
            hlabor5BajaEquipo.Value = null;
            hlabor6BajaEquipo.Value = null;
            hlabor7BajaEquipo.Value = null;

        }

        #endregion

        protected void BtnImgEditar_Click(object sender, ImageClickEventArgs e)
        {
            TxtHorasAnioBajaEquipo.Text = @"0";
            TxtHorasSemanaBajaEquipo.Text = @"0";

            var gridViewRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            hiddenRowIndexEquiboBaja.Value = gridViewRow.RowIndex.ToString(CultureInfo.InvariantCulture);
            var dataKey = grdEquiposBaja.DataKeys[gridViewRow.RowIndex];

            txtColor.Focus();

            if (dataKey == null) return;

            hidIdCreditoSustitucion.Value = dataKey[0].ToString();

            datosComplementarios.CssClass = "PanelVisible";
            IdCredSustitucion = Convert.ToInt32(dataKey[0].ToString());
            var tecnologiaSel = grdEquiposBaja.Rows[gridViewRow.RowIndex].Cells[4].Text;
            var producto = grdEquiposBaja.Rows[gridViewRow.RowIndex].Cells[5].Text;
            Grupo = grdEquiposBaja.Rows[gridViewRow.RowIndex].Cells[6].Text;
            HidIdTecnologia.Value = grdEquiposBaja.Rows[gridViewRow.RowIndex].Cells[10].Text;

            lblInformacionEB.Text = @"Tecnología: " + Convert.ToString(tecnologiaSel) + @"  Producto: " +
                                    Convert.ToString(producto);
            var tecnologia = Tecnologia.Obtiene_Tecnologia(tecnologiaSel);
            hidCveTecnologia.Value = tecnologia.Dx_Cve_CC;

            var ofoto = new CRE_FOTOS
            {
                No_Credito = NumeroCredito,
                IdCreditoSustitucion = int.Parse(hidIdCreditoSustitucion.Value),
                idTipoFoto = 2
            };

            imgOkEquipoViejo.Visible = InfoCompAltaBajaEquipos.ImagenAsignadaCreditoSustitucion(ofoto);

            LLenaHorariosEquiposBaja();
            LimpiaDatosComplemetarios();


            var existe = SolicitudCreditoAcciones.ObtenerComplementarioSustitucion(new K_CREDITO_SUSTITUCION
            {
                No_Credito = NumeroCredito,
                Id_Credito_Sustitucion =int.Parse(hidIdCreditoSustitucion.Value)
            });

            if (existe == null) return;

            txtMarca.Text = existe.Dx_Marca;
            txtColor.Text = existe.Dx_Color;
            txtModelo.Text = existe.Dx_Marca == null && existe.Dx_Color == null ? string.Empty : existe.Dx_Modelo_Producto;
            txtAntiguedad.Value = existe.Dx_Antiguedad == null ? (double?) null : int.Parse(existe.Dx_Antiguedad);

            // Si ya esta capturado se selecciona y se bloquea para que no se pueda modificar
            if (existe.Id_Centro_Disp != null)
            {
                var tipoCentro = existe.Fg_Tipo_Centro_Disp.Equals("M") ? "-(Matriz)" : "-(Sucursal)";
                drpCAyD.SelectedValue = existe.Id_Centro_Disp + tipoCentro;
            }

            for (byte a = 0; a <= 7; a++)
            {
                var existeHorario =
                    CapturaSolicitud.ObtenHorariosOperacionPorDiaOperacion_IdCredSust(NumeroCredito, 3, a,
                        IdCredSustitucion);
                if (existeHorario == null) continue;

                switch (a)
                {
                    case 1:
                        DDXInicioLunesBajaEquipo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor1BajaEquipo.Value = (double?) existeHorario.Horas_Laborables;
                        break;
                    case 2:
                        DDXInicioMartesBajaEquipo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor2BajaEquipo.Value = (double?) existeHorario.Horas_Laborables;
                        break;
                    case 3:
                        DDXInicioMiercolesBajaEquipo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor3BajaEquipo.Value = (double?) existeHorario.Horas_Laborables;
                        break;
                    case 4:
                        DDXInicioJuevesBajaEquipo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor4BajaEquipo.Value = (double?) existeHorario.Horas_Laborables;
                        break;
                    case 5:
                        DDXInicioViernesBajaEquipo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor5BajaEquipo.Value = (double?) existeHorario.Horas_Laborables;
                        break;
                    case 6:
                        DDXInicioSabadoBajaEquipo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor6BajaEquipo.Value = (double?) existeHorario.Horas_Laborables;
                        break;
                    case 7:
                        DDXInicioDomingoBajaEquipo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor7BajaEquipo.Value = (double?) existeHorario.Horas_Laborables;
                        break;
                }
            }

            var totHorasOperacion = CapturaSolicitud.ObtenTotalHorariosOperacion_idCredSust(NumeroCredito,
                IdCredSustitucion);
            if (totHorasOperacion == null) return;
            TxtHorasAnioBajaEquipo.Text = totHorasOperacion.HORAS_AÑO.ToString();
            TxtHorasSemanaBajaEquipo.Text = totHorasOperacion.HORAS_SEMANA.ToString();

            TxtHorasAnioBajaEquipo.Focus();
        }

        private void LlenaCayD(string tecnologia)
        {
            var userModel = (US_USUARIOModel)Session["UserInfo"];
            var disposalCenterDatatable = InfoCompAltaBajaEquipos.Get_CAT_CENTRO_DISPByTECHNOLOGY(2, tecnologia, userModel.Id_Usuario);
            if (disposalCenterDatatable == null) return;
            drpCAyD.DataSource = disposalCenterDatatable;
            drpCAyD.DataTextField = "Dx_Nombre_Comercial";
            drpCAyD.DataValueField = "Id_Centro_Disp";
            drpCAyD.DataBind();
            drpCAyD.Items.Insert(0, "Seleccione");
        }

        protected void txtAntiguedad_TextChanged(object sender, EventArgs e)
        {
            if (txtAntiguedad.Value != null)
            {
                if (txtAntiguedad.Value < 10)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                          string.Format("alert('{0}');", "La antigüedad minima es de 10 años."), true);
                    txtAntiguedad.Value = null;
                }
            }
        }

        protected void CargaDatosBajaEquiposCComp()
        {
            txtRazonSocialEquiposBaja.Text = SolicitudCreditoAcciones.ObtenNombreComercial(NumeroCredito);
            datosComplementarios.CssClass = "PanelNoVisible";
            lblInformacionEB.Text = @"Tecnología";
            txtCreditoNumEquiposBaja.Text = NumeroCredito;
            LstDatosEquiposBajas = InfoCompAltaBajaEquipos.Get_Info_Equipos_Baja(NumeroCredito);
            grdEquiposBaja.DataSource = LstDatosEquiposBajas;
            grdEquiposBaja.DataBind();

            HidRequiereCAyD.Value = null;

            //Si existe algun producto con tecnologia de susutitucion se pide el CAyD
            var requiereCayd = SolicitudCreditoAcciones.CreditoRequiereCayD(NumeroCredito);
            if (requiereCayd != null)
            {
                LlenaCayD(requiereCayd.Dx_Nombre_General);
                HidRequiereCAyD.Value = "Ok";
                var CAyDAsignado = SolicitudCreditoAcciones.ObtenerCayDPorCredito(NumeroCredito);
                if (CAyDAsignado != null)
                {
                    drpCAyD.SelectedValue = string.Format("{0}-({1})", CAyDAsignado.Id_Centro_Disp,
                        CAyDAsignado.Fg_Tipo_Centro_Disp.Equals("M") ? "Matriz" : "Sucursal");
                }
            }
            else
            {
                lblCAyD.Visible = false;
                drpCAyD.Visible = false;
            }

            datosComplementarios.Enabled = !new OpEquiposAbEficiencia().EsReasignacion(NumeroCredito);
            drpCAyD.Enabled = datosComplementarios.Enabled;
            UploadedBajaEquipoViejo.Enabled = datosComplementarios.Enabled;
            Button3.Enabled = datosComplementarios.Enabled;
        }

        protected void btnGuardarDatosCompEquipoBaja_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid) return;
                string mensaje = ValidaDatosEquiposDeBaja();
                if (!mensaje.Equals(string.Empty))
                {
                    ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                           string.Format("alert('{0}');", mensaje), true);
                    return;
                }

                //Guarda los datos en las tablas correspondientes
                if (InsertaDatosCreditoSustitucion())
                {
                    var ckbSelect =
                        grdEquiposBaja.Rows[Convert.ToInt32(hiddenRowIndexEquiboBaja.Value)].FindControl("ckbSelect") as
                            CheckBox;
                    if (ckbSelect != null && !ckbSelect.Checked)
                    {
                        ckbSelect.Checked = true;
                        grdEquiposBaja.Rows[Convert.ToInt32(hiddenRowIndexEquiboBaja.Value)].BackColor = Color.Chartreuse;
                    }

                    LimpiaDatosComplemetarios();
                    datosComplementarios.CssClass = "PanelNoVisible";
                }
           
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                           string.Format("alert('{0}');", ex.Message), true);
            }
        }

        private void LimpiaDatosComplemetarios()
        {
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtColor.Text = "";
            txtAntiguedad.Value = null;
            LimpiarHorarioOperacion(
                hlabor1BajaEquipo, DDXInicioLunesBajaEquipo,
                hlabor2BajaEquipo, DDXInicioMartesBajaEquipo,
                hlabor3BajaEquipo, DDXInicioMiercolesBajaEquipo,
                hlabor4BajaEquipo, DDXInicioJuevesBajaEquipo,
                hlabor5BajaEquipo, DDXInicioViernesBajaEquipo,
                hlabor6BajaEquipo, DDXInicioSabadoBajaEquipo,
                hlabor7BajaEquipo, DDXInicioDomingoBajaEquipo);
        }

        protected void LimpiarHorarioOperacion(
               RadNumericTextBox hlabor1, DropDownList ddxLunes,
               RadNumericTextBox hlabor2, DropDownList ddxMartes,
               RadNumericTextBox hlabor3, DropDownList ddxMiercoles,
               RadNumericTextBox hlabor4, DropDownList ddxJueves,
               RadNumericTextBox hlabor5, DropDownList ddxViernes,
               RadNumericTextBox hlabor6, DropDownList ddxSabado,
               RadNumericTextBox hlabor7, DropDownList ddxDomingo
               )
        {
            
                ddxLunes.BackColor = Color.White;
                ddxMartes.BackColor = Color.White;
                ddxMiercoles.BackColor = Color.White;
                ddxJueves.BackColor = Color.White;
                ddxViernes.BackColor = Color.White;
                ddxSabado.BackColor = Color.White;
                ddxDomingo.BackColor = Color.White;
                hlabor1.BackColor = Color.White;
                hlabor2.BackColor = Color.White;
                hlabor3.BackColor = Color.White;
                hlabor4.BackColor = Color.White;
                hlabor5.BackColor = Color.White;
                hlabor6.BackColor = Color.White;
                hlabor7.BackColor = Color.White;
           
        }

        private bool InsertaDatosCreditoSustitucion()
        {
            var check = false;

            if (!ValidarHorarioOperacion(
                hlabor1BajaEquipo, DDXInicioLunesBajaEquipo,
                hlabor2BajaEquipo, DDXInicioMartesBajaEquipo,
                hlabor3BajaEquipo, DDXInicioMiercolesBajaEquipo,
                hlabor4BajaEquipo, DDXInicioJuevesBajaEquipo,
                hlabor5BajaEquipo, DDXInicioViernesBajaEquipo,
                hlabor6BajaEquipo, DDXInicioSabadoBajaEquipo,
                hlabor7BajaEquipo, DDXInicioDomingoBajaEquipo)) return false;
            
            

            if (!ActualizarCreditoSustitucion()) return false;
            if (InsertaHorariosSustitucion(3, hlabor1BajaEquipo, DDXInicioLunesBajaEquipo,
                hlabor2BajaEquipo, DDXInicioMartesBajaEquipo,
                hlabor3BajaEquipo, DDXInicioMiercolesBajaEquipo,
                hlabor4BajaEquipo, DDXInicioJuevesBajaEquipo,
                hlabor5BajaEquipo, DDXInicioViernesBajaEquipo,
                hlabor6BajaEquipo, DDXInicioSabadoBajaEquipo,
                hlabor7BajaEquipo, DDXInicioDomingoBajaEquipo,
                TxtHorasSemanaBajaEquipo.Text.Equals(string.Empty) ? 0 : double.Parse(TxtHorasSemanaBajaEquipo.Text),
                noSemanasBajaEquipo.Value.ToString().Equals(string.Empty) ? 0 : double.Parse(noSemanasBajaEquipo.Value.ToString()),
                TxtHorasAnioBajaEquipo.Text.Equals(string.Empty) ? 0 : double.Parse(TxtHorasAnioBajaEquipo.Text)
                ))
                check = true;

            return check;
        }

        public bool ActualizarCreditoSustitucion()
        {
            int idTecnologia = HidIdTecnologia.Value != string.Empty ? int.Parse(HidIdTecnologia.Value) : 0;
            int consecutivo = SolicitudCreditoAcciones.ObtenerConsecutivoPreFolio(NumeroCredito, idTecnologia);
            var oCreditoSustitucion = new K_CREDITO_SUSTITUCION();
            oCreditoSustitucion.Id_Credito_Sustitucion = int.Parse(hidIdCreditoSustitucion.Value);
            oCreditoSustitucion.No_Credito = NumeroCredito;
            oCreditoSustitucion.Dx_Marca = txtMarca.Text.ToUpper();
            oCreditoSustitucion.Dx_Color = txtColor.Text.ToUpper();
            oCreditoSustitucion.Dx_Modelo_Producto = txtModelo.Text.ToUpper();
            oCreditoSustitucion.Dx_Antiguedad  = txtAntiguedad.Value.ToString();
            oCreditoSustitucion.Id_Pre_Folio = String.Format("{0}-{1}-{2}", NumeroCredito, consecutivo,hidCveTecnologia.Value);
            return SolicitudCreditoAcciones.ActualizarCreditoSustitucion(oCreditoSustitucion);

        }

        public void ActualizaCAyD()
        {
            if (drpCAyD.SelectedIndex != -1 && drpCAyD.SelectedIndex != 0)
            {
                string[] CentroDispo = drpCAyD.SelectedValue.Split('-');

                int Id_Centro_Disp = CentroDispo.Length > 0 ? int.Parse(CentroDispo[0]) : 0;
                string Tipo_Centro_Disp = string.Empty;
                switch (CentroDispo[1].Trim())
                {
                    case "(Matriz)":
                        Tipo_Centro_Disp = "M";
                        break;
                    case "(Sucursal)":
                        Tipo_Centro_Disp = "B";
                        break;
                }
                SolicitudCreditoAcciones.ActualizarCreditoSustitucionCayD(Id_Centro_Disp, Tipo_Centro_Disp,
                    NumeroCredito);
            }
        }
        

        protected bool InsertaHorariosSustitucion(byte tipoHorario,
                RadNumericTextBox hlabor1, DropDownList ddxLunes,
                RadNumericTextBox hlabor2, DropDownList ddxMartes,
                RadNumericTextBox hlabor3, DropDownList ddxMiercoles,
                RadNumericTextBox hlabor4, DropDownList ddxJueves,
                RadNumericTextBox hlabor5, DropDownList ddxViernes,
                RadNumericTextBox hlabor6, DropDownList ddxSabado,
                RadNumericTextBox hlabor7, DropDownList ddxDomingo,
                double horasSemana,
                double totSemanas,
                double horasAño
                )
        {
            var band = false;
            var lstHorarios = new List<CLI_HORARIOS_OPERACION>();

            lstHorarios.Clear();
            CLI_HORARIOS_OPERACION horario;

            if (hlabor1.Value > 0 &&
                ddxLunes.SelectedItem.Text != @"Seleccione")     //Lunes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 1,
                    Hora_Inicio = ddxLunes.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor1.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                    };

                lstHorarios.Add(horario);
            }

            if (hlabor2.Value > 0 &&
                ddxMartes.SelectedItem.Text != @"Seleccione")     //Martes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 2,
                    Hora_Inicio = ddxMartes.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor2.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor3.Value > 0 &&
                ddxMiercoles.SelectedItem.Text != @"Seleccione")     //Miercoles
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 3,
                    Hora_Inicio = ddxMiercoles.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor3.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor4.Value > 0 &&
                ddxJueves.SelectedItem.Text != @"Seleccione")     //Jueves
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 4,
                    Hora_Inicio = ddxJueves.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor4.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor5.Value > 0 &&
                ddxViernes.SelectedItem.Text != @"Seleccione")     //Viernes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 5,
                    Hora_Inicio = ddxViernes.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor5.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor6.Value > 0 &&
                ddxSabado.SelectedItem.Text != @"Seleccione")     //Sabado
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 6,
                    Hora_Inicio = ddxSabado.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor6.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor7.Value > 0 &&
                ddxDomingo.SelectedItem.Text != @"Seleccione")     //Domingo
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 7,
                    Hora_Inicio = ddxDomingo.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor7.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            var hOperTotal = new H_OPERACION_TOTAL
            {
                No_Credito = NumeroCredito,
                IDTIPOHORARIO = tipoHorario,
                HORAS_SEMANA = horasSemana,
                SEMANAS_AÑO = totSemanas,
                HORAS_AÑO = horasAño,
                Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null,
            };

            if (tipoHorario == 3)
            {
                if (SolicitudCreditoAcciones.ActualizaHorarioOperacion_IdCredSust(lstHorarios, hOperTotal))
                    band = true;
            }

            if (tipoHorario == 2)
            {
                if (SolicitudCreditoAcciones.ActualizaHorarioOperacion_IdCredProducto(lstHorarios, hOperTotal))
                    band = true;
            }
            if (tipoHorario == 1)
            {
                if (SolicitudCreditoAcciones.ActualizaHorarioOperacion(lstHorarios, hOperTotal))
                    band = true;
            }

            return band;
        }


        private string ValidaDatosEquiposDeBaja()
        {
            string mensaje = string.Empty;
            //Valida los horarios por dia
            decimal hrsSemanal = TxtHorasSemanaBajaEquipo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasSemanaBajaEquipo.Text);

            if (hrsSemanal < 1) mensaje = "Debe capturar el horario de operación, de por lo menos un día.";

            //Validar los Horarios totales
            decimal hrsAnioTotal = noSemanasBajaEquipo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(noSemanasBajaEquipo.Text);

            if (hrsAnioTotal < 1) mensaje += "Debe capturar el numero de semanas del equipo viejo.";

            //la fotografia del equipo haya sido cargada
            if (!InfoCompAltaBajaEquipos.ImagenAsignadaCreditoSustitucion(new CRE_FOTOS()
            {
                No_Credito = NumeroCredito,
                idTipoFoto = 2,
                IdCreditoSustitucion = int.Parse(hidIdCreditoSustitucion.Value)
            }))
                mensaje += "Debe cargar la foto del equipo.";
            return mensaje;
        }

        protected bool ValidarHorarioOperacion(
               RadNumericTextBox hlabor1, DropDownList ddxLunes,
               RadNumericTextBox hlabor2, DropDownList ddxMartes,
               RadNumericTextBox hlabor3, DropDownList ddxMiercoles,
               RadNumericTextBox hlabor4, DropDownList ddxJueves,
               RadNumericTextBox hlabor5, DropDownList ddxViernes,
               RadNumericTextBox hlabor6, DropDownList ddxSabado,
               RadNumericTextBox hlabor7, DropDownList ddxDomingo
               )
           {
            var band = true;

            LimpiarHorarioOperacion(hlabor1,ddxLunes,
                hlabor2, ddxMartes,
                hlabor3, ddxMiercoles,
                hlabor4, ddxJueves,
                hlabor5, ddxViernes,
                hlabor6, ddxSabado,
                hlabor7, ddxDomingo);
           
            if (hlabor1.Value > 0 && ddxLunes.SelectedItem.Text == @"Seleccione")     //Lunes
            {
                ddxLunes.BackColor = Color.Red;
                band = false;
            }
            if (hlabor2.Value > 0 && ddxMartes.SelectedItem.Text == @"Seleccione")     
            {
                ddxMartes.BackColor = Color.Red;
                band = false;
            }
            if (hlabor3.Value > 0 && ddxMiercoles.SelectedItem.Text == @"Seleccione")    
            {
                ddxMiercoles.BackColor = Color.Red;
                band = false;
            }
            if (hlabor4.Value > 0 && ddxJueves.SelectedItem.Text == @"Seleccione")     
            {
                ddxJueves.BackColor = Color.Red;
                band = false;
            }
            if (hlabor5.Value > 0 && ddxViernes.SelectedItem.Text == @"Seleccione")     
            {
                ddxViernes.BackColor = Color.Red;
                band = false;
            }
            if (hlabor6.Value > 0 && ddxSabado.SelectedItem.Text == @"Seleccione")     
            {
                ddxSabado.BackColor = Color.Red;
                band = false;
            }
            if (hlabor7.Value > 0 && ddxDomingo.SelectedItem.Text == @"Seleccione")    
            {
                ddxDomingo.BackColor = Color.Red;
                band = false;
            }

            if (ddxLunes.SelectedItem.Text != @"Seleccione" && hlabor1.Value == null)    
            {
                hlabor1.BackColor = Color.Red;
                band = false;
            }
            if (ddxMartes.SelectedItem.Text != @"Seleccione" && hlabor2.Value == null)
            {
                hlabor2.BackColor = Color.Red;
                band = false;
            }
            if (ddxMiercoles.SelectedItem.Text != @"Seleccione" && hlabor3.Value == null)
            {
                hlabor3.BackColor = Color.Red;
                band = false;
            }
            if (ddxJueves.SelectedItem.Text != @"Seleccione" && hlabor4.Value == null)
            {
                hlabor4.BackColor = Color.Red;
                band = false;
            }
            if (ddxViernes.SelectedItem.Text != @"Seleccione" && hlabor5.Value == null)
            {
                hlabor5.BackColor = Color.Red;
                band = false;
            }
            if (ddxSabado.SelectedItem.Text != @"Seleccione" && hlabor6.Value == null)
            {
                hlabor6.BackColor = Color.Red;
                band = false;
            }
            if (ddxDomingo.SelectedItem.Text != @"Seleccione" && hlabor7.Value == null)
            {
                hlabor7.BackColor = Color.Red;
                band = false;
            }
            
            return band;
        }

        #region Carga de Fotografias

        public bool FormatosValidos(string formatoImagen)
        {
            var formatos = new List<string> {".emf", ".wmf", ".jpg", ".jpeg", ".jpe", ".png", ".bmp", ".tif"};
            return formatos.Contains(formatoImagen.ToLower());

        }

        protected void UploadedBajaEquipoViejo_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                if (e.File.FileName != null)
                {
                    if (FormatosValidos(e.File.GetExtension()))
                    {
                        var oFoto = new CRE_FOTOS();
                        oFoto.Nombre = e.File.GetName();
                        oFoto.Extension = e.File.GetExtension();
                        oFoto.Longitud = e.File.ContentLength;

                        byte[] b = new byte[e.File.ContentLength];
                        e.File.InputStream.Read(b, 0, e.File.ContentLength);

                        oFoto.No_Credito = NumeroCredito;
                        oFoto.idTipoFoto = 2; // Equipo Viejo
                        oFoto.Foto = b;
                        oFoto.Estatus = true;
                        oFoto.FechaAdicion = DateTime.Now;
                        oFoto.AdicionadoPor = Session["UserName"].ToString();
                        oFoto.IdCreditoSustitucion = int.Parse(hidIdCreditoSustitucion.Value);
                        if (InfoCompAltaBajaEquipos.GuardarImagenCreditoSustitucion(oFoto))
                            imgOkEquipoViejo.ImageUrl = "~/CentralModule/images/icono_correcto.png";
                        else
                            imgOkEquipoViejo.ImageUrl = "~/CentralModule/images/eliminar-icono.png";

                        ClearContents(sender as Control);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                          string.Format("alert('{0}');", "El Formato de Imagen no es válido. (Intente con emf, wmf, jpg, jpeg, jpe, png, bmp, tif)"), true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                              string.Format("alert('{0}');",
                                                  "No se encontro archivo, intentelo de nuevo."),
                                              true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError",
                                          string.Format("alert('{0}');", ex.Message), true);
            }

        }

        private void ClearContents(Control control)
        {
            for (var i = 0; i < Session.Keys.Count; i++)
            {
                if (Session.Keys[i].Contains(control.ClientID))
                {
                    Session.Remove(Session.Keys[i]);
                    break;
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            var status = HidstateLoad.Value;
            imgOkEquipoViejo.ImageUrl = string.Format("~/CentralModule/images/{0}", status.Equals("ok") ? "icono_correcto.png" : "eliminar-icono.png");
            imgOkEquipoViejo.Visible = true;
        }

        protected void verEquipoViejo_Click(object sender, ImageClickEventArgs e)
        {
            string url = string.Format("window.open('VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                                        NumeroCredito, hidIdCreditoSustitucion.Value, "2",null);

            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);
        }

        protected void grdEquiposBaja_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int idTecnologia = int.Parse(e.Row.Cells[10].Text);
                int idCreditoSustitucion = int.Parse(e.Row.Cells[1].Text);                
                if (InfoCompAltaBajaEquipos.CargaCompletaCreditoSustitucion(NumeroCredito, idTecnologia, idCreditoSustitucion))
                {
                    e.Row.BackColor = Color.Chartreuse;
                    var ckbSelect = e.Row.FindControl("ckbSelect") as CheckBox;
                    if (ckbSelect != null) ckbSelect.Checked = true;
                }

            }
        }
        #endregion

        #endregion

        #region ALTA BAJA DE EQUIPOS
       
        #region Inicio
        private void IniciaCapturaCompAltaEquipos()
        {
            //NumeroCredito = "PAEEEMDB03A08236";
            // PAEEEMDB03A08236
            // Adquisicion "PAEEEMDB01A04682"; 
            // Sustitucion  "PAEEEMDX12J00120";
            txtCreditoNumAltaEquipos.Text = NumeroCredito;
            txtRazonSocialAltaEquipos.Text = SolicitudCreditoAcciones.ObtenNombreComercial(NumeroCredito);
            var dt = InfoCompAltaBajaEquipos.Get_Info_Equipos_Alta(NumeroCredito);
            var lstEquipos = InfoCompAltaBajaEquipos.Get_Info_Equipos_Alta_Por_Cantidad(dt);
            grdEquiposAlta.DataSource = lstEquipos;
            grdEquiposAlta.DataBind();
            datosComplementariosAlta.CssClass = "PanelNoVisible";
            imgOKLoaderFachada.Visible = ValidaDatosFachada();

            var esReasignacion = new OpEquiposAbEficiencia().EsReasignacion(NumeroCredito);
            btnDisplayReceiptToSettle.Visible = !esReasignacion;
            datosComplementariosAlta.Enabled = EquipoViejo.Enabled = !esReasignacion;
            UploadFachada.Visible = UploadEquipoNuevo.Visible = UploadEquipoViejo.Visible = !esReasignacion;
            btnBoletaBajaEficiencia.Visible = esReasignacion;
            btnBoletaBajaEficiencia.Enabled = esReasignacion;
        }
        protected void CargaInformacionInicial(int idCreditoProducto, byte idConsecutivo)
        {

            // Carga Inicial equipos Viejos
            for (byte a = 0; a <= 7; a++)
            {
                var existeHorario = CapturaSolicitud.ObtenHorariosOperacionPorDiaOperacion_IdCredProd(NumeroCredito, 3, a,
                    idCreditoProducto, idConsecutivo);
                if (existeHorario == null) continue;

                switch (a)
                {
                    case 1:
                        DDXInicioLunesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor1.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 2:
                        DDXInicioMartesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor2.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 3:
                        DDXInicioMiercolesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor3.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 4:
                        DDXInicioJuevesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor4.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 5:
                        DDXInicioViernesAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor5.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 6:
                        DDXInicioSabadoAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor6.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 7:
                        DDXInicioDomingoAltaEquipoViejo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlabor7.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                }
            }
            var totHorasOperacion = CapturaSolicitud.ObtenTotalHorariosOperacion_idCredProd(NumeroCredito, idCreditoProducto, idConsecutivo, 3);
            noSemanasAltaEquipoViejo.Value = 52;
            if (totHorasOperacion != null)
            {
                TxtHorasAnioAltaEquipoViejo.Text = totHorasOperacion.HORAS_AÑO.ToString();
                TxtHorasSemanaAltaEquipoViejo.Text = totHorasOperacion.HORAS_SEMANA.ToString();
                noSemanasAltaEquipoViejo.Value = totHorasOperacion.SEMANAS_AÑO;
            }

            // Carga Inicial equipos Nuevo
            for (byte a = 0; a <= 7; a++)
            {
                var existeHorario = CapturaSolicitud.ObtenHorariosOperacionPorDiaOperacion_IdCredProd(NumeroCredito, 2, a,
                    idCreditoProducto, idConsecutivo);
                if (existeHorario == null) continue;

                switch (a)
                {
                    case 1:
                        DDXInicioLunesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlaborNuevo1.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 2:
                        DDXInicioMartesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlaborNuevo2.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 3:
                        DDXInicioMiercolesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlaborNuevo3.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 4:
                        DDXInicioJuevesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlaborNuevo4.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 5:
                        DDXInicioViernesAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlaborNuevo5.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 6:
                        DDXInicioSabadoAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlaborNuevo6.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                    case 7:
                        DDXInicioDomingoAltaEquipoNuevo.SelectedItem.Text = existeHorario.Hora_Inicio;
                        hlaborNuevo7.Value = (double?)existeHorario.Horas_Laborables;
                        break;
                }
            }
            var totHorasOperacionNuevo = CapturaSolicitud.ObtenTotalHorariosOperacion_idCredProd(NumeroCredito, idCreditoProducto, idConsecutivo, 2);
            TxtSemanasAnioAltaEquipoNuevo.Value = 52;
            if (totHorasOperacionNuevo != null)
            {
                TxtHorasAnioAltaEquipoNuevo.Text = totHorasOperacionNuevo.HORAS_AÑO.ToString();
                TxtHorasSemanaAltaEquipoNuevo.Text = totHorasOperacionNuevo.HORAS_SEMANA.ToString();
                TxtSemanasAnioAltaEquipoNuevo.Value = totHorasOperacionNuevo.SEMANAS_AÑO;
            }

        }
        protected void grdEquiposAlta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idTecnologia = ((GrdEquiposAlta)(e.Row.DataItem)).Cve_Tecnologia;
                int idConsecutivo = ((GrdEquiposAlta)(e.Row.DataItem)).idConsecutivo;
                int idCreditoProducto = ((GrdEquiposAlta)(e.Row.DataItem)).ID_CREDITO_PRODUCTO;

                if (InfoCompAltaBajaEquipos.CargaCompletaCreditoProducto(txtCreditoNumAltaEquipos.Text, idConsecutivo, idTecnologia, idCreditoProducto))
                {
                    e.Row.BackColor = Color.Chartreuse;
                    var ckbSelect = e.Row.FindControl("ckbSelect") as CheckBox;
                    if (ckbSelect != null) ckbSelect.Checked = true;


                    
                }

            }
        }
        #endregion

        #region Llenar Catalogos

        protected void LLenaHorariosEquiposNuevos()
        {
            var listHorarios = CatalogosSolicitud.ObtenHorariosTrabajo();

            DDXInicioLunesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioLunesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioLunesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioLunesAltaEquipoNuevo.DataBind();
            DDXInicioLunesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioLunesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioMartesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioMartesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioMartesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioMartesAltaEquipoNuevo.DataBind();
            DDXInicioMartesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioMartesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioMiercolesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioMiercolesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioMiercolesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioMiercolesAltaEquipoNuevo.DataBind();
            DDXInicioMiercolesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioMiercolesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioJuevesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioJuevesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioJuevesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioJuevesAltaEquipoNuevo.DataBind();
            DDXInicioJuevesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioJuevesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioViernesAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioViernesAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioViernesAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioViernesAltaEquipoNuevo.DataBind();
            DDXInicioViernesAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioViernesAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioSabadoAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioSabadoAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioSabadoAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioSabadoAltaEquipoNuevo.DataBind();
            DDXInicioSabadoAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioSabadoAltaEquipoNuevo.SelectedIndex = 0;

            DDXInicioDomingoAltaEquipoNuevo.DataSource = listHorarios;
            DDXInicioDomingoAltaEquipoNuevo.DataValueField = "CveValorCatalogo";
            DDXInicioDomingoAltaEquipoNuevo.DataTextField = "DescripcionCatalogo";
            DDXInicioDomingoAltaEquipoNuevo.DataBind();
            DDXInicioDomingoAltaEquipoNuevo.Items.Insert(0, "Seleccione");
            DDXInicioDomingoAltaEquipoNuevo.SelectedIndex = 0;

        }
        protected void LLenaHorariosEquiposViejos()
        {
            var listHorarios = CatalogosSolicitud.ObtenHorariosTrabajo();

            DDXInicioLunesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioLunesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioLunesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioLunesAltaEquipoViejo.DataBind();
            DDXInicioLunesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioLunesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioMartesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioMartesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioMartesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioMartesAltaEquipoViejo.DataBind();
            DDXInicioMartesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioMartesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioMiercolesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioMiercolesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioMiercolesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioMiercolesAltaEquipoViejo.DataBind();
            DDXInicioMiercolesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioMiercolesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioJuevesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioJuevesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioJuevesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioJuevesAltaEquipoViejo.DataBind();
            DDXInicioJuevesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioJuevesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioViernesAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioViernesAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioViernesAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioViernesAltaEquipoViejo.DataBind();
            DDXInicioViernesAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioViernesAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioSabadoAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioSabadoAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioSabadoAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioSabadoAltaEquipoViejo.DataBind();
            DDXInicioSabadoAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioSabadoAltaEquipoViejo.SelectedIndex = 0;

            DDXInicioDomingoAltaEquipoViejo.DataSource = listHorarios;
            DDXInicioDomingoAltaEquipoViejo.DataValueField = "CveValorCatalogo";
            DDXInicioDomingoAltaEquipoViejo.DataTextField = "DescripcionCatalogo";
            DDXInicioDomingoAltaEquipoViejo.DataBind();
            DDXInicioDomingoAltaEquipoViejo.Items.Insert(0, "Seleccione");
            DDXInicioDomingoAltaEquipoViejo.SelectedIndex = 0;


        }

        #endregion

        #region Botones Impresion de Documentos
        protected void btnDisplayCreditCheckList_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Check List Expediente&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayCreditContract_Click(object sender, EventArgs e)
        {
            if (Tipo_Sociedad == (int)CompanyType.PERSONAFISICA)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=ContratoCreditoPF&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else if (Tipo_Sociedad == (int)CompanyType.REPECO)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=ContratoCreditoREPECO&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=ContratoCreditoPM&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);

        }
        protected void btnDisplayEquipmentAct_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Acta Entrega-Recepcion&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayCreditRequest1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Solicitud de Credito&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayPromissoryNote_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Pagare&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayGuaranteeEndorsement_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Endoso en Garantia&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayQuota1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Presupuesto de Inversion&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayGuarantee_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Carta Compromiso Aval&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayRepaymentSchedule_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Tabla de Amortizacion&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayDisposalBonusReceipt_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Recibo de Chatarrizacion&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnDisplayReceiptToSettle_Click(object sender, EventArgs e)
        {            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=EquipoBajaEficiencia&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnAmortPag_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=TablaAmortizacionPagare&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }
        protected void btnBoletaBajaEficiencia_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintForm", "window.open('../SupplierModule/PrintForm.aspx?ReportName=Boleta_EquipoBajaEficiencia&CreditNumber=" + NumeroCredito + "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        public bool TieneFoto(string No_Credito)
        {
            return InfoCompAltaBajaEquipos.TieneFoto(No_Credito);
        }
        #endregion

        #region Editar Equipos
        protected void BtnImgEditarAltaEquipo_Click(object sender, ImageClickEventArgs e)
        {

            lblInformacionEA.Text = @"Tecnología";
            LimpiaDatosComplemetariosAlta();
            lblTipoMov.Text = "";
            EquipoViejo.Visible = false;
            EquipoNuevo.Visible = false;

            var gridViewRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            hiddenRowIndexEquiboAlta.Value = gridViewRow.RowIndex.ToString(CultureInfo.InvariantCulture);
            var dataKey = grdEquiposAlta.DataKeys[gridViewRow.RowIndex];

            if (dataKey != null)
            {
                hidDataKey0.Value = dataKey[0].ToString(); //Id_Credito_Producto
                hidDataKey.Value = dataKey[1].ToString(); //idConsecutivo
                datosComplementariosAlta.CssClass = "PanelVisible";
                IdCredProducto = Convert.ToInt32(dataKey[0].ToString());
                var tecnologiaSel = grdEquiposAlta.Rows[gridViewRow.RowIndex].Cells[4].Text;
                var producto = grdEquiposAlta.Rows[gridViewRow.RowIndex].Cells[7].Text;

                lblInformacionEA.Text = @"Tecnología: " + Convert.ToString(tecnologiaSel) + @"  Producto: " +
                                        Convert.ToString(producto);
                var datosCreditoSeleccionado =
                    InfoCompAltaBajaEquipos.ObtieneCveProductoporIdCreditoProducto(IdCredProducto);
                var tipoMovimiento = InfoCompAltaBajaEquipos.Get_Tipo_Movimiento(datosCreditoSeleccionado);
                HidTipoMovimiento.Value = tipoMovimiento;

                var ofoto = new CRE_FOTOS()
                {
                    No_Credito = txtCreditoNumAltaEquipos.Text,
                    idConsecutivoFoto = int.Parse(hidDataKey.Value),
                    idCreditoProducto = int.Parse(hidDataKey0.Value)
                };


                switch (tipoMovimiento)
                {
                    case "1":
                        lblTipoMov.Text = @"(Adquisicion)";
                        EquipoNuevo.Visible = true;
                        EquipoViejo.Visible = true;
                        ofoto.idTipoFoto = 2;
                        imgOKLoaderEquipoViejo.Visible = InfoCompAltaBajaEquipos.ImagenAsignada(ofoto);
                        ofoto.idTipoFoto = 3;
                        imgOkEquipoNuevo.Visible = InfoCompAltaBajaEquipos.ImagenAsignada(ofoto);
                        break;
                    case "2":
                        lblTipoMov.Text = @"(Sustitución)";
                        EquipoNuevo.Visible = true;
                        ofoto.idTipoFoto = 3;
                        imgOkEquipoNuevo.Visible = InfoCompAltaBajaEquipos.ImagenAsignada(ofoto);
                        break;
                    default:
                        lblTipoMov.Text = @"";
                        break;

                }

                LLenaHorariosEquiposNuevos();
                LLenaHorariosEquiposViejos();
                CargaInformacionInicial(int.Parse(hidDataKey0.Value), byte.Parse(hidDataKey.Value));

            }
        }
        #endregion

        #region GuardarDatosEquipos
        protected void btnGuardarDatosCompEquipoAlta_Click(object sender, EventArgs e)
        {
            // Apartir del tipo de Movimiento validar que por lo menos un horario sea capturado
            // que la fotografia de la fachada haya sido cargada
            // que la fotografia del equipo haya sido cargada

            //Guardar los horarios por dia y totales por tipo de movimiento
            string mensaje = string.Empty;
           
            switch (HidTipoMovimiento.Value)
            {
                case "1":
                    mensaje += ValidaDatosEquipoNuevos();
                    mensaje += ValidaDatosEquiposViejos();
                    if (mensaje.Equals(string.Empty))
                    {
                        //Alta Horarios de equipo Nuevo
                        if (InsertaHorariosCreditoProducto(2, hlaborNuevo1,
                            DDXInicioLunesAltaEquipoNuevo,
                            hlaborNuevo2,
                            DDXInicioMartesAltaEquipoNuevo,
                            hlaborNuevo3,
                            DDXInicioMiercolesAltaEquipoNuevo,
                            hlaborNuevo4,
                            DDXInicioJuevesAltaEquipoNuevo,
                            hlaborNuevo5,
                            DDXInicioViernesAltaEquipoNuevo,
                            hlaborNuevo6,
                            DDXInicioSabadoAltaEquipoNuevo,
                            hlaborNuevo7,
                            DDXInicioDomingoAltaEquipoNuevo,
                            double.Parse(TxtHorasSemanaAltaEquipoNuevo.Text),
                            double.Parse(TxtSemanasAnioAltaEquipoNuevo.Text),
                            double.Parse(TxtHorasAnioAltaEquipoNuevo.Text),
                            byte.Parse(hidDataKey.Value)) &&

                            // Alta horarios de equipo Viejo
                            InsertaHorariosCreditoProducto(3, hlabor1,
                                DDXInicioLunesAltaEquipoViejo,
                                hlabor2,
                                DDXInicioMartesAltaEquipoViejo,
                                hlabor3,
                                DDXInicioMiercolesAltaEquipoViejo,
                                hlabor4,
                                DDXInicioJuevesAltaEquipoViejo,
                                hlabor5,
                                DDXInicioViernesAltaEquipoViejo,
                                hlabor6,
                                DDXInicioSabadoAltaEquipoViejo,
                                hlabor7,
                                DDXInicioDomingoAltaEquipoViejo,
                                double.Parse(TxtHorasSemanaAltaEquipoViejo.Text),
                                double.Parse(noSemanasAltaEquipoViejo.Text),
                                double.Parse(TxtHorasAnioAltaEquipoViejo.Text),
                                byte.Parse(hidDataKey.Value)))
                        {
                            var ckbSelect = grdEquiposAlta.Rows[Convert.ToInt32(hiddenRowIndexEquiboAlta.Value)].FindControl("ckbSelect") as CheckBox;
                            if (ckbSelect != null && !ckbSelect.Checked)
                            {
                                ckbSelect.Checked = true;
                                grdEquiposAlta.Rows[Convert.ToInt32(hiddenRowIndexEquiboAlta.Value)].BackColor = Color.Chartreuse;
                            }
                        
                            datosComplementariosAlta.CssClass = "PanelNoVisible";

                        }


                    }
                    break;
                case "2":
                    mensaje += ValidaDatosEquipoNuevos();
                    if (mensaje.Equals(string.Empty))
                    {
                        // Alta horarios de equipo Nuevo
                        if (InsertaHorariosCreditoProducto(2, hlaborNuevo1,
                            DDXInicioLunesAltaEquipoNuevo,
                            hlaborNuevo2,
                            DDXInicioMartesAltaEquipoNuevo,
                            hlaborNuevo3,
                            DDXInicioMiercolesAltaEquipoNuevo,
                            hlaborNuevo4,
                            DDXInicioJuevesAltaEquipoNuevo,
                            hlaborNuevo5,
                            DDXInicioViernesAltaEquipoNuevo,
                            hlaborNuevo6,
                            DDXInicioSabadoAltaEquipoNuevo,
                            hlaborNuevo7,
                            DDXInicioDomingoAltaEquipoNuevo,
                            double.Parse(TxtHorasSemanaAltaEquipoNuevo.Text),
                            double.Parse(TxtSemanasAnioAltaEquipoNuevo.Text),
                            double.Parse(TxtHorasAnioAltaEquipoNuevo.Text),
                            byte.Parse(hidDataKey.Value)))
                        {
                            var ckbSelect = grdEquiposAlta.Rows[Convert.ToInt32(hiddenRowIndexEquiboAlta.Value)].FindControl("ckbSelect") as CheckBox;
                            if (ckbSelect != null && !ckbSelect.Checked)
                            {
                                ckbSelect.Checked = true;
                                grdEquiposAlta.Rows[Convert.ToInt32(hiddenRowIndexEquiboAlta.Value)].BackColor = Color.Chartreuse;
                            }

                            datosComplementariosAlta.CssClass = "PanelNoVisible";
                        }
                    }

                    break;

            }


            if (!mensaje.Equals(string.Empty))
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError", string.Format("alert('{0}');", mensaje), true);


        }
        private void LimpiaDatosComplemetariosAlta()
        {
            TxtHorasAnioAltaEquipoNuevo.Text = "";
            TxtHorasAnioAltaEquipoViejo.Text = "";
            TxtSemanasAnioAltaEquipoNuevo.Text = "";
            noSemanasAltaEquipoViejo.Text = "";
            TxtHorasSemanaAltaEquipoNuevo.Text = "";
            TxtHorasSemanaAltaEquipoViejo.Text = "";
            hlaborNuevo1.Text = "";
            hlaborNuevo2.Text = "";
            hlaborNuevo3.Text = "";
            hlaborNuevo4.Text = "";
            hlaborNuevo5.Text = "";
            hlaborNuevo6.Text = "";
            hlaborNuevo7.Text = "";

            LimpiarHorarioOperacion(hlaborNuevo1,
                                DDXInicioLunesAltaEquipoNuevo,
                                hlaborNuevo2,
                                DDXInicioMartesAltaEquipoNuevo,
                                hlaborNuevo3,
                                DDXInicioMiercolesAltaEquipoNuevo,
                                hlaborNuevo4,
                                DDXInicioJuevesAltaEquipoNuevo,
                                hlaborNuevo5,
                                DDXInicioViernesAltaEquipoNuevo,
                                hlaborNuevo6,
                                DDXInicioSabadoAltaEquipoNuevo,
                                hlaborNuevo7,
                                DDXInicioDomingoAltaEquipoNuevo);

            LimpiarHorarioOperacion(hlabor1,
                DDXInicioLunesAltaEquipoViejo,
                hlabor2,
                DDXInicioMartesAltaEquipoViejo,
                hlabor3,
                DDXInicioMiercolesAltaEquipoViejo,
                hlabor4,
                DDXInicioJuevesAltaEquipoViejo,
                hlabor5,
                DDXInicioViernesAltaEquipoViejo,
                hlabor6,
                DDXInicioSabadoAltaEquipoViejo,
                hlabor7,
                DDXInicioDomingoAltaEquipoViejo);




        }
        protected bool InsertaHorariosCreditoProducto(byte tipoHorario,
                RadNumericTextBox hlabor1, DropDownList ddxLunes,
                RadNumericTextBox hlabor2, DropDownList ddxMartes,
                RadNumericTextBox hlabor3, DropDownList ddxMiercoles,
                RadNumericTextBox hlabor4, DropDownList ddxJueves,
                RadNumericTextBox hlabor5, DropDownList ddxViernes,
                RadNumericTextBox hlabor6, DropDownList ddxSabado,
                RadNumericTextBox hlabor7, DropDownList ddxDomingo,
                double horasSemana,
                double totSemanas,
                double horasAño,
                byte idConsecutivo
           )
        {
            var band = false;
            var lstHorarios = new List<CLI_HORARIOS_OPERACION>();
            int idCreditoProducto = int.Parse(hidDataKey0.Value);


            if(!ValidarHorarioOperacion( hlabor1,  ddxLunes,
                 hlabor2,  ddxMartes,
                 hlabor3,  ddxMiercoles,
                 hlabor4,  ddxJueves,
                 hlabor5,  ddxViernes,
                 hlabor6,  ddxSabado,
                 hlabor7,  ddxDomingo)) return false;



            lstHorarios.Clear();
            CLI_HORARIOS_OPERACION horario;

            if (hlabor1.Value > 0 &&
                ddxLunes.SelectedItem.Text != @"Seleccione")     //Lunes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 1,
                    Hora_Inicio = ddxLunes.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor1.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };

                lstHorarios.Add(horario);
            }

            if (hlabor2.Value > 0 &&
                ddxMartes.SelectedItem.Text != @"Seleccione")     //Martes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 2,
                    Hora_Inicio = ddxMartes.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor2.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor3.Value > 0 &&
                ddxMiercoles.SelectedItem.Text != @"Seleccione")     //Miercoles
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 3,
                    Hora_Inicio = ddxMiercoles.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor3.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor4.Value > 0 &&
                ddxJueves.SelectedItem.Text != @"Seleccione")     //Jueves
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 4,
                    Hora_Inicio = ddxJueves.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor4.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor5.Value > 0 &&
                ddxViernes.SelectedItem.Text != @"Seleccione")     //Viernes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 5,
                    Hora_Inicio = ddxViernes.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor5.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor6.Value > 0 &&
                ddxSabado.SelectedItem.Text != @"Seleccione")     //Sabado
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 6,
                    Hora_Inicio = ddxSabado.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor6.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            if (hlabor7.Value > 0 &&
                ddxDomingo.SelectedItem.Text != @"Seleccione")     //Domingo
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 7,
                    Hora_Inicio = ddxDomingo.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor7.Value.ToString()),
                    IDCONSECUTIVO = idConsecutivo,
                    ID_CREDITO_PRODUCTO = idCreditoProducto
                };
                lstHorarios.Add(horario);
            }

            var hOperTotal = new H_OPERACION_TOTAL
            {
                No_Credito = NumeroCredito,
                IDTIPOHORARIO = tipoHorario,
                HORAS_SEMANA = horasSemana,
                SEMANAS_AÑO = totSemanas,
                HORAS_AÑO = horasAño,
                ID_CREDITO_PRODUCTO = idCreditoProducto,
                IDCONSECUTIVO = idConsecutivo
            };

            if (SolicitudCreditoAcciones.ActualizaHorarioOperacion_IdCredProducto(lstHorarios, hOperTotal))
                band = true;


            return band;
        }

        #endregion

        #region Carga Fotos
        
        public void UploadFachada_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                if (e.File.FileName != null)
                {
                    if (FormatosValidos(e.File.GetExtension()))
                    {
                        var oFoto = new CRE_FOTOS();
                        oFoto.Nombre = e.File.GetName();
                        oFoto.Extension = e.File.GetExtension();
                        oFoto.Longitud = e.File.ContentLength;

                        byte[] b = new byte[e.File.ContentLength];
                        e.File.InputStream.Read(b, 0, e.File.ContentLength);

                        oFoto.No_Credito = txtCreditoNumAltaEquipos.Text;
                        oFoto.idTipoFoto = 1; // Fachada
                        oFoto.Foto = b;
                        oFoto.Estatus = true;
                        oFoto.FechaAdicion = DateTime.Now;
                        oFoto.AdicionadoPor = Session["UserName"].ToString();
                        oFoto.idConsecutivoFoto = 1;
                        oFoto.idCreditoProducto = null;
                        oFoto.IdCreditoSustitucion = null;
                        if (InfoCompAltaBajaEquipos.GuardarImagenFachada(oFoto))
                            imgOKLoaderFachada.ImageUrl = "~/CentralModule/images/icono_correcto.png";
                        else
                            imgOKLoaderFachada.ImageUrl = "~/CentralModule/images/eliminar-icono.png";

                    }
                    else
                    {
                        
                        ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "NextError",
                            string.Format("alert('{0}');",
                                "El Formato de Imagen no es válido. (Intente con emf, wmf, jpg, jpeg, jpe, png, bmp, tif)"),
                            true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                              string.Format("alert('{0}');",
                                                  "No se encontro archivo, intentelo de nuevo."),
                                              true);
                }
            }
            catch
                (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "NextError",
                    string.Format("alert('{0}');", ex.Message), true);
            }

        }
        public void UploadEquipoViejo_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {

                if(e.File.FileName != null)
                {

                    if (FormatosValidos(e.File.GetExtension()))
                   {
                       var oFoto = new CRE_FOTOS();
                       oFoto.Nombre = e.File.GetName();
                       oFoto.Extension = e.File.GetExtension();
                       oFoto.Longitud = e.File.ContentLength;

                       byte[] b = new byte[e.File.ContentLength];
                       e.File.InputStream.Read(b, 0, e.File.ContentLength);

                        oFoto.No_Credito = txtCreditoNumAltaEquipos.Text;
                        oFoto.idTipoFoto = 2; // Equipo Viejo
                        oFoto.Foto = b;
                        oFoto.Estatus = true;
                        oFoto.FechaAdicion = DateTime.Now;
                        oFoto.AdicionadoPor = Session["UserName"].ToString();
                        oFoto.idConsecutivoFoto = int.Parse(hidDataKey.Value);
                        oFoto.idCreditoProducto = int.Parse(hidDataKey0.Value);
                        if (InfoCompAltaBajaEquipos.GuardarImagenCreditoProducto(oFoto))
                            imgOKLoaderEquipoViejo.ImageUrl = "~/CentralModule/images/icono_correcto.png";
                        else
                            imgOKLoaderEquipoViejo.ImageUrl = "~/CentralModule/images/eliminar-icono.png";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                          string.Format("alert('{0}');", "El Formato de Imagen no es válido. (Intente con emf, wmf, jpg, jpeg, jpe, png, bmp, tif)"), true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                              string.Format("alert('{0}');",
                                                  "No se encontro archivo, intentelo de nuevo."),
                                              true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                          string.Format("alert('{0}');", ex.Message), true);
            }
        }
        public void UploadEquipoNuevo_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                if (e.File.FileName != null)
                {
                    if (FormatosValidos(e.File.GetExtension()))
                    {
                        var oFoto = new CRE_FOTOS();
                        oFoto.Nombre = e.File.GetName();
                        oFoto.Extension = e.File.GetExtension();
                        oFoto.Longitud = e.File.ContentLength;

                        byte[] b = new byte[e.File.ContentLength];
                        e.File.InputStream.Read(b, 0, e.File.ContentLength);
                        
                        oFoto.No_Credito = txtCreditoNumAltaEquipos.Text;
                        oFoto.idTipoFoto = 3; // Equipo Nuevo
                        oFoto.Foto = b;
                        oFoto.Estatus = true;
                        oFoto.FechaAdicion = DateTime.Now;
                        oFoto.AdicionadoPor = Session["UserName"].ToString();
                        oFoto.idConsecutivoFoto = int.Parse(hidDataKey.Value);
                        oFoto.idCreditoProducto = int.Parse(hidDataKey0.Value);
                        if (InfoCompAltaBajaEquipos.GuardarImagenCreditoProducto(oFoto))
                            imgOkEquipoNuevo.ImageUrl = "~/CentralModule/images/icono_correcto.png";
                        else
                            imgOkEquipoNuevo.ImageUrl = "~/CentralModule/images/eliminar-icono.png";

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                          string.Format("alert('{0}');", "El Formato de Imagen no es válido. (Intente con emf, wmf, jpg, jpeg, jpe, png, bmp, tif)"), true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                              string.Format("alert('{0}');",
                                                  "No se encontro archivo, intentelo de nuevo."),
                                              true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "NextError",
                                          string.Format("alert('{0}');", ex.Message), true);
            }
        }
        protected void imgbtnVer_Click(object sender, ImageClickEventArgs e)
        {
            string url = string.Format("window.open('VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                                      NumeroCredito, hidDataKey0.Value, "1", hidDataKey.Value);

            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);

        }
        protected void verEquipoNuevo_Click(object sender, ImageClickEventArgs e)
        {
            string url = string.Format("window.open('VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                                       NumeroCredito, hidDataKey0.Value, "3", hidDataKey.Value);

            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);
        }
        protected void verEquipoViejoAlta_Click(object sender, ImageClickEventArgs e)
        {
            string url = string.Format("window.open('VisorImagenes.aspx?CreditNumber={0}&IdCreditoSustitucion={1}&idTipoFoto={2}&IdConsecutivo={3}','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');",
                                                                      NumeroCredito, hidDataKey0.Value, "4", hidDataKey.Value);

            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm", url, true);
        }
        protected void btnRefresh2_Click(object sender, EventArgs e)
        {
            var status = HidstateLoad2.Value;
            switch (HidActualiza2Ok.Value)
            {
                case "Fachada":
                    imgOKLoaderFachada.ImageUrl = string.Format("~/CentralModule/images/{0}", status.Equals("ok") ? "icono_correcto.png" : "eliminar-icono.png");
                    imgOKLoaderFachada.Visible = true;
                    break;
                case "Viejo":
                    imgOKLoaderEquipoViejo.ImageUrl = string.Format("~/CentralModule/images/{0}", status.Equals("ok") ? "icono_correcto.png" : "eliminar-icono.png");
                    imgOKLoaderEquipoViejo.Visible = true;
                    break;
                case "Nuevo":
                    imgOkEquipoNuevo.ImageUrl = string.Format("~/CentralModule/images/{0}", status.Equals("ok") ? "icono_correcto.png" : "eliminar-icono.png");
                    imgOkEquipoNuevo.Visible = true;
                    break;
            }


        }
        #endregion

        #region Validaciones
        protected string ValidaDatosEquipoNuevos()
        {
            // que la fotografia de la fachada haya sido cargada
            string mensaje = string.Empty;
            //Valida los horarios por dia
            decimal hrsSemanal = TxtHorasSemanaAltaEquipoNuevo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasSemanaAltaEquipoNuevo.Text);

            if (hrsSemanal < 1) mensaje = "Debe capturar el horario de operación del equipo nuevo, de por lo menos un día.";

            //Validar los Horarios totales
            decimal hrsAnioTotal = TxtHorasAnioAltaEquipoNuevo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasAnioAltaEquipoNuevo.Text);

            if (hrsAnioTotal < 1) mensaje += "Debe capturar el número de semanas del equipo nuevo.";

            //la fotografia del equipo haya sido cargada
            if (!InfoCompAltaBajaEquipos.ExisteFotoEquipoCreditoProducto(new CRE_FOTOS()
            {
                No_Credito = NumeroCredito,
                idTipoFoto = 3,
                idConsecutivoFoto = int.Parse(hidDataKey.Value),
                idCreditoProducto = int.Parse(hidDataKey0.Value)
            }))
                mensaje += "Debe cargar la fotografía del equipo nuevo.";
            return mensaje;
        }
        private string ValidaDatosEquiposViejos()
        {
            string mensaje = string.Empty;
            //Valida los horarios por dia
            decimal hrsSemanal = TxtHorasSemanaAltaEquipoViejo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasSemanaAltaEquipoViejo.Text);

            if (hrsSemanal < 1) mensaje = "Debe capturar el horario de operación del equipo viejo, de por lo menos un día.";

            //Validar los Horarios totales
            decimal hrsAnioTotal = TxtHorasAnioAltaEquipoViejo.Text.Equals(string.Empty)
                ? 0
                : decimal.Parse(TxtHorasAnioAltaEquipoViejo.Text);

            if (hrsAnioTotal < 1) mensaje += "Debe capturar el número de semanas del equipo viejo.";

            //la fotografia del equipo haya sido cargada
            if (!InfoCompAltaBajaEquipos.ExisteFotoEquipoCreditoProducto(new CRE_FOTOS()
            {
                No_Credito = NumeroCredito,
                idTipoFoto = 2,
                idConsecutivoFoto = int.Parse(hidDataKey.Value),
                idCreditoProducto = int.Parse(hidDataKey0.Value)
            }))
                mensaje += "Debe cargar la fotografía del equipo viejo.";
            return mensaje;
        }
        private bool ValidaDatosFachada()
        {

            return InfoCompAltaBajaEquipos.ExisteFotoFachada(new CRE_FOTOS()
            {
                No_Credito = NumeroCredito,
                idTipoFoto = 1,
                idConsecutivoFoto = 1
            });

        }
        #endregion
        #endregion

        #region Otros

        protected void BloqueaCamposCliente(bool valido)
        {
            if (MopValido)
            {
                valido = !MopValido;
                DDXTipoPersona.Enabled = valido;
                TxtNombrePFisicaVPyME.Enabled = valido;
                TxtApellidoPaternoVPyME.Enabled = valido;
                TxtApellidoMaternoVPyME.Enabled = valido;
                TxtFechaNacimientoVPyME.Enabled = valido;
                TxtRFCFisicaVPyME.Enabled = valido;

                TxtRazonSocialVPyME.Enabled = valido;
                TxtFechaConstitucionVPyME.Enabled = valido;
                TxtFechaConstitucionVPyME.Enabled = valido;
                TxtRFCMoralVPyME.Enabled = valido;

                TxtCP.Enabled = valido;
                DDXEstado.Enabled = valido;
                DDXMunicipio.Enabled = valido;
                DDXColonia.Enabled = valido;
            }
            
            TxtCPCBas.Enabled = valido;
            DDXEstadoCBas.Enabled = valido;
            DDXMunicipioCBas.Enabled = valido;
            DDXColoniaCBas.Enabled = valido;
            TxtCalleCBas.Enabled = valido;
            TxtTelefonoCBas.Enabled = valido;
            TxtNumeroExteriorCBas.Enabled = valido;
            TxtNumeroInteriorCBas.Enabled = valido;
            txtReferenciaDomNegocioMoral.Enabled = valido;
            DDXTipoPropiedadCBas.Enabled = valido;

            ddxMismoDom_Fiscal_Negocio.Enabled = valido;
            panelMismoDom_Fiscal_Negocio.Enabled = valido;
        }

        protected void BloqueaDireccionFiscal()
        {
            TxtCPCBas.Enabled = false;
            DDXEstadoCBas.Enabled = false;
            DDXMunicipioCBas.Enabled = false;
            DDXColoniaCBas.Enabled = false;
        }

        protected bool InsertaHorarios(byte tipoHorario,
                 RadNumericTextBox hlabor1, DropDownList ddxLunes,
                 RadNumericTextBox hlabor2, DropDownList ddxMartes,
                 RadNumericTextBox hlabor3, DropDownList ddxMiercoles,
                 RadNumericTextBox hlabor4, DropDownList ddxJueves,
                 RadNumericTextBox hlabor5, DropDownList ddxViernes,
                 RadNumericTextBox hlabor6, DropDownList ddxSabado,
                 RadNumericTextBox hlabor7, DropDownList ddxDomingo, double horasSemana, double totSemanas, double horasAño)
        {
            var band = false;
            var lstHorarios = new List<CLI_HORARIOS_OPERACION>();

            lstHorarios.Clear();
            CLI_HORARIOS_OPERACION horario;

            if (hlabor1.Value > 0 &&
                ddxLunes.SelectedItem.Text != @"Seleccione")     //Lunes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 1,
                    Hora_Inicio = ddxLunes.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor1.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };

                lstHorarios.Add(horario);
            }

            if (hlabor2.Value > 0 &&
                ddxMartes.SelectedItem.Text != @"Seleccione")     //Martes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 2,
                    Hora_Inicio = ddxMartes.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor2.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor3.Value > 0 &&
                ddxMiercoles.SelectedItem.Text != @"Seleccione")     //Miercoles
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 3,
                    Hora_Inicio = ddxMiercoles.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor3.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor4.Value > 0 &&
                ddxJueves.SelectedItem.Text != @"Seleccione")     //Jueves
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 4,
                    Hora_Inicio = ddxJueves.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor4.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor5.Value > 0 &&
                ddxViernes.SelectedItem.Text != @"Seleccione")     //Viernes
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 5,
                    Hora_Inicio = ddxViernes.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor5.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor6.Value > 0 &&
                ddxSabado.SelectedItem.Text != @"Seleccione")     //Sabado
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 6,
                    Hora_Inicio = ddxSabado.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor6.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            if (hlabor7.Value > 0 &&
                ddxDomingo.SelectedItem.Text != @"Seleccione")     //Domingo
            {
                horario = new CLI_HORARIOS_OPERACION
                {
                    No_Credito = NumeroCredito,
                    IDTIPOHORARIO = tipoHorario,
                    ID_DIA_OPERACION = 7,
                    Hora_Inicio = ddxDomingo.SelectedItem.Text,
                    Horas_Laborables = decimal.Parse(hlabor7.Value.ToString()),
                    Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
                };
                lstHorarios.Add(horario);
            }

            var hOperTotal = new H_OPERACION_TOTAL
            {
                No_Credito = NumeroCredito,
                IDTIPOHORARIO = tipoHorario,
                HORAS_SEMANA = horasSemana,
                SEMANAS_AÑO = totSemanas,
                HORAS_AÑO = horasAño,
                Id_Credito_Sustitucion = tipoHorario == 3 ? IdCredSustitucion : (int?)null
            };

            if (tipoHorario == 3)
            {
                if (SolicitudCreditoAcciones.ActualizaHorarioOperacion_IdCredSust(lstHorarios, hOperTotal))
                    band = true;
            }

            if (tipoHorario == 2)
            {
                if (SolicitudCreditoAcciones.ActualizaHorarioOperacion_IdCredProducto(lstHorarios, hOperTotal))
                    band = true;
            }
            if (tipoHorario == 1)
            {
                if (SolicitudCreditoAcciones.ActualizaHorarioOperacion(lstHorarios, hOperTotal))
                    band = true;
            }

            return band;
        }
            
        
       
        
        private void Salir()
        {
            var userModel = (US_USUARIOModel)Session["UserInfo"];

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

        protected void btnEditarValidacionPyme_Click(object sender, EventArgs e)
        {
            DDXGiroEmpresa.Enabled = true;
            DDXSector.Enabled = true;
            TxtNombreComercial.Enabled = true;
            TxtNoEmpleados.Enabled = true;
            TxtGastosMensuales.Enabled = true;
            TxtVentasAnuales.Enabled = true;
            DDXColonia.Enabled = true;
            TxtCP.Enabled = true;
            DDXMunicipio.Enabled = true;
            DDXMunicipio.Enabled = true;
            DDXEstado.Enabled = true;
            DDXTipoPersona.Enabled = true;
            TxtRFCFisicaVPyME.Enabled = true;
            TxtRFCMoralVPyME.Enabled = true;
            TxtNombrePFisicaVPyME.Enabled = true;
            TxtApellidoPaternoVPyME.Enabled = true;
            TxtApellidoMaternoVPyME.Enabled = true;
            TxtRazonSocialVPyME.Enabled = true;
            TxtFechaNacimientoVPyME.Enabled = true;
            TxtFechaConstitucionVPyME.Enabled = true;

            EditaPyme = true;
            BloqueaCamposCliente(!MopValido);
        }

        protected void ddxRepLegal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddxRepLegal.SelectedValue == "SI")
            {
                TxtNombreRepLegal.Text = TxtNombrePFisicaCBas.Text.ToUpper();
                TxtApPaternoRepLegal.Text = TxtApellidoPaternoCBas.Text.ToUpper();
                TxtApMaternoRepLegal.Text = TxtApellidoMaternoCBas.Text.ToUpper();
                TxtEmailRepLegal.Text = TxtEmailCBas.Text.ToUpper();
                TxtTelefonoRepLegal.Text = TxttelefonoFiscalCBas.Text.ToUpper();
                PanelRepLegal.Visible = false;

                BloqueaPoderNotarialRepLegal(false); 
            }
            else
            {
                PanelRepLegal.Visible = true;
                TxtNombreRepLegal.Text = "";
                TxtApPaternoRepLegal.Text = "";
                TxtApMaternoRepLegal.Text = "";
                TxtEmailRepLegal.Text = "";
                TxtTelefonoRepLegal.Text = "";

                BloqueaPoderNotarialRepLegal(true);

                var lstEstado = CatalogosSolicitud.ObtenCatEstadosRepublica();
                if (lstEstado == null) return;

                DDXEstadoPNRL.Items.Clear();
                DDXEstadoPNRL.DataSource = lstEstado;
                DDXEstadoPNRL.DataValueField = "Cve_Estado";
                DDXEstadoPNRL.DataTextField = "Dx_Nombre_Estado";
                DDXEstadoPNRL.DataBind();

                DDXEstadoPNRL.Items.Insert(0, "Seleccione");
                DDXEstadoPNRL.SelectedIndex = 0;
            }

        }

        protected void BloqueaPoderNotarialRepLegal(bool activo)
        {
            TxtNumeroEscrituraPNCliente.Enabled = activo;
            TxtPNCliente.Enabled = activo;
            TxtNomNotarioPNCliente.Enabled = activo;
            DDXEstadoPNRL.Enabled = activo;
            DDXMunicipioPNRL.Enabled = activo;
            TxtNotariaPNRL.Enabled = activo;

            RequiredFieldValidator59.Enabled = activo;
            RequiredFieldValidator60.Enabled = activo;
            RequiredFieldValidator61.Enabled = activo;
            RequiredFieldValidator62.Enabled = activo;
            RequiredFieldValidator63.Enabled = activo;
            RequiredFieldValidator64.Enabled = activo;

            PanelRepLegal.Visible = activo;
            PanelPoderRepLegal.Visible = activo;
        }

        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            var razonSocial = DDXTipoPersona.SelectedValue == "1"
                                          ? TxtNombrePFisicaVPyME.Text + " " + TxtApellidoPaternoVPyME.Text + " " +
                                            TxtApellidoMaternoVPyME.Text
                                          : TxtRazonSocialVPyME.Text;
            wuAltaBajaEquipos1.InicializaEdicionPresupuesto(NumeroCredito);
        }

        #endregion

        protected void btnStepPre_Click(object sender, EventArgs e)
        {
            var stepNext =
                wizardPages.FindControl("StepNavigationTemplateContainerID").FindControl("btnStepNext") as Button;

            if (stepNext != null) stepNext.Enabled = true;
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
        //    try
        //    {

        //        //HttpFileCollection files = Request.Files;

        //        HttpPostedFile file = UPL_PDF.PostedFile; //Request.Files[files];
        //        if (file.ContentLength > 0)
        //        {
        //            var stepNext = wizardPages.FindControl("StepNavigationTemplateContainerID").FindControl("btnStepNext") as Button;
        //            stepNext.Enabled = false;                    
        //            int size = file.ContentLength;
        //            string name = file.FileName;
        //            int position = name.LastIndexOf("\\");
        //            name = name.Substring(position + 1);
        //            string contentType = file.ContentType;

        //            if (contentType == "application/pdf")
        //            {
        //                byte[] PDF = new byte[size];

        //                file.InputStream.Read(PDF, 0, size);


        //                var RFC = ObtenRFC();

        //                var Us_Dist = ((US_USUARIOModel)Session["UserInfo"]).Id_Usuario;
        //                var Dep_Us_Dist = ((US_USUARIOModel)Session["UserInfo"]).Id_Departamento;
        //                var US_Tipo_US = ((US_USUARIOModel)Session["UserInfo"]).Tipo_Usuario;
        //                var ID_Dist = ((US_USUARIOModel)Session["UserInfo"]).Id_Usuario;
        //                var Comprobante = PDF;
        //                var Fecha_Solicitud = DateTime.Now;
        //                string RazonSocial_Nombre;
        //                string Fecha_Nac_Reg;
        //                byte TipoPersona;
        //                //if (TipoSociedad == (int)CompanyType.MORAL)
        //                if (TxtNombrePFisicaVPyME.Text == "")
        //                {
        //                    RazonSocial_Nombre = TxtRazonSocialVPyME.Text;
        //                    Fecha_Nac_Reg = TxtFechaConstitucionVPyME.Text != "" ? TxtFechaConstitucionVPyME.Text : TxtFechaNacimientoVPyME.Text;
        //                    TipoPersona = 2;
        //                }
        //                else
        //                {
        //                    RazonSocial_Nombre = TxtNombrePFisicaVPyME.Text + " " + TxtApellidoPaternoVPyME.Text + " " + TxtApellidoMaternoVPyME.Text;
        //                    Fecha_Nac_Reg = TxtFechaNacimientoVPyME.Text != "" ? TxtFechaNacimientoVPyME.Text : TxtFechaConstitucionVPyME.Text;
        //                    TipoPersona = 1;
        //                }

        //                bool Se_Almacena = Validacion_RFC_L.ClassInstance.Almacena_Solicitud(RFC, Us_Dist, Dep_Us_Dist, US_Tipo_US, RazonSocial_Nombre, Fecha_Nac_Reg, TipoPersona, Comprobante, Fecha_Solicitud);

        //                if (Se_Almacena)
        //                {
        //                    //enviar correo y alert de que se envia solicitud
        //                    List<CorreosValidacionRFC> CorreoJefeZona = Validacion_RFC_L.ClassInstance.Obten_JefeZona(ID_Dist, US_Tipo_US);
        //                    string NomCorreoFallido = string.Empty;


        //                    if (CorreoJefeZona != null)
        //                    {
        //                        foreach (var nuevo in CorreoJefeZona)
        //                        {
        //                            try
        //                            {
        //                                MailUtility.MailValidacionRFC("NotificacionZonaValRFC.html",
        //                                            nuevo.NombreDistribuidor,
        //                                            nuevo.NombreJefeZona, RFC, nuevo.Correo, null);
        //                            }
        //                            catch (Exception)
        //                            {
        //                                NomCorreoFallido += NomCorreoFallido == string.Empty ? " " + nuevo.NombreJefeZona : ", " + nuevo.NombreJefeZona;
        //                            }
        //                        }

        //                        if (NomCorreoFallido != string.Empty)
        //                            ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError", "alert('No fue Posible enviar el correo los siguientes Jefes de zona: " + NomCorreoFallido + "');", true);
        //                    }
        //                    else
        //                    {
        //                    }
        //                    btnFinalizar.Enabled = false;

        //                    ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError", "EnviaNotificacion()", true);
        //                    //Response.Redirect("../Login/Login.aspx");
        //                    //return;
        //                }
        //                else
        //                {
        //                    ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError", "alert('No se posible guardar la informacion de la solicitud');", true);
        //                    //no se almaceno
        //                }

        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError", "alert('Se debe seleccionar un achivo de tipo PDF');", true);
        //            }

        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError", "alert('Se debe seleccionar un archivo');", true);
        //        }



        //    }
        //    catch (Exception err)
        //    {
        //        //ScriptManager.RegisterClientScriptBlock(panel, typeof(Page), "NextError", "alert('error');", true);

        //    }
        }

        public string ObtenRFC()
        {
            if (TxtRFCFisicaCBas.Text != "" && TxtRFCFisicaCBas.Visible == true)
            {
                return TxtRFCFisicaCBas.Text;
            }
            else if (TxtRFCFisicaVPyME.Text != "" && TxtRFCFisicaVPyME.Visible == true)
            {
                return TxtRFCFisicaVPyME.Text;
            }
            else if (TxtRFCMoralCBas.Text != "" && TxtRFCMoralCBas.Visible == true)
            {
                return TxtRFCMoralCBas.Text;
            }
            else if (TxtRFCMoralVPyME.Text != "" && TxtRFCMoralVPyME.Visible == true)
            {
                return TxtRFCMoralVPyME.Text;
            }
            else if (TxtRFCOS.Text != "" && TxtRFCOS.Visible == true)
            {
                return TxtRFCOS.Text;
            }
            return null;

        }

        protected void UPL_PDF_Load(object sender, EventArgs e)
        {

        }

    
        
    }
}