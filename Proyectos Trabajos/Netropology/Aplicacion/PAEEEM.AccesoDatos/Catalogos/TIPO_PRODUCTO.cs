using PAEEEM.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class TIPO_PRODUCTO
    {
        public CAT_TIPO_PRODUCTO Agregar(CAT_TIPO_PRODUCTO tipoProducto)
        {
            CAT_TIPO_PRODUCTO catTipoProducto = null;

            using (var r = new Repositorio<CAT_TIPO_PRODUCTO>())
            {
                catTipoProducto = r.Extraer(c => c.Ft_Tipo_Producto == tipoProducto.Ft_Tipo_Producto);

                if (catTipoProducto == null)
                    catTipoProducto = r.Agregar(tipoProducto);
                else
                    throw new Exception("El producto ya existe en la BD.");
            }

            return catTipoProducto;
        }


        public CAT_TIPO_PRODUCTO ObtienePorCondicion(Expression<Func<CAT_TIPO_PRODUCTO, bool>> criterio)
        {
            CAT_TIPO_PRODUCTO catTipoProducto = null;

            using (var r = new Repositorio<CAT_TIPO_PRODUCTO>())
            {
                catTipoProducto = r.Extraer(criterio);
            }

            return catTipoProducto;
        }


        public List<CAT_TIPO_PRODUCTO> ObtieneTodos()
        {
            List<CAT_TIPO_PRODUCTO> listTipoProductos = null;

            using (var r = new Repositorio<CAT_TIPO_PRODUCTO>())
            {
                listTipoProductos = r.Filtro(null);
            }

            return listTipoProductos;
        }


        public bool Actualizar(CAT_TIPO_PRODUCTO tipoProducto)
        {
            bool actualiza = false;

            var catTipoProducto = ObtienePorCondicion(c => c.Ft_Tipo_Producto == tipoProducto.Ft_Tipo_Producto);

            if (catTipoProducto != null)
            {
                using (var r = new Repositorio<CAT_TIPO_PRODUCTO>())
                {
                    actualiza = r.Actualizar(catTipoProducto);
                }
            }
            else
            {
                throw new Exception("El tipo producto con Id: " + catTipoProducto.Ft_Tipo_Producto + " no fue encontrado.");
            }

            return actualiza;
        }


        public bool Eliminar(int idTipoProducto)
        {
            bool elimina = false;

            var catTipoProducto = ObtienePorCondicion(c => c.Ft_Tipo_Producto == idTipoProducto);

            if (catTipoProducto != null)
            {
                using (var r = new Repositorio<CAT_TIPO_PRODUCTO>())
                {
                    elimina = r.Eliminar(catTipoProducto);
                }
            }
            else
            {
                throw new Exception("No se encontro el producto indicado.");
            }

            return elimina;
        }


        public List<CAT_TIPO_PRODUCTO> FitrarPorCondicion(Expression<Func<CAT_TIPO_PRODUCTO, bool>> criterio)
        {
            List<CAT_TIPO_PRODUCTO> listTipoProducto = null;

            using (var r = new Repositorio<CAT_TIPO_PRODUCTO>())
            {
                listTipoProducto =  new List<CAT_TIPO_PRODUCTO>(r.Filtro(criterio).ToList().OrderBy(me => me.Dx_Tipo_Producto));
            }

            return listTipoProducto;
        }

    }
}
