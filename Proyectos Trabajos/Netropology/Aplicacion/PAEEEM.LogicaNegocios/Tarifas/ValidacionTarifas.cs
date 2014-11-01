using System;
using System.Collections.Generic;
using System.Linq;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.Entidades.Trama;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;

namespace PAEEEM.LogicaNegocios.Tarifas
{
    public class ValidacionTarifas
    {               

        //OBTIENE PERIODO SIMPLE DE RECUPERACION
        public decimal GetValorPsr(CompConceptosPsr conceptosPsr)
        {
            decimal valorPsr = 0.0M;

            valorPsr = (conceptosPsr.MontoFinanciamento/(conceptosPsr.AhorroEconomico*conceptosPsr.Periodo));
                
            return valorPsr;
        }

        public decimal GetValorCapPago(CompConceptosCappago conceptosCappago, int idTipoFacturacion, decimal amortizacion)
        {
            decimal valorCapPago = 0.0M;
            //var idTipoFacturacion = int.Parse(parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato);


            valorCapPago = (conceptosCappago.Flujo / (amortizacion /idTipoFacturacion));

            return valorCapPago;
        }


        public CompResultado GetResultados(decimal validaRegla, decimal psr, decimal capPago, decimal nuevaFacturacion , decimal consNeg, decimal pagRecBim)
        {
            var paramGlobales = new ParametrosGlobales();

            //OBTENCION DE VALORES QUE EMPLEAREMOS COMO ESTANDAR DE VALIDACION
            var validaPsr = int.Parse(paramGlobales.ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 5).VALOR); //PARA EL PSR
            var validaCaPago = int.Parse(paramGlobales.ObtienePorCondicion(p => p.IDPARAMETRO == 2 && p.IDSECCION == 5).VALOR); //PARA CAPACIDAD DE PAGO
            var validaNFactuNeg = int.Parse(paramGlobales.ObtienePorCondicion(p => p.IDPARAMETRO == 3 && p.IDSECCION == 5).VALOR); // PARA NUEVA FATURA NEGATIVA
            var validaConNegtivo = int.Parse(paramGlobales.ObtienePorCondicion(p => p.IDPARAMETRO == 4 && p.IDSECCION == 5).VALOR); // PARA CONSUMO NEGATIVO



            var resultado = new CompResultado
                {
                    ValidacionValue = pagRecBim <= validaRegla,
                    PsrValue = psr <= validaPsr,
                    CapacidadPagoValue = capPago > validaCaPago,
                    NuevaFacturaNegativaValue = nuevaFacturacion >= validaNFactuNeg,
                    ConsumoNegativoValue = consNeg > validaConNegtivo
                };

            return resultado;
        }


        public List<Presupuesto> ObtenResumenPresupuesto(List<EquipoAltaEficiencia> LstAltaEficiencia, List<EquipoBajaEficiencia> LstBajaEficiencia, decimal valorIva)
        {
            var presupuesto = new OpEquiposAbEficiencia().ObtieneConceptosPresupuesto();

            if (LstAltaEficiencia.Count > 0)
            {
                var costoEquipos = 0.0M;
                var montoIncentivo = 0.0M;
                var gastosInstalacion = 0.0M;

                foreach (var equipoAltaEficiencia in LstAltaEficiencia)
                {
                    costoEquipos = costoEquipos + equipoAltaEficiencia.Importe_Total_Sin_IVA;
                    montoIncentivo = montoIncentivo + equipoAltaEficiencia.MontoIncentivo;
                    gastosInstalacion = gastosInstalacion + equipoAltaEficiencia.Gasto_Instalacion;
                }

                //var montoChatarrizacion = LstBajaEficiencia.Aggregate(0.0M,
                //                                                      (current, equipoBajaEficiencia) =>
                //                                                      current +
                //                                                      (equipoBajaEficiencia.MontoChatarrizacion *
                //                                                       equipoBajaEficiencia.Cantidad));

                var montoChatarrizacion =
                    LstBajaEficiencia.Where(
                        equipoBajaEficiencia =>
                        LstAltaEficiencia.Any(p => p.Cve_Grupo.ToString() == equipoBajaEficiencia.Cve_Grupo.ToString()))
                                     .Sum(
                                         equipoBajaEficiencia =>
                                         equipoBajaEficiencia.MontoChatarrizacion*equipoBajaEficiencia.Cantidad);

                var montoIva = valorIva * (costoEquipos + gastosInstalacion);
                var subTotal = (costoEquipos + gastosInstalacion) + montoIva;
                var descuento = montoIncentivo - montoChatarrizacion;

                presupuesto.First(p => p.IdPresupuesto == 3).Resultado = costoEquipos;
                presupuesto.First(p => p.IdPresupuesto == 4).Resultado = montoChatarrizacion;
                presupuesto.First(p => p.IdPresupuesto == 5).Resultado = montoIncentivo;
                presupuesto.First(p => p.IdPresupuesto == 6).Resultado = descuento;
                presupuesto.First(p => p.IdPresupuesto == 7).Resultado = gastosInstalacion;
                presupuesto.First(p => p.IdPresupuesto == 2).Resultado = montoIva;
                presupuesto.First(p => p.IdPresupuesto == 1).Resultado = subTotal;

                var total = subTotal - presupuesto.First(p => p.IdPresupuesto == 6).Resultado;

                presupuesto.First(p => p.IdPresupuesto == 8).Resultado = total;

                if (descuento < 0)
                    throw new Exception("Capture un monto mayor para el equipo de Alta");
            }

            return presupuesto;
        }

