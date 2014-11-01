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

public partial class WF_ConfigVariables : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Configuración de Variables";
        Sesion.DescripcionPagina = "Elija la configuración que tendrá el sistema";
        if (Sesion.SUSCRIPCION_ID != 1 || !Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Configuracion))
        {
            CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            return;
        }
        if (!IsPostBack)
        {
            //string[] cadena= CeC_Config.NombrePersona.Split(new char[] {'(', ',', ')', ' '});
            txtNombrePersona.Text = Sesion.ConfiguraSuscripcion.NombrePersona;
            txtMensajeJscript.Text = CeC_Config.MensajeJScript;
            txtRutaPDF.Text = CeC_Config.RutaReportesPDF;
            txtRutaXLS.Text = CeC_Config.RutaReportesXLS;
//            TxtLeyendaRep.Text = CeC_Config.LEYENDA_REPORTE_ASISTENCIA;
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Configuración de Variables", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
   }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            CeC_Config.MensajeJScript = txtMensajeJscript.Text;
            CeC_Config.RutaReportesPDF = txtRutaPDF.Text;
            CeC_Config.RutaReportesXLS = txtRutaXLS.Text;
   //         CeC_Config.LEYENDA_REPORTE_ASISTENCIA = TxtLeyendaRep.Text;
            //El Usuario Siempre Escribe '+' para concatenar, asi que
            //Si es oracle, la concatenación se hará con concat en esta 
            //parte del codigo
            /*CeC_Config.NombrePersona = txtNombrePersona.Text;
            string Texto = CeC_Config.NombrePersona_QRY;
            CeC_BD.ActualizaNombresEmpleados();*/
            CeC_BD.CreaRelacionesEmpleados();
            Sesion.Redirige("WF_Main.aspx");          
        }
        catch (Exception ex)
        {
            lOperacion.Text = "No se Pudo Realizar La Operación";
        }
    }

    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
    }
}
