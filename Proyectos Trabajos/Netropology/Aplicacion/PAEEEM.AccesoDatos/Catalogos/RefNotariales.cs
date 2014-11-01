using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class beRefNotariales
    {

        public static CLI_Referencias_Notariales Obtener(int IdReferencia)
        {
            CLI_Referencias_Notariales obj = null;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                obj = r.Extraer(m => m.IdReferencia_Nota == IdReferencia);
            }

            return obj;
        }

        public static CLI_Referencias_Notariales Obtener(int IdCliente, int IdTipoReferencia)
        {
            CLI_Referencias_Notariales obj = null;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                obj = r.Extraer(m => m.IdCliente == IdCliente && m.IdTipoReferencia == IdTipoReferencia);
            }

            return obj;
        }

        public static CLI_Referencias_Notariales Insertar(CLI_Referencias_Notariales refNotarial)
        {
            CLI_Referencias_Notariales obj;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                obj = r.Agregar(refNotarial);
            }

            return obj;
        }

        public static bool Actualizar(CLI_Referencias_Notariales refNotarial)
        {
            bool obj = false;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                obj = r.Actualizar(refNotarial);
            }

            return obj;
        }

    }
}
