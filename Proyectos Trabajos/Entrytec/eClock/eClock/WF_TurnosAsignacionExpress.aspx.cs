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

public partial class WF_AsignacionTurnosExpress : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    DS_TurnosTableAdapters.EC_ASIGNACIONEXPRESSTableAdapter TATurnos = new DS_TurnosTableAdapters.EC_ASIGNACIONEXPRESSTableAdapter();
    DS_Turnos DSTurnos = new DS_Turnos();
    DS_AsignacionTurnosExpress DS = new DS_AsignacionTurnosExpress();
    DS_AsignacionTurnosExpressTableAdapters.TurnosExpressDatatableTableAdapter TA = new DS_AsignacionTurnosExpressTableAdapters.TurnosExpressDatatableTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        //// Permisos****************************************
        //if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Turnos))
        //{
        //    if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Turnos0Asignacion))
        //    {
        //        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Empleados0Editar_Turnos_Anuales, true))
        //        {
        //            WIBtn_Guardar.Visible = Wdg_EC_PERSONAS.Visible = Wpn_EC_TURNOS_.Visible = false;
        //            return;
        //        }
        //        if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Turnos0Asignacion0Express, true))
        //        {
        //            WIBtn_Guardar.Visible = Wdg_EC_PERSONAS.Visible = Wpn_EC_TURNOS_.Visible = false;
        //            return;
        //        }
        //    }
        //}
        ////**************************************************

        if (Sesion.WF_TurnosAsignacionExpressQry.Length > 0)
        {
            if (Sesion.WF_EmpleadosFil_Qry != "")
                Sesion.WF_TurnosAsignacionExpressQry = Sesion.WF_EmpleadosFil_Qry_Temp = Sesion.WF_EmpleadosFil_Qry;
            //  Sesion.WF_EmpleadosFil_Qry = "";
            TA.ActualizaIn("AND PERSONA_ID IN(" + Sesion.WF_TurnosAsignacionExpressQry + ") AND (EC_PERSONAS.SUSCRIPCION_ID IN (SELECT EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Sesion.USUARIO_ID + "))");
        }
        else
        {
            TA.ActualizaIn("AND PERSONA_ID IN(" + Sesion.WF_EmpleadosFil_ObtenFiltroDefault + ") AND (EC_PERSONAS.SUSCRIPCION_ID IN (SELECT EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Sesion.USUARIO_ID + "))");
        }
        //CeC_Grid.AplicaFormato(Wdg_EC_PERSONAS);

        if (CeC_Personas.ObtenPersonasNO(Sesion.SUSCRIPCION_ID) > 50 && Sesion.WF_EmpleadosFil_Qry.Length == 0 && Sesion.WF_TurnosAsignacionExpressQry.Length == 0)
            Sesion.WF_EmpleadosFil(false, false, false, "Continuar", "Asignacion de Turnos Express", "WF_TurnosAsignacionExpress.aspx", "Asignacion de Turnos Express", false, true, false);
        else
        {
            if (Sesion.WF_EmpleadosFil_Qry.Length > 0)
            {
                Sesion.WF_TurnosAsignacionExpressQry = Sesion.WF_EmpleadosFil_Qry_Temp = Sesion.WF_EmpleadosFil_Qry;
            }
            else
            {
                Sesion.WF_EmpleadosFil_Qry = "";
            }
        }
        Sesion.TituloPagina = "Asignación de Turnos Express";
        Sesion.DescripcionPagina = "Primero seleccione los empleados y por último el nuevo turno, el cual será asignado inmediatamente";
        if (Sesion.EsWizard > 0)
        {
            WIBtn_Guardar.Text = "Siguiente";
            WIBtn_Guardar.Appearance.Image.Url = "./Imagenes/gtk-go-forward.png";
            Sesion.TituloPagina = "Asignación de Turnos Express";
            Sesion.DescripcionPagina = "Selecciona a los empleados a los que desea asignar un turno luego seleccione el turno que les será asignado a los empleados. Los cambios se guardan automáticamente ";
        }
        else
        {
            WIBtn_Guardar.Visible = false;
        }
        //Agregar Módulo Log
        DS.TurnosExpressDatatable.Clear();
        TA.Fill(DS.TurnosExpressDatatable);
        Wdg_EC_PERSONAS.DataSource = DS;
        Wdg_EC_PERSONAS.DataBind();
        if (!IsPostBack)
        {
            if (Sesion.SUSCRIPCION_ID > 0)
            {
                //TATurnos.Fill(DSTurnos.EC_ASIGNACIONEXPRESS, Sesion.SUSCRIPCION_ID);
                RbnL_EC_TURNOS.DataSource = DSTurnos.EC_ASIGNACIONEXPRESS;
                RbnL_EC_TURNOS.DataBind();
            }
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Asignación Turnos Express", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
        Sesion.ControlaBoton(ref WIBtn_Guardar);
        Sesion.ControlaBoton(ref WIBtn_Filtro);
        
    }

    protected void WIBtn_Filtro_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        Sesion.WF_EmpleadosFil(false, false, false, "Continuar", "Asignacion de Turnos Express", "WF_TurnosAsignacionExpress.aspx", "Asignacion de Turnos Express", false, true, false);
    }
    protected void WIBtn_Guardar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        if (Sesion.EsWizard > 0)
        {
            Sesion.EsWizard = 0;
            Sesion.Redirige("WF_WizardMail.aspx");
        }
        else
            Sesion.Redirige("WF_Main.aspx");
        Sesion.WF_EmpleadosFil_Qry = Sesion.WF_EmpleadosFil_Qry_Temp = "";
    }

    protected void RbnL_EC_TURNOS_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RbnL_EC_TURNOS.DataTextField = "TURNO_NOMBRE";
            RbnL_EC_TURNOS.DataValueField = "TURNO_ID";
            RbnL_EC_TURNOS.DataBind();
            if (Wdg_EC_PERSONAS.Rows.Count > 100)
                WIBtn_Guardar_Click(null, null);
        }
    }
    protected void RbnL_EC_TURNOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        //for (int i = 0; i < Wdg_EC_PERSONAS.Rows.Count; i++)
        //{
        //    int PersonaID = CeC_Personas.ObtenPersonaID(CeC.Convierte2Int(Wdg_EC_PERSONAS.Rows[i].DataItem));//Cells[UWGPersonas.Columns.FromKey("PERSONA_LINK_ID").Index].Value));
        //    CeC_Turnos.AsignaHorarioPred(PersonaID, CeC.Convierte2Int(RbnL_EC_TURNOS.SelectedValue), Sesion.SESION_ID);
        //    //CeC_Campos.GuardaValor("TURNO_ID", CeC_Personas.ObtenPersonaID(Convert.ToInt32(UWGPersonas.Rows[i].Cells[UWGPersonas.Columns.FromKey("PERSONA_LINK_ID").Index].Value.ToString())), RBTurnos.SelectedValue);
        //    Wdg_EC_PERSONAS.Rows[i].DataKey. = RbnL_EC_TURNOS.SelectedItem.Text; //[UWGPersonas.Columns.FromKey("TURNO_NOMBRE").Index].Value = RBTurnos.SelectedItem.Text;
        //    if (UWGPersonas.Rows[i].Selected == true)
        //    {
        //        //CeC_BD.EjecutaComando("UPDATE EC_PERSONAS SET TURNO_ID = ")
        //        int PersonaID = CeC_Personas.ObtenPersonaID(CeC.Convierte2Int(UWGPersonas.Rows[i].Cells[UWGPersonas.Columns.FromKey("PERSONA_LINK_ID").Index].Value));
        //        CeC_Turnos.AsignaHorarioPred(PersonaID, CeC.Convierte2Int(RBTurnos.SelectedValue), Sesion.SESION_ID);
        //        //CeC_Campos.GuardaValor("TURNO_ID", CeC_Personas.ObtenPersonaID(Convert.ToInt32(UWGPersonas.Rows[i].Cells[UWGPersonas.Columns.FromKey("PERSONA_LINK_ID").Index].Value.ToString())), RBTurnos.SelectedValue);
        //        UWGPersonas.Rows[i].Cells[UWGPersonas.Columns.FromKey("TURNO_NOMBRE").Index].Value = RBTurnos.SelectedItem.Text;
        //    }
    //    }
    //    //UWGPersonas_InitializeDataSource(null, null);
    }
    //protected void InicializarWebDataGrid()
    //{
    //    try
    //    {
    //        DS_AsignacionTurnosExpress DS = new DS_AsignacionTurnosExpress();
    //        Sesion = CeC_Sesion.Nuevo(this);

    //        DS_AsignacionTurnosExpressTableAdapters.TurnosExpressDatatableTableAdapter TA = new DS_AsignacionTurnosExpressTableAdapters.TurnosExpressDatatableTableAdapter();

    //        if (Sesion.WF_TurnosAsignacionExpressQry.Length > 0)
    //        {
    //            if (Sesion.WF_EmpleadosFil_Qry != "")
    //                Sesion.WF_TurnosAsignacionExpressQry = Sesion.WF_EmpleadosFil_Qry_Temp = Sesion.WF_EmpleadosFil_Qry;
    //            //  Sesion.WF_EmpleadosFil_Qry = "";
    //            TA.ActualizaIn("AND PERSONA_ID IN(" + Sesion.WF_TurnosAsignacionExpressQry + ") AND (EC_PERSONAS.SUSCRIPCION_ID IN (SELECT EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Sesion.USUARIO_ID + "))");
    //        }
    //        else
    //        {
    //            TA.ActualizaIn("AND PERSONA_ID IN(" + Sesion.WF_EmpleadosFil_ObtenFiltroDefault + ") AND (EC_PERSONAS.SUSCRIPCION_ID IN (SELECT EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID FROM EC_PERMISOS_SUSCRIP WHERE EC_PERMISOS_SUSCRIP.USUARIO_ID = " + Sesion.USUARIO_ID + "))");
    //        }
    //        // DS.TurnosExpressDatatable.Clear();
    //        //TA.Fill(DS.TurnosExpressDatatable);
    //        Wdg_EC_PERSONAS.DataSource = DS;
    //        Wdg_EC_PERSONAS.DataBind();
    //        //CeC_Grid.AplicaFormato(Wdg_EC_PERSONAS);
    //    }
    //    catch (Exception ex)
    //    { 
    //    }
    //}
   
    //protected void UWGPersonas_SelectedRowsChange(object sender, Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs e)
    //{
    //    try
    //    {
    //        int aux = -1;
    //        for (int i = 0; i < UWGPersonas.Rows.Count; i++)
    //        {
    //            if (UWGPersonas.Rows[i].Selected == true)
    //            {
    //                seleccionadas++;
    //                aux = i;
    //            }
    //        }
    //        if (seleccionadas > 1)
    //            RBTurnos.ClearSelection();
    //        if (seleccionadas == 1)
    //            RBTurnos.SelectedValue = RBTurnos.Items.FindByText(UWGPersonas.Rows[aux].Cells[UWGPersonas.Columns.FromKey("TURNO_NOMBRE").Index].Value.ToString()).Value;
    //        seleccionadas = 0;
    //    }
    //    catch (Exception ex)
    //    {
    //        CIsLog2.AgregaError(ex);
    //    }
    //}
}
