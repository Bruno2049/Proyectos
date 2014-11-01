using System;
using System.Collections.Generic;
using System.Text;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.Tarifas;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Tarifas;
using System.Linq;
using PAEEEM.Entidades.Trama;

namespace PAEEEM.LogicaNegocios.Tarifas
{
    public class AlgoritmoTarifaOM
    {
        private readonly CompTarifa _objComplejoTom = new CompTarifa();
        private readonly int _claveIva = 0;        
        private readonly int _tipoFacturacion = 0;
        private readonly string _tarifa = "";        
        private readonly CompParseo _parseo = new CompParseo();
        private List<CompFacturacion> _periodosFacturados = new List<CompFacturacion>();
        private decimal _validaPenalizacion = 0.0M;
        private decimal _validaBonificacion = 0.0M;
        private decimal _paramUnoFact = 0.0M;
        private decimal _divisor = 0.0M;        

        //VARIABLES PARA LA PENSALIZACION
        private decimal _paramUnoP = 0.0M;
        private decimal _paramDosP = 0.0M;
        private decimal _paramTresP = 0.0M;
        private decimal _paramCuatroP = 0.0M;

        //VARIABLES PARA LA BONIFICACION
        private decimal _paramUnoB = 0.0M;
        private decimal _paramDosB = 0.0M;
        private decimal _paramTresB = 0.0M;
        private decimal _paramCuatroB = 0.0M;

        //VARIABLES DEL KVAR       
        private decimal _kvar = 0.0M;

        //VARIABLES PARA BANCO DE CAPACITORES
        private bool _periodosValidosBC;
        private bool _maximoFactoresPotenciaBC;


        private List<TR_PARAMETROS_GLOBALES> parametrosGlobales = new List<TR_PARAMETROS_GLOBALES>();
       
        public CompTarifa Tom
        {
            get { return _objComplejoTom; }
        }

        public List<CompFacturacion> PeriodosFacturados
        {
            get { return _periodosFacturados; }
        }

        public decimal Kvar
        {
            get { return _kvar; }
        }

        public bool periodosValidosBC
        {
            get { return _periodosValidosBC; }
        }

        public bool maximoFactoresPotenciaBC
        {
            get { return _maximoFactoresPotenciaBC; }
        }

        public AlgoritmoTarifaOM(CompParseo parseo)
        {
            cargaParametrosIniciales();

            _claveIva = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).ValorEntero;
            _tipoFacturacion = int.Parse(parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato);
            _tipoFacturacion = _tipoFacturacion != 1 ? 2 : _tipoFacturacion;
            _tarifa = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;

            _parseo = parseo;            

            ValidacionTarifa(_parseo.HistorialDetconsumos);

            if (Tom.AnioFactValido && Tom.PeriodosValidos)
            {
                if (_periodosValidosBC && maximoFactoresPotenciaBC)
                    _kvar = CalculakVAR();
            }
        }

        public AlgoritmoTarifaOM(CompParseo parseo, bool factConAhorro)
        {
            cargaParametrosIniciales();

            _claveIva = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).ValorEntero;
            _tipoFacturacion = int.Parse(parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato);
            _tipoFacturacion = _tipoFacturacion != 1 ? 2 : _tipoFacturacion;
            _tarifa = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;

            _parseo = parseo;

