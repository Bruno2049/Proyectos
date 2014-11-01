using System;
using System.Collections.Generic;
using System.Linq;
using PAEEEM.AccesoDatos.AltaBajaEquipos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Entidades.Alta_Equipos;
using PAEEEM.Entidades.Alta_Solicitud;
using PAEEEM.LogicaNegocios.AltaBajaEquipos;

namespace PAEEEM.LogicaNegocios.Tarifas
{
    public class CalculosAhorroTecnologia
    {
        #region Atributos
        private readonly decimal _fmes = 0.0M;

        #endregion

        public CalculosAhorroTecnologia()
        {
            _fmes = Convert.ToDecimal(new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 12).VALOR);

        }

        #region Refrigeracion Comercial

        public decimal KwhAhorroRc(RefrigeracionComercial refrigeracion)
        {
            var kwhAhorro = 0.0M;
            refrigeracion.FMes = _fmes;

            if (refrigeracion != null)
            {
                kwhAhorro = (refrigeracion.FDeg*refrigeracion.FBio) * 
                            ((refrigeracion.NoEb * refrigeracion.AEb * refrigeracion.CapEb * refrigeracion.FMes)
                                -(refrigeracion.NoEa * refrigeracion.AEa * refrigeracion.CapEa * refrigeracion.FMes));                
            }            
          

            return kwhAhorro;
        }


        public decimal KwAhorroRc(RefrigeracionComercial refrigeracion)
        {
            var kwAhorro = 0.0M;

            if (refrigeracion != null)
            {
                kwAhorro = (refrigeracion.FDeg*refrigeracion.FBio)*
                           ((refrigeracion.NoEb*(refrigeracion.AEb/refrigeracion.Horas)*refrigeracion.CapEb)
                            - (refrigeracion.NoEa*(refrigeracion.AEa/refrigeracion.Horas)*refrigeracion.CapEa));

            }            


            return kwAhorro;
        }


        #endregion

        #region Aire Acondicionado

        public decimal CalculoAhorroKwAA(AireAcondicionado aireAcondicionado)
        {
            var kwAhorro = 0.0M;

            if (aireAcondicionado != null)
            {               
                kwAhorro = (aireAcondicionado.FDegAA*aireAcondicionado.FBio*aireAcondicionado.FCAA)*
                           ((aireAcondicionado.NoEb*aireAcondicionado.kWEB) -
                            (aireAcondicionado.NoEa*aireAcondicionado.kWEA));
            }

            return kwAhorro;
        }

        public decimal CalculoAhorroKwHAA(AireAcondicionado aireAcondicionado)
        {
            var kwhAhorro = 0.0M;

            if (aireAcondicionado != null)
            {
                aireAcondicionado.FmesAA = _fmes;

                kwhAhorro = (aireAcondicionado.FDegAA*aireAcondicionado.FBio)*
                            ((aireAcondicionado.NoEb*aireAcondicionado.kWhEB) -
                             (aireAcondicionado.NoEa*aireAcondicionado.kWEA*aireAcondicionado.OdiaAA*
                              aireAcondicionado.FmesAA));
            }

            return kwhAhorro;
        }

        #endregion

        #region Iluminación (Lineal, inducción y LED)

        public decimal CalculoAhorroKwIL(IluminacionLID iluminacion)
        {
            var kwAhorro = 0.0M;

            if (iluminacion != null)
            {
                var kwEquiposBaja = RedondeaValor(iluminacion.NoEb*iluminacion.PEB*(1.0M/1000.0M));
                var kwEquiposAlta = RedondeaValor(iluminacion.NoEa*iluminacion.PEA*(1.0M/1000.0M));

                kwAhorro = (iluminacion.FDegIlu*iluminacion.FBio*iluminacion.FCIlu)*(kwEquiposBaja - kwEquiposAlta);
                //((iluminacion.NoEb*iluminacion.PEB*(1.0M/1000.0M)) -
                // (iluminacion.NoEa*iluminacion.PEA*(1.0M/1000.0M)));
            }

            return RedondeaValor(kwAhorro);
        }

