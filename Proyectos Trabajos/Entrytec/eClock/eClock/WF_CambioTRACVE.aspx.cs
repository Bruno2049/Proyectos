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
	/// Descripción breve de WF_CambioTRACVE.
	/// </summary>
	public partial class WF_CambioTRACVE : System.Web.UI.Page
	{
		CeC_Sesion Sesion;

		private void Habilitarcontroles(bool Caso)
		{
			if(!Sesion.TienePermiso(CEC_RESTRICCIONES.S))
			{
				Panel1.Visible = Caso;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aquí el código de usuario para inicializar la página
			Sesion = CeC_Sesion.Nuevo(this);

			Sesion.TituloPagina = "Cambio de Tracve";
			Sesion.DescripcionPagina = "Estas Editando el No Empleado " + Sesion.WF_Empleados_PERSONA_LINK_ID;

			// Permisos****************************************
			string [] Permiso = new string [10];
			
			Permiso[0] = "S";

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
					Sesion.WF_EmpleadosBus_Link = "WF_CambioTRACVE.aspx";
					Sesion.WF_EmpleadosBus();
				}
				else
				{
					Sesion.WF_EmpleadosBus_Query_Temp = Sesion.WF_EmpleadosFil_Qry;
				}
			}

			Sesion.WF_EmpleadosBus_Query = "";
			Label3.Text = Sesion.WF_Empleados_PERSONA_LINK_ID.ToString();

            //Agregar Módulo Log
            if (!IsPostBack)
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Cambio TRACVE", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
			this.Enviar_Reporte.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Enviar_Reporte_Click);

		}
		#endregion

		private void Enviar_Reporte_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			
			LError.Text = "";
			LCorrecto.Text = "";
			int TRACVE = 0 ;
			try
			{
				TRACVE = Convert.ToInt32(TextBox1.Text);
			}
			catch(Exception ex)
			{
				LError.Text = "Error " + ex.Message;
			}
			try
			{
				if (TextBox1.Text == Label3.Text)
				{
					LError.Text = "El tracve debe de ser diferente del original";
					return;
				}	
				int Nempleados = CeC_BD.EjecutaEscalarInt("Select count(*) EC_PERSONAS_DATOS where tracve = "+TextBox1.Text);
				int Npersonas = CeC_BD.EjecutaEscalarInt("Select count(*) EC_PERSONAS where persona_link_id  = "+TextBox1.Text);

				if (Nempleados > 1)
				{
					LError.Text = "+2 Numero de Empleados encontrados";
					return;
				}
				else if(Nempleados <=0)
				{
					if (Npersonas > 1)
					{
						LError.Text = "+2 Numero de Personas encontrados";
						return;
					}
				}
				string qry1 = "Update EC_PERSONAS_DATOS set TRACVE = " + TRACVE + " where tracve = "+Label3.Text;
				string qry2 = "Update EC_PERSONAS set Persona_link_id = " + TRACVE+ " where Persona_link_id = "+Label3.Text;

				string str_conexion = Sesion.ObtenerValorWebconfigP("gBDatos.ConnectionString");
				System.Data.OleDb.OleDbConnection Conexion = new System.Data.OleDb.OleDbConnection(str_conexion);

				if (Conexion.State!=System.Data.ConnectionState.Open)
						Conexion.Open();
			
				System.Data.OleDb.OleDbCommand Commando1 = new System.Data.OleDb.OleDbCommand(qry1,Conexion);
				System.Data.OleDb.OleDbCommand Commando2 = new System.Data.OleDb.OleDbCommand(qry2,Conexion);

				Commando1.ExecuteNonQuery();
				Commando2.ExecuteNonQuery();
                if (Conexion.State == ConnectionState.Open)
                    Conexion.Dispose();
			}
			catch(Exception ex)
			{
				LError.Text = "Error "+ex.Message;
			}
			LCorrecto.Text = "Cambio de No.Empleado(TRACVE) realizado satisfactoriamente";
		}
	}
}
