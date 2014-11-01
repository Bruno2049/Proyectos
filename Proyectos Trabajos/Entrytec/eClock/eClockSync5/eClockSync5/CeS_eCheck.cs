using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace eClockSync5
{
    class CeS_eCheck
    {
        public static WS_eCheck.WS_eCheck m_Ws_eCheck = null;
        public static System.Net.CookieContainer m_Cookies = null;
        public static System.Net.WebProxy ws_eCheck_WebProxy;
        public static System.Net.ICredentials ws_eCheck_Credencial;


        public static string m_Usuario = "";
        public static string m_Clave = "";
        public static bool m_GuardarClave = true;
        public static string m_RutaWebService = "";
        public static string m_SitiosIds = "";


        public static bool CargarParametros()
        {
            m_RutaWebService = eClockSync5.Properties.Settings.Default.RutaWebService;
            m_Usuario = eClockSync5.Properties.Settings.Default.Usuario;
            m_Clave = eClockSync5.Properties.Settings.Default.Clave;
            m_GuardarClave = eClockSync5.Properties.Settings.Default.RecordarCredenciales;
            m_SitiosIds = eClockSync5.Properties.Settings.Default.SitiosIDs;
            return true;
        }
        public static bool GuardaParametros()
        {

            eClockSync5.Properties.Settings.Default.Usuario = m_Usuario;
            eClockSync5.Properties.Settings.Default.Clave = m_Clave;
            eClockSync5.Properties.Settings.Default.RecordarCredenciales = m_GuardarClave;
            eClockSync5.Properties.Settings.Default.SitiosIDs = m_SitiosIds;
            eClockSync5.Properties.Settings.Default.Save();
            return true;
        }
        public static bool Conectar()
        {
            try
            {
                CargarParametros();
                m_Ws_eCheck = new WS_eCheck.WS_eCheck();
                m_Ws_eCheck.Url = m_RutaWebService;

                if (eClockSync5.Properties.Settings.Default.Proxy_URL.Length > 0)
                {

                    if (eClockSync5.Properties.Settings.Default.Proxy_Usuario.Length > 0)
                    {
                        ws_eCheck_Credencial = new System.Net.NetworkCredential(eClockSync5.Properties.Settings.Default.Proxy_Usuario, eClockSync5.Properties.Settings.Default.Proxy_Clave);
                        ws_eCheck_WebProxy = new System.Net.WebProxy(eClockSync5.Properties.Settings.Default.Proxy_URL, true, null, ws_eCheck_Credencial);
                    }
                    else
                        ws_eCheck_WebProxy = new System.Net.WebProxy(eClockSync5.Properties.Settings.Default.Proxy_URL);
                    m_Ws_eCheck.Proxy = ws_eCheck_WebProxy;
                }
                m_Cookies = new System.Net.CookieContainer();
                m_Ws_eCheck.CookieContainer = m_Cookies;
                //m_Ws_eCheck.DisplayInitializationUI();
                if (!ValidarUsuario())
                    return false;
                if (!CargaPrimeraVez())
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                CeLog2.AgregaErrorMsg(ex);
            }
            return false;
        }
        public static bool ValidarUsuario()
        {
            Frm_LogIn Dlg = new Frm_LogIn();
            bool Correcto = true;
            do
            {
                if (!Correcto)
                    Dlg.Incorrecto();
                if (m_Usuario == "" || Correcto == false || !m_GuardarClave)
                    if (Dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return false;

                int UsuarioID = m_Ws_eCheck.ValidarUsuario(m_Usuario, m_Clave);
                if (UsuarioID > 0)
                {
                    Correcto = true;
                    GuardaParametros();
                }
                else
                    Correcto = false;
            } while (!Correcto);
            return Correcto;
        }

        /// <summary>
        /// Calcula el valor del Hash de un texto
        /// </summary>
        /// <param name="Texto">Texto de donde se va a obtener el hash</param>
        /// <returns></returns>
        public static string CalculaHash(string Texto)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider Sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            string HashSR = BitConverter.ToString(Sha1.ComputeHash(new System.IO.MemoryStream(System.Text.ASCIIEncoding.Default.GetBytes(Texto))));
            return HashSR;
        }
        public static bool EstaConectado()
        {
            return EstaConectado(true);
        }
        public static bool EstaConectado(bool Reconectar)
        {
            try
            {
                if (m_Ws_eCheck != null)
                    if (m_Ws_eCheck.ObtenSESION_ID() > 0)
                        return true;
            }
            catch
            { }
            if (Reconectar && Conectar())
                return EstaConectado(Reconectar);
            return false;
        }
        public static DataSet ObtenSitios()
        {
            if (!EstaConectado())
                return null;
            return m_Ws_eCheck.ObtenSitiosDSMenu();
        }
        public static bool CargaPrimeraVez()
        {
            if (m_SitiosIds == "")
            {
                if (!EligeSitios())
                    return false;
            }
            return true;
        }
        public static bool EligeSitios()
        {
            Frm_Sitios Frm = new Frm_Sitios();
            Frm.SitiosIDs = m_SitiosIds;
            if (Frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return false;
            CeS_eCheck.m_SitiosIds = Frm.SitiosIDsSeleccionados;
            GuardaParametros();
            return true;
        }

        public static string ObtenUrl(string UrlRelativa)
        {
            string Ruta = m_RutaWebService.Substring(0, m_RutaWebService.LastIndexOf("/"));
            return Ruta + "/" + UrlRelativa;
        }

        public static bool NavegaOlvidoClave()
        {
            if (!EstaConectado())
                return false;
            return Frm_Vista.MuestraUrl(m_Ws_eCheck.ObtenLinkOlvidoClave());
        }
        public static bool NavegaNuevoUsuario()
        {
            if (!EstaConectado())
                return false;
            return Frm_Vista.MuestraUrl(m_Ws_eCheck.ObtenLinkNuevoUsuario());
        }

        public static List<CeTerminalSync> ObtenTerminales(System.Windows.Forms.ListView Lst_Terminales)
        {
            if (!EstaConectado())
                return null;
            List <CeTerminalSync> Terminales = new List<CeTerminalSync>();
            string []sSitiosIds= CeC.ObtenArregoSeparador(m_SitiosIds, ",");
            foreach (string sSitioID in sSitiosIds)
            {
                int SitioID = CeC.Convierte2Int(sSitioID);
                WS_eCheck.DS_WSPersonasTerminales.EC_SITIOSDataTable DT = m_Ws_eCheck.ObtenSitio(SitioID);
                WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESDataTable dtTerminales = m_Ws_eCheck.ObtenTerminales(SitioID);
                if (dtTerminales.Rows.Count < 1)
                {
                    Frm_Sitios FrmSitios = new Frm_Sitios();
                    FrmSitios.SitiosIDs = m_SitiosIds;
                    if (FrmSitios.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        m_SitiosIds = FrmSitios.SitiosIDsSeleccionados;
                        GuardaParametros();
                        return ObtenTerminales(Lst_Terminales);
                    }
                    else
                        return null;
                    
                }
                foreach (WS_eCheck.DS_WSPersonasTerminales.EC_TERMINALESRow Terminal in dtTerminales)
                {
                    try
                    {
                        CeTerminalSync TerminalSync = CeTerminalSync.NuevaClase(Terminal);
                        if (TerminalSync != null)
                        {
                            TerminalSync.ws_eCheck = m_Ws_eCheck;
                            TerminalSync.DatosSitio = DT[0];
                            TerminalSync.SePuedeSincincronizarListas();
                            TerminalSync.CreaListViewItem(Lst_Terminales);
                            Terminales.Add(TerminalSync);
                            //TerminalSync.Sincroniza(Terminal, ws_eCheck);
                        }
                    }
                    catch (System.Exception e)
                    {
                        CeLog2.AgregaError(e);

                    }

                    CeTerminalSync.Sleep(1000);
                }
            }
            return Terminales;
            
        }

    }
}
