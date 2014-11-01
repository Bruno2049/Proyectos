using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.Operacion_Datos;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.BussinessLayer;
using PAEEEM.Entidades;
using PAEEEM.AccesoDatos;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;
using PAEEEM.Helpers;
using PAEEEM.LogicaNegocios.Trama;

namespace PAEEEM.LogicaNegocios.SolicitudCredito
{
    public class RecuperacionDatos
    {
        public RecuperacionDatos()
        {}

        public bool ResignaDatosCredito(string idCreditoActual, string idCreditoNuevo, ParseoTrama parseo, out string errorCode)
        {
            var periodoConsumoFinal =
                    parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 159).Dato;
            var credito = SolicitudCreditoAcciones.ObtenCredito(idCreditoActual);

            var cliente = SolicitudCreditoAcciones.ObtenCliente((int)credito.Id_Proveedor, (int)credito.Id_Branch,
                                                                (int)credito.IdCliente);
            
            if (ValidaMontoMaximo(cliente.RFC, Convert.ToDecimal(credito.Monto_Solicitado)))
            {
                var nuevosDatosCredito = new CRE_Credito()
                    {
                        No_Credito = idCreditoNuevo,
                        IdCliente = credito.IdCliente,
                        Id_Proveedor = credito.Id_Proveedor,
                        Id_Branch = credito.Id_Branch,
                        IdNegocio = credito.IdNegocio,
                        RPU = credito.RPU,
                        Id_Trama = credito.Id_Trama,
                        No_Ahorro_Economico = credito.No_Ahorro_Economico,
                        Monto_Solicitado = credito.Monto_Solicitado,
                        Monto_Total_Pagar = credito.Monto_Total_Pagar,
                        Capacidad_Pago = credito.Capacidad_Pago,
                        No_Plazo_Pago = credito.No_Plazo_Pago,
                        Cve_Periodo_Pago = credito.Cve_Periodo_Pago,
                        Tasa_Interes = credito.Tasa_Interes,
                        Tasa_Fija = credito.Tasa_Fija,
                        CAT = credito.CAT,
                        Tasa_IVA = credito.Tasa_IVA,
                        Adquisicion_Sust = 0,
                        Fecha_Pendiente = DateTime.Now.Date,
                        Fecha_Ultmod = DateTime.Now.Date,
                        Usr_Ultmod = credito.Usr_Ultmod,
                        Tasa_IVA_Intereses = credito.Tasa_IVA_Intereses,
                        Tipo_Usuario = credito.Tipo_Usuario,
                        ID_Prog_Proy = credito.ID_Prog_Proy,
                        Cve_Estatus_Credito = (int) CreditStatus.PENDIENTE,
                        Gastos_Instalacion = credito.Gastos_Instalacion,
                        No_Ahorro_Consumo = credito.No_Ahorro_Consumo,
                        No_Ahorro_Demanda = credito.No_Ahorro_Demanda,
                        Consumo_Promedio = credito.Consumo_Promedio,
                        Demanda_Maxima = credito.Demanda_Maxima,
                        Factor_Potencia = credito.Factor_Potencia,
                        KVARs = credito.KVARs,
                        Solicitud_Reasignada = true
                    };


                var resultado = new OpEquiposAbEficiencia().InsertaCredito(nuevosDatosCredito);

                if (resultado != null)
                {
                    InsertaDatosCredito(idCreditoActual, resultado.No_Credito);
                    InsertaCalculosCredito(idCreditoActual, resultado.No_Credito);
                    //InsertaFotos(idCreditoActual, resultado.No_Credito);
                    GeneraNuevaTablaAmortizacion(resultado, periodoConsumoFinal, idCreditoActual);
                }

                errorCode = "";
                return true;
            }
            else
            {
                errorCode = "El cliente excede el monto máximo por RFC";
                return false;
            }
        }

