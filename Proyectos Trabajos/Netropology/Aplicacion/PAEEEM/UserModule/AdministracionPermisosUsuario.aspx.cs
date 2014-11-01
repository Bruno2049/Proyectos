using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAEEEM.Entidades;
using PAEEEM.Entidades.AdminUsuarios;
using PAEEEM.Entities;
using PAEEEM.LogicaNegocios;
using Telerik.Web.UI;

namespace PAEEEM.UserModule
{
    public partial class AdministracionPermisosUsuario : System.Web.UI.Page
    {
        #region Variables globales

        public List<RolPermiso> LstRolPermiso
        {
            get
            {
                return ViewState["LstRolPermiso"] == null
                           ? new List<RolPermiso>()
                           : ViewState["LstRolPermiso"] as List<RolPermiso>;
            }
            set { ViewState["LstRolPermiso"] = value; }
        }

        public List<UsuarioPermiso> LstUsuarioPermiso
        {
            get
            {
                return ViewState["LstUsuarioPermiso"] == null
                           ? new List<UsuarioPermiso>()
                           : ViewState["LstUsuarioPermiso"] as List<UsuarioPermiso>;
            }
            set { ViewState["LstUsuarioPermiso"] = value; }
        }

        public bool ActualizaAcciones
        {
            get { return ViewState["ActualizaAcciones"] != null && bool.Parse(ViewState["ActualizaAcciones"].ToString()); }
            set { ViewState["ActualizaAcciones"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LstRolPermiso = new List<RolPermiso>();
                LstUsuarioPermiso = new List<UsuarioPermiso>();
                LLenaCatalogoRoles();
                CargaRaiz();
                LLenaCatalogoAcciones();
                ActualizaAcciones = false;
            }
        }

        #region Catalogos

        protected void LLenaCatalogoRoles()
        {
            var lstRoles = new PermisoUsuarioBL().ObtenRoles();

            if (lstRoles.Count > 0)
            {
                RadCmbRoles.DataSource = lstRoles;
                RadCmbRoles.DataValueField = "Id_Rol";
                RadCmbRoles.DataTextField = "Nombre_Rol";
                RadCmbRoles.DataBind();

                RadCmbRoles.Items.Insert(0, new RadComboBoxItem("Seleccione"));
                RadCmbRoles.SelectedIndex = 0;
            }
        }

        protected void LLenaUsuarios(int idRol)
        {
            var lstUsuarios = new PermisoUsuarioBL().ObtenUsuarioPorRol(idRol);
            RadListUsuarios.DataSource = lstUsuarios;
            RadListUsuarios.DataValueField = "Id_Usuario";
            RadListUsuarios.DataTextField = "Nombre_Usuario";
            RadListUsuarios.DataBind();

            RadListUsuarios.Items.Insert(0, new RadListBoxItem(""));
            RadListUsuarios.SelectedIndex = 0;
        }

        protected void LLenaCatalogoAcciones()
        {
            RadLstAcciones.Items.Clear();
            var lstAcciones = new PermisoUsuarioBL().ObtenCatAcciones();
            RadLstAcciones.DataSource = lstAcciones;
            RadLstAcciones.DataValueField = "ID_Acciones";
            RadLstAcciones.DataTextField = "Nombre_Accion";
            RadLstAcciones.DataBind();
        }

        #endregion

        #region Metodos Protegidos

        protected void CargaRaiz()
        {
            RadTreeViewPermisos.Nodes.Clear();
            var permisoRaiz = new PermisoUsuarioBL().ObtenPermisosRaiz();

            if (permisoRaiz != null)
            {
                var nodoRaiz = new RadTreeNode(permisoRaiz.NombreNavegacion, permisoRaiz.IdPermiso.ToString(CultureInfo.InvariantCulture))
                    {
                        ToolTip = permisoRaiz.IdNavegacion.ToString(CultureInfo.InvariantCulture)
                    };
                RadTreeViewPermisos.Nodes.Add(nodoRaiz);
                CargaNodosHijosARaiz(nodoRaiz);
            }
        }

        protected void CargaNodosHijosARaiz(RadTreeNode nodoRaiz)
        {
            var lstNodosNavegacion = new PermisoUsuarioBL().ObtenPermisosPorPadre(nodoRaiz.ToolTip);
            lstNodosNavegacion = lstNodosNavegacion.FindAll(me => me.TipoPermiso == "P" && me.CodigoPadres == nodoRaiz.ToolTip);

            if (lstNodosNavegacion.Count > 0)
            {
                foreach (var permisoUsuario in lstNodosNavegacion)
                {
                    var nodoHijo = new RadTreeNode(permisoUsuario.NombreNavegacion,
                                                   permisoUsuario.IdPermiso.ToString(CultureInfo.InvariantCulture))
                        {
                            ImageUrl = "..\\Resources\\Images\\page.gif",
                            ToolTip = permisoUsuario.IdNavegacion.ToString()
                        };
                    nodoRaiz.Nodes.Add(nodoHijo);
                    CargaNodosHijosARaiz(nodoHijo);                 
                }
            }
        }

        protected void CargaPermisosRol(int idRol)
        {
            RadTreeViewPermisos.UncheckAllNodes();

            if (RadTreeViewPermisos.Nodes.Count > 0)
            {
                LstRolPermiso = new PermisoUsuarioBL().ObtenPermisosPorRol(idRol);

                if (LstRolPermiso.Count > 0)
                {
                    for (int i = 0; i < RadTreeViewPermisos.Nodes.Count; i++)
                    {
                        var idPermiso = int.Parse(RadTreeViewPermisos.Nodes[i].Value);
                        var rolPermiso = LstRolPermiso.FirstOrDefault(me => me.IdPermiso == idPermiso);

                        if (rolPermiso != null)
                            RadTreeViewPermisos.Nodes[i].Checked = true;

                        MarcaNodosHijos(RadTreeViewPermisos.Nodes[i]);
                    }
                }
            }
        }

        protected void MarcaNodosHijos(RadTreeNode nodo)
        {
            if (nodo.Nodes.Count > 0)
            {
                for (int i = 0; i < nodo.Nodes.Count; i++)
                {
                    var idPermiso = int.Parse(nodo.Nodes[i].Value);
                    var rolPermiso = LstRolPermiso.FirstOrDefault(me => me.IdPermiso == idPermiso);

                    if (rolPermiso != null)
                        nodo.Nodes[i].Checked = true;

                    MarcaNodosHijos(nodo.Nodes[i]);
                }
            }
        }

        protected void InsertaPermisoEnLista(RadTreeNode nodo)
        {
            if (RadListUsuarios.SelectedIndex != 0)
            {
                var permisoUsuario = LstUsuarioPermiso.FirstOrDefault(me => me.IdPermiso == int.Parse(nodo.Value));

                if (permisoUsuario != null)
                {
                    permisoUsuario.Estatus = nodo.Checked;
                }
                else
                {
                    var permisoRolExistente = LstRolPermiso.FirstOrDefault(me => me.IdPermiso == int.Parse(nodo.Value));
                    var permisoExistente = permisoRolExistente != null;

                    var newPermisoUsuario = new UsuarioPermiso
                        {
                            IdPermiso = int.Parse(nodo.Value),
                            IdUsuario = int.Parse(RadListUsuarios.SelectedValue),
                            Estatus = nodo.Checked,
                            ExistePermisoRol = permisoExistente
                        };
                    LstUsuarioPermiso.Add(newPermisoUsuario);
                }

                if (nodo.Nodes.Count > 0)
                {
                    for (int i = 0; i < nodo.Nodes.Count; i++)
                    {
                        InsertaPermisoEnLista(nodo.Nodes[i]);
                    }
                }
            }
        }

        protected void CargaPermisosUsuario()
        {
            if (RadTreeViewPermisos.Nodes.Count > 0)
            {

                if (LstUsuarioPermiso.Count > 0)
                {
                    for (int i = 0; i < RadTreeViewPermisos.Nodes.Count; i++)
                    {
                        var idPermiso = int.Parse(RadTreeViewPermisos.Nodes[i].Value);
                        var usuarioPermiso = LstUsuarioPermiso.FirstOrDefault(me => me.IdPermiso == idPermiso);

                        if (usuarioPermiso != null)
                            RadTreeViewPermisos.Nodes[i].Checked = usuarioPermiso.Estatus;

                        MarcaNodosPermisoUsuario(RadTreeViewPermisos.Nodes[i]);
                    }
                }
            }
        }

        protected void MarcaNodosPermisoUsuario(RadTreeNode nodo)
        {
            if (nodo.Nodes.Count > 0)
            {
                for (int i = 0; i < nodo.Nodes.Count; i++)
                {
                    var idPermiso = int.Parse(nodo.Nodes[i].Value);
                    var usuarioPermiso = LstUsuarioPermiso.FirstOrDefault(me => me.IdPermiso == idPermiso);

                    if (usuarioPermiso != null)
                        nodo.Nodes[i].Checked = usuarioPermiso.Estatus;

                    MarcaNodosPermisoUsuario(nodo.Nodes[i]);
                }
            }
        }

        protected List<int> ObtenListaPermisos()
        {
            var intList = new List<int>();
            if (RadTreeViewPermisos.Nodes.Count > 0)
            {
                for (var i = 0; i < RadTreeViewPermisos.Nodes.Count; i++)
                {
                    if (RadTreeViewPermisos.Nodes[i].Checked)
                    {
                        var permiso = int.Parse(RadTreeViewPermisos.Nodes[i].Value);
                        intList.Add(permiso);
                        intList = ObtenListaHIjos(RadTreeViewPermisos.Nodes[i], intList);
                    }
                }
            }
            return intList;
        }

        protected List<int> ObtenListaHIjos(RadTreeNode nodo, List<int> intList)
        {
            if (nodo.Nodes.Count > 0)
            {
                for (var i = 0; i < nodo.Nodes.Count; i++)
                {
                    if (nodo.Nodes[i].Checked)
                    {
                        var permiso = int.Parse(nodo.Nodes[i].Value);
                        intList.Add(permiso);
                        intList = ObtenListaHIjos(nodo.Nodes[i], intList);
                    }
                }
            }
            return intList;
        }

        protected void CargaAccionesRol(int idRol)
        {
            LLenaCatalogoAcciones();
            var lstAccionesRol = new PermisoUsuarioBL().ObtenAccionesRol(idRol);

            if (lstAccionesRol.Count > 0)
            {
                for (int i = 0; i < RadLstAcciones.Items.Count; i++)
                {
                    var idAccion = int.Parse(RadLstAcciones.Items[i].Value);
                    
                    if (lstAccionesRol.FindAll(me => me.ID_Acciones == idAccion).Count > 0)
                    {
                        RadLstAcciones.Items[i].Checked = true;
                    }
                }
            }
        }

        protected void CargaAccionesUsuario(int idUsuario)
        {
            var lstAccionesUsuario = new PermisoUsuarioBL().ObtenAccionesUsuario(idUsuario);

            if (lstAccionesUsuario.Count > 0)
            {
                for (int i = 0; i < RadLstAcciones.Items.Count; i++)
                {
                    var idAccion = int.Parse(RadLstAcciones.Items[i].Value);

                    if (lstAccionesUsuario.FindAll(me => me.ID_Acciones == idAccion && me.Estatus == true).Count > 0)
                    {
                        RadLstAcciones.Items[i].Checked = true;
                    }

                    if (lstAccionesUsuario.FindAll(me => me.ID_Acciones == idAccion && me.Estatus == false).Count > 0)
                    {
                        RadLstAcciones.Items[i].Checked = false;
                    }
                }
            }
        }

        protected void InsertaAccionesUsuario(int idUsuario, int idRol)
        {
            var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;
            var lsAcciones = new List<ACCIONES_USUARIO>();

            for (int i = 0; i < RadLstAcciones.Items.Count; i++)
            {
                var accionUsuario = new ACCIONES_USUARIO();
                accionUsuario.ID_Acciones = byte.Parse(RadLstAcciones.Items[i].Value);
                accionUsuario.Id_Usuario = idUsuario;
                accionUsuario.Estatus = RadLstAcciones.Items[i].Checked;
                accionUsuario.FechaAdicion = DateTime.Now.Date;
                accionUsuario.AdicionadoPor = usuario;
                lsAcciones.Add(accionUsuario);
            }

            new PermisoUsuarioBL().ActualizaAccionesUsuario(lsAcciones, idRol);
        }

        protected List<int> ObtenAcciones()
        {
            var intList = new List<int>();

            for (var i = 0; i < RadLstAcciones.Items.Count; i++)
            {
                if (RadLstAcciones.Items[i].Checked)
                {
                    intList.Add(int.Parse(RadLstAcciones.Items[i].Value));
                }
            }

            return intList;
        }

        #endregion

        #region Eventos

        protected void RadCmbRoles_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadCmbRoles.SelectedIndex != 0)
            {
                LLenaUsuarios(int.Parse(RadCmbRoles.SelectedValue));
                CargaPermisosRol(int.Parse(RadCmbRoles.SelectedValue));
                CargaAccionesRol(int.Parse(RadCmbRoles.SelectedValue));
            }
        }

