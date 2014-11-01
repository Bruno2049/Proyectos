namespace eClock
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    /*
	/// <summary>
	///		Descripción breve de WC_Menu.
	/// </summary>
    public partial class WC_Menu : System.Web.UI.UserControl
	{
		protected Infragistics.WebUI.UltraWebNavigator.UltraWebMenu UltraWebMenu1;
		protected System.Web.UI.WebControls.LinkButton LBRegresar;
		protected System.Web.UI.WebControls.Label LTexto;
		protected System.Web.UI.WebControls.Label LTitulo;
		protected System.Web.UI.WebControls.Label LDescripcion;
        
		CeC_Sesion Sesion;
        public string Tutulo
        {
            set
            {
                this.LTitulo.Text = value;
            }
            get
            {
                return LTitulo.Text;
            }
        }

		public string Descripcion
		{
			set
			{
				this.LDescripcion.Text = value;
			}
			get
			{
				return LDescripcion.Text;
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				Sesion = CeC_Sesion.Nuevo(this.Page);
				LTexto.Text = Sesion.USUARIO_NOMBRE  + " ("  + Sesion.USUARIO_USUARIO + ")";
			}
			catch
			{
				
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
		///		Método necesario para admitir el Diseñador. No se puede modificar
		///		el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.UltraWebMenu1.MenuItemClicked += new Infragistics.WebUI.UltraWebNavigator.MenuItemClickedEventHandler(this.UltraWebMenu1_MenuItemClicked);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LBRegresar_Click(object sender, System.EventArgs e)
		{
			CeC_Sesion Sesion = CeC_Sesion.Nuevo(this.Page);
			Sesion.Redirige(Sesion.Pagina_Anterior);
		}

		private void UltraWebMenu1_MenuItemClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemEventArgs e)
		{

			string f1 = e.Item.Text.ToString();

			if (f1=="Salir")
			{
				Sesion.USUARIO_ID = -1;
				//Sesion.SESION_ID =0 ;
				Sesion.Redirige("WF_Login.aspx");
			}		

		}
	}
      */
}
