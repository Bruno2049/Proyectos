using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Publicidad
    {

        ES_Publicidad.S_PublicidadClient m_S_Publicidad = null;
        CeC_SesionBase m_SesionBase = null;

        public Publicidad(CeC_SesionBase SesionBase)
        {            
            m_S_Publicidad = new ES_Publicidad.S_PublicidadClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Publicidad.svc"));
            m_SesionBase = SesionBase;
        }

        public string ObtenImagenNombre(int PublicidadID)
        {
            string NombreFoto = "Pub_" + PublicidadID.ToString() + ".jpg";
            return NombreFoto;
        }

        public delegate void ObtenImagenArgs(byte[] Imagen);
        public event ObtenImagenArgs ObtenImagenEvent;

        public void ObtenImagen(int PublicidadID)
        {
            string NombreFoto = ObtenImagenNombre(PublicidadID);
            DateTime FechaModificacion = CeC.FechaNula;
            if (CeC_Stream.sExisteArchivo(NombreFoto))
            {
                FechaModificacion = CeC_Stream.sFechaHoraModificacion(NombreFoto);
                if (ObtenImagenEvent != null)
                    ObtenImagenEvent(CeC_Stream.sLeerBytes(NombreFoto));
            }

            m_S_Publicidad.ObtenImagenCompleted += delegate(object sender, ES_Publicidad.ObtenImagenCompletedEventArgs e)
                {
                    try
                    {
                        if (e.Result != null)
                        {
                           // m_SesionBase.MuestraMensaje("Imagen Obtenida", 3);
                            byte[] Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<byte[]>(e.Result);
                            if (Datos.Length > 0)
                            {
                                CeC_Stream.sNuevoBytes(NombreFoto, Datos);
                                if (ObtenImagenEvent != null)
                                    ObtenImagenEvent(Datos);
                            }
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        CeC_Log.AgregaError(ex);
                    }
                    m_SesionBase.MuestraMensaje("Error de Red", 5);
                };
            m_S_Publicidad.ObtenImagenAsync(PublicidadID, FechaModificacion);
        }

        public string ArchivoNombre(int TipoPublicidad)
        {
            return "Publicidad" + TipoPublicidad + ".Lst";
        }

        public delegate void ObtenListadoArgs(string Resultados);
        public event ObtenListadoArgs ObtenListadoEvent;

        public void ObtenListado(int TipoPublicidad)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Listado");
            string ArchivoListadoNombre = ArchivoNombre(TipoPublicidad);
            ListadoJsonLocal LjLocal = ListadoJsonLocal.Cargar(ArchivoListadoNombre);
            if (LjLocal.Listado != null && ObtenListadoEvent != null)
            {
                try
                {
                    ObtenListadoEvent(LjLocal.Listado);
                }
                catch { }
            }

            m_S_Publicidad.ObtenListadoCompleted += delegate(object sender, ES_Publicidad.ObtenListadoCompletedEventArgs e)
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
                                ObtenListadoEvent(e.Result);
                        }

                        return;
                    }
                    catch (Exception ex)
                    {
                        CeC_Log.AgregaError(ex);
                    }
                    m_SesionBase.MuestraMensaje("Error de Red", 5);
                };
            m_S_Publicidad.ObtenListadoAsync(TipoPublicidad, LjLocal.Hash);
        }



    }
}
