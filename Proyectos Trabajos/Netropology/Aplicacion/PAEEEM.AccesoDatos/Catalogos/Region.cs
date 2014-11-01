using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Region
    {
        public CAT_REGION Agregar(CAT_REGION region)
        {
            CAT_REGION catRegion = null;

            using (var r = new Repositorio<CAT_REGION>())
            {
                catRegion = r.Extraer(c => c.Cve_Region == region.Cve_Region);

                if (catRegion == null)
                    catRegion = r.Agregar(region);
                else
                    throw new Exception("La Region ya existe en la BD.");
            }

            return catRegion;
        }


        public CAT_REGION ObtienePorCondicion(Expression<Func<CAT_REGION, bool>> criterio)
        {
            CAT_REGION trTipoZona = null;

            using (var r = new Repositorio<CAT_REGION>())
            {
                trTipoZona = r.Extraer(criterio);
            }

            return trTipoZona;
        }

        public List<TR_TIPO_ZONA> ObtieneTodos()
        {
            List<TR_TIPO_ZONA> listTipoZona = null;

            using (var r = new Repositorio<TR_TIPO_ZONA>())
            {
                listTipoZona = r.Filtro(null);
            }

            return listTipoZona;
        }


        public bool Actualizar(CAT_REGION region)
        {
            bool actualiza = false;

            var catRegion = ObtienePorCondicion(c => c.Cve_Region == region.Cve_Region);

            if (catRegion != null)
            {
                using (var r = new Repositorio<CAT_REGION>())
                {
                    actualiza = r.Actualizar(catRegion);
                }
            }
            else
            {
                throw new Exception("La Region con Id: " + region.Cve_Region + " no fue encontrada.");
            }

            return actualiza;
        }


        public bool Eliminar(int idRegion)
        {
            bool elimina = false;

            var catRegion = ObtienePorCondicion(c => c.Cve_Region == idRegion);

            if (catRegion != null)
            {
                using (var r = new Repositorio<CAT_REGION>())
                {
                    elimina = r.Eliminar(catRegion);
                }
            }
            else
            {
                throw new Exception("No se encontro la region indicada.");
            }

            return elimina;
        }


        public List<CAT_REGION> FitrarPorCondicion(Expression<Func<CAT_REGION, bool>> criterio)
        {
            List<CAT_REGION> listEstado = null;

            using (var r = new Repositorio<CAT_REGION>())
            {
                listEstado = r.Filtro(criterio).ToList();
            }

            return listEstado;
        }


        public static List<CAT_REGION> CatRegion()
        {
            List<CAT_REGION> reg = null;
            using (var r = new Repositorio<CAT_REGION>())
            {
                reg = r.Filtro(me => me.Dx_Nombre_Region != null);
            }
            return reg;
        }

        public static List<CAT_REGION> CatRegionid(int idRegion)
        {
            List<CAT_REGION> reg = null;
            using (var r = new Repositorio<CAT_REGION>())
            {
                reg = r.Filtro(me => me.Cve_Region == idRegion);
            }
            return reg;
        }

    }
}
