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
	/// Descripción breve de WF_IncidenciasE.
	/// </summary>
	public partial class WF_IncidenciasE : System.Web.UI.Page
	{
		protected System.Data.OleDb.OleDbDataAdapter DA_Incidencias;
		protected System.Data.OleDb.OleDbConnection Conexion;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbCommand1;
		protected DS_Incidencias dS_Incidencias1;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		protected System.Data.OleDb.OleDbDataAdapter DA_Personas_Diario;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
		protected System.Data.OleDb.OleDbDataAdapter DA_TipoIncidencia;
		protected System.Data.OleDb.OleDbCommand Commando_PersonasDiario;
		protected System.Data.OleDb.OleDbDataAdapter DA_Edicion_PersonaDiario;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand4;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand4;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand3;
		CeC_Sesion Sesion;
		string QueryRemplazo = "";
	
		private void Habilitarcontroles()
		{
			if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Empleados0Incidencias_Justificacion0Borrar) && !Sesion.TienePermiso(CEC_RESTRICCIONES.S0Empleados0Incidencias_Justificacion0Grupo))
			{
				BDeshacerCambios.Visible = false;
                BGuardarCambios.Visible = false;
                Grid.Visible = false;
                TipoIncidenciaNombre.Visible = false;
                FechaI.Visible = false;
                FechaF.Visible = false;
                IncidenciaC.Visible = false;
                Label1.Visible = false;
                Label2.Visible = false;
                Label3.Visible = false;
                Comentariolabel.Visible = false;
                CBBorrarI.Visible = false;
                WebPanel2.Visible = false;
                CBListado.Visible = false;
			}
			if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Empleados0Incidencias_Justificacion0Borrar))
			{
                CBBorrarI.Visible = false;
			}
			if (Sesion.TienePermiso(CEC_RESTRICCIONES.S0Empleados0Incidencias_Justificacion0Grupo))
			{
				QueryRemplazo = "and EC_PERSONAS.SUSCRIPCION_ID in (Select EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID from EC_PERMISOS_SUSCRIP where EC_PERMISOS_SUSCRIP.usuario_id = "+Sesion.USUARIO_ID+")";
			}
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aquí el código de usuario para inicializar la página

			Grid.DisplayLayout.CellClickActionDefault=Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
            CeC_Grid.AplicaFormato(Grid);
			Sesion = CeC_Sesion.Nuevo(this);
			Sesion.TituloPagina = "Incidencias-Edición";
			Sesion.DescripcionPagina = "Ingrese los datos para crear o modificar la incidencia";
	
            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0Empleados0Incidencias_Justificacion, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

			if (!IsPostBack)
			{
               
				if (Sesion.WF_EmpleadosFil_Qry.Length< 1)
				{					
					Sesion.WF_EmpleadosFil_HayDiasLaborables = false;
                    TipoIncidenciaNombre.Visible = CBBorrarI.Checked;
                    IncidenciaC.Visible = CBBorrarI.Checked;
                    Label3.Visible = CBBorrarI.Checked;
                    Comentariolabel.Visible = CBBorrarI.Checked;

                    Sesion.WF_EmpleadosFil(true, true, false, "Muestra Resultados",
                 "Nueva Justificación y/o Incidencia", "WF_IncidenciasE.aspx",
                "", false, true, true);
                    return;

				}
				else
				{
					Sesion.WF_EmpleadosFil_Qry_Temp = Sesion.WF_EmpleadosFil_Qry;
					Sesion.WF_EmpleadosFil_Qry = "";

				}
			}
		
				DA_Personas_Diario.SelectCommand.CommandText = DA_Personas_Diario.SelectCommand.CommandText.Replace("ORDER", " AND EC_PERSONAS.PERSONA_ID IN ("+Sesion.WF_EmpleadosFil_Qry_Temp+") "+QueryRemplazo+" ORDER ");
	//			DA_Personas_Diario.Fill(dS_Incidencias1.EC_PERSONASINCIDENCIASE);

			DA_TipoIncidencia.Fill(dS_Incidencias1.EC_TIPO_INCIDENCIAS);

			/*DS_Incidencias.EC_TIPO_INCIDENCIASRow Fila = dS_Incidencias1.EC_TIPO_INCIDENCIAS.NewEC_TIPO_INCIDENCIASRow();


				Fila.TIPO_INCIDENCIA_ID = 0;
				Fila.TIPO_INCIDENCIA_NOMBRE = "Sin Incidencia";
				Fila.TIPO_INCIDENCIA_ABR = "SN";
				Fila.TIPO_INCIDENCIA_BORRADO = 0;

			dS_Incidencias1.EC_TIPO_INCIDENCIAS.AddEC_TIPO_INCIDENCIASRow(Fila);*/
			
			NuevoTipoIncidencias(0,"Sin Incidencia","SN");

			int dd = dS_Incidencias1.EC_PERSONASINCIDENCIASE.Rows.Count;

			if(!IsPostBack)
			{
				Grid.DataBind();
				TipoIncidenciaNombre.DataBind();
                TipoIncidenciaNombre.Columns.RemoveAt(3);
				FechaI.Value = Sesion.WF_EmpleadosFil_FechaI;
				FechaF.Value = Sesion.WF_EmpleadosFil_FechaF.AddDays(-1);

                Grid.Height = Unit.Percentage(140);
			}
            Sesion.ControlaBoton(ref BDeshacerCambios);
            Sesion.ControlaBoton(ref BGuardarCambios);
		}

		private void NuevoTipoIncidencias(int key , string Nombre,string Abreviatura)
		{
			DS_Incidencias.EC_TIPO_INCIDENCIASRow Fila = dS_Incidencias1.EC_TIPO_INCIDENCIAS.NewEC_TIPO_INCIDENCIASRow();
			Fila.TIPO_INCIDENCIA_ID = key;
			Fila.TIPO_INCIDENCIA_NOMBRE = Nombre;
			Fila.TIPO_INCIDENCIA_ABR = Abreviatura;
			Fila.TIPO_INCIDENCIA_BORRADO = 0;

			dS_Incidencias1.EC_TIPO_INCIDENCIAS.AddEC_TIPO_INCIDENCIASRow(Fila);
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
			this.DA_Incidencias = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.Conexion = new System.Data.OleDb.OleDbConnection();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbCommand1 = new System.Data.OleDb.OleDbCommand();
			this.dS_Incidencias1 = new DS_Incidencias();
			this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.DA_Personas_Diario = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
			this.DA_TipoIncidencia = new System.Data.OleDb.OleDbDataAdapter();
			this.Commando_PersonasDiario = new System.Data.OleDb.OleDbCommand();
			this.DA_Edicion_PersonaDiario = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand3 = new System.Data.OleDb.OleDbCommand();
			((System.ComponentModel.ISupportInitialize)(this.dS_Incidencias1)).BeginInit();
			this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardarCambios_Click);
			// 
			// DA_Incidencias
			// 
			this.DA_Incidencias.InsertCommand = this.oleDbInsertCommand1;
			this.DA_Incidencias.SelectCommand = this.oleDbSelectCommand1;
			this.DA_Incidencias.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									 new System.Data.Common.DataTableMapping("Table", "EC_INCIDENCIAS", new System.Data.Common.DataColumnMapping[] {
																																																						new System.Data.Common.DataColumnMapping("INCIDENCIA_ID", "INCIDENCIA_ID"),
																																																						new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_ID"),
																																																						new System.Data.Common.DataColumnMapping("INCIDENCIA_COMENTARIO", "INCIDENCIA_COMENTARIO"),
																																																						new System.Data.Common.DataColumnMapping("SESION_ID", "SESION_ID"),
																																																						new System.Data.Common.DataColumnMapping("INCIDENCIA_FECHAHORA", "INCIDENCIA_FECHAHORA")})});
			this.DA_Incidencias.UpdateCommand = this.oleDbCommand1;
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
			// Conexion
			// 
			this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT INCIDENCIA_ID, TIPO_INCIDENCIA_ID, INCIDENCIA_COMENTARIO, SESION_ID, TO_DA" +
				"TE(INCIDENCIA_FECHAHORA) AS INCIDENCIA_FECHAHORA FROM EC_INCIDENCIAS";
			this.oleDbSelectCommand1.Connection = this.Conexion;
			// 
			// oleDbCommand1
			// 
			this.oleDbCommand1.CommandText = "UPDATE EC_INCIDENCIAS SET TIPO_INCIDENCIA_ID = ?, INCIDENCIA_COMENTARIO = ?, INC" +
				"IDENCIA_FECHAHORA = ?, SESION_ID = ? WHERE (INCIDENCIA_ID = ?)";
			this.oleDbCommand1.Connection = this.Conexion;
			this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_COMENTARIO", System.Data.OleDb.OleDbType.VarChar, 255, "INCIDENCIA_COMENTARIO"));
			this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_FECHAHORA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "INCIDENCIA_FECHAHORA"));
			this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("SESION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SESION_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Original, null));
			// 
			// dS_Incidencias1
			// 
			this.dS_Incidencias1.DataSetName = "DS_Incidencias";
			this.dS_Incidencias1.Locale = new System.Globalization.CultureInfo("es-MX");
			// 
			// oleDbSelectCommand2
			// 
            this.oleDbSelectCommand2.CommandText = "SELECT EC_PERSONAS.PERSONA_ID, EC_PERSONAS.PERSONA_NOMBRE, EC_PERSONAS_DATOS.DATARE, EC_PERSONAS_DATOS.DATDEP, EC_PERSONAS_DATOS.DATCCT, EC_PERSONAS_DATOS.CNOCVE, EC_PERSONAS_DATOS.CIACVE, EC_PERSONAS.PERSONA_BORRADO FROM EC_PERSONAS, EC_PERSONAS_DATOS WHERE EC_PERSONAS.PERSONA_LINK_ID =EC_PERSONAS_DATOS." + CeC_Campos.CampoTE_Llave.ToString() + " ORDER BY EC_PERSONAS_DATOS." + CeC_Campos.CampoTE_Llave.ToString();
			this.oleDbSelectCommand2.Connection = this.Conexion;
			// 
			// DA_Personas_Diario
			// 
			this.DA_Personas_Diario.DeleteCommand = this.oleDbDeleteCommand1;
			this.DA_Personas_Diario.InsertCommand = this.oleDbInsertCommand2;
			this.DA_Personas_Diario.SelectCommand = this.oleDbSelectCommand2;
			this.DA_Personas_Diario.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										 new System.Data.Common.DataTableMapping("Table", "EC_PERSONASINCIDENCIASE", new System.Data.Common.DataColumnMapping[] {
																																																									 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_ID", "PERSONA_DIARIO_ID"),
																																																									 new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																									 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_FECHA", "PERSONA_DIARIO_FECHA"),
																																																									 new System.Data.Common.DataColumnMapping("PERSONA_NOMBRE", "PERSONA_NOMBRE"),
																																																									 new System.Data.Common.DataColumnMapping("TIPO_INC_SIS_NOMBRE", "TIPO_INC_SIS_NOMBRE"),
																																																									 new System.Data.Common.DataColumnMapping("TIPO_INC_C_SIS_NOMBRE", "TIPO_INC_C_SIS_NOMBRE")})});
			this.DA_Personas_Diario.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// oleDbSelectCommand3
			// 
			this.oleDbSelectCommand3.CommandText = "SELECT TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE, TIPO_INCIDENCIA_ABR, TIPO_INCI" +
				"DENCIA_BORRADO FROM EC_TIPO_INCIDENCIAS WHERE (TIPO_INCIDENCIA_BORRADO = 0)";
			this.oleDbSelectCommand3.Connection = this.Conexion;
			// 
			// DA_TipoIncidencia
			// 
			this.DA_TipoIncidencia.DeleteCommand = this.oleDbDeleteCommand2;
			this.DA_TipoIncidencia.InsertCommand = this.oleDbInsertCommand3;
			this.DA_TipoIncidencia.SelectCommand = this.oleDbSelectCommand3;
			this.DA_TipoIncidencia.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "EC_TIPO_INCIDENCIAS", new System.Data.Common.DataColumnMapping[] {
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_ID"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_NOMBRE", "TIPO_INCIDENCIA_NOMBRE"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ABR", "TIPO_INCIDENCIA_ABR"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_BORRADO", "TIPO_INCIDENCIA_BORRADO")})});
			this.DA_TipoIncidencia.UpdateCommand = this.oleDbUpdateCommand2;
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
			// 
			// DA_Edicion_PersonaDiario
			// 
			this.DA_Edicion_PersonaDiario.DeleteCommand = this.oleDbDeleteCommand3;
			this.DA_Edicion_PersonaDiario.InsertCommand = this.oleDbInsertCommand4;
			this.DA_Edicion_PersonaDiario.SelectCommand = this.oleDbSelectCommand4;
			this.DA_Edicion_PersonaDiario.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											   new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS_DIARIO_Update", new System.Data.Common.DataColumnMapping[] {
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
				"W_PERSONAS_DIARIO WHERE (PERSONA_ID = ?) AND (PERSONA_DIARIO_FECHA >= ?) AND (PE" +
				"RSONA_DIARIO_FECHA < ?)";
			this.oleDbSelectCommand4.Connection = this.Conexion;
			this.oleDbSelectCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbSelectCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
			this.oleDbSelectCommand4.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA1", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
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
			((System.ComponentModel.ISupportInitialize)(this.dS_Incidencias1)).EndInit();

		}
		#endregion

		protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			FechaI.Value = Sesion.WF_EmpleadosFil_FechaI;
			FechaF.Value = Sesion.WF_EmpleadosFil_FechaF;
			IncidenciaC.Text = "";
		}

		protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			LError.Text = "";
			LCorrecto.Text = "";
			int ret = 0;

			if(!CBBorrarI.Checked)
			{
				if(TipoIncidenciaNombre.SelectedIndex == -1)
				{
					LError.Text = "Debes de seleccionar un tipo de Incidencia";
					return;
				}
			}
            if (FechaF.Text == "Null")
            {
                LError.Text = "Debes de Seleccionar una Fecha Final";
                return;
            }
            if (FechaI.Text == "Null")
            {
                LError.Text = "Debes de Seleccionar una Fecha Inicial";
                return;
            }
            if ((DateTime)FechaI.Value > (DateTime)FechaF.Value)
            {
                LError.Text = "La fecha de Inicio debe de ser Menor o Igual a la Fecha Final";
                return;
            }
			try
			{
				int Incidencias_id = CeC_Autonumerico.GeneraAutonumerico("EC_INCIDENCIAS","INCIDENCIA_ID");
				int indice = TipoIncidenciaNombre.SelectedIndex;
				string IncidenciaComentario  = IncidenciaC.Text;
				int Tipincidencia = 0;

				if (!CBBorrarI.Checked)
				{
					Tipincidencia = Convert.ToInt32(TipoIncidenciaNombre.SelectedCell.Row.Cells[0].Value); 				
					CeC_BD.EjecutaComando("INSERT INTO EC_INCIDENCIAS(INCIDENCIA_ID,TIPO_INCIDENCIA_ID,INCIDENCIA_COMENTARIO,INCIDENCIA_FECHAHORA,SESION_ID) VALUES ("+Incidencias_id+","+Tipincidencia+",'"+IncidenciaComentario+"',"+CeC_BD.SqlFechaHora(DateTime.Now)+","+Sesion.SESION_ID+")");
				}

				for (int i =0; i < Grid.Rows.Count; i++)
				{
                    int PERSONA_ID = Convert.ToInt32(CeC_Personas.ObtenPersonaID(Convert.ToInt32(Grid.Rows[i].DataKey)));
                    string strPERSONANom = Convert.ToString(Grid.Rows[i].Cells[0].Value);
                    if (!CBListado.Checked)
					{
						if(Grid.Rows[i].Selected)
						{

					//CeC_Personas.ObtenPersonaID()
					//int PERSONA_ID = Convert.ToInt32(Grid.Rows[i].Cells[1].Value);

					//		if (Conexion.State!=System.Data.ConnectionState.Open)
					//				Conexion.Open();

							int Persona_Diario_Count_Rows = CeC_BD.EjecutaEscalarInt("Select count(PERSONA_ID) from EC_PERSONAS_DIARIO WHERE EC_PERSONAS_DIARIO.PERSONA_ID ="+PERSONA_ID);
							if (Persona_Diario_Count_Rows<=0)
							{
								CeC_Asistencias.Existe_Persona_in_PersonaDiario(PERSONA_ID);
							}
							DA_Edicion_PersonaDiario.SelectCommand.Parameters["PERSONA_ID"].Value = PERSONA_ID;
							DA_Edicion_PersonaDiario.SelectCommand.Parameters["PERSONA_DIARIO_FECHA"].Value = Convert.ToDateTime(FechaI.Text);
							DA_Edicion_PersonaDiario.SelectCommand.Parameters["PERSONA_DIARIO_FECHA1"].Value = Convert.ToDateTime(FechaF.Text).AddDays(1);
							DA_Edicion_PersonaDiario.Fill(dS_Incidencias1.EC_PERSONAS_DIARIO_Update);
							for (int l =0 ;l<dS_Incidencias1.EC_PERSONAS_DIARIO_Update.Rows.Count; l++)
							{
								DS_Incidencias.EC_PERSONAS_DIARIO_UpdateRow FilaPersona = (DS_Incidencias.EC_PERSONAS_DIARIO_UpdateRow)dS_Incidencias1.EC_PERSONAS_DIARIO_Update.Rows[l];
								
								if(TipoIncidenciaNombre.SelectedRow == null)
								{
									FilaPersona.INCIDENCIA_ID = 0;
								}
								else
								{
									if (!CBBorrarI.Checked)
									{
										FilaPersona.INCIDENCIA_ID = Incidencias_id;
									}
									else
									{
										FilaPersona.INCIDENCIA_ID = 0;
									}
								}
								
							}
							
							 ret += DA_Edicion_PersonaDiario.Update(dS_Incidencias1.EC_PERSONAS_DIARIO_Update);

//							CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET EC_PERSONAS_DIARIO.INCIDENCIA_ID = "+Incidencias_id.ToString()+" WHERE EC_PERSONAS_DIARIO.PERSONA_ID = "+PERSONA_DIARIO_ID.ToString()+" AND EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA <= "+CeC_BD.SqlFechaHora((DateTime)FechaI.Value)+" AND EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= "+CeC_BD.SqlFechaHora((DateTime)FechaF.Value));
							//Agregar ModuloLog***
							if(!CBBorrarI.Checked)
								Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION,"Empleado Incidencias",Incidencias_id,strPERSONANom,Sesion.SESION_ID);
							else
								Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION,"Empleado Incidencias",Tipincidencia,strPERSONANom,Sesion.SESION_ID);

							//*****
						}
					}
					else
					{
//						CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET EC_PERSONAS_DIARIO.INCIDENCIA_ID = "+Incidencias_id.ToString()+" WHERE EC_PERSONAS_DIARIO.PERSONA_ID = "+PERSONA_DIARIO_ID.ToString()+" AND EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA <= " +CeC_BD.SqlFechaHora((DateTime)FechaI.Value)+" AND EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= "+CeC_BD.SqlFechaHora((DateTime)FechaF.Value));

						DA_Edicion_PersonaDiario.SelectCommand.Parameters["PERSONA_ID"].Value = PERSONA_ID;
						DA_Edicion_PersonaDiario.SelectCommand.Parameters["PERSONA_DIARIO_FECHA"].Value = Convert.ToDateTime(FechaI.Text);
						DA_Edicion_PersonaDiario.SelectCommand.Parameters["PERSONA_DIARIO_FECHA1"].Value = Convert.ToDateTime(FechaF.Text).AddDays(1);

						DA_Edicion_PersonaDiario.Fill(dS_Incidencias1.EC_PERSONAS_DIARIO_Update);

						for (int l =0 ;l<dS_Incidencias1.EC_PERSONAS_DIARIO_Update.Rows.Count; l++)
						{
							DS_Incidencias.EC_PERSONAS_DIARIO_UpdateRow FilaPersona = (DS_Incidencias.EC_PERSONAS_DIARIO_UpdateRow)dS_Incidencias1.EC_PERSONAS_DIARIO_Update.Rows[l];
							
							if(Convert.ToInt32(TipoIncidenciaNombre.SelectedRow.Cells[0].Value) == -9999)
							{
								FilaPersona.INCIDENCIA_ID = 0;
							}
							else
							{
								if (!CBBorrarI.Checked)
								{
									FilaPersona.INCIDENCIA_ID = Incidencias_id;
								}
								else
								{
									FilaPersona.INCIDENCIA_ID = 0;
								}
							}
						}
							 ret += DA_Edicion_PersonaDiario.Update(dS_Incidencias1.EC_PERSONAS_DIARIO_Update);
						
							//Agregar ModuloLog***
						if(!CBBorrarI.Checked)
							Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION,"Empleado Incidencias",Incidencias_id,strPERSONANom,Sesion.SESION_ID);
						else
							Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION,"Empleado Incidencias",Tipincidencia,strPERSONANom,Sesion.SESION_ID);
							//*****
					}
				}
				Sesion.WF_EmpleadosFil_Qry  =""; 

				if (ret> 0)
				{
					LCorrecto.Text = "Incidencias Asigandas Satisfactoriamente"; 
					return;
				}
			}
			catch(Exception ex)
			{
				LError.Text = ex.Message;
				return;
			}
			LError.Text = "Debes de seleccionar una fila";
		}

        protected void TipoIncidenciaNombre_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(TipoIncidenciaNombre);
        }

        protected DataSet ds = null;
        
        protected void Grid_DataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
        {
            Sesion = CeC_Sesion.Nuevo(this);
            if (Sesion.WF_EmpleadosFil_Qry_Temp.Length > 0)
            {
                ds = CeC_Campos.ObtenDataSetTEGrid(false, " AND EC_PERSONAS.PERSONA_ID IN (" + Sesion.WF_EmpleadosFil_Qry_Temp + ")");
                Grid.DataSource = ds;
                Grid.DataMember = ds.Tables[0].TableName;
                Grid.DataKeyField = CeC_Campos.CampoTE_Llave;
            }
        }

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            Grid.Attributes.Add("CellClickHandler", "if (Grid_CellClickHandler" +
                    "'Grid',igtbl_getActiveCell('Grid'),0) {return false;})");
            CeC_Grid.AplicaFormato(Grid, false, false, true, false);
            Grid.Height = Unit.Percentage(100);
        }

        protected void CBBorrarI_CheckedChanged(object sender, EventArgs e)
        {
            TipoIncidenciaNombre.Visible = !CBBorrarI.Checked;
            IncidenciaC.Visible = !CBBorrarI.Checked;
            Label3.Visible = !CBBorrarI.Checked;
            Comentariolabel.Visible = !CBBorrarI.Checked;
        }
    }
}