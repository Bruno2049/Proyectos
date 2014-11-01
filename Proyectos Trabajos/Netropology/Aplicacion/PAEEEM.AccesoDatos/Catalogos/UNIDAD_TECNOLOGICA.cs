using PAEEEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class UNIDAD_TECNOLOGICA
    {
        public ABE_UNIDAD_TECNOLOGIA Agregar(ABE_UNIDAD_TECNOLOGIA unidadTecnologica)
        {
            ABE_UNIDAD_TECNOLOGIA catUnidadTecnologica = null;

            using (var r = new Repositorio<ABE_UNIDAD_TECNOLOGIA>())
            {
                catUnidadTecnologica = r.Extraer(c => c.IDUNIDAD == unidadTecnologica.IDUNIDAD);

                if (catUnidadTecnologica == null)
                    catUnidadTecnologica = r.Agregar(unidadTecnologica);
                else
                    throw new Exception("La unidad tecnologica ya existe en la BD.");
            }

            return catUnidadTecnologica;
        }


        public ABE_UNIDAD_TECNOLOGIA ObtienePorCondicion(Expression<Func<ABE_UNIDAD_TECNOLOGIA, bool>> criterio)
        {
            ABE_UNIDAD_TECNOLOGIA catUnidadTecnologica = null;

            using (var r = new Repositorio<ABE_UNIDAD_TECNOLOGIA>())
            {
                catUnidadTecnologica = r.Extraer(criterio);
            }

            return catUnidadTecnologica;
        }


        public List<ABE_UNIDAD_TECNOLOGIA> ObtieneTodos()
        {
            List<ABE_UNIDAD_TECNOLOGIA> listUnidadTecnologica = null;

            using (var r = new Repositorio<ABE_UNIDAD_TECNOLOGIA>())
            {
                listUnidadTecnologica = r.Filtro(null);
            }

            return listUnidadTecnologica;
        }


        public bool Actualizar(ABE_UNIDAD_TECNOLOGIA unidadTecnologica)
        {
            bool actualiza = false;

            var catUnidadTecnologica = ObtienePorCondicion(c => c.IDUNIDAD == unidadTecnologica.IDUNIDAD);

            if (catUnidadTecnologica != null)
            {
                using (var r = new Repositorio<ABE_UNIDAD_TECNOLOGIA>())
                {
                    actualiza = r.Actualizar(catUnidadTecnologica);
                }
            }
            else
            {
                throw new Exception("La unidad tecnologica con Id: " + catUnidadTecnologica.IDUNIDAD + " no fue encontrado.");
            }

            return actualiza;
        }


        public bool Eliminar(int idUnidadTecnologica)
        {
            bool elimina = false;

            var catUnidadTecnologica = ObtienePorCondicion(c => c.IDUNIDAD == idUnidadTecnologica);

            if (catUnidadTecnologica != null)
            {
                using (var r = new Repositorio<ABE_UNIDAD_TECNOLOGIA>())
                {
                    elimina = r.Eliminar(catUnidadTecnologica);
                }
            }
            else
            {
                throw new Exception("No se encontro el producto indicado.");
            }

            return elimina;
        }


        public List<ABE_UNIDAD_TECNOLOGIA> FitrarPorCondicion(Expression<Func<ABE_UNIDAD_TECNOLOGIA, bool>> criterio)
        {
            List<ABE_UNIDAD_TECNOLOGIA> listUnidadTecnologica = null;

            using (var r = new Repositorio<ABE_UNIDAD_TECNOLOGIA>())
            {
                listUnidadTecnologica = r.Filtro(criterio).ToList();
            }

            return listUnidadTecnologica;
        }
    }
}