        public decimal CalculoAhorroKwhIL(IluminacionLID iluminacion)
        {
            var kwhAhorro = 0.0M;

            if (iluminacion != null)
            {
                //iluminacion.FmesIlu = _fmes;

                var khHEquiposBaja = (iluminacion.NoEb * iluminacion.PEB * (iluminacion.OanioIlu / 1000.0M)) / iluminacion.FmesIlu;
                var kwHEquiposAlta = (iluminacion.NoEa*iluminacion.PEA*(iluminacion.OanioIlu/1000.0M))/
                                     iluminacion.FmesIlu;



                kwhAhorro = (iluminacion.FDegIlu*iluminacion.FBio)*(khHEquiposBaja - kwHEquiposAlta);
                //(((iluminacion.NoEb*iluminacion.PEB*(iluminacion.OanioIlu/1000.0M))/iluminacion.FmesIlu) -
                // ((iluminacion.NoEa*iluminacion.PEA*(iluminacion.OanioIlu/1000.0M))/iluminacion.FmesIlu));
            }

            return kwhAhorro;
        }

        #endregion

        #region Motores Eléctricos (Alg_Ahorros_ME)

        public decimal CalculaAhorroKwMotores(MotoresElectricos motoresElectricos)
        {
            var kwAhorro = 0.0M;

            if (motoresElectricos != null)
            {
                
                kwAhorro = motoresElectricos.FDegME*motoresElectricos.FBio*
                           ((motoresElectricos.HPnominal*motoresElectricos.FcargaME*motoresElectricos.NoEb*
                             (100.0M/motoresElectricos.EficienciaEB)) -
                            (motoresElectricos.HPnominal*motoresElectricos.FcargaME*motoresElectricos.NoEa*
                             (100.0M/motoresElectricos.EficienciaEA)));

            }

            return kwAhorro;
        }

        public decimal CalculaAhorroKwhMotores(MotoresElectricos motoresElectricos)
        {
            var kwhAhorro = 0.0M;

            if (motoresElectricos != null)
            {
                motoresElectricos.FmesME = _fmes;

                kwhAhorro = motoresElectricos.FDegME*motoresElectricos.FBio*
                            ((motoresElectricos.kwAhorro*motoresElectricos.OanioME)/motoresElectricos.FmesME);
            }

            return kwhAhorro;
        }

        #endregion


        protected decimal RedondeaValor(decimal valor)
        {
            valor = Math.Truncate(valor * 1000000) / 1000000;
            valor = Math.Round(valor, 4);

            return valor;
        }

