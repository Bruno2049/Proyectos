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
	/// Descripción breve de WF_Horario.
	/// </summary>
	public partial class WF_Horario : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebDataInput.WebImageButton WebImageButton3;
		protected Infragistics.WebUI.WebDataInput.WebImageButton WebImageButton1;
		protected Infragistics.WebUI.WebCombo.WebCombo ModeloTerminalId;
		protected Infragistics.WebUI.WebDataInput.WebImageButton WebImageButton2;
		CeC_Sesion Sesion;
		DS_HorarioPersona ds_HorarioPersona;
		string Parametros;
		private int ID = 0;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Sesion = CeC_Sesion.Nuevo(this);
			ds_HorarioPersona = (DS_HorarioPersona)Sesion.WF_HorarioPersona_Datos;
			 Parametros = Sesion.Parametros;
			if(Parametros.Length > 0)
			{
				try
				{
					ID = Convert.ToInt32(Parametros);
				}
				catch
				{
				}
			}
			if(!IsPostBack)
			{	
				try
				{
					if(ds_HorarioPersona.HorarioPersona[ID].IsTURNO_DIA_HBLOQUENull())
						CTipoTurno.SelectedIndex = 0;
					else
						CTipoTurno.SelectedIndex = 1;
					CargaHijo();
					LFecha.Text = ds_HorarioPersona.HorarioPersona[ID].PERSONA_DIARIO_FECHA.ToString("dddd dd");
				}
				catch
				{
				}
                //Agregar Módulo Log
                Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Horarios", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
			}
		}

		public void CargaHijo()
		{
			string Comando = "<script language=JavaScript> " +"document.getElementById(\"IFrameTurno\").src=\"WF_HorarioDia.aspx?Parametros=" + Parametros + "\";"+"  </script>";
			this.RegisterStartupScript("Jajj",Comando);
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
			this.CTipoTurno.SelectedRowChanged += new Infragistics.WebUI.WebCombo.SelectedRowChangedEventHandler(this.CTipoTurno_SelectedRowChanged);

		}
		#endregion

		private void CTipoTurno_SelectedRowChanged(object sender, Infragistics.WebUI.WebCombo.SelectedRowChangedEventArgs e)
		{
			if(CTipoTurno.SelectedIndex > 0)
			{
				ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HBLOQUE = CeC_BD.FechaNula.AddMinutes(30);
			}
			else
			{
				ds_HorarioPersona.HorarioPersona[ID].SetTURNO_DIA_HBLOQUENull();
				//ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HBLOQUE = DBNull.Value;
			}
			CargaHijo();
		}

        protected void CTipoTurno_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
        {
            CeC_Grid.AplicaFormato(CTipoTurno);
        }
    }
}