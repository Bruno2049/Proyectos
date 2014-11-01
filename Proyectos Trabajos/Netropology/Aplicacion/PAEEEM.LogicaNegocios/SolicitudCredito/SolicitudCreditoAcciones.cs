using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.Entidades.Utilizables;
using PAEEEM.AccesoDatos;
using PAEEEM.Entidades.CancelarRechazar;
using PAEEEM.LogicaNegocios.LOG;

namespace PAEEEM.LogicaNegocios.SolicitudCredito
{
    public class SolicitudCreditoAcciones
    {
        private static readonly SolicitudCreditoAcciones _classInstance = new SolicitudCreditoAcciones();

        public static SolicitudCreditoAcciones ClassInstance
        {
            get { return _classInstance; }
        }


        public bool InsertaCreditoLog(K_CREDITO_LOG creditoLog)
        {
            var creditoInsert = CREDITO_LOG.ClassInstance.Agregar(creditoLog);

            return creditoInsert.No_RPU != null;
        }


        ////add by @l3x////
        public static CANCELAR_RECHAZAR Agregarcancel(CANCELAR_RECHAZAR cancelado)
        {
            CANCELAR_RECHAZAR cancel;
            using (var a = new Repositorio<CANCELAR_RECHAZAR>())
            {
                cancel = a.Agregar(cancelado);
            }
            return cancel;
        }

        public static int IncrementarFonDisponibleEincentivo(decimal montoSolici, string credit)
        {
            var iResult = 0;
            decimal resul;
            if (credit != "")
            {
                K_CREDITO_COSTO obj;
                using (var r = new Repositorio<K_CREDITO_COSTO>())
                {
                    obj = r.Extraer(c => c.No_Credito == credit);
                }
                var cost = (decimal)obj.Mt_Costo;

                K_CREDITO_DESCUENTO obj2;
                using (var r = new Repositorio<K_CREDITO_DESCUENTO>())
                {
                    obj2 = r.Extraer(d => d.No_Credito == credit);
                }
                var des = (decimal)obj2.Mt_Descuento;
                resul = cost + des;

            }
            else
            {
                resul = 0;
            }

            try
            {

                var pro = Programa.Obtener();

                pro.Mt_Fondo_Disponible = pro.Mt_Fondo_Disponible + montoSolici;
                pro.Mt_Fondo_Disponible_Incentivo = pro.Mt_Fondo_Disponible_Incentivo + resul;


                if (Programa.Actualizar(pro))
                {
                    iResult++;
                }

            }
            catch (Exception e)
            {
            }

            return iResult;
        }

        public static int DecrementarrFonDisponibleEincentivo(decimal montoSolici, string credit)
        {
            var iResult = 0;
            decimal resul;
            if (credit != "")
            {
                K_CREDITO_COSTO obj;
                using (var r = new Repositorio<K_CREDITO_COSTO>())
                {
                    obj = r.Extraer(c => c.No_Credito == credit);
                }
                var cost = (decimal)obj.Mt_Costo;

                K_CREDITO_DESCUENTO obj2;
                using (var r = new Repositorio<K_CREDITO_DESCUENTO>())
                {
                    obj2 = r.Extraer(d => d.No_Credito == credit);
                }
                var des = (decimal)obj2.Mt_Descuento;
                resul = cost + des;

            }
            else
            {
                resul = 0;
            }

            try
            {

                var pro = Programa.Obtener();

                pro.Mt_Fondo_Disponible = pro.Mt_Fondo_Disponible - montoSolici;
                pro.Mt_Fondo_Disponible_Incentivo = pro.Mt_Fondo_Disponible_Incentivo - resul;


                if (Programa.Actualizar(pro))
                {
                    iResult++;
                }

            }
            catch (Exception e)
            {
            }

            return iResult;
        }

        public static List<PlantillaMotivoCancel> DatosMotivo(string credit)
        {
            var dat = MotivoCancelacionPlantilla.datosemail(credit);

            return dat;
        }

        ////<------////


        public static K_DATOS_PYME GuarDatosPyme(K_DATOS_PYME datos)
        {
            var resultado = CapturaSolicitud.GuardDatosPyme(datos);

            return resultado;
        }