        protected void InsertaDatosCredito(string idCreditoActual, string idCreditoNuevo)
        {
            var lstProductosAlta = new CreCredito().ObtenCreditoProductos(idCreditoActual);

            foreach (var kCreditoProducto in lstProductosAlta)
            {
                kCreditoProducto.No_Credito = idCreditoNuevo;
                var producto = new K_CREDITO_PRODUCTO
                    {
                        No_Credito = idCreditoNuevo,
                        Cve_Producto = kCreditoProducto.Cve_Producto,
                        No_Cantidad = kCreditoProducto.No_Cantidad,
                        Mt_Precio_Unitario = kCreditoProducto.Mt_Precio_Unitario,
                        Mt_Precio_Unitario_Sin_IVA = kCreditoProducto.Mt_Precio_Unitario_Sin_IVA,
                        Mt_Total = kCreditoProducto.Mt_Total,
                        Dt_Fecha_Credito_Producto = DateTime.Now.Date,
                        Cve_Producto_Capacidad = kCreditoProducto.Cve_Producto_Capacidad,
                        Mt_Gastos_Instalacion_Mano_Obra = kCreditoProducto.Mt_Gastos_Instalacion_Mano_Obra,
                        Grupo = kCreditoProducto.Grupo,
                        IdGrupo = kCreditoProducto.IdGrupo,
                        CapacidadSistema = kCreditoProducto.CapacidadSistema,
                        Incentivo = kCreditoProducto.Incentivo
                    };
                
                var newCreditoProducto = new CreCredito().InsertaProductoAlta(producto);

                if (newCreditoProducto != null)
                {
                    var lstHorariosOperacion = new CreCredito().ObtenHorariosOperacionProd(idCreditoActual,
                                                                                           kCreditoProducto
                                                                                               .ID_CREDITO_PRODUCTO);

                    if (lstHorariosOperacion.Count > 0)
                    {
                        foreach (var cliHorariosOperacion in lstHorariosOperacion)
                        {
                            var newHorarioOperacion = new CLI_HORARIOS_OPERACION();
                            newHorarioOperacion.No_Credito = idCreditoNuevo;
                            newHorarioOperacion.ID_DIA_OPERACION = cliHorariosOperacion.ID_DIA_OPERACION;
                            newHorarioOperacion.IDTIPOHORARIO = cliHorariosOperacion.IDTIPOHORARIO;
                            newHorarioOperacion.ID_CREDITO_PRODUCTO = newCreditoProducto.ID_CREDITO_PRODUCTO;
                            newHorarioOperacion.Hora_Inicio = cliHorariosOperacion.Hora_Inicio;
                            newHorarioOperacion.IDCONSECUTIVO = cliHorariosOperacion.IDCONSECUTIVO;
                            newHorarioOperacion.Horas_Laborables = cliHorariosOperacion.Horas_Laborables;

                            new CreCredito().InsertaHorarioOperacion(newHorarioOperacion);
                        }
                    }

                    var lstOperacionHotal = new CreCredito().ObtenOperacionTotalProd(idCreditoActual,
                                                                                     kCreditoProducto
                                                                                         .ID_CREDITO_PRODUCTO);
                    if (lstOperacionHotal.Count > 0)
                    {
                        foreach (var hOperacionTotal in lstOperacionHotal)
                        {
                            var newOperacionTotal = new H_OPERACION_TOTAL();
                            newOperacionTotal.No_Credito = idCreditoNuevo;
                            newOperacionTotal.IDTIPOHORARIO = hOperacionTotal.IDTIPOHORARIO;
                            newOperacionTotal.ID_CREDITO_PRODUCTO = newCreditoProducto.ID_CREDITO_PRODUCTO;
                            newOperacionTotal.HORAS_SEMANA = hOperacionTotal.HORAS_SEMANA;
                            newOperacionTotal.SEMANAS_AÑO = hOperacionTotal.SEMANAS_AÑO;
                            newOperacionTotal.HORAS_AÑO = hOperacionTotal.HORAS_AÑO;
                            newOperacionTotal.IDCONSECUTIVO = hOperacionTotal.IDCONSECUTIVO;

                            new CreCredito().InsertaOperacionTotal(newOperacionTotal);
                        }
                    }

                    var lstFotos = new CreCredito().ObtenFotosProducto(idCreditoActual,
                                                                       kCreditoProducto.ID_CREDITO_PRODUCTO);
                    if (lstFotos.Count > 0)
                    {
                        foreach (var creFotos in lstFotos)
                        {
                            var newFoto = new CRE_FOTOS
                            {
                                No_Credito = idCreditoNuevo,
                                idTipoFoto = creFotos.idTipoFoto,
                                idCreditoProducto = newCreditoProducto.ID_CREDITO_PRODUCTO,
                                idConsecutivoFoto = creFotos.idConsecutivoFoto,
                                Nombre = creFotos.Nombre,
                                Extension = creFotos.Extension,
                                Longitud = creFotos.Longitud,
                                Foto = creFotos.Foto,
                                Estatus = creFotos.Estatus,
                                AdicionadoPor = creFotos.AdicionadoPor,
                                FechaAdicion = DateTime.Now.Date
                            };

                            new CreCredito().InsertaFoto(newFoto);
                        }
                    }
                }
            }

            var lstProductosBaja = new CreCredito().ObtenCreditoSustitucion(idCreditoActual);

            foreach (var kCreditoSustitucion in lstProductosBaja)
            {
                //kCreditoSustitucion.No_Credito = idCreditoNuevo;
                var prefolio = kCreditoSustitucion.Id_Pre_Folio.Replace(idCreditoActual, idCreditoNuevo);

                var sustitucion = new K_CREDITO_SUSTITUCION
                    {
                        No_Credito = idCreditoNuevo,
                        Cve_Tecnologia = kCreditoSustitucion.Cve_Tecnologia,
                        No_Unidades = kCreditoSustitucion.No_Unidades,
                        Id_Centro_Disp = kCreditoSustitucion.Id_Centro_Disp,
                        Dt_Fecha_Credito_Sustitucion = DateTime.Now.Date,
                        Dx_Tipo_Producto = kCreditoSustitucion.Dx_Tipo_Producto,
                        Dx_Modelo_Producto = kCreditoSustitucion.Dx_Modelo_Producto,
                        No_Serial = kCreditoSustitucion.No_Serial,
                        Dx_Marca = kCreditoSustitucion.Dx_Marca,
                        Dx_Color = kCreditoSustitucion.Dx_Color,
                        No_Peso = kCreditoSustitucion.No_Peso,
                        Cve_Capacidad_Sust = kCreditoSustitucion.Cve_Capacidad_Sust,
                        Fg_Tipo_Centro_Disp = kCreditoSustitucion.Fg_Tipo_Centro_Disp,
                        Dx_Antiguedad = kCreditoSustitucion.Dx_Antiguedad,
                        Fg_Si_Funciona = kCreditoSustitucion.Fg_Si_Funciona,
                        Dt_Fecha_Recepcion = kCreditoSustitucion.Dt_Fecha_Recepcion,
                        Id_Folio = kCreditoSustitucion.Id_Folio,
                        Id_Pre_Folio = prefolio,
                        Grupo = kCreditoSustitucion.Grupo,
                        IdSistemaArreglo = kCreditoSustitucion.IdSistemaArreglo,
                        CapacidadSistema = kCreditoSustitucion.CapacidadSistema,
                        Unidad = kCreditoSustitucion.Unidad,
                        IdGrupo = kCreditoSustitucion.IdGrupo
                    };

                var newCreditoSustitucion = new CreCredito().InsertaProductoBaja(sustitucion);

                if (newCreditoSustitucion != null)
                {
                    //var datosCayD = new CreCredito().ObtenDatosCayD(kCreditoSustitucion.Id_Credito_Sustitucion);

                    //if (datosCayD != null)
                    //{
                    //    var newDatosCayd = new K_PRODUCTO_CHARACTERS
                    //        {
                    //            Id_Credito_Sustitucion = newCreditoSustitucion.Id_Credito_Sustitucion,
                    //            Id_Pre_Folio = prefolio,
                    //            Dx_Marca = datosCayD.Dx_Marca,
                    //            Dx_Modelo_Producto = datosCayD.Dx_Modelo_Producto,
                    //            No_Serial = datosCayD.No_Serial,
                    //            Dx_Color = datosCayD.Dx_Color,
                    //            No_Peso = datosCayD.No_Peso,
                    //            Dx_Antiguedad = datosCayD.Dx_Antiguedad,
                    //            Cve_Capacidad_Sust = datosCayD.Cve_Capacidad_Sust
                    //        };

                    //    new CreCredito().InsertaDatosCayd(newDatosCayd);
                    //}

                    new CreCredito().ActualizaDatosCayd(prefolio, kCreditoSustitucion.Id_Credito_Sustitucion, newCreditoSustitucion.Id_Credito_Sustitucion);

                    var lstHorariosOperacion = new CreCredito().ObtenHorariosOperacionSust(idCreditoActual,
                                                                                           kCreditoSustitucion
                                                                                               .Id_Credito_Sustitucion);

                    if (lstHorariosOperacion.Count > 0)
                    {
                        foreach (var cliHorariosOperacion in lstHorariosOperacion)
                        {
                            var newHorarioOperacion = new CLI_HORARIOS_OPERACION();
                            newHorarioOperacion.No_Credito = idCreditoNuevo;
                            newHorarioOperacion.ID_DIA_OPERACION = cliHorariosOperacion.ID_DIA_OPERACION;
                            newHorarioOperacion.IDTIPOHORARIO = cliHorariosOperacion.IDTIPOHORARIO;
                            newHorarioOperacion.Id_Credito_Sustitucion = newCreditoSustitucion.Id_Credito_Sustitucion;
                            newHorarioOperacion.Hora_Inicio = cliHorariosOperacion.Hora_Inicio;
                            newHorarioOperacion.IDCONSECUTIVO = cliHorariosOperacion.IDCONSECUTIVO;
                            newHorarioOperacion.Horas_Laborables = cliHorariosOperacion.Horas_Laborables;

                            new CreCredito().InsertaHorarioOperacion(newHorarioOperacion);
                        }
                    }

                    var lstOperacionTotal = new CreCredito().ObtenOperacionTotalSust(idCreditoActual,
                                                                                     kCreditoSustitucion
                                                                                         .Id_Credito_Sustitucion);
                    if (lstOperacionTotal.Count > 0)
                    {
                        foreach (var hOperacionTotal in lstOperacionTotal)
                        {
                            var newOperacionTotal = new H_OPERACION_TOTAL();
                            newOperacionTotal.No_Credito = idCreditoNuevo;
                            newOperacionTotal.IDTIPOHORARIO = hOperacionTotal.IDTIPOHORARIO;
                            newOperacionTotal.Id_Credito_Sustitucion = newCreditoSustitucion.Id_Credito_Sustitucion;
                            newOperacionTotal.HORAS_SEMANA = hOperacionTotal.HORAS_SEMANA;
                            newOperacionTotal.SEMANAS_AÑO = hOperacionTotal.SEMANAS_AÑO;
                            newOperacionTotal.HORAS_AÑO = hOperacionTotal.HORAS_AÑO;
                            newOperacionTotal.IDCONSECUTIVO = hOperacionTotal.IDCONSECUTIVO;

                            new CreCredito().InsertaOperacionTotal(newOperacionTotal);
                        }
                    }

                    var lstFotos = new CreCredito().ObtenFotosSustitucion(idCreditoActual,
                                                                          kCreditoSustitucion.Id_Credito_Sustitucion);

                    if (lstFotos.Count > 0)
                    {
                        foreach (var creFotos in lstFotos)
                        {
                            var newFoto = new CRE_FOTOS
                            {
                                No_Credito = idCreditoNuevo,
                                idTipoFoto = creFotos.idTipoFoto,
                                IdCreditoSustitucion = newCreditoSustitucion.Id_Credito_Sustitucion,
                                idConsecutivoFoto = creFotos.idConsecutivoFoto,
                                Nombre = creFotos.Nombre,
                                Extension = creFotos.Extension,
                                Longitud = creFotos.Longitud,
                                Foto = creFotos.Foto,
                                Estatus = creFotos.Estatus,
                                AdicionadoPor = creFotos.AdicionadoPor,
                                FechaAdicion = DateTime.Now.Date
                            };

                            new CreCredito().InsertaFoto(newFoto);
                        }
                    }

                    kCreditoSustitucion.No_Serial = "CANCEL";
                    kCreditoSustitucion.Dt_Fecha_Recepcion = null;
                    kCreditoSustitucion.Id_Folio = "";
                    new CreCredito().ActualizaEquipoBaja(kCreditoSustitucion);
                }                
            }

            var creditoCosto = new CreCredito().ObtenCreditoCosto(idCreditoActual);
            var costo = new K_CREDITO_COSTO
                {
                    No_Credito = idCreditoNuevo,
                    Mt_Costo = creditoCosto.Mt_Costo,
                    Dt_Credito_Costo = DateTime.Now.Date
                };
            new CreCredito().InsertaCreditoCosto(costo);

            var creditoDescuento = new CreCredito().ObtenCreditoDescuento(idCreditoActual);
            var descuento = new K_CREDITO_DESCUENTO
                {
                    No_Credito = idCreditoNuevo,
                    Mt_Descuento = creditoDescuento.Mt_Descuento,
                    Dt_Credito_Descuento = DateTime.Now.Date
                };
            new CreCredito().InsertaCreditoDescuento(descuento);

            //var lstAmortizaciones = new CreCredito().ObtenAmortizacionesCredito(idCreditoActual);

            //foreach (var kCreditoAmortizacion in lstAmortizaciones)
            //{
            //    kCreditoAmortizacion.No_Credito = idCreditoNuevo;
            //    new CreCredito().InsertaAmortizacionCredito(kCreditoAmortizacion);
            //}

            var lstHorariosNegocio = new CreCredito().ObtenHorariosOperacionNegocio(idCreditoActual, 1);

            if (lstHorariosNegocio.Count > 0)
            {
                foreach (var cliHorariosOperacion in lstHorariosNegocio)
                {
                    var newHorarioOperacion = new CLI_HORARIOS_OPERACION();
                    newHorarioOperacion.No_Credito = idCreditoNuevo;
                    newHorarioOperacion.ID_DIA_OPERACION = cliHorariosOperacion.ID_DIA_OPERACION;
                    newHorarioOperacion.IDTIPOHORARIO = cliHorariosOperacion.IDTIPOHORARIO;
                    newHorarioOperacion.Hora_Inicio = cliHorariosOperacion.Hora_Inicio;
                    newHorarioOperacion.IDCONSECUTIVO = cliHorariosOperacion.IDCONSECUTIVO;
                    newHorarioOperacion.Horas_Laborables = cliHorariosOperacion.Horas_Laborables;

                    new CreCredito().InsertaHorarioOperacion(newHorarioOperacion);
                }
            }

            var lstOperacionTotalNegocio = new CreCredito().ObtenOperacionTotalNegocio(idCreditoActual, 1);

            if (lstOperacionTotalNegocio.Count > 0)
            {
                foreach (var hOperacionTotal in lstOperacionTotalNegocio)
                {
                    var newOperacionTotal = new H_OPERACION_TOTAL();
                    newOperacionTotal.No_Credito = idCreditoNuevo;
                    newOperacionTotal.IDTIPOHORARIO = hOperacionTotal.IDTIPOHORARIO;
                    newOperacionTotal.HORAS_SEMANA = hOperacionTotal.HORAS_SEMANA;
                    newOperacionTotal.SEMANAS_AÑO = hOperacionTotal.SEMANAS_AÑO;
                    newOperacionTotal.HORAS_AÑO = hOperacionTotal.HORAS_AÑO;
                    newOperacionTotal.IDCONSECUTIVO = hOperacionTotal.IDCONSECUTIVO;

                    new CreCredito().InsertaOperacionTotal(newOperacionTotal);
                }
            }

            var lstFotosNegocio = new CreCredito().ObtenFotosNegocio(idCreditoActual, 1);

            if (lstFotosNegocio.Count > 0)
            {
                foreach (var creFotos in lstFotosNegocio)
                {
                    var newFoto = new CRE_FOTOS
                    {
                        No_Credito = idCreditoNuevo,
                        idTipoFoto = creFotos.idTipoFoto,
                        idConsecutivoFoto = creFotos.idConsecutivoFoto,
                        Nombre = creFotos.Nombre,
                        Extension = creFotos.Extension,
                        Longitud = creFotos.Longitud,
                        Foto = creFotos.Foto,
                        Estatus = creFotos.Estatus,
                        AdicionadoPor = creFotos.AdicionadoPor,
                        FechaAdicion = DateTime.Now.Date
                    };

                    new CreCredito().InsertaFoto(newFoto);
                }
            }
        }

