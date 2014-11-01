using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Localizaciones
    {
        ES_Localizaciones.S_LocalizacionesClient m_S_Localizaciones = null;
        CeC_SesionBase m_SesionBase = null;

        public Localizaciones(CeC_SesionBase SesionBase)
        {
            m_S_Localizaciones = new ES_Localizaciones.S_LocalizacionesClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Localizaciones.svc"));
            m_SesionBase = SesionBase;
        }

        public delegate void ObtenTextoHArgs(object Destino, string Texto);
        public event ObtenTextoHArgs ObtenTextoEvent;

        public void ObtenEtiqueta(string LocalizacionLlave, string TextoPredeterminado, object Destino)
        {
            ObtenEtiqueta(m_SesionBase.IDIOMA, LocalizacionLlave, TextoPredeterminado, Destino);
        }

        public void ObtenEtiqueta(string LocalizacionIdioma, string LocalizacionLlave, string TextoPredeterminado, object Destino)
        {
            if (LocalizacionIdioma == "C#")
            {
                if (ObtenTextoEvent != null)
                    ObtenTextoEvent(Destino, LocalizacionLlave);
                return;
            }
            if (m_Localizaciones != null && m_Localizaciones.Count > 01)
            {
                try
                {
                    var Resultado = from R in m_Localizaciones
                                    where R.LOCALIZACION_LLAVE == LocalizacionLlave
                                    select R.LOCALIZACION_ETIQUETA;
                    string Texto = null;
                    if (Resultado.Count() > 00)
                    {
                        Texto = Resultado.Min<string>();
                    }
                    else
                    {

                    }
                    if (Texto == null)
                        Texto = TextoPredeterminado;
                    if (ObtenTextoEvent != null)
                        ObtenTextoEvent(Destino, Texto);
                }
                catch
                {
                }
                return;
            }

#if DEBUG
            try
            {
                m_S_Localizaciones.ObtenEtiquetaDebugCompleted += delegate(object sender, ES_Localizaciones.ObtenEtiquetaDebugCompletedEventArgs e)
                {
                    try
                    {
                        if (ObtenTextoEvent != null)
                            ObtenTextoEvent(Destino, e.Result);
                    }
                    catch { m_SesionBase.MuestraMensaje("No se obtuvieron los Links por que no hay Conexión."); }

                };
                m_S_Localizaciones.ObtenEtiquetaDebugAsync(LocalizacionIdioma, LocalizacionLlave, TextoPredeterminado);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
#else
            m_S_Localizaciones.ObtenEtiquetaCompleted += delegate(object sender, ES_Localizaciones.ObtenEtiquetaCompletedEventArgs e)
            {
                try
                {
                    if (ObtenTextoEvent != null)
                        ObtenTextoEvent(Destino, e.Result);
                }
                catch { m_SesionBase.MuestraMensaje("No se obtuvieron los Links por que no hay Conexión."); }
            };
            m_S_Localizaciones.ObtenEtiquetaAsync(LocalizacionIdioma, LocalizacionLlave);
#endif

        }
        public event System.EventHandler<ES_Localizaciones.ObtenAyudaCompletedEventArgs> ObtenAyudaCompleted;
        public void ObtenAyuda(string LocalizacionIdioma, string LocalizacionLlave, object Destino)
        {
            if (LocalizacionIdioma == "C#")
            {
                if (ObtenTextoEvent != null)
                    ObtenTextoEvent(Destino, LocalizacionLlave);
                return;
            }
            if (m_Localizaciones != null)
            {
                try
                {
                    var Resultado = from R in m_Localizaciones
                                    where R.LOCALIZACION_LLAVE == LocalizacionLlave
                                    select R.LOCALIZACION_AYUDA;
                    if (Resultado.Count() > 00)
                    {
                        if (ObtenTextoEvent != null)
                            ObtenTextoEvent(Destino, Resultado.Min<string>());
                        return;
                    }
                    else
                    {
                        string Texto = "";
                    }

                }
                catch
                { }
            }

            m_S_Localizaciones.ObtenAyudaCompleted += delegate(object sender, ES_Localizaciones.ObtenAyudaCompletedEventArgs e)
            {
                try
                {
                    if (ObtenAyudaCompleted != null)
                        ObtenAyudaCompleted(Destino, e);
                    if (ObtenTextoEvent != null)
                        ObtenTextoEvent(Destino, e.Result);
                }
                catch { m_SesionBase.MuestraMensaje("No se obtuvieron los Links por que no hay Conexión."); }

            };
            m_S_Localizaciones.ObtenAyudaAsync(LocalizacionIdioma, LocalizacionLlave);
        }


        /// <summary>
        /// Funcion delegado que se encarga de mandar a llamar el
        /// servicio de ObtenEtiquetasAyuda.
        /// </summary>
        /// <param name="Resultado"></param>


        public string NombreArchivo
        {
            get { return m_SesionBase.IDIOMA + ".loc"; }
        }
        public static List<Modelos.Localizaciones.Model_Localizaciones> m_Localizaciones = null;
        public void ObtenEtiquetasAyuda()
        {
            try
            {
                //Controladores.Sesion.CerrarSesionArgs
                ListadoJsonLocal JsonLocal = ListadoJsonLocal.Cargar(NombreArchivo);
                if (JsonLocal.Hash != "")
                {
                    m_Localizaciones = Modelos.Localizaciones.Model_Localizaciones.Nuevos(JsonLocal);
                }
                m_SesionBase.MuestraMensaje("Downloading Labels");
                m_S_Localizaciones.ObtenEtiquetasAyudaCompleted += delegate(object sender, ES_Localizaciones.ObtenEtiquetasAyudaCompletedEventArgs e)
                    {
                        try
                        {
                            CeC_Log.AgregaLog("Log");
                            string EtiquetasAyuda;
                            if (e.Result == null)
                            {
                                m_SesionBase.MuestraMensaje("Sin datos", 5);
                                return;
                            }
                            if (e.Result == "==")
                            {
                                m_SesionBase.MuestraMensaje("Listo", 3);
                                return;
                            }
                            //CeC_Log.AgregaLog(e.Result);
                            string Resultado = eClockBase.Controladores.CeC_ZLib.ZJson2Json(e.Result);
                            JsonLocal = new ListadoJsonLocal(Resultado);
                            CeC_Log.AgregaLog("Guardando...");
                            JsonLocal.lGuarda(NombreArchivo);
                            CeC_Log.AgregaLog("Nuevo");
                            m_Localizaciones = Modelos.Localizaciones.Model_Localizaciones.Nuevos(JsonLocal);
                            CeC_Log.AgregaLog("Mensaje");
                            m_SesionBase.MuestraMensaje("Etiquetas Obtenidas", 3);

                            return;
                        }
                        catch (Exception ex)
                        {
                            CeC_Log.AgregaLog(ex.InnerException.StackTrace);

                            CeC_Log.AgregaError(ex);
                        }

                        m_SesionBase.MuestraMensaje("Error al obtener localizaciones", 5);
                    };
                m_S_Localizaciones.ObtenEtiquetasAyudaAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.IDIOMA, JsonLocal.Hash);
            }
            catch (Exception exx)
            {
                CeC_Log.AgregaLog(exx.InnerException.StackTrace);
                m_SesionBase.MuestraMensaje("Error al obtener localizaciones", 5);
                CeC_Log.AgregaError(exx);
            }

           
        }

        public static void sObtenEtiquetasAyuda(CeC_SesionBase SesionBase)
        {
            Localizaciones Loc = new Localizaciones(SesionBase);
            Loc.ObtenEtiquetasAyuda();
        }

    }
}
