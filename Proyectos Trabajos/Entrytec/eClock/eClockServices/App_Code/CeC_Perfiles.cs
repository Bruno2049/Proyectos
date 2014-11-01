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
/// Descripción breve de CeC_Perfiles
/// </summary>
public class CeC_Perfiles : CeC_Tabla
{
    int m_Perfil_Id = 0;
    [Description("Identificador de perfil")]
    [DisplayNameAttribute("Perfil_Id")]
    public int Perfil_Id { get { return m_Perfil_Id; } set { m_Perfil_Id = value; } }
    string m_Perfil_Nombre = "";
    [Description("Nombre del perfil")]
    [DisplayNameAttribute("Perfil_Nombre")]
    public string Perfil_Nombre { get { return m_Perfil_Nombre; } set { m_Perfil_Nombre = value; } }
    bool m_Perfil_Borrado = false;
    [Description("Indica si se ha borrado el registro")]
    [DisplayNameAttribute("Perfil_Borrado")]
    public bool Perfil_Borrado { get { return m_Perfil_Borrado; } set { m_Perfil_Borrado = value; } }

    public CeC_Perfiles(CeC_Sesion Sesion)
        : base("EC_PERFILES", "PERFIL_ID", Sesion)
    { }

    public CeC_Perfiles(int PerfilID, CeC_Sesion Sesion)
        : base("EC_PERFILES", "PERFIL_ID", Sesion)
    {
        Carga(PerfilID.ToString(), Sesion);
    }

    /// <summary>
    /// Función que permite Agregar, Editar o Borrar Perfiles.
    /// </summary>
    /// <param name="PerfilId">Identificador de perfil</param>
    /// <param name="PerfilNombre">Nombre del perfil</param>
    /// <param name="PerfilBorrado">Indica si se ha borrado el registro</param>
    /// <param name="Sesion">Variable de Sesion</param>
    /// <returns>True </returns>
    public bool Actualiza(int PerfilId, string PerfilNombre, bool PerfilBorrado,
        CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(PerfilId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Perfil_Id = PerfilId;
            Perfil_Nombre = PerfilNombre;
            Perfil_Borrado = PerfilBorrado;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Obtiene un listado de perfiles, si la suscripcion del usuario es menor o igual a uno mostrara 
    /// todos los perfiles, si es mayor, solo mostrara los perfiles que se crearon en dicha suscripcion
    /// </summary>
    /// <param name="Sesion"></param>
    /// <returns></returns>
    public static DataSet ObtenPerfilesMenu(CeC_Sesion Sesion, bool MostrarBorrado)
    {
        string QryNoMostrarBorrado = "";
        if (!MostrarBorrado)
            QryNoMostrarBorrado = "PERFIL_BORRADO = 0 ";
        string Qry = "SELECT PERFIL_ID, PERFIL_NOMBRE FROM EC_PERFILES ";
        if (Sesion.SUSCRIPCION_ID <= 1)
            Qry += (!MostrarBorrado ? "WHERE " + QryNoMostrarBorrado : "");
        else
            Qry += "WHERE " + CeC_Autonumerico.ValidaSuscripcion("EC_PERFILES", "PERFIL_ID", Sesion.SUSCRIPCION_ID) + (!MostrarBorrado ? " AND " + QryNoMostrarBorrado : "");
        return CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Obtiene la lista de los Perfiles del Sistema
    /// </summary>
    /// <param name="PerfilBorrado">Si se desea ver los perfiles borrados se estable en true</param>
    /// <returns>DataSet con los datos de los Perfiles</returns>
    public static DataSet ObtenPerfilesDS(bool MostrarBorrados)
    {
        string Qry = " SELECT PERFIL_ID, PERFIL_NOMBRE " +
                     " FROM EC_PERFILES ";
        if (!MostrarBorrados)
        {
            Qry += " WHERE PERFIL_BORRADO = 0 ";
        }
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
}