using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading;

namespace eClockBase.Controladores
{
    public class Sesion
    {

        ES_Sesion.S_SesionClient m_S_Sesion = null;
        CeC_SesionBase m_SesionBase = null;
        public Sesion(CeC_SesionBase SesionBase)
        {
            m_S_Sesion = new ES_Sesion.S_SesionClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Sesion.svc"));
            m_SesionBase = SesionBase;
        }


        /// <summary>
        /// Se llama cuando se termino satisfactoriamente de logear y cargar propiedades genericas de sesion
        /// </summary>
        public event System.EventHandler LogeoFinalizado = null;

        public event System.EventHandler<ES_Sesion.CreaSesionCompletedEventArgs> CreaSesionCompleted = null;
        public void CreaSesion_Inicio(string Usuario, string Clave, bool MantenerSesion)
        {
            m_SesionBase.MuestraMensaje("Validando...");
            m_S_Sesion.CreaSesionCompleted += m_S_Sesion_CreaSesionCompleted;
            m_S_Sesion.CreaSesionCompleted += CreaSesionCompleted;
            m_SesionBase.MantenerSesion = MantenerSesion;
            m_S_Sesion.CreaSesionAsync(Usuario, Clave);
        }
        public void CreaSesion_InicioAdv(string Usuario, string Clave, bool MantenerSesion)
        {
            m_SesionBase.MuestraMensaje("Validando...");
            m_S_Sesion.CreaSesionAdvCompleted += m_S_Sesion_CreaSesionAdvCompleted;
            m_SesionBase.MantenerSesion = MantenerSesion;
            m_S_Sesion.CreaSesionAdvAsync(Usuario, Clave);
        }
        void m_S_Sesion_CreaSesionAdvCompleted(object sender, ES_Sesion.CreaSesionAdvCompletedEventArgs e)
        {
            try
            {
                if (e.Result != "")
                {
                    m_SesionBase.MuestraMensaje("Correcto", 1);
                    m_SesionBase.Mdl_Sesion = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Sesion.Model_Sesion>(e.Result);
                    m_SesionBase.SESION_SEGURIDAD = m_SesionBase.Mdl_Sesion.SESION_SEGURIDAD;
                    m_SesionBase.SUSCRIPCION_ID_SELECCIONADA = m_SesionBase.SUSCRIPCION_ID = m_SesionBase.Mdl_Sesion.SUSCRIPCION_ID;
                    m_SesionBase.USUARIO_ID = m_SesionBase.Mdl_Sesion.USUARIO_ID;
                    m_SesionBase.GuardaDatos();

                }
                else
                    m_SesionBase.MuestraMensaje("Error..", 5);
            }
            catch
            {
                m_SesionBase.MuestraMensaje("Error..", 5);
            }

            if (LogeoFinalizado != null)
                LogeoFinalizado(sender, e);
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Resultados"></param>
        public delegate void CreaSesionAdvSuscripcionArgs(string Resultados);
        public event CreaSesionAdvSuscripcionArgs CreaSesionAdvSuscripcionEvent;

        public void CreaSesionAdvSuscripcion(string Usuario, string Clave, bool MantenerSesion)
        {
            m_SesionBase.MuestraMensaje("Creando Sesion..");
            m_SesionBase.MantenerSesion = MantenerSesion;
            m_S_Sesion.CreaSesionAdvSuscripcionCompleted += m_S_Sesion_CreaSesionAdvSuscripcionCompleted;
            m_S_Sesion.CreaSesionAdvSuscripcionAsync(Usuario, Clave, m_SesionBase.Suscripcion);

        }

        void m_S_Sesion_CreaSesionAdvSuscripcionCompleted(object sender, ES_Sesion.CreaSesionAdvSuscripcionCompletedEventArgs e)
        {

            try
            {
                if (e.Result != null)
                {
                    if (e.Result == "")
                    {
                        m_SesionBase.MuestraMensaje("Usuario o clave no validos", 5);
                    }
                    else
                    {
                        m_SesionBase.MuestraMensaje("Sesion Creada", 1);
                        m_SesionBase.Mdl_Sesion = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Sesion.Model_Sesion>(e.Result);
                        m_SesionBase.SESION_SEGURIDAD = m_SesionBase.Mdl_Sesion.SESION_SEGURIDAD;
                        m_SesionBase.SUSCRIPCION_ID_SELECCIONADA = m_SesionBase.SUSCRIPCION_ID = m_SesionBase.Mdl_Sesion.SUSCRIPCION_ID;
                        m_SesionBase.USUARIO_ID = m_SesionBase.Mdl_Sesion.USUARIO_ID;
                        m_SesionBase.GuardaDatos();
                    }
                    if (CreaSesionAdvSuscripcionEvent != null)
                        CreaSesionAdvSuscripcionEvent(e.Result);
                }
                else
                    m_SesionBase.MuestraMensaje("Error al crear la sesion", 5);
                if (LogeoFinalizado != null)
                    LogeoFinalizado(sender, e);
                return;
            }
            catch
            {
            }
            if (CreaSesionAdvSuscripcionEvent != null)
                CreaSesionAdvSuscripcionEvent(null);

            if (LogeoFinalizado != null)
                LogeoFinalizado(sender, e);

            m_SesionBase.MuestraMensaje("Error de Red", 3);
        }

        void m_S_Sesion_CreaSesionCompleted(object sender, ES_Sesion.CreaSesionCompletedEventArgs e)
        {
            m_SesionBase.SESION_SEGURIDAD = e.Result;
            if (!m_SesionBase.EstaLogeado())
            {
                m_SesionBase.MuestraMensaje("Error..", 5);
                return;
            }
            m_SesionBase.MuestraMensaje("Correcto", 3);
            m_S_Sesion.ObtenSuscripcionIDCompleted += delegate(object OSsender, ES_Sesion.ObtenSuscripcionIDCompletedEventArgs OSe)
            {
                m_SesionBase.SUSCRIPCION_ID = OSe.Result;
                m_S_Sesion.ObtenUsuarioIDCompleted += delegate(object senderOU, ES_Sesion.ObtenUsuarioIDCompletedEventArgs eOU)
                {
                    m_SesionBase.USUARIO_ID = eOU.Result;
                    m_SesionBase.GuardaDatos();
                    if (LogeoFinalizado != null)
                        LogeoFinalizado(sender, e);
                };
                m_S_Sesion.ObtenUsuarioIDAsync(m_SesionBase.SESION_SEGURIDAD);
            };
            m_S_Sesion.ObtenSuscripcionIDAsync(m_SesionBase.SESION_SEGURIDAD);

        }

        public delegate void ObtenSesionDatosArgs(bool Correcto);
        public event ObtenSesionDatosArgs ObtenSesionDatosEvent;
        public void ObtenSesionDatos()
        {
            m_SesionBase.MuestraMensaje("Obteniendo datos...");
            m_S_Sesion.ObtenSesionDatosCompleted += m_S_Sesion_ObtenSesionDatosCompleted;
            m_S_Sesion.ObtenSesionDatosAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.SESION_SEGURIDAD);

        }

        void m_S_Sesion_ObtenSesionDatosCompleted(object sender, ES_Sesion.ObtenSesionDatosCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    if (e.Result == "")
                    {
                        m_SesionBase.MuestraMensaje("Usuario o clave no validos", 5);
                    }
                    else
                    {
                        m_SesionBase.MuestraMensaje("Sesion Creada", 1);
                        m_SesionBase.Mdl_Sesion = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Sesion.Model_Sesion>(e.Result);
                        m_SesionBase.SESION_SEGURIDAD = m_SesionBase.Mdl_Sesion.SESION_SEGURIDAD;
                        m_SesionBase.SUSCRIPCION_ID_SELECCIONADA = m_SesionBase.SUSCRIPCION_ID = m_SesionBase.Mdl_Sesion.SUSCRIPCION_ID;
                        m_SesionBase.USUARIO_ID = m_SesionBase.Mdl_Sesion.USUARIO_ID;
                        m_SesionBase.GuardaDatos();
                        if (ObtenSesionDatosEvent != null)
                            ObtenSesionDatosEvent(true);
                        return;
                    }

                }
                else
                    m_SesionBase.MuestraMensaje("Error al crear la sesion", 5);
                if (ObtenSesionDatosEvent != null)
                    ObtenSesionDatosEvent(false);

                return;
            }
            catch
            {
            }

            m_SesionBase.MuestraMensaje("Error de Red", 5);
            if (ObtenSesionDatosEvent != null)
                ObtenSesionDatosEvent(false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Listado"></param>
        public delegate void CambioListadoArgs(string Listado);
        public event CambioListadoArgs CambioListadoEvent;

        public string ArchivoNombre(string NombreTabla, string CampoLlave, string CampoNombre, string CampoAdicional,
        string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string OtroFiltro, int SuscripcionSeleccionadaID)
        {
            return ArchivoNombre(NombreTabla + CampoLlave + CampoNombre + CampoAdicional + CampoDescripcion
            + CampoImagen + MostrarBorrados + OtroFiltro + SuscripcionSeleccionadaID);
        }
        public string ArchivoNombre(string Texto, int SuscripcionSeleccionadaID)
        {
            return ArchivoNombre(Texto + SuscripcionSeleccionadaID);
        }
        public string ArchivoNombre(string Texto)
        {
            return MD5Core.GetHashString(Texto) + ".Lst";
        }
        public string NormalizaFiltro(string Filtro)
        {
            if (Filtro == null || Filtro == "")
                return Filtro;
            string R = Filtro;
            R = R.Replace("@SUSCRIPCION_ID", m_SesionBase.SUSCRIPCION_ID_SELECCIONADA.ToString());
            R = R.Replace("@USUARIO_ID", m_SesionBase.USUARIO_ID.ToString());
            if (m_SesionBase.Mdl_Sesion != null)
            {
                R = R.Replace("@PERSONA_ID", m_SesionBase.Mdl_Sesion.PERSONA_ID.ToString());
            }
            //            R = R.Replace("@PERSONA_ID", m_SesionBase.Mdl_Sesion..ToString());
            return R;
        }
        public void ObtenListado(string NombreTabla, string CampoLlave, string CampoNombre, string CampoAdicional,
string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string OtroFiltro)
        {
            ObtenListado(NombreTabla, CampoLlave, CampoNombre, CampoAdicional, CampoDescripcion, CampoImagen, MostrarBorrados, OtroFiltro, "", false);
        }
        public void ObtenListado(string NombreTabla, string CampoLlave, string CampoNombre, string CampoAdicional,
        string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string OtroFiltro, string Or)
        {
            ObtenListado(NombreTabla, CampoLlave, CampoNombre, CampoAdicional, CampoDescripcion, CampoImagen, MostrarBorrados, OtroFiltro, Or, false);
        }
        public void ObtenListado(string NombreTabla, string CampoLlave, string CampoNombre, string CampoAdicional,
        string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string OtroFiltro, string Or, bool ForzaActualizacion)
        {
            try
            {
                if (!m_SesionBase.EstaLogeado())
                    return;
                OtroFiltro = NormalizaFiltro(OtroFiltro);
                string ArchivoListadoNombre = ArchivoNombre(NombreTabla, CampoLlave, CampoNombre, CampoAdicional, CampoDescripcion, CampoImagen, MostrarBorrados, OtroFiltro, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA);
                string Hash = "";
                if (m_SesionBase.GuardaCopiaLocal)
                {
                    ListadoJsonLocal LjLocal = ListadoJsonLocal.Cargar(ArchivoListadoNombre);
                    if (LjLocal.Listado != null && !ForzaActualizacion)
                    {
                        Hash = LjLocal.Hash;
                        if (CambioListadoEvent != null)
                            CambioListadoEvent(LjLocal.Listado);

                    }
                }
                m_SesionBase.MuestraMensaje("Actualizando Datos");
                m_S_Sesion.ObtenListadoCompleted += delegate(object Sender, ES_Sesion.ObtenListadoCompletedEventArgs e)
                {
                    try
                    {
                        m_SesionBase.MuestraMensaje("Listo", 3);

                        if (e.Result != null && e.Result != "==")
                        {
                            string Resultado = eClockBase.Controladores.CeC_ZLib.ZJson2Json(e.Result);
                            if (m_SesionBase.GuardaCopiaLocal)
                            {
                                Resultado = eClockBase.CeC.Json2JsonList(Resultado);
                                ListadoJsonLocal.Guarda(ArchivoListadoNombre, Resultado);
                            }
                            if (CambioListadoEvent != null)
                                CambioListadoEvent(Resultado);
                        }
                        /*if (CambioListadoEvent != null && e.Result == "==" && ForzaActualizacion)
                        {
                            CambioListadoEvent(LjLocal.Listado);
                        }*/

                        if (CambioListadoEvent != null && e.Result == null)
                        {
                            CambioListadoEvent(null);
                        }

                        return;
                    }
                    catch (Exception ex)
                    {
                        CeC_Log.AgregaError(ex);
                    }
                    m_SesionBase.MuestraMensaje("Error de Red", 5);
                };

                m_S_Sesion.ObtenListadoAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA, NombreTabla, CampoLlave, CampoNombre, CampoAdicional,
             CampoDescripcion, CampoImagen, MostrarBorrados, OtroFiltro, Or, Hash);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                m_SesionBase.MuestraMensaje("Error al obtener Datos", 10);
            }
        }

        public event System.EventHandler<ES_Sesion.ObtenListadoCatalogoCompletedEventArgs> ObtenListadoCatalogoCompleted;
        public void ObtenListadoCatalogo(string CampoLlave)
        {
            m_SesionBase.MuestraMensaje("Cargando Datos");
            string ArchivoListadoNombre = ArchivoNombre(CampoLlave, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA);
            ListadoJsonLocal LjLocal = ListadoJsonLocal.Cargar(ArchivoListadoNombre);
            if (LjLocal.Listado != null && CambioListadoEvent != null)
            {
                CambioListadoEvent(LjLocal.Listado);

            }
            m_S_Sesion.ObtenListadoCatalogoCompleted += delegate(object Sender, ES_Sesion.ObtenListadoCatalogoCompletedEventArgs e)
            {
                try
                {
                    m_SesionBase.MuestraMensaje("Listo", 1);
                    if (e.Result != null && e.Result != "==")
                        ListadoJsonLocal.Guarda(ArchivoListadoNombre, e.Result);

                    if (CambioListadoEvent != null && e.Result != "==")
                    {
                        if (e.Result != null)
                            CambioListadoEvent(e.Result);
                    }
                }
                catch (Exception ex)
                {
                    CeC_Log.AgregaError(ex);
                }
            };
            m_S_Sesion.ObtenListadoCatalogoCompleted += ObtenListadoCatalogoCompleted;
            m_S_Sesion.ObtenListadoCatalogoAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA, CampoLlave, LjLocal.Hash);
        }
        public static List<ListadoJson> String2ListadoJSon(string Texto)
        {

            return eClockBase.Controladores.CeC_ZLib.Json2Object<List<ListadoJson>>(Texto);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Resultado">Valores negativos para errores, -1 = error de red</param>
        /// <param name="Datos"></param>
        public delegate void ObtenDatosArgs(int Resultado, string Datos);
        /// <summary>
        /// 
        /// </summary>
        public event ObtenDatosArgs ObtenDatosEvent;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tabla"></param>
        /// <param name="CamposLlave"></param>
        /// <param name="Modelo"></param>
        /// <param name="CamposOrden"></param>
        /// <returns></returns>
        public void ObtenDatos(string Tabla, string CamposLlave, object Modelo, string CamposOrden = "", string OtroFiltro = "")
        {

            if (!m_SesionBase.EstaLogeado())
                return;
            m_SesionBase.MuestraMensaje("Obteniendo Datos");
            m_S_Sesion.ObtenDatosCompleted += m_S_Sesion_ObtenDatosCompleted;
            m_S_Sesion.ObtenDatosAsync(m_SesionBase.SESION_SEGURIDAD, Tabla, CamposLlave, JsonConvert.SerializeObject(Modelo), CamposOrden, OtroFiltro);
        }

        void m_S_Sesion_ObtenDatosCompleted(object sender, ES_Sesion.ObtenDatosCompletedEventArgs e)
        {
            try
            {
                try
                {
                    if (e.Result == null || e.Result == "")
                    {
                        if (ObtenDatosEvent != null)
                            ObtenDatosEvent(-2, "");
                        return;
                    }
                    if (e.Result == "ERROR_SIN_RESULTADOS" || e.Result == "ERROR_DESCONOCIDO")
                    {
                        if (ObtenDatosEvent != null)
                            ObtenDatosEvent(0, e.Result);
                        return;
                    }

                    int R = 1;
                    m_SesionBase.MuestraMensaje("Listo", 3);
                    if (ObtenDatosEvent != null)
                        ObtenDatosEvent(R, CeC.ParchaJson(e.Result));
                    return;
                }
                catch { }
                m_SesionBase.MuestraMensaje("Error desconocido", 5);
                return;
            }
            catch { }
            if (ObtenDatosEvent != null)
                ObtenDatosEvent(-1, "");
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }


        public delegate void GuardaDatos1aNArgs(int NoModificados);
        public event GuardaDatos1aNArgs GuardaDatos1aNEvent;
        public void GuardaDatos1aN(string Tabla, string CampoLlaveUno, string ValorLlaveUno, string CampoLlaveN, string Activos)
        {
            m_S_Sesion.GuardaDatos1aNCompleted += m_S_Sesion_GuardaDatos1aNCompleted;
            m_S_Sesion.GuardaDatos1aNAsync(m_SesionBase.SESION_SEGURIDAD, Tabla, CampoLlaveUno, ValorLlaveUno, CampoLlaveN, Activos, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA);
        }

        void m_S_Sesion_GuardaDatos1aNCompleted(object sender, ES_Sesion.GuardaDatos1aNCompletedEventArgs e)
        {
            try
            {
                if (e.Result >= 0)
                {
                    if (GuardaDatos1aNEvent != null)
                        GuardaDatos1aNEvent(e.Result);
                    m_SesionBase.MuestraMensaje("Listo", 2);
                }
                else
                    m_SesionBase.MuestraMensaje("No guardo", 5);
                return;
            }
            catch { }
            if (GuardaDatos1aNEvent != null)
                GuardaDatos1aNEvent(-1);
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }


        //public event GuardaDatosCompleted;
        public delegate void GuardaDatosArgs(int Guardados);
        public event GuardaDatosArgs GuardaDatosEvent;


        public void GuardaDatos(string Tabla, string Llaves, object Modelo, bool EsNuevo)
        {
            GuardaDatos(Tabla, Llaves, JsonConvert.SerializeObject(Modelo), EsNuevo);
        }
        public void GuardaDatos(string Tabla, string Llaves, string Modelo, bool EsNuevo)
        {
            m_SesionBase.MuestraMensaje("Guardando datos...");
            m_S_Sesion.GuardaDatosCompleted += m_S_Sesion_GuardaDatosCompleted;

            m_S_Sesion.GuardaDatosAsync(m_SesionBase.SESION_SEGURIDAD, Tabla, Llaves, CeC_ZLib.Json2ZJson(Modelo), m_SesionBase.SUSCRIPCION_ID_SELECCIONADA, EsNuevo);
        }

        void m_S_Sesion_GuardaDatosCompleted(object sender, ES_Sesion.GuardaDatosCompletedEventArgs e)
        {
            try
            {

                m_SesionBase.MuestraMensaje("Listo", 3);
                if (GuardaDatosEvent != null)
                    GuardaDatosEvent(e.Result);
                return;
            }
            catch { }
            if (GuardaDatosEvent != null)
                GuardaDatosEvent(-1);
            m_SesionBase.MuestraMensaje("Error de red", 5);

        }



        public delegate void ObtenDatosPersonaArgs(eClockBase.Modelos.Sesion.Model_DatosPersona Datos);
        public event ObtenDatosPersonaArgs ObtenDatosPersonaEvent;

        public void ObtenDatosPersona()
        {
            m_SesionBase.MuestraMensaje("Obteniendo Listado..");
            m_S_Sesion.ObtenDatosPersonaCompleted += m_S_Sesion_ObtenDatosPersonaCompleted;
            m_S_Sesion.ObtenDatosPersonaAsync(m_SesionBase.SESION_SEGURIDAD);

        }

        void m_S_Sesion_ObtenDatosPersonaCompleted(object sender, ES_Sesion.ObtenDatosPersonaCompletedEventArgs e)
        {

            try
            {
                eClockBase.Modelos.Sesion.Model_DatosPersona Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<eClockBase.Modelos.Sesion.Model_DatosPersona>(e.Result);
                m_SesionBase.MuestraMensaje("Listo", 2);
                if (ObtenDatosPersonaEvent != null)
                    ObtenDatosPersonaEvent(Datos);
                return;
            }
            catch { }
            if (ObtenDatosPersonaEvent != null)
                ObtenDatosPersonaEvent(null);
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

        public delegate void BorrarDatosArgs(int NoAfectados);
        public event BorrarDatosArgs BorradosDatos;
        public void BorrarDatos(string Tabla, string Llaves, string Modelo)
        {
            m_SesionBase.MuestraMensaje("Borrando Datos");
            m_S_Sesion.BorrarDatosCompleted += m_S_Sesion_BorrarDatosCompleted;
            m_S_Sesion.BorrarDatosAsync(m_SesionBase.SESION_SEGURIDAD, Tabla, Llaves, Modelo);
        }
        public void BorrarDatos(string Tabla, string Llaves, object Modelo)
        {
            BorrarDatos(Tabla, Llaves, JsonConvert.SerializeObject(Modelo));
        }

        void m_S_Sesion_BorrarDatosCompleted(object sender, ES_Sesion.BorrarDatosCompletedEventArgs e)
        {
            try
            {
                if (e.Result > 0)
                    m_SesionBase.MuestraMensaje("Borrado Satisfactoriamente", 3);
                else
                    m_SesionBase.MuestraMensaje("No hay borrados");
                if (BorradosDatos != null)
                    BorradosDatos(e.Result);
                return;
            }
            catch { }
            if (BorradosDatos != null)
                BorradosDatos(-999999);
            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

        public delegate void CerrarSesionArgs(bool Cerrado);
        public event CerrarSesionArgs SesionCerrada;
        public void CerrarSesion()
        {
            m_SesionBase.MuestraMensaje("Cerrando Sesion");
            m_S_Sesion.CierraSesionCompleted += m_S_Sesion_CierraSesionCompleted;
            m_S_Sesion.CierraSesionAsync(m_SesionBase.SESION_SEGURIDAD);
            m_SesionBase.LimpiaDatos();
        }

        void m_S_Sesion_CierraSesionCompleted(object sender, ES_Sesion.CierraSesionCompletedEventArgs e)
        {
            try
            {

                if (e.Result == true)
                {
                    m_SesionBase.MuestraMensaje("Sesion Cerrada Satisfactoriamente", 3);
                    if (SesionCerrada != null)
                        SesionCerrada(true);
                }
                else
                    m_SesionBase.MuestraMensaje("Hubo problemas al Cerrar Sesion Intente Nuevamente");
            }
            catch
            {
                m_SesionBase.MuestraMensaje("Error de red");
            }
            if (SesionCerrada != null)
                SesionCerrada(false);
        }


        public delegate void ObtenNoCambiosArgs(string Resultado);
        public event ObtenNoCambiosArgs ObtenNoCambiosEvent;

        public void ObtenNoCambios(string Tabla)
        {
            m_SesionBase.MuestraMensaje("Obteniendo");
            m_S_Sesion.ObtenNoCambiosCompleted += m_S_Sesion_ObtenNoCambiosCompleted;
            m_S_Sesion.ObtenNoCambiosAsync(m_SesionBase.SESION_SEGURIDAD, Tabla, "");
        }

        void m_S_Sesion_ObtenNoCambiosCompleted(object sender, ES_Sesion.ObtenNoCambiosCompletedEventArgs e)
        {
            try
            {

                if ((e.Result != null) && (e.Result != ""))
                {
                    m_SesionBase.MuestraMensaje("Obteniendo datos NO cambiados", 3);
                    if (ObtenNoCambiosEvent != null)
                        ObtenNoCambiosEvent(e.Result);
                }
                else
                    m_SesionBase.MuestraMensaje("Error al obtener Datos", 5);
                return;
            }
            catch
            { }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }


        public delegate void GuardaConsultaArgs(bool Restultado);
        public event GuardaConsultaArgs GuardaConsultaEvent;

        public void GuardaConsulta(string TablaNombre)
        {
            m_SesionBase.MuestraMensaje("Guardando Datos");
            m_S_Sesion.GuardaConsultaCompleted += m_S_Sesion_GuardaConsultaCompleted;
            m_S_Sesion.GuardaConsultaAsync(m_SesionBase.SESION_SEGURIDAD, TablaNombre, "");
        }

        void m_S_Sesion_GuardaConsultaCompleted(object sender, ES_Sesion.GuardaConsultaCompletedEventArgs e)
        {
            try
            {
                if (e.Result == true)
                {

                    m_SesionBase.MuestraMensaje("Datos Guardados", 3);

                    if (GuardaConsultaEvent != null)
                        GuardaConsultaEvent(e.Result);
                }
                else
                    m_SesionBase.MuestraMensaje("Error al guardar datos", 5);
                return;
            }
            catch
            {
            }
            if (GuardaConsultaEvent != null)
                GuardaConsultaEvent(false);
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }

        public delegate void ObtenConfigArgs(string Resultado);
        public event ObtenConfigArgs ObtenConfigEvent;

        public void ObtenConfig(string Variable, int Tipo0Sistema1Suscripcion2Usuario)
        {
            m_SesionBase.MuestraMensaje("Cargando Configuración...");
            m_S_Sesion.ObtenConfigCompleted += m_S_Sesion_ObtenConfigCompleted;
            m_S_Sesion.ObtenConfigAsync(m_SesionBase.SESION_SEGURIDAD, Variable, Tipo0Sistema1Suscripcion2Usuario);
        }

        void m_S_Sesion_ObtenConfigCompleted(object sender, ES_Sesion.ObtenConfigCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    m_SesionBase.MuestraMensaje("Datos Guardados", 3);
                    if (ObtenConfigEvent != null)
                        ObtenConfigEvent(e.Result);
                }
                else
                    m_SesionBase.MuestraMensaje("Error al guardar datos", 5);
                return;
            }
            catch
            {
            }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }

        public delegate void GuardaConfigArgs(bool Resultado);
        public event GuardaConfigArgs GuardaConfigEvent;

        public void GuardaConfig(string Variable, object Valor, int Tipo0Sistema1Suscripcion2Usuario)
        {
            GuardaConfig(Variable, JsonConvert.SerializeObject(Valor), Tipo0Sistema1Suscripcion2Usuario);
        }
        public void GuardaConfig(string Variable, string Valor, int Tipo0Sistema1Suscripcion2Usuario)
        {
            m_SesionBase.MuestraMensaje("Guardando Datos");
            m_S_Sesion.GuardaConfigCompleted += m_S_Sesion_GuardaConfigCompleted;
            m_S_Sesion.GuardaConfigAsync(m_SesionBase.SESION_SEGURIDAD, Variable, Valor, Tipo0Sistema1Suscripcion2Usuario);
        }

        void m_S_Sesion_GuardaConfigCompleted(object sender, ES_Sesion.GuardaConfigCompletedEventArgs e)
        {

            try
            {
                if (e.Result != null)
                {
                    m_SesionBase.MuestraMensaje("Datos Guardados", 3);
                    if (GuardaConfigEvent != null)
                        GuardaConfigEvent(e.Result);
                    return;
                }
                else
                    m_SesionBase.MuestraMensaje("Error al guardar datos", 5);
                if (GuardaConfigEvent != null)
                    GuardaConfigEvent(false);
                return;
            }
            catch
            {
            }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
            if (GuardaConfigEvent != null)
                GuardaConfigEvent(false);
        }

        public string ObtenImagenNombre(string Tabla, int Llave)
        {
            string NombreFoto = Tabla + "_" + Llave.ToString() + ".jpg";
            return NombreFoto;
        }
        public string ObtenImagenNombre(string Tabla, int Llave, int Ancho, int Alto)
        {
            string NombreFoto = Tabla + "_" + Llave.ToString() + "_" + Ancho + "x" + Alto + ".jpg";
            return NombreFoto;
        }

        public delegate void ObtenImagenArgs(byte[] Imagen);
        public event ObtenImagenArgs ObtenImagenFinalizado;

        public void ObtenImagen(string Tabla, string CampoLlave, int Llave, string CampoImagen, bool GuardaCopiaLocal = true)
        {

            string NombreFoto = ObtenImagenNombre(Tabla, Llave);
            DateTime FechaModificacion = CeC.FechaNula;
            if (GuardaCopiaLocal)
                if (CeC_Stream.sExisteArchivo(NombreFoto))
                {
                    FechaModificacion = CeC_Stream.sFechaHoraModificacion(NombreFoto);

                    if (ObtenImagenFinalizado != null)
                        ObtenImagenFinalizado(CeC_Stream.sLeerBytes(NombreFoto));
                }

            //m_SesionBase.MuestraMensaje("Cargando Datos");
            m_S_Sesion.ObtenImagenCompleted += delegate(object sender, ES_Sesion.ObtenImagenCompletedEventArgs e)
            {
                try
                {
                    if (e.Result != null)
                    {
                        byte[] Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<byte[]>(e.Result);

                        ///la imagen va a ser igual siempre que le pasen una fecha desde (ultima foto guardada)
                        if (!CeC.EsImagenIgual(Datos))
                        {
                            if (GuardaCopiaLocal)
                                CeC_Stream.sNuevoBytes(NombreFoto, Datos);
                            if (ObtenImagenFinalizado != null)
                                ObtenImagenFinalizado(Datos);
                        }
                        else
                            if (FechaModificacion <= CeC.FechaNula)
                                if (ObtenImagenFinalizado != null)
                                    ObtenImagenFinalizado(Datos);
                    }
                    else
                        if (ObtenImagenFinalizado != null)
                            ObtenImagenFinalizado(null);
                    return;
                }
                catch (Exception ex)
                {
                    //m_SesionBase.MuestraMensaje("Error al cargar los datos", 10);
                    CeC_Log.AgregaError(ex);
                }
                if (ObtenImagenFinalizado != null)
                    ObtenImagenFinalizado(null);
                m_SesionBase.MuestraMensaje("Error de Red", 5);
            };
            m_S_Sesion.ObtenImagenAsync(m_SesionBase.SESION_SEGURIDAD, Tabla, CampoLlave, Llave, CampoImagen, FechaModificacion);
        }
        public delegate void ObtenImagenThumbnailArgs(byte[] Imagen);
        public event ObtenImagenThumbnailArgs ObtenImagenThumbnailFinalizado;

        public void ObtenImagenThumbnail(string Tabla, string CampoLlave, string NombreArchivo, string CampoImagen, bool GuardaCopiaLocal = true)
        {
            NombreArchivo = CeC.ObtenColumnaSeparador(NombreArchivo, ".", 0);
            string Tamano = CeC.ObtenColumnaSeparador(NombreArchivo, "_", 1);
            string sLlave = CeC.ObtenColumnaSeparador(NombreArchivo, "_", 0);
            string sAncho = CeC.ObtenColumnaSeparador(Tamano, "x", 0);
            string sAlto = CeC.ObtenColumnaSeparador(Tamano, "x", 1);
            int Ancho = CeC.Convierte2Int(sAncho, 0);
            int Alto = CeC.Convierte2Int(sAlto, 0);
            int Llave = CeC.Convierte2Int(sLlave);
            ObtenImagenThumbnail(Tabla, CampoLlave, Llave, CampoImagen, Ancho, Alto, GuardaCopiaLocal);

        }
        public void ObtenImagenThumbnail(string Tabla, string CampoLlave, int Llave, string CampoImagen, int Ancho, int Alto, bool GuardaCopiaLocal = true)
        {
            string NombreFoto = ObtenImagenNombre(Tabla, Llave, Ancho, Alto);
            DateTime FechaModificacion = CeC.FechaNula;
            if (GuardaCopiaLocal && CeC_Stream.sExisteArchivo(NombreFoto))
            {
                FechaModificacion = CeC_Stream.sFechaHoraModificacion(NombreFoto);
                if (ObtenImagenThumbnailFinalizado != null)
                    ObtenImagenThumbnailFinalizado(CeC_Stream.sLeerBytes(NombreFoto));
            }

            //m_SesionBase.MuestraMensaje("Cargando Datos");
            m_S_Sesion.ObtenImagenThumbnailCompleted += delegate(object sender, ES_Sesion.ObtenImagenThumbnailCompletedEventArgs e)
           {
               try
               {
                   if (e.Result != null)
                   {
                       byte[] Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<byte[]>(e.Result);
                       ///la imagen va a ser igual siempre que le pasen una fecha desde (ultima foto guardada)
                       if (!CeC.EsImagenIgual(Datos))
                       {
                           CeC_Stream.sNuevoBytes(NombreFoto, Datos);
                           if (ObtenImagenThumbnailFinalizado != null)
                               ObtenImagenThumbnailFinalizado(Datos);
                       }
                       else
                           if (FechaModificacion <= CeC.FechaNula)
                               if (ObtenImagenThumbnailFinalizado != null)
                                   ObtenImagenThumbnailFinalizado(Datos);
                   }
                   else
                       if (ObtenImagenThumbnailFinalizado != null)
                           ObtenImagenThumbnailFinalizado(null);
                   return;
               }
               catch (Exception ex)
               {
                   //m_SesionBase.MuestraMensaje("Error al cargar los datos", 10);
                   CeC_Log.AgregaError(ex);
               }
               m_SesionBase.MuestraMensaje("Error de Red", 5);
               if (ObtenImagenThumbnailFinalizado != null)
                   ObtenImagenThumbnailFinalizado(null);
           };
            m_S_Sesion.ObtenImagenThumbnailAsync(m_SesionBase.SESION_SEGURIDAD, Tabla, CampoLlave, Llave, CampoImagen, FechaModificacion, Ancho, Alto);
        }

        public delegate void ImportarArgs(bool Errores, string Resultado);
        public event ImportarArgs ImportarEvent;
        public void Importar(object Datos)
        {
            Importar(Datos.GetType().FullName, JsonConvert.SerializeObject(Datos));
        }
        public void Importar(string Clase, string Datos)
        {
            CeC_Stream.sNuevoTexto(Clase + ".imp", Datos);
            m_SesionBase.MuestraMensaje("Guardando Datos");
            m_S_Sesion.ImportarCompleted += m_S_Sesion_ImportarCompleted;
            m_S_Sesion.ImportarAsync(m_SesionBase.SESION_SEGURIDAD, Clase, Datos, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA);
        }

        void m_S_Sesion_ImportarCompleted(object sender, ES_Sesion.ImportarCompletedEventArgs e)
        {


            try
            {
                if (!CeC.Compara(e.Result, "ERROR", true))
                {
                    m_SesionBase.MuestraMensaje("Datos Guardados", 3);
                    if (ImportarEvent != null)
                        ImportarEvent(false, e.Result);
                    return;
                }
                else
                    m_SesionBase.MuestraMensaje("Error al guardar datos", 5);
                if (ImportarEvent != null)
                    ImportarEvent(true, "ERROR_DESCONOCIDO");
                return;
            }
            catch
            {
            }
            m_SesionBase.MuestraMensaje("Error de Red", 5);
            if (ImportarEvent != null)
                ImportarEvent(true, "ERROR_RED");
        }

        public delegate void Inicia_eClock5Args(bool Resultado);
        public event Inicia_eClock5Args Inicia_eClock5Finalizado;

        public void Inicia_eClock5()
        {
            m_S_Sesion.Inicia_eClock5Completed += m_S_Sesion_Inicia_eClock5Completed;
            m_S_Sesion.Inicia_eClock5Async(m_SesionBase.SESION_SEGURIDAD);
        }

        void m_S_Sesion_Inicia_eClock5Completed(object sender, ES_Sesion.Inicia_eClock5CompletedEventArgs e)
        {
            bool R = false;
            try
            {
                if (e.Result)
                    R = true;
            }
            catch
            {
                m_SesionBase.MuestraMensaje("Error de Red", 5);
            }
            if (Inicia_eClock5Finalizado != null)
                Inicia_eClock5Finalizado(R);
        }

    }





    public class ListadoJsonLocal
    {
        public string Hash { get; set; }
        public string Listado { get; set; }
        public ListadoJsonLocal()
        { }
        public ListadoJsonLocal(ListadoJson lListado)
        {
            Listado = JsonConvert.SerializeObject(lListado);
            Hash = MD5Core.GetHashString(Listado);
        }
        public ListadoJsonLocal(string lListado)
        {
            Listado = lListado;
            Hash = MD5Core.GetHashString(Listado);
        }
        public static ListadoJsonLocal Cargar(string ArchivoNombre)
        {
            ListadoJsonLocal L = null;
            try
            {

                L = eClockBase.Controladores.CeC_ZLib.Json2Object<ListadoJsonLocal>(CeC_Stream.sLeerString(ArchivoNombre));
                if (L != null)
                    return L;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            L = new ListadoJsonLocal();
            /* L.Hash = "";
             L.Listado = "";*/
            return L;
        }
        public bool lGuarda(string ArchivoNombre)
        {
            try
            {
                return CeC_Stream.sNuevoTexto(ArchivoNombre, JsonConvert.SerializeObject(this));

            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return false;
        }
        public static bool Guarda(string ArchivoNombre, string Listado)
        {
            try
            {
                ListadoJsonLocal NuevoArchivo = new ListadoJsonLocal(Listado);
                return CeC_Stream.sNuevoTexto(ArchivoNombre, JsonConvert.SerializeObject(NuevoArchivo));

            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return false;
        }



    }

    public class ListadoJson
    {
        public object Llave { get; set; }
        public object Nombre { get; set; }
        public object Adicional { get; set; }
        public object Descripcion { get; set; }
        public object Imagen { get; set; }
        public ListadoJson()
        { }

        public ListadoJson(object oLlave, object oNombre, object oAdicional, object oDescripcion, object oImagen)
        {
            Llave = oLlave;
            Nombre = oNombre;
            Adicional = oAdicional;
            Descripcion = oDescripcion;
            Imagen = oImagen;
        }

        public override string ToString()
        {
            if (Nombre == null)
                return "";
            return Nombre.ToString();
        }
    }

}
