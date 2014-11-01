using PAEEEM.AccesoDatos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.Credito
{
    public class blRefCliente
    {

        public static CLI_Ref_Cliente Obtener(int IdReferencia)
        {
            CLI_Ref_Cliente cliente = beRefCliente.Obtener(IdReferencia);

            return cliente;
        }

        public static CLI_Ref_Cliente Obtener(int IdCliente, int IdTipoReferencia)
        {
            CLI_Ref_Cliente cliente = beRefCliente.Obtener(IdCliente, IdTipoReferencia);

            return cliente;
        }

        public static CLI_Ref_Cliente Insertar(CLI_Ref_Cliente refCliente)
        {
            CLI_Ref_Cliente nuevo = null;

            var datos = beRefCliente.Insertar(refCliente);

            if (datos != null)
            {
                nuevo = datos;
            }

            return nuevo;
        }

        public static bool Actualizar(CLI_Ref_Cliente refCliente)
        {
            bool obj;

            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                obj = r.Actualizar(refCliente);
            }

            return obj;
        }

    }
}
