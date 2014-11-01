using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Nominas
{
	// Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
	// Para crear una operación que devuelva XML,
	//     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
	//     e incluya la siguiente línea en el cuerpo de la operación:
	//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
	[OperationContract]
	public void DoWork()
	{
		// Agregue aquí la implementación de la operación
		return;
	}
    /// <summary>
    /// Obtiene los datos del recibo de nomina,
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="PersonaID"></param>
    /// <param name="RecNominaID"></param>
    /// <param name="AutoGuardar">si es true tomara los datos previamente almacenados del modelo</param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenDatosRecibo(string SesionSeguridad, int RecNominaID, string Lang)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        eClockBase.Modelos.Nomina.Reporte_RecNomina RecNominas = CeC_RecNominas.ObtenRecibosNomina(new eClockBase.Modelos.Nomina.Model_Parametros(RecNominaID), Lang, Sesion);
        return Newtonsoft.Json.JsonConvert.SerializeObject(RecNominas);
    }
	// Agregue aquí más operaciones y márquelas con [OperationContract]

    // Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
    // Para crear una operación que devuelva XML,
    //     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
    //     e incluya la siguiente línea en el cuerpo de la operación:
    //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
    [OperationContract]
    public bool ConfirmaReciboNominaImpreso(string SesionSeguridad, int RecNominaID, int RecNominaImp)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return false;            
            return CeC_RecNominas.ConfirmaReciboNominaImpreso(RecNominaID, RecNominaImp);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    //Indica que esta funcion puede ser publica y usable desde el cliente del servicio
    /// <summary>
    /// Importa los recibos de nomina
    /// </summary>
    /// <param name="SesionSeguridad">Cadena que tiene datos sobre la sesion</param>
    /// <param name="Datos">Modelo de una lista de datos de Importación de recibos de nomina en formato JSon</param>
    /// <returns>PersonasLinkIds separados por coma que puedieron ser importados satisfactoriamente</returns>
    [OperationContract]
    public string ImportaRecibos(string SesionSeguridad, string Datos)
    {
        string PersonasLinksIds = "";
        try
        {
            //Usando Sesion Seguridad carga un objeto de tipo Sesion
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            //Si el Sesion Seguridad no es valido o ha expirado será nulo
            if (Sesion == null)
                return "";
            //Si la cadena Json esta comprimida, la descomprime si no la regresa igual
            string JsonSinComp = eClockBase.Controladores.CeC_ZLib.ZJson2Json(Datos);
            //Si la cadena Json no es una lista, la combierte en lista
            string JsonLista = eClockBase.CeC.Json2JsonList(JsonSinComp);
            //Deserealiza la lista Json en una lista de tipo Model_RecNominasPDFyXML
            List<eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML> RecibosNomina =
                Newtonsoft.Json.JsonConvert.DeserializeObject<
                    List<eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML>
                    >(JsonLista);
            foreach (eClockBase.Modelos.Nomina.Model_RecNominasPDFyXML RecNomina in RecibosNomina)
            {
                try
                {
                    //Obtiene el identificador unico de la persona a partir de su numero de empleado, y validando la suscripcion
                    int PersonaID = CeC_Personas.ObtenPersonaIDBySuscripcion(RecNomina.PERSONA_LINK_ID, Sesion.SUSCRIPCION_ID);
                    //Creo un modelo de la tabla que deseo editar
                    eClockBase.Modelos.Model_REC_NOMINAS ModeloRecNomina = new eClockBase.Modelos.Model_REC_NOMINAS();
                    //Como persona_id y nomina_id son tambien llaves unicas del registro, puedo usarlas
                    //para buscar el registro a editar
                    ModeloRecNomina.NOMINA_ID = RecNomina.NOMINA_ID;
                    ModeloRecNomina.PERSONA_ID = PersonaID;
                    bool EsNuevo = true;
                    ///Obtiene de la base de datos en la tabla EC_REC_NOMINAS con llaves NOMINA_ID,PERSONA_ID un 
                    ///modelo de datos  eClockBase.Modelos.Model_REC_NOMINAS ModeloRecNomina               
                    string JsonModeloRecNomina = CeC_Tablas.ObtenDatos("EC_REC_NOMINAS", "NOMINA_ID,PERSONA_ID", ModeloRecNomina, Sesion);
                    try
                    {
                        ModeloRecNomina = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_REC_NOMINAS>(JsonModeloRecNomina);
                        EsNuevo = false;
                    }
                    catch { }

                    
                    ///Asigna el pdf y el xml para ser guardados
                    ModeloRecNomina.REC_NOMINA_PDF = RecNomina.REC_NOMINA_PDF;
                    ModeloRecNomina.REC_NOMINA_XML = RecNomina.REC_NOMINA_XML;

                    ///Obtener datos de la nomina
                    eClockBase.Modelos.Model_NOMINAS Nomina = new eClockBase.Modelos.Model_NOMINAS();
                    Nomina.NOMINA_ID = RecNomina.NOMINA_ID;
                    string JsonNomina = CeC_Tablas.ObtenDatos("EC_NOMINAS", "NOMINA_ID", Nomina, Sesion);
                    Nomina = Newtonsoft.Json.JsonConvert.DeserializeObject<eClockBase.Modelos.Model_NOMINAS>(JsonNomina);
                    
                    //Asignar datos de la nomina al recibo de nomina
                    ModeloRecNomina.TIPO_NOMINA_ID = Nomina.TIPO_NOMINA_ID;



                    ///Guarda en la base de datos en la tabla EC_REC_NOMINAS con llave primaria REC_NOMINA_ID
                    if (CeC_Tablas.GuardaDatos("EC_REC_NOMINAS", "REC_NOMINA_ID", ModeloRecNomina, EsNuevo, Sesion, Sesion.SUSCRIPCION_ID) > 0)
                        //Agrega a la lista de personaslinksids separadas por coma un personalinkid
                        PersonasLinksIds = CeC.AgregaSeparador(PersonasLinksIds, RecNomina.PERSONA_LINK_ID.ToString(), ",");

                }
                catch (Exception ex)
                {
                    CIsLog2.AgregaError(ex);
                }
            }
            //return CeC_RecNominas.ConfirmaReciboNominaImpreso(RecNominaID, RecNominaImp);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }

        return PersonasLinksIds;
    }
}
