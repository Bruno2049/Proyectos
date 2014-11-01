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
	/// Descripción breve de WF_Dias_FestivosE.
	/// </summary>
	public partial class WF_Dias_FestivosE : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Data.OleDb.OleDbDataAdapter DA_DiasFEstivos_Edicion;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		protected System.Data.OleDb.OleDbConnection Conexion;
		protected DS_DiasFestivos dS_DiasFestivos1;
		CeC_Sesion Sesion;
		DS_DiasFestivos.EC_DIAS_FESTIVOS_EdicionRow Fila;
		
		private void Habilitarcontroles(bool Caso,string Restriccion)
		{
			if(!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Dias_Festivos0Nuevo) && !Sesion.TienePermiso(CEC_RESTRICCIONES.S0Dias_Festivos0Editar))
			{
				BGuardarCambios.Visible = Caso;
				BDeshacerCambios.Visible = Caso;
				Fecha.Visible = Caso;
				RVDiaFestivoNombre.Visible = Caso;
			}
			if (!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Dias_Festivos0Borrar))
			{
				CBBorrar.Visible = Caso;
				LBorrar.Visible = Caso;
			}
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aquí el código de usuario para inicializar la página
			Sesion = CeC_Sesion.Nuevo(this);
			Sesion.TituloPagina = "Edición de los Dias Festivos";
			Sesion.DescripcionPagina = "Ingrese los datos";

			// Permisos****************************************
			
			string [] Permiso = new string [10];
			/*Permiso[0] = "S";
			Permiso[1] = "S.Dias_Festivos";
			Permiso[2] = "S.Dias_Festivos.Nuevo";
			Permiso[3] = "S.Dias_Festivos.Editar";
			Permiso[4] = "S.Dias_Festivos.Borrar";*/
			Permiso[0] = CEC_RESTRICCIONES.S0Dias_Festivos0Nuevo;
			Permiso[1] = CEC_RESTRICCIONES.S0Dias_Festivos0Editar;
			Permiso[2] = CEC_RESTRICCIONES.S0Dias_Festivos0Borrar;

			if (!Sesion.Acceso(Permiso,CIT_Perfiles.Acceso(Sesion.PERFIL_ID,this)))
			{
				CIT_Perfiles.CrearVentana(this,Sesion.MensajeVentanaJScript(),Sesion.TituloPagina,"Aceptar","WF_Main.aspx","","");
				Habilitarcontroles(false,Sesion.Restriccion.ToString());
				return;
			}

			Habilitarcontroles(false,Sesion.Restriccion.ToString());
			//**************************************************

			DA_DiasFEstivos_Edicion.SelectCommand.Parameters[0].Value = Sesion.WF_Dias_Festivos_DIAFESTIVO_ID;
			DA_DiasFEstivos_Edicion.Fill(dS_DiasFestivos1.EC_DIAS_FESTIVOS_Edicion);

			if(Sesion.WF_Dias_Festivos_DIAFESTIVO_ID> 0)
			{
				if(dS_DiasFestivos1.EC_DIAS_FESTIVOS_Edicion.Rows.Count>0)
				{
					Fila = (DS_DiasFestivos.EC_DIAS_FESTIVOS_EdicionRow)dS_DiasFestivos1.EC_DIAS_FESTIVOS_Edicion.Rows[0];
                    if (!IsPostBack)
                    {
                        AtarControles(true);
                        //Agregar Módulo Log
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Días Festivos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
                    }
				}
				else
				{
					Fila = dS_DiasFestivos1.EC_DIAS_FESTIVOS_Edicion.NewEC_DIAS_FESTIVOS_EdicionRow();
					LBorrar.Visible = false;
					CBBorrar.Visible = false;
				}
			}
			else
			{
					Fila = dS_DiasFestivos1.EC_DIAS_FESTIVOS_Edicion.NewEC_DIAS_FESTIVOS_EdicionRow();
                    if (!IsPostBack)
                    {
                        AtarControles(true);
                        //Agregar Módulo Log
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Días Festivos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
                    }
				LBorrar.Visible = false;
				CBBorrar.Visible = false;
			}
		}

		private void AtarControles(bool Caso)
		{
			if (Caso == true)
			{
				if(Sesion.WF_Dias_Festivos_DIAFESTIVO_ID>-1)
				{
					try
					{
						DiaFestivoId.Text = Convert.ToString(Fila.DIA_FESTIVO_ID);
					}
					catch
					{
						DiaFestivoId.Text ="";
					}
				}
				else
				{
			//		DiaFestivoId.Text = Convert.ToString(CeC_Autonumerico.GeneraAutonumerico("EC_DIAS_FESTIVOS","DIA_FESTIVO_ID"));
				}
				try
				{
		            Fecha.SelectedDate  = Fila.DIA_FESTIVO_FECHA;
				}
				catch
				{
                    Fecha.SelectedDate = System.DateTime.Today;
				}
				try
				{
					DiaFestivoNombre.Text = Fila.DIA_FESTIVO_NOMBRE;
				}
				catch
				{
					DiaFestivoNombre.Text = "";
				}
				try
				{
					CBBorrar.Checked = Convert.ToBoolean(Fila.DIA_FESTIVO_BORRADO);
				}
				catch
				{
					CBBorrar.Checked = false;
				}
			}
			else
			{
                Fila.DIA_FESTIVO_ID = 0;//CeC_Autonumerico.GeneraAutonumerico("EC_DIAS_FESTIVOS", "DIA_FESTIVO_ID");
				//	DiaFestivoFecha.Value = Convert.ToString(DateTime.Today);
				Fila.DIA_FESTIVO_FECHA = Convert.ToDateTime(Fecha.SelectedDate);
				Fila.DIA_FESTIVO_NOMBRE = DiaFestivoNombre.Text;
				Fila.DIA_FESTIVO_BORRADO = Convert.ToInt32(CBBorrar.Checked);
			}
			Sesion.TituloPagina = "Dias Festivos-Edicion";
			Sesion.DescripcionPagina = "Ingrese los datos del dia festivo";
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
			this.DA_DiasFEstivos_Edicion = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.Conexion = new System.Data.OleDb.OleDbConnection();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.dS_DiasFestivos1 = new DS_DiasFestivos();
			((System.ComponentModel.ISupportInitialize)(this.dS_DiasFestivos1)).BeginInit();
			this.BDeshacerCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
			this.BGuardarCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BGuardarCambios_Click);
			// 
			// DA_DiasFEstivos_Edicion
			// 
			this.DA_DiasFEstivos_Edicion.DeleteCommand = this.oleDbDeleteCommand1;
			this.DA_DiasFEstivos_Edicion.InsertCommand = this.oleDbInsertCommand1;
			this.DA_DiasFEstivos_Edicion.SelectCommand = this.oleDbSelectCommand1;
			this.DA_DiasFEstivos_Edicion.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											  new System.Data.Common.DataTableMapping("Table", "EC_DIAS_FESTIVOS_Edicion", new System.Data.Common.DataColumnMapping[] {
																																																										   new System.Data.Common.DataColumnMapping("DIA_FESTIVO_ID", "DIA_FESTIVO_ID"),
																																																										   new System.Data.Common.DataColumnMapping("DIA_FESTIVO_FECHA", "DIA_FESTIVO_FECHA"),
																																																										   new System.Data.Common.DataColumnMapping("DIA_FESTIVO_NOMBRE", "DIA_FESTIVO_NOMBRE"),
																																																										   new System.Data.Common.DataColumnMapping("DIA_FESTIVO_BORRADO", "DIA_FESTIVO_BORRADO")})});
			this.DA_DiasFEstivos_Edicion.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// oleDbDeleteCommand1
			// 
			this.oleDbDeleteCommand1.CommandText = "DELETE FROM EC_DIAS_FESTIVOS WHERE (DIA_FESTIVO_ID = ?) AND (DIA_FESTIVO_BORRADO" +
				" = ? OR ? IS NULL AND DIA_FESTIVO_BORRADO IS NULL) AND (DIA_FESTIVO_FECHA = ?) A" +
				"ND (DIA_FESTIVO_NOMBRE = ?)";
			this.oleDbDeleteCommand1.Connection = this.Conexion;
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "DIA_FESTIVO_ID", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_BORRADO1", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DIA_FESTIVO_FECHA", System.Data.DataRowVersion.Original, null));
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DIA_FESTIVO_NOMBRE", System.Data.DataRowVersion.Original, null));
			// 
			// Conexion
			// 
			this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
			// 
			// oleDbInsertCommand1
			// 
			this.oleDbInsertCommand1.CommandText = "INSERT INTO EC_DIAS_FESTIVOS(DIA_FESTIVO_ID, DIA_FESTIVO_FECHA, DIA_FESTIVO_NOMB" +
				"RE, DIA_FESTIVO_BORRADO) VALUES (?, ?, ?, ?)";
			this.oleDbInsertCommand1.Connection = this.Conexion;
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "DIA_FESTIVO_ID", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "DIA_FESTIVO_FECHA"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "DIA_FESTIVO_NOMBRE"));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT DIA_FESTIVO_ID, DIA_FESTIVO_FECHA, DIA_FESTIVO_NOMBRE, DIA_FESTIVO_BORRADO" +
				" FROM EC_DIAS_FESTIVOS WHERE (DIA_FESTIVO_ID = ?)" +
				"";
			this.oleDbSelectCommand1.Connection = this.Conexion;
			this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "DIA_FESTIVO_ID", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbUpdateCommand1
			// 
			this.oleDbUpdateCommand1.CommandText = "UPDATE EC_DIAS_FESTIVOS SET DIA_FESTIVO_FECHA = ?, DIA_FESTIVO_NOMBRE = ?, DIA_F" +
				"ESTIVO_BORRADO = ? WHERE (DIA_FESTIVO_ID = ?)";
			this.oleDbUpdateCommand1.Connection = this.Conexion;
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_FECHA", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "DIA_FESTIVO_FECHA"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_NOMBRE", System.Data.OleDb.OleDbType.VarChar, 45, "DIA_FESTIVO_NOMBRE"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DIA_FESTIVO_BORRADO", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(1)), ((System.Byte)(0)), "DIA_FESTIVO_BORRADO", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DIA_FESTIVO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "DIA_FESTIVO_ID", System.Data.DataRowVersion.Original, null));
			// 
			// dS_DiasFestivos1
			// 
			this.dS_DiasFestivos1.DataSetName = "DS_DiasFestivos";
			this.dS_DiasFestivos1.Locale = new System.Globalization.CultureInfo("es-MX");
			((System.ComponentModel.ISupportInitialize)(this.dS_DiasFestivos1)).EndInit();

		}
		#endregion

        protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			AtarControles(true);
		}

        protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			LCorrecto.Text = "";
			LError.Text = "";
			try
			{
				AtarControles(false);
				DateTime Fecha1 = Fecha.SelectedDate;
					
				string Fecha_A = Fecha1.Year.ToString();
				string Fecha_M = Fecha1.Month.ToString();
				string Fecha_D = Fecha1.Day.ToString();
					
				string Fechastr = string.Concat(Fecha_A,"/",Fecha_M,"/",Fecha_D);
				
				int DIAFESTIVO_ID_Imp = CeC_BD.EjecutaEscalarInt("Select DIA_FESTIVO_ID From EC_DIAS_FESTIVOS WHERE DIA_FESTIVO_FECHA = " + CeC_BD.SqlFecha(Fecha1) + " AND DIA_FESTIVO_BORRADO = 0)");
				int DIAFESTIVO_ID = CeC_BD.EjecutaEscalarInt("Select DIA_FESTIVO_ID From EC_DIAS_FESTIVOS WHERE DIA_FESTIVO_FECHA = " + CeC_BD.SqlFecha(Fecha1) + " AND DIA_FESTIVO_ID!="+DIAFESTIVO_ID_Imp+"  AND DIA_FESTIVO_BORRADO = 0 ");

				int Modificaciones = 0;
				if (Sesion.WF_Dias_Festivos_DIAFESTIVO_ID<0)
				{
					if (DIAFESTIVO_ID<=0)
					{
						Fila.DIA_FESTIVO_ID = CeC_Autonumerico.GeneraAutonumerico("EC_DIAS_FESTIVOS","DIA_FESTIVO_ID",Sesion);
						DiaFestivoId.Text = Convert.ToString(Fila.DIA_FESTIVO_ID);	
						dS_DiasFestivos1.EC_DIAS_FESTIVOS_Edicion.AddEC_DIAS_FESTIVOS_EdicionRow(Fila);
					}
					else
					{
						Fila.DIA_FESTIVO_ID  = DIAFESTIVO_ID;
					}
				}	
				if (Sesion.WF_Dias_Festivos_DIAFESTIVO_ID<0)
				{
					//Insercion
					if (DIAFESTIVO_ID>0)
					{
						LError.Text  = "Dia Festivo ya existe en la base de Datos";
						return;
					}
					else
					{
						//Agregar ModuloLog***
						int FID = Convert.ToInt32(Fila.DIA_FESTIVO_ID);
						string  FNombre = Fila.DIA_FESTIVO_NOMBRE.ToString();
						Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.NUEVO,"Días Festivos",FID,FNombre,Sesion.SESION_ID);
						//*****
					}
				}
				else
				{
					//Update
					/*if (DIAFESTIVO_ID_Imp>0 && DIAFESTIVO_ID<0)
					{
						LError.Text  = "Dia Festivo ya esta existe en la base de Datos";
						return;
					}
					else*/ if (DIAFESTIVO_ID_Imp>0 && DIAFESTIVO_ID>0)
						   {
							   LError.Text = "Existen duplicados de este Dia";
							   return;
						   }
						   else
						   {
							   //Agregar ModuloLog***
							   string  FNombre = Fila.DIA_FESTIVO_NOMBRE.ToString();
							   Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.EDICION,"Dias Festivos",Sesion.WF_Dias_Festivos_DIAFESTIVO_ID,FNombre,Sesion.SESION_ID);
							   //*****
						   }
				}
				DA_DiasFEstivos_Edicion.UpdateCommand.Parameters[0].Value = Fila.DIA_FESTIVO_ID;
				Modificaciones = DA_DiasFEstivos_Edicion.Update(dS_DiasFestivos1.EC_DIAS_FESTIVOS_Edicion);
				LCorrecto.Text = Modificaciones.ToString() + " registros modificados";

				Sesion.Redirige("WF_Dias_Festivos.aspx");
			}
			catch(Exception ex)
			{
				LError.Text = "Error :" + ex.Message;
			}
		}
	}
}