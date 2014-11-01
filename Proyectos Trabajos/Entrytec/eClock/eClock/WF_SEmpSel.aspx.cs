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
	/// Descripci�n breve de WF_SEmpSel.
	/// </summary>
	public partial class WF_SEmpSel : System.Web.UI.Page
	{
	
		CeC_Sesion Sesion;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aqu� el c�digo de usuario para inicializar la p�gina
			Sesion = CeC_Sesion.Nuevo(this);


			if(!IsPostBack)
			{
				CheckBox1.Checked = true;
                //Agregar M�dulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Empleados Seleccionados", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
			}


			Sesion.AplicarAEmpleadosSel = CheckBox1.Checked;
			

		}

		#region C�digo generado por el Dise�ador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Dise�ador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// M�todo necesario para admitir el Dise�ador. No se puede modificar
		/// el contenido del m�todo con el editor de c�digo.
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
