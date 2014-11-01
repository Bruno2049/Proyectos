using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace eClockBase.Controladores
{
    public class Incidencias
    {
        ES_Incidencias.S_IncidenciasClient m_S_Incidencias = null;
        CeC_SesionBase m_SesionBase = null;

        public Incidencias(CeC_SesionBase SesionBase)
        {
            m_S_Incidencias = new ES_Incidencias.S_IncidenciasClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Incidencias.svc"));
            m_SesionBase = SesionBase;
        }

        public delegate void CargarIncidenciasFinalizadoArgs(bool Guardado);
        public event CargarIncidenciasFinalizadoArgs CargarIncidenciasEvent;

        public void CargarIncidencias(List<eClockBase.Modelos.Incidencias.Model_Incidencias> IncidenciasAgregar)
        {
            string DatosIncidencias = JsonConvert.SerializeObject(IncidenciasAgregar);
            m_SesionBase.MuestraMensaje("Agregando incidencias");
            m_S_Incidencias.CargaIncidenciasCompleted += m_S_Incidencias_CargaIncidenciasCompleted;
            m_S_Incidencias.CargaIncidenciasAsync(m_SesionBase.SESION_SEGURIDAD, DatosIncidencias);
        }

        void m_S_Incidencias_CargaIncidenciasCompleted(object sender, ES_Incidencias.CargaIncidenciasCompletedEventArgs e)
        {
            try
            {
                if (e.Result == null)
                {
                    m_SesionBase.MuestraMensaje("Guardado", 1);
                    if (CargarIncidenciasEvent != null)
                        CargarIncidenciasEvent(true);
                    return;
                }
                else
                {

                }
            }
            catch { m_SesionBase.MuestraMensaje("Error al guardar", 5); }

            if (CargarIncidenciasEvent != null)
                CargarIncidenciasEvent(false);
            m_SesionBase.MuestraMensaje("Error al Guardar", 10);
        }

        public delegate void AsignaIncidenciaPersonasDiarioArgs(int NoGuardados);
        public event AsignaIncidenciaPersonasDiarioArgs AsignaIncidenciaPersonasDiarioEvent;

        public void AsignaIncidenciaPersonasDiario(int TipoIncidenciaID, string PersonasDiariosIDs, string IncidenciaComentario)
        {
            m_SesionBase.MuestraMensaje("Guardando Datos");
            m_S_Incidencias.AsignaIncidenciaPersonasDiarioCompleted += m_S_Incidencias_AsignaIncidenciaPersonasDiarioCompleted;
            m_S_Incidencias.AsignaIncidenciaPersonasDiarioAsync(m_SesionBase.SESION_SEGURIDAD, TipoIncidenciaID, PersonasDiariosIDs, IncidenciaComentario);
        }

        void m_S_Incidencias_AsignaIncidenciaPersonasDiarioCompleted(object sender, ES_Incidencias.AsignaIncidenciaPersonasDiarioCompletedEventArgs e)
        {
            try
            {
                if (AsignaIncidenciaPersonasDiarioEvent != null)
                    AsignaIncidenciaPersonasDiarioEvent(e.Result);
                m_SesionBase.MuestraMensaje("Datos Guardados", 1);
            }
            catch { m_SesionBase.MuestraMensaje("Error al guardar"); }

        }



        public delegate void StatusVacacionesArgs(Modelos.Incidencias.Model_Vacaciones Status);
        public event StatusVacacionesArgs StatusVacacionesEvent;

        public void StatusVacaciones()
        {
            m_SesionBase.MuestraMensaje("Obteniendo status...");
            m_S_Incidencias.StatusVacacionesCompleted += m_S_Incidencias_StatusVacacionesCompleted;
            m_S_Incidencias.StatusVacacionesAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.Mdl_Sesion.PERSONA_ID);

        }

        void m_S_Incidencias_StatusVacacionesCompleted(object sender, ES_Incidencias.StatusVacacionesCompletedEventArgs e)
        {
            try
            {
                switch (e.Result)
                {
                    case null:
                        m_SesionBase.MuestraMensaje("Error Desconocido", 5);
                        break;
                    case "ERROR":
                        m_SesionBase.MuestraMensaje("Error Desconocido 2", 5);
                        break;
                    case "SIN_INCIDENCIA_VACACIONES":
                        m_SesionBase.MuestraMensaje("no se ha configurado la incidencia de vacaciones", 5);
                        break;
                    case "SIN_INCIDENCIA_REGLA":
                        m_SesionBase.MuestraMensaje("no se ha creado una regla de vacaciones valida", 5);
                        break;

                    default:
                        m_SesionBase.MuestraMensaje("Listo", 3);
                        if (StatusVacacionesEvent != null)
                            StatusVacacionesEvent(eClockBase.Controladores.CeC_ZLib.Json2Object<Modelos.Incidencias.Model_Vacaciones>(e.Result));
                        break;
                }
                return;

            }
            catch
            {
            }
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

        public delegate void StatusDiasArgs(List<eClockBase.Modelos.Incidencias.Model_StatusDia> StatusDias);
        public event StatusDiasArgs StatusDiasEvent;

        public void StatusDias(DateTime FechaInicio, DateTime FechaFinal, string Filtro)
        {
            m_SesionBase.MuestraMensaje("Obteniendo status...");
            m_S_Incidencias.StatusDiasCompleted += m_S_Incidencias_StatusDiasCompleted;
            m_S_Incidencias.StatusDiasAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.Mdl_Sesion.PERSONA_ID, FechaInicio, FechaFinal, Filtro);

        }

        void m_S_Incidencias_StatusDiasCompleted(object sender, ES_Incidencias.StatusDiasCompletedEventArgs e)
        {
            try
            {
                switch (e.Result)
                {
                    case null:
                        m_SesionBase.MuestraMensaje("Error Desconocido", 5);
                        break;
                    case "ERROR":
                        m_SesionBase.MuestraMensaje("Error Desconocido 2", 5);
                        break;
                    default:
                        m_SesionBase.MuestraMensaje("Listo", 3);
                        if (StatusDiasEvent != null)
                            StatusDiasEvent(eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Incidencias.Model_StatusDia>>(e.Result));
                        break;
                }
                return;

            }
            catch
            {
            }
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }


        public delegate void SolicitaVacacionesArgs(string Solicitados);
        public event SolicitaVacacionesArgs SolicitaVacacionesEvent;

        public void SolicitaVacaciones(List<DateTime> Fechas, string Comentario)
        {
            string FechasJson = JsonConvert.SerializeObject(Fechas);
            m_SesionBase.MuestraMensaje("Solicitando Vacaciones");

            m_S_Incidencias.AsignaIncidenciaPersonasDiarioCompleted += m_S_Incidencias_AsignaIncidenciaPersonasDiarioCompleted;
            m_S_Incidencias.SolicitaVacacionesCompleted += m_S_Incidencias_SolicitaVacacionesCompleted;
            m_S_Incidencias.SolicitaVacacionesAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.Mdl_Sesion.PERSONA_ID, FechasJson, Comentario);
        }

        void m_S_Incidencias_SolicitaVacacionesCompleted(object sender, ES_Incidencias.SolicitaVacacionesCompletedEventArgs e)
        {
            try
            {
                switch (e.Result)
                {
                    case null:
                        m_SesionBase.MuestraMensaje("Error Desconocido", 5);
                        break;
                    case "ERROR":
                        m_SesionBase.MuestraMensaje("Error Desconocido 2", 5);
                        break;
                    case "SIN_INCIDENCIA_VACACIONES":
                        m_SesionBase.MuestraMensaje("no se ha configurado la incidencia de vacaciones", 5);
                        break;
                    case "NO_TIENE_SALDO":
                        m_SesionBase.MuestraMensaje("no tiene saldo suficiente de vacaciones", 5);
                        break;
                    case "OK":
                        m_SesionBase.MuestraMensaje("Solicitud enviada satisfactoriamente", 3);
                        break;
                    case "NOK":
                        m_SesionBase.MuestraMensaje("Solicitud no enviada", 5);
                        break;
                    default:
                        m_SesionBase.MuestraMensaje("Error Desconocido 3", 5);
                        break;
                }
                if (SolicitaVacacionesEvent != null)
                    SolicitaVacacionesEvent(e.Result);
                return;
            }
            catch
            {
            }
            if (SolicitaVacacionesEvent != null)
                SolicitaVacacionesEvent("ERROR_RED");
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }


        ///************************************
        public delegate void SolicitaIncidenciaArgs(string Solicitados);
        public event SolicitaIncidenciaArgs SolicitaIncidenciaEvent;

        public void SolicitaIncidencia(List<DateTime> Fechas, string Comentario, int TipoIncidenciaID)
        {
            string FechasJson = JsonConvert.SerializeObject(Fechas);
            m_SesionBase.MuestraMensaje("Solicitando Incidencia");
            m_S_Incidencias.SolicitaIncidenciaCompleted += m_S_Incidencias_SolicitaIncidenciaCompleted;
            m_S_Incidencias.SolicitaIncidenciaAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.Mdl_Sesion.PERSONA_ID, FechasJson, TipoIncidenciaID, Comentario);
        }

        void m_S_Incidencias_SolicitaIncidenciaCompleted(object sender, ES_Incidencias.SolicitaIncidenciaCompletedEventArgs e)
        {
            try
            {
                switch (e.Result)
                {
                    case null:
                        m_SesionBase.MuestraMensaje("Error Desconocido", 5);
                        break;
                    case "ERROR":
                        m_SesionBase.MuestraMensaje("Error Desconocido 2", 5);
                        break;
                    case "NO_TIENE_SALDO":
                        m_SesionBase.MuestraMensaje("no tiene saldo suficiente para Incidencias", 5);
                        break;
                    case "OK":
                        m_SesionBase.MuestraMensaje("Solicitud enviada satisfactoriamente", 3);
                        break;
                    case "NOK":
                        m_SesionBase.MuestraMensaje("Solicitud no enviada", 5);
                        break;
                    default:
                        m_SesionBase.MuestraMensaje("Error Desconocido 3", 5);
                        break;
                }
                if (SolicitaIncidenciaEvent != null)
                    SolicitaIncidenciaEvent(e.Result);
                return;
            }
            catch
            {
            }
            if (SolicitaIncidenciaEvent != null)
                SolicitaIncidenciaEvent("ERROR_RED");
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }
        ///************************************
        public delegate void StatusHorasArgs(List<eClockBase.Modelos.Incidencias.Model_StatusHoras> Status);
        public event StatusHorasArgs StatusHorasEvent;

        public void StatusHoras(string PersonasDiariosIDs)
        {
            m_SesionBase.MuestraMensaje("Obteniendo status de Horas...");
            m_S_Incidencias.StatusHorasCompleted += m_S_Incidencias_StatusHorasCompleted;
            m_S_Incidencias.StatusHorasAsync(m_SesionBase.SESION_SEGURIDAD, PersonasDiariosIDs);
        }

        void m_S_Incidencias_StatusHorasCompleted(object sender, ES_Incidencias.StatusHorasCompletedEventArgs e)
        {
            try
            {
                if (StatusHorasEvent != null)
                    StatusHorasEvent(eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Incidencias.Model_StatusHoras>>(e.Result));
                m_SesionBase.MuestraMensaje("Status Obtenido", 3);
                return;
            }
            catch
            {
            }
            if (StatusHorasEvent != null)
                StatusHorasEvent(null);
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }

        ///************************************
        public delegate void StatusReglaArgs(List<eClockBase.Modelos.Incidencias.Model_StatusRegla> Status);
        public event StatusReglaArgs StatusReglaEvent;

        public void StatusRegla(int TipoIncidenciaID, string PersonasDiariosIDs)
        {
            m_SesionBase.MuestraMensaje("Obteniendo status de Horas...");
            m_S_Incidencias.StatusReglaCompleted += m_S_Incidencias_StatusReglaCompleted;
            m_S_Incidencias.StatusReglaAsync(m_SesionBase.SESION_SEGURIDAD, TipoIncidenciaID, PersonasDiariosIDs);
        }

        void m_S_Incidencias_StatusReglaCompleted(object sender, ES_Incidencias.StatusReglaCompletedEventArgs e)
        {

            try
            {
                if (StatusReglaEvent != null)
                    StatusReglaEvent(eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Incidencias.Model_StatusRegla>>(e.Result));
                m_SesionBase.MuestraMensaje("Status Obtenido", 3);
                return;
            }
            catch
            {
            }
            if (StatusReglaEvent != null)
                StatusReglaEvent(null);
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }

        ///************************************
        public delegate void StatusReglaHorasArgs(eClockBase.Modelos.Incidencias.Model_StatusRegla Status);
        public event StatusReglaHorasArgs StatusReglaHorasEvent;

        public void StatusReglaHoras(int TipoIncidenciaID, int PersonaDiarioID, decimal Horas)
        {
            m_SesionBase.MuestraMensaje("Obteniendo status de Horas...");
            m_S_Incidencias.StatusReglaHorasCompleted += m_S_Incidencias_StatusReglaHorasCompleted;
            m_S_Incidencias.StatusReglaHorasAsync(m_SesionBase.SESION_SEGURIDAD, TipoIncidenciaID, PersonaDiarioID, Horas);
        }

        void m_S_Incidencias_StatusReglaHorasCompleted(object sender, ES_Incidencias.StatusReglaHorasCompletedEventArgs e)
        {
            try
            {
                if (StatusReglaHorasEvent != null)
                    StatusReglaHorasEvent(eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Incidencias.Model_StatusRegla>(e.Result));
                m_SesionBase.MuestraMensaje("Status Obtenido", 3);
                return;
            }
            catch
            {
            }
            if (StatusReglaHorasEvent != null)
                StatusReglaHorasEvent(null);
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }

        //**********************************************************************************************************
        public delegate void ObtenerSaldosArgs(List<eClockBase.Modelos.Incidencias.Model_SaldosTiposIncidenciasR> Saldos);
        public event ObtenerSaldosArgs ObtenerSaldosFinalizado;

        public void ObtenerSaldos(int PERSONA_ID, string Agrupacion, string TiposIncidenciasIDS)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos");
            m_S_Incidencias.ObtenerSaldosCompleted += m_S_Incidencias_ObtenerSaldosCompleted;
            //m_S_Asistencias.ObtenAsistenciaLinealNAsync(m_SesionBase.SESION_SEGURIDAD, PersonaDiarioInicio, PersonaDiarioFin, m_SesionBase.IDIOMA);            
            m_S_Incidencias.ObtenerSaldosAsync(m_SesionBase.SESION_SEGURIDAD, PERSONA_ID, Agrupacion, TiposIncidenciasIDS);
        }

        void m_S_Incidencias_ObtenerSaldosCompleted(object sender, ES_Incidencias.ObtenerSaldosCompletedEventArgs e)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Listo", 3);
                string Resultado = e.Result;
                List<eClockBase.Modelos.Incidencias.Model_SaldosTiposIncidenciasR> SaldosIncidencias = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Incidencias.Model_SaldosTiposIncidenciasR>>(Resultado);
                if (ObtenerSaldosFinalizado != null)
                {

                    ObtenerSaldosFinalizado(SaldosIncidencias);
                }
            }
            catch { m_SesionBase.MuestraMensaje("No se puede mostrar resultados", 5); }
        }

        public delegate void EnviaANominaArgs(bool Resultado);
        public event EnviaANominaArgs EnviaANominaFinalizado;
        public void EnviaANomina(int PeriodoID)
        {
            m_SesionBase.MuestraMensaje("Enviando a nomina");
            m_S_Incidencias.EnviaANominaCompleted += m_S_Incidencias_EnviaANominaCompleted;
            m_S_Incidencias.EnviaANominaAsync(m_SesionBase.SESION_SEGURIDAD, PeriodoID, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA);
        }

        void m_S_Incidencias_EnviaANominaCompleted(object sender, ES_Incidencias.EnviaANominaCompletedEventArgs e)
        {
            bool R = false;
            try
            {
                if (e.Result)
                {
                    R = true;
                    m_SesionBase.MuestraMensaje("Se ha enviado a nomina satisfactoriamente", 3);
                }
                else
                {
                    m_SesionBase.MuestraMensaje("No se pudo enviar a nomina", 5);
                }

            }
            catch { m_SesionBase.MuestraMensaje("Error de red", 5); }
            if (EnviaANominaFinalizado != null)
                EnviaANominaFinalizado(R);
        }

        //**********************************************************************************************************
        public delegate void ObtenerHistorialArgs(List<eClockBase.Modelos.Incidencias.Model_Historial> Historial);
        public event ObtenerHistorialArgs ObtenerHistorialFinalizado;

        public void ObtenerHistorial(int PERSONA_ID, string Agrupacion, DateTime FechaInicio, DateTime FechaFin, string TiposIncidenciasIDS)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos");
            m_S_Incidencias.ObtenerHistorialCompleted += m_S_Incidencias_ObtenerHistorialCompleted;
            m_S_Incidencias.ObtenerHistorialAsync(m_SesionBase.SESION_SEGURIDAD, PERSONA_ID, Agrupacion, FechaInicio, FechaFin, TiposIncidenciasIDS);
        }

        void m_S_Incidencias_ObtenerHistorialCompleted(object sender, ES_Incidencias.ObtenerHistorialCompletedEventArgs e)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Listo", 3);
                string Resultado = e.Result;
                List<eClockBase.Modelos.Incidencias.Model_Historial> Historial = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Incidencias.Model_Historial>>(Resultado);
                if (ObtenerHistorialFinalizado != null)
                {
                    ObtenerHistorialFinalizado(Historial);
                }
            }
            catch (Exception ex)
            {
                m_SesionBase.MuestraMensaje("No se puede mostrar resultados", 5);
                CeC_Log.AgregaError(ex);
            }
        }

        public delegate void CorrigeMovimientoInventarioArgs(int Resultado);
        public event CorrigeMovimientoInventarioArgs CorrigeMovimientoInventarioFinalizado;

        public void CorrigeMovimientoInventario(int Almacen_IncID)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos");
            m_S_Incidencias.CorrigeMovimientoInventarioCompleted += m_S_Incidencias_CorrigeMovimientoInventarioCompleted;
            m_S_Incidencias.CorrigeMovimientoInventarioAsync(m_SesionBase.SESION_SEGURIDAD, Almacen_IncID);
        }

        void m_S_Incidencias_CorrigeMovimientoInventarioCompleted(object sender, ES_Incidencias.CorrigeMovimientoInventarioCompletedEventArgs e)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Listo", 3);
                if (CorrigeMovimientoInventarioFinalizado != null)
                {
                    CorrigeMovimientoInventarioFinalizado(e.Result);
                }
            }
            catch (Exception ex)
            {
                m_SesionBase.MuestraMensaje("No se puede mostrar resultados", 5);
                CeC_Log.AgregaError(ex);
            }
        }

    }
}