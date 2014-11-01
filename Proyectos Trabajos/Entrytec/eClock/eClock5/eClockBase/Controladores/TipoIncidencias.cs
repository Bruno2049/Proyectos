using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class TipoIncidencias
    {
        ES_TipoIncidencias.S_TipoIncidenciasClient m_S_TipoIncidencias = null;
        CeC_SesionBase m_SesionBase = null;

        public TipoIncidencias(CeC_SesionBase SesionBase)
        {
            m_S_TipoIncidencias = new ES_TipoIncidencias.S_TipoIncidenciasClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_TipoIncidencias.svc"));
            m_SesionBase = SesionBase;
        }

        public delegate void GuardadoArgs(bool Guardado);
        public event GuardadoArgs GuardadoEvent;

        public void Guardar(Modelos.Model_TIPO_INCIDENCIAS TipoIncidencias)
        {
            string DatoTipoIncidencias = JsonConvert.SerializeObject(TipoIncidencias);
            m_SesionBase.MuestraMensaje("Guardando Datos");

            m_S_TipoIncidencias.GuardaDatosCompleted += m_S_TipoIncidencias_GuardaDatosCompleted;
            m_S_TipoIncidencias.GuardaDatosAsync(m_SesionBase.SESION_SEGURIDAD, DatoTipoIncidencias);
        }

        void m_S_TipoIncidencias_GuardaDatosCompleted(object sender, ES_TipoIncidencias.GuardaDatosCompletedEventArgs e)
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
    }
}
