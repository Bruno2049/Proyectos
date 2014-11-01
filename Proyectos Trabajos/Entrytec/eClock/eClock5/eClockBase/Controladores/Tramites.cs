using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Tramites
    {
        ES_Tramites.S_TramitesClient m_S_Tramites = null;
        CeC_SesionBase m_SesionBase = null;

        public Tramites(CeC_SesionBase SesionBase)
        {
            m_S_Tramites = new ES_Tramites.S_TramitesClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Tramites.svc"));
            m_SesionBase = SesionBase;
        }


        public delegate void NuevoArgs(int Resultado);
        public event NuevoArgs NuevoEvent;

        public void Nuevo(int TipoTramiteID, int PersonaID, int TipoPrioridadID, string Descripcion)
        {
            m_SesionBase.MuestraMensaje("Solicitando Tramite..");
            m_S_Tramites.NuevoCompleted += m_S_Tramites_NuevoCompleted;
            m_S_Tramites.NuevoAsync(m_SesionBase.SESION_SEGURIDAD, TipoTramiteID, PersonaID, TipoPrioridadID, Descripcion);
        }

        void m_S_Tramites_NuevoCompleted(object sender, ES_Tramites.NuevoCompletedEventArgs e)
        {
            try
            {
                if (e.Result > 0)
                {
                    m_SesionBase.MuestraMensaje("Tramite Solicitado", 3);
                    if (NuevoEvent != null)
                        NuevoEvent(e.Result);
                }
                else
                    m_SesionBase.MuestraMensaje("Error al Solicitar Tramite", 5);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }

    }
}
