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
using System.Data.OleDb;
using System.Xml;

namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_AsignarGrupoUsuarios.
    /// </summary>
    public partial class WF_AsignarGrupoUsuarios : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbDataAdapter DA_Usuarios;
        protected DS_AsignaGrupoUsuario dS_AsignaGrupoUsuario1;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
        protected System.Data.OleDb.OleDbDataAdapter DA_link_Grupos;

        private int respuesta = 0;
        CeC_Sesion Sesion;

        private void Habilitarcontroles()
        {
            Panel1.Visible = false;
            Grid.Visible = false;
            WebImageButton2.Visible = false;
            WebImageButton1.Visible = false;
            BBRegresar.Visible = false;
        }

        private void CargarGrid(int respuesta, bool dataBindforzado)
        {
            DataSet DS = new DataSet("DS_Tabla1");
            DataTable DT = new DataTable("DT_1");
            DataTable DT2 = new DataTable("DT_2");
            DataRow DR;
            DataRow DR2;

            DT.Columns.Add("USUARIO_ID", System.Type.GetType("System.Decimal"));
            DT.Columns.Add("PERFIL_ID", System.Type.GetType("System.Decimal"));
            DT.Columns.Add("USUARIO_USUARIO", System.Type.GetType("System.String"));
            DT.Columns.Add("USUARIO_NOMBRE", System.Type.GetType("System.String"));
            DT.Columns.Add("USUARIO_DESCRIPCION", System.Type.GetType("System.String"));
            DT.Columns.Add("USUARIO_CLAVE", System.Type.GetType("System.String"));
            DT.Columns.Add("USUARIO_BORRADO", System.Type.GetType("System.Decimal"));

            string query = "SELECT     USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCION, USUARIO_CLAVE, USUARIO_BORRADO " +
                            " FROM         EC_USUARIOS " +
                            " WHERE     (USUARIO_BORRADO = 0)";

            if (Conexion.State != System.Data.ConnectionState.Open)
                Conexion.Open();

            OleDbCommand Commando = new OleDbCommand(query, Conexion);
            OleDbDataReader lector;

            lector = Commando.ExecuteReader();

            while (lector.Read())
            {
                DR = DT.NewRow();
                try
                {
                    DR[0] = lector.GetValue(0);
                    DR[1] = lector.GetValue(1);
                    DR[2] = lector.GetValue(2);
                    DR[3] = lector.GetValue(3);
                    DR[4] = lector.GetValue(4);
                    DR[5] = lector.GetValue(5);
                    DR[6] = lector.GetValue(6);
                }
                catch
                {
                }
                DT.Rows.Add(DR);
            }
            lector.Close();

            DT2.Columns.Add("USUARIO_ID", System.Type.GetType("System.Decimal"));
            DT2.Columns.Add("GRUPO_" + respuesta + "_ID", System.Type.GetType("System.Decimal"));
            DT2.Columns.Add("GRUPO_" + respuesta + "_NOMBRE", System.Type.GetType("System.String"));

            string QRY = "SELECT     EC_USUARIOS_GRUPOS_" + respuesta + ".USUARIO_ID, EC_USUARIOS_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID, eC_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_NOMBRE " +
                " FROM EC_USUARIOS_GRUPOS_" + respuesta + ", eC_GRUPOS_" + respuesta + " " +
                " WHERE (EC_USUARIOS_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID = eC_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID) And  EC_USUARIOS_GRUPOS_" + respuesta + ".USUARIO_ID in (SELECT USUARIO_ID FROM EC_USUARIOS WHERE (USUARIO_BORRADO = 0))  ";

            if (Conexion.State != System.Data.ConnectionState.Open)
                Conexion.Open();

            OleDbCommand Commando2 = new OleDbCommand(QRY, Conexion);
            OleDbDataReader Lector2;

            Lector2 = Commando2.ExecuteReader();

            while (Lector2.Read())
            {
                DR2 = DT2.NewRow();
                try
                {
                    DR2[0] = Lector2.GetValue(0);
                    DR2[1] = Lector2.GetValue(1);
                    DR2[2] = Lector2.GetValue(2);
                }
                catch
                {
                }
                DT2.Rows.Add(DR2);
            }
            Lector2.Close();

            DS.Tables.Add(DT2);
            DS.Tables.Add(DT);

            DataColumn ColumnaPadre = DS.Tables["DT_1"].Columns["USUARIO_ID"];
            DataColumn ColumnaHijo = DS.Tables["DT_2"].Columns["USUARIO_ID"];

            DataRelation relacion = new System.Data.DataRelation("Mike2", ColumnaPadre, ColumnaHijo);
            DS.Relations.Add(relacion);

            Grid.DataSource = DS;
            Grid.DataMember = DS.Tables["DT_1"].TableName.ToString();

            if (dataBindforzado)
            {
                Grid.DataBind();
                Configuracion();
            }
            else
            {
                if (!IsPostBack)
                {
                    Grid.DataBind();
                    Configuracion();
                }
            }
            Conexion.Close();
        }

        private void Configuracion()
        {
            Grid.Bands[0].Columns[0].Hidden = true;
            Grid.Bands[0].Columns[1].Hidden = true;
            Grid.Bands[0].Columns[2].Header.Caption = "Usuario";
            Grid.Bands[0].Columns[3].Header.Caption = "Nombre";
            Grid.Bands[0].Columns[3].Width = System.Web.UI.WebControls.Unit.Parse("250px");
            Grid.Bands[0].Columns[4].Header.Caption = "Descripción";
            Grid.Bands[0].Columns[4].Width = System.Web.UI.WebControls.Unit.Parse("250px");
            Grid.Bands[0].Columns[5].Hidden = true;
            Grid.Bands[0].Columns[6].Hidden = true;

            Grid.Bands[1].Columns[0].Hidden = true;
            Grid.Bands[1].Columns[1].Hidden = true;
            Grid.Bands[1].Columns[2].Header.Caption = "Nombre del Grupo";
            Grid.Bands[1].Columns[2].Width = System.Web.UI.WebControls.Unit.Parse("200px");
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página
            Sesion = CeC_Sesion.Nuevo(this);
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;

            Sesion.TituloPagina = "Asignar Grupo a Usuarios";
            Sesion.DescripcionPagina = "Seleccione un usuario para asignarle un grupo";

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Usuarios0Asignar_Grupo_Usuarios, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            if (!IsPostBack)
            {
                Sesion.WF_Grupos1ControlCAdenaControl = "";
                //Grid.DataBind();
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asignación de Grupo a Usuarios", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            }

            string ret = Request.QueryString["Grupo"];
            int resultado = Sesion.ConvertforzadoInt32(ret);

            if (resultado <= 0 || resultado > 3)
                Sesion.Redirige("WF_Main.aspx");

            Sesion.WF_QueryString_Valor = resultado;
            respuesta = resultado;

            CargarGrid(respuesta, false);
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
            this.Conexion = new System.Data.OleDb.OleDbConnection();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.DA_Usuarios = new System.Data.OleDb.OleDbDataAdapter();
            this.dS_AsignaGrupoUsuario1 = new DS_AsignaGrupoUsuario();
            this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
            this.DA_link_Grupos = new System.Data.OleDb.OleDbDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dS_AsignaGrupoUsuario1)).BeginInit();
            this.BBRegresar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBRegresar_Click);
            this.WebImageButton2.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WebImageButton2_Click);
            this.WebImageButton1.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WebImageButton1_Click);
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT USUARIO_ID, PERFIL_ID, USUARIO_USUARIO, USUARIO_NOMBRE, USUARIO_DESCRIPCIO" +
                "N, USUARIO_CLAVE, USUARIO_BORRADO FROM EC_USUARIOS WHERE (USUARIO_BORRADO = 0)";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // DA_Usuarios
            // 
            this.DA_Usuarios.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_Usuarios.InsertCommand = this.oleDbInsertCommand1;
            this.DA_Usuarios.SelectCommand = this.oleDbSelectCommand1;
            this.DA_Usuarios.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "EC_USUARIOS", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_ID", "USUARIO_ID"),
																																																				  new System.Data.Common.DataColumnMapping("PERFIL_ID", "PERFIL_ID"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_USUARIO", "USUARIO_USUARIO"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_NOMBRE", "USUARIO_NOMBRE"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_DESCRIPCION", "USUARIO_DESCRIPCION"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_CLAVE", "USUARIO_CLAVE"),
																																																				  new System.Data.Common.DataColumnMapping("USUARIO_BORRADO", "USUARIO_BORRADO")})});
            this.DA_Usuarios.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // dS_AsignaGrupoUsuario1
            // 
            this.dS_AsignaGrupoUsuario1.DataSetName = "DS_AsignaGrupoUsuario";
            this.dS_AsignaGrupoUsuario1.Locale = new System.Globalization.CultureInfo("es-MX");
            // 
            // oleDbSelectCommand2
            // 
            this.oleDbSelectCommand2.CommandText = "SELECT EC_PERMISOS_SUSCRIP.USUARIO_ID, EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID, eC_GR" +
                "UPOS_1.SUSCRIPCION_NOMBRE FROM EC_PERMISOS_SUSCRIP, EC_SUSCRIPCION WHERE eC_USUARIO" +
                "S_GRUPOS_1.SUSCRIPCION_ID = EC_SUSCRIPCION.SUSCRIPCION_ID";
            this.oleDbSelectCommand2.Connection = this.Conexion;
            // 
            // DA_link_Grupos
            // 
            this.DA_link_Grupos.DeleteCommand = this.oleDbDeleteCommand2;
            this.DA_link_Grupos.InsertCommand = this.oleDbInsertCommand2;
            this.DA_link_Grupos.SelectCommand = this.oleDbSelectCommand2;
            this.DA_link_Grupos.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									 new System.Data.Common.DataTableMapping("Table", "EC_PERMISOS_SUSCRIP", new System.Data.Common.DataColumnMapping[] {
																																																							  new System.Data.Common.DataColumnMapping("USUARIO_ID", "USUARIO_ID"),
																																																							  new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																							  new System.Data.Common.DataColumnMapping("SUSCRIPCION_NOMBRE", "SUSCRIPCION_NOMBRE")})});
            this.DA_link_Grupos.UpdateCommand = this.oleDbUpdateCommand2;
            this.Load += new System.EventHandler(this.Page_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dS_AsignaGrupoUsuario1)).EndInit();

        }
        #endregion


        private void WebImageButton2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            char[] Caracter = new char[1];
            Caracter[0] = Convert.ToChar(",");
            string[] ItemsControl = Sesion.WF_Grupos1ControlCAdenaControl.Split(Caracter);

            LCorrecto.Text = "";
            LError.Text = "";

            try
            {
                for (int y = 0; y < Grid.Rows.Count; y++)
                {
                    int Usuario_ID = Convert.ToInt32(Grid.Rows[y].Cells[0].Value);
                    
                    if (!CBListado.Checked)
                    {
                        if (Grid.Rows[y].Selected)
                        {
                            GuardarHistorial(Usuario_ID);
                            //	CeC_BD.EjecutaComando("Delete from EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = "+Usuario_ID.ToString());
                            for (int i = 0; i < ItemsControl.Length; i++)
                            {
                                int ret = CeC_BD.EjecutaComando("UPDATE EC_USUARIOS_GRUPOS_" + respuesta + " SET GRUPO_" + respuesta + "_ID = " + ItemsControl[i] + " WHERE USUARIO_ID = " + Usuario_ID + "  AND GRUPO_" + respuesta + "_ID = " + ItemsControl[i]);

                                if (ret <= 0)
                                    CeC_BD.EjecutaComando("INSERT INTO EC_USUARIOS_GRUPOS_" + respuesta + "(USUARIO_ID,GRUPO_" + respuesta + "_ID) VALUES(" + Usuario_ID + "," + ItemsControl[i] + ")");
                            }
                        }
                    }
                    else
                    {
                        GuardarHistorial(Usuario_ID);
                        // CeC_BD.EjecutaComando("Delete from EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = "+Usuario_ID.ToString());
                        for (int i = 0; i < ItemsControl.Length; i++)
                        {
                            int ret = CeC_BD.EjecutaComando("UPDATE EC_USUARIOS_GRUPOS_" + respuesta + " SET GRUPO_" + respuesta + "_ID = " + ItemsControl[i] + " WHERE USUARIO_ID = " + Usuario_ID + "  AND GRUPO_" + respuesta + "_ID = " + ItemsControl[i]);
                            if (ret <= 0)
                                CeC_BD.EjecutaComando("INSERT INTO EC_USUARIOS_GRUPOS_" + respuesta + "(USUARIO_ID,GRUPO_" + respuesta + "_ID) VALUES(" + Usuario_ID + "," + ItemsControl[i] + ")");
                        }
                    }
                }

                //DA_Usuarios.Fill(dS_AsignaGrupoUsuario1.EC_USUARIOS);
                //DA_link_Grupos.Fill(dS_AsignaGrupoUsuario1.EC_PERMISOS_SUSCRIP);

                CargarGrid(respuesta, true);
                LCorrecto.Text = "Resgistros Modificados Satisfactoriamente";
                return;
            }
            catch (Exception ex)
            {
                LError.Text = "Error :" + ex.Message;
                return;
            }
            LError.Text = "Debes de seleccionar uno o varios usuarios";
        }

        private void WebImageButton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            /*for (int i = 0 ;i < Grid.Rows.Count; i++) codigo para el antigûo borrado
            {
                if (Grid.Rows[i].Selected)
                {
                    int Usuario_id = Convert.ToInt32(Grid.Rows[i].Cells[0].Value);
                    Sesion.WF_AsignarGrupoUsuarios_Usuario_ID = Usuario_id;
                    Sesion.Redirige("WF_AsignarGrupoUsuariosE.aspx");
                    return;
                }
            }*/
            char[] Caracters = new char[1];
            Caracters[0] = Convert.ToChar(",");
            string[] ItemsControl = Sesion.WF_Grupos1ControlCAdenaControl.Split(Caracters);

            LCorrecto.Text = "";
            LError.Text = "";
            try
            {
                for (int y = 0; y < Grid.Rows.Count; y++)
                {
                    int Usuario_ID = Convert.ToInt32(Grid.Rows[y].Cells[0].Value);
                    if (!CBListado.Checked)
                    {
                        if (Grid.Rows[y].Selected)
                        {
                            GuardarHistorial(Usuario_ID);
                            //	CeC_BD.EjecutaComando("Delete from EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = "+Usuario_ID.ToString());
                            for (int i = 0; i < ItemsControl.Length; i++)
                            {
                                int ret = CeC_BD.EjecutaComando("Delete from  EC_USUARIOS_GRUPOS_" + respuesta + "  WHERE USUARIO_ID = " + Usuario_ID + "  AND GRUPO_" + respuesta + "_ID = " + ItemsControl[i]);
                            }
                        }
                    }
                    else
                    {
                        // CeC_BD.EjecutaComando("Delete from EC_PERMISOS_SUSCRIP WHERE USUARIO_ID = "+Usuario_ID.ToString());
                        GuardarHistorial(Usuario_ID);
                        for (int i = 0; i < ItemsControl.Length; i++)
                        {
                            int ret = CeC_BD.EjecutaComando("Delete from  EC_USUARIOS_GRUPOS_" + respuesta + "  WHERE USUARIO_ID = " + Usuario_ID + "  AND GRUPO_" + respuesta + "_ID = " + ItemsControl[i]);
                        }
                    }
                }

                CargarGrid(respuesta, true);

                LCorrecto.Text = "Resgistros Modificados Satisfactoriamente";
                return;
            }
            catch (Exception ex)
            {
                LError.Text = "Error :" + ex.Message;
                return;
            }
            LError.Text = "Debes de seleccionar uno o varios usuarios";
        }

        private void BBRegresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.Redirige("WF_Main.aspx");
        }

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid, false, false, true, false);
        }

        private void GuardarHistorial(int usuarioID)
        {
            OleDbDataAdapter cmd = new OleDbDataAdapter("SELECT GRUPO_" + respuesta + "_ID FROM EC_USUARIOS_GRUPOS_" + respuesta + " WHERE USUARIO_ID = " + usuarioID , Conexion);
            DataSet DS = new DataSet();
            cmd.Fill(DS);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                int grupoID = Convert.ToInt32(DS.Tables[0].Rows[0][0]);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(Server.MapPath(@"XML_HistorialGrupoUsuarios.xml"));
                XmlElement xmlUsuario = xmldoc.CreateElement("Usuario");
                XmlAttribute xmlGrupo = xmldoc.CreateAttribute("Grupo"+respuesta);
                XmlAttribute xmlFechaCambio = xmldoc.CreateAttribute("FechaCambio");

                xmlUsuario.InnerText = usuarioID.ToString();
                xmlGrupo.InnerXml = DS.Tables[0].Rows[i][0].ToString();
                xmlFechaCambio.InnerXml = DateTime.Now.Date.ToString();

                xmlUsuario.SetAttributeNode(xmlGrupo);
                xmlUsuario.SetAttributeNode(xmlFechaCambio);
                xmldoc.DocumentElement.AppendChild(xmlUsuario);

                xmldoc.Save(Server.MapPath(@"XML_HistorialGrupoUsuarios.xml"));
            }
        }
    }
}
