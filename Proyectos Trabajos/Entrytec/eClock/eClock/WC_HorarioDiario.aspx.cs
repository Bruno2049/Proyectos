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
	/// Descripci�n breve de WC_HorarioDiario.
	/// </summary>
	public partial class WC_HorarioDiario : System.Web.UI.Page
	{
	/*	protected Infragistics.WebUI.WebDataInput.WebDateTimeEdit WebDateTimeEdit1;
		protected System.Web.UI.WebControls.Label Label2;
		protected Infragistics.WebUI.WebDataInput.WebDateTimeEdit WebDateTimeEdit2;
		protected System.Web.UI.WebControls.Label Label3;
		protected Infragistics.WebUI.WebDataInput.WebDateTimeEdit WebDateTimeEdit3;
		protected System.Web.UI.WebControls.Label Label4;
		protected Infragistics.WebUI.WebDataInput.WebDateTimeEdit WebDateTimeEdit4;
		protected System.Web.UI.WebControls.Label Label5;
		protected Infragistics.WebUI.WebDataInput.WebDateTimeEdit WebDateTimeEdit5;
		protected System.Web.UI.WebControls.Label Label7;
		protected Infragistics.WebUI.WebDataInput.WebDateTimeEdit WebDateTimeEdit7;
		protected System.Web.UI.WebControls.Label Label1;
		
	*/
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aqu� el c�digo de usuario para inicializar la p�gina
			
			


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
			this.WebDateTimeEdit7.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.WebDateTimeEdit7_ValueChange);
			this.WebDateTimeEdit2.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.WebDateTimeEdit2_ValueChange);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void WebDateTimeEdit7_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			
		}

		private void WebDateTimeEdit2_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
		
		}
	}
}
