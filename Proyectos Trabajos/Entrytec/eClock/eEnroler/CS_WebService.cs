using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace eEnroler
{
    class CS_WebService
    {
#if ECLOCK
        static bool m_EseClock = true;
#else
          static bool m_EseClock = false;
#endif
        static bool m_Conectado = false;

        static WS_Checador.WSChecador m_WsChecador = null;
        static WS_eCheck.WS_eCheck m_WseCheck = null;
        static int m_UsuarioID = 0;
        static string sUsuario = "";
        static string sClave = "";
        public static bool Conecta()
        {
            try
            {
                if (m_EseClock && m_WseCheck == null)
                {
                    m_WseCheck = new WS_eCheck.WS_eCheck();
                    System.Net.WebProxy ws_eCheck_WebProxy;
                    System.Net.ICredentials ws_eCheck_Credencial;

                    if (eEnroler.Properties.Settings.Default.Proxy_URL.Length > 0)
                    {

                        if (eEnroler.Properties.Settings.Default.Proxy_Usuario.Length > 0)
                        {
                            ws_eCheck_Credencial = new System.Net.NetworkCredential(eEnroler.Properties.Settings.Default.Proxy_Usuario, eEnroler.Properties.Settings.Default.Proxy_Clave, eEnroler.Properties.Settings.Default.Proxy_Dominio);
                            ws_eCheck_WebProxy = new System.Net.WebProxy(eEnroler.Properties.Settings.Default.Proxy_URL, true, null, ws_eCheck_Credencial);
                        }
                        else
                            ws_eCheck_WebProxy = new System.Net.WebProxy(eEnroler.Properties.Settings.Default.Proxy_URL);
                        m_WseCheck.Proxy = ws_eCheck_WebProxy;
                                                
                        CeLog2.AgregaLog("Usando Proxy " + eEnroler.Properties.Settings.Default.Proxy_URL);
                    }
                    m_WseCheck.CookieContainer = new System.Net.CookieContainer();    
               
                }


                if (!m_EseClock && m_WsChecador == null)
                {
                    m_WsChecador = new eEnroler.WS_Checador.WSChecador();
                    m_WsChecador.CookieContainer = new System.Net.CookieContainer();
                }
                if (sUsuario == "")
                {
                    sUsuario = eEnroler.Properties.Settings.Default.Usuario;
                    sClave = eEnroler.Properties.Settings.Default.Clave;
                }

                ///Valida si es eClock Web, en su mayoria deberá pasar por aqui, salvo Merck
                if (m_EseClock && sUsuario.Length <= 0)
                {
                    FLogIn Frm = new FLogIn();
                    Frm.ws_eCheck = m_WseCheck;
                    if (Frm.ShowDialog() != DialogResult.Yes)
                    {
                        Application.Exit();
                        return false;
                    }
                    sUsuario = Frm.Usuario;
                    sClave = Frm.Clave;
                    return m_Conectado = true;
                }
                if (m_EseClock)
                    m_UsuarioID = m_WseCheck.ValidarUsuario(sUsuario, sClave);
                else
                    m_UsuarioID = m_WsChecador.ValidarUsuario(sUsuario, sClave);

                if (m_UsuarioID < 0)
                {
                    MessageBox.Show("Usuario y Clave Invalidos", "Atención");
                    return false;
                }
                m_Conectado = true;
                return true;
            }
            catch(Exception ex)
            {
                CeLog2.AgregaError(ex);
                MessageBox.Show("No se pudo conectar a eClock", "Atención");
                return false;
            }
        }
        public static int ObtenPersonaID(int PersonaLink)
        {
            if (m_EseClock)
                return m_WseCheck.ObtenPersonaID(PersonaLink);
            return m_WsChecador.ObtenPersonaID(PersonaLink);
        }
        public static string PersonaNombre(int PersonaID)
        {
            if (m_EseClock)
                return m_WseCheck.PersonaNombre(PersonaID);
            return m_WsChecador.PersonaNombre(PersonaID);
        }
        public static string ObtenValorCampoAdicional(int TerminalID, int Persona_ID)
        {
            if (m_EseClock)
                return m_WseCheck.ObtenValorCampoAdicional(TerminalID, Persona_ID);
            return m_WsChecador.ObtenValorCampoAdicional(TerminalID, Persona_ID);
        }
        public static byte[] ObtenFoto(int PersonaID)
        {
            if (m_EseClock)
                return m_WseCheck.ObtenFoto(PersonaID);
            return m_WsChecador.ObtenFoto(PersonaID);
        }
        public static bool HayHuella(int PersonaID, int TerminalID, int NoHuella)
        {
            if (m_EseClock)
                return m_WseCheck.HayHuella(PersonaID, TerminalID, NoHuella);
            return m_WsChecador.HayHuella(PersonaID, TerminalID, NoHuella);
        }

        public static bool AsignaHuella(int PersonaID, int TerminalID, byte[] Huella, int NoHuella)
        {
            if (m_EseClock)
                return m_WseCheck.AsignaHuella(PersonaID,TerminalID,Huella,NoHuella);
            return m_WsChecador.AsignaHuella(PersonaID, TerminalID, Huella, NoHuella);
        }

        public static bool AsignaCampoAdicional( int TerminalID, int PersonaID, string ValorCampoAdicional)
        {
            if (m_EseClock)
                return m_WseCheck.AsignaCampoAdicional(TerminalID, PersonaID, ValorCampoAdicional);
            return m_WsChecador.AsignaCampoAdicional(TerminalID, PersonaID, ValorCampoAdicional);
        }

        public static bool AsignaVectorAVec(int PersonaID, int AlmacenID, byte[] Huella, int NoHuella)
        {
            if (m_EseClock)
                return m_WseCheck.AsignaVectorAVec( PersonaID,AlmacenID,Huella,NoHuella);
            return m_WsChecador.AsignaVectorAVec(PersonaID, AlmacenID, Huella, NoHuella);
        }
    
    }
}
