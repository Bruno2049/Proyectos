using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Descripción breve de CeC_SesionBD
/// </summary>
public class CeC_SesionBD
{

    private int m_Sesion_ID = 0;
	public CeC_SesionBD(int Sesion_ID)
	{
        m_Sesion_ID = Sesion_ID;
	}
 /// <summary>
 /// Obtiene o establece el valor de una sesion en la base de datos
 /// </summary>
    public DS_WSPersonasTerminales DS_EmpleadosTerminal
    {
        get {
            byte[] Valor = ObtenSesionBin("DS_EmpleadosTerminal", null);
            DS_WSPersonasTerminales ValorDS = new DS_WSPersonasTerminales();
            System.IO.MemoryStream MS = new System.IO.MemoryStream(Valor);
            ValorDS.ReadXml(MS);
            return ValorDS;
//            return (DS_WSPersonasTerminales)ObtenSesionDS("DS_EmpleadosTerminal"); 
        }
        set { GuardaSesionDS("DS_EmpleadosTerminal", value); }
    }
    /// <summary>
    /// Obtiene o establece una FotoNueva
    /// </summary>
    public byte[] FotoNueva
    {
        get { return ObtenSesionBin("FotoNueva", null); }
        set {GuardaSesionBin("FotoNueva", value); }        
    }
    public string ActualizoFoto
    {
        get { return ObtenSesion("ActualizoFoto", null); }
        set { GuardaSesion("ActualizoFoto", value); }     
    }
     
    /// <summary>
    /// Guarda una variable de sesión en la base de datos del sistema
    /// </summary>
    private bool GuardaSesion(string Variable, string Valor)
    {
        try
        {
            //Se reemplaza comilla simple por comilla doble para no tener problemas con el insert y update
            Valor = Valor.Replace("'", "''");
            int Registros = CeC_BD.EjecutaComando("UPDATE EC_SESIONES_VAR SET SESION_VAR_VALOR = '" + Valor + "' WHERE SESION_ID = " + m_Sesion_ID.ToString() + " AND SESION_VAR_VARIABLE = '" + Variable + "'");
            if (Registros <= 0)
                Registros = CeC_BD.EjecutaComando("INSERT INTO EC_SESIONES_VAR (SESION_VAR_ID, SESION_ID, SESION_VAR_VARIABLE, SESION_VAR_VALOR) VALUES( " +
                    CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES_VAR", "SESION_VAR_ID").ToString() + ", " + m_Sesion_ID.ToString() + " , '" + Variable + "', '" + Valor + "')");
            if (Registros > 0)
                return true;
        }
        catch
        {
        }
        return false;
    }

    /// <summary>
    /// Guarda una variable de sesión en la base de datos del sistema
    /// </summary>
    private bool GuardaSesionDS(string Variable, DataSet Valor)
    {
        try
        {
            System.IO.StringWriter SW = new System.IO.StringWriter();
            /*
            Valor.WriteXml(SW, XmlWriteMode.WriteSchema);
            Valor.WriteXml(SW, XmlWriteMode.IgnoreSchema);
            Valor.WriteXml(SW, XmlWriteMode.DiffGram);*/

            System.IO.MemoryStream MS = new System.IO.MemoryStream();
            Valor.WriteXml(MS, XmlWriteMode.WriteSchema);
            GuardaSesionBin(Variable, MS.GetBuffer());
        }
        catch
        {
        }
        return false;
    }

    /// <summary>
    /// Guarda una variable de sesión en la base de datos del sistema
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    private bool GuardaSesionBin(string Variable, byte[] Valor)
    {
        int SESION_VAR_ID = CeC_BD.EjecutaEscalarInt("SELECT SESION_VAR_ID FROM EC_SESIONES_VAR WHERE SESION_ID = " +
            m_Sesion_ID.ToString() + " AND SESION_VAR_VARIABLE = '" + Variable + "'");
        if (SESION_VAR_ID < 0)
        {
            SESION_VAR_ID = CeC_Autonumerico.GeneraAutonumerico("EC_SESIONES_VAR", "SESION_VAR_ID");
            CeC_BD.EjecutaComando("INSERT INTO EC_SESIONES_VAR (SESION_VAR_ID, SESION_ID, SESION_VAR_VARIABLE) VALUES(" + SESION_VAR_ID.ToString() + "," + m_Sesion_ID.ToString() + ",'" + Variable + "')");
        }
        return CeC_BD.AsignaBinario("EC_SESIONES_VAR", "SESION_VAR_ID", SESION_VAR_ID, "SESION_VAR_BIN", Valor);
    }

    /// <summary>
    /// Obtiene una variable de sesión almacenada en la base de datos 
    /// </summary>
    private string ObtenSesion(string Variable, string ValorDefecto)
    {
        try
        {
            object Valor = CeC_BD.EjecutaEscalar("SELECT SESION_VAR_VALOR FROM EC_SESIONES_VAR WHERE SESION_VAR_VARIABLE = '" + Variable + "' AND SESION_ID = " + m_Sesion_ID.ToString());
            if (Valor == null)
                return ValorDefecto;
            return Convert.ToString(Valor);
        }
        catch (Exception ex)
        {

            return ValorDefecto;
        }
    }

    /// <summary>
    /// Obtiene una variable de sesión almacenada en Dataset
    /// </summary>
    private DataSet ObtenSesionDS(string Variable)
    {
        try
        {
            byte[] Valor = ObtenSesionBin(Variable, null);
            DataSet ValorDS = new DataSet();
            System.IO.MemoryStream MS = new System.IO.MemoryStream(Valor);
            ValorDS.ReadXml(MS);
            return ValorDS;
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// Obtiene una variable de sesión binaria almacenada en la base de datos 
    /// </summary>
    /// <param name="Variable"></param>
    /// <returns></returns>
    private byte[] ObtenSesionBin(string Variable, byte[] ValorDefecto)
    {
        int SESION_VAR_ID = CeC_BD.EjecutaEscalarInt("SELECT SESION_VAR_ID FROM EC_SESIONES_VAR WHERE SESION_ID = " +
            m_Sesion_ID.ToString() + " AND SESION_VAR_VARIABLE = '" + Variable + "'");
        if (SESION_VAR_ID < 0)
        {
            return ValorDefecto;
        }
        return CeC_BD.ObtenBinario("EC_SESIONES_VAR", "SESION_VAR_ID", SESION_VAR_ID, "SESION_VAR_BIN");
    }

    public DataSet UltimoReporteDS
    {
        get { return ObtenSesionDS("UltimoReporteDS"); }
        set { GuardaSesionDS("UltimoReporteDS", value); }
    }

}
