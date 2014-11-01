using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class beCliente
    {

        public static CLI_Cliente Obtener(int IdCliente)
        {
            CLI_Cliente obj = null;

            using (var r = new Repositorio<CLI_Cliente>())
            {
                obj = r.Extraer(m => m.IdCliente == IdCliente);
            }

            return obj;
        }

        public static CLI_Cliente Insertar(CLI_Cliente cliente)
        {
            CLI_Cliente obj;

            using (var r = new Repositorio<CLI_Cliente>())
            {
                obj = r.Agregar(cliente);
            }

            return obj;
        }

        public static bool Actualizar(CLI_Cliente cliente)
        {
            bool obj = false;

            using (var r = new Repositorio<CLI_Cliente>())
            {
                obj = r.Actualizar(cliente);
            }

            return obj;
        }

    }
}