        protected void InsertaFotos(string idCreditoActual, string idCreditoNuevo)
        {
            var lstFotos = new CreCredito().ObtenFotosCredito(idCreditoActual);

            if (lstFotos.Count > 0)
            {
                foreach (var creFotos in lstFotos)
                {
                    var newFoto = new CRE_FOTOS
                        {
                            No_Credito = idCreditoNuevo,
                            idTipoFoto = creFotos.idTipoFoto,
                            idCreditoProducto = creFotos.idCreditoProducto,
                            IdCreditoSustitucion = creFotos.IdCreditoSustitucion,
                            idConsecutivoFoto = creFotos.idConsecutivoFoto,
                            Nombre = creFotos.Nombre,
                            Extension = creFotos.Extension,
                            Longitud = creFotos.Longitud,
                            Foto = creFotos.Foto,
                            Estatus = creFotos.Estatus,
                            AdicionadoPor = creFotos.AdicionadoPor,
                            FechaAdicion = DateTime.Now.Date
                        };

                    new CreCredito().InsertaFoto(newFoto);
                }
            }
        }

        protected void GeneraNuevaTablaAmortizacion(CRE_Credito credito, string periodoConsumoFinal, string idCreditoActual)
        {
            var lstAmortizaciones = new CreCredito().ObtenAmortizacionesCredito(idCreditoActual);
            var fechaPrimerPago = lstAmortizaciones.First(me => me.No_Pago == 1).Dt_Fecha;
            var fechaActual = DateTime.Now.Date;

            if (fechaPrimerPago < fechaActual)
            {
                foreach (var kCreditoAmortizacion in lstAmortizaciones)
                {
                    kCreditoAmortizacion.No_Credito = credito.No_Credito;
                    new CreCredito().InsertaAmortizacionCredito(kCreditoAmortizacion);
                }
            }
            else
            {
                DataTable CreditAmortizacionDt =
                    K_CREDITOBll.ClassInstance.CalculateCreditAmortizacion(credito.No_Credito,
                                                                           Convert.ToDouble(credito.Monto_Solicitado),
                                                                           (double) credito.Tasa_Fija/100,
                                                                           (int) credito.No_Plazo_Pago,
                                                                           (int) credito.Cve_Periodo_Pago,
                                                                           (double) credito.Tasa_Interes/100,
                                                                           (double) credito.Tasa_IVA_Intereses/
                                                                           100,
                                                                           (double) credito.CAT,
                                                                           periodoConsumoFinal);

                var creditoAmortizacion = new OpEquiposAbEficiencia().InsertaAmortizacionesCredito(
                    CreditAmortizacionDt, credito.No_Credito);
            }
        }

