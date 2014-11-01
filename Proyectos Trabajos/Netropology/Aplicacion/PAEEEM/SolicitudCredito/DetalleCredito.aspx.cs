using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.LogicaNegocios.Credito;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using System.Web.UI;

namespace PAEEEM.SolicitudCredito
{
    public partial class DetalleCredito : System.Web.UI.Page
    {
        public string NoCredito { set; get; }
        //public DatosConsultaCredito credito
        //{
        //    set
        //    {
        //        Session["credito"] = value;
        //    }
        //    get
        //    {
        //        return (DatosConsultaCredito)Session["credito"];
        //    }
        //}

        public DataTable Credito
        {
            set { Session["credito"] = value; }
            get { return (DataTable) Session["credito"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["UserInfo"] == null)
                {
                    Response.Redirect("../Login/Login.aspx");
                    return;
                }

                NoCredito = Request.QueryString["creditno"] ?? PreviousPage.NoCredito;
                CargarDatos();
                var url = "../SolicitudCredito/Equipos.aspx?Token=" + NoCredito;
                var urlHist = "../SolicitudCredito/HistorialModificaciones.aspx?Token=" + NoCredito;
                HlinkEquipos.NavigateUrl = url;
                HlinkHistModificaciones.NavigateUrl = urlHist;
                const string url2 = "../SolicitudCredito/DatosCredito.aspx";
                HLRegresar.NavigateUrl = url2;

                var lista = DetalleResumenEquipos.ClassInstance.ObtenHistorico(lblRPUtbl.Text);
                DGV_HistoricoConsultas.DataSource = lista;
                RadWindow1.OpenerElementID = HyperLink3.ClientID;
                RadWindow1.DataBind();

            }
        }

        private void CargarDatos()
        {
            Deshabilitar();
            Credito = ConsultaCredito.Credito(NoCredito);
            DatosDistribuidor();
            DatosSolicitud();
            DatosCliente();
            DatosRepresentanteLegal();
            DatosObligadoSolidario();
            DatosTrama();
            Habilitar();
            //TablaAmortizacion();
        }

        #region DATOS

        private void DatosDistribuidor()
        {
            lblRazonSocialDist.Text = Credito.Rows[0]["Dx_Razon_Social"].ToString() ?? "";
            lblNombreComercialDist.Text = Credito.Rows[0]["Dx_Nombre_Comercial"].ToString() ?? "";
            lblContactoDist.Text = Credito.Rows[0]["Dx_Nombre_Repre"].ToString() ?? "";
            lblCorreoDist.Text = Credito.Rows[0]["Dx_Email_Repre"].ToString() ?? "";
            lblTelefonoDist.Text = Credito.Rows[0]["Dx_Telefono_Repre"].ToString() ?? "";

            lblCPDistF.Text = Credito.Rows[0]["Dx_Domicilio_Fiscal_CP"].ToString() ?? "";
            lblEstDistF.Text =
                LogicaNegocios.Credito.ConsultaCredito.Estados((int) Credito.Rows[0]["Cve_Estado_Fisc"])
                    .Dx_Nombre_Estado ?? "";
            lblDMDistF.Text =
                LogicaNegocios.Credito.ConsultaCredito.DelMun((int) Credito.Rows[0]["Cve_Deleg_Municipio_Fisc"])
                    .Dx_Deleg_Municipio ?? "";
            lblColDistF.Text = "";
            lblCaDistF.Text = Credito.Rows[0]["Dx_Domicilio_Fiscal_Calle"].ToString() ?? "";
            lblNumDistF.Text = Credito.Rows[0]["Dx_Domicilio_Fiscal_Num"].ToString() ?? "";

            lblCPDistN.Text = Credito.Rows[0]["Dx_Domicilio_Part_CP"].ToString() ?? "";
            lblEstDistN.Text =
                LogicaNegocios.Credito.ConsultaCredito.Estados((int) Credito.Rows[0]["Cve_Estado_Part"])
                    .Dx_Nombre_Estado ?? "";
            lblDMDistN.Text =
                LogicaNegocios.Credito.ConsultaCredito.DelMun((int) Credito.Rows[0]["Cve_Deleg_Municipio_Part"])
                    .Dx_Deleg_Municipio ?? "";
            lblColDistN.Text = "";
            lblCaDistN.Text = Credito.Rows[0]["Dx_Domicilio_Part_Calle"].ToString() ?? "";
            lblNumDistN.Text = Credito.Rows[0]["Dx_Domicilio_Part_Num"].ToString() ?? "";

        }

