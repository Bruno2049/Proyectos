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
	/// Descripción breve de WF_EmpleadosE.
	/// </summary>
	public partial class WF_EmpleadosE : System.Web.UI.Page
	{
		protected System.Data.OleDb.OleDbDataAdapter DA_Empleados;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		protected System.Data.OleDb.OleDbConnection Conexion;
		protected DS_Empleados dS_Empleados1;
		CeC_Sesion Sesion;
		protected System.Data.OleDb.OleDbCommand Borrar_Empleado;
		protected DS_Personas dS_Personas1;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand2;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand2;
		protected System.Data.OleDb.OleDbDataAdapter DA_Personas;
		protected System.Data.OleDb.OleDbDataAdapter DA_Tipo_Turnos;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand3;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand3;
		protected DS_Turnos dS_Turnos1;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand4;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand4;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand4;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand4;
		protected System.Data.OleDb.OleDbDataAdapter DA_Personas_Terminales;
		DS_Empleados.EC_PERSONAS_DATOSRow Fila;
		private string QueryGrupo = "";

		private void Habilitarcontroles(bool Caso)
		{
			string PermisoFinal = CeC_Sesion.LeeStringSesion(this, "PermisosComparacion");
            if(!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Empleados))
			{
				if (!Sesion.ExistePermiso("S.Empleados.Editar",PermisoFinal) && !Sesion.ExistePermiso("S.Empleados.Nuevo",PermisoFinal))
				{
					BGuardarCambios.Visible = Caso;
					BDeshacerCambios.Visible = Caso;
					Tracve.Visible = Caso;
					Tranom.Visible = Caso;
					DatareWC.Visible = Caso;
					DatdepWC.Visible = Caso;
					DatcctWC.Visible = Caso;
					CnocveWC.Visible = Caso; 
					CiacveWC.Visible = Caso;
					CBTurno.Visible = Caso;
					Persona_email.Visible = Caso;
					CBBorrar.Visible = Caso;
					LBorrar.Visible = Caso;
					RVTracve.Visible = Caso;
					RVTranom.Visible = Caso;
					Ns.Visible = Caso;
					GrupoWC.Visible  = Caso;
					Panel1.Visible =Caso;
					WebImageButton1.Visible = Caso;
					Image1.Visible = Caso;
					Panel2.Visible = Caso;
				}
				if (!Sesion.ExistePermiso("S.Empleados.Borrar",PermisoFinal))
				{
					CBBorrar.Visible = Caso;
					LBorrar.Visible = Caso;
				}
				if (!Sesion.ExistePermiso("S.Empleados.Listado.Grupo",PermisoFinal))
				{
					//QueryGrupo = ""
				}
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Sesion = CeC_Sesion.Nuevo(this);
			//Titulo y descripción
			Sesion.TituloPagina = "Empleados-Edición";
			Sesion.DescripcionPagina = "Ingrese o modifique la información del empleado";
			//Fin del Titulo y descripción

			// Permisos****************************************
			string [] Permiso = new string [10];

			Permiso[0] = "S";
			Permiso[1] = "S.Empleados";
			Permiso[2] = "S.Empleados.Borrar";
			Permiso[3] = "S.Empleados.Nuevo";
			Permiso[4] = "S.Empleados.Editar";

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
				if(Sesion.WF_EmpleadosBus_Query.Length < 1)
				{
					Sesion.WF_EmpleadosBus_Link = "WF_EmpleadosE.aspx";
					Sesion.WF_EmpleadosBus();
				}
				else
				{
					Sesion.WF_EmpleadosBus_Query_Temp = Sesion.WF_EmpleadosFil_Qry;
				}
			}
			
			Sesion.WF_EmpleadosBus_Query = "";
			
			DA_Tipo_Turnos.Fill(dS_Turnos1.EC_TURNOS_Combo);

            CBTurno.DataSource = dS_Turnos1;
            CBTurno.DataMember = dS_Turnos1.EC_TURNOS_Combo.TableName;
            CBTurno.DataValue = dS_Turnos1.EC_TURNOS_Combo.TURNO_IDColumn.ColumnName;
            CBTurno.DisplayValue = dS_Turnos1.EC_TURNOS_Combo.TURNO_NOMBREColumn.ColumnName;
			CBTurno.DataBind();

			DA_Empleados.SelectCommand.Parameters[0].Value = Sesion.WF_Empleados_PERSONA_LINK_ID;
			DA_Empleados.Fill(dS_Empleados1.EC_PERSONAS_DATOS);
			
			DA_Personas.SelectCommand.Parameters[0].Value  = Sesion.WF_Empleados_PERSONA_LINK_ID;
			DA_Personas.Fill(dS_Personas1.EC_PERSONAS);
			
			if(!IsPostBack)
			{
                try
                {
                    CBTurno.Columns.FromKey("TURNO_ID").Hidden = true;
                    CBTurno.Columns.FromKey("TURNO_BORRADO").Hidden = true;
                }
                catch
                {
                }
				Sesion.WF_Empleados_LlenadoCombo("Datare",DatareWC,"Área");
				Sesion.WF_Empleados_LlenadoCombo("Datdep",DatdepWC,"Departamento");
				Sesion.WF_Empleados_LlenadoCombo("Datcct",DatcctWC,"Centro de Costos");
				Sesion.WF_Empleados_LlenadoCombo("Cnocve",CnocveWC,"Nómina");
				Sesion.WF_Empleados_LlenadoCombo("Ciacve",CiacveWC,"Compañía");
				Sesion.WF_Empleados_LlenadoCombo("Grupo",GrupoWC,"Grupo");
				if (Sesion.ValidaAcceso("S.Empleados"))
				{	
					//Corregir las siguientes lineas solo temporal para merck
					if(CeC_Config.CampoGrupo1.IndexOf("DATCCT")>0)
					{
						DatcctWC.Rows.Clear();
						Sesion.LlenaCombo("EC_SUSCRIPCION", "SUSCRIPCION_NOMBRE",DatcctWC,"Centro de Costos");
					}
					if(CeC_Config.CampoGrupo2.IndexOf("GRUPO")>0)
					{
						GrupoWC.Rows.Clear();
						Sesion.LlenaCombo("EC_GRUPOS_2", "GRUPO_2_NOMBRE",GrupoWC,"Grupo");
					}
				}
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Empleados Edición", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            }
			//string valor  = WebCombo1.Rows[WebCombo1.SelectedIndex].ToString();
			//WebCombo1.

			if (Sesion.WF_Empleados_PERSONA_LINK_ID>0)
			{
				if (dS_Empleados1.EC_PERSONAS_DATOS.Rows.Count> 0)
				{
					Fila = (DS_Empleados.EC_PERSONAS_DATOSRow)dS_Empleados1.EC_PERSONAS_DATOS.Rows[0];
					if(!IsPostBack)
						AtarControles(true);
				}
				else
				{
					Fila = dS_Empleados1.EC_PERSONAS_DATOS.NewEC_PERSONAS_DATOSRow();
				}
			}
			else
			{
				CBBorrar.Visible = false;
				LBorrar.Visible = false;
				Fila = dS_Empleados1.EC_PERSONAS_DATOS.NewEC_PERSONAS_DATOSRow();
			}
		}

		private void AtarControles(bool Caso)
		{
			if (Caso == true)
			{
				try
				{
					/*string Mail  = CeC_BD.EjecutaEscalarString("Select EC_PERSONAS.Persona_Email"+
						"FROM EC_PERSONAS,EC_TURNOS " +
						"WHERE EC_TURNOS.TURNO_ID = EC_PERSONAS.TURNO_ID AND EC_PERSONAS.PERSONA_LINK_ID = "+Fila.TRACVE);*/

					string email_reg = CeC_BD.EjecutaEscalarString("Select EC_PERSONAS.Persona_Email"+
						" FROM EC_PERSONAS " +
						"WHERE EC_PERSONAS.PERSONA_LINK_ID = "+Fila.TRACVE.ToString());

					Persona_email.Text = email_reg;
				}
				catch
				{
					Persona_email.Text = ""; 
				}
				
				//TURNOS

				try
				{
					int Combo_Indice = Convert.ToInt32(CeC_BD.EjecutaEscalar("Select EC_PERSONAS.TURNO_ID "+
						"FROM EC_PERSONAS " +
						"WHERE EC_PERSONAS.PERSONA_LINK_ID = "+Fila.TRACVE));
//                    CeC_Grid.SeleccionaID(WebCombo1, Combo_Indice);
//                    WebCombo1.SelectedIndex = CBTurno.FindByValue(Combo_Indice.ToString()).Row.Index; 
					CBTurno.SelectedIndex = CBTurno.FindByValue(Combo_Indice.ToString()).Row.Index; 
				}
				catch(Exception ex)
				{
					CBTurno.SelectedIndex  = 0;
				}
				//EC_PERSONAS_DATOS
				try
				{
					Tracve.Text = Convert.ToString(Fila.TRACVE);
					Tracve.Value = Convert.ToString(Fila.TRACVE);
				}
				catch{
					Tracve.Text = "";
					Tracve.Value = null;
				}
				try
				{
					Tranom.Text = Fila.TRANOM;					
				}
				catch
				{
					Tranom.Text = "";
				}
					//if (Fila.DATARE.ToString() == "NaN" || Fila.DATARE.ToString() == "NeuN")
					
					//else
				try
				{
					//Datare.Text = Fila.DATARE.ToString();
					DatareWC.SelectedIndex = DatareWC.FindByValue(Fila.DATARE.ToString()).Row.Index;
				}
				catch
				{
					DatareWC.SelectedIndex = -1;
				}
				try
				{	
					//Datdep.Text = Fila.DATDEP;
					DatdepWC.SelectedIndex = DatdepWC.FindByValue(Fila.DATDEP.ToString()).Row.Index;
				}
				catch
				{
					DatdepWC.SelectedIndex = -1;
					//Datdep.Text = "";
				}
				try
				{
					//Datcct.Text = Fila.DATCCT;
					DatcctWC.SelectedIndex = DatcctWC.FindByValue(Fila.DATCCT.ToString()).Row.Index;
				}
				catch
				{
					DatcctWC.SelectedIndex = -1;
					//Datcct.Text ="";
				}
				try
				{
					//Cnocve.Text = Fila.CNOCVE;
					CnocveWC.SelectedIndex = CnocveWC.FindByValue(Fila.CNOCVE.ToString()).Row.Index;
				}
				catch
				{
					CnocveWC.SelectedIndex = -1;
					//Cnocve.Text = "";
				}
				try 
				{
					//Ciacve.Text = Fila.CIACVE;
					CiacveWC.SelectedIndex = CiacveWC.FindByValue(Fila.CIACVE.ToString()).Row.Index;
				}
				catch
				{
					//Ciacve.Text  = "";
					CiacveWC.SelectedIndex = -1;
				}
				try
				{
					Ns.Text = Fila.NS;
				}
				catch
				{
					Ns.Text = "";
				}
				try
				{
					//Grupo.Text = Fila.GRUPO;
					GrupoWC.SelectedIndex = GrupoWC.FindByValue(Fila.GRUPO.ToString()).Row.Index;
				}
				catch
				{
					//Grupo.Text = "";
					GrupoWC.SelectedIndex = -1;
				}
			}
			else
			{
				Fila.TRACVE = Convert.ToInt32(Tracve.Value);
				Fila.TRANOM = Tranom.Text; 
				try
				{
					Fila.DATARE = DatareWC.SelectedCell.Text;
				}
				catch
				{
					Fila.DATARE = "";
				}
				try
				{
					Fila.DATDEP = DatdepWC.SelectedCell.Text;
				}
				catch
				{
					Fila.DATDEP = "";
				}
				try
				{	
					Fila.DATCCT = DatcctWC.SelectedCell.Text;
				}
				catch
				{
					Fila.DATCCT = "";
				}
				try
				{
					Fila.CNOCVE = CnocveWC.SelectedCell.Text;
				}
				catch
				{
					Fila.CNOCVE = "";
				}
				try
				{
					Fila.CIACVE = CiacveWC.SelectedCell.Text;
				}
				catch
				{
					Fila.CIACVE = "";
				}
				try
				{
					Fila.NS = Ns.Text;
				}
				catch
				{
					Fila.NS = "";
				}
				try
				{
					Fila.GRUPO = GrupoWC.SelectedCell.Text;
				}
				catch
				{
					Fila.GRUPO = "";
				}
					CBBorrar.Checked = false;
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
			this.DA_Empleados = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.Conexion = new System.Data.OleDb.OleDbConnection();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.dS_Empleados1 = new DS_Empleados();
			this.Borrar_Empleado = new System.Data.OleDb.OleDbCommand();
			this.dS_Personas1 = new DS_Personas();
			this.oleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
			this.DA_Personas = new System.Data.OleDb.OleDbDataAdapter();
			this.DA_Tipo_Turnos = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand3 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand3 = new System.Data.OleDb.OleDbCommand();
			this.dS_Turnos1 = new DS_Turnos();
			this.oleDbSelectCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand4 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand4 = new System.Data.OleDb.OleDbCommand();
			this.DA_Personas_Terminales = new System.Data.OleDb.OleDbDataAdapter();
			((System.ComponentModel.ISupportInitialize)(this.dS_Empleados1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dS_Personas1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dS_Turnos1)).BeginInit();
			this.WebImageButton1.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WebImageButton1_Click);
			this.WebImageButton2.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WebImageButton2_Click);
			this.BDeshacerCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
			this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardarCambios_Click);
			// 
			// DA_Empleados
			// 
			this.DA_Empleados.DeleteCommand = this.oleDbDeleteCommand1;
			this.DA_Empleados.InsertCommand = this.oleDbInsertCommand1;
			this.DA_Empleados.SelectCommand = this.oleDbSelectCommand1;
			this.DA_Empleados.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS_DATOS", new System.Data.Common.DataColumnMapping[] {
																																																					new System.Data.Common.DataColumnMapping("TRACVE", "TRACVE"),
																																																					new System.Data.Common.DataColumnMapping("TRANOM", "TRANOM"),
																																																					new System.Data.Common.DataColumnMapping("DATARE", "DATARE"),
																																																					new System.Data.Common.DataColumnMapping("DATDEP", "DATDEP"),
																																																					new System.Data.Common.DataColumnMapping("DATCCT", "DATCCT"),
																																																					new System.Data.Common.DataColumnMapping("CNOCVE", "CNOCVE"),
																																																					new System.Data.Common.DataColumnMapping("CIACVE", "CIACVE"),
																																																					new System.Data.Common.DataColumnMapping("NS", "NS"),
																																																					new System.Data.Common.DataColumnMapping("GRUPO", "GRUPO")})});
			this.DA_Empleados.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// oleDbDeleteCommand1
			// 
			this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_PERSONAS_DATOS WHERE (TRACVE = ?)";
			this.oleDbDeleteCommand1.Connection = this.Conexion;
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TRACVE", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TRACVE", System.Data.DataRowVersion.Original, null));
			// 
			// Conexion
			// 
			this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
			// 
			// oleDbInsertCommand1
			// 
			this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_PERSONAS_DATOS (TRACVE, TRANOM, DATARE, DATDEP, DATCCT, CNOCVE, CIACVE" +
				", NS, GRUPO) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
			this.oleDbInsertCommand1.Connection = this.Conexion;
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TRACVE", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TRACVE", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TRANOM", System.Data.OleDb.OleDbType.VarChar, 60, "TRANOM"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DATARE", System.Data.OleDb.OleDbType.VarChar, 40, "DATARE"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DATDEP", System.Data.OleDb.OleDbType.VarChar, 40, "DATDEP"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DATCCT", System.Data.OleDb.OleDbType.VarChar, 10, "DATCCT"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CNOCVE", System.Data.OleDb.OleDbType.VarChar, 4, "CNOCVE"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CIACVE", System.Data.OleDb.OleDbType.VarChar, 4, "CIACVE"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("NS", System.Data.OleDb.OleDbType.VarChar, 32, "NS"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("GRUPO", System.Data.OleDb.OleDbType.VarChar, 45, "GRUPO"));
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT TRACVE, TRANOM, DATARE, DATDEP, DATCCT, CNOCVE, CIACVE, NS, GRUPO FROM eC" +
				"_EMPLEADOS WHERE (TRACVE = ?)";
			this.oleDbSelectCommand1.Connection = this.Conexion;
			this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TRACVE", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TRACVE", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbUpdateCommand1
			// 
			this.oleDbUpdateCommand1.CommandText = "UPDATE EC_PERSONAS_DATOS SET TRACVE = ?, TRANOM = ?, DATARE = ?, DATDEP = ?, DATCCT =" +
				" ?, CNOCVE = ?, CIACVE = ?, NS = ?, GRUPO = ? WHERE (TRACVE = ?)";
			this.oleDbUpdateCommand1.Connection = this.Conexion;
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TRACVE", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TRACVE", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TRANOM", System.Data.OleDb.OleDbType.VarChar, 60, "TRANOM"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DATARE", System.Data.OleDb.OleDbType.VarChar, 40, "DATARE"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DATDEP", System.Data.OleDb.OleDbType.VarChar, 40, "DATDEP"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DATCCT", System.Data.OleDb.OleDbType.VarChar, 10, "DATCCT"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CNOCVE", System.Data.OleDb.OleDbType.VarChar, 4, "CNOCVE"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CIACVE", System.Data.OleDb.OleDbType.VarChar, 4, "CIACVE"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("NS", System.Data.OleDb.OleDbType.VarChar, 32, "NS"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("GRUPO", System.Data.OleDb.OleDbType.VarChar, 45, "GRUPO"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TRACVE", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TRACVE", System.Data.DataRowVersion.Original, null));
			// 
			// dS_Empleados1
			// 
			this.dS_Empleados1.DataSetName = "DS_Empleados";
			this.dS_Empleados1.Locale = new System.Globalization.CultureInfo("es-MX");
			// 
			// Borrar_Empleado
			// 
			this.Borrar_Empleado.CommandText = "UPDATE EC_PERSONAS SET PERSONA_BORRADO = 1 WHERE (PERSONA_LINK_ID = ?)";
			this.Borrar_Empleado.Connection = this.Conexion;
			this.Borrar_Empleado.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_LINK_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_LINK_ID", System.Data.DataRowVersion.Original, null));
			// 
			// dS_Personas1
			// 
			this.dS_Personas1.DataSetName = "DS_Personas";
			this.dS_Personas1.Locale = new System.Globalization.CultureInfo("es-MX");
			// 
			// oleDbSelectCommand2
			// 
			this.oleDbSelectCommand2.CommandText = "SELECT PERSONA_ID, PERSONA_LINK_ID, TIPO_PERSONA_ID, SUSCRIPCION_ID, GRUPO_2_ID, GRUP" +
				"O_3_ID, PERSONA_NOMBRE, TURNO_ID FROM EC_PERSONAS WHERE (PERSONA_ID = ?)";
			this.oleDbSelectCommand2.Connection = this.Conexion;
			this.oleDbSelectCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbInsertCommand2
			// 
			this.oleDbInsertCommand2.CommandText = "INSERT INTO EC_PERSONAS(PERSONA_ID, PERSONA_LINK_ID, TIPO_PERSONA_ID, SUSCRIPCION_ID" +
				", GRUPO_2_ID, GRUPO_3_ID, PERSONA_NOMBRE, TURNO_ID) VALUES (?, ?, ?, ?, ?, ?, ?," +
				" ?)";
			this.oleDbInsertCommand2.Connection = this.Conexion;
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_LINK_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_LINK_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_PERSONA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("SUSCRIPCION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("GRUPO_2_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_2_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("GRUPO_3_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_3_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "PERSONA_NOMBRE"));
			this.oleDbInsertCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbUpdateCommand2
			// 
			this.oleDbUpdateCommand2.CommandText = @"UPDATE EC_PERSONAS SET PERSONA_ID = ?, PERSONA_LINK_ID = ?, TIPO_PERSONA_ID = ?, SUSCRIPCION_ID = ?, GRUPO_2_ID = ?, GRUPO_3_ID = ?, PERSONA_NOMBRE = ?, TURNO_ID = ? WHERE (PERSONA_ID = ?) AND (SUSCRIPCION_ID = ? OR ? IS NULL AND SUSCRIPCION_ID IS NULL) AND (GRUPO_2_ID = ? OR ? IS NULL AND GRUPO_2_ID IS NULL) AND (GRUPO_3_ID = ? OR ? IS NULL AND GRUPO_3_ID IS NULL) AND (PERSONA_LINK_ID = ?) AND (PERSONA_NOMBRE = ? OR ? IS NULL AND PERSONA_NOMBRE IS NULL) AND (TIPO_PERSONA_ID = ? OR ? IS NULL AND TIPO_PERSONA_ID IS NULL) AND (TURNO_ID = ? OR ? IS NULL AND TURNO_ID IS NULL)";
			this.oleDbUpdateCommand2.Connection = this.Conexion;
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_LINK_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_LINK_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_PERSONA_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("SUSCRIPCION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("GRUPO_2_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_2_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("GRUPO_3_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_3_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("PERSONA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "PERSONA_NOMBRE"));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_GRUPO_2_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_2_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_GRUPO_2_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_2_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_GRUPO_3_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_3_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_GRUPO_3_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_3_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_LINK_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_LINK_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERSONA_NOMBRE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_NOMBRE1", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERSONA_NOMBRE", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_PERSONA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_PERSONA_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_PERSONA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbDeleteCommand2
			// 
			this.oleDbDeleteCommand2.CommandText = @"DELETE FROM EC_PERSONAS WHERE (PERSONA_ID = ?) AND (SUSCRIPCION_ID = ? OR ? IS NULL AND SUSCRIPCION_ID IS NULL) AND (GRUPO_2_ID = ? OR ? IS NULL AND GRUPO_2_ID IS NULL) AND (GRUPO_3_ID = ? OR ? IS NULL AND GRUPO_3_ID IS NULL) AND (PERSONA_LINK_ID = ?) AND (PERSONA_NOMBRE = ? OR ? IS NULL AND PERSONA_NOMBRE IS NULL) AND (TIPO_PERSONA_ID = ? OR ? IS NULL AND TIPO_PERSONA_ID IS NULL) AND (TURNO_ID = ? OR ? IS NULL AND TURNO_ID IS NULL)";
			this.oleDbDeleteCommand2.Connection = this.Conexion;
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_SUSCRIPCION_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "SUSCRIPCION_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_GRUPO_2_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_2_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_GRUPO_2_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_2_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_GRUPO_3_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_3_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_GRUPO_3_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "GRUPO_3_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_LINK_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "PERSONA_LINK_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERSONA_NOMBRE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_PERSONA_NOMBRE1", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PERSONA_NOMBRE", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_PERSONA_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_PERSONA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_PERSONA_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_PERSONA_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand2.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TURNO_ID1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TURNO_ID", System.Data.DataRowVersion.Original, null));
			// 
			// DA_Personas
			// 
			this.DA_Personas.DeleteCommand = this.oleDbDeleteCommand2;
			this.DA_Personas.InsertCommand = this.oleDbInsertCommand2;
			this.DA_Personas.SelectCommand = this.oleDbSelectCommand2;
			this.DA_Personas.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								  new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																				  new System.Data.Common.DataColumnMapping("PERSONA_LINK_ID", "PERSONA_LINK_ID"),
																																																				  new System.Data.Common.DataColumnMapping("TIPO_PERSONA_ID", "TIPO_PERSONA_ID"),
																																																				  new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																				  new System.Data.Common.DataColumnMapping("GRUPO_2_ID", "GRUPO_2_ID"),
																																																				  new System.Data.Common.DataColumnMapping("GRUPO_3_ID", "GRUPO_3_ID"),
																																																				  new System.Data.Common.DataColumnMapping("PERSONA_NOMBRE", "PERSONA_NOMBRE"),
																																																				  new System.Data.Common.DataColumnMapping("TURNO_ID", "TURNO_ID")})});
			this.DA_Personas.UpdateCommand = this.oleDbUpdateCommand2;
			// 
			// DA_Tipo_Turnos
			// 
			this.DA_Tipo_Turnos.DeleteCommand = this.oleDbDeleteCommand3;
			this.DA_Tipo_Turnos.InsertCommand = this.oleDbInsertCommand3;
			this.DA_Tipo_Turnos.SelectCommand = this.oleDbSelectCommand3;
			this.DA_Tipo_Turnos.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									 new System.Data.Common.DataTableMapping("Table", "EC_TURNOS_Combo", new System.Data.Common.DataColumnMapping[] {
																																																						 new System.Data.Common.DataColumnMapping("TURNO_ID", "TURNO_ID"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_NOMBRE", "TURNO_NOMBRE"),
																																																						 new System.Data.Common.DataColumnMapping("TURNO_BORRADO", "TURNO_BORRADO")})});
			this.DA_Tipo_Turnos.UpdateCommand = this.oleDbUpdateCommand3;
			// 
			// oleDbDeleteCommand3
			// 
			this.oleDbDeleteCommand3.CommandText = "DELETE FROM EC_TIPO_TURNOS WHERE (TIPO_TURNO_ID = ?) AND (TIPO_TURNO_NOMBRE = ?)" +
				"";
			this.oleDbDeleteCommand3.Connection = this.Conexion;
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_TURNO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_TURNO_NOMBRE", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbInsertCommand3
			// 
			this.oleDbInsertCommand3.CommandText = "INSERT INTO EC_TIPO_TURNOS(TIPO_TURNO_NOMBRE, TIPO_TURNO_ID) VALUES (?, ?)";
			this.oleDbInsertCommand3.Connection = this.Conexion;
			this.oleDbInsertCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_TURNO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TIPO_TURNO_NOMBRE"));
			this.oleDbInsertCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbSelectCommand3
			// 
			this.oleDbSelectCommand3.CommandText = "SELECT TURNO_ID, TURNO_NOMBRE, TURNO_BORRADO FROM EC_TURNOS WHERE (TURNO_BORRADO" +
				" = 0)";
			this.oleDbSelectCommand3.Connection = this.Conexion;
			// 
			// oleDbUpdateCommand3
			// 
			this.oleDbUpdateCommand3.CommandText = "UPDATE EC_TIPO_TURNOS SET TIPO_TURNO_NOMBRE = ?, TIPO_TURNO_ID = ? WHERE (TIPO_T" +
				"URNO_ID = ?) AND (TIPO_TURNO_NOMBRE = ?)";
			this.oleDbUpdateCommand3.Connection = this.Conexion;
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_TURNO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "TIPO_TURNO_NOMBRE"));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("TIPO_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_TURNO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "TIPO_TURNO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbUpdateCommand3.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TIPO_TURNO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TIPO_TURNO_NOMBRE", System.Data.DataRowVersion.Original, null));
			// 
			// dS_Turnos1
			// 
			this.dS_Turnos1.DataSetName = "DS_Turnos";
			this.dS_Turnos1.Locale = new System.Globalization.CultureInfo("es-MX");
			// 
			// oleDbSelectCommand4
			// 
			this.oleDbSelectCommand4.CommandText = "SELECT TERMINAL_ID, TERMINAL_NOMBRE FROM EC_TERMINALES";
			this.oleDbSelectCommand4.Connection = this.Conexion;
			// 
			// DA_Personas_Terminales
			// 
			this.DA_Personas_Terminales.DeleteCommand = this.oleDbDeleteCommand4;
			this.DA_Personas_Terminales.InsertCommand = this.oleDbInsertCommand4;
			this.DA_Personas_Terminales.SelectCommand = this.oleDbSelectCommand4;
			this.DA_Personas_Terminales.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											 new System.Data.Common.DataTableMapping("Table", "EC_TERMINALES_PERSONAS_ASIGNAR", new System.Data.Common.DataColumnMapping[] {
																																																												new System.Data.Common.DataColumnMapping("TERMINAL_ID", "TERMINAL_ID"),
																																																												new System.Data.Common.DataColumnMapping("TERMINAL_NOMBRE", "TERMINAL_NOMBRE")})});
			this.DA_Personas_Terminales.UpdateCommand = this.oleDbUpdateCommand4;
			((System.ComponentModel.ISupportInitialize)(this.dS_Empleados1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dS_Personas1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dS_Turnos1)).EndInit();

		}
		#endregion

		private void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			LError.Text = "";
			LCorrecto.Text = "";
			AtarControles(true);
		}

		private void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			
		}

		private void WebCombo1_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
		{
		}

		protected void WebImageButton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			string StrFileName = File1.PostedFile.FileName.Substring(File1.PostedFile.FileName.LastIndexOf("\\") + 1) ;
			string StrFileType = File1.PostedFile.ContentType ;
			int IntFileSize =File1.PostedFile.ContentLength;
			int PersonaId = CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Sesion.WF_Empleados_PERSONA_LINK_ID.ToString()); 
				
			if (IntFileSize <=0)
				Response.Write(" <font color='Red' size='2'>Uploading of file " + StrFileName + " failed </font>");
			else
			{
				byte [] Datos = new byte[File1.PostedFile.ContentLength];
				File1.PostedFile.InputStream.Read(Datos,0,File1.PostedFile.ContentLength);
				if (Datos != null && CeC_Personas.AsignaFoto(PersonaId,Datos))
				{
					//Foto Guardada correctamente
					//LCorrecto.Text = "Foto Guardada correctamente";
				}
				else
				{
					//No se pudo guardar la foto
					//LError.Text = "No se pudo guardar la foto";
				}
			}
		}

		private void WebImageButton2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			if (Convert.ToInt32(Tracve.Value) > 0)
			{
				int Persona_id = CeC_BD.EjecutaEscalarInt("Select Persona_id From EC_PERSONAS where EC_PERSONAS.persona_link_id = "+ Tracve.Value);

				if (Persona_id > 0 )
				{
					BGuardarCambios_Click(sender,e);
					CeC_Personas.BorraFoto(Persona_id);
				}
			}
			else
			{
				LError.Text = "Error: el No.Empleado(tracve) no es válido"; 
			}
		}

        protected void DatareWC_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(DatareWC);
        }

        protected void DatdepWC_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(DatdepWC);
        }

        protected void DatcctWC_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(DatcctWC);
        }

        protected void CnocveWC_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(CnocveWC);
        }

        protected void CiacveWC_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(CiacveWC);
        }

        protected void GrupoWC_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(GrupoWC);
        }

        protected void TIPO_TURNO_COMBO_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(CBTurno);
        }

        protected void BGuardarCambios_Click1(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
        }
        protected void BGuardarCambios_Click2(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
        {
        }
    }
}