        public static K_DATOS_PYME BuscaDatosPyme(string noRpu)
        {
            var resultado = CapturaSolicitud.BuscaDatosPyme(noRpu);

            return resultado;
        }

        public static bool ActualizaDatosPyme(K_DATOS_PYME pyme)
        {
            var resultado = CapturaSolicitud.ActualizaDatosPyme(pyme);

            return resultado;
        }

        public static K_CREDITO_TEMP InsertaCreditoTemp(K_CREDITO_TEMP creditoTemp)
        {
            var resultado = CapturaSolicitud.InsertaCreditoTemp(creditoTemp);

            return resultado;
        }

        public static List<CLI_HORARIOS_OPERACION> ObtenHorariosOperacion(string noCredito)
        {
            var resultado = CapturaSolicitud.ObtenHorariosOperacion(noCredito);

            return resultado;
        }

        public static List<CLI_HORARIOS_OPERACION> ObtenHorariosXTipoOperacion(string noCredito, int tipoOperacion)
        {
            var resultado = CapturaSolicitud.ObtenHorariosXTipoOperacion(noCredito, tipoOperacion);

            return resultado;
        }

        public static H_OPERACION_TOTAL ObtenTotalHorariosXTipoOperacion(string noCredito, int tipoOperacion)
        {
            var resultado = CapturaSolicitud.ObtenTotalHorariosXTipoOperacion(noCredito, tipoOperacion);

            return resultado;
        }

        public static bool ActualizaHorarioOperacion(List<CLI_HORARIOS_OPERACION> lsthorario,
            H_OPERACION_TOTAL totHorasOperacionTotal)
        {
            var actualiza = false;
            var inserto = false;

            foreach (var cHorariosOperacion in lsthorario)
            {
                var existe = CapturaSolicitud.ObtenHorariosOperacionPorDiaOperacion(cHorariosOperacion.No_Credito,
                    cHorariosOperacion.IDTIPOHORARIO, cHorariosOperacion.ID_DIA_OPERACION);
                if (existe != null)
                {
                    cHorariosOperacion.IDHORARIOOPERACION = existe.IDHORARIOOPERACION;
                    actualiza = CapturaSolicitud.ActualizaHorarioOperacion(cHorariosOperacion);
                }
                else
                {
                    if (CapturaSolicitud.InsertaHorariosOperacion(cHorariosOperacion) != null)
                        actualiza = true;
                }
            }

            if (!actualiza) return false;

            var existeTotHoras =
                CapturaSolicitud.ObtenTotalHorariosOperacion_negocio(totHorasOperacionTotal.No_Credito);
            if (existeTotHoras != null)
            {
                totHorasOperacionTotal.ID_H_OPERACION_TOT = existeTotHoras.ID_H_OPERACION_TOT;
                if (CapturaSolicitud.ActualizaTotalHorasOperacion(totHorasOperacionTotal))
                    inserto = true;
            }
            else
            {
                if (CapturaSolicitud.InsertaTotalHorasOperacion(totHorasOperacionTotal) != null)
                    inserto = true;
            }

            return inserto;
        }

        public static bool ActualizaHorarioOperacion_IdCredSust(List<CLI_HORARIOS_OPERACION> lsthorario,
            H_OPERACION_TOTAL totHorasOperacionTotal)
        {
            var actualiza = false;
            var inserto = false;

            foreach (var cHorariosOperacion in lsthorario)
            {
                var existe =
                    CapturaSolicitud.ObtenHorariosOperacionPorDiaOperacion_IdCredSust(cHorariosOperacion.No_Credito,
                        cHorariosOperacion.IDTIPOHORARIO, cHorariosOperacion.ID_DIA_OPERACION,
                        (int) cHorariosOperacion.Id_Credito_Sustitucion != 0
                            ? (int) cHorariosOperacion.Id_Credito_Sustitucion
                            : 0);

                if (existe != null)
                {
                    cHorariosOperacion.IDHORARIOOPERACION = existe.IDHORARIOOPERACION;
                    actualiza = CapturaSolicitud.ActualizaHorarioOperacion(cHorariosOperacion);
                }
                else
                {
                    if (CapturaSolicitud.InsertaHorariosOperacion(cHorariosOperacion) != null)
                        actualiza = true;
                }
            }

            if (actualiza)
            {
                var existeTotHoras =
                    CapturaSolicitud.ObtenTotalHorariosOperacion_idCredSust(totHorasOperacionTotal.No_Credito,
                        (int) totHorasOperacionTotal.Id_Credito_Sustitucion);

                if (existeTotHoras != null)
                {
                    totHorasOperacionTotal.ID_H_OPERACION_TOT = existeTotHoras.ID_H_OPERACION_TOT;

                    if (CapturaSolicitud.ActualizaTotalHorasOperacion(totHorasOperacionTotal))
                        inserto = true;
                }
                else
                {
                    if (CapturaSolicitud.InsertaTotalHorasOperacion(totHorasOperacionTotal) != null)
                        inserto = true;
                }
            }

            return inserto;
        }

