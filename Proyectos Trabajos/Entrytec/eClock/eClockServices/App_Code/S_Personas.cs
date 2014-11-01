using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Newtonsoft.Json;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Personas
{
    [OperationContract]
    public void DoWork()
    {
        // Agregue aquí la implementación de la operación
        return;
    }

    [OperationContract]
    public int ObtenPersonaIDBySuscripcion(string SesionSeguridad, int Persona_Link_ID, int SuscripcionID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        return CeC_Personas.ObtenPersonaIDBySuscripcion(Persona_Link_ID, SuscripcionID);
    }

    [OperationContract]
    public bool ReagruparEmpleados(string SesionSeguridad, string Campos_Dato_IDs)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        Sesion.ConfiguraSuscripcion.CamposAgrupaciones = Campos_Dato_IDs;
        CeC_Agrupaciones.RegeneraAgrupaciones(Sesion.SUSCRIPCION_ID, 1, Campos_Dato_IDs, true);
        return true;
    }

    [OperationContract]
    public string ObtenAgrupacionEmpleados(string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return Sesion.ConfiguraSuscripcion.CamposAgrupaciones;
    }

    [OperationContract]
    public int QuitaPermisoAgrupacion(string SesionSeguridad, int PermisoUsuarioID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        if (CeC_Agrupaciones.QuitaPermisoUsuario(PermisoUsuarioID))
            return 1;
        return 0;
    }

    [OperationContract]
    public int AsignaPermisoAgrupacion(string SesionSeguridad, string Usuario, string Agrupacion, int PermisoID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        int UsuarioID = CeC_Usuarios.ObtenUsuarioID(Usuario);
        if (UsuarioID <= 0)
            return -3;
        if (!CeC_Agrupaciones.AsignaPermiso(Sesion.SESION_ID, Sesion.SUSCRIPCION_ID, UsuarioID, Agrupacion, PermisoID))
            return -2;
        else
            return 1;
        return 0;
    }

    [OperationContract]
    public string ObtenPermisosAgrupacion(string SesionSeguridad, int SuscripcionID, string Agrupacion)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return null;
        return CeC_Agrupaciones.ObtenPermisosUsuariosList(SuscripcionID, Agrupacion);
    }
    /// <summary>
    /// Retrae la busqueda de las personas con la sesionseguridad y
    /// posteriormente las intriduce dentro de un dataset que 
    /// se convertira en tipo dataset JsonList
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Busqueda"></param>
    /// <returns> Devuelve el DATASET en FORMATO JSON LIST</returns>
    [OperationContract]
    public string BuscaPersonas(string SesionSeguridad, string Busqueda)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return CeC_BD.DataSet2JsonV2(CeC_Personas.BuscaPersonas(Busqueda, Sesion));
    }

    [OperationContract]
    public string BuscaPersona(string SesionSeguridad, string Busqueda)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        return CeC_BD.DataSet2JsonV2(CeC_Personas.BuscaPersona(Busqueda, Sesion));
    }

    /// <summary>
    /// Agrega una persona con los campos correspondientes.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="ModeloJson"></param>
    /// <returns></returns>
    [OperationContract]
    public String AgregaPersona(string SesionSeguridad, string ModeloJson)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        try
        {
            return CeC_Personas.ImportaRegistros(ModeloJson, 1, true, Sesion.SUSCRIPCION_ID, Sesion).ToString();
            
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    [OperationContract]
    public int Agrega(string SesionSeguridad, int PersonaLinkID, int TipoPersonaID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        try
        {
            return CeC_Personas.Agrega(PersonaLinkID, TipoPersonaID, Sesion);

        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return -2;
    }

    [OperationContract]
    public bool GuardaValor(string SesionSeguridad, int Persona_ID, string Campo, string Valor)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        try
        {
            return CeC_Personas.GuardaValor(Persona_ID, Campo, Valor, Sesion.SESION_ID);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }

    [OperationContract]
    public String ObtenFoto(string SesionSeguridad, int Persona_ID, DateTime FechaHoraMinima)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        try
        {
            byte[] Foto = CeC_Personas.ObtenFoto(Persona_ID, FechaHoraMinima);
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
    private string ObtenFotoThumbnail(string SesionSeguridad, int Persona_ID, DateTime FechaHoraMinima, int Ancho, int Alto)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            byte[] Foto = CeC_Personas.ObtenFoto(Persona_ID, FechaHoraMinima);
            if (Foto == null)
                return null;

            return JsonConvert.SerializeObject(CeC.Imagen2Thumbnail(Foto, Ancho, Alto));
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Persona_ID"></param>
    /// <param name="Foto">Byte[]Serializado a String con JSon</param>
    /// <returns></returns>
    [OperationContract]
    public bool GuardaFoto(string SesionSeguridad, int Persona_ID, string FotoString)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        try
        {
            byte[] Foto = JsonConvert.DeserializeObject<byte[]>(FotoString);
            return CeC_Personas.AsignaFoto(Persona_ID, Foto, Sesion);

        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }

    [OperationContract]
    public bool GuardaImagen(string SesionSeguridad, int Persona_ID, string ImagenString, int TipoImagenID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        try
        {
            byte[] Firma = JsonConvert.DeserializeObject<byte[]>(ImagenString);
            switch (TipoImagenID)
            {
                case 1:
                    return CeC_Personas.AsignaFoto(Persona_ID, Firma, Sesion);
                    break;
                case 2:
                    return CeC_Personas.AsignaFirma(Persona_ID, Firma, Sesion);
                    break;
            }
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }

    [OperationContract]
    public String ObtenDatos(string SesionSeguridad, int Persona_ID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        try
        {

            eClockBase.Modelos.Personas.Model_Datos Datos = CeC_Personas.ObtenDatosPersonaModelo(Persona_ID);
            return JsonConvert.SerializeObject(Datos);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }

}
