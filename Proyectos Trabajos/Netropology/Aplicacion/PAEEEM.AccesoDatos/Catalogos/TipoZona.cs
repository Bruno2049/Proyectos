using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class TipoZona
    {

        public TR_TIPO_ZONA Agregar(TR_TIPO_ZONA tipoZona)
        {
            TR_TIPO_ZONA trTipoZona = null;

            using (var r = new Repositorio<TR_TIPO_ZONA>())
            {
                trTipoZona = r.Extraer(c => c.IDZONA == tipoZona.IDZONA);

                if (trTipoZona == null)
                    trTipoZona = r.Agregar(tipoZona);
                else
                    throw new Exception("El tipo de zona ya existe en la BD.");
            }

            return trTipoZona;
        }


        public TR_TIPO_ZONA ObtienePorCondicion(Expression<Func<TR_TIPO_ZONA, bool>> criterio)
        {
            TR_TIPO_ZONA trTipoZona = null;

            using (var r = new Repositorio<TR_TIPO_ZONA>())
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

        public bool Actualizar(TR_TIPO_ZONA tipoZona)
        {
            bool actualiza = false;

            var catEstado = ObtienePorCondicion(c => c.IDZONA == tipoZona.IDZONA);

            if (catEstado != null)
            {
                using (var r = new Repositorio<TR_TIPO_ZONA>())
                {
                    actualiza = r.Actualizar(catEstado);
                }
            }
            else
            {
                throw new Exception("El Tipo de Zona con Id: " + tipoZona.IDZONA + " no fue encontrado.");
            }

            return actualiza;
        }


        public bool Eliminar(int idZona)
        {
            bool elimina = false;

            var trTipoZona = ObtienePorCondicion(c => c.IDZONA == idZona);

            if (trTipoZona != null)
            {
                using (var r = new Repositorio<TR_TIPO_ZONA>())
                {
                    elimina = r.Eliminar(trTipoZona);
                }
            }
            else
            {
                throw new Exception("No se encontro el tipo de zona indicado.");
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
