using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_SolicitudNueva : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    string PorInsertar;
    string PersonasDiariosIds;
    int TipoIncidenciaID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        
        TipoIncidenciaID = CeC.Convierte2Int(CeC.ObtenColumnaSeparador(Sesion.Parametros, "@", 0));
        PersonasDiariosIds = CeC.Convierte2String(CeC.ObtenColumnaSeparador(Sesion.Parametros, "@", 1));

        if (!IsPostBack)
        {
            Lbl_TipoIncidencia.Text = Cec_Incidencias.ObtenTipoIncidenciaNombre(TipoIncidenciaID);
            Lbl_PerFechas.Text = CeC_Asistencias.ObtenTexto(PersonasDiariosIds);
//            Lbl_Html.Text = CeC_IncidenciasRegla.ObtenHTML(TipoIncidenciaRID, PersonasDiariosIds);
        }
    }
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LError.Text = "";
        LCorrecto.Text = "";
        //Sesion.Redirige("WF_AsistenciasEmp.aspx?Parametros=REGRESO");
        if (Lbl_Html.Text.IndexOf("Saldo Insuficiente") > 0)
        {
            LError.Text = "No se puede guardar porque hay saldo insuficiente";
            return;
        }
        if (CeC_Solicitudes.AgregaJustificacion(TipoIncidenciaID, PersonasDiariosIds, Tbx_Comentario.Text, Sesion))
        {
            LCorrecto.Text = "Solicitud realizada satisfactoriamente";
            return;
        }
        LError.Text = "No se puedo solicitar la justificacion";
        return;
    }
    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_AsistenciasEmp.aspx?Parametros=REGRESO");
    }
}
