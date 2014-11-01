using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using eClockBase.Modelos.Accesos;

namespace eClockBase.Controladores
{
    /// <summary>
    /// Esta es la clase en la cual se realiza la gestion de las peticiones y metodos 
    /// </summary>
    public class Accesos
    {
        /// <summary>
        /// Se crea una propiedad en la cual se le asignara el control de la comunicacion con el servicio S_Accesos y se le asignaran los eventos de metodo asincrono
        /// </summary>
        ES_Accesos.S_AccesosClient m_S_Accesos = null;

        /// <summary>
        /// Objeto de la clase CeC_sesionBase en este se obtendra mucha de la informacion de el usuario que esencial para la busqueda de accesos
        /// </summary>
        CeC_SesionBase m_SesionBase = null;

        /// <summary>
        /// Es el unico constructor de Accesos y en este se alamcenara lo necesario para poder comunicarse con 
        /// </summary>
        /// <param name="SesionBase">
        /// Sesionbase es necesaria para obtener la informacion de el usuario y poder establecer una comunicacion con el servicio
        /// </param>
        public Accesos(CeC_SesionBase SesionBase)
        {
            m_S_Accesos = new ES_Accesos.S_AccesosClient(SesionBase.ObtenBasicHttpBinding(), SesionBase.ObtenEndpointAddress("S_Accesos.svc"));
            m_SesionBase = SesionBase;
        }

        /// <summary>
        /// Se declaran las propiedades y eventos con los cuales se Utilizara el Modelo de Accesos y el los eventos
        /// </summary>
        /// <param name="Accesos">El evento Se obtendra un Listado del Modelo Model_Accesos y alamcenala las propiedades</param>
        public delegate void ObtenAccesosArgs(List<eClockBase.Modelos.Accesos.Model_Accesos> Accesos);
        /// <summary>
        /// Este evento se utilizara para verificar si se concluyo con ta tarea pendiente
        /// </summary>
        public event ObtenAccesosArgs EventoObtenAccesosFinalizado;

        /// <summary>
        /// Este metodo se encarga de dealizar una peticion a eClockServices para obtener los Accesos con el envio de los parametros necesarios
        /// para obtenerlos, Ademas en esta se realiza pa peticion asincrona
        /// </summary>
        /// <param name="MuestraAgrupacion">
        /// Envia un booleano para verificar si mostrara la agrupacion
        /// </param>
        /// <param name="Persona_ID">
        /// Se envia Persona_ID para realizar busqueda
        /// </param>
        /// <param name="Agrupacion">
        /// Envia el fromato en string de la agrupacion<
        /// </param>
        /// <param name="FechaInicial">
        /// En FechaInicial es el intervalo inicial de la busqueda de los accesos
        /// </param>
        /// <param name="FechaFinal">
        /// >En FechaFinal es el intervalo final de la busqueda de los accesos
        /// </param>
        /// <param name="TerminalesIDs">
        /// Se envia el ID de parametro De la terminar que se va a solicitar el acceso
        /// </param>
        /// <param name="TipoAccesosIds">
        /// Se Envia si el tipo de acceso si fue correcto o no
        /// </param>
        public void ObtenAccesos(bool MuestraAgrupacion, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string TerminalesIDs, string TipoAccesosIds)
        {
            //Se envia un mensaje a muestra mensage
            m_SesionBase.MuestraMensaje("Obteniendo Datos");
            //Se crea el metodo y es donde se enviara el resultado despues de que se complete la operacion
            m_S_Accesos.ObtenAccesosCompleted += m_S_AccesosObtenAccesosCompleted;
            //Se Crea el metodo asincrono y se envian los parametros correspondientes
            m_S_Accesos.ObtenAccesosAsync(m_SesionBase.SESION_SEGURIDAD, MuestraAgrupacion, Persona_ID, Agrupacion, FechaInicial, FechaFinal, TerminalesIDs, TipoAccesosIds);
        }
        /// <summary>
        /// Este evento se ejecutara en el momento en que se complete la operacion y se disparara el envento
        /// </summary>
        /// <param name="sender">
        /// Se captura de el evento sender
        /// <param name="e">
        /// Este parametro es el encargado de resivir el prarametro y esta serializado en un JSON
        /// </param>
        void m_S_AccesosObtenAccesosCompleted(object sender, ES_Accesos.ObtenAccesosCompletedEventArgs e)
        {
            try
            {
                //Se crea la variable Resultado y se almacena el resultado que trajo e
                string Resultado = e.Result;
                //Muestra mensaje de Listo a el metodo MuestraMensaje
                m_SesionBase.MuestraMensaje("Listo", 3);
                //Esta linea crea un List de el modelo Model_Accesos llamado ObtenerAccesos y desearializa el resultado obtenido de e.Resultado y Cada uno de los resultados obtenidos  los envia como Model_Accesos a la lista
                List<eClockBase.Modelos.Accesos.Model_Accesos> ObtenerAccesos = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Modelos.Accesos.Model_Accesos>>(Resultado);
                //En caso de que el evento se haya completado se envia la lista 
                if (EventoObtenAccesosFinalizado != null)
                    EventoObtenAccesosFinalizado(ObtenerAccesos);
                return;
            }
            catch (Exception ex)
            {
                //Si el metodo cae en una excepcion se creara un registro a la clase CeC_Log y se llama al metodo agregarError con la excepcion ex
                CeC_Log.AgregaError(ex);
            }
            //En caso de que el evento nos se complete  se enviara el resultado como nulo y se mostrara un mensage de error de red
            m_SesionBase.MuestraMensaje("Error de Red", 5);
            if (EventoObtenAccesosFinalizado != null)
                EventoObtenAccesosFinalizado(null);
        }
        /// <summary>
        /// Este metodo se encargara de realizar la busqueda de una checada
        /// </summary>
        /// <param name="TerminalID">
        /// Se requiere el valor de Terminar_id para realizar la busqueda
        /// </param>
        /// <param name="Llave">
        /// </param>
        /// <param name="FechaHora">
        /// Se Indica la hora y fecha en que se realizo la checada
        /// </param>
        /// <param name="TAcceso">
        /// Se indica que se requiere el Tipo de acceso 
        /// </param>
        /// <param name="AgregarInmediatamente">
        /// Se envia un voleado en donde se indica si se decea agregar el acceso
        /// </param>
        public void AgregarCheckada(int TerminalID, string Llave, DateTime FechaHora, int TAcceso, bool AgregarInmediatamente)
        {
            //Se asigna el evento de el metodo asincrono de AgregarChecada
            m_S_Accesos.AgregaChecadaCompleted += m_S_Accesos_AgregaChecadaCompleted;
            //Se implementa el metodo Async y se envian los parametros correspondientes
            m_S_Accesos.AgregaChecadaAsync(m_SesionBase.SESION_SEGURIDAD, TerminalID, Llave, FechaHora, TAcceso, AgregarInmediatamente);
        }