        private void DatosSolicitud()
        {
            lblNoSolicitud.Text = Credito.Rows[0]["No_Credito"].ToString() ?? "";
            lblRPUtbl.Text = Credito.Rows[0]["RPU"].ToString() ?? "";
            lblEstatustbl.Text = Credito.Rows[0]["Dx_Estatus_Credito"].ToString() ?? "";
            lblUsuModtbl.Text = Credito.Rows[0]["Usr_Ultmod"].ToString() ?? "";
            lblUltModtbl.Text = Convert.ToDateTime(Credito.Rows[0]["Fecha_Ultmod"].ToString()).ToShortDateString() ?? "";
                //
            lblPendtbl.Text = Convert.ToDateTime(Credito.Rows[0]["Fecha_Pendiente"].ToString()).ToShortDateString() ??
                              ""; //
            lblPEntrtbl.Text = Credito.Rows[0]["Fecha_Por_entregar"].ToString() != ""
                ? Convert.ToDateTime(Credito.Rows[0]["Fecha_Por_entregar"].ToString()).ToShortDateString()
                : "";
            lblERevtbl.Text = Credito.Rows[0]["Fecha_En_revision"].ToString() != ""
                ? Convert.ToDateTime(Credito.Rows[0]["Fecha_En_revision"].ToString()).ToShortDateString()
                : "";
            lblAuttbl.Text = Credito.Rows[0]["Fecha_Autorizado"].ToString() != ""
                ? Convert.ToDateTime(Credito.Rows[0]["Fecha_Autorizado"].ToString()).ToShortDateString()
                : "";
            lblRechtbl.Text = Credito.Rows[0]["Fecha_Rechazado"].ToString() != ""
                ? Convert.ToDateTime(Credito.Rows[0]["Fecha_Rechazado"].ToString()).ToShortDateString()
                : "";
            lblMOPNoValtbl.Text = Credito.Rows[0]["Fecha_Calificación_MOP_no_válida"].ToString() != ""
                ? Convert.ToDateTime(Credito.Rows[0]["Fecha_Calificación_MOP_no_válida"].ToString()).ToShortDateString()
                : "";
            lblCanctbl.Text = Credito.Rows[0]["Fecha_Cancelado"].ToString() != ""
                ? Convert.ToDateTime(Credito.Rows[0]["Fecha_Cancelado"].ToString()).ToShortDateString()
                : "";

            lblNoConsulta.Text = @"1";
                //credito.No_Consultas_Crediticias!=null?credito.No_Consultas_Crediticias.ToString():"";
            lblMOP.Text = Credito.Rows[0]["No_MOP"].ToString() != "" ? Credito.Rows[0]["No_MOP"].ToString() : "";
            lblFolio.Text = Credito.Rows[0]["Folio_Consulta"].ToString() != ""
                ? Credito.Rows[0]["Folio_Consulta"].ToString()
                : "";
            lblFecConsulta.Text = Credito.Rows[0]["Fecha_Consulta"].ToString() != ""
                ? Convert.ToDateTime(Credito.Rows[0]["Fecha_Consulta"].ToString()).ToShortDateString()
                : "";

            //lblPostSIC.Text = Credito.Rows[0]["Afectacion_SICOM_fecha"].ToString() != "" ? Convert.ToDateTime(Credito.Rows[0]["Afectacion_SICOM_fecha"].ToString()).ToShortDateString() : "";
            //lblPostSIR.Text = Credito.Rows[0]["Afectacion_SIRCA_Fecha"].ToString() != "" ? Convert.ToDateTime(Credito.Rows[0]["Afectacion_SIRCA_Fecha"].ToString()).ToShortDateString() : "";

            lblNoIntetbl.Text = Credito.Rows[0]["ID_Intelisis"].ToString() != ""
                ? Credito.Rows[0]["ID_Intelisis"].ToString()
                : "N/A";
            lblfecPagDisttbl.Text = Credito.Rows[0]["Fecha_Pago_Intelisis"].ToString() != ""
                ? Convert.ToDateTime(Credito.Rows[0]["Fecha_Pago_Intelisis"].ToString()).ToShortDateString()
                : "N/A";
            lblMonPagtbl.Text = Credito.Rows[0]["Mt_Monto_Pagado"].ToString() != ""
                ? Credito.Rows[0]["Mt_Monto_Pagado"].ToString()
                : "N/A";

        }

