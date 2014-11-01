using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Localizaciones
{
    [OperationContract]
    public void DoWork()
    {
        // Agregue aquí la implementación de la operación
        return;
    }

    /// <summary>
    /// Obtiene la etiqueta correspondiente
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenEtiqueta(string LocalizacionIdioma, string LocalizacionLlave)
    {
        return CeC_Localizaciones.ObtenLocalizacionEtiqueta(LocalizacionIdioma, LocalizacionLlave);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenEtiquetaDebug(string LocalizacionIdioma, string LocalizacionLlave, string TextoPredeterminado)
    {
        return CeC_Localizaciones.ObtenLocalizacionEtiqueta(LocalizacionIdioma, LocalizacionLlave, TextoPredeterminado, true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenAyuda(string LocalizacionIdioma, string LocalizacionLlave)
    {
        return CeC_Localizaciones.ObtenLocalizacionAyuda(LocalizacionIdioma, LocalizacionLlave);
    }

    [OperationContract]
    public string ObtenEtiquetasAyuda(string SesionSeguridad, string LocalizacionIdioma, string Hash)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        int Suscripcion = 0;
        if (Sesion != null)
            Suscripcion = Sesion.SUSCRIPCION_ID;
        string Json = eClockBase.Controladores.CeC_ZLib.Json2ZJson(CeC_BD.DataSet2JsonV2(CeC_Localizaciones.ObtenEtiquetasAyuda(LocalizacionIdioma, Suscripcion, true)));
        if (MD5Core.GetHashString(Json) == Hash)
            return "==";
        return eClockBase.Controladores.CeC_ZLib.Json2ZJson(Json);
        return Json;
    }

    // Agregue aquí más operaciones y márquelas con [OperationContract]
}
