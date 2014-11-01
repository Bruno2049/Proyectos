using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace eClock5
{
    /// <summary>
    /// 
    /// </summary>
    public class CeC_Sesion: eClockBase.CeC_SesionBase
    {
        private static CeC_Sesion m_SesionActual = null;

        public static eClockBase.CeC_SesionBase ObtenSesion(object VentanaActual)
        {
            if (m_SesionActual == null)
            {
                //m_SesionActual = new CeC_Sesion();
                string Guardado = eClockBase.CeC_SesionBase.CargarDatosString();
                if (Guardado.Length > 0)
                {
                    m_SesionActual = JsonConvert.DeserializeObject<CeC_Sesion>(Guardado);
                    m_SesionActual.SUSCRIPCION_ID_SELECCIONADA = -1;
                    if (!m_SesionActual.MantenerSesion)
                        m_SesionActual.SESION_SEGURIDAD = "";
                }
                else
                    m_SesionActual = new CeC_Sesion();
            }
            return (eClockBase.CeC_SesionBase)m_SesionActual;
        }


        public override void MuestraMensaje(string Mensaje, int TimeOutSegundos = -1)
        {
            base.MuestraMensaje(Mensaje, TimeOutSegundos);

        }
        public override bool AsignaControlMensaje(object Control)
        {
             
            return base.AsignaControlMensaje(Control);
        }
    }
}