        private void DatosCliente()
        {
            lblNomRSCli.Text = Credito.Rows[0]["NombreRazonSocial"].ToString() ?? "";
            lblNomComCli.Text = Credito.Rows[0]["Nombre_Comercial"].ToString() ?? "";
            lblRFCCli.Text = Credito.Rows[0]["RFC"].ToString() ?? "";
            lblCURPCliD.Text = Credito.Rows[0]["CURP"].ToString() ?? "";
            lblGiroEmpCli.Text = Credito.Rows[0]["Dx_Tipo_Industria"].ToString() ?? "";
            lblSectorCli.Text = Credito.Rows[0]["Dx_Sector"].ToString() != ""
                ? Credito.Rows[0]["Dx_Sector"].ToString()
                : "";
            lblEstCivCliD.Text = Credito.Rows[0]["Dx_Estado_Civil"].ToString() ?? "";
            lblRegMatCliD.Text = Credito.Rows[0]["RegimenConyugal"].ToString() != "" &&
                                 Credito.Rows[0]["RegimenConyugal"].ToString() != ""
                ? Credito.Rows[0]["RegimenConyugal"].ToString()
                : "N/A";
            lblEmailCli.Text = Credito.Rows[0]["email"].ToString() ?? "";
            lblTipIdenCli.Text = Credito.Rows[0]["TipoIdentificacion"].ToString() ?? "";
            lblNumIdenCli.Text = Credito.Rows[0]["Numero_Identificacion"].ToString() ?? "";
            lblNomConyCliD.Text = Credito.Rows[0]["Nombre_Notario"].ToString() != "" &&
                                  Credito.Rows[0]["Nombre_Notario"].ToString() != ""
                ? Credito.Rows[0]["Nombre_Notario"].ToString()
                : "N/A";
            DomicilioFiscalCliente();
            DomicilioNegocioCliente();
        }

        private void DomicilioFiscalCliente()
        {
            var domFiscal = LogicaNegocios.Credito.blDireccion.Obtener((int) Credito.Rows[0]["IdCliente"], 2);
            if (domFiscal != null)
            {
                lblCPCliF.Text = domFiscal.CP ?? "";
                if (domFiscal.Cve_Estado != null)
                    lblEstCliF.Text =
                        LogicaNegocios.Credito.ConsultaCredito.Estados((int) domFiscal.Cve_Estado).Dx_Nombre_Estado ??
                        "";
                if (domFiscal.Cve_Deleg_Municipio != null)
                    lblDelMunCliF.Text =
                        LogicaNegocios.Credito.ConsultaCredito.DelMun((int) domFiscal.Cve_Deleg_Municipio)
                            .Dx_Deleg_Municipio ?? "";
                lblColCliF.Text = domFiscal.Colonia ?? "";
                lblCalleCliF.Text = domFiscal.Calle ?? "";
                lblNumExtCliF.Text = domFiscal.Num_Ext ?? "";
                lblNumIntCliF.Text = domFiscal.Num_Int ?? "";
                lblTipProCliF.Text = domFiscal.Cve_Tipo_Propiedad != null &&
                                     LogicaNegocios.Credito.ConsultaCredito.TipoPropiedad(
                                         (int) domFiscal.Cve_Tipo_Propiedad).Dx_Tipo_Propiedad != null
                    ? LogicaNegocios.Credito.ConsultaCredito.TipoPropiedad((int) domFiscal.Cve_Tipo_Propiedad)
                        .Dx_Tipo_Propiedad
                    : "";
                lblTelCliF.Text = domFiscal.Telefono_Local;
                lblReferDomF.Text = domFiscal.Referencia_Dom ?? "";
            }
            else
            {
                lblCPCliF.Text = "";
                lblEstCliF.Text = "";
                lblDelMunCliF.Text = "";
                lblColCliF.Text = "";
                lblCalleCliF.Text = "";
                lblNumExtCliF.Text = "";
                lblNumIntCliF.Text = "";
                lblTipProCliF.Text = "";
                lblTelCliF.Text = "";
                lblReferDomF.Text = "";
            }
        }

