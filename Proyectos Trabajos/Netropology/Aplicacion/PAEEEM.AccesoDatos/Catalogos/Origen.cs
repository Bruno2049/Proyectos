using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Origen
    {

        public TR_ORIGEN Agregar(TR_ORIGEN origen)
        {
            TR_ORIGEN catOrigen = null;

            using (var r = new Repositorio<TR_ORIGEN>())
            {
                catOrigen = r.Extraer(c => c.ID_VALOR == origen.ID_VALOR);

                if (catOrigen == null)
                    catOrigen = r.Agregar(origen);
                else
                    throw new Exception("El origen ya existe en la BD.");
            }

            return catOrigen;
        }


        public TR_ORIGEN ObtienePorCondicion(Expression<Func<TR_ORIGEN, bool>> criterio)
        {
            TR_ORIGEN trOrigen = null;

            using (var r = new Repositorio<TR_ORIGEN>())
            {
                trOrigen = r.Extraer(criterio);
            }

            return trOrigen;
        }

        public List<TR_ORIGEN> ObtieneTodos()
        {
            List<TR_ORIGEN> listTipoZona = null;

            using (var r = new Repositorio<TR_ORIGEN>())
            {
                listTipoZona = r.Filtro(null);
            }

            return listTipoZona;
        }

        public bool Actualizar(TR_ORIGEN origen)
        {
            bool actualiza = false;

            var trOrigen = ObtienePorCondicion(c => c.ID_VALOR == origen.ID_VALOR);

            if (trOrigen != null)
            {
                using (var r = new Repositorio<TR_ORIGEN>())
                {
                    actualiza = r.Actualizar(trOrigen);
                }
            }
            else
            {
                throw new Exception("El Origen con Id: " + origen.ID_VALOR + " no fue encontrado.");
            }

            return actualiza;
        }

        public bool Eliminar(int idOrigen)
        {
            bool elimina = false;

            var trOrigen = ObtienePorCondicion(c => c.ID_VALOR == idOrigen);

            if (trOrigen != null)
            {
                using (var r = new Repositorio<TR_ORIGEN>())
                {
                    elimina = r.Eliminar(trOrigen);
                }
            }
            else
            {
                throw new Exception("No se encontro el origen indicado.");
            }

            return elimina;
        }


        public List<TR_ORIGEN> FitrarPorCondicion(Expression<Func<TR_ORIGEN, bool>> criterio)
        {
            List<TR_ORIGEN> listEstado = null;

            using (var r = new Repositorio<TR_ORIGEN>())
            {
                listEstado = r.Filtro(criterio).ToList();
            }

            return listEstado;
        }

    }
}
