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

public partial class WF_Wizardc : System.Web.UI.Page
{
    CeC_Sesion Sesion;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Asistente de Configuracion";
        Sesion.DescripcionPagina = "Agrupacion Principal";
        if (!IsPostBack)
        {

            CeC_Grid.AsignaCatalogo(WCGpo1, "CAMPO_NOMBRE");
            CeC_Grid.SeleccionaID(WCGpo1, CeC_Config.CampoGrupo1);
            CeC_Grid.AplicaFormato(WCGpo1);
            string anterior;
            anterior = WCGpo1.DisplayValue;
            this.Master.FindControl("WC_Menu1").FindControl("mnu_Main").Visible = !Convert.ToBoolean(Sesion.EsWizard);
            this.Master.FindControl("WCBotonesEncabezado1").Visible = !Convert.ToBoolean(Sesion.EsWizard);
            CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CATALOGO_ID = 1 WHERE CAMPO_ETIQUETA = '" + anterior + "'");
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asistente de Configuración", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }

    
protected void  BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
{
    
    CeC_Config.CampoGrupo1 = WCGpo1.DataValue.ToString();
    CeC_Config.NombreGrupo1 = WCGpo1.DisplayValue;// WCGpo1.SelectedRow.Cells[1].ToString();
    CeC_Campos Campos = new CeC_Campos();
    if (WCGpo1.DisplayValue != null)
    {
        CeC_BD.EjecutaComando("UPDATE EC_CAMPOS SET CATALOGO_ID = 2 WHERE CAMPO_ETIQUETA = '" + WCGpo1.DisplayValue + "'");
        CeC_BD.CreaRelacionesEmpleados();

        Sesion.Redirige("WF_Wizardd.aspx");
    }
}
    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CeC_Grid.SeleccionaID(WCGpo1, CeC_Config.CampoGrupo1);
        
    }
}
