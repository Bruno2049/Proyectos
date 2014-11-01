using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class S_Suscripciones
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
    /// Agrega una nueva Suscripcion
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="SuscripcionNombre">Nombre de la Suscripción</param>
    /// <param name="SuscripPrecioId">Identificador único del precio</param>
    /// <param name="SuscripDatosRazon">Contiene la razon social del propietario de la suscripcion</param>
    /// <param name="SuscripDatosRfc">RFC</param>
    /// <param name="SuscripDatosDireccion1">Campo uno de Dirección</param>
    /// <param name="SuscripDatosDireccion2">Campo dos de Dirección</param>
    /// <param name="SuscripDatosCiudad"></param>
    /// <param name="SuscripDatosEstado"></param>
    /// <param name="SuscripDatosPais"></param>
    /// <param name="EdoSuscripcionId"></param>
    /// <param name="SuscripDatosFacturar">Indica si el cliente requiere Factura</param>
    /// <param name="SuscripDatosContratacion">Contiene la fecha de contratación</param>
    /// <param name="SuscripDatosEmpleados">Contiene la cantidad de Empleados que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosTerminales">Contiene la cantidad de Terminales que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosUsuarios">Contiene la cantidad de Usuarios que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosAlumnos">Contiene la cantidad de Alumnos que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosVisitantes">Contiene la cantidad de Visitantes que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosAdicionales">Indica si permitirá empleados, terminales, etc adicionales a los autorizados</param>
    /// <param name="SuscripDatosOtros">Contiene datos adicionales a validar</param>
    /// <param name="SuscripDatosMensual">Mensualidad de pago por la suscripcion</param>
    /// <param name="SuscripDatosFinal">Fecha en la que finalizará el contrato</param>
    /// <returns>Verdadero si se agrego con éxito la nueva Suscripción</returns>
    [OperationContract]
    public bool AgregaSuscripcion(string SesionSeguridad, string SuscripcionNombre,bool SuscripcionBorrado,int SuscripPrecioId,string SuscripcionRazon,string SuscripcionRfc,string SuscripcionDireccion1,string SuscripcionDireccion2,string SuscripcionCiudad,string SuscripcionEstado,string SuscripcionPais,int EdoSuscripcionId,bool SuscripcionFacturar,DateTime SuscripcionContratacion,int SuscripcionEmpleados,int SuscripcionTerminales,int SuscripcionUsuarios,int SuscripcionAlumnos,int SuscripcionVisitantes,bool SuscripcionAdicionales,string SuscripcionOtros,int SuscripcionMensual,DateTime SuscripDatosFinal)
    {
        try
        {
            int SuscripcionId = -999999;
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return false;
            if (!CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, "S.Suscripciones.Alta"))
                return false;
            CeC_Suscripcion l_Suscripcion = new CeC_Suscripcion(Sesion);
            return l_Suscripcion.Actualiza(SuscripcionId,
                                    EdoSuscripcionId,
                                    SuscripPrecioId,
                                    SuscripcionNombre,
                                    false,
                                    SuscripcionRazon,
                                    SuscripcionRfc,
                                    SuscripcionDireccion1,
                                    SuscripcionDireccion2,
                                    SuscripcionCiudad,
                                    SuscripcionEstado,
                                    SuscripcionPais,
                                    SuscripcionFacturar,
                                    SuscripcionContratacion,
                                    SuscripcionEmpleados,
                                    SuscripcionTerminales,
                                    SuscripcionUsuarios,
                                    SuscripcionAlumnos,
                                    SuscripcionVisitantes,
                                    SuscripcionAdicionales,
                                    SuscripcionOtros,
                                    SuscripcionMensual,
                                    SuscripDatosFinal,
                                        Sesion);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    /// <summary>
    /// Permite modificar los datos de una Suscripcion.
    /// </summary>
    /// <param name="SesionSeguridad"></param>
    /// <param name="SuscripcionNombre">Nombre de la Suscripción</param>
    /// <param name="SuscripPrecioId">Identificador único del precio</param>
    /// <param name="SuscripDatosRazon">Contiene la razon social del propietario de la suscripcion</param>
    /// <param name="SuscripDatosRfc">RFC</param>
    /// <param name="SuscripDatosDireccion1">Campo uno de Dirección</param>
    /// <param name="SuscripDatosDireccion2">Campo dos de Dirección</param>
    /// <param name="SuscripDatosCiudad"></param>
    /// <param name="SuscripDatosEstado"></param>
    /// <param name="SuscripDatosPais"></param>
    /// <param name="EdoSuscripcionId"></param>
    /// <param name="SuscripDatosFacturar">Indica si el cliente requiere Factura</param>
    /// <param name="SuscripDatosContratacion">Contiene la fecha de contratación</param>
    /// <param name="SuscripDatosEmpleados">Contiene la cantidad de Empleados que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosTerminales">Contiene la cantidad de Terminales que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosUsuarios">Contiene la cantidad de Usuarios que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosAlumnos">Contiene la cantidad de Alumnos que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosVisitantes">Contiene la cantidad de Visitantes que se permitiran en esta suscripcion</param>
    /// <param name="SuscripDatosAdicionales">Indica si permitirá empleados, terminales, etc adicionales a los autorizados</param>
    /// <param name="SuscripDatosOtros">Contiene datos adicionales a validar</param>
    /// <param name="SuscripDatosMensual">Mensualidad de pago por la suscripcion</param>
    /// <param name="SuscripDatosFinal">Fecha en la que finalizará el contrato</param>
    /// <returns>Verdadero si se modifico con éxito la nueva Suscripción</returns>
    [OperationContract]
    public bool ModificaSuscripcion(string SesionSeguridad, string SuscripcionNombre, bool SuscripcionBorrado, int SuscripPrecioId, string SuscripcionRazon, string SuscripcionRfc, string SuscripcionDireccion1, string SuscripcionDireccion2, string SuscripcionCiudad, string SuscripcionEstado, string SuscripcionPais, int EdoSuscripcionId, bool SuscripcionFacturar, DateTime SuscripcionContratacion, int SuscripcionEmpleados, int SuscripcionTerminales, int SuscripcionUsuarios, int SuscripcionAlumnos, int SuscripcionVisitantes, bool SuscripcionAdicionales, string SuscripcionOtros, int SuscripcionMensual, DateTime SuscripDatosFinal)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            int Suscripcion_Id = 0;
            if (Sesion == null)
                return false;
            Suscripcion_Id = CeC_Suscripcion.ObtenSuscripcionID(SuscripcionNombre);
            CeC_Suscripcion l_Suscripcion = new CeC_Suscripcion(Suscripcion_Id, Sesion);
            l_Suscripcion.Actualiza(l_Suscripcion.Suscripcion_Id,
                                    EdoSuscripcionId,
                                    SuscripPrecioId,
                                    SuscripcionNombre,
                                    SuscripcionBorrado,
                                    SuscripcionRazon,
                                    SuscripcionRfc,
                                    SuscripcionDireccion1,
                                    SuscripcionDireccion2,
                                    SuscripcionCiudad,
                                    SuscripcionEstado,
                                    SuscripcionPais,
                                    SuscripcionFacturar,
                                    SuscripcionContratacion,
                                    SuscripcionEmpleados,
                                    SuscripcionTerminales,
                                    SuscripcionUsuarios,
                                    SuscripcionAlumnos,
                                    SuscripcionVisitantes,
                                    SuscripcionAdicionales,
                                    SuscripcionOtros,
                                    SuscripcionMensual,
                                    SuscripDatosFinal,
                                    Sesion);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    [OperationContract]
    public bool BorraSuscripcion(string SesionSeguridad, int SuscripcionId)
    {
        try
        {
            CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
            if (Sesion == null)
                return false;
            CeC_Suscripcion l_Suscripcion = new CeC_Suscripcion(SuscripcionId, Sesion);
            return l_Suscripcion.Actualiza(l_Suscripcion.Suscripcion_Id,
                                    l_Suscripcion.Edo_Suscripcion_Id,
                                    l_Suscripcion.Suscrip_Precio_Id,
                                    l_Suscripcion.Suscripcion_Nombre,
                                    true,
                                    l_Suscripcion.Suscripcion_Razon,
                                    l_Suscripcion.Suscripcion_Rfc,
                                    l_Suscripcion.Suscripcion_Direccion1,
                                    l_Suscripcion.Suscripcion_Direccion2,
                                    l_Suscripcion.Suscripcion_Ciudad,
                                    l_Suscripcion.Suscripcion_Estado,
                                    l_Suscripcion.Suscripcion_Pais,
                                    l_Suscripcion.Suscripcion_Facturar,
                                    l_Suscripcion.Suscripcion_Contratacion,
                                    l_Suscripcion.Suscripcion_Empleados,
                                    l_Suscripcion.Suscripcion_Terminales,
                                    l_Suscripcion.Suscripcion_Usuarios,
                                    l_Suscripcion.Suscripcion_Alumnos,
                                    l_Suscripcion.Suscripcion_Visitantes,
                                    l_Suscripcion.Suscripcion_Adicionales,
                                    l_Suscripcion.Suscripcion_Otros,
                                    l_Suscripcion.Suscripcion_Mensual,
                                    l_Suscripcion.Suscripcion_Final,
                                        Sesion);
        }
        catch(Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    //[OperationContract]
    //public DataSet Listar(string SesionSeguridad, bool MostrarBorrados)
    //{
    //    try
    //    {
    //        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
    //        if (Sesion == null)
    //            return null;
    //        return CeC_Suscripcion.Listar(MostrarBorrados);
    //    }
    //    catch (Exception ex)
    //    {
    //        CIsLog2.AgregaError(ex);
    //    }
    //    return null;
    //}
    [OperationContract]
    public int CreaSuscripcion(string SesionSeguridad, string SuscripcionNombre, string SuscripcionContacto, string SuscripcionContactoEmail)
    {
        CeC_Sesion Sesion = CeC_Sesion.Carga(SesionSeguridad);
        if (Sesion == null)
            return -9999;
        if(!CeC_Restricciones.TieneDerecho(Sesion.PERFIL_ID, "S.Suscripciones.Alta"))
            return -9998;
        return CeC_Suscripcion.CreaSuscripcion(Sesion, SuscripcionNombre, SuscripcionContacto, SuscripcionContactoEmail);
    }

    [OperationContract]
    public int ObtenSuscripcionID(string Suscripcion)
    {
        return CeC_Suscripcion.ObtenSuscripcionID(Suscripcion);
    }

    [OperationContract]
    public string ObtenSuscripcionUrl(string Suscripcion)
    {
        return CeC_Suscripcion.ObtenSuscripcionURL(Suscripcion);
    }
	// Agregue aquí más operaciones y márquelas con [OperationContract]
}
