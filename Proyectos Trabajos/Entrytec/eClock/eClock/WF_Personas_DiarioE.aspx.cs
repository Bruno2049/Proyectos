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
	/// Descripción breve de WF_Personas_DiarioE.
	/// </summary>
	public partial class WF_Personas_DiarioE : System.Web.UI.Page
	{
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected System.Data.OleDb.OleDbConnection oleDbConnection1;
		protected System.Data.OleDb.OleDbDataAdapter DAPersonas_Diario;
		protected DS_Personas_Diario DS_Personas_Diario1;
		protected System.Data.OleDb.OleDbDataAdapter DAAccesos;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbCommand1;
		CeC_Sesion Sesion;
		protected System.Data.OleDb.OleDbDataAdapter DATipoIncidencias;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbCommand2;
		protected System.Data.OleDb.OleDbCommand CInsIncidencia;
		protected System.Data.OleDb.OleDbCommand CInsAcceso;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand4;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
		protected System.Data.OleDb.OleDbDataAdapter DA_Inc_Sis;
		DS_Personas_Diario.EC_PERSONAS_DIARIOERow FilaDiario = null;
		
		
		protected void Habilitarcontroles(bool Caso)
		{
			if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Empleados0Consultar_Asistencia0Editar0Sin_Justificar))
			{
				CheckBox1.Visible = Caso;
				Panel1.Visible  = Caso;
				Panel2.Visible = Caso;
				BRegresar.Visible = Caso;
				BDeshacerCambios.Visible = Caso;
				BGuardarCambios.Visible = Caso;
				Grid.Visible = Caso;
				CheckBox1.Checked = Caso;
				Label3.Visible = Caso;
				TipoIncSystem.Visible = Caso;
			}
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//ejecutar siempre para cargar variables de sesión
			Sesion = CeC_Sesion.Nuevo(this);

			// Permisos****************************************
			string [] Permiso = new string [10];
			
			/*Permiso[0] = "S";
			Permiso[1] = "S.Empleados";
			Permiso[2] = "S.Empleados.Editar_Estatus";*/
			Permiso[0] = CEC_RESTRICCIONES.S0Empleados0Consultar_Asistencia0Editar0Sin_Justificar;

			if (!Sesion.Acceso(Permiso,CIT_Perfiles.Acceso(Sesion.PERFIL_ID,this)))
			{
				CIT_Perfiles.CrearVentana(this,Sesion.MensajeVentanaJScript(),Sesion.TituloPagina,"Aceptar","WF_Main.aspx","","");
				Habilitarcontroles(false);
				return;
			}

			Habilitarcontroles(false);

			//**************************************************

			DAAccesos.SelectCommand.Parameters["ACCESO_FECHAHORA"].Value = Sesion.WF_Personas_Diario_Fecha.AddHours(-12);
			DAAccesos.SelectCommand.Parameters["ACCESO_FECHAHORA1"].Value = Sesion.WF_Personas_Diario_Fecha.AddHours(36);
			DAAccesos.SelectCommand.Parameters["PERSONA_ID"].Value = Sesion.WF_Empleados_PERSONA_ID;
			DAAccesos.Fill(DS_Personas_Diario1);
			DAPersonas_Diario.SelectCommand.Parameters["PERSONA_ID"].Value = Sesion.WF_Empleados_PERSONA_ID;
			DAPersonas_Diario.SelectCommand.Parameters["PERSONA_DIARIO_FECHA"].Value = Sesion.WF_Personas_Diario_Fecha;
			DAPersonas_Diario.Fill(DS_Personas_Diario1);

			DA_Inc_Sis.Fill(DS_Personas_Diario1.EC_TIPO_INC_SIS__);

			if(DS_Personas_Diario1.EC_PERSONAS_DIARIOE.Count> 0)
			{
				FilaDiario = DS_Personas_Diario1.EC_PERSONAS_DIARIOE[0];
			}
			DATipoIncidencias.Fill(DS_Personas_Diario1);

			if(!IsPostBack)
			{
                Sesion.WF_Personas_DiarioE_Guardado = 0;
				TipoIncSystem.DataBind();
				
				//Agregar ModuloLog***
				Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA,"Persona Diario Edicion",Sesion.USUARIO_ID,Sesion.USUARIO_NOMBRE,Sesion.SESION_ID);
				//*****				

				Sesion.TituloPagina = "Edición de asistencia del día " + Sesion.WF_Personas_Diario_Fecha.ToString("D");
				Sesion.DescripcionPagina = "Empleado = " +Sesion.WF_Empleados_PERSONA_LINK_ID + " ("+ Sesion.WF_Empleados_PERSONA_NOMBRE+ ")";
				int Persona_ID = Sesion.WF_Empleados_PERSONA_ID;
