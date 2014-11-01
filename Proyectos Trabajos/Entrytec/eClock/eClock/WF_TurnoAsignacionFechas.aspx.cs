using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WF_TurnoAsignacionFechas : System.Web.UI.Page
{
    CeC_Sesion Sesion;

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        CargaDatosDS();
    }

    /// <summary>
    /// Boton que asigna el horario seleccionado en el rango de fechas a las personas.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnMover_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            Lbl_Correcto.Text = "Se asignaron correctamente los horarios a las siguientes personas: ";
            Lbl_Error.Text = "Hubo errores al asignar los horarios a las siguientes personas:";

            string[] Persona_Link_IDS = CeC.ObtenArregoSeparador(Tbx_PERSONA_LINK_ID.Text, ",");
            int TurnoID = CeC.Convierte2Int(Wco_EC_TURNOS.DataValue);
            DateTime FechaInicio = Cal_Desde.SelectedDate;
            DateTime FechaFin = Cal_Hasta.SelectedDate;
            foreach (string Persona_Link_ID in Persona_Link_IDS)
            {
                if (AsignaHorario(TurnoID, CeC.Convierte2Int(Persona_Link_ID), FechaInicio, FechaFin) == true)
                {
                    Lbl_Correcto.Text += Persona_Link_ID + ", ";
                }
                else
                { 
                    Lbl_Error.Text += Persona_Link_ID + ", ";
                }
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnoAsignacionFechas.BtnMover_Click", ex);
        }
    }
    /// <summary>
    /// Inicializa la variable de Sesion y el combo con los turnos dados de alta
    /// </summary>
    protected void CargaDatosDS()
    {
        try
        {
            Sesion = CeC_Sesion.Nuevo(this);
            //  CeC_Campos.ReiniciaCampos();
            Wco_EC_TURNOS.Visible = true;
            DataSet DS = CeC_Turnos.ObtenTurnosDSMenu(Sesion.SUSCRIPCION_ID);
            if (DS != null)
            {
                Wco_EC_TURNOS.DataSource = DS;
                Wco_EC_TURNOS.DataValueField = "TURNO_ID";
                Wco_EC_TURNOS.DataTextField = "TURNO_NOMBRE";
                Wco_EC_TURNOS.DataBind();
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnoAsignacionFechas.CargaDatosDS", ex);
        }
    }
    /// <summary>
    /// Asigna el horario a la persona por rango de fechas. No cambia el Turno Predeterminado.
    /// </summary>
    /// <param name="TurnoID">Identificador de Turno seleccionado</param>
    /// <param name="Persona_Link_ID">Numero de trabajador</param>
    /// <param name="FechaInicio">Fecha de inicio del turno</param>
    /// <param name="FechaFin">Fecha de fin del turno</param>
    /// <returns>Verdadero si cambio el turno en los dias seleccionados. Falso en caso de que no se halla cambiado</returns>
    bool AsignaHorario(int TurnoID, int Persona_Link_ID, DateTime FechaInicio, DateTime FechaFin)
    {
        try
        {
            int Persona_ID = CeC_Personas.ObtenPersonaID(Persona_Link_ID);
            if (CeC_Turnos.AsignaHorario(Persona_ID, TurnoID, FechaInicio, FechaFin, Sesion) > 0)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_TurnoAsignacionFechas.AsignaHorario", ex);
        }
        return false;
    }
}
