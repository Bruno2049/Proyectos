using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Controladores
{
    public class Licencias
    {
        ES_Licencias.S_LicenciasClient m_S_Licencias = null;
        CeC_SesionBase m_SesionBase = null;

        public Licencias(CeC_SesionBase SesionBase)
        {            
            m_S_Licencias = new ES_Licencias.S_LicenciasClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Licencias.svc"));
            m_SesionBase = SesionBase;
        }
        public void ValidaLicencia()
        {
            m_S_Licencias.ValidaLicenciaCompleted += delegate(object sender, ES_Licencias.ValidaLicenciaCompletedEventArgs e)
            {
                try
                {
                    if (e.Result != "")
                    {
                        m_SesionBase.Maquina = e.Result;
                        m_SesionBase.GuardaDatos();
                        fLicenciaCambio(true);
                    }
                    else
                        fLicenciaCambio(false);
                }
                catch { m_SesionBase.MuestraMensaje("No hay Conexión"); }
            };
            m_S_Licencias.ValidaLicenciaAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.Licencia, m_SesionBase.Maquina);
        }

        public void ActualizaMaquina()
        {
            Random rnd = new Random();
            m_SesionBase.Maquina = rnd.Next().ToString();
        }
        public delegate void LicenciaCambioArgs(bool Correcta);
        public event LicenciaCambioArgs LicenciaCambio;
        private void fLicenciaCambio(bool Correcta)
        {
            if (LicenciaCambio != null)
                LicenciaCambio(Correcta);
        }
        public void UsaLicencia(string Licencia)
        {
            m_SesionBase.Licencia = Licencia;
            ActualizaMaquina();
            m_S_Licencias.UsaLicenciaCompleted += delegate(object sender, ES_Licencias.UsaLicenciaCompletedEventArgs e)
            {
                if (e.Result > 0)
                {
                    m_SesionBase.GuardaDatos();
                    fLicenciaCambio(true);
                }
                else
                    fLicenciaCambio(false);
            };
            m_S_Licencias.UsaLicenciaAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.Licencia, m_SesionBase.Maquina);
        }

        public void CreaUsaLicencia()
        {
            ActualizaMaquina();
            m_S_Licencias.CreaUsaLicenciaCompleted += delegate(object sender, ES_Licencias.CreaUsaLicenciaCompletedEventArgs e)
            {
                try
                {
                    if (e.Result != "" && e.Result != "ERROR")
                    {
                        m_SesionBase.Licencia = e.Result;
                        m_SesionBase.GuardaDatos();
                        fLicenciaCambio(true);
                    }
                    else
                        fLicenciaCambio(false);
                }
                catch { m_SesionBase.MuestraMensaje("Error al guardar."); }
            };
            m_S_Licencias.CreaUsaLicenciaAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.Maquina);
        }

    }
}
