using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.Tarifas;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.Entidades.Trama;

namespace PAEEEM.LogicaNegocios.Tarifas
{    

    public class AlgoritmoTarifa02
    {
        private CompTarifa _objComplejoT02 = new CompTarifa();
        private readonly int _claveIva = 0;
        private readonly string _tipoFacturacion = "";
        private readonly CompParseo _parseo = new CompParseo();
        
        public CompParseo ParseoCompleto
        {
            get { return _parseo; }
        }

        public CompTarifa T02
        {
            get { return _objComplejoT02; }
        }

        public AlgoritmoTarifa02(CompParseo parseo, bool factConAhorro)
        {
            _claveIva = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).ValorEntero;
            _tipoFacturacion = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato;
            _tipoFacturacion = _tipoFacturacion != "1" ? "2" : _tipoFacturacion;
        }

        public AlgoritmoTarifa02(CompParseo parseo)
        {
            _claveIva = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).ValorEntero;
            _tipoFacturacion = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato;
            _tipoFacturacion = _tipoFacturacion != "1" ? "2" : _tipoFacturacion;


            _parseo = parseo;


            ValidaTarifa(_parseo.HistorialDetconsumos);
        }


        private void ValidaTarifa(List<CompHistorialDetconsumo> historialDetconsumos)
        {
            var detalle = new CompDetalleFechas();
            //OBTIENE LOS VALORES PARA EL DETALLE DE DEMANDA
            var detConsumoFechaMax = historialDetconsumos.Max(p => p.Fecha);
            DateTime? detConsumoFechaMin = null;

            if (_tipoFacturacion == "1")
                detConsumoFechaMin = detConsumoFechaMax.Value.AddMonths(-11);
            else if (_tipoFacturacion == "2")
                detConsumoFechaMin = detConsumoFechaMax.Value.AddMonths(-12);

            detalle.FechaMax = detConsumoFechaMax;
            detalle.FechaMin = detConsumoFechaMin;

            var histDetConsumo = historialDetconsumos.FirstOrDefault(p => p.Fecha == detConsumoFechaMin);

            if (histDetConsumo == null)
            {
                var fechaMin = historialDetconsumos.FindAll(p => p.Id > 0).Min(p => p.Fecha);
                if(fechaMin < detConsumoFechaMin)
                    histDetConsumo = historialDetconsumos.FirstOrDefault(p => p.Fecha == detConsumoFechaMin.Value.AddMonths(1));
            }

            if (histDetConsumo != null)//SI ES DISTINTO DE NULL CUMPLE CON EL AÑO DE FACTURACION
            {
                T02.AnioFactValido = true;

                var periodoMinimo = 0;
                if (_tipoFacturacion == "1")//MENSUAL                    
                    periodoMinimo = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 2 && p.IDSECCION == 4).VALOR);

                if (_tipoFacturacion == "2")//BIMESTRAL                    
                    periodoMinimo = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 4).VALOR);

                var rstNoTotalConsumo = historialDetconsumos.Count(p => p.Fecha >= histDetConsumo.Fecha && p.Fecha <= detalle.FechaMax);

                if (rstNoTotalConsumo >= periodoMinimo)
                {

                    T02.PeriodosValidos = true;
                    T02.Periodos = rstNoTotalConsumo;


                    var cnptos = CreaConceptos(_parseo.InfoConsumo.Detalle.Promedio, _parseo.InfoDemanda.DemandaMax);
                    if (T02.ValidaFechaTablasTarifas)
                        T02.FactSinAhorro = Facturacion(cnptos, true);
                }
                else
                {
                    T02.PeriodosValidos = false;
                    T02.Periodos = rstNoTotalConsumo;
                }

            }
            else
            {
                //PERIODO DE FACTURACION INVALIDA
                T02.AnioFactValido = false;
            }

        }


        public CompFacturacion CreaConceptos(decimal ConsumoPromedio, decimal demandaMaxima)
        {
            var cnptos = new CompFacturacion();
            var fechaApicable = DateTime.Now.ToString("MMMM-yyyy").ToUpper();
            var ktarifa02 = Tarifa02.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaApicable);


            //falta establecer condicion por sino trae registro en la tabla de k_tarifa_03
            if (ktarifa02 != null)
            {
                T02.ValidaFechaTablasTarifas = true;
                cnptos.ConceptosFacturacion = new List<CompConceptosFacturacion>();
                int contador = 0;
                foreach (var parametroGlobal in new ParametrosGlobales().FiltraPorCondicion(p => p.IDSECCION == 14))
                {

                    if (parametroGlobal.IDPARAMETRO >= 1 && parametroGlobal.IDPARAMETRO <= 4)
                    {
                        cnptos.ConceptosFacturacion.Add
                        (
                            new CompConceptosFacturacion
                            {
                                IdConcepto = parametroGlobal.IDPARAMETRO,
                                Concepto = parametroGlobal.VALOR,                                
                            }
                        );
                    }

                    

                    if (parametroGlobal.IDPARAMETRO >= 5 && parametroGlobal.IDPARAMETRO <= 8)                    
                    {//unicamente se llena los valores de acuerdo a la regla indicada 
                        var valor = Convert.ToDecimal(parametroGlobal.VALOR);                        

                        if (parametroGlobal.IDPARAMETRO == 6)
                        {
                            valor = ConsumoPromedio > valor ? valor : ConsumoPromedio;
                            ConsumoPromedio = ConsumoPromedio - valor;
                        }

                        if (parametroGlobal.IDPARAMETRO == 7)
                        {
                            valor = ConsumoPromedio > valor ? valor : ConsumoPromedio;
                            ConsumoPromedio = ConsumoPromedio - valor;
                        }

                        cnptos.ConceptosFacturacion[contador].CPromedioODemMax = valor;
                        contador++;
                    }

                    
                }

                //cuando finaliza el llenado de valores se debera de realizar una validacion para el total de consumo
                //debido a que se le restara menos 101

                //var reglaConsumo = cnptos.ConceptosFacturacion.Sum(p => p.CPromedioODemMax);

                //if (reglaConsumo < ConsumoPromedio)
                //    T02.CumpleConsumo = true;
                //else
                //{
                //    T02.CumpleConsumo = false;
                //    //return cnptos;
                //}
                
                //EN LO SIGUIENTES CONCEPTO UNICAMENTE SE REALIZAR UNA RESTA AL CONCEPTO DE EXCEDENTE EN SU CONSUMO LOS DEMAS PASAN DIRECTO                
                cnptos.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional = (decimal) ktarifa02.MT_COSTO_KWH_FIJO;        
                cnptos.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional = (decimal) ktarifa02.MT_COSTO_KWH_BASICO;          
                cnptos.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 3).CargoAdicional = (decimal) ktarifa02.MT_COSTO_KWH_INTERMEDIO;
                cnptos.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).CPromedioODemMax = ConsumoPromedio;
                cnptos.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).CargoAdicional = (decimal) ktarifa02.MT_COSTO_KWH_EXCEDENTE;

            }
            else
            {
                T02.ValidaFechaTablasTarifas = false;
            }
            return cnptos;
        }


        public CompFacturacion Facturacion(CompFacturacion facturacion, bool sinAhorro)
        {
            var divisor = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 2).VALOR);

            //Operacion de Facturacion sin ahorro operacion de calculo de facturacion
            for (int i = 0; i < facturacion.ConceptosFacturacion.Count; i++)
            {
                var concepto = facturacion.ConceptosFacturacion[i];
                if (facturacion.ConceptosFacturacion[i].IdConcepto != 1)
                {                   
                    concepto.Facturacion = Math.Round(concepto.CPromedioODemMax*concepto.CargoAdicional, 2);
                    facturacion.ConceptosFacturacion[i] = concepto;
                }
                else
                {
                    
                    concepto.Facturacion = Math.Round(concepto.CargoAdicional, 2);
                    facturacion.ConceptosFacturacion[i] = concepto;
                }
                
            }
        


            //calculo del subtotal
            var subTotal = Math.Round(facturacion.ConceptosFacturacion.Sum(factSinAhorro => factSinAhorro.Facturacion), 2);
            var iva = Math.Round(subTotal * (_claveIva / divisor), 2); // CLAVE IVA
            var total = Math.Round(subTotal + iva, 2);
            var pagoFacturaBiOMe = Math.Round(total * Convert.ToInt32(_tipoFacturacion), 2);//FACTURACION
            var montoMaxFacturar = 0.0M;


            if (sinAhorro)
                montoMaxFacturar =
                    Math.Round(
                        pagoFacturaBiOMe*
                        Convert.ToDecimal(
                            new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 3)
                                                    .VALOR), 2);


            facturacion.Subtotal = subTotal;
            facturacion.Iva = iva;
            facturacion.Total = total;
            facturacion.PagoFactBiMen = pagoFacturaBiOMe;
            facturacion.MontoMaxFacturar = montoMaxFacturar;

            return facturacion;
        }

    }
}
