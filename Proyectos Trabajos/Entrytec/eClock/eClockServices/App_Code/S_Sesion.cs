using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Sesion
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
    /// <summary>
    /// Crea una sesión validando el usuario y contraseña
    /// </summary>
    /// <param name="Usuario"></param>
    /// <param name="Clave"></param>
    /// <returns>Vacio si no fu correcto de lo contrario la sesion seguridad</returns>
    [OperationContract]
    //[WebGet]
    public string CreaSesion(string Usuario, string Clave)
    {
        if (Usuario.Length < 1 || Clave.Length < 1)
            return "";
        int UsuarioID = CeC_Sesion.ValidarUsuario(Usuario, Clave);
        if (UsuarioID <= 0)
            return "";
        int SesionID = CeC_Sesion.CreaSesionID(UsuarioID);
        if (SesionID > 0)
            return CeC_Sesion.ObtenSeguridadSesionID(SesionID);
        return "";
    }

    /// <summary>
    /// Crea una nueva sesion validando usuario y contraseña 
    /// </summary>
    /// <param name="Usuario"></param>
    /// <param name="Clave"></param>
    /// <returns>Regresa un modelo de datos </returns>
    [OperationContract]
    public string CreaSesionAdv(string Usuario, string Clave)
    {
        return CreaSesionAdvSuscripcion(Usuario, Clave, "");
    }

    [OperationContract]
    public string CreaSesionAdvSuscripcion(string Usuario, string Clave, string Suscripcion)
    {
        try
        {
            if (Usuario.Length < 1 || Clave.Length < 1)
                return "";
            int UsuarioID = CeC_Sesion.ValidarUsuario(Usuario, Clave, Suscripcion);
            if (UsuarioID <= 0)
                return "";
            int SesionID = CeC_Sesion.CreaSesionID(UsuarioID);
            string SesionSeguridad = "";
            if (SesionID > 0)
            {
                SesionSeguridad = CeC_Sesion.ObtenSeguridadSesionID(SesionID);
                CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
                if (Sesion != null)
                {
                    eClockBase.Modelos.Model_USUARIOS UsuarioMdl = new eClockBase.Modelos.Model_USUARIOS();
                    UsuarioMdl.USUARIO_ID = UsuarioID;
                    string SesionAdv = CeC_Tabla.ObtenDatos("EC_USUARIOS", "USUARIO_ID", JsonConvert.SerializeObject(UsuarioMdl), Sesion);
                    eClockBase.Modelos.Sesion.Model_Sesion MSesion = JsonConvert.DeserializeObject<eClockBase.Modelos.Sesion.Model_Sesion>(SesionAdv);
                    MSesion.SESION_SEGURIDAD = SesionSeguridad;
                    return JsonConvert.SerializeObject(MSesion);
                }
            }
        }
        catch { }
        return "";
    }

    [OperationContract]
    public string ObtenSesionDatos(string SesionSeguridad, string Clave)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion != null)
            {
                eClockBase.Modelos.Model_USUARIOS UsuarioMdl = new eClockBase.Modelos.Model_USUARIOS();
                UsuarioMdl.USUARIO_ID = ObtenUsuarioID(SesionSeguridad);
                string SesionAdv = CeC_Tabla.ObtenDatos("EC_USUARIOS", "USUARIO_ID", JsonConvert.SerializeObject(UsuarioMdl), Sesion);
                eClockBase.Modelos.Sesion.Model_Sesion MSesion = JsonConvert.DeserializeObject<eClockBase.Modelos.Sesion.Model_Sesion>(SesionAdv);
                MSesion.SESION_SEGURIDAD = SesionSeguridad;
                return JsonConvert.SerializeObject(MSesion);
            }
        }
        catch { }
        return "";
    }


    [OperationContract]
    // [WebGet]
    public int ObtenUsuarioID(string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -9999;
        return Sesion.USUARIO_ID;
    }

    [OperationContract]
    // [WebGet]
    public int ObtenSuscripcionID(string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -9999;
        return Sesion.SUSCRIPCION_ID;
    }

    [OperationContract]
    //  [WebGet]
    public int ObtenPerfilID(string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -9999;
        return Sesion.PERFIL_ID;
    }
    [OperationContract]
    // [WebGet]
    public bool CierraSesion(string SesionSeguridad)
    {
        return CeC_Sesion.CierraSesion(SesionSeguridad);
    }
    /*  [OperationContract]
      public DataSet ObtenListado(string SesionSeguridad, int SuscripcionID, string NombreTabla, string CampoLlave, string CampoNombre,
          string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string OtroFiltro)
      {
          CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
          if (Sesion == null)
              return null;
          if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
              return null;
          return CeC_Tablas.ObtenListado(SuscripcionID, NombreTabla, CampoLlave, CampoNombre,
           CampoDescripcion, CampoImagen, MostrarBorrados, OtroFiltro);
      }*/
    /// <summary>
    /// Obtiene el listado en formato Json
    /// </summary>
    /// <param name="SesionSeguridad">Firma para comprobar que estes Logeado</param>
    /// <param name="SuscripcionID">Id de la Suscripcion Actual</param>
    /// <param name="NombreTabla">Nombre de la tabla a consultar</param>
    /// <param name="CampoLlave">Nombre del(los) campo(s) llave de la tabla</param>
    /// <param name="CampoNombre">Campo Nombre de la table</param>
    /// <param name="CampoAdicional">CAmpo Adicional</param>
    /// <param name="CampoDescripcion">Campo de la descripcion</param>
    /// <param name="CampoImagen">Campo de la imagen</param>
    /// <param name="MostrarBorrados">Indica si seran mostrados los campos borrados</param>
    /// <param name="OtroFiltro">Si es necesario indicar otro filtro</param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenListado(string SesionSeguridad, int SuscripcionID, string NombreTabla, string CampoLlave, string CampoNombre, string CampoAdicional,
        string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string OtroFiltro, string Or, string Hash)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return null;
        string JSon = CeC_Tablas.ObtenListadoJson(SuscripcionID, NombreTabla, CampoLlave, CampoNombre, CampoAdicional,
         CampoDescripcion, CampoImagen, MostrarBorrados, OtroFiltro, Or);
        if (Hash != null && Hash != "" && JSon != null && JSon != "")
            if (MD5Core.GetHashString(JSon) == Hash)
                return "==";
        return eClockBase.Controladores.CeC_ZLib.Json2ZJson(JSon);
        return JSon;
    }

    [OperationContract]
    public string ObtenListadoCatalogo(string SesionSeguridad, int SuscripcionID, string CampoLlave, string Hash)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return null;
        string JSon = CeC_Tablas.ObtenListadoJson(SuscripcionID, CampoLlave);
        if (Hash != null && Hash != "" && JSon != null && JSon != "")
            if (MD5Core.GetHashString(JSon) == Hash)
                return "==";
        return JSon;
    }

    /* [OperationContract]
     public string ObtenDatos(string SesionSeguridad, string Tabla, string Llaves, string Modelo)
     {
         CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
         if (Sesion == null)
             return null;
         System.IO.MemoryStream MS2 = new System.IO.MemoryStream(CeC.ObtenArregloBytes(Modelo));
         var SF2 = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
         object Obj = SF2.Deserialize(MS2);

         object Resultado = CeC_Tabla.ObtenDatos(Tabla, Llaves, Obj, Sesion);
         MS2 = new System.IO.MemoryStream();
         SF2.Serialize(MS2,Resultado);
         return CeC.ObtenString(MS2.GetBuffer());
     }*/


    [OperationContract]
    public string ObtenDatos(string SesionSeguridad, string Tabla, string Llaves, string Modelo, string CamposOrden, string OtroFiltro)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;

        return CeC_Tabla.ObtenDatos(Tabla, Llaves, Modelo, Sesion, CamposOrden, OtroFiltro);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Tabla"></param>
    /// <param name="Llaves"></param>
    /// <param name="Modelo"></param>
    /// <param name="SuscripcionID"></param>
    /// <param name="EsNuevo"></param>
    /// <returns>Negativo si existio un error, positivo el ID generadoo 1 si es edicion</returns>
    [OperationContract]
    public int GuardaDatos(string SesionSeguridad, string Tabla, string Llaves, string Modelo, int SuscripcionID, bool EsNuevo)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return -2;
        
        return CeC_Tabla.GuardaDatos(Tabla, Llaves, Modelo, EsNuevo, Sesion, SuscripcionID);

    }

    [OperationContract]
    public int GuardaDatos1aN(string SesionSeguridad, string Tabla, string CampoLlaveUno, string ValorLlaveUno, string CampoLlaveN, string Activos, int SuscripcionID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return -2;
        string CampoBorrado = CeC_Tablas.Obten_TablaCBorrado(Tabla);
        return CeC_Tabla.GuardaDatos1aN(Tabla, CampoLlaveUno, ValorLlaveUno, CampoLlaveN, Activos, Sesion, SuscripcionID);
    }

    [OperationContract]
    public int BorrarDatos(string SesionSeguridad, string Tabla, string Llaves, string Modelo)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -1;
        /*   if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
               return -2;*/
        return CeC_Tabla.BorrarDatos(Tabla, Llaves, Modelo, Sesion);
    }

    [OperationContract]
    public string ObtenDatosPersona(string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        try
        {
            eClockBase.Modelos.Sesion.Model_DatosPersona Datos;
            S_Personas Servicio = new S_Personas();

            Datos = JsonConvert.DeserializeObject<eClockBase.Modelos.Sesion.Model_DatosPersona>(
                Servicio.ObtenDatos(SesionSeguridad, Sesion.PERSONA_ID));
            Datos.UltimaSesion = Sesion.ObtenUltimaSesion();
            return JsonConvert.SerializeObject(Datos);
        }
        catch { }
        return null;

    }

    /// <summary>
    /// Obtiene el total de registros modificados o creados de determinadas tablas
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="Tablas">tablas separadas por coma</param>
    /// <param name="Add">Texto Adicional, poner nada</param>
    /// <returns></returns>
    [OperationContract]
    public string ObtenNoCambios(string SesionSeguridad, string Tablas, string Add)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;

        return CeC_Tablas.ObtenNoCambiosTablas(Tablas, Add, Sesion);

    }

    [OperationContract]
    public bool GuardaConsulta(string SesionSeguridad, string TablaNombre, string Add)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;

        return CeC_Tablas.GuardaConsulta(TablaNombre, Add, Sesion);

    }

    [OperationContract]
    public string ObtenImagen(string SesionSeguridad, string Tabla, string CampoLlave, int Llave, string CampoBinario, DateTime FechaHoraMinima)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;

            byte[] Foto = CeC_BD.ObtenBinario(Tabla, CampoLlave, Llave, CampoBinario, FechaHoraMinima, Sesion);
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
    private string ObtenImagenThumbnail(string SesionSeguridad, string Tabla, string CampoLlave, int Llave, string CampoBinario, DateTime FechaHoraMinima, int Ancho, int Alto)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return null;
            byte[] Imagen = CeC_BD.ObtenBinario(Tabla, CampoLlave, Llave, CampoBinario, FechaHoraMinima, Sesion);
            if (Imagen == null)
                return null;
            if (eClockBase.CeC.EsImagenIgual(Imagen))
                return JsonConvert.SerializeObject(eClockBase.CeC.ImagenIgual);
            return JsonConvert.SerializeObject(CeC.Imagen2Thumbnail(Imagen, Ancho, Alto));
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return "ERROR";
    }


    [OperationContract]
    public string ObtenConfig(string SesionSeguridad, string Variable, int Tipo0Sistema1Suscripcion2Usuario)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        switch (Tipo0Sistema1Suscripcion2Usuario)
        {
            case 0:
                return CeC_Config.ObtenConfig(0, Variable, "");
            case 1:
                return CeC_Config.ObtenConfig(Sesion.USUARIO_ID_SUSCRIPCION, Variable, "");
        }
        return CeC_Config.ObtenConfig(Sesion.USUARIO_ID, Variable, "");
    }

    [OperationContract]
    public bool GuardaConfig(string SesionSeguridad, string Variable, string Valor, int Tipo0Sistema1Suscripcion2Usuario)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        switch (Tipo0Sistema1Suscripcion2Usuario)
        {
            case 0:
                if (!CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, "S.ConfiguracionGral"))
                    return false;
                return CeC_Config.GuardaConfig(0, Variable, Valor);
            case 1:
                if (!CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, "S.ConfiguracionSuscripcion"))
                    return false;

                return CeC_Config.GuardaConfig(Sesion.USUARIO_ID_SUSCRIPCION, Variable, Valor);
        }
        if (!CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, "S.ConfiguracionUsuario"))
            return false;
        return CeC_Config.GuardaConfig(Sesion.USUARIO_ID, Variable, Valor);
    }

    [OperationContract]
    public string Importar(string SesionSeguridad, string Modelo, string Valor, int SuscripcionID)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return null;
        if (SuscripcionID != Sesion.SUSCRIPCION_ID && Sesion.SUSCRIPCION_ID > 1)
            return "ERROR_SUSCRIPCION";
        switch (Modelo)
        {
            case "eClockBase.Modelos.Nomina.Model_RecNominasImportar":
                {
                    eClockBase.Modelos.Nomina.Model_RecNominasImportar Imp = JsonConvert.DeserializeObject<eClockBase.Modelos.Nomina.Model_RecNominasImportar>(Valor);
                    if (Imp != null)
                        return JsonConvert.SerializeObject(CeC_RecNominas.Importar(Imp, Sesion, SuscripcionID));
                }
                break;
        }

        return "";
    }

    [OperationContract]
    public bool Inicia_eClock5(string SesionSeguridad)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return false;
        CMd_Base.gActualizaEmpleados(Sesion.SUSCRIPCION_ID, false);
        //CMd_Base.gActualizaTurnos(Sesion.SUSCRIPCION_ID);
        CMd_Base.gActualizaTiposIncidencias(Sesion.SUSCRIPCION_ID);
        CMd_Base.gRecibeIncidencias(DateTime.Now.Date, DateTime.Now.Date, "");
        return true;
    }
}