        public EquipoAltaEficiencia CalculaAhorroTecnologia(EquipoAltaEficiencia equipoAlta, EquipoBajaEficiencia EquipoBajaSelecionado, List<EquipoBajaEficiencia> LstBajaEficiencia, List<EquipoAltaEficiencia> LstAltaEficiencia, DatosPyme Pyme, decimal FBio, decimal Fmes)
        {
            var horasOperacion = new OpEquiposAbEficiencia().ObtenHorasOPeracion(Pyme.CveSectorEconomico,
                                                                                      Pyme.CveEstado,
                                                                                      Pyme.CveMunicipioNafin);
            var FDeg = 0.0M;

            try
            {
                FDeg = new RegionesBioclimaticas().ObtenFactorDegradacion(
                me =>
                me.Cve_Deleg_Municipio == Pyme.CveDelegMunicipio && me.Cve_Tecnologia == equipoAlta.Cve_Tecnologia)
                                       .FACTOR_DEGRADACION1;
            }
            catch
            {
                FDeg = 0.0M;
            }

            #region Refrigeracion Comercial

            if (EquipoBajaSelecionado.Dx_Tecnologia.Contains("REFRIGERACION"))
            {
                var ahorrosRefriferacion = new RefrigeracionComercial();
                var valoresProducto =
                    new OpEquiposAbEficiencia().ObtenValoresProducto(equipoAlta.FtTipoProducto);

                if (valoresProducto != null)
                {
                    ahorrosRefriferacion.V1 = RedondeaValor(valoresProducto.VARIABLE_1);
                    ahorrosRefriferacion.V2 = RedondeaValor(valoresProducto.VARIABLE_2);
                }

                ahorrosRefriferacion.FDeg = FDeg;
                ahorrosRefriferacion.FBio = FBio;
                ahorrosRefriferacion.Horas = horasOperacion.RC_D;

                ahorrosRefriferacion.NoEb =
                    LstBajaEficiencia.FindAll(me => me.Cve_Grupo == equipoAlta.Cve_Grupo)
                                     .Sum(me => me.Cantidad);

                var SumaAhorroKwhMesBaja = 0.0M;
                var SumaAhorroKwHorasBaja = 0.0M;

                foreach (
                    var equipoBajaEficiencia in
                        LstBajaEficiencia.FindAll(me => me.Cve_Grupo == equipoAlta.Cve_Grupo))
                {
                    var noEquipos = Convert.ToDecimal(equipoBajaEficiencia.Cantidad);
                    var capacidadEB = decimal.Parse(equipoBajaEficiencia.Dx_Consumo);
                    var AequipoBaja =
                        (decimal)new OpEquiposAbEficiencia().ObtenNomEse22(equipoBajaEficiencia.Ft_Tipo_Producto,
                                                                            capacidadEB).NOM_022_ESE_2000;
                    AequipoBaja = RedondeaValor(AequipoBaja); 

                    var ahorroKwhMes = Convert.ToDecimal(noEquipos * capacidadEB * AequipoBaja * Fmes);
                    ahorroKwhMes = RedondeaValor(ahorroKwhMes);
                    SumaAhorroKwhMesBaja = Math.Round(SumaAhorroKwhMesBaja + ahorroKwhMes, 4);

                    var kwLtsHoras = AequipoBaja / ahorrosRefriferacion.Horas;
                    kwLtsHoras = RedondeaValor(kwLtsHoras);
                    var ahorroKwHoras = Convert.ToDecimal(kwLtsHoras * (capacidadEB * noEquipos));
                    ahorroKwHoras = Math.Round(ahorroKwHoras, 4);
                    SumaAhorroKwHorasBaja = Math.Round(SumaAhorroKwHorasBaja + ahorroKwHoras, 4);

                }

                var SumaAhorroKwhMesAlta = 0.0M;
                var SumaAhorroKwHorasAlta = 0.0M;
                var AEa = ahorrosRefriferacion.V1 *
                          (decimal)Math.Pow(Convert.ToDouble(equipoAlta.No_Capacidad),
                                             -Convert.ToDouble(ahorrosRefriferacion.V2));
                AEa = RedondeaValor(AEa);

                SumaAhorroKwhMesAlta = Convert.ToDecimal(equipoAlta.Cantidad * AEa * equipoAlta.No_Capacidad * Fmes);
                SumaAhorroKwhMesAlta = RedondeaValor(SumaAhorroKwhMesAlta);


                var kwLtsHorasAlta = AEa / ahorrosRefriferacion.Horas;
                kwLtsHorasAlta = RedondeaValor(kwLtsHorasAlta);

                SumaAhorroKwHorasAlta =
                    Convert.ToDecimal(equipoAlta.Cantidad * kwLtsHorasAlta * equipoAlta.No_Capacidad);
                SumaAhorroKwHorasAlta = RedondeaValor(SumaAhorroKwHorasAlta);


                var ahorroKwH = (FDeg * FBio) * (SumaAhorroKwhMesBaja - SumaAhorroKwhMesAlta);
                var ahorrokw = (FDeg * FBio) * (SumaAhorroKwHorasBaja - SumaAhorroKwHorasAlta);

                equipoAlta.KwAhorro = RedondeaValor(ahorrokw);
                equipoAlta.KwhAhorro = RedondeaValor(ahorroKwH);
            }

            #endregion

            #region Aire Acondicionado

            if (EquipoBajaSelecionado.Dx_Tecnologia.Contains("AIRE"))
            {

                var FCAA =
                    decimal.Parse(
                        new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 1 && p.IDSECCION == 15)
                                                .VALOR);

                var OdiaAA = horasOperacion.AA_D;
                    //new OpEquiposAbEficiencia().ObtenHorasOPeracion(Pyme.CveSectorEconomico, Pyme.CveEstado,
                    //                                                Pyme.CveMunicipioNafin).AA_D;

                var ahorroKwEquiposBaja = 0.0M;
                var ahorroKwhEquiposBaja = 0.0M;

                foreach (
                    var equipoBajaEficiencia in
                        LstBajaEficiencia.FindAll(me => me.Cve_Grupo == EquipoBajaSelecionado.Cve_Grupo))
                {
                    var ahorrosAA =
                        new OpEquiposAbEficiencia().ObtenAhorrosAireAcondicionado(
                            equipoBajaEficiencia.Cve_Tecnologia,
                            decimal.Parse(
                                equipoBajaEficiencia.Dx_Consumo));

                    var ahorrokW = Convert.ToDecimal(equipoBajaEficiencia.Cantidad * ahorrosAA.KW_NOM_073_94);
                    var ahorrokWh = Convert.ToDecimal(equipoBajaEficiencia.Cantidad * ahorrosAA.KWH_MES_NOM_073_94);

                    ahorrokW = Math.Round(ahorrokW, 4);
                    ahorrokWh = Math.Round(ahorrokWh, 4);

                    ahorroKwEquiposBaja = ahorroKwEquiposBaja + ahorrokW;
                    ahorroKwhEquiposBaja = ahorroKwhEquiposBaja + ahorrokWh;
                }

                var ahorroKwEquiposAlta = 0.0M;
                var ahorroKwhEquipoAlta = 0.0M;

                foreach (
                    var equipoAltaEficiencia in
                        LstAltaEficiencia.FindAll(me => me.Cve_Grupo == equipoAlta.Cve_Grupo))
                {
                    var ahorrosAA =
                        new OpEquiposAbEficiencia().ObtenAhorrosAireAcondicionado(
                            EquipoBajaSelecionado.Cve_Tecnologia,
                            equipoAltaEficiencia.No_Capacidad);
                    var ahorrokWEA = Convert.ToDecimal(equipoAltaEficiencia.Cantidad * ahorrosAA.KW_SELLO_FIDE);

                    var ahorroKwhEA =
                        Convert.ToDecimal(equipoAltaEficiencia.Cantidad * ahorrosAA.KW_SELLO_FIDE * OdiaAA * Fmes);

                    ahorrokWEA = Math.Round(ahorrokWEA, 4);
                    ahorroKwhEA = Math.Round(ahorroKwhEA, 4);

                    ahorroKwEquiposAlta = ahorroKwEquiposAlta + ahorrokWEA;
                    ahorroKwhEquipoAlta = ahorroKwhEquipoAlta + ahorroKwhEA;
                }

                var kwAhorroAA = (FDeg * FBio * FCAA) * (ahorroKwEquiposBaja - ahorroKwEquiposAlta);
                var kwHAhorroAA = (FDeg * FBio) * (ahorroKwhEquiposBaja - ahorroKwhEquipoAlta);

                kwAhorroAA = Math.Round(kwAhorroAA, 4);
                kwHAhorroAA = Math.Round(kwHAhorroAA, 4);

                equipoAlta.KwAhorro = kwAhorroAA;
                equipoAlta.KwhAhorro = kwHAhorroAA;
            }

