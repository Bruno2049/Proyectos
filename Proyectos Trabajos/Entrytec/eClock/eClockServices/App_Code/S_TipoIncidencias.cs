using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_TipoIncidencias
{
    [OperationContract]
	public void DoWork()
	{
	}

    /// <summary>
    /// Almacena los datos del tipo de incidencias, deserializando y serializando los datos.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Datos"></param>
    /// <returns></returns>
    [OperationContract]
    public String GuardaDatos(string SesionSeguridad, string Datos)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        eClockBase.Modelos.Model_TIPO_INCIDENCIAS TipoIncidencias = JsonConvert.DeserializeObject<eClockBase.Modelos.Model_TIPO_INCIDENCIAS>(Datos);
        try
        {
            bool EsNuevo = true;
            if (TipoIncidencias.TIPO_INCIDENCIA_ID > 0)
            {
                EsNuevo = false;
            }
            int TipoIncidencia_ID = CeC_Tabla.GuardaDatos("EC_TIPO_INCIDENCIAS", "TIPO_INCIDENCIA_ID", JsonConvert.SerializeObject(TipoIncidencias), EsNuevo, Sesion, Sesion.SUSCRIPCION_ID);
            if (EsNuevo)
                TipoIncidencias.TIPO_INCIDENCIA_ID = TipoIncidencia_ID;
            return JsonConvert.SerializeObject(TipoIncidencias);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }
}
