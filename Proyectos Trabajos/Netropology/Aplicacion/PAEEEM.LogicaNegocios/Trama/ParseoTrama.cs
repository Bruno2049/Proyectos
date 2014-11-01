using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.AccesoDatos.SolicitudCredito;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.Entidades.Trama;
using PAEEEM.LogicaNegocios.SolicitudCredito;


namespace PAEEEM.LogicaNegocios.Trama
{   
    public class ParseoTrama
    {
        #region Propiedades

        private CompParseo _objComplexTrama = new CompParseo();    
        private readonly TipoOperacionesTrama _tipoOperaciones = new TipoOperacionesTrama();
        private readonly InformacionParseo _trinfoGeneral = new InformacionParseo();
        private readonly List<TR_INFORMACION_PARSEO> _listInfoGeneral;
        private int _idResponseData;


        public CompParseo ComplexParseo
        {
            get { return _objComplexTrama; }           
        }
       
        public int idResponseData
        {
            get { return _idResponseData; }
        }

        #endregion

        
        public ParseoTrama(string trama)
        {
            _listInfoGeneral = new List<TR_INFORMACION_PARSEO>();
            _objComplexTrama.Trama = trama;           

            _listInfoGeneral = _trinfoGeneral.FitrarPorCondicion(c => c.ESTATUS);

            ExtraeInformacionGeneral(_listInfoGeneral);

            ExtraeConsumo(_listInfoGeneral);    
        
            ExtraeDetalleConsumo(_listInfoGeneral);

            _objComplexTrama.InfoConsumo = InfoDemandaConsumo(false, _objComplexTrama.HistorialDetconsumos);
            _objComplexTrama.InfoDemanda = InfoDemandaConsumo(true, _objComplexTrama.HistorialDetconsumos);
            EstablePeriodosValidos(ComplexParseo.HistorialDetconsumos);
            InsertaResponseData(_objComplexTrama);
        }

        
        //METODO PARA EXTRAER LA INFORMACION QUE PERTENECE A LA SECCION DE INFORMACION GENERAL
        private void ExtraeInformacionGeneral(List<TR_INFORMACION_PARSEO> listInfoGeneral)
        {
            var informacionGeneral = new CompInformacionGeneral();
            string trama = _objComplexTrama.Trama;
            var listConceptos = new List<CompConcepto>();


            foreach (var compConcepto in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 1))
            {//FACTURACION Y TIPO DE FACTURACION
                listConceptos.Add(new CompConcepto {
                    Id = compConcepto.ID_INFO,
                    PuntoInicial = compConcepto.INICIAL,
                    PuntoFinal = compConcepto.FINAL,
                    PuntoLongitud = compConcepto.LONGITUD,
                    Concepto = compConcepto.CONCEPTO,
                    Dato = trama.Substring(compConcepto.INICIAL - 1, compConcepto.LONGITUD).Trim(),
                    ValorCadena = _tipoOperaciones.GetFacturaValor(compConcepto.INICIAL, compConcepto.LONGITUD, trama)
                });
            }

            var concepto = new CompConcepto();
            var trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 2);

