using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    public class Personas
    {
        ES_Personas.S_PersonasClient m_S_Personas = null;
        CeC_SesionBase m_SesionBase = null;

        public Personas(CeC_SesionBase SesionBase)
        {
            m_S_Personas = new ES_Personas.S_PersonasClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Personas.svc"));
            m_SesionBase = SesionBase;
        }
        string m_TextoABuscar = "";
        public void BuscaPersonas(string TextoABuscar)
        {
            m_SesionBase.MuestraMensaje("Buscando Datos");
            m_S_Personas.BuscaPersonasCompleted += m_S_Personas_BuscaPersonasCompleted;
            m_S_Personas.BuscaPersonasAsync(m_SesionBase.SESION_SEGURIDAD, m_TextoABuscar = TextoABuscar);
        }
        public delegate void BuscaPersonasFinalizadoArgs(List<Modelos.Personas.Model_PersonasBusqueda> Asistencias, string TextoBuscado);
        //Se crea el evento del delegado ObtenAsistenciaFinalizadoArgs
        public event BuscaPersonasFinalizadoArgs BuscaPersonasFinalizado;

        void m_S_Personas_BuscaPersonasCompleted(object sender, ES_Personas.BuscaPersonasCompletedEventArgs e)
        {
            try
            {
                m_SesionBase.MuestraMensaje("Listo", 1);
                //Se obtiene la cadena resultado que contrndra el Json
                string Resultado = e.Result;
                //Se Deserealiza el el resultado y se convierte en en una lista
                List<Modelos.Personas.Model_PersonasBusqueda> ResultadoPersonas = eClockBase.Controladores.CeC_ZLib.Json2Object<List<Modelos.Personas.Model_PersonasBusqueda>>(Resultado);
                //Se comprueba el evento que sea diferente de nulo
                if (BuscaPersonasFinalizado != null)
                    //Si es diferente se envia el finalizado
                    BuscaPersonasFinalizado(ResultadoPersonas, m_TextoABuscar);
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                m_SesionBase.MuestraMensaje("Error al obtener datos", 10);
            }
        }
        public delegate void AgregarPersonasFinalizadoArgs(bool Guardado);
        public event AgregarPersonasFinalizadoArgs AgregarPersonasEvent;

        public void AgregarPersonas(string DatosPersonas)
        {
            m_SesionBase.MuestraMensaje("Agregando personas");
            m_S_Personas.AgregaPersonaCompleted += m_S_Personas_AgregaPersonaCompleted;
            m_S_Personas.AgregaPersonaAsync(m_SesionBase.SESION_SEGURIDAD, DatosPersonas);
        }

        void m_S_Personas_AgregaPersonaCompleted(object sender, ES_Personas.AgregaPersonaCompletedEventArgs e)
        {
            try
            {
                if (e.Result == null)
                {
                    m_SesionBase.MuestraMensaje("Guardado", 1);
                    if (AgregarPersonasEvent != null)
                        AgregarPersonasEvent(true);
                    return;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                m_SesionBase.MuestraMensaje("Error al guardar");
            }
            if (AgregarPersonasEvent != null)
                AgregarPersonasEvent(false);
            m_SesionBase.MuestraMensaje("Error al Guardar", 10);
        }
        /// <summary>
        /// Delegado que llama ala funcion de la base de datos para OBTENER la foto.
        /// </summary>
        /// <param name="Foto"></param>


        public string ObtenFotoNombre(int PersonaID)
        {
            string NombreFoto = PersonaID.ToString() + ".jpg";
            return NombreFoto;
        }

        public string ObtenImagenNombre(int PersonaID, int TipoImagenID)
        {
            string NombreFoto = PersonaID.ToString() + TipoImagenID + ".jpg";
            return NombreFoto;
        }

        public delegate void ObtenFotoFinalizadoArgs(byte[] Foto);
        public event ObtenFotoFinalizadoArgs ObtenFotoEvent;

        public void ObtenFoto(int Persona_ID, bool GuardaCopiaLocal = true)
        {
            string NombreFoto = ObtenFotoNombre(Persona_ID);
            DateTime FechaModificacion = CeC.FechaNula;
            if (GuardaCopiaLocal && CeC_Stream.sExisteArchivo(NombreFoto))
            {
                FechaModificacion = CeC_Stream.sFechaHoraModificacion(NombreFoto);
                if (ObtenFotoEvent != null)
                    ObtenFotoEvent(CeC_Stream.sLeerBytes(NombreFoto));
            }
            m_S_Personas.ObtenFotoCompleted += delegate(object sender, ES_Personas.ObtenFotoCompletedEventArgs e)
            {
                try
                {
                    if (e.Result != null)
                    {
                        byte[] Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<byte[]>(e.Result);
                        if (!CeC.EsImagenIgual(Datos))
                        {
                            if (GuardaCopiaLocal)
                                CeC_Stream.sNuevoBytes(NombreFoto, Datos);
                            if (ObtenFotoEvent != null)
                                ObtenFotoEvent(Datos);
                        }
                        else
                            if (FechaModificacion <= CeC.FechaNula)
                                if (ObtenFotoEvent != null)
                                    ObtenFotoEvent(Datos);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    CeC_Log.AgregaError(ex);
                }
                if (ObtenFotoEvent != null)
                    ObtenFotoEvent(null);
            };
            m_S_Personas.ObtenFotoAsync(m_SesionBase.SESION_SEGURIDAD, Persona_ID, FechaModificacion);
        }

        public string ObtenFotoNombre(int PersonaID, int Ancho, int Alto)
        {
            string NombreFoto = PersonaID.ToString() + "_" + Ancho + "x" + Alto + ".jpg";
            return NombreFoto;
        }

        public delegate void ObtenFotoThumbnailFinalizadoArgs(byte[] Foto);
        public event ObtenFotoThumbnailFinalizadoArgs ObtenFotoThumbnailEvent;

        public void ObtenFotoThumbnail(string NombreArchivo, bool GuardaCopiaLocal = true)
        {
            NombreArchivo = CeC.ObtenColumnaSeparador(NombreArchivo, ".", 0);
            string Tamano = CeC.ObtenColumnaSeparador(NombreArchivo, "_", 1);
            string sLlave = CeC.ObtenColumnaSeparador(NombreArchivo, "_", 0);
            string sAncho = CeC.ObtenColumnaSeparador(Tamano, "x", 0);
            string sAlto = CeC.ObtenColumnaSeparador(Tamano, "x", 1);
            int Ancho = CeC.Convierte2Int(sAncho, 0);
            int Alto = CeC.Convierte2Int(sAlto, 0);
            int Llave = CeC.Convierte2Int(sLlave);
            ObtenFotoThumbnail(Llave, Ancho, Alto, GuardaCopiaLocal);

        }

        public void ObtenFotoThumbnail(int Persona_ID, int Ancho, int Alto, bool GuardaCopiaLocal = true)
        {
            string NombreFoto = ObtenFotoNombre(Persona_ID, Ancho, Alto);
            DateTime FechaModificacion = CeC.FechaNula;

            if (GuardaCopiaLocal && CeC_Stream.sExisteArchivo(NombreFoto))
            {
                FechaModificacion = CeC_Stream.sFechaHoraModificacion(NombreFoto);
                if (ObtenFotoThumbnailEvent != null)
                    ObtenFotoThumbnailEvent(CeC_Stream.sLeerBytes(NombreFoto));
            }

            m_S_Personas.ObtenFotoThumbnailCompleted += delegate(object sender, ES_Personas.ObtenFotoThumbnailCompletedEventArgs e)
            {
                try
                {
                    if (e.Result != null && e.Result != "ERRROR")
                    {
                        byte[] Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<byte[]>(e.Result);
                        if (!CeC.EsImagenIgual(Datos))
                        {
                            if (GuardaCopiaLocal)
                                CeC_Stream.sNuevoBytes(NombreFoto, Datos);
                            if (ObtenFotoThumbnailEvent != null)
                                ObtenFotoThumbnailEvent(Datos);
                        }
                        else
                            if (FechaModificacion <= CeC.FechaNula)
                                if (ObtenFotoThumbnailEvent != null)
                                    ObtenFotoThumbnailEvent(Datos);
                        
                        return;
                    }
                }
                catch (Exception ex)
                {
                    CeC_Log.AgregaError(ex);
                }
                //Si se ha cargado una imagen previamente guardada no se mostrará la nueva en caso de un error
                if (FechaModificacion <= CeC.FechaNula)
                    if (ObtenFotoThumbnailEvent != null)
                        ObtenFotoThumbnailEvent(null);
            };
            m_S_Personas.ObtenFotoThumbnailAsync(m_SesionBase.SESION_SEGURIDAD, Persona_ID, FechaModificacion, Ancho, Alto);
        }


        /// <summary>
        /// Funcion que se encarga de llamar la funcion de mi servicio para GUARDAR la foto.
        /// </summary>
        /// <param name="Foto"></param>
        public delegate void GuardaFotoArgs(bool FotoGuardada);
        public event GuardaFotoArgs GuardaFotoEvent;

        public void GuardaFoto(int Persona_ID, byte[] Foto)
        {
            m_SesionBase.MuestraMensaje("Guardando Foto", 5);
            m_S_Personas.GuardaFotoCompleted += m_S_Personas_GuardaFotoCompleted;
            m_S_Personas.GuardaFotoAsync(m_SesionBase.SESION_SEGURIDAD, Persona_ID, JsonConvert.SerializeObject(Foto));
            string NombreFoto = ObtenFotoNombre(Persona_ID);
            CeC_Stream.sNuevoBytes(NombreFoto, Foto);
        }

        void m_S_Personas_GuardaFotoCompleted(object sender, ES_Personas.GuardaFotoCompletedEventArgs e)
        {
            try
            {
                if (e.Result != false)
                    m_SesionBase.MuestraMensaje("Foto Guardada", 3);
                else
                    m_SesionBase.MuestraMensaje("Foto NO Guardada", 3);
            }
            catch { }
        }
        ///******************************************************************************************
        public delegate void GuardaImagenArgs(bool ImagenGuardada);
        public event GuardaImagenArgs GuardaImagenEvent;

        public void GuardaImagen(int Persona_ID, byte[] Imagen, int TipoImagenID)
        {
            m_SesionBase.MuestraMensaje("Guardando Imagen", 5);

            m_S_Personas.GuardaImagenCompleted += m_S_Personas_GuardaImagenCompleted;


            m_S_Personas.GuardaImagenAsync(m_SesionBase.SESION_SEGURIDAD, Persona_ID, JsonConvert.SerializeObject(Imagen), TipoImagenID);
            string NombreImagen = ObtenImagenNombre(Persona_ID, TipoImagenID);
            CeC_Stream.sNuevoBytes(NombreImagen, Imagen);
        }

        void m_S_Personas_GuardaImagenCompleted(object sender, ES_Personas.GuardaImagenCompletedEventArgs e)
        {
            try
            {
                if (e.Result != false)
                    m_SesionBase.MuestraMensaje("Imagen Guardada", 3);
                else
                    m_SesionBase.MuestraMensaje("Imagen NO Guardada", 3);
            }
            catch { }
        }


        //********************************************************************************************
        //********************************************************************************************
        //********************************************************************************************
        public delegate void GuardaValorFinalizadoArgs(bool Guardado);
        public event GuardaValorFinalizadoArgs GuardaValorFinalizadoEvent;

        public void GuardaValor(int Persona_ID, string Campo, string Valor)
        {
            m_S_Personas.GuardaValorCompleted += m_S_Personas_GuardaValorCompleted;
            m_S_Personas.GuardaValorAsync(m_SesionBase.SESION_SEGURIDAD, Persona_ID, Campo, Valor);
        }

        void m_S_Personas_GuardaValorCompleted(object sender, ES_Personas.GuardaValorCompletedEventArgs e)
        {
            try
            {
                if (GuardaValorFinalizadoEvent != null)
                    GuardaValorFinalizadoEvent(e.Result);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

            m_SesionBase.MuestraMensaje("Error de red", 5);

        }
        //**********************************************************************************************
        //**********************************************************************************************
        //**********************************************************************************************
        public delegate void ObtenPersonaIDBySuscripcionArgs(int Resultado);
        public event ObtenPersonaIDBySuscripcionArgs ObtenPersonaIDBySuscripcionEvent;

        public void ObtenPersonaIDBySuscripcion(int Persona_Link_ID)
        {
            m_S_Personas.ObtenPersonaIDBySuscripcionCompleted += m_S_Personas_ObtenPersonaIDBySuscripcionCompleted;
            m_S_Personas.ObtenPersonaIDBySuscripcionAsync(m_SesionBase.SESION_SEGURIDAD, Persona_Link_ID, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA);
        }

        void m_S_Personas_ObtenPersonaIDBySuscripcionCompleted(object sender, ES_Personas.ObtenPersonaIDBySuscripcionCompletedEventArgs e)
        {
            if (ObtenPersonaIDBySuscripcionEvent != null)
                ObtenPersonaIDBySuscripcionEvent(e.Result);
        }
        //**********************************************************************************************
        //**********************************************************************************************
        //**********************************************************************************************
        public delegate void AgregaArgs(int Guardado);
        public event AgregaArgs AgregaFinalizadoEvent;

        public void Agrega(int PersonaLinkID, int TipoPersonaID)
        {
            m_S_Personas.AgregaCompleted += m_S_Personas_AgregaCompleted;
            m_S_Personas.AgregaAsync(m_SesionBase.SESION_SEGURIDAD, PersonaLinkID, TipoPersonaID);
        }

        void m_S_Personas_AgregaCompleted(object sender, ES_Personas.AgregaCompletedEventArgs e)
        {
            if (AgregaFinalizadoEvent != null)
                AgregaFinalizadoEvent(e.Result);
        }

        public delegate void ReagruparEmpleadosArgs(bool Reagrupados);
        public event ReagruparEmpleadosArgs EmpleadosReagrupadosEvent;

        public void ReagruparEmpleados(string CAMPO_DATOS_IDS)
        {
            m_SesionBase.MuestraMensaje("Reagrupando Empleados");
            m_S_Personas.ReagruparEmpleadosCompleted += m_S_Personas_ReagruparEmpleadosCompleted;
            m_S_Personas.ReagruparEmpleadosAsync(m_SesionBase.SESION_SEGURIDAD, CAMPO_DATOS_IDS);
        }

        void m_S_Personas_ReagruparEmpleadosCompleted(object sender, ES_Personas.ReagruparEmpleadosCompletedEventArgs e)
        {


            try
            {
                if (e.Result != false)
                {
                    m_SesionBase.MuestraMensaje("Empleados Reagrupados", 3);
                    if (EmpleadosReagrupadosEvent != null)
                        EmpleadosReagrupadosEvent(e.Result);
                }
                else
                {
                    m_SesionBase.MuestraMensaje("Error al Reagrupar", 5);
                }
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }



        public delegate void AgrupacionObtenidaArgs(string Obtenidos);
        public event AgrupacionObtenidaArgs AgrupacionObtenidaEvent;

        public void ObtenAgrupacionEmpleados()
        {
            m_SesionBase.MuestraMensaje("Obteniendo Agrupaciones");
            m_S_Personas.ObtenAgrupacionEmpleadosCompleted += m_S_Personas_ObtenAgrupacionEmpleadosCompleted;
            m_S_Personas.ObtenAgrupacionEmpleadosAsync(m_SesionBase.SESION_SEGURIDAD);
        }

        void m_S_Personas_ObtenAgrupacionEmpleadosCompleted(object sender, ES_Personas.ObtenAgrupacionEmpleadosCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    m_SesionBase.MuestraMensaje("Agrupaciones Obtenidas", 3);
                    if (AgrupacionObtenidaEvent != null)
                        AgrupacionObtenidaEvent(e.Result);
                }
                else
                {
                    m_SesionBase.MuestraMensaje("Error al obtener las agrupaciones", 5);

                }
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

        public delegate void ObtenPermisosAgrupacionArgs(string ListadoPermisos);
        public event ObtenPermisosAgrupacionArgs ObtenPermisosAgrupacionEvent;

        public void ObtenPermisosAgrupacion(string Agrupacion)
        {
            if (Agrupacion == null || Agrupacion == "")
                Agrupacion = "|";
            m_SesionBase.MuestraMensaje("Obteniendo Permisos Otorgados");
            m_S_Personas.ObtenPermisosAgrupacionCompleted += m_S_Personas_ObtenPermisosAgrupacionCompleted;
            m_S_Personas.ObtenPermisosAgrupacionAsync(m_SesionBase.SESION_SEGURIDAD, m_SesionBase.SUSCRIPCION_ID_SELECCIONADA, Agrupacion);
        }

        void m_S_Personas_ObtenPermisosAgrupacionCompleted(object sender, ES_Personas.ObtenPermisosAgrupacionCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    m_SesionBase.MuestraMensaje("Listo", 3);
                    if (ObtenPermisosAgrupacionEvent != null)
                        ObtenPermisosAgrupacionEvent(e.Result);
                }
                else
                {
                    m_SesionBase.MuestraMensaje("Error al obtener Permisos Otorgado", 5);

                }
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

        public delegate void AsignaPermisoAgrupacionArgs(int Resultado);
        public event AsignaPermisoAgrupacionArgs AsignaPermisoAgrupacionEvent;

        public void AsignaPermisoAgrupacion(string Usuario, string Agrupacion, int TipoPermisoID)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Permisos Otorgados");
            m_S_Personas.AsignaPermisoAgrupacionCompleted += m_S_Personas_AsignaPermisoAgrupacionCompleted;
            m_S_Personas.AsignaPermisoAgrupacionAsync(m_SesionBase.SESION_SEGURIDAD, Usuario, Agrupacion, TipoPermisoID);
        }

        void m_S_Personas_AsignaPermisoAgrupacionCompleted(object sender, ES_Personas.AsignaPermisoAgrupacionCompletedEventArgs e)
        {

            try
            {

                if (e.Result == 1)
                    m_SesionBase.MuestraMensaje("Permiso Otorgado", 3);
                else
                    m_SesionBase.MuestraMensaje("Error al otorgar permiso", 5);
                if (AsignaPermisoAgrupacionEvent != null)
                    AsignaPermisoAgrupacionEvent(e.Result);

                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

        public delegate void QuitaPermisoAgrupacionArgs(int Resultado);
        public event QuitaPermisoAgrupacionArgs QuitaPermisoAgrupacionEvent;

        public void QuitaPermisoAgrupacion(int PermisoUsuarioID)
        {
            m_SesionBase.MuestraMensaje("Obteniendo Permisos Otorgados");
            m_S_Personas.QuitaPermisoAgrupacionCompleted += m_S_Personas_QuitaPermisoAgrupacionCompleted;
            m_S_Personas.QuitaPermisoAgrupacionAsync(m_SesionBase.SESION_SEGURIDAD, PermisoUsuarioID);
        }

        void m_S_Personas_QuitaPermisoAgrupacionCompleted(object sender, ES_Personas.QuitaPermisoAgrupacionCompletedEventArgs e)
        {
            try
            {
                if (e.Result == 1)
                    m_SesionBase.MuestraMensaje("Permiso eliminado", 3);
                else
                    m_SesionBase.MuestraMensaje("Error al eliminar el permiso", 5);
                if (QuitaPermisoAgrupacionEvent != null)
                    QuitaPermisoAgrupacionEvent(e.Result);
                return;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }

            m_SesionBase.MuestraMensaje("Error de red", 5);
        }

    }
}
