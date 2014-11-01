using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Restricciones 
    {
        ES_Restricciones.S_RestriccionesClient m_S_Restricciones = null;
        CeC_SesionBase m_SesionBase = null;
        public Restricciones(CeC_SesionBase SesionBase)          
        {
            m_S_Restricciones = new ES_Restricciones.S_RestriccionesClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Restricciones.svc"));
            m_SesionBase = SesionBase;            
            //m_S_Restricciones.va
        }
        public delegate void TieneDerechosArgs(string RestriccionesPermitidas);
        public event TieneDerechosArgs TieneDerechosEvent;
        public void TieneDerechos(string Restricciones)
        {
            m_S_Restricciones.TieneDerechosCompleted += delegate(object sender, ES_Restricciones.TieneDerechosCompletedEventArgs e)
                {
                   
                    if (TieneDerechosEvent != null)
                    {
                        string RestriccionesPermitidas = "";
                        string[]sRestricciones = CeC.ObtenArregoSeparador(Restricciones, ",");
                        for (int Cont = 0; Cont < sRestricciones.Length; Cont ++ )
                        {
                            try{
                                if (e.Result[Cont] == '1')
                                    RestriccionesPermitidas = CeC.AgregaSeparador(RestriccionesPermitidas, sRestricciones[Cont], ",");
                            }catch{}
                            
                        }
                        TieneDerechosEvent(RestriccionesPermitidas);
                    }

                };
            m_S_Restricciones.TieneDerechosAsync(m_SesionBase.SESION_SEGURIDAD,Restricciones);
        }

    }
}