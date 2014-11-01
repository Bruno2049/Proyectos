using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos;
using PAEEEM.AccesoDatos.Catalogos;
using PAEEEM.Entidades;

namespace PAEEEM.LogicaNegocios.Credito
{
    public class blDireccion
    {

        public static DIR_Direcciones Obtener(int IdDireccion)
        {
            DIR_Direcciones cliente = beDireccion.Obtener(IdDireccion);

            return cliente;
        }

        public static DIR_Direcciones Obtener(int IdDireccion, int IdTipoDomicilio)
        {
            DIR_Direcciones cliente = beDireccion.Obtener(IdDireccion,IdTipoDomicilio);

            return cliente;
        }

        public static DIR_Direcciones Insertar(DIR_Direcciones direccion)
        {
            DIR_Direcciones nuevo = null;

            var datos = beDireccion.Insertar(direccion);

            if (datos != null)
            {
                nuevo = datos;
            }

            return nuevo;
        }

        public static bool Actualizar(DIR_Direcciones direccion)
        {
            bool obj;

            using (var r = new Repositorio<DIR_Direcciones>())
            {
                obj = r.Actualizar(direccion);
            }

            return obj;
        }

    }
}
