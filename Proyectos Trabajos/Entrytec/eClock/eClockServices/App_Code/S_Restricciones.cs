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
public class S_Restricciones
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
    /// Devulve si un perfil tiene derecho sobre determinada restricción.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Restriccion"></param>
    /// <returns></returns>
    [OperationContract]
    public bool TieneDerecho(string SesionSeguridad, string Restriccion)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        return CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, Restriccion);
    }
    /// <summary>
    /// Devulve si un perfil tiene derechos sobre determinadas restricciónes.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Restricciones"></param>
    /// <returns></returns>
    [OperationContract]
    public string TieneDerechos(string SesionSeguridad, string Restricciones)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return "";
        string R = CeC_Restricciones.TieneDerechos(Sesion.PERFIL_ID, Restricciones);
        return R;
    }
         
	// Agregue aquí más operaciones y márquelas con [OperationContract]
}
