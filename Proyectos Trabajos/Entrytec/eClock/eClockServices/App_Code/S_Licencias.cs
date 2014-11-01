using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Licencias
{
    public void DoWork()
    {
    }

 /*   [OperationContract]
    public DataSet ObtenLicencias(string SesionSeguridad, int SuscripcionID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;

        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return null;
        return CeC_Licencias.ObtenLicencias(SuscripcionID);
    }
    */
    /// <summary>
    /// Se encarga de llamar ala fucnion CreaLicencia que se encuentra
    /// dentro de la clase CeC_Licencias.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="UsuarioID"></param>
    /// <param name="SuscripcionID"></param>
    /// <param name="OrigenID"></param>
    /// <param name="DistribuidorID"></param>
    /// <param name="VigenciaMeses"></param>
    /// <returns></returns>
    [OperationContract]
    public string CreaLicencia(string SesionSeguridad, int UsuarioID, int SuscripcionID, int OrigenID, int DistribuidorID, int VigenciaMeses)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return "";
        return CeC_Licencias.CreaLicencia(Sesion, UsuarioID, SuscripcionID, OrigenID, DistribuidorID, VigenciaMeses);
    }
    /// <summary>
    /// Obtiene la validacion de la licencia con respecto
    /// ala maquina que hace referencia.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Licencia"></param>
    /// <param name="Maquina"></param>
    /// <returns></returns>
    [OperationContract]
    public string ValidaLicencia(string SesionSeguridad, string Licencia, string Maquina)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return "";
        return CeC_Licencias.ValidaLicencia(Licencia,Maquina);
    }
    /// <summary>
    /// Llama un metodo para verificar y actualizar la licencia
    /// que actualmente se requiere utilizar, para ser ocupada en la ejecucion.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Licencia"></param>
    /// <param name="Maquina"></param>
    /// <returns></returns>
    [OperationContract]
    public int UsaLicencia(string SesionSeguridad, string Licencia, string Maquina)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        return CeC_Licencias.UsaLicencia(Licencia,Maquina);
    }
    /// <summary>
    /// Crea una sesion sobre la ejecucion y crea la licencia de la misma ejecucion
    /// para ser utilizada en el sistema en ejecución
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Maquina"></param>
    /// <returns></returns>
    [OperationContract]
    public string CreaUsaLicencia(string SesionSeguridad, string Maquina)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return "";
        string Licencia = CeC_Licencias.CreaLicencia(Sesion, Sesion.USUARIO_ID, Sesion.SUSCRIPCION_ID, 3, 0, 2);
        if (CeC_Licencias.UsaLicencia(Licencia, Maquina) <= 0)
            return "ERROR";
        return Licencia;
    }
}
