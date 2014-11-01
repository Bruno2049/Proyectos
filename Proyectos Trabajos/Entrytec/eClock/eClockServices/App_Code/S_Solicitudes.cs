using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;
using System.Data;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Solicitudes
{
	// Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
	// Para crear una operación que devuelva XML,
	//     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
	//     e incluya la siguiente línea en el cuerpo de la operación:
	//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
	[OperationContract]
    public String SolicitudVacaciones(string SesionSeguridad)
	{
		// Agregue aquí la implementación de la operación
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        DataSet DS = CeC_Solicitudes.SolicitudVacaciones();
        string JSon = CeC_BD.DataSet2JsonV2(DS);
        return JSon;
	}

	// Agregue aquí más operaciones y márquelas con [OperationContract]
}
