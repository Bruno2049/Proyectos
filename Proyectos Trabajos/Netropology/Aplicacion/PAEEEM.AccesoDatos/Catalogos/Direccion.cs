using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class beDireccion
    {

        public static DIR_Direcciones Obtener(int IdDireccion)
        {
            DIR_Direcciones obj = null;

            using (var r = new Repositorio<DIR_Direcciones>())
            {
                obj = r.Extraer(m => m.IdDireccion == IdDireccion);
            }

            return obj;
        }

        public static DIR_Direcciones Obtener(int IdCliente, int IdTipoDomicilio)
        {
            DIR_Direcciones obj = null;

            using (var r = new Repositorio<DIR_Direcciones>())
            {
                obj = r.Extraer(m => m.IdCliente == IdCliente && m.IdTipoDomicilio == IdTipoDomicilio);
            }

            return obj;
        }

        public static DIR_Direcciones Insertar(DIR_Direcciones direccion)
        {
            DIR_Direcciones obj;

            using (var r = new Repositorio<DIR_Direcciones>())
            {
                obj = r.Agregar(direccion);
            }

            return obj;
        }

        public static bool Actualizar(DIR_Direcciones direccion)
        {
            bool obj = false;

            using (var r = new Repositorio<DIR_Direcciones>())
            {
                obj = r.Actualizar(direccion);
            }

            return obj;
        }

    }
}