            #endregion

            #region Iluminacion

            if (EquipoBajaSelecionado.Dx_Tecnologia.Contains("ILUMINACION"))
            {
                var ahorrosIluminacion = new IluminacionLID();

                ahorrosIluminacion.FBio = FBio;
                ahorrosIluminacion.FDegIlu = FDeg;
                ahorrosIluminacion.FCIlu = decimal.Parse(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 2 && p.IDSECCION == 16).VALOR);
                ahorrosIluminacion.NoEb = EquipoBajaSelecionado.Cantidad;
                ahorrosIluminacion.NoEa = equipoAlta.Cantidad;
                ahorrosIluminacion.PEB =
                    (decimal)new OpEquiposAbEficiencia().ObtenSistemaEb(EquipoBajaSelecionado.Cve_Consumo).Potencia;
                ahorrosIluminacion.PEA = new OpEquiposAbEficiencia().ObtenPotenciaEaIl(equipoAlta.Cve_Modelo);
                ahorrosIluminacion.PEB = RedondeaValor(ahorrosIluminacion.PEB);
                ahorrosIluminacion.PEA = RedondeaValor(ahorrosIluminacion.PEA);

                if (EquipoBajaSelecionado.Cve_Tecnologia == 3)
                    ahorrosIluminacion.OanioIlu = horasOperacion.IL_A;

                if (EquipoBajaSelecionado.Cve_Tecnologia == 6)
                    ahorrosIluminacion.OanioIlu = horasOperacion.ILD_A;

                if (EquipoBajaSelecionado.Cve_Tecnologia == 8)
                    ahorrosIluminacion.OanioIlu = horasOperacion.II_A;
                    
