using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.Tarifas;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Tarifas;
using PAEEEM.Entidades.Trama;

namespace PAEEEM.LogicaNegocios.Tarifas
{
    public class AlgoritmoTarifaHM
    {
        private readonly CompTarifa _objComplejoHm = new CompTarifa();
        private readonly int _claveIva = 0;
        private readonly int _tipoFacturacion = 0;
        private readonly string _tarifa = "";        
        private readonly CompParseo _parseo = new CompParseo();        
        private List<CompFacturacion> _periodosFacturados = new List<CompFacturacion>();
        private decimal _validaPenalizacion = 0.0M;
        private decimal _validaBonificacion = 0.0M;
        private decimal _divisor = 0.0M;        

        //VARIABLES PARA LA PENALIZACION
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
           

        public CompTarifa Thm
        {
            get { return _objComplejoHm; }
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

        public AlgoritmoTarifaHM(CompParseo parseo)
        {
            CargaParametrosIniciales();
            
            _claveIva = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).ValorEntero ;
            _tipoFacturacion = int.Parse(parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato);
            _tarifa = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;

            _parseo = parseo;       

            ValidacionTarifa(_parseo.HistorialDetconsumos);

            if (Thm.AnioFactValido && Thm.PeriodosValidos)
            {
                if (_periodosValidosBC && maximoFactoresPotenciaBC)
                    _kvar = CalculakVAR();
            }
        }

        public AlgoritmoTarifaHM(CompParseo parseo, bool factConAhorro)
        {
            CargaParametrosIniciales();

            _claveIva = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).ValorEntero;
            _tipoFacturacion = int.Parse(parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato);
            _tarifa = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;

            _parseo = parseo;

            var detConsumoFechaMax = _parseo.HistorialDetconsumos.Max(p => p.Fecha);
            var detConsumoFechaMin = detConsumoFechaMax.Value.AddMonths(-11);
            var rstNoTotalConsumo = _parseo.HistorialDetconsumos.Count(p => p.Fecha >= detConsumoFechaMin && p.Fecha <= detConsumoFechaMax);
            Thm.Periodos = rstNoTotalConsumo;
        }