        public static bool ActualizaHorarioOperacion_IdCredProducto(List<CLI_HORARIOS_OPERACION> lsthorario,
            H_OPERACION_TOTAL totHorasOperacionTotal)
        {
            var actualiza = false;
            var inserto = false;

            foreach (var cHorariosOperacion in lsthorario)
            {
                var existe =
                    CapturaSolicitud.ObtenHorariosOperacionPorDiaOperacion_IdCredProd(cHorariosOperacion.No_Credito,
                        cHorariosOperacion.IDTIPOHORARIO, cHorariosOperacion.ID_DIA_OPERACION,
                        (int) cHorariosOperacion.ID_CREDITO_PRODUCTO, cHorariosOperacion.IDCONSECUTIVO);

                if (existe != null)
                {
                    cHorariosOperacion.IDHORARIOOPERACION = existe.IDHORARIOOPERACION;
                    actualiza = CapturaSolicitud.ActualizaHorarioOperacion(cHorariosOperacion);
                }
                else
                {
                    if (CapturaSolicitud.InsertaHorariosOperacion(cHorariosOperacion) != null)
                        actualiza = true;
                }
            }

            if (actualiza)
            {
                var existeTotHoras =
                    CapturaSolicitud.ObtenTotalHorariosOperacion_idCredProd(totHorasOperacionTotal.No_Credito,
                        (int) totHorasOperacionTotal.ID_CREDITO_PRODUCTO, totHorasOperacionTotal.IDCONSECUTIVO,
                        totHorasOperacionTotal.IDTIPOHORARIO);

                if (existeTotHoras != null)
                {
                    totHorasOperacionTotal.ID_H_OPERACION_TOT = existeTotHoras.ID_H_OPERACION_TOT;
                    if (CapturaSolicitud.ActualizaTotalHorasOperacion(totHorasOperacionTotal))
                        inserto = true;
                }
                else
                {
                    if (CapturaSolicitud.InsertaTotalHorasOperacion(totHorasOperacionTotal) != null)
                        inserto = true;
                }
            }

            return inserto;
        }

        public static bool ActualizaFotoAltaEquipo(string nnumCredito, string tipo, byte[] foto)
        {
            //var datosActualiar = CapturaSolicitud.ObtenDatoskCreditoProducto(idCredProducto);
            //if (tipo == "Nuevo")
            //{
            //    datosActualiar.Fotografia_equipo_Nuevo = foto;
            //}
            //if(tipo == "Viejo")
            //{
            //    datosActualiar.Fotografia_equipo_Viejo= foto;
            //}
            //  return CapturaSolicitud.actualizaFoto_IdCredProducto(datosActualiar);
            return false;
        }

        public static bool ActualizaFotoBajaEquipo(int idCredSust, byte[] foto)
        {
            //var datosActualiar = CapturaSolicitud.ObtenDatoskCreditoSustitucion(idCredSust);

            //    datosActualiar.Fotografia_equipo= foto;

            //return CapturaSolicitud.actualizaFoto_IdCredSustitucion(datosActualiar);
            return false;
        }

        public static CLI_Cliente InsertaCliente(CLI_Cliente cliente)
        {
            var newCliente = Captura.InsertaCliente(cliente);

            return newCliente;
        }

        public static CLI_Negocio InsertaNegocio(CLI_Negocio negocio)
        {
            var newNegocio = Captura.InsertaNegocio(negocio);

            return newNegocio;
        }

        public static bool ActualizaDatosInfoGeneral(Cliente cliente)
        {
            var cliCliente = cliente.DatosCliente;
            var idProveedor = cliCliente.Id_Proveedor;
            var idSucursal = cliCliente.Id_Branch;
            var idCliente = cliCliente.IdCliente;
            var idNegocio = cliCliente.CLI_Negocio.First().IdNegocio;

            var actualizo = false;
            var actualiza = Captura.ActualizaCliente(cliCliente);

            var referenciaMatrimonial = ObtenReferenciasNotarialPorTipo(idProveedor, idSucursal, idCliente, idNegocio, 8);
            if (cliente.DatosCliente.Cve_Estado_Civil == 1 && referenciaMatrimonial != null)
            //soltero y tenia referencia Persona Casado
            {
                Captura.EliminaReferenciaNotarial(referenciaMatrimonial);
            }
            var newDatosCliente = new Cliente();

            if (!actualiza) return false;

            newDatosCliente.DatosCliente = cliente.DatosCliente;
            foreach (var direccionActualiza in cliente.DireccionesCliente)
            {
                direccionActualiza.IdCliente = cliente.IdCliente;

                var direccion = Captura.ObtenDireccionCliente(idProveedor, idSucursal, idCliente, idNegocio,
                    Convert.ToByte(direccionActualiza.IdTipoDomicilio));

                if (direccion != null)
                {
                    direccionActualiza.IdDireccion = direccion.IdDireccion;
                    Captura.ActualizaDireccion(direccionActualiza);
                }

                else
                {
                    Captura.InsertaDirecciones(direccionActualiza);
                }
                actualizo = true;
            }
            return actualizo;
        }

        public static Cliente ActualizaInfoComplementaria(Cliente cliente,int idUsuario, int idRol, int idDepartamento)
        {
            var cliCliente = cliente.DatosCliente;
            var idProveedor = cliCliente.Id_Proveedor;
            var idSucursal = cliCliente.Id_Branch;
            var idCliente = cliCliente.IdCliente;
            var idNegocio = cliCliente.CLI_Negocio.First().IdNegocio;

            var lstRefNorariales = new List<CLI_Referencias_Notariales>();

            //Referencias del cliente
            var datosReferencias = Captura.ObtenReferenciasCliente(idProveedor, idSucursal, idCliente, idNegocio);

            if (datosReferencias.Count > 0)
            {
                if (cliente.RepresentanteLegal != null)
                {
                    cliente.RepresentanteLegal.IdCliente = cliente.IdCliente;

                    var datosRepLegal =
                        datosReferencias.FirstOrDefault(me => me.IdCliente == cliente.RepresentanteLegal.IdCliente &&
                                                              me.IdTipoReferencia ==
                                                              cliente.RepresentanteLegal.IdTipoReferencia);

                    cliente.RepresentanteLegal.IdReferencia = datosRepLegal.IdReferencia;
                    Captura.ActualizaReferencia(cliente.RepresentanteLegal);
                }

                if (cliente.RepLegalObligadoSolidario != null)
                {

                    cliente.RepLegalObligadoSolidario.IdCliente = cliente.IdCliente;

                    var datosRepLegalOS =
                        datosReferencias.FirstOrDefault(
                            me => me.IdCliente == cliente.RepLegalObligadoSolidario.IdCliente &&
                                  me.IdTipoReferencia == cliente.RepLegalObligadoSolidario.IdTipoReferencia);

                    cliente.RepLegalObligadoSolidario.IdReferencia = datosRepLegalOS.IdReferencia;
                    Captura.ActualizaReferencia(cliente.RepLegalObligadoSolidario);
                }

                cliente.ObligadoSolidario.IdCliente = cliente.IdCliente;

                var datosRefOblidadoSol =
                    datosReferencias.FirstOrDefault(me => me.IdCliente == cliente.ObligadoSolidario.IdCliente &&
                                                          me.IdTipoReferencia ==
                                                          cliente.ObligadoSolidario.IdTipoReferencia);

                cliente.ObligadoSolidario.IdReferencia = datosRefOblidadoSol.IdReferencia;
                Captura.ActualizaReferencia(cliente.ObligadoSolidario);
            }
            else
            {
                cliente.RepresentanteLegal.IdCliente = cliente.IdCliente;
                var newReferencia = Captura.InsertaReferenciaCliente(cliente.RepresentanteLegal);
                cliente.RepresentanteLegal = newReferencia;

                cliente.ObligadoSolidario.IdCliente = cliente.IdCliente;
                newReferencia = Captura.InsertaReferenciaCliente(cliente.ObligadoSolidario);
                cliente.ObligadoSolidario = newReferencia;

                if (cliente.RepLegalObligadoSolidario != null)
                {
                    cliente.RepLegalObligadoSolidario.IdCliente = cliente.IdCliente;
                    newReferencia = Captura.InsertaReferenciaCliente(cliente.RepLegalObligadoSolidario);
                    cliente.RepLegalObligadoSolidario = newReferencia;
                }

                Insertlog.InsertarLog(idUsuario, idRol, idDepartamento, "SOLICITUD DE CREDITO",
                            "CAPTURA COMPLEMENTARIA CLIENTE", ObtenCreditoPorIdCliente(cliente.IdCliente).No_Credito,
                            "", "", "", "");
            }


            //DIRECCION OBLIGADO SOLIDARIO
            cliente.ObligadoSolidario.IdCliente = cliente.IdCliente;

            var dirObligadoSolidario = cliente.DireccionesCliente.FirstOrDefault(me => me.IdTipoDomicilio == 3);

            if (dirObligadoSolidario != null)
            {
                dirObligadoSolidario.IdCliente = cliente.IdCliente;
                dirObligadoSolidario.IdReferencia = cliente.ObligadoSolidario.IdReferencia;
                var direccionObligadosolidario = Captura.ObtenDireccionCliente(idProveedor, idSucursal, idCliente, idNegocio,
                    Convert.ToByte(dirObligadoSolidario.IdTipoDomicilio));

                if (direccionObligadosolidario != null)
                {
                    dirObligadoSolidario.IdDireccion = direccionObligadosolidario.IdDireccion;
                    Captura.ActualizaDireccion(dirObligadoSolidario);
                }
                else
                {
                    Captura.InsertaDirecciones(dirObligadoSolidario);
                    // cliente.ObligadoSolidario.IdDireccion = newDireccion.IdDireccion;
                }
            }

            //Referencias Notariales del cliente
            foreach (var referenciaNotarial in cliente.ReferenciasNotariales)
            {
                CLI_Referencias_Notariales newRefNotarial = null;
                referenciaNotarial.IdCliente = cliente.IdCliente;
                var datosReferenciasNotariales = Captura.ObtenReferenciasNotariales(idProveedor, idSucursal, idCliente, idNegocio);

                var datosActualRefenciaPorId =
                    datosReferenciasNotariales.FirstOrDefault(
                        me =>
                            me.IdCliente == referenciaNotarial.IdCliente &&
                            me.IdTipoReferencia == referenciaNotarial.IdTipoReferencia);

                if (datosActualRefenciaPorId != null)
                {
                    referenciaNotarial.IdReferencia_Nota = datosActualRefenciaPorId.IdReferencia_Nota;
                    Captura.ActualizaReferenciaNotarial(referenciaNotarial);
                }
                else
                {

                    newRefNotarial = Captura.InsertaReferenciasNotariales(referenciaNotarial);
                }

                lstRefNorariales.Add(newRefNotarial);
            }

            cliente.ReferenciasNotariales = lstRefNorariales;

            return cliente;
        }

        public static CLI_Cliente ObtenCliente(int idProveedor, int idSucursal, int idCliente)
        {
            var cliente = Captura.ObtenCliente(idProveedor, idSucursal, idCliente);

            return cliente;
        }

        public static CLI_Negocio ObtenNegocioCliente(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            var negocio = Captura.ObtenNegocioCliente(idProveedor, idSucursal, idCliente, idNegocio);

            return negocio;
        }

        public static Cliente ObtenClienteComplejo(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            var cliCliente = Captura.ObtenCliente(idProveedor, idSucursal, idCliente);
            cliCliente.CLI_Negocio = new List<CLI_Negocio> { Captura.ObtenNegocioCliente(idProveedor, idSucursal, idCliente, idNegocio) };                
            var cliente = new Cliente
                          {
                              IdCliente = idCliente,
                              DatosCliente = cliCliente,
                              DireccionesCliente = Captura.ObtenDireccionesCliente(idProveedor, idSucursal, idCliente, idNegocio),
                              ReferenciasNotariales = Captura.ObtenReferenciasNotariales(idProveedor, idSucursal, idCliente, idNegocio)
                          };

            var lstRefClientes = Captura.ObtenReferenciasCliente(idProveedor, idSucursal, idCliente, idNegocio);

            if (lstRefClientes == null || lstRefClientes.Count <= 0) return cliente;

            cliente.RepresentanteLegal = lstRefClientes.FirstOrDefault(me => me.IdTipoReferencia == 1);
            cliente.ObligadoSolidario = lstRefClientes.FirstOrDefault(me => me.IdTipoReferencia == 2);
            cliente.RepLegalObligadoSolidario = lstRefClientes.FirstOrDefault(me => me.IdTipoReferencia == 3);

            return cliente;
        }

        public static CRE_Credito ObtenCredito(string noCredito)
        {
            var datosCredito = Captura.ObtenCredito(noCredito);

            return datosCredito;
        }

        public static CRE_Credito ObtenCreditoPorIdCliente(int  idCliente)
        {
            var datosCredito = Captura.ObtenCreditoPorIdCliente(idCliente);

            return datosCredito;
        }
        
        #region Captura Complementaria Credito Sustitucion

        public static K_CREDITO_SUSTITUCION ObtenerComplementarioSustitucion(
            K_CREDITO_SUSTITUCION oSustitucion)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var oComplementario = (from c in context.K_CREDITO_SUSTITUCION
                    where c.No_Credito == oSustitucion.No_Credito
                          && c.Id_Credito_Sustitucion == oSustitucion.Id_Credito_Sustitucion
               select c).FirstOrDefault();

