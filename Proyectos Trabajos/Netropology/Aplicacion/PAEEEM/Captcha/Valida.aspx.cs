using System;
using System.Linq;
using System.Web;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.Entidades.RPUZona;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios.LOG;
using PAEEEM.LogicaNegocios.SolicitudCredito;
using PAEEEM.LogicaNegocios.RPUZona;

namespace PAEEEM.Captcha
{
    public partial class Valida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCheckResult.Text = "";
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            var estado = "";


            try
            {
                if (MyCaptcha1.IsValid)
                {
                    var validator = new CodeValidator();

                    string error;
                    string[] causa;

                    if (validator.ValidateServiceCode(txtServiceCode.Text, out error, ref estado, out causa))
                    {

                        lblCheckResult.Text = string.Format(
                            HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaResultRPUValid") as string
                            , txtServiceCode.Text);

                        if (validator.ValidacionTarifa(validator.ParseoTrama.ComplexParseo, out error, out causa))
                        {

                            var usuario = ((US_USUARIOModel) Session["UserInfo"]).Nombre_Usuario;

                            Region_Zona zonaFide;
                            ZonaRPURD zonaCfe;

                            var existeZona = ValidacionZonaL.ClassInstance.ObtieneZonaCfe(txtServiceCode.Text, out zonaFide,out zonaCfe );

                            if (existeZona)
                            {

                                var zonaValida = ValidacionZonaL.ClassInstance.CoincideZona(txtServiceCode.Text, usuario,
                                    zonaFide.cve_zona);

                                var userModel = (US_USUARIOModel) Session["UserInfo"];

                                if (zonaValida)
                                {
                                    if (userModel != null && userModel.Id_Rol == 3) // distribuidor
                                    {
                                            string idCreditoActual;
                                            var reasignaRecupera =
                                                new RecuperacionDatos().ValidaReasignarRecuperar(
                                                    userModel.Nombre_Usuario,
                                                    out error,
                                                    out idCreditoActual,
                                                    txtServiceCode.Text);
                                            if (reasignaRecupera != 0)
                                            {
                                                if (reasignaRecupera == 1)
                                                {
                                                    Session["ValidRPU"] = txtServiceCode.Text;
                                                    Session["PARSEO_TRAMA"] = validator.ParseoTrama;

                                                    Response.Redirect(
                                                        "../SupplierModule/CapturaDatosSolicitud.aspx?TokenR=" +
                                                        Convert.ToBase64String(
                                                            System.Text.Encoding.Default.GetBytes(
                                                                idCreditoActual)));
                                                }
                                                if (reasignaRecupera == 2)
                                                {
                                                    var idCreditoNuevo =
                                                        new RecuperacionDatos().GeneraNumeroCredito(
                                                            validator.ParseoTrama);

                                                    if (new RecuperacionDatos().ResignaDatosCredito(idCreditoActual,
                                                        idCreditoNuevo, validator.ParseoTrama, out error))
                                                    {
                                                        var lstAmortizacionesActual =
                                                            new CreCredito().ObtenAmortizacionesCredito(idCreditoActual);
                                                        var fechaPrimerPagoActual =
                                                            lstAmortizacionesActual.First(me => me.No_Pago == 1)
                                                                .Dt_Fecha;

                                                        var lstAmortizacionesNuevo =
                                                            new CreCredito().ObtenAmortizacionesCredito(idCreditoNuevo);
                                                        var fechaPrimerPagoNuevo =
                                                            lstAmortizacionesNuevo.First(me => me.No_Pago == 1).Dt_Fecha;

                                                        Insertlog.InsertarLog(
                                                            Convert.ToInt16(Session["IdUserLogueado"]),
                                                            Convert.ToInt16(Session["IdRolUserLogueado"]),
                                                            Convert.ToInt16(Session["IdDepartamento"]),
                                                            //idRegionUsuario,idZona
                                                            "SOLICITUD DE CREDITO",
                                                            "ACTUALIZACIÓN TABLA DE AMORTIZACIÓN",
                                                            idCreditoNuevo,
                                                            "", "Número Crédito Anterior: " + idCreditoActual,
                                                            "Fecha Primer Pago: " + fechaPrimerPagoActual,
                                                            "Fecha Primer Pago: " + fechaPrimerPagoNuevo);

                                                        Response.Redirect(
                                                            "../SupplierModule/CapturaDatosSolicitud.aspx?Token=" +
                                                            Convert.ToBase64String(
                                                                System.Text.Encoding.Default.GetBytes(
                                                                    idCreditoNuevo)));
                                                    }
                                                    else
                                                    {
                                                        Insertlog.InsertarLog(
                                                            Convert.ToInt16(Session["IdUserLogueado"]),
                                                            Convert.ToInt16(Session["IdRolUserLogueado"]),
                                                            Convert.ToInt16(Session["IdDepartamento"]),
                                                            "SOLICITUD DE CREDITO",
                                                            "REASIGNAR", txtServiceCode.Text,
                                                            error, "", "", "");

                                                        lblCheckResult.Text =
                                                            string.Format(
                                                                HttpContext.GetGlobalResourceObject("DefaultResource",
                                                                    "CaptchaResultRPUInvalid")
                                                                    as
                                                                    string
                                                                , txtServiceCode.Text, error);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Session["ValidRPU"] = txtServiceCode.Text;
                                                Session["PARSEO_TRAMA"] = validator.ParseoTrama;
                                                Response.Redirect("../SupplierModule/CapturaDatosSolicitud.aspx");
                                            }
                                    }
                                }
                                else
                                {
                                    Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                        Convert.ToInt16(Session["IdRolUserLogueado"]),
                                        Convert.ToInt16(Session["IdDepartamento"]), "SOLICITUD DE CREDITO",
                                        "REGIONALIZACION", txtServiceCode.Text,
                                        string.Format(
                                            "El RPU ingresado es válido para el programa, pero pertenece a la Zona  {0},  de la Gerencia Regional FIDE {1}. Para ingresar esta solicitud de financiamiento deberá gestionar su alta como distribuidor en dicha zona FIDE",
                                            zonaFide.Dx_Nombre_Zona, zonaFide.Dx_Nombre_Region), "", "", "");

                                    lblCheckResult.Text =
                                        string.Format(
                                            HttpContext.GetGlobalResourceObject("DefaultResource",
                                                "CaptchaResultRPUInvalid") as string,
                                            txtServiceCode.Text,
                                            string.Format(
                                                "El RPU ingresado es válido para el programa, pero pertenece a la Zona  {0},  de la Gerencia Regional FIDE {1}. Para ingresar esta solicitud de financiamiento deberá gestionar su alta como distribuidor en dicha zona FIDE",
                                                zonaFide.Dx_Nombre_Zona, zonaFide.Dx_Nombre_Region));
                                }
                            }
                            else
                            {
                                lblCheckResult.Text = string.Format("La zona {0} de CFE no coincide con las Zonas de FIDE", zonaCfe.Zone);
                            }
                        }
                        else
                        {
                            var rpuNoValido = CausaMotivoNosujetoCredito(causa);
                            if (rpuNoValido[0] != "")
                            {
                                Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                    Convert.ToInt16(Session["IdRolUserLogueado"]),
                                    Convert.ToInt16(Session["IdDepartamento"]), "VALIDACION RPU",
                                    rpuNoValido[0], txtServiceCode.Text,
                                    error, rpuNoValido[1], "", "");
                            }
                            lblCheckResult.Text =
                                string.Format(
                                    HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaResultRPUInvalid")
                                        as
                                        string
                                    , txtServiceCode.Text, error);
                        }

                        HyperLink1.Visible = false;
                    }
                    else
                    {
                        var rpuNoValido = CausaMotivoNosujetoCredito(causa);

                        if (rpuNoValido[0] != "")
                        {
                            Insertlog.InsertarLog(Convert.ToInt16(Session["IdUserLogueado"]),
                                Convert.ToInt16(Session["IdRolUserLogueado"]),
                                Convert.ToInt16(Session["IdDepartamento"]),
                                "VALIDACION RPU",
                                rpuNoValido[0], txtServiceCode.Text,
                                error, rpuNoValido[1], "", "");
                        }
                        lblCheckResult.Text =
                            string.Format(
                                HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaResultRPUInvalid") as
                                    string
                                , txtServiceCode.Text, error);

                        if (causa[0] != "1" || causa[0] != "3" || causa[0] != "5")
                        {
                            HyperLink1.Visible = true;
                        }
                    }

                    // Generar nueva imagen para que no pueda ser reutilizada
                    MyCaptcha1.TryNew();

                    // limpiar formulario?
                    txtServiceCode.Text =
                        HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaClear") as string;
                }
                else
                    lblCheckResult.Text =
                        HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaResultTextInvalid") as string;
            }
            catch (Exception)
            {
                lblCheckResult.Text = HttpContext.GetGlobalResourceObject("DefaultResource", "CaptchaFatal") as string;
            }
        }

        public string[] CausaMotivoNosujetoCredito(string[] causa)
        {
            var motivo = new[] {"","" };

            switch (causa[0])
            {
                case "1":
                    motivo[0] = @"YA CUENTA CON CRÉDITO ACTIVO";
                    break;
                case "2":
                    motivo[0] = @"SIN CONEXIÓN A LA TRAMA";
                    motivo[1] = causa[1];
                    break;
                case "3":
                    motivo[0] = @"ADEUDOS";
                    break;
                case "4":
                    motivo[0] = @"ESTATUS DEL USUARIO";
                    motivo[1] = causa[1];
                    break;
                case "5":
                    motivo[0] = @"TARIFA NO VALIDA";
                    break;
                case "6":
                    motivo[0] = "NO CUENTA CON PERIODO MINIMO EN CFE";
                    motivo[1] = causa[1];
                    break;
                case "7":
                    motivo[0] = @"NO CUENTA CON PERIODOS MINIMOS PARA SOLICITUD";
                    motivo[1] = causa[1];
                    break;
                case "8":
                    motivo[0] = @"SIN CONSUMO";
                    motivo[1] = causa[1];
                    break;
                default:
                    motivo[0] = "";
                    break;

            }
            return motivo;
        }
    }
}
