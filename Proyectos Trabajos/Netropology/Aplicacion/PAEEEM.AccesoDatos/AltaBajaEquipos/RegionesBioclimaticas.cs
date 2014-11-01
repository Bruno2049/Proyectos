using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.AltaBajaEquipos
{
    public class RegionesBioclimaticas
    {

        public ABE_REGIONES_BIOCLIMATICAS Agregar(ABE_REGIONES_BIOCLIMATICAS bioclima)
        {
            ABE_REGIONES_BIOCLIMATICAS abeRegBioclima = null;

            using (var r = new Repositorio<ABE_REGIONES_BIOCLIMATICAS>())
            {
                abeRegBioclima = r.Extraer(c => c.IDREGION_BIOCLIMA ==  bioclima.IDREGION_BIOCLIMA);

                if (abeRegBioclima == null)
                    abeRegBioclima = r.Agregar(abeRegBioclima);
                else
                    throw new Exception("La region bioclimatica ya existe en la BD.");
            }

            return abeRegBioclima;
        }


        public ABE_REGIONES_BIOCLIMATICAS ObtienePorCondicion(Expression<Func<ABE_REGIONES_BIOCLIMATICAS, bool>> criterio)
        {
            ABE_REGIONES_BIOCLIMATICAS abeRegBioclima = null;


            using (var r = new Repositorio<ABE_REGIONES_BIOCLIMATICAS>())
            {
                abeRegBioclima = r.Extraer(criterio);
            }

            return abeRegBioclima;
        }

        public FACTOR_DEGRADACION ObtenFactorDegradacion(Expression<Func<FACTOR_DEGRADACION, bool>> criterio)
        {
            using (var r = new Repositorio<FACTOR_DEGRADACION>())
            {
                return r.Extraer(criterio);
            }
        }

        public List<ABE_REGIONES_BIOCLIMATICAS> ObtieneTodos()
        {
            List<ABE_REGIONES_BIOCLIMATICAS> regionesBioclima = null;

            using (var r = new Repositorio<ABE_REGIONES_BIOCLIMATICAS>())
            {
                regionesBioclima = r.Filtro(null);
            }

            return regionesBioclima;
        }


        public bool Actualizar(ABE_REGIONES_BIOCLIMATICAS bioclima)
        {
            bool actualiza = false;

            var abeRBioclima = ObtienePorCondicion(p => p.IDREGION_BIOCLIMA == bioclima.IDREGION_BIOCLIMA);

            if (bioclima != null)
            {
                using (var r = new Repositorio<ABE_REGIONES_BIOCLIMATICAS>())
                {
                    actualiza = r.Actualizar(bioclima);
                }
            }
            else
            {
                throw new Exception("La región bioclimatica  con Id: " + abeRBioclima.IDREGION_BIOCLIMA + " no fue encontrada.");
            }

            return actualiza;
        }


        public bool Elminar(int idRegionBioclima)
        {
            bool elimina = false;

            var abeRegBioclima = ObtienePorCondicion(p => p.IDREGION_BIOCLIMA == idRegionBioclima);

            if (abeRegBioclima != null)
            {
                using (var r = new Repositorio<ABE_REGIONES_BIOCLIMATICAS>())
                {
                    elimina = r.Eliminar(abeRegBioclima);
                }
            }
            else
            {
                throw new Exception("No se encontro la región bioclimatica indicada.");
            }

            return elimina;
        }


        public List<ABE_REGIONES_BIOCLIMATICAS> FiltraPorCondicion(Expression<Func<ABE_REGIONES_BIOCLIMATICAS, bool>> criterio)
        {
            List<ABE_REGIONES_BIOCLIMATICAS> listRegiones = null;

            using (var r = new Repositorio<ABE_REGIONES_BIOCLIMATICAS>())
            {
                listRegiones = r.Filtro(criterio);
            }

            return listRegiones;
        } 


        

    }
}
