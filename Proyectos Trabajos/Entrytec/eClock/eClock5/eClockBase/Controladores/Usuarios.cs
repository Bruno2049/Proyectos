using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Usuarios
    {
        ES_Usuarios.S_UsuariosClient m_S_Usuarios = null;
        CeC_SesionBase m_SesionBase = null;

        public Usuarios(CeC_SesionBase SesionBase)
        {
            m_S_Usuarios = new ES_Usuarios.S_UsuariosClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Usuarios.svc"));
            m_SesionBase = SesionBase;
        }
        private string m_Usuario;
        private string m_Clave;
        public void CreaUsuarioSuscripcion(string Usuario, string Clave, string Nombre, string Descripcion, string eMail)
        {
            m_Usuario = Usuario;
            m_Clave = Clave;
            m_S_Usuarios.CreaUsuarioSuscripcionCompleted += m_S_Usuarios_CreaUsuarioSuscripcionCompleted;
            m_S_Usuarios.CreaUsuarioSuscripcionAsync("SETUP", Usuario, Clave, Nombre, Descripcion, eMail);
        }

        void m_S_Usuarios_CreaUsuarioSuscripcionCompleted(object sender, ES_Usuarios.CreaUsuarioSuscripcionCompletedEventArgs e)
        {
            if (e.Result > 0)
            {

                Sesion NuevaSesion = new Sesion(m_SesionBase);
                NuevaSesion.LogeoFinalizado += NuevaSesion_LogeoFinalizado;
                NuevaSesion.CreaSesionCompleted += NuevaSesion_CreaSesionCompleted;
                NuevaSesion.CreaSesion_Inicio(m_Usuario, m_Clave, false);

            }
            else
                SuscripcionCreadaEvent(false, e.Result);
        }

        void NuevaSesion_CreaSesionCompleted(object sender, ES_Sesion.CreaSesionCompletedEventArgs e)
        {
            if (!m_SesionBase.EstaLogeado())
                SuscripcionCreadaEvent(false, 1);
        }

        public delegate void SuscripcionCreadaArgs(bool Guardado, int Resultado);
        public event SuscripcionCreadaArgs SuscripcionCreadaEvent;

        void NuevaSesion_LogeoFinalizado(object sender, EventArgs e)
        {

            SuscripcionCreadaEvent(true, 1);
        }

        public delegate void CreadoUsuarioEmpleadoArgs(string Resultado);
        public event CreadoUsuarioEmpleadoArgs CreadoUsuarioEmpleado;

        public void CreaUsuarioEmpleado(string NoEmpleado, string Clave, string eMail)
        {
            CreaUsuarioEmpleado(NoEmpleado, Clave, eMail, m_SesionBase.Suscripcion);
        }
        public void CreaUsuarioEmpleado(string NoEmpleado, string Clave, string eMail, string Suscripcion)
        {
            m_SesionBase.MuestraMensaje("Creando Usuario..");
            m_S_Usuarios.CreaUsuarioEmpleadoCompleted += m_S_Usuarios_CreaUsuarioEmpleadoCompleted;
            m_S_Usuarios.CreaUsuarioEmpleadoAsync(NoEmpleado, Clave, eMail, Suscripcion);
        }
        void m_S_Usuarios_CreaUsuarioEmpleadoCompleted(object sender, ES_Usuarios.CreaUsuarioEmpleadoCompletedEventArgs e)
        {
            
            try
            {
                if(e.Result == "OK")
                    m_SesionBase.MuestraMensaje("Usuario Creado",3);
                else
                    m_SesionBase.MuestraMensaje("No se pudo crear", 5);
                if (CreadoUsuarioEmpleado != null)
                    CreadoUsuarioEmpleado(e.Result);
                return;
            }
            catch { }
            if (CreadoUsuarioEmpleado != null)
                CreadoUsuarioEmpleado("No hay Servidor");
            m_SesionBase.MuestraMensaje("Error", 5);
        }


        public delegate void OlvidoClaveEmpleadoArgs(string Resultado);
        public event OlvidoClaveEmpleadoArgs OlvidoClaveEmpleadoEvent;

        public void OlvidoClaveEmpleado(string NoEmpleado, string Suscripcion)
        {
            m_SesionBase.MuestraMensaje("Recuperando Contraseña..");
            m_S_Usuarios.OlvidoClaveEmpleadoCompleted += m_S_Usuarios_OlvidoClaveEmpleadoCompleted;
            m_S_Usuarios.OlvidoClaveEmpleadoAsync(NoEmpleado, Suscripcion);
        }

        void m_S_Usuarios_OlvidoClaveEmpleadoCompleted(object sender, ES_Usuarios.OlvidoClaveEmpleadoCompletedEventArgs e)
        {
            try
            {
                if (e.Result == "OK")
                    m_SesionBase.MuestraMensaje("Contraseña enviada a su e-mail", 3);
                else
                    m_SesionBase.MuestraMensaje("Error al recuperar contraseña", 5);
                if (OlvidoClaveEmpleadoEvent != null)
                    OlvidoClaveEmpleadoEvent(e.Result);

                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            if (OlvidoClaveEmpleadoEvent != null)
                OlvidoClaveEmpleadoEvent("ERROR_RED");
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }

        public delegate void OlvidoClaveArgs(string Resultado);
        public event OlvidoClaveArgs OlvidoClaveEvent;

        public void OlvidoClave(string UsuarioOeMail, string Firma)
        {
            m_SesionBase.MuestraMensaje("Recuperando Contraseña..");
            m_S_Usuarios.OlvidoClaveCompleted += m_S_Usuarios_OlvidoClaveCompleted;
            m_S_Usuarios.OlvidoClaveAsync(UsuarioOeMail, Firma);
        }

        void m_S_Usuarios_OlvidoClaveCompleted(object sender, ES_Usuarios.OlvidoClaveCompletedEventArgs e)
        {
            try
            {
                if (e.Result == "OK")
                    m_SesionBase.MuestraMensaje("Contraseña enviada a su e-mail", 3);
                else
                    m_SesionBase.MuestraMensaje("Error al recuperar contraseña", 5);

                if (OlvidoClaveEvent != null)
                    OlvidoClaveEvent(e.Result);

                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            if (OlvidoClaveEvent != null)
                OlvidoClaveEvent("ERROR_RED");

            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }

        public delegate void CambioPasswordArgs(bool Resultado);
            public event CambioPasswordArgs CambioPasswordEvent;

        public void CambioPassword(string ClaveAnterior, string ClaveNueva)
        {
            m_SesionBase.MuestraMensaje("Recuperando Contraseña..");
            m_S_Usuarios.CambiaPasswordCompleted += m_S_Usuarios_CambiaPasswordCompleted;
            m_S_Usuarios.CambiaPasswordAsync(m_SesionBase.SESION_SEGURIDAD, ClaveAnterior, ClaveNueva);
        }

        void m_S_Usuarios_CambiaPasswordCompleted(object sender, ES_Usuarios.CambiaPasswordCompletedEventArgs e)
        {
            try
            {
                if (e.Result == true)
                    m_SesionBase.MuestraMensaje("Contraseña Cambiada", 3);
                if (CambioPasswordEvent != null)
                    CambioPasswordEvent(e.Result);
                else
                    m_SesionBase.MuestraMensaje("Error al cambiar la contraseña", 5);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }


        public delegate void ValidaPasswordArgs(bool Resultado);
        public event ValidaPasswordArgs ValidaPasswordEvent;

        public void ValidaPassword(string Clave)
        {
            m_SesionBase.MuestraMensaje("Validando Password..");
            m_S_Usuarios.ValidaPasswordCompleted += m_S_Usuarios_ValidaPasswordCompleted;
            m_S_Usuarios.ValidaPasswordAsync(m_SesionBase.SESION_SEGURIDAD, Clave);
        }

        void m_S_Usuarios_ValidaPasswordCompleted(object sender, ES_Usuarios.ValidaPasswordCompletedEventArgs e)
        {
            try
            {
                if (e.Result == true)
                {
                    m_SesionBase.MuestraMensaje("Contraseña correcta", 3);
                    if (ValidaPasswordEvent != null)
                        ValidaPasswordEvent(e.Result);
                }
                else
                {
                    m_SesionBase.MuestraMensaje("Error de Contraseña", 5);
                    if (ValidaPasswordEvent != null)
                        ValidaPasswordEvent(e.Result);
                }
                
                //return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }

        public delegate void ObtenerUsuarioSincronizadorArgs(Modelos.Model_USUARIOS UsuarioSuscripcion);
        public event ObtenerUsuarioSincronizadorArgs ObtenerUsuarioSincronizadorEvent;

        public void ObtenerUsuarioSincronizador()
        {
            m_SesionBase.MuestraMensaje("Obteniendo datos..");
            m_S_Usuarios.ObtenerUsuarioSincronizadorCompleted += m_S_Usuarios_ObtenerUsuarioSincronizadorCompleted;
            m_S_Usuarios.ObtenerUsuarioSincronizadorAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA);
        }

        void m_S_Usuarios_ObtenerUsuarioSincronizadorCompleted(object sender, ES_Usuarios.ObtenerUsuarioSincronizadorCompletedEventArgs e)
        {
            
            try
            {
                if (ObtenerUsuarioSincronizadorEvent != null)
                    ObtenerUsuarioSincronizadorEvent(eClockBase.Controladores.CeC_ZLib.Json2Object<Modelos.Model_USUARIOS>(e.Result));
                    m_SesionBase.MuestraMensaje("Datos Obtenidos", 5);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
            if (ObtenerUsuarioSincronizadorEvent != null)
                ObtenerUsuarioSincronizadorEvent(null);
        }
    }
}