            var detConsumoFechaMax = _parseo.HistorialDetconsumos.Max(p => p.Fecha);
            var detConsumoFechaMin = detConsumoFechaMax.Value.AddMonths(-11);
            var rstNoTotalConsumo = _parseo.HistorialDetconsumos.Count(p => p.Fecha >= detConsumoFechaMin && p.Fecha <= detConsumoFechaMax);
            Tom.Periodos = rstNoTotalConsumo;
        }

        private void cargaParametrosIniciales()
        {
            parametrosGlobales = new ParametrosGlobales().FiltraPorCondicion(p => new[] {2, 7, 8, 9, 10 }.Contains(p.IDSECCION));
            _validaPenalizacion = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 2 && p.IDSECCION == 7).VALOR);
            _validaBonificacion = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 3 && p.IDSECCION == 7).VALOR);
            _paramUnoFact = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 1 && p.IDSECCION == 10).VALOR);
            _divisor = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 1 && p.IDSECCION == 2).VALOR);

            //PARAMETROS DE PENALIZACION
            _paramUnoP = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 1 && p.IDSECCION == 8).VALOR);//3M
            _paramDosP = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 2 && p.IDSECCION == 8).VALOR);//5M
            _paramTresP = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 3 && p.IDSECCION == 8).VALOR);//0.90M
            _paramCuatroP = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 4 && p.IDSECCION == 8).VALOR);//1

            //PARAMETROS DE BONIFICACION
            _paramUnoB = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 1 && p.IDSECCION == 9).VALOR);//1M
            _paramDosB = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 2 && p.IDSECCION == 9).VALOR);//4M
            _paramTresB = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 3 && p.IDSECCION == 9).VALOR);//1M
            _paramCuatroB = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 4 && p.IDSECCION == 9).VALOR);//0.90M            
           
        }


        private void ValidacionTarifa(List<CompHistorialDetconsumo> historialDetconsumos)
        {            
            var detalle = new CompDetalleFechas();
            //OBTIENE LOS VALORES PARA EL DETALLE DE DEMANDA
            var detConsumoFechaMax = historialDetconsumos.Max(p => p.Fecha);
            var detConsumoFechaMin = detConsumoFechaMax.Value.AddMonths(-11);

            detalle.FechaMax = detConsumoFechaMax;
            detalle.FechaMin = detConsumoFechaMin;

            var factPotIdeal = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDSECCION == 11 && p.IDPARAMETRO == 1).VALOR);
            var maximoFactoresBC =
                int.Parse(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDSECCION == 4 && p.IDPARAMETRO == 4).VALOR);
            
            var histDetConsumo = historialDetconsumos.FirstOrDefault(p => p.Fecha == detConsumoFechaMin);

            //if (histDetConsumo == null)
            //{
            //    var fechaMin = historialDetconsumos.FindAll(p => p.Id > 0).Min(p => p.Fecha);
            //    histDetConsumo = historialDetconsumos.FirstOrDefault(p => p.Fecha == fechaMin);
            //}

            if (histDetConsumo != null)//SI ES DISTINTO DE NULL CUMPLE CON EL AÑO DE FACTURACION
            {
                Tom.AnioFactValido = true;               
                //PERIODO MENSUAL                    
                var periodoMinimo = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 2 && p.IDSECCION == 4).VALOR);
                var periodoMinimoBc = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 3 && p.IDSECCION == 4).VALOR);
                var nrofactorePotencia = _parseo.HistorialDetconsumos.Count(p => p.FactorPotencia >= factPotIdeal);

                _maximoFactoresPotenciaBC = !(nrofactorePotencia >= maximoFactoresBC);


                var rstNoTotalConsumo = historialDetconsumos.Count(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax);

                if (rstNoTotalConsumo >= periodoMinimo)
                {
                   
                    Tom.PeriodosValidos = true;
                    Tom.Periodos = rstNoTotalConsumo;

                    _periodosValidosBC = rstNoTotalConsumo >= periodoMinimoBc;

                    ValidacionFacturacion(historialDetconsumos);
                }
                else
                {
                    Tom.PeriodosValidos = false;
                    Tom.Periodos = rstNoTotalConsumo;
                    _periodosValidosBC = false;
                }

            }
            else
            {
               //PERIODO DE FACTURACION INVALIDA
                Tom.AnioFactValido = false;               
            }

        }

        private CompFacturacion CreaConceptos()
        {
            var idParametros = new[] { 1, 2, 3, 4, 5, 6, 18 };

            var comFacturacion = new CompFacturacion { ConceptosFacturacion = new List<CompConceptosFacturacion>() };

            foreach (var concepto in new ParametrosGlobales().FiltraPorCondicion(p => p.IDSECCION == 6 && idParametros.Contains(p.IDPARAMETRO)))
            {
                comFacturacion.ConceptosFacturacion.Add(new CompConceptosFacturacion
                {
                    IdConcepto = concepto.IDPARAMETRO,
                    Concepto = concepto.VALOR,
                });
            }

            return comFacturacion;
        }

        private void ValidacionFacturacion(List<CompHistorialDetconsumo> historialDetconsumos)
        {
            var valFactorPotencia = false;
            var validaFP = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 7).VALOR);
            var detConsumoFp = historialDetconsumos.FirstOrDefault(p => p.Id > 0 && p.FactorPotencia >= validaFP);//0.9M

            valFactorPotencia = detConsumoFp != null;

            var comFacturacion = CreaConceptos();

            Tom.FactSinAhorro = CalculoPeriodosFacturacion(comFacturacion, historialDetconsumos);

            //obtencion de cargo adicional y cargo a demanda maxima           
            //var idParametros = new[] { 1, 2, 4, 5, 6, 18};

            //var comFacturacion = new CompFacturacion {ConceptosFacturacion = new List<CompConceptosFacturacion>()};

            //foreach (var concepto in new ParametrosGlobales().FiltraPorCondicion(p => p.IDSECCION == 6 && idParametros.Contains(p.IDPARAMETRO)))
            //{
            //    comFacturacion.ConceptosFacturacion.Add(new CompConceptosFacturacion
            //    {
            //        IdConcepto = concepto.IDPARAMETRO,
            //        Concepto = concepto.VALOR,
            //    });
            //}  

            
           
            //if (valFactorPotencia)
            //{
            //    //debido a que el factor potencia supera los 0.9 o es igual no se realiza un solo consumo o demanda 
            //    //el calculo para la tarifa es de manera individual periodo por periodo                
            //    Tom.FactSinAhorro = CalculoPeriodosFacturacion(comFacturacion, historialDetconsumos);
            //}
            //else
            //{                
               
            //    var fechaActual = DateTime.Now.ToString("MMMM-yyyy").ToUpper();
            //    var idRegion = int.Parse(_tarifa.Substring(1, 1));

            //    var kTarifaOm = TarifaOm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);

            //    if (kTarifaOm != null)
            //    {
            //        Tom.ValidaFechaTablasTarifas = true;    
                
            //        var cargoAdicional = kTarifaOm.MT_CARGO_KWH_CONSUMO;
            //        var cargoDemandaMax = kTarifaOm.MT_CARGO_KW_DEMANDA;

            //        //INICIALIZAR VALORES PARA CARGO ADICIONAL Y DEMANDA MAXIMA
            //        comFacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional =
            //            (decimal) cargoAdicional;
            //        comFacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional =
            //            (decimal) cargoDemandaMax;


            //        //obtencion de consumo promedio y demanda maxima para realizar los calculos de la tarifa
            //        var costoPromedio = _parseo.InfoConsumo.Detalle.Promedio;
            //        var demandaMax = _parseo.InfoDemanda.Detalle.DemandaMax;
            //        var factorPotencia =
            //            Math.Round(
            //                _parseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia)/Tom.Periodos, 4);

            //        Tom.FactSinAhorro = comFacturacion;
            //        //INICIALIZACION PARA EL CONCEPTO DE KWH
            //        Tom.FactSinAhorro.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax =
            //            costoPromedio;

            //        //INICIALIZACION PARA EL CONCEPTO DE KW
            //        Tom.FactSinAhorro.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax =
            //            demandaMax;

            //        //INICIALIZACION PARA EL CONCEPTO DE FACTOR DE POTENCIA
            //        Tom.FactSinAhorro.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).FactorPotencia =
            //            factorPotencia;


            //        Tom.FactSinAhorro = Facturacion(Tom.FactSinAhorro, true);
            //    }
            //    else
            //    {
            //        Tom.ValidaFechaTablasTarifas = false;
            //    }
            //}

        }


        //private CompInfoConsDeman InfoDemandaConsumo(bool demanda, List<CompHistorialDetconsumo> historialDetconsumos)
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
        //    if (demanda)
        //    {
        //        var DemandaMax = historialDetconsumos.Where(p => p.Fecha <= detConsumoFechaMax && p.Fecha >= detalle.FechaMin).Max(p => p.Demanda);

        //        infoConsDeman.DemandaMax = Math.Round((decimal)DemandaMax,2);
        //        detalle.DemandaMax = infoConsDeman.DemandaMax;
        //    }
        //    else
        //    {                
        //        var rstConsTotal = historialDetconsumos.Where(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax).Sum(p => p.Consumo);

        //        var rstNoTotalConsumo = Tom.Periodos;

        //        infoConsDeman.Suma = rstConsTotal;
        //        detalle.Promedio = Math.Round(rstConsTotal /rstNoTotalConsumo,2);               
        //        Tom.Periodos = rstNoTotalConsumo;
                
        //    }

        //    infoConsDeman.Detalle = detalle;
        //    return infoConsDeman;
        //}


        public CompFacturacion Facturacion(CompFacturacion comfacturacion, bool sinAhorro)
        {                        
            //OPERACIONES PARA LOS CONCEPTOS            
            var cptokWh = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1);//kWh
            var cptokW = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2);//kW 
            var cptoFPotencia = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4);//factor potencia
            var cptoBonificacion = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 5);//BONIFICACION
            var cptoPenalizacion = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 6);//PENALIZACION
            var cptoFact = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 18);//FACT
            var cptoBajaTension = comfacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 3);

            cptoFPotencia.CPromedioODemMax = cptoFPotencia.FactorPotencia;
            cptokWh.Facturacion = Math.Round(cptokWh.CPromedioODemMax * cptokWh.CargoAdicional, 2, MidpointRounding.AwayFromZero);
            cptokW.Facturacion = Math.Round(cptokW.CPromedioODemMax * cptokW.CargoAdicional, 2, MidpointRounding.AwayFromZero);           

            //MONTO DE FACTURACION MENSUAL SIN IVA

            var medicion2porcent = ((_paramUnoFact)*(cptokW.Facturacion + cptokWh.Facturacion));
            var fact = Math.Round((cptokW.Facturacion + cptokWh.Facturacion) + medicion2porcent, 2, MidpointRounding.AwayFromZero);
            cptoBajaTension.Facturacion = Math.Round(medicion2porcent, 2, MidpointRounding.AwayFromZero);
          
            //VALIDACION DEL FACTOR DE POTENCIA
            //SI CUMPLE CON LA SIGUIENTE VALIDACION SE CALCULA LA PENALIZACION
            if (cptoFPotencia.FactorPotencia < _validaPenalizacion)//0.9000M
            {
                //SE CALCULA LA PENALIZACION
                try
                {
                    cptoPenalizacion.FactorPotencia = Math.Round((cptokW.Facturacion + cptokWh.Facturacion) * ((_paramUnoP / _paramDosP) * ((_paramTresP / cptoFPotencia.FactorPotencia) - _paramCuatroP)), 2, MidpointRounding.AwayFromZero);
                    cptoPenalizacion.Facturacion = Math.Round((cptokW.Facturacion + cptokWh.Facturacion) * ((_paramUnoP / _paramDosP) * ((_paramTresP / cptoFPotencia.FactorPotencia) - _paramCuatroP)), 2, MidpointRounding.AwayFromZero);
                }
                catch (DivideByZeroException)
                {
                    cptoPenalizacion.FactorPotencia = 0;
                    cptoPenalizacion.Facturacion = 0;
                }


                cptoFact.FactorPotencia = Math.Round(fact + cptoPenalizacion.FactorPotencia, 2, MidpointRounding.AwayFromZero);
            }
            else if (cptoFPotencia.FactorPotencia >= _validaBonificacion)//0.9100M
            {
                try
                {
                    cptoBonificacion.FactorPotencia = Math.Round((cptokW.Facturacion + cptokWh.Facturacion) * ((_paramUnoB / _paramDosB) * (_paramTresB - (_paramCuatroB / cptoFPotencia.FactorPotencia))), 2, MidpointRounding.AwayFromZero);
                    cptoBonificacion.Facturacion = Math.Round((cptokW.Facturacion + cptokWh.Facturacion) * ((_paramUnoB / _paramDosB) * (_paramTresB - (_paramCuatroB / cptoFPotencia.FactorPotencia))), 2, MidpointRounding.AwayFromZero);
                }               
                catch (DivideByZeroException)
                {
                    cptoBonificacion.FactorPotencia = 0;
                    cptoBonificacion.Facturacion = 0;
                }

                cptoFact.FactorPotencia = Math.Round(fact - cptoBonificacion.FactorPotencia, 2, MidpointRounding.AwayFromZero);
            }
            else if (cptoFPotencia.FactorPotencia >= _validaPenalizacion && cptoFPotencia.FactorPotencia < _validaBonificacion)
            {
                cptoFact.FactorPotencia = fact;
            }
            

            comfacturacion.ConceptosFacturacion.Clear();                                                                       

            comfacturacion.ConceptosFacturacion.Add(cptokWh);
            comfacturacion.ConceptosFacturacion.Add(cptokW);
            comfacturacion.ConceptosFacturacion.Add(cptoBajaTension);
            comfacturacion.ConceptosFacturacion.Add(cptoFPotencia);
            comfacturacion.ConceptosFacturacion.Add(cptoBonificacion);
            comfacturacion.ConceptosFacturacion.Add(cptoPenalizacion);
            comfacturacion.ConceptosFacturacion.Add(cptoFact);


            var subTotal = Math.Round(cptoFact.FactorPotencia, 2, MidpointRounding.AwayFromZero); //Math.Round(comfacturacion.ConceptosFacturacion.Sum(p => p.Facturacion),2);
            var iva = Math.Round(subTotal * (_claveIva / _divisor), 2, MidpointRounding.AwayFromZero); //CLAVE IVA
            var total = Math.Round(subTotal + iva, 2, MidpointRounding.AwayFromZero);
            var pagoFacturaBiOMe = Math.Round(total * Convert.ToInt32(_tipoFacturacion), 2, MidpointRounding.AwayFromZero);//1 = FACTURACION
            var montoMaxFacturar = 0.0M;

            if(sinAhorro)
                montoMaxFacturar = Math.Round(pagoFacturaBiOMe * Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 3).VALOR), 2, MidpointRounding.AwayFromZero);

            comfacturacion.Subtotal = subTotal;
            comfacturacion.Iva = iva;
            comfacturacion.Total = total;
            comfacturacion.PagoFactBiMen = pagoFacturaBiOMe;
            comfacturacion.MontoMaxFacturar = montoMaxFacturar;
            comfacturacion.MontoFactMensualSNIVA = fact;

            return comfacturacion;
        }


        private CompFacturacion CalculoPeriodosFacturacion(CompFacturacion compFacturacion, List<CompHistorialDetconsumo> historialDetconsumosconsumos)
        {
            var facturacionFinal = new CompFacturacion();
            var listPeriodosFacturacion = new List<CompFacturacion>();
            

            //VALIDACION DE PERIODOS QUE ENTRAN EN EL RANGO DE LOS PERIODOS PERMITIDOS
            foreach (var detalleConsumo in historialDetconsumosconsumos.FindAll(me => me.Id > 0))
            {
                if (detalleConsumo.Id > 0)
                {
                    var fechaActual = detalleConsumo.Fecha.Value.ToString("MMMM-yyyy").ToUpper();
                    var idRegion = int.Parse(_tarifa.Substring(1, 1));
                    var kTarifaOm = TarifaOm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);

                    if (kTarifaOm == null)
                    {
                        Tom.ValidaFechaTablasTarifas = false;
                        break;
                    }
                    else
                    {
                        Tom.ValidaFechaTablasTarifas = true;
                    }

                    var cargoPromedio = kTarifaOm.MT_CARGO_KWH_CONSUMO;
                    var cargoDemandaMax = kTarifaOm.MT_CARGO_KW_DEMANDA;

                    var compFacturacionFP = CreaConceptos();

                    //OBTENER Y CREAR CONCEPTOS DE FACTURACION DE ACUERDO AL NUMERO DE PERIODOS VALIDOS
                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax = detalleConsumo.Consumo;
                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional = (decimal)cargoPromedio;
                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax = detalleConsumo.Demanda;
                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional = (decimal)cargoDemandaMax;

                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).FactorPotencia = detalleConsumo.FactorPotencia;

                    var facturacion = Facturacion(compFacturacionFP, true);
                    listPeriodosFacturacion.Add(facturacion);                            
                }                       

            }
                        
            /* ESTA LINEA ES PARA MANTENER EN MEMORIA LOS OBJETOS QUE POSIBLEMENTE SE ALMACENEN EN 
             * LA BASE DE DATOS DEPENDIENDO DE LAS VALIDACIONES PROPUESTAS
            */

            _periodosFacturados = listPeriodosFacturacion;

            //REALIZAR LA SUMATORIA DE TODOS LOS PERIODOS Y OBTENER UNA FACTURACION SIN AHORRO
            facturacionFinal.Iva = Math.Round(listPeriodosFacturacion.Sum(p=> p.Iva)/Tom.Periodos,2);
            facturacionFinal.MontoFactMensualSNIVA = Math.Round(listPeriodosFacturacion.Sum(p => p.MontoFactMensualSNIVA) / Tom.Periodos, 2);
            facturacionFinal.MontoMaxFacturar = Math.Round(listPeriodosFacturacion.Sum(p => p.MontoMaxFacturar) / Tom.Periodos, 2);
            facturacionFinal.PagoFactBiMen = Math.Round(listPeriodosFacturacion.Sum(p => p.PagoFactBiMen) / Tom.Periodos, 2);
            facturacionFinal.Subtotal = Math.Round(listPeriodosFacturacion.Sum(p => p.Subtotal) / Tom.Periodos, 2);
            facturacionFinal.Total = Math.Round(listPeriodosFacturacion.Sum(p => p.Total) / Tom.Periodos, 2);

            var facturacionOM = new StringBuilder();
            foreach (var facturacion in listPeriodosFacturacion)
            {
                facturacionOM.AppendLine(facturacion.Total.ToString());
            }

            var bandera = facturacionOM.ToString();

            return facturacionFinal;
        }

        public CompFacturacion FacturacionFutura(decimal consumoPromedio, decimal demandaMaxima)
        {
            var compFacturacionFutura = CreaConceptos();
            //var fechaActual = DateTime.Now.ToString("MMMM-yyyy").ToUpper();
            var idRegion = int.Parse(_tarifa.Substring(1, 1));

            //var kTarifaOm = TarifaOm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);
            //var cargoAdicional = kTarifaOm.MT_CARGO_KWH_CONSUMO;
            //var cargoDemandaMax = kTarifaOm.MT_CARGO_KW_DEMANDA;

            var cargoAdicional = 0.0M;
            var cargoDemandaMax = 0.0M;

            foreach (var detalleConsumo in _parseo.HistorialDetconsumos.FindAll(me => me.Id > 0))
            {
                var fechaActual = detalleConsumo.Fecha.Value.ToString("MMMM-yyyy").ToUpper();
                //var idRegion = int.Parse(_tarifa.Substring(1, 1));
                var kTarifaOm = TarifaOm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);

                cargoAdicional = cargoAdicional + (decimal)kTarifaOm.MT_CARGO_KWH_CONSUMO;
                cargoDemandaMax = cargoDemandaMax + (decimal)kTarifaOm.MT_CARGO_KW_DEMANDA;
            }

            cargoAdicional = RedondeaValor(cargoAdicional / Tom.Periodos);
            cargoDemandaMax = RedondeaValor(cargoDemandaMax / Tom.Periodos);

            //INICIALIZAR VALORES PARA CARGO ADICIONAL Y DEMANDA MAXIMA
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional =
                (decimal)cargoAdicional;
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional =
                (decimal)cargoDemandaMax;

            var costoPromedio = consumoPromedio;
            var demandaMax = demandaMaxima;
            var factorPotencia =
                Math.Round(
                    _parseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia) / Tom.Periodos, 4);

            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax =
                        costoPromedio;

            //INICIALIZACION PARA EL CONCEPTO DE KW
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax =
                demandaMax;

            //INICIALIZACION PARA EL CONCEPTO DE FACTOR DE POTENCIA
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).FactorPotencia =
                factorPotencia;

            compFacturacionFutura = Facturacion(compFacturacionFutura, false);

            return compFacturacionFutura;
        }

        private decimal CalculakVAR()
        {
            var kvar = 0.0M;

            //PROMEDIO DEL FACTOR DE POTENCIA SIN TOMAR EN CUENTA REGISTRO MAYORES 0.9000
            //PARAMETROS PARA EL KVAR

            var factPotIdeal = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDSECCION == 11 && p.IDPARAMETRO == 1).VALOR);
            var demandaMax = _parseo.InfoDemanda.DemandaMax;
            var factPotActual = _parseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia) / _parseo.HistorialDetconsumos.Count(p => p.Id > 0);

            kvar = demandaMax * (((decimal)(Math.Sqrt((1 - Math.Pow((double)factPotActual, 2)))) / factPotActual) - ((decimal)(Math.Sqrt(1 - Math.Pow((double)factPotIdeal, 2))) / factPotIdeal));

            var var1 = ((decimal) (Math.Sqrt((1 - Math.Pow((double) factPotActual, 2))))/factPotActual);

            
            return RedondeaValor(kvar);
        }

        private decimal RedondeaValor(decimal valor)
        {
            valor = Math.Truncate(valor * 1000000) / 1000000;
            valor = Math.Round(valor, 4);

            return valor;
        }

        #region PROCESO DE SUBESTACIONES ELECTRICAS

        public CompFacturacion FacturacionFuturaCambioTarifa(string tarifaOrigen, decimal consumoPromedio, decimal ahorroConsumo,
                                                 decimal demandaMaxima, decimal ahorroDemanda, bool combinaTecnologias)
        {
            var compFacturacionFutura = CreaConceptos();
            var factPotIdeal = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDSECCION == 11 && p.IDPARAMETRO == 1).VALOR);
            var fechaActual = DateTime.Now.ToString("MMMM-yyyy").ToUpper();
            var hMOm = 360.0M;
            var demandaPromedio = 0.0M;

            var lstTarifaOM = TarifaOm.ObtenTarifasPorCondicion(p => p.FECHA_APLICABLE == fechaActual);


            if (tarifaOrigen == "02")
                demandaPromedio = consumoPromedio / hMOm;

            if (tarifaOrigen == "03")
                demandaPromedio = demandaMaxima;

            if (combinaTecnologias)
            {
                consumoPromedio = consumoPromedio - ahorroConsumo;
                demandaPromedio = demandaPromedio - ahorroDemanda;
            }

            consumoPromedio = Math.Round(consumoPromedio, 4);
            demandaPromedio = Math.Round(demandaPromedio, 4);

            var totalReistrosOM = lstTarifaOM.Count;
            var sumaDemanda = lstTarifaOM.Sum(p => p.MT_CARGO_KW_DEMANDA);
            var sumaConsumo = lstTarifaOM.Sum(p => p.MT_CARGO_KWH_CONSUMO);

            var cargoConsumo = sumaConsumo / totalReistrosOM;
            var cargoDemandaMax = sumaDemanda / totalReistrosOM;

            //INICIALIZAR VALORES PARA CARGO ADICIONAL Y DEMANDA MAXIMA
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional =
                (decimal)cargoConsumo;
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional =
                (decimal)cargoDemandaMax;

            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax =
                        consumoPromedio;

            //INICIALIZACION PARA EL CONCEPTO DE KW
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax =
                demandaPromedio;

            //INICIALIZACION PARA EL CONCEPTO DE FACTOR DE POTENCIA
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).FactorPotencia =
                factPotIdeal;

            compFacturacionFutura = Facturacion(compFacturacionFutura, false);

            return compFacturacionFutura;
        }

        #endregion

        #region PROCESO BANCO DE CAPACITORES

        public CompFacturacion FacturacionFuturaBC(decimal consumoPromedio, decimal ahorroConsumo,
                                                 decimal demandaMaxima, decimal ahorroDemanda, bool combinaTecnologias)
        {
            var compFacturacionFutura = CreaConceptos();
            var idRegion = int.Parse(_tarifa.Substring(1, 1));
            var factPotIdeal = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDSECCION == 11 && p.IDPARAMETRO == 1).VALOR);

            if (combinaTecnologias)
            {
                consumoPromedio = consumoPromedio - ahorroConsumo;
                demandaMaxima = demandaMaxima - ahorroDemanda;
            }

            var cargoAdicional = 0.0M;
            var cargoDemandaMax = 0.0M;

            foreach (var detalleConsumo in _parseo.HistorialDetconsumos.FindAll(me => me.Id > 0))
            {
                var fechaActual = detalleConsumo.Fecha.Value.ToString("MMMM-yyyy").ToUpper();
                var kTarifaOm = TarifaOm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);

                cargoAdicional = cargoAdicional + (decimal)kTarifaOm.MT_CARGO_KWH_CONSUMO;
                cargoDemandaMax = cargoDemandaMax + (decimal)kTarifaOm.MT_CARGO_KW_DEMANDA;
            }

            cargoAdicional = RedondeaValor(cargoAdicional / Tom.Periodos);
            cargoDemandaMax = RedondeaValor(cargoDemandaMax / Tom.Periodos);

            //var kTarifaOm = TarifaOm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);
            //var cargoAdicional = kTarifaOm.MT_CARGO_KWH_CONSUMO;
            //var cargoDemandaMax = kTarifaOm.MT_CARGO_KW_DEMANDA;

            //consumoPromedio = Math.Round(consumoPromedio, 4);
            //demandaMaxima = Math.Round(demandaMaxima, 4);


            //INICIALIZAR VALORES PARA CARGO ADICIONAL Y DEMANDA MAXIMA
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional =
                (decimal)cargoAdicional;
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional =
                (decimal)cargoDemandaMax;

            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax =
                        consumoPromedio;

            //INICIALIZACION PARA EL CONCEPTO DE KW
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax =
                demandaMaxima;

            //INICIALIZACION PARA EL CONCEPTO DE FACTOR DE POTENCIA
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).FactorPotencia =
                factPotIdeal;

            compFacturacionFutura = Facturacion(compFacturacionFutura, false);

            return compFacturacionFutura;
        }

        #endregion

    }
}