        protected void RadTreeViewPermisos_NodeCheck(object sender, RadTreeNodeEventArgs e)
        {
            var nodo = e.Node;

            if (nodo.Checked)
            {
                nodo.CheckChildNodes();
                nodo.ParentNode.Checked = true;
            }
            else
            {
                nodo.UncheckChildNodes();
            }

            InsertaPermisoEnLista(nodo);
        }

        protected void RadListUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizaAcciones = false;
            CargaPermisosRol(int.Parse(RadCmbRoles.SelectedValue));
            if (!RadTreeViewPermisos.Enabled)
                RadTreeViewPermisos.Enabled = true;

            LstUsuarioPermiso = new List<UsuarioPermiso>();
            LstUsuarioPermiso = new PermisoUsuarioBL().ObtenPermisosUsuario(int.Parse(RadListUsuarios.SelectedValue));

            if (LstUsuarioPermiso.Count > 0)
            {
                CargaAccionesRol(int.Parse(RadCmbRoles.SelectedValue));
                CargaPermisosUsuario();
            }

            CargaAccionesUsuario(int.Parse(RadListUsuarios.SelectedValue));
        }

        protected void RadBtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (RadListUsuarios.SelectedIndex != 0)
                {
                    if (LstUsuarioPermiso.Count > 0)
                    {
                        var usuario = ((US_USUARIOModel) Session["UserInfo"]).Nombre_Usuario;
                        var lstNewPermisos = new PermisoUsuarioBL().ActualizaPermisosUsuario(LstUsuarioPermiso, usuario);

                        LstUsuarioPermiso = lstNewPermisos;
                    }

                    if (ActualizaAcciones)
                    {
                        InsertaAccionesUsuario(int.Parse(RadListUsuarios.SelectedValue),
                                               int.Parse(RadCmbRoles.SelectedValue));
                    }

                    rwmVentana.RadAlert("Se actualizarón los permisos del usuario correctamente", 300, 150,
                                        "Permisos usuario", null);

                    LstUsuarioPermiso = new List<UsuarioPermiso>();
                    ActualizaAcciones = false;
                }
                else
                {
                    var listPermisos = ObtenListaPermisos();
                    new PermisoUsuarioBL().InsertaRolPermiso(listPermisos, int.Parse(RadCmbRoles.SelectedValue));

                    if (ActualizaAcciones)
                    {
                        var lstAccionesRol = ObtenAcciones();
                        var usuario = ((US_USUARIOModel)Session["UserInfo"]).Nombre_Usuario;
                        new PermisoUsuarioBL().InsertaAccionesRol(lstAccionesRol, int.Parse(RadCmbRoles.SelectedValue), usuario);
                    }

                    rwmVentana.RadAlert("Se actualizarón los permisos del Rol correctamente", 300, 150,
                                        "Permisos Rol", null);

                    LstUsuarioPermiso = new List<UsuarioPermiso>();
                    ActualizaAcciones = false;
                }

                LstRolPermiso = new List<RolPermiso>();
                LstUsuarioPermiso = new List<UsuarioPermiso>();
                LLenaCatalogoRoles();
                CargaRaiz();
                LLenaCatalogoAcciones();
                ActualizaAcciones = false;
                RadListUsuarios.Items.Clear();
                RadLstAcciones.Visible = false;
            }
            catch (Exception ex)
            {
                rwmVentana.RadAlert("Ocurrió un problema al actualizar los permisos: " +  ex.Message, 300, 150, "Permisos usuario", null);
            }
        }

        protected void RadLstAcciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void RadTreeViewPermisos_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            var nodo = e.Node;

            if (nodo.Checked)
            {
                if (nodo.Value == "12" || nodo.Value == "54")
                    RadLstAcciones.Visible = true;
                else
                    RadLstAcciones.Visible = false;
            }
        }

        protected void RadLstAcciones_ItemCheck(object sender, RadListBoxItemEventArgs e)
        {
            ActualizaAcciones = true;
        }

        #endregion                

        
    }
}