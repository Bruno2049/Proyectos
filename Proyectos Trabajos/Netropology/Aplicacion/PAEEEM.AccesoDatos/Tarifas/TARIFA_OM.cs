using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Tarifas
{
    public class TarifaOm
    {
        public static K_TARIFA_OM Insertar(K_TARIFA_OM informacion)
        {
            K_TARIFA_OM trInformacion;

            using (var r = new Repositorio<K_TARIFA_OM>())
            {
                trInformacion = r.Extraer(c => c.ID_TARIFA_OM == informacion.ID_TARIFA_OM);

                if (trInformacion == null)
                    trInformacion = r.Agregar(informacion);
                else
                    throw new Exception("El registro ya existe en la BD.");
            }
            return trInformacion;
        }

        public static K_TARIFA_OM ObtienePorId(int id)
        {
            K_TARIFA_OM trInfoGeneral;

            using (var r = new Repositorio<K_TARIFA_OM>())
            {
                trInfoGeneral = r.Extraer(tr => tr.ID_TARIFA_OM == id);
            }
            return trInfoGeneral;
        }

        public static bool Actualizar(K_TARIFA_OM informacion)
        {
            bool actualiza;

            var trInfoGeneral = ObtienePorId(informacion.ID_TARIFA_OM);

            if (trInfoGeneral != null)
            {
                using (var r = new Repositorio<K_TARIFA_OM>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("El Id: " + informacion.ID_TARIFA_OM + " no fue encontrado.");
            }

            return actualiza;
        }

        public static K_TARIFA_OM ObtienePorCondicion(Expression<Func<K_TARIFA_OM,bool>> criterio)
        {
            K_TARIFA_OM trInfoGeneral;

            using (var r = new Repositorio<K_TARIFA_OM>())
            {
                trInfoGeneral = r.Extraer(criterio);
            }
            return trInfoGeneral;
        }

        public static List<K_TARIFA_OM> ObtenTarifasPorCondicion(Expression<Func<K_TARIFA_OM, bool>> criterio)
        {
            List<K_TARIFA_OM> lstTarifaOM = null;

            using (var r = new Repositorio<K_TARIFA_OM>())
            {
                lstTarifaOM = r.Filtro(criterio);
            }

            return lstTarifaOM;
        }
    }
}
