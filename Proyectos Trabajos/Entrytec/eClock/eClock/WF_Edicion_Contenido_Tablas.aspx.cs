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
namespace eClock
{
    /// <summary>
    /// Descripción breve de WF_Edicion_Contenido_Tablas.
    /// </summary>
    public partial class WF_Edicion_Contenido_Tablas : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected System.Data.OleDb.OleDbDataAdapter DA_Grupos;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected DS_Eicion_Contenido dS_Eicion_Contenido1;
        CeC_Sesion Sesion;
        private int respuesta = 0;
        private string QueryReemplazo_GRID = "";
        private int Usuarios_Ligados_Grupo_Count = 0;

        private void LlenarWebcombo()
        {
            DD_Grupos.Items.Clear();
            string qry = "Select  GRUPO_" + respuesta + "_ID ,GRUPO_" + respuesta + "_NOMBRE  from eC_GRUPOS_" + respuesta + " where GRUPO_" + respuesta + "_ID > 0 AND GRUPO_" + respuesta + "_ID != " + LGrupoIdBorrado.Text + " ORDER BY GRUPO_" + respuesta + "_ID";

            if (Conexion.State != System.Data.ConnectionState.Open)
                Conexion.Open();

            if (QueryReemplazo_GRID.Length > 0)
            {
                qry = QueryReemplazo_GRID;
            }

            OleDbCommand commando = new OleDbCommand(qry, Conexion);
            OleDbDataReader lector;

            lector = commando.ExecuteReader();

            while (lector.Read())
            {
                try
                {
                    //QueryReemplazo_GRID = "Select  eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID ,eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_NOMBRE  from eC_GRUPOS_" + respuesta + ", EC_USUARIOS_GRUPOS_"+respuesta+" where eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID > 0 AND EC_USUARIOS_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID = eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID AND EC_USUARIOS_GRUPOS_"+respuesta+".USUARIO_ID = "+Sesion.USUARIO_ID+"  ORDER BY GRUPO_"+respuesta+"_ID";
                    if (Convert.ToInt32(lector.GetValue(0)) != Convert.ToInt32(LGrupoIdBorrado.Text))
                        DD_Grupos.Items.Add(new ListItem(Convert.ToString(lector.GetValue(1)), Convert.ToString(lector.GetValue(0))));
                }
                catch
                {
                    if (Conexion.State == ConnectionState.Open)
                        Conexion.Dispose();
                }
            }
            lector.Close();
            Conexion.Close();
        }

        private void LLenarcontroles(int modo)
        {
            DataSet DS = new DataSet("DSGrupos");
            DataTable DT = new DataTable("Tabla_Grupos");
            DataRow DR;

            DT.Columns.Add("Grupo_" + respuesta + "_ID", System.Type.GetType("System.Decimal"));
            DT.Columns.Add("Grupo_" + respuesta + "_NOMBRE", System.Type.GetType("System.String"));

            Grid.DataMember = "Tabla_Grupos";
            Grid.DataSource = DS;

            string qry = "Select  GRUPO_" + respuesta + "_ID ,GRUPO_" + respuesta + "_NOMBRE  from eC_GRUPOS_" + respuesta + " where GRUPO_" + respuesta + "_ID > 0 ORDER BY GRUPO_" + respuesta + "_ID";

            if (Conexion.State != System.Data.ConnectionState.Open)
                Conexion.Open();
            if (QueryReemplazo_GRID.Length > 0)
                qry = QueryReemplazo_GRID;

            OleDbCommand commando = new OleDbCommand(qry, Conexion);
            OleDbDataReader lector;

            lector = commando.ExecuteReader();

            while (lector.Read())
            {
                DR = DT.NewRow();
                try
                {
                    DR[0] = lector.GetValue(0);
                }
                catch
                {
                }
                try
                {
                    DR[1] = lector.GetValue(1);
                }
                catch
                {
                }
                DT.Rows.Add(DR);
            }
            DS.Tables.Add(DT);

            lector.Close();
            Conexion.Close();

            if (modo != 0)
            {
                //WebCombo1.DataBind();
                Grid.DataBind();
                Grid.Columns[0].Header.Caption = "ID Grupo";
                Grid.Columns[1].Header.Caption = "Nombre";
            }
            else
            {
                if (!IsPostBack)
                {
                    //WebCombo1.DataBind();
                    Grid.DataBind();
                    Grid.Columns[0].Header.Caption = "ID Grupo";
                    Grid.Columns[1].Header.Caption = "Nombre";
                }
            }
        }

