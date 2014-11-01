using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class beRefCliente
    {

        public static CLI_Ref_Cliente Obtener(int IdReferencia)
        {
            CLI_Ref_Cliente obj = null;

            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                obj = r.Extraer(m => m.IdReferencia == IdReferencia);
            }

            return obj;
        }
        
        public static CLI_Ref_Cliente Obtener(int IdCliente, int IdTipoReferencia)
        {
            CLI_Ref_Cliente obj = null;

            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                obj = r.Extraer(m => m.IdCliente == IdCliente && m.IdTipoReferencia == IdTipoReferencia);
            }

            return obj;
        }

        public static CLI_Ref_Cliente Insertar(CLI_Ref_Cliente refCliente)
        {
            CLI_Ref_Cliente obj;

            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                obj = r.Agregar(refCliente);
            }

            return obj;
        }

        public static bool Actualizar(CLI_Ref_Cliente refCliente)
        {
            bool obj = false;

            using (var r = new Repositorio<CLI_Ref_Cliente>())
            {
                obj = r.Actualizar(refCliente);
            }

            return obj;
        }

    }
}
