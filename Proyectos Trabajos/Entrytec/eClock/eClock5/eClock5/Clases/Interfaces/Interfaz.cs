using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClock5.Clases.Interfaces
{
    class Interfaz
    {

        public eClockBase.Modelos.Nomina.Model_Interfaz Parametros = null;
        public Interfaz()
        {
        }
        public bool Carga(Interfaz Anterior)
        {
            this.Parametros = Anterior.Parametros;
            this.Sesion = Anterior.Sesion;
            return true;
        }
        public eClockBase.CeC_SesionBase Sesion = null;

        public delegate void CargaParametrosArgs(bool Cargados);
        public event CargaParametrosArgs CargaParametrosEvent;
        public void CargaParametros(object VentanaActual)
        {
            if (Parametros != null)
                return;
            Sesion = CeC_Sesion.ObtenSesion(VentanaActual);
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.ObtenConfigEvent += cSesion_ObtenConfigEvent;
            cSesion.ObtenConfig("InterfazNomina", 1);
        }

        void cSesion_ObtenConfigEvent(string Resultado)
        {
            try
            {
                
                Parametros = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Nomina.Model_Interfaz>(Resultado);
            }
            catch { }
            if(Parametros ==null)
                Parametros = new eClockBase.Modelos.Nomina.Model_Interfaz();
            if (CargaParametrosEvent != null)
                CargaParametrosEvent(Parametros != null);
        }

        public delegate void GuardaParametrosArgs(bool Resultado);
        public event GuardaParametrosArgs GuardaParametrosEvent;
        public void GuardaParametros()
        {
            eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
            cSesion.GuardaConfigEvent += cSesion_GuardaConfigEvent;
            cSesion.GuardaConfig("InterfazNomina", Parametros, 1);
        }

        void cSesion_GuardaConfigEvent(bool Resultado)
        {
            if (GuardaParametrosEvent != null)
                GuardaParametrosEvent(Resultado);
        }
        protected virtual eClockBase.Modelos.Nomina.Model_RecNominasImportar ObtenRecibosNominaR(string NominaExID, int Ano, int PeriodoNO)
        {
            return null;
        }

        public eClockBase.Modelos.Nomina.Model_RecNominasImportar ObtenRecibosNomina(string NominaExID, int Ano, int PeriodoNO)
        {
            Interfaz InterfacRec = null;
            switch (Parametros.SISTEMA_NOMINA_ID)
            {
                case 1:
                    InterfacRec = new OracleBS();
                    break;
            }
            InterfacRec.Carga(this);
            if (InterfacRec != null)
                return InterfacRec.ObtenRecibosNominaR(NominaExID, Ano, PeriodoNO);
            return null;
        }

    }
}