        private void DomicilioNegocioCliente()
        {
            var domNegocio = LogicaNegocios.Credito.blDireccion.Obtener((int) Credito.Rows[0]["IdCliente"], 1);
            if (domNegocio != null)
            {
                lblCPCliN.Text = domNegocio.CP ?? "";
                if (domNegocio.Cve_Estado != null)
                    lblEstCliN.Text =
                        LogicaNegocios.Credito.ConsultaCredito.Estados((int) domNegocio.Cve_Estado).Dx_Nombre_Estado ??
                        "";
                if (domNegocio.Cve_Deleg_Municipio != null)
                    lblDelMunCliN.Text =
                        LogicaNegocios.Credito.ConsultaCredito.DelMun((int) domNegocio.Cve_Deleg_Municipio)
                            .Dx_Deleg_Municipio ?? "";
                lblColCliN.Text = domNegocio.Colonia ?? "";
                lblCalleCliN.Text = domNegocio.Calle ?? "";
                lblNumExtCliN.Text = domNegocio.Num_Ext ?? "";
                lblNumIntCliN.Text = domNegocio.Num_Int ?? "";
                lblTipProCliN.Text = domNegocio.Cve_Tipo_Propiedad != null &&
                                     LogicaNegocios.Credito.ConsultaCredito.TipoPropiedad(
                                         (int) domNegocio.Cve_Tipo_Propiedad).Dx_Tipo_Propiedad != null
                    ? LogicaNegocios.Credito.ConsultaCredito.TipoPropiedad((int) domNegocio.Cve_Tipo_Propiedad)
                        .Dx_Tipo_Propiedad
                    : "";
                lblTelCliN.Text = domNegocio.Telefono_Local ?? "";
                lblReferDomN.Text = domNegocio.Referencia_Dom ?? "";
            }
            else
            {
                lblCPCliN.Text = "";
                lblEstCliN.Text = "";
                lblDelMunCliN.Text = "";
                lblColCliN.Text = "";
                lblCalleCliN.Text = "";
                lblNumExtCliN.Text = "";
                lblNumIntCliN.Text = "";
                lblTipProCliN.Text = "";
                lblTelCliN.Text = "";
                lblReferDomN.Text = "";
            }
        }

        private void DatosRepresentanteLegal()
        {
            var repreLegal = LogicaNegocios.Credito.blRefCliente.Obtener((int) Credito.Rows[0]["IdCliente"], 1);
            if (repreLegal != null)
            {
                lblNomRL.Text = repreLegal.Nombre + @" " + repreLegal.Ap_Paterno + @" " + repreLegal.Ap_Materno ?? "";
                lblEmailRL.Text = repreLegal.email ?? "";
                lblTelRL.Text = repreLegal.Telefono_Local ?? "";
            }
            else
            {
                lblNomRL.Text = "";
                lblEmailRL.Text = "";
                lblTelRL.Text = "";
            }
            var podNotarial = LogicaNegocios.Credito.blRefNotariales.Obtener((int) Credito.Rows[0]["IdCliente"], 6);
            if (podNotarial == null)
            {
                lblNumEscRL.Text = "";
                lblFecEscRL.Text = "";
                lblNomNotRL.Text = "";
                lblNumNotRL.Text = "";
                lblEstRL.Text = "";
                lblDelMunRL.Text = "";
            }
            else
            {
                lblNumEscRL.Text = podNotarial.Numero_Escritura ?? "";
                lblFecEscRL.Text = podNotarial.Fecha_Escritura.HasValue
                    ? podNotarial.Fecha_Escritura.Value.ToShortDateString()
                    : "";
                lblNomNotRL.Text = podNotarial.Nombre_Notario ?? "";
                lblNumNotRL.Text = podNotarial.Numero_Notaria ?? "";
                lblEstRL.Text = podNotarial.Estado != null &&
                                LogicaNegocios.Credito.ConsultaCredito.Estados((int) podNotarial.Estado)
                                    .Dx_Nombre_Estado != null
                    ? LogicaNegocios.Credito.ConsultaCredito.Estados((int) podNotarial.Estado).Dx_Nombre_Estado
                    : "";
                lblDelMunRL.Text = podNotarial.Municipio != null &&
                                   LogicaNegocios.Credito.ConsultaCredito.DelMun((int) podNotarial.Municipio)
                                       .Dx_Deleg_Municipio != null
                    ? LogicaNegocios.Credito.ConsultaCredito.DelMun((int) podNotarial.Municipio).Dx_Deleg_Municipio
                    : "";
            }
        }

