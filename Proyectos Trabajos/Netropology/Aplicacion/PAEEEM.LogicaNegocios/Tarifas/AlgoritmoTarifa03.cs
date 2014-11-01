using System;
using System.Collections.Generic;
using System.Linq;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.Tarifas;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.Entidades.Trama;

namespace PAEEEM.LogicaNegocios.Trama
{
    public class AlgoritmoTarifa03
    {
        private CompTarifa _objComplejoT03 = new CompTarifa();
        private readonly int _claveIva = 0;
        private readonly string _tipoFacturacion = "";
        private readonly CompParseo _parseo = new CompParseo();

        public CompParseo ParseoCompleto
        {
            get { return _parseo; }
        }
       
        public CompTarifa T03
        {
            get { return _objComplejoT03; }           
        }


        public AlgoritmoTarifa03(CompParseo parseo)
        {
            _claveIva = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).ValorEntero;
            _tipoFacturacion = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato;
            _tipoFacturacion = _tipoFacturacion != "1" ? "2" : _tipoFacturacion;

            _parseo = parseo;


            ValidaTarifa(_parseo.HistorialDetconsumos);
        }

        public AlgoritmoTarifa03(CompParseo parseo, bool factConAhorro)
        {
            _claveIva = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).ValorEntero;
            _tipoFacturacion = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato;
            _tipoFacturacion = _tipoFacturacion != "1" ? "2" : _tipoFacturacion;
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

            //if (histDetConsumo == null)
            //{
            //    var fechaMin = historialDetconsumos.FindAll(p => p.Id > 0).Min(p => p.Fecha);
            //    histDetConsumo = historialDetconsumos.FirstOrDefault(p => p.Fecha == fechaMin);
            //}

            if (histDetConsumo != null)//SI ES DISTINTO DE NULL CUMPLE CON EL AÑO DE FACTURACION
            {
                T03.AnioFactValido = true;               

                var periodoMinimo = 0;
                if (_tipoFacturacion == "1")//MENSUAL                    
                    periodoMinimo = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 2 && p.IDSECCION == 4).VALOR);

                if (_tipoFacturacion == "2")//BIMESTRAL                    
                    periodoMinimo = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 4).VALOR);

                var rstNoTotalConsumo = historialDetconsumos.Count(p => p.Fecha >= histDetConsumo.Fecha && p.Fecha <= detalle.FechaMax);

                if (rstNoTotalConsumo >= periodoMinimo)
                {
                    
                    T03.PeriodosValidos = true;
                    T03.Periodos = rstNoTotalConsumo;
                                     

                    var cnptos = CreaConceptos(_parseo.InfoConsumo.Detalle.Promedio, _parseo.InfoDemanda.DemandaMax);
                    if(T03.ValidaFechaTablasTarifas)
                        T03.FactSinAhorro = Facturacion(cnptos, true);
                }
                else
                {
                    T03.PeriodosValidos = false;
                    T03.Periodos = rstNoTotalConsumo;                                       
                }

            }
            else
            {
                //PERIODO DE FACTURACION INVALIDA
                T03.AnioFactValido = false;                
            }

        }


        ////METODO PARA OBTENER EL DETALLE DEL CONSUMO O DEMANDA ESTE PROCESO SOLO ES PARA TARIFA 03
        //private CompInfoConsDeman InfoDemandaConsumo(bool Demanda, List<CompHistorialDetconsumo> historialDetconsumos)
        //{
        //    var infoConsDeman = new CompInfoConsDeman();
        //    var detalle = new CompDetalleFechas();

        //    //OBTIENE LOS VALORES PARA EL DETALLE DE DEMANDA
        //    var detConsumoFechaMax = historialDetconsumos.Max(p => p.Fecha);
        //    var detConsumoFechaMin = detConsumoFechaMax.Value.AddMonths(-11);

        //    detalle.FechaMax = detConsumoFechaMax;

        //    var histDetConsumo = historialDetconsumos.FirstOrDefault(p => p.Fecha == detConsumoFechaMin);
            
        //    detalle.FechaMin = histDetConsumo.Fecha;            


        //    //PREGUNTA SI ES DEMANDA O CONSUMO
        //    if (Demanda)
        //    {
        //        var DemandaMax = historialDetconsumos.Where(p => p.Fecha <= detConsumoFechaMax && p.Fecha >= detalle.FechaMin).Max(p => p.Demanda);

        //        infoConsDeman.DemandaMax = Math.Round((decimal)DemandaMax,2);
        //        detalle.DemandaMax = infoConsDeman.DemandaMax;               
        //    }
        //    else
        //    {                
        //        var rstConsTotal = historialDetconsumos.Where(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax).Sum(p => p.Consumo);

        //        var rstNoTotalConsumo = historialDetconsumos.Count(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax);

        //        infoConsDeman.Suma = rstConsTotal;
        //        detalle.Promedio = Math.Round(rstConsTotal / rstNoTotalConsumo,2); //REDONDEO A DOS DECIMALES                              
        //    }

        //    infoConsDeman.Detalle = detalle;
        //    return infoConsDeman;
        //}


        public CompFacturacion CreaConceptos(decimal ConsumoPromedio, decimal demandaMaxima)
        {
            var cnptos = new CompFacturacion();
            var fechaApicable = DateTime.Now.ToString("MMMM-yyyy").ToUpper();
            var ktarifa03 =
                Tarifa03.ObtienePorCondicion(
                    p => p.FECHA_APLICABLE == fechaApicable);


            //falta establecer condicion por sino trae registro en la tabla de k_tarifa_03
            if (ktarifa03 != null)
            {
                T03.ValidaFechaTablasTarifas = true;
                cnptos.ConceptosFacturacion = new List<CompConceptosFacturacion>();
                foreach (var parametroGlobal in new ParametrosGlobales().FiltraPorCondicion(p => p.IDSECCION == 6 && (p.IDPARAMETRO == 1 || p.IDPARAMETRO == 2)))
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

                cnptos.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax = ConsumoPromedio;
                cnptos.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional = (decimal)ktarifa03.MT_CARGO_ADICIONAL;
                cnptos.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax = demandaMaxima;
                cnptos.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional = (decimal) ktarifa03.MT_CARGO_DEMANDA_MAX;

            }
            else
            {
                T03.ValidaFechaTablasTarifas = false;
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
                concepto.Facturacion = Math.Round(concepto.CPromedioODemMax * concepto.CargoAdicional,2);
                facturacion.ConceptosFacturacion[i] = concepto;
            }
            

            //calculo del subtotal
            var subTotal = Math.Round(facturacion.ConceptosFacturacion.Sum(factSinAhorro => factSinAhorro.Facturacion),2); 
            var iva = Math.Round(subTotal * (_claveIva/divisor),2); // CLAVE IVA
            var total = Math.Round(subTotal + iva,2);
            var pagoFacturaBiOMe = Math.Round(total * Convert.ToInt32(_tipoFacturacion),2);//FACTURACION
            var montoMaxFacturar = 0.0M;


            if(sinAhorro)
                montoMaxFacturar = Math.Round(pagoFacturaBiOMe * Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 3).VALOR),2);


            facturacion.Subtotal = subTotal;
            facturacion.Iva = iva;
            facturacion.Total = total;
            facturacion.PagoFactBiMen = pagoFacturaBiOMe;
            facturacion.MontoMaxFacturar = montoMaxFacturar;

            return facturacion;
        }
        


    }
}
