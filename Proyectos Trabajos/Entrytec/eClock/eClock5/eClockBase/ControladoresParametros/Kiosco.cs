using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.ControladoresParametros
{
    public class Kiosco
    {
        public eClockBase.Modelos.Kiosco.Model_Parametros Parametros = null;
        public eClockBase.CeC_SesionBase Sesion = null;

        public delegate void CargaParametrosArgs(bool Cargados);
        public event CargaParametrosArgs CargaParametrosEvent;
        public void CargaParametros(eClockBase.CeC_SesionBase SesionActual, bool SoloNulo = true)
        {
            if (SoloNulo && Parametros != null)
                return;
            Sesion = SesionActual;
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.ObtenConfigEvent += cSesion_ObtenConfigEvent;
            cSesion.ObtenConfig(this.GetType().Name, 1);
        }

        void cSesion_ObtenConfigEvent(string Resultado)
        {
            try
            {
                Parametros = new Modelos.Kiosco.Model_Parametros();
                if (Resultado != "")
                    Parametros = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Kiosco.Model_Parametros>(Resultado);
            }
            catch { }
            if (CargaParametrosEvent != null)
                CargaParametrosEvent(Parametros != null);
        }

        public delegate void GuardaParametrosArgs(bool Resultado);
        public event GuardaParametrosArgs GuardaParametrosEvent;
        public void GuardaParametros()
        {
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.GuardaConfigEvent += cSesion_GuardaConfigEvent;
            cSesion.GuardaConfig(this.GetType().Name, Parametros, 1);
        }

        void cSesion_GuardaConfigEvent(bool Resultado)
        {
            if (GuardaParametrosEvent != null)
                GuardaParametrosEvent(Resultado);
        }
    }
}