        private int QuerySGEt()
        {
            try
            {
                return Convert.ToInt32(Request.QueryString["catalogo"]);
            }
            catch
            {
                return 3;
            }
        }

        private void Controles_Visible()
        {
            Grid.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = false;
            BEditarGrupo.Visible = false;
            BAgregarGrupo.Visible = false;
            BBorrarGrupo.Visible = false;
        }

        private void Habilitarcontroles()
        {
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Agrupaciones0Nuevo0Grupo))
                BAgregarGrupo.Visible = false;
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Agrupaciones0Editar0Grupo))
                BEditarGrupo.Visible = false;
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Agrupaciones0Borrar0Grupo))
                BBorrarGrupo.Visible = false;
            if (Sesion.TienePermiso(CEC_RESTRICCIONES.S0Agrupaciones0Editar0Grupo) || Sesion.TienePermiso(CEC_RESTRICCIONES.S0Agrupaciones0Nuevo0Grupo) || Sesion.TienePermiso(CEC_RESTRICCIONES.S0Agrupaciones0Borrar0Grupo))
                QueryReemplazo_GRID = "Select  eC_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID ,eC_GRUPOS_" + respuesta +
                    ".GRUPO_" + respuesta + "_NOMBRE  from eC_GRUPOS_" + respuesta + ", EC_USUARIOS_GRUPOS_" + respuesta +
                    " where eC_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID > 0 AND EC_USUARIOS_GRUPOS_" + respuesta +
                    ".GRUPO_" + respuesta + "_ID = eC_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID AND EC_USUARIOS_GRUPOS_" +
                    respuesta + ".USUARIO_ID = " + Sesion.USUARIO_ID + "  ORDER BY GRUPO_" + respuesta + "_ID";
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Sesion = CeC_Sesion.Nuevo(this);
            CeC_Grid.AplicaFormato(Grid);
            respuesta = QuerySGEt();

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Agrupaciones, true))
            {
                Controles_Visible();
                return;
            }
            Habilitarcontroles();
            //**************************************************

            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            Grid.Height = System.Web.UI.WebControls.Unit.Parse("100%");

            string Titulo = "";

            if (respuesta == 1)
                Titulo = CeC_Config.NombreGrupo1;
            else if (respuesta == 2)
                Titulo = CeC_Config.NombreGrupo2;
            else if (respuesta == 3)
                Titulo = CeC_Config.NombreGrupo3;
            else
                Sesion.Redirige("WF_main.aspx");

            Label1.Text = "Nombre de " + Titulo;
            Sesion.TituloPagina = "Edicion de " + Titulo;
            Sesion.DescripcionPagina = "Elija un " + Titulo + " y editelo o bórrelo";
            LLenarcontroles(0);
            if (!IsPostBack)
            {
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Carga de Desayunos Express", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            }
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
            this.DA_Grupos = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.dS_Eicion_Contenido1 = new DS_Eicion_Contenido();
            ((System.ComponentModel.ISupportInitialize)(this.dS_Eicion_Contenido1)).BeginInit();
            this.BBorrarAceptar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBorrarAceptar_Click);
            this.BBorrarCancelar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBorrarCancelar_Click);
            this.BGuardar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardar_Click);
            this.BCancelar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BCancelar_Click);
            this.BEditarGrupo.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BEditarGrupo_Click);
            this.BBorrarGrupo.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBorrarGrupo_Click);
            this.BAgregarGrupo.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BAgregarGrupo_Click);
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // DA_Grupos
            // 
            this.DA_Grupos.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_Grupos.InsertCommand = this.oleDbInsertCommand1;
            this.DA_Grupos.SelectCommand = this.oleDbSelectCommand1;
            this.DA_Grupos.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "EC_SUSCRIPCION", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																				new System.Data.Common.DataColumnMapping("SUSCRIPCION_NOMBRE", "SUSCRIPCION_NOMBRE")})});
            this.DA_Grupos.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_SUSCRIPCION WHERE (SUSCRIPCION_ID = ?) AND (SUSCRIPCION_NOMBRE = ? OR ? IS N" +
                "ULL AND SUSCRIPCION_NOMBRE IS NULL)";
            this.oleDbDeleteCommand1.Connection = this.Conexion;
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "SUSCRIPCION_NOMBRE", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_NOMBRE1", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "SUSCRIPCION_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_SUSCRIPCION(SUSCRIPCION_ID, SUSCRIPCION_NOMBRE) VALUES (?, ?)";
            this.oleDbInsertCommand1.Connection = this.Conexion;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("SUSCRIPCION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("SUSCRIPCION_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "SUSCRIPCION_NOMBRE"));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT SUSCRIPCION_ID, SUSCRIPCION_NOMBRE FROM EC_SUSCRIPCION";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = "UPDATE EC_SUSCRIPCION SET SUSCRIPCION_ID = ?, SUSCRIPCION_NOMBRE = ? WHERE (SUSCRIPCION_ID = ?)" +
                " AND (SUSCRIPCION_NOMBRE = ? OR ? IS NULL AND SUSCRIPCION_NOMBRE IS NULL)";
            this.oleDbUpdateCommand1.Connection = this.Conexion;
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("SUSCRIPCION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("SUSCRIPCION_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "SUSCRIPCION_NOMBRE"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "SUSCRIPCION_NOMBRE", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_NOMBRE1", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "SUSCRIPCION_NOMBRE", System.Data.DataRowVersion.Original, null));
            // 
            // dS_Eicion_Contenido1
            // 
            this.dS_Eicion_Contenido1.DataSetName = "DS_Eicion_Contenido";
            this.dS_Eicion_Contenido1.Locale = new System.Globalization.CultureInfo("es-MX");
            ((System.ComponentModel.ISupportInitialize)(this.dS_Eicion_Contenido1)).EndInit();

        }
        #endregion

        private string Selecccionado(int Celda)
        {
            try
            {
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    if (Grid.Rows[i].Selected)
                        return Grid.Rows[i].Cells[Celda].Value.ToString();
                }
                return "-1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void BCancelar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            LCorrecto.Text = "";
            LError.Text = "";
        }

        private void BEditarGrupo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;

            Grid.Height = System.Web.UI.WebControls.Unit.Parse("50%");
            TextBox1.Text = Selecccionado(1);

            Grupo_Nombre.Text = Selecccionado(1);
            if (Grupo_Nombre.Text == "-1")
            {
                Panel1.Visible = false;
                return;
            }
            Grupo_id_l.Text = CeC_BD.EjecutaEscalarString("SELECT GRUPO_" + respuesta + "_ID FROM eC_GRUPOS_" + respuesta + " WHERE GRUPO_" + respuesta + "_NOMBRE = '" + Grupo_Nombre.Text + "'");
            int ret = ExisteLigaGrupo(Convert.ToInt32(Grupo_id_l.Text), "EC_GRUPOS_" + respuesta, "EC_PERSONAS", "GRUPO_" + respuesta + "_ID");
            int ret2 = ExisteLiga_Persona_grupos(Convert.ToInt32(Grupo_id_l.Text));
            if (ret > 0)
            {
                //				if (QueryReemplazo_GRID.Length > 0)
                {
                    if (ret2 == Sesion.USUARIO_ID && Usuarios_Ligados_Grupo_Count == 1)
                    {
                        Panel1.Visible = true;
                        Sesion.Caso_Agrupaciones_Editar = 1;//el grupo le pertenece totalmente
                    }
                    else if (ret2 == Sesion.USUARIO_ID && Usuarios_Ligados_Grupo_Count > 1)
                    {
                        Sesion.Caso_Agrupaciones_Editar = 2;//el grupo es compartido con otro usuario
                        Grid.Height = System.Web.UI.WebControls.Unit.Parse("100%");
                        LError.Text = "No se puede efectuar operaciòn.El Grupo es Compartido con otro usuario. Se recomienda crear un nuevo grupo";
                    }
                    else
                    {
                        Sesion.Caso_Agrupaciones_Editar = 3;// el grupo se puede borrar
                    }
                }
            }
        }

        private void BAgregarGrupo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;

            LCorrecto.Text = "";
            LError.Text = "";

            Grid.Height = System.Web.UI.WebControls.Unit.Parse("50%");

            Grupo_id_l.Text = "-9999";
            TextBox1.Text = "";
        }

        private bool Editar_Grupo(int respuesta, string CampoValor, int CampoValor_id)
        {
            string TablaG = "";
            if (respuesta == 1)
                TablaG = "EC_SUSCRIPCION";
            else if (respuesta == 2)
                TablaG = "EC_GRUPOS_2";
            else if (respuesta == 3)
                TablaG = "EC_GRUPOS_3";

            string CNombre = "GRUPO_" + respuesta + "_NOMBRE";
            string CID = "GRUPO_" + respuesta + "_ID";

            int ret = CeC_BD.EjecutaEscalarInt("Select " + CID + " from " + TablaG + " where " + CNombre + " = '" + CampoValor + "' OR " + CNombre + " = '" + CampoValor.ToUpper() + "' ");

            int ret2 = CeC_BD.EjecutaEscalarInt("Select count(" + CID + ") from " + TablaG + " where " + CNombre + " = '" + CampoValor + "' OR " + CNombre + " = '" + CampoValor.ToUpper() + "'  ");

            if (ret2 == 0 || ret == CampoValor_id)
            {
                int modificaciones = CeC_BD.EjecutaComando("UPDATE " + TablaG + " SET " + CNombre + " = '" + CampoValor + "' Where " + CID + " = " + CampoValor_id);
                CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATOS SET " + Obtener_Campoconfig(respuesta) + " = '" + CampoValor + "' Where " + Obtener_Campoconfig(respuesta) + " = '" + Grupo_Nombre.Text + "'");
                if (modificaciones > 0)
                {
                    LCorrecto.Text = "Resgitros modificatos exitosamente";
                    return true;
                }
            }
            else
            {
                LError.Text = "Ya existe un grupo con ese nombre";
                return false;
            }
            return false;
        }

        private bool Insertar_Grupo(int respuesta, string CampoValor)
        {
            string TablaG = "";
            string TablaG_Asignacion_Grupo = "";

            TablaG = ObtenerGrupo(respuesta);
            TablaG_Asignacion_Grupo = ObtenerGrupo_USUARIOS(respuesta);

            string CNombre = "GRUPO_" + respuesta + "_NOMBRE";
            string CID = "GRUPO_" + respuesta + "_ID";

            string Usuarios_ID = "USUARIO_ID";
            string Usuarios_ID_Grupo = "GRUPO_" + respuesta + "_ID";

            int ret = CeC_BD.EjecutaEscalarInt("Select count(*) from " + TablaG + " where " + CNombre + " = '" + CampoValor.ToUpper() + "'");

            if (ret > 0)
            {
                LError.Text = "Ya existe un registro con ese nombre";
                return false;
            }

            int id_Nuevo = CeC_Autonumerico.GeneraAutonumerico(TablaG, CID);

            int ret2 = 0;

            ret2 = CeC_BD.EjecutaComando("INSERT INTO " + TablaG + "(" + CID + "," + CNombre + ") VALUES (" + id_Nuevo + ",'" + CampoValor.ToUpper() + "')");

            if (QueryReemplazo_GRID.Length > 0 && ret2 > 0)
            {
                CeC_BD.EjecutaComando("INSERT INTO " + TablaG_Asignacion_Grupo + "(" + Usuarios_ID + "," + Usuarios_ID_Grupo + ") VALUES (" + Sesion.USUARIO_ID + "," + id_Nuevo + ")");
            }

            if (ret2 > 0)
            {
                LCorrecto.Text = "Grupo agregado satisfactoriamente";
                return true;
            }
            else
                LError.Text = "No se ha podido agregar el grupo";

            return false;
        }

        private int ExisteLiga_Persona_grupos(int grupo_ID)
        {
            int ret = 0;
            ret = CeC_BD.EjecutaEscalarInt("SELECT EC_USUARIOS_GRUPOS_" + respuesta + ".USUARIO_ID FROM eC_GRUPOS_" + respuesta + ", EC_USUARIOS_GRUPOS_" + respuesta + " WHERE EC_USUARIOS_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID  =  eC_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID AND  EC_USUARIOS_GRUPOS_" + respuesta + ".USUARIO_ID = " + Sesion.USUARIO_ID + " AND  eC_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID = " + grupo_ID + "  ");
            //Usuarios_Ligados_Grupo_Count = CeC_BD.EjecutaEscalarInt("SELECT count(*) FROM eC_GRUPOS_"+respuesta+", EC_USUARIOS_GRUPOS_"+respuesta+" WHERE EC_USUARIOS_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID  =  eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID AND  EC_USUARIOS_GRUPOS_"+respuesta+".USUARIO_ID = "+Sesion.USUARIO_ID+" AND  eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID = "+grupo_ID+" ");
            Usuarios_Ligados_Grupo_Count = CeC_BD.EjecutaEscalarInt("SELECT count(*) FROM eC_GRUPOS_" + respuesta + ", EC_USUARIOS_GRUPOS_" + respuesta + " WHERE EC_USUARIOS_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID  =  eC_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID AND  eC_GRUPOS_" + respuesta + ".GRUPO_" + respuesta + "_ID = " + grupo_ID + " ");
            return ret;
        }

        private void BBorrarGrupo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;

            LCorrecto.Text = "";
            LError.Text = "";

            Grid.Height = System.Web.UI.WebControls.Unit.Parse("50%");

            LGrupoBorrado.Text = Selecccionado(1);
            LGrupoIdBorrado.Text = Selecccionado(0);

            LlenarWebcombo();

            int ret = ExisteLigaGrupo(Convert.ToInt32(LGrupoIdBorrado.Text), "EC_GRUPOS_" + respuesta, "EC_PERSONAS", "GRUPO_" + respuesta + "_ID");
            int ret2 = ExisteLiga_Persona_grupos(Convert.ToInt32(LGrupoIdBorrado.Text));

            if (ret > 0)
            {
                //&& ret2 == 1 && ret2 == Sesion.USUARIO_ID
                if (QueryReemplazo_GRID.Length > 0)
                {
                    if (ret2 == Sesion.USUARIO_ID && Usuarios_Ligados_Grupo_Count == 1)
                    {
                        Sesion.Caso_Agrupaciones = 1;//el grupo le pertenece totalmente
                    }
                    else if (ret2 == Sesion.USUARIO_ID && Usuarios_Ligados_Grupo_Count > 1)
                    {
                        Sesion.Caso_Agrupaciones = 2;//el grupo es compartido con otro usuario
                    }
                    else
                    {
                        Sesion.Caso_Agrupaciones = 3;// el grupo se puede borrar
                    }
                }
                Mensaje.Text = "EL Grupo (" + LGrupoBorrado.Text + " con Id " + LGrupoIdBorrado.Text + ") se encuentra asignado a " + ret + " empleados, para poder borrarlo es necesario seleccionar otro grupo en la caja de seleccion. Este a su vez se asignará a los empleados del grupo que desea borrar.";
                DD_Grupos.Visible = true;
            }
            else
            {
                Mensaje.Text = " No existe Liga en el Grupo " + LGrupoBorrado.Text + ". Asi que es factible que borre este grupo ya que no ha sido asignado";
                DD_Grupos.Visible = false;
            }
        }

        private void Refrescar_Controles()
        {

            //DataSet DS = new DataSet("DSGrupos");
            //DataTable DT = new DataTable("Tabla_Grupos");
            //DataRow DR;
            LLenarcontroles(1);
            //WebCombo1.DataBind();
            //Grid.DataBind();

        }

        private int ExisteLigaGrupo(int id_grupo, string Tabla1, string Tabla2, string campoliga)
        {
            int ret = CeC_BD.EjecutaEscalarInt("Select Count(*) From " + Tabla1 + "," + Tabla2 + " where " + Tabla1 + "." + campoliga + " = " + Tabla2 + "." + campoliga + " and " + Tabla1 + "." + campoliga + " = " + id_grupo);
            if (ret > 0)
                return ret;
            return 0;
        }

        private void BBorrarCancelar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            LCorrecto.Text = "";
            LError.Text = "";
        }

        protected void BBorrarAceptar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LCorrecto.Text = "";
            LError.Text = "";
            int ret = ExisteLigaGrupo(Convert.ToInt32(LGrupoIdBorrado.Text), "EC_GRUPOS_" + respuesta, "EC_PERSONAS", "GRUPO_" + respuesta + "_ID");


            if (ret > 0)
            {
                if (DD_Grupos.SelectedIndex != -1)
                {
                    Actualizar_Grupos_ligas(Convert.ToInt32(DD_Grupos.Items[DD_Grupos.SelectedIndex].Value), Convert.ToInt32(LGrupoIdBorrado.Text), DD_Grupos.Items[DD_Grupos.SelectedIndex].Text, LGrupoBorrado.Text);
                }
                else
                    LError.Text = "Es necesario seleccionar una fila del la lista de seleccion";
            }
            else
                Borrar_Ligas(Convert.ToInt32(LGrupoIdBorrado.Text));

            Refrescar_Controles();
            Panel1.Visible = false;
            Panel2.Visible = false;
        }
        private void Borrar_Ligas(int ID_grupo_Viejo)
        {
            string qry4 = "DELETE FROM " + Obtener_Usuarios_grupo(respuesta) + " WHERE " + Obtener_Usuarios_grupo_id(respuesta) + " = " + ID_grupo_Viejo;//desligar grupo a usuarios
            string qry1 = "DELETE FROM " + ObtenerGrupo(respuesta) + " WHERE " + ObtenerGrupo_y_ID(respuesta) + " = " + ID_grupo_Viejo;
            try
            {
                CeC_BD.EjecutaComando(qry4);
            }
            catch (Exception ex)
            {
                LError.Text = "Error: Borrado de liga de la Tabla Usuario_Grupo ";
                return;
            }
            try
            {
                CeC_BD.EjecutaComando(qry1);
            }
            catch (Exception ex)
            {
                LError.Text = "Error: Actualizado de la liga de personas ";
                return;
            }
            LCorrecto.Text = "Grupo Borrado satisfactoriamente";
        }

        private void Actualizar_Grupos_ligas(int ID_grupoNuevo, int ID_grupo_Viejo, string Campo_Actualizar_Valor_Nuevo, string Campo_Actualizar_Valor_Viejo)
        {
            LError.Text = "";
            LCorrecto.Text = "";

            string qry4 = "DELETE FROM " + Obtener_Usuarios_grupo(respuesta) + " WHERE " + Obtener_Usuarios_grupo_id(respuesta) + " = " + ID_grupo_Viejo; //desligar grupo a usuarios
            string qry3 = "UPDATE EC_PERSONAS SET " + ObtenerGrupo_y_ID_Personas(respuesta) + " = " + ID_grupoNuevo + "  WHERE " + ObtenerGrupo_y_ID_Personas(respuesta) + " = " + ID_grupo_Viejo;
            string qry2 = "UPDATE EC_PERSONAS_DATOS SET " + Obtener_Campoconfig(respuesta) + " = '" + Campo_Actualizar_Valor_Nuevo + "'  WHERE " + Obtener_Campoconfig(respuesta) + " = '" + Campo_Actualizar_Valor_Viejo + "'";
            string qry1 = "DELETE FROM " + ObtenerGrupo(respuesta) + " WHERE " + ObtenerGrupo_y_ID(respuesta) + " = " + ID_grupo_Viejo;

            if (QueryReemplazo_GRID.Length > 0)
            {
                qry4 = "DELETE FROM " + Obtener_Usuarios_grupo(respuesta) + " WHERE " + Obtener_Usuarios_grupo_id(respuesta) + " = " + ID_grupo_Viejo + "  AND " + ObtenerGrupo_USUARIOS(respuesta) + ".USUARIO_ID = " + Sesion.USUARIO_ID;  //desligar grupo a usuarios
            }

            try
            {
                int ret1 = CeC_BD.EjecutaComando(qry4);
                if (ret1 > 0 || Sesion.Caso_Agrupaciones == 3)
                    LCorrecto.Text = "  [Borrado de liga de la Tabla Usuario_Grupo] \n";
                else
                    LError.Text = "  [Error: Borrado de liga de la Tabla Usuario_Grupo] \n";
            }
            catch (Exception ex)
            {
                LError.Text = "  [Error: Borrado de liga de la Tabla Usuario_Grupo]  \n";
            }
            try
            {
                if (Sesion.Caso_Agrupaciones == 1 || Sesion.Caso_Agrupaciones == 3)
                {
                    CeC_BD.EjecutaComando(qry3);
                    LCorrecto.Text += "[Actualizado de liga de  personas] \n";
                }
                else
                {
                    LError.Text += "[Accion no permitida: Actualizado de liga de  personas]   \n";
                }
            }
            catch (Exception ex)
            {
                LError.Text = "[Error: Actualizado de liga de personas]   \n";
            }
            try
            {
                if (Sesion.Caso_Agrupaciones == 1 || Sesion.Caso_Agrupaciones == 3)
                {
                    CeC_BD.EjecutaComando(qry2);
                    LCorrecto.Text += "[Actualizado de liga de empleados] \n ";
                }
                else
                {
                    LError.Text += "[Accion no permitida: Actualizado de liga de  empleados]    \n";
                }
            }
            catch (Exception ex)
            {
                LError.Text = "[Error: Actualizado de la liga empleados]   \n ";
            }
            try
            {
                if (Sesion.Caso_Agrupaciones == 3 || Sesion.Caso_Agrupaciones == 1)
                {
                    CeC_BD.EjecutaComando(qry1);
                    LCorrecto.Text += "[Borrado de Grupo]  \n";
                }
                else
                {
                    LError.Text += "[Accion no permitida: Borrado de Grupo]  \n";

                    LError.Text += "  (El grupo Se encuentra asignado a dos o más ususarios)";
                }


            }
            catch (Exception ex)
            {
                LError.Text = "Error: borrado del grupo";

            }

        }
        private string Obtener_Campoconfig(int Cual)
        {
            switch (Cual)
            {
                case 1:

                    return CeC_Config.CampoGrupo1;
                    break;
                case 2:

                    return CeC_Config.CampoGrupo2;
                    break;
                case 3:

                    return CeC_Config.CampoGrupo3;
                    break;
            }
            return "";
        }

        private string ObtenerGrupo_USUARIOS(int cual)
        {
            string TablaG = "";
            if (respuesta == 1)
                TablaG = "EC_PERMISOS_SUSCRIP";
            else if (respuesta == 2)
                TablaG = "EC_USUARIOS_GRUPOS_2";
            else if (respuesta == 3)
                TablaG = "EC_USUARIOS_GRUPOS_3";

            return TablaG;

        }

        private string ObtenerGrupo(int cual)
        {
            string TablaG = "";
            if (respuesta == 1)
                TablaG = "EC_SUSCRIPCION";
            else if (respuesta == 2)
                TablaG = "EC_GRUPOS_2";
            else if (respuesta == 3)
                TablaG = "EC_GRUPOS_3";

            return TablaG;

        }
        private string ObtenerGrupo_y_ID(int cual)
        {
            string TablaG = "";
            if (respuesta == 1)
                TablaG = "EC_SUSCRIPCION.SUSCRIPCION_ID";
            else if (respuesta == 2)
                TablaG = "EC_GRUPOS_2.GRUPO_2_ID";
            else if (respuesta == 3)
                TablaG = "EC_GRUPOS_3.GRUPO_3_ID";

            return TablaG;

        }
        private string ObtenerGrupo_y_ID_Personas(int cual)
        {
            string TablaG = "";
            if (respuesta == 1)
                TablaG = "EC_PERSONAS.SUSCRIPCION_ID";
            else if (respuesta == 2)
                TablaG = "EC_PERSONAS.GRUPO_2_ID";
            else if (respuesta == 3)
                TablaG = "EC_PERSONAS.GRUPO_3_ID";

            return TablaG;

        }
        private string Obtener_Usuarios_grupo(int cual)
        {
            string TablaG = "";
            if (respuesta == 1)
                TablaG = "EC_PERMISOS_SUSCRIP";
            else if (respuesta == 2)
                TablaG = "EC_USUARIOS_GRUPOS_2";
            else if (respuesta == 3)
                TablaG = "EC_USUARIOS_GRUPOS_3";

            return TablaG;
        }

        private string Obtener_Usuarios_grupo_id(int cual)
        {
            string TablaG = "";
            if (respuesta == 1)
                TablaG = "EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID";
            else if (respuesta == 2)
                TablaG = "EC_USUARIOS_GRUPOS_2.GRUPO_2_ID";
            else if (respuesta == 3)
                TablaG = "EC_USUARIOS_GRUPOS_3.GRUPO_3_ID";

            return TablaG;
        }

        private void BGuardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LCorrecto.Text = "";
            LError.Text = "";


            if (Grupo_id_l.Text == "-9999")
                Insertar_Grupo(respuesta, TextBox1.Text);
            else
            {
                if (Sesion.Caso_Agrupaciones_Editar == 1 || Sesion.Caso_Agrupaciones_Editar == 3)
                    Editar_Grupo(respuesta, TextBox1.Text, Convert.ToInt32(Grupo_id_l.Text));
            }

            Panel1.Visible = false;
            Panel2.Visible = false;

            LLenarcontroles(1);
        }

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {

            /*Grid.Attributes.Add("CellClickHandler", "if (Grid_CellClickHandler" +
                "'Grid',igtbl_getActiveCell('Grid'),0) {return false;})");
            CeC_Grid.AplicaFormato(Grid);
            Grid.DisplayLayout.HeaderTitleModeDefault = Infragistics.WebUI.UltraWebGrid.CellTitleMode.Always;*/
        }
    }
}