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
    /// Descripción breve de WF_Asignacion_Grupo_Persona.
    /// </summary>
    public partial class WF_Asignacion_Grupo_Persona : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbConnection Conexion;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbDataAdapter DA_Grupo1;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
        protected System.Data.OleDb.OleDbDataAdapter DA_Personas1;
        protected DS_Asignacion_Grupo_Persona dS_Asignacion_Grupo_Persona1;
        private int respuesta = 0;
        private string Grupos_Seleccionados = "";
        string Query_Admin0Grupo = "";
        string Query_Admin0Grupo_Secundario = "";
        CeC_Sesion Sesion;

        private void Habilitarcontroles()
        {
            if (Sesion.TienePermiso(CEC_RESTRICCIONES.S0Asignaciones))
            {
                Query_Admin0Grupo = "Select Grupo_" + respuesta + "_ID,Grupo_" + respuesta + "_NOMBRE FROM " + ObtenerGrupo(respuesta) + " Where Grupo_" + respuesta + "_ID > 0 ORDER BY Grupo_" + respuesta + "_ID"; //ADMINISTRADOR
                Query_Admin0Grupo_Secundario = "Select PERSONA_LINK_ID,PERSONA_NOMBRE,Grupo_" + respuesta + "_ID,0 as SELECCIONADO FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0 AND Grupo_" + respuesta + "_ID > 0 ORDER BY PERSONA_LINK_ID"; //ADMIN;
            }
            if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Asignaciones0Grupo))
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
                Grid.Visible = false;
            }
            else
            {
                Query_Admin0Grupo = "Select eC_Grupos_" + respuesta + ".Grupo_" + respuesta + "_ID,eC_Grupos_" + respuesta + ".Grupo_" + respuesta + "_NOMBRE FROM " + ObtenerGrupo(respuesta) + "," + ObtenerUsuariosGrupos_SoloTabla(respuesta) + "  Where eC_Grupos_" + respuesta + ".Grupo_" + respuesta + "_ID > 0 AND " + ObtenerUsuariosGrupos_ID(respuesta) + " = eC_Grupos_" + respuesta + ".Grupo_" + respuesta + "_ID  And USUARIO_ID= " + Sesion.USUARIO_ID + " ORDER BY eC_Grupos_" + respuesta + ".Grupo_" + respuesta + "_ID ";	//USUARIOS
                Query_Admin0Grupo_Secundario = "Select PERSONA_LINK_ID,PERSONA_NOMBRE,Grupo_" + respuesta + "_ID,0 as SELECCIONADO FROM EC_PERSONAS Where PERSONA_BORRADO = 0 AND Grupo_" + respuesta + "_ID > 0 AND " + campoEC_PERSONAS(respuesta) + " in (Select " + ObtenerUsuariosGrupos_ID(respuesta) + " from " + ObtenerUsuariosGrupos_SoloTabla(respuesta) + " where " + ObtenerUsuariosGrupos_SoloTabla(respuesta) + ".usuario_id= " + Sesion.USUARIO_ID + ") ORDER BY PERSONA_LINK_ID ";//USUARIOS				
            }
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Introducir aquí el código de usuario para inicializar la página
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            respuesta = QuerySGEt();

            Sesion = CeC_Sesion.Nuevo(this);

            Sesion.TituloPagina = "Asignación de Grupos por " + Obtener_Campoconfig_TEXTO(respuesta);
            Sesion.DescripcionPagina = "";

            //CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATOS SET GRUPO = "+
            //	" (SELECT GRUPO_"+respuesta+"_NOMBRE FROM eC_GRUPOS_"+respuesta+", EC_PERSONAS " +
            //	" WHERE EC_PERSONAS.GRUPO_"+respuesta+"_ID = eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID " + 
            //	" AND PERSONA_LINK_ID = TRACVE) ");
            //				" where tracve IN (SELECT PERSONA_ID as tracve FROM EC_PERSONAS, eC_GRUPOS_"+respuesta+" " +
            //				" WHERE  eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID  = EC_PERSONAS.GRUPO_"+respuesta+"_ID) ");

            /*CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATOS set GRUPO = " +
                " (SELECT NOMBRE_GRUPO FROM (SELECT   EC_PERSONAS_DATOS.tracve as persona, " + 
                " eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID as Grupo, " +
                " eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_Nombre as nombre_grupo  " + 
                " FROM  EC_PERSONAS_DATOS, eC_GRUPOS_"+respuesta+", EC_PERSONAS  " + 
                " WHERE   EC_PERSONAS_DATOS.TRACVE = EC_PERSONAS.PERSONA_LINK_ID AND " + 
                " eC_GRUPOS_"+respuesta+".GRUPO_"+respuesta+"_ID = EC_PERSONAS.GRUPO_"+respuesta+"_ID) " + 
                " WHERE persona = EC_PERSONAS_DATOS.tracve) ");*/

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Asignaciones0Grupo, true))
            {
                Habilitarcontroles();
                return;
            }
            Habilitarcontroles();
            //**************************************************

            Label1.Text = "Asignar Empleado a " + Obtener_Campoconfig_TEXTO(respuesta);
            LLenarGRid(0);

            if (!IsPostBack)
            {
                LlenarWebcombo();
                //Agrega Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "A", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
        private int ConvertForzado(string NEmp)
        {
            try
            {
                return Convert.ToInt32(NEmp);
            }
            catch
            {
                return -1;
            }
        }

        private void LLenarGRid(int caso)
        {

            try
            {

                DataSet DS = new DataSet("DSGrupos");
                DataTable DT = new DataTable("Tabla_Grupos");
                DataTable DT2 = new DataTable("Tabla_Grupos_2");
                DataRow DR;
                DataRow DR2;

                DT.Columns.Add("Grupo_" + respuesta + "_ID", System.Type.GetType("System.Decimal"));
                DT.Columns.Add("Grupo_" + respuesta + "_NOMBRE", System.Type.GetType("System.String"));


                DT2.Columns.Add("PERSONA_LINK_ID", System.Type.GetType("System.Decimal"));
                DT2.Columns.Add("PERSONA_NOMBRE", System.Type.GetType("System.String"));
                DT2.Columns.Add("Grupo_" + respuesta + "_ID", System.Type.GetType("System.Decimal"));
                DT2.Columns.Add("SELECCIONADO", System.Type.GetType("System.Decimal"));


                //And "+campoEC_PERSONAS(respuesta)+ " in (Select " +ObtenerUsuariosGrupos_ID(respuesta) +	" from "+ObtenerUsuariosGrupos_SoloTabla(respuesta)+ " where "+ObtenerUsuariosGrupos_SoloTabla(respuesta)+".usuario_id = "+Sesion.USUARIO_ID+")

                string Qry = "";
                string Qry2 = "";

                Qry = Query_Admin0Grupo;
                Qry2 = Query_Admin0Grupo_Secundario;


                //QueryRemplazo = "and EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = "+Sesion.USUARIO_ID+")";

                if (Conexion.State != System.Data.ConnectionState.Open)
                    Conexion.Open();

                OleDbCommand comando = new OleDbCommand(Qry, Conexion);
                OleDbDataReader lector;
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    DR = DT.NewRow();

                    try
                    {
                        DR[0] = lector.GetValue(0);
                        DR[1] = lector.GetValue(1);
                    }
                    catch
                    {

                    }

                    DT.Rows.Add(DR);
                }

                lector.Close();

                OleDbCommand comando2 = new OleDbCommand(Qry2, Conexion);
                OleDbDataReader lector2;
                lector2 = comando2.ExecuteReader();

                while (lector2.Read())
                {
                    DR2 = DT2.NewRow();

                    try
                    {
                        DR2[0] = lector2.GetValue(0);
                        DR2[1] = lector2.GetValue(1);
                        DR2[2] = lector2.GetValue(2);
                        DR2[3] = lector2.GetValue(3);
                    }
                    catch (Exception ex)
                    {
                        LError.Text = ex.Message;
                    }
                    DT2.Rows.Add(DR2);
                }

                lector2.Close();

                DS.Tables.Add(DT2);
                DS.Tables.Add(DT);

                DataColumn ColumnaPadre = DS.Tables["Tabla_Grupos"].Columns["Grupo_" + respuesta + "_ID"];
                DataColumn ColumnaHijo = DS.Tables["Tabla_Grupos_2"].Columns["Grupo_" + respuesta + "_ID"];

                DataRelation relacion = new System.Data.DataRelation("Mike", ColumnaPadre, ColumnaHijo);
                DS.Relations.Add(relacion);

                Grid.DataMember = "Tabla_Grupos";
                Grid.DataSource = DS;

                if (caso == 0)
                {
                    if (!IsPostBack)
                    {
                        Grid.DataBind();
                        Grid.Bands[0].Columns[1].Width = System.Web.UI.WebControls.Unit.Parse("500px");
                        Grid.Bands[1].Columns[1].Width = System.Web.UI.WebControls.Unit.Parse("200px");
                        Grid.Bands[1].Columns[(int)Grid.Bands[1].Columns.Count - 1].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
                        Grid.Bands[1].Columns[(int)Grid.Bands[1].Columns.Count - 1].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;

                        Grid.Bands[0].Columns[0].Header.Caption = "Grupo ID";
                        Grid.Bands[0].Columns[1].Header.Caption = "Grupo Nombre";

                        Grid.Bands[1].Columns[0].Header.Caption = "No. Empleado";
                        Grid.Bands[1].Columns[1].Header.Caption = "Nombre";
                        Grid.Bands[1].Columns[2].Header.Caption = "Grupo";
                        Grid.Bands[1].Columns[3].Header.Caption = "Seleccionado";
                        Grid.Bands[1].Columns[2].Hidden = true;
                    }
                }
                else
                {

                    Grid.DataBind();
                    Grid.Bands[0].Columns[1].Width = System.Web.UI.WebControls.Unit.Parse("500px");
                    Grid.Bands[1].Columns[1].Width = System.Web.UI.WebControls.Unit.Parse("200px");
                    Grid.Bands[1].Columns[(int)Grid.Bands[1].Columns.Count - 1].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
                    Grid.Bands[1].Columns[(int)Grid.Bands[1].Columns.Count - 1].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
                }

                //////************************
                //codigo para asegurarse de lo que se muestra en pantalla en cuento a grupos 
                //sean los que estam asignados

                try
                {
                    for (int i = 0; i < Grid.Rows.Count; i++)
                    {
                        string str_Grupo = Grid.Rows[i].Cells[1].Text;
                        for (int j = 0; j < Grid.Rows[i].Rows.Count; j++)
                        {
                            string TRACVE_Empleado = Grid.Rows[i].Rows[j].Cells[0].Value.ToString();
                            string Campo_Empleados = Sesion.OBTEN_GRUPO_CONFIG_STR(respuesta);

                            if (Campo_Empleados.Length > 0)
                                CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATOS SET " + Campo_Empleados + " = '" + str_Grupo + "' where EC_PERSONAS_DATOS." + CeC_Campos.CampoTE_Llave + "=" + NoEmpleado1.Text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LError.Text = "Error ." + ex.Message;
                }
                //////************************

                Conexion.Close();
            }
            catch (Exception ex)
            {
                LError.Text = ex.Message;
            }

        }

        private string[] MoverRegistros()
        {
            Grupos_Seleccionados = "";

            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                for (int j = 0; j < Grid.Rows[i].Rows.Count; j++)
                {
                    int checada = Convert.ToInt32(Grid.Rows[i].Rows[j].Cells[(int)Grid.Bands[1].Columns.Count - 1].Value);
                    if (checada > 0)
                    {
                        int TRACVE = Convert.ToInt32(Grid.Rows[i].Rows[j].Cells[0].Value);
                        Grupos_Seleccionados += TRACVE + "|";
                    }
                }
            }
            char[] Caracter = new char[1] { Convert.ToChar("|") };
            string[] ret = Grupos_Seleccionados.Split(Caracter);
            return ret;
        }

        private void LlenarWebcombo()
        {
            ComboGrupo1.Items.Clear();
            ComboGrupo2.Items.Clear();

            string qry = Query_Admin0Grupo;

            if (Conexion.State != System.Data.ConnectionState.Open)
                Conexion.Open();

            OleDbCommand commando = new OleDbCommand(qry, Conexion);
            OleDbDataReader lector;

            lector = commando.ExecuteReader();

            while (lector.Read())
            {
                try
                {
                    ComboGrupo1.Items.Add(new ListItem(Convert.ToString(lector.GetValue(1)), Convert.ToString(lector.GetValue(0))));
                    ComboGrupo2.Items.Add(new ListItem(Convert.ToString(lector.GetValue(1)), Convert.ToString(lector.GetValue(0))));
                }
                catch
                {
                }
            }
            lector.Close();
            Conexion.Close();

            ComboGrupo1.DataBind();
            ComboGrupo2.DataBind();
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
            this.DA_Grupo1 = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
            this.DA_Personas1 = new System.Data.OleDb.OleDbDataAdapter();
            this.dS_Asignacion_Grupo_Persona1 = new DS_Asignacion_Grupo_Persona();
            ((System.ComponentModel.ISupportInitialize)(this.dS_Asignacion_Grupo_Persona1)).BeginInit();
            this.BAsignacionGrupo.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BAsignacionGrupo_Click);
            this.Moverempleados.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Moverempleados_Click);
            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT SUSCRIPCION_ID, SUSCRIPCION_NOMBRE FROM EC_SUSCRIPCION WHERE (SUSCRIPCION_ID > 0)";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            // 
            // DA_Grupo1
            // 
            this.DA_Grupo1.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_Grupo1.InsertCommand = this.oleDbInsertCommand1;
            this.DA_Grupo1.SelectCommand = this.oleDbSelectCommand1;
            this.DA_Grupo1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "EC_SUSCRIPCION", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																				new System.Data.Common.DataColumnMapping("SUSCRIPCION_NOMBRE", "SUSCRIPCION_NOMBRE")})});
            this.DA_Grupo1.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbSelectCommand2
            // 
            this.oleDbSelectCommand2.CommandText = "SELECT SUSCRIPCION_ID, PERSONA_ID, PERSONA_LINK_ID, PERSONA_BORRADO, 0 AS SELECCIONAD" +
                "O FROM EC_PERSONAS WHERE (PERSONA_BORRADO = 0)";
            this.oleDbSelectCommand2.Connection = this.Conexion;
            // 
            // DA_Personas1
            // 
            this.DA_Personas1.DeleteCommand = this.oleDbDeleteCommand2;
            this.DA_Personas1.InsertCommand = this.oleDbInsertCommand2;
            this.DA_Personas1.SelectCommand = this.oleDbSelectCommand2;
            this.DA_Personas1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS_1", new System.Data.Common.DataColumnMapping[] {
																																																					 new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																					 new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																					 new System.Data.Common.DataColumnMapping("PERSONA_LINK_ID", "PERSONA_LINK_ID"),
																																																					 new System.Data.Common.DataColumnMapping("PERSONA_BORRADO", "PERSONA_BORRADO")})});
            this.DA_Personas1.UpdateCommand = this.oleDbUpdateCommand2;
            // 
            // dS_Asignacion_Grupo_Persona1
            // 
            this.dS_Asignacion_Grupo_Persona1.DataSetName = "DS_Asignacion_Grupo_Persona";
            this.dS_Asignacion_Grupo_Persona1.Locale = new System.Globalization.CultureInfo("es-MX");
            this.Load += new System.EventHandler(this.Page_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dS_Asignacion_Grupo_Persona1)).EndInit();

        }
        #endregion

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

        private string Obtener_Campoconfig_TEXTO(int Cual)
        {
            switch (Cual)
            {
                case 1:
                    return CeC_Config.NombreGrupo1;
                    break;
                case 2:
                    return CeC_Config.NombreGrupo2;
                    break;
                case 3:
                    return CeC_Config.NombreGrupo3;
                    break;
            }
            return "";
        }

        private void Asignacion_UnopUno()
        {
            LError.Text = "";
            LCorrecto.Text = "";

            if (NoEmpleado1.Text.Length > 0)
            {
                if (ComboGrupo1.SelectedIndex != -1)
                {
                    int NoEmpleado = ConvertForzado(NoEmpleado1.Text);
                    if (NoEmpleado <= 0)
                    {
                        LError.Text = "Numero de Empleados no válido";
                        return;
                    }
                    else
                    {
                        int ret1 = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET " + campoEC_PERSONAS(respuesta) + " = " + ComboGrupo1.SelectedItem.Value + " WHERE EC_PERSONAS.PERSONA_LINK_ID = " + NoEmpleado1.Text);
                        int ret2 = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATOS SET " + Obtener_Campoconfig(respuesta) + " = '" + ComboGrupo1.SelectedItem.Text + "' WHERE EC_PERSONAS_DATOS." + CeC_Campos.CampoTE_Llave + "=" + NoEmpleado1.Text);

                        if (ret1 + ret2 == 2)
                            LCorrecto.Text = "El empleado fue asignado satisfactoriamente";
                        else
                            LError.Text = "No. de Empleado no válido";
                    }
                }
                else
                {
                    LError.Text = "Debes de seleccionar un grupo para poder asignarlo a un Empleado";
                    return;
                }
            }
        }

        private int Mover_Update(int PERSONA_LINK_ID)
        {
            try
            {
                int ret = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET " + campoEC_PERSONAS(respuesta) + " = " + ComboGrupo2.SelectedItem.Value + " WHERE EC_PERSONAS.PERSONA_LINK_ID = " + PERSONA_LINK_ID);
                int ret2 = CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DATOS SET " + Obtener_Campoconfig(respuesta) + " = '" + ComboGrupo2.SelectedItem.Text + "' WHERE EC_PERSONAS_DATOS." + CeC_Campos.CampoTE_Llave + "=" + PERSONA_LINK_ID);
                return ret;
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        private void Moverempleados_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            LError.Text = "";
            LCorrecto.Text = "";

            int ret = 0;

            if (ComboGrupo2.SelectedIndex != -1)
            {
                string[] GruposMover = MoverRegistros();
                for (int i = 0; i < GruposMover.Length; i++)
                {
                    if (GruposMover[i] != null && GruposMover[i].Length > 0)
                    {
                        ret += Mover_Update(Convert.ToInt32(GruposMover[i]));
                    }
                }

                if (ret > 0)
                    LCorrecto.Text = "Cambios Realizados Satisfactoriamente";
                else
                    LError.Text = "Debes de seleccionar una fila para poder asignar un grupo";
            }
            else
            {
                LError.Text = "Para poder mover los Empleados seleccionados en la lista es necesario seleccionar un grupo de destino";
            }
            LLenarGRid(1);
        }

        private void BAsignacionGrupo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Asignacion_UnopUno();
            LLenarGRid(1);
        }

        private bool ComprobacionGrupal(int Tracve)
        {
            string QueryRemplazo = "SELECT PERSONA_NOMBRE FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Tracve + " And " + campoEC_PERSONAS(respuesta) + " in (Select " + ObtenerUsuariosGrupos_ID(respuesta) + " from " + ObtenerUsuariosGrupos_SoloTabla(respuesta) + " where " + ObtenerUsuariosGrupos_SoloTabla(respuesta) + ".usuario_id = " + Sesion.USUARIO_ID + ")";
            string ret = CeC_BD.EjecutaEscalarString(QueryRemplazo);
            if (ret.Length > 0)
                return true;
            return false;
        }

        private string campoEC_PERSONAS(int Querystring_respuesta)
        {
            string TablaG = "";
            if (Querystring_respuesta == 1)
                TablaG = "EC_PERSONAS.SUSCRIPCION_ID";
            else if (Querystring_respuesta == 2)
                TablaG = "EC_PERSONAS.GRUPO_2_ID";
            else if (Querystring_respuesta == 3)
                TablaG = "EC_PERSONAS.GRUPO_3_ID";
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

        private string ObtenerUsuariosGrupos_ID(int cual)
        {
            string TablaG = "";
            if (respuesta == 1)
                TablaG = "EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID";
            else if (respuesta == 2)
                TablaG = "EC_USUARIOS_GRUPOS_2.Grupo_2_ID";
            else if (respuesta == 3)
                TablaG = "EC_USUARIOS_GRUPOS_3.Grupo_3_ID";
            return TablaG;
        }

        private string ObtenerUsuariosGrupos_SoloTabla(int cual)
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

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid);
        }
    }
}
