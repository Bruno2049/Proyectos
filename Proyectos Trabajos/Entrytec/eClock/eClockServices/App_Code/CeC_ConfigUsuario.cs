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
/// Descripción breve de CeC_ConfigUsuario
/// </summary>
public class CeC_ConfigUsuario : CeC_Tabla
{
    int m_Config_Usuario_Id = 0;
    [Description("Identificador de configuración para usuario")]
    [DisplayNameAttribute("Config_Usuario_Id")]
    public int Config_Usuario_Id { get { return m_Config_Usuario_Id; } set { m_Config_Usuario_Id = value; } }
    int m_Usuario_Id = 0;
    [Description("Identificador de configuración para usuario")]
    [DisplayNameAttribute("Usuario_Id")]
    public int Usuario_Id { get { return m_Usuario_Id; } set { m_Usuario_Id = value; } }
    string m_Config_Usuario_Variable = "";
    [Description("Identificador de configuración para usuario")]
    [DisplayNameAttribute("Config_Usuario_Variable")]
    public string Config_Usuario_Variable { get { return m_Config_Usuario_Variable; } set { m_Config_Usuario_Variable = value; } }
    string m_Config_Usuario_Valor = "";
    [Description("Identificador de configuración para usuario")]
    [DisplayNameAttribute("Config_Usuario_Valor")]
    public string Config_Usuario_Valor { get { return m_Config_Usuario_Valor; } set { m_Config_Usuario_Valor = value; } }
    byte[] m_Config_Usuario_Valor_Bin = { 0 };
    [Description("Identificador de configuración para usuario")]
    [DisplayNameAttribute("Config_Usuario_Valor_Bin")]
    public byte[] Config_Usuario_Valor_Bin { get { return m_Config_Usuario_Valor_Bin; } set { m_Config_Usuario_Valor_Bin = value; } }
    public CeC_ConfigUsuario(CeC_Sesion Sesion)
        : base("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID", Sesion)
    { }

    public CeC_ConfigUsuario(int Config_Usuario_Id, CeC_Sesion Sesion)
        : base("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID", Sesion)
    {
        Carga(Config_Usuario_Id.ToString(), Sesion);
    }

    public bool Actualiza(int ConfigUsuarioId, int UsuarioId, string ConfigUsuarioVariable, string ConfigUsuarioValor, byte[] ConfigUsuarioValorBin,
    CeC_Sesion Sesion)
    {
        try
        {
            bool Nuevo = false;
            if (!Carga(ConfigUsuarioId.ToString(), Sesion))
                Nuevo = true;
            m_EsNuevo = Nuevo;
            Config_Usuario_Id = ConfigUsuarioId;
            Usuario_Id = UsuarioId;
            Config_Usuario_Variable = ConfigUsuarioVariable;
            Config_Usuario_Valor = ConfigUsuarioValor;
            Config_Usuario_Valor_Bin = ConfigUsuarioValorBin;
            if (Guarda(Sesion))
            {
                return true;
            }
        }
        catch { }
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="UsuarioID"></param>
    /// <returns></returns>
    public static DataSet ObtenConfigDetalle(int UsuarioID)
    {
        string Qry = "SELECT CONFIG_USUARIO_ID, USUARIO_ID, CONFIG_USUARIO_VARIABLE, CONFIG_USUARIO_VALOR, CONFIG_USUARIO_VALOR_BIN FROM EC_CONFIG_USUARIO " +
        "WHERE USUARIO_ID = " + UsuarioID + ";";
        //string Qry = "SELECT     TOP (100) PERCENT EC_USUARIOS.USUARIO_NOMBRE, EC_USUARIOS.USUARIO_USUARIO, EC_PERFILES.PERFIL_NOMBRE, " +
        //              "EC_CONFIG_USUARIO.CONFIG_USUARIO_VARIABLE, EC_CONFIG_USUARIO.CONFIG_USUARIO_VALOR, " +
        //              "EC_CONFIG_USUARIO.CONFIG_USUARIO_VALOR_BIN, EC_USUARIOS.USUARIO_ID, EC_USUARIOS.PERFIL_ID " +
        //              "FROM EC_CONFIG_USUARIO INNER JOIN " +
        //              "EC_USUARIOS ON EC_CONFIG_USUARIO.USUARIO_ID = EC_USUARIOS.USUARIO_ID INNER JOIN " +
        //              "EC_PERFILES ON EC_USUARIOS.PERFIL_ID = EC_PERFILES.PERFIL_ID";
        return (DataSet)CeC_BD.EjecutaDataSet(Qry);
    }
}