        private void DatosObligadoSolidario()
        {
            var obliSolid = LogicaNegocios.Credito.blRefCliente.Obtener((int) Credito.Rows[0]["IdCliente"], 2);
            var domObliSolid = LogicaNegocios.Credito.blDireccion.Obtener((int) Credito.Rows[0]["IdCliente"], 3);

            if (obliSolid != null && domObliSolid != null)
            {
                switch (obliSolid.Cve_Tipo_Sociedad)
                {
                    case 1:
                        InfoObliSoliFis.Visible = true;
                        InfoObliSoliMor.Visible = false;
                        lblTipPerOS.Text = @"FÍSICA";
                        lblNomOS.Text = obliSolid.Nombre + @" " + obliSolid.Ap_Paterno + @" " + obliSolid.Ap_Materno ??
                                        "";
                        lblRFCOS.Text = obliSolid.RFC ?? "";
                        lblCURPOS.Text = obliSolid.CURP ?? "";
                        lblFecNacOS.Text = obliSolid.Fec_Nacimiento.HasValue
                            ? obliSolid.Fec_Nacimiento.Value.ToShortDateString()
                            : "";
                        lblTelOS.Text = obliSolid.Telefono_Local ?? "";
                        lblCPOS.Text = domObliSolid.CP ?? "";
                        lblEstOS.Text = domObliSolid.Cve_Estado != null &&
                                        LogicaNegocios.Credito.ConsultaCredito.Estados((int) domObliSolid.Cve_Estado)
                                            .Dx_Nombre_Estado != null
                            ? LogicaNegocios.Credito.ConsultaCredito.Estados((int) domObliSolid.Cve_Estado)
                                .Dx_Nombre_Estado
                            : "";
                        lblDelMunOS.Text = domObliSolid.Cve_Deleg_Municipio != null &&
                                           LogicaNegocios.Credito.ConsultaCredito.DelMun(
                                               (int) domObliSolid.Cve_Deleg_Municipio).Dx_Deleg_Municipio != null
                            ? LogicaNegocios.Credito.ConsultaCredito.DelMun((int) domObliSolid.Cve_Deleg_Municipio)
                                .Dx_Deleg_Municipio
                            : "";
                        lblColOS.Text = domObliSolid.Colonia ?? "";
                        lblCalleOS.Text = domObliSolid.Calle ?? "";
                        lblNumOS.Text = domObliSolid.Num_Ext ?? "";
                        break;
                    case 2:
                    {
                        InfoObliSoliFis.Visible = false;
                        InfoObliSoliMor.Visible = true;
                        lblTipPerOSM.Text = @"MORAL";
                        lblRSOS.Text = obliSolid.Razon_Social ?? "";
                        lblNomOSM.Text = obliSolid.Nombre + @" " + obliSolid.Ap_Paterno + @" " + obliSolid.Ap_Materno ??
                                         "";
                        lblEmailOS.Text = obliSolid.email ?? "";
                        lblTelOSM.Text = obliSolid.Telefono_Local ?? "";

                        var podNotarial =
                            LogicaNegocios.Credito.blRefNotariales.Obtener((int) Credito.Rows[0]["IdCliente"], 4);
                        if (podNotarial == null)
                        {
                            lblNumEscOSP.Text = "";
                            lblFecOSP.Text = "";
                            lblNomNotOSP.Text = "";
                            lblEstOSP.Text = "";
                            lblDelMunOSP.Text = "";
                            lblNumNotOSP.Text = "";
                        }
                        else
                        {
                            lblNumEscOSP.Text = podNotarial.Numero_Escritura ?? "";
                            lblFecOSP.Text = podNotarial.Fecha_Escritura.HasValue
                                ? podNotarial.Fecha_Escritura.Value.ToShortDateString()
                                : "";
                            lblNomNotOSP.Text = podNotarial.Nombre_Notario ?? "";
                            lblNumNotOSP.Text = podNotarial.Numero_Notaria ?? "";
                            lblEstOSP.Text = podNotarial.Estado != null &&
                                             LogicaNegocios.Credito.ConsultaCredito.Estados((int) podNotarial.Estado)
                                                 .Dx_Nombre_Estado != null
                                ? LogicaNegocios.Credito.ConsultaCredito.Estados((int) podNotarial.Estado)
                                    .Dx_Nombre_Estado
                                : "";
                            lblDelMunOSP.Text = podNotarial.Municipio != null &&
                                                LogicaNegocios.Credito.ConsultaCredito.DelMun(
                                                    (int) podNotarial.Municipio).Dx_Deleg_Municipio != null
                                ? LogicaNegocios.Credito.ConsultaCredito.DelMun((int) podNotarial.Municipio)
                                    .Dx_Deleg_Municipio
                                : "";
                        }

                        var actaConst =
                            LogicaNegocios.Credito.blRefNotariales.Obtener((int) Credito.Rows[0]["IdCliente"], 5);
                        if (actaConst == null)
                        {
                            lblNumEscOSA.Text = "";
                            lblFecOSA.Text = "";
                            lblNomNotOSA.Text = "";
                            lblEstOSA.Text = "";
                            lblDelMunOSA.Text = "";
                            lblNumNotOSA.Text = "";
                        }
                        else
                        {
                            lblNumEscOSA.Text = actaConst.Numero_Escritura ?? "";
                            lblFecOSA.Text = actaConst.Fecha_Escritura.HasValue
                                ? actaConst.Fecha_Escritura.Value.ToShortDateString()
                                : "";
                            lblNomNotOSA.Text = actaConst.Nombre_Notario ?? "";
                            lblNumNotOSA.Text = actaConst.Numero_Notaria ?? "";
                            lblEstOSA.Text =
                                LogicaNegocios.Credito.ConsultaCredito.Estados((int) actaConst.Estado).Dx_Nombre_Estado ??
                                "";
                            lblDelMunOSA.Text =
                                LogicaNegocios.Credito.ConsultaCredito.DelMun((int) actaConst.Municipio)
                                    .Dx_Deleg_Municipio ?? "";
                        }
                    }
                        break;
                }
            }
            else
            {
                InfoObliSoliFis.Visible = false;
                InfoObliSoliMor.Visible = false;
                lblTipPerOS.Text = "";
                lblNomOS.Text = "";
                lblRFCOS.Text = "";
                lblCURPOS.Text = "";
                lblFecNacOS.Text = "";
                lblTelOS.Text = "";
                lblCPOS.Text = "";
                lblEstOS.Text = "";
                lblDelMunOS.Text = "";
                lblColOS.Text = "";
                lblCalleOS.Text = "";
                lblNumOS.Text = "";

                InfoObliSoliFis.Visible = false;
                InfoObliSoliMor.Visible = false;
                lblTipPerOSM.Text = "";
                lblRSOS.Text = "";
                lblNomOSM.Text = "";
                lblEmailOS.Text = "";
                lblTelOSM.Text = "";
                lblNumEscOSP.Text = "";
                lblFecOSP.Text = "";
                lblNomNotOSP.Text = "";
                lblEstOSP.Text = "";
                lblDelMunOSP.Text = "";
                lblNumNotOSP.Text = "";
                lblNumEscOSA.Text = "";
                lblFecOSA.Text = "";
                lblNomNotOSA.Text = "";
                lblEstOSA.Text = "";
                lblDelMunOSA.Text = "";
                lblNumNotOSA.Text = "";
            }
        }

        public void DatosTrama()
        {
            var tarifa = "";
            var usuarioCfe = "";
            var rpu = "";
            var numCuenta = "";
            var estado = "";
            var delegMunicipio = "";
            var colonia = "";
            var calle = "";
            var fecIniPer = "";
            var fecFinPer = "";
            var fecCons = "";

            //if (Credito.Rows[0]["Consumo_Promedio"].ToString() != "")
            if (LogicaNegocios.Credito.ConsultaCredito.EsNuevo(Credito.Rows[0]["No_Credito"].ToString()))
            {
                var responseData =
                    LogicaNegocios.Credito.ConsultaCredito.DatosTrama(Credito.Rows[0]["No_Credito"].ToString());

                usuarioCfe = responseData.Name ?? "";
                rpu = responseData.ServiceCode ?? "";
                numCuenta = responseData.CN ?? "";
                estado = LogicaNegocios.Credito.ConsultaCredito.Estados(responseData.StateCode).Dx_Nombre_Estado ?? "";
                delegMunicipio = responseData.Poblacion ?? "";
                colonia = responseData.Colonia ?? "";
                fecIniPer = string.Format("{0}/{1}/{2}", responseData.PeriodStartDate.Substring(0, 4),
                    responseData.PeriodStartDate.Substring(4, 2), responseData.PeriodStartDate.Substring(6, 2));
                fecFinPer = string.Format("{0}/{1}/{2}", responseData.PeriodEndDate.Substring(0, 4),
                    responseData.PeriodEndDate.Substring(4, 2), responseData.PeriodEndDate.Substring(6, 2));
                fecCons = responseData.FechaConsulta.HasValue
                    ? responseData.FechaConsulta.Value.ToShortDateString()
                    : "";
                calle = responseData.Address ?? "";
                tarifa = responseData.Rate ?? "";
                //var tramaAnterior = new OpEquiposAbEficiencia().ObtenTrama(Credito.Rows[0]["No_Credito"].ToString());
                //var parseoAnterior = new ParseoTrama(tramaAnterior);

                //rpu =
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 7).Dato != null ?
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 7).Dato : "";
                //numCuenta =
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 160).Dato != null ?
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 160).Dato : "";
                //estado = LogicaNegocios.Credito.ConsultaCredito.Estados(
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).Dato).Dx_Nombre_Estado != null ? LogicaNegocios.Credito.ConsultaCredito.Estados(
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).Dato).Dx_Nombre_Estado : "";
                //fecIniPer =
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 158).Dato != null ?
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 158).Dato : "";
                //fecFinPer =
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 159).Dato != null ?
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 159).Dato : "";
                //calle =
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 147).Dato != null ?
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 147).Dato : "";

                //usuarioCfe =
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 146 && p.Concepto == "NOMBRE DEL USUARIO").Dato != "" ?
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 146 && p.Concepto == "NOMBRE DEL USUARIO").Dato : "";

                //tarifa =
                //    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;

            }
            else
            {
                //var responseData = new CreCredito().ObtenResponseData(Credito.Rows[0]["RPU"].ToString(), (DateTime)Credito.Rows[0]["Fecha_Pendiente"]);
                var responseData = LogicaNegocios.Credito.ConsultaCredito.DatosTrama(Credito.Rows[0]["RPU"].ToString(),
                    (DateTime) Credito.Rows[0]["Fecha_Pendiente"]);
                if (responseData != null)
                {
                    usuarioCfe = responseData.Name ?? "";
                    rpu = responseData.ServiceCode ?? "";
                    numCuenta = responseData.CN ?? "";
                    estado = LogicaNegocios.Credito.ConsultaCredito.Estados(responseData.StateCode).Dx_Nombre_Estado ??
                             "";
                    delegMunicipio = responseData.Poblacion ?? "";
                    colonia = responseData.Colonia ?? "";
                    fecIniPer =
                        string.Format("{0}/{1}/{2}", responseData.PeriodStartDate.Substring(0, 4),
                            responseData.PeriodStartDate.Substring(4, 2), responseData.PeriodStartDate.Substring(6, 2)) ??
                        "";
                    fecFinPer =
                        string.Format("{0}/{1}/{2}", responseData.PeriodEndDate.Substring(0, 4),
                            responseData.PeriodEndDate.Substring(4, 2), responseData.PeriodEndDate.Substring(6, 2)) ??
                        "";
                    fecCons = responseData.FechaConsulta.HasValue
                        ? responseData.FechaConsulta.Value.ToShortDateString()
                        : "";
                    calle = responseData.Address ?? "";
                    tarifa = responseData.Rate ?? "";
                }
            }

            var sub = LogicaNegocios.Credito.ConsultaCredito.Subestacion(Credito.Rows[0]["No_Credito"].ToString());

            lblTarifatbl.Text = tarifa ?? "";

            lblUsuCFEO.Text = usuarioCfe ?? "";
            lblRPUO.Text = rpu ?? "";
            lblFecConsO.Text = fecCons ?? "";
            lblNumCuenO.Text = numCuenta ?? "";
            lblCPO.Text = "";
            lblEstO.Text = estado ?? "";
            lblDelMunO.Text = delegMunicipio ?? "";
            lblColO.Text = colonia ?? "";
            lblCalleO.Text = calle ?? "";
            //lblNumO.Text = "FALTA";
            lblFecInPer.Text = fecIniPer ?? "";
            lblFecFinPer.Text = fecFinPer ?? "";

            if (sub != null)
            {
                lblUsuCFEA.Text = usuarioCfe ?? "";
                lblRPUA.Text = sub.RPU_Nuevo ?? "";
                lblFecConsA.Text = fecCons ?? "";
                lblNumCuenA.Text = sub.No_Cuenta_Nueva ?? "";
                lblCPA.Text = "";
                lblEstA.Text = estado ?? "";
                lblDelMunA.Text = delegMunicipio ?? "";
                lblColA.Text = colonia ?? "";
                lblCalleA.Text = calle ?? "";
                //lblNumA.Text = "FALTA";
            }
            else
            {
                lblUsuCFEA.Text = "";
                lblRPUA.Text = "";
                lblFecConsA.Text = "";
                lblNumCuenA.Text = "";
                lblCPA.Text = "";
                lblEstA.Text = "";
                lblDelMunA.Text = "";
                lblColA.Text = "";
                lblCalleA.Text = "";
                //lblNumA.Text = "";
            }
        }

        #endregion

        private void Habilitar()
        {
            InfoDistr.Visible = true;
            links.Visible = true;
            InfoSolicitud.Visible = true;
            InfoCli.Visible = true;
            InfoDomCli.Visible = true;
            InfoReprLegal.Visible = true;
            switch (Convert.ToInt32(Credito.Rows[0]["Cve_Tipo_Sociedad"].ToString()))
            {
                case 1:
                    lblNombreRSCli.Text = @"Nombre: ";
                    lblTipPersCli.Text = @"FÍSICA";
                    lblCURPCliT.Visible = true;
                    lblCURPCliD.Visible = true;
                    lblFecNacRegCliT.Text = @"FECHA DE NACIMIENTO: ";
                    lblFecNacRegCliD.Text = Credito.Rows[0]["Fec_Nacimiento"].ToString() != ""
                        ? Convert.ToDateTime(Credito.Rows[0]["Fec_Nacimiento"]).ToShortDateString()
                        : "";
                    lblEstCivCliT.Visible = true;
                    lblEstCivCliD.Visible = true;
                    lblRegMatCliT.Visible = true;
                    lblRegMatCliD.Visible = true;
                    lblNomConyCliT.Visible = true;
                    lblNomConyCliD.Visible = true;
                    break;
                case 2:
                    lblNombreRSCli.Text = @"RAZON SOCIAL: ";
                    lblTipPersCli.Text = @"MORAL";
                    lblCURPCliT.Visible = false;
                    lblCURPCliD.Visible = false;
                    lblFecNacRegCliT.Text = @"FECHA DE REGISTRO: ";
                    lblFecNacRegCliD.Text = @"FALTA";
                    lblEstCivCliT.Visible = false;
                    lblEstCivCliD.Visible = false;
                    lblRegMatCliT.Visible = false;
                    lblRegMatCliD.Visible = false;
                    lblNomConyCliT.Visible = false;
                    lblNomConyCliD.Visible = false;
                    break;
            }
            InfoTrama.Visible = true;
        }

        private void Deshabilitar()
        {
            InfoDistr.Visible = false;
            links.Visible = false;
            InfoSolicitud.Visible = false;
            InfoCli.Visible = false;
            InfoDomCli.Visible = false;
            InfoReprLegal.Visible = false;
            InfoObliSoliFis.Visible = false;
            InfoObliSoliMor.Visible = false;
            InfoTrama.Visible = false;
        }

        protected void DGV_HistoricoConsultas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {

        }

        protected void imgExportaPDF_Click(object sender, ImageClickEventArgs e)
        {
            //if (gvTablaAmortizacion.Rows.Count>0)
            //{
            ////string attachment = "attachment; filename=TablaAmoritzacion.pdf";
            ////Response.AddHeader("content-disposition", attachment);
            ////Response.ContentType = "application/pdf";
            ////StringWriter sw = new StringWriter();
            ////HtmlTextWriter htw = new HtmlTextWriter(sw);
            ////gvTablaAmortizacion.AllowPaging = false;
            ////HtmlForm frm = new HtmlForm();
            //////Turn off the view state
            ////this.EnableViewState = false;
            //////Remove the charset from the Content-Type header
            ////Response.Charset = String.Empty;
            ////gvTablaAmortizacion.Parent.Controls.Add(frm);
            ////TablaAmortizacion();
            ////frm.Attributes["runat"] = "server";
            ////frm.Controls.Add(gvTablaAmortizacion);
            ////frm.RenderControl(htw);
            ////Response.Write(sw.ToString());
            ////Response.End();

            //DataTable dt = ToDataTable(tabla);
            //ExportToPdf(TablaAmortizacion());
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "PrintForm",
                "window.open('../SupplierModule/PrintForm.aspx?ReportName=Tabla de Amortizacion&CreditNumber=" +
                Credito.Rows[0]["No_Credito"] +
                "','','top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');", true);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        //private void TablaAmortizacion()
        //{
        //    var tabla = LogicaNegocios.Credito.ConsultaCredito.TablaAmortizacion(Credito.Rows[0]["No_Credito"].ToString());
        //    if (tabla.ToList().Count > 1)
        //    {
        //        gvTablaAmortizacion.DataSource = tabla;
        //        gvTablaAmortizacion.DataBind();
        //        lblFechaAmortizacion.Text = tabla.ToList().FirstOrDefault().Dt_Fecha.Value.ToShortDateString();
        //    }
        //}
    }
}