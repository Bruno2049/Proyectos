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
public class S_ConfigUsuario
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
    /// Se encarga de mandar a llamar una funcion dentro de la Clase CeC_Config
    /// para almacenar una variable de configuracion dentro de la base de datos.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    [OperationContract]
    public bool GuardaConfig(string SesionSeguridad, string Variable, string Valor)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;

        return CeC_Config.GuardaConfig(Sesion.USUARIO_ID, Variable, Valor);
    }
    /// <summary>
    /// Manda a llamar una funcion para obtener la configuracion 
    /// especifica de algun usuario.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenConfig(string SesionSeguridad, string Variable, string ValorDefecto)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return ValorDefecto;

        return CeC_Config.ObtenConfig(Sesion.USUARIO_ID, Variable, ValorDefecto);
    }
    // Agregue aquí más operaciones y márquelas con [OperationContract]
}
