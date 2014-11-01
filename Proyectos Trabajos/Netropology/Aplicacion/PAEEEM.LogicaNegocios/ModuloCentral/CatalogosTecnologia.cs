using System;
using System.Collections.Generic;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class CatalogosTecnologia
    {
        private static readonly CatalogosTecnologia _classInstance = new CatalogosTecnologia();
        public static CatalogosTecnologia ClassInstance { get { return _classInstance; } }

        public CatalogosTecnologia()
        { }

        #region Catalogos Combos
        public List<CAT_TIPO_MOVIMIENTO> ObtenCatTipoMovimiento()
        {
            var catTipoMovimiento = AccesoDatos.Catalogos.TIPO_MOVIMIENTO.ClassInstance.ObtieneTodos();

            return catTipoMovimiento;
        }

        public List<CAT_EQUIPOS_BAJA_ALTA> ObtenCatEquiposBajaAlta()
        {
            var catEquiposBajaAlta = AccesoDatos.Catalogos.EQUIPOS_BAJA_ALTA.ClassInstance.ObtieneTodos();

            return catEquiposBajaAlta;
        }

        public List<CAT_FACTOR_SUSTITUCION> ObtenCatFactorSustitucion()
        {
            var catFactorSustitucion = AccesoDatos.Catalogos.FACTOR_SUSTITUCION.ClassInstance.ObtieneTodos();

            return catFactorSustitucion;
        }

        public List<CAT_TIPO_TECNOLOGIA> ObtenCatTipoTecnologia()
        {
            var catTipoTecnologia = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtenTiposTeconogia();

            return catTipoTecnologia;
        }

        public static List<ABE_REGIONES_BIOCLIMATICAS> CatRegionBioclimaticas()
        {
            var catRegionBio = AccesoDatos.Catalogos.Tecnologia.ObtenRegionesBioclimaticas();

            return catRegionBio;
        }


        public static List<CAT_PLANTILLA> OCatPlantillas()
        {
            var catPlantillas = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtenTiposPlantillas();

            return catPlantillas;
        }

        public static CAT_PLANTILLA OCatPlantilla(int idPlantilla)
        {
            var plantilla = AccesoDatos.Catalogos.Tecnologia.ObtenDatosCatPlantilla(idPlantilla);

            return plantilla;
        }
        
        public static List<CAT_TECNOLOGIA> OCatTecnologias()
        {
            var catTecnologias = AccesoDatos.Catalogos.Tecnologia.ObtenCatTecnologias();

            return catTecnologias;
        }

        public static List<CAT_TECNOLOGIA> OCatTecnologiasSustitucion()
        {
            var catTecnologias = AccesoDatos.Catalogos.Tecnologia.ObtenCatTecnologiasSustitucion();

            return catTecnologias;
        }

        public static CAT_TECNOLOGIA BuscaTecnologia(int idTecnologia)
        {
            var tecnologia = AccesoDatos.Catalogos.Tecnologia.ObtieneCat_Tecnologia(idTecnologia);

            return tecnologia;
        }

        #endregion

        #region Caracteristicas Tecnologia

        public List<COMP_TECNOLOGIA> ObtenTecnologias()
        {
            var rownum = 1;
            var listTecnologias = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtenListaTecnologias();

            foreach (var tecnologia in listTecnologias)
            {
                tecnologia.tarifasTecnologia = ObtenTarifasXTecnologia(tecnologia.CveTecnologia);
                tecnologia.Rownum = rownum;

                foreach (var tarifa in tecnologia.tarifasTecnologia)
                {
                    switch (tarifa.DxTarifa)
                    {
                        case "02":
                            tecnologia.Tarifa02 = "SI";
                            break;

                        case "03":
                            tecnologia.Tarifa03 = "SI";
                            break;

                        case "OM":
                            tecnologia.TarifaOM = "SI";
                            break;

                        case "HM":
                            tecnologia.TarifaHM = "SI";
                            break;
                    }
                }

                rownum++;
            }

            return listTecnologias;
        }

        public COMP_TECNOLOGIA ObtenTecnologiaSeleccionada(int? idTecnologia)
        {
            var resultado = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtenTecnologiaFiltro(idTecnologia);

            return resultado;
        }

        public List<Tarifa_Tecnologia> ObtenTarifasXTecnologia(int? idTecnologia)
        {
            var resultado = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtenTarifasXTecnologia(idTecnologia);

            return resultado;
        }

        public List<Combinacion_Tecnologia> ObtenCombinacionTecnologias(int? idTecnologia)
        {
            var resultado = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ObtenTecnologiasCombinadas(idTecnologia);

            return resultado;
        }

        #endregion

        #region Agregar NuevaTecnologia

        public CAT_TIPO_TECNOLOGIA AGregarTipoTecnologia(CAT_TIPO_TECNOLOGIA tipoTdecnologia)
        {
            var resultado = AccesoDatos.Catalogos.Tecnologia.AGregarTipoTecnologia(tipoTdecnologia);

            return resultado;
        }
        
        public int AgregarNuevaTecnologia(COMP_TECNOLOGIA nuevatecnologia)
        {
            var tecnologia = new CAT_TECNOLOGIA
            {
                Cve_Tecnologia = nuevatecnologia.CveTecnologia,
                Dx_Nombre_General = nuevatecnologia.DxNombreGeneral,
                Dx_Nombre_Particular = nuevatecnologia.DxNombreGeneral,
                Dt_Fecha_Tecnologoia = DateTime.Now.Date,
                Cve_Tipo_Tecnologia = nuevatecnologia.CveTipoTecnologia,
                Dx_Cve_CC = nuevatecnologia.DxCveCC,
                Cve_Tipo_Movimiento = nuevatecnologia.CveTipoMovimiento,
                Cve_Equipos_Baja = nuevatecnologia.CveEquiposBaja,
                Cve_Equipos_Alta = nuevatecnologia.CveEquiposAlta,
                Cve_Chatarrizacion = nuevatecnologia.CveChatarrizacion,
                Monto_Chatarrizacion = nuevatecnologia.Monto_Chatarrizacion,
                Cve_Factor_Sustitucion = nuevatecnologia.CveFactorSustitucion,
                Cve_Incentivo = nuevatecnologia.CveIncentivo,
                Monto_Incentivo = nuevatecnologia.MontoIncentivo,
                Combina_Tecnologias = nuevatecnologia.CombinaTecnologias,
                Estatus = nuevatecnologia.Estatus,
                Cve_Plantilla = nuevatecnologia.CvePlantilla,
                Cve_Leyenda = nuevatecnologia.CveLeyenda
            };

            var tipoTecnologia = new CAT_TIPO_TECNOLOGIA
            {
                Dx_Nombre = tecnologia.Dx_Nombre_General,
                Dt_Fecha_Tipo_Tecnologia = DateTime.Now
            };

            var resTipotecnologia = AGregarTipoTecnologia(tipoTecnologia);

            CAT_TECNOLOGIA resultado = null;

            if (resTipotecnologia.Cve_Tipo_Tecnologia > 0)
            {
                tecnologia.Cve_Tipo_Tecnologia = resTipotecnologia.Cve_Tipo_Tecnologia;
                resultado = AccesoDatos.Catalogos.Tecnologia.ClassInstance.AgregarTecnologia(tecnologia);

                if (resultado.Cve_Tecnologia != 0)
                {
                    if (resultado.Combina_Tecnologias == 1)
                    {
                        foreach (var combinacionTec in nuevatecnologia.tecnologiasCombinadas)
                        {
                            var combinacion = new CAT_COMBINACION_TECNOLOGIAS
                            {
                                Cve_tecnologia = resultado.Cve_Tecnologia,
                                Cve_tecnologia_combinada = combinacionTec.CveTecnologiaCombinada,
                                Estatus = combinacionTec.Estatus,
                                Fecha_adicion = DateTime.Now
                            };

                            AccesoDatos.Catalogos.Tecnologia.ClassInstance.AgregarCombinacionTecnologia(combinacion);
                        }
                    }

                    foreach (var tarifaNva in nuevatecnologia.tarifasTecnologia)
                    {
                        var tarifa = new CAT_TARIFAS_X_TECNOLOGIA
                        {
                            Cve_tecnologia = resultado.Cve_Tecnologia,
                            Cve_Tarifa = tarifaNva.CveTarifa,
                            Estatus = tarifaNva.Estatus,
                            Fecha_adicion = DateTime.Now
                        };

                        AccesoDatos.Catalogos.Tecnologia.ClassInstance.AgregarTarifaXTecnologia(tarifa);
                    }
                }
            }

            if (resultado != null)
                return resultado.Cve_Tecnologia;

            else
                return 0;
        }
        #endregion

        #region Actualizar tecnologia

        public bool ActualizarTecnologia(COMP_TECNOLOGIA tecnologiaCambios)
        {
            var tecnologia = AccesoDatos.Catalogos.Tecnologia.ObtieneCat_Tecnologia(tecnologiaCambios.CveTecnologia);

            //tecnologia.Cve_Tecnologia = tecnologiaCambios.CveTecnologia;
            tecnologia.Dx_Nombre_General = tecnologiaCambios.DxNombreGeneral;
            tecnologia.Dx_Nombre_Particular = tecnologiaCambios.DxNombreGeneral;
            //tecnologia.Cve_Tipo_Tecnologia = tecnologiaCambios.CveTipoTecnologia;
            tecnologia.Dx_Cve_CC = tecnologiaCambios.DxCveCC;
            tecnologia.Cve_Tipo_Movimiento = tecnologiaCambios.CveTipoMovimiento;
            tecnologia.Cve_Equipos_Baja = tecnologiaCambios.CveEquiposBaja;
            tecnologia.Cve_Equipos_Alta = tecnologiaCambios.CveEquiposAlta;
            tecnologia.Cve_Chatarrizacion = tecnologiaCambios.CveChatarrizacion;
            tecnologia.Monto_Chatarrizacion = tecnologiaCambios.Monto_Chatarrizacion;
            tecnologia.Cve_Factor_Sustitucion = tecnologiaCambios.CveFactorSustitucion;
            tecnologia.Cve_Incentivo = tecnologiaCambios.CveIncentivo;
            tecnologia.Monto_Incentivo = tecnologiaCambios.MontoIncentivo;
            tecnologia.Combina_Tecnologias = tecnologiaCambios.CombinaTecnologias;
            tecnologia.Estatus = tecnologiaCambios.Estatus;
            tecnologia.Cve_Plantilla = tecnologiaCambios.CvePlantilla;
            tecnologia.Cve_Leyenda = tecnologiaCambios.CveLeyenda;
            
            var insertoTecnologia = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ActualizaTecnologia(tecnologia);

            return insertoTecnologia;
        }

        public bool ActualizaTecnologiasCombinadas(List<Combinacion_Tecnologia> listTecnologiasCombinadas)
        {
            CAT_COMBINACION_TECNOLOGIAS resultado = null;
            var actualiza = false;
            try
            {
                foreach (var tecCombinada in listTecnologiasCombinadas)
                {
                    var combinacion = new CAT_COMBINACION_TECNOLOGIAS
                    {
                        Cve_tecnologia = tecCombinada.CveTecnologia,
                        Cve_tecnologia_combinada = tecCombinada.CveTecnologiaCombinada,
                        Estatus = tecCombinada.Estatus,
                        Fecha_adicion = DateTime.Now
                    };

                    if (tecCombinada.EstatusInt == 0)
                        resultado = AccesoDatos.Catalogos.Tecnologia.ClassInstance.AgregarCombinacionTecnologia(combinacion);
                    if (resultado != null && resultado.Cve_tecnologia != 0)
                        actualiza = true;
                    else
                        actualiza = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ActualizaCombinacionTecnologia(combinacion);
                }
                return actualiza;
            }
            catch
            {
                return false;
            }
        }

        public bool ActualizaTarifasXTecnologia(List<Tarifa_Tecnologia> listTarifasXTecnologia)
        {
            var actualiza = false;

            try
            {
                foreach (var tarifaTecnologia in listTarifasXTecnologia)
                {
                    var tarifa = new CAT_TARIFAS_X_TECNOLOGIA
                    {
                        Cve_tecnologia = tarifaTecnologia.CveTecnologia,
                        Cve_Tarifa = tarifaTecnologia.CveTarifa,
                        Estatus = tarifaTecnologia.Estatus,
                        Fecha_adicion = DateTime.Now
                    };

                    if (tarifaTecnologia.EstatusInt == 0)
                    {
                      var datos =  AccesoDatos.Catalogos.Tecnologia.ClassInstance.AgregarTarifaXTecnologia(tarifa);
                        if (datos.Cve_Tarifa != 0)
                            actualiza = true;
                    }
                    else
                        actualiza = AccesoDatos.Catalogos.Tecnologia.ClassInstance.ActualizaTarifaXTecnologia(tarifa);
                }
                return actualiza;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