        protected void InsertaCalculosCredito(string idCreditoActual, string idCreditoNuevo)
        {
            var lstEquiposBajaAgrupados = new CreCredito().ObtenEquiposBajaCredito(idCreditoActual);

            foreach (var lstEquiposBajaAgrupado in lstEquiposBajaAgrupados)
            {
                lstEquiposBajaAgrupado.No_Credito = idCreditoNuevo;
                new CreCredito().InsertaCreProductoBaja(lstEquiposBajaAgrupado);
            }

            var capacidadPago = new CreCredito().ObtenCapacidadPago(idCreditoActual);
            capacidadPago.No_Credito = idCreditoNuevo;
            capacidadPago.Fecha_Adicion = DateTime.Now.Date;
            new CreCredito().InsertaCapacidadPago(capacidadPago);

            var psr = new CreCredito().ObtenPSR(idCreditoActual);
            psr.No_Credito = idCreditoNuevo;
            psr.Fecha_Adicion = DateTime.Now.Date;
            new CreCredito().InsertaPSR(psr);

            #region Facturacion Actual
            var facturacionActual = new CreCredito().ObtenFacturacion(idCreditoActual, 1);

            var lstFacturacionActualDetalle =
                new CreCredito().ObtenFacturacionDetalle(facturacionActual.IdFactura);

            var nuevaFacturacionActual = new CRE_Facturacion
                {
                    No_Credito = idCreditoNuevo,
                    IdTipoFacturacion = facturacionActual.IdTipoFacturacion,
                    Cve_Tarifa = facturacionActual.Cve_Tarifa,
                    Subtotal = facturacionActual.Subtotal,
                    Iva = facturacionActual.Iva,
                    Total = facturacionActual.Total,
                    PagoFactBiMen = facturacionActual.PagoFactBiMen,
                    MontoMaxFacturar = facturacionActual.MontoMaxFacturar,
                    Fecha_Adicion = DateTime.Now.Date,
                    AdicionadoPor = facturacionActual.AdicionadoPor,
                    Estatus = 1
                };

            var resultado = new CreCredito().InsertaFacturacion(nuevaFacturacionActual);

            if (resultado != null)
            {
                foreach (var creFacturacionDetalle in lstFacturacionActualDetalle)
                {
                    var detalle = new CRE_FACTURACION_DETALLE
                        {
                            IdFactura = resultado.IdFactura,
                            Concepto = creFacturacionDetalle.Concepto,
                            Consumo = creFacturacionDetalle.Consumo,
                            CostoMensual = creFacturacionDetalle.CostoMensual,
                            Facturacion = creFacturacionDetalle.Facturacion,
                            Fecha_Adicion = DateTime.Now,
                            AdicionadoPor = creFacturacionDetalle.AdicionadoPor,
                            Estatus = 1
                        };

                    new CreCredito().InsertaFacturaciondetalle(detalle);
                }
            }

            #endregion

            #region Facturacion Futura

            var facturacionFutura = new CreCredito().ObtenFacturacion(idCreditoActual, 2);
            var lstFacturacionFuturaDetalle = new CreCredito().ObtenFacturacionDetalle(facturacionFutura.IdFactura);

            var nuevaFacturacionFutura = new CRE_Facturacion
                {
                    No_Credito = idCreditoNuevo,
                    IdTipoFacturacion = 2,
                    Cve_Tarifa = facturacionFutura.Cve_Tarifa,
                    Subtotal = facturacionFutura.Subtotal,
                    Iva = facturacionFutura.Iva,
                    Total = facturacionFutura.Total,
                    PagoFactBiMen = facturacionFutura.PagoFactBiMen,
                    MontoMaxFacturar = facturacionFutura.MontoMaxFacturar,
                    Fecha_Adicion = DateTime.Now.Date,
                    AdicionadoPor = facturacionFutura.AdicionadoPor,
                    Estatus = 1
                };

            var resultado2 = new CreCredito().InsertaFacturacion(nuevaFacturacionFutura);

            if (resultado2 != null)
            {
                foreach (var creFacturacionDetalle in lstFacturacionFuturaDetalle)
                {
                    var detalle = new CRE_FACTURACION_DETALLE
                        {
                            IdFactura = resultado2.IdFactura,
                            Concepto = creFacturacionDetalle.Concepto,
                            Consumo = creFacturacionDetalle.Consumo,
                            CostoMensual = creFacturacionDetalle.CostoMensual,
                            Facturacion = creFacturacionDetalle.CostoMensual,
                            Fecha_Adicion = DateTime.Now.Date,
                            AdicionadoPor = creFacturacionDetalle.AdicionadoPor,
                            Estatus = 1
                        };

                    new CreCredito().InsertaFacturaciondetalle(detalle);
                }
            }

            #endregion

            var lstHistorico = new CreCredito().ObtenHistoricoConsumo(idCreditoActual);

            foreach (var creHistoricoConsumo in lstHistorico)
            {
                creHistoricoConsumo.No_Credito = idCreditoNuevo;
                new CreCredito().InsertaHistoricoConsumo(creHistoricoConsumo);
            }

            var lstGrupos = new CreCredito().ObtenGruposTecnologia(idCreditoActual);

            foreach (var creGruposTecnologia in lstGrupos)
            {
                creGruposTecnologia.No_Credito = idCreditoNuevo;
                new CreCredito().InsertaGrupoTecnologia(creGruposTecnologia);
            }
        }

