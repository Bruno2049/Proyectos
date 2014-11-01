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
public class S_Justificaciones
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

	// Agregue aquí más operaciones y márquelas con [OperationContract]
}
