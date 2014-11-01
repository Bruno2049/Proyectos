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

public partial class WF_OtrosProductos : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);

        Sesion.TituloPagina = "Asistente de Configuracion";
        Sesion.DescripcionPagina = "Otros productos de EntryTec";
        try
        {
            this.Master.FindControl("WC_Menu1").FindControl("mnu_Main").Visible = !Convert.ToBoolean(Sesion.EsWizard);
            this.Master.FindControl("WCBotonesEncabezado1").Visible = !Convert.ToBoolean(Sesion.EsWizard);
        }
        catch { }
/*        IsProtectServer.CeT_PS PS = new IsProtectServer.CeT_PS();
        PS.Directorio = HttpRuntime.AppDomainAppPath;
        int version = Convert.ToInt32(PS.LlaveProducto.Substring(3, 1));
        if (version != 1)
            btn_BuscarTerminales.Visible = false;*/
        //Agregar Módulo Log
        Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Otros Productos", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
    }


    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CeC_Config.MostrarWizardInicio = false;
        Sesion.Redirige("WF_LogIn.aspx");
    }
    protected void btn_BuscarTerminales_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CeC_BD.EjecutaComando("UPDATE EC_TERMINALES SET TERMINAL_BORRADO = 1 WHERE TERMINAL_ID = 1");
        Sesion.Redirige("eClockDesc.application?SESION_ID=" + Sesion.SESION_ID + "&BUSCAR=1");

    }
}