            if (trInformacion != null)
            {
                //CLAVE DEL ESTADO
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato = trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorCadena = _tipoOperaciones.GetClaveEstadoValor(trInformacion.INICIAL,trInformacion.LONGITUD, trama);
                listConceptos.Add(concepto);
            }

            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 3);

            if (trInformacion != null)
            {
                //CLAVE DEL IVA
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato = trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorEntero = _tipoOperaciones.GetClaveIvaValor(trInformacion.INICIAL,trInformacion.LONGITUD, trama);
                listConceptos.Add(concepto);
            }

            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 4);

            if (trInformacion != null)
            {     
                //CLAVE TARIFA
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato = trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorCadena = _tipoOperaciones.GetTarifaValor(trInformacion.INICIAL, trInformacion.LONGITUD, trama);
                listConceptos.Add(concepto);
            }

            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 5);

            if (trInformacion != null)
            {
                //ESTATUS USUARIO
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato = trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorCadena = _tipoOperaciones.GetEstatusUsuarioValor(trInformacion.INICIAL, trInformacion.LONGITUD, trama);
                listConceptos.Add(concepto);
            }

            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 6);

            if (trInformacion != null)
            {
                //RPU
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato =  trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorCadena = _tipoOperaciones.GetRpuValor(trInformacion.INICIAL, trInformacion.LONGITUD, trama);
                listConceptos.Add(concepto);
            }

            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 7);

            if (trInformacion != null)
            {
                //NO DE ADEUDOS
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato = trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorEntero = _tipoOperaciones.GetCadenaNumerico(trInformacion.INICIAL, trInformacion.LONGITUD, trama);
                listConceptos.Add(concepto);
            }

            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 8);

            if (trInformacion != null)
            {
                //kVArh
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato =  trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorEntero = _tipoOperaciones.GetCadenaNumerico(trInformacion.INICIAL, trInformacion.LONGITUD, trama);
                listConceptos.Add(concepto);
            }


            //CONCEPTOS DE CADENA A CADENA
            foreach (var conpto in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 18))
            {
                listConceptos.Add(new CompConcepto
                    {
                        Id = conpto.ID_INFO,
                        PuntoInicial = conpto.INICIAL,
                        PuntoFinal = conpto.FINAL,
                        PuntoLongitud = conpto.LONGITUD,
                        Concepto = conpto.CONCEPTO,
                        Dato = trama.Substring(conpto.INICIAL - 1, conpto.LONGITUD).Trim(),
                        ValorCadena = trama.Substring(conpto.INICIAL - 1, conpto.LONGITUD).Trim()
                    });
            }

            //CONCEPTOS DE CADENA A VALOR NUMERICO
            foreach (var conpto in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 19))
            {
                listConceptos.Add(new CompConcepto
                {
                    Id = conpto.ID_INFO,
                    PuntoInicial = conpto.INICIAL,
                    PuntoFinal = conpto.FINAL,
                    PuntoLongitud = conpto.LONGITUD,
                    Concepto = conpto.CONCEPTO,
                    Dato = trama.Substring(conpto.INICIAL - 1, conpto.LONGITUD).Trim(),
                    ValorEntero = _tipoOperaciones.GetCadenaNumerico(conpto.INICIAL, conpto.LONGITUD, trama)
                });
            }

            //CONCEPTOS DE CADENA ANIOMESDIA A VALOR FECHA DIA/MES/ANIO
            foreach (var conpto in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 20))
            {
                listConceptos.Add(new CompConcepto
                {
                    Id = conpto.ID_INFO,
                    PuntoInicial = conpto.INICIAL,
                    PuntoFinal = conpto.FINAL,
                    PuntoLongitud = conpto.LONGITUD,
                    Concepto = conpto.CONCEPTO,
                    Dato = trama.Substring(conpto.INICIAL - 1, conpto.LONGITUD).Trim(),
                    ValorFecha = _tipoOperaciones.GetFechaDiaMesAnioValor(conpto.INICIAL, conpto.LONGITUD, trama)
                });
            }

            //NOCUENTA 
            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 21);

            if (trInformacion != null)
            {
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato = trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorCadena = _tipoOperaciones.GetNoCuenta(trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim(),trama);
                listConceptos.Add(concepto);
            }
            


            //ORIGEN
            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 24);
            if (trInformacion != null)
            {
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato = trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorCadena = _tipoOperaciones.GetOrigen(trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim());
                listConceptos.Add(concepto);
            }


            //ESTATUS SICOM
            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 25);
            if (trInformacion != null)
            {
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato = trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorCadena = _tipoOperaciones.GetEstatusCmnSicom(trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim());
                listConceptos.Add(concepto);
            }

            
            //CLAVE DEL MUNICIPIO
            concepto = new CompConcepto();
            trInformacion = listInfoGeneral.FirstOrDefault(c => c.IDTIPO_OPERACION == 26);
            if (trInformacion != null)
            {
                concepto.Id = trInformacion.ID_INFO;
                concepto.PuntoInicial = trInformacion.INICIAL;
                concepto.PuntoFinal = trInformacion.FINAL;
                concepto.PuntoLongitud = trInformacion.LONGITUD;
                concepto.Concepto = trInformacion.CONCEPTO;
                concepto.Dato = trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim();
                concepto.ValorCadena = _tipoOperaciones.GetMunicipio(trama.Substring(trInformacion.INICIAL - 1, trInformacion.LONGITUD).Trim());
                listConceptos.Add(concepto);
            }            


            informacionGeneral.Conceptos = listConceptos;

            #region Operacion de consumos en informacion general
            //ID DE CONSUMOS EN SECCION DE INFORMACION GENERAL ID:9           
            var listConcepto = new List<CompConcepto>();
          
            foreach (var infoGeneral in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 9))
            {
                listConcepto.Add(new CompConcepto
                {
                    Id = infoGeneral.ID_INFO,
                    PuntoInicial = infoGeneral.INICIAL,
                    PuntoFinal = infoGeneral.FINAL,
                    PuntoLongitud = infoGeneral.LONGITUD,
                    Concepto = infoGeneral.CONCEPTO.Trim(),
                    Dato = _objComplexTrama.Trama.Substring(infoGeneral.INICIAL - 1, infoGeneral.LONGITUD).Trim(),
                    ValorEntero = _tipoOperaciones.GetCadenaNumerico(infoGeneral.INICIAL, infoGeneral.LONGITUD, _objComplexTrama.Trama)
                });
            }

            informacionGeneral.ConsumoP = listConcepto;
           

            #endregion

            #region Operacion de demandas en informacion general
            listConcepto = new List<CompConcepto>();

            foreach (var infoGeneral in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 10))
            {
                listConcepto.Add(new CompConcepto
                {
                    Id = infoGeneral.ID_INFO,
                    PuntoInicial = infoGeneral.INICIAL,
                    PuntoFinal = infoGeneral.FINAL,
                    PuntoLongitud = infoGeneral.LONGITUD,
                    Concepto = infoGeneral.CONCEPTO.Trim(),
                    Dato = _objComplexTrama.Trama.Substring(infoGeneral.INICIAL - 1, infoGeneral.LONGITUD).Trim(),
                    ValorEntero = _tipoOperaciones.GetCadenaNumerico(infoGeneral.INICIAL, infoGeneral.LONGITUD, _objComplexTrama.Trama)
                });
            }


            informacionGeneral.DemandaP = listConcepto;
                       
            #endregion
            

            _objComplexTrama.InformacionGeneral = informacionGeneral;
            listConcepto = null;
        }

        //METODO PARA EXTRAER LA INFORMACION QUE PERTENECE A LA SECCION DE CONSUMOS
        private void ExtraeConsumo(List<TR_INFORMACION_PARSEO> listInfoGeneral)
        {
            #region Operacion de Fechas en seccion de Consumo
            var listConceptos = new List<CompConcepto>();
            var consumo = new CompConsumo();

            foreach (var infoGeneral in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 11))
            {
                listConceptos.Add(new CompConcepto
                {
                    Id = infoGeneral.ID_INFO,
                    PuntoInicial = infoGeneral.INICIAL,
                    PuntoFinal = infoGeneral.FINAL,
                    PuntoLongitud = infoGeneral.LONGITUD,
                    Concepto = infoGeneral.CONCEPTO.Trim(),
                    Dato = _objComplexTrama.Trama.Substring(infoGeneral.INICIAL - 1, infoGeneral.LONGITUD).Trim(),
                    ValorFecha = _tipoOperaciones.GetFechaMesAnioValor(infoGeneral.INICIAL, infoGeneral.LONGITUD, _objComplexTrama.Trama)
                });
            }

            consumo.Fechas = listConceptos;
           
            #endregion

            #region Operacion de Consumos en seccion de Consumo
            listConceptos = new List<CompConcepto>();           

            foreach (var infoGeneral in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 15))
            {
                listConceptos.Add(new CompConcepto
                {
                    Id = infoGeneral.ID_INFO,
                    PuntoInicial = infoGeneral.INICIAL,
                    PuntoFinal = infoGeneral.FINAL,
                    PuntoLongitud = infoGeneral.LONGITUD,
                    Concepto = infoGeneral.CONCEPTO.Trim(),
                    Dato = _objComplexTrama.Trama.Substring(infoGeneral.INICIAL - 1, infoGeneral.LONGITUD).Trim(),
                    ValorEntero = _tipoOperaciones.GetCadenaNumerico(infoGeneral.INICIAL , infoGeneral.LONGITUD, _objComplexTrama.Trama)
                });
            }

            consumo.Consumos = listConceptos;           
            
            #endregion

            _objComplexTrama.Consumo = consumo;
            listConceptos = null;



            _objComplexTrama.HistorialConsumo = HistorialConsumos(_objComplexTrama.Consumo);
        }

        //METODO PARA EXTRAER LA INFORMACION QUE PERTENECE A LA SECCION DE DETALLE DE CONSUMOS
        private void ExtraeDetalleConsumo(List<TR_INFORMACION_PARSEO> listInfoGeneral)
        {
            #region Operacion de Periodo año en seccion de detalle de consumo
            var lisConceptos = new List<CompConcepto>();
            var detalleConsumos = new CompDetalleConsumo();

            foreach (var infoGeneral in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 13))
            {
                lisConceptos.Add(new CompConcepto
                {
                    Id = infoGeneral.ID_INFO,
                    PuntoInicial = infoGeneral.INICIAL,
                    PuntoFinal = infoGeneral.FINAL,
                    PuntoLongitud = infoGeneral.LONGITUD,
                    Concepto = infoGeneral.CONCEPTO.Trim(),
                    Dato = _objComplexTrama.Trama.Substring(infoGeneral.INICIAL - 1, infoGeneral.LONGITUD).Trim()
                });
            }

            detalleConsumos.PeriodoAnio = lisConceptos;
           
            #endregion

            #region Operacion de Periodo mes en seccion de detalle de consumo
            lisConceptos = new List<CompConcepto>();

            int i = 0;
            foreach (var infoGeneral in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 12))
            {
                lisConceptos.Add(new CompConcepto
                {
                    Id = infoGeneral.ID_INFO,
                    PuntoInicial = infoGeneral.INICIAL,
                    PuntoFinal = infoGeneral.FINAL,
                    PuntoLongitud = infoGeneral.LONGITUD,
                    Concepto = infoGeneral.CONCEPTO.Trim(),
                    Dato = _objComplexTrama.Trama.Substring(infoGeneral.INICIAL - 1, infoGeneral.LONGITUD).Trim(),
                    ValorFecha = _tipoOperaciones.GetPeriodoMesValor(infoGeneral.INICIAL, infoGeneral.LONGITUD,
                                                                   detalleConsumos.PeriodoAnio[i].Dato, _objComplexTrama.Trama)
                });
                i++;
            }

            detalleConsumos.PeriodoMes = lisConceptos;
            
            #endregion

            #region Operacion de Consumo en seccion de detalle de consumo
            lisConceptos = new List<CompConcepto>();
           

            foreach (var infoGeneral in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 16))
            {
                lisConceptos.Add(new CompConcepto
                {
                    Id = infoGeneral.ID_INFO,
                    PuntoInicial = infoGeneral.INICIAL,
                    PuntoFinal = infoGeneral.FINAL,
                    PuntoLongitud = infoGeneral.LONGITUD,
                    Concepto = infoGeneral.CONCEPTO.Trim(),
                    Dato = _objComplexTrama.Trama.Substring(infoGeneral.INICIAL - 1, infoGeneral.LONGITUD).Trim(),
                    ValorEntero = _tipoOperaciones.GetCadenaNumerico(infoGeneral.INICIAL, infoGeneral.LONGITUD, _objComplexTrama.Trama)
                });
            }

            detalleConsumos.Consumo = lisConceptos;
            

            #endregion

            #region Operacion de Demanda en seccion de detalle de consumo
            lisConceptos = new List<CompConcepto>();
            

            foreach (var infoGeneral in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 17))
            {
                lisConceptos.Add(new CompConcepto
                {
                    Id = infoGeneral.ID_INFO,
                    PuntoInicial = infoGeneral.INICIAL,
                    PuntoFinal = infoGeneral.FINAL,
                    PuntoLongitud = infoGeneral.LONGITUD,
                    Concepto = infoGeneral.CONCEPTO.Trim(),
                    Dato = _objComplexTrama.Trama.Substring(infoGeneral.INICIAL - 1, infoGeneral.LONGITUD).Trim(),
                    ValorEntero = _tipoOperaciones.GetCadenaNumerico(infoGeneral.INICIAL, infoGeneral.LONGITUD, _objComplexTrama.Trama)
                });
            }


            detalleConsumos.Demanda = lisConceptos;          

            #endregion

            #region Operacion de Factor de Potencia en seccion de detalle de consumo
            lisConceptos = new List<CompConcepto>();
            

            foreach (var infoGeneral in listInfoGeneral.Where(c => c.IDTIPO_OPERACION == 14))
            {
                lisConceptos.Add(new CompConcepto
                {
                    Id = infoGeneral.ID_INFO,
                    PuntoInicial = infoGeneral.INICIAL,
                    PuntoFinal = infoGeneral.FINAL,
                    PuntoLongitud = infoGeneral.LONGITUD,
                    Concepto = infoGeneral.CONCEPTO.Trim(),
                    Dato = _objComplexTrama.Trama.Substring(infoGeneral.INICIAL - 1, infoGeneral.LONGITUD).Trim(),
                    ValorDecimal = _tipoOperaciones.GetFactorPotenciaValor(infoGeneral.INICIAL, infoGeneral.LONGITUD, _objComplexTrama.Trama)
                });
            }

            detalleConsumos.FactorPotencia = lisConceptos;
            

            #endregion

            _objComplexTrama.DetalleConsumos = detalleConsumos;
            _objComplexTrama.HistorialDetconsumos = HistorialDetalleConsumo(_objComplexTrama.DetalleConsumos);

                        
            lisConceptos = null;
        }

        //METODO PARA GENERAR EL HISTORIAL DE CONSUMOS
        private List<CompHistorialConsumo> HistorialConsumos(CompConsumo objComplexConsumos)
        {
            var historialConsumo = new List<CompHistorialConsumo>();            

            for (int i = 0; i < objComplexConsumos.Consumos.Count ; i++)
            {
               historialConsumo.Add(new CompHistorialConsumo
                    {
                        IdHistorial = i +1,
                        Consumo = objComplexConsumos.Consumos[i].ValorEntero,
                        Fecha = objComplexConsumos.Fechas[i].ValorFecha
                    });
            }

            return historialConsumo;
        }

        //METODO PARA GENERAR EL HISTORIAL DE DETALLE DE CONSUMOS
        private List<CompHistorialDetconsumo> HistorialDetalleConsumo (CompDetalleConsumo objComplexDetConsumo)
        {
            var historialDetConsumo = new List<CompHistorialDetconsumo>();

            for (int i = 0; i < objComplexDetConsumo.Consumo.Count; i++)
            {
                historialDetConsumo.Add(new CompHistorialDetconsumo
                    {
                        IdHistorial = i+1,
                        Fecha = objComplexDetConsumo.PeriodoMes[i].ValorFecha,
                        Consumo = objComplexDetConsumo.Consumo[i].ValorEntero,
                        Demanda = objComplexDetConsumo.Demanda[i].ValorEntero,
                        FactorPotencia = objComplexDetConsumo.FactorPotencia[i].ValorDecimal
                    });
            }
           

            /* VALIDAR DUPLICADO DE FECHAS SI ES ASI REALIZAR LO SIGUIENTE 
             * OBTENER EL REGISTRO CON CONSUMO MAXIMO   
             */

            //OBTENCION DE LISTA DE OBJETOS QUE SE REPITEN EN FECHAS
            var listaFechas = historialDetConsumo.GroupBy(p => p.Fecha).Where(p => p.Count() > 1).Select(p => p.Key.Value).ToList();

            if (listaFechas != null)
            {
                //OBTENCION DE ID DE HISTORIAL DE CONSUMOS
                for (var i = 0; i < listaFechas.Count; i++)
                {
                    var consumoMax = historialDetConsumo.Where(p => p.Fecha == listaFechas[i].Date).Max(p => p.Consumo);
                    var detalleCons = historialDetConsumo.FirstOrDefault(p => p.Fecha == listaFechas[i].Date && p.Consumo == consumoMax);


                    historialDetConsumo.RemoveAll(p => p.Fecha == listaFechas[i].Date && p.IdHistorial != detalleCons.IdHistorial);
                }

                //RESETEO DE VALORES UNICAMENTE EN EL IDHISTORIAL
                var idHistorial = 1;
                foreach (var detalleConsumo in historialDetConsumo)
                {
                    detalleConsumo.IdHistorial = idHistorial;
                    idHistorial++;
                }
            }


            return historialDetConsumo;
        }                
       
        //METODO PARA REALIZAR INFORMACION DEL CONSUMO Y DE LA DEMANDA
        private CompInfoConsDeman InfoDemandaConsumo(bool Demanda, List<CompHistorialDetconsumo> historialDetconsumos)
        {
            var infoConsDeman = new CompInfoConsDeman();
            var detalle = new CompDetalleFechas();

            //OBTIENE LOS VALORES PARA EL DETALLE DE DEMANDA
            var detConsumoFechaMax = historialDetconsumos.Max(p => p.Fecha);
            DateTime? detConsumoFechaMin = null;
            var _tipoFacturacion = _objComplexTrama.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato;
            _tipoFacturacion = _tipoFacturacion != "1" ? "2" : _tipoFacturacion;

            try
            {
                if (_tipoFacturacion == "1")
                    detConsumoFechaMin = detConsumoFechaMax.Value.AddMonths(-11);
                else if (_tipoFacturacion == "2")
                    detConsumoFechaMin = detConsumoFechaMax.Value.AddMonths(-10);
            }
            catch
            { }

            var histDetConsumo = historialDetconsumos.FirstOrDefault(p => p.Fecha == detConsumoFechaMin);

            if (histDetConsumo != null)
            {
                if (histDetConsumo.Fecha != detConsumoFechaMin)
                {
                    return new CompInfoConsDeman();
                }
            }
            else
            {
                var historial = historialDetconsumos.FindAll(p => p.Consumo > 0);
                var fechaMin = historial.Min(p => p.Fecha);
                histDetConsumo = historialDetconsumos.FirstOrDefault(p => p.Fecha == fechaMin);
            }

            if (detConsumoFechaMax != null)
                detalle.FechaMax = detConsumoFechaMax;
            
            if (histDetConsumo != null)
                detalle.FechaMin = histDetConsumo.Fecha;

            var periodoPago = int.Parse(_tipoFacturacion);
            //PREGUNTA SI ES DEMANDA O CONSUMO
            if (Demanda)
            {
                var rDemandaTotal = Convert.ToDecimal(historialDetconsumos.Where(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax).Sum(p => p.Demanda));

                var rNoTotalDemanda = historialDetconsumos.Count(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax);

                if (periodoPago == 2)
                    rNoTotalDemanda = rNoTotalDemanda > 6 ? 6 : rNoTotalDemanda;

                var DemandaMax = 
                    (rNoTotalDemanda > 0 
                        ? Math.Round((rDemandaTotal / rNoTotalDemanda), 4) 
                        : 0);
                
                var DemandaMaxPerido = 
                    (periodoPago != 0 
                        ? DemandaMax / periodoPago 
                        : 0);

                infoConsDeman.DemandaMax = Math.Round((decimal)DemandaMaxPerido, 4);
                detalle.DemandaMax = infoConsDeman.DemandaMax;

                //var DemandaMax = historialDetconsumos.Where(p => p.Fecha <= detConsumoFechaMax && p.Fecha >= detalle.FechaMin).Max(p => p.Demanda);
                //var DemandaMaxPerido = DemandaMax/periodoPago;

                //infoConsDeman.DemandaMax = Math.Round((decimal)DemandaMaxPerido, 2);
                //detalle.DemandaMax = infoConsDeman.DemandaMax;
            }
            else
            {
                var rstConsTotal = historialDetconsumos.Where(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax).Sum(p => p.Consumo);

                var rstNoTotalConsumo = historialDetconsumos.Count(p => p.Fecha >= detalle.FechaMin && p.Fecha <= detalle.FechaMax);

                if (periodoPago == 2)
                    rstNoTotalConsumo = rstNoTotalConsumo > 6 ? 6 : rstNoTotalConsumo;

                infoConsDeman.Suma = rstConsTotal;

                detalle.Promedio =
                    (rstNoTotalConsumo > 0 && periodoPago > 0 
                        ? Math.Round((rstConsTotal/rstNoTotalConsumo)/periodoPago, 4)
                        : 0); //REDONDEO A DOS DECIMALES
            }

            infoConsDeman.Detalle = detalle;
            return infoConsDeman;
        }

        //METODO PARA ESTABLECER VALIDACION DE PERIODOS DENTRO DE LAS FECHAS INICIALES Y MAXIMAS
        public void EstablePeriodosValidos(List<CompHistorialDetconsumo> historialDetconsumos)
        {            
            //validacion de fechas dentro del rango para establecer un id al historial detalle de consumo
            foreach (var detalleConsumo in historialDetconsumos)
            {
                if (detalleConsumo.Fecha < _objComplexTrama.InfoConsumo.Detalle.FechaMin)
                    detalleConsumo.Id = 0;
                else
                    detalleConsumo.Id = detalleConsumo.IdHistorial;
            }


            _objComplexTrama.Periodos =_objComplexTrama.HistorialDetconsumos.Count(p => p.Id > 0);
        }

        //METODO PARA INSERTAR EN LA TABLA RESPONSEDATA
        private void InsertaResponseData(CompParseo parseo)
        {
            var complexResponseData = new CompResponseData();
            var responseData = new AccesoDatos.SolicitudCredito.ResponseData();

            #region Informacion General
            complexResponseData.Cn = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 160).Dato;
            complexResponseData.ServiceCode = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 6).Dato;
            complexResponseData.Rate = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 4).Dato;
            complexResponseData.Periodof = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 1).Dato;
            complexResponseData.Name = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 146).Dato;
            complexResponseData.Address = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 147).Dato;
            complexResponseData.Dircomp1 = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 148).Dato;
            complexResponseData.Dircomp2 = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 149).Dato;
            complexResponseData.Colonia = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 150).Dato;
            complexResponseData.Poblacion = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 166).Dato;
            complexResponseData.StateName = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 152).Dato;
            complexResponseData.Threads = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 161).Dato;
            complexResponseData.Tel = ""; //no se emplea
            complexResponseData.Email = "";//no se emplea
            complexResponseData.Meter = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 154).Dato;
            complexResponseData.DueDate = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 156).Dato;
            complexResponseData.PeriodStartDate = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 158).Dato;
            complexResponseData.PeriodEndDate = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 159).Dato;
            complexResponseData.HighConsumption = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 88).Dato;
            complexResponseData.Vat = "";//no se emplea
            complexResponseData.Zone = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 153).Dato;
            complexResponseData.Biling = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 162).Dato;
            complexResponseData.RgnCfe = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 145).Dato;
            complexResponseData.MinConsumptionDate = parseo.Consumo.Fechas.FirstOrDefault(p => p.Id == 17).Dato;
            complexResponseData.Anexo1 = "";// no se emplea
            complexResponseData.HighConsumptionSummer = ""; //no se emplea
            complexResponseData.HighConsumptionWinter = ""; //no se emplea
            complexResponseData.AvgConsumptionSummer = ""; //no se emplea
            complexResponseData.AvgConsumptionWinter = ""; // no se emplea
            complexResponseData.HighAnnualConsumption = ""; // no se emplea
            complexResponseData.HighAvgConsumption = parseo.InfoConsumo.Detalle.Promedio == null ? null : parseo.InfoConsumo.Detalle.Promedio.ToString(CultureInfo.InvariantCulture);
            complexResponseData.TotalPeriods = parseo.Periodos.ToString(CultureInfo.InvariantCulture);
            complexResponseData.WinterConsumptionHistory = ""; //no se emplea
            complexResponseData.SummerConsumptionHistory = ""; //no se emplea
            complexResponseData.PaymentHistory = ""; //no se emplea
            complexResponseData.UserStatus = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 5).Dato;
            complexResponseData.Source = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 163).Dato;
            complexResponseData.ComStatus = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 164).Dato;
            complexResponseData.Dac = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 155).Dato;
            complexResponseData.StateCode = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 2).Dato;
            complexResponseData.CityCode = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 165).Dato;
            complexResponseData.ZoneType = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 151).Dato;
            complexResponseData.PreviousBalance = ""; //no se emplea
            complexResponseData.Payment = ""; //no se emplea
            complexResponseData.PreviousDebit = "";// no se emplea
            complexResponseData.FullPreviousDebit = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 156).Dato; // no se emplea
            complexResponseData.CurrentBillingStatus = "";//no se emplea
            complexResponseData.CurrentBillingDueDate = "";//no se emplea
            complexResponseData.CurrentDebit = "";//no se emplea
            #endregion

            #region PERIODO MES CON VALOR DE FECHA

            complexResponseData.Fecha01 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 65).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha02 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 70).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha03 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 75).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha04 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 80).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha05 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 85).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha06 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 90).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha07 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 95).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha08 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 100).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha09 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 105).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha10 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 110).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha11 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 115).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha12 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 120).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha13 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 125).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha14 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 130).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha15 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 135).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha16 = parseo.DetalleConsumos.PeriodoMes.FirstOrDefault(p => p.Id == 140).ValorFecha.Value.ToString("dd/MM/yyyy");
            complexResponseData.Fecha17 = "";
            complexResponseData.Fecha18 = "";
            complexResponseData.Fecha19 = "";
            complexResponseData.Fecha20 = "";
            complexResponseData.Fecha21 = "";
            complexResponseData.Fecha22 = "";
            complexResponseData.Fecha23 = "";
            complexResponseData.Fecha24 = "";
            #endregion

            #region INFORMACION DEL CONSUMO
            complexResponseData.Consumo01 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 67).Dato;
            complexResponseData.Consumo02 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 72).Dato;
            complexResponseData.Consumo03 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 77).Dato;
            complexResponseData.Consumo04 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 82).Dato;
            complexResponseData.Consumo05 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 87).Dato;
            complexResponseData.Consumo06 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 92).Dato;
            complexResponseData.Consumo07 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 97).Dato;
            complexResponseData.Consumo08 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 102).Dato;
            complexResponseData.Consumo09 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 107).Dato;
            complexResponseData.Consumo10 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 112).Dato;
            complexResponseData.Consumo11 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 117).Dato;
            complexResponseData.Consumo12 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 122).Dato;
            complexResponseData.Consumo13 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 127).Dato;
            complexResponseData.Consumo14 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 132).Dato;
            complexResponseData.Consumo15 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 137).Dato;
            complexResponseData.Consumo16 = parseo.DetalleConsumos.Consumo.FirstOrDefault(p => p.Id == 142).Dato;
            complexResponseData.Consumo17 = "";
            complexResponseData.Consumo18 = "";
            complexResponseData.Consumo19 = "";
            complexResponseData.Consumo20 = "";
            complexResponseData.Consumo21 = "";
            complexResponseData.Consumo22 = "";
            complexResponseData.Consumo23 = "";
            complexResponseData.Consumo24 = "";
            #endregion

            #region Historial de Consumo
            complexResponseData.DateDue01 = "";
            complexResponseData.DatePayment01 = "";


            complexResponseData.DateDue02 = "";
            complexResponseData.DatePayment02 = "";

            complexResponseData.DateDue03 = "";
            complexResponseData.DatePayment03 = "";

            complexResponseData.DateDue04 = "";
            complexResponseData.DatePayment04 = "";

            complexResponseData.DateDue05 = "";
            complexResponseData.DatePayment05 = "";

            complexResponseData.DateDue06 = "";
            complexResponseData.DatePayment06 = "";

            complexResponseData.DateDue07 = "";
            complexResponseData.DatePayment07 = "";

            complexResponseData.DateDue08 = "";
            complexResponseData.DatePayment08 = "";

            complexResponseData.DateDue09 = "";
            complexResponseData.DatePayment09 = "";

            complexResponseData.DateDue10 = "";
            complexResponseData.DatePayment10 = "";

            complexResponseData.DateDue11 = "";
            complexResponseData.DatePayment11 = "";

            complexResponseData.DateDue12 = "";
            complexResponseData.DatePayment12 = "";

            complexResponseData.DateDue13 = "";
            complexResponseData.DatePayment13 = "";

            complexResponseData.DateDue14 = "";
            complexResponseData.DatePayment14 = "";

            complexResponseData.DateDue15 = "";
            complexResponseData.DatePayment15 = "";

            complexResponseData.DateDue16 = "";
            complexResponseData.DatePayment16 = "";

            complexResponseData.DateDue17 = "";
            complexResponseData.DatePayment17 = "";

            complexResponseData.DateDue18 = "";
            complexResponseData.DatePayment18 = "";

            complexResponseData.DateDue19 = "";
            complexResponseData.DatePayment19 = "";

            complexResponseData.DateDue20 = "";
            complexResponseData.DatePayment20 = "";

            complexResponseData.DateDue21 = "";
            complexResponseData.DatePayment21 = "";

            complexResponseData.DateDue22 = "";
            complexResponseData.DatePayment22 = "";

            complexResponseData.DateDue23 = "";
            complexResponseData.DatePayment23 = "";

            complexResponseData.DateDue24 = "";
            complexResponseData.DatePayment24 = "";
            #endregion

            complexResponseData.Region = "";
            complexResponseData.Zona = "";
            complexResponseData.Estado = "";

            #region INFORMACION DE LA DEMANDA
            complexResponseData.Demandar01 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 68).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar02 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 73).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar03 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 78).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar04 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 83).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar05 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 88).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar06 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 93).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar07 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 98).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar08 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 103).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar09 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 108).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar10 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 113).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar11 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 118).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar12 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 123).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar13 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 128).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar14 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 133).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar15 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 138).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.Demandar16 = parseo.DetalleConsumos.Demanda.FirstOrDefault(p => p.Id == 143).Dato.ToString(CultureInfo.InvariantCulture);
            complexResponseData.DemandarMax = parseo.InfoDemanda.Detalle.DemandaMax.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FechaAlta = null;
            complexResponseData.FechaConsulta = DateTime.Now;
            complexResponseData.ClaveIva = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 3).Dato;
            complexResponseData.DebitNumber = parseo.InformacionGeneral.Conceptos.FirstOrDefault(p => p.Id == 7).Dato;
            #endregion

            #region INFORMACION DEL FACTOR POTENCIAL
            complexResponseData.FactorPotencia01 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 69).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia02 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 74).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia03 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 79).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia04 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 84).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia05 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 89).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia06 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 94).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia07 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 99).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia08 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 104).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia09 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 109).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia10 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 114).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia11 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 119).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia12 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 124).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia13 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 129).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia14 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 134).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia15 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 139).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            complexResponseData.FactorPotencia16 = parseo.DetalleConsumos.FactorPotencia.FirstOrDefault(p => p.Id == 144).ValorDecimal.ToString(CultureInfo.InvariantCulture);
            #endregion

            complexResponseData.Trama = _objComplexTrama.Trama;
            var mensaje = responseData.insertaResponseData(complexResponseData);

            var idRd = 0;
            bool esnumero = int.TryParse(mensaje, out idRd);

            if (esnumero)
                _idResponseData = idRd;
        }
    }
}
