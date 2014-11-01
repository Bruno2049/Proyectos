using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Tarifas;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Tarifas
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<CAT_TARIFA> ObtieneTiposTarifas()
        {
            List<CAT_TARIFA> listTipoTarifa;

            using (var r = new Repositorio<CAT_TARIFA>())
            {
                listTipoTarifa = r.Filtro(c => true);
            }
            return listTipoTarifa;
        }

        public List<CAT_REGIONES_TARIFAS> ObtieneRegiones()
        {
            List<CAT_REGIONES_TARIFAS> listRegiones;

            using (var r = new Repositorio<CAT_REGIONES_TARIFAS>())
            {
                listRegiones = r.Filtro(c => true);
            }
            return listRegiones;
        }

        public List<DetalleTarifa> ObtieneDatosTarifa02(string fecha)
        {
            var query = (from tarifa02 in _contexto.K_TARIFA_02
                where tarifa02.FECHA_APLICABLE.Contains(fecha == "" ? tarifa02.FECHA_APLICABLE : fecha)
                orderby tarifa02.FECHA_MODIFICACION ?? tarifa02.FECHA_ADICION descending 
                select new DetalleTarifa
                {
                    IdTarifa02 = tarifa02.ID_TARIFA_02,
                    MtCostoKwhBasico = tarifa02.MT_COSTO_KWH_BASICO,
                    MtCostoKwhFijo = tarifa02.MT_COSTO_KWH_FIJO,
                    MtCostoKwhIntermedio = tarifa02.MT_COSTO_KWH_INTERMEDIO,
                    MtCostoKwhExcedente = tarifa02.MT_COSTO_KWH_EXCEDENTE,
                    Usuario = tarifa02.MODIFICADO_POR ?? tarifa02.ADICIONADO_POR,
                    Año =
                        tarifa02.MODIFICADO_POR == null
                            ? tarifa02.FECHA_ADICION.Year
                            : tarifa02.FECHA_MODIFICACION.Value.Year,
                    Dia =
                        tarifa02.MODIFICADO_POR == null
                            ? tarifa02.FECHA_ADICION.Day
                            : tarifa02.FECHA_MODIFICACION.Value.Day,
                    Hora =
                        tarifa02.MODIFICADO_POR == null
                            ? tarifa02.FECHA_ADICION.Hour
                            : tarifa02.FECHA_MODIFICACION.Value.Hour,
                    Minutos =
                        tarifa02.MODIFICADO_POR == null
                            ? tarifa02.FECHA_ADICION.Minute
                            : tarifa02.FECHA_MODIFICACION.Value.Minute,
                    Mes =
                        tarifa02.MODIFICADO_POR == null
                            ? tarifa02.FECHA_ADICION.Month == 1
                                ? "ENERO"
                                : tarifa02.FECHA_ADICION.Month == 2
                                    ? "FEBRERO"
                                    : tarifa02.FECHA_ADICION.Month == 3
                                        ? "MARZO"
                                        : tarifa02.FECHA_ADICION.Month == 4
                                            ? "ABRIL"
                                            : tarifa02.FECHA_ADICION.Month == 5
                                                ? "MAYO"
                                                : tarifa02.FECHA_ADICION.Month == 6
                                                    ? "JUNIO"
                                                    : tarifa02.FECHA_ADICION.Month == 7
                                                        ? "JULIO"
                                                        : tarifa02.FECHA_ADICION.Month == 8
                                                            ? "AGOSTO"
                                                            : tarifa02.FECHA_ADICION.Month == 9
                                                                ? "SEPTIEMBRE"
                                                                : tarifa02.FECHA_ADICION.Month == 10
                                                                    ? "OCTUBRE"
                                                                    : tarifa02.FECHA_ADICION.Month == 11
                                                                        ? "NOVIEMBRE"
                                                                        : tarifa02.FECHA_ADICION.Month == 12
                                                                            ? "DICIEMBRE"
                                                                            : "-"
                            : tarifa02.FECHA_MODIFICACION.Value.Month == 1
                                ? "ENERO"
                                : tarifa02.FECHA_MODIFICACION.Value.Month == 2
                                    ? "FEBRERO"
                                    : tarifa02.FECHA_MODIFICACION.Value.Month == 3
                                        ? "MARZO"
                                        : tarifa02.FECHA_MODIFICACION.Value.Month == 4
                                            ? "ABRIL"
                                            : tarifa02.FECHA_MODIFICACION.Value.Month == 5
                                                ? "MAYO"
                                                : tarifa02.FECHA_MODIFICACION.Value.Month == 6
                                                    ? "JUNIO"
                                                    : tarifa02.FECHA_MODIFICACION.Value.Month == 7
                                                        ? "JULIO"
                                                        : tarifa02.FECHA_MODIFICACION.Value.Month == 8
                                                            ? "AGOSTO"
                                                            : tarifa02.FECHA_MODIFICACION.Value.Month == 9
                                                                ? "SEPTIEMBRE"
                                                                : tarifa02.FECHA_MODIFICACION.Value.Month == 10
                                                                    ? "OCTUBRE"
                                                                    : tarifa02.FECHA_MODIFICACION.Value.Month == 11
                                                                        ? "NOVIEMBRE"
                                                                        : tarifa02.FECHA_MODIFICACION.Value.Month == 12
                                                                            ? "DICIEMBRE"
                                                                            : "-"
                }).ToList();

            return query;
        }

        public List<DetalleTarifa> ObtieneDatosTarifa03(string fecha)
        {
            var query = (from tarifa03 in _contexto.K_TARIFA_03
                where tarifa03.FECHA_APLICABLE.Contains(fecha == "" ? tarifa03.FECHA_APLICABLE : fecha)
                orderby tarifa03.FECHA_MODIFICACION ?? tarifa03.FECHA_ADICION descending 
                select new DetalleTarifa
                {
                    IdTarifa03 = tarifa03.ID_TARIFA_03,
                    MtCargoDemandaMax = tarifa03.MT_CARGO_DEMANDA_MAX,
                    MtCargoAdicional = tarifa03.MT_CARGO_ADICIONAL,
                    Usuario = tarifa03.MODIFICADO_POR ?? tarifa03.ADICIONADO_POR,
                    Año =
                        tarifa03.MODIFICADO_POR == null
                            ? tarifa03.FECHA_ADICION.Year
                            : tarifa03.FECHA_MODIFICACION.Value.Year,
                    Dia =
                        tarifa03.MODIFICADO_POR == null
                            ? tarifa03.FECHA_ADICION.Day
                            : tarifa03.FECHA_MODIFICACION.Value.Day,
                    Hora =
                        tarifa03.MODIFICADO_POR == null
                            ? tarifa03.FECHA_ADICION.Hour
                            : tarifa03.FECHA_MODIFICACION.Value.Hour,
                    Minutos =
                        tarifa03.MODIFICADO_POR == null
                            ? tarifa03.FECHA_ADICION.Minute
                            : tarifa03.FECHA_MODIFICACION.Value.Minute,
                    Mes =
                        tarifa03.MODIFICADO_POR == null
                            ? tarifa03.FECHA_ADICION.Month == 1
                                ? "ENERO"
                                : tarifa03.FECHA_ADICION.Month == 2
                                    ? "FEBRERO"
                                    : tarifa03.FECHA_ADICION.Month == 3
                                        ? "MARZO"
                                        : tarifa03.FECHA_ADICION.Month == 4
                                            ? "ABRIL"
                                            : tarifa03.FECHA_ADICION.Month == 5
                                                ? "MAYO"
                                                : tarifa03.FECHA_ADICION.Month == 6
                                                    ? "JUNIO"
                                                    : tarifa03.FECHA_ADICION.Month == 7
                                                        ? "JULIO"
                                                        : tarifa03.FECHA_ADICION.Month == 8
                                                            ? "AGOSTO"
                                                            : tarifa03.FECHA_ADICION.Month == 9
                                                                ? "SEPTIEMBRE"
                                                                : tarifa03.FECHA_ADICION.Month == 10
                                                                    ? "OCTUBRE"
                                                                    : tarifa03.FECHA_ADICION.Month == 11
                                                                        ? "NOVIEMBRE"
                                                                        : tarifa03.FECHA_ADICION.Month == 12
                                                                            ? "DICIEMBRE"
                                                                            : "-"
                            : tarifa03.FECHA_MODIFICACION.Value.Month == 1
                                ? "ENERO"
                                : tarifa03.FECHA_MODIFICACION.Value.Month == 2
                                    ? "FEBRERO"
                                    : tarifa03.FECHA_MODIFICACION.Value.Month == 3
                                        ? "MARZO"
                                        : tarifa03.FECHA_MODIFICACION.Value.Month == 4
                                            ? "ABRIL"
                                            : tarifa03.FECHA_MODIFICACION.Value.Month == 5
                                                ? "MAYO"
                                                : tarifa03.FECHA_MODIFICACION.Value.Month == 6
                                                    ? "JUNIO"
                                                    : tarifa03.FECHA_MODIFICACION.Value.Month == 7
                                                        ? "JULIO"
                                                        : tarifa03.FECHA_MODIFICACION.Value.Month == 8
                                                            ? "AGOSTO"
                                                            : tarifa03.FECHA_MODIFICACION.Value.Month == 9
                                                                ? "SEPTIEMBRE"
                                                                : tarifa03.FECHA_MODIFICACION.Value.Month == 10
                                                                    ? "OCTUBRE"
                                                                    : tarifa03.FECHA_MODIFICACION.Value.Month == 11
                                                                        ? "NOVIEMBRE"
                                                                        : tarifa03.FECHA_MODIFICACION.Value.Month == 12
                                                                            ? "DICIEMBRE"
                                                                            : "-"
                }).ToList();

            return query;
        }

        public List<DetalleTarifa> ObtieneDatosTarifa0M(string fecha, int idRegion)
        {
            var query = (from region in _contexto.CAT_REGIONES_TARIFAS
                join T in _contexto.K_TARIFA_OM
                    on region.id_region
                    equals T.ID_REGION
                         where T.FECHA_APLICABLE.Contains(fecha == "" ? T.FECHA_APLICABLE : fecha)
                    && T.ID_REGION == (idRegion == 0 ? T.ID_REGION : idRegion)
                         orderby T.FECHA_MODIFICACION ?? T.FECHA_ADICION descending 
                select new DetalleTarifa
                {
                    IdTarifaOm = T.ID_TARIFA_OM,
                    IdRegion = region.id_region,
                    Descripcion = region.Descripcion,
                    MtCargoKwhConsumo = T.MT_CARGO_KWH_CONSUMO,
                    MtCargoKwDemanda = T.MT_CARGO_KW_DEMANDA,
                    Usuario = T.MODIFICADO_POR ?? T.ADICIONADO_POR,
                    Año = T.MODIFICADO_POR == null ? T.FECHA_ADICION.Year : T.FECHA_MODIFICACION.Value.Year,
                    Dia = T.MODIFICADO_POR == null ? T.FECHA_ADICION.Day : T.FECHA_MODIFICACION.Value.Day,
                    Hora = T.MODIFICADO_POR == null ? T.FECHA_ADICION.Hour : T.FECHA_MODIFICACION.Value.Hour,
                    Minutos = T.MODIFICADO_POR == null ? T.FECHA_ADICION.Minute : T.FECHA_MODIFICACION.Value.Minute,
                    Mes =
                        T.MODIFICADO_POR == null
                            ? T.FECHA_ADICION.Month == 1
                                ? "ENERO"
                                : T.FECHA_ADICION.Month == 2
                                    ? "FEBRERO"
                                    : T.FECHA_ADICION.Month == 3
                                        ? "MARZO"
                                        : T.FECHA_ADICION.Month == 4
                                            ? "ABRIL"
                                            : T.FECHA_ADICION.Month == 5
                                                ? "MAYO"
                                                : T.FECHA_ADICION.Month == 6
                                                    ? "JUNIO"
                                                    : T.FECHA_ADICION.Month == 7
                                                        ? "JULIO"
                                                        : T.FECHA_ADICION.Month == 8
                                                            ? "AGOSTO"
                                                            : T.FECHA_ADICION.Month == 9
                                                                ? "SEPTIEMBRE"
                                                                : T.FECHA_ADICION.Month == 10
                                                                    ? "OCTUBRE"
                                                                    : T.FECHA_ADICION.Month == 11
                                                                        ? "NOVIEMBRE"
                                                                        : T.FECHA_ADICION.Month == 12
                                                                            ? "DICIEMBRE"
                                                                            : "-"
                            : T.FECHA_MODIFICACION.Value.Month == 1
                                ? "ENERO"
                                : T.FECHA_MODIFICACION.Value.Month == 2
                                    ? "FEBRERO"
                                    : T.FECHA_MODIFICACION.Value.Month == 3
                                        ? "MARZO"
                                        : T.FECHA_MODIFICACION.Value.Month == 4
                                            ? "ABRIL"
                                            : T.FECHA_MODIFICACION.Value.Month == 5
                                                ? "MAYO"
                                                : T.FECHA_MODIFICACION.Value.Month == 6
                                                    ? "JUNIO"
                                                    : T.FECHA_MODIFICACION.Value.Month == 7
                                                        ? "JULIO"
                                                        : T.FECHA_MODIFICACION.Value.Month == 8
                                                            ? "AGOSTO"
                                                            : T.FECHA_MODIFICACION.Value.Month == 9
                                                                ? "SEPTIEMBRE"
                                                                : T.FECHA_MODIFICACION.Value.Month == 10
                                                                    ? "OCTUBRE"
                                                                    : T.FECHA_MODIFICACION.Value.Month == 11
                                                                        ? "NOVIEMBRE"
                                                                        : T.FECHA_MODIFICACION.Value.Month == 12
                                                                            ? "DICIEMBRE"
                                                                            : "-"
                }).ToList();

            return query;
        }

        public List<DetalleTarifa> ObtieneDatosTarifa0M(string fecha)
        {
            var query1 = (from region in _contexto.CAT_REGIONES_TARIFAS
                          join T in _contexto.K_TARIFA_OM
                              on region.id_region
                              equals T.ID_REGION
                          where T.FECHA_APLICABLE == fecha
                          select new DetalleTarifa
                          {
                              IdTarifaOm = T.ID_TARIFA_OM,
                              IdRegion = T.ID_REGION,
                              Descripcion = region.Descripcion,
                              MtCargoKwhConsumo = T.MT_CARGO_KWH_CONSUMO,
                              MtCargoKwDemanda = T.MT_CARGO_KW_DEMANDA,
                              Usuario = T.MODIFICADO_POR ?? T.ADICIONADO_POR
                          }).ToList();

            var query2 = (from region in _contexto.CAT_REGIONES_TARIFAS
                          select new DetalleTarifa
                          {
                              IdRegion = region.id_region,
                              Descripcion = region.Descripcion,
                              MtCargoKwhConsumo = 0.00,
                              MtCargoKwDemanda = 0.00,
                              Usuario = ""
                          }).ToList();

            var query = (from info in query2
                         join tarifas in query1
                             on info.IdRegion equals tarifas.IdRegion into joinedInfoTarifas
                         from y1 in joinedInfoTarifas.DefaultIfEmpty()
                         select new DetalleTarifa
                         {
                             IdTarifaOm = y1 == null ? 0 : y1.IdTarifaOm,
                             IdRegion = info.IdRegion,
                             Descripcion = info.Descripcion,
                             MtCargoKwhConsumo = y1 == null ? 0.00 : y1.MtCargoKwhConsumo,
                             MtCargoKwDemanda = y1 == null ? 0.00 : y1.MtCargoKwDemanda,
                             Usuario = y1 == null ? "" : y1.Usuario
                         }).ToList();

            return query;
        }

        public List<DetalleTarifa> ObtieneDatosTarifaHm(string fecha, int idRegion)
        {
            var query = (from region in _contexto.CAT_REGIONES_TARIFAS
                join T in _contexto.K_TARIFA_HM
                    on region.id_region
                    equals T.ID_REGION
                where T.FECHA_APLICABLE.Contains(fecha == "" ? T.FECHA_APLICABLE : fecha)
                      && T.ID_REGION == (idRegion == 0 ? T.ID_REGION : idRegion)
                         orderby T.FECHA_MODIFICACION ?? T.FECHA_ADICION descending 
                select new DetalleTarifa
                {
                    IdTarifaHm = T.ID_TARIFA_HM,
                    IdRegion = T.ID_REGION,
                    Descripcion = region.Descripcion,
                    MtCargoDemanda = T.MT_CARGO_DEMANDA,
                    MtCargoBase = T.MT_CARGO_BASE,
                    MtCargoIntermedia = T.MT_CARGO_INTERMEDIA,
                    MtCargoPunta = T.MT_CARGO_PUNTA,
                    Promedio = T.PROMEDIO_TARIFA ?? 0.00,
                    Usuario = T.MODIFICADO_POR ?? T.ADICIONADO_POR,
                    Año = T.MODIFICADO_POR == null ? T.FECHA_ADICION.Year : T.FECHA_MODIFICACION.Value.Year,
                    Dia = T.MODIFICADO_POR == null ? T.FECHA_ADICION.Day : T.FECHA_MODIFICACION.Value.Day,
                    Hora = T.MODIFICADO_POR == null ? T.FECHA_ADICION.Hour : T.FECHA_MODIFICACION.Value.Hour,
                    Minutos = T.MODIFICADO_POR == null ? T.FECHA_ADICION.Minute : T.FECHA_MODIFICACION.Value.Minute,
                    Mes =
                        T.MODIFICADO_POR == null
                            ? T.FECHA_ADICION.Month == 1
                                ? "ENERO"
                                : T.FECHA_ADICION.Month == 2
                                    ? "FEBRERO"
                                    : T.FECHA_ADICION.Month == 3
                                        ? "MARZO"
                                        : T.FECHA_ADICION.Month == 4
                                            ? "ABRIL"
                                            : T.FECHA_ADICION.Month == 5
                                                ? "MAYO"
                                                : T.FECHA_ADICION.Month == 6
                                                    ? "JUNIO"
                                                    : T.FECHA_ADICION.Month == 7
                                                        ? "JULIO"
                                                        : T.FECHA_ADICION.Month == 8
                                                            ? "AGOSTO"
                                                            : T.FECHA_ADICION.Month == 9
                                                                ? "SEPTIEMBRE"
                                                                : T.FECHA_ADICION.Month == 10
                                                                    ? "OCTUBRE"
                                                                    : T.FECHA_ADICION.Month == 11
                                                                        ? "NOVIEMBRE"
                                                                        : T.FECHA_ADICION.Month == 12
                                                                            ? "DICIEMBRE"
                                                                            : "-"
                            : T.FECHA_MODIFICACION.Value.Month == 1
                                ? "ENERO"
                                : T.FECHA_MODIFICACION.Value.Month == 2
                                    ? "FEBRERO"
                                    : T.FECHA_MODIFICACION.Value.Month == 3
                                        ? "MARZO"
                                        : T.FECHA_MODIFICACION.Value.Month == 4
                                            ? "ABRIL"
                                            : T.FECHA_MODIFICACION.Value.Month == 5
                                                ? "MAYO"
                                                : T.FECHA_MODIFICACION.Value.Month == 6
                                                    ? "JUNIO"
                                                    : T.FECHA_MODIFICACION.Value.Month == 7
                                                        ? "JULIO"
                                                        : T.FECHA_MODIFICACION.Value.Month == 8
                                                            ? "AGOSTO"
                                                            : T.FECHA_MODIFICACION.Value.Month == 9
                                                                ? "SEPTIEMBRE"
                                                                : T.FECHA_MODIFICACION.Value.Month == 10
                                                                    ? "OCTUBRE"
                                                                    : T.FECHA_MODIFICACION.Value.Month == 11
                                                                        ? "NOVIEMBRE"
                                                                        : T.FECHA_MODIFICACION.Value.Month == 12
                                                                            ? "DICIEMBRE"
                                                                            : "-"
                }).ToList();

            return query;
        }

        public List<DetalleTarifa> ObtieneDatosTarifaHm(string fecha)
        {
            var query1 = (from region in _contexto.CAT_REGIONES_TARIFAS
                join T in _contexto.K_TARIFA_HM
                    on region.id_region
                    equals T.ID_REGION
                where T.FECHA_APLICABLE == fecha
                select new DetalleTarifa
                {
                    IdTarifaHm = T.ID_TARIFA_HM,
                    IdRegion = T.ID_REGION,
                    Descripcion = region.Descripcion,
                    MtCargoDemanda = T.MT_CARGO_DEMANDA,
                    MtCargoBase = T.MT_CARGO_BASE,
                    MtCargoIntermedia = T.MT_CARGO_INTERMEDIA,
                    MtCargoPunta = T.MT_CARGO_PUNTA,
                    Promedio = T.PROMEDIO_TARIFA ?? 0.0,
                    Usuario = T.MODIFICADO_POR ?? T.ADICIONADO_POR
                }).ToList();


            var query2 = (from region in _contexto.CAT_REGIONES_TARIFAS
                select new DetalleTarifa
                {
                    IdRegion = region.id_region,
                    Descripcion = region.Descripcion,
                    MtCargoDemanda = 0.00,
                    MtCargoBase = 0.00,
                    MtCargoIntermedia = 0.00,
                    MtCargoPunta = 0.00,
                    Promedio = 0.00,
                    Usuario = ""
                }).ToList();

            var query = (from info in query2
                join tarifas in query1
                    on info.IdRegion equals tarifas.IdRegion into joinedInfoTarifas
                from y1 in joinedInfoTarifas.DefaultIfEmpty()
                select new DetalleTarifa
                {
                    IdTarifaHm = y1 == null ? 0 : y1.IdTarifaHm,
                    IdRegion = info.IdRegion,
                    Descripcion = info.Descripcion,
                    MtCargoDemanda = y1 == null ? 0.00 : y1.MtCargoDemanda,
                    MtCargoBase = y1 == null ? 0.00 : y1.MtCargoBase,
                    MtCargoIntermedia = y1 == null ? 0.00 : y1.MtCargoIntermedia,
                    MtCargoPunta = y1 == null ? 0.00 : y1.MtCargoPunta,
                    Promedio = y1 == null ? 0.00 : y1.Promedio,
                    Usuario = y1 == null ? "" : y1.Usuario
                }).ToList();
            return query;
        }
    }
}


