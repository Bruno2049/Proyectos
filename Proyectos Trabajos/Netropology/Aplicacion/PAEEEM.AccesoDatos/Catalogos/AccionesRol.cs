using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AdminUsuarios;

namespace PAEEEM.AccesoDatos.Catalogos
{
    public class AccionesRol
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public List<AccionUsuario> AccionesMonitorRol(int monitor, int idRol)
        {
            var resultado = (from acciones in _contexto.CAT_ACCIONES
                join rol in _contexto.ACCIONES_ROL
                    on acciones.ID_Acciones equals rol.ID_Acciones
                where rol.Id_Rol == idRol && acciones.ID_MONITOR == monitor //&& acciones.Estatus
                select new AccionUsuario
                {
                    IdAccion = acciones.ID_Acciones,
                    NombreAccion = acciones.Nombre_Accion,
                    EstatusAccion = acciones.Estatus
                }).Distinct().ToList();

            return resultado;

        }

        public List<AccionUsuario> accionesEstatus(int estatusProveedor)
        {
            var resultado = (from CA in _contexto.CAT_ACCIONES
                             join AEP in _contexto.ACCION_ESTATUS_PROVEEDOR on CA.ID_Acciones equals AEP.ID_Acciones
                             where AEP.Cve_Estatus_Proveedor == estatusProveedor
                             select new AccionUsuario
                                 {
                                     IdAccion = CA.ID_Acciones,
                                     NombreAccion = CA.Nombre_Accion,
                                     EstatusAccion = CA.Estatus
                                 }).Distinct().ToList();

            return resultado;
        }
    }
}
