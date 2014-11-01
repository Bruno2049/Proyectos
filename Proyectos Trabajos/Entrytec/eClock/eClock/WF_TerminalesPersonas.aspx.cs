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

public partial class WF_TerminalesPersonas : System.Web.UI.Page
{

    protected int Terminal_ID;
    protected CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);

        Terminal_ID = CeC.Convierte2Int(Sesion.Parametros);
        lnombre.Text = CeC_Terminales.ObtenTerminalNombre(Terminal_ID);
        Lbl_Correcto.Text = "";
        Lbl_Error.Text = "";
        //Agregar Módulo Log
        if (!IsPostBack)
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asignacion de Empleados a Terminal", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);

    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, true, false, true, true);
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        if (IsPostBack)
            Page_Load(null, null);
        ActualizaDatos();
    }
    protected void ActualizaDatos()
    {
        ActualizaDatos(false);
    }
    protected void ActualizaDatos(bool LimpiarGrid)
    {
        try
        {
            if (Sesion == null)
                return;
            if (Sesion.SESION_ID <= 0)
                return;
            DataSet DS = null;
            if (LimpiarGrid)
            {
                Grid.Clear();
                Grid.DataMember = "";
            }
            DS = CeC_Terminales.ObtenPersonasDS(Terminal_ID, Sesion.SUSCRIPCION_ID);
            Grid.DataSource = DS.Tables[0];
            Grid.DataMember = DS.Tables[0].TableName;
            Grid.DataKeyField = "PERSONA_ID";
            Grid.DataBind();
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            Lbl_Error.Text = "Hubo errores al asignar los persmisos de acceso. Revise sus permisos.";
            Lbl_Correcto.Text = "";
        }
    }

 
    protected void BtnSeleccionarTodos_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (CeC_Terminales.PermisoDaTodos(Terminal_ID, Sesion.SUSCRIPCION_ID) > 0)
        {
            Grid.Clear();
            ActualizaDatos(true);
            Lbl_Correcto.Text = "Se asignaron los accesos correctamente";
            Lbl_Error.Text = "";
        }
    }


    protected void BtnDeseleccionarTodos_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (CeC_Terminales.PermisoQuitaTodos(Terminal_ID, Sesion.SUSCRIPCION_ID) > 0)
        {
            Grid.Clear();
            ActualizaDatos(true);
            Lbl_Correcto.Text = "Se quitaron los accesos correctamente";
            Lbl_Error.Text = "";
        }
    }
    protected void BtnDenegar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CambiaEstado(false);
    }
    protected void BtnPermitir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        CambiaEstado(true);
    }
    void CambiaEstado(bool PermitirAcceso)
    {
        int Numero_Resgistos = Grid.Rows.Count;
        if (Numero_Resgistos <= 0)
        {
            Lbl_Error.Text = "Debe seleccionar al menos una persona.";
            Lbl_Correcto.Text = "";
            return;
        }
        for (int i = 0; i < Numero_Resgistos; i++)
        {
            if (Grid.Rows[i].Selected)
            {
                int Persona_ID = Convert.ToInt32(Grid.Rows[i].DataKey);
                if (PermitirAcceso)
                {
                    if (CeC_Terminales.PermisoDa(Persona_ID, Terminal_ID))
                    {
                        Lbl_Correcto.Text = "Se asignaron los accesos correctamente";
                        Lbl_Error.Text = "";
                    }
                }
                else
                {
                    if (CeC_Terminales.PermisoQuita(Persona_ID, Terminal_ID))
                    {
                        Lbl_Correcto.Text = "Se quitaron los accesos correctamente";
                        Lbl_Error.Text = "";
                    }
                }

            }
        }
        Grid.Clear();
        ActualizaDatos(true);
    }
}