//				LFecha.Text = Sesion.WF_Personas_Diario_Fecha.ToString("D");
                AccesoEId.Value = Sesion.WF_Personas_Diario_Fecha;
                AccesoSId.Value = Sesion.WF_Personas_Diario_Fecha;
                AccesoCSId.Value = Sesion.WF_Personas_Diario_Fecha;
                AccesoCSId.Value = Sesion.WF_Personas_Diario_Fecha;
				if(FilaDiario != null)
				{
					LAccesoEId.Text = FilaDiario.ACCESO_E_ID.ToString();
					LAccesoSId.Text = FilaDiario.ACCESO_S_ID.ToString();
					LAccesoCSId.Text = FilaDiario.ACCESO_CS_ID.ToString();
                    CeC_Grid.SeleccionaID(TipoIncSystem, FilaDiario.TIPO_INC_SIS_ID);
					LAccesoCRId.Text = FilaDiario.ACCESO_CR_ID.ToString();
					if(!FilaDiario.IsACCESO_ENull())
					{
						this.AccesoEId.Value = FilaDiario.ACCESO_E;
						AccesoEId.ToolTip = AccesoEId.Value.ToString();
						LAccesoEId.ToolTip = LAccesoEId.Text;
						this.LAccesoET.Text = FilaDiario.ACCESO_E_TERMINAL;					
					}
					if(!FilaDiario.IsACCESO_SNull())
					{
						this.AccesoSId.Value = FilaDiario.ACCESO_S;
						AccesoSId.ToolTip = AccesoSId.Value.ToString();
						LAccesoSId.ToolTip = LAccesoSId.Text;
						this.LAccesoST.Text = FilaDiario.ACCESO_S_TERMINAL;			
					}
					if(!FilaDiario.IsACCESO_CSNull())
					{
						this.AccesoCSId.Value = FilaDiario.ACCESO_CS;
						AccesoCSId.ToolTip = AccesoCSId.Value.ToString();
						this.LAccesoCST.Text = FilaDiario.ACCESO_CS_TERMINAL;			
					}
					if(!FilaDiario.IsACCESO_CRNull())
					{
						this.AccesoCRId.Value = FilaDiario.ACCESO_CR;
						AccesoCRId.ToolTip = AccesoCRId.Value.ToString();
						this.LAccesoCRT.Text = FilaDiario.ACCESO_CR_TERMINAL;				
//						LAccesoCRT.ToolTip = FilaDiario.
					}											
					Grid.DataBind();
                    DataSet DS = Cec_Incidencias.ObtenTiposIncidenciasMenu(Sesion.SUSCRIPCION_ID);
                    TipoIncidenciaId.DataSource = DS;
                    TipoIncidenciaId.DataTextField = "TIPO_INCIDENCIA_NOMBRE";
                    TipoIncidenciaId.DataValueField = "TIPO_INCIDENCIA_ID";
					TipoIncidenciaId.DataBind();			
					try
					{
						if(FilaDiario.INCIDENCIA_ID > 0)
						{
							TipoIncidenciaMotivo.Text = FilaDiario.INCIDENCIA_COMENTARIO;
							TipoIncidenciaId.DataValue = FilaDiario.TIPO_INCIDENCIA_ID;
//							Infragistics.WebUI.UltraWebGrid.UltraGridRow GR = TipoIncidenciaId.FindByValue(FilaDiario.TIPO_INCIDENCIA_ID.ToString()).Activate();
//							TipoIncidenciaId.Rows.FromKey(FilaDiario.TIPO_INCIDENCIA_ID.ToString()).Selected = true;;
						}
					}
					catch
					{
					}
				}
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
		protected void InitializeComponent()
		{    
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			this.DAPersonas_Diario = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
			this.oleDbCommand2 = new System.Data.OleDb.OleDbCommand();
			this.DS_Personas_Diario1 = new DS_Personas_Diario();
			this.DAAccesos = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbCommand1 = new System.Data.OleDb.OleDbCommand();
			this.DATipoIncidencias = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.CInsIncidencia = new System.Data.OleDb.OleDbCommand();
			this.CInsAcceso = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
			this.DA_Inc_Sis = new System.Data.OleDb.OleDbDataAdapter();
			((System.ComponentModel.ISupportInitialize)(this.DS_Personas_Diario1)).BeginInit();
			this.BAccesoEId.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BAcceso_Click);
			this.BAccsesoSId.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BAcceso_Click);
			this.BRegresar.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BRegresar_Click);
			this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardarCambios_Click);
			// 
			// DAPersonas_Diario
			// 
			this.DAPersonas_Diario.SelectCommand = this.oleDbSelectCommand1;
			this.DAPersonas_Diario.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS_DIARIOE", new System.Data.Common.DataColumnMapping[] {
																																																								new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_ID", "PERSONA_DIARIO_ID"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_E_ID", "ACCESO_E_ID"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_S_ID", "ACCESO_S_ID"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_CS_ID", "ACCESO_CS_ID"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_CR_ID", "ACCESO_CR_ID"),
																																																								new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																								new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_FECHA", "PERSONA_DIARIO_FECHA"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INC_SIS_ID", "TIPO_INC_SIS_ID"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INC_C_SIS_ID", "TIPO_INC_C_SIS_ID"),
																																																								new System.Data.Common.DataColumnMapping("INCIDENCIA_ID", "INCIDENCIA_ID"),
																																																								new System.Data.Common.DataColumnMapping("TURNO_DIA_ID", "TURNO_DIA_ID"),
																																																								new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TT", "PERSONA_DIARIO_TT"),
																																																								new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TE", "PERSONA_DIARIO_TE"),
																																																								new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TC", "PERSONA_DIARIO_TC"),
																																																								new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_TDE", "PERSONA_DIARIO_TDE"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_E", "ACCESO_E"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_S", "ACCESO_S"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_CS", "ACCESO_CS"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_CR", "ACCESO_CR"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_E_TERMINAL", "ACCESO_E_TERMINAL"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_S_TERMINAL", "ACCESO_S_TERMINAL"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_CS_TERMINAL", "ACCESO_CS_TERMINAL"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_CR_TERMINAL", "ACCESO_CR_TERMINAL"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_ID", "ACCESO_ID"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_E_TERMINAL_ID", "ACCESO_E_TERMINAL_ID"),
																																																								new System.Data.Common.DataColumnMapping("ACCESO_S_TERMINAL_ID", "ACCESO_S_TERMINAL_ID"),
																																																								new System.Data.Common.DataColumnMapping("EXPR1", "EXPR1"),
																																																								new System.Data.Common.DataColumnMapping("EXPR2", "EXPR2"),
																																																								new System.Data.Common.DataColumnMapping("EXPR3", "EXPR3"),
																																																								new System.Data.Common.DataColumnMapping("EXPR5", "EXPR5"),
																																																								new System.Data.Common.DataColumnMapping("EXPR6", "EXPR6"),
																																																								new System.Data.Common.DataColumnMapping("EXPR4", "EXPR4"),
																																																								new System.Data.Common.DataColumnMapping("INCIDENCIA_COMENTARIO", "INCIDENCIA_COMENTARIO"),
																																																								new System.Data.Common.DataColumnMapping("INCIDENCIA_EXTRAS", "INCIDENCIA_EXTRAS"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_ID"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_NOMBRE", "TIPO_INCIDENCIA_NOMBRE"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ABR", "TIPO_INCIDENCIA_ABR")})});
			this.DAPersonas_Diario.UpdateCommand = this.oleDbCommand2;
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, EC_PERSONAS_DIARIO.ACCESO_E_ID, " +
				"EC_PERSONAS_DIARIO.ACCESO_S_ID, EC_PERSONAS_DIARIO.ACCESO_CS_ID, EC_PERSONAS_DI" +
				"ARIO.ACCESO_CR_ID, EC_PERSONAS_DIARIO.PERSONA_ID, EC_PERSONAS_DIARIO.PERSONA_D" +
				"IARIO_FECHA, EC_PERSONAS_DIARIO.TIPO_INC_SIS_ID, EC_PERSONAS_DIARIO.TIPO_INC_C" +
				"_SIS_ID, EC_PERSONAS_DIARIO.INCIDENCIA_ID, EC_PERSONAS_DIARIO.TURNO_DIA_ID, " +
				"EC_PERSONAS_DIARIO.PERSONA_DIARIO_TT, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TE, eC_" +
				"PERSONAS_DIARIO.PERSONA_DIARIO_TC, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TDE, eC_A" +
				"CCESOS.ACCESO_FECHAHORA AS ACCESO_E, EC_ACCESOS_1.ACCESO_FECHAHORA AS ACCESO_S," +
				" EC_ACCESOS_2.ACCESO_FECHAHORA AS ACCESO_CS, EC_ACCESOS_3.ACCESO_FECHAHORA AS " +
				"ACCESO_CR, EC_TERMINALES.TERMINAL_NOMBRE AS ACCESO_E_TERMINAL, EC_TERMINALES_1" +
				".TERMINAL_NOMBRE AS ACCESO_S_TERMINAL, EC_TERMINALES_2.TERMINAL_NOMBRE AS ACCES" +
				"O_CS_TERMINAL, EC_TERMINALES_3.TERMINAL_NOMBRE AS ACCESO_CR_TERMINAL, eC_ACCES" +
				"OS.ACCESO_ID, EC_TERMINALES.TERMINAL_ID AS ACCESO_E_TERMINAL_ID, EC_TERMINALES" +
				"_1.TERMINAL_ID AS ACCESO_S_TERMINAL_ID, EC_ACCESOS_1.ACCESO_ID AS EXPR1, eC_AC" +
				"CESOS_2.ACCESO_ID AS EXPR2, EC_ACCESOS_3.ACCESO_ID AS EXPR3, EC_TERMINALES_2.T" +
				"ERMINAL_ID AS EXPR5, EC_TERMINALES_3.TERMINAL_ID AS EXPR6, EC_INCIDENCIAS.INCI" +
				"DENCIA_ID AS EXPR4, EC_INCIDENCIAS.INCIDENCIA_COMENTARIO, EC_INCIDENCIAS.INCID" +
				"ENCIA_EXTRAS, EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ID, EC_TIPO_INCIDENCIAS.TIPO" +
				"_INCIDENCIA_NOMBRE, EC_TIPO_INCIDENCIAS.TIPO_INCIDENCIA_ABR FROM EC_PERSONAS_D" +
				"IARIO, EC_ACCESOS, EC_ACCESOS EC_ACCESOS_1, EC_ACCESOS EC_ACCESOS_2, eC_AC" +
				"CESOS EC_ACCESOS_3, EC_TERMINALES, EC_TERMINALES EC_TERMINALES_1, eC_TERMIN" +
				"ALES EC_TERMINALES_2, EC_TERMINALES EC_TERMINALES_3, EC_INCIDENCIAS, eC_TIP" +
				"O_INCIDENCIAS WHERE EC_PERSONAS_DIARIO.ACCESO_E_ID = EC_ACCESOS.ACCESO_ID AND " +
				"EC_PERSONAS_DIARIO.ACCESO_S_ID = EC_ACCESOS_1.ACCESO_ID AND EC_PERSONAS_DIARI" +
				"O.ACCESO_CS_ID = EC_ACCESOS_2.ACCESO_ID AND EC_PERSONAS_DIARIO.ACCESO_CR_ID = " +
				"EC_ACCESOS_3.ACCESO_ID AND EC_ACCESOS.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID" +
				" AND EC_ACCESOS_1.TERMINAL_ID = EC_TERMINALES_1.TERMINAL_ID AND EC_ACCESOS_2." +
				"TERMINAL_ID = EC_TERMINALES_2.TERMINAL_ID AND EC_ACCESOS_3.TERMINAL_ID = eC_T" +
				"ERMINALES_3.TERMINAL_ID AND EC_PERSONAS_DIARIO.INCIDENCIA_ID = EC_INCIDENCIAS." +
				"INCIDENCIA_ID AND EC_INCIDENCIAS.TIPO_INCIDENCIA_ID = EC_TIPO_INCIDENCIAS.TIPO" +
				"_INCIDENCIA_ID AND (EC_PERSONAS_DIARIO.PERSONA_ID = ?) AND (EC_PERSONAS_DIARIO" +
				".PERSONA_DIARIO_FECHA = ?)";
			this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
			this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
			// 
			// oleDbConnection1
			// 
			this.oleDbConnection1.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
			// 
			// oleDbCommand2
			// 
			this.oleDbCommand2.CommandText = @"UPDATE EC_PERSONAS_DIARIO SET ACCESO_E_ID = ?, ACCESO_S_ID = ?, ACCESO_CS_ID = ?, ACCESO_CR_ID = ?, PERSONA_ID = ?, PERSONA_DIARIO_FECHA = ?, TIPO_INC_SIS_ID = ?, TIPO_INC_C_SIS_ID = ?, INCIDENCIA_ID = ?, TURNO_DIA_ID = ?, PERSONA_DIARIO_TT = ?, PERSONA_DIARIO_TE = ?, PERSONA_DIARIO_TC = ?, PERSONA_DIARIO_TDE = ? WHERE (PERSONA_DIARIO_ID = ?)";
			this.oleDbCommand2.Connection = this.oleDbConnection1;
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_E_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "ACCESO_E_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_S_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "ACCESO_S_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_CS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "ACCESO_CS_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_CR_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "ACCESO_CR_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_SIS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INC_SIS_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INC_C_SIS_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INC_C_SIS_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_DIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_DIA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_TT", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_TT"));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_TE", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_TE"));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_TC", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_TC"));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_TDE", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_TDE"));
			this.oleDbCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_DIARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_DIARIO_ID", System.Data.DataRowVersion.Original, null));
			// 
			// DS_Personas_Diario1
			// 
			this.DS_Personas_Diario1.DataSetName = "DS_Personas_Diario";
			this.DS_Personas_Diario1.Locale = new System.Globalization.CultureInfo("es-MX");
			// 
			// DAAccesos
			// 
			this.DAAccesos.SelectCommand = this.oleDbSelectCommand2;
			this.DAAccesos.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "EC_ACCESOSE", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ACCESO_ID", "ACCESO_ID"),
																																																				new System.Data.Common.DataColumnMapping("ACCESO_FECHAHORA", "ACCESO_FECHAHORA"),
																																																				new System.Data.Common.DataColumnMapping("TERMINAL_NOMBRE", "TERMINAL_NOMBRE"),
																																																				new System.Data.Common.DataColumnMapping("TIPO_ACCESO_NOMBRE", "TIPO_ACCESO_NOMBRE"),
																																																				new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																				new System.Data.Common.DataColumnMapping("TERMINAL_ID", "TERMINAL_ID"),
																																																				new System.Data.Common.DataColumnMapping("TIPO_ACCESO_ID", "TIPO_ACCESO_ID")})});
			this.DAAccesos.UpdateCommand = this.oleDbCommand1;
			// 
			// oleDbSelectCommand2
			// 
			this.oleDbSelectCommand2.CommandText = @"SELECT EC_ACCESOS.ACCESO_ID, EC_ACCESOS.ACCESO_FECHAHORA, EC_TERMINALES.TERMINAL_NOMBRE, EC_TIPO_ACCESOS.TIPO_ACCESO_NOMBRE, EC_ACCESOS.PERSONA_ID, EC_TERMINALES.TERMINAL_ID, EC_TIPO_ACCESOS.TIPO_ACCESO_ID FROM EC_ACCESOS, EC_TERMINALES, EC_TIPO_ACCESOS WHERE EC_ACCESOS.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND EC_ACCESOS.TIPO_ACCESO_ID = EC_TIPO_ACCESOS.TIPO_ACCESO_ID AND (eC_ACCESOS.ACCESO_FECHAHORA >= ?) AND (eC_ACCESOS.ACCESO_FECHAHORA <= ?) AND (eC_ACCESOS.PERSONA_ID = ?)";
			this.oleDbSelectCommand2.Connection = this.oleDbConnection1;
			this.oleDbSelectCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_FECHAHORA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "ACCESO_FECHAHORA"));
			this.oleDbSelectCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_FECHAHORA1", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "ACCESO_FECHAHORA"));
			this.oleDbSelectCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbCommand1
			// 
			this.oleDbCommand1.Connection = this.oleDbConnection1;
			// 
			// DATipoIncidencias
			// 
			this.DATipoIncidencias.DeleteCommand = this.oleDbDeleteCommand1;
			this.DATipoIncidencias.InsertCommand = this.oleDbInsertCommand1;
			this.DATipoIncidencias.SelectCommand = this.oleDbSelectCommand3;
			this.DATipoIncidencias.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "EC_TIPO_INCIDENCIAS", new System.Data.Common.DataColumnMapping[] {
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ID", "TIPO_INCIDENCIA_ID"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_NOMBRE", "TIPO_INCIDENCIA_NOMBRE"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_ABR", "TIPO_INCIDENCIA_ABR"),
																																																								new System.Data.Common.DataColumnMapping("TIPO_INCIDENCIA_BORRADO", "TIPO_INCIDENCIA_BORRADO")})});
			this.DATipoIncidencias.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// oleDbDeleteCommand1
			// 
			this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_TIPO_INCIDENCIAS WHERE (TIPO_INCIDENCIA_ID = ?) AND (TIPO_INCIDEN" +
				"CIA_ABR = ? OR ? IS NULL AND TIPO_INCIDENCIA_ABR IS NULL) AND (TIPO_INCIDENCIA_B" +
				"ORRADO = ? OR ? IS NULL AND TIPO_INCIDENCIA_BORRADO IS NULL) AND (TIPO_INCIDENCI" +
				"A_NOMBRE = ?)";
			this.oleDbDeleteCommand1.Connection = this.oleDbConnection1;
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ABR", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ABR", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ABR1", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ABR", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_NOMBRE", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand1
			// 
			this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_TIPO_INCIDENCIAS(TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE, TIPO" +
				"_INCIDENCIA_ABR, TIPO_INCIDENCIA_BORRADO) VALUES (?, ?, ?, ?)";
			this.oleDbInsertCommand1.Connection = this.oleDbConnection1;
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TIPO_INCIDENCIA_NOMBRE"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ABR", System.Data.OleDb.OleDbType.VarChar, 2, "TIPO_INCIDENCIA_ABR"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbSelectCommand3
			// 
			this.oleDbSelectCommand3.CommandText = "SELECT TIPO_INCIDENCIA_ID, TIPO_INCIDENCIA_NOMBRE, TIPO_INCIDENCIA_ABR, TIPO_INCI" +
				"DENCIA_BORRADO FROM EC_TIPO_INCIDENCIAS WHERE (TIPO_INCIDENCIA_BORRADO = 0" +
				") ORDER BY TIPO_INCIDENCIA_NOMBRE";
			this.oleDbSelectCommand3.Connection = this.oleDbConnection1;
			// 
			// oleDbUpdateCommand1
			// 
			this.oleDbUpdateCommand1.CommandText = @"UPDATE EC_TIPO_INCIDENCIAS SET TIPO_INCIDENCIA_ID = ?, TIPO_INCIDENCIA_NOMBRE = ?, TIPO_INCIDENCIA_ABR = ?, TIPO_INCIDENCIA_BORRADO = ? WHERE (TIPO_INCIDENCIA_ID = ?) AND (TIPO_INCIDENCIA_ABR = ? OR ? IS NULL AND TIPO_INCIDENCIA_ABR IS NULL) AND (TIPO_INCIDENCIA_BORRADO = ? OR ? IS NULL AND TIPO_INCIDENCIA_BORRADO IS NULL) AND (TIPO_INCIDENCIA_NOMBRE = ?)";
			this.oleDbUpdateCommand1.Connection = this.oleDbConnection1;
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TIPO_INCIDENCIA_NOMBRE"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ABR", System.Data.OleDb.OleDbType.VarChar, 2, "TIPO_INCIDENCIA_ABR"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ABR", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ABR", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_ABR1", System.Data.OleDb.OleDbType.VarChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ABR", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "TIPO_INCIDENCIA_BORRADO", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_INCIDENCIA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_INCIDENCIA_NOMBRE", System.Data.DataRowVersion.Original, null));
			// 
			// CInsIncidencia
			// 
			this.CInsIncidencia.CommandText = "INSERT INTO EC_INCIDENCIAS (INCIDENCIA_ID, TIPO_INCIDENCIA_ID, INCIDENCIA_COMENT" +
				"ARIO, INCIDENCIA_FECHAHORA, INCIDENCIA_EXTRAS, SESION_ID) VALUES (?, ?, ?, ?, ?," +
				" ?)";
			this.CInsIncidencia.Connection = this.oleDbConnection1;
			this.CInsIncidencia.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
			this.CInsIncidencia.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_INCIDENCIA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_INCIDENCIA_ID", System.Data.DataRowVersion.Current, null));
			this.CInsIncidencia.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_COMENTARIO", System.Data.OleDb.OleDbType.VarChar, 255, "INCIDENCIA_COMENTARIO"));
			this.CInsIncidencia.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_FECHAHORA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "INCIDENCIA_FECHAHORA"));
			this.CInsIncidencia.Parameters.Add(new System.Data.OleDb.OleDbParameter("INCIDENCIA_EXTRAS", System.Data.OleDb.OleDbType.VarChar, 255, "INCIDENCIA_EXTRAS"));
			this.CInsIncidencia.Parameters.Add(new System.Data.OleDb.OleDbParameter("SESION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SESION_ID", System.Data.DataRowVersion.Current, null));
			// 
			// CInsAcceso
			// 
			this.CInsAcceso.CommandText = "INSERT INTO EC_ACCESOS (ACCESO_ID, PERSONA_ID, TERMINAL_ID, ACCESO_FECHAHORA, TI" +
				"PO_ACCESO_ID) VALUES (?, ?, ?, ?, ?)";
			this.CInsAcceso.Connection = this.oleDbConnection1;
			this.CInsAcceso.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "ACCESO_ID", System.Data.DataRowVersion.Current, null));
			this.CInsAcceso.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
			this.CInsAcceso.Parameters.Add(new System.Data.OleDb.OleDbParameter("TERMINAL_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TERMINAL_ID", System.Data.DataRowVersion.Current, null));
			this.CInsAcceso.Parameters.Add(new System.Data.OleDb.OleDbParameter("ACCESO_FECHAHORA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "ACCESO_FECHAHORA"));
			this.CInsAcceso.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_ACCESO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_ACCESO_ID", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbSelectCommand4
			// 
			this.oleDbSelectCommand4.CommandText = "SELECT TIPO_INC_SIS_ID, TIPO_INC_SIS_NOMBRE, TIPO_INC_SIS_ABR FROM EC_TIPO_INC_S" +
				"IS WHERE (TIPO_INC_SIS_ID <> 0)";
			this.oleDbSelectCommand4.Connection = this.oleDbConnection1;
			// 
			// DA_Inc_Sis
			// 
			this.DA_Inc_Sis.DeleteCommand = this.oleDbDeleteCommand2;
			this.DA_Inc_Sis.InsertCommand = this.oleDbInsertCommand2;
			this.DA_Inc_Sis.SelectCommand = this.oleDbSelectCommand4;
			this.DA_Inc_Sis.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								 new System.Data.Common.DataTableMapping("Table", "EC_TIPO_INC_SIS__", new System.Data.Common.DataColumnMapping[] {
																																																					   new System.Data.Common.DataColumnMapping("TIPO_INC_SIS_ID", "TIPO_INC_SIS_ID"),
																																																					   new System.Data.Common.DataColumnMapping("TIPO_INC_SIS_NOMBRE", "TIPO_INC_SIS_NOMBRE"),
																																																					   new System.Data.Common.DataColumnMapping("TIPO_INC_SIS_ABR", "TIPO_INC_SIS_ABR")})});
			this.DA_Inc_Sis.UpdateCommand = this.oleDbUpdateCommand2;
			((System.ComponentModel.ISupportInitialize)(this.DS_Personas_Diario1)).EndInit();

		}
		#endregion

		protected void BAcceso_Click(object sender, System.EventArgs e)
		{
		}

		protected int InsertaAcceso(DateTime Hora)
		{
			DateTime FechaHora = FilaDiario.PERSONA_DIARIO_FECHA + Hora.TimeOfDay;
			int ACCESO_ID = CeC_Autonumerico.GeneraAutonumerico("EC_ACCESOS","ACCESO_ID");

			if (oleDbConnection1.State != System.Data.ConnectionState.Open)
					oleDbConnection1.Open();
			
			CInsAcceso.Parameters["ACCESO_ID"].Value = ACCESO_ID;
			CInsAcceso.Parameters["PERSONA_ID"].Value = FilaDiario.PERSONA_ID;
			CInsAcceso.Parameters["TERMINAL_ID"].Value = 0;
			CInsAcceso.Parameters["ACCESO_FECHAHORA"].Value = FechaHora;
			CInsAcceso.Parameters["TIPO_ACCESO_ID"].Value = 1;
			
			CInsAcceso.ExecuteNonQuery();
			return ACCESO_ID;
		}

		protected void BRegresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			Sesion.WF_EmpleadosBus_Query = Sesion.WF_EmpleadosBus_Query_Temp;
			Sesion.Redirige("WF_Personas_Diario.aspx");
		}

		protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			LCorrecto.Text = "";
			LError.Text = "";
            if (Sesion.WF_Personas_DiarioE_Guardado < 1)
            {
                if (FilaDiario != null)
                {
                    //				if(
                    int Index = TipoIncidenciaId.SelectedIndex;
                    if (Index < 0 && !CheckBox1.Checked)
                    {
                        LError.Text = "Atención: es necesario seleccionar el tipo de justificación";
                        return;
                    }
                    int TipoIncidencia_ID = Convert.ToInt32(TipoIncidenciaId.DataValue);

                    if (TipoIncidenciaMotivo.Text.Length <= 0 && !CheckBox1.Checked)
                    {
                        LError.Text = "Atención: es necesario escribir un motivo de justificación (Comentario)";
                        return;
                    }

                    try
                    {

                        int Incidencia_ID = Convert.ToInt32(FilaDiario.INCIDENCIA_ID);
                        if (!CheckBox1.Checked)
                        {
                            if (oleDbConnection1.State != ConnectionState.Open)
                                oleDbConnection1.Open();

                            Incidencia_ID = CeC_Autonumerico.GeneraAutonumerico("EC_INCIDENCIAS", "INCIDENCIA_ID");
                            this.CInsIncidencia.Parameters["INCIDENCIA_ID"].Value = Incidencia_ID;
                            this.CInsIncidencia.Parameters["TIPO_INCIDENCIA_ID"].Value = TipoIncidencia_ID;
                            this.CInsIncidencia.Parameters["INCIDENCIA_COMENTARIO"].Value = TipoIncidenciaMotivo.Text;
                            this.CInsIncidencia.Parameters["INCIDENCIA_FECHAHORA"].Value = DateTime.Now;
                            this.CInsIncidencia.Parameters["INCIDENCIA_EXTRAS"].Value = "";
                            this.CInsIncidencia.Parameters["SESION_ID"].Value = Sesion.SESION_ID;

                            CInsIncidencia.ExecuteNonQuery();

                            FilaDiario.INCIDENCIA_ID = Incidencia_ID;
                        }
                        else
                        {
                            if (TipoIncSystem.SelectedIndex != -1)
                            {
                                FilaDiario.INCIDENCIA_ID = 0;
                                FilaDiario.TIPO_INC_SIS_ID = Convert.ToInt32(TipoIncSystem.SelectedRow.Cells[0].Text);
                            }
                            else
                            {
                                LError.Text = "La operación no es valida. Se requiere seleccionar un tipo de incidencia del sistema";
                                return;
                            }

                        }
                        if ((AccesoEId.Date > AccesoSId.Date) && (AccesoSId.Text != ""))
                        {
                            AccesoSId.Date = AccesoSId.Date.AddDays(1);
                        }
                        if ((AccesoEId.Date > AccesoSId.Date)&&(AccesoEId.Text != "" && AccesoSId.Text != ""))
                        {
                            
                            LError.Text = "La hora de salida es menor a la de entrada";
                            return;
                        }
                        if (AccesoEId.Value.ToString() != AccesoEId.ToolTip && AccesoEId.Text != "")
                            FilaDiario.ACCESO_E_ID = InsertaAcceso(Convert.ToDateTime(AccesoEId.Value));
                        else
                            if (LAccesoEId.Text.Length > 0)
                                FilaDiario.ACCESO_E_ID = Convert.ToInt32(LAccesoEId.Text);

                        if (AccesoSId.Value.ToString() != AccesoSId.ToolTip && AccesoSId.Text != "")
                            FilaDiario.ACCESO_S_ID = InsertaAcceso(Convert.ToDateTime(AccesoSId.Value));
                        else
                            if (LAccesoSId.Text.Length > 0)
                                FilaDiario.ACCESO_S_ID = Convert.ToInt32(LAccesoSId.Text);

                        if (LAccesoCSId.Text.Length > 0)
                            FilaDiario.ACCESO_CS_ID = Convert.ToInt32(LAccesoCSId.Text);
                        if (LAccesoCRId.Text.Length > 0)
                            FilaDiario.ACCESO_CR_ID = Convert.ToInt32(LAccesoCRId.Text);
                        try
                        {
                            FilaDiario.PERSONA_DIARIO_TT = CeC_BD.TimeSpan2DateTime(Convert.ToDateTime(AccesoSId.Value) - Convert.ToDateTime(AccesoEId.Value));
                        }
                        catch
                        {
                        }

                        int IDPersoDiario = Convert.ToInt32(FilaDiario.PERSONA_DIARIO_ID);
                        //Agregar ModuloLog***
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Persona Diario Edicion", IDPersoDiario, "Calendario", Sesion.SESION_ID);
                        //*****				
                        FilaDiario.TIPO_INC_SIS_ID = Convert.ToInt32(TipoIncSystem.DataValue);
                        int ret3 = DAPersonas_Diario.Update(DS_Personas_Diario1);
                        if (ret3 > 0)
                            LCorrecto.Text = "Asistencia actualizada satisfactoriamente";
                        Sesion.WF_Personas_DiarioE_Guardado = 1;
                        Sesion.WF_EmpleadosBus_Query = Sesion.WF_EmpleadosBus_Query_Temp;
                        Sesion.Redirige("WF_Personas_Diario.aspx");
                    }
                    catch (Exception ex)
                    {
                        string rt = ex.Message;
                        LError.Text = rt;

                    }
                }
				//	CeC_Asistencias.CalculaDia(FilaDiario.PERSONA_DIARIO_ID);

			}
		}

		protected void BAcceso_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			int Numero_Resgistos = Grid.Rows.Count;

			for (int i = 0 ; i<Numero_Resgistos; i++)
			{
				if (Grid.Rows[i].Activated)
				{
					try
					{
						
						if(((Infragistics.WebUI.WebDataInput.WebImageButton )sender).ID == "BAccesoEId")
						{
							LAccesoEId.Text = Grid.Rows[i].Cells[0].Value.ToString();
							LAccesoEId.ToolTip = LAccesoEId.Text;
							AccesoEId.Value = Convert.ToDateTime(Grid.Rows[i].Cells[1].Value);
							AccesoEId.ToolTip = AccesoEId.Value.ToString();
							LAccesoET.Text = Grid.Rows[i].Cells[2].Value.ToString();
							LAccesoET.ToolTip = Convert.ToInt32(Grid.Rows[i].Cells[5].Value).ToString();
						}
						else
						{
							LAccesoSId.Text = Grid.Rows[i].Cells[0].Value.ToString();
							LAccesoSId.ToolTip = LAccesoSId.Text;

							AccesoSId.Value = Convert.ToDateTime(Grid.Rows[i].Cells[1].Value);							
							AccesoSId.ToolTip = AccesoEId.Value.ToString();

							LAccesoST.Text = Grid.Rows[i].Cells[2].Value.ToString();
							LAccesoST.ToolTip = Convert.ToInt32(Grid.Rows[i].Cells[5].Value).ToString();
						}
						return;
					}
					catch(Exception ex)
					{
						LError.Text = "Error :" + ex.Message;
						return;
					}
					
				}
			}
		}
        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid);
        }
        protected void TipoIncidenciaId_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(TipoIncidenciaId);
            //TipoIncidenciaId.Columns[3].Hidden  = true;
        }
        protected void TipoIncSystem_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(TipoIncSystem);
        }
}
}
