using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Controladores
{
    public class Suscripciones
    {
        ES_Suscripciones.S_SuscripcionesClient m_S_Suscripciones = null;
        CeC_SesionBase m_SesionBase = null;

        public Suscripciones(CeC_SesionBase SesionBase)
        {
            m_S_Suscripciones = new ES_Suscripciones.S_SuscripcionesClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Suscripciones.svc"));
            m_SesionBase = SesionBase;
        }

        public delegate void ObtenSuscripcionIDArgs(int SuscripcionID);
        public event ObtenSuscripcionIDArgs ObtenidoSuscripcionID;

        public void ObtenSuscripcionID(string Suscripcion)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Suscripcion..");
            m_S_Suscripciones.ObtenSuscripcionIDCompleted += delegate(object sender, ES_Suscripciones.ObtenSuscripcionIDCompletedEventArgs e)
                {
                    try
                    {
                        if (e.Result > 0)
                        {
                            m_SesionBase.Suscripcion = Suscripcion;
                            m_SesionBase.GuardaDatos();
                            m_SesionBase.MuestraMensaje("Suscripcion Existente", 3);
                        }
                        else
                            m_SesionBase.MuestraMensaje("No Existe la Suscripcion", 3);
                        if (ObtenidoSuscripcionID != null)
                            ObtenidoSuscripcionID(e.Result);
                        return;
                    }
                    catch { }
                    if (ObtenidoSuscripcionID != null)
                        ObtenidoSuscripcionID(-9999);
                    m_SesionBase.MuestraMensaje("Error", 5);
                };
            m_S_Suscripciones.ObtenSuscripcionIDAsync(Suscripcion);
        }

        public delegate void ObtenSuscripcionURLArgs(string URL);
        public event ObtenSuscripcionURLArgs ObtenSuscripcionURLEvent;

        public void ObtenSuscripcionURL(string Suscripcion)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Suscripcion..");
            m_S_Suscripciones.ObtenSuscripcionUrlCompleted += delegate (object sender, ES_Suscripciones.ObtenSuscripcionUrlCompletedEventArgs e)
            {
                try
                {
                    if (e.Result != null && e.Result != "")
                    {
                        m_SesionBase.RutaServicios = e.Result;
                        m_SesionBase.GuardaDatos();
                        m_SesionBase.MuestraMensaje("URL Actualizada", 3);
                    }
                    else
                        m_SesionBase.MuestraMensaje("No Existe la Suscripcion", 5);
                    if (ObtenSuscripcionURLEvent != null)
                        ObtenSuscripcionURLEvent(e.Result);
                    return;
                }
                catch { }
                if (ObtenSuscripcionURLEvent != null)
                    ObtenSuscripcionURLEvent(null);
                m_SesionBase.MuestraMensaje("Error de red", 5);
            };
            m_S_Suscripciones.ObtenSuscripcionUrlAsync(Suscripcion);
        }


    }
}
