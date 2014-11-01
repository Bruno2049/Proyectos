using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Estado
    {

        public CAT_ESTADO Agregar(CAT_ESTADO estado)
        {
            CAT_ESTADO catEstado = null;

            using (var r = new Repositorio<CAT_ESTADO>())
            {
                catEstado = r.Extraer(c => c.Cve_Estado == estado.Cve_Estado);

                if (catEstado == null)
                    catEstado = r.Agregar(estado);
                else
                    throw new Exception("El estado ya existe en la BD.");
            }

            return catEstado;
        }


        public CAT_ESTADO ObtienePorCondicion(Expression<Func<CAT_ESTADO,bool>> criterio)
        {
            CAT_ESTADO catEstado = null;

            using (var r = new Repositorio<CAT_ESTADO>())
            {
                catEstado = r.Extraer(criterio);
            }

            return catEstado;
        }


        public List<CAT_ESTADO> ObtieneTodos()
        {
            List<CAT_ESTADO> listCatEstado = null;

            using (var r = new Repositorio<CAT_ESTADO>())
            {
                listCatEstado = r.Filtro(null);
            }

            return listCatEstado;
        }


        public bool Actualizar(CAT_ESTADO estado)
        {
            bool actualiza = false;

            var catEstado = ObtienePorCondicion(c => c.Cve_Estado == estado.Cve_Estado);

            if (catEstado != null)
            {
                using (var r = new Repositorio<CAT_ESTADO>())
                {
                    actualiza = r.Actualizar(catEstado);
                }
            }
            else
            {
                throw new Exception("El estado con Id: " + catEstado.Cve_Estado + " no fue encontrado.");
            }

            return actualiza;
        }


        public bool Eliminar(int idEstado)
        {
            bool elimina = false;

            var catEstado = ObtienePorCondicion(c => c.Cve_Estado == idEstado);

            if (catEstado != null)
            {
                using (var r = new Repositorio<CAT_ESTADO>())
                {
                    elimina = r.Eliminar(catEstado);
                }
            }
            else
            {
                throw new Exception("No se encontro el Estado indicado.");
            }

            return elimina;
        }


        public List<CAT_ESTADO> FitrarPorCondicion(Expression<Func<CAT_ESTADO, bool>> criterio)
        {
            List<CAT_ESTADO> listEstado = null;

            using (var r = new Repositorio<CAT_ESTADO>())
            {
                listEstado = r.Filtro(criterio).ToList();
            }

            return listEstado;
        }



    }
}