                ahorrosIluminacion.FmesIlu = Convert.ToDecimal(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 2 && p.IDSECCION == 12).VALOR);

                var kwAhorroIL = new CalculosAhorroTecnologia().CalculoAhorroKwIL(ahorrosIluminacion);
                var kwHAhorroIL = new CalculosAhorroTecnologia().CalculoAhorroKwhIL(ahorrosIluminacion);

                kwAhorroIL = RedondeaValor(kwAhorroIL);
                kwHAhorroIL = RedondeaValor(kwHAhorroIL);

                equipoAlta.KwAhorro = kwAhorroIL;
                equipoAlta.KwhAhorro = kwHAhorroIL;
            }

            #endregion

            #region Motores Electricos

            if (EquipoBajaSelecionado.Dx_Tecnologia.Contains("MOTORES"))
            {

                var sumkwAhorroEquipoBaja = 0.0M;
                
                var FcargaME = decimal.Parse(
                    new ParametrosGlobales().ObtienePorCondicion(p => p.IDPARAMETRO == 3 && p.IDSECCION == 16).VALOR);

                foreach (var equipoBajaEficiencia in LstBajaEficiencia.FindAll(me => me.Cve_Grupo == equipoAlta.Cve_Grupo))
                {
                    var eficienciaEquipoBaja =
                        new OpEquiposAbEficiencia().ObtenPotenciaNominalEa(
                            decimal.Parse(equipoBajaEficiencia.Dx_Consumo), equipoBajaEficiencia.Cve_Tecnologia)
                                                   .EF_PROM_MOT_ESTDAR;

                    var potenciaMotores =
                        new OpEquiposAbEficiencia().ObtenPotenciaNominalEa(
                            Convert.ToDecimal(equipoBajaEficiencia.Dx_Consumo), equipoBajaEficiencia.Cve_Tecnologia);

                    var hpNominal = potenciaMotores.POTENCIA_NOMINAL_KW;

                    var kwAhorroEquipoBaja = Convert.ToDecimal(hpNominal * FcargaME * equipoBajaEficiencia.Cantidad * (1.0M / eficienciaEquipoBaja));
                    kwAhorroEquipoBaja = RedondeaValor(kwAhorroEquipoBaja);
                    sumkwAhorroEquipoBaja = sumkwAhorroEquipoBaja + kwAhorroEquipoBaja;
                }

                var sumkwAhorroEquipoAlta = 0.0M;

                foreach (
                    var equipoAltaEficiencia in
                        LstAltaEficiencia.FindAll(me => me.Cve_Grupo == equipoAlta.Cve_Grupo))
                {
                    var potenciaMotores =
                        new OpEquiposAbEficiencia().ObtenPotenciaNominalEa(equipoAltaEficiencia.No_Capacidad, equipoAltaEficiencia.Cve_Tecnologia);
                    
                    var eficienciaEquipoAlta = (decimal)potenciaMotores.EF_PROM_MOT_ALT_SF;
                    var hpNominal = (decimal)potenciaMotores.POTENCIA_NOMINAL_KW;

                    var kwAhorroEquipoAlta =
                        Convert.ToDecimal(hpNominal*FcargaME*equipoAltaEficiencia.Cantidad*(1.0M/eficienciaEquipoAlta));
                    kwAhorroEquipoAlta = RedondeaValor(kwAhorroEquipoAlta);
                    sumkwAhorroEquipoAlta = sumkwAhorroEquipoAlta + kwAhorroEquipoAlta;
                }

                var OanioME = horasOperacion.ME_A;
                var OmesMe = horasOperacion.ME_M;
                    //new OpEquiposAbEficiencia().ObtenHorasOPeracion(Pyme.CveSectorEconomico, Pyme.CveEstado,
                    //                                                Pyme.CveMunicipioNafin).ME_A;

                var kwAhorroME = FDeg * FBio * (sumkwAhorroEquipoBaja - sumkwAhorroEquipoAlta);
                kwAhorroME = RedondeaValor(kwAhorroME);
                var kwHAhorroME = FDeg * FBio * ((kwAhorroME * OanioME) / OmesMe);
                kwHAhorroME = RedondeaValor(kwHAhorroME);

                equipoAlta.KwAhorro = kwAhorroME;
                equipoAlta.KwhAhorro = kwHAhorroME;
            }

            #endregion

            return equipoAlta;
        }
    }
}
