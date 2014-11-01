using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace eClock5.Clases
{
    class CeC_eClockSync
    {
        DispatcherTimer TimerEjecutaSincronizador;
        Modelos.eClockSync eClockSyncModelo;
        public void PararSincronizador()
        {
            if (TimerEjecutaSincronizador != null)
            {
                TimerEjecutaSincronizador.Stop();
            }
        }

        public void GuardaConfig(eClockBase.CeC_SesionBase Sesion)
        {
            try
            {
                eClockSyncModelo = Modelos.eClockSync.Carga();
                string ArchivoConfig = eClockSyncModelo.RutaApp + ".config";
                if (!System.IO.File.Exists(ArchivoConfig))
                {
                    System.IO.File.Copy(eClockSyncModelo.RutaApp + ".xml", ArchivoConfig);
                }
                Clases.CeC_AppConfigW.ActualizaAppSettings(ArchivoConfig, "eClockSync_WS_eCheck_WS_eCheck", Sesion.ObtenRutaServicio("WS_eCheck.asmx"));
                Clases.CeC_AppConfigW.ActualizaAppSettings(ArchivoConfig, "DireccionServicioWeb", Sesion.ObtenRutaServicio("WS_eCheck.asmx"));
                Clases.CeC_AppConfigW.ActualizaAppSettings(ArchivoConfig, "Usuario", eClockSyncModelo.Usuario);
                Clases.CeC_AppConfigW.ActualizaAppSettings(ArchivoConfig, "Password", eClockSyncModelo.Password);
                Clases.CeC_AppConfigW.ActualizaAppSettings(ArchivoConfig, "Proxy_URL", eClockSyncModelo.Proxy_URL);
                Clases.CeC_AppConfigW.ActualizaAppSettings(ArchivoConfig, "Proxy_Usuario", eClockSyncModelo.Proxy_Usuario);
                Clases.CeC_AppConfigW.ActualizaAppSettings(ArchivoConfig, "Proxy_Clave", eClockSyncModelo.Proxy_Clave);
                Clases.CeC_AppConfigW.ActualizaAppSettings(ArchivoConfig, "Sitio_ID", eClockSyncModelo.Sitios);
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        public void EjecutaSincronizador(eClockBase.CeC_SesionBase Sesion)
        {
            try
            {
                GuardaConfig(Sesion);
                if (eClockSyncModelo.Ejecutar)
                {
                    TimerEjecutaSincronizador = new DispatcherTimer();
                    TimerEjecutaSincronizador.Tick += TimerEjecutaSincronizador_Tick;
                    TimerEjecutaSincronizador.Interval = new TimeSpan(0, 1, 0);
                    TimerEjecutaSincronizador.Start();

                    TimerEjecutaSincronizador_Tick(this, null);
                }
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        public bool Ejecuta(eClockBase.CeC_SesionBase Sesion)
        {
            GuardaConfig(Sesion);
            return EjecutaApp();
        }

        private bool EjecutaApp()
        {
            try
            {
                eClockBase.CeC_Log.AgregaLog("Ejecutando Sincronizador");
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(eClockSyncModelo.RutaApp);
                process.StartInfo.FileName = System.IO.Path.GetFileName(eClockSyncModelo.RutaApp);
                process.Start();
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        void TimerEjecutaSincronizador_Tick(object sender, EventArgs e)
        {
            try
            {
                if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(eClockSyncModelo.RutaApp)).Count() < 1)
                {
                    EjecutaApp();
                }
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }
    }
}
