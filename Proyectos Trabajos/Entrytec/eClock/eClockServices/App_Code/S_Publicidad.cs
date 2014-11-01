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
public class S_Publicidad
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
    public String ObtenImagen(int PublicidadID, DateTime FechaHoraMinima)
    {
        try
        {

            byte[] Foto = CeC_Publicidad.ObtenImagen(PublicidadID, FechaHoraMinima);
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
    public string ObtenListado(int TipoPublicidad, string Hash)
    {
        try
        {
            DataSet DS = CeC_Publicidad.ObtenListado(TipoPublicidad);
            string JSon = CeC_BD.DataSet2JsonV2(DS);
            if (JSon == null)
                return null;
            if (Hash != null && Hash != "")
                if (MD5Core.GetHashString(JSon) == Hash)
                    return "==";
            return JSon;
        }
        catch { }
        return null;
    }

	// Agregue aquí más operaciones y márquelas con [OperationContract]
}
