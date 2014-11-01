using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class WF_PersonasNoRegistradas : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        LCorrecto.Text = "";
        LError.Text = "";
    }
    protected void UltraWebGrid1_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, false, false, true, false);
    }
    void ActualizaDatos()
    {
        DataSet DS = CeC_Personas.ObtenPersonasNoCreadas(Sesion.SUSCRIPCION_ID);
        Grid.DataSource = DS;
        Grid.DataKeyField = "TERMINALES_DEXTRAS_ID";
    }
    protected void UltraWebGrid1_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        ActualizaDatos();
    }
    protected void BBorrarUsuarios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

        if (Sesion.SESION_ID <= 0)
            return;

        int Numero_Resgistos = Grid.Rows.Count;
        int Errores = 0;
        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                int TERMINALES_DEXTRAS_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                if (!CeC_Personas.BorraRegistroPersonaNoCreada(TERMINALES_DEXTRAS_ID))
                    Errores++;
            }
        }
        if (Errores == 0)
            LCorrecto.Text = "Se han borrado los registros seleccionados";
        else
            LError.Text = "No se pudieron borrar " + Errores + " registros";
        ActualizaDatos();
        Grid.DataBind();
    }
    protected void WebImageButton1_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {

    }
    protected void Btn_Recalcular_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "Se calcularon " + CeC_Accesos.ProcesaAccesosViejos() + " Accesos";
    }
}
