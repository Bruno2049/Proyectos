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

public partial class WF_TurnoAsignacion : System.Web.UI.Page
{
    CeC_Sesion Sesion= null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Sesion = CeC_Sesion.Nuevo(this);
            if (!IsPostBack)
            {
                CbxTurnos.DataSource = CeC_Turnos.ObtenTurnosDSAgregado(Sesion.SUSCRIPCION_ID);
                CbxTurnos.DataTextField = "TURNO_NOMBRE";
                CbxTurnos.DataValueField = "TURNO_ID";
                CbxTurnos.DataBind();
                CbxTurnos.SelectedIndex = 0;
                ActualizaDatos();
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosAsignacion.Page_Load", ex);
        }
    }
    protected void ActualizaDatos()
    {
        try
        {
            DataSet DS_PersonasTurno = null;
            if (Sesion == null)
                Sesion = CeC_Sesion.Nuevo(this);
            DS_PersonasTurno = CeC_Turnos.ObtenPersonasTurno(Sesion.eClock_Turno_ID, Sesion.USUARIO_ID);
            if (DS_PersonasTurno != null)
            {
                Grid.DataSource = DS_PersonasTurno;
                Grid.DataMember = DS_PersonasTurno.Tables[0].TableName;
                Grid.DataKeyField = CeC_Campos.CampoTE_Llave;
                Grid.DisplayLayout.Pager.AllowPaging = false;
                Grid.DisplayLayout.LoadOnDemand = Infragistics.WebUI.UltraWebGrid.LoadOnDemand.NotSet;
                Grid.DataBind();
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosAsignacion.ActualizaDatos", ex);
        }
    }
    protected void BtnMover_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            int Turno_ID = Convert.ToInt32(CbxTurnos.DataValue);
            int Numero_registros = Grid.Rows.Count;

            for (int i = 0; i < Numero_registros; i++)
            {
                if (Grid.Rows[i].Selected)
                {
                    int PERSONA_LINK_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                    int PersonaID = CeC_Personas.ObtenPersonaID(PERSONA_LINK_ID);
                    CeC_Turnos.AsignaHorarioPred(PersonaID, Turno_ID, Sesion.SESION_ID);
                }
            }
            Grid.Clear();
            ActualizaDatos();
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosAsignacion.BtnMover_Click", ex);
        }
    }
    protected void BtnAgregar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            int PERSONA_LINK_ID = CeC.Convierte2Int(Wne_Persona_Link_ID.Value);
            int PersonaID = CeC_Personas.ObtenPersonaID(PERSONA_LINK_ID);
            int Turno_ID = Sesion.WF_Turnos_TURNO_ID;
            CeC_Turnos.AsignaHorarioPred(PersonaID, Turno_ID, Sesion.SESION_ID);
            Grid.Clear();
            ActualizaDatos();
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnosAsignacion.BtnAgregar_Click", ex);
        }
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid,false,false,true,false);
    }
    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }
}
