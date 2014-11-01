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
public class S_Campos
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
    public string ListaCampos(string SesionSeguridad, int TIPO_PERSONA_ID, int SuscripcionID, string Lang)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return null;

        eClockBase.Modelos.Model_CAMPOS_DATOS Campos = new eClockBase.Modelos.Model_CAMPOS_DATOS();
        Campos.SUSCRIPCION_ID = SuscripcionID;
        Campos.TIPO_PERSONA_ID = TIPO_PERSONA_ID;
        string Resultado = CeC_Tabla.ObtenDatos("EC_CAMPOS_DATOS", "SUSCRIPCION_ID,TIPO_PERSONA_ID", Newtonsoft.Json.JsonConvert.SerializeObject(Campos), Sesion,"CAMPO_DATO_ORDEN");
        if (Resultado == "ERROR_SIN_RESULTADOS")
        {
            CeC_CamposDatos.CreaCamposPredeterminados(SuscripcionID, TIPO_PERSONA_ID, Lang, Sesion);
            Resultado = CeC_Tabla.ObtenDatos("EC_CAMPOS_DATOS", "SUSCRIPCION_ID,TIPO_PERSONA_ID", Newtonsoft.Json.JsonConvert.SerializeObject(Campos), Sesion, "CAMPO_DATO_ORDEN");
        }
        return Resultado;
    }

    [OperationContract]
    public string GuardaCampos(string SesionSeguridad, string JSonModelo)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            int Actualizados = 0;
            List<eClockBase.Modelos.Model_CAMPOS_DATOS> CamposDatos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<eClockBase.Modelos.Model_CAMPOS_DATOS>>(JSonModelo);

            return CeC_CamposDatos.GuardaCampos(CamposDatos, Sesion).ToString();
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return "ERROR";
    }
    // Agregue aquí más operaciones y márquelas con [OperationContract]
}
