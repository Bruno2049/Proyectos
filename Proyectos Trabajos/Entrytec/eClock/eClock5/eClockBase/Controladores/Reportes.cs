using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Reportes
    {

        ES_Reportes.S_ReportesClient m_S_Reportes = null;
        CeC_SesionBase m_SesionBase = null;

        public Reportes(CeC_SesionBase SesionBase)
        {
            m_S_Reportes = new ES_Reportes.S_ReportesClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Reportes.svc"));
            m_SesionBase = SesionBase;
        }


        public string ArchivoNombre(int UsuarioID)
        {
            return "Reportes" + UsuarioID + ".Lst";
        }

        public delegate void ObtenListadoArgs(List<Modelos.Reportes.Model_Reportes> Reportes);
        public event ObtenListadoArgs ObtenListadoEvent;

        public void ObtenListado(string Modelos)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Listado");
            string ArchivoListadoNombre = ArchivoNombre(m_SesionBase.USUARIO_ID);
            ListadoJsonLocal LjLocal = ListadoJsonLocal.Cargar(ArchivoListadoNombre);
            if (LjLocal.Listado != null && ObtenListadoEvent != null)
            {
                try
                {
                    ObtenListadoEvent(eClockBase.Controladores.CeC_ZLib.Json2Object<List<Modelos.Reportes.Model_Reportes>>(LjLocal.Listado));
                }
                catch { }
            }

            m_S_Reportes.ObtenListadoCompleted += delegate(object sender, ES_Reportes.ObtenListadoCompletedEventArgs e)
            {
                try
                {
                    if (e.Result != null)
                        m_SesionBase.MuestraMensaje("Listado Obtenido", 3);
                    else
                        m_SesionBase.MuestraMensaje("Error al obtener el listado", 5);

                    if (e.Result != null && e.Result != "==")
                        ListadoJsonLocal.Guarda(ArchivoListadoNombre, e.Result);
                    if (ObtenListadoEvent != null && e.Result != "==")
                    {
                        if (e.Result != null)
                            ObtenListadoEvent(eClockBase.Controladores.CeC_ZLib.Json2Object<List<Modelos.Reportes.Model_Reportes>>(e.Result));
                    }

                    return;
                }
                catch (Exception ex)
                {
                    CeC_Log.AgregaError(ex);
                }
                m_SesionBase.MuestraMensaje("Error de Red", 5);
            };
            m_S_Reportes.ObtenListadoAsync(m_SesionBase.SESION_SEGURIDAD, LjLocal.Hash);
        }
        public string ObtenReporteNombre(int FormatoRepID)
        {
            string Extencion = "";
            switch (FormatoRepID)
            {
                case 1:
                    Extencion = ".xls";
                    break;
                default:
                    Extencion = ".pdf";
                    break;
            }
            return "ReporteTmp" + Extencion;
        }
        public delegate void ObtenReporteArgs(Byte[] ArchivoReporte, string PathArchivo);
        public event ObtenReporteArgs ObtenReporteEvent;
        public void ObtenReporte(int ReporteID, string Parametros, int FormatoRepID)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Reporte");
            m_S_Reportes.ObtenReporteCompleted += delegate(object sender, ES_Reportes.ObtenReporteCompletedEventArgs e)
                {
                    try
                    {
                        if (e.Result != null && e.Result != "")
                        {
                            m_SesionBase.MuestraMensaje("Reporte Obtenido", 3);
                            byte[] ReporteBytes = eClockBase.Controladores.CeC_ZLib.Json2Object<Byte[]>(e.Result);
                            //Temporal
                            string Archivo = ObtenReporteNombre(FormatoRepID);
                            if (m_SesionBase.GuardaCopiaLocal)
                                CeC_Stream.sNuevoBytes(Archivo, ReporteBytes);

                            if (ObtenReporteEvent != null)
                                ObtenReporteEvent(ReporteBytes, Archivo);
                        }
                        else
                        {
                            m_SesionBase.MuestraMensaje("No hay datos o no se puedo obtener el Reporte", 5);
                            if (ObtenReporteEvent != null)
                                ObtenReporteEvent(null, "");
                        }

                        return;
                    }
                    catch (Exception ex)
                    {
                        CeC_Log.AgregaError(ex);
                    }
                    m_SesionBase.MuestraMensaje("Error de Red", 5);
                    if (ObtenReporteEvent != null)
                        ObtenReporteEvent(null, "");
                };
            m_S_Reportes.ObtenReporteAsync(m_SesionBase.SESION_SEGURIDAD, ReporteID, Parametros, FormatoRepID, m_SesionBase.IDIOMA);
        }

        public delegate void ObtenReporteeMailArgs(bool Estado);
        public event ObtenReporteeMailArgs ObtenReporteeMailEvent;

        public void ObtenReporteeMail(string Titulo, string Cuerpo, int ReporteID, object Parametros, int FormatoRepID)
        {
            ObtenReporteeMail("", Titulo, Cuerpo, ReporteID, JsonConvert.SerializeObject(Parametros), FormatoRepID);
        }

        public void ObtenReporteeMail(string Titulo, string Cuerpo, int ReporteID, string Parametros, int FormatoRepID)
        {
            ObtenReporteeMail("", Titulo, Cuerpo, ReporteID, Parametros, FormatoRepID);
        }

        public void ObtenReporteeMail(string eMails, string Titulo, string Cuerpo, int ReporteID, string Parametros, int FormatoRepID)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Reporte");

            m_S_Reportes.ObtenReporteMailCompleted += delegate(object sender, ES_Reportes.ObtenReporteMailCompletedEventArgs e)
            {
                try
                {
                    if (e.Result != null && e.Result != "" && e.Result == "OK")
                    {
                        m_SesionBase.MuestraMensaje("Reporte enviado al email", 3);
                        if (ObtenReporteeMailEvent != null)
                            ObtenReporteeMailEvent(true);
                        return;
                    }
                    else
                        m_SesionBase.MuestraMensaje("Error al obtener el Reporte", 5);
                    if (ObtenReporteeMailEvent != null)
                        ObtenReporteeMailEvent(false);
                    return;
                }
                catch (Exception ex)
                {
                    CeC_Log.AgregaError(ex);
                }
                m_SesionBase.MuestraMensaje("Error de Red", 5);
                if (ObtenReporteeMailEvent != null)
                    ObtenReporteeMailEvent(false);
            };
            m_S_Reportes.ObtenReporteMailAsync(m_SesionBase.SESION_SEGURIDAD, eMails, Titulo, Cuerpo, ReporteID, Parametros, FormatoRepID, m_SesionBase.IDIOMA);
        }




    }
}
