using PAEEEM.AccesoDatos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.Credito
{
    public class blRefNotariales
    {

        public static CLI_Referencias_Notariales Obtener(int IdReferencia)
        {
            CLI_Referencias_Notariales cliente = beRefNotariales.Obtener(IdReferencia);

            return cliente;
        }

        public static CLI_Referencias_Notariales Obtener(int IdCliente, int IdTipoReferencia)
        {
            CLI_Referencias_Notariales cliente = beRefNotariales.Obtener(IdCliente, IdTipoReferencia);

            return cliente;
        }

        public static CLI_Referencias_Notariales Insertar(CLI_Referencias_Notariales refNotarial)
        {
            CLI_Referencias_Notariales nuevo = null;

            var datos = beRefNotariales.Insertar(refNotarial);

            if (datos != null)
            {
                nuevo = datos;
            }

            return nuevo;
        }

        public static bool Actualizar(CLI_Referencias_Notariales refNotarial)
        {
            bool obj;

            using (var r = new Repositorio<CLI_Referencias_Notariales>())
            {
                obj = r.Actualizar(refNotarial);
            }

            return obj;
        }

    }
}
