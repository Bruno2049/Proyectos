using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WF_JustificaDias : System.Web.UI.Page
{
    protected CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.Parametros.Length > 0)
           Sesion.WF_JustificaDiasParametros = Sesion.Parametros;
        Sesion.WF_EmpleadosBus_Link = "WF_Personas_Diario.aspx";
        if (!IsPostBack)
        {
            Cbx_TipoIncidencia.DataSource = Cec_Incidencias.ObtenTiposIncidenciasMenu(Sesion.SUSCRIPCION_ID);
        }

    }

    protected void Cbx_TipoIncidencia_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Cbx_TipoIncidencia);
    }
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

        LError.Text = "";
        LCorrecto.Text = "";
        try
        {
            int Valor = Convert.ToInt32(Cbx_TipoIncidencia.DataValue);
            int IncidenciaID = Cec_Incidencias.CreaIncidencia(Valor, txt_TipoIncidenciaMotivo.Text, Sesion.SESION_ID);
            if (IncidenciaID <= 0)
            {
                LError.Text = "No se pudo crear la incidencia";
                return;
            }
            string Errores = "";
            string[] Elementos = Sesion.WF_JustificaDiasParametros.Split(new char[] { '@' });
            for (int Cont = 0; Cont < Elementos.Length; Cont++)
            {
                string[] Valores = Elementos[Cont].Split(new char[] { '-' });
                if (Valores[0] == "")
                    continue;
                int Persona_Link_ID = Convert.ToInt32(Valores[0]);
                string CampoNombre = Valores[1];
                WS_PersonasSemana PS = new WS_PersonasSemana();
                int NoDias = PS.ObtenNoDiasDif(CampoNombre);
                if (NoDias < 0)
                {
                    Errores += " No se pudo justificar a " + Persona_Link_ID + " (error de fecha)\n";
                    continue;
                }
                int PersonaID = CeC_Personas.ObtenPersonaID(Persona_Link_ID, Sesion.USUARIO_ID);
                if (PersonaID < 0)
                {
                    Errores += " No se pudo justificar a " + Persona_Link_ID + " (persona no existe)\n";
                    continue;
                }
                DateTime FechaInc = Sesion.WF_EmpleadosFil_FechaI.AddDays(NoDias);
                if (Cec_Incidencias.AsignaIncidencia(FechaInc, FechaInc, PersonaID, IncidenciaID,Sesion.SESION_ID) <= 0)
                {
                    Errores += " No se pudo justificar a " + Persona_Link_ID + " (error al asignar la incidencia)\n";
                    continue;
                }
            }
            LError.Text = Errores;
            if (Errores.Length <= 0)
                LCorrecto.Text = "Incidencia guardadas apropiadamente";
        }
        catch (Exception Ex)
        {
            CIsLog2.AgregaError(Ex);
            LError.Text = "Ocurrio un error incontrolado vea los archivos de log";
        }
    }
}
