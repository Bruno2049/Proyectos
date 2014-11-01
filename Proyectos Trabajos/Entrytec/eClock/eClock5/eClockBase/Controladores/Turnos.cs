using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Turnos
    {
        ES_Turnos.S_TurnosClient m_S_Turnos = null;
        CeC_SesionBase m_SesionBase = null;

        public Turnos(CeC_SesionBase SesionBase)
        {
            m_S_Turnos = new ES_Turnos.S_TurnosClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Turnos.svc"));
            m_SesionBase = SesionBase;
        }

        public delegate void ObtenDatosFinalizadoArgs(Modelos.Turnos.Model_Turno Turno);
        public event ObtenDatosFinalizadoArgs ObtenDatosFinalizado;
        /// <summary>
        /// Obtiene los datos de EC_Turnos
        /// </summary>
        /// <param name="TurnoID">ID del Turno</param>
        public void ObtenDatos(int TurnoID)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos");
            m_S_Turnos.ObtenDatosCompleted += m_S_Turnos_ObtenDatosCompleted;
            m_S_Turnos.ObtenDatosAsync(m_SesionBase.SESION_SEGURIDAD, TurnoID);

        }

        void m_S_Turnos_ObtenDatosCompleted(object sender, ES_Turnos.ObtenDatosCompletedEventArgs e)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Listo", 1);
                Modelos.Turnos.Model_Turno Turno = eClockBase.Controladores.CeC_ZLib.Json2Object<Modelos.Turnos.Model_Turno>(e.Result);
                if (ObtenDatosFinalizado != null)
                    ObtenDatosFinalizado(Turno);
            }
            catch (Exception ex)
            {
                m_SesionBase.MuestraMensaje("Error al cargar los datos", 10);
                CeC_Log.AgregaError(ex);
            }
        }

        public delegate void GuardadoArgs(bool Guardado);
        public event GuardadoArgs GuardadoEvent;

        public void Guardar(Modelos.Turnos.Model_Turno Turno)
        {
            string DatoTurno = JsonConvert.SerializeObject(Turno);
            m_SesionBase.MuestraMensaje("Guardando Datos");

            m_S_Turnos.GuardaDatosCompleted += m_S_Turnos_GuardaDatosCompleted;
            m_S_Turnos.GuardaDatosAsync(m_SesionBase.SESION_SEGURIDAD, DatoTurno);
        }

        void m_S_Turnos_GuardaDatosCompleted(object sender, ES_Turnos.GuardaDatosCompletedEventArgs e)
        {
            try
            {
                if (e.Result != "ERROR")
                {
                    m_SesionBase.MuestraMensaje("Guardado", 1);
                    if (GuardadoEvent != null)
                        GuardadoEvent(true);
                    return;
                }
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            if (GuardadoEvent != null)
                GuardadoEvent(false);
            m_SesionBase.MuestraMensaje("Error al Guardar", 10);
        }

        public delegate void AsignadoTurnoArgs(bool Guardado);
        public event AsignadoTurnoArgs AsignadoTurnoEvent;

        public void AsignarTurno(List<Modelos.Turnos.Model_TurnoImportacion> TurnoImportacion)
        {
            string DatoTurnoImportacion = JsonConvert.SerializeObject(TurnoImportacion);
            m_SesionBase.MuestraMensaje("Importando datos");

            m_S_Turnos.AsignacionTurnosAEmpleadosCompleted += m_S_Turnos_AsignacionTurnosAEmpleadosCompleted;
            m_S_Turnos.AsignacionTurnosAEmpleadosAsync(m_SesionBase.SESION_SEGURIDAD, DatoTurnoImportacion);
        }

        void m_S_Turnos_AsignacionTurnosAEmpleadosCompleted(object sender, ES_Turnos.AsignacionTurnosAEmpleadosCompletedEventArgs e)
        {
            try
            {
                if (e.Result > 0)
                {
                    m_SesionBase.MuestraMensaje("Guardados " + e.Result.ToString() + " registros.", 1);
                    if (GuardadoEvent != null)
                        GuardadoEvent(true);
                    return;
                }
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            if (GuardadoEvent != null)
                GuardadoEvent(false);
            m_SesionBase.MuestraMensaje("Error al Guardar", 5);
        }

        public delegate void AsignadoHorarioAPersonaDiarioIDsArgs(int NoGuardados);
        public event AsignadoHorarioAPersonaDiarioIDsArgs AsignadoHorarioAPersonaDiarioIDsEvent;

        public void AsignaHorarioAPersonaDiarioIDs(string Persona_Diario_IDs, int Turno_ID)
        {
            m_SesionBase.MuestraMensaje("Guardando Datos");
            m_S_Turnos.AsignaHorarioAPersonaDiarioIDsCompleted += m_S_Turnos_AsignaHorarioAPersonaDiarioIDsCompleted;
            m_S_Turnos.AsignaHorarioAPersonaDiarioIDsAsync(m_SesionBase.SESION_SEGURIDAD, Persona_Diario_IDs, Turno_ID);
        }

        void m_S_Turnos_AsignaHorarioAPersonaDiarioIDsCompleted(object sender, ES_Turnos.AsignaHorarioAPersonaDiarioIDsCompletedEventArgs e)
        {
            try
            {
                if (AsignadoHorarioAPersonaDiarioIDsEvent != null)
                    AsignadoHorarioAPersonaDiarioIDsEvent(e.Result);
                m_SesionBase.MuestraMensaje("Datos Guardados", 3);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            if (AsignadoHorarioAPersonaDiarioIDsEvent != null)
                AsignadoHorarioAPersonaDiarioIDsEvent(-1);
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

        public delegate void AsignaHorarioPredeterminadoAPersonaDiarioIDsArgs(int NoGuardados);
        public event AsignaHorarioPredeterminadoAPersonaDiarioIDsArgs AsignaHorarioPredeterminadoAPersonaDiarioIDsEvent;

        public void AsignaHorarioPredeterminadoAPersonaDiarioIDs(string Persona_Diario_IDs, int Turno_ID)
        {
            m_SesionBase.MuestraMensaje("Guardando Datos");
            m_S_Turnos.AsignaHorarioPredeterminadoAPersonaDiarioIDsCompleted += m_S_Turnos_AsignaHorarioPredeterminadoAPersonaDiarioIDsCompleted;
            m_S_Turnos.AsignaHorarioPredeterminadoAPersonaDiarioIDsAsync(m_SesionBase.SESION_SEGURIDAD, Persona_Diario_IDs, Turno_ID);
        }

        void m_S_Turnos_AsignaHorarioPredeterminadoAPersonaDiarioIDsCompleted(object sender, ES_Turnos.AsignaHorarioPredeterminadoAPersonaDiarioIDsCompletedEventArgs e)
        {
            try
            {

                if (AsignaHorarioPredeterminadoAPersonaDiarioIDsEvent != null)
                    AsignaHorarioPredeterminadoAPersonaDiarioIDsEvent(e.Result);
                m_SesionBase.MuestraMensaje("Datos Guardados", 3);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            if (AsignaHorarioPredeterminadoAPersonaDiarioIDsEvent != null)
                AsignaHorarioPredeterminadoAPersonaDiarioIDsEvent(-1);
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

    }
}