        public List<Presupuesto> ObtenResumenPresupuestoConsulta(List<EquipoAltaEficiencia> LstAltaEficiencia, List<EquipoBajaEficiencia> LstBajaEficiencia, decimal valorIva, string idCredito)
        {
            var presupuesto = new OpEquiposAbEficiencia().ObtieneConceptosPresupuesto();

            if (LstAltaEficiencia.Count > 0)
            {
                var costoEquipos = 0.0M;
                var gastosInstalacion = 0.0M;
                var credito = SolicitudCredito.SolicitudCreditoAcciones.ObtenCredito(idCredito);
                var tasaIva = Convert.ToDecimal(credito.Tasa_IVA/100);
                var montoChatarrizacion = new CreCredito().ObtenMontoChatarrizacion(idCredito);
                var descuento = new CreCredito().ObtenMontoDescuento(idCredito);
                var montoIncentivo = descuento; // + montoChatarrizacion;
                descuento = descuento - montoChatarrizacion;

                foreach (var equipoAltaEficiencia in LstAltaEficiencia)
                {
                    costoEquipos = costoEquipos + equipoAltaEficiencia.Importe_Total_Sin_IVA;
                    gastosInstalacion = gastosInstalacion + (equipoAltaEficiencia.Gasto_Instalacion / (1.0M + tasaIva));
                }

                var montoIva = tasaIva * (costoEquipos + gastosInstalacion);
                var subTotal = (costoEquipos + gastosInstalacion) + montoIva;

                presupuesto.First(p => p.IdPresupuesto == 3).Resultado = costoEquipos;
                presupuesto.First(p => p.IdPresupuesto == 4).Resultado = montoChatarrizacion;
                presupuesto.First(p => p.IdPresupuesto == 5).Resultado = montoIncentivo;
                presupuesto.First(p => p.IdPresupuesto == 6).Resultado = descuento;
                presupuesto.First(p => p.IdPresupuesto == 7).Resultado = gastosInstalacion;
                presupuesto.First(p => p.IdPresupuesto == 2).Resultado = montoIva;
                presupuesto.First(p => p.IdPresupuesto == 1).Resultado = subTotal;

                var total = subTotal - presupuesto.First(p => p.IdPresupuesto == 6).Resultado;

                presupuesto.First(p => p.IdPresupuesto == 8).Resultado = total;
            }

            return presupuesto;
        }

        public bool ValidaEquiposAltayBaja(List<EquipoAltaEficiencia> LstAltaEficiencia,
                                           List<EquipoBajaEficiencia> LstBajaEficiencia)
        {
            foreach (var equipoBajaEficiencia in LstBajaEficiencia)
            {
                if (!LstAltaEficiencia.Any(p => p.Cve_Grupo == equipoBajaEficiencia.Cve_Grupo))
                    return false;
            }

            return true;
        }
        
        public List<CompResultadoValidacion> ObtenResultadosValidacion(CompResultado resultado)
        {
            var lstResultadosValidacion = new List<CompResultadoValidacion>();

            var resultadoValidacion = new CompResultadoValidacion
                {
                    IdResultado = 1,
                    Nombre = "Incremento en facturación del 20%",
                    Resultado = resultado.ValidacionValue ? "PASA" : "NO PASA"
                };
            lstResultadosValidacion.Add(resultadoValidacion);

            resultadoValidacion = new CompResultadoValidacion
                {
                    IdResultado = 2,
                    Nombre = "Periodo simple de recuperación",
                    Resultado = resultado.PsrValue ? "PASA" : "NO PASA"
                };
            lstResultadosValidacion.Add(resultadoValidacion);

            resultadoValidacion = new CompResultadoValidacion
            {
                IdResultado = 3,
                Nombre = "Nueva Facturación",
                Resultado = resultado.NuevaFacturaNegativaValue ? "PASA" : "NO PASA"
            };
            lstResultadosValidacion.Add(resultadoValidacion);

            resultadoValidacion = new CompResultadoValidacion
            {
                IdResultado = 4,
                Nombre = "Energía Eléctrica Suficiente",
                Resultado = resultado.ConsumoNegativoValue ? "PASA" : "NO PASA"
            };
            lstResultadosValidacion.Add(resultadoValidacion);

            resultadoValidacion = new CompResultadoValidacion
            {
                IdResultado = 5,
                Nombre = "Capacidad de Pago",
                Resultado = resultado.CapacidadPagoValue ? "PASA" : "NO PASA"
            };
            lstResultadosValidacion.Add(resultadoValidacion);

            return lstResultadosValidacion;
        }

    }
}
