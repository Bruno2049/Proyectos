using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class ComunicacionSicom
    {
        public TR_COMUNICACION_SICOM Agregar(TR_COMUNICACION_SICOM comunicacionSicom)
        {
            TR_COMUNICACION_SICOM trCmnSicom = null;

            using (var r = new Repositorio<TR_COMUNICACION_SICOM>())
            {
                trCmnSicom = r.Extraer(c => c.ID_ESTATUS_COM == comunicacionSicom.ID_ESTATUS_COM);

                if (trCmnSicom == null)
                    trCmnSicom = r.Agregar(comunicacionSicom);
                else
                    throw new Exception("Comunicacion Sicom ya existe en la BD.");
            }

            return trCmnSicom;
        }


        public TR_COMUNICACION_SICOM ObtienePorCondicion(Expression<Func<TR_COMUNICACION_SICOM, bool>> criterio)
        {
            TR_COMUNICACION_SICOM trTipoZona = null;

            using (var r = new Repositorio<TR_COMUNICACION_SICOM>())
            {
                trTipoZona = r.Extraer(criterio);
            }

            return trTipoZona;
        }


        public List<TR_COMUNICACION_SICOM> ObtieneTodos()
        {
            List<TR_COMUNICACION_SICOM> listCmnSicom = null;

            using (var r = new Repositorio<TR_COMUNICACION_SICOM>())
            {
                listCmnSicom = r.Filtro(null);
            }

            return listCmnSicom;
        }


        public bool Actualizar(TR_COMUNICACION_SICOM comunicacionSicom)
        {
            bool actualiza = false;

            var trCmnSicom = ObtienePorCondicion(c => c.ID_ESTATUS_COM == comunicacionSicom.ID_ESTATUS_COM);

            if (trCmnSicom != null)
            {
                using (var r = new Repositorio<TR_COMUNICACION_SICOM>())
                {
                    actualiza = r.Actualizar(trCmnSicom);
                }
            }
            else
            {
                throw new Exception("Comunicacion Sicom con Id: " + comunicacionSicom.ID_ESTATUS_COM + " no fue encontrada.");
            }

            return actualiza;
        }

        public bool Eliminar(int idEstatus)
        {
            bool elimina = false;

            var trCmnSicom = ObtienePorCondicion(c => c.ID_ESTATUS_COM == idEstatus);

            if (trCmnSicom != null)
            {
                using (var r = new Repositorio<TR_COMUNICACION_SICOM>())
                {
                    elimina = r.Eliminar(trCmnSicom);
                }
            }
            else
            {
                throw new Exception("No se encontro la comunicacion sicom indicada.");
            }

            return elimina;
        }


        public List<TR_COMUNICACION_SICOM> FitrarPorCondicion(Expression<Func<TR_COMUNICACION_SICOM, bool>> criterio)
        {
            List<TR_COMUNICACION_SICOM> listCmnSicom = null;

            using (var r = new Repositorio<TR_COMUNICACION_SICOM>())
            {
                listCmnSicom = r.Filtro(criterio).ToList();
            }

            return listCmnSicom;
        }

    }
}
