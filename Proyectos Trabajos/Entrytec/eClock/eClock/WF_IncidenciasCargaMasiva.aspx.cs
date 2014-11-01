using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_IncidenciasCargaMasiva : System.Web.UI.Page
{
    CeC_Sesion Sesion;
    String Mensaje;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Se crea la Sesion
        Sesion = CeC_Sesion.Nuevo(this);
        Sesion.TituloPagina = "Carga Masiva de Incidencias";
        Sesion.DescripcionPagina = "Teclee en la columna correspondiente el Número de Empleado, la Fecha de Inicio, la Fecha de Fin y la abreviatura de la incidencia";
        //Lbl_Mesaje_Correcto.Text = Sesion.Parametros;
        //Lbl_Mensaje_Error.Text = Sesion.Parametros;
        Lbl_Instrucciones.Text = "Introduzca o copie los datos en la columna que corresponda.";
        if (!this.IsPostBack)
        {
            int max_rows = 30;
            // Se llenan las cabeceras de las columnas
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 0, CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave));
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[0].Key = CeC_Campos.ObtenEtiqueta(CeC_Campos.CampoTE_Llave);
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 1, "Fecha Inicio");
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[1].Key = "Fecha Inicio";
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 2, "Fecha Termino");
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[2].Key = "Fecha Fin";
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 3, "Incidencia");
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[3].Key = "Abreviatura de Incidencia";
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 4, "Motivo de la Incidencia");
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[3].Key = "Motivo de la Incidencia";
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 5, "No. Folio");
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[5].Key = "No. Folio de Incidencia";
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 6, "Hora Inicio");
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[6].Key = "Hora de Inicio";
            this.UWG_ASIGNACION_INCIDENCIAS.Columns.Add("Col" + 7, "Hora Fin");
            this.UWG_ASIGNACION_INCIDENCIAS.Columns[7].Key = "Hora de Termino";
            // Se crea un Grid de tamaño max_rows
            for (int i = 0; i < max_rows; i++)
            {
                this.UWG_ASIGNACION_INCIDENCIAS.Rows.Add();
            }
            //Agregar Módulo Log
            Sesion.AgregaLogModulo(CeC_Sesion.LOG_TABLA_TIPO.CONSULTA, "Carga de Incidencias Express", Sesion.USUARIO_ID, Sesion.USUARIO_NOMBRE);
        }
    }
    protected void Guardar()
    {
        string registrosConErrores = "";
        int registrosCorrectos = 0;
        bool ret = false;
        int Persona_Link_ID = 0;
        int PersonaID = 0;
        int PersonaDiarioID = 0;
        DateTime FechaInicial = CeC_BD.FechaNula;
        DateTime FechaFinal = CeC_BD.FechaNula;
        String AbreviaturaInc = "";
        String MotivoIncidencia = "";
        String Folio = "";
        TimeSpan HoraInicio = new TimeSpan();
        TimeSpan HoraFin = new TimeSpan();
        TimeSpan HoraInicioAux = new TimeSpan();
        TimeSpan HoraFinAux = new TimeSpan();
        DateTime FechaHoraInicio = CeC_BD.FechaNula;;
        DateTime FechaHoraFin = CeC_BD.FechaNula;;
        for (int i = 0; i < UWG_ASIGNACION_INCIDENCIAS.Rows.Count; i++)
        {
            try
            {
                // Checamos que se introduzcan los mínimos de datos necesarios
                if (UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[0].Value == null &&
                    UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[1].Value == null &&
                    UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[3].Value == null)
                    continue;
                // Se leen los datos necesarios
                Persona_Link_ID = CeC.Convierte2Int(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[0].Value);
                FechaInicial = CeC.Convierte2DateTime(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[1].Value);
                AbreviaturaInc = CeC.Convierte2String(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[3].Value);
                // Obtenemos el PersonaID
                PersonaID = CeC_Personas.ObtenPersonaID(Persona_Link_ID);
                // Obtenemos los PersonaDiarioID para obtener sus accesos
                PersonaDiarioID = CeC_Asistencias.ObtenPersonaDiarioID(PersonaID, FechaInicial);
                // Los campos que no necesitan validar información se inicializan con su valor capturado o se calcula su valor predeterminado
                if (UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[2].Value == null)
                {
                    // Si no se capturo la Fecha Inicial la inicializamos con el valor de la Fecha Inicial
                    FechaFinal = CeC.Convierte2DateTime(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[1].Value);
                }
                else
                    FechaFinal = CeC.Convierte2DateTime(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[2].Value);
                if (UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[4].Value == null)
                    MotivoIncidencia = "";
                else
                    MotivoIncidencia = CeC.Convierte2String(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[4].Value);
                if (UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[5].Value == null)
                    Folio = "";
                else
                    Folio = CeC.Convierte2String(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[5].Value);
                // Validamos que no sea necesario meter ambas horas cuando son justificaciones de entrada o salida
                if(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[6].Value == null)
                {
                    HoraInicio = CeC_BD.DateTime2TimeSpan(CeC_Asistencias.ObtenHoraEntradaDia(PersonaDiarioID));
                }
                else
                    HoraInicio = TimeSpan.Parse(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[6].Text);
                if (UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[7].Value == null)
                {
                    HoraFin = CeC_BD.DateTime2TimeSpan(CeC_Asistencias.ObtenHoraSalidaDia(PersonaDiarioID));
                }
                else
                    HoraFin = TimeSpan.Parse(UWG_ASIGNACION_INCIDENCIAS.Rows[i].Cells[7].Text);
                // Guardamos la Fecha y la Hora del Acceso que se quiere cargar a la incidencia
                if (HoraInicio.Ticks != HoraFin.Ticks)
                {
                    if (HoraInicio.Ticks < HoraFin.Ticks)
                    {
                        HoraInicioAux = new TimeSpan(HoraFin.Days, HoraInicio.Hours, HoraInicio.Minutes, HoraInicio.Seconds);
                        HoraFinAux = HoraFin; 
                    }
                    else
                    {
                        HoraFinAux = new TimeSpan(HoraInicio.Days, HoraFin.Hours, HoraFin.Minutes, HoraFin.Seconds);
                        HoraInicioAux = HoraInicio;
                    }
                    FechaHoraInicio = CeC_BD.TimeSpan2DateTime(HoraInicioAux);
                    FechaHoraFin = CeC_BD.TimeSpan2DateTime(HoraFinAux);
                }
                FechaHoraInicio = CeC_BD.TimeSpan2DateTime(HoraInicio);
                FechaHoraFin = CeC_BD.TimeSpan2DateTime(HoraFin);

                if (GuardaIncidencias(Persona_Link_ID, FechaInicial, FechaFinal, AbreviaturaInc, MotivoIncidencia, Folio, FechaHoraInicio, FechaHoraFin))
                {
                    registrosCorrectos = i;
                    ret = true;
                }
                else
                {
                    registrosConErrores += (i + 1).ToString() + ", ";
                    ret = false;
                }
            }
            catch (Exception ex) 
            {
                ret = false;
                registrosConErrores += (i + 1).ToString() + ", ";
                CIsLog2.AgregaError(ex); 
            }
        }
        if (!ret || registrosConErrores != "")
        {
            Lbl_Mensaje_Correcto.Text = "";
            Lbl_Mensaje_Error.Text = "Existieron errores en la importacion del registro(s) No. " + registrosConErrores +
                                        "Revise que la información introducida sea correcta.";
        }
        else
        {
            Lbl_Mensaje_Error.Text = "";
            Lbl_Mensaje_Correcto.Text = "Se importaron con exito " + (registrosCorrectos + 1) + " registros. ";
        }
    }

    protected bool GuardaIncidencias(int Persona_Link_ID, DateTime FechaInicial, DateTime FechaFinal, String AbreviaturaInc, String MotivoIncidencia, String Folio, DateTime HoraInicio, DateTime HoraFin)
    {
        try
        {
            int TipoIncidenciaID = Cec_Incidencias.ObtenTipoIncidenciaID(AbreviaturaInc);
            int PersonaID = CeC_Empleados.ObtenPersonaID(Persona_Link_ID);
            int PersonaDiarioID = CeC_Asistencias.ObtenPersonaDiarioID(PersonaID, FechaInicial);
            string NombreIncidencia = Cec_Incidencias.ObtenTipoIncidenciaNombre(TipoIncidenciaID);
            
            if (TipoIncidenciaID > 0)
            {
                int IncidenciaID = Cec_Incidencias.CreaIncidencia(TipoIncidenciaID, MotivoIncidencia, Sesion.SESION_ID);
                if (IncidenciaID > 0)
                {
                    int Persona_ID = CeC_Empleados.ObtenPersonaID(Persona_Link_ID, Sesion.USUARIO_ID);
                    if (Persona_ID <= 0)
                    {
                        CIsLog2.AgregaError("Numero de empleado " + Persona_Link_ID + " no existe.");
                        return false;
                    }
                    if (Cec_Incidencias.AsignaIncidencia(FechaInicial, FechaFinal, Persona_ID, IncidenciaID, Sesion.SESION_ID) > 0)
                    {
                        if (AsignaIncidencias(IncidenciaID, TipoIncidenciaID, PersonaDiarioID, Persona_Link_ID, MotivoIncidencia, Folio, HoraInicio, HoraFin, FechaInicial, FechaFinal))
                            return true;
                    }
                }
            }
            return false;
        }
        catch (Exception Ex)
        {
            CIsLog2.AgregaError(Ex);
            return false;
        }
    }
    public bool AsignaIncidencias(int IncidenciaID, int TipoIncidenciaID, int PersonaDiarioID, int Persona_Link_ID, string MotivoIncidencia, string Folio, DateTime HoraInicio, DateTime HoraFin, DateTime FechaInicial, DateTime FechaFinal)
    {
        string Extra = "";
        bool ret = false;
        WS_eCheck WS = new WS_eCheck();
        DateTime Intervalo = CeC_BD.FechaNula;
        try
        {
            Extra = CeC.AgregaSeparador(Extra, "INCIDENCIA_ID=" + IncidenciaID, "|");
            int diferencia = 0;
            int PersonaID = CeC_Empleados.ObtenPersonaID(Persona_Link_ID);
            bool TieneIncidenciaRegla = CeC_IncidenciasRegla.TieneIncidenciaRegla(TipoIncidenciaID);
            DateTime Fecha = FechaInicial;
            diferencia = FechaFinal.Day - FechaInicial.Day;

            int TipoIncidenciaRID = CeC_IncidenciasRegla.ObtenTipo_Incidencia_R_ID(PersonaDiarioID);
            for (int i = 0; i <= diferencia; i++)
            {
                if (TipoIncidenciaRID > 0 && TieneIncidenciaRegla)
                {
                    PersonaDiarioID = CeC_Asistencias.ObtenPersonaDiarioID(PersonaID, Fecha.AddDays(i));
                    if (CeC_IncidenciasRegla.AsignaIncidencia(TipoIncidenciaRID, PersonaDiarioID.ToString(), ObtenComentarios(TipoIncidenciaRID) + Folio, Sesion.SESION_ID) > 0)
                    {
                        DateTime FechaHoraInicio = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
                        DateTime FechaHoraFin = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
                        //if (CeC_Accesos.AgregaChecada(CeC_Config.TerminalID_CargaMasivaIncidencias, Persona_Link_ID.ToString(), FechaHoraInicio.AddDays(i), Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, true))
                        //{
                        //    CeC_Accesos.AgregaChecada(CeC_Config.TerminalID_CargaMasivaIncidencias, Persona_Link_ID.ToString(), FechaHoraFin.AddDays(i), Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, true);
                        ret = true;
                        continue;
                        //}
                        //if (WS.RegistrarChecada(WS.ObtenPersonaID(Persona_Link_ID), CeC_Config.TerminalID_CargaMasivaIncidencias, Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), FechaHoraInicio.AddDays(i)))
                        //{
                        //    WS.RegistrarChecada(WS.ObtenPersonaID(Persona_Link_ID), CeC_Config.TerminalID_CargaMasivaIncidencias, Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), FechaHoraFin.AddDays(i));
                        //    ret = true;
                        //    continue;
                        //}
                    }
                }
                else
                {
                    DateTime FechaHoraInicio = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, HoraInicio.Hour, HoraInicio.Minute, HoraInicio.Second);
                    DateTime FechaHoraFin = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, HoraFin.Hour, HoraFin.Minute, HoraFin.Second);
                    CeC_Accesos_Jus.NuevaJustificacion(1, Intervalo, MotivoIncidencia, true, PersonaDiarioID, HoraInicio, HoraFin, out Extra, Sesion);
                    if (HoraInicio != CeC_BD.FechaNula && HoraFin != CeC_BD.FechaNula)
                    {
                        //if (CeC_Accesos.AgregaChecada(CeC_Config.TerminalID_CargaMasivaIncidencias, Persona_Link_ID.ToString(), FechaHoraInicio.AddDays(i), Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, true))
                        //{
                        //    CeC_Accesos.AgregaChecada(CeC_Config.TerminalID_CargaMasivaIncidencias, Persona_Link_ID.ToString(), FechaHoraFin.AddDays(i), Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, true);
                        //    ret = true;
                        //    continue;
                        //}
                        if (WS.RegistrarChecada(WS.ObtenPersonaID(Persona_Link_ID), CeC_Config.TerminalID_CargaMasivaIncidencias, Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), FechaHoraInicio.AddDays(i)))
                        {
                            WS.RegistrarChecada(WS.ObtenPersonaID(Persona_Link_ID), CeC_Config.TerminalID_CargaMasivaIncidencias, Convert.ToInt32(WS_eCheck.TipoAccesos.Correcto), FechaHoraFin.AddDays(i));
                            ret = true;
                            continue;
                        }
                    }
                    ret = true;
                    continue;
                }
            }
            return ret;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError("WF_IncidenciasCargaMasiva.AsignaIncidencias", ex);
            return false;
        }
    }
    protected void WIBtn_GuardarContinuar_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            Guardar();
            //Sesion.Redirige("WF_IncidenciasCargaMasiva.aspx?Parametros=" + Mensaje);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }

    }

    protected void WIBtn_Salir_Click(object sender, Infragistics.WebUI.WebDataInput.ButtonEventArgs e)
    {
        try
        {
            Sesion.Redirige("WF_AsistenciasEmp.aspx?Parametros=REGRESO");
            //if (Sesion.eClock_Agrupacion == "")
            //    Sesion.Redirige("WF_AsistenciasEmp.aspx?Parametros=REGRESO");
            //else
            //    Sesion.Redirige("WF_AsistenciasEmp.aspx?Parametros=AGRUPACION&Agrupacion=" + Sesion.eClock_Agrupacion, true);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
    }

    public string ObtenComentarios(int TipoIncidenciaRID)
    {
        string Ret = "";
        string[] Campos = CeC.ObtenArregoSeparador(CeC_Campos_Inc_R.ObtenCampos(TipoIncidenciaRID, Sesion), ",");
        if (Campos.Length <= 0)
            return Lbl_Mensaje_Error.Text;
        return CamposIncidencia.ObtenValores(TipoIncidenciaRID, "");
        /*        TipoIncidenciaRID
                foreach (string Campo in Campos)
                {
                    string NombreCampo = Campo.Trim();
                    Ret = CeC.AgregaSeparador(Ret, NombreCampo + "=" + CeC_Campos.ObtenValorCampo(ObtenCampo(NombreCampo)).ToString(), "&");
                }

                return Ret;*/
    }

    
}