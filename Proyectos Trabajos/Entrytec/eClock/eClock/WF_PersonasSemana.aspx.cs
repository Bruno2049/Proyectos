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

public partial class WF_PersonasSemana : System.Web.UI.Page
{
    protected CeC_Sesion Sesion;
    string Parametros;

    protected void MuestraFiltro()
    {
        if (Sesion.WF_PerSemParametros == "Turnos")
        {
            Sesion.WF_EmpleadosFil(false, 1, false, "Siguiente", "Consulta de turnos semanales", "WF_PersonasSemana.aspx", "Filtro de Personas", false, true, false);
        }
        else
        {
            Sesion.WF_EmpleadosFil(false, 1, false, "Siguiente", "Consulta de asistencia semanal", "WF_PersonasSemana.aspx", "Filtro de Personas", false, true, false);
        }
    }

    protected void DeshabilitarControles()
    {
        btn_Justificar.Visible = false;
        btnAsignarTurno.Visible = false;
        btnFiltro.Visible = false;
        Grid.Visible = false;
        LBAnterior.Visible = false;
        LBSiguiente.Visible = false;
        Webimagebutton1.Visible = false;
        WDC_PerSem.Visible = false;
        TurnosCombo.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Sesion = CeC_Sesion.Nuevo(this);
        if (Sesion.Parametros != "")
            Sesion.WF_PerSemParametros = Sesion.Parametros;
        // Permisos****************************************
        Sesion.WF_PerSemParametros = "Turnos";
        if (Sesion.WF_PerSemParametros == "Turnos")
        {
            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Turnos))
                if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Turnos0Asignacion))
                    if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Turnos0Asignacion0Avanzada, true))
                    {
                        DeshabilitarControles();
                        return;
                    }
        }
        else
            if (!Sesion.TienePermiso(eClock.CEC_RESTRICCIONES.S0Empleados0Consultar_Asistencia))
            if (!Sesion.TienePermisoOHijos(eClock.CEC_RESTRICCIONES.S0Empleados0Consultar_Asistencia0Semanal, true))
            {
                DeshabilitarControles();
                return;
            }
        //**************************************************


        if (Sesion.WF_PerSemParametros == "Turnos")
        {
            Sesion.TituloPagina = "Consulta de Turnos a la Semana";
        }
        else
        {
            Sesion.TituloPagina = "Consulta de Asistencia Semanal";
        }
        Sesion.DescripcionPagina = "Puede cambiar el horario de los empleados así como también justificar las faltas";
