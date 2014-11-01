using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class Mes
    {
        public TR_MES Agregar(TR_MES mes)
        {
            TR_MES trMes = null;

            using (var r = new Repositorio<TR_MES>())
            {
                trMes = r.Extraer(c => c.IDMES == mes.IDMES);

                if (trMes == null)
                    trMes = r.Agregar(mes);
                else
                    throw new Exception("El mes ya existe en la BD.");
            }

            return trMes;
        }


        public TR_MES ObtienePorCondicion(Expression<Func<TR_MES, bool>> criterio)
        {
            TR_MES trMes = null;

            using (var r = new Repositorio<TR_MES>())
            {
                trMes = r.Extraer(criterio);
            }

            return trMes;
        }


        public List<TR_MES> ObtieneTodos()
        {
            List<TR_MES> listMeses = null;

            using (var r = new Repositorio<TR_MES>())
            {
                listMeses = r.Filtro(null);
            }

            return listMeses;
        }


        public bool Actualizar(TR_MES mes)
        {
            bool actualiza = false;

            var trMes = ObtienePorCondicion(c => c.IDMES == mes.IDMES);

            if (trMes != null)
            {
                using (var r = new Repositorio<TR_MES>())
                {
                    actualiza = r.Actualizar(mes);
                }
            }
            else
            {
                throw new Exception("El mes con Id: " + trMes.IDMES + " no fue encontrado.");
            }

            return actualiza;
        }


        public bool Eliminar(int idMes)
        {
            bool elimina = false;

            var trMes = ObtienePorCondicion(c => c.IDMES == idMes);

            if (trMes != null)
            {
                using (var r = new Repositorio<TR_MES>())
                {
                    elimina = r.Eliminar(trMes);
                }
            }
            else
            {
                throw new Exception("No se encontro el mes indicado.");
            }

            return elimina;
        }


        public List<TR_MES> FitrarPorCondicion(Expression<Func<TR_MES, bool>> criterio)
        {
            List<TR_MES> listMeses = null;

            using (var r = new Repositorio<TR_MES>())
            {
                listMeses = r.Filtro(criterio).ToList();
            }

            return listMeses;
        }

    }
}
