using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

/// <summary>
/// Descripción breve de CeC_Suscrip_Datos
/// </summary>
public class CeC_Suscrip_Datos : CeC_Tabla
{
    int m_Suscripcion_Id = 0;
    [Description("Identificador único de la Suscripcion")]
    [DisplayNameAttribute("Suscripcion_Id")]
    public int Suscripcion_Id { get { return m_Suscripcion_Id; } set { m_Suscripcion_Id = value; } }
    int m_Suscrip_Precio_Id = 0;
    [Description("Identificador único del precio")][DisplayNameAttribute("Suscrip_Precio_Id")]
    public int Suscrip_Precio_Id{get { return m_Suscrip_Precio_Id; }set { m_Suscrip_Precio_Id = value; }}
    string m_Suscrip_Datos_Razon = "";
    [Description("Contiene la razon social del propietario de la suscripcion")][DisplayNameAttribute("Suscrip_Datos_Razon")]
    public string Suscrip_Datos_Razon{get { return m_Suscrip_Datos_Razon; }set { m_Suscrip_Datos_Razon = value; }}
    string m_Suscrip_Datos_Rfc = "";
    [Description("RFC")][DisplayNameAttribute("Suscrip_Datos_Rfc")]
    public string Suscrip_Datos_Rfc{get { return m_Suscrip_Datos_Rfc; }set { m_Suscrip_Datos_Rfc = value; }}
    string m_Suscrip_Datos_Direccion1 = "";
    [Description("Campo uno de Dirección")][DisplayNameAttribute("Suscrip_Datos_Direccion1")]
    public string Suscrip_Datos_Direccion1{get { return m_Suscrip_Datos_Direccion1; }set { m_Suscrip_Datos_Direccion1 = value; }}
    string m_Suscrip_Datos_Direccion2 = "";
    [Description("Campo dos de Dirección")][DisplayNameAttribute("Suscrip_Datos_Direccion2")]
    public string Suscrip_Datos_Direccion2{get { return m_Suscrip_Datos_Direccion2; }set { m_Suscrip_Datos_Direccion2 = value; }}
    string m_Suscrip_Datos_Ciudad = "";
    [Description("")][DisplayNameAttribute("Suscrip_Datos_Ciudad")]
    public string Suscrip_Datos_Ciudad{get { return m_Suscrip_Datos_Ciudad; }set { m_Suscrip_Datos_Ciudad = value; }}
    string m_Suscrip_Datos_Estado = "";
    [Description("")][DisplayNameAttribute("Suscrip_Datos_Estado")]
    public string Suscrip_Datos_Estado{get { return m_Suscrip_Datos_Estado; }set { m_Suscrip_Datos_Estado = value; }}
    string m_Suscrip_Datos_Pais = "";
    [Description("")][DisplayNameAttribute("Suscrip_Datos_Pais")]
    public string Suscrip_Datos_Pais{get { return m_Suscrip_Datos_Pais; }set { m_Suscrip_Datos_Pais = value; }}
    int m_Edo_Suscripcion_Id = 0;
    [Description("")][DisplayNameAttribute("Edo_Suscripcion_Id")]
    public int Edo_Suscripcion_Id{get { return m_Edo_Suscripcion_Id; }set { m_Edo_Suscripcion_Id = value; }}
    bool m_Suscrip_Datos_Facturar = true;
    [Description("Indica si el cliente requiere Factura")][DisplayNameAttribute("Suscrip_Datos_Facturar")]
    public bool Suscrip_Datos_Facturar{get { return m_Suscrip_Datos_Facturar; }set { m_Suscrip_Datos_Facturar = value; }}
    DateTime m_Suscrip_Datos_Contratacion = CeC_BD.FechaNula;
    [Description("Contiene la fecha de contratación")][DisplayNameAttribute("Suscrip_Datos_Contratacion")]
    public DateTime Suscrip_Datos_Contratacion{get { return m_Suscrip_Datos_Contratacion; }set { m_Suscrip_Datos_Contratacion = value; }}
    int m_Suscrip_Datos_Empleados = 0;
    [Description("Contiene la cantidad de Empleados que se permitiran en esta suscripcion")][DisplayNameAttribute("Suscrip_Datos_Empleados")]
    public int Suscrip_Datos_Empleados{get { return m_Suscrip_Datos_Empleados; }set { m_Suscrip_Datos_Empleados = value; }}
    int m_Suscrip_Datos_Terminales = 0;
    [Description("Contiene la cantidad de Terminales que se permitiran en esta suscripcion")][DisplayNameAttribute("Suscrip_Datos_Terminales")]
    public int Suscrip_Datos_Terminales{get { return m_Suscrip_Datos_Terminales; }set { m_Suscrip_Datos_Terminales = value; }}
    int m_Suscrip_Datos_Usuarios = 0;
    [Description("Contiene la cantidad de Usuarios que se permitiran en esta suscripcion")][DisplayNameAttribute("Suscrip_Datos_Usuarios")]
    public int Suscrip_Datos_Usuarios{get { return m_Suscrip_Datos_Usuarios; }set { m_Suscrip_Datos_Usuarios = value; }}
    int m_Suscrip_Datos_Alumnos = 0;
    [Description("Contiene la cantidad de Alumnos que se permitiran en esta suscripcion")][DisplayNameAttribute("Suscrip_Datos_Alumnos")]
    public int Suscrip_Datos_Alumnos{get { return m_Suscrip_Datos_Alumnos; }set { m_Suscrip_Datos_Alumnos = value; }}
    int m_Suscrip_Datos_Visitantes = 0;
    [Description("Contiene la cantidad de Visitantes que se permitiran en esta suscripcion")][DisplayNameAttribute("Suscrip_Datos_Visitantes")]
    public int Suscrip_Datos_Visitantes{get { return m_Suscrip_Datos_Visitantes; }set { m_Suscrip_Datos_Visitantes = value; }}
    bool m_Suscrip_Datos_Adicionales = false;[Description("Indica si permitirá empleados, terminales, etc adicionales a los autorizados")][DisplayNameAttribute("Suscrip_Datos_Adicionales")]
    public bool Suscrip_Datos_Adicionales{get { return m_Suscrip_Datos_Adicionales; }set { m_Suscrip_Datos_Adicionales = value; }}
    string m_Suscrip_Datos_Otros = "";
    [Description("Contiene datos adicionales a validar")][DisplayNameAttribute("Suscrip_Datos_Otros")]
    public string Suscrip_Datos_Otros{get { return m_Suscrip_Datos_Otros; }set { m_Suscrip_Datos_Otros = value; }}
    int m_Suscrip_Datos_Mensual = 0;
    [Description("Mensualidad de pago por la suscripcion")][DisplayNameAttribute("Suscrip_Datos_Mensual")]
    public int Suscrip_Datos_Mensual{get { return m_Suscrip_Datos_Mensual; }set { m_Suscrip_Datos_Mensual = value; }}
    DateTime m_Suscrip_Datos_Final = CeC_BD.FechaNula;
    [Description("Fecha en la que finalizará el contrato")][DisplayNameAttribute("Suscrip_Datos_Final")]
    public DateTime Suscrip_Datos_Final{get { return m_Suscrip_Datos_Final; }set { m_Suscrip_Datos_Final = value; }}

