using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Tarifas
{
    public class Tarifa02
    {
        public static K_TARIFA_02 Insertar(K_TARIFA_02 informacion)
        {
            K_TARIFA_02 trInformacion;

            using (var r = new Repositorio<K_TARIFA_02>())
            {
                trInformacion = r.Extraer(c => c.ID_TARIFA_02 == informacion.ID_TARIFA_02);

                if (trInformacion == null)
                    trInformacion = r.Agregar(informacion);
                else
                    throw new Exception("El registro ya existe en la BD.");
            }
            return trInformacion;
        }

        public static K_TARIFA_02 ObtienePorId(int id)
        {
            K_TARIFA_02 trInfoGeneral;

            using (var r = new Repositorio<K_TARIFA_02>())
            {
                trInfoGeneral = r.Extraer(tr => tr.ID_TARIFA_02 == id);
            }
            return trInfoGeneral;
        }

        public static bool Actualizar(K_TARIFA_02 informacion)
        {
            bool actualiza;

            var trInfoGeneral = ObtienePorId(informacion.ID_TARIFA_02);

            if (trInfoGeneral != null)
            {
                using (var r = new Repositorio<K_TARIFA_02>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("El Id: " + informacion.ID_TARIFA_02 + " no fue encontrado.");
            }

            return actualiza;
        }


        public static K_TARIFA_02 ObtienePorCondicion(Expression<Func<K_TARIFA_02,bool>> criterio)
        {

            var tarifa02 = new K_TARIFA_02();

            using (var r = new Repositorio<K_TARIFA_02>())
            {
                tarifa02 = r.Extraer(criterio);
            }

            return tarifa02;
        }
    }
}
