using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class CapacidadSustitucion
    {

        public CAT_CAPACIDAD_SUSTITUCION Agregar(CAT_CAPACIDAD_SUSTITUCION capacidadSustitucion)
        {
            CAT_CAPACIDAD_SUSTITUCION catCapacidad = null;

            using (var r = new Repositorio<CAT_CAPACIDAD_SUSTITUCION>())
            {
                catCapacidad = r.Extraer(c => c.Cve_Capacidad_Sust == capacidadSustitucion.Cve_Capacidad_Sust);

                if (catCapacidad == null)
                    catCapacidad = r.Agregar(capacidadSustitucion);
                else
                    throw new Exception("La capacidad de sustición ya existe en la BD.");
            }

            return catCapacidad;
        }


        public CAT_CAPACIDAD_SUSTITUCION ObtienePorCondicion(Expression<Func<CAT_CAPACIDAD_SUSTITUCION, bool>> criterio)
        {
            CAT_CAPACIDAD_SUSTITUCION catCapacidad = null;

            using (var r = new Repositorio<CAT_CAPACIDAD_SUSTITUCION>())
            {
                catCapacidad = r.Extraer(criterio);
            }

            return catCapacidad;
        }


        public List<CAT_CAPACIDAD_SUSTITUCION> ObtieneTodos()
        {
            List<CAT_CAPACIDAD_SUSTITUCION> listCapacidad = null;

            using (var r = new Repositorio<CAT_CAPACIDAD_SUSTITUCION>())
            {
                listCapacidad = r.Filtro(null);
            }

            return listCapacidad;
        }


        public bool Actualizar(CAT_CAPACIDAD_SUSTITUCION capacidadSustitucion)
        {
            bool actualiza = false;

            var catCapacidadSust = ObtienePorCondicion(c => c.Cve_Capacidad_Sust == capacidadSustitucion.Cve_Capacidad_Sust);

            if (catCapacidadSust != null)
            {
                using (var r = new Repositorio<CAT_CAPACIDAD_SUSTITUCION>())
                {
                    actualiza = r.Actualizar(catCapacidadSust);
                }
            }
            else
            {
                throw new Exception("La capacidad de sustición con Id: " + catCapacidadSust.Cve_Capacidad_Sust + " no fue encontrada.");
            }

            return actualiza;
        }


        public bool Eliminar(int idCapacidad)
        {
            bool elimina = false;

            var catCapacidadSust = ObtienePorCondicion(c => c.Cve_Capacidad_Sust == idCapacidad);

            if (catCapacidadSust != null)
            {
                using (var r = new Repositorio<CAT_CAPACIDAD_SUSTITUCION>())
                {
                    elimina = r.Eliminar(catCapacidadSust);
                }
            }
            else
            {
                throw new Exception("No se encontro la capacidad de sustición indicada.");
            }

            return elimina;
        }


        public List<CAT_CAPACIDAD_SUSTITUCION> FitrarPorCondicion(Expression<Func<CAT_CAPACIDAD_SUSTITUCION, bool>> criterio)
        {
            List<CAT_CAPACIDAD_SUSTITUCION> listCapacidad = null;

            using (var r = new Repositorio<CAT_CAPACIDAD_SUSTITUCION>())
            {
                listCapacidad = r.Filtro(criterio).ToList();
            }

            return listCapacidad;
        }


    }
}
