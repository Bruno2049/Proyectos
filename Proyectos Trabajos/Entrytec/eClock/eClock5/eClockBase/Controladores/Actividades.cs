using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace eClockBase.Controladores
{
    /// <summary>
    /// En esta clase se realiza la gestion en la Actividades y el proceso de la comunicacion con eClockServices atraves de S_Activides.svc
    /// </summary>
    public class Actividades
    {
        /// <summary>
        /// Se crea el objeto m_S_Actividades nulo y en este gestionara la comunicacion con S_Actividades
        /// </summary>
        ES_Actividades.S_ActividadesClient m_S_Actividades = null;
        /// <summary>
        /// Se crea un objeto CeC_SesionBase para la gestion de el usuario de la sesion y los datos necesarios para esto
        /// </summary>
        CeC_SesionBase m_SesionBase = null;

        /// <summary>
        /// Constructos de la Clase Actividades en la cual se generara la conexion client de S_Actividades 
        /// </summary>
        /// <param name="SesionBase">Este parametro es esencial para poder utilizar el metodo adecuadamente ya que con este se obtiene la informacion de el usuario</param>
        public Actividades(CeC_SesionBase SesionBase)
        {
            // Se crea la asociacion de el entre el cliente y el servidor 
            m_S_Actividades = new ES_Actividades.S_ActividadesClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Actividades.svc"));
            //Se almacena la Sesion de el usuario para verificar datos nesesarios para las operaciones
            m_SesionBase = SesionBase;
        }

        /// <summary>
        /// Este metodo se encarga de relizar un string con el identificador de la imagen, nombre y extencion de la imagen para verificar un igual en el dispositivo del cliente
        /// </summary>
        /// <param name="ActividadID">Se requiere el Actividad_ID para realizar la busqueda de la imagen </param>
        /// <returns> Regresa un String con el nombre completo de la imagen </returns>
        public string ObtenImagenNombre(int ActividadID)
        {
            //Se Crea el string que almacenara el nombre se coloca un identificador, ActividadID y estenxion
            string NombreFoto = "Act_" + ActividadID.ToString() + ".jpg";
            //Se envia el resultado con NombreFoto
            return NombreFoto;
        }

        /// <summary>
        /// Este metodo seral el delegado de de Obtener la imagen en byte 
        /// </summary>
        /// <param name="Imagen"></param>
        public delegate void ObtenImagenArgs(byte[] Imagen);

        /// <summary>
        /// Este Almacenara el evento de el estado de el metodo asyn
        /// </summary>
        public event ObtenImagenArgs ObtenImagenFinalizado;
        
        /// <summary>
        /// Este metodo Se encargara de verificar si el archivo ya se a descargado en el dispositivo client
        /// </summary>
        /// <param name="ActividadID">Este parametro es el que se necesitara para buscar la imagen ya que es la composicion de el nombre cuando se guarda en dispositivo cliente</param>
        public void ObtenImagen(int ActividadID)
        {
            //Obtiene el nombre completo de la imagen con el nombre del ActividadID atraves de el metodo
            string NombreFoto = ObtenImagenNombre(ActividadID);
            //Se asigna una fecha nula deacuerdo a clase CeC
            DateTime FechaModificacion = CeC.FechaNula;
            //Se verifica si la imagen existe
            if (CeC_Stream.sExisteArchivo(NombreFoto))
            {
                //Al encontrar que la imagen existe se realiza una verificacion de la fecha en que fue modificada
                FechaModificacion = CeC_Stream.sFechaHoraModificacion(NombreFoto);
                //En caso de que concluya el metodo ObtenImagen se ejecutara la instrucion
                if (ObtenImagenFinalizado != null)
                    ObtenImagenFinalizado(CeC_Stream.sLeerBytes(NombreFoto));
            }

            //Se crea el evento y el metodo ObtenerImagen en el cual al concluir con la entrara en el metodo delegado cuando resiva el evento
            m_S_Actividades.ObtenImagenCompleted += delegate (object sender, ES_Actividades.ObtenImagenCompletedEventArgs e)
                    {
                        try
                        {

                            byte[] Datos = null;
                            //Si regeso algun dato lo deserializa
                            if(e.Result != null)
                                Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<byte[]>(e.Result);

                            if(Datos != null && !CeC.EsImagenIgual(Datos))
                                CeC_Stream.sNuevoBytes(NombreFoto, Datos);

                            if(!CeC.EsImagenIgual(Datos))
                                //Si el el evento no es nulos se llamara al metodo asyn finalizado
                                if (ObtenImagenFinalizado != null)
                                    ObtenImagenFinalizado(Datos);
                            /*
                            //En caso de que "e" Obtenga un resultado entrara en codigo de la condicional
                            if (e.Result != null)
                            {
                                // se crea un arrego de byte para almacena la imagen en la que tambien se realiza el proceso de serializacion
                                byte[] Datos = eClockBase.Controladores.CeC_ZLib.Json2Object<byte[]>(e.Result);

                                //Si se consigue una imagen se entrara en el codigo de la condicional
                                if (Datos.Length > 0)
                                {
                                    //Se verifica una comparacion por el moento no es funcional
                                    CeC_Stream.sNuevoBytes(NombreFoto, Datos);
                                    //Si el el evento no es nulos se llamara al metodo asyn finalizado
                                    if (ObtenImagenFinalizado != null)
                                        ObtenImagenFinalizado(Datos);
                                }
                            }*/
                            return;
                        }
                        catch (Exception ex)
                        {
                            //m_SesionBase.MuestraMensaje("Error al cargar los datos", 10);
                            CeC_Log.AgregaError(ex);
                        }
                        m_SesionBase.MuestraMensaje("Error de Red", 5);
                    };
            //Se crea el metodo asincrono con eClockServices en ObtenerImagen sel servicio S_Actividades.svn para obtener imagen y se envian los datos de secion seguridad, ID_Actividad y Fecha de modificacion
            m_S_Actividades.ObtenImagenAsync(m_SesionBase.SESION_SEGURIDAD, ActividadID, FechaModificacion);
        }
        
        /// <summary>
        /// Es el metodo delegado cuando se completa la operacion con eClockServices
        /// </summary>
        /// <param name="Resultado">Se pide un int que de el resultado derivado de la operacion</param>
        public delegate void IncribirseArgs(int Resultado);
        /// <summary>
        /// Este sera el evento que gestionara la comunicacion con el servicio y almacenar el resultado de la operacion
        /// </summary>
        public event IncribirseArgs IncribirseEvent;

        /// <summary>
        /// Este metodo se utilizara para realizar la inscripcion de una persona a una actividad previamente guardada
        /// </summary>
        /// <param name="ActividadID">Se indica como el ID de la actividad Ubicado en EC_Actividades y Actividad_ID</param>
        /// <param name="PersonaID">Se indica como el ID de el Usuario que decea suscribirse y se encuentra en EC_Personas y el campo Persona_ID</param>
        /// <param name="TipoInscripcionID">Se identifica como el ID de Tipo de Inscripcion ya sea para eClock y Kiosko verificar en EC_Tipo_Actividad</param>
        /// <param name="Descripcion">Se  identifica como una Descripcion de la inscripcion</param>
        public void Incribirse(int ActividadID, int PersonaID, int TipoInscripcionID, string Descripcion)
        {
            //Se envia un mensaje  por sesionBase
            m_SesionBase.MuestraMensaje("Inscribiendoce..");
            //Se crea el metodo completed del metodo async de Inscribirse
            m_S_Actividades.IncribirseCompleted += m_S_Actividades_IncribirseCompleted;
            //Se crea el metodo async para Inscribirse de eClockServices
            m_S_Actividades.IncribirseAsync(m_SesionBase.SESION_SEGURIDAD, ActividadID, PersonaID, TipoInscripcionID, Descripcion);
        }

        /// <summary>
        /// Al completarse el metodo Inscribirse de se ejecutara este emtodo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">"e" Contendra el resultado de la operacion de S_Inscipcion.Inscribirse</param>
        void m_S_Actividades_IncribirseCompleted(object sender, ES_Actividades.IncribirseCompletedEventArgs e)
        {
            try
            {
                //Si se obtiene un resultado se envia un mensaje atraves de SesionBase y se envia el resultado a IncribirseEvent
                if (e.Result > 0)
                {
                    m_SesionBase.MuestraMensaje("Inscripción Satisfactoria", 3);
                    if (IncribirseEvent != null)
                        IncribirseEvent(e.Result);
                }
                else
                    //Si hay un error sera necesario verificar el metodo en eClockServices 
                    m_SesionBase.MuestraMensaje("Error al crear la inscripción", 5);
                return;
            }
            catch (Exception ex)
            {                
                CeC_Log.AgregaError(ex);
            }
            //No fue posible establecer una conexion con el servicio
            m_SesionBase.MuestraMensaje("Error de Red", 5);
        }
    }
}
