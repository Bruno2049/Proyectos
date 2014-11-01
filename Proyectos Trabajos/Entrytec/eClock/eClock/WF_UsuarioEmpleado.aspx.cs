using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_UsuarioEmpleado.
    /// </summary>
    public partial class WF_UsuarioEmpleado : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbDataAdapter DA_Personas;
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected DS_UsuariosxE dS_UsuariosxE1;
        CeC_Sesion Sesion;

        private void Habilitarcontroles()
        {
            UltraWebGrid1.Visible = false;
            WebImageButton2.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página
            Sesion = CeC_Sesion.Nuevo(this);

            Sesion.TituloPagina = "Creación de un Usuario apartir de un Empleado";
            Sesion.DescripcionPagina = "Seleccione un Empleado para la creación de un Usuario";

            UltraWebGrid1.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            CeC_Grid.AplicaFormato(UltraWebGrid1);

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Usuarios0Nuevo_Apartir_Empleado, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            if (!IsPostBack)
            {
                if (Sesion.WF_EmpleadosFil_Qry.Length < 1)
                {

                    Sesion.WF_EmpleadosFil(false, false, false,
                        "Muestra Resultados", "Filtro-Usuarios Apartir de Empleados",
                        "WF_UsuarioEmpleado.aspx", "Realiza el filtro para la seleccion de Empleados", false, true, false);
                    return;
                }
                else
                {
                    Sesion.WF_UsuarioEmpleado_QRY = Sesion.WF_EmpleadosFil_Qry_Temp = Sesion.WF_EmpleadosFil_Qry;
                    Sesion.WF_EmpleadosFil_Qry = "";
                    DA_Personas.SelectCommand.CommandText = DA_Personas.SelectCommand.CommandText.Replace("ORDER", " AND EC_PERSONAS.PERSONA_ID IN (" + Sesion.WF_EmpleadosFil_Qry_Temp + ") ORDER ");
                    DA_Personas.Fill(dS_UsuariosxE1.EC_PERSONAS);
                }
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Creación de Usuario desde Empleado", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            }
            for (int i = 0; i < dS_UsuariosxE1.EC_PERSONAS.Rows.Count; i++)
            {
                DS_UsuariosxE.EC_PERSONASRow FilaUsuarios = (DS_UsuariosxE.EC_PERSONASRow)dS_UsuariosxE1.EC_PERSONAS.Rows[i];
                FilaUsuarios.PERSONA_CLAVE = ExtraerClave(Convert.ToString(FilaUsuarios.PERSONA_NOMBRE));
            }
            if (!IsPostBack)
                UltraWebGrid1.DataBind();
        }

        public string ExtraerClave(string NombreEmpleados)
        {
            string ret = CeC_BD.EjecutaEscalarString("Select usuario_clave from EC_USUARIOS where usuario_nombre like '%" + NombreEmpleados + "%'");

            if (ret.Length > 0)
                return ret;
            return "";
        }

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.DA_Personas = new System.Data.OleDb.OleDbDataAdapter();
            this.dS_UsuariosxE1 = new DS_UsuariosxE();
            ((System.ComponentModel.ISupportInitialize)(this.dS_UsuariosxE1)).BeginInit();
            this.WebImageButton2.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WebImageButton2_Click);
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT PERSONA_ID, PERSONA_LINK_ID, TIPO_PERSONA_ID, SUSCRIPCION_ID, PERSONA_NOMBRE, " +
                "PERSONA_EMAIL, PERSONA_BORRADO FROM EC_PERSONAS WHERE (PERSONA_BORRADO = 0) ORD" +
                "ER BY PERSONA_LINK_ID";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // DA_Personas
            // 
            this.DA_Personas.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_Personas.InsertCommand = this.oleDbInsertCommand1;
            this.DA_Personas.SelectCommand = this.oleDbSelectCommand1;
            this.DA_Personas.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																				  new System.Data.Common.DataColumnMapping("PERSONA_LINK_ID", "PERSONA_LINK_ID"),
																																																				  new System.Data.Common.DataColumnMapping("TIPO_PERSONA_ID", "TIPO_PERSONA_ID"),
																																																				  new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																				  new System.Data.Common.DataColumnMapping("PERSONA_NOMBRE", "PERSONA_NOMBRE"),
																																																				  new System.Data.Common.DataColumnMapping("PERSONA_EMAIL", "PERSONA_EMAIL"),
																																																				  new System.Data.Common.DataColumnMapping("PERSONA_BORRADO", "PERSONA_BORRADO")})});
            this.DA_Personas.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // dS_UsuariosxE1
            // 
            this.dS_UsuariosxE1.DataSetName = "DS_UsuariosxE";
            this.dS_UsuariosxE1.Locale = new System.Globalization.CultureInfo("es-MX");
            ((System.ComponentModel.ISupportInitialize)(this.dS_UsuariosxE1)).EndInit();

        }
        #endregion

        private bool compUsuario(string strUsuario)
        {
            int ret = 0;
            ret = CeC_BD.EjecutaEscalarInt("SELECT EC_USUARIOS.USUARIO_ID FROM EC_USUARIOS WHERE EC_USUARIOS.USUARIO_NOMBRE ='" + strUsuario + "'");

            if (ret > 0)
                return true;
            return false;
        }

        private string StringReversa(string Cadena)
        {
            string letra = "";
            string Cadenavolteada = "";

            for (int i = Cadena.Length - 1; i >= 0; i--)
            {
                letra = Cadena.Substring(i, 1);
                if (letra == " ")
                {
                    break;
                }
                else
                {
                    Cadenavolteada = letra + Cadenavolteada;
                }
            }
            return Cadenavolteada.ToLower();
        }

        private void WebImageButton2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LError.Text = "";
            LCorrecto.Text = "";

            try
            {
                if (UltraWebGrid1.Rows.Count <= 0)
                {
                    LError.Text = "No hay registro al que se le puedan asignar los cambios";
                    return;
                }

                for (int i = 0; i < UltraWebGrid1.Rows.Count; i++)
                {
                    int Persona_ID = Convert.ToInt32(UltraWebGrid1.Rows[i].Cells[0].Text);
                    string Nombre_usuario = UltraWebGrid1.Rows[i].Cells[1].Text;
                    string Nombre_UsuarioSimple = UltraWebGrid1.Rows[i].Cells[4].Text;
                    string CorreoE = UltraWebGrid1.Rows[i].Cells[5].Text;
                    Nombre_usuario = StringReversa(Nombre_usuario);

                    if ((!CBListado.Checked && UltraWebGrid1.Rows[i].Selected) || !!CBListado.Checked)
                    {
                        if (!compUsuario(Nombre_usuario))
                        {
                            int U_ID = CeC_Autonumerico.GeneraAutonumerico("EC_USUARIOS", "USUARIO_ID");
                            //string  Usuario_Us = Nombre_usuario + U_ID.ToString("0000");
                            //string Usuario_Us = Convert.ToString(CeC_BD.EjecutaEscalar("Select persona_link_id from EC_PERSONAS where persona_nombre LIKE '%"+Nombre_UsuarioSimple+"%'" ));
                            Random Clave = new Random();
                            int ClaveInt = Clave.Next(1000, 5000);
                            string Persona_Nombre = CeC_BD.ObtenPersonaNombre(Persona_ID);
                            string Persona_Email = CeC_BD.ObtenPersonaEMail(Persona_ID);

                            CeC_BD.EjecutaComando("INSERT INTO EC_USUARIOS(USUARIO_ID,PERFIL_ID,USUARIO_USUARIO,USUARIO_NOMBRE,USUARIO_DESCRIPCION,USUARIO_CLAVE, USUARIO_EMAIL,USUARIO_BORRADO) VALUES ("
                                + U_ID + ",2,'" + Nombre_usuario + "','" +
                                Persona_Nombre + "','" + "'," + ClaveInt.ToString() + ", '" + Persona_Email + "',0)");
                            //Se obtiene el g
                            // string Grupo = CeC_BD.EjecutaEscalarString("SELECT " + CeC_Config.CampoGrupo1 + " FROM EC_PERSONAS_DATOS WHERE " + CeC_Campos.CampoTE_Llave + " = " + Nombre_usuario);
                            int Grupo_ID = CeC_BD.ObtenPersonaSUSCRIPCION_ID(Persona_ID);
                            if (Grupo_ID > 0)
                                CeC_BD.EjecutaComando("INSERT INTO EC_PERMISOS_SUSCRIP (USUARIO_ID, SUSCRIPCION_ID) VALUES(" + U_ID + ", " + Grupo_ID + ")");
                        }
                    }
                }
                LCorrecto.Text = "Registros Modificados Satisfactoriamente";
                //Especialmente para asignarle un grupo default Centro de costos a los usuarios
                RefrescarGrid();
                return;
            }
            catch (Exception ex)
            {
                LError.Text = "Error: " + ex.Message;
                return;
            }
            LError.Text = "Debes de seleccionar una fila";
        }

        private void RefrescarGrid()
        {
            DA_Personas.SelectCommand.CommandText = DA_Personas.SelectCommand.CommandText.Replace("ORDER", " AND EC_PERSONAS.PERSONA_ID IN (" + Sesion.WF_EmpleadosFil_Qry_Temp + ") ORDER ");
            DA_Personas.Fill(dS_UsuariosxE1.EC_PERSONAS);

            for (int i = 0; i < dS_UsuariosxE1.EC_PERSONAS.Rows.Count; i++)
            {
                DS_UsuariosxE.EC_PERSONASRow FilaUsuarios = (DS_UsuariosxE.EC_PERSONASRow)dS_UsuariosxE1.EC_PERSONAS.Rows[i];
                FilaUsuarios.PERSONA_CLAVE = ExtraerClave(Convert.ToString(FilaUsuarios.PERSONA_NOMBRE));
            }
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns[0].Hidden = true;
        }

        protected void UltraWebGrid1_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
        {
            Sesion = CeC_Sesion.Nuevo(this);
            try
            {
                if (Sesion.WF_UsuarioEmpleado_QRY.Length > 0)
                {
                    DA_Personas.SelectCommand.CommandText = DA_Personas.SelectCommand.CommandText.Replace("ORDER", " AND EC_PERSONAS.PERSONA_ID IN (" + Sesion.WF_UsuarioEmpleado_QRY + ") ORDER ");
                    DA_Personas.Fill(dS_UsuariosxE1.EC_PERSONAS);
                }
                UltraWebGrid1.DataSource = dS_UsuariosxE1;
                UltraWebGrid1.Columns[0].Hidden = true;
            }
            catch (Exception ex) { }
        }

        protected void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            UltraWebGrid1.Columns[0].Hidden = true;
            CeC_Grid.AplicaFormato(UltraWebGrid1, false, false, false, false);
        }
    }
}