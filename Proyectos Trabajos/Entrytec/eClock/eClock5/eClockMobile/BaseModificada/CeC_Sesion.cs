using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web;
namespace eClockMobile.BaseModificada
{
    public class CeC_Sesion : eClockBase.CeC_SesionBase
    {

        public static DateTime FechaInicio
        {
            get
            {
                DateTime dtFechaInicio = CeC.Convierte2DateTime(HttpContext.Current.Session["FechaInicio"]);
                if (dtFechaInicio == CeC.FechaNula || dtFechaInicio == CeC.FechaNull)
                {
                    dtFechaInicio = DateTime.Now.AddDays(-7);
                }
                return dtFechaInicio;
            }
            set { HttpContext.Current.Session["FechaInicio"] = value; }
        }
        public static DateTime FechaFin
        {
            get
            {
                DateTime dtFechaFin = CeC.Convierte2DateTime(HttpContext.Current.Session["FechaFin"]);
                if (dtFechaFin == CeC.FechaNula || dtFechaFin == CeC.FechaNull)
                {
                    dtFechaFin = DateTime.Now.AddDays(-1);

                }
                return dtFechaFin;
            }
            set { HttpContext.Current.Session["FechaFin"] = value; }
        }


        public static eClockBase.CeC_SesionBase ObtenSesion(object VentanaActual)
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["m_SesionActual"] != null)
                    return (eClockBase.CeC_SesionBase)System.Web.HttpContext.Current.Session["m_SesionActual"];
                else
                {
                    HttpCookie CkeClock = System.Web.HttpContext.Current.Request.Cookies["SESION_SEGURIDAD"];
                    if (CkeClock != null)
                    {
                        string SesionSeguridad = CkeClock.Value;
                        CeC_Sesion Cargar = new CeC_Sesion();
                        Cargar.RutaServicios = eClockMobile.Properties.Settings.Default.eClockServicesUrl;
                        Cargar.SESION_SEGURIDAD = SesionSeguridad;
                        Cargar.GuardaCopiaLocal = false;
                        eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Cargar);
                        cSesion.ObtenSesionDatosEvent += delegate(bool Correcto)
                        {
                            if (Correcto == false)
                            {
                                CierraSesion(VentanaActual);
                            }
                            else
                            {
                                if (Cargar.SUSCRIPCION_ID_SELECCIONADA == -1)
                                    Cargar.SUSCRIPCION_ID_SELECCIONADA = Cargar.SUSCRIPCION_ID;
                                string sCorrecto = Correcto.ToString();
                            }
                        };
                        cSesion.ObtenSesionDatos();
                        System.Web.Security.FormsAuthentication.SetAuthCookie(Cargar.SESION_SEGURIDAD, false);
                        return (eClockBase.CeC_SesionBase)(System.Web.HttpContext.Current.Session["m_SesionActual"] = Cargar);
                    }
                }
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            CeC_Sesion Nueva = new CeC_Sesion();
            Nueva.RutaServicios = eClockMobile.Properties.Settings.Default.eClockServicesUrl;
            Nueva.GuardaCopiaLocal = false;
            return (eClockBase.CeC_SesionBase)(System.Web.HttpContext.Current.Session["m_SesionActual"] = Nueva);
        }

        public static bool CierraSesion(object VentanaActual)
        {
            try
            {
                eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(VentanaActual);
                eClockBase.Controladores.Sesion cSesion = new eClockBase.Controladores.Sesion(Sesion);
                cSesion.CerrarSesion();

                HttpCookie SS = new HttpCookie("SESION_SEGURIDAD", null);
                SS.Expires = DateTime.Now;
                System.Web.HttpContext.Current.Response.SetCookie(SS);

                System.Web.Security.FormsAuthentication.SignOut();
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        public static bool EsPersona
        {
            get
            {
                eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(null);
                if (Sesion.Mdl_Sesion == null || Sesion.Mdl_Sesion.PERSONA_ID <= 0)
                    return false;
                return true;
            }
        }

        public static bool EsKiosco
        {
            get
            {
                eClockBase.CeC_SesionBase Sesion = BaseModificada.CeC_Sesion.ObtenSesion(null);
                if (Sesion.Mdl_Sesion == null || Sesion.Mdl_Sesion.PERFIL_ID != 7)
                    return false;
                return true;
            }
        }
        public override void MuestraMensaje(string Mensaje, int TimeOutSegundos = -1)
        {
            base.MuestraMensaje(Mensaje, TimeOutSegundos);

            if (TimeOutSegundos > 0)
            {


            }
            else
            {

                //Lbl_Estado.Opacity = 1;
            }

        }
        public override bool AsignaControlMensaje(object Control)
        {

            return base.AsignaControlMensaje(Control);
        }

        public override bool GuardaDatos()
        {
            try
            {
                System.Web.HttpContext.Current.Session["m_SesionActual"] = this;
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }


    }
}
