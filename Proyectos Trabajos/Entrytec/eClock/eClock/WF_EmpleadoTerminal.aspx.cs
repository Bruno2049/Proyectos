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
	/// Descripción breve de WF_EmpleadoTerminal.
	/// </summary>
	public partial class WF_EmpleadoTerminal : System.Web.UI.Page
	{
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		protected System.Data.OleDb.OleDbDataAdapter DA_EmpleadoTerminal;
		protected System.Data.OleDb.OleDbConnection Conexion;
		protected DS_EmpleadoxTerminal dS_EmpleadoxTerminal1;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
		protected System.Data.OleDb.OleDbDataAdapter DA_TERMINALES;
		protected System.Data.OleDb.OleDbCommand Asignacion_Terminales;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand3;
		protected System.Data.OleDb.OleDbDataAdapter DA_Terminal_link;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand4;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand4;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand4;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand4;
		protected System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
		CeC_Sesion Sesion;
	
		private void Habilitarcontroles(bool Caso)
		{
			if(!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Empleados))
			{
                if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Empleados0Editar_Terminal))
				{
					//QueryGrupo = ""
					UltraWebGrid1.Visible = Caso;
                    Webimagebutton1.Visible = Caso;
					BDeshacerCambios.Visible = Caso;
					Button1.Visible = Caso;
				}
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aquí el código de usuario para inicializar la página
			UltraWebGrid1.DisplayLayout.CellClickActionDefault=Infragistics.WebUI.UltraWebGrid.CellClickAction.RowSelect;
			Sesion = CeC_Sesion.Nuevo(this);
			Sesion.TituloPagina = "Asignación de Terminales";
			Sesion.DescripcionPagina = "Seleccione una o varias terminales para asignarla a Empleados";

			// Permisos****************************************
			string [] Permiso = new string [10];
			
			/*Permiso[0] = "S";
			Permiso[1] = "S.Empleados";
			Permiso[2] = "S.Empleados.Editar_Terminal";*/
			Permiso[0] = CEC_RESTRICCIONES.S0Empleados0Editar_Terminal;


			if (!Sesion.Acceso(Permiso,CIT_Perfiles.Acceso(Sesion.PERFIL_ID,this)))
			{
				CIT_Perfiles.CrearVentana(this,Sesion.MensajeVentanaJScript(),Sesion.TituloPagina,"Aceptar","WF_Main.aspx","","");
				Habilitarcontroles(false);
				return;
			}

			Habilitarcontroles(false);

			//**************************************************

			if (!IsPostBack)
			{

				if(Sesion.WF_EmpleadosFil_Qry.Length<1)
				{
				
					Sesion.WF_EmpleadosFil_HayFecha = 0;
					Sesion.WF_EmpleadosFil_HayExportacion = false;
					Sesion.WF_EmpleadosFil_BotonMensaje = "Mostrar Resultados";
					Sesion.WF_EmpleadosFil_HayTerminales = false ;
					Sesion.WF_EmpleadosFil_HayDiasLaborables = false;
					Sesion.WF_EmpleadosFil_HayEmpleados = true;
					Sesion.TituloPagina = "Filtro-Empleados Terminales";

					Sesion.WF_EmpleadosFil_Titulo = "Asignación de Terminal por usuario";
					Sesion.WF_EmpleadosFil_LINK = "WF_EmpleadoTerminal.aspx";
					Sesion.WF_EmpleadosFil();
					return;
				}
				else
				{
					Sesion.WF_EmpleadosFil_Qry_Temp = Sesion.WF_EmpleadosFil_Qry;
					Sesion.WF_EmpleadosFil_Qry = "";
				}
			}
			
			Sesion.WF_EmpleadosFil_HayFecha = 2;

			/*if(EmpleadoCheckBox1.Checked)
				DA_EmpleadoTerminal.SelectCommand.CommandText = DA_EmpleadoTerminal.SelectCommand.CommandText.Replace("AND (EC_PERSONAS.PERSONA_BORRADO = 0)"," ");
				*/
			
			DA_EmpleadoTerminal.SelectCommand.CommandText = DA_EmpleadoTerminal.SelectCommand.CommandText.Replace("ORDER","AND EC_PERSONAS.PERSONA_ID IN ("+Sesion.WF_EmpleadosFil_Qry_Temp+") ORDER ");
			DA_Terminal_link.SelectCommand.CommandText = DA_Terminal_link.SelectCommand.CommandText.Replace("ORDER","AND EC_PERSONAS_terminales.PERSONA_ID IN ("+Sesion.WF_EmpleadosFil_Qry_Temp+") ORDER ");
			
			//Agregar ModuloLog***
			Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA,"Empleados_Terminal",Sesion.USUARIO_ID,Sesion.USUARIO_NOMBRE,Sesion.SESION_ID);
			//*****

			Sesion.DA_ModQueryAddColumnaUltraGrid(DA_EmpleadoTerminal, "AND (EC_PERSONAS.PERSONA_BORRADO = 0)",EmpleadoCheckBox1);
		
			DA_EmpleadoTerminal.Fill(dS_EmpleadoxTerminal1.EC_EMPLEADOXTERMINAL);
			DA_Terminal_link.Fill(dS_EmpleadoxTerminal1.EC_PERSONAS_TERMINALES_LINK);
			if(!IsPostBack)
			{
				//DA_EmpleadoTerminal.Fill(dS_EmpleadoxTerminal1.EC_EMPLEADOXTERMINAL);
				UltraWebGrid1.DataBind();
				DA_TERMINALES.Fill(dS_EmpleadoxTerminal1.TERMINALES_NOMBRE);
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
			this.DA_EmpleadoTerminal = new System.Data.OleDb.OleDbDataAdapter();
			this.dS_EmpleadoxTerminal1 = new DS_EmpleadoxTerminal();
			this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
			this.DA_TERMINALES = new System.Data.OleDb.OleDbDataAdapter();
			this.Asignacion_Terminales = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand3 = new System.Data.OleDb.OleDbCommand();
			this.DA_Terminal_link = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			((System.ComponentModel.ISupportInitialize)(this.dS_EmpleadoxTerminal1)).BeginInit();
			this.BDeshacerCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
			this.Button1.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Button1_Click);
			this.Webimagebutton1.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Webimagebutton1_Click);
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = @"SELECT EC_PERSONAS.PERSONA_ID, EC_PERSONAS_DATOS.TRACVE, EC_PERSONAS_DATOS.TRANOM, EC_PERSONAS_DATOS.DATARE, EC_PERSONAS_DATOS.DATDEP, EC_PERSONAS_DATOS.DATCCT, EC_PERSONAS_DATOS.CNOCVE, EC_PERSONAS_DATOS.CIACVE, EC_PERSONAS_DATOS.NS, 123456789 AS STATUS FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE EC_PERSONAS_DATOS.TRACVE = EC_PERSONAS.PERSONA_LINK_ID AND (EC_PERSONAS.PERSONA_BORRADO = 0) ORDER BY EC_PERSONAS_DATOS.TRACVE";
			this.oleDbSelectCommand1.Connection = this.Conexion;
			// 
			// Conexion
			// 
			this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
			// 
			// DA_EmpleadoTerminal
			// 
			this.DA_EmpleadoTerminal.DeleteCommand = this.oleDbDeleteCommand1;
			this.DA_EmpleadoTerminal.InsertCommand = this.oleDbInsertCommand1;
			this.DA_EmpleadoTerminal.SelectCommand = this.oleDbSelectCommand1;
			this.DA_EmpleadoTerminal.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										  new System.Data.Common.DataTableMapping("Table", "EC_EMPLEADOXTERMINAL", new System.Data.Common.DataColumnMapping[] {
																																																								   new System.Data.Common.DataColumnMapping("TRACVE", "TRACVE"),
																																																								   new System.Data.Common.DataColumnMapping("TRANOM", "TRANOM"),
																																																								   new System.Data.Common.DataColumnMapping("DATARE", "DATARE"),
																																																								   new System.Data.Common.DataColumnMapping("DATDEP", "DATDEP"),
																																																								   new System.Data.Common.DataColumnMapping("DATCCT", "DATCCT"),
																																																								   new System.Data.Common.DataColumnMapping("CNOCVE", "CNOCVE"),
																																																								   new System.Data.Common.DataColumnMapping("CIACVE", "CIACVE"),
																																																								   new System.Data.Common.DataColumnMapping("NS", "NS")})});
			this.DA_EmpleadoTerminal.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// dS_EmpleadoxTerminal1
			// 
			this.dS_EmpleadoxTerminal1.DataSetName = "DS_EmpleadoxTerminal";
			this.dS_EmpleadoxTerminal1.Locale = new System.Globalization.CultureInfo("es-MX");
			// 
			// oleDbSelectCommand2
			// 
			this.oleDbSelectCommand2.CommandText = "SELECT TERMINAL_ID, TERMINAL_NOMBRE FROM EC_TERMINALES WHERE (TERMINAL_BORRADO =" +
				" 0) ORDER BY TERMINAL_ID";
			this.oleDbSelectCommand2.Connection = this.Conexion;
			// 
			// DA_TERMINALES
			// 
			this.DA_TERMINALES.DeleteCommand = this.oleDbDeleteCommand2;
			this.DA_TERMINALES.InsertCommand = this.oleDbInsertCommand2;
			this.DA_TERMINALES.SelectCommand = this.oleDbSelectCommand2;
			this.DA_TERMINALES.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "TERMINALES_NOMBRE", new System.Data.Common.DataColumnMapping[] {
																																																						 new System.Data.Common.DataColumnMapping("TERMINAL_ID", "TERMINAL_ID"),
																																																						 new System.Data.Common.DataColumnMapping("TERMINAL_NOMBRE", "TERMINAL_NOMBRE")})});
			this.DA_TERMINALES.UpdateCommand = this.oleDbUpdateCommand2;
			// 
			// Asignacion_Terminales
			// 
			this.Asignacion_Terminales.Connection = this.Conexion;
			// 
			// oleDbSelectCommand3
			// 
			this.oleDbSelectCommand3.CommandText = @"SELECT EC_PERSONAS_TERMINALES.PERSONA_ID, EC_PERSONAS_TERMINALES.TERMINAL_ID, EC_TERMINALES.TERMINAL_NOMBRE, EC_TERMINALES.TERMINAL_BORRADO FROM EC_PERSONAS_TERMINALES, EC_TERMINALES WHERE EC_PERSONAS_TERMINALES.TERMINAL_ID = EC_TERMINALES.TERMINAL_ID AND (eC_TERMINALES.TERMINAL_BORRADO = 0) ORDER BY EC_PERSONAS_TERMINALES.PERSONA_ID";
			this.oleDbSelectCommand3.Connection = this.Conexion;
			// 
			// DA_Terminal_link
			// 
			this.DA_Terminal_link.DeleteCommand = this.oleDbDeleteCommand3;
			this.DA_Terminal_link.InsertCommand = this.oleDbInsertCommand3;
			this.DA_Terminal_link.SelectCommand = this.oleDbSelectCommand3;
			this.DA_Terminal_link.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS_TERMINALES_LINK", new System.Data.Common.DataColumnMapping[] {
																																																									   new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																									   new System.Data.Common.DataColumnMapping("TERMINAL_ID", "TERMINAL_ID"),
																																																									   new System.Data.Common.DataColumnMapping("TERMINAL_NOMBRE", "TERMINAL_NOMBRE")})});
			this.DA_Terminal_link.UpdateCommand = this.oleDbUpdateCommand3;
			// 
			// oleDbSelectCommand4
			// 
			this.oleDbSelectCommand4.Connection = this.Conexion;
			// 
			// oleDbDataAdapter1
			// 
			this.oleDbDataAdapter1.DeleteCommand = this.oleDbDeleteCommand4;
			this.oleDbDataAdapter1.InsertCommand = this.oleDbInsertCommand4;
			this.oleDbDataAdapter1.SelectCommand = this.oleDbSelectCommand4;
			this.oleDbDataAdapter1.UpdateCommand = this.oleDbUpdateCommand4;
			((System.ComponentModel.ISupportInitialize)(this.dS_EmpleadoxTerminal1)).EndInit();

		}
		#endregion
		
		private void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			Sesion.Redirige("WF_Main.aspx");
		}

		private void Webimagebutton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			char [] Caracter = new char[1];
			Caracter[0] = Convert.ToChar("|");

			string [] TerminalesSel  = Sesion.WF_ControlTermnales_Terminales_Selecionadas.Split(Caracter);
			int TerminalesQuitadas = 0 ;
			
			for (int empleado =0; empleado < UltraWebGrid1.Rows.Count; empleado ++)
			{
				int Persona_Id_E = Convert.ToInt32(UltraWebGrid1.Rows[empleado].Cells[0].Value);			
				if (!CBListado.Checked)
				{
					if (UltraWebGrid1.Rows[empleado].Selected)
					{
						for (int i = 0 ; i <TerminalesSel.Length ; i++)
						{
							if (TerminalesSel[i].Length > 0)
							{
									//update
									CeC_BD.EjecutaComando("Delete from EC_PERSONAS_TERMINALES WHERE PERSONA_ID="+Persona_Id_E+" AND Terminal_Id="+TerminalesSel[i]);
									//Agregar ModuloLog***
									Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO,"Empleado Terminal",Convert.ToInt32(TerminalesSel[i]),"Persona_ID"+Persona_Id_E,Sesion.SESION_ID);
									//*****
								TerminalesQuitadas ++;
							}
						}
					}
				}
				else
				{
					for (int i = 0 ; i <TerminalesSel.Length ; i++)
					{
						if (TerminalesSel[i].Length > 0)
						{
							//update
							CeC_BD.EjecutaComando("Delete from EC_PERSONAS_TERMINALES WHERE PERSONA_ID="+Persona_Id_E+" AND Terminal_Id="+TerminalesSel[i]);
							//Agregar ModuloLog***
							Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.BORRADO,"Empleado Terminal",Convert.ToInt32(TerminalesSel[i]),"Persona_ID"+Persona_Id_E,Sesion.SESION_ID);
							//*****
							TerminalesQuitadas ++;
						}
					}
				}
			}
					
			dS_EmpleadoxTerminal1.EC_PERSONAS_TERMINALES_LINK.Clear();
			dS_EmpleadoxTerminal1.EC_EMPLEADOXTERMINAL.Clear();
			
			DA_EmpleadoTerminal.SelectCommand.CommandText = DA_EmpleadoTerminal.SelectCommand.CommandText.Replace("ORDER","AND EC_PERSONAS.PERSONA_ID IN ("+Sesion.WF_EmpleadosFil_Qry_Temp+") ORDER ");
			DA_Terminal_link.SelectCommand.CommandText = DA_Terminal_link.SelectCommand.CommandText.Replace("ORDER","AND EC_PERSONAS_terminales.PERSONA_ID IN ("+Sesion.WF_EmpleadosFil_Qry_Temp+") ORDER ");
			DA_EmpleadoTerminal.Fill(dS_EmpleadoxTerminal1.EC_EMPLEADOXTERMINAL);
			DA_Terminal_link.Fill(dS_EmpleadoxTerminal1.EC_PERSONAS_TERMINALES_LINK);

			UltraWebGrid1.DataBind();
			
			if (TerminalesQuitadas> 0)
					LCorrecto.Text  = "Regsitros Modificados Exitosamente";
			else
					LError.Text = "Dedes de seleccionar una fila";
		}

		protected void EmpleadoCheckBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DataBind();
		}

		private void Asignar_Terminales()
		{
			char [] Caracter = new char[1];
			Caracter[0] = Convert.ToChar("|");

			string [] TerminalesSel  = Sesion.WF_ControlTermnales_Terminales_Selecionadas.Split(Caracter);

			for (int empleado =0; empleado < UltraWebGrid1.Rows.Count; empleado ++)
			{
				int Persona_Id_E = Convert.ToInt32(UltraWebGrid1.Rows[empleado].Cells[0].Value);			
				if (!CBListado.Checked)
				{
					if (UltraWebGrid1.Rows[empleado].Selected)
					{
						for (int i = 0 ; i <TerminalesSel.Length ; i++)
						{
							if (TerminalesSel[i].Length > 0)
							{
								int TErminal = Comprobacion_Asignacion(Persona_Id_E,Convert.ToInt32(TerminalesSel[i]));

								if (TErminal>-1)
								{
									//update
									CeC_BD.EjecutaComando("Update EC_PERSONAS_TERMINALES SET Terminal_Id ="+TerminalesSel[i]+" WHERE PERSONA_ID="+Persona_Id_E+" AND Terminal_Id="+TerminalesSel[i]);
									//Agregar ModuloLog***
									Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION,"Empleado Terminal",Sesion.USUARIO_ID,Sesion.USUARIO_NOMBRE,Sesion.SESION_ID);
									//*****
								}
								else
								{
									//insert
									CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_TERMINALES (PERSONA_ID, TERMINAL_ID) VALUES ("+Persona_Id_E+","+TerminalesSel[i]+")");
									//Agregar ModuloLog***
									Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO,"Empleado Terminal",Sesion.USUARIO_ID,Sesion.USUARIO_NOMBRE,Sesion.SESION_ID);
									//*****
								}
							}
						}
					}
				}
				else
				{
					for (int i = 0 ; i <TerminalesSel.Length ; i++)
					{
						if (TerminalesSel[i].Length > 0)
						{
							int TErminal = Comprobacion_Asignacion(Persona_Id_E,Convert.ToInt32(TerminalesSel[i]));
							if (TErminal>-1)
							{
								//update
								CeC_BD.EjecutaComando("Update EC_PERSONAS_TERMINALES SET Terminal_Id ="+TerminalesSel[i]+" WHERE PERSONA_ID="+Persona_Id_E+" AND Terminal_Id="+TerminalesSel[i]);
								//Agregar ModuloLog***
								Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION,"Empleado Terminal",Sesion.USUARIO_ID,Sesion.USUARIO_NOMBRE,Sesion.SESION_ID);
								//*****
							}
							else
							{
								//insert
								CeC_BD.EjecutaComando("INSERT INTO EC_PERSONAS_TERMINALES (PERSONA_ID, TERMINAL_ID) VALUES ("+Persona_Id_E+","+TerminalesSel[i]+")");
								//Agregar ModuloLog***
								Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO,"Empleado Terminal",Sesion.USUARIO_ID,Sesion.USUARIO_NOMBRE,Sesion.SESION_ID);
								//*****
							}
						}
					}
				}
			}
		
			dS_EmpleadoxTerminal1.EC_PERSONAS_TERMINALES_LINK.Clear();
			dS_EmpleadoxTerminal1.EC_EMPLEADOXTERMINAL.Clear();
			
			DA_EmpleadoTerminal.SelectCommand.CommandText = DA_EmpleadoTerminal.SelectCommand.CommandText.Replace("ORDER","AND EC_PERSONAS.PERSONA_ID IN ("+Sesion.WF_EmpleadosFil_Qry_Temp+") ORDER ");
			DA_Terminal_link.SelectCommand.CommandText = DA_Terminal_link.SelectCommand.CommandText.Replace("ORDER","AND EC_PERSONAS_terminales.PERSONA_ID IN ("+Sesion.WF_EmpleadosFil_Qry_Temp+") ORDER ");
			DA_EmpleadoTerminal.Fill(dS_EmpleadoxTerminal1.EC_EMPLEADOXTERMINAL);
			DA_Terminal_link.Fill(dS_EmpleadoxTerminal1.EC_PERSONAS_TERMINALES_LINK);

			UltraWebGrid1.DataBind();
		}

		private int Comprobacion_Asignacion(int Persona_ID, int Terminal_ID)
		{
			int ret = CeC_BD.EjecutaEscalarInt("Select Terminal_ID From EC_PERSONAS_TERMINALES WHERE PERSONA_ID = "+Persona_ID.ToString()+" AND TERMINAL_ID ="+ Terminal_ID.ToString());
			if (ret>0)
				return ret;
			else
				return -1;
		}

		private void Button1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			LError.Text = "";
			LCorrecto.Text = "";
			if (Sesion.WF_ControlTermnales_Terminales_Selecionadas.Length <=0 )
			{
				LError.Text = "Debes de Seleccionar una Terminal";
				return;
			}
			Asignar_Terminales();
		}

		private void BDeshacerCambios_Click()
		{
		}

        protected void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(UltraWebGrid1, false, false, true, false);
        }
    }
}