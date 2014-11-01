using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AltaBajaEquipos;
using PAEEEM.Entidades.ModuloCentral;
using PAEEEM.Entidades.Tecnologias;
using PAEEEM.Entidades.Utilizables;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Tecnologia
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
        private static readonly Tecnologia _classInstance = new Tecnologia();
        public static Tecnologia ClassInstance { get { return _classInstance; } }

        public Tecnologia()
        { }

        #region Metodos de Consulta
        public List<COMP_TECNOLOGIA> ObtenListaTecnologias()
        {
            var resultado = (from tecnologias in _contexto.CAT_TECNOLOGIA
                             join m in _contexto.CAT_TIPO_MOVIMIENTO
                                on tecnologias.Cve_Tipo_Movimiento
                                equals m.Cve_Tipo_Movimiento
                             join e in _contexto.CAT_EQUIPOS_BAJA_ALTA
                                 on tecnologias.Cve_Equipos_Baja
                                 equals e.Cve_Equipos_Baja_Alta
                             join e2 in _contexto.CAT_EQUIPOS_BAJA_ALTA
                                 on tecnologias.Cve_Equipos_Alta
                                 equals e2.Cve_Equipos_Baja_Alta
                             join f in _contexto.CAT_FACTOR_SUSTITUCION
                                 on tecnologias.Cve_Factor_Sustitucion
                                 equals f.Cve_Factor_Sutitucion
                             select new COMP_TECNOLOGIA
                             {
                                 CveTecnologia = tecnologias.Cve_Tecnologia,
                                 DxNombreGeneral = tecnologias.Dx_Nombre_General,
                                 DtFechaTecnologoia = tecnologias.Dt_Fecha_Tecnologoia,
                                 DxCveCC = tecnologias.Dx_Cve_CC,
                                 CveEsquema = tecnologias.Cve_Esquema,
                                 CveGasto = tecnologias.Cve_Gasto,
                                 CveTipoMovimiento = tecnologias.Cve_Tipo_Movimiento,
                                 DxTipoMovimiento = m.Dx_Tipo_Movimiento,
                                 CveEquiposBaja = tecnologias.Cve_Equipos_Baja,
                                 DxEquiposBaja = e.Dx_Equipos_Baja_Alta,
                                 CveEquiposAlta = tecnologias.Cve_Equipos_Alta,
                                 DxEquiposAlta = e2.Dx_Equipos_Baja_Alta,
                                 CveChatarrizacion = tecnologias.Cve_Chatarrizacion,
                                 DxChatarrizacion = tecnologias.Cve_Chatarrizacion == 1 ? "SI" : "NO",
                                 Monto_Chatarrizacion = tecnologias.Monto_Chatarrizacion,
                                 CveFactorSustitucion = tecnologias.Cve_Factor_Sustitucion,
                                 DxFactorSustitucion = f.Dx_Factor_Sustitucion,
                                 CveIncentivo = tecnologias.Cve_Incentivo,
                                 DxIncentivo = tecnologias.Cve_Incentivo == 1 ? "SI" : "NO",
                                 MontoIncentivo = tecnologias.Monto_Incentivo != null ? (tecnologias.Monto_Incentivo / 100) : 0,
                                 CombinaTecnologias = tecnologias.Combina_Tecnologias,
                                 DxTecnologias = tecnologias.Combina_Tecnologias == 1 ? "SI" : "NO",
                                 Estatus = tecnologias.Estatus,
                                 DxEstatus = tecnologias.Estatus == 1 ? "ACTIVA" : "INACTIVA",
                                 CveLeyenda = tecnologias.Cve_Leyenda
                             }).ToList();

            return resultado;

        }

        public COMP_TECNOLOGIA ObtenTecnologiaFiltro(int? idTecnologia)
        {
            var resultado = (from tecnologias in _contexto.CAT_TECNOLOGIA
                             join m in _contexto.CAT_TIPO_MOVIMIENTO
                                on tecnologias.Cve_Tipo_Movimiento
                                equals m.Cve_Tipo_Movimiento
                             join e in _contexto.CAT_EQUIPOS_BAJA_ALTA
                                 on tecnologias.Cve_Equipos_Baja
                                 equals e.Cve_Equipos_Baja_Alta
                             join e2 in _contexto.CAT_EQUIPOS_BAJA_ALTA
                                 on tecnologias.Cve_Equipos_Alta
                                 equals e2.Cve_Equipos_Baja_Alta
                             join f in _contexto.CAT_FACTOR_SUSTITUCION
                                 on tecnologias.Cve_Factor_Sustitucion
                                 equals f.Cve_Factor_Sutitucion
                             //where tecnologias.Cve_Tecnologia == idTecnologia
                             select new COMP_TECNOLOGIA
                             {
                                 CveTecnologia = tecnologias.Cve_Tecnologia,
                                 CveTipoTecnologia = (int)tecnologias.Cve_Tipo_Tecnologia,
                                 DxNombreGeneral = tecnologias.Dx_Nombre_General,
                                 DtFechaTecnologoia = tecnologias.Dt_Fecha_Tecnologoia,
                                 DxCveCC = tecnologias.Dx_Cve_CC,
                                 CveEsquema = tecnologias.Cve_Esquema,
                                 CveGasto = tecnologias.Cve_Gasto,
                                 CveTipoMovimiento = tecnologias.Cve_Tipo_Movimiento,
                                 DxTipoMovimiento = m.Dx_Tipo_Movimiento,
                                 CveEquiposBaja = tecnologias.Cve_Equipos_Baja,
                                 DxEquiposBaja = e.Dx_Equipos_Baja_Alta,
                                 CveEquiposAlta = tecnologias.Cve_Equipos_Alta,
                                 DxEquiposAlta = e2.Dx_Equipos_Baja_Alta,
                                 CveChatarrizacion = tecnologias.Cve_Chatarrizacion,
                                 DxChatarrizacion = tecnologias.Cve_Chatarrizacion == 1 ? "SI" : "NO",
                                 Monto_Chatarrizacion = tecnologias.Monto_Chatarrizacion,
                                 CveFactorSustitucion = tecnologias.Cve_Factor_Sustitucion,
                                 DxFactorSustitucion = f.Dx_Factor_Sustitucion,
                                 CveIncentivo = tecnologias.Cve_Incentivo,
                                 DxIncentivo = tecnologias.Cve_Incentivo == 1 ? "SI" : "NO",
                                 MontoIncentivo = tecnologias.Monto_Incentivo ?? 0,
                                 CombinaTecnologias = tecnologias.Combina_Tecnologias,
                                 DxTecnologias = tecnologias.Combina_Tecnologias == 1 ? "SI" : "NO",
                                 Estatus = tecnologias.Estatus,
                                 DxEstatus = tecnologias.Estatus == 1 ? "ACTIVA" : "INACTIVA",
                                 CvePlantilla = tecnologias.Cve_Plantilla,
                                 CveLeyenda = tecnologias.Cve_Leyenda
                             }).ToList().FirstOrDefault(me => me.CveTecnologia == idTecnologia);
            return resultado;
        }

        public COMP_TECNOLOGIA ObtenObjetoTecnologia(int? idTecnologia)
        {
            var tecnologia = (from tecnologias in _contexto.CAT_TECNOLOGIA
                              join m in _contexto.CAT_TIPO_MOVIMIENTO
                                 on tecnologias.Cve_Tipo_Movimiento
                                 equals m.Cve_Tipo_Movimiento
                              join e in _contexto.CAT_EQUIPOS_BAJA_ALTA
                                  on tecnologias.Cve_Equipos_Baja
                                  equals e.Cve_Equipos_Baja_Alta
                              join e2 in _contexto.CAT_EQUIPOS_BAJA_ALTA
                                  on tecnologias.Cve_Equipos_Alta
                                  equals e2.Cve_Equipos_Baja_Alta
                              join f in _contexto.CAT_FACTOR_SUSTITUCION
                                  on tecnologias.Cve_Factor_Sustitucion
                                  equals f.Cve_Factor_Sutitucion
                              //where tecnologias.Cve_Tecnologia == idTecnologia
                              select new COMP_TECNOLOGIA
                              {
                                  CveTecnologia = tecnologias.Cve_Tecnologia,
                                  CveTipoTecnologia = (int)tecnologias.Cve_Tipo_Tecnologia,
                                  DxNombreGeneral = tecnologias.Dx_Nombre_General,
                                  DtFechaTecnologoia = tecnologias.Dt_Fecha_Tecnologoia,
                                  DxCveCC = tecnologias.Dx_Cve_CC,
                                  CveEsquema = tecnologias.Cve_Esquema,
                                  CveGasto = tecnologias.Cve_Gasto,
                                  CveTipoMovimiento = tecnologias.Cve_Tipo_Movimiento,
                                  DxTipoMovimiento = m.Dx_Tipo_Movimiento,
                                  CveEquiposBaja = tecnologias.Cve_Equipos_Baja,
                                  DxEquiposBaja = e.Dx_Equipos_Baja_Alta,
                                  CveEquiposAlta = tecnologias.Cve_Equipos_Alta,
                                  DxEquiposAlta = e2.Dx_Equipos_Baja_Alta,
                                  CveChatarrizacion = tecnologias.Cve_Chatarrizacion,
                                  DxChatarrizacion = tecnologias.Cve_Chatarrizacion == 1 ? "SI" : "NO",
                                  Monto_Chatarrizacion = tecnologias.Monto_Chatarrizacion,
                                  CveFactorSustitucion = tecnologias.Cve_Factor_Sustitucion,
                                  DxFactorSustitucion = f.Dx_Factor_Sustitucion,
                                  CveIncentivo = tecnologias.Cve_Incentivo,
                                  DxIncentivo = tecnologias.Cve_Incentivo == 1 ? "SI" : "NO",
                                  MontoIncentivo = tecnologias.Monto_Incentivo ?? 0,
                                  CombinaTecnologias = tecnologias.Combina_Tecnologias,
                                  DxTecnologias = tecnologias.Combina_Tecnologias == 1 ? "SI" : "NO",
                                  Estatus = tecnologias.Estatus,
                                  DxEstatus = tecnologias.Estatus == 1 ? "ACTIVA" : "INACTIVA",
                                  CvePlantilla = tecnologias.Cve_Plantilla
                              }).ToList().FirstOrDefault(me => me.CveTecnologia == idTecnologia);

            if (tecnologia == null) return null;

            tecnologia.tarifasTecnologia = ObtenTarifasXTecnologia(idTecnologia);

            if (tecnologia.CombinaTecnologias == 1)
                tecnologia.tecnologiasCombinadas = ObtenTecnologiasCombinadas(idTecnologia);

            return tecnologia;
        }



        public CAT_TARIFA ObtieneCat_Tarifa(int id)
        {
            CAT_TARIFA trInfoGeneral;

            using (var r = new Repositorio<CAT_TARIFA>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Cve_Tarifa == id);
            }
            return trInfoGeneral;
        }

        public static CAT_TECNOLOGIA ObtieneCat_Tecnologia(int id)
        {
            CAT_TECNOLOGIA trInfoGeneral;

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Cve_Tecnologia == id);
            }
            return trInfoGeneral;
        }

        public static CAT_TECNOLOGIA Obtiene_Tecnologia(string desTecnologia)
        {
            CAT_TECNOLOGIA trInfoGeneral;

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Dx_Nombre_General == desTecnologia);
            }
            return trInfoGeneral;
        }

        public static List<ABE_REGIONES_BIOCLIMATICAS> ObtenRegionesBioclimaticas()
        {
            List<ABE_REGIONES_BIOCLIMATICAS> resultado;

            using (var r = new Repositorio<ABE_REGIONES_BIOCLIMATICAS>())
            {
                resultado = r.Filtro(me => me.ESTATUS);
            }

            return resultado;
        }


        public List<Tarifa_Tecnologia> ObtenTarifasXTecnologia(int? idTecnologia)
        {
            var resultado = (from tarifas in _contexto.CAT_TARIFAS_X_TECNOLOGIA
                             join tar in _contexto.CAT_TARIFA
                                on tarifas.Cve_Tarifa
                                equals tar.Cve_Tarifa
                             where tarifas.Cve_tecnologia == idTecnologia
                             select new Tarifa_Tecnologia
                             {
                                 CveTecnologia = tarifas.Cve_tecnologia,
                                 CveTarifa = tarifas.Cve_Tarifa,
                                 DxTarifa = tar.Dx_Tarifa,
                                 Estatus = (int)tarifas.Estatus,
                                 FechaAdicion = tarifas.Fecha_adicion,
                                 EstatusInt = 1,
                             }).ToList();

            return resultado;
        }

        public List<Combinacion_Tecnologia> ObtenTecnologiasCombinadas(int? idTecnologia)
        {
            var resultado = (from tecnologiasCombinadas in _contexto.CAT_COMBINACION_TECNOLOGIAS
                             join tec in _contexto.CAT_TECNOLOGIA
                               on tecnologiasCombinadas.Cve_tecnologia_combinada
                               equals tec.Cve_Tecnologia
                             where tecnologiasCombinadas.Cve_tecnologia == idTecnologia
                             select new Combinacion_Tecnologia
                             {
                                 CveTecnologia = tecnologiasCombinadas.Cve_tecnologia,
                                 CveTecnologiaCombinada = tecnologiasCombinadas.Cve_tecnologia_combinada,
                                 TecnologíaCombinada = tec.Dx_Nombre_General,
                                 Estatus = tecnologiasCombinadas.Estatus,
                                 FechaAdicion = tecnologiasCombinadas.Fecha_adicion,
                                 EstatusInt = 1,
                             }).ToList();

            return resultado;
        }

        public List<CAT_TIPO_TECNOLOGIA> ObtenTiposTeconogia()
        {
            List<CAT_TIPO_TECNOLOGIA> resultado;

            using (var r = new Repositorio<CAT_TIPO_TECNOLOGIA>())
            {
                resultado = r.Filtro(me => me.Cve_Tipo_Tecnologia > 0);
            }

            return resultado;
        }

        public List<CAT_PLANTILLA> ObtenTiposPlantillas()
        {
            List<CAT_PLANTILLA> resultadCatPlantillas;

            using (var r = new Repositorio<CAT_PLANTILLA>())
            {
                resultadCatPlantillas = r.Filtro(me => me.Cve_Plantilla > 0);
            }

            return resultadCatPlantillas;
        }

        public List<CAT_TECNOLOGIA> ObtieneTecnologias(Expression<Func<CAT_TECNOLOGIA, bool>> criterio)
        {
            var tecnologias = new List<CAT_TECNOLOGIA>();

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                tecnologias = r.Filtro(criterio);
            }

            return tecnologias;
        }

        public List<CAT_TECNOLOGIA> ObtieneTecnologiasXTarifa(int CveTarifa)
        {
            var tecnologia = new List<CAT_TECNOLOGIA>();

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                tecnologia = (from tecnologias in _contexto.CAT_TECNOLOGIA
                              join tar in _contexto.CAT_TARIFAS_X_TECNOLOGIA
                                on tecnologias.Cve_Tecnologia equals tar.Cve_tecnologia
                              where tar.Cve_Tarifa == CveTarifa
                              select tecnologias).Distinct().ToList<CAT_TECNOLOGIA>();
            }

            return tecnologia;
        }

        public List<CAT_TECNOLOGIA> ObtieneTecnologiasXTarifaProveedor(int CveTarifa, int idProveedor)
        {
            var tecnologia = new List<CAT_TECNOLOGIA>();

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                tecnologia = (from tecnologias in _contexto.CAT_TECNOLOGIA
                              join tar in _contexto.CAT_TARIFAS_X_TECNOLOGIA
                                on tecnologias.Cve_Tecnologia equals tar.Cve_tecnologia
                              join p in _contexto.CAT_PRODUCTO
                                on tecnologias.Cve_Tecnologia equals p.Cve_Tecnologia
                              join k2 in _contexto.K_PROVEEDOR_PRODUCTO
                                on p.Cve_Producto equals k2.Cve_Producto
                              where tar.Cve_Tarifa == CveTarifa
                              && k2.Id_Proveedor == idProveedor
                              select tecnologias).Distinct().ToList<CAT_TECNOLOGIA>();
            }

            return tecnologia;
        }

        public List<ABE_TABLAS_AHORRO> ObtieneConsumos(int FtTipoTecnologia)
        {
            var tecnologias = new List<ABE_TABLAS_AHORRO>();

            using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
            {
                tecnologias = (from consumos in _contexto.ABE_TABLAS_AHORRO
                               where consumos.FT_TIPO_PRODUCTO == FtTipoTecnologia
                               orderby consumos.CAP_INICIAL ascending
                               select consumos).ToList<ABE_TABLAS_AHORRO>();
            }

            return tecnologias;
        }

        public List<Valor_Catalogo> ObtenCapacidades(int tipoProducto)
        {

            var lstCapacidad = (from capacidad in _contexto.CAT_CAPACIDAD_SUSTITUCION
                                join prd in _contexto.CAT_PRODUCTO
                                    on capacidad.Cve_Capacidad_Sust equals prd.Cve_Capacidad_Sust
                                where prd.Ft_Tipo_Producto == tipoProducto
                                group capacidad by capacidad.Cve_Capacidad_Sust
                                into cat
                                select new Valor_Catalogo
                                    {
                                        CveValorCatalogo = SqlFunctions.StringConvert((double)cat.Average(x => x.Cve_Capacidad_Sust)).Trim(), //capacidad.Cve_Capacidad_Sust.ToString(),
                                        DescripcionCatalogo = SqlFunctions.StringConvert(cat.Average(x => x.No_Capacidad)).Trim()
                                    }
                               ).ToList();

            return lstCapacidad;
        }

        public List<CAT_CAPACIDAD_SUSTITUCION> ObtenCapacidadSustitucion(int idTecnologia)
        {

            var lstCapacidad = (from capacidad in _contexto.CAT_CAPACIDAD_SUSTITUCION
                                where capacidad.Cve_Tecnologia == idTecnologia
                                //&& capacidad.Ft_Tipo_Producto == tipoProducto
                                orderby capacidad.No_Capacidad
                                select capacidad).Distinct()
                                                 .ToList<CAT_CAPACIDAD_SUSTITUCION>();

            var lstCapacidadesSustitucion =
                new List<CAT_CAPACIDAD_SUSTITUCION>(lstCapacidad.OrderBy(me => me.No_Capacidad));

            return lstCapacidadesSustitucion;
        }

        public List<CAT_CAPACIDAD_SUSTITUCION> ObtenCapacidadSustitucionSa(int idTecnologia, int tipoProducto)
        {

            var lstCapacidad = (from capacidad in _contexto.CAT_CAPACIDAD_SUSTITUCION
                                where capacidad.Cve_Tecnologia == idTecnologia
                                && capacidad.Ft_Tipo_Producto == tipoProducto
                                orderby capacidad.No_Capacidad
                                select capacidad).Distinct()
                                                 .ToList<CAT_CAPACIDAD_SUSTITUCION>();

            var lstCapacidadesSustitucion =
                new List<CAT_CAPACIDAD_SUSTITUCION>(lstCapacidad.OrderBy(me => me.No_Capacidad));

            return lstCapacidadesSustitucion;
        }

        public List<Valor_Catalogo> ObtenSistemaArreglo(int tipoProducto)
        {
            var lstSistemaArreglo = (from sistema in _contexto.SISTEMA_ARREGLO
                                     where sistema.Ft_Tipo_Producto == tipoProducto
                                     select new Valor_Catalogo
                                         {
                                             CveValorCatalogo =
                                                 SqlFunctions.StringConvert((double) sistema.IdSistemaArreglo).Trim(),
                                             DescripcionCatalogo = sistema.SistemaArreglo
                                         }).ToList();

            return lstSistemaArreglo;
        }

        public byte ObtenIdSistemaArreglo(int idCapacidadSustitucion)
        {
            var idSistemaArreglo = (from capacidad in _contexto.CAT_CAPACIDAD_SUSTITUCION
                                    where capacidad.Cve_Capacidad_Sust == idCapacidadSustitucion
                                    select capacidad).ToList().FirstOrDefault().IdSistemaArreglo;

            return (byte)idSistemaArreglo;
        }

        //CAPACIDAD SOLO PARA AIRE ACONDICIONADO Y MOTORES ELECTRICOS
        public List<ABE_TABLAS_AHORRO> ObtieneConsumosAA_ME(int cveTecnolgia)
        {
            var tecnologias = new List<ABE_TABLAS_AHORRO>();

            using (var r = new Repositorio<ABE_TABLAS_AHORRO>())
            {
                tecnologias = (from consumos in _contexto.ABE_TABLAS_AHORRO
                               where consumos.CVE_TECNOLOGIA == cveTecnolgia
                               orderby consumos.CAP_INICIAL ascending
                               select consumos).ToList();
            }

            return tecnologias;
        }

        public static CAT_PLANTILLA ObtenDatosCatPlantilla(int idPlantilla)
        {
            CAT_PLANTILLA plantilla = null;

            using (var r = new Repositorio<CAT_PLANTILLA>())
            {
                plantilla = r.Extraer(me => me.Cve_Plantilla == idPlantilla);
            }

            return plantilla;
        }

        public static List<CAT_TECNOLOGIA> ObtenCatTecnologias()
        {
            List<CAT_TECNOLOGIA> resultado;

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                resultado = r.Filtro(me => me.Estatus == 1);
            }

            return resultado;
        }

        public static List<CAT_TECNOLOGIA> ObtenCatTecnologiasSustitucion()
        {
            List<CAT_TECNOLOGIA> resultado;

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                resultado = r.Filtro(me => me.Estatus == 1 && (me.Cve_Esquema == 0 && me.Cve_Tipo_Movimiento == "2") || (me.Cve_Esquema == 1 && me.Cve_Tipo_Movimiento == "2"));
            }

            return resultado;
        }


        public CompDetalleTecnologia ValidaEquipoParaAlta(int idTecnologia)
        {
            var complexTecnologia =(from TEC in _contexto.CAT_TECNOLOGIA
                                    join TT in _contexto.CAT_TIPO_TECNOLOGIA on TEC.Cve_Tipo_Tecnologia equals
                                        TT.Cve_Tipo_Tecnologia
                                    join EB in _contexto.CAT_EQUIPOS_BAJA_ALTA on TEC.Cve_Equipos_Baja equals
                                        EB.Cve_Equipos_Baja_Alta
                                    join EA in _contexto.CAT_EQUIPOS_BAJA_ALTA on TEC.Cve_Equipos_Alta equals
                                        EA.Cve_Equipos_Baja_Alta
                                    join TM in _contexto.CAT_TIPO_MOVIMIENTO on TEC.Cve_Tipo_Movimiento equals
                                        TM.Cve_Tipo_Movimiento
                                    join FS in _contexto.CAT_FACTOR_SUSTITUCION on TEC.Cve_Factor_Sustitucion equals
                                        FS.Cve_Factor_Sutitucion
                                    where TEC.Cve_Tecnologia == idTecnologia
                                    select new CompDetalleTecnologia
                                        {
                                            CveTecnologia = TEC.Cve_Tecnologia,
                                            DxNombreGeneral = TEC.Dx_Nombre_General,
                                            CveTipoTecnologia = TT.Cve_Tipo_Tecnologia,
                                            TipoTecnologia = TT.Dx_Nombre,
                                            DxCveCc = TEC.Dx_Cve_CC,
                                            CveTipoMovimiento = TM.Cve_Tipo_Movimiento,
                                            DxTipoMovimiento = TM.Dx_Tipo_Movimiento,
                                            CveEquiposBaja = EB.Cve_Equipos_Baja_Alta,
                                            DxEquiposBaja = EB.Dx_Equipos_Baja_Alta,
                                            CveEqupoAlta = EA.Cve_Equipos_Baja_Alta,
                                            DxEquipoAlta = EA.Dx_Equipos_Baja_Alta,
                                            CveFactorSusticion = FS.Cve_Factor_Sutitucion,
                                            DxFactorSusticion = FS.Dx_Factor_Sustitucion,
                                            CveChatarrizacion = TEC.Cve_Chatarrizacion.Value == 1,
                                            CveGasto = TEC.Cve_Gasto.Value == 1,
                                            MontoChatarrizacion = TEC.Monto_Chatarrizacion,
                                            CveIncentivo = TEC.Cve_Incentivo.Value == 1,
                                            MontoIncentivo = (decimal)TEC.Monto_Incentivo,
                                            CveEsquema = TEC.Cve_Esquema != null ? (int)TEC.Cve_Esquema : 0,
                                            CombinaTecnologias = TEC.Combina_Tecnologias.Value == 1,
                                            NumeroGrupos = (int)TEC.NumeroGrupos
                                        }).FirstOrDefault();

            return complexTecnologia;
        }

        #endregion

        #region Metodos de Insercion
        public CAT_TECNOLOGIA AgregarTecnologia(CAT_TECNOLOGIA tecnologia)
        {
            CAT_TECNOLOGIA resultado;
            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                resultado = r.Agregar(tecnologia);
            }
            return resultado;
        }

        public CAT_TARIFAS_X_TECNOLOGIA AgregarTarifaXTecnologia(CAT_TARIFAS_X_TECNOLOGIA tarifaTecnologia)
        {
            CAT_TARIFAS_X_TECNOLOGIA resultado;

            using (var r = new Repositorio<CAT_TARIFAS_X_TECNOLOGIA>())
            {
                resultado = r.Agregar(tarifaTecnologia);
            }

            return resultado;
        }

        public CAT_COMBINACION_TECNOLOGIAS AgregarCombinacionTecnologia(CAT_COMBINACION_TECNOLOGIAS combinacionTecnologia)
        {
            CAT_COMBINACION_TECNOLOGIAS resultado;

            using (var r = new Repositorio<CAT_COMBINACION_TECNOLOGIAS>())
            {
                resultado = r.Agregar(combinacionTecnologia);
            }

            return resultado;
        }

        public static CAT_TIPO_TECNOLOGIA AGregarTipoTecnologia(CAT_TIPO_TECNOLOGIA tipoTdecnologia)
        {
            CAT_TIPO_TECNOLOGIA resultado = null;

            using (var r = new Repositorio<CAT_TIPO_TECNOLOGIA>())
            {
                resultado = r.Agregar(tipoTdecnologia);
            }

            return resultado;
        }

        #endregion

        #region Metodos de Actualizacion

        public bool ActualizaTecnologia(CAT_TECNOLOGIA tecnologia)
        {
            bool actualiza;

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                actualiza = r.Actualizar(tecnologia);
            }

            return actualiza;
        }

        public bool ActualizaCombinacionTecnologia(CAT_COMBINACION_TECNOLOGIAS combinacion)
        {
            bool actualiza;

            using (var r = new Repositorio<CAT_COMBINACION_TECNOLOGIAS>())
            {
                actualiza = r.Actualizar(combinacion);
            }

            return actualiza;
        }

        public bool ActualizaTarifaXTecnologia(CAT_TARIFAS_X_TECNOLOGIA tarifa)
        {
            bool actualiza;

            using (var r = new Repositorio<CAT_TARIFAS_X_TECNOLOGIA>())
            {
                actualiza = r.Actualizar(tarifa);
            }

            return actualiza;
        }

        #endregion

        #region Metodos de Eliminacion

        public bool EliminarTarifaTecnologia(CAT_TARIFAS_X_TECNOLOGIA tarifaTecnologia)
        {
            bool resultado;

            using (var r = new Repositorio<CAT_TARIFAS_X_TECNOLOGIA>())
            {
                resultado = r.Eliminar(tarifaTecnologia);
            }

            return resultado;
        }

        public bool EliminarCombinacionTecnologia(CAT_COMBINACION_TECNOLOGIAS combinacionTecnologia)
        {
            bool resultado;

            using (var r = new Repositorio<CAT_COMBINACION_TECNOLOGIAS>())
            {
                resultado = r.Eliminar(combinacionTecnologia);
            }

            return resultado;
        }

        public bool EliminarTecnologia(CAT_TECNOLOGIA tecnologia)
        {
            bool resultado;

            using (var r = new Repositorio<CAT_TECNOLOGIA>())
            {
                resultado = r.Eliminar(tecnologia);
            }

            return resultado;
        }

        #endregion
    }
}
