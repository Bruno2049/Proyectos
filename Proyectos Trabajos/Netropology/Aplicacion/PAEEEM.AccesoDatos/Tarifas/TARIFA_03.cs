using System;
using System.Linq.Expressions;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Tarifas
{
    public class Tarifa03
    {
        public static K_TARIFA_03 Insertar(K_TARIFA_03 informacion)
        {
            K_TARIFA_03 trInformacion;

            using (var r = new Repositorio<K_TARIFA_03>())
            {
                trInformacion = r.Extraer(c => c.ID_TARIFA_03 == informacion.ID_TARIFA_03);

                if (trInformacion == null)
                    trInformacion = r.Agregar(informacion);
                else
                    throw new Exception("El registro ya existe en la BD.");
            }
            return trInformacion;
        }

        public static K_TARIFA_03 ObtienePorId(int id)
        {
            K_TARIFA_03 trInfoGeneral;

            using (var r = new Repositorio<K_TARIFA_03>())
            {
                trInfoGeneral = r.Extraer(tr => tr.ID_TARIFA_03 == id);
            }
            return trInfoGeneral;
        }

        public static bool Actualizar(K_TARIFA_03 informacion)
        {
            bool actualiza;

            var trInfoGeneral = ObtienePorId(informacion.ID_TARIFA_03);

            if (trInfoGeneral != null)
            {
                using (var r = new Repositorio<K_TARIFA_03>())
                {
                    actualiza = r.Actualizar(informacion);
                }
            }
            else
            {
                throw new Exception("El Id: " + informacion.ID_TARIFA_03 + " no fue encontrado.");
            }

            return actualiza;
        }


        public static K_TARIFA_03 ObtienePorCondicion(Expression<Func<K_TARIFA_03,bool>> criterio)
        {
            K_TARIFA_03 trInfoGeneral;

            using (var r = new Repositorio<K_TARIFA_03>())
            {
                trInfoGeneral = r.Extraer(criterio);
            }
            return trInfoGeneral;
        }
    }
}

