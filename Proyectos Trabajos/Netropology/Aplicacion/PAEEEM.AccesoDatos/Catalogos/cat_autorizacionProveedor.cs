using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PAEEEM.Entidades;
using PAEEEM.Entidades.ModuloCentral;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class cat_autorizacionProveedor
    { //Augusto

        public static List<CAT_ZONA> catZonas()
        {
            List<CAT_ZONA> zonas = null;
            using (var r = new Repositorio<CAT_ZONA>())
            {
                zonas = r.Filtro(l => l.Dx_Nombre_Zona != null);
            }
            return zonas;
        }

        public static List<CAT_ZONA> catZonasXidRegion(int idReg)
        {
            List<CAT_ZONA> zonas = null;
            using (var r = new Repositorio<CAT_ZONA>())
            {
                zonas = r.Filtro(l => l.Dx_Nombre_Zona != null && l.Cve_Region == idReg);
            }
            return zonas;
        }

        public static List<CAT_ESTATUS_PROVEEDOR> catEstatus()
        {
            List<CAT_ESTATUS_PROVEEDOR> estatus = null;
            using (var r = new Repositorio<CAT_ESTATUS_PROVEEDOR>())
            {
                estatus = r.Filtro(l => l.Dx_Estatus_Proveedor != null);
            }
            return estatus;
        }

        //Actualizar Matrices

        public static CAT_PROVEEDOR obtieneParaActualizar(int idProveedor)
        {
          CAT_PROVEEDOR proveedor = null;

            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                proveedor = r.Extraer(l => l.Id_Proveedor == idProveedor);
            }
            return proveedor;
        }


        public bool ActalizaMatriz(CAT_PROVEEDOR update)
        {
            bool actualiza = false;

            using (var r = new Repositorio<CAT_PROVEEDOR>())
            {
                actualiza = r.Actualizar(update);
            }

            return actualiza;
        }

        //Actualiza Usuarios

        public static List<US_USUARIO> obtieneUsuarios(int id_Departamento, string tipoUser)
        {
            List<US_USUARIO> lista = null;
            using (var us = new Repositorio<US_USUARIO>())
            {
                lista = us.Filtro(l => l.Id_Departamento == id_Departamento && l.Tipo_Usuario==tipoUser);
            }
            return lista;
        }

        public bool actualizaUsuarios(US_USUARIO updateUSER)
        {
            bool actualizar = false;
            using (var r = new Repositorio<US_USUARIO>())
            {
                actualizar = r.Actualizar(updateUSER);
            }

            return actualizar;
        }

        //Actualiza Sucursales

        public static CAT_PROVEEDORBRANCH obtieneSucursal(int id_branch)
        {
            CAT_PROVEEDORBRANCH sucursal = null;
            using (var su = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                sucursal = su.Extraer(l => l.Id_Branch == id_branch);
            }
            return sucursal;
        }

        public bool ActalizaSucursalBranch(CAT_PROVEEDORBRANCH update)
        {
            bool actualiza = false;

            using (var r = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                actualiza = r.Actualizar(update);
            }

            return actualiza;
        }

        public static List<CAT_PROVEEDORBRANCH> listaSucursales(int id_matriz)
        {
            List<CAT_PROVEEDORBRANCH> lista = null;
            using (var lis = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                lista = lis.Filtro(l => l.Id_Proveedor == id_matriz);
            }
            return lista;
        }

        //Virtuales 

        public static List<CAT_PROVEEDORBRANCH> listaSucursalesVirtuales(int id_dependencia)
        {
            List<CAT_PROVEEDORBRANCH> lista = null;
            using (var lis = new Repositorio<CAT_PROVEEDORBRANCH>())
            {
                lista = lis.Filtro(l => l.id_Dependencia ==id_dependencia);
            }
            return lista;
        }

        //MotivosCancelacion
        public static List<MOTIVOS_CANCELACION_PROVEEDOR> motivosCancelacion()
        {
            List<MOTIVOS_CANCELACION_PROVEEDOR> lista = null;
            using (var r = new Repositorio<MOTIVOS_CANCELACION_PROVEEDOR>())
            {
                lista = r.Filtro(l => l.ID_MOTIVO != null);
            }

            return lista;
        }

    }
}
