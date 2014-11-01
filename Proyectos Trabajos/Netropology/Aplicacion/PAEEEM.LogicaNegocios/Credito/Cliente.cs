using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.Credito
{
    public class blCliente
    {

        public static CLI_Cliente Obtener(int IdCliente)
        {
            CLI_Cliente cliente = beCliente.Obtener(IdCliente);

            return cliente;
        }

        public static CLI_Cliente Insertar(CLI_Cliente cliente)
        {
            CLI_Cliente nuevo = null;

            var datos = beCliente.Insertar(cliente);

            if (datos != null)
            {
                nuevo = datos;
            }

            return nuevo;
        }

        public static bool Actualizar(CLI_Cliente cliente)
        {
            bool obj;

            using (var r = new Repositorio<CLI_Cliente>())
            {
                obj = r.Actualizar(cliente);
            }

            return obj;
        }

    }
}
