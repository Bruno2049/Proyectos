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
/// Descripción breve de CeC_Suscrip_Precios
/// </summary>
public class CeC_Suscrip_Precios : CeC_Tabla
{
    int m_Suscrip_Precio_Id = 0;
    [Description("Identificador del cargo realizado a la suscripcion")]
    [DisplayNameAttribute("Suscrip_Precio_Id")]
    public int Suscrip_Precio_Id { get { return m_Suscrip_Precio_Id; } set { m_Suscrip_Precio_Id = value; } }
    int m_Suscrip_Precio_Desc = 0;
    [Description("Descripcion del precio del tipo de suscripción")]
    [DisplayNameAttribute("Suscrip_Precio_Desc")]
    public int Suscrip_Precio_Desc { get { return m_Suscrip_Precio_Desc; } set { m_Suscrip_Precio_Desc = value; } }
    string m_Suscrip_Precio_Empleados = "";
    [Description("Precio por empleado adicional")]
    [DisplayNameAttribute("Suscrip_Precio_Empleados")]
    public string Suscrip_Precio_Empleados { get { return m_Suscrip_Precio_Empleados; } set { m_Suscrip_Precio_Empleados = value; } }
    string m_Suscrip_Precio_Terminales = "";
    [Description("Precio por terminal adicional")]
    [DisplayNameAttribute("Suscrip_Precio_Terminales")]
    public string Suscrip_Precio_Terminales { get { return m_Suscrip_Precio_Terminales; } set { m_Suscrip_Precio_Terminales = value; } }
    string m_Suscrip_Precio_Usuarios = "";
    [Description("Precio por usuario adicional")]
    [DisplayNameAttribute("Suscrip_Precio_Usuarios")]
    public string Suscrip_Precio_Usuarios { get { return m_Suscrip_Precio_Usuarios; } set { m_Suscrip_Precio_Usuarios = value; } }
    string m_Suscrip_Precio_Alumnos = "";
    [Description("Precio por alumno adicional")]
    [DisplayNameAttribute("Suscrip_Precio_Alumnos")]
    public string Suscrip_Precio_Alumnos { get { return m_Suscrip_Precio_Alumnos; } set { m_Suscrip_Precio_Alumnos = value; } }
    string m_Suscrip_Precio_Visitantes = "";
    [Description("Precio por visitante adicional")]
    [DisplayNameAttribute("Suscrip_Precio_Visitantes")]
    public string Suscrip_Precio_Visitantes { get { return m_Suscrip_Precio_Visitantes; } set { m_Suscrip_Precio_Visitantes = value; } }
    bool m_Suscrip_Precio_Borrados = false;
    [Description("Indica si se contarán los elementos en status borrado")]
    [DisplayNameAttribute("Suscrip_Precio_Borrados")]
    public bool Suscrip_Precio_Borrados { get { return m_Suscrip_Precio_Borrados; } set { m_Suscrip_Precio_Borrados = value; } }
	public CeC_Suscrip_Precios(CeC_Sesion Sesion)
        : base("EC_SUSCRIP_PRECIOS", "SUSCRIP_PRECIO_ID", Sesion)
    {
    }

    public CeC_Suscrip_Precios(int Suscripcion_ID, CeC_Sesion Sesion)
        : base("EC_SUSCRIP_PRECIOS", "SUSCRIP_PRECIO_ID", Sesion)
    {
        Carga(Suscripcion_ID.ToString(), Sesion);
    }

    /// <summary>
    /// Actualiza los datos de Precio de Suscripción. Si no existe la Suscripción, crea una nueva.
    /// </summary>
    /// <param name="SuscripPrecioId">Identificador del cargo realizado a la suscripcion</param>
    /// <param name="SuscripPrecioDesc">Descripcion del precio del tipo de suscripción</param>
    /// <param name="SuscripPrecioEmpleados">Precio por empleado adicional</param>
    /// <param name="SuscripPrecioTerminales">Precio por terminal adicional</param>
    /// <param name="SuscripPrecioUsuarios">Precio por usuario adicional</param>
    /// <param name="SuscripPrecioAlumnos">Precio por alumno adicional</param>
    /// <param name="SuscripPrecioVisitantes">Precio por visitante adicional</param>
    /// <param name="SuscripPrecioBorrados">Indica si se contarán los elementos en status borrado</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>Verdadero si se modifico o se agrego nuevos Datos de Precios de Suscripción. Falso en caso de que no se agregue o modifique.</returns>
    public bool Actualiza(int SuscripPrecioId, int SuscripPrecioDesc, string SuscripPrecioEmpleados, string SuscripPrecioTerminales, string SuscripPrecioUsuarios, string SuscripPrecioAlumnos, string SuscripPrecioVisitantes, bool SuscripPrecioBorrados,
CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(SuscripPrecioId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Suscrip_Precio_Id = SuscripPrecioId; 
            Suscrip_Precio_Desc = SuscripPrecioDesc; 
            Suscrip_Precio_Empleados = SuscripPrecioEmpleados; 
            Suscrip_Precio_Terminales = SuscripPrecioTerminales; 
            Suscrip_Precio_Usuarios = SuscripPrecioUsuarios; 
            Suscrip_Precio_Alumnos = SuscripPrecioAlumnos; 
            Suscrip_Precio_Visitantes = SuscripPrecioVisitantes; 
            Suscrip_Precio_Borrados = SuscripPrecioBorrados;
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
}