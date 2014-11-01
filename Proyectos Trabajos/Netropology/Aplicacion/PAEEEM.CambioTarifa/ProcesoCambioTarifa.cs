using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using PAEEEM.AccesoDatos.Operacion_Datos;
using PAEEEM.Captcha;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.Credito;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.LogicaNegocios.Trama;
using PAEEEM.Helpers;
using System.Net.Mail;
using System.Configuration;


namespace PAEEEM.CambioTarifa
{
    public class ProcesoCambioTarifa
    {
        public void ValidaCreditosCambioTarifa()
        {
            var lstCreCambioTarifa = new CambioTarifaDal().ObtenCreditosCambioTarifa();
            var validator = new CodeValidator();

            if (lstCreCambioTarifa != null && lstCreCambioTarifa.Count > 0)
            {
                foreach (var creditoCambioTarifa in lstCreCambioTarifa)
                {
                    var error = "";
                    var estado = "";

                    if (validator.ValidateServiceCodeEdit(creditoCambioTarifa.RpuNuevo, out error, ref estado))
                    {
                        var parseo = validator.ParseoTrama;
                        var credito = blCredito.Obtener(creditoCambioTarifa.IdCredito);
                        credito.RPU = creditoCambioTarifa.RpuNuevo;
                        credito.Id_Trama = parseo.idResponseData;
                        blCredito.Actualizar(credito);

                        ActualizaDirecciones((int)credito.Id_Proveedor, (int)credito.Id_Branch,
                                             (int)credito.IdCliente, (int)credito.IdNegocio,
                                             creditoCambioTarifa.RpuNuevo);
                        InsertaSubestacion(creditoCambioTarifa, parseo, credito.Consumo_Promedio != null, (DateTime)credito.Fecha_Pendiente);

                        var actualiza = new CambioTarifaDal().ActualizaCambioRPU(creditoCambioTarifa.IdCredito);

                        EnviaEmailExitoso(creditoCambioTarifa);
                    }
                }
            }
        }

        protected void ActualizaDirecciones(int idProveedor, int idSucursal, int idCliente, int idNegocio, string rpu)
        {
            var lstDirecciones = Captura.ObtenDireccionesCliente(idProveedor, idSucursal, idCliente, idNegocio);

            if (lstDirecciones != null && lstDirecciones.Count > 0)
            {
                foreach (var dirDireccionese in lstDirecciones)
                {
                    dirDireccionese.RPU = rpu;
                    Captura.ActualizaDireccion(dirDireccionese);
                }
            }
        }

        protected void InsertaSubestacion(CreditoCambioTarifa cambioTarifa, ParseoTrama parseoNuevo, bool esNuevoPaeeem, DateTime fechaConsulta)
        {                       
            string tarifaOrigen = "";
            int cvePeriodoOrigen = 0;
            string noCuentaOrigen = "";

            string tarifaNueva = "";
            int cvePeriodoNuevo = 0;
            string noCuentaNueva = "";
            int idTramaOrigen = 0;
            int idTramaNueva = 0;

            if (esNuevoPaeeem)
            {
                var tramaAnterior = new OpEquiposAbEficiencia().ObtenTrama(cambioTarifa.IdCredito); 
                var parseoAnterior = new ParseoTrama(tramaAnterior);
                tarifaOrigen =
                    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;
                cvePeriodoOrigen =
                    int.Parse(
                        parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato);
                noCuentaOrigen =
                    parseoAnterior.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 160).Dato;
                idTramaOrigen = parseoAnterior.idResponseData;
            }
            else
            {
                var responseData = new CreCredito().ObtenResponseData(cambioTarifa.RpuActual, fechaConsulta);

                if (responseData != null)
                {
                    tarifaOrigen = responseData.Rate;
                    cvePeriodoOrigen = int.Parse(responseData.Periodof);
                    noCuentaOrigen = responseData.CN;
                    idTramaOrigen = (int)responseData.Id_Trama;
                }
            }

            tarifaNueva = parseoNuevo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;
            cvePeriodoNuevo =
                int.Parse(parseoNuevo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato);
            noCuentaNueva =
                parseoNuevo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 160).Dato;
            idTramaNueva = parseoNuevo.idResponseData;

            var subestacion = new SUBESTACIONES();
            subestacion.No_Credito = cambioTarifa.IdCredito;
            subestacion.RPU_Origen = cambioTarifa.RpuActual;
            subestacion.RPU_Nuevo = cambioTarifa.RpuNuevo;
            subestacion.Tarifa_Origen = tarifaOrigen;
            subestacion.Tarifa_Nueva = tarifaNueva;
            subestacion.Cve_Periodo_Orig = cvePeriodoOrigen;
            subestacion.Cve_Periodo_Nuevo = cvePeriodoNuevo;
            subestacion.No_Cuenta_Origen = noCuentaOrigen;
            subestacion.No_Cuenta_Nueva = noCuentaNueva;
            subestacion.IdTrama_Orig = idTramaOrigen;
            subestacion.IdTrama_Nueva = idTramaNueva;
            subestacion.Fecha_Creacion = DateTime.Now;

            new CambioTarifaDal().InsertaSubestacion(subestacion);
        }

        protected void EnviaEmailExitoso(CreditoCambioTarifa credito)
        {
            var usUsuario = new AdministracionUsuarios().ObtenDatosUsuario(credito.UsuarioDistribuidor);
            string correoDist = "";

            if (usUsuario.Tipo_Usuario == GlobalVar.SUPPLIER_BRANCH)
            {
                var proveedor = new AdministracionUsuarios().ObtenProveedorBranch((int)usUsuario.Id_Departamento);
                correoDist = proveedor != null ? proveedor.Dx_Email_Repre : "";
            }
            else
            {
                var proveedor = new AdministracionUsuarios().ObtenProveedor((int)usUsuario.Id_Departamento);
                correoDist = proveedor != null ? proveedor.Dx_Email_Repre : "";
            }

            ValiacionRpuSicom("EmailRPUValido.html", credito.UsuarioDistribuidor, credito.IdCredito,
                                                correoDist, credito.RpuActual, credito.RpuNuevo);
        }

        private static string GetEmailTemplateBody(string templatename)
        {
            string TempaltePath = AppDomain.CurrentDomain.BaseDirectory;
            string FilePath = Path.Combine(TempaltePath, templatename);
            string Result = string.Empty;

            try
            {
                if (File.Exists(FilePath))
                {
                    Result = File.ReadAllText(FilePath);
                }
            }
            catch
            {
                
            }
            return Result;
        }

        public void ValiacionRpuSicom(string template, string usuarioDist, string noCredito, string correoDist, string rpuAnterior, string rpuNuevo)
        {
            var cuerpo = GetEmailTemplateBody(template);
            cuerpo = cuerpo.Replace("#USUARIO_DIST#", usuarioDist);
            cuerpo = cuerpo.Replace("#NO_CREDITO#", noCredito);
            cuerpo = cuerpo.Replace("#RPU_ANTERIOR#", rpuAnterior);
            cuerpo = cuerpo.Replace("#RPU_NUEVO#", rpuNuevo);

            var port = ConfigurationSettings.AppSettings["port"];
            var host = ConfigurationSettings.AppSettings["host"];
            var from = ConfigurationSettings.AppSettings["from"];

            var mail = new MailMessage();
            mail.To.Add(correoDist);
            mail.Subject = "Cambio de Tarifa Crédito: " + noCredito;
            mail.From = new MailAddress(from);
            mail.Body = cuerpo;
            mail.IsBodyHtml = true;

            var client = new SmtpClient
                {
                    Port = int.Parse(port),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = host,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("paeeem@fide.org", "paeeem@fide.org")
                };
            client.Send(mail);

            client.Dispose();

            //SendEmail(mail, null, settings[3], settings[2]);
        }
    }    
}
