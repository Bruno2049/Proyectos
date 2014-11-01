using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Periodos
    {
        ES_Periodos.S_PeriodosClient m_S_Periodos = null;
        CeC_SesionBase m_SesionBase = null;

        public Periodos(CeC_SesionBase SesionBase)
        {
            m_S_Periodos = new ES_Periodos.S_PeriodosClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Periodos.svc"));
            m_SesionBase = SesionBase;
        }


        public delegate void ObtenListadoFinalizadoArgs(List<Modelos.Periodos.Model_Listado> Listado);
        public event ObtenListadoFinalizadoArgs ObtenListadoFinalizado;

        public void ObtenListado(DateTime FechaDesde, DateTime FechaHasta, int EdoPeriodo)
        {
            m_S_Periodos.ObtenListadoCompleted += m_S_Periodos_ObtenListadoCompleted;
            m_S_Periodos.ObtenListadoAsync(m_SesionBase.SESION_SEGURIDAD, FechaDesde, FechaHasta, EdoPeriodo, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA);
        }

        void m_S_Periodos_ObtenListadoCompleted(object sender, ES_Periodos.ObtenListadoCompletedEventArgs e)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Listo", 1);
                List<Modelos.Periodos.Model_Listado> Listado = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Modelos.Periodos.Model_Listado>>(e.Result);
                if (ObtenListadoFinalizado != null)
                    ObtenListadoFinalizado(Listado);
            }
            catch (Exception ex)
            {
                m_SesionBase.MuestraMensaje("Error al cargar los datos", 10);
                CeC_Log.AgregaError(ex);
            }
        }
    }
}
