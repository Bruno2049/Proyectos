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

public partial class WF_ExportarIncidenciasAdam : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);

    }
    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "";
        LError.Text = "";

        //        if (CMd_Base.gEnviaIncidencias(new DateTime(2011, 07, 18), new DateTime(2011, 07, 24), 11724, "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID IN(13287,11666,13378,13215,11828,10574) AND SUSCRIPCION_ID = 2"))
        /*if (CMd_Base.gEnviaIncidencias(new DateTime(2011, 07, 18), new DateTime(2011, 07, 24), 11724, "SELECT PERSONA_ID FROM EC_PERSONAS WHERE TIPO_NOMINA ='T1' AND SUSCRIPCION_ID = 2"))
        {
            LCorrecto.Text = "Se han enviado las incidencias con éxito";
            return;
        }
        return;
        */

        int Numero_registros = Grid.Rows.Count;

        for (int i = 0; i < Numero_registros; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                try
                {
                    int Periodo_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    DateTime Inicio = Convert.ToDateTime(Grid.Rows[i].Cells.FromKey("PERIODO_ASIS_INICIO").Value);
                    DateTime Fin = Convert.ToDateTime(Grid.Rows[i].Cells.FromKey("PERIODO_ASIS_FIN").Value);
                    string PERIODO_N_ID = CeC.Convierte2String(Grid.Rows[i].Cells.FromKey("PERIODO_N_ID").Value);
                    string FiltroEmpleados = "SELECT EC_PERSONAS.PERSONA_ID FROM EC_PERSONAS_DATOS, EC_PERSONAS WHERE EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID AND PERSONA_BORRADO = 0 AND TIPO_NOMINA IN (SELECT TIPO_NOMINA_NOMBRE FROM EC_TIPO_NOMINA WHERE PERIODO_N_ID = " + PERIODO_N_ID + " ) AND SUSCRIPCION_ID = " + Sesion.SUSCRIPCION_ID;
                    if (CeC_Periodos.ObtenEstado(Periodo_ID) == CeC_Periodos.EDO_PERIODO.Cerrado)
                    {
                        LCorrecto.Text = "Periodo Procesado";
                        return;
                    }
                    if (CMd_Base.gEnviaIncidencias(Inicio, Fin, Periodo_ID, FiltroEmpleados))
                    {
                        LCorrecto.Text = "Se han enviado las incidencias con exito";
                        return;
                    }
                    LError.Text = "No se pudieron enviar las incidencias";
                    return;
                }
                catch (Exception ex)
                {
                    LError.Text = "Error : " + ex.Message;
                    return;
                }
            }
        }
        LError.Text = "Debes de seleccionar una fila";

    }
    protected void BDeshacerCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

    }
    protected void ActualizaDatos()
    {
        try
        {
            Sesion = CeC_Sesion.Nuevo(this);
            DataSet DS = CeC_Periodos_N.ObtenPeriodosDetalle(DateTime.Now.AddMonths(-4), DateTime.Now, 0, Sesion.SUSCRIPCION_ID);
            Grid.DataSource = DS;
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = "PERIODO_ID";
        }
        catch { }
    }
    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }
    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, false, false, false, false);
    }
    protected void Btn_Actualizar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CMd_Base.gActualizaTiposNomina(Sesion.SUSCRIPCION_ID);
    }
}