    public CeC_Suscrip_Datos(CeC_Sesion Sesion)
        : base("EC_SUSCRIP_DATOS", "SUSCRIPCION_ID", Sesion)
    {
    }

    public CeC_Suscrip_Datos(int Suscrip_ID, CeC_Sesion Sesion)
        : base("EC_SUSCRIP_DATOS", "SUSCRIPCION_ID", Sesion)
    {
        Carga(Suscrip_ID.ToString(), Sesion);
    }

    //public CeC_Suscripcion(int Usuarioid, CeC_Sesion Sesion)
    //    : base("EC_SUSCRIP_DATOS", "SUSCRIPCION_ID", Sesion)
    //{
    //    Usuario_ID = Usuarioid;
    //    Configuracion = new CeC_Config(Usuario_ID);
    //}

    /// <summary>
    /// Actualiza o modifica los datos descriptivos de una Suscripción. Si no existe, crea una nueva.
    /// </summary>
    /// <param name="SuscripcionId">Identificador único de la Suscripcion</param>
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
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>Verdadero si se modifico o agrego una nueva suscripción. Falso en caso de que no se pudo agregar o modificar los datos de una Suscripcion</returns>
    public bool Actualiza(int SuscripcionId, int SuscripPrecioId, string SuscripDatosRazon, string SuscripDatosRfc, string SuscripDatosDireccion1, string SuscripDatosDireccion2, string SuscripDatosCiudad, string SuscripDatosEstado, string SuscripDatosPais, int EdoSuscripcionId, bool SuscripDatosFacturar, DateTime SuscripDatosContratacion, int SuscripDatosEmpleados, int SuscripDatosTerminales, int SuscripDatosUsuarios, int SuscripDatosAlumnos, int SuscripDatosVisitantes, bool SuscripDatosAdicionales, string SuscripDatosOtros, int SuscripDatosMensual, DateTime SuscripDatosFinal,
CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(SuscripcionId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Suscripcion_Id = SuscripcionId; 
            Suscrip_Precio_Id = SuscripPrecioId; 
            Suscrip_Datos_Razon = SuscripDatosRazon; 
            Suscrip_Datos_Rfc = SuscripDatosRfc; 
            Suscrip_Datos_Direccion1 = SuscripDatosDireccion1; 
            Suscrip_Datos_Direccion2 = SuscripDatosDireccion2; 
            Suscrip_Datos_Ciudad = SuscripDatosCiudad; 
            Suscrip_Datos_Estado = SuscripDatosEstado; 
            Suscrip_Datos_Pais = SuscripDatosPais; 
            Edo_Suscripcion_Id = EdoSuscripcionId; 
            Suscrip_Datos_Facturar = SuscripDatosFacturar; 
            Suscrip_Datos_Contratacion = SuscripDatosContratacion; 
            Suscrip_Datos_Empleados = SuscripDatosEmpleados; 
            Suscrip_Datos_Terminales = SuscripDatosTerminales; 
            Suscrip_Datos_Usuarios = SuscripDatosUsuarios; 
            Suscrip_Datos_Alumnos = SuscripDatosAlumnos; 
            Suscrip_Datos_Visitantes = SuscripDatosVisitantes; 
            Suscrip_Datos_Adicionales = SuscripDatosAdicionales; 
            Suscrip_Datos_Otros = SuscripDatosOtros; 
            Suscrip_Datos_Mensual = SuscripDatosMensual; 
            Suscrip_Datos_Final = SuscripDatosFinal;

            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch(Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return false;
    }
    /// <summary>
    /// Obtiene el identificador de la Suscripción
    /// </summary>
    /// <param name="SuscripcionNombreORazonORFC">Nombre de la Suscripcion o la Razon Social o el RFC</param>
    /// <returns>SuscripcionID. Devuelve 0 si no se encontro la Suscripción.</returns>
    public static int ObtenSuscripcionID(string SuscripcionNombreORazonORFC)
    {
        try
        {
            SuscripcionNombreORazonORFC = SuscripcionNombreORazonORFC.ToLower();
            return CeC_BD.EjecutaEscalarInt("SELECT SUSCRIPCION_ID FROM EC_SUSCRIP_DATOS WHERE LOWER(SUSCRIP_DATOS_RAZON) = '"
                + CeC_BD.ObtenParametroCadena(SuscripcionNombreORazonORFC)
                + "' OR LOWER(SUSCRIP_DATOS_RFC) = '" + CeC_BD.ObtenParametroCadena(SuscripcionNombreORazonORFC) + "'");
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }
        return 0;
    }
}