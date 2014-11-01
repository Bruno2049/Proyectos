namespace eClock
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Web.UI;
	/// <summary>
	///		Descripción breve de WC_Horario.
	/// </summary>
	public partial class WC_Horario : System.Web.UI.UserControl
	{
		private int m_Dia = 0;
		public int Dia
		{
			set {m_Dia = value;}
			get{return m_Dia;}
		}
		public static Byte[] ObtenArregloBytes(string Cadena)
		{
			if (Cadena.Length < 1)
				return null;
			Byte[] Arreglo = new byte[Cadena.Length + 1];
			for (int Cont = 0; Cont < Cadena.Length; Cont++)
			{
				Arreglo[Cont] = Convert.ToByte(Cadena[Cont]);
			}
			Arreglo[Cadena.Length] = 0;
			return Arreglo;
		}
/*		public void AsignaNoDia(int Dia)
		{
			System.Web.UI.ControlCollection Control = this.Controls;
			string Datos = "<iframe id=\"IFrameHorario"+Dia.ToString()+"\" style=\"Z-INDEX: 103; WIDTH: 180px; HEIGHT: 296px\" frameBorder=\"no\" " + 
			"\"WF_Horario.aspx?Parametros=\"" + Dia.ToString() + "\" tabIndex=\"0\"></iframe>";			
			this.Response.BinaryWrite(ObtenArregloBytes(Datos));
		//	CeC_Sesion.EjecutaScript(this.,"document.getElementById(\"IFrameHorario\").src=);
		}*/
		private void Page_Load(object sender, System.EventArgs e)
		{

//			CeC_Sesion.EjecutaScript(this.Page,Datos);
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
		///		Método necesario para admitir el Diseñador. No se puede modificar
		///		el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
//			this.RenderChildren
		}
		#endregion
		protected override void Render(HtmlTextWriter output)
		{          
//			if(!this.IsPostBack)
			{
				string Datos = "<iframe id=\"IFrameHorario"+Dia.ToString()+"\" scrolling=\"no\" style=\"Z-INDEX: 103; WIDTH: 185px; HEIGHT: 296px\" frameBorder=\"no\" " + 
					"src=\"WF_Horario.aspx?Parametros=" + Dia.ToString() + "\" tabIndex=\"0\"></iframe>";
				output.Write(Datos);
				// Render Controls.
				RenderChildren(output);
			}
		}

	}
}
