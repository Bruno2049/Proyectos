using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase
{
    public class CeC_SesionBase
    {

        static public DateTime FechaNula { get { return new DateTime(2006, 01, 1); } }

        /// <summary>
        /// Guarda una copia local para hacer más rápida la consulta, no usar en web
        /// </summary>
        public bool GuardaCopiaLocal = true;

        string m_SesionSeguridad = "";
        /// <summary>
        /// Obtiene o establece el valor de la seguridad de sesión
        /// </summary>
        public string SESION_SEGURIDAD
        {
            get { return m_SesionSeguridad; }
            set { m_SesionSeguridad = value; }
        }

        private int m_USUARIO_ID = -1;

        public int USUARIO_ID
        {
            get { return m_USUARIO_ID; }
            set { m_USUARIO_ID = value; }
        }

        int m_SUSCRIPCION_ID_SELECCIONADA = -1;
        /// <summary>
        /// Contiene la suscripcionID Seleccionada
        /// </summary>
        public int SUSCRIPCION_ID_SELECCIONADA
        {
            get
            {

                if (m_SUSCRIPCION_ID_SELECCIONADA < 0)
                {
                    m_SUSCRIPCION_ID_SELECCIONADA = SUSCRIPCION_ID;
                }
                return m_SUSCRIPCION_ID_SELECCIONADA;
            }

            set { m_SUSCRIPCION_ID_SELECCIONADA = value; }
        }

        int m_SUSCRIPCION_ID = -1;
        public int SUSCRIPCION_ID
        {
            get { return m_SUSCRIPCION_ID; }

            set { m_SUSCRIPCION_ID = value; }
        }

        string m_IDIOMA = "es";
        /// <summary>
        /// Obtiene o Establece el idioma
        /// </summary>
        public string IDIOMA
        {
            get { return m_IDIOMA; }
            set
            {
                m_IDIOMA = value;

            }
        }

        private bool m_MantenerSesion = false;

        public bool MantenerSesion
        {
            get { return m_MantenerSesion; }
            set { m_MantenerSesion = value; }
        }

        private string m_RutaServicios = "http://services.eclock.com.mx";

        public string RutaServicios
        {
            get { return m_RutaServicios; }
            set { m_RutaServicios = value; }
        }


        private string m_Licencia = "";

        public string Licencia
        {
            get { return m_Licencia; }
            set { m_Licencia = value; }
        }

        private string m_Maquina = "";

        public string Maquina
        {
            get
            {
                if (m_Maquina == "")
                {

                }
                return m_Maquina;
            }
            set { m_Maquina = value; }
        }

        private string m_Suscripcion = "";

        public string Suscripcion
        {
            get { return m_Suscripcion; }
            set { m_Suscripcion = value; }
        }



        private eClockBase.Modelos.Sesion.Model_Sesion m_Mdl_Sesion = null;

        public eClockBase.Modelos.Sesion.Model_Sesion Mdl_Sesion
        {
            get { return m_Mdl_Sesion; }
            set { m_Mdl_Sesion = value; }
        }

        public bool EstaLogeado()
        {
            /*  if (!System.Web.HttpContext.Current.Request.IsAuthenticated)
                  return false;*/
            return SESION_SEGURIDAD.Length > 0;
        }

        public string ObtenRutaServicio(string Servicio)
        {
            string RutaServicio = m_RutaServicios;
            if (m_RutaServicios[m_RutaServicios.Length - 1] != '/')
                RutaServicio += "/";
            RutaServicio += Servicio;
            return RutaServicio;
        }

        public System.ServiceModel.EndpointAddress ObtenEndpointAddress(string Servicio)
        {
            return new System.ServiceModel.EndpointAddress(ObtenRutaServicio(Servicio));
        }

        public System.ServiceModel.BasicHttpBinding ObtenBasicHttpBinding()
        {
            System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
            result.MaxBufferSize = int.MaxValue;
            
            result.MaxReceivedMessageSize = int.MaxValue;
            result.GetType().GetProperty("ReaderQuotas").SetValue(result, System.Xml.XmlDictionaryReaderQuotas.Max, null);
            /*
            System.Xml.XmlDictionaryReaderQuotas myReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas();
            myReaderQuotas.MaxStringContentLength = 2147483647;
            myReaderQuotas.MaxArrayLength = 2147483647; 
            myReaderQuotas.MaxBytesPerRead = 2147483647;
            myReaderQuotas.MaxDepth = 2147483647;
            myReaderQuotas.MaxNameTableCharCount = 2147483647;*/
            //System.Reflection.PropertyInfo PI = result.GetType().GetProperty("ReaderQuotas");
            
            //PI.GetType().GetProperty("MaxStringContentLength").SetValue(PI, 2147483647, null);
            //result.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            //result.TextEncoding =  Encoding.
            result.ReceiveTimeout = new TimeSpan(0, 10, 0);
            result.CloseTimeout = new TimeSpan(0, 10, 0);
            result.OpenTimeout = new TimeSpan(0, 10, 0);
            result.SendTimeout = new TimeSpan(0, 10, 0);
            //result.rea
            /*result.

                /*
                 * closeTimeout="00:10:00" openTimeout="00:10:00"
          receiveTimeout="00:10:00" sendTimeout="00:10:00" maxBufferPoolSize="2147483647"
          maxBufferSize="2147483647" maxReceivedMessageSize="2147483647">
          <security mode="None" />
<readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647"  maxArrayLength="2147483647" maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647" />*/
            return result;
        }

        public virtual bool AsignaControlMensaje(object Control)
        {
            return true;
        }
        protected bool m_EstaMostrandoMensaje = false;
        public virtual bool EstaMostrandoMensaje()
        {
            return m_EstaMostrandoMensaje;
        }

        public virtual void MuestraMensaje(string Mensaje, int TimeOutSegundos = -1)
        {

        }

        #region Guardar y leer
        static CeC_SesionBase m_VariablesSesion = new CeC_SesionBase();
        class Valores
        {
            public string Variable;
            public object Contenido;
            public Valores(string VariableSesion, object ContenidoSesion)
            {
                Variable = VariableSesion;
                Contenido = ContenidoSesion;
            }
        }
        static List<Valores> m_Valores = new List<Valores>();
        /// <summary>
        /// Guarda el Objeto Sesion
        /// </summary>
        /// <param name="VariableSesion"></param>
        /// <param name="Contenido"></param>
        /// <returns></returns>
        public virtual bool GuardaObjectSesion(string VariableSesion, object Contenido)
        {
            try
            {
                var results = from V in m_Valores
                              where V.Variable == VariableSesion
                              select V;
                foreach (var result in results)
                {
                    result.Contenido = Contenido;
                    return true;
                }
                m_Valores.Add(new Valores(VariableSesion, Contenido));
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VariableSesion"></param>
        /// <returns></returns>
        public virtual object LeeObjectSesion(string VariableSesion)
        {
            try
            {
                var results = from V in m_Valores
                              where V.Variable == VariableSesion
                              select V;
                foreach (var result in results)
                {
                    return result.Contenido;
                }
                return null;

            }
            catch (Exception Ex)
            {
                //		Redirige("Default.htm");
                //				Pagina.Server.Transfer("Cerrar.htm");
                return "";
            }
        }

        /// <summary>
        /// Guarda la Pagina Sesion
        /// </summary>
        /// <param name="Pagina"></param>
        /// <param name="VariableSesion"></param>
        /// <param name="Contenido"></param>
        /// <returns></returns>
        private static bool GuardaStringSesion(string VariableSesion, string Contenido)
        {
            try
            {
                return m_VariablesSesion.GuardaObjectSesion(VariableSesion, Contenido);
            }
            catch (Exception Ex)
            {
                return false;
            }
        }



        /// <summary>
        /// Intenta obtener la Sesion 
        /// </summary>
        /// <param name="Pagina"></param>
        /// <param name="VariableSesion"></param>
        /// <returns></returns>
        private static string LeeStringSesion(string VariableSesion)
        {
            return LeeStringSesion(VariableSesion, "");
        }
        private static string LeeStringSesion(string VariableSesion, string Predeterminado)
        {
            try
            {
                string Valor = (string)m_VariablesSesion.LeeObjectSesion(VariableSesion);
                if (Valor == null)
                    return Predeterminado;
                return (string)Valor;
            }
            catch (Exception Ex)
            {
                return Predeterminado;
            }
        }

        /// <summary>
        /// Lee el instante de tiempo de la sesion
        /// </summary>
        /// <param name="Pagina"></param>
        /// <param name="VariableSesion"></param>
        /// <returns></returns>
        private static DateTime LeeDateTimeSesion(string VariableSesion, DateTime FechaPredeterminada)
        {
            try
            {
                DateTime Temp = Convert.ToDateTime(m_VariablesSesion.LeeObjectSesion(VariableSesion));
                if (Temp.Year <= 1)
                    return FechaPredeterminada;
                return Temp;
            }
            catch (Exception Ex)
            {
                return FechaPredeterminada;
            }
        }

        private static DateTime LeeDateTimeSesion(string VariableSesion)
        {
            return LeeDateTimeSesion(VariableSesion, FechaNula);
        }
        /// <summary>
        /// Guarda el instante de tiempo de la sesion
        /// </summary>
        /// <param name="Pagina"></param>
        /// <param name="VariableSesion"></param>
        /// <param name="Contenido"></param>
        /// <returns></returns>
        private static bool GuardaDateTimeSesion(string VariableSesion, DateTime Contenido)
        {
            try
            {
                return m_VariablesSesion.GuardaObjectSesion(VariableSesion, Contenido);
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Intenta guardar la sesion
        /// </summary>
        /// <param name="Pagina"></param>
        /// <param name="VariableSesion"></param>
        /// <param name="Contenido"></param>
        /// <returns></returns>
        private static bool GuardaIntSesion(string VariableSesion, int Contenido)
        {
            try
            {

                return m_VariablesSesion.GuardaObjectSesion(VariableSesion, Contenido);
            }
            catch (Exception Ex)
            {
                //	Redirige("Default.htm");
                return false;
            }
        }

        /// <summary>
        /// Regresa la sesion 
        /// </summary>
        /// <param name="Pagina"></param>
        /// <param name="VariableSesion"></param>
        /// <param name="Predeterminado"></param>
        /// <returns></returns>
        private static int LeeIntSesion(string VariableSesion, int Predeterminado)
        {
            try
            {
                return (int)m_VariablesSesion.LeeObjectSesion(VariableSesion);
            }
            catch (Exception Ex)
            {
                return Predeterminado;
            }
        }
        private static bool GuardaBoolSesion(string VariableSesion, bool Valor)
        {
            return m_VariablesSesion.GuardaObjectSesion(VariableSesion, Valor);
        }

        private static bool LeeBoolSesion(string VariableSesion, bool Predeterminado)
        {
            try
            {
                return (bool)m_VariablesSesion.LeeObjectSesion(VariableSesion);
            }
            catch { }
            return Predeterminado;
        }

        #endregion

        public static string CargarDatosString()
        {
            try
            {
                return CeC_Stream.sLeerString("CeC_SesionBase.json");
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return "";
        }
        public static CeC_SesionBase CargarDatos()
        {
            try
            {
                return eClockBase.Controladores.CeC_ZLib.Json2Object<CeC_SesionBase>(CargarDatosString());
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return null;
        }
        public virtual bool GuardaDatos()
        {
            try
            {
                CeC_Stream.sNuevoTexto("CeC_SesionBase.json", JsonConvert.SerializeObject(this));
                return
                    true;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return false;
        }

        public bool LimpiaDatos()
        {
            m_SesionSeguridad = "";
            m_USUARIO_ID = -1;
            return GuardaDatos();
        }

    }
}
