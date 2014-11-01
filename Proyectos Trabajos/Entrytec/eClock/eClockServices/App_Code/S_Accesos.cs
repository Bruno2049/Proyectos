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
public class S_Accesos
{
    [OperationContract]
    public void DoWork()
    {
        // Agregue aquí la implementación de la operación
        return;
    }

    /// <summary>
    /// Carga la sesion con un codigo de seguridad enviado através de SesionSeguridad
    /// La variable de SesionSeguridad lleva el codigo de seguridad que autentificara la secion
    /// y devuelve los usuarios que accesaron ya sea si fueron borrados o no del sistema de fechas especificas.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="MuestraAgrupacion"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Agrupacion"></param>
    /// <param name="FechaInicial"></param>
    /// <param name="FechaFinal"></param>
    /// <param name="TerminalesIDs"></param>
    /// <param name="TipoAccesosIds"></param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenAccesos(string SesionSeguridad, bool MuestraAgrupacion, int Persona_ID, string Agrupacion, DateTime FechaInicial, DateTime FechaFinal, string TerminalesIDs, string TipoAccesosIds)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        DataSet DS = CeC_Accesos.ObtenAccesosV5(MuestraAgrupacion, Persona_ID, Agrupacion, FechaInicial, FechaFinal, TerminalesIDs, TipoAccesosIds, Sesion.USUARIO_ID);
        List<eClockBase.Modelos.Accesos.Model_Accesos> LAccesos = (List<eClockBase.Modelos.Accesos.Model_Accesos>)CeC_BD.ConvertTo<eClockBase.Modelos.Accesos.Model_Accesos>(DS.Tables[0]);
        string ZJson = eClockBase.Controladores.CeC_ZLib.Object2ZJson(LAccesos);
        /*string Json = CeC_BD.DataSet2JsonV2(DS);
        string ZJson = eClockBase.Controladores.CeC_ZLib.Json2ZJson(Json);*/
        return ZJson;
    }

    /// <summary>
    /// 
    /// 
    ///
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    [OperationContract]
    public bool AgregaChecada(string SesionSeguridad, int TerminalID, string Llave, DateTime FechaHora, int TAccesoID, bool AgregarInmediatamente)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        return CeC_Accesos.AgregaChecada(TerminalID, Llave, FechaHora, TAccesoID, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, AgregarInmediatamente);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="TerminalID"></param>
    /// <param name="PersonaID"></param>
    /// <param name="FechaHora"></param>
    /// <param name="TAccesoID"></param>
    /// <returns>El acceso ID usado</returns>
    [OperationContract]
    public int AgregarAcceso(string SesionSeguridad, int TerminalID, int PersonaID, DateTime FechaHora, int TAccesoID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        return CeC_Accesos.AgregarAcceso(PersonaID, TerminalID, TAccesoID, FechaHora, Sesion.SESION_ID, Sesion.SUSCRIPCION_ID);
    }

    [OperationContract]
    public string AgregaAccesos(string SesionSeguridad, string DatoAccesos)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        List<eClockBase.Modelos.Accesos.Model_AccesosAgregar> AccesosAgregar = JsonConvert.DeserializeObject<List<eClockBase.Modelos.Accesos.Model_AccesosAgregar>>(DatoAccesos);
        string AccesosID = null;
        int AccesoID;
        try
        {
            foreach (eClockBase.Modelos.Accesos.Model_AccesosAgregar Acceso in AccesosAgregar)
            {
                int PersonaID = CeC_Personas.ObtenPersonaIDBySuscripcion(Acceso.PERSONA_LINK_ID, Sesion.SUSCRIPCION_ID);
                AccesoID = CeC_Accesos.AgregarAcceso(PersonaID,
                                                        Acceso.TERMINAL_ID,
                                                        Acceso.TIPO_ACCESO_ID,
                                                        Acceso.ACCESO_FECHAHORA,
                                                        Sesion.SESION_ID,
                                                        Sesion.SUSCRIPCION_ID);
                AccesosID += AccesoID + ",";
            }
            return AccesosID;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return null;
        }
    }


    // Agregue aquí más operaciones y márquelas con [OperationContract]
}
