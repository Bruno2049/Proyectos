using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Infragistics.WebUI.UltraWebNavigator;
namespace eClock
{
    public partial class eClock : System.Web.UI.Page
    {
        CeC_Sesion Sesion;
        protected void LogSeguimiento(string Texto)
        {
            CIsLog2.AgregaLog(Texto);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                /* if (!IsPostBack)
                 {
                     int UsuarioRespuesta = CeC_Sesion.ValidarUsuario("admin", "admin");
                     Sesion = CeC_Sesion.Nuevo(this, UsuarioRespuesta);
                 }
                 else*/
                Sesion = CeC_Sesion.Nuevo(this);
                if (Sesion == null)
                    return;
                LogSeguimiento("eClock.Cargando Inicio");
                if (!IsPostBack && Sesion.SESION_ID > 0)
                {
                    CeC_Asistencias.CambioAccesosPendientes();
                    CeC_Asistencias.CambioDatosEmp();
                    Btn_Usuario.Text = Sesion.USUARIO_NOMBRE;
                    CMd_Base.gActualizaEmpleados(Sesion.SUSCRIPCION_ID, false);
                    //CMd_Base.gActualizaTurnos(Sesion.SUSCRIPCION_ID);
                    CMd_Base.gRecibeIncidencias(DateTime.Now.Date, DateTime.Now.Date, "");
                    ActualizaEmpleados();
                    ActualizaTurnos();
                    ActualizaTerminales();
                    if (Sesion.PERFIL_ID != 4 && Sesion.PERFIL_ID != 1)
                    {
                        TabTurno.Tabs.FromKey("TabTurno_Edicion").Visible = false;
                        TabTurnos.Tabs.FromKey("TabTurnos_NuevoTurno").Visible = false;
                        //TabTurnos.Tabs.FromKey("TabTurnos_CreacionExpress").Visible = false;
                        TabTurnos.Tabs.FromKey("TabTurnos_Lista").Visible = false;
                        TreeConfiguracion.Visible = false;
                        TreeConfiguracion.Find("WF_ReorganizarEmpleados.aspx").Hidden = true;
                        ///Validar Permisos
                        /* TabEmpleados.Tabs.FromKey("TabEmpleados_Importar").Visible = false;
                         TabEmpleados.Tabs.FromKey("TabEmpleados_Empleados").Visible = false;
                         TabEmpleados.Tabs.FromKey("TabEmpleados_Nuevo").Visible = false;
                         TabEmpleados.Tabs.FromKey("TabEmpleados_Herramientas").Visible = false;*/

                        TabAgrupacion.Tabs.FromKey("TabAgrupacion_Nuevo").Visible = false;
                        TabAgrupacion.Tabs.FromKey("TabAgrupacion_Empleados").Visible = false;
                        TabAgrupacion.Tabs.FromKey("TabAgrupacion_Importar").Visible = false;
                        TabAgrupacion.Tabs.FromKey("TabAgrupacion_Supervisores").Visible = false;
                        TabEmpleado.Tabs.FromKey("TabEmpleado_PermisosAcceso").Visible = false;
                        TabEmpleado.Tabs.FromKey("TabEmpleado_Edicion").Visible = false;


                        TreeUtilerias.Visible = false;
                        TreeUtilerias.Find("WF_Log.aspx").Hidden = true;
                    }
                    if (Sesion.SUSCRIPCION_ID != 1)
                    {
                        //                    TreeConfiguracion.Find("WF_Terminales.aspx").Hidden = true;
                        TreeConfiguracion.Find("WF_Usuarios.aspx").Hidden = true;
                        TreeConfiguracion.Find("WF_ConfigSuscripcion.aspx").Hidden = true;
                        TreeConfiguracion.Find("WF_Suscripciones.aspx").Hidden = true;
                        TreeConfiguracion.Find("WF_Sitios.aspx").Hidden = true;
                        while (TabConfiguracionAdv.Tabs.Count > 1)
                            TabConfiguracionAdv.Tabs.RemoveAt(1);
                    }
                    if (Sesion.PERFIL_ID != 1 && Sesion.PERFIL_ID != 2 && Sesion.PERFIL_ID != 4 && Sesion.PERFIL_ID != 5)
                    {
                        ListBar.Groups.FromKey("Turnos").Enabled = false;
                        ListBar.Groups.FromKey("Utilerias").Enabled = false;
                        ListBar.Groups.FromKey("Configuracion").Enabled = false;
                        ///Validar Permisos
                        /* TabEmpleados.Tabs.FromKey("TabEmpleados_Nuevo").Visible = false;
                         TabEmpleados.Tabs.FromKey("TabEmpleados_Herramientas").Visible = false;
                         TabEmpleados.Tabs.FromKey("TabEmpleados_HorasExtras").Visible = false;
                         TabEmpleados.Tabs.FromKey("TabEmpleados_TareasPendientes").Visible = false;*/

                        TabAgrupacion.Tabs.FromKey("TabAgrupacion_Empleados").Visible = false;
                        TabAgrupacion.Tabs.FromKey("TabAgrupacion_Importar").Visible = false;
                        TabAgrupacion.Tabs.FromKey("TabAgrupacion_HorasExtras").Visible = false;
                        TabAgrupacion.Tabs.FromKey("TabAgrupacion_Nuevo").Visible = false;
                        TabAgrupacion.Tabs.FromKey("TabAgrupacion_Supervisores").Visible = false;

                        TabEmpleado.Tabs.FromKey("TabEmpleado_HorasExtras").Visible = false;
                        TabEmpleado.Tabs.FromKey("TabEmpleado_AsistenciaAnual").Visible = false;
                        TabEmpleado.Tabs.FromKey("TabEmpleado_Edicion").Visible = false;
                        TabEmpleado.Tabs.FromKey("TabEmpleado_PermisosAcceso").Visible = false;

                        TreeConfiguracion.Find("WF_Terminales.aspx").Hidden = true;
                    }
                }
                LogSeguimiento("eClock.Cargando");
            }
            catch (Exception ex)
            {
                CIsLog2.AgregaError("eClock.aspx.cs-PageLoad()", ex);
            }
        }
        protected Node AgregaEmpleado(Node Nodo, string Texto, string Tag)
        {
            Node NNodo = Nodo.Nodes.Add(Texto, Tag);
            //        NNodo.SelectedImageUrl = NNodo.ImageUrl = "~/Imagenes/Iconos/Empleado16.png";
            NNodo.ShowExpand = false;
            NNodo.Expanded = false;
            return NNodo;
        }

        protected void AgregaEmpleado(Node Nodo, DS_eClock.EC_PERSONASRow Empleado)
        {
            string Nombre = "";
            if (!Empleado.IsPERSONA_NOMBRENull())
                Nombre = Empleado.PERSONA_NOMBRE;
            AgregaEmpleado(Nodo, Nombre + "(" + Empleado.PERSONA_LINK_ID.ToString() + ")", Empleado.PERSONA_ID.ToString());
        }
        protected void AgregaEmpleado(Node Nodo, int PersonaID, int PersonaLinkID, string Nombre)
        {
            AgregaEmpleado(Nodo, Nombre + "(" + PersonaLinkID.ToString() + ")", PersonaID.ToString());
        }
        protected Node AgregaAgrupacion(Node Nodo, string Agrupacion)
        {
            string[] SNodos = Agrupacion.Split(new char[] { '|' });
            Node NNodo = Nodo.Nodes.Add(SNodos[SNodos.Length - 1], Agrupacion);
            //        NNodo.SelectedImageUrl = NNodo.ImageUrl = "~/Imagenes/Iconos/Agrupacion16.png";

            NNodo.ShowExpand = true;
            return NNodo;
        }
        void CargaEmpleados(Node Nodo, string TextoAgrupacion)
        {
            DataSet DSPersonas = null;
            string Qry = "";

            if (TextoAgrupacion == "")
                Qry = "SELECT        EC_PERSONAS.PERSONA_ID, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, " +
                    "EC_PERSONAS.SUSCRIPCION_ID " +
                    "FROM            EC_PERSONAS INNER JOIN " +
                    "EC_PERMISOS_SUSCRIP ON EC_PERSONAS.SUSCRIPCION_ID = EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID " +
                    "WHERE        (EC_PERSONAS.PERSONA_BORRADO = 0) AND (EC_PERSONAS.AGRUPACION_NOMBRE = '' OR " +
                    "EC_PERSONAS.AGRUPACION_NOMBRE = '|' OR " +
                    "EC_PERSONAS.AGRUPACION_NOMBRE IS NULL) AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Sesion.USUARIO_ID + ") " +
                    "ORDER BY EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.PERSONA_LINK_ID";
            else
            {
                if (TextoAgrupacion[TextoAgrupacion.Length - 1] != '|')
                    TextoAgrupacion += "|";

                Qry =
                    "SELECT        EC_PERSONAS.PERSONA_ID, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.AGRUPACION_NOMBRE, " +
                    "EC_PERSONAS.SUSCRIPCION_ID " +
                    "FROM            EC_PERSONAS INNER JOIN " +
                    "EC_USUARIOS_PERMISOS ON EC_PERSONAS.SUSCRIPCION_ID = EC_USUARIOS_PERMISOS.SUSCRIPCION_ID " +
                    "WHERE        (EC_PERSONAS.PERSONA_BORRADO = 0) AND (EC_PERSONAS.AGRUPACION_NOMBRE like '" + TextoAgrupacion + "%') AND  " +
                    "(EC_PERSONAS.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO + '%') AND (EC_USUARIOS_PERMISOS.USUARIO_ID = " + Sesion.USUARIO_ID + ") AND  " +
                    "(EC_PERSONAS.SUSCRIPCION_ID = " + Sesion.SUSCRIPCION_ID + ") " +
                    "ORDER BY EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.PERSONA_LINK_ID ";
                if (CeC_BD.EsOracle)
                    Qry = Qry.Replace("+", "||");
            }
            DSPersonas = (DataSet)CeC_BD.EjecutaDataSet(Qry);

            if (DSPersonas != null && DSPersonas.Tables.Count > 0 && DSPersonas.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow Fila in DSPersonas.Tables[0].Rows)
                {
                    AgregaEmpleado(Nodo, CeC.Convierte2Int(Fila["PERSONA_ID"]), CeC.Convierte2Int(Fila["PERSONA_LINK_ID"]), CeC.Convierte2String(Fila["PERSONA_NOMBRE"]));
                }
            }

            /*CeC_T_Inc_Acceso Acc = new CeC_T_Inc_Acceso(Sesion);
            Acc.Guarda(Sesion);*/
        }

        Node CreaNodo(Node Nodo, string Agrupacion)
        {
            if (Agrupacion.Length > 2)
                Agrupacion = Agrupacion.Substring(1, Agrupacion.Length - 2);
            else
                Agrupacion = "";
            string[] SNodos = Agrupacion.Split(new char[] { '|' });
            Node NuevoNodo = Nodo;
            string Tag = "";
            bool EsPrimero = true;
            foreach (string SNodo in SNodos)
            {
                /*if (SNodo.Length < 1)
                    continue;*/

                Tag += "|" + SNodo;
                Node TNodo = NuevoNodo.Nodes.Search(SNodo, Tag, null, false);
                if (TNodo == null)
                {
                    NuevoNodo = AgregaAgrupacion(NuevoNodo, Tag);
                }
                else
                    NuevoNodo = TNodo;

            }
            return NuevoNodo;
        }

        void CargaAgrupacionesAgil(Node Nodo)
        {
            
            string Qry = "SELECT  EC_PERSONAS.PERSONA_ID, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE,  " +
                    "EC_PERSONAS.AGRUPACION_NOMBRE, EC_PERSONAS.SUSCRIPCION_ID " +
                    "FROM         EC_PERSONAS INNER JOIN " +
                    "EC_USUARIOS_PERMISOS ON EC_PERSONAS.SUSCRIPCION_ID = EC_USUARIOS_PERMISOS.SUSCRIPCION_ID " +
                    "WHERE     (EC_PERSONAS.PERSONA_BORRADO = 0) AND (EC_PERSONAS.AGRUPACION_NOMBRE LIKE EC_USUARIOS_PERMISOS.USUARIO_PERMISO "+CeC_BD.Concatenador+" '%') AND " +
                    "(EC_USUARIOS_PERMISOS.USUARIO_ID = "+Sesion.USUARIO_ID+") " +
                    "ORDER BY EC_PERSONAS.AGRUPACION_NOMBRE, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS.PERSONA_LINK_ID";
            DataSet DSPersonas = (DataSet)CeC_BD.EjecutaDataSet(Qry);
            string AgrupacionAnterior= "<Anterior>";
            if (DSPersonas != null && DSPersonas.Tables.Count > 0 && DSPersonas.Tables[0].Rows.Count > 0)
            {
                Node NodoEmpleado = Nodo;
                foreach (DataRow Fila in DSPersonas.Tables[0].Rows)
                {
                    string Agrupacion = CeC.Convierte2String(Fila["AGRUPACION_NOMBRE"]);
                    if (AgrupacionAnterior != Agrupacion)
                        NodoEmpleado = CreaNodo(Nodo, Agrupacion);
                    AgregaEmpleado(NodoEmpleado, CeC.Convierte2Int(Fila["PERSONA_ID"]), CeC.Convierte2Int(Fila["PERSONA_LINK_ID"]), CeC.Convierte2String(Fila["PERSONA_NOMBRE"]));
                }
            }
        }
        void CargaAgrupaciones(Node Nodo)
        {
            DS_eClock.EC_AGRUPACIONESDataTable DT = new DS_eClock.EC_AGRUPACIONESDataTable();
            DS_eClockTableAdapters.EC_AGRUPACIONESTableAdapter TA = new DS_eClockTableAdapters.EC_AGRUPACIONESTableAdapter();
            DT = null;
            try { DT = TA.GetDataByUsuario(Sesion.USUARIO_ID); }
            catch { }

            if (DT == null || DT.Rows.Count <= 0)
            {

                try
                {
                    if (CeC_BD.EsOracle)
                        DT = TA.GetDataByUsuarioPermisoOracle(Sesion.USUARIO_ID);
                    else
                        DT = TA.GetDataByUsuarioPermiso(Sesion.USUARIO_ID);
                }
                catch { }

            }
            //DT = TA.GetDataByUsuarioPermiso(Sesion.USUARIO_ID);
            if (DT != null && DT.Rows.Count > 0)
            {
                string Agrupacion = "";
                foreach (DS_eClock.EC_AGRUPACIONESRow Fila in DT)
                {
                    Node NodoAgrupacion = CreaNodo(Nodo, Fila.AGRUPACION_NOMBRE);

                    CargaEmpleados(NodoAgrupacion, Fila.AGRUPACION_NOMBRE);
                }
            }
        }
        bool QuitaVacios(Node Nodo)
        {
            bool Borrar = true;
            try
            {
                foreach (Node tNodo in Nodo.Nodes)
                {
                    if (QuitaVacios(tNodo))
                        tNodo.Hidden = true;
                    else
                        Borrar = false;
                }
            }
            catch { }
            if (CeC.Convierte2Int(Nodo.Tag) > 0)
                return false;
            return Borrar;
        }
        protected void ActualizaEmpleados()
        {
            LogSeguimiento("ActualizaEmpleados Inicio");
            Node Maestro = TreeEmpleados.Nodes.Add("Todos los Empleados");
            Maestro.Tag = "MAESTRO";
            if (CeC_BD.EsOracle)
                CeC_BD.EjecutaComando("UPDATE EC_USUARIOS_PERMISOS SET USUARIO_PERMISO = '|' where USUARIO_PERMISO is null");
            CargaAgrupacionesAgil(Maestro);
            //CargaAgrupaciones(Maestro);
            //CargaEmpleados(Maestro, "");
            if (!Sesion.ConfiguraSuscripcion.MostrarAgrupacionesVacias)
                QuitaVacios(Maestro);
            Maestro.Selected = true;
            Maestro.Expanded = true;
            LogSeguimiento("ActualizaEmpleados Fin");
        }
        string ObtenPath(Node Nodo)
        {
            if (Nodo == null)
                return "";
            if (Nodo.Tag.ToString() == "MAESTRO")
                return "";
            return ObtenPath(Nodo.Parent) + "|" + Nodo.Text;
        }
        bool EsAgrupacion(Node Node)
        {
            try
            {
                if (Convert.ToInt32(Node.Tag) > 0)
                    return false;
            }
            catch { }
            return true;
        }
        protected void ActualizaTerminales()
        {
            Node Maestro = TreeConfiguracion.Nodes.Search("Terminales", "WF_Terminales.aspx", null, false);
            DataSet DS = CeC_Terminales.ObtenTerminalesCatalogo(Sesion.SUSCRIPCION_ID);
            if (DS == null)
                return;
            DataTable DT = DS.Tables[0];
            if (DT != null && DT.Rows.Count > 0)
            {
                string Agrupacion = "";
                foreach (DataRow Fila in DT.Rows)
                {

                    Maestro.Nodes.Add(Fila["TERMINAL_NOMBRE"].ToString(), "WF_TerminalesPersonas.aspx?Parametros=" + Fila["TERMINAL_ID"].ToString());
                }
            }
        }

        protected void ActualizaTurnos()
        {
            Node Maestro = TreeTurnos.Nodes.Add("Todos los Turnos");
            Maestro.Tag = "MAESTRO";

            Maestro.Selected = true;
            Maestro.Expanded = true;
            DataSet DS = CeC_Turnos.ObtenTurnosDS(Sesion.SUSCRIPCION_ID);
            if (DS == null)
                return;
            DataTable DT = DS.Tables[0];
            if (DT != null && DT.Rows.Count > 0)
            {
                string Agrupacion = "";
                foreach (DataRow Fila in DT.Rows)
                {
                    Maestro.Nodes.Add(Fila["TURNO_NOMBRE"].ToString(), Fila["TURNO_ID"].ToString());
                }
            }
        }

        protected void TreeEmpleados_NodeDropped(object sender, WebTreeNodeDroppedEventArgs e)
        {
            char[] delimiterChars = { '(', ')' };
            string[] words = e.SourceData.Split(delimiterChars);
            try
            {
                int Persona_Link_ID = Convert.ToInt32(words[1]);
                int Persona_ID = CeC_Empleados.ObtenPersonaID(Persona_Link_ID, Sesion.USUARIO_ID);
                Node Nodo = TreeEmpleados.Find(Persona_ID.ToString());
                if (Nodo != null)
                {
                    Node NodoDestino = e.Node;

                    if (NodoDestino.Tag.ToString()[0] != '|' && NodoDestino.Tag.ToString()[0] != 'M')
                        NodoDestino = NodoDestino.Parent;
                    if (CeC_Empleados.AsignaAgrupacion(Persona_Link_ID, NodoDestino.Tag.ToString(), Sesion.USUARIO_ID))
                    {
                        Nodo.Parent.Nodes.Remove(Nodo);
                        AgregaEmpleado(NodoDestino, Nodo.Text, Nodo.Tag.ToString());
                    }
                }
            }
            catch { }
        }
        protected void BtnAgrupacionAgregar_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Node Seleccionado = TreeEmpleados.SelectedNode;
            if (Seleccionado == null)
                Seleccionado = TreeEmpleados.Nodes[0];

            if (!EsAgrupacion(Seleccionado))
                Seleccionado = Seleccionado.Parent;

            Node Nodo = AgregaAgrupacion(Seleccionado, TbxAgrupacion.Text);
            string Path = ObtenPath(Nodo);
            CeC_Agrupaciones.AgregaAgrupacion(Sesion.SUSCRIPCION_ID, Path);
            Nodo.Tag = Path;
        }

        protected void BtnCambiarNombre_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Node Seleccionado = TreeEmpleados.SelectedNode;
            if (Seleccionado == null)
                return;


            if (!EsAgrupacion(Seleccionado))
            {
                Seleccionado.Text = TbxAgrupacion.Text;
                CeC_Personas.GuardaValor(Convert.ToInt32(Seleccionado.Tag), "NOMBRE_COMPLETO", TbxAgrupacion.Text, Sesion.SESION_ID);
                return;
            }
            string Tag = Seleccionado.Tag.ToString();
            int P = Tag.LastIndexOf('|');

            string NTag = Tag.Substring(0, P + 1) + TbxAgrupacion.Text;
            CeC_Agrupaciones.CambiaNombreAgrupacion(Sesion.USUARIO_ID, Tag, NTag);
            Seleccionado.Text = TbxAgrupacion.Text;
            //Nodo.Tag = Path;
        }
        bool AgregaNodosBusqueda(Node Maestro, DataSet Datos, string Campo)
        {
            try
            {
                string ValorAnterior = "";
                Node Nodo = null;
                foreach (DataRow DR in Datos.Tables[0].Rows)
                {
                    string Valor = DR[0].ToString();
                    if (Valor != ValorAnterior)
                    {
                        Nodo = Maestro.Nodes.Add(Valor, "S" + Campo + "='" + Valor + "'");
                        ValorAnterior = Valor;
                    }
                    AgregaEmpleado(Nodo, DR[1].ToString() + "(" + DR[2].ToString() + ")", DR[3].ToString());


                }
                return true;
            }
            catch
            {
                //CIsLog2.AgregaError(ex);
            }
            return false;
        }
        bool BuscaCampos(Node Maestro, string Texto)
        {
            Texto = Texto.Trim();
            string SCampos = "EC_PERSONAS.PERSONA_LINK_ID, PERSONA_NOMBRE, " + CeC_Campos.ObtenListaCamposTEGrid() + ",PERSONA_EMAIL, TURNO_NOMBRE, AGRUPACION_NOMBRE";
            string[] Campos = SCampos.Split(new char[] { ',' });
            string CampoID = "PERSONA_LINK_ID";
            foreach (string Campo in Campos)
            {
                try
                {
                    string SQL = "";
                    if (!CeC_BD.EsOracle)
                        SQL = " COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI ";
                    string SCampo = Campo.Trim();
                    string Condicion = SCampo + " like '%" + Texto + "%' ";
                    if (SCampo == "PERSONA_LINK_ID")
                        continue;
                    if (SCampo == "PERSONA_NOMBRE")
                        continue;
                    if (SCampo == "PERSONA_ID")
                        continue;

                    string Qry = "SELECT " + SCampo + ",PERSONA_NOMBRE,EC_PERSONAS.PERSONA_LINK_ID,EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS, EC_PERSONAS_DATOS, EC_TURNOS WHERE EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DATOS.PERSONA_ID AND " +
                        "EC_TURNOS.TURNO_ID = EC_PERSONAS.TURNO_ID AND EC_PERSONAS.PERSONA_ID IN (" + CeC_Agrupaciones.ObtenSQLPersonaIDsPermisos(Sesion.USUARIO_ID)
                        + " )  AND " + SCampo + " like '%" + Texto + "%' " + SQL + " ORDER BY " + SCampo + ", PERSONA_NOMBRE";
                    string CampoNombre = "";
                    if (CampoID.Length > 0)
                    {
                        CampoNombre = CeC_Campos.ObtenEtiqueta(CampoID);
                        CampoID = "";
                    }
                    else
                        CampoNombre = CeC_Campos.ObtenEtiqueta(SCampo);

                    DataSet DS = (DataSet)CeC_BD.EjecutaDataSet(Qry);

                    if (DS != null && DS.Tables[0].Rows.Count > 0)
                    {
                        Node Nodo = Maestro.Nodes.Add(CampoNombre, "S" + Condicion);

                        AgregaNodosBusqueda(Nodo, DS, Campo);
                    }
                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
            return true;
        }
        protected void Lbtn_Buscar_Click(object sender, EventArgs e)
        {
            if (Tbx_Busqueda.Text.Length <= 0)
                return;
            int Nodos = TreeEmpleados.Nodes.Count;
            TreeEmpleados.Nodes[0].Expanded = false;
            Node Maestro;
            if (Nodos == 1)
                Maestro = TreeEmpleados.Nodes.Add("Resultados de la busqueda");
            else
                Maestro = TreeEmpleados.Nodes[1];
            Maestro.Tag = "|||";
            Maestro.Selected = true;
            Maestro.Expanded = true;
            Maestro.Nodes.Clear();
            BuscaCampos(Maestro, Tbx_Busqueda.Text);
        }
        protected void Tbx_Busqueda_EnterKeyPress(object sender, EventArgs e)
        {
            Lbtn_Buscar_Click(sender, e);
        }
        protected void Btn_Principal_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.Redirige("eClock.aspx");
        }
        protected void Btn_Salir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.CierraSesion();
            Sesion.Redirige("WF_Login.aspx");
        }
    }
}