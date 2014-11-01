using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.AdminUsuarios;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AdminUsuarios;

namespace PAEEEM.LogicaNegocios
{
    public class PermisoUsuarioBL
    {
        public List<PermisoUsuario> ObtenPermisosPorPadre(string idPadre)
        {
            var lstPermisos = new PermisoUsuarioDal().ObtenPermisosPorPadre(idPadre);

            return lstPermisos;
        }

        public PermisoUsuario ObtenPermisosRaiz()
        {
            var permisosRaiz = new PermisoUsuarioDal().ObtenPermisosRaiz();

            return permisosRaiz;
        }

        public List<US_ROL> ObtenRoles()
        {
            var lstRoles = new PermisoUsuarioDal().ObtenRoles();

            return lstRoles;
        }

        public List<US_USUARIO> ObtenUsuarioPorRol(int idRol)
        {
            var lstUsuarios = new PermisoUsuarioDal().ObtenUsuarioPorRol(idRol);

            return lstUsuarios;
        }

        public List<RolPermiso> ObtenPermisosPorRol(int idRol)
        {
            var lstPermisosRol = new PermisoUsuarioDal().ObtenPermisosPorRol(idRol);

            return lstPermisosRol;
        }

        public List<UsuarioPermiso> ObtenPermisosUsuario(int idusuario)
        {
            var lstUsuarioPermisos = new PermisoUsuarioDal().ObtenPermisosUsuario(idusuario);

            return lstUsuarioPermisos;
        }

        public List<CAT_ACCIONES> ObtenCatAcciones()
        {
            var lstAcciones = new PermisoUsuarioDal().ObtenCatAcciones();

            return lstAcciones;
        }

        public List<ACCIONES_ROL> ObtenAccionesRol(int idRol)
        {
            var lstAcciones = new PermisoUsuarioDal().ObtenAccionesRol(idRol);

            return lstAcciones;
        }

        public List<UsuarioPermiso> ActualizaPermisosUsuario(List<UsuarioPermiso> lstUsuarioPermiso, string usuario)
        {
            lstUsuarioPermiso.RemoveAll(me => me.IdUsuarioPermiso == 0 && me.Estatus == false && me.ExistePermisoRol == false);
            
            foreach (var usuarioPermiso in lstUsuarioPermiso.FindAll(me => me.IdUsuarioPermiso == 0))
            {
                var permiso = new US_USUARIO_PERMISO
                    {
                        Id_Usuario = usuarioPermiso.IdUsuario,
                        Id_Permiso = usuarioPermiso.IdPermiso,
                        Estatus = usuarioPermiso.Estatus,
                        FechaAdicion = DateTime.Now.Date,
                        AdicionadoPor = usuario
                    };

                new PermisoUsuarioDal().InsertaPermisoUsuario(permiso);
            }

            foreach (var usuarioPermiso in lstUsuarioPermiso.FindAll(me => me.IdUsuarioPermiso > 0))
            {
                var permisoExistente = new PermisoUsuarioDal().ObtenUsuarioPermiso(usuarioPermiso.IdUsuarioPermiso);

                if (permisoExistente != null)
                {
                    permisoExistente.Estatus = usuarioPermiso.Estatus;
                    new PermisoUsuarioDal().ActualizaUsuarioPermiso(permisoExistente);
                }
            }

            var idUsuario = lstUsuarioPermiso.FirstOrDefault().IdUsuario;
            var lstPermisosctualizadosUsuario = ObtenPermisosUsuario(idUsuario);

            return lstPermisosctualizadosUsuario;
        }

        public void ActualizaAccionesUsuario(List<ACCIONES_USUARIO> lstAccionesUsuario, int idRol)
        {
            new PermisoUsuarioDal().EliminaAccionesUsuario(lstAccionesUsuario.First().Id_Usuario);
            
            var lstAccionesRol = new PermisoUsuarioDal().ObtenAccionesRol(idRol);

            if (lstAccionesRol.Count > 0)
            {
                foreach (var accionesUsuario in from accionesUsuario in lstAccionesUsuario let accionRolExiste = lstAccionesRol.FirstOrDefault(me => me.ID_Acciones == accionesUsuario.ID_Acciones) where accionRolExiste != null where accionRolExiste.Estatus == accionesUsuario.Estatus select accionesUsuario)
                {
                    accionesUsuario.IdAccionUsuario = 1;
                }
            }

            lstAccionesUsuario.RemoveAll(me => me.IdAccionUsuario == 1);
            
            foreach (var accionesUsuario in lstAccionesUsuario)
            {
                var accionExiste = new PermisoUsuarioDal().ObtenAccionUusuario(accionesUsuario.Id_Usuario,
                                                                               accionesUsuario.ID_Acciones);

                if (accionExiste != null)
                {
                    accionExiste.Estatus = accionesUsuario.Estatus;
                    new PermisoUsuarioDal().ActualizaAccionUsuario(accionExiste);
                }
                else
                {
                    new PermisoUsuarioDal().InsertaAccionUsuario(accionesUsuario);
                }
            }
        }

        public List<ACCIONES_USUARIO> ObtenAccionesUsuario(int idUsuario)
        {
            var lstAcciones = new PermisoUsuarioDal().ObtenAccionesUsuario(idUsuario);

            return lstAcciones;
        }

        public void InsertaRolPermiso(List<int> intList, int idRol)
        {
            new PermisoUsuarioDal().EliminaRolPermisos(idRol);

            foreach (var i in intList)
            {
                var rolPermiso = new US_ROL_PERMISO {Id_Rol = idRol, Id_Permiso = i};

                new PermisoUsuarioDal().InsertaRolPermiso(rolPermiso);
            }
        }

        public void InsertaAccionesRol(List<int> intList, int idRol, string usuario)
        {
            new PermisoUsuarioDal().EliminaAccionesRol(idRol);

            foreach (var i in intList)
            {
                var accionRol = new ACCIONES_ROL();
                accionRol.Id_Rol = idRol;
                accionRol.ID_Acciones = Convert.ToByte(i);
                accionRol.Estatus = true;
                accionRol.Fecha_Adicion = DateTime.Now.Date;
                accionRol.Adicionado_Por = usuario;

                new PermisoUsuarioDal().InsertaAccionRol(accionRol);
            }
        }
    }
}
