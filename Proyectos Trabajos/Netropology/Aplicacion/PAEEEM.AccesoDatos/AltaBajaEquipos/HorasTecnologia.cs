using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.AltaBajaEquipos
{
    public class HorasTecnologia
    {

        public ABE_HORAS_TECNOLOGIA Agregar(ABE_HORAS_TECNOLOGIA horaTecnologia)
        {
            ABE_HORAS_TECNOLOGIA trParametros = null;

            using (var r = new Repositorio<ABE_HORAS_TECNOLOGIA>())
            {
                trParametros = r.Extraer(c => c.IDHORAS_TECNOLOGIA == horaTecnologia.IDHORAS_TECNOLOGIA);

                if (trParametros == null)
                    trParametros = r.Agregar(trParametros);
                else
                    throw new Exception("La hora de la tecnologia ya existe en la BD.");
            }

            return trParametros;
        }


        public ABE_HORAS_TECNOLOGIA ObtienePorCondicion(Expression<Func<ABE_HORAS_TECNOLOGIA, bool>> criterio)
        {
            ABE_HORAS_TECNOLOGIA trParametro = null;


            using (var r = new Repositorio<ABE_HORAS_TECNOLOGIA>())
            {
                trParametro = r.Extraer(criterio);
            }

            return trParametro;
        }


        public List<ABE_HORAS_TECNOLOGIA> ObtieneTodos()
        {
            List<ABE_HORAS_TECNOLOGIA> listParametros = null;

            using (var r = new Repositorio<ABE_HORAS_TECNOLOGIA>())
            {
                listParametros = r.Filtro(null);
            }

            return listParametros;
        }


        public bool Actualizar(ABE_HORAS_TECNOLOGIA horaTecnologia)
        {
            bool actualiza = false;

            var abeHoraTec = ObtienePorCondicion(p => p.IDHORAS_TECNOLOGIA == horaTecnologia.IDHORAS_TECNOLOGIA);

            if (horaTecnologia != null)
            {
                using (var r = new Repositorio<ABE_HORAS_TECNOLOGIA>())
                {
                    actualiza = r.Actualizar(horaTecnologia);
                }
            }
            else
            {
                throw new Exception("La hora de la tecnologia con Id: " + abeHoraTec.IDHORAS_TECNOLOGIA + " no fue encontrada.");
            }

            return actualiza;
        }


        public bool Elminar(int idHoraTecnologia)
        {
            bool elimina = false;

            var abeHoraTec = ObtienePorCondicion(p => p.IDHORAS_TECNOLOGIA == idHoraTecnologia);

            if (abeHoraTec != null)
            {
                using (var r = new Repositorio<ABE_HORAS_TECNOLOGIA>())
                {
                    elimina = r.Eliminar(abeHoraTec);
                }
            }
            else
            {
                throw new Exception("No se encontro la hora de la tecnologia indicada.");
            }

            return elimina;
        }


        public List<ABE_HORAS_TECNOLOGIA> FiltraPorCondicion(Expression<Func<ABE_HORAS_TECNOLOGIA, bool>> criterio)
        {
            List<ABE_HORAS_TECNOLOGIA> listHoras = null;

            using (var r = new Repositorio<ABE_HORAS_TECNOLOGIA>())
            {
                listHoras = r.Filtro(criterio);
            }

            return listHoras;
        }

    }
}
