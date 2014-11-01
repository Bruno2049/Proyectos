using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace eClock
{
	/// <summary>
	///		Descripción breve de WC_Menu.
	/// </summary>
    public partial class WC_Menu : System.Web.UI.UserControl
	{
		//protected Infragistics.WebUI.UltraWebNavigator.UltraWebMenu UltraWebMenu1;
//		protected System.Web.UI.WebControls.LinkButton LBRegresar;
	/*	protected System.Web.UI.WebControls.Label LTexto;
		protected System.Web.UI.WebControls.Label LTitulo;
		protected System.Web.UI.WebControls.Label LDescripcion;*/
		CeC_Sesion Sesion;

/*		public string Tutulo
		{
			set
			{
		//		this.LTitulo.Text = value;
                
			}
			get
			{
		//		return LTitulo.Text;
			}
		}
		public string Descripcion
		{
			set
			{
		//		this.LDescripcion.Text = value;
			}
			get
			{
		//		return LDescripcion.Text;

 * }*/
		//}

/*        public string DT
        {
            set
            {


//                this.NamingContainer.Site.Name = value;
            }
            get
            {
//                return this.NamingContainer.Site.Name.ToString();
            }
        }*/
        string Grupo1 = "-1";
        string Grupo2 = "-1";
        string Grupo3 = "-1";
        int NoGrupos = -1;
        private void CambiaNombresAgrup(Infragistics.WebUI.UltraWebNavigator.Items Elementos)
        {
            if (Elementos == null)
                return;
            if (Grupo1 == "-1")
                Grupo1 = CeC_Config.NombreGrupo1;
            if (Grupo2 == "-1")
                Grupo2 = CeC_Config.NombreGrupo2;
            if (Grupo3 == "-1")
                Grupo3 = CeC_Config.NombreGrupo3;
            if (NoGrupos == -1)
                NoGrupos = CeC_Config.CampoGrupoSeleccionado;
            foreach (Infragistics.WebUI.UltraWebNavigator.Item Elemento in Elementos)
            {
                if (Elemento.Text.IndexOf("GRUPO1") >= 0 && NoGrupos < 1)
                    Elemento.Hidden = true;
                if (Elemento.Text.IndexOf("GRUPO2") >= 0 && NoGrupos < 2)
                    Elemento.Hidden = true;
                if (Elemento.Text.IndexOf("GRUPO3") >= 0 && NoGrupos < 3)
                    Elemento.Hidden = true;
                Elemento.Text = Elemento.Text.Replace("GRUPO1", Grupo1);
                Elemento.Text = Elemento.Text.Replace("GRUPO2", Grupo2);
                Elemento.Text = Elemento.Text.Replace("GRUPO3", Grupo3);
                CambiaNombresAgrup(Elemento.Items);
            }
        }
        private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
                
              
				Sesion = CeC_Sesion.Nuevo(this.Page);
                CambiaNombresAgrup(mnu_Main.Items);
	//			LTexto.Text = Sesion.USUARIO_NOMBRE  + " ("  + Sesion.USUARIO_USUARIO + ")";
     //           Tutulo = Sesion.TituloPagina;
      //          Descripcion = Sesion.DescripcionPagina;
       //         DT = Sesion.TituloPagina;
                //this.NamingContainer.Site.Name.ToString() = value;
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
	/*		this.UltraWebMenu1.MenuItemClicked += new Infragistics.WebUI.UltraWebNavigator.MenuItemClickedEventHandler(this.UltraWebMenu1_MenuItemClicked);
			this.Load += new System.EventHandler(this.Page_Load);
            */
		}
		#endregion

		private void LBRegresar_Click(object sender, System.EventArgs e)
		{
		//	CeC_Sesion Sesion = CeC_Sesion.Nuevo(this.Page);
	//		Sesion.Redirige(Sesion.Pagina_Anterior);
		}

		private void UltraWebMenu1_MenuItemClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemEventArgs e)
		{

	//		string f1 = e.Item.Text.ToString();

//			if (f1=="Salir")
			{
	//			Sesion.USUARIO_ID = -1;
				//Sesion.SESION_ID =0 ;
//				Sesion.Redirige("WF_Login.aspx");
			}		

		}


        protected void mnu_Main_Init(object sender, EventArgs e)
        {
            CambiaNombresAgrup(mnu_Main.Items);
        }
}
}
