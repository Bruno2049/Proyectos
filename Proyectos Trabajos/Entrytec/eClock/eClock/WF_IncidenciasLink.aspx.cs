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

public partial class WF_IncidenciasLink : System.Web.UI.Page
{
    DS_IncidenciasLink ds_IncLink;
    DS_IncidenciasLinkTableAdapters.EC_TIPO_INCIDENCIAS_EX_INCTableAdapter TA_IncLink;
    CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Incidencias Extra";
        Sesion.DescripcionPagina = "Relacione las Incidencias del Personalizadas con las Incidencias Extra";

        if (!IsPostBack)
        {
            /*******
             Permiso
             *******/
            string[] Permiso = new string[4];
            Permiso[0] = eClock.CEC_RESTRICCIONES.S0Incidencias0Link;

            if (!Sesion.Acceso(Permiso, CIT_Perfiles.Acceso(Sesion.PERFIL_ID, this)))
            {
                CIT_Perfiles.CrearVentana(this, Sesion.MensajeVentanaJScript(), Sesion.TituloPagina, "Aceptar", "WF_Main.aspx", "", "");
            }

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

    private void ActualizaDatos()
    {
        Sesion = CeC_Sesion.Nuevo(this);
        ds_IncLink = new DS_IncidenciasLink();
        TA_IncLink = new DS_IncidenciasLinkTableAdapters.EC_TIPO_INCIDENCIAS_EX_INCTableAdapter();
        TA_IncLink.Fill(ds_IncLink.EC_TIPO_INCIDENCIAS_EX_INC);
        Grid.DataSource = ds_IncLink.EC_TIPO_INCIDENCIAS_EX_INC;
        Grid.DataMember = ds_IncLink.EC_TIPO_INCIDENCIAS_EX_INC.TableName;
        Grid.DataKeyField = "TIPO_INCIDENCIA_ID";
        Grid.DataBind();
    }

    protected void btn_regresar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.Redirige("WF_IncidenciasEx.aspx");
    }

    protected void btn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        ds_IncLink = new DS_IncidenciasLink();
        TA_IncLink = new DS_IncidenciasLinkTableAdapters.EC_TIPO_INCIDENCIAS_EX_INCTableAdapter();
        try
        {
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                decimal IncID = Convert.ToDecimal(Grid.Rows[i].Cells[0].Value);
                decimal IncExID = Convert.ToDecimal(Grid.Rows[i].Cells[1].Value);
                TA_IncLink.UpdateIncEx(IncExID, IncID);
            }
            CeC_Grid.AplicaFormato(Grid, true, false, false, true);
            LCorrecto.Text = "Se han guardado los cambios correctamente";
        }
        catch
        {
            LError.Text = "Los cambios no pudieron guardarse";
        }
    }
}
