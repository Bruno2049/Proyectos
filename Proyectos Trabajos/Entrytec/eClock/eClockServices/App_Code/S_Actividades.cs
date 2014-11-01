using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Actividades
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
    public String ObtenImagen(string SesionSeguridad, int ActividadID, DateTime FechaHoraMinima)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;

            byte[] Foto = CeC_Actividades.ObtenImagen(ActividadID, FechaHoraMinima, Sesion);
            if (Foto == null)
                return null;
            return JsonConvert.SerializeObject(Foto);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    [OperationContract]
    public int Incribirse(string SesionSeguridad, int ActividadID, int PersonaID, int TipoInscripcionID, string Descripcion)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        return CeC_Actividades.Incribirse(ActividadID, PersonaID, TipoInscripcionID, Descripcion, Sesion);
    }
    // Agregue aquí más operaciones y márquelas con [OperationContract]
}
