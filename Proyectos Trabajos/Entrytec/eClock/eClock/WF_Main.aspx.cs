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
	/// Descripción breve de WF_Main.
	/// </summary>
	public partial class WF_Main : System.Web.UI.Page
	{
		CeC_Sesion Sesion;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aquí el código de usuario para inicializar la página
            return;
			Sesion = CeC_Sesion.Nuevo(this);
			Sesion.WF_EmpleadosFil_Qry = "";
            Sesion.TituloPagina = "";
            Sesion.DescripcionPagina = "";
			//this.RegisterStartupScript("PRueba","<script language=JavaScript> theObjects = document.getElementsByTagName(\"object\"); for (var i = 0; i < theObjects.length; i++) { theObjects[i].outerHTML = theObjects[i].outerHTML; }</script>");
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Página Principal", 0, "Página Principal", Sesion.SESION_ID);
            this.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>CargarImagenes();</script>");
/*            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Nuevo))
            {
                MenuEmpleados.Items[0].Items[0].Hidden = true;
                MenuEmpleados.Items[0].Items[3].Hidden = true;
            }*/
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

        protected void MenuEmpleados_MenuItemClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemEventArgs e)
        {
            Sesion.Redirige("WF_EmpleadosN.aspx");
        }
        protected void MenuTurnos_MenuItemClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemEventArgs e)
        {
            Sesion.Redirige("WF_TurnosAsignacionExpress.aspx");
        }
        protected void MenuAsistencia_MenuItemClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemEventArgs e)
        {
            Sesion.Redirige("WF_PersonasSemana.aspx?Parametros=Asistencia");
        }
        protected void MenuReportes_MenuItemClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebMenuItemEventArgs e)
        {
            Sesion.Redirige("WFR_ComidasTiempoDetallado.aspx");
        }
}
}
