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

public partial class WF_EnviaIncidencias : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Exportación e Importación de Incidencias a su Sistema de Nómina";
        Sesion.DescripcionPagina = "Exporta e Importa Asistencias, Faltas, Retardos y Justificaciones dentro de un rango de fechas, en una base previamente configurada";

        // Permisos****************************************
        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Incidencias0Sincronizacion, true))
        {
            WebPanel2.Visible = false;
            return;
        }

        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Incidencias0Sincronizacion0Envio, true))
            btn_Enviar.Visible = false;

        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Incidencias0Sincronizacion0Recepcion, true))
            btn_Recibir.Visible = false;

        //**************************************************

        if (!IsPostBack)
        {    //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Empleados", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
            tbx_SQLEmpleados.Text = CeC_Config.IncidenciasEmpleadosSQL;
            FechaI.SelectedDate = DateTime.Today;
            FechaF.SelectedDate = DateTime.Today;
        }
    }
    protected void GuardaSQL()
    {
        CeC_Config.IncidenciasEmpleadosSQL = tbx_SQLEmpleados.Text;
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_Main.aspx");
    }
    protected void btn_Recibir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        GuardaSQL();
        if (CMd_Base.gRecibeIncidencias(FechaI.SelectedDate, FechaF.SelectedDate, CeC_Config.IncidenciasEmpleadosSQL))
            LCorrecto.Text = "Se recibieron las incidencias correctamente";
        else
            LError.Text = "No se pudieron recibir las incidencias o existio un error al intentarlo";

    }
    protected void btn_Enviar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        GuardaSQL();
        if (CMd_Base.gEnviaIncidencias(FechaI.SelectedDate, FechaF.SelectedDate,0, CeC_Config.IncidenciasEmpleadosSQL))
            LCorrecto.Text = "Se enviaron las incidencias correctamente";
        else
            LError.Text = "No se pudieron enviar las incidencias o existio un error al intentarlo";
    }
}