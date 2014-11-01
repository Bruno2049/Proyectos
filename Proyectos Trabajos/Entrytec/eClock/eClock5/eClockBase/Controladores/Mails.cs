using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Controladores
{
    public class Mails
    {
        ES_Mails.S_MailsClient m_S_Mails = null;
        CeC_SesionBase m_SesionBase = null;

        public Mails(CeC_SesionBase SesionBase)
        {
            m_S_Mails = new ES_Mails.S_MailsClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Mails.svc"));
            m_SesionBase = SesionBase;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UsuarioID"></param>
        /// <param name="UsuarioIDCon"></param>
        /// <returns></returns>
        public string ObtenChatsNombre(int UsuarioID)
        {
            return "Chats" + UsuarioID + ".Lst";
        }
        public delegate void ObtenChatsArgs(string Resultados);
        public event ObtenChatsArgs ObtenChatsEvent;

        public void ObtenChats()
        {
            m_SesionBase.MuestraMensaje("Obteniendo Chats");
            string ArchivoListadoNombre = ObtenChatsNombre(m_SesionBase.USUARIO_ID);
            ListadoJsonLocal LjLocal = ListadoJsonLocal.Cargar(ArchivoListadoNombre);
            if (LjLocal.Listado != null && ObtenChatsEvent != null)
            {
                try
                {
                    ObtenChatsEvent(LjLocal.Listado);
                }
                catch { }
            }

            m_S_Mails.ObtenChatsCompleted += delegate(object sender, ES_Mails.ObtenChatsCompletedEventArgs e)
                {
                    try
                    {
                        if (e.Result != null)
                        {
                            m_SesionBase.MuestraMensaje("Mensajes Mostrados", 3);
                        }
                        else
                        {
                            m_SesionBase.MuestraMensaje("Error al mostrar los mensajes", 5);
                        }
                        
                        if (e.Result != null && e.Result != "==")
                            ListadoJsonLocal.Guarda(ArchivoListadoNombre, e.Result);

                        if (ObtenChatsEvent != null && e.Result != "==")
                        {
                            if (e.Result != null)
                                ObtenChatsEvent(e.Result);
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        CeC_Log.AgregaError(ex);
                    }
                    m_SesionBase.MuestraMensaje("Error de Red", 5);
                };
            m_S_Mails.ObtenChatsAsync(m_SesionBase.SESION_SEGURIDAD, LjLocal.Hash);
        }
        //****************************************************************************************************************************
        //****************************************************************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UsuarioID"></param>
        /// <param name="UsuarioIDCon">Usuario con quien chatea</param>
        /// <returns></returns>
        public string ObtenChatsConNombre(int UsuarioID, int UsuarioIDCon)
        {
            return "Chats" + UsuarioID + "Con" + UsuarioIDCon + ".Lst";
        }
        public delegate void ObtenChatsConArgs(string Resultados);
        public event ObtenChatsConArgs ObtenChatsConEvent;

        public void ObtenChatsCon(int UsuarioIDCon)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Chats");
            string ArchivoListadoNombre = ObtenChatsConNombre(m_SesionBase.USUARIO_ID, UsuarioIDCon);
            ListadoJsonLocal LjLocal = ListadoJsonLocal.Cargar(ArchivoListadoNombre);
            if (LjLocal.Listado != null && ObtenChatsConEvent != null)
            {
                try
                {
                    ObtenChatsConEvent(LjLocal.Listado);
                }
                catch { }
            }

            m_S_Mails.ObtenChatsConCompleted += delegate(object sender, ES_Mails.ObtenChatsConCompletedEventArgs e)
            {
                try
                {
                    /// En estas primeras 4 lineas evalua si e.Result(resultado del servicio); es nulo si es diferente
                    /// de nulo, muestra un mensaje de Listado Obtenido si no manda Error.
                    if (e.Result != null)
                        m_SesionBase.MuestraMensaje("Listado Obtenido", 3);
                    else
                        m_SesionBase.MuestraMensaje("error al obtener el listado", 5);

                    /// En estas proximas 2 lineas evalua si e.Result es diferente de nulo "Y" si es diferente de "=="
                    /// Si cumple la condicion guarda el ListadoJson en un ARCHIVO LOCAL, en caso contrario no lo hace.
                    if (e.Result != null && e.Result != "==")
                        ListadoJsonLocal.Guarda(ArchivoListadoNombre, e.Result);

                    /// En estas proximas 5 lineas evalua si el evento "ObtenChatsConEvent" es diferente de null osea que si ya 
                    /// se le asigno una funcion al evento desde la VISTA, "Y" evalua si es diferente de "=="
                    /// Si se cumplen las dos condiciones evalua por ultimo si e.Result es diferente de null, si se cumple 
                    /// envia los resultados ala funcion asignada al evento "ObtenChatsConEvent"
                    if (ObtenChatsConEvent != null && e.Result != "==")
                    {
                        if (e.Result != null)
                            ObtenChatsConEvent(e.Result);
                    }
                    return;

                }
                catch (Exception ex)
                {
                    CeC_Log.AgregaError(ex);
                }
                m_SesionBase.MuestraMensaje("Error de Red", 5);
            };
            m_S_Mails.ObtenChatsConAsync(m_SesionBase.SESION_SEGURIDAD, UsuarioIDCon, LjLocal.Hash);
        }
        //****************************************************************************************************************************
        //****************************************************************************************************************************
        //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UsuarioID"></param>
        /// <param name="UsuarioIDCon">Usuario con quien chatea</param>
        /// <returns></returns>
        public string BorraMensajesConConNombre(int UsuarioID, int UsuarioIDCon)
        {
            return "Chats" + UsuarioID + "Con" + UsuarioIDCon + ".Lst";
        }
        public delegate void BorraMensajesConArgs(string Resultados);
        public event BorraMensajesConArgs BorraMensajesConEvent;

        public void BorraMensajesCon(int UsuarioIDCon)
        {
            m_SesionBase.MuestraMensaje("Borrando Mensajes");
            string ArchivoListadoNombre = BorraMensajesConConNombre(m_SesionBase.USUARIO_ID, UsuarioIDCon);
            ListadoJsonLocal LjLocal = ListadoJsonLocal.Cargar(ArchivoListadoNombre);
            if (LjLocal.Listado != null && BorraMensajesConEvent != null)
            {
                try
                {
                    BorraMensajesConEvent(LjLocal.Listado);
                }
                catch { }
            }

            m_S_Mails.BorraMensajesConCompleted += delegate(object sender, ES_Mails.BorraMensajesConCompletedEventArgs e)
            {
                try
                {
                    /// En estas primeras 4 lineas evalua si e.Result(resultado del servicio); es nulo si es diferente
                    /// de nulo, muestra un mensaje de Listado Obtenido si no manda Error.
                    if (e.Result != null)
                        m_SesionBase.MuestraMensaje("Borrando Mensajes", 3);
                    else
                        m_SesionBase.MuestraMensaje("error al Borrar Mensajes", 5);

                    /// En estas proximas 2 lineas evalua si e.Result es diferente de nulo "Y" si es diferente de "=="
                    /// Si cumple la condicion guarda el ListadoJson en un ARCHIVO LOCAL, en caso contrario no lo hace.
                    if (e.Result != null && e.Result != -1)
                        ListadoJsonLocal.Guarda(ArchivoListadoNombre, e.Result.ToString());

                    /// En estas proximas 5 lineas evalua si el evento "ObtenChatsConEvent" es diferente de null osea que si ya 
                    /// se le asigno una funcion al evento desde la VISTA, "Y" evalua si es diferente de "=="
                    /// Si se cumplen las dos condiciones evalua por ultimo si e.Result es diferente de null, si se cumple 
                    /// envia los resultados ala funcion asignada al evento "ObtenChatsConEvent"
                    if (BorraMensajesConEvent != null && e.Result != -1)
                    {
                        if (e.Result != null)
                            BorraMensajesConEvent(e.Result.ToString());
                    }
                    return;

                }
                catch (Exception ex)
                {
                    CeC_Log.AgregaError(ex);
                }
                m_SesionBase.MuestraMensaje("Error de Red", 5);
            };
            m_S_Mails.BorraMensajesConAsync(m_SesionBase.SESION_SEGURIDAD, UsuarioIDCon, LjLocal.Hash);
        }
        //****************************************************************************************************************************
        //****************************************************************************************************************************
        //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Resultados"></param>
        public delegate void EnviaMensajeArgs(bool Resultados);
        public event EnviaMensajeArgs EnviaMensajeEvent;

        public void EnviaMensaje(int UsuarioIDDestino, string Mensaje)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Chats");
            m_S_Mails.EnviaMensajeCompleted += m_S_Mails_EnviaMensajeCompleted;
            m_S_Mails.EnviaMensajeAsync(m_SesionBase.SESION_SEGURIDAD, UsuarioIDDestino, Mensaje, "");
        }

        void m_S_Mails_EnviaMensajeCompleted(object sender, ES_Mails.EnviaMensajeCompletedEventArgs e)
        {
            try
            {
                if (e.Result == true)
                    m_SesionBase.MuestraMensaje("Mensaje enviado", 3);
                else
                    m_SesionBase.MuestraMensaje("Error al enviar el mensaje", 5);

                if (EnviaMensajeEvent != null)
                    EnviaMensajeEvent(e.Result);

                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }


        //*****************************************************************
        public delegate void ObtenChatsConDesdeArgs(string Resultados);
        public event ObtenChatsConDesdeArgs ObtenChatsConDesdeEvent;

        public void ObtenChatsConDesde(int UsuarioIDCon, int MailID)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Chats");
            m_S_Mails.ObtenChatsConDesdeCompleted += m_S_Mails_ObtenChatsConDesdeCompleted;
            m_S_Mails.ObtenChatsConDesdeAsync(m_SesionBase.SESION_SEGURIDAD, UsuarioIDCon, MailID);
        }

        void m_S_Mails_ObtenChatsConDesdeCompleted(object sender, ES_Mails.ObtenChatsConDesdeCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                    m_SesionBase.MuestraMensaje("Mensaje Cargado", 3);
                else
                    m_SesionBase.MuestraMensaje("Error al cargar de los mensajes", 5);
                if (ObtenChatsConDesdeEvent != null)
                    ObtenChatsConDesdeEvent(e.Result);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }
        //*******************************************************
        public string ContactosArchivoNombre(int UsuarioID)
        {
            return "MailsC" + UsuarioID + ".Lst";
        }

        public delegate void ObtenContactosArgs(string Resultados);
        public event ObtenContactosArgs ObtenContactosEvent;

        public void ObtenContactos()
        {
            m_SesionBase.MuestraMensaje("Obteniendo Listado");
            string ArchivoListadoNombre = ContactosArchivoNombre(m_SesionBase.USUARIO_ID);
            ListadoJsonLocal LjLocal = ListadoJsonLocal.Cargar(ArchivoListadoNombre);
            if (LjLocal.Listado != null && ObtenContactosEvent != null)
            {
                try
                {
                    ObtenContactosEvent(LjLocal.Listado);
                }
                catch { }
            }

            m_S_Mails.ObtenContactosCompleted += delegate(object sender, ES_Mails.ObtenContactosCompletedEventArgs e)
            {
                try
                {
                    if (e.Result != null)
                        m_SesionBase.MuestraMensaje("Listado Obtenido", 3);
                    else
                        m_SesionBase.MuestraMensaje("Error al obtener el listado", 5);

                    if (e.Result != null && e.Result != "==")
                        ListadoJsonLocal.Guarda(ArchivoListadoNombre, e.Result);
                    if (ObtenContactosEvent != null && e.Result != "==")
                    {
                        if (e.Result != null)
                            ObtenContactosEvent(e.Result);
                    }

                    return;
                }
                catch (Exception ex)
                {
                    CeC_Log.AgregaError(ex);
                }
                m_SesionBase.MuestraMensaje("Error de Red", 5);
            };
            m_S_Mails.ObtenContactosAsync(m_SesionBase.SESION_SEGURIDAD, LjLocal.Hash);
        }
        //**************************************************************************************





    }
}
