using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de CeC_Licencias
/// </summary>
public class CeC_Licencias
{
    public CeC_Licencias()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Se encarga de enviar el contenido de las variables de creacion de licencia
    /// al metodo correspondiente CreaLicencia.
    /// </summary>
    /// <param name="Sesion">la sesion que se intenta autentificar</param>
    /// <param name="UsuarioID">El ID del usuario al cual se le creara una licencia</param>
    /// <param name="SuscripcionID"></param>
    /// <param name="OrigenID"></param>
    /// <param name="DistribuidorID"></param>
    /// <param name="VigenciaMeses"></param>
    /// <returns></returns>
    public static string CreaLicencia(CeC_Sesion Sesion, int UsuarioID, int SuscripcionID, int OrigenID, int DistribuidorID, int VigenciaMeses)
    {
        return CreaLicencia(Sesion, UsuarioID, SuscripcionID, OrigenID, DistribuidorID, 231, 21, VigenciaMeses);
    }
    /// <summary>
    /// Crea la licencia y la inserta sobre la base de datos.
    /// </summary>
    /// <param name="Sesion"></param>
    /// <param name="UsuarioID"></param>
    /// <param name="SuscripcionID"></param>
    /// <param name="OrigenID"></param>
    /// <param name="DistribuidorID"></param>
    /// <param name="Producto"></param>
    /// <param name="Version"></param>
    /// <param name="VigenciaMeses"></param>
    /// <returns></returns>
    public static string CreaLicencia(CeC_Sesion Sesion, int UsuarioID, int SuscripcionID, int OrigenID, int DistribuidorID, int Producto, int Version, int VigenciaMeses)
    {
        int LicenciaID = CeC_Autonumerico.GeneraAutonumerico("EC_LICENCIAS", "LICENCIA_ID", Sesion);
        Int64 NLicenciaID = 04618006734 + Convert.ToInt64(LicenciaID);
        string Licencia = Producto.ToString("000") + Version.ToString("00") + NLicenciaID.ToString("00000000000");
        long Aleatorio = DateTime.Now.Ticks % 100;

        byte[] Arreglo = CeC.ObtenArregloBytes(Licencia);
        int Chk = 0;
        foreach (byte Caracter in Arreglo)
        {
            Chk = Chk + Caracter * 7 + 52;
        }
        Chk = Chk % 100;
        
        Licencia += Aleatorio.ToString("00") + Chk.ToString("00");
        Licencia = Licencia.Substring(0, 5) + "-" + Licencia.Substring(5, 5) + "-" + Licencia.Substring(10, 5) + "-" + Licencia.Substring(15, 5);
        if (CeC_BD.EjecutaComando("INSERT INTO EC_LICENCIAS (" +
            "LICENCIA_ID, LICENCIA_ORIGEN_ID, LICENCIA_DISTRIBUIDOR_ID, USUARIO_ID, SUSCRIPCION_ID, LICENCIA, " +
            "LICENCIA_CREACIONFH, LICENCIA_VIGENCIAFH, LICENCIA_INSTALACIONES) VALUES(" +
            LicenciaID + "," + OrigenID + "," + DistribuidorID + "," + UsuarioID + "," + SuscripcionID + ",'" + Licencia + "'," +
            CeC_BD.SqlFechaHora(DateTime.Now) + "," + CeC_BD.SqlFechaHora(DateTime.Now.AddMonths(VigenciaMeses)) + ",0)") > 0)
            return Licencia;
        return "";
    }

    public static DataSet ObtenLicencias(int SuscripcionID)
    {
        string Qry = "SELECT        EC_LICENCIAS.LICENCIA_ID, EC_LICENCIAS.LICENCIA, EC_LICENCIAS_DISTRIBUIDORES.LICENCIA_DISTRIBUIDOR,  " +
"                         EC_LICENCIAS_ORIGEN.LICENCIA_ORIGEN, EC_USUARIOS.USUARIO_NOMBRE, EC_SUSCRIPCION.SUSCRIPCION_NOMBRE,  " +
"                         EC_LICENCIAS.LICENCIA_CREACIONFH, EC_LICENCIAS.LICENCIA_VIGENCIAFH, EC_LICENCIAS.LICENCIA_INSTALACIONFH,  " +
"                         EC_LICENCIAS.LICENCIA_COMPRAFH, EC_LICENCIAS.LICENCIA_INSTALACIONES, EC_LICENCIAS.LICENCIA_USOFH,  " +
"                         EC_LICENCIAS.LICENCIA_SOFTWARE, EC_LICENCIAS.LICENCIA_SOFTWARE_ACT, EC_LICENCIAS.LICENCIA_MAQUINA " +
"FROM            EC_LICENCIAS INNER JOIN " +
"                         EC_LICENCIAS_DISTRIBUIDORES ON  " +
"                         EC_LICENCIAS.LICENCIA_DISTRIBUIDOR_ID = EC_LICENCIAS_DISTRIBUIDORES.LICENCIA_DISTRIBUIDOR_ID INNER JOIN " +
"                         EC_LICENCIAS_ORIGEN ON EC_LICENCIAS.LICENCIA_ORIGEN_ID = EC_LICENCIAS_ORIGEN.LICENCIA_ORIGEN_ID INNER JOIN " +
"                         EC_USUARIOS ON EC_LICENCIAS.USUARIO_ID = EC_USUARIOS.USUARIO_ID INNER JOIN " +
"                         EC_SUSCRIPCION ON EC_LICENCIAS.SUSCRIPCION_ID = EC_SUSCRIPCION.SUSCRIPCION_ID " +
"WHERE        (EC_LICENCIAS.LICENCIA_BORRADO = 0) " + " AND EC_LICENCIAS.SUSCRIPCION_ID = " + SuscripcionID;
        return CeC_BD.EjecutaDataSet(Qry);
    }
    /// <summary>
    /// Selecciona el numero de instalaciones de dicha licencia desde la base de datos
    /// y va incrementando el numero de concurrencias de la licencia
    /// posteriormente hace una actualizacion sobre la misma tabla  incluyendo licencia
    /// de instalaciones y licencia de maquina.
    /// </summary>
    /// <param name="Licencia"></param>
    /// <param name="Maquina"></param>
    /// <returns></returns>
    public static int UsaLicencia(string Licencia, string Maquina)
    {
        int Instalaciones = CeC_BD.EjecutaEscalarInt("SELECT LICENCIA_INSTALACIONES FROM EC_LICENCIAS WHERE LICENCIA = '" + CeC_BD.ObtenParametroCadena(Licencia) +
            "'");
        Instalaciones++;
        int R = CeC_BD.EjecutaComando("UPDATE EC_LICENCIAS SET LICENCIA_INSTALACIONFH = " + CeC_BD.QueryFechaHora + ", LICENCIA_INSTALACIONES = " + Instalaciones + ", LICENCIA_MAQUINA = '" + CeC_BD.ObtenParametroCadena(Maquina) +
            "' WHERE LICENCIA = '" + CeC_BD.ObtenParametroCadena(Licencia) +
            "'");
        if (R <= 0)
            return 0;
        return Instalaciones;
    }

    /// <summary>
    /// Manda como parametro la variable que contiene el
    /// numero de licencia, para asi poderlo validar con el
    /// que se encuentra en la base de datos.
    /// </summary>
    /// <param name="Licencia">numero de licencia a comprobar</param>
    /// <param name="Maquina"> Numero de Maquina que hace referencia la licencia</param>
    /// <returns></returns>
    public static string ValidaLicencia(string Licencia, string Maquina)
    {
        int R = CeC_BD.EjecutaComando("UPDATE EC_LICENCIAS SET LICENCIA_USOFH = " + CeC_BD.QueryFechaHora +
    "WHERE LICENCIA = '" + CeC_BD.ObtenParametroCadena(Licencia) + "' AND LICENCIA_MAQUINA = '" + CeC_BD.ObtenParametroCadena(Maquina) +
    "'");
        if (R <= 0)
            return "";
        return Maquina;
    }

}