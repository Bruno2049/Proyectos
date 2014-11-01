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
using Infragistics.WebUI.WebSchedule;
namespace eClock 
{
	/// <summary>
	/// Descripción breve de WF_HorarioPersona.
	/// </summary>
	public partial class WF_HorarioPersona : System.Web.UI.Page
	{
		protected System.Data.OleDb.OleDbDataAdapter DAHorarioPersona;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected System.Data.OleDb.OleDbConnection oleDbConnection1;
		protected DS_HorarioPersona dS_HorarioPersona1;
		protected System.Data.OleDb.OleDbDataAdapter DASemanas;
		protected System.Data.OleDb.OleDbDataAdapter DAAnos;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;

		CeC_Sesion Sesion;
		DS_HorarioPersona ds_HorarioPersona;

		private int ID = 0;
		private void Habilitarcontroles(bool Caso)
		{
			if(!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Empleados0Editar_Turnos_Anuales))
			{
				//QueryGrupo = ""
				CAno.Visible = Caso;
				Label1.Visible = Caso;
				Label2.Visible = Caso;
				CSemana.Visible = Caso;
				Button1.Visible = Caso;
				Button2.Visible = Caso;
				WC_Horario1.Visible = Caso;
				WC_Horario2.Visible = Caso;
				WC_Horario3.Visible = Caso;
				WC_Horario4.Visible = Caso;
				WC_Horario5.Visible = Caso;
				WC_Horario6.Visible = Caso;
				WC_Horario7.Visible = Caso;	
            }
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aquí el código de usuario para inicializar la página
			Sesion = CeC_Sesion.Nuevo(this);
			Sesion.TituloPagina = "Turnos Anuales";
			DateTime FechaSemana = Sesion.WF_HorarioPersona_Semana;

			string [] Permiso = new string [10];
			
			/*
			Permiso[0] = "S";
			Permiso[1] = "S.Empleados";
			Permiso[2] = "S.Empleados.Editar_Turnos_Anuales";*/
			Permiso[0] = CEC_RESTRICCIONES.S0Empleados0Editar_Turnos_Anuales;

			if (!Sesion.Acceso(Permiso,CIT_Perfiles.Acceso(Sesion.PERFIL_ID,this)))
			{
				CIT_Perfiles.CrearVentana(this,Sesion.MensajeVentanaJScript(),Sesion.TituloPagina,"Aceptar","WF_Main.aspx","","");
				Habilitarcontroles(false);
				return;
			}

			Habilitarcontroles(false);

			//**************************************************		

			if (Sesion.WF_EmpleadosBus_Query.Length < 1)
			{
				Sesion.WF_EmpleadosBus_Link = "WF_HorarioPersona.aspx";
				Sesion.WF_EmpleadosBus();
				return;
			}
			ds_HorarioPersona = (DS_HorarioPersona)Sesion.WF_HorarioPersona_Datos;
			
//			if(!IsPostBack)
			
				WC_Horario1.Dia = 0;
				WC_Horario2.Dia = 1;
				WC_Horario3.Dia = 2;
				WC_Horario4.Dia = 3;
				WC_Horario5.Dia = 4;
				WC_Horario6.Dia = 5;
				WC_Horario7.Dia = 6;

			DAAnos.Fill(dS_HorarioPersona1);

