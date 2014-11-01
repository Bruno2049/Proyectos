using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.AltaBajaEquipos
{
    public class RegionesMunFactBio
    {
        public ABE_REGIONES_MUNICIPIO_FACT_BIO Agregar(ABE_REGIONES_MUNICIPIO_FACT_BIO horaTecnologia)
        {
            ABE_REGIONES_MUNICIPIO_FACT_BIO regionesBioMunicipio = null;

            using (var r = new Repositorio<ABE_REGIONES_MUNICIPIO_FACT_BIO>())
            {
                regionesBioMunicipio = r.Extraer(c => c.IDREGION_MUNICIPIO == horaTecnologia.FACTOR_BIOCLIMATICO);

                if (regionesBioMunicipio == null)
                    regionesBioMunicipio = r.Agregar(regionesBioMunicipio);
                else
                    throw new Exception("El factor bioclimatico del municipio ya existe en la BD.");
            }

            return regionesBioMunicipio;
        }


        public ABE_REGIONES_MUNICIPIO_FACT_BIO ObtienePorCondicion(Expression<Func<ABE_REGIONES_MUNICIPIO_FACT_BIO, bool>> criterio)
        {
            ABE_REGIONES_MUNICIPIO_FACT_BIO factoBioclimatico = null;

            using (var r = new Repositorio<ABE_REGIONES_MUNICIPIO_FACT_BIO>())
            {
                factoBioclimatico = r.Extraer(criterio);
            }

            return factoBioclimatico;
        }

        //public CAT_DELEG_MUNICIPIO ObtienePorCondicion


        public List<ABE_REGIONES_MUNICIPIO_FACT_BIO> ObtieneTodos()
        {
            List<ABE_REGIONES_MUNICIPIO_FACT_BIO> listFactorBioclimatico = null;

            using (var r = new Repositorio<ABE_REGIONES_MUNICIPIO_FACT_BIO>())
            {
                listFactorBioclimatico = r.Filtro(null);
            }

            return listFactorBioclimatico;
        }


        public bool Actualizar(ABE_REGIONES_MUNICIPIO_FACT_BIO regionesBioMunicipio)
        {
            bool actualiza = false;

            var regionesBioMun = ObtienePorCondicion(p => p.IDREGION_MUNICIPIO == regionesBioMunicipio.IDREGION_MUNICIPIO);

            if (regionesBioMunicipio != null)
            {
                using (var r = new Repositorio<ABE_REGIONES_MUNICIPIO_FACT_BIO>())
                {
                    actualiza = r.Actualizar(regionesBioMunicipio);
                }
            }
            else
            {
                throw new Exception("El factor bioclimatico del municipio con Id: " + regionesBioMun.IDREGION_MUNICIPIO + " no fue encontrado.");
            }

            return actualiza;
        }


        public bool Elminar(int idFactorBioclimatico)
        {
            bool elimina = false;

            var factorBioclimatico = ObtienePorCondicion(p => p.IDREGION_MUNICIPIO == idFactorBioclimatico);

            if (factorBioclimatico != null)
            {
                using (var r = new Repositorio<ABE_REGIONES_MUNICIPIO_FACT_BIO>())
                {
                    elimina = r.Eliminar(factorBioclimatico);
                }
            }
            else
            {
                throw new Exception("No se encontro el factor bioclimatico indicado.");
            }

            return elimina;
        }


        public List<ABE_REGIONES_MUNICIPIO_FACT_BIO> FiltraPorCondicion(Expression<Func<ABE_REGIONES_MUNICIPIO_FACT_BIO, bool>> criterio)
        {
            List<ABE_REGIONES_MUNICIPIO_FACT_BIO> listHoras = null;

            using (var r = new Repositorio<ABE_REGIONES_MUNICIPIO_FACT_BIO>())
            {
                listHoras = r.Filtro(criterio);
            }

            return listHoras;
        }
        

    }
}
