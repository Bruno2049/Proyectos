using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;


namespace PAEEEM.AccesoDatos.Tarifas
{
    public class FactorDegradacionDatos
    {
        private static readonly FactorDegradacionDatos _classInstance = new FactorDegradacionDatos();

        public static FactorDegradacionDatos ClassInstance
        {
            get { return _classInstance; }
        }

        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public FactorDegradacionDatos()
        {
        }

        public object IrPorDatos(int region, int tecnologia)
        {
            var datos = (from T in _contexto.CAT_TECNOLOGIA
                join fd in _contexto.FACTOR_DEGRADACION on T.Cve_Tecnologia equals fd.Cve_Tecnologia
                join m in _contexto.CAT_DELEG_MUNICIPIO on fd.Cve_Deleg_Municipio equals m.Cve_Deleg_Municipio
                join r in _contexto.ABE_REGIONES_BIOCLIMATICAS on m.IDREGION_BIOCLIMA equals r.IDREGION_BIOCLIMA
                where r.IDREGION_BIOCLIMA == (region == 0 ? r.IDREGION_BIOCLIMA : region)
                      && T.Cve_Tecnologia == (tecnologia == 0 ? T.Cve_Tecnologia : tecnologia)
                group new {FD = fd, M = m, T, R = r} by
                    new
                    {
                        m.IDREGION_BIOCLIMA,
                        r.REGION,
                        T.Cve_Tecnologia,
                        T.Dx_Nombre_General,
                        fd.MODIFICADO_POR,
                        fd.FACTOR_DEGRADACION1,
                        fd.ADICIONADO_POR
                    }
                into p
                select new //CAT_Tecnologia_FD
                {
                    count = p.Count(),
                    p.Key.IDREGION_BIOCLIMA,
                    p.Key.REGION,
                    p.Key.Cve_Tecnologia,
                    p.Key.Dx_Nombre_General,
                    p.Key.MODIFICADO_POR,
                    p.Key.FACTOR_DEGRADACION1,
                    p.Key.ADICIONADO_POR
                }).ToList();

            return datos.OrderBy(m => (m.IDREGION_BIOCLIMA.ToString() + m.Cve_Tecnologia)).ToList();

        }

        public List<CAT_DELEG_MUNICIPIO> TraeMunicipios(Expression<Func<CAT_DELEG_MUNICIPIO, bool>> region)
        {
            List<CAT_DELEG_MUNICIPIO> verifica;

            using (var r = new Repositorio<CAT_DELEG_MUNICIPIO>())
            {
                verifica = r.Filtro(region);
            }

            return verifica;
        }

        public List<FACTOR_DEGRADACION> TraeFd(Expression<Func<FACTOR_DEGRADACION, bool>> region)
        {
            List<FACTOR_DEGRADACION> verifica;

            using (var r = new Repositorio<FACTOR_DEGRADACION>())
            {
                verifica = r.Filtro(region);
            }

            return verifica;
        }

        public bool Actualiza_FD(decimal factorDegradacion, int idregionBioclima, int cveTecnologia, string usuario)
        {

            var a = false;

            var filtros = (from p in _contexto.FACTOR_DEGRADACION
                          join A in _contexto.CAT_DELEG_MUNICIPIO on p.Cve_Deleg_Municipio equals A.Cve_Deleg_Municipio
                          where p.Cve_Tecnologia == cveTecnologia && A.IDREGION_BIOCLIMA == idregionBioclima
                          select p).ToList();

            foreach (var item in filtros)
            {
                item.FACTOR_DEGRADACION1 = factorDegradacion;
                item.MODIFICADO_POR = usuario;
                item.FECHA_MODIFICACION = DateTime.Today.Date;

                a = Actualizar(item);
            }
            return a;
        }

        public static bool Actualizar(FACTOR_DEGRADACION informacion)
        {
            bool actualiza;

            var trInfoGeneral = ObtienePorId(informacion.Cve_Deleg_Municipio,informacion.Cve_Tecnologia);

            if (trInfoGeneral == null) return false;
            trInfoGeneral.FACTOR_DEGRADACION1 = informacion.FACTOR_DEGRADACION1;
            trInfoGeneral.MODIFICADO_POR = informacion.MODIFICADO_POR;
            trInfoGeneral.FECHA_MODIFICACION = DateTime.Today.Date;

            using (var r = new Repositorio<FACTOR_DEGRADACION>())
            {
                actualiza = r.Actualizar(trInfoGeneral);
            }
            return actualiza;
        }

        public static FACTOR_DEGRADACION ObtienePorId(int cveDelegMunicipio, int cveTecnologia)
        {
            FACTOR_DEGRADACION trInfoGeneral;

            using (var r = new Repositorio<FACTOR_DEGRADACION>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Cve_Deleg_Municipio == cveDelegMunicipio && tr.Cve_Tecnologia == cveTecnologia);
            }
            return trInfoGeneral;
        }

    }
}
