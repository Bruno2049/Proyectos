using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class WF_TareasPendientes : System.Web.UI.Page
{
    CeC_Sesion Sesion = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        Sesion = CeC_Sesion.Nuevo(this);
        if(!IsPostBack)
        {
            if (CeC_Sitios.ObtenTerminalesNo(Sesion.USUARIO_ID) > 0)
                Lnk_SinTerminal.Visible = false;
            if (CeC_Personas.ObtenPersonasNO(Sesion.SUSCRIPCION_ID) > 0)
                Lnk_EmpleadosImportar.Visible = false;
            if (CeC_Personas.ObtenPersonasNoSinRegistro(Sesion.SUSCRIPCION_ID) <= 0)
                Lnk_EmpleadosEnTerminal.Visible = false;
            if (CeC_Turnos.ObtenCantidadTurnos(Sesion.SUSCRIPCION_ID, false) < 1)
                CeC_Turnos.CreaTurnosPredeterminados(Sesion.USUARIO_ID, Sesion.SUSCRIPCION_ID);
            if (CeC_Personas.ObtenSinTurnoNo(Sesion.USUARIO_ID) <= 0)
                Lnk_EmpleadosSinTurno.Visible = false;
            if (CeC_AsistenciasHE.ObtenNoHorasExtrasXAplicar(Sesion.SUSCRIPCION_ID,Sesion.USUARIO_ID) < 1)
                Lnk_EmpleadosHorasExtras.Visible = false;
            if (CeC_Solicitudes.ObtenNoPorAutorizar(Sesion.USUARIO_ID) <= 0)
                Lnk_SolicitudesPendientes.Visible = false;
        }
    }
}
