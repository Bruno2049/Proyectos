using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class EstatusUsuario
    {

        public TR_ESTATUS_USUARIO Agregar(TR_ESTATUS_USUARIO estatusUsuario)
        {
            TR_ESTATUS_USUARIO trEstatusUsuario = null;

            using (var r = new Repositorio<TR_ESTATUS_USUARIO>())
            {
                trEstatusUsuario = r.Extraer(c => c.ID_ESTATUS == estatusUsuario.ID_ESTATUS);

                if (trEstatusUsuario == null)
                    trEstatusUsuario = r.Agregar(estatusUsuario);
                else
                    throw new Exception("El estatus del usuario ya existe en la BD.");
            }

            return trEstatusUsuario;
        }


        public TR_ESTATUS_USUARIO ObtienePorCondicion(Expression<Func<TR_ESTATUS_USUARIO, bool>> criterio)
        {
            TR_ESTATUS_USUARIO trEstatusUsuario = null;

            using (var r = new Repositorio<TR_ESTATUS_USUARIO>())
            {
                trEstatusUsuario = r.Extraer(criterio);
            }

            return trEstatusUsuario;
        }


        public List<TR_ESTATUS_USUARIO> ObtieneTodos()
        {
            List<TR_ESTATUS_USUARIO> listEstatusUsuario = null;

            using (var r = new Repositorio<TR_ESTATUS_USUARIO>())
            {
                listEstatusUsuario = r.Filtro(null);
            }

            return listEstatusUsuario;
        }


        public bool Actualizar(TR_ESTATUS_USUARIO estatusUsuario)
        {
            bool actualiza = false;

            var trEstatusUsuario = ObtienePorCondicion(c => c.ID_ESTATUS == estatusUsuario.ID_ESTATUS);

            if (trEstatusUsuario != null)
            {
                using (var r = new Repositorio<TR_ESTATUS_USUARIO>())
                {
                    actualiza = r.Actualizar(estatusUsuario);
                }
            }
            else
            {
                throw new Exception("El estatus de usuario con Id: " + trEstatusUsuario.ID_ESTATUS + " no fue encontrado.");
            }

            return actualiza;
        }


        public bool Eliminar(string idEstatus)
        {
            bool elimina = false;

            var trEstatusUsuario = ObtienePorCondicion(c => c.ID_ESTATUS == idEstatus);

            if (trEstatusUsuario != null)
            {
                using (var r = new Repositorio<TR_ESTATUS_USUARIO>())
                {
                    elimina = r.Eliminar(trEstatusUsuario);
                }
            }
            else
            {
                throw new Exception("No se encontro el estatus del usuario indicado.");
            }

            return elimina;
        }


        public List<TR_ESTATUS_USUARIO> FitrarPorCondicion(Expression<Func<TR_ESTATUS_USUARIO, bool>> criterio)
        {
            List<TR_ESTATUS_USUARIO> listEstatusUsuario = null;

            using (var r = new Repositorio<TR_ESTATUS_USUARIO>())
            {
                listEstatusUsuario = r.Filtro(criterio).ToList();
            }

            return listEstatusUsuario;
        }


    }
}