                return oComplementario;
            }

        }

        public static K_CREDITO_SUSTITUCION ObtenerComplementarioSustitucionCapturado(string noCredito)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var oComplementario = (from c in context.K_CREDITO_SUSTITUCION
                                       where c.No_Credito == noCredito
                                             && c.Id_Centro_Disp != null
                                             && c.Fg_Tipo_Centro_Disp !=null
                                       select c).FirstOrDefault();

                return oComplementario;
            }

        }

      
        #endregion

        public static bool ValidaDirecciones(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            var resultado = Captura.ValidaDirecciones(idProveedor, idSucursal, idCliente, idNegocio);

            return resultado;
        }

        public static bool ValidaReferenciasCliente(int idProveedor, int idSucursal, int idCliente, int idNegocio)
        {
            var resultado = Captura.ValidaReferenciasCliente(idProveedor, idSucursal, idCliente, idNegocio);

            return resultado;
        }

        public static string ObtenNombreComercial(string noCrdito)
        {
            string nombreComercial;
            using (var context = new PAEEEM_DESAEntidades())
            {
                nombreComercial = (from c in context.CRE_Credito
                    join cl in context.CLI_Cliente on c.IdCliente equals cl.IdCliente
                    where c.No_Credito.Equals(noCrdito)
                    select
                        cl.Cve_Tipo_Sociedad.Value != 1
                            ? cl.Razon_Social
                            : cl.Nombre + " " + cl.Ap_Paterno + " " + cl.Ap_Materno
                    ).FirstOrDefault().ToString(CultureInfo.InvariantCulture);
            }

            return nombreComercial;
        }

        public static bool ActualizarCreditoSustitucion(K_CREDITO_SUSTITUCION oSustitucion)
        {
            bool actualizo = false;
            using (var context = new PAEEEM_DESAEntidades())
            {
                var datosSustitucion = (from c in context.K_CREDITO_SUSTITUCION
                                       where c.Id_Credito_Sustitucion == oSustitucion.Id_Credito_Sustitucion
                                       select c).FirstOrDefault();

                if (datosSustitucion != null)
                {
                    datosSustitucion.Id_Centro_Disp = oSustitucion.Id_Centro_Disp;
                    datosSustitucion.Fg_Tipo_Centro_Disp = oSustitucion.Fg_Tipo_Centro_Disp;
                    datosSustitucion.Dx_Marca = oSustitucion.Dx_Marca;
                    datosSustitucion.Dx_Color = oSustitucion.Dx_Color;
                    datosSustitucion.Dx_Modelo_Producto = oSustitucion.Dx_Modelo_Producto;
                    datosSustitucion.Id_Pre_Folio = datosSustitucion.Id_Pre_Folio ?? oSustitucion.Id_Pre_Folio;
                    datosSustitucion.Dx_Antiguedad = oSustitucion.Dx_Antiguedad;
                    context.SaveChanges();
                    actualizo = true;
                }

                return actualizo;
            }
        }

        public static int ObtenerConsecutivoPreFolio(string noCredito, int idTecnologia)
        {
            var consecutivo = 0;
            using (var context = new PAEEEM_DESAEntidades())
            {

                var preFolio = (from p in context.K_CREDITO_SUSTITUCION
                                where p.No_Credito == noCredito
                                      && p.Cve_Tecnologia == idTecnologia
                                      && p.Id_Pre_Folio != null
                                select p.Id_Pre_Folio
                    ).ToList();

                if (preFolio.Count == 0) return consecutivo + 1;

                var mayor = preFolio.Select(pre_folio => pre_folio.Split('-')).Select(partes => int.Parse(partes[1])).Concat(new[] { 0 }).Max();

                consecutivo = mayor;
            }
            return consecutivo + 1;
        }

        public static CAT_TECNOLOGIA CreditoRequiereCayD(string noCredito)
        {
            
            using (var context = new PAEEEM_DESAEntidades())
            {
              return  (from k in context.K_CREDITO_SUSTITUCION
                    join t in context.CAT_TECNOLOGIA on k.Cve_Tecnologia equals t.Cve_Tecnologia
                    where k.No_Credito.Equals(noCredito)
                          && t.Cve_Esquema == 0
                    select t
                    ).FirstOrDefault();
            }

            
        }

        public static K_CREDITO_SUSTITUCION ObtenerCayDPorCredito(string noCredito)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                return (from k in context.K_CREDITO_SUSTITUCION
                                              where k.No_Credito.Equals(noCredito)
                                              && k.Id_Centro_Disp != null 
                                              && k.Fg_Tipo_Centro_Disp != null
                                              select k).FirstOrDefault();
                
            }
        }


        public static void ActualizarCreditoSustitucionCayD(int idDispo, string tipoDispo, string noCredito)
        {
            using (var context = new PAEEEM_DESAEntidades())
            {
                var lstCreditosSustitucion = (from k in context.K_CREDITO_SUSTITUCION
                        join t in context.CAT_TECNOLOGIA on k.Cve_Tecnologia equals t.Cve_Tecnologia
                        where k.No_Credito.Equals(noCredito)
                              && t.Cve_Esquema == 0
                        select k
                      ).ToList();

                foreach (var kCreditoSustitucion in lstCreditosSustitucion)
                {
                    kCreditoSustitucion.Id_Centro_Disp = idDispo;
                    kCreditoSustitucion.Fg_Tipo_Centro_Disp = tipoDispo;
                    context.SaveChanges();
                }
            }
        }

        public static ConsultaCrediticia ObtenConsultaPruebas()
        {
            var consultaCrediticia = new ConsultaCrediticia();
            var r = new Random(DateTime.Now.Millisecond);
            var aleatorio = r.Next(1000000000);

            consultaCrediticia.Folio = "0" + aleatorio;
            consultaCrediticia.Mop =
                new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 5 && p.IDSECCION == 16).VALOR;

            return consultaCrediticia;
        }


        public static CLI_Referencias_Notariales ObtenReferenciasNotarialPorTipo(int idProveedor, int idSucursal,
          int idCliente, int idNegocio, int idTipoRef)
        {
            var refNotariales = Captura.ObtenReferenciaNotarialPorTipo(idProveedor, idSucursal, idCliente, idNegocio, idTipoRef);
            return refNotariales;
        }

        public static MOTIVOS_RECHAZOS_CANCELACIONES ObtenEstatusCreditoCancelarRechazar(int idMotivo)
        {
            var datosMotivos = CapturaSolicitud.ObtenEstatusCreditoCancelarRechazar(idMotivo);
            return datosMotivos;
        }


    }
}


