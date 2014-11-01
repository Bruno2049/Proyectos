using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Modelos.Asistencias;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    /// <summary>
    /// Controlador para la vista de asistencias, se incluyen las funciones que interactuaran con el services y devolveran los datos a la vista
    /// </summary>
    public class Asistencias
    {
        /// <summary>
        /// Este Objeto sera el encargado de la gestion de client con eClockServices
        /// </summary>
        ES_Asistencias.S_AsistenciasClient m_S_Asistencias = null;

        /// <summary>
        /// En este se encargara de almacenar el SesionBase de el usuario
        /// </summary>
        CeC_SesionBase m_SesionBase = null;

        /// <summary>
        /// Este es el constructos de el controlador aqui se inicializara la sesion y la conexion de S_Asistencias.svc
        /// </summary>
        /// <param name="SesionBase">El SesionBase se encargara de almacenar y gestionar los datos del usuario</param>
        public Asistencias(CeC_SesionBase SesionBase)
        {
            m_S_Asistencias = new ES_Asistencias.S_AsistenciasClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Asistencias.svc"));
            m_SesionBase = SesionBase;
        }
        /// <summary>
        /// Se crea el delegado ObtenAsistenciaFinalizadoArgs que recibe como parametro una lista de Model_Asistencia
        /// </summary>        
        public delegate void ObtenAsistenciaFinalizadoArgs(List<Model_Asistencia> Asistencias);
        
        /// <summary>
        /// Se crea el evento del delegado ObtenAsistenciaFinalizadoArgs
        /// </summary>
        public event ObtenAsistenciaFinalizadoArgs EventObtenAsistenciaFinalizado;

        /// <summary>
        /// Funcion que se conectara al Services para obtener la asistencia
        /// </summary>
        /// <param name="EntradaSalida">En este se debe indicar si se requiere la Hora de salida</param>
        /// <param name="Comida">Indica si se requiere las checadas en comida </param>
        /// <param name="HorasExtras">Indica si se requiere informacion de horas extra</param>
        /// <param name="Totales">Indica si se requiere totales </param>
        /// <param name="Incidencia">Indica si se requieren las incidencias</param>
        /// <param name="TurnoDia">Indica si se requiere el turno del dia</param>
        /// <param name="TiposIncidenciasSistemaIDs">Envia los id de las incidencias de el sistema</param>
        /// <param name="TiposIncidenciasIDs">Envia los id de las incidencias</param>
        /// <param name="MuestraAgrupacion">Envia si se requiere La agrupacion</param>
        /// <param name="MuestraEmpleado">Indica si se requiere el empleado</param>
        /// <param name="Persona_ID">Indica que se necesita el Persona_ID de el </param>
        /// <param name="Agrupacion"></param>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        public void ObtenAsistencia(bool EntradaSalida, bool Comida, bool HorasExtras, bool Totales, bool Incidencia, bool TurnoDia, string TiposIncidenciasSistemaIDs, 
            string TiposIncidenciasIDs, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
        {
            //Se le asigna al evento m_S_Asistencias.ObtenAsistenciaCompleted el delegado m_S_Asistencias_ObtenAsistenciaCompleted
            m_S_Asistencias.ObtenAsistenciaCompleted += m_S_Asistencias_ObtenAsistenciaCompleted;
            //Se manda a llamar el metodo OntenAsistenciasAsync del services para que se cargue de forma asincrona enviandoles los paramatros que solicite
            m_S_Asistencias.ObtenAsistenciaAsync(m_SesionBase.SESION_SEGURIDAD, EntradaSalida, Comida, HorasExtras, Totales, Incidencia, TurnoDia, TiposIncidenciasSistemaIDs, TiposIncidenciasIDs, MuestraAgrupacion, MuestraEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal);
        }
        //Aqui inicia el evento m_S_Asistencias_ObtenAsistenciaCompleted son el sender o enviador y el objeto e con la lista
        void m_S_Asistencias_ObtenAsistenciaCompleted(object sender, ES_Asistencias.ObtenAsistenciaCompletedEventArgs e)
        {
            //Se obtiene la cadena resultado que contrndra el Json
            string Resultado = e.Result;
            //Se Deserealiza el el resultado y se convierte en en una lista
            List<Model_Asistencia> Asistencias = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Model_Asistencia>>(Resultado);
            //Se comprueba el evento que sea diferente de nulo
            if (EventObtenAsistenciaFinalizado != null)
                //Si es diferente se envia el finalizado
                EventObtenAsistenciaFinalizado(Asistencias);
        }

        public delegate void ObtenAsistenciaTotalesSaldosArgs(List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos> Asistencia);
        public event ObtenAsistenciaTotalesSaldosArgs ObtenAsistenciaTotalesSaldosFinalizado;

        public void ObtenAsistenciaTotalesSaldos(bool MostrarAgrupacion, bool MostrarEmpleado, int Persona_ID, string Agrupacion)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos...");
            m_S_Asistencias.ObtenAsistenciaTotalesSaldosCompleted += m_S_Asistencias_ObtenAsistenciaTotalesSaldosCompleted;
            m_S_Asistencias.ObtenAsistenciaTotalesSaldosAsync(m_SesionBase.SESION_SEGURIDAD, MostrarAgrupacion, MostrarEmpleado, Persona_ID, Agrupacion, DateTime.Now, DateTime.Now);
        }

        void m_S_Asistencias_ObtenAsistenciaTotalesSaldosCompleted(object sender, ES_Asistencias.ObtenAsistenciaTotalesSaldosCompletedEventArgs e)
        {

            try
            {
                m_SesionBase.MuestraMensaje("Listo", 5);
                string Resultado = e.Result;
                List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos> Asistencias = Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotalesSaldos>>(Resultado);
                if (ObtenAsistenciaTotalesSaldosFinalizado != null)
                {
                    //m_SesionBase.MuestraMensaje("Cargando Datos", 10);
                    ObtenAsistenciaTotalesSaldosFinalizado(Asistencias);
                }
            }

            catch { m_SesionBase.MuestraMensaje("No se puede mostrar resultados", 5); }
        }

        public delegate void ObtenAsistenciaTotalesArgs(List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotales> Asistencia);
        public event ObtenAsistenciaTotalesArgs ObtenAsistenciaTotalesFinalizado;

        public void ObtenAsistenciaTotales(bool MostrarAgrupacion, bool MostrarEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos...");
            m_S_Asistencias.ObtenAsistenciaTotalesCompleted += m_S_Asistencias_ObtenAsistenciaTotalesCompleted;
            m_S_Asistencias.ObtenAsistenciaTotalesAsync(m_SesionBase.SESION_SEGURIDAD, MostrarAgrupacion, MostrarEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal);
        }

        void m_S_Asistencias_ObtenAsistenciaTotalesCompleted(object sender, ES_Asistencias.ObtenAsistenciaTotalesCompletedEventArgs e)
        {


            try
            {
                m_SesionBase.MuestraMensaje("Listo", 5);
                string Resultado = e.Result;
                List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotales> Asistencias = Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Asistencias.Model_AsistenciaTotales>>(Resultado);
                if (ObtenAsistenciaTotalesFinalizado != null)
                {
                    //m_SesionBase.MuestraMensaje("Cargando Datos", 10);
                    ObtenAsistenciaTotalesFinalizado(Asistencias);
                }
            }

            catch { m_SesionBase.MuestraMensaje("No se puede mostrar resultados", 5); }
        }

        public delegate void ObtenTiemposArgs(eClockBase.Modelos.Asistencias.Model_Tiempos Tiempos);
        public event ObtenTiemposArgs ObtenTiemposFinalizado;

        public void ObtenTiempos(int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos...");
            m_S_Asistencias.ObtenTiemposCompleted += m_S_Asistencias_ObtenTiemposCompleted;
            m_S_Asistencias.ObtenTiemposAsync(m_SesionBase.SESION_SEGURIDAD, Persona_ID, Agrupacion, FechaInicial, FechaFinal);
        }

        void m_S_Asistencias_ObtenTiemposCompleted(object sender, ES_Asistencias.ObtenTiemposCompletedEventArgs e)
        {

            try
            {
                m_SesionBase.MuestraMensaje("Listo", 5);
                string Resultado = e.Result;
                eClockBase.Modelos.Asistencias.Model_Tiempos Tiempos = Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Asistencias.Model_Tiempos>(Resultado);
                if (ObtenTiemposFinalizado != null)
                {
                    //m_SesionBase.MuestraMensaje("Cargando Datos", 10);
                    ObtenTiemposFinalizado(Tiempos);
                }
            }

            catch { m_SesionBase.MuestraMensaje("No se puede mostrar resultados", 5); }
        }

        public delegate void ObtenAsistenciaLinealV5Args(List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_V5> Asistencia);
        public event ObtenAsistenciaLinealV5Args ObtenAsistenciaLinealV5Finalizado;

        public void ObtenAsistenciaLinealV5(int PERSONA_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string OrdenarPor, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos...");
            m_S_Asistencias.ObtenAsistenciaLinealV5Completed += m_S_Asistencias_ObtenAsistenciaLinealV5Completed;
            m_S_Asistencias.ObtenAsistenciaLinealV5Async(m_SesionBase.SESION_SEGURIDAD, PERSONA_ID, Agrupacion, FechaInicial, FechaFinal, OrdenarPor, TiposIncidenciasSistemaIDs, TiposIncidenciasIDs, m_SesionBase.IDIOMA);
        }

        void m_S_Asistencias_ObtenAsistenciaLinealV5Completed(object sender, ES_Asistencias.ObtenAsistenciaLinealV5CompletedEventArgs e)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Listo", 5);
                string Resultado = e.Result;
                List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_V5> Asistencias = Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_V5>>(Resultado);
                if (ObtenAsistenciaLinealV5Finalizado != null)
                {
                    //m_SesionBase.MuestraMensaje("Cargando Datos", 10);
                    ObtenAsistenciaLinealV5Finalizado(Asistencias);
                }
            }

            catch { m_SesionBase.MuestraMensaje("No se puede mostrar resultados", 5); }
        }


        public delegate void ObtenAsistenciaLinealNArgs(List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N> Asistencia);
        public event ObtenAsistenciaLinealNArgs EventoObtenAsistenciaLinealNFinalizado;

        public void ObtenAsistenciaLinealN(int PersonaDiarioInicio, int PersonaDiarioFin)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos...");
            m_S_Asistencias.ObtenAsistenciaLinealNCompleted += m_S_Asistencias_ObtenAsistenciaLinealNCompleted;
            m_S_Asistencias.ObtenAsistenciaLinealNAsync(m_SesionBase.SESION_SEGURIDAD, PersonaDiarioInicio, PersonaDiarioFin, m_SesionBase.IDIOMA);
        }

        void m_S_Asistencias_ObtenAsistenciaLinealNCompleted(object sender, ES_Asistencias.ObtenAsistenciaLinealNCompletedEventArgs e)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Listo", 5);
                string Resultado = e.Result;
                List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N> Asistencias = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Asistencias.Model_Asistencia_Lineal_N>>(Resultado);
                if (EventoObtenAsistenciaLinealNFinalizado != null)
                {
                    //m_SesionBase.MuestraMensaje("Cargando Datos", 10);
                    EventoObtenAsistenciaLinealNFinalizado(Asistencias);
                }
                return;
            }

            catch { m_SesionBase.MuestraMensaje("No se puede mostrar resultados", 5); }
            if (EventoObtenAsistenciaLinealNFinalizado != null)
            {
                EventoObtenAsistenciaLinealNFinalizado(null);
            }
        }
        //*****************************************************************************************************************************
        public delegate void ObtenAsistenciaLinealArgs(List<eClockBase.Modelos.Asistencias.Model_Asistencia> Asistencia);
        public event ObtenAsistenciaLinealArgs ObtenAsistenciaLinealFinalizado;

        public void ObtenAsistenciaLineal(int PERSONA_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string Campos, string OrdenarPor, string TiposIncidenciasSistemaIDs, string TiposIncidenciasIDs)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos");
            m_S_Asistencias.ObtenAsistenciaLinealCompleted += m_S_Asistencias_ObtenAsistenciaLinealCompleted;
            //m_S_Asistencias.ObtenAsistenciaLinealNAsync(m_SesionBase.SESION_SEGURIDAD, PersonaDiarioInicio, PersonaDiarioFin, m_SesionBase.IDIOMA);
            m_S_Asistencias.ObtenAsistenciaLinealAsync(m_SesionBase.SESION_SEGURIDAD, PERSONA_ID, Agrupacion, FechaInicial, FechaFinal, Campos, OrdenarPor, TiposIncidenciasSistemaIDs, TiposIncidenciasIDs);
        }

        void m_S_Asistencias_ObtenAsistenciaLinealCompleted(object sender, ES_Asistencias.ObtenAsistenciaLinealCompletedEventArgs e)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Listo", 5);
                string Resultado = e.Result;
                List<eClockBase.Modelos.Asistencias.Model_Asistencia> Asistencias = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Asistencias.Model_Asistencia>>(Resultado);
                if (ObtenAsistenciaLinealFinalizado != null)
                {
                    //m_SesionBase.MuestraMensaje("Cargando Datos", 10);
                    ObtenAsistenciaLinealFinalizado(Asistencias);
                }
            }

            catch { m_SesionBase.MuestraMensaje("No se puede mostrar resultados", 5); }
        }
        //****************************************************************************************************************************
        //*************************************************************************************
        public delegate void ObtenAsistenciaHorizontalArgs(List<object> AsistenciaHorizontal);
        public event ObtenAsistenciaHorizontalArgs EventoObtenAsistenciaHorizontalFinalizado;

        public void ObtenAsistenciaHorizontal(bool EntradaSalida, bool TurnoDia, bool IncidenciaAbr, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
        {
            m_S_Asistencias.ObtenAsistenciaHorizontalCompleted += m_S_Asistencias_ObtenAsistenciaHorizontalCompleted;
            m_S_Asistencias.ObtenAsistenciaHorizontalAsync(m_SesionBase.SESION_SEGURIDAD, EntradaSalida, TurnoDia, IncidenciaAbr, MuestraAgrupacion, MuestraEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal);

        }

        public delegate void ObtenAsistenciaHorizontalNAbr31Args(List<eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31> AsistenciaHorizontalN);
        public event ObtenAsistenciaHorizontalNAbr31Args ObtenAsistenciaHorizontalNAbr31Finalizado;

        public void ObtenAsistenciaHorizontalN(bool EntradaSalida, bool TurnoDia, bool IncidenciaAbr, bool ColorTurno, bool ColorIncidencia, bool MuestraAgrupacion, bool MuestraEmpleado, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Cargando Datos");
                m_S_Asistencias.ObtenAsistenciaHorizontalNCompleted += m_S_Asistencias_ObtenAsistenciaHorizontalNCompleted;
                if (FechaFinal - FechaInicial > new TimeSpan(31, 0, 0, 0))
                {
                    FechaFinal = FechaInicial + new TimeSpan(31, 0, 0, 0);
                    m_SesionBase.MuestraMensaje("Solo se mostrarán 31 días");
                }
                m_S_Asistencias.ObtenAsistenciaHorizontalNAsync(m_SesionBase.SESION_SEGURIDAD, EntradaSalida, TurnoDia, IncidenciaAbr, ColorTurno, ColorIncidencia, MuestraAgrupacion, MuestraEmpleado, Persona_ID, Agrupacion, FechaInicial, FechaFinal, m_SesionBase.IDIOMA);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                m_SesionBase.MuestraMensaje("Error al obtener datos", 10);
            }
        }

        void m_S_Asistencias_ObtenAsistenciaHorizontalNCompleted(object sender, ES_Asistencias.ObtenAsistenciaHorizontalNCompletedEventArgs e)
        {
            try
            {
                string Resultado = e.Result;

                if (EventoObtenAsistenciaHorizontalFinalizado != null)
                {
                    List<object> Asistencias = eClockBase.Controladores.CeC_ZLib.Json2Object<List<object>>(Resultado);
                    EventoObtenAsistenciaHorizontalFinalizado(Asistencias);
                }
                if (ObtenAsistenciaHorizontalNAbr31Finalizado != null)
                {
                    ObtenAsistenciaHorizontalNAbr31Finalizado(Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Asistencias.Reporte_AsistenciaAbr31>>(Resultado));
                }
                m_SesionBase.MuestraMensaje("Listo", 3);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                m_SesionBase.MuestraMensaje("Error al obtener datos", 10);
            }
            if (ObtenAsistenciaHorizontalNAbr31Finalizado != null)
                ObtenAsistenciaHorizontalNAbr31Finalizado(null);
        }


        void m_S_Asistencias_ObtenAsistenciaHorizontalCompleted(object sender, ES_Asistencias.ObtenAsistenciaHorizontalCompletedEventArgs e)
        {
            string Resultado = e.Result;
            List<object> Asistencias = eClockBase.Controladores.CeC_ZLib.Json2Object<List<object>>(Resultado);
            if (EventoObtenAsistenciaHorizontalFinalizado != null)
                EventoObtenAsistenciaHorizontalFinalizado(Asistencias);
        }

        public delegate void ObtenAsistenciaHEArgs(List<Model_HorasExtra> AsistenciaHE);
        public event ObtenAsistenciaHEArgs EventoObtenAsistenciaHE;

        public void ObtenAsistenciaHE(bool EntradaSalida, bool Comida, bool Totales, bool Incidencia, bool TurnoDia, bool MuestraAgrupacion, bool MuestraEmpleado, bool MuestraCeros, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Horas Extras");
            m_S_Asistencias.ObtenHorasExtrasCompleted += m_S_Asistencias_ObtenHorasExtrasCompleted;
            m_S_Asistencias.ObtenHorasExtrasAsync(m_SesionBase.SESION_SEGURIDAD, EntradaSalida, Comida, Totales, Incidencia, TurnoDia, MuestraAgrupacion, MuestraEmpleado, MuestraCeros, Persona_ID, Agrupacion, FechaInicial, FechaFinal);
        }

        void m_S_Asistencias_ObtenHorasExtrasCompleted(object sender, ES_Asistencias.ObtenHorasExtrasCompletedEventArgs e)
        {
            try
            {
                string Resultado = e.Result;
                m_SesionBase.MuestraMensaje("Listo", 3);
                List<Model_HorasExtra> AsitenciasHorasE = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Model_HorasExtra>>(Resultado);
                if (EventoObtenAsistenciaHE != null)
                    EventoObtenAsistenciaHE(AsitenciasHorasE);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                m_SesionBase.MuestraMensaje("Error al obtener datos", 5);
            }
        }

        public delegate void AplicaHorasExtrasArgs(List<int> PERSONA_D_HE_IDs);
        public event AplicaHorasExtrasArgs AplicaHorasExtrasEvent;
        public void AplicaHorasExtras(List<Modelos.Asistencias.Model_AplicaHorasExtras> ParametrosHorasExtras)
        {
            m_SesionBase.MuestraMensaje("Aplicando Horas Extras");
            m_S_Asistencias.AplicaHorasExtrasCompleted += m_S_Asistencias_AplicaHorasExtrasCompleted;
            m_S_Asistencias.AplicaHorasExtrasAsync(m_SesionBase.SESION_SEGURIDAD, JsonConvert.SerializeObject(ParametrosHorasExtras), false);
        }
        public void AplicaHorasExtras(List<Modelos.Asistencias.Model_AplicaHorasExtrasAv> ParametrosHorasExtras)
        {
            m_SesionBase.MuestraMensaje("Aplicando Horas Extras");
            m_S_Asistencias.AplicaHorasExtrasCompleted += m_S_Asistencias_AplicaHorasExtrasCompleted;
            m_S_Asistencias.AplicaHorasExtrasAsync(m_SesionBase.SESION_SEGURIDAD, JsonConvert.SerializeObject(ParametrosHorasExtras), true);
        }

        void m_S_Asistencias_AplicaHorasExtrasCompleted(object sender, ES_Asistencias.AplicaHorasExtrasCompletedEventArgs e)
        {
            try
            {
                List<int> iGuardados = new List<int>();

                if (e.Result == null)
                {
                    m_SesionBase.MuestraMensaje("Error al obtener datos", 5);
                    return;
                }
                else
                {
                    string Resultado = e.Result;
                    m_SesionBase.MuestraMensaje("Listo", 3);
                    string[] sGuardados = CeC.ObtenArregoSeparador(Resultado, ",");

                    foreach (string sGuardado in sGuardados)
                    {
                        iGuardados.Add(CeC.Convierte2Int(sGuardado));
                    }
                }
                if (AplicaHorasExtrasEvent != null)
                    AplicaHorasExtrasEvent(iGuardados);
                return;
            }
            catch { m_SesionBase.MuestraMensaje("Error de red", 5); }
        }

        public delegate void QuitaHorasExtrasArgs(List<int> PERSONA_D_HE_IDs);
        public event QuitaHorasExtrasArgs QuitaHorasExtrasEvent;
        public void QuitaHorasExtras(string sPERSONA_D_HE_IDs, bool Forza = false)
        {
            m_SesionBase.MuestraMensaje("Quitando Horas Extras");
            m_S_Asistencias.QuitaHorasExtrasCompleted += m_S_Asistencias_QuitaHorasExtrasCompleted;
            m_S_Asistencias.QuitaHorasExtrasAsync(m_SesionBase.SESION_SEGURIDAD, sPERSONA_D_HE_IDs, Forza);
        }

        void m_S_Asistencias_QuitaHorasExtrasCompleted(object sender, ES_Asistencias.QuitaHorasExtrasCompletedEventArgs e)
        {
            try
            {
                List<int> iGuardados = new List<int>();

                if (e.Result == null)
                {
                    m_SesionBase.MuestraMensaje("Error al obtener datos", 5);
                    return;
                }
                else
                {
                    string Resultado = e.Result;
                    m_SesionBase.MuestraMensaje("Listo", 3);
                    string[] sGuardados = CeC.ObtenArregoSeparador(Resultado, ",");

                    foreach (string sGuardado in sGuardados)
                    {
                        iGuardados.Add(CeC.Convierte2Int(sGuardado));
                    }
                }
                if (QuitaHorasExtrasEvent != null)
                    QuitaHorasExtrasEvent(iGuardados);
                return;
            }
            catch { m_SesionBase.MuestraMensaje("Error de red", 5); }
        }
    }

}
