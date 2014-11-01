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
using System.Data.OleDb;

public partial class WF_IncidenciasEx : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    DS_IncidenciasExTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter TA_IncidenciasEx = new DS_IncidenciasExTableAdapters.EC_TIPO_INCIDENCIAS_EXTableAdapter();
    DS_IncidenciasEx dS_IncidenciasEx1 = new DS_IncidenciasEx();

    private void Habilitarcontroles()
    {
        Grid.Visible = false;
        btn_Guardar.Visible = false;
        btn_IncEx.Visible = false;
        btn_IncExSis.Visible = false;
        LCorrecto.Visible = false;
        LError.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Incidencias Extra";
        Sesion.DescripcionPagina = "Cambie el Tipo de Falta correspondiente a cada Incidencia Extra";

        if (!IsPostBack)
        {
            // Permisos****************************************
            if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Incidencias0Ex, true))
            {
                Habilitarcontroles();
                return;
            }
            //**************************************************

            ActualizaDatos();

            /*****************
             Agregar ModuloLog
             *****************/
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "IncidenciasEx", 0, "Incidencias Ex", Sesion.SESION_ID);

        }
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, true, false, false, true);
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }

    /// <summary>
    /// Actualiza los datos en el Grid
    /// </summary>
    protected void ActualizaDatos()
    {
        decimal borrado = 0;
        if (chk_Borrada.Checked)
            borrado = 1;
        Sesion = CeC_Sesion.Nuevo(this);
        TA_IncidenciasEx.Fill(dS_IncidenciasEx1.EC_TIPO_INCIDENCIAS_EX, borrado);
        Grid.DataSource = dS_IncidenciasEx1.EC_TIPO_INCIDENCIAS_EX;
        Grid.DataMember = dS_IncidenciasEx1.EC_TIPO_INCIDENCIAS_EX.TableName;
        Grid.DataKeyField = "INCIDENCIAS EX";
        Grid.DataBind();
        CeC_Grid.AplicaFormato(Grid, true, false, false, true);
    }

    protected void btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
       for (int i=0; i < Grid.Rows.Count;i++)
        {
           TA_IncidenciasEx.UpdateFalta(Convert.ToDecimal(Grid.Rows[i].Cells[5].Value),Convert.ToDecimal(Grid.Rows[i].Cells[0].Value));
        }
        CeC_Grid.AplicaFormato(Grid, true, false, false, true);
    }

    protected void btn_Asignar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_IncidenciasLink.aspx");
    }

    protected void btn_IncExSis_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_IncidenciasLinkSis.aspx");
    }

    protected void chk_Borrada_CheckedChanged(object sender, EventArgs e)
    {
        ActualizaDatos();
    }
}