/*        LTitulo.Text = Sesion.TituloPagina;
        LDescripcion.Text = Sesion.DescripcionPagina;*/
        if (Sesion.WF_EmpleadosFil_Qry.Length > 0)
        {
            Sesion.WF_EmpleadosFil_Qry_Temp = Sesion.WF_EmpleadosFil_Qry;
            Sesion.WF_EmpleadosFil_Qry = "";
            Sesion.WF_PersonasSemanaQry = Sesion.WF_EmpleadosFil_Qry_Temp;
        }
        else
            if (Sesion.WF_PersonasSemanaQry.Length == 0)
            {
                Sesion.WF_EmpleadosFil_FechaI = DateTime.Today.AddDays(-7);
                if (CeC_Personas.ObtenPersonasNO(Sesion.SUSCRIPCION_ID) > 100)
                {
                    TurnosCombo.DataTextField = "";
                    TurnosCombo.DataValueField = "";
                    MuestraFiltro();
                    return;
                }
                else
                    Sesion.WF_PersonasSemanaQry = Sesion.WF_EmpleadosFil_ObtenFiltroDefault;
                ActualizaDatos();
            }
        if (!IsPostBack)
        {
            TurnosCombo.DataSource = CeC_Turnos.ObtenTurnosDSAgregado(Sesion.SUSCRIPCION_ID);
            TurnosCombo.DataBind();
            TurnosCombo.SelectedIndex = 0;
            //Sesion.WF_EmpleadosFil_FechaI = DateTime.Today.AddDays(-7);
            WDC_PerSem.Value = Sesion.WF_EmpleadosFil_FechaI;
            //Sesion.WF_PersonasSemanaCambiosHorarioDataTable = null;
            //Agrega Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Turno Semanal por Persona ", 0, "Cambia Horario de los Empleados");
        }
        Sesion.ControlaBoton(ref btn_Justificar);
        Sesion.ControlaBoton(ref btnAsignarTurno);
        Sesion.ControlaBoton(ref btnFiltro);
        Sesion.ControlaBoton(ref Webimagebutton1);
    }

    protected void AsignaFormatoDia(Infragistics.WebUI.UltraWebGrid.UltraGridColumn Columna, int NO)
    {
        Columna.Header.Caption = Sesion.WF_EmpleadosFil_FechaI.AddDays(NO).ToString("dddd dd-MM-yy");
        Columna.Header.Style.Font.Size = 7;
        Columna.CellStyle.Font.Size = 6;
        Columna.Width = Unit.Pixel(50);
    }

    protected string ObtenTurnoNombre(int Turno_ID)
    {
        if (Turno_ID == 0)
            return "Predeterminado";
        if (Turno_ID == -1)
            return "Descanso";
        return CeC_BD.EjecutaEscalarString("SELECT TURNO_NOMBRE FROM EC_TURNOS WHERE TURNO_ID = " + Turno_ID);
    }

    protected void Grid_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {
        CeC_Grid.AplicaFormato(Grid, true, false, true, true);
        // if (!IsPostBack)
        {
            //Introduce la máscara de Hora en la columna 7 (Hrs. Extra a Aplicar)
            //Infragistics.WebUI.UltraWebGrid.UltraGridColumn Fila = Grid.Columns.FromKey("TURNO_ID_D0");
            //Fila.Hidden = true;
            Grid.Columns.FromKey("PERSONA_DIARIO_FECHA").Hidden = true;
            Grid.Columns.FromKey("TURNO_ID_D0").Hidden = true;
            Grid.Columns.FromKey("TURNO_ID_D1").Hidden = true;
            Grid.Columns.FromKey("TURNO_ID_D2").Hidden = true;
            Grid.Columns.FromKey("TURNO_ID_D3").Hidden = true;
            Grid.Columns.FromKey("TURNO_ID_D4").Hidden = true;
            Grid.Columns.FromKey("TURNO_ID_D5").Hidden = true;
            Grid.Columns.FromKey("TURNO_ID_D6").Hidden = true;

                Grid.Columns.FromKey("SUSCRIPCION_NOMBRE").Hidden = true;

            if (Sesion.WF_PerSemParametros == "Turnos")
            {
                AsignaFormatoDia(Grid.Columns.FromKey("TURNO_D0"), 0);
                AsignaFormatoDia(Grid.Columns.FromKey("TURNO_D1"), 1);
                AsignaFormatoDia(Grid.Columns.FromKey("TURNO_D2"), 2);
                AsignaFormatoDia(Grid.Columns.FromKey("TURNO_D3"), 3);
                AsignaFormatoDia(Grid.Columns.FromKey("TURNO_D4"), 4);
                AsignaFormatoDia(Grid.Columns.FromKey("TURNO_D5"), 5);
                AsignaFormatoDia(Grid.Columns.FromKey("TURNO_D6"), 6);
                Grid.Columns.FromKey("ASISTENCIA_D0").Hidden = true;
                Grid.Columns.FromKey("ASISTENCIA_D1").Hidden = true;
                Grid.Columns.FromKey("ASISTENCIA_D2").Hidden = true;
                Grid.Columns.FromKey("ASISTENCIA_D3").Hidden = true;
                Grid.Columns.FromKey("ASISTENCIA_D4").Hidden = true;
                Grid.Columns.FromKey("ASISTENCIA_D5").Hidden = true;
                Grid.Columns.FromKey("ASISTENCIA_D6").Hidden = true;
            }
            else
            {
                AsignaFormatoDia(Grid.Columns.FromKey("ASISTENCIA_D0"), 0);
                AsignaFormatoDia(Grid.Columns.FromKey("ASISTENCIA_D1"), 1);
                AsignaFormatoDia(Grid.Columns.FromKey("ASISTENCIA_D2"), 2);
                AsignaFormatoDia(Grid.Columns.FromKey("ASISTENCIA_D3"), 3);
                AsignaFormatoDia(Grid.Columns.FromKey("ASISTENCIA_D4"), 4);
                AsignaFormatoDia(Grid.Columns.FromKey("ASISTENCIA_D5"), 5);
                AsignaFormatoDia(Grid.Columns.FromKey("ASISTENCIA_D6"), 6);
                Grid.Columns.FromKey("TURNO_D0").Hidden = true;
                Grid.Columns.FromKey("TURNO_D1").Hidden = true;
                Grid.Columns.FromKey("TURNO_D2").Hidden = true;
                Grid.Columns.FromKey("TURNO_D3").Hidden = true;
                Grid.Columns.FromKey("TURNO_D4").Hidden = true;
                Grid.Columns.FromKey("TURNO_D5").Hidden = true;
                Grid.Columns.FromKey("TURNO_D6").Hidden = true;
            }
            Grid.DisplayLayout.SelectTypeCellDefault = Infragistics.WebUI.UltraWebGrid.SelectType.Extended;
            Grid.DisplayLayout.SelectTypeColDefault = Infragistics.WebUI.UltraWebGrid.SelectType.Extended;
            Grid.DisplayLayout.CellClickActionDefault = Infragistics.WebUI.UltraWebGrid.CellClickAction.CellSelect;
        }
    }

    protected void Grid_InitializeDataSource(object sender, Infragistics.WebUI.UltraWebGrid.UltraGridEventArgs e)
    {
        ActualizaDatos();
    }

    protected void ActualizaDatos()
    {
        try
        {
            Sesion = CeC_Sesion.Nuevo(this);
            if (Sesion.WF_PersonasSemanaQry.Length <= 0)
                return;
            WDC_PerSem.Value = Sesion.WF_EmpleadosFil_FechaI;
            DS_Personas_SemanaTableAdapters.Personas_SemanaTableAdapter TA = new DS_Personas_SemanaTableAdapters.Personas_SemanaTableAdapter();
            DS_Personas_Semana DS = new DS_Personas_Semana();
            TA.ActualizaIn(Sesion.WF_PersonasSemanaQry);
            TA.FillByFecha(DS.Personas_Semana, Sesion.WF_EmpleadosFil_FechaI);
            //TA.Fill(DS.Personas_Semana);
            Grid.DataSource = DS.Personas_Semana;
            Grid.DataMember = DS.Personas_Semana.TableName;
            Grid.DataKeyField = "PERSONA_LINK_ID";
            DS_Personas_Semana.CambiosHorarioDataTable DT = Sesion.WF_PersonasSemanaCambiosHorarioDataTable;
            if (DT != null)
            {
                foreach (DS_Personas_Semana.CambiosHorarioRow Fila in DT)
                {
                    if (Fila.PERSONA_DIARIO_FECHA >= Sesion.WF_EmpleadosFil_FechaI && Fila.PERSONA_DIARIO_FECHA <= Sesion.WF_EmpleadosFil_FechaI.AddDays(6))
                    {
                        DS_Personas_Semana.Personas_SemanaRow FilaPersona = DS.Personas_Semana.FindByPERSONA_LINK_ID(Fila.PERSONA_LINK_ID);
                        if (FilaPersona != null)
                            FilaPersona["TURNO_D" + ((TimeSpan)(Fila.PERSONA_DIARIO_FECHA - Sesion.WF_EmpleadosFil_FechaI)).Days.ToString()] = ObtenTurnoNombre(Fila.TURNO_ID);
                    }
                }
            }
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
    }

    protected void btnFiltro_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        MuestraFiltro();
    }

    protected void TurnosCombo_InitializeLayout(object sender, Infragistics.WebUI.UltraWebGrid.LayoutEventArgs e)
    {

        CeC_Grid.AplicaFormato(TurnosCombo);
        TurnosCombo.DropDownLayout.RowStyle.Height = TurnosCombo.DropDownLayout.RowHeightDefault = Unit.Pixel(20);
    }

    protected void BGuardarCambios_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        LCorrecto.Text = "";
        LError.Text = "";
        try
        {
            DS_Personas_Semana.CambiosHorarioDataTable DT = Sesion.WF_PersonasSemanaCambiosHorarioDataTable;
            if (DT != null)
            {
                foreach (DS_Personas_Semana.CambiosHorarioRow Fila in DT)
                {
                    int Persona_ID = CeC_Personas.ObtenPersonaID(Fila.PERSONA_LINK_ID, Sesion.USUARIO_ID);
                    if (Persona_ID > 0)
                    {
                        if (Fila.PERSONA_DIARIO_FECHA == CeC_BD.FechaNula)
                        {
                            CeC_Turnos.AsignaHorarioPred(Persona_ID, Fila.TURNO_ID, Sesion.SESION_ID);
                        }
                        else
                        {
                            CeC_Turnos.AsignaHorario(Persona_ID, Fila.TURNO_ID, Fila.PERSONA_DIARIO_FECHA, Fila.PERSONA_DIARIO_FECHA, Sesion);
                        }
                    }
                }
            }
            LCorrecto.Text = "Los datos fueron guardados con éxito";
            Sesion.WF_PersonasSemanaCambiosHorarioDataTable = null;
            return;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        LError.Text = "Ocurrió un error al guardar";
    }

    protected void LBAnterior_Click(object sender, EventArgs e)
    {
        Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddDays(-7);
        RecargarGrid();
    }
    protected void LBSiguiente_Click(object sender, EventArgs e)
    {
        Sesion.WF_EmpleadosFil_FechaI = Sesion.WF_EmpleadosFil_FechaI.AddDays(7);
        RecargarGrid();
    }
    protected void WDC_PerSem_ValueChanged(object sender, Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs e)
    {
        Sesion.WF_EmpleadosFil_FechaI = Convert.ToDateTime(WDC_PerSem.Value);
        RecargarGrid();
    }
    protected void RecargarGrid()
    {
        Sesion.Redirige(Sesion.Pagina_Actual);
        return;
        Grid_InitializeLayout(null, null);
        Grid_InitializeDataSource(null, null);
        this.Response.Redirect(Sesion.Pagina_Actual);
    }
}