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
public class S_Periodos
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

    [OperationContract]
    public string ObtenListado(string SesionSeguridad, DateTime FechaDesde, DateTime FechaHasta, int EdoPeriodo, int SuscripcionID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return "ERROR_SUSCRIPCION";
        return CeC_BD.DataSet2JsonV2(CeC_Periodos_N.ObtenPeriodosDetalle(FechaDesde, FechaHasta, EdoPeriodo, SuscripcionID));
    }
	// Agregue aquí más operaciones y márquelas con [OperationContract]
}
