#define DEBUG_SQL

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

using System.Data.OleDb;

using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;


/// <summary>
/// Descripción breve de CeC_BD
/// </summary>
public class CeC_BD
{
    static int TimeOutSegundos = 180;
    public static string CadenaConexionStr = "";
    /// <summary>
    /// Cadena que almacena la cadena de conexión para la base de datos
    /// </summary>
    private string m_CadenaConexion = "";
    /// <summary>
    /// Propiedad que obtiene la cadena de conexión para la base de datos
    /// </summary>
    public string CeC_BD_CadenaConexion
    {
        get { return m_CadenaConexion; }

    }

    /// <summary>
    /// Instancia de la clase
    /// </summary>
    private static CeC_BD m_BD = new CeC_BD(CeC_BD.CadenaConexion());

    /// <summary>
    /// Constructor de la clase que especifica el valor inicial de la cadena de conexión
    /// </summary>
    /// <param name="CadenaConexion">Cadena de conexión para la base de datos</param>
    public CeC_BD(string CadenaConexion)
    {
        m_CadenaConexion = CadenaConexion;
    }

    /// <summary>
    /// Cadena que almacena el mensaje del último error en la clase lEjecutaDataSet 
    /// </summary>
    private static string m_UltimoErrorBD = "";

    /// <summary>
    /// Propiedad que obtiene el último mensaje de error de la clase lEjecutaDataSet
    /// </summary>
    public static string UltimoErrorBD
    {
        get { return m_UltimoErrorBD; }
    }

    /// <summary>
    /// Propiedad local que obtiene el concatenador adecuado para la base de datos
    /// </summary>
    public string lConcatenador
    {
        get
        {
            if (lEsOracle)
                return "||";
            return "+";
        }
    }

    /// <summary>
    /// Propiedad que obtiene el concatenador adecuado para la base de datos
    /// </summary>
    public static string Concatenador
    {
        get
        {
            return m_BD.lConcatenador;
            //if (m_BD.lEsOracle)
            //    return "||";
            //return "+";
        }
    }