        public string GeneraNumeroCredito(ParseoTrama parseo)
        {
            var RGN_CFE =
                parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 145).Dato;
            var tipoZona = parseo.ComplexParseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 160).Dato;
            tipoZona = tipoZona.Substring(4, 3);
            var noCredito = "PAEEEM" + RGN_CFE + tipoZona +
                            string.Format("{0:00000}", Convert.ToInt32(LsUtility.GetNumberSequence("CREDITO")));

            return noCredito;
        }

        #region recuperacion datos

        public int ValidaReasignarRecuperar(string usuario, out string ErrorCode, out string idCredito, string rpu)
        {
            var lstCreditos =
                new CreCredito().ObtenListaCreditos(
                    me => new byte?[] {7, 10}.Contains(me.Cve_Estatus_Credito) && me.RPU == rpu && me.Consumo_Promedio != null);
            CRE_Credito credito = null;
            var usUsuario = new AdministracionUsuarios().ObtenDatosUsuario(usuario);
            var accion = 0; // 1 - Recuperar, 2 - Reasignar
            var idProveedor = 0;
            var idBranch = 0;

            if (usUsuario.Tipo_Usuario == GlobalVar.SUPPLIER_BRANCH)
            {
                var proveedor = new AdministracionUsuarios().ObtenProveedorBranch((int) usUsuario.Id_Departamento);
                idProveedor = proveedor != null ? proveedor.Id_Proveedor : 0;
                idBranch = proveedor != null ? proveedor.Id_Branch : 0;
            }
            else
            {
                var proveedor = new AdministracionUsuarios().ObtenProveedor((int)usUsuario.Id_Departamento);
                idProveedor = proveedor != null ? proveedor.Id_Proveedor : 0;
            }

            if (lstCreditos != null && lstCreditos.Count > 0)
            {
                var fechaMax = lstCreditos.Max(me => me.Fecha_Cancelado);

                credito = lstCreditos.LastOrDefault(me => me.Fecha_Cancelado == fechaMax);

                if (credito != null)
                {
                    idCredito = credito.No_Credito;
                    var lstFechasCredito = CargaFechasCredito(credito);
                    lstFechasCredito =
                        lstFechasCredito.FindAll(
                            me => me.Dt_Fecha_Estatus_Credito != null && !new int?[] { 5, 7 }.Contains(me.Cve_Estatus_Credito));
                    //lstFechasCredito =
                    //    lstFechasCredito.FindAll(
                    //        me => me.Dt_Fecha_Estatus_Credito != null && me.Cve_Estatus_Credito != 7);
                    

                    if (credito.Id_Proveedor == idProveedor && credito.Id_Branch == idBranch)
                    {
                        var lstCreditoSustitucion = new CreCredito().ObtenCreditoSustitucion(credito.No_Credito);

                        if (lstCreditoSustitucion != null && lstCreditoSustitucion.Count > 0)
                        {
                            if (lstCreditoSustitucion.FindAll(me => me.Id_Folio != null).Count > 0)
                            {
                                var ultimaFechaCredito = lstFechasCredito.Max(me => me.Dt_Fecha_Estatus_Credito);
                                var ultimoEstatus =
                                    lstFechasCredito.LastOrDefault(
                                        me => me.Dt_Fecha_Estatus_Credito == ultimaFechaCredito).Cve_Estatus_Credito;

                                if (ultimoEstatus == 2 || ultimoEstatus == 3)
                                {
                                    accion = 2;
                                    ErrorCode = "";
                                }
                                else
                                {
                                    ErrorCode = "El último estatus de la solicitud encontrada no es el correcto para reasignar la solicitud";
                                }
                            }
                            else
                            {
                                lstFechasCredito = lstFechasCredito.FindAll(me => me.Cve_Estatus_Credito != 10);
                                var ultimaFechaCredito = lstFechasCredito.Max(me => me.Dt_Fecha_Estatus_Credito);
                                var ultimoEstatus =
                                    lstFechasCredito.LastOrDefault(
                                        me => me.Dt_Fecha_Estatus_Credito == ultimaFechaCredito).Cve_Estatus_Credito;

                                if (ultimoEstatus == 1 || ultimoEstatus == 2 || ultimoEstatus == 3)
                                {
                                    accion = 1;
                                    ErrorCode = "";
                                }
                                else
                                {
                                    ErrorCode = "El último estatus de la solicitud encontrada no es el correcto para recuperar los datos";
                                }
                            }
                        }
                        else
                        {
                            lstFechasCredito = lstFechasCredito.FindAll(me => me.Cve_Estatus_Credito != 10);
                            var ultimaFechaCredito = lstFechasCredito.Max(me => me.Dt_Fecha_Estatus_Credito);
                            var ultimoEstatus =
                                lstFechasCredito.LastOrDefault(
                                    me => me.Dt_Fecha_Estatus_Credito == ultimaFechaCredito).Cve_Estatus_Credito;

                            if (ultimoEstatus == 1 || ultimoEstatus == 2 || ultimoEstatus == 3)
                            {
                                accion = 1;
                                ErrorCode = "";
                            }
                            else
                            {
                                ErrorCode = "El último estatus de la solicitud encontrada no es el correcto para recuperar los datos";
                            }
                        }
                    }
                    else
                    {
                        ErrorCode = "La solicitud encontrada no pertenece al distribuidor distribuidor";
                    }
                }
                else
                {
                    idCredito = "";
                    ErrorCode = "No existen datos a recuperar con el RPU proporcionado";
                }
            }            
            else
            {
                idCredito = "";
                ErrorCode = "No existen datos a recuperar con el RPU proporcionado";
            }

            idCredito = credito != null ? credito.No_Credito : "";
            return accion;
        }

        protected List<CAT_ESTATUS_CREDITO> CargaFechasCredito(CRE_Credito credito)
        {
            var lstFechasCredito = new CreCredito().ObtenEstatusCreditos();

            foreach (var catEstatusCredito in lstFechasCredito)
            {
                switch (catEstatusCredito.Cve_Estatus_Credito)
                {
                    case 1:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_Pendiente;
                        break;
                    case 2:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_Por_entregar;
                        break;
                    case 3:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_En_revision;
                        break;
                    case 4:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_Autorizado;
                        break;
                    case 5:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_Rechazado;
                        break;
                    case 6:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_Finanzas;
                        break;
                    case 7:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_Cancelado;
                        break;
                    case 8:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_Beneficiario_con_adeudos;
                        break;
                    case 9:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_Tarifa_fuera_de_programa;
                        break;
                    case 10:
                        catEstatusCredito.Dt_Fecha_Estatus_Credito = credito.Fecha_Calificación_MOP_no_válida;
                        break;
                }
            }

            return lstFechasCredito;
        }

        public
            bool ValidaMontoMaximo(string rfc, decimal montoSolicitado)
        {
            //var montoDispuesto = Math.Round(new OpEquiposAbEficiencia().ValidaMontoMaximoCredito(Pyme.RFC),2);
            decimal montoDispuesto = 0.0M;

            montoDispuesto =
                Math.Round(new OpEquiposAbEficiencia().ValidaMontoMaximoCredito(rfc), 2);

            var montoTotal = montoDispuesto + montoSolicitado;

            var topeMaximoXRFC = Convert.ToDecimal(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 4 && p.IDSECCION == 16).VALOR);

            if (montoTotal <= topeMaximoXRFC)
                return true;
            return false;
        }

        #endregion
    }
}
