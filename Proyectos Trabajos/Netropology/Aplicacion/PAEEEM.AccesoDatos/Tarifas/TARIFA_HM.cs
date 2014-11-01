using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Tarifas
{
    public  class TarifaHm
    {
        public static K_TARIFA_HM Insertar(K_TARIFA_HM informacion)
        {
            K_TARIFA_HM trInformacion;

            using (var r = new Repositorio<K_TARIFA_HM>())
            {
                trInformacion = r.Extraer(c => c.ID_TARIFA_HM == informacion.ID_TARIFA_HM);

                if (trInformacion == null)
                    trInformacion = r.Agregar(informacion);
                else
                    throw new Exception("El registro ya existe en la BD.");
            }
            return trInformacion;
        }

        public static K_TARIFA_HM ObtienePorId(int id)
        {
            K_TARIFA_HM trInfoGeneral;

            using (var r = new Repositorio<K_TARIFA_HM>())
            {
                trInfoGeneral = r.Extraer(tr => tr.ID_TARIFA_HM == id);
            }
            return trInfoGeneral;
        }

        public static bool Actualizar(K_TARIFA_HM informacion)
        {
            bool actualiza;

            var trInfoGeneral = ObtienePorId(informacion.ID_TARIFA_HM);

            if (trInfoGeneral != null)
            {
                using (var r = new Repositorio<K_TARIFA_HM>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("El Id: " + informacion.ID_TARIFA_HM + " no fue encontrado.");
            }

            return actualiza;
        }

        public static K_TARIFA_HM ObtienePorCondicion(Expression<Func<K_TARIFA_HM, bool>> criterio)
        {
            K_TARIFA_HM trInfoGeneral;

            using (var r = new Repositorio<K_TARIFA_HM>())
            {
                trInfoGeneral = r.Extraer(criterio);
            }
            return trInfoGeneral;
        }

        public static List<K_TARIFA_HM> ObtenTarifasPorCondicion(Expression<Func<K_TARIFA_HM, bool>> criterio)
        {
            List<K_TARIFA_HM> lstTarifaHM = null;

            using (var r = new Repositorio<K_TARIFA_HM>())
            {
                lstTarifaHM = r.Filtro(criterio);
            }

            return lstTarifaHM;
        }
    }
}