    #region Ejecuta Comando Static
    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor (el primer campo del primer registro).
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>El primer campo del primer registro. Nulo si hubo en error</returns>
    public object lEjecutaEscalar(string Comando)
    {
        if (Comando == "")
            eClockBase.CeC_Log.AgregaError("lEjecutaDataSet No Existe comando a procesar ");
        OleDbConnection Conexion = new OleDbConnection(m_CadenaConexion);
        Conexion.Open();
        try
        {
            OleDbCommand Cmd = new OleDbCommand(Comando, Conexion);
#if DEBUG_SQL
            eClockBase.CeC_Log.AgregaLog("EjecutaEscalar  " + Comando);
#endif
            object Ret = Cmd.ExecuteScalar();
#if DEBUG_SQL
            if (Ret != null)
                eClockBase.CeC_Log.AgregaLog("EjecutaEscalar " + Comando + " Ret " + Ret.ToString());
            else
                eClockBase.CeC_Log.AgregaLog("EjecutaEscalar " + Comando + " Ret NULL");
#endif
            Conexion.Dispose();
            return Ret;

        }
        catch (Exception ex)
        {
#if DEBUG_SQL
            Console.WriteLine("EjecutaEscalar Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
            eClockBase.CeC_Log.AgregaError("EjecutaEscalar Comando " + Comando);
            eClockBase.CeC_Log.AgregaError(ex);


            Conexion.Dispose();
            return null;
        }
        return null;

    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor (el primer campo del primer registro). 
    /// Si existe un error regresara -9999
    /// </summary>
    public static object EjecutaEscalar(string Comando)
    {
        return m_BD.lEjecutaEscalar(Comando);
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor entero (el primer campo del primer registro). 
    /// Si existe un error regresara -9999
    /// </summary>
    public int lEjecutaEscalarInt(string Comando)
    {
        object Valor = lEjecutaEscalar(Comando);
        if (Valor == null)
            return -9999;
        try { return Convert.ToInt32(Valor); }
        catch { }
        return -9999;
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor entero (el primer campo del primer registro), 
    /// si existe un error regresara -9999
    /// </summary>	
    public static int EjecutaEscalarInt(string Comando)
    {
        return m_BD.lEjecutaEscalarInt(Comando);
    }

    /// <summary>
    /// Ejecuta comando SQL y regresa un valor booleano
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <param name="Predeterminado">Valor predeterminado</param>
    /// <returns>Predeterminado si Comando es Nulo. Verdadero si Comando es diferente de 0, 
    /// falso en otro caso</returns>
    public bool lEjecutaEscalarBool(string Comando, bool Predeterminado)
    {
        object Valor = lEjecutaEscalar(Comando);
        if (Valor == null)
            return Predeterminado;
        try
        {
            if (Convert.ToInt32(Valor) != 0)
                return true;
            return false;
        }
        catch { }
        return Predeterminado;
    }

    /// <summary>
    /// Ejecuta comando SQL y regresa un valor booleano
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <param name="Predeterminado">Valor predeterminado</param>
    /// <returns>Predeterminado si Comando es Nulo. Verdadero si Comando es diferente de 0, 
    /// falso en otro caso</returns>
    public static bool EjecutaEscalarBool(string Comando, bool Predeterminado)
    {
        return m_BD.lEjecutaEscalarBool(Comando, Predeterminado);
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor decimal (el primer campo del primer registro), 
    /// si existe un error regresara -9999
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>El primer campo del primer registro</returns>
    public decimal lEjecutaEscalarDecimal(string Comando)
    {
        object Valor = lEjecutaEscalar(Comando);
        if (Valor == null)
            return -9999;
        try { return Convert.ToDecimal(Valor); }
        catch { }
        return -9999;
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor decimal (el primer campo del primer registro), 
    /// si existe un error regresara ""
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>El primer campo del primer registro</returns>
    public static decimal EjecutaEscalarDecimal(string Comando)
    {
        return m_BD.lEjecutaEscalarDecimal(Comando);
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor cadena (el primer campo del primer registro),    
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>El primer campo del primer registro. Si existe un error regresara ""</returns>

    public string lEjecutaEscalarString(string Comando)
    {
        object Valor = lEjecutaEscalar(Comando);
        if (Valor == null)
            return "";
        try { return Convert.ToString(Valor); }
        catch { }
        return "";
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor cadena (el primer campo del primer registro), 
    /// si existe un error regresara ""
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>El primer campo del primer registro. Si existe un error regresara ""</returns>
    public static string EjecutaEscalarString(string Comando)
    {
        return m_BD.lEjecutaEscalarString(Comando);
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor cadena (el primer campo del primer registro), 
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <param name="ValorPredeterminado">Valor predeterminado de fecha</param>
    /// <returns>El primer campo del primer registro. Si existe un error regresara FechaNula</returns>
    public DateTime lEjecutaEscalarDateTime(string Comando, DateTime ValorPredeterminado)
    {
        object Valor = lEjecutaEscalar(Comando);
        if (Valor == null)
            return ValorPredeterminado;
        try { return Convert.ToDateTime(Valor); }
        catch { }
        return ValorPredeterminado;
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor cadena (el primer campo del primer registro), 
    /// si existe un error regresara FechaNula
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>El primer campo del primer registro. Si existe un error regresara FechaNula</returns>
    public DateTime lEjecutaEscalarDateTime(string Comando)
    {
        return lEjecutaEscalarDateTime(Comando, FechaNula);
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor cadena (el primer campo del primer registro), 
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>El primer campo del primer registro. Si existe un error regresara FechaNula</returns>
    public static DateTime EjecutaEscalarDateTime(string Comando)
    {
        return m_BD.lEjecutaEscalarDateTime(Comando);
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa un único valor cadena (el primer campo del primer registro), 
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <param name="ValorPredeterminado">Valor predeterminado de fecha</param>
    /// <returns>El primer campo del primer registro. Si existe un error regresara FechaNula</returns>
    public static DateTime EjecutaEscalarDateTime(string Comando, DateTime ValorPredeterminado)
    {
        return m_BD.lEjecutaEscalarDateTime(Comando, ValorPredeterminado);
    }

    /// <summary>
    /// Verifica si es Oracle U OLEDB, en cualquiera de los dos casos establece la conexion
    /// y crea un DATASET, en caso de no establecerla creara un error
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>Dataset. En cualquier otro caso regresara NULL</returns>
    public DataSet lEjecutaDataSet(string Comando)
    {
        m_UltimoErrorBD = "";
        if (Comando == "")
            eClockBase.CeC_Log.AgregaError("lEjecutaDataSet No Existe comando a procesar ");
#if DEBUG_SQL
        eClockBase.CeC_Log.AgregaLog("lEjecutaDataSet  " + Comando);
#endif
        if (lEsOracle)
        {

            System.Data.OracleClient.OracleConnection Conexion = new System.Data.OracleClient.OracleConnection();
            Conexion.ConnectionString = CadenaConexionOracle(m_CadenaConexion);
            try
            {
                Conexion.Open();
                System.Data.OracleClient.OracleDataAdapter DA =
                    new System.Data.OracleClient.OracleDataAdapter(Comando, Conexion);
                DA.SelectCommand.CommandTimeout = TimeOutSegundos;
                DataSet DS = new DataSet();
                DA.Fill(DS);
                Conexion.Dispose();
                return DS;
            }
            catch (Exception ex)
            {
                if (Conexion.State == ConnectionState.Open)
                    Conexion.Dispose();
                m_UltimoErrorBD = ex.Message;
                string Mensage = ex.Message;

                eClockBase.CeC_Log.AgregaError("EjecutaDataSet Oracle Comando " + Comando);

                eClockBase.CeC_Log.AgregaError(ex);
            }

            return null;
        }
        else
        {
            OleDbConnection Conexion = new OleDbConnection(m_CadenaConexion);

            try
            {
                Conexion.Open();
                //OleDbCommand Cmd = new OleDbCommand(Comando, Conexion);
                OleDbDataAdapter DA = new OleDbDataAdapter(Comando, Conexion);
                DA.SelectCommand.CommandTimeout = TimeOutSegundos;
                DataSet DS = new DataSet();
                DA.Fill(DS);
                Conexion.Dispose();
#if DEBUG_SQL
                eClockBase.CeC_Log.AgregaLog("lEjecutaDataSet  Fin " + Comando);
#endif
                return DS;

            }
            catch (Exception ex)
            {
#if DEBUG_SQL
                Console.WriteLine("EjecutaReader Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
                m_UltimoErrorBD = ex.Message;
                eClockBase.CeC_Log.AgregaError("EjecutaDataSet Comando " + Comando);

                eClockBase.CeC_Log.AgregaError(ex);
                Conexion.Dispose();
                return null;
            }
        }
        return null;
    }

    /// <summary>
    /// Verifica si es Oracle U OLEDB, en cualquiera de los dos casos establece la conexion
    /// y crea un DATASET, en caso de no establecerla creara un error
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>Dataset. En cualquier otro caso regresara NUL</returns>
    public static DataSet EjecutaDataSet(string Comando)
    {
        return (DataSet)m_BD.lEjecutaDataSet(Comando);
    }

    /// <summary>
    /// Establece una conexion OLEDB, crea un DATASET y no aplica las asignaciones del DataAdapter,
    /// en otro caso devolvera un error
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>Dataset. En cualquier otro caso regresara NUL</returns>
    public object lEjecutaDataSet_Schema(string Comando)
    {
        OleDbConnection Conexion = new OleDbConnection(m_CadenaConexion);
        Conexion.Open();
        try
        {
            //OleDbCommand Cmd = new OleDbCommand(Comando, Conexion);
            OleDbDataAdapter DA = new OleDbDataAdapter(Comando, Conexion);
            DataSet DS = new DataSet();
            DA.FillSchema(DS, SchemaType.Source);
            //OleDbCommandBuilder CB = new OleDbCommandBuilder();
            //object Ret = Cmd.ExecuteReader();

            Conexion.Dispose();
            return DS;

        }
        catch (Exception ex)
        {
#if DEBUG_SQL
            Console.WriteLine("EjecutaReader Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
            eClockBase.CeC_Log.AgregaError("EjecutaDataSet_Schema Comando " + Comando);

            eClockBase.CeC_Log.AgregaError(ex);
            Conexion.Dispose();
            return null;
        }
        return null;
    }

    /// <summary>
    /// Establece una conexion OLEDB, crea un DATASET y no aplica las asignaciones del DataAdapter,
    /// en otro caso devolvera un error
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>Dataset. En cualquier otro caso regresara NUL</returns>
    public static object EjecutaDataSet_Schema(string Comando)
    { return m_BD.lEjecutaDataSet_Schema(Comando); }

    /// <summary>
    ///  Establece una conexion OLEDB, ejecuta un comando SQL y regresa DataReader, si existe un error regresara NULL
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>DataReader, si existe un error regresara NULL</returns>
    public object lEjecutaReader(string Comando)
    {
        OleDbConnection Conexion = new OleDbConnection(m_CadenaConexion);
        Conexion.Open();
        try
        {
            OleDbCommand Cmd = new OleDbCommand(Comando, Conexion);

            object Ret = Cmd.ExecuteReader();

            //		Conexion.Dispose();
            return Ret;

        }
        catch (Exception ex)
        {
#if DEBUG_SQL
            Console.WriteLine("EjecutaReader Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
            eClockBase.CeC_Log.AgregaError("EjecutaReader Comando " + Comando);
            eClockBase.CeC_Log.AgregaError(ex);
            Conexion.Dispose();
            return null;
        }
        return null;
    }

    /// <summary>
    ///  Establece una conexion OLEDB, ejecuta un comando SQL y regresa DataReader, si existe un error regresara NULL
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <returns>DataReader, si existe un error regresara NULL</returns>
    public static object EjecutaReader(string Comando)
    {
        return m_BD.lEjecutaReader(Comando);
    }

    /// <summary>
    /// Ejecuta un DataReader, si existe un error regresara un valor NULL
    /// </summary>
    /// <param name="Comando">Comando</param>
    /// <param name="Conexion">Conexión a fuente de datos</param>
    /// <returns>DataReader, si existe un error regresara NULL</returns>
    public object lEjecutaReader(string Comando, out OleDbConnection Conexion)
    {
        Conexion = new OleDbConnection(m_CadenaConexion);
        Conexion.Open();
        try
        {
            OleDbCommand Cmd = new OleDbCommand(Comando, Conexion);
            Cmd.CommandTimeout = TimeOutSegundos;
            object Ret = Cmd.ExecuteReader();

            //		Conexion.Dispose();
            return Ret;

        }
        catch (Exception ex)
        {
#if DEBUG_SQL
            Console.WriteLine("EjecutaReader Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
            Conexion.Dispose();
            eClockBase.CeC_Log.AgregaError("EjecutaReader Comando " + Comando);
            eClockBase.CeC_Log.AgregaError(ex);
            return null;
        }
        return null;
    }

    /// <summary>
    /// Ejecuta un DataReader, si existe un error regresara un valor NULL
    /// </summary>
    /// <param name="Comando">Comando SQL</param>
    /// <param name="Conexion">Conexión a fuente de datos</param>
    /// <returns>DataReader, si existe un error regresara NULL</returns>
    public static object EjecutaReader(string Comando, out OleDbConnection Conexion)
    {
        return m_BD.lEjecutaReader(Comando, out Conexion);
    }

    public int lEjecutaComando(string Comando)
    {
        return lEjecutaComando(Comando, true);

    }
    /// <summary>
    /// Ejecuta un comando SQL.
    /// </summary>
    public static int EjecutaComando(string Comando)
    {
        return EjecutaComando(Comando, true);
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa el numero de registros modificados con dicho comando
    /// </summary>
    public int lEjecutaComando(string Comando, bool AgregaError)
    {
        OleDbConnection Conexion = new OleDbConnection(m_CadenaConexion);
        Conexion.Open();
        try
        {
            OleDbCommand Cmd = new OleDbCommand(Comando, Conexion);

#if DEBUG_SQL
            eClockBase.CeC_Log.AgregaLog("EjecutaComando " + Comando);
#endif

            Cmd.CommandTimeout = TimeOutSegundos;

            int Ret = Cmd.ExecuteNonQuery();

#if DEBUG_SQL
            eClockBase.CeC_Log.AgregaLog("EjecutaComando " + Ret);
#endif

            Conexion.Dispose();
            return Ret;
        }
        catch (Exception ex)
        {
            //string l = ex.Message;
#if DEBUG_SQL
            Console.WriteLine("EjecutaComando Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
            m_UltimoErrorBD = ex.Message;
            if (AgregaError)
                eClockBase.CeC_Log.AgregaError("EjecutaComando Comando " + Comando);
            Conexion.Dispose();
            if (AgregaError)
                eClockBase.CeC_Log.AgregaError(ex);
            return -1;
        }
        return 0;
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa el numero de registros modificados con dicho comando
    /// </summary>
    public static int EjecutaComando(string Comando, bool AgregaError)
    {
        return m_BD.lEjecutaComando(Comando, AgregaError);
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa el numero de registros modificados con dicho comando
    /// </summary>
    public int lEjecutaComandoRTexto(string CadenaConexion, string Comando, string NombreParametro, string ValorParametro, int LenEnBD)
    {
        if (CadenaConexion.Length <= 0)
            CadenaConexion = m_CadenaConexion;
        OleDbConnection Conexion = new OleDbConnection(CadenaConexion);
        Conexion.Open();
        try
        {
            OleDbCommand Cmd = new OleDbCommand();
            Cmd.Connection = Conexion;
            Cmd.CommandText = Comando;
#if DEBUG_SQL
            eClockBase.CeC_Log.AgregaLog("EjecutaComando " + Comando);
#endif
            Cmd.CommandTimeout = TimeOutSegundos;
            Cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter(NombreParametro, System.Data.OleDb.OleDbType.VarChar, LenEnBD, NombreParametro));
            Cmd.Parameters[0].Value = ValorParametro;
            int Ret = Cmd.ExecuteNonQuery();
#if DEBUG_SQL
            eClockBase.CeC_Log.AgregaLog("EjecutaComando " + Ret);
#endif
            Conexion.Dispose();
            return Ret;
        }
        catch (Exception ex)
        {
            //string l = ex.Message;
#if DEBUG_SQL
            Console.WriteLine("EjecutaComando Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
            eClockBase.CeC_Log.AgregaError("EjecutaComandoRTexto CadenaConexion " + CadenaConexion + ", Comando " + Comando);
            Conexion.Dispose();
            eClockBase.CeC_Log.AgregaError(ex);
            return 0;
        }
        return 0;
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa el numero de registros modificados con dicho comando
    /// </summary>
    public static int EjecutaComandoRTexto(string CadenaConexion, string Comando, string NombreParametro, string ValorParametro, int LenEnBD)
    {
        return m_BD.lEjecutaComandoRTexto(CadenaConexion, Comando, NombreParametro, ValorParametro, LenEnBD);
    }

    /// <summary>
    /// Ejecuta un comando SQL y regresa el numero de registros modificados con dicho comando
    /// </summary>
    public static int EjecutaComando(string CadenaConexion, string Comando)
    {
        OleDbConnection Conexion = new OleDbConnection(CadenaConexion);
        Conexion.Open();
        try
        {
            OleDbCommand Cmd = new OleDbCommand(Comando, Conexion);
#if DEBUG_SQL
            eClockBase.CeC_Log.AgregaLog("EjecutaComando " + Comando);
#endif
            Cmd.CommandTimeout = TimeOutSegundos;
            int Ret = Cmd.ExecuteNonQuery();
#if DEBUG_SQL
            eClockBase.CeC_Log.AgregaLog("EjecutaComando Ret" + Ret);
#endif
            Conexion.Dispose();
            return Ret;
        }
        catch (Exception ex)
        {
            //string l = ex.Message;
#if DEBUG_SQL
            Console.WriteLine("EjecutaComando Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
            eClockBase.CeC_Log.AgregaError("EjecutaComando CadenaConexion " + CadenaConexion + ", Comando " + Comando);

            Conexion.Dispose();
            eClockBase.CeC_Log.AgregaError(ex);
            return 0;
        }
        return 0;
    }

    #endregion




    /// <summary>
    /// Contiene la sentencia Sql para obtener la fecha y hora, en Oracle (SELECT sysdate from dual) y en SQL Server (select getdate())
    /// </summary>
    public static string QueryFechaHora
    {
        get
        {
            if (EsOracle)
                return "(SELECT sysdate from dual)";
            return "GetDate()";
        }
    }

    /// <summary>
    /// Contiene la sentencia Sql para obtener la fecha , en Oracle (SELECT sysdate from dual) y en SQL Server (select getdate())
    /// </summary>
    public static string QueryFecha
    {
        get
        {
            if (EsOracle)
                return "(SELECT sysdate from dual)";
            return "CONVERT (date, GETDATE())";
        }
    }
    /// <summary>
    /// Retorna una Fecha Nula por default que es del dia 01/Enero/2006
    /// </summary>
    public static DateTime FechaNula
    {
        get { return new DateTime(2006, 01, 01); }
    }
    /// <summary>
    /// Obtiene la Fecha y Hora en Oracle o SQL, el formato de la fecha a obtener es dd/MM/yyyy HH:mm:ss
    /// </summary>
    /// <param name="FechaHora">Fecha y hora</param>
    /// <returns></returns>
    public static string SqlFechaHora(DateTime FechaHora)
    {
        if (FechaHora == new DateTime(1, 1, 1))
            FechaHora = FechaNula;
        if (EsOracle)
            return " TO_DATE('" + FechaHora.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS') ";
        return " convert(datetime,'" + FechaHora.ToString("dd/MM/yyyy HH:mm:ss") + "',103) ";

    }
    /// <summary>
    /// Retorna la Fecha Nula en SQL
    /// </summary>
    /// <returns>Fecha Nula</returns>
    public static string SqlFechaNula()
    { return SqlFecha(FechaNula); }
    /// <summary>
    /// Regresa la fecha en formato ORACLE o SQL, formato a devolver es dd/MM/yyyy
    /// </summary>
    /// <param name="Fecha">Fecha</param>
    /// <returns></returns>
    public static string SqlFecha(DateTime Fecha)
    {
        return m_BD.lSqlFecha(Fecha);
    }
    public string lSqlFecha(DateTime Fecha)
    {
        if (lEsOracle)
            return " TO_DATE('" + Fecha.ToString("dd/MM/yyyy") + "','DD/MM/YYYY') ";

        return " convert(datetime,'" + Fecha.ToString("dd/MM/yyyy") + "',103) ";
    }
    public static string SqlFecha(string CampoFecha)
    {
        return m_BD.lSqlFecha(CampoFecha);
    }
    public string lSqlFecha(string CampoFecha)
    {
        if (lEsOracle)
            return " TO_DATE(" + CampoFecha + ",'DD/MM/YYYY') ";

        return " convert(datetime," + CampoFecha + ",103) ";
    }
    /// <summary>
    /// Compara la longitud de la Hora le quita los espacios y la convierte al formato predeterminado
    /// </summary>
    /// <param name="Hora">Hora</param>
    /// <returns>FechaHora, en otro caso devolvera FechaNula</returns>
    public static DateTime Hora2DateTime(string Hora)
    {
        DateTime FechaHora = CeC_BD.FechaNula;
        if (Hora.Length > 5)
        {
            try
            {
                Hora = Hora.Trim();
                string[] Horas = Hora.Split(new char[] { ' ' });
                string[] Elems = Horas[0].Split(new char[] { ':' });
                int iHora = 0;
                int iMinuto = 0;
                int iSegundos = 0;
                iHora = Convert.ToInt16(Elems[0]);
                if (Elems.Length > 1)
                    iMinuto = Convert.ToInt16(Elems[1]);
                if (Elems.Length > 2)
                    iSegundos = Convert.ToInt16(Elems[2]);
                if (Horas.Length > 1)
                    if (Horas[1].ToUpper().Substring(0, 1) == "P")
                        iHora += 12;
                FechaHora = FechaHora.AddHours(iHora);
                FechaHora = FechaHora.AddMinutes(iMinuto);
                FechaHora = FechaHora.AddSeconds(iSegundos);
                return FechaHora;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }


            return FechaNula;
        }
        if (Hora.Length < 4)
            return FechaNula;
        if (Hora.Length < 5)
        {
            Hora = "0" + Hora;
        }
        FechaHora = FechaHora.AddHours(Convert.ToInt32(Hora.Substring(0, 2)));
        FechaHora = FechaHora.AddMinutes(Convert.ToInt32(Hora.Substring(3, 2)));
        return FechaHora;
    }
    /// <summary>
    /// Suma a FechaNula un intervalo de tiempo
    /// </summary>
    /// <param name="Tiempo">Intervalo de Tiempo</param>
    /// <returns></returns>
    public static DateTime TimeSpan2DateTime(TimeSpan Tiempo)
    {
        return FechaNula + Tiempo;
        //		return new DateTime(2006, 01, 01 + Tiempo.Days, Tiempo.Hours, Tiempo.Minutes, Tiempo.Seconds);
    }
    /// <summary>
    /// Compara la longitud de la Hora eliminando espacios y le decrementa la FechaNula
    /// </summary>
    /// <param name="Hora">Hora</param>
    /// <returns></returns>
    public static TimeSpan Hora2TimeSpan(string Hora)
    {
        return Hora2DateTime(Hora) - FechaNula;
    }
    /// <summary>
    /// Obtiene un instante de tiempo(fecha y hora) de un intervalo de tiempo dado
    /// </summary>
    /// <param name="TiempoDateTime"></param>
    /// <returns></returns>
    public static DateTime TimeSpanDate2DateTime(DateTime TiempoDateTime)
    {
        TimeSpan Tiempo = DateTime2TimeSpan(TiempoDateTime);
        return TimeSpan2DateTime(Tiempo);
    }
    /// <summary>
    /// Regresa el Tiempo menos el decremento de la FechaNula(01/01/2006)
    /// </summary>
    /// <param name="Tiempo">Tiempo</param>
    /// <returns></returns>
    public static TimeSpan DateTime2TimeSpan(DateTime Tiempo)
    {
        TimeSpan Ts = Tiempo - FechaNula;
        Ts = new TimeSpan(Ts.Days, Ts.Hours, Ts.Minutes, Ts.Seconds);
        return Ts;
    }
    /// <summary>
    /// Regresa un instante de tiempo(Fecha y Hora), de Tiempo menos la FechaNula(01/01/2006)
    /// </summary>
    /// <param name="Tiempo">Intervalo de Tiempo</param>
    /// <returns></returns>
    public static DateTime DateTime2TimeSpanDate(DateTime Tiempo)
    {
        return TimeSpan2DateTime(Tiempo - FechaNula);
    }



    /// <summary>
    /// Obtiene la Cadena de Configuracion en caso de no hallarla
    /// devolvera un error
    /// </summary>
    /// <param name="Variable"></param>
    /// <param name="Default"></param>
    /// <returns>Cadena de Configuracion</returns>
    public static string ObtenCadenaConfig(string Variable, string Default)
    {
        try
        {
            System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            string Cadena = ((string)(configurationAppSettings.GetValue(Variable, typeof(string))));
            //			string Cadena = Globales.CadenaConnOLE();
            return Cadena;
        }
        catch (Exception ex)
        {
            eClockBase.CeC_Log.AgregaError(ex);
        }
        return Default;
    }
    /// <summary>
    /// Verifica que exista un unico valor entero(el primer campo del primer registro)
    /// </summary>
    /// <param name="Tabla">Nombre de la Tabla</param>
    /// <param name="Campo">Nombre del Campo</param>
    /// <param name="QryAdicional">Consulta adicional</param>
    /// <returns>Existe</returns>
    public static int ExisteRegistro(string Tabla, string Campo, string QryAdicional)
    {
        int existe = 0;

        existe = EjecutaEscalarInt(("SELECT count(" + Campo + ") FROM " + Tabla + " " + QryAdicional));
        return existe;
    }

    /// <summary>
    /// Método que obtiene la cadena de conexión a la base de datos
    /// </summary>
    public static string CadenaConexion()
    {
        if (CadenaConexionStr.Length > 0)
            return CadenaConexionStr;
        CadenaConexionStr = "";
        return CadenaConexionStr;
    }
    /// <summary>
    /// Regresa el texto buscado en una cadena
    /// </summary>
    /// <param name="Cadena">Cadena</param>
    /// <param name="Dato">Dato a buscar</param>
    /// <returns></returns>
    public static string BuscaValorTexto(string Cadena, string Dato)
    {
        return BuscaValorTexto(Cadena, Dato, ";");
    }
    /// <summary>
    /// Regresa una cadena obtenida de un dato
    /// </summary>
    /// <param name="Cadena"></param>
    /// <param name="Dato"></param>
    /// <param name="Separador"></param>
    /// <returns></returns>
    public static string BuscaValorTexto(string Cadena, string Dato, string Separador)
    {

        string CadenaMayus = Cadena.ToUpper();
        Dato = Dato.ToUpper();
        int Pos = CadenaMayus.IndexOf(Dato);
        if (Pos >= 0)
        {
            string CadeTemp = Cadena.Substring(Pos + Dato.Length);
            Pos = CadeTemp.IndexOf(Separador);
            if (Pos > 0)
            {
                CadeTemp = CadeTemp.Substring(0, Pos);
            }
            return CadeTemp;
        }
        return "";

    }

    /*
     * insert into EC_PERMISOS_SUSCRIP (usuario_id,SUSCRIPCION_ID) select usuario_id, SUSCRIPCION_ID from (select EC_USUARIOS.usuario_id, sap from EC_USUARIOS, EC_PERMISOS_SUSCRIP, EC_SUSCRIPCION,cctsap where EC_USUARIOS.usuario_id = EC_PERMISOS_SUSCRIP.usuario_id and EC_PERMISOS_SUSCRIP.SUSCRIPCION_ID= EC_SUSCRIPCION.SUSCRIPCION_ID AND SUSCRIPCION_NOMBRE = adam)t, EC_SUSCRIPCION where sap = EC_SUSCRIPCION.SUSCRIPCION_NOMBRE*/

    /// <summary>
    /// Crea registros en las tablas de grupos y personas sobre nuevos empleados
    /// </summary>
    public static void CreaRelacionesEmpleados()
    {
        return;

    }




    /// <summary>
    /// Calcula el valor del Hash de un texto
    /// </summary>
    /// <param name="Texto">Texto de donde se va a obtener el hash</param>
    /// <returns></returns>
    public static string CalculaHash(string Texto)
    {
        System.Security.Cryptography.SHA1CryptoServiceProvider Sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        string HashSR = BitConverter.ToString(Sha1.ComputeHash(new System.IO.MemoryStream(System.Text.ASCIIEncoding.Default.GetBytes(Texto))));
        return HashSR;
    }
    public static string CalculaHash(object[] Arreglo)
    {
        string sArreglo = "";
        foreach (object Objeto in Arreglo)
        {
            sArreglo += Objeto.ToString();
        }
        return CalculaHash(sArreglo);
    }

    public static string ObtenSentencias(string Archivo)
    {
        string archivo;
        if (!CeC_BD.EsOracle)
        {
            archivo = File.ReadAllText(Archivo);
            archivo = archivo.Replace(" INTEGER UNSIGNED ", " decimal(38) ");
            archivo = archivo.Replace(" INTEGER ", " decimal(38) ");
            archivo = archivo.Replace(" BOOL ", " decimal(1) ");
            archivo = archivo.Replace(" TIME ", " DATETIME ");
            archivo = archivo.Replace(" DATE ", " DATETIME ");
            archivo = archivo.Replace(" BLOB ", " image ");
            archivo = archivo.Replace(" DECIMAL ", " decimal(18,2) ");
        }
        else
        {
            archivo = File.ReadAllText(Archivo);
            archivo = archivo.Replace(" INTEGER UNSIGNED ", " NUMBER(38) ");
            archivo = archivo.Replace(" INTEGER ", " NUMBER(38) ");
            archivo = archivo.Replace(" BOOL ", " NUMBER(1) ");
            archivo = archivo.Replace(" VARCHAR(", " VARCHAR2(");
            archivo = archivo.Replace(" DATETIME ", " DATE ");
            archivo = archivo.Replace(" TIME ", " DATE ");
            archivo = archivo.Replace(" DECIMAL ", " NUMBER(34,4) ");

        }
        return archivo;
    }

    public static int EjecutaQrys(string Qrys)
    {
        int Correctos = 0;
        string[] Codigo = Qrys.Split(new char[] { ';' });
        foreach (string linea in Codigo)
        {
            string NLinea = linea; // linea.Replace("GO", "");
            NLinea = NLinea.Trim();
            if (NLinea.Length >= 2 && NLinea.Substring(0, 2) == "GO")
                NLinea = NLinea.Substring(2);
            NLinea = NLinea.Trim();
            if (EjecutaComando(NLinea) > 0)
                Correctos++;

        }
        return Correctos;
    }


    public static bool EjecutaParcheOracle()
    {
        string PrefijoTablas = "EC_";
        string Qry = "SELECT TABLE_NAME, COLUMN_NAME, DATA_LENGTH from all_tab_columns where TABLE_NAME like '" + PrefijoTablas + "%' and DATA_TYPE = 'VARCHAR2' AND NULLABLE = 'N' order by owner, table_name, column_id";

        DataSet DS = (DataSet)EjecutaDataSet(Qry);
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                EjecutaComando("alter table \"" + eClockBase.CeC.Convierte2String(DR["TABLE_NAME"]) + "\" modify (\"" +
                    eClockBase.CeC.Convierte2String(DR["COLUMN_NAME"]) + "\" VARCHAR2(" + eClockBase.CeC.Convierte2String(DR["DATA_LENGTH"]) + ") NULL)");
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Propiedad local que determina si la cadena de conexión es de una base de datos de Oracle.
    /// Regresa true si es una cadena de conexión de Oracle y false en caso contrario.
    /// </summary>
    public bool lEsOracle
    {
        get
        {
            if (m_CadenaConexion.IndexOf("MSDAORA") > 0 || m_CadenaConexion.IndexOf("OraOLEDB") > 0)
                return true;
            return false;
        }
    }

    /// <summary>
    /// Propiedad que determina si la cadena de conexión es de una base de datos de Oracle.
    /// Regresa true si es una cadena de conexión de Oracle y false en caso contrario.
    /// </summary>
    public static bool EsOracle
    {
        get
        {
            return m_BD.lEsOracle;
            //if (CadenaConexion().IndexOf("MSDAORA") > 0 || CadenaConexion().IndexOf("OraOLEDB") > 0)
            //    return true;
            //return false;
        }
    }

    /// <summary>
    /// Regresa la cadena de conexion ORACLE(USER ID,PASSWORD,DATASOURCE)
    /// </summary>
    /// <returns>Cadena de Conexion</returns>
    public static string CadenaConexionOracle()
    {

        return CadenaConexionOracle(CadenaConexion());
    }

    public static string CadenaConexionOracle(string Cadena)
    {

        string Usuario = BuscaValorTexto(Cadena, "USER ID=");
        string Clave = BuscaValorTexto(Cadena, "Password=");
        string SID = BuscaValorTexto(Cadena, "Data Source=");
        string CadeConexion = "user id=" + Usuario + ";data source=" + SID + ";password=" + Clave;
        return CadeConexion;
    }
    /// <summary>
    ///Devuelve (OleDbConnection) una conexión abierta a la base de datos definida en la cadena de conexión de esta clase
    ///o de lo contrario devuelve error
    /// </summary>
    public static object ObtenConexion()
    {
        try
        {
            OleDbConnection Conexion = new OleDbConnection(CadenaConexion());
            Conexion.Open();
            return Conexion;
        }
        catch (System.Exception e)
        {
            //				e.Message;
#if DEBUG_SQL
            Console.WriteLine("ObtenConexion Err {0:G},  {1:G}", e.Message, e.StackTrace);
#endif
            eClockBase.CeC_Log.AgregaError(e);


        }
        return null;
    }




    #region Manejo Binarios

    /// <summary>
    /// Asigna un binario a la imagen actualizandolo, en caso de
    /// no poder actualizarla generara un error
    /// </summary>
    /// <param name="Tabla">Nombre de la Tabla</param>
    /// <param name="CampoLlave"></param>
    /// <param name="Llave"></param>
    /// <param name="CampoBinario"></param>
    /// <param name="Binario"></param>
    /// <returns></returns>
    public static bool AsignaBinario(string Tabla, string CampoLlave, int Llave, string CampoBinario, byte[] Binario)
    {
        return AsignaBinario(Tabla, CampoLlave + "=" + Llave, CampoBinario, Binario);
    }

    /// <summary>
    /// Asigna un binario a un registro de una tabla
    /// </summary>
    /// <param name="Tabla"></param>
    /// <param name="FiltroUpdate">Filtro sin Where</param>
    /// <param name="CampoBinario"></param>
    /// <param name="Binario"></param>
    /// <returns></returns>
    public static bool AsignaBinario(string Tabla, string FiltroUpdate, string CampoBinario, byte[] Binario)
    {
        if (EsOracle)
        {

            System.Data.OracleClient.OracleConnection Conexion = new System.Data.OracleClient.OracleConnection();
            Conexion.ConnectionString = CeC_BD.CadenaConexionOracle();
            try
            {
                Conexion.Open();
                System.Data.OracleClient.OracleCommand Selecciona = Conexion.CreateCommand();
                Selecciona.CommandText = "UPDATE " + Tabla +
                                               " SET  " + CampoBinario + " = :IMAGEN WHERE " + FiltroUpdate;
                Selecciona.Parameters.Add("IMAGEN", System.Data.OracleClient.OracleType.Blob);
                if (Binario != null)
                    Selecciona.Parameters[0].Value = Binario;
                else
                    Selecciona.Parameters[0].Value = System.DBNull.Value;

                Selecciona.ExecuteNonQuery();
                Conexion.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                if (Conexion.State == ConnectionState.Open)
                    Conexion.Dispose();
                string Mensage = ex.Message;
                eClockBase.CeC_Log.AgregaError(ex);
            }

            return false;
        }
        try
        {
            object ObjetoCon = CeC_BD.ObtenConexion();
            if (ObjetoCon == null)
                return false;
            OleDbConnection Conexion = (OleDbConnection)ObjetoCon;
            OleDbCommand Comando = Conexion.CreateCommand();
            Comando.CommandText = "UPDATE " + Tabla +
                " SET  " + CampoBinario + " = ? WHERE " + FiltroUpdate;
            int Longitud = 10;
            if (Binario != null)
                Longitud = Binario.Length;
            Comando.Parameters.Add(CampoBinario, System.Data.OleDb.OleDbType.VarBinary);
            //Comando.Parameters.Add(CampoLlave, System.Data.OleDb.OleDbType.Numeric, 38,CampoLlave);

            //Comando.Parameters[CampoLlave].Value = Llave;

            if (Binario != null)
                Comando.Parameters[CampoBinario].Value = Binario;
            else
                Comando.Parameters[CampoBinario].Value = System.DBNull.Value;

            int ret = Comando.ExecuteNonQuery();
            Conexion.Close();
            return true;
        }
        catch (Exception ex)
        {
            string Error;
            Error = ex.Message;

            eClockBase.CeC_Log.AgregaError(ex);
        }

        return false;
    }



    /// <summary>
    /// Selecciona un campo con dato binario de la tabla , donde el campo llave
    /// sea igual a la llave y regresara ese dato binario
    /// </summary>
    /// <param name="Tabla">Nombre de la Tabla</param>
    /// <param name="CampoLlave">Campo Llave</param>
    /// <param name="Llave"></param>
    /// <param name="CampoBinario"></param>
    /// <returns></returns>
    public static byte[] ObtenBinario(string Tabla, string CampoLlave, int Llave, string CampoBinario)
    {
        if (EsOracle)
        {

            System.Data.OracleClient.OracleConnection Conexion = new System.Data.OracleClient.OracleConnection();
            Conexion.ConnectionString = CeC_BD.CadenaConexionOracle();
            try
            {
                Conexion.Open();
                System.Data.OracleClient.OracleCommand Selecciona = Conexion.CreateCommand();
                Selecciona.CommandText = "SELECT " + CampoBinario + " FROM " + Tabla + " WHERE " + CampoLlave + " = " + Llave.ToString();

                object Obj = Selecciona.ExecuteScalar();
                if (Obj != System.DBNull.Value)
                {
                    System.Array Arreglo = (System.Array)Obj;
                    byte[] Dato = new byte[Arreglo.Length];
                    Array.Copy(Arreglo, Dato, Arreglo.Length);
                    Conexion.Dispose();
                    return Dato;
                }
                Conexion.Dispose();
            }
            catch (Exception ex)
            {
                if (Conexion.State == ConnectionState.Open)
                    Conexion.Dispose();
                string Mensage = ex.Message;
                eClockBase.CeC_Log.AgregaError(ex);
            }

            return null;
        }
        {
            object Obj = CeC_BD.EjecutaEscalar("SELECT " + CampoBinario + " FROM " + Tabla + " WHERE " + CampoLlave + " = " + Llave.ToString());
            if (Obj != null && Obj != DBNull.Value)
                return (byte[])Obj;
        }
        return null;
    }
    /// <summary>
    /// Obtiene un campo con dato binario de la tabla y regresa ese dato
    /// en caso contrio regresara null 
    /// </summary>
    /// <param name="Tabla"></param>
    /// <param name="CampoLlave1"></param>
    /// <param name="Llave1"></param>
    /// <param name="CampoLlave2"></param>
    /// <param name="Llave2"></param>
    /// <param name="CampoBinario"></param>
    /// <returns></returns>
    public static byte[] ObtenBinario(string Tabla, string CampoLlave1, int Llave1, string CampoLlave2, int Llave2, string CampoBinario)
    {
        try
        {

            if (EsOracle)
            {

                System.Data.OracleClient.OracleConnection Conexion = new System.Data.OracleClient.OracleConnection();
                Conexion.ConnectionString = CeC_BD.CadenaConexionOracle();
                try
                {
                    Conexion.Open();
                    System.Data.OracleClient.OracleCommand Selecciona = Conexion.CreateCommand();
                    Selecciona.CommandText = "SELECT " + CampoBinario + " FROM " + Tabla + " WHERE " + CampoLlave1 + " = " + Llave1.ToString() + " AND " + CampoLlave2 + " = " + Llave2.ToString();
                    object Obj = Selecciona.ExecuteScalar();
                    if (Obj != System.DBNull.Value)
                    {
                        System.Array Arreglo = (System.Array)Obj;
                        byte[] Dato = new byte[Arreglo.Length];
                        Array.Copy(Arreglo, Dato, Arreglo.Length);
                        Conexion.Dispose();
                        return Dato;
                    }
                    Conexion.Dispose();
                }
                catch (Exception ex)
                {
                    if (Conexion.State == ConnectionState.Open)
                        Conexion.Dispose();
                    string Mensage = ex.Message;
                    eClockBase.CeC_Log.AgregaError(ex);
                }

                return null;
            }
            {
                object Obj = CeC_BD.EjecutaEscalar("SELECT " + CampoBinario + " FROM " + Tabla + " WHERE " + CampoLlave1 + " = " + Llave1.ToString() + " AND " + CampoLlave2 + " = " + Llave2.ToString());
                if (Obj != null)
                    return (byte[])Obj;
            }
        }
        catch (Exception)
        {


        }
        return null;
    }
    #endregion




    public static Byte[] ObtenArregloBytes(string Cadena)
    {
        if (Cadena.Length < 1)
            return null;
        Byte[] Arreglo = new byte[Cadena.Length + 1];
        for (int Cont = 0; Cont < Cadena.Length; Cont++)
        {
            Arreglo[Cont] = Convert.ToByte(Cadena[Cont]);
        }
        Arreglo[Cadena.Length] = 0;
        return Arreglo;
    }


    /// <summary>
    /// No pone el valor entre comillas
    /// </summary>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public static string ObtenParametroCadena(string Valor)
    {
        if (Valor == null)
            return "";
        return Valor.Replace("'", "''");
    }

    /// <summary>
    /// Para texto no se debe agregar comillas simples
    /// </summary>
    /// <param name="Qry"></param>
    /// <param name="Parametro"></param>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public static string AsignaParametro(string Qry, string Parametro, string Valor)
    {
        return Qry.Replace("@" + Parametro + "@", "'" + ObtenParametroCadena(Valor) + "'");
    }
    public static string AsignaParametro(string Qry, string Parametro, int Valor)
    {
        return Qry.Replace("@" + Parametro + "@", Valor.ToString());
    }

    public static string AsignaParametro(string Qry, string Parametro, DateTime FechaYHora)
    {
        return Qry.Replace("@" + Parametro + "@", SqlFechaHora(FechaYHora));
    }
    public static string AsignaParametroFecha(string Qry, string Parametro, DateTime Fecha)
    {
        return Qry.Replace("@" + Parametro + "@", SqlFecha(Fecha));
    }
    public static string AsignaParametroSQL(string Qry, string Parametro, string Valor)
    {
        return Qry.Replace("@" + Parametro + "@", Valor);
    }

    public static DataTable ConvertTo<T>(IList<T> list)
    {
        DataTable table = CreateTable<T>();
        Type entityType = typeof(T);
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

        foreach (T item in list)
        {
            DataRow row = table.NewRow();

            foreach (PropertyDescriptor prop in properties)
            {
                row[prop.Name] = prop.GetValue(item);
            }

            table.Rows.Add(row);
        }

        return table;
    }

    public static IList<T> ConvertTo<T>(IList<DataRow> rows)
    {
        IList<T> list = null;

        if (rows != null)
        {
            list = new List<T>();

            foreach (DataRow row in rows)
            {
                T item = CreateItem<T>(row);
                list.Add(item);
            }
        }

        return list;
    }

    public static IList<T> ConvertTo<T>(DataTable table)
    {
        if (table == null)
        {
            return null;
        }

        List<DataRow> rows = new List<DataRow>();

        foreach (DataRow row in table.Rows)
        {
            rows.Add(row);
        }

        return ConvertTo<T>(rows);
    }



    public static T CreateItem<T>(DataRow row)
    {
        T obj = default(T);
        if (row != null)
        {
            obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                try
                {
                    object value = row[column.ColumnName];
                    if (value == System.DBNull.Value)
                        continue;
                    prop.SetValue(obj, eClockBase.CeC.ObtenValor(value, prop.PropertyType), null);
                }
                catch
                {
                    /*// You can log something here
                    throw;*/
                }
            }
        }

        return obj;
    }

    public static DataTable CreateTable<T>()
    {
        Type entityType = typeof(T);
        DataTable table = new DataTable(entityType.Name);
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

        foreach (PropertyDescriptor prop in properties)
        {
            table.Columns.Add(prop.Name, prop.PropertyType);
        }

        return table;
    }

    public static string DataSet2Json(DataSet DS)
    {
        try
        {
            if (DS == null)
                return null;
            StringWriter sw = new StringWriter();
            DS.WriteXml(sw, XmlWriteMode.IgnoreSchema);
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sw.ToString());

            //return JsonConvert.SerializeXmlNode(xd.DocumentElement["Table"], Newtonsoft.Json.Formatting.None, true);
            return JsonConvert.SerializeXmlNode(xd);
        }
        catch (Exception ex)
        {
            eClockBase.CeC_Log.AgregaError(ex);
        }
        return null;
    }

    public static string DataSet2JsonList(DataSet DS, bool ArregloOpcional = false)
    {
        string Json = DataSet2Json(DS);
        if (Json == null)
            return null;
        string Buscar = "\"" + DS.Tables[0].TableName + "\":";
        int Pos = Json.IndexOf(Buscar);
        if (Pos > 0)
        {
            string Res = Json.Substring(Pos + Buscar.Length, Json.Length - Pos - Buscar.Length - 2);

            if (!ArregloOpcional && Res[0] != '[')
                Res = "[" + Res + "]";
            return Res;

        }
        int Cor = Json.IndexOf("[");
        if (Cor > 0)
            Json = Json.Substring(Cor);
        Cor = Json.LastIndexOf("]");
        if (Cor > 0)
            Json = Json.Substring(0, Cor + 1);
        return Json;
    }

    public DataTable lObtenTablas()
    {
        try
        {
            OleDbConnection Conexion = new OleDbConnection(m_CadenaConexion);
            Conexion.Open();
            return Conexion.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        }
        catch (Exception ex)
        {
            eClockBase.CeC_Log.AgregaError(ex);
        }
        return null;
    }

}
