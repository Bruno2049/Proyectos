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
public class S_Reportes
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
    public string ObtenListado(string SesionSeguridad, string Hash)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        string JSon = CeC_BD.DataSet2JsonV2(CeC_Reportes.ObtenListadoReportes(Sesion));
        if (Hash != null && Hash != "" && JSon != null && JSon != "")
            if (MD5Core.GetHashString(JSon) == Hash)
                return "==";
        return JSon;

    }

    [OperationContract]
    public string ObtenReporte(string SesionSeguridad, int ReporteID, string Parametros, int FormatoRepID, string Lang)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;

            return CeC_Reportes.ObtenReporte(ReporteID, Parametros, FormatoRepID, Sesion, SesionSeguridad, Lang);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    [OperationContract]
    public string ObtenReporteMail(string SesionSeguridad, string eMails, string Titulo, string Cuerpo, int ReporteID, string Parametros, int FormatoRepID, string Lang)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            if (eMails == null || eMails == "")
                eMails = CeC_Usuarios.ObtenUsuarioeMail(Sesion.USUARIO_ID);
             if (eMails == null || eMails == "")
                 return "ERROR_SIN_EMAIL";
            string Reporte = CeC_Reportes.ObtenReporte(ReporteID, Parametros, FormatoRepID, Sesion, SesionSeguridad, Lang);
            byte[] ArchivoReporte = eClockBase.Controladores.CeC_ZLib.Json2Object<byte[]>(Reporte);

            if (ArchivoReporte != null)
                if (CeC_Mails.EnviarMail(eMails, "", Titulo, Cuerpo, ArchivoReporte))
                    return "OK";
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

}
