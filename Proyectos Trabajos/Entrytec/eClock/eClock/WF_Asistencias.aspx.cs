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
using Infragistics.WebUI.UltraWebNavigator;

public partial class WF_Asistencias : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Asistencia";
        Sesion.DescripcionPagina = "Seleccione un sitio para borrarlo o editarlo, o cree un sitio nuevo";
//        if (!IsPostBack)
        {
            Node Empleados = TrEmpleados.Nodes.Add("Todos");
            DataSet DS = CeC_Campos.CatalogoDT(CeC_Config.CampoGrupo1, Sesion);
            if (DS != null)
            {
                foreach (DataRow DR in DS.Tables[0].Rows)
                {

                    Node newNode = Empleados.Nodes.Add(DR[0].ToString());
                    newNode.ShowExpand = true;
                }

            }
        }
    }
    protected void ScriptManager1_Init(object sender, EventArgs e)
    {

    }
    protected void TrEmpleados_DemandLoad(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
    {
        DataSet DS = CeC_BD.ObtenPersonasYPersonaLink(CeC_Config.CampoGrupo1 + " = '" + e.Node.Text + "' ORDER BY PERSONA_NOMBRE");
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                e.Node.Nodes.Add(DR[0].ToString() + " (" + DR[1].ToString() + ")");

            }
            e.Node.Expanded = true;
        }
    }
    protected void BNuevo_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_EmpleadosEd.aspx?Parametros=Nuevo");
    }
}