        private void CargaParametrosIniciales()
        {
            parametrosGlobales = new ParametrosGlobales().FiltraPorCondicion(p => new[] { 7, 8, 9, 10, 2}.Contains(p.IDSECCION));
            _validaPenalizacion = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 2 && p.IDSECCION == 7).VALOR);
            _validaBonificacion = Convert.ToDecimal(parametrosGlobales.First(p => p.IDPARAMETRO == 3 && p.IDSECCION == 7).VALOR);
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
                Thm.AnioFactValido = true;              
                //PERIODO MENSUAL             
                
                var periodoMinimo = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 2 && p.IDSECCION == 4).VALOR);
                var periodoMinimoBc = int.Parse(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 3 && p.IDSECCION == 4).VALOR);
                var nrofactorePotencia = _parseo.HistorialDetconsumos.Count(p => p.FactorPotencia >= factPotIdeal);

                _maximoFactoresPotenciaBC = !(nrofactorePotencia >= maximoFactoresBC);
                
                var rstNoTotalConsumo = historialDetconsumos.Where(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax).Select(p => p.Fecha).Distinct().Count();

                if (rstNoTotalConsumo >= periodoMinimo)
                {
                   
                    Thm.PeriodosValidos = true;
                    Thm.Periodos = rstNoTotalConsumo;
                    _periodosValidosBC = rstNoTotalConsumo >= periodoMinimoBc;            

                    ValidacionFacturacion(historialDetconsumos);
                }
                else
                {
                    Thm.PeriodosValidos = false;
                    Thm.Periodos = rstNoTotalConsumo;
                    _periodosValidosBC = false;               
                }

            }
            else
            {
                //PERIODO DE FACTURACION INVALIDA
                Thm.AnioFactValido = false;                               
            }

        }

        private CompFacturacion CreaConceptos()
        {
            var idParametros = new[] { 1, 2, 4, 5, 6, 18 };

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
            var validaFP = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 7).VALOR);
            var detConsumoFp = historialDetconsumos.Where(p => p.Id > 0 && p.FactorPotencia >= validaFP);//0.9M

            var valFactorPotencia = detConsumoFp.Any();

            var comFacturacion = CreaConceptos();
            Thm.FactSinAhorro = CalculoPeriodosFacturacion(comFacturacion, historialDetconsumos);

            //obtencion de cargo adicional y cargo a demanda maxima                    
            //var idParametros = new[] { 1, 2, 4, 5, 6 , 18};

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
            //    Thm.FactSinAhorro = CalculoPeriodosFacturacion(comFacturacion, historialDetconsumos);
            //}
            //else
            //{
            //    var fechaActual = DateTime.Now.ToString("MMMM-yyyy").ToUpper();
            //    var idRegion = int.Parse(_tarifa.Substring(1, 1));

            //    var kTarifaHm = TarifaHm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);


            //    if (kTarifaHm != null)
            //    {
            //        Thm.ValidaFechaTablasTarifas = true;
            //        var cargoPromedio = (decimal) kTarifaHm.MT_CARGO_BASE + (decimal) kTarifaHm.MT_CARGO_INTERMEDIA +
            //                            (decimal) kTarifaHm.MT_CARGO_PUNTA/3.0M;
            //        var cargoDemandaMax = kTarifaHm.MT_CARGO_DEMANDA;

            //        cargoPromedio = Math.Round(cargoPromedio, 4);
            //        cargoDemandaMax = Math.Round(cargoDemandaMax, 4);

            //        //INICIALIZAR VALORES PARA CARGO ADICIONAL Y DEMANDA MAXIMA
            //        comFacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional =
            //            cargoPromedio;
            //        comFacturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional =
            //            (decimal) cargoDemandaMax;

            //        //obtencion de consumo promedio y demanda maxima para realizar los calculos de la tarifa
            //        var costoPromedio = _parseo.InfoConsumo.Detalle.Promedio;
            //        var demandaMax = _parseo.InfoDemanda.Detalle.DemandaMax;
            //        var factorPotencia = _parseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia)/
            //                             Thm.Periodos;
            //        factorPotencia = Math.Round(factorPotencia, 4);

            //        Thm.FactSinAhorro = comFacturacion;
            //        //INICIALIZACION PARA EL CONCEPTO DE KWH
            //        Thm.FactSinAhorro.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax =
            //            costoPromedio;

            //        //INICIALIZACION PARA EL CONCEPTO DE KW
            //        Thm.FactSinAhorro.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax =
            //            demandaMax;

            //        //INICIALIZACION PARA EL CONCEPTO DE FACTOR DE POTENCIA
            //        Thm.FactSinAhorro.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).FactorPotencia =
            //            factorPotencia;


            //        Thm.FactSinAhorro = Facturacion(Thm.FactSinAhorro, true);
            //    }
            //    else
            //    {
            //        Thm.ValidaFechaTablasTarifas = false;
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
           
        //    //volvemos a inicializar la fecha de consumo minima
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
        //        var rstNoTotalConsumo = historialDetconsumos.Count(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax);

        //        infoConsDeman.Suma = rstConsTotal;
        //        detalle.Promedio = Math.Round(rstConsTotal / rstNoTotalConsumo,2);                                
        //    }

        //    infoConsDeman.Detalle = detalle;
        //    return infoConsDeman;
        //}


        public CompFacturacion Facturacion(CompFacturacion facturacion, bool sinAhorro)
        {            

            //OPERACIONES PARA LOS CONCEPTOS            
            var cptokWh = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1);//kWh
            var cptokW = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2);//kW 
            var cptoFPotencia = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4);//factor potencia
            var cptoBonificacion = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 5);//BONIFICACION
            var cptoPenalizacion = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 6);//PENALIZACION
            var cptoFact = facturacion.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 18);//FACT

            cptoFPotencia.CPromedioODemMax = cptoFPotencia.FactorPotencia;
            cptokWh.Facturacion = Math.Round(cptokWh.CPromedioODemMax * cptokWh.CargoAdicional, 2, MidpointRounding.AwayFromZero);
            cptokW.Facturacion = Math.Round(cptokW.CPromedioODemMax * cptokW.CargoAdicional, 2, MidpointRounding.AwayFromZero);


            var fact = Math.Round(cptokW.Facturacion + cptokWh.Facturacion, 2, MidpointRounding.AwayFromZero);

            //VALIDACION DEL FACTOR DE POTENCIA
            //SI CUMPLE CON LA SIGUIENTE VALIDACION SE CALCULA LA PENALIZACION
            if (cptoFPotencia.FactorPotencia < _validaPenalizacion)//0.9000M
            {
                //SE CALCULA LA PENALIZACION
                try
                {
                    cptoPenalizacion.FactorPotencia = Math.Round((cptokW.Facturacion + cptokWh.Facturacion) * ((_paramUnoP / _paramDosP) * ((_paramTresP / cptoFPotencia.FactorPotencia) - _paramCuatroP)), 2, MidpointRounding.AwayFromZero);
                    cptoPenalizacion.Facturacion = cptoPenalizacion.FactorPotencia;
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
                //SE CALCULA LA BONIFICACION
                try
                {
                    cptoBonificacion.FactorPotencia = Math.Round((cptokW.Facturacion + cptokWh.Facturacion) * ((_paramUnoB / _paramDosB) * (_paramTresB - (_paramCuatroB / cptoFPotencia.FactorPotencia))), 2, MidpointRounding.AwayFromZero);
                    cptoBonificacion.Facturacion = cptoBonificacion.FactorPotencia;
                }
                catch (Exception)
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


            facturacion.ConceptosFacturacion.Clear();

            facturacion.ConceptosFacturacion.Add(cptokWh);
            facturacion.ConceptosFacturacion.Add(cptokW);
            facturacion.ConceptosFacturacion.Add(cptoFPotencia);
            facturacion.ConceptosFacturacion.Add(cptoBonificacion);
            facturacion.ConceptosFacturacion.Add(cptoPenalizacion);
            facturacion.ConceptosFacturacion.Add(cptoFact);

            var subTotal = Math.Round(cptoFact.FactorPotencia, 2, MidpointRounding.AwayFromZero);//Math.Round(facturacion.ConceptosFacturacion.Sum(p => p.Facturacion),2);
            var iva = Math.Round(subTotal * (_claveIva / _divisor), 2, MidpointRounding.AwayFromZero); //CLAVE IVA
            var total = Math.Round(subTotal + iva, 2, MidpointRounding.AwayFromZero);
            var pagoFacturaBiOMe = Math.Round(total * Convert.ToInt32(_tipoFacturacion), 2, MidpointRounding.AwayFromZero);//1 = FACTURACION
            var montoMaxFacturar = 0.0M;

            if (sinAhorro)
                montoMaxFacturar = Math.Round(pagoFacturaBiOMe * Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 3).VALOR), 2, MidpointRounding.AwayFromZero);

            facturacion.Subtotal = subTotal;
            facturacion.Iva = iva;
            facturacion.Total = total;
            facturacion.PagoFactBiMen = pagoFacturaBiOMe;
            facturacion.MontoMaxFacturar = montoMaxFacturar;
            facturacion.MontoFactMensualSNIVA = fact;

            return facturacion;
        }


        private CompFacturacion CalculoPeriodosFacturacion(CompFacturacion compFacturacion, List<CompHistorialDetconsumo> historialDetconsumos)
        {
            var facturacionFinal = new CompFacturacion();
            var listPeriodosFacturacion = new List<CompFacturacion>();
            

            //VALIDACION DE PERIODOS QUE ENTRAN EN EL RANGO DE LOS PERIODOS PERMITIDOS
            foreach (var detalleConsumo in historialDetconsumos)
            {
                if (detalleConsumo.Id > 0)
                {

                    var fechaActual = detalleConsumo.Fecha.Value.ToString("MMMM-yyyy");
                    var idRegion = int.Parse(_tarifa.Substring(1, 1));
                    var kTarifaHm = TarifaHm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);

                    if (kTarifaHm == null)
                    {
                        Thm.ValidaFechaTablasTarifas = false;
                        break;
                    }
                    else
                    {
                        Thm.ValidaFechaTablasTarifas = true;
                    }
                                          
                    var cargoPromedio = (kTarifaHm.MT_CARGO_BASE + kTarifaHm.MT_CARGO_INTERMEDIA + kTarifaHm.MT_CARGO_PUNTA) / 3;
                    var cargoDemandaMax = kTarifaHm.MT_CARGO_DEMANDA;

                    var compFacturacionFP = CreaConceptos();

                    //OBTENER Y CREAR CONCEPTOS DE FACTURACION DE ACUERDO AL NUMERO DE PERIODOS VALIDOS
                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax = detalleConsumo.Consumo;
                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional = Math.Round((decimal)cargoPromedio,4);
                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax = detalleConsumo.Demanda;
                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional = (decimal)cargoDemandaMax;

                    compFacturacionFP.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).FactorPotencia =
                        Math.Round(detalleConsumo.FactorPotencia, 4);

                    //listPeriodosFacturacion.Add(Facturacion(compFacturacionFP, true));
                    var facturacion = Facturacion(compFacturacionFP, true);
                    listPeriodosFacturacion.Add(facturacion);
                }
                        
            }

            /*ESTA LINEA ES PARA MANTENER EN MEMORIA LOS OBJETOS QUE POSIBLEMENTE SE ALMACENEN EN 
            *LA BASE DE DATOS DEPENDIENDO DE LAS VALIDACIONES PROPUESTAS
            */

            _periodosFacturados = listPeriodosFacturacion;

            //REALIZAR LA SUMATORIA DE TODOS LOS PERIODOS Y OBTENER UNA FACTURACION SIN AHORRO
            facturacionFinal.Iva = Math.Round(listPeriodosFacturacion.Sum(p => p.Iva) / Thm.Periodos, 2, MidpointRounding.AwayFromZero);
            facturacionFinal.MontoFactMensualSNIVA = Math.Round(listPeriodosFacturacion.Sum(p => p.MontoFactMensualSNIVA) / Thm.Periodos, 2, MidpointRounding.AwayFromZero);
            facturacionFinal.MontoMaxFacturar = Math.Round(listPeriodosFacturacion.Sum(p => p.MontoMaxFacturar) / Thm.Periodos, 2, MidpointRounding.AwayFromZero);
            facturacionFinal.PagoFactBiMen = Math.Round(listPeriodosFacturacion.Sum(p => p.PagoFactBiMen) / Thm.Periodos, 2, MidpointRounding.AwayFromZero);
            facturacionFinal.Subtotal = Math.Round(listPeriodosFacturacion.Sum(p => p.Subtotal) / Thm.Periodos, 2, MidpointRounding.AwayFromZero);
            facturacionFinal.Total = Math.Round(listPeriodosFacturacion.Sum(p => p.Total) / Thm.Periodos, 2, MidpointRounding.AwayFromZero);

            var facturacionhm = new StringBuilder();
            foreach (var facturacion in listPeriodosFacturacion)
            {
                facturacionhm.AppendLine(facturacion.Total.ToString());
            }

            var bandera = facturacionhm.ToString();
            
            return facturacionFinal;
        }

        
        public CompFacturacion FacturacionFutura(decimal consumoPromedio, decimal demandaMaxima)
        {
            var compFacturacionFutura = CreaConceptos();

            var idRegion = int.Parse(_tarifa.Substring(1, 1));

            var cargoPromedio = 0.0M;
            var cargoDemandaMax = 0.0M;

            //var kTarifaHm = TarifaHm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);

            //if (kTarifaHm != null)
            //{
            foreach (var detalleConsumo in _parseo.HistorialDetconsumos.FindAll(me => me.Id > 0))
            {
                var fechaActual = detalleConsumo.Fecha.Value.ToString("MMMM-yyyy").ToUpper();
                var kTarifaHm = TarifaHm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);

                var costoConsumo = Math.Round(((decimal)kTarifaHm.MT_CARGO_BASE + (decimal)kTarifaHm.MT_CARGO_INTERMEDIA +
                                        (decimal)kTarifaHm.MT_CARGO_PUNTA) / 3.0M, 4);

                var costoDemanda = (decimal)kTarifaHm.MT_CARGO_DEMANDA;

                cargoPromedio = cargoPromedio + costoConsumo;
                cargoDemandaMax = cargoDemandaMax + costoDemanda;
            }

            cargoPromedio = Math.Round(cargoPromedio/Thm.Periodos, 4);
            cargoDemandaMax = Math.Round(cargoDemandaMax/Thm.Periodos, 4);
            
            //cargoPromedio = Math.Round(cargoPromedio, 4);
            //cargoDemandaMax = Math.Round(cargoDemandaMax, 4);

            //INICIALIZAR VALORES PARA CARGO ADICIONAL Y DEMANDA MAXIMA
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional =
                cargoPromedio;
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional =
                (decimal)cargoDemandaMax;

            //obtencion de consumo promedio y demanda maxima para realizar los calculos de la tarifa
            var costoPromedio = consumoPromedio;
            var demandaMax = demandaMaxima;
            var factorPotencia = _parseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia) /
                                    Thm.Periodos;
            factorPotencia = Math.Round(factorPotencia, 4);

            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax =
                    costoPromedio;

            //INICIALIZACION PARA EL CONCEPTO DE KW
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax =
                demandaMax;

            //INICIALIZACION PARA EL CONCEPTO DE FACTOR DE POTENCIA
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).FactorPotencia =
                factorPotencia;


            compFacturacionFutura = Facturacion(compFacturacionFutura, true);
            //}

            return compFacturacionFutura;
        }

        #region PROCESO DE SUBESTACIONES ELECTRICAS

        public CompFacturacion FacturacionFuturaCambioTarifa(string tarifaOrigen, decimal consumoPromedio,
                                                             decimal ahorroConsumo,
                                                             decimal demandaMaxima, decimal ahorroDemanda,
                                                             bool combinaTecnologias)
        {
            var compFacturacionFutura = CreaConceptos();
            var factPotIdeal = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDSECCION == 11 && p.IDPARAMETRO == 1).VALOR);
            var fechaActual = DateTime.Now.ToString("MMMM-yyyy").ToUpper();
            var hMOm = 360.0M;
            var demandaPromedio = 0.0M;

            var lstTarifaHM = TarifaHm.ObtenTarifasPorCondicion(p => p.FECHA_APLICABLE == fechaActual);

            var sumacargoPromedio = 0.0M;
            var sumacargoDemandaMax = 0.0M;
            
            foreach (var kTarifaHm in lstTarifaHM)
            {
                var cargoConsumo = (decimal)kTarifaHm.MT_CARGO_BASE + (decimal)kTarifaHm.MT_CARGO_INTERMEDIA +
                                        (decimal)kTarifaHm.MT_CARGO_PUNTA / 3.0M;
                var cargoDemanda = (decimal)kTarifaHm.MT_CARGO_DEMANDA;

                sumacargoPromedio = sumacargoPromedio + cargoConsumo;
                sumacargoDemandaMax = sumacargoDemandaMax + cargoDemanda;
            }

            var cargoPromedio = Convert.ToDecimal(sumacargoPromedio/lstTarifaHM.Count);
            var cargoDemandaMax = Convert.ToDecimal(sumacargoDemandaMax / lstTarifaHM.Count);

            cargoPromedio = Math.Round(cargoPromedio, 4);
            cargoDemandaMax = Math.Round(cargoDemandaMax, 4);

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

            //INICIALIZAR VALORES PARA CARGO ADICIONAL Y DEMANDA MAXIMA
            compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional =
                (decimal)cargoPromedio;
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

            var fechaActual = DateTime.Now.ToString("MMMM-yyyy").ToUpper();
            var idRegion = int.Parse(_tarifa.Substring(1, 1));

            if (combinaTecnologias)
            {
                consumoPromedio = consumoPromedio - ahorroConsumo;
                demandaMaxima = demandaMaxima - ahorroDemanda;
            }

            var kTarifaHm = TarifaHm.ObtienePorCondicion(p => p.FECHA_APLICABLE == fechaActual && p.ID_REGION == idRegion);

            if (kTarifaHm != null)
            {
                var cargoPromedio = ((decimal)kTarifaHm.MT_CARGO_BASE + (decimal)kTarifaHm.MT_CARGO_INTERMEDIA +
                                        (decimal)kTarifaHm.MT_CARGO_PUNTA) / 3.0M;
                var cargoDemandaMax = kTarifaHm.MT_CARGO_DEMANDA;

                cargoPromedio = Math.Round(cargoPromedio, 4);
                cargoDemandaMax = Math.Round(cargoDemandaMax, 4);

                //INICIALIZAR VALORES PARA CARGO ADICIONAL Y DEMANDA MAXIMA
                compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CargoAdicional =
                    cargoPromedio;
                compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CargoAdicional =
                    (decimal)cargoDemandaMax;

                //obtencion de consumo promedio y demanda maxima para realizar los calculos de la tarifa
                var costoPromedio = consumoPromedio;
                var demandaMax = demandaMaxima;
                var factorPotencia = _parseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia) /
                                     Thm.Periodos;
                factorPotencia = Math.Round(factorPotencia, 4);

                compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 1).CPromedioODemMax =
                        costoPromedio;

                //INICIALIZACION PARA EL CONCEPTO DE KW
                compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 2).CPromedioODemMax =
                    demandaMax;

                //INICIALIZACION PARA EL CONCEPTO DE FACTOR DE POTENCIA
                compFacturacionFutura.ConceptosFacturacion.FirstOrDefault(p => p.IdConcepto == 4).FactorPotencia =
                    factorPotencia;


                compFacturacionFutura = Facturacion(compFacturacionFutura, true);
            }

            return compFacturacionFutura;
        }

        #endregion
        //EMPLEADO PARA OBTENER EL 25% LIMITE MAYOR DEL PRECIO DE UN EQUIPO ALTA EFICIENCIA
        private decimal CalculakVAR()
        {
            var kvar = 0.0M;

            //PROMEDIO DEL FACTOR DE POTENCIA SIN TOMAR EN CUENTA REGISTRO MAYORES 0.9000
            //PARAMETROS PARA EL KVAR

            var factPotIdeal = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDSECCION == 11 && p.IDPARAMETRO == 1).VALOR);
            var demandaMax = _parseo.InfoDemanda.DemandaMax;
            var factPotActual = _parseo.HistorialDetconsumos.Where(p => p.Id > 0).Sum(p => p.FactorPotencia) / _parseo.HistorialDetconsumos.Count(p => p.Id > 0);

            kvar = demandaMax * (((decimal)(Math.Sqrt((1 - Math.Pow((double)factPotActual, 2)))) / factPotActual) - ((decimal)(Math.Sqrt(1 - Math.Pow((double)factPotIdeal, 2))) / factPotIdeal));

            var var1 = ((decimal)(Math.Sqrt((1 - Math.Pow((double)factPotActual, 2)))) / factPotActual);


            return RedondeaValor(kvar);
        }

        private decimal RedondeaValor(decimal valor)
        {
            valor = Math.Truncate(valor * 1000000) / 1000000;
            valor = Math.Round(valor, 4);

            return valor;
        }
    }
}
