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
	/// Descripci�n breve de WF_Personas_ImaS.
	/// </summary>
	public partial class WF_Personas_ImaS : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            try
            {
                CeC_Sesion Sesion;
                Sesion = CeC_Sesion.Nuevo(this);
                int PersonaId = Sesion.WF_Personas_ImaS_Persona_ID;// CeC_BD.EjecutaEscalarInt("SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID = " + Sesion.WF_Empleados_PERSONA_LINK_ID.ToString());
                byte[] Ima = CeC_Personas.ObtenFoto(PersonaId);
                if (Ima != null)
                {
                    Response.ContentType = "image/Jpeg";
                    Response.BinaryWrite(Ima);
                }
            }
            catch
            {
            }
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
