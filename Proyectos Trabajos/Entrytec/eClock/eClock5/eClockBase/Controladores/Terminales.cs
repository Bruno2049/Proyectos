using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Terminales
    {
        ES_Terminales.S_TerminalesClient m_S_Terminales = null;
        CeC_SesionBase m_SesionBase = null;

        public Terminales(CeC_SesionBase SesionBase)
        {
            m_S_Terminales = new ES_Terminales.S_TerminalesClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Terminales.svc"));
            m_SesionBase = SesionBase;
        }

        public delegate void GuardadoArgs(bool Guardado);
        public event GuardadoArgs GuardadoEvent;

        public void Guardar(Modelos.Model_TERMINALES Terminale)
        {
            string DatoTerminal = JsonConvert.SerializeObject(Terminale);
            m_SesionBase.MuestraMensaje("Guardando Datos");

            m_S_Terminales.GuardaDatosCompleted += m_S_Terminales_GuardaDatosCompleted;
            m_S_Terminales.GuardaDatosAsync(m_SesionBase.SESION_SEGURIDAD, DatoTerminal);
        }

        public void Guardar(List<Modelos.Model_TERMINALES> Terminales)
        {
            string DatoTerminal = JsonConvert.SerializeObject(Terminales);
            m_SesionBase.MuestraMensaje("Guardando Datos");

            m_S_Terminales.GuardaDatosCompleted += m_S_Terminales_GuardaDatosCompleted;
            m_S_Terminales.GuardaDatosAsync(m_SesionBase.SESION_SEGURIDAD, DatoTerminal);
        }
        void m_S_Terminales_GuardaDatosCompleted(object sender, ES_Terminales.GuardaDatosCompletedEventArgs e)
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
            }
            if (GuardadoEvent != null)
                GuardadoEvent(false);
            m_SesionBase.MuestraMensaje("Error al Guardar", 10);
        }
    }
}