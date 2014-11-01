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
	/// Descripci�n breve de WF_PaginaReporte.
	/// </summary>
	public partial class WF_PaginaReporte : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			CeC_Sesion Sesion = CeC_Sesion.Nuevo(this);
            if (Sesion.Parametros.Length <= 0)
            {
                ShowPdf1.FilePath = "~\\" + Sesion.ArchivoReporte;
                Sesion.TituloPagina = "Reporte";
                Sesion.DescripcionPagina = "En la parte inferior se muestra el reporte de acuerdo a los datos especificados.";
            }
            else
            {
                Sesion.Redirige(Sesion.ArchivoReporte);
            }
			//Sesion.Redirige(Sesion.ArchivoReporte);
            //Agregar M�dulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Reporte", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
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
	}
}
