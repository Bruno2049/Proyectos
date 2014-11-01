using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Empleados
{
    [OperationContract]
	public void DoWork()
	{
        return;
	}

    /// <summary>
    /// Manda a llamar una funcion que a su vez esta localiza el ID de la persona
    /// para posteriormente validad la subscripcion.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="NumeroDeEmpleado"></param>
    /// <param name="USUARIO_ID"></param>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene el ID de la Persona usando su Número de Empleado", MessageName = "AsignaIncidencia", EnableSession = true)]
    [OperationContract]
    public int ObtenPersonaID(string SesionSeguridad, int NumeroDeEmpleado)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        return CeC_Empleados.ObtenPersonaID(NumeroDeEmpleado, Sesion.USUARIO_ID);
    }

    /// <summary>
    /// Obtiene la clave del empleado y como referencia utiliza el ID del mismo
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="NumeroDeEmpleado"></param>
    /// <param name="USUARIO_ID"></param>
    /// <returns></returns>
    [WebMethod(Description = "Obtiene el ID de la Persona usando su Número de Empleado", MessageName = "ClaveEmpleado2PersonaID", EnableSession = true)]
    [OperationContract]
    public int ClaveEmpleado2PersonaID(string SesionSeguridad, string NumeroDeEmpleado)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        return CeC_Empleados.ClaveEmpl2PersonaID(NumeroDeEmpleado, Sesion.USUARIO_ID);
    }
}