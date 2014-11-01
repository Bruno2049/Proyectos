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
	/// Descripción breve de WF_EmpleadosBus.
	/// </summary>
	public partial class WF_EmpleadosBus : System.Web.UI.Page
	{
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		protected System.Data.OleDb.OleDbDataAdapter DA_Personas_E;
		protected System.Data.OleDb.OleDbConnection Conexion;
		protected DS_EmpleadoBuscar dS_EmpleadoBuscar1;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
		protected System.Data.OleDb.OleDbDataAdapter DA_Horario;
		CeC_Sesion Sesion;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aquí el código de usuario para inicializar la página
			UltraWebGrid1.DisplayLayout.CellClickActionDefault=Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
			Sesion = CeC_Sesion.Nuevo(this);
			Sesion.TituloPagina = "Empleados Busqueda por No.  de Empleado";
			Sesion.DescripcionPagina = "Ingrese el No. de Empleado";
			Sesion.DA_ModQueryAddColumnaUltraGrid(DA_Personas_E,"AND (EC_PERSONAS.PERSONA_BORRADO = 0)",EmpleadosBusCheckBox1);
			
			DA_Horario.Fill(dS_EmpleadoBuscar1.EC_TURNOS);
			DS_EmpleadoBuscar.EC_TURNOSRow FilaTurnos = dS_EmpleadoBuscar1.EC_TURNOS.NewEC_TURNOSRow();

//TURNO_ID TIPO_TURNO_ID TURNO_NOMBRE TURNO_ASISTENCIA 
//0			0			 No Asignado		0 

			FilaTurnos.TIPO_TURNO_ID  = 0;
			FilaTurnos.TURNO_NOMBRE = "";
			FilaTurnos.TURNO_ID = 0;
			FilaTurnos.TURNO_ASISTENCIA = 0;

			dS_EmpleadoBuscar1.EC_TURNOS.AddEC_TURNOSRow(FilaTurnos);

			if(!IsPostBack)
			{
				Sesion.WF_EmpleadosBus_Query ="";
				TIPO_TURNO_COMBO.DataBind();
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
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.Conexion = new System.Data.OleDb.OleDbConnection();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.DA_Personas_E = new System.Data.OleDb.OleDbDataAdapter();
			this.dS_EmpleadoBuscar1 = new DS_EmpleadoBuscar();
			this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
			this.DA_Horario = new System.Data.OleDb.OleDbDataAdapter();
			((System.ComponentModel.ISupportInitialize)(this.dS_EmpleadoBuscar1)).BeginInit();
			this.Button1.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Button1_Click);
			this.BBuscarEmpleado.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BBuscarEmpleado_Click);
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = @"SELECT EC_PERSONAS.PERSONA_ID, EC_PERSONAS.PERSONA_LINK_ID, EC_PERSONAS.PERSONA_EMAIL, EC_PERSONAS_DATOS.CNOCVE, EC_PERSONAS_DATOS.CIACVE, EC_PERSONAS_DATOS.TRACVE, EC_PERSONAS_DATOS.TRANOM, EC_PERSONAS_DATOS.DATARE, EC_PERSONAS_DATOS.DATDEP, EC_PERSONAS_DATOS.DATCCT, EC_TURNOS.TIPO_TURNO_ID, EC_TURNOS.TURNO_NOMBRE, EC_PERSONAS.TURNO_ID, EC_PERSONAS.PERSONA_BORRADO, 123456789 AS STATUS FROM EC_PERSONAS, EC_TURNOS, EC_PERSONAS_DATOS WHERE EC_PERSONAS.TURNO_ID = EC_TURNOS.TURNO_ID AND EC_PERSONAS.PERSONA_LINK_ID = EC_PERSONAS_DATOS.TRACVE AND (EC_PERSONAS.PERSONA_BORRADO = 0) ORDER BY EC_PERSONAS_DATOS.TRACVE";
			this.oleDbSelectCommand1.Connection = this.Conexion;
			// 
			// Conexion
			// 
			this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
			// 
			// DA_Personas_E
			// 
			this.DA_Personas_E.DeleteCommand = this.oleDbDeleteCommand1;
			this.DA_Personas_E.InsertCommand = this.oleDbInsertCommand1;
			this.DA_Personas_E.SelectCommand = this.oleDbSelectCommand1;
			this.DA_Personas_E.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS", new System.Data.Common.DataColumnMapping[] {
																																																					new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																					new System.Data.Common.DataColumnMapping("PERSONA_LINK_ID", "PERSONA_LINK_ID"),
																																																					new System.Data.Common.DataColumnMapping("TIPO_PERSONA_ID", "TIPO_PERSONA_ID"),
																																																					new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																					new System.Data.Common.DataColumnMapping("GRUPO_2_ID", "GRUPO_2_ID"),
																																																					new System.Data.Common.DataColumnMapping("GRUPO_3_ID", "GRUPO_3_ID"),
																																																					new System.Data.Common.DataColumnMapping("PERSONA_NOMBRE", "PERSONA_NOMBRE"),
																																																					new System.Data.Common.DataColumnMapping("PERSONA_EMAIL", "PERSONA_EMAIL"),
																																																					new System.Data.Common.DataColumnMapping("TURNO_NOMBRE", "TURNO_NOMBRE")})});
			this.DA_Personas_E.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// dS_EmpleadoBuscar1
			// 
			this.dS_EmpleadoBuscar1.DataSetName = "DS_EmpleadoBuscar";
			this.dS_EmpleadoBuscar1.Locale = new System.Globalization.CultureInfo("es-MX");
			// 
			// oleDbSelectCommand2
			// 
			this.oleDbSelectCommand2.CommandText = "SELECT TURNO_ID, TIPO_TURNO_ID, TURNO_NOMBRE, TURNO_ASISTENCIA, TURNO_BORRADO FRO" +
				"M EC_TURNOS WHERE (TURNO_BORRADO = 0)";
			this.oleDbSelectCommand2.Connection = this.Conexion;
			// 
			// DA_Horario
			// 
			this.DA_Horario.DeleteCommand = this.oleDbDeleteCommand2;
			this.DA_Horario.InsertCommand = this.oleDbInsertCommand2;
			this.DA_Horario.SelectCommand = this.oleDbSelectCommand2;
			this.DA_Horario.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								 new System.Data.Common.DataTableMapping("Table", "EC_TURNOS", new System.Data.Common.DataColumnMapping[] {
																																																			   new System.Data.Common.DataColumnMapping("TURNO_ID", "TURNO_ID"),
																																																			   new System.Data.Common.DataColumnMapping("TIPO_TURNO_ID", "TIPO_TURNO_ID"),
																																																			   new System.Data.Common.DataColumnMapping("TURNO_NOMBRE", "TURNO_NOMBRE"),
																																																			   new System.Data.Common.DataColumnMapping("TURNO_ASISTENCIA", "TURNO_ASISTENCIA"),
																																																			   new System.Data.Common.DataColumnMapping("TURNO_BORRADO", "TURNO_BORRADO")})});
			this.DA_Horario.UpdateCommand = this.oleDbUpdateCommand2;
			((System.ComponentModel.ISupportInitialize)(this.dS_EmpleadoBuscar1)).EndInit();

		}
		#endregion
		
        private void BBuscarEmpleado_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			int Tracve_Persona_Id = -1 ;
			string Tranom_Persona="";

			if (UltraWebGrid1.Rows.Count<1)
				return;
			for (int i = 0 ;i< Convert.ToInt32(UltraWebGrid1.Rows.Count); i++)
			{
				if (UltraWebGrid1.Rows[i].Selected)
				{
					Tracve_Persona_Id = Convert.ToInt32(UltraWebGrid1.Rows[i].Cells[1].Value);
					Tranom_Persona = Convert.ToString(UltraWebGrid1.Rows[i].Cells[6].Value);
					break;
				}
			}

			if (Tracve_Persona_Id<0)
			{
				LError.Text = "Seleccione un registro de la lista";
				return;
			}

			int Respuesta_Clave = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS where PERSONA_LINK_ID = " + Tracve_Persona_Id +"");

			if (Respuesta_Clave>0)
			{
				//Agregar ModuloLog***
				Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA,"Empleados Busqueda",Tracve_Persona_Id,Tranom_Persona,Sesion.SESION_ID);
				//*****
			}
			
			if (Respuesta_Clave>0)
			{
				LCorrecto.Text = "Empleado Exsistente";
				CeC_Sesion Sesion = CeC_Sesion.Nuevo(this);
				Sesion.WF_Empleados_PERSONA_LINK_ID = Tracve_Persona_Id;
				Sesion.WF_Empleados_PERSONA_ID = Respuesta_Clave;
				Sesion.WF_Empleados_PERSONA_NOMBRE = Tranom_Persona;
				Sesion.WF_EmpleadosBus_Query = "SELECT PERSONA_ID FROM EC_PERSONAS where PERSONA_LINK_ID = " + Tracve_Persona_Id +"";
				Sesion.Redirige(Sesion.WF_EmpleadosBus_Link);
				//Sesion.Redirige(Sesion.UrlDestino);
			}
			else
			{
				LError.Text = "Empleado Inexistente";
			}
		}

		private void Button1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			string [] Query  = new string[10];
			LError.Text =""; 
			for (int i = 0 ; i<Query.Length; i++)
			{
				Query[i] = "";
			}
			if(Tracve.Text.Length>0)
				Query[0] = "EC_PERSONAS_DATOS.TRACVE Like '%" +Tracve.Value+"%'";
			
            if (Tranom.Text.Length>0)
			{
				Tranom.Text = Tranom.Text.ToUpper();
				Query[1] = "EC_PERSONAS_DATOS.TRANOM Like '%" + Tranom.Text+"%'";
			}	
			if(Datare.Text.Length>0)
				Query[2] = "EC_PERSONAS_DATOS.DATARE Like '%"+ Datare.Value + "%'";
			
			if(Datdep.Text.Length>0)
				Query[3] = "EC_PERSONAS_DATOS.DATDEP Like '%"+Datdep.Value+ "%'";
			
			if(Datcct.Text.Length>0)
				Query[4] = "EC_PERSONAS_DATOS.DATCCT Like '%"+ Datcct.Value+"%'";
			
			if(Cnocve.Text.Length>0)
				Query[5] = "EC_PERSONAS_DATOS.CNOCVE Like '%"+Cnocve.Value+"%'";
			
			if(Ciacve.Text.Length>0)
				Query[6] = "EC_PERSONAS_DATOS.CIACVE Like '%"+Ciacve.Value+"%'";
			
			if(TIPO_TURNO_COMBO.SelectedIndex != -1)
				Query[7] = "EC_TURNOS.TURNO_NOMBRE LIKE '%"+TIPO_TURNO_COMBO.SelectedCell.Text+"%'";
			
			if(Persona_email.Text.Length>0)
				Query[8] = "EC_PERSONAS.Persona_Email LIKE '%" +Persona_email.Text+"%'";
			if(Grupo.Text.Length>0)
				Query[9] = "EC_PERSONAS_DATOS.GRUPO Like '%"+Grupo.Text+"%'";
			
			string SQL_QueryCompleta = "";
			bool Realizar_Query = false;
			
			for (int i = 0; i<Query.Length;i++)
			{
				if(Query[i].Length>0)
				{
					SQL_QueryCompleta += " AND " + Query[i].ToString();
					Realizar_Query = true;
				}
			}

			if (Realizar_Query)
			{
				DA_Personas_E.SelectCommand.CommandText = DA_Personas_E.SelectCommand.CommandText.Replace("ORDER"," "+SQL_QueryCompleta+"  ORDER ");
				DA_Personas_E.SelectCommand.CommandText = DA_Personas_E.SelectCommand.CommandText.Replace("AND AND"," AND ");
				DA_Personas_E.Fill(dS_EmpleadoBuscar1.EC_PERSONAS);
				UltraWebGrid1.DataBind();
			}
			else
			{
				LError.Text = "Debes de introducir la información necesaria para realizar tu filtro";
			}
		}

        protected void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(UltraWebGrid1);
        }

        protected void TIPO_TURNO_COMBO_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(TIPO_TURNO_COMBO);
        }
}
}
