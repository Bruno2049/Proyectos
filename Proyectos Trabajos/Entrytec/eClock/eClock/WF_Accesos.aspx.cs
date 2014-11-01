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

public partial class WF_Accesos : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.WF_EmpleadosFil_OpcionAgrega("AccesosCC.jpg", "WFR_AccesoCC.aspx", "Accesos por " + CeC_Config.NombreGrupo1, "Accesos por " + CeC_Config.NombreGrupo1);
        Sesion.WF_EmpleadosFil_OpcionAgrega("AccesosT.jpg", "WFR_AccesoT.aspx", "Accesos por " + CeC_Config.NombreGrupo1, "Accesos por " + CeC_Config.NombreGrupo1);
        Sesion.WF_EmpleadosFil_OpcionAgrega("AccesosTerminal.jpg", "WFR_AccesosTerminalDetallado.aspx", "Accesos por " + CeC_Config.NombreGrupo1, "Accesos por " + CeC_Config.NombreGrupo1);
        Sesion.WF_EmpleadosFil(true, true, true, "Muestra Resultados",
           "Filtro de empleados y accesos", "WFR_AccesosTerminalDetallado.aspx",
           "Accesos detallados", true, true, false);
    }
}