        /// <summary>
        /// Al completarse la operacion se verificara se se obtuvo un valor booleano si no es aci caera en una excepcion 
        /// </summary>
        void m_S_Accesos_AgregaChecadaCompleted(object sender, ES_Accesos.AgregaChecadaCompletedEventArgs e)
        {
            try
            {
                bool Resultado = e.Result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Es el delegado de AgregaAcceso y controlara el metodo al obtenerse el evento 
        /// </summary>
        /// <param name="Guardado">Este es el boeleano en el cual se indicara su el evento fue agregada la checada</param>
        public delegate void AgregaAccesosArgs(bool Guardado);
        /// <summary>
        /// Este Evento se encargara de almacenar el resultado al guardar la checado
        /// </summary>
        public event AgregaAccesosArgs AgregarAccesosEvent;

        /// <summary>
        /// Este etodo se encargara de almacear un acceso y solo requiere que se se envie un List del Modelo Model_AccesosAgregar
        /// </summary>
        /// <param name="AccesosAgregar">Es una lista de Model_AccesosAgregar necesario para la operacion</param>
        public void AgregarAccesos(List<eClockBase.Modelos.Accesos.Model_AccesosAgregar> AccesosAgregar)
        {
            //En esta linea se realiza una serializacion en JSON y se almacena en el string DatosAccesos
            string DatoAccesos = JsonConvert.SerializeObject(AccesosAgregar);
            //Se envia un mensage informativo atraves de MuestraMensaje
            m_SesionBase.MuestraMensaje("Guardando Datos");

            //Se Alamacena el evento en el cual anteriormente declarado y se asigna al metodo que enviara la respuesta
            m_S_Accesos.AgregaAccesosCompleted += m_S_Accesos_AgregaAccesosCompleted;

            //Se realiza la ejecucion de el metodo async y envia los parametros del metodo AgragaAcceso de eClockServices
            m_S_Accesos.AgregaAccesosAsync(m_SesionBase.SESION_SEGURIDAD, DatoAccesos);
        }

        void m_S_Accesos_AgregaAccesosCompleted(object sender, ES_Accesos.AgregaAccesosCompletedEventArgs e)
        {
            try
            {
                //Verifica si el acceso fue guardado 
                if (e.Result != null)
                {
                    //En caso de que se guarde el acceso se enviara un mensage al usuario atraves de session
                    m_SesionBase.MuestraMensaje("Guardado", 3);
                    //Si el Evento deja de ser nulo se ira a AgregarAccesosEvent y enviara true
                    if (AgregarAccesosEvent != null)
                        AgregarAccesosEvent(true);
                    return;
                }
                else
                {
                    m_SesionBase.MuestraMensaje("Error al Guardar", 10);
                }

            }
            catch
            {
                //En caso de que no se guarde el acceso se enviara un mensage de error al usuario atrabes de session
                m_SesionBase.MuestraMensaje("Error de red", 10);
            }

            //Si no fue posible Guardar el acceso se cambiara a false al metodo y se dejara de intentar guardar el acceso ademas se enviara un mensaje de eror
            if (AgregarAccesosEvent != null)
                AgregarAccesosEvent(false);
            
        }
    }
}
