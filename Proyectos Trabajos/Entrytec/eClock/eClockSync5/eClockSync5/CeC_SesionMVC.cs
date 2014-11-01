using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace eClockSync5
{
    public class CeC_SesionMVC
    {
        public static DateTime FechaNula { get { return new DateTime(2006, 01, 1); } }


        /// <summary>
        /// Obtiene o establece el valor de la seguridad de sesión
        /// </summary>
        public static string SESION_SEGURIDAD
        {
            get { return LeeStringSesion("SESION_SEGURIDAD"); }
            set { GuardaStringSesion("SESION_SEGURIDAD", value); }
        }
        /// <summary>
        /// Contiene la suscripcionID Seleccionada
        /// </summary>
        public static int SUSCRIPCION_ID_SELECCIONADA
        {
            get
            {
                int SuscripcionID = LeeIntSesion("SUSCRIPCION_ID_SELECCIONADA",-1);
                if (SuscripcionID < 0)
                {
                    SuscripcionID = SUSCRIPCION_ID_SELECCIONADA = SUSCRIPCION_ID;
                }
                return SuscripcionID;
            }

            set { GuardaIntSesion("SUSCRIPCION_ID_SELECCIONADA", value); }
        }
        public static int SUSCRIPCION_ID
        {
            get
            {
                int SuscripcionID = LeeIntSesion("SUSCRIPCION_ID", -1);
                if (SuscripcionID < 0)
                {
                    eClockSync5.ES_Sesion.S_SesionClient cSSesion = new ES_Sesion.S_SesionClient();
                    SuscripcionID = SUSCRIPCION_ID = cSSesion.ObtenSuscripcionID(SESION_SEGURIDAD);
                }
                return SuscripcionID;
            }

            set { GuardaIntSesion("SUSCRIPCION_ID", value); }
        }

        /// <summary>
        /// Obtiene o Establece el idioma
        /// </summary>
        //public static string IDIOMA
        //{
        //    get
        //    {

        //        string Idioma = LeeStringSesion("IDIOMA", "");
        //        //Si no tiene idioma verifica en la configuración
        //        if (Idioma == "")
        //        {
        //            if (EstaLogeado())
        //            {
        //                Idioma = CeC_ConfigUsuarioMVC.IDIOMA;
        //            }
        //        }
        //        //Asigna el idioma predeterminado
        //        if (Idioma == "")
        //            Idioma = "es";
        //        return Idioma;
        //    }
        //    set
        //    {
        //        // Aplica el idioma a todo el eClock Mobile con el parametro value
        //        GuardaStringSesion("IDIOMA", value);
        //        CultureInfo mostrarIdioma = new CultureInfo(value);
        //        System.Threading.Thread.CurrentThread.CurrentCulture = mostrarIdioma;
        //        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(mostrarIdioma.Name);
        //        System.Threading.Thread.CurrentThread.CurrentUICulture = mostrarIdioma;
        //    }
        //}

        public static bool EstaLogeado()
        {
            /*  if (!System.Web.HttpContext.Current.Request.IsAuthenticated)
                  return false;*/
            return SESION_SEGURIDAD.Length > 0;
        }

        /// <summary>
        /// Guarda el Objeto Sesion
        /// </summary>
        /// <param name="VariableSesion"></param>
        /// <param name="Contenido"></param>
        /// <returns></returns>
        private static bool GuardaObjectSesion(string VariableSesion, object Contenido)
        {
            try
            {
                System.Web.HttpContext.Current.Session[VariableSesion] = Contenido;
                return true;
            }
            catch (Exception Ex)
            {
                return false;
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
                return GuardaObjectSesion(VariableSesion, Contenido);
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
        private static object LeeObjectSesion(string VariableSesion)
        {
            try
            {
                object Temp;
                Temp = System.Web.HttpContext.Current.Session[VariableSesion];
                if (Temp == null)
                {
                    Temp = CeC_CookieMVC.LeeCookie(VariableSesion);
                    if (Temp != null)
                        GuardaObjectSesion(VariableSesion, Temp);
                }
                return Temp;

            }
            catch (Exception Ex)
            {
                //		Redirige("Default.htm");
                //				Pagina.Server.Transfer("Cerrar.htm");
                return "";
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
                string Valor = (string)LeeObjectSesion(VariableSesion);
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
                DateTime Temp = Convert.ToDateTime(LeeObjectSesion(VariableSesion));
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
                return GuardaObjectSesion(VariableSesion, Contenido);
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

                return GuardaObjectSesion(VariableSesion, Contenido);
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
                return (int)LeeObjectSesion(VariableSesion);
            }
            catch (Exception Ex)
            {
                return Predeterminado;
            }
        }
        private static bool GuardaBoolSesion(string VariableSesion, bool Valor)
        {
            return GuardaObjectSesion(VariableSesion, Valor);
        }

        private static bool LeeBoolSesion(string VariableSesion, bool Predeterminado)
        {
            try
            {
                return (bool)LeeObjectSesion(VariableSesion);
            }
            catch { }
            return Predeterminado;
        }

    }

    public class CeC_CookieMVC
    {

        private static string CookieName = "CkeClock";
        private static int m_Expiracion = 255;
        public static int Expiracion
        {
            get { return m_Expiracion; }
            set
            {
                if (value != m_Expiracion)
                {
                    m_Expiracion = value;
                    GuardaCookie("", "");
                }
            }
        }
        public static string SESION_SEGURIDAD
        {
            get
            {
                return LeeCookie("SESION_SEGURIDAD", "");
            }
            set
            {
                GuardaCookie("SESION_SEGURIDAD", value);
            }
        }
        public static object LeeCookie(string Variable)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains(CookieName))
                {
                    return HttpContext.Current.Request.Cookies[CookieName][Variable];
                }
            }
            catch { }
            return null;
        }
        public static string LeeCookie(string Variable, string Predeterminado)
        {
            try
            {
                return LeeCookie(Variable).ToString();
            }
            catch { }
            return Predeterminado;
        }

        public static bool GuardaCookie(string Variable, string Contenido)
        {
            try
            {
                HttpCookie myCookie = new HttpCookie(CookieName);
                if (Variable != "")
                    myCookie[Variable] = Contenido;
                myCookie.Expires = DateTime.Now.AddDays(Expiracion);
                HttpContext.Current.Response.Cookies.Add(myCookie);
                return true;
            }
            catch { }
            return false;
        }
        public static bool BorraCookie()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch { }
            return false;
        }
    }
}