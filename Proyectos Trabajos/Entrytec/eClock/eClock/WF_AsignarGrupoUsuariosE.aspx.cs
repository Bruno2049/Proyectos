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
	/// Descripción breve de WF_AsignarGrupoUsuariosE.
	/// </summary>
	public partial class WF_AsignarGrupoUsuariosE : System.Web.UI.Page
	{
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		protected System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		protected System.Data.OleDb.OleDbDataAdapter DA_Grupos;
		protected System.Data.OleDb.OleDbConnection Conexion;
		protected DS_AsignaGrupoUsuario dS_AsignaGrupoUsuario1;

		CeC_Sesion Sesion;
	
		private void Habilitarcontroles(bool Caso,string Restriccion)
		{
			if(!Sesion.TienePermiso(CEC_RESTRICCIONES.S0Usuarios0Asignar_Grupo_Usuarios))
			{
				Grid.Visible = Caso;
				WebImageButton1.Visible = Caso;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Especialmente para asignarle un grupo default Centro de costos a los usuarios
			CeC_BD.EjecutaComando("INSERT INTO EC_PERMISOS_SUSCRIP (USUARIO_ID, SUSCRIPCION_ID) SELECT USUARIO_ID, SUSCRIPCION_ID FROM EC_USUARIOS, EC_PERSONAS_DATOS, EC_SUSCRIPCION  WHERE USUARIO_ID NOT IN (SELECT USUARIO_ID FROM EC_PERMISOS_SUSCRIP) AND USUARIO_BORRADO = 0 AND EC_PERSONAS_DATOS.DATCCT = EC_SUSCRIPCION.SUSCRIPCION_NOMBRE AND EC_PERSONAS_DATOS.TRACVE = USUARIO_USUARIO ");

			// Introducir aquí el código de usuario para inicializar la página
			Sesion = CeC_Sesion.Nuevo(this);
			Sesion.TituloPagina = "Asignación de Grupos";
			Sesion.DescripcionPagina = "Para borrar un Grupo Asignado, solo da click en la casilla de la fila llamada Borrado y presiona el botón Guardar Cambios";

			// Permisos****************************************
			
			string [] Permiso = new string [10];
			
			/*Permiso[0] = "S";
			Permiso[1] = "S.Usuarios";
			Permiso[2] = "S.Usuarios.Asignar_Grupo_Usuarios";*/
			Permiso[0] = CEC_RESTRICCIONES.S0Usuarios0Asignar_Grupo_Usuarios;

			if (!Sesion.Acceso(Permiso,CIT_Perfiles.Acceso(Sesion.PERFIL_ID,this)))
			{
				CIT_Perfiles.CrearVentana(this,Sesion.MensajeVentanaJScript(),Sesion.TituloPagina,"Aceptar","WF_Main.aspx","","");
				Habilitarcontroles(false,Sesion.Restriccion.ToString());
				return;
			}

			Habilitarcontroles(false,Sesion.Restriccion.ToString());
			//**************************************************

			DA_Grupos.SelectCommand.Parameters[0].Value = Sesion.WF_AsignarGrupoUsuarios_Usuario_ID;
			DA_Grupos.Fill(dS_AsignaGrupoUsuario1.EC_PERMISOS_SUSCRIP_Asignados);

            if (!IsPostBack)
            {
                Grid.DataBind();
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asignación Grupo a Usuarios Edición", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
			this.DA_Grupos = new System.Data.OleDb.OleDbDataAdapter();
			this.dS_AsignaGrupoUsuario1 = new DS_AsignaGrupoUsuario();
			((System.ComponentModel.ISupportInitialize)(this.dS_AsignaGrupoUsuario1)).BeginInit();
			this.WebImageButton2.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WebImageButton2_Click);
			this.WebImageButton1.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.WebImageButton1_Click);
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = @"SELECT EC_PERMISOS_SUSCRIP.USUARIO_ID, EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID, EC_SUSCRIPCION.SUSCRIPCION_NOMBRE, 0 AS BORRADO FROM EC_PERMISOS_SUSCRIP, EC_SUSCRIPCION WHERE EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID = EC_SUSCRIPCION.SUSCRIPCION_ID AND (EC_PERMISOS_SUSCRIP.USUARIO_ID = ?)";
			this.oleDbSelectCommand1.Connection = this.Conexion;
			this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("USUARIO_ID", System.Data.OleDb.OleDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(38)), ((System.Byte)(0)), "USUARIO_ID", System.Data.DataRowVersion.Current, null));
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
																								new System.Data.Common.DataTableMapping("Table", "EC_PERMISOS_SUSCRIP_Asignados", new System.Data.Common.DataColumnMapping[] {
																																																								   new System.Data.Common.DataColumnMapping("USUARIO_ID", "USUARIO_ID"),
																																																								   new System.Data.Common.DataColumnMapping("SUSCRIPCION_ID", "SUSCRIPCION_ID"),
																																																								   new System.Data.Common.DataColumnMapping("SUSCRIPCION_NOMBRE", "SUSCRIPCION_NOMBRE")})});
			this.DA_Grupos.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// dS_AsignaGrupoUsuario1
			// 
			this.dS_AsignaGrupoUsuario1.DataSetName = "DS_AsignaGrupoUsuario";
			this.dS_AsignaGrupoUsuario1.Locale = new System.Globalization.CultureInfo("es-MX");
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dS_AsignaGrupoUsuario1)).EndInit();

		}
		#endregion

		private void WebImageButton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			try
			{
				int Modificados = 0 ;
				for (int i = 0 ; i < Grid.Rows.Count; i++)
				{
					int Grupo_id = Convert.ToInt32(Grid.Rows[i].Cells[1].Value);
					if (Convert.ToInt32(Grid.Rows[i].Cells[3].Value) > 0)
					{
						int ret  = CeC_BD.EjecutaComando("Delete from EC_PERMISOS_SUSCRIP where usuario_id = "+Sesion.WF_AsignarGrupoUsuarios_Usuario_ID+" and SUSCRIPCION_ID="+Grupo_id);
						if(ret>0)
							Modificados++;
					}
				}
				dS_AsignaGrupoUsuario1.EC_PERMISOS_SUSCRIP_Asignados.Clear();
				DA_Grupos.SelectCommand.Parameters[0].Value = Sesion.WF_AsignarGrupoUsuarios_Usuario_ID;
				DA_Grupos.Fill(dS_AsignaGrupoUsuario1.EC_PERMISOS_SUSCRIP_Asignados);
				Grid.DataBind();
				
				LCorrecto.Text = Modificados+" registros Modificados";
			}
			catch (Exception ex)
			{
				LError.Text = "Error :" + ex.Message;
			}
		}

		private void WebImageButton2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			Sesion.Redirige("WF_AsignarGrupoUsuarios.aspx");
		}

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid);
        }
    }
}
