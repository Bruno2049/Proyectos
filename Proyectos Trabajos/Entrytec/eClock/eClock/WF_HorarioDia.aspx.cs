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
	/// Descripción breve de WF_HorarioDia.
	/// </summary>
	public partial class WF_HorarioDia : System.Web.UI.Page
	{
		CeC_Sesion Sesion;

		DS_HorarioPersona ds_HorarioPersona;
		private int ID_PersonaDD = -1; 

		private void CfgTipoTurno(bool EsFlexTime)
		{
			if(!EsFlexTime)
			{
				LBloque.Visible = false;
				LHTrabajo.Visible = false;
				EBloque.Visible = false;
				EHTrabajo.Visible = false;
			}
			else
			{
				LRetardo.Text = "Entrada M";
				LEMax.Visible = false;
				EEMax.Visible =false;
				LSalida.Text = "Tol. Bloque";
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Sesion = CeC_Sesion.Nuevo(this);
			ds_HorarioPersona = (DS_HorarioPersona)Sesion.WF_HorarioPersona_Datos;
			string Parametros = Sesion.Parametros;
			if(Parametros.Length > 0)
			{
				int ID = Convert.ToInt32(Parametros);
				ID_PersonaDD = ID;
				if(!IsPostBack)
				{	
					try
					{
						if(!ds_HorarioPersona.HorarioPersona[ID].IsTURNO_DIA_HBLOQUENull() && ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HBLOQUE.TimeOfDay.TotalMinutes >0)
							CfgTipoTurno(true);
						else
							CfgTipoTurno(false);
						EEntrada.Date = ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HE;
						ERetardo.Date = ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HERETARDO;
						EEMax.Date = ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HEMAX;
						if(!ds_HorarioPersona.HorarioPersona[ID].IsTURNO_DIA_HCSNull())
							EComida.Date = ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HCS;
						if(!ds_HorarioPersona.HorarioPersona[ID].IsTURNO_DIA_HCRNull())
							ERegreso.Date = ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HCR;
						ESalida.Date = ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HS;

						EHTrabajo.Date =ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HTIEMPO;
						EBloque.Date = ds_HorarioPersona.HorarioPersona[ID].TURNO_DIA_HBLOQUE;
						//				LFecha.Text  +=Comando;
                        //Agregar Módulo Log
                        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Horario por Día", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
					}
					catch
					{
					}
				}
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
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{    
			this.EEntrada.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.EEntrada_ValueChange);
			this.ERetardo.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.ERetardo_ValueChange);
			this.EEMax.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.EEMax_ValueChange);
			this.EComida.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.EComida_ValueChange);
			this.ERegreso.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.ERegreso_ValueChange);
			this.ESalida.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.ESalida_ValueChange);
			this.EBloque.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.EBloque_ValueChange);
			this.EHTrabajo.ValueChange += new Infragistics.WebUI.WebDataInput.ValueChangeHandler(this.EHTrabajo_ValueChange);

		}
		#endregion

		private void EEntrada_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			ds_HorarioPersona.HorarioPersona[ID_PersonaDD].TURNO_DIA_HE = EEntrada.Date;
			Sesion.DiArreglo[ID_PersonaDD] = 1;
			AsignacionValor(ID_PersonaDD,1);
		}

		private void ERetardo_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			ds_HorarioPersona.HorarioPersona[ID_PersonaDD].TURNO_DIA_HERETARDO = ERetardo.Date;
			Sesion.DiArreglo[ID_PersonaDD] = 1;
			AsignacionValor(ID_PersonaDD,1);
		}

		private void EEMax_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			ds_HorarioPersona.HorarioPersona[ID_PersonaDD].TURNO_DIA_HEMAX = EEMax.Date;
			Sesion.DiArreglo[ID_PersonaDD] = 1;
			AsignacionValor(ID_PersonaDD,1);
		}

		private void EComida_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			ds_HorarioPersona.HorarioPersona[ID_PersonaDD].TURNO_DIA_HCS = EComida.Date;
			Sesion.DiArreglo[ID_PersonaDD] = 1;
			AsignacionValor(ID_PersonaDD,1);
		}

		private void ERegreso_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			ds_HorarioPersona.HorarioPersona[ID_PersonaDD].TURNO_DIA_HCR = ERegreso.Date;
			Sesion.DiArreglo[ID_PersonaDD] = 1;
			AsignacionValor(ID_PersonaDD,1);
		}

		private void ESalida_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			ds_HorarioPersona.HorarioPersona[ID_PersonaDD].TURNO_DIA_HS = ESalida.Date;
			Sesion.DiArreglo[ID_PersonaDD] = 1;
			AsignacionValor(ID_PersonaDD,1);
		}

		private void EBloque_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			ds_HorarioPersona.HorarioPersona[ID_PersonaDD].TURNO_DIA_HBLOQUE = EBloque.Date;
			Sesion.DiArreglo[ID_PersonaDD] = 1;
			AsignacionValor(ID_PersonaDD,1);
		}

		private void EHTrabajo_ValueChange(object sender, Infragistics.WebUI.WebDataInput.ValueChangeEventArgs e)
		{
			ds_HorarioPersona.HorarioPersona[ID_PersonaDD].TURNO_DIA_HTIEMPO = EHTrabajo.Date;
			Sesion.DiArreglo[ID_PersonaDD] = 1;
			AsignacionValor(ID_PersonaDD,1);
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
		}
		
		private void AsignacionValor(int Dia, int Valor)
		{
            switch (Dia)
            {
                case 0:
                    Sesion.Dia0 = Valor.ToString();
                    break;
                case 1:
                    Sesion.Dia1 = Valor.ToString();
                    break;
                case 2:
                    Sesion.Dia2 = Valor.ToString();
                    break;
                case 3:
                    Sesion.Dia3 = Valor.ToString();
                    break;
                case 4:
                    Sesion.Dia4 = Valor.ToString();
                    break;
                case 5:
                    Sesion.Dia5 = Valor.ToString();
                    break;
                case 6:
                    Sesion.Dia6 = Valor.ToString();
                    break;
            }
		}
	}
}
