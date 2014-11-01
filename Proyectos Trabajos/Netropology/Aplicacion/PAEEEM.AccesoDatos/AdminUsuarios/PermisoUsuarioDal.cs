using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AdminUsuarios;

namespace PAEEEM.AccesoDatos.AdminUsuarios
{
    public class PermisoUsuarioDal
    {
        private readonly PAEEEM_DESAEntidades _contexto = new PAEEEM_DESAEntidades();

        public PermisoUsuarioDal()
        {}

        public List<PermisoUsuario> ObtenPermisosPorPadre(string idPadre)
        {
            var resultado = (from permiso in _contexto.US_PERMISO
                            join navegacion in _contexto.US_NAVEGACION
                                on permiso.Valor_Permiso equals navegacion.Id_Navegacion
                            select new PermisoUsuario
                                {
                                    IdNavegacion = navegacion.Id_Navegacion,
                                    NombreNavegacion = navegacion.Nombre_Navegacion,
                                    UrlNavegacion = navegacion.Url_Navegacion,
                                    CodigoPadres = navegacion.Codigo_Padres,
                                    RutaPadres = navegacion.Ruta_Padres,
                                    Estatus = navegacion.Estatus,
                                    NivelNavegacion = (int) navegacion.Nivel_Navegacion,
                                    Secuencia = navegacion.Secuencia,
                                    TipoPermiso = permiso.Tipo_Permiso,
                                    IdPermiso = permiso.Id_Permiso
                                }).ToList();

            var lstPermisos = new List<PermisoUsuario>(resultado.OrderBy(me => me.Secuencia));

            return lstPermisos;
        }

        public PermisoUsuario ObtenPermisosRaiz()
        {
            var resultado = (from permiso in _contexto.US_PERMISO
                             join navegacion in _contexto.US_NAVEGACION
                                 on permiso.Valor_Permiso equals navegacion.Id_Navegacion
                             where navegacion.Nivel_Navegacion == 0
                             select new PermisoUsuario
                                 {
                                     IdNavegacion = navegacion.Id_Navegacion,
                                     NombreNavegacion = navegacion.Nombre_Navegacion,
                                     UrlNavegacion = navegacion.Url_Navegacion,
                                     CodigoPadres = navegacion.Codigo_Padres,
                                     RutaPadres = navegacion.Ruta_Padres,
                                     Estatus = navegacion.Estatus,
                                     NivelNavegacion = (int) navegacion.Nivel_Navegacion,
                                     Secuencia = navegacion.Secuencia,
                                     TipoPermiso = permiso.Tipo_Permiso,
                                     IdPermiso = permiso.Id_Permiso
                                 }).ToList().FirstOrDefault();

            return resultado;
        }

        public List<US_ROL> ObtenRoles()
        {
            List<US_ROL> lstRoles = null;

            using (var r = new Repositorio<US_ROL>())
            {
                var roles = r.Filtro(me => me.Id_Rol > 0);
                lstRoles = new List<US_ROL>(roles.OrderBy(me => me.Nombre_Rol));
            }

            return lstRoles;
        }

        public List<US_USUARIO> ObtenUsuarioPorRol(int idRol)
        {
            List<US_USUARIO> lstUsuarios = null;

            using (var r = new Repositorio<US_USUARIO>())
            {
                var usuarios = r.Filtro(me => me.Id_Rol == idRol && me.Estatus == "A" && me.Nombre_Usuario != "");

                lstUsuarios = new List<US_USUARIO>(usuarios.OrderBy(me => me.Nombre_Usuario));
            }

            return lstUsuarios;
        }

        public List<RolPermiso> ObtenPermisosPorRol(int idRol)
        {
            var resultado = (from rolPermiso in _contexto.US_ROL_PERMISO
                             join permiso in _contexto.US_PERMISO
                                 on rolPermiso.Id_Permiso equals permiso.Id_Permiso
                             join rol in _contexto.US_ROL
                                 on rolPermiso.Id_Rol equals rol.Id_Rol
                             join navegacion in _contexto.US_NAVEGACION
                                 on permiso.Valor_Permiso equals navegacion.Id_Navegacion
                             where rolPermiso.Id_Rol == idRol
                             select new RolPermiso
                                 {
                                     IdNavegacion = navegacion.Id_Navegacion,
                                     NombreNavegacion = navegacion.Nombre_Navegacion,
                                     UrlNavegacion = navegacion.Url_Navegacion,
                                     CodigoPadres = navegacion.Codigo_Padres,
                                     RutaPadres = navegacion.Ruta_Padres,
                                     NivelNavegacion = (int) navegacion.Nivel_Navegacion,
                                     Secuencia = navegacion.Secuencia,
                                     IdRol = rol.Id_Rol,
                                     NombreRol = rol.Nombre_Rol,
                                     RelacionRol = rol.Relacion_Rol,
                                     IdPermiso = permiso.Id_Permiso,
                                     TipoPermiso = permiso.Tipo_Permiso
                                 }).ToList();

            return resultado;
        }

        public US_ROL_PERMISO InsertaRolPermiso(US_ROL_PERMISO rolPermiso)
        {
            using (var r = new Repositorio<US_ROL_PERMISO>())
            {
                return r.Agregar(rolPermiso);
            }
        }

        public void EliminaRolPermisos(int idRol)
        {
            using (var r = new Repositorio<US_ROL_PERMISO>())
            {
                var lstRolPermiso = r.Filtro(me => me.Id_Rol == idRol);

                if (lstRolPermiso.Count > 0)
                {
                    foreach (var usRolPermiso in lstRolPermiso)
                    {
                        r.Eliminar(usRolPermiso);
                    }
                }
            }
        }

        public List<UsuarioPermiso> ObtenPermisosUsuario(int idusuario)
        {
            var lstPermisosUsuario = (from permiso in _contexto.US_USUARIO_PERMISO
                                      where permiso.Id_Usuario == idusuario
                                      select new UsuarioPermiso
                                          {
                                              IdUsuarioPermiso = permiso.IdUsuarioPermiso,
                                              IdUsuario = (int) permiso.Id_Usuario,
                                              IdPermiso = (int) permiso.Id_Permiso,
                                              Estatus = (bool) permiso.Estatus,
                                              ExistePermisoRol = false,
                                              FechaAdicion = (DateTime) permiso.FechaAdicion,
                                              AdicionadoPor = permiso.AdicionadoPor
                                          }).ToList();

            return lstPermisosUsuario;
        }

        public List<CAT_ACCIONES> ObtenCatAcciones()
        {
            List<CAT_ACCIONES> lstAcciones = null;

            using (var r = new Repositorio<CAT_ACCIONES>())
            {
                lstAcciones = r.Filtro(me => me.Estatus && me.ID_MONITOR == 1);
            }

            return lstAcciones;
        }

        public List<ACCIONES_ROL> ObtenAccionesRol(int idRol)
        {
            List<ACCIONES_ROL> lstAcciones = null;

            using (var r = new Repositorio<ACCIONES_ROL>())
            {
                lstAcciones = r.Filtro(me => me.Id_Rol == idRol && me.Estatus);
            }

            return lstAcciones;
        }

        public void InsertaAccionRol(ACCIONES_ROL accionRol)
        {
            using (var r = new Repositorio<ACCIONES_ROL>())
            {
                r.Agregar(accionRol);
            }
        }

        public bool ActualizaAccionRol(ACCIONES_ROL accionRol)
        {
            using (var r = new Repositorio<ACCIONES_ROL>())
            {
                return r.Actualizar(accionRol);
            }
        }

        public List<ACCIONES_USUARIO> ObtenAccionesUsuario(int idUsuario)
        {
            using (var r = new Repositorio<ACCIONES_USUARIO>())
            {
                return r.Filtro(me => me.Id_Usuario == idUsuario);
            }
        }

        public void InsertaAccionUsuario(ACCIONES_USUARIO accionUsuario)
        {
            using (var r = new Repositorio<ACCIONES_USUARIO>())
            {
                r.Agregar(accionUsuario);
            }
        }

        public bool ActualizaAccionUsuario(ACCIONES_USUARIO accionUsuario)
        {
            using (var r = new Repositorio<ACCIONES_USUARIO>())
            {
                return r.Actualizar(accionUsuario);
            }
        }

        public ACCIONES_USUARIO ObtenAccionUusuario(int idUsuario, int idAccion)
        {
            using (var r = new Repositorio<ACCIONES_USUARIO>())
            {
                return r.Extraer(me => me.Id_Usuario == idUsuario && me.ID_Acciones == idAccion);
            }
        }

        public void EliminaAccionesUsuario(int idUsuario)
        {
            using (var r = new Repositorio<ACCIONES_USUARIO>())
            {
                var lstAccUsuario = r.Filtro(me => me.Id_Usuario == idUsuario);

                if (lstAccUsuario.Count > 0)
                {
                    foreach (var accionesUsuario in lstAccUsuario)
                    {
                        r.Eliminar(accionesUsuario);
                    }
                }
            }
        }

        public void EliminaAccionesRol(int idRol)
        {
            using (var r = new Repositorio<ACCIONES_ROL>())
            {
                var lstAccionesRol = r.Filtro(me => me.Id_Rol == idRol);

                if (lstAccionesRol.Count > 0)
                {
                    foreach (var accionesRol in lstAccionesRol)
                    {
                        r.Eliminar(accionesRol);
                    }
                }
            }
        }

        #region Operaciones Us_Usuario_Permisos

        public US_USUARIO_PERMISO ObtenUsuarioPermiso(int idUsuarioPermiso)
        {
            US_USUARIO_PERMISO usUsuarioPermiso = null;

            using (var r = new Repositorio<US_USUARIO_PERMISO>())
            {
                usUsuarioPermiso = r.Extraer(me => me.IdUsuarioPermiso == idUsuarioPermiso);
            }

            return usUsuarioPermiso;
        }
        
        public void InsertaPermisoUsuario(US_USUARIO_PERMISO usuarioPermiso)
        {
            using (var r = new Repositorio<US_USUARIO_PERMISO>())
            {
                r.Agregar(usuarioPermiso);
            }
        }

        public void ActualizaUsuarioPermiso(US_USUARIO_PERMISO usuarioPermiso)
        {
            using (var r = new Repositorio<US_USUARIO_PERMISO>())
            {
                r.Actualizar(usuarioPermiso);
            }
        }

        #endregion


        public List<US_USUARIO> ObtenUsuarios()
        {
            using (var r = new Repositorio<US_USUARIO>())
            {
                return r.Filtro(me => me.Id_Usuario > 0);
            }
        }

        public void ActualizaUsuario(US_USUARIO usuario)
        {
            using (var r = new Repositorio<US_USUARIO>())
            {
                r.Actualizar(usuario);
            }
        }
    }
}
