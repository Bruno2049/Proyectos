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
	/// Descripción breve de WF_HuellasNAsig.
	/// </summary>
	public partial class WF_HuellasNAsig : System.Web.UI.Page
	{
		protected System.Data.OleDb.OleDbConnection Conexion;
		protected System.Data.OleDb.OleDbDataAdapter DA_HuellasNAsig;
		protected System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		protected DS_HuellasNAsig dS_HuellasNAsig1;
		CeC_Sesion Sesion;
		private void ActualizaGrid()
		{
			dS_HuellasNAsig1.Clear();
			DA_HuellasNAsig.Fill(dS_HuellasNAsig1);
			Grid.DataBind();
		}
		private void LimpiaMsg()
		{
			LError.Text = "";
			LCorrecto.Text = "";
		}

		private void Habilitarcontroles()
		{
				Grid.Visible = false;
				Webimagebutton2.Visible = false;
				Webimagebutton3.Visible = false;
				BDeshacerCambios.Visible = false;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Sesion = CeC_Sesion.Nuevo(this);
			Sesion.TituloPagina = "Huellas no asignadas";
			Sesion.DescripcionPagina = "Se muestran las checadas que no tienen correspondencia con algun empleado dependiendo del campo llave de la terminal."
                +" Seleccione las huellas y presione 'Borrar Seleccionadas' para borrar las huellas";

            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(CEC_RESTRICCIONES.S0HuellaNoAsignada, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

			if(!IsPostBack)
			{
				ActualizaGrid();
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Huellas No Asignadas", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
			}
			// Introducir aquí el código de usuario para inicializar la página
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
			this.DA_HuellasNAsig = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.dS_HuellasNAsig1 = new DS_HuellasNAsig();
			((System.ComponentModel.ISupportInitialize)(this.dS_HuellasNAsig1)).BeginInit();
			this.Webimagebutton2.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Webimagebutton2_Click);
			this.Webimagebutton3.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.Webimagebutton3_Click);
			this.BDeshacerCambios.Click += new Infragistics.WebUI.WebDataInput.ClickHandler(this.BDeshacerCambios_Click);
			// 
			// Conexion
			// 
			this.Conexion.ConnectionString = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
			// 
			// DA_HuellasNAsig
			// 
			this.DA_HuellasNAsig.SelectCommand = this.oleDbSelectCommand1;
			this.DA_HuellasNAsig.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "EC_PERSONAS", new System.Data.Common.DataColumnMapping[] {
																																																					  new System.Data.Common.DataColumnMapping("QUITAR", "QUITAR"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_ID", "PERSONA_ID"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_LINK_ID", "PERSONA_LINK_ID"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_NOMBRE", "PERSONA_NOMBRE"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_S_HUELLA_FECHA", "PERSONA_S_HUELLA_FECHA"),
																																																					  new System.Data.Common.DataColumnMapping("PERSONA_EMAIL", "PERSONA_EMAIL")})});
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT 0 AS QUITAR, TERMINALES_DEXTRAS_TEXTO1 FROM EC_TERMINALES_DEXTRAS WHERE (" +
				"TIPO_TERM_DEXTRAS_ID = 2) GROUP BY TERMINALES_DEXTRAS_TEXTO1 ORDER BY TERMINALES" +
				"_DEXTRAS_TEXTO1";
			this.oleDbSelectCommand1.Connection = this.Conexion;
			// 
			// dS_HuellasNAsig1
			// 
			this.dS_HuellasNAsig1.DataSetName = "DS_HuellasNAsig";
			this.dS_HuellasNAsig1.Locale = new System.Globalization.CultureInfo("es-MX");
			((System.ComponentModel.ISupportInitialize)(this.dS_HuellasNAsig1)).EndInit();

		}
		#endregion

		private void Webimagebutton3_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			CheckTodos(true);
		}

		private void Webimagebutton2_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			CheckTodos(false);
		}
		void CheckTodos(bool Check)
		{
			for(int Cont = 0; Cont < Grid.Rows.Count;Cont ++)
			{
				if(Check)
					Grid.Rows[Cont].Cells[0].Value = 1;
				else
					Grid.Rows[Cont].Cells[0].Value = 0;
			}
		}

		private void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
		{
			int Borrados = 0;
			int NBorrados = 0;
			string TBorrados = "";
			string TNBorrados = "";
			LimpiaMsg();

			for(int Cont = 0; Cont < Grid.Rows.Count;Cont ++)
			{
				if(Convert.ToInt32(Grid.Rows[Cont].Cells[0].Value) != 0)
				{
					string Texto = Convert.ToString(Grid.Rows[Cont].Cells[1].Value) ;
					string Completo =  Texto;
					if(CeC_BD.EjecutaComando("DELETE EC_TERMINALES_DEXTRAS where TIPO_TERM_DEXTRAS_ID = 2 AND TERMINALES_DEXTRAS_TEXTO1 = '" + Texto +"'")<= 0)
					{
						TNBorrados += Completo + "\n";
						NBorrados ++;
					}
					else
					{
						TBorrados += Completo + "\n";
						Borrados ++;
					}
				}
			}
			if (NBorrados > 0)
			{
				LError.Text = "No se pudieron quitar los siguientes empleados de la lista: \n" + TNBorrados;
			}
			if (Borrados > 0)
			{
				LCorrecto.Text = "\nSe quitaron los siguientes empleados de la lista \n" + TBorrados;
			}
			ActualizaGrid();
		}

        protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(Grid);
        }
    }
}
