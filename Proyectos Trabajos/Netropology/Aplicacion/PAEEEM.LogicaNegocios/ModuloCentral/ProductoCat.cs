using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.LogicaNegocios.ModuloCentral
{
    public class ProductoCat
    {
        public static int ObtieneProducXid(int cveProduc)
        {
            CAT_PRODUCTO producto = null;
            int precioMax=0;
            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                producto = r.Extraer(tr => tr.Cve_Producto==cveProduc);
            }
            precioMax = (int)producto.Mt_Precio_Max;

            return precioMax;
        }

        public static CAT_PRODUCTO ProducXid(int cveProduc)
        {
            CAT_PRODUCTO producto = null;
            using (var r = new Repositorio<CAT_PRODUCTO>())
            {
                producto = r.Extraer(tr => tr.Cve_Producto == cveProduc);
            }

            return producto;
        }

        public static K_PROVEEDOR_PRODUCTO ObtienePorId(int product)
        {
            K_PROVEEDOR_PRODUCTO trInfoGeneral;

            using (var r = new Repositorio<K_PROVEEDOR_PRODUCTO>())
            {
                trInfoGeneral = r.Extraer(tr => tr.Cve_Producto == product);
            }
            return trInfoGeneral;
        }

        public static List<CatProductProveedor> ObtieneProducDistri(int cveProduc)
        {
            PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();
            var datos= (from kProduct in _contexto.K_PROVEEDOR_PRODUCTO
                            join Cprove in _contexto.CAT_PROVEEDOR on kProduct.Id_Proveedor equals Cprove.Id_Proveedor
                            join Cproduc in _contexto.CAT_PRODUCTO on kProduct.Cve_Producto equals Cproduc.Cve_Producto
                            join Cmarca in _contexto.CAT_MARCA on Cproduc.Cve_Marca equals Cmarca.Cve_Marca
                            
                            where kProduct.Cve_Producto==cveProduc
                            select new CatProductProveedor
                            {
                              cveProduc=kProduct.Cve_Producto,
                              idProve=kProduct.Id_Proveedor,
                              status=kProduct.Cve_Estatus_Prov_Prod,
                              precio=kProduct.Mt_Precio_Unitario,
                              correo=Cprove.Dx_Email_Repre,
                              //
                              marca=Cmarca.Dx_Marca,
                              modelo=Cproduc.Dx_Modelo_Producto,
                              representante=Cprove.Dx_Nombre_Repre,
                              StatusProveedor=Cprove.Cve_Estatus_Proveedor

                            }).ToList();
            
            return datos;
        }


        public static bool Actualizar(CAT_PRODUCTO product)
        {
            bool actualiza;

            var creInfoGeneral = ProducXid(product.Cve_Producto);

            if (creInfoGeneral != null)
            {
                using (var r = new Repositorio<CAT_PRODUCTO>())
                {
                    actualiza = r.Actualizar(product);
                }
            }
            else
            {
                throw new Exception("El Id: " + product.Cve_Producto + " no fue encontrado.");
            }

            return actualiza;
        }
    }
}
