using System;
using System.Collections.Generic;
using System.Text;

namespace eClockSync
{
    class CeClockSync
    {
        System.Net.CookieContainer cookies = null;
        WS_eCheck.WS_eCheck ws_eCheck = null;
        public bool m_Sincronizando = false;
        public bool m_Parar = false;
        private System.Threading.Thread m_Thread;


        public void Iniciar(string[] args)
        {
            // TODO: agregar código aquí para iniciar el servicio.

            m_Thread = new System.Threading.Thread(new System.Threading.ThreadStart(Sincroniza));
            m_Thread.Start();
        }
        public void EsperaSegundos(int Segundos)
        {
            for (int Cont = 0; Cont < Segundos && !m_Parar; Cont++)
                CeTerminalSync.Sleep(1000);
        }
        public void BorraTempViejos()
        {
            try
            {
                string[] Archivos = System.IO.Directory.GetFiles(RutaTemporal, "eClock *");
                foreach (string RutaArchivo in Archivos)
                {
                    try
                    {
                        if (System.IO.File.GetCreationTime(RutaArchivo) < DateTime.Today.AddDays(-eClockSync.Properties.Settings.Default.Vigencia_Log))
                        {
                            System.IO.File.Delete(RutaArchivo);
                        }
                    }
                    catch (Exception ex)
                    {
                        CeLog2.AgregaError(ex);
                    }
                    CeTerminalSync.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }

        }

        public void BorraLogViejos()
        {
            BorraTempViejos();
            try
            {
                string[] Archivos = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "eClockSync *.log");
                foreach (string RutaArchivo in Archivos)
                {
                    try
                    {
                        string Archivo = RutaArchivo.Substring(RutaArchivo.LastIndexOf('\\') + 1);
                        DateTime FechaArchivo = new DateTime(Convert.ToInt32(Archivo.Substring(11, 4)), Convert.ToInt32(Archivo.Substring(16, 2)), Convert.ToInt32(Archivo.Substring(19, 2)));
                        if (FechaArchivo < DateTime.Today.AddDays(-eClockSync.Properties.Settings.Default.Vigencia_Log))
                        {
                            System.IO.File.Delete(RutaArchivo);
                        }
                    }
                    catch (Exception ex)
                    {
                        CeLog2.AgregaError(ex);
                    }
                    CeTerminalSync.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                CeLog2.AgregaError(ex);
            }
        }

        private DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }

        string RutaTemporal = "";
        public void Sincroniza()
        {
            RutaTemporal = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, eClockSync.Properties.Settings.Default.RutaTemporal);
            CeTerminalSync.RutaTemporal = RutaTemporal;
            CeLog2.S_TipoAlmacenamiento = CeLog2.CeLog2_Almacenamiento.Archivo;
            m_Sincronizando = true;
            bool CerrarConError = true;
            bool EsPrimeraEjecucion = true;
#if AUTOEXIT
            bool Salir = false;
#endif

            //return;
            
            while (!m_Parar)
            {
                try
                {
#if AUTOEXIT
                    if (Salir)
                    {
                        break;

                    }
                    Salir = true;
#endif

                    CeLog2.S_NombreDestino = "eClockSync " + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                    CeLog2.AgregaLog("Iniciando eClockSync ProductVersion-" + System.Windows.Forms.Application.ProductVersion);
                    CeLog2.AgregaLog("Iniciando eClockSync ProductDateTime-" + RetrieveLinkerTimestamp().ToString());
                     
                    if (ws_eCheck == null)
                    {
                        ws_eCheck = new eClockSync.WS_eCheck.WS_eCheck();
#if ECLOCK_WEB
                        ws_eCheck.Url = "http://web.eclock.com.mx/WS_eCheck.asmx";
#endif
                        System.Net.WebProxy ws_eCheck_WebProxy;
                        System.Net.ICredentials ws_eCheck_Credencial;

                        if (eClockSync.Properties.Settings.Default.Proxy_URL.Length > 0)
                        {

                            if (eClockSync.Properties.Settings.Default.Proxy_Usuario.Length > 0)
                            {
                                ws_eCheck_Credencial = new System.Net.NetworkCredential(eClockSync.Properties.Settings.Default.Proxy_Usuario, eClockSync.Properties.Settings.Default.Proxy_Clave);
                                ws_eCheck_WebProxy = new System.Net.WebProxy(eClockSync.Properties.Settings.Default.Proxy_URL, true, null, ws_eCheck_Credencial);
                            }
                            else
                                ws_eCheck_WebProxy = new System.Net.WebProxy(eClockSync.Properties.Settings.Default.Proxy_URL);
                            ws_eCheck.Proxy = ws_eCheck_WebProxy;
                        }
                        cookies = new System.Net.CookieContainer();
                        ws_eCheck.CookieContainer = cookies;
                        CeLog2.AgregaLog("Validando Usuario " + ws_eCheck.Url);
                        int Usuario_ID = ws_eCheck.ValidarUsuarioCrypt(eClockSync.Properties.Settings.Default.Usuario, eClockSync.Properties.Settings.Default.Password);
                        if (Usuario_ID <= 0 && CerrarConError)
                        {
#if !AUTOEXIT
                            if (EsPrimeraEjecucion)
                                CeLog2.AgregaErrorMsg("Usuario y Clave no validos");
                            else
#endif
                            {
                                CeLog2.AgregaError("Usuario y Clave no validos");
                                EsperaSegundos(600);
                            }
                            break;
                        }
                        //DateTime Fecha = ws_eCheck.ObtenFechaHora();
                        //string Texto = ws_eCheck.PersonaNombre(1);                       


                    }

                    try
                    {

                        CeLog2.AgregaLog("Sincronizando...");
                        WS_eCheck.DS_WSPersonasTerminales.EC_SITIOSDataTable DT = null;

                        int SitioID = eClockSync.Properties.Settings.Default.Sitio_ID;
                        if (SitioID < 0)
                            SitioID = ws_eCheck.ObtenSitioID();
                        
                       
                        DT = ws_eCheck.ObtenSitio(SitioID);
                        if (DT == null || DT.Rows.Count <= 0 && CerrarConError)
                        {
#if !AUTOEXIT
                        if (EsPrimeraEjecucion)
                            CeLog2.AgregaErrorMsg("Sitio no existe");
                        else
#endif
                            {
                                ws_eCheck = null;
                                CeLog2.AgregaError("Sitio no existe");
                                EsperaSegundos(600);
                                continue;
                            }
                            
                        }


                        CerrarConError = false;

                        WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESDataTable Terminales = ws_eCheck.ObtenTerminales(SitioID);
                        foreach (WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow Terminal in Terminales)
                        {
                            try
                            {
                                CeTerminalSync TerminalSync = CeTerminalSync.NuevaClase(Terminal);
                                if (TerminalSync != null)
                                {
                                    TerminalSync.ws_eCheck = ws_eCheck;
                                    TerminalSync.DatosSitio = DT[0];
                                    TerminalSync.SePuedeSincincronizarListas();

                                    TerminalSync.Sincroniza(Terminal, ws_eCheck);
                                }
                            }
                            catch (System.Exception e)
                            {
                                CeLog2.AgregaError(e);

                            }

                            CeTerminalSync.Sleep(1000);
                        }
                        decimal Segundos = DT[0].SITIO_SEGUNDOS_SYNC;
                        BorraLogViejos();
                        //EsperaSegundos(1);
                        CeLog2.AgregaLog("Esperando " + Segundos + "seg");
                        EsperaSegundos(Convert.ToInt32(Segundos));
                    }
                    catch (System.Exception e)
                    {
                        CeLog2.AgregaError(e);
                        ws_eCheck = null;
                        EsperaSegundos(30);
                    }
                }
                catch (System.Exception e)
                {
                    CeLog2.AgregaError(e);
                    ws_eCheck = null;
                    EsperaSegundos(30);
                }
            }


            CeLog2.AgregaLog("Finalizando eClockSync");
            m_Sincronizando = false;
        }
        public void Parar()
        {
            m_Parar = true;
            for (int Cont = 0; Cont < 600 && m_Sincronizando; Cont++)
            {
                CeTerminalSync.Sleep(100);
            }
        }

    }
}