			if(!IsPostBack)
			{
				CAno.DataBind();
				CAno.DataValue = FechaSemana.Year.ToString();
				//inicio de la configuracion de horario persona anual
				for(int i = 0; i< 7; i++)//quitar el arreglo de la clase ciarreglo
				{
					Sesion.DiArreglo[i] =0;
				}
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Horario por Persona", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);	
/*				DASemanas.SelectCommand.Parameters[0].Value = Convert.ToInt32(CAno.DataValue.ToString());
				DASemanas.Fill(dS_HorarioPersona1);
				CSemana.DataBind();
				CSemana.DataValue = FechaSemana;*/
				CargaSemana(Convert.ToInt32(CAno.DataValue.ToString()));
				CargaSemana(FechaSemana);
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
			this.DAHorarioPersona = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
			this.dS_HorarioPersona1 = new DS_HorarioPersona();
			this.DASemanas = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
			this.DAAnos = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand3 = new System.Data.OleDb.OleDbCommand();
			((System.ComponentModel.ISupportInitialize)(this.dS_HorarioPersona1)).BeginInit();
			this.CSemana.SelectedRowChanged += new Infragistics.WebUI.WebCombo.SelectedRowChangedEventHandler(this.CSemana_SelectedRowChanged);
			this.CAno.SelectedRowChanged += new Infragistics.WebUI.WebCombo.SelectedRowChangedEventHandler(this.CAno_SelectedRowChanged);
			this.Button2.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Button2_Click);
			this.Button1.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Button1_Click);
			// 
			// DAHorarioPersona
			// 
			this.DAHorarioPersona.SelectCommand = this.oleDbSelectCommand1;
			this.DAHorarioPersona.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "HorarioPersona", new System.Data.Common.DataColumnMapping[] {
																																																						 new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																						 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_FECHA", "PERSONA_DIARIO_FECHA"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_ID", "TURNO_DIA_ID"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_NOMBRE", "TURNO_NOMBRE"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HEMIN", "TURNO_DIA_HEMIN"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HE", "TURNO_DIA_HE"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HEMAX", "TURNO_DIA_HEMAX"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HERETARDO", "TURNO_DIA_HERETARDO"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HSMIN", "TURNO_DIA_HSMIN"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HS", "TURNO_DIA_HS"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HSMAX", "TURNO_DIA_HSMAX"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HBLOQUE", "TURNO_DIA_HBLOQUE"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HBLOQUET", "TURNO_DIA_HBLOQUET"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HTIEMPO", "TURNO_DIA_HTIEMPO"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HCS", "TURNO_DIA_HCS"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_DIA_HCR", "TURNO_DIA_HCR"),
																																																						 new System.Data.Common.DataColumnMapping("PERSONA_DIARIO_ID", "PERSONA_DIARIO_ID"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_ID", "TURNO_ID"),
																																																						 new System.Data.Common.DataColumnMapping("EXPR1", "EXPR1")})});
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = @"SELECT EC_PERSONAS_DIARIO.PERSONA_ID, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, EC_PERSONAS_DIARIO.TURNO_DIA_ID, EC_TURNOS.TURNO_NOMBRE, EC_TURNOS_DIA.TURNO_DIA_HEMIN, EC_TURNOS_DIA.TURNO_DIA_HE, EC_TURNOS_DIA.TURNO_DIA_HEMAX, EC_TURNOS_DIA.TURNO_DIA_HERETARDO, EC_TURNOS_DIA.TURNO_DIA_HSMIN, EC_TURNOS_DIA.TURNO_DIA_HS, EC_TURNOS_DIA.TURNO_DIA_HSMAX, EC_TURNOS_DIA.TURNO_DIA_HBLOQUE, EC_TURNOS_DIA.TURNO_DIA_HBLOQUET, EC_TURNOS_DIA.TURNO_DIA_HTIEMPO, EC_TURNOS_DIA.TURNO_DIA_HCS, EC_TURNOS_DIA.TURNO_DIA_HCR, EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID, EC_TURNOS.TURNO_ID, EC_TURNOS_DIA.TURNO_DIA_ID AS EXPR1 FROM EC_PERSONAS_DIARIO, EC_TURNOS_DIA, EC_TURNOS, EC_TURNOS_SEMANAL_DIA WHERE EC_PERSONAS_DIARIO.TURNO_DIA_ID = EC_TURNOS_DIA.TURNO_DIA_ID AND EC_TURNOS_DIA.TURNO_DIA_ID = EC_TURNOS_SEMANAL_DIA.TURNO_DIA_ID (+) AND EC_TURNOS.TURNO_ID (+) = EC_TURNOS_SEMANAL_DIA.TURNO_ID AND (EC_PERSONAS_DIARIO.PERSONA_ID = ?) AND (EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA >= ?) AND (EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA <= ?) ORDER BY EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA";
			this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
			this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
			this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_DIARIO_FECHA1", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "PERSONA_DIARIO_FECHA"));
			// 
			// oleDbConnection1
			// 
			this.oleDbConnection1.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
			// 
			// dS_HorarioPersona1
			// 
			this.dS_HorarioPersona1.DataSetName = "DS_HorarioPersona";
			this.dS_HorarioPersona1.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// DASemanas
			// 
			this.DASemanas.SelectCommand = this.oleDbSelectCommand2;
			this.DASemanas.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								new System.Data.Common.DataTableMapping("Table", "Semanas", new System.Data.Common.DataColumnMapping[] {
																																																		   new System.Data.Common.DataColumnMapping("DIAS_TRABAJO", "DIAS_TRABAJO"),
																																																		   new System.Data.Common.DataColumnMapping("DESCRIPCION", "DESCRIPCION")})});
			// 
			// oleDbSelectCommand2
			// 
			this.oleDbSelectCommand2.CommandText = "SELECT DIAS_TRABAJO, TO_CHAR(DIAS_TRABAJO, \'DD MON YY\') AS DESCRIPCION FROM eC_D" +
				"IAS_TRABAJO WHERE (TO_CHAR(DIAS_TRABAJO, \'D\') = \'7\') AND (TO_CHAR(DIAS_TRABAJO, " +
				"\'YYYY\') = ?) ORDER BY DIAS_TRABAJO";
			this.oleDbSelectCommand2.Connection = this.oleDbConnection1;
			this.oleDbSelectCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PARAM1", System.Data.OleDb.OleDbType.Integer));
			// 
			// DAAnos
			// 
			this.DAAnos.InsertCommand = this.oleDbInsertCommand2;
			this.DAAnos.SelectCommand = this.oleDbSelectCommand3;
			this.DAAnos.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																							 new System.Data.Common.DataTableMapping("Table", "Anos", new System.Data.Common.DataColumnMapping[] {
																																																	 new System.Data.Common.DataColumnMapping("ANOS", "ANOS")})});
			// 
			// oleDbInsertCommand2
			// 
			this.oleDbInsertCommand2.CommandText = "SELECT ANOS FROM (SELECT TO_CHAR(DIAS_TRABAJO , \'YYYY\') AS Anos FROM EC_DIAS_TRA" +
				"BAJO) DERIVEDTBL GROUP BY ANOS ORDER BY ANOS";
			this.oleDbInsertCommand2.Connection = this.oleDbConnection1;
			// 
			// oleDbSelectCommand3
			// 
			this.oleDbSelectCommand3.CommandText = "SELECT ANOS FROM (SELECT TO_CHAR(DIAS_TRABAJO , \'YYYY\') AS Anos FROM EC_DIAS_TRA" +
				"BAJO) DERIVEDTBL GROUP BY ANOS ORDER BY ANOS";
			this.oleDbSelectCommand3.Connection = this.oleDbConnection1;
			((System.ComponentModel.ISupportInitialize)(this.dS_HorarioPersona1)).EndInit();

		}
		#endregion
		
        private bool CargaSemana(int Ano)
		{
			try
			{
				CSemana.SelectedIndex = -1;
				CSemana.Rows.Clear();
				DASemanas.SelectCommand.Parameters[0].Value = Ano;
				dS_HorarioPersona1.Semanas.Clear();
				DASemanas.Fill(dS_HorarioPersona1);
				CSemana.DataBind();
				return true;//CargaSemana(CSemana.DataValue);
			}
			catch
			{}
			return false;
		}

		private bool CargaSemana(DateTime Semana)
		{
			try
			{
				Sesion.WF_HorarioPersona_Semana = Semana;
				CSemana.DataValue = Semana;
				DateTime FechaSemanaFin = Semana.AddDays(7);
				//liena que proviene de la consulta previa Sesion.WF_Empleados_PERSONA_ID
				DAHorarioPersona.SelectCommand.Parameters["PERSONA_ID"].Value = Sesion.WF_Empleados_PERSONA_ID;
				DAHorarioPersona.SelectCommand.Parameters["PERSONA_DIARIO_FECHA"].Value = Semana;
				DAHorarioPersona.SelectCommand.Parameters["PERSONA_DIARIO_FECHA1"].Value = FechaSemanaFin;
				DAHorarioPersona.Fill(dS_HorarioPersona1);

				Sesion.WF_HorarioPersona_Datos = dS_HorarioPersona1;
				return true;
			}
			catch
			{
			}
			return false;
		}

		private void CSemana_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
		{
			Sesion.WF_HorarioPersona_Semana = Convert.ToDateTime(CSemana.DataValue);
			CargaSemana(Sesion.WF_HorarioPersona_Semana );
		}

		private void CAno_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
		{
			DateTime Fecha = Convert.ToDateTime(CSemana.DataValue);
			int AnoN = Convert.ToInt32(CAno.DataValue.ToString());
			int Dias = 7*53 * (AnoN - Fecha.Year );
			Fecha = Fecha.AddDays( Dias);
			CargaSemana(AnoN);
			CargaSemana(Fecha);
/*			DASemanas.SelectCommand.Parameters[0].Value = AnoN
			dS_HorarioPersona1.Semanas.Clear();
			DASemanas.Fill(dS_HorarioPersona1);*/
		}

		private void GurdarCambiosDB(int DiaSemana)
		{
			int Turno_Dia_id = CeC_Autonumerico.GeneraAutonumerico("EC_TURNOS_DIA","TURNO_DIA_ID");
			CeC_BD.EjecutaComando("INSERT INTO EC_TURNOS_DIA (" +
				" TURNO_DIA_ID, " + " TURNO_DIA_HE, " + " TURNO_DIA_HEMAX, " +
				" TURNO_DIA_HERETARDO, " + " TURNO_DIA_HS, " + " TURNO_DIA_HBLOQUE, " +
				" TURNO_DIA_HTIEMPO, " + " TURNO_DIA_HCS, " + " TURNO_DIA_HCR, " +
				" TURNO_DIA_HEMIN, " + " TURNO_DIA_HSMIN, " + " TURNO_DIA_HSMAX, " +
				" TURNO_DIA_HBLOQUET, " + " TURNO_DIA_HAYCOMIDA, " + " TURNO_DIA_HCTIEMPO, " +
				" TURNO_DIA_HCTOLERA) " + " VALUES (" + Turno_Dia_id.ToString() + "," +
				CeC_BD.SqlFechaHora(ds_HorarioPersona.HorarioPersona[DiaSemana].TURNO_DIA_HE) + "," +
				CeC_BD.SqlFechaHora(ds_HorarioPersona.HorarioPersona[DiaSemana].TURNO_DIA_HEMAX) + "," +
				CeC_BD.SqlFechaHora(ds_HorarioPersona.HorarioPersona[DiaSemana].TURNO_DIA_HERETARDO) + "," +
				CeC_BD.SqlFechaHora(ds_HorarioPersona.HorarioPersona[DiaSemana].TURNO_DIA_HS) + "," +
				CeC_BD.SqlFechaHora(ds_HorarioPersona.HorarioPersona[DiaSemana].TURNO_DIA_HBLOQUE) + "," +
				CeC_BD.SqlFechaHora(ds_HorarioPersona.HorarioPersona[DiaSemana].TURNO_DIA_HTIEMPO) + "," +
				CeC_BD.SqlFechaHora(ds_HorarioPersona.HorarioPersona[DiaSemana].TURNO_DIA_HCS) + "," +
				CeC_BD.SqlFechaHora(ds_HorarioPersona.HorarioPersona[DiaSemana].TURNO_DIA_HCR) + "," +
				"0,0,0,0,0,0,0)");
					
			DateTime Persona_Fecha = DateTime.Today;
			//Agregar ModuloLog***
			Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO,"Horario Persona",Turno_Dia_id,"Cambio de Hora",Sesion.SESION_ID);
			//*****
			CeC_BD.EjecutaComando("UPDATE EC_PERSONAS_DIARIO SET " +
				"TURNO_DIA_ID = " + Turno_Dia_id + " WHERE PERSONA_DIARIO_FECHA = "+Persona_Fecha+" AND PESONA_ID = "+Convert.ToInt32(Sesion.WF_Empleados_PERSONA_ID));
			Sesion.WF_EmpleadosBus_Query = "";
		}

		private void Button1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			//Sesion.WF_EmpleadosFil_Qry = "";
			try
			{
				if(Convert.ToInt32(Sesion.Dia0)>0)
					GurdarCambiosDB(0);
				if(Convert.ToInt32(Sesion.Dia1)>0)
					GurdarCambiosDB(1);
				if(Convert.ToInt32(Sesion.Dia2)>0)
					GurdarCambiosDB(2);
				if(Convert.ToInt32(Sesion.Dia3)>0)
					GurdarCambiosDB(3);
				if(Convert.ToInt32(Sesion.Dia4)>0)
					GurdarCambiosDB(4);
				if(Convert.ToInt32(Sesion.Dia5)>0)
					GurdarCambiosDB(5);
				if(Convert.ToInt32(Sesion.Dia6)>0)
					GurdarCambiosDB(6);
			}
			catch(Exception ex)
			{
				string eR = ex.Message;
			}
			Sesion.WF_EmpleadosFil_Qry = "";
			Sesion.Redirige("WF_Main.aspx");
		}

		private void Button2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			Sesion.WF_EmpleadosFil_Qry = "";
		}

        protected void CSemana_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(CSemana);
        }

        protected void CAno_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(CAno);
        }
    }
}
