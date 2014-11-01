using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Operacion_Datos
{
    public class AdministracionUsuarios
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public AdministracionUsuarios ()
        {
            
        }

        public US_USUARIO ObtenDatosUsuario(string usuario)
        {
            US_USUARIO usUsuario = null;

            using (var r = new Repositorio<US_USUARIO>())
            {
                usUsuario = r.Extraer(me => me.Nombre_Usuario == usuario);
            }

            return usUsuario;
        }

        public CAT_PROVEEDOR ObtenProveedor(int idProveedor)
        {
            CAT_PROVEEDOR proveedor = null;

            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                proveedor = r.Extraer(me => me.Id_Proveedor == idProveedor);
            }

            return proveedor;
        }

        public CAT_PROVEEDORBRANCH ObtenProveedorBranch(int idBranch)
        {
            CAT_PROVEEDORBRANCH proveedorbranch = null;

            var lstProveedorBranch = (from proveedor in _contexto.CAT_PROVEEDOR
                                      join branch in _contexto.CAT_PROVEEDORBRANCH
                                          on proveedor.Id_Proveedor equals branch.Id_Proveedor
                                      where branch.Id_Branch == idBranch
                                      select branch).Distinct().ToList();

            proveedorbranch = lstProveedorBranch.FirstOrDefault();

            return proveedorbranch;
        }
    }
}
