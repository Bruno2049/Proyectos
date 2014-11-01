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
	/// Descripción breve de WF_SEmpSel.
	/// </summary>
	public partial class WF_SEmpSel : System.Web.UI.Page
	{
	
		CeC_Sesion Sesion;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aquí el código de usuario para inicializar la página
			Sesion = CeC_Sesion.Nuevo(this);


			if(!IsPostBack)
			{
				CheckBox1.Checked = true;
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Empleados Seleccionados", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
			}


			Sesion.AplicarAEmpleadosSel = CheckBox1.Checked;
			

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

		}
		#endregion

		protected void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
		{
				Sesion.AplicarAEmpleadosSel = CheckBox1.Checked;
			
		}
	}
}
