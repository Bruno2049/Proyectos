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

public partial class WF_ConfigAlertas : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Configuracion))
        {
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            return;
        }
        if (!IsPostBack)
        {
            Cbx_FaltasEmpleado.Checked = CeC_Config.AlertaFaltasEmpleado;
            Cbx_FaltasSupervisor.Checked = CeC_Config.AlertaFaltasSupervisor;
            Cbx_RetardosEmpleado.Checked = CeC_Config.AlertaRetardosEmpleado;
            Cbx_RetardosSupervisor.Checked = CeC_Config.AlertaRetardosSupervisor;
            Cbx_Terminal.Checked = CeC_Config.AlertaTerminalFuera;
        }
    }
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Configuracion))
        {
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            return;
        }
        CeC_Config.AlertaFaltasEmpleado = Cbx_FaltasEmpleado.Checked;
        CeC_Config.AlertaFaltasSupervisor = Cbx_FaltasSupervisor.Checked;
        CeC_Config.AlertaRetardosEmpleado = Cbx_RetardosEmpleado.Checked;
        CeC_Config.AlertaRetardosSupervisor = Cbx_RetardosSupervisor.Checked;
        CeC_Config.AlertaTerminalFuera = Cbx_Terminal.Checked;
        LOperacion.Text = "Se han guardado los datos con exito";
    }

}
