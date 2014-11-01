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
    /// Descripción breve de WF_Inasistencias.
    /// </summary>
    public partial class WF_Inasistencias : System.Web.UI.Page
    {
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        protected System.Data.OleDb.OleDbDataAdapter DA_Inasistencias;
        protected DS_Inasistencias dS_Inasistencias1;
        protected System.Data.OleDb.OleDbConnection Conexion;
        CeC_Sesion Sesion;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
        protected System.Data.OleDb.OleDbDataAdapter DA_TipoIncidencias;
        protected System.Data.OleDb.OleDbDataAdapter DA_Edicion_PersonaDiario;
        protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand3;
        protected System.Data.OleDb.OleDbCommand oleDbInsertCommand4;
        protected System.Data.OleDb.OleDbCommand oleDbSelectCommand4;
        protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand3;
        protected System.Data.OleDb.OleDbDataAdapter DA_Incidencias;
        protected System.Data.OleDb.OleDbCommand oleDbCommand1;
        protected System.Data.OleDb.OleDbCommand oleDbCommand2;
        protected System.Data.OleDb.OleDbCommand oleDbCommand3;
        protected System.Data.OleDb.OleDbCommand Commando_PersonasDiario;
        private string QueryRemplazo = "";

        private void Habilitarcontroles()
        {
            Grid.Visible = false;
            WebPanel2.Visible = false;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            Sesion = CeC_Sesion.Nuevo(this);
            Sesion.TituloPagina = "Listado de Inasistencias";
            Sesion.DescripcionPagina = "Seleccione un empleado para justificar la falta del día específico";
            // Introducir aquí el código de usuario para inicializar la página
            LError.Text = "";
            LCorrecto.Text = "";

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Empleados0Inasistencias, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            if (!IsPostBack)
            {
                if (Sesion.WF_EmpleadosFil_Qry.Length < 1)
                {
                    Sesion.WF_EmpleadosFil(true, true, false, "Muestra Busqueda", "Inasistencias",
                        "WF_Inasistencias.aspx", "", false, true, true);
                    return;
                }
                else
                {
                    Sesion.WF_EmpleadosFil_Qry_Temp = Sesion.WF_EmpleadosFil_Qry;
                    Sesion.WF_EmpleadosFil_Qry = "";
                }

                /*Si exiten los permisos correspondientes*/
                /*Termina el area de permisos*/

                DateTime Fecha_I = Sesion.WF_EmpleadosFil_FechaI;
                DateTime Fecha_F = Sesion.WF_EmpleadosFil_FechaF;

                DA_Inasistencias.SelectCommand.CommandText = DA_Inasistencias.SelectCommand.CommandText.Replace("ORDER", " AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" + Sesion.WF_EmpleadosFil_Qry_Temp + ") " + QueryRemplazo + " ORDER ");

                DA_Inasistencias.SelectCommand.Parameters["PERSONA_DIARIO_FECHA"].Value = Fecha_I;
                DA_Inasistencias.SelectCommand.Parameters["PERSONA_DIARIO_FECHA1"].Value = Fecha_F;

                /*if (Sesion.WF_EmpleadosFil_DNL == 0)
                    DA_Inasistencias.SelectCommand.CommandText = DA_Inasistencias.SelectCommand.CommandText.Replace("ORDER", "AND (EC_TIPO_INC_SIS.TIPO_INC_SIS_ID <> 10) AND (EC_TIPO_INC_SIS.TIPO_INC_SIS_ID <> 11) ORDER");
                DA_Inasistencias.SelectCommand.CommandText = DA_Inasistencias.SelectCommand.CommandText.Replace("ORDER", " AND (EC_TIPO_INC_SIS.TIPO_INC_SIS_ID <> 1) AND (EC_PERSONAS_DIARIO.INCIDENCIA_ID = 0) AND (EC_TIPO_INC_SIS.TIPO_INC_SIS_ID <> 0) ORDER ");
                DA_Inasistencias.Fill(dS_Inasistencias1);
                DA_TipoIncidencias.Fill(dS_Inasistencias1);
                Conexion.Close();
                Conexion.Dispose();

                TipoIncidenciaNombre.DataBind();*/
                DA_TipoIncidencias.Fill(dS_Inasistencias1);
                TipoIncidenciaNombre.DataSource = dS_Inasistencias1;
                CeC_Grid.AplicaFormato(TipoIncidenciaNombre);
                Grid.DataBind();
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Inasistencias", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            }
            Sesion.ControlaBoton(ref BGuardarCambios);
            Sesion.ControlaBoton(ref BJusAccesos);
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
            this.DA_Inasistencias = new System.Data.OleDb.OleDbDataAdapter();
            this.dS_Inasistencias1 = new DS_Inasistencias();
            this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
            this.DA_TipoIncidencias = new System.Data.OleDb.OleDbDataAdapter();
            this.DA_Edicion_PersonaDiario = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand3 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand4 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand4 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand3 = new System.Data.OleDb.OleDbCommand();
            this.DA_Incidencias = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbCommand2 = new System.Data.OleDb.OleDbCommand();
            this.oleDbCommand3 = new System.Data.OleDb.OleDbCommand();
            this.Commando_PersonasDiario = new System.Data.OleDb.OleDbCommand();
            ((System.ComponentModel.ISupportInitialize)(this.dS_Inasistencias1)).BeginInit();
            this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardarCambios_Click);
            this.BJusAccesos.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BJusAccesos_Click);

            // 
            // Conexion
            // 
            this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT EC_SUSCRIPCION.SUSCRIPCION_NOMBRE, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PE" +
                "RSONA_NOMBRE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_ACCESOS.ACCESO_FECHA" +
                "HORA AS ACCESO_E, EC_ACCESOS_1.ACCESO_FECHAHORA AS ACCESO_S, EC_PERSONAS_DIARI" +
                "O.PERSONA_DIARIO_TT, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TDE, EC_TIPO_INC_SIS.TI" +
                "PO_INC_SIS_NOMBRE, EC_INCIDENCIAS.INCIDENCIA_ID, EC_TIPO_INCIDENCIAS.TIPO_INCI" +
                "DENCIA_NOMBRE, EC_INCIDENCIAS.INCIDENCIA_COMENTARIO, EC_ACCESOS.ACCESO_ID, eC" +
                "_GRUPOS_1.SUSCRIPCION_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, EC_TIPO_INCIDENCIA" +
                "S.TIPO_INCIDENCIA_ID, EC_TIPO_INC_SIS.TIPO_INC_SIS_ID, EC_ACCESOS_1.ACCESO_ID " +
                "AS ACCESO_ID, EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS_DIARIO, EC_ACCESOS, eC" +
                "_PERSONAS, EC_SUSCRIPCION, EC_INCIDENCIAS, EC_TIPO_INC_SIS, EC_TIPO_INCIDENCIAS" +
                ", EC_ACCESOS EC_ACCESOS_1 WHERE EC_PERSONAS_DIARIO.ACCESO_E_ID = EC_ACCESOS." +
                "ACCESO_ID AND EC_PERSONAS_DIARIO.PERSONA_ID = EC_PERSONAS.PERSONA_ID AND eC_P" +
                "ERSONAS.SUSCRIPCION_ID = EC_SUSCRIPCION.SUSCRIPCION_ID AND EC_PERSONAS_DIARIO.INCIDENCIA_" +
                "ID = EC_INCIDENCIAS.INCIDENCIA_ID AND EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID = eC" +
                "_TIPO_INC_SIS.TIPO_INC_SIS_ID AND EC_INCIDENCIAS.TIPO_INCIDENCIA_ID = EC_TIPO_" +
                "INCIDENCIAS.TIPO_INCIDENCIA_ID AND EC_PERSONAS_DIARIO.ACCESO_S_ID = EC_ACCESOS" +
                "_1.ACCESO_ID AND (EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= ?) AND (eC_PERSON" +
                "AS_DIARIO.PERSONA_DIARIO_FECHA < ?) ORDER BY EC_SUSCRIPCION.SUSCRIPCION_NOMBRE, eC_PE" +
                "RSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS_DIARIO.PERSONA" +
                "_DIARIO_FECHA";
            this.oleDbSelectCommand1.Connection = this.Conexion;
            this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
            this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA1", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_INCIDENCIAS (INCIDENCIA_ID, TIPO_INCIDENCIA_ID, INCIDENCIA_COMENT" +
                "ARIO, SESION_ID, INCIDENCIA_FECHAHORA) VALUES (?, ?, ?, ?, ?)";
            this.oleDbInsertCommand1.Connection = this.Conexion;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_COMENTARIO", System.Data.OleDb.OleDbType.VarChar, 255, "INCIDENCIA_COMENTARIO"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("SESION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SESION_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_FECHAHORA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "INCIDENCIA_FECHAHORA"));
            // 
            // DA_Inasistencias
            // 
            this.DA_Inasistencias.DeleteCommand = this.oleDbDeleteCommand1;
            this.DA_Inasistencias.InsertCommand = this.oleDbInsertCommand1;
            this.DA_Inasistencias.SelectCommand = this.oleDbSelectCommand1;
            this.DA_Inasistencias.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS_DIARIO", new System.Data.Common.DataColumnMapping[] {
																																																							  new System.Data.Common.DataColumnMapping("SUSCRIPCION_NOMBRE", "SUSCRIPCION_NOMBRE"),
																																																							  new System.Data.Common.DataColumnMapping("PERSONA_LINK_ID", "PERSONA_LINK_ID"),
																																																							  new System.Data.Common.DataColumnMapping("PERSONA_NOMBRE", "PERSONA_NOMBRE"),
																																																							  new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_FECHA", "PERSONA_DIARIO_FECHA"),
																																																							  new System.Data.Common.DataColumnMapping("ACCESO_E", "ACCESO_E"),
																																																							  new System.Data.Common.DataColumnMapping("ACCESO_S", "ACCESO_S"),
																																																							  new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TT", "PERSONA_DIARIO_TT"),
																																																							  new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TDE", "PERSONA_DIARIO_TDE"),
																																																							  new System.Data.Common.DataColumnMapping("TIPO_INC_SIS_NOMBRE", "TIPO_INC_SIS_NOMBRE"),
																																																							  new System.Data.Common.DataColumnMapping("INCIDENCIA_ID", "INCIDENCIA_ID"),
																																																							  new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_NOMBRE", "TIPO_INCIDENCIA_NOMBRE"),
																																																							  new System.Data.Common.DataColumnMapping("INCIDENCIA_COMENTARIO", "INCIDENCIA_COMENTARIO"),
																																																							  new System.Data.Common.DataColumnMapping("ACCESO_ID", "ACCESO_ID"),
																																																							  new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																							  new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_ID", "PERSONA_DIARIO_ID"),
																																																							  new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_ID"),
																																																							  new System.Data.Common.DataColumnMapping("TIPO_INC_SIS_ID", "TIPO_INC_SIS_ID"),
																																																							  new System.Data.Common.DataColumnMapping("ACCESO_ID1", "ACCESO_ID1"),
																																																							  new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID")})});
            this.DA_Inasistencias.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // dS_Inasistencias1
            // 
            this.dS_Inasistencias1.DataSetName = "DS_Inasistencias";
            this.dS_Inasistencias1.Locale = new System.Globalization.CultureInfo("es-ES");
            // 
            // oleDbSelectCommand2
            // 
            this.oleDbSelectCommand2.CommandText = "SELECT TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE, TIPO_INCIDENCIA_ABR, TIPO_INCI" +
                "DENCIA_BORRADO FROM EC_TIPO_INCIDENCIAS WHERE (TIPO_INCIDENCIA_BORRADO = 0)";
            this.oleDbSelectCommand2.Connection = this.Conexion;
            // 
            // DA_TipoIncidencias
            // 
            this.DA_TipoIncidencias.DeleteCommand = this.oleDbDeleteCommand2;
            this.DA_TipoIncidencias.InsertCommand = this.oleDbInsertCommand2;
            this.DA_TipoIncidencias.SelectCommand = this.oleDbSelectCommand2;
            this.DA_TipoIncidencias.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										 new System.Data.Common.DataTableMapping("Table", "EC_TIPO_INCIDENCIAS", new System.Data.Common.DataColumnMapping[] {
																																																								 new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_ID"),
																																																								 new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_NOMBRE", "TIPO_INCIDENCIA_NOMBRE"),
																																																								 new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ABR", "TIPO_INCIDENCIA_ABR"),
																																																								 new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_BORRADO", "TIPO_INCIDENCIA_BORRADO")})});
            this.DA_TipoIncidencias.UpdateCommand = this.oleDbUpdateCommand2;
            // 
            // DA_Edicion_PersonaDiario
            // 
            this.DA_Edicion_PersonaDiario.DeleteCommand = this.oleDbDeleteCommand3;
            this.DA_Edicion_PersonaDiario.InsertCommand = this.oleDbInsertCommand4;
            this.DA_Edicion_PersonaDiario.SelectCommand = this.oleDbSelectCommand4;
            this.DA_Edicion_PersonaDiario.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											   new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS_DIARIO", new System.Data.Common.DataColumnMapping[] {
																																																									  new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_ID", "PERSONA_DIARIO_ID"),
																																																									  new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																									  new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_FECHA", "PERSONA_DIARIO_FECHA"),
																																																									  new System.Data.Common.DataColumnMapping("INCIDENCIA_ID", "INCIDENCIA_ID")})});
            this.DA_Edicion_PersonaDiario.UpdateCommand = this.oleDbUpdateCommand3;
            // 
            // oleDbDeleteCommand3
            // 
            this.oleDbDeleteCommand3.CommandText = "DELETE FROM EC_PERSONAS_DIARIO WHERE (PERSONA_DIARIO_ID = ?) AND (INCIDENCIA_ID " +
                "= ?) AND (PERSONA_DIARIO_FECHA = ?) AND (PERSONA_ID = ?)";
            this.oleDbDeleteCommand3.Connection = this.Conexion;
            this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_DIARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_DIARIO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERSONA_DIARIO_FECHA", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Original, null));
            // 
            // oleDbInsertCommand4
            // 
            this.oleDbInsertCommand4.CommandText = "INSERT INTO EC_PERSONAS_DIARIO(PERSONA_DIARIO_ID, PERSONA_ID, PERSONA_DIARIO_FEC" +
                "HA, INCIDENCIA_ID) VALUES (?, ?, ?, ?)";
            this.oleDbInsertCommand4.Connection = this.Conexion;
            this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_DIARIO_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
            this.oleDbInsertCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            // 
            // oleDbSelectCommand4
            // 
            this.oleDbSelectCommand4.CommandText = "SELECT PERSONA_DIARIO_ID, PERSONA_ID, PERSONA_DIARIO_FECHA, INCIDENCIA_ID FROM IT" +
                "W_PERSONAS_DIARIO ORDER BY PERSONA_ID";
            this.oleDbSelectCommand4.Connection = this.Conexion;
            // 
            // oleDbUpdateCommand3
            // 
            this.oleDbUpdateCommand3.CommandText = "UPDATE EC_PERSONAS_DIARIO SET PERSONA_DIARIO_ID = ?, PERSONA_ID = ?, PERSONA_DIA" +
                "RIO_FECHA = ?, INCIDENCIA_ID = ? WHERE (PERSONA_DIARIO_ID = ?) AND (INCIDENCIA_I" +
                "D = ?) AND (PERSONA_DIARIO_FECHA = ?) AND (PERSONA_ID = ?)";
            this.oleDbUpdateCommand3.Connection = this.Conexion;
            this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_DIARIO_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
            this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_DIARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_DIARIO_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERSONA_DIARIO_FECHA", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Original, null));
            // 
            // DA_Incidencias
            // 
            this.DA_Incidencias.InsertCommand = this.oleDbCommand1;
            this.DA_Incidencias.SelectCommand = this.oleDbCommand2;
            this.DA_Incidencias.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									 new System.Data.Common.DataTableMapping("Table", "EC_INCIDENCIAS", new System.Data.Common.DataColumnMapping[] {
																																																						new System.Data.Common.DataColumnMapping("INCIDENCIA_ID", "INCIDENCIA_ID"),
																																																						new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_ID"),
																																																						new System.Data.Common.DataColumnMapping("INCIDENCIA_COMENTARIO", "INCIDENCIA_COMENTARIO"),
																																																						new System.Data.Common.DataColumnMapping("SESION_ID", "SESION_ID"),
																																																						new System.Data.Common.DataColumnMapping("INCIDENCIA_FECHAHORA", "INCIDENCIA_FECHAHORA")})});
            this.DA_Incidencias.UpdateCommand = this.oleDbCommand3;
            // 
            // oleDbCommand1
            // 
            this.oleDbCommand1.CommandText = "INSERT INTO EC_INCIDENCIAS (INCIDENCIA_ID, TIPO_INCIDENCIA_ID, INCIDENCIA_COMENT" +
                "ARIO, SESION_ID, INCIDENCIA_FECHAHORA) VALUES (?, ?, ?, ?, ?)";
            this.oleDbCommand1.Connection = this.Conexion;
            this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_COMENTARIO", System.Data.OleDb.OleDbType.VarChar, 255, "INCIDENCIA_COMENTARIO"));
            this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("SESION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SESION_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_FECHAHORA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "INCIDENCIA_FECHAHORA"));
            // 
            // oleDbCommand2
            // 
            this.oleDbCommand2.CommandText = "SELECT INCIDENCIA_ID, TIPO_INCIDENCIA_ID, INCIDENCIA_COMENTARIO, SESION_ID, TO_DA" +
                "TE(INCIDENCIA_FECHAHORA) AS INCIDENCIA_FECHAHORA FROM EC_INCIDENCIAS";
            this.oleDbCommand2.Connection = this.Conexion;
            // 
            // oleDbCommand3
            // 
            this.oleDbCommand3.CommandText = "UPDATE EC_INCIDENCIAS SET TIPO_INCIDENCIA_ID = ?, INCIDENCIA_COMENTARIO = ?, INC" +
                "IDENCIA_FECHAHORA = ?, SESION_ID = ? WHERE (INCIDENCIA_ID = ?)";
            this.oleDbCommand3.Connection = this.Conexion;
            this.oleDbCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_COMENTARIO", System.Data.OleDb.OleDbType.VarChar, 255, "INCIDENCIA_COMENTARIO"));
            this.oleDbCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_FECHAHORA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "INCIDENCIA_FECHAHORA"));
            this.oleDbCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("SESION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SESION_ID", System.Data.DataRowVersion.Current, null));
            this.oleDbCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Original, null));
            // 
            // Commando_PersonasDiario
            // 
            this.Commando_PersonasDiario.CommandText = "UPDATE EC_PERSONAS_DIARIO SET INCIDENCIA_ID = ? WHERE (PERSONA_DIARIO_FECHA >= ?" +
                ") AND (PERSONA_ID = ?) AND (PERSONA_DIARIO_FECHA <= ?)";
            this.Commando_PersonasDiario.Connection = this.Conexion;
            this.Commando_PersonasDiario.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
            this.Commando_PersonasDiario.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Original, null));
            this.Commando_PersonasDiario.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERSONA_DIARIO_FECHA", System.Data.DataRowVersion.Original, null));
            this.Commando_PersonasDiario.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_DIARIO_FECHA1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERSONA_DIARIO_FECHA", System.Data.DataRowVersion.Original, null));

            ((System.ComponentModel.ISupportInitialize)(this.dS_Inasistencias1)).EndInit();

        }
        #endregion

        protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            try
            {
                DataSet siguiente = new DataSet();
                string qry = "";
                int indice = TipoIncidenciaNombre.SelectedIndex;
                int ret = 0;
                string IncidenciaComentario = IncidenciaC.Text;
                int Tipincidencia = 0;
                Tipincidencia = Convert.ToInt32(TipoIncidenciaNombre.DataValue);
                //Inserta la Incidencia
                int reto = 0;
                int Incidencias_id = -1;
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    if (Grid.Rows[i].Selected)
                    {
                        if (reto == 0)
                        {
                            Incidencias_id = CeC_Autonumerico.GeneraAutonumerico("EC_INCIDENCIAS", "INCIDENCIA_ID");
                            CeC_BD.EjecutaComando("INSERT INTO EC_INCIDENCIAS(INCIDENCIA_ID,TIPO_INCIDENCIA_ID,INCIDENCIA_COMENTARIO,INCIDENCIA_FECHAHORA,SESION_ID) VALUES (" + Incidencias_id + "," + Tipincidencia + ",'" + IncidenciaComentario + "'," + CeC_BD.SqlFechaHora(DateTime.Now) + "," + Sesion.SESION_ID + ")");
                        }
                        int PersonaDID = Convert.ToInt32(Grid.Rows[i].Cells[14].Value);
                        if (Convert.ToInt32(TipoIncidenciaNombre.SelectedIndex) < 0)
                        {
                            LError.Text = "Debes de seleccioar una fila";
                        }
                        reto += CeC_BD.EjecutaComando("Update EC_PERSONAS_DIARIO SET INCIDENCIA_ID = " + Incidencias_id + " WHERE PERSONA_DIARIO_ID = " + PersonaDID);
                        if (reto > 0)
                        {
                            LError.Text = "";
                            LCorrecto.Text = reto + " Inasistencias justificadas correctamente";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LError.Text = ex.Message.ToString();
            }
        }

        protected void BJustificar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            try
            {
                //Inserta la Incidencia
                int indice = TipoIncidenciaNombre.SelectedIndex;
                int ret = 0;
                string IncidenciaComentario = IncidenciaC.Text;
                int Tipincidencia = 0;
                Tipincidencia = Convert.ToInt32(TipoIncidenciaNombre.SelectedCell.Row.Cells[0].Value);
                int Incidencias_id = CeC_Autonumerico.GeneraAutonumerico("EC_INCIDENCIAS", "INCIDENCIA_ID");
                CeC_BD.EjecutaComando("INSERT INTO EC_INCIDENCIAS(INCIDENCIA_ID,TIPO_INCIDENCIA_ID,INCIDENCIA_COMENTARIO,INCIDENCIA_FECHAHORA,SESION_ID) VALUES (" + Incidencias_id + "," + Tipincidencia + ",'" + IncidenciaComentario + "'," + CeC_BD.SqlFechaHora(DateTime.Now) + "," + Sesion.SESION_ID + ")");
                //Inserta la Incidencia
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    int PersonaDID = Convert.ToInt32(Grid.Rows[i].Cells[15].Value);
                    if (Convert.ToInt32(TipoIncidenciaNombre.SelectedIndex) > -1)
                    {
                        LError.Text = "Debes de seleccioar una fila";
                    }
                    int reto = CeC_BD.EjecutaComando("Update EC_PERSONAS_DIARIO SET INCIDENCIA_ID = " + Incidencias_id + " WHERE PERSONA_DIARIO_ID = " + PersonaDID);
                    if (reto > 0)
                    {
                        LError.Text = "";
                        LCorrecto.Text = "Inasistencias justificadas correctamente";
                        //MuestraControles(2);
                    }
                }
                Sesion.WF_EmpleadosFil_Qry = "";
            }
            catch (Exception ex)
            {
                LCorrecto.Text = "";
                LError.Text = ex.Message.ToString();
            }
        }

        protected void BJusAccesos_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    Sesion.WF_Personas_Diario_Fecha = Convert.ToDateTime(Grid.Rows[i].Cells[3].Value);
                    Sesion.WF_Empleados_PERSONA_ID = Convert.ToInt32(Grid.Rows[i].Cells[18].Value);
                    Sesion.WF_Empleados_PERSONA_NOMBRE = CeC_BD.ObtenPersonaNombre(Sesion.WF_Empleados_PERSONA_ID);
                    Sesion.WF_Empleados_PERSONA_LINK_ID = Convert.ToInt32(Grid.Rows[i].Cells[1].Value);
                    Selecciona(false);
                    Sesion.Redirige("WF_Personas_DiarioE.aspx");
                    Sesion.WF_EmpleadosFil_Qry = "";
                }
            }
        }

        protected void Selecciona(bool Sele)
        {
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                if (Sele)
                    Grid.Rows[i].Cells[0].Value = 1;
                else
                    Grid.Rows[i].Cells[0].Value = 0;
            }
        }

        protected void BRegresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Sesion.Redirige("WF_Inasistencias.aspx");
        }

        protected void BSelección_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Selecciona(true);
        }

        protected void BAnular_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
            Selecciona(false);
        }

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid);
            try
            {
                Grid.Columns.FromKey("persona_id").Hidden = true;
                Grid.Columns.FromKey("Acceso_id1").Hidden = true;
                Grid.Columns.FromKey("acceso_id").Hidden = true;
                Grid.Columns.FromKey("incidencia_id").Hidden = true;
                Grid.Columns.FromKey("acceso_e").Hidden = true;
                Grid.Columns.FromKey("acceso_s").Hidden = true;
                Grid.Columns.FromKey("tipo_inc_sis_id").Hidden = true;
                Grid.Columns.FromKey("persona_diario_id").Hidden = true;
                Grid.Columns.FromKey("Justifica").Hidden = true;
                Grid.Columns.FromKey("tipo_incidencia_id").Hidden = true;
                Grid.Columns.FromKey("SUSCRIPCION_ID").Hidden = true;
                Grid.Columns.FromKey("persona_diario_fecha").EditorControlID = "txthora";
                Grid.Columns.FromKey("persona_diario_fecha").Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Custom;
            }
            catch (Exception ex)
            { }
        }
        protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
        {
            try
            {
                Sesion = CeC_Sesion.Nuevo(this);
                DateTime Fecha_I = Sesion.WF_EmpleadosFil_FechaI;
                DateTime Fecha_F = Sesion.WF_EmpleadosFil_FechaF;

                DA_Inasistencias.SelectCommand.CommandText = DA_Inasistencias.SelectCommand.CommandText.Replace("ORDER", " AND EC_PERSONAS_DIARIO.PERSONA_ID IN (" + Sesion.WF_EmpleadosFil_Qry_Temp + ") " + QueryRemplazo + " ORDER ");

                DA_Inasistencias.SelectCommand.Parameters["PERSONA_DIARIO_FECHA"].Value = Fecha_I;
                DA_Inasistencias.SelectCommand.Parameters["PERSONA_DIARIO_FECHA1"].Value = Fecha_F;

                if (Sesion.WF_EmpleadosFil_DNL == 0)
                    DA_Inasistencias.SelectCommand.CommandText = DA_Inasistencias.SelectCommand.CommandText.Replace("ORDER", "AND (EC_TIPO_INC_SIS.TIPO_INC_SIS_ID <> 10) AND (EC_TIPO_INC_SIS.TIPO_INC_SIS_ID <> 11) ORDER");
                DA_Inasistencias.SelectCommand.CommandText = DA_Inasistencias.SelectCommand.CommandText.Replace("ORDER", " AND (EC_TIPO_INC_SIS.TIPO_INC_SIS_ID <> 1) AND (EC_PERSONAS_DIARIO.INCIDENCIA_ID = 0) AND (EC_TIPO_INC_SIS.TIPO_INC_SIS_ID <> 0) ORDER ");
                DA_Inasistencias.SelectCommand.Connection.ConnectionString = CeC_BD.CadenaConexion();
                DA_Inasistencias.Fill(dS_Inasistencias1);

                Conexion.Close();
                Conexion.Dispose();

                Grid.DataSource = dS_Inasistencias1.EC_PERSONAS_DIARIO;
            }
            catch (Exception ex)
            { }
        }
    }
}
