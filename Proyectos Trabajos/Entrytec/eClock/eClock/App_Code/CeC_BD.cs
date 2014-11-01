#define DEBUG_SQL

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Data.OleDb;

using System.IO;
using System.EnterpriseServices;

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
            CIsLog2.AgregaError("lEjecutaDataSet No Existe comando a procesar ");
        OleDbConnection Conexion = new OleDbConnection(m_CadenaConexion);
        Conexion.Open();
        try
        {
            OleDbCommand Cmd = new OleDbCommand(Comando, Conexion);
#if DEBUG_SQL
            CIsLog2.AgregaLog("EjecutaEscalar  " + Comando);
#endif
            object Ret = Cmd.ExecuteScalar();
#if DEBUG_SQL
            if (Ret != null)
                CIsLog2.AgregaLog("EjecutaEscalar " + Comando + " Ret " + Ret.ToString());
            else
                CIsLog2.AgregaLog("EjecutaEscalar " + Comando + " Ret NULL");
#endif
            Conexion.Dispose();
            return Ret;

        }
        catch (Exception ex)
        {
#if DEBUG_SQL
            Console.WriteLine("EjecutaEscalar Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
            CIsLog2.AgregaError("EjecutaEscalar Comando " + Comando);
            CIsLog2.AgregaError(ex);


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
    public object lEjecutaDataSet(string Comando)
    {
        m_UltimoErrorBD = "";
        if (Comando == "")
            CIsLog2.AgregaError("lEjecutaDataSet No Existe comando a procesar ");
#if DEBUG_SQL
        CIsLog2.AgregaLog("lEjecutaDataSet  " + Comando);
#endif
        if (EsOracle)
        {

            System.Data.OracleClient.OracleConnection Conexion = new System.Data.OracleClient.OracleConnection();
            Conexion.ConnectionString = CeC_BD.CadenaConexionOracle();
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

                CIsLog2.AgregaError("EjecutaDataSet Oracle Comando " + Comando);

                CIsLog2.AgregaError(ex);
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
                CIsLog2.AgregaLog("lEjecutaDataSet  Fin " + Comando);
#endif
                return DS;

            }
            catch (Exception ex)
            {
#if DEBUG_SQL
                Console.WriteLine("EjecutaReader Err {0:G},  {1:G}", ex.Message, ex.StackTrace);
#endif
                m_UltimoErrorBD = ex.Message;
                CIsLog2.AgregaError("EjecutaDataSet Comando " + Comando);

                CIsLog2.AgregaError(ex);
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
            CIsLog2.AgregaError("EjecutaDataSet_Schema Comando " + Comando);

            CIsLog2.AgregaError(ex);
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
            CIsLog2.AgregaError("EjecutaReader Comando " + Comando);
            CIsLog2.AgregaError(ex);
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
            CIsLog2.AgregaError("EjecutaReader Comando " + Comando);
            CIsLog2.AgregaError(ex);
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
            CIsLog2.AgregaLog("EjecutaComando " + Comando);

#endif
            Cmd.CommandTimeout = TimeOutSegundos;
            CeC.Sleep(0);
            int Ret = Cmd.ExecuteNonQuery();
#if DEBUG_SQL
            CIsLog2.AgregaLog("EjecutaComando " + Ret);
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
                CIsLog2.AgregaError("EjecutaComando Comando " + Comando);
            Conexion.Dispose();
            if (AgregaError)
                CIsLog2.AgregaError(ex);
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
            CIsLog2.AgregaLog("EjecutaComando " + Comando);
#endif
            Cmd.CommandTimeout = TimeOutSegundos;
            Cmd.Parameters.Add(new System.Data.OleDb.OleDbParameter(NombreParametro, System.Data.OleDb.OleDbType.VarChar, LenEnBD, NombreParametro));
            Cmd.Parameters[0].Value = ValorParametro;
            int Ret = Cmd.ExecuteNonQuery();
#if DEBUG_SQL
            CIsLog2.AgregaLog("EjecutaComando " + Ret);
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
            CIsLog2.AgregaError("EjecutaComandoRTexto CadenaConexion " + CadenaConexion + ", Comando " + Comando);
            Conexion.Dispose();
            CIsLog2.AgregaError(ex);
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
            CIsLog2.AgregaLog("EjecutaComando " + Comando);
#endif
            Cmd.CommandTimeout = TimeOutSegundos;
            CeC.Sleep(0);
            int Ret = Cmd.ExecuteNonQuery();
#if DEBUG_SQL
            CIsLog2.AgregaLog("EjecutaComando Ret" + Ret);
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
            CIsLog2.AgregaError("EjecutaComando CadenaConexion " + CadenaConexion + ", Comando " + Comando);

            Conexion.Dispose();
            CIsLog2.AgregaError(ex);
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
                CIsLog2.AgregaError(ex);
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
            CIsLog2.AgregaError(ex);
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
        try
        {
            CadenaConexionStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //            System.Web.Configuration.
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            try
            {

                System.Configuration.Configuration WebConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                System.Configuration.ConnectionStringSettings ConStrings = WebConfiguration.ConnectionStrings.ConnectionStrings["ConnectionString"];
                CadenaConexionStr = ConStrings.ConnectionString;
            }
            catch
            {
                System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
                string Cadena = ((string)(configurationAppSettings.GetValue("gBDatos.ConnectionString", typeof(string))));
                CadenaConexionStr = Cadena;
                return Cadena;
            }


        }
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
    /// <summary>
    /// 
    /// </summary>
    public static void IniciaBase()
    {
        IniciaBase(false);
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
    /// <summary>
    /// Compara en un archivo si es SQL leerá el Archivo eCMSSQL.sql en caso de que sea ORACLE
    /// leera eCORACLE.txt, calcula el hash de este archivo, e iniciara la base
    /// </summary>
    /// <param name="Forza"></param>
    public static void IniciaBase(bool Forza)
    {
        string archivo = "";
        ///Si es eClock no ejecutar esto ahora se hará en eClock Services
        return;
        if (!CeC_BD.EsOracle)
        {
            archivo += ObtenSentencias(HttpRuntime.AppDomainAppPath + "eC_Actualizacion_Sql.sql");
            archivo += ";" + ObtenSentencias(HttpRuntime.AppDomainAppPath + "eCMSSQL.sql");
            archivo += ";" + ObtenSentencias(HttpRuntime.AppDomainAppPath + "eCVMSSQL.sql");
        }
        else
        {
            archivo += ObtenSentencias(HttpRuntime.AppDomainAppPath + "eC_Actualizacion_Oracle.sql");
            archivo += ";" + ObtenSentencias(HttpRuntime.AppDomainAppPath + "eCORACLE.sql");
            archivo += ";" + ObtenSentencias(HttpRuntime.AppDomainAppPath + "eCVORACLE.sql");
        }
        string HashBD = CalculaHash(archivo);
        if (!Forza && HashBD == CeC_Config.HashQuiery_IniciaBase)
            return;
        CeC_Config.HashQuiery_IniciaBase = HashBD;
        string[] Codigo = archivo.Split(new char[] { ';' });
        string ArchivoLog = CIsLog2.S_NombreDestino;
        CIsLog2.S_NombreDestino += ".ActBD.log";
        foreach (string linea in Codigo)
        {
            string NLinea = linea; // linea.Replace("GO", "");
            NLinea = NLinea.Trim();
            if (NLinea.Length >= 2 && NLinea.Substring(0, 2) == "GO")
                NLinea = NLinea.Substring(2);
            NLinea = NLinea.Trim();
            EjecutaComando(NLinea);
            CeC.Sleep(0);
        }
        if (EsOracle)
            EjecutaParcheOracle();
        CeC_Tablas.IniciaTablas(null);
        CIsLog2.S_NombreDestino = ArchivoLog;
        /*
        foreach (string linea2 in Codigo)
        {
            EjecutaComando(linea2);
            CeC.Sleep(1);
        }    */



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
                EjecutaComando("alter table \"" + CeC.Convierte2String(DR["TABLE_NAME"]) + "\" modify (\"" +
                    CeC.Convierte2String(DR["COLUMN_NAME"]) + "\" VARCHAR2(" + CeC.Convierte2String(DR["DATA_LENGTH"]) + ") NULL)");
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
        string Cadena = CadenaConexion();
        //string CadenaMayus = Cadena.ToUpper();

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
            CIsLog2.AgregaError(e);


        }
        return null;
    }



    /// <summary>
    /// Obtiene el nombre a partir del ID persona
    /// </summary>
    public static string ObtenPersonaNombre(int Persona_ID)
    {
        string Qry = "SELECT PERSONA_NOMBRE FROM EC_PERSONAS WHERE   PERSONA_ID = " + Persona_ID.ToString() + "";
        return EjecutaEscalarString(Qry);
    }

    /// <summary>
    /// Obtiene el numero de personas activas en la base de datos
    /// </summary>
    public static int ObtenNoPersonas()
    {
        string Qry = "SELECT Count(persona_ID) FROM EC_PERSONAS WHERE   PERSONA_BORRADO = 0";
        return EjecutaEscalarInt(Qry);
    }
    /// <summary>
    /// Obtiene el correo electrónico a partir del ID persona
    /// </summary>
    public static string ObtenPersonaEMail(int Persona_ID)
    {
        string Qry = "SELECT PERSONA_EMAIL FROM EC_PERSONAS WHERE   PERSONA_ID = " + Persona_ID.ToString() + "";
        return EjecutaEscalarString(Qry);
    }

    public static DataSet ObtenPersonas(string Filtro)
    {
        string Qry = "SELECT PERSONA_NOMBRE FROM EC_PERSONAS, EC_PERSONAS_DATOS WHERE PERSONA_LINK_ID = " + CeC_Campos.CampoTE_Llave;
        if (Filtro.Length > 0)
        {
            Qry += " AND " + Filtro;
        }
        object obj = CeC_BD.EjecutaDataSet(Qry);
        DataSet DTS = (DataSet)obj;
        CeC_Campos.AplicaFormatoDataset(DTS);
        return DTS;
    }
    public static DataSet ObtenPersonasYPersonaLink(string Filtro)
    {
        string Qry = "SELECT PERSONA_NOMBRE, PERSONA_LINK_ID FROM EC_PERSONAS, EC_PERSONAS_DATOS WHERE PERSONA_LINK_ID = " + CeC_Campos.CampoTE_Llave;
        if (Filtro.Length > 0)
        {
            Qry += " AND " + Filtro;
        }
        object obj = CeC_BD.EjecutaDataSet(Qry);
        DataSet DTS = (DataSet)obj;
        CeC_Campos.AplicaFormatoDataset(DTS);
        return DTS;
    }

    /// <summary>
    /// Obtiene el persona Link ID a partir del ID persona
    /// </summary>
    public static int ObtenPersonaLinkID(int Persona_ID)
    {
        string Qry = "SELECT PERSONA_LINK_ID FROM EC_PERSONAS WHERE PERSONA_ID = " + Persona_ID.ToString() + "";
        return EjecutaEscalarInt(Qry);
    }

    /// <summary>
    /// Obtiene el Grupo a partir del Persona_ID
    /// </summary>
    public static int ObtenPersonaSUSCRIPCION_ID(int Persona_ID)
    {
        string Qry = "SELECT SUSCRIPCION_ID FROM EC_PERSONAS WHERE   PERSONA_ID = " + Persona_ID.ToString() + "";
        return EjecutaEscalarInt(Qry);
    }

    /********************************************************************
            Creado:	13:2:2006   20:59
            Nombre de Función: 	BorraRowBatch
            Autor:	Moisés Trejo	
            Parámetros:
            Propósito:	Ejecutar automáticamente el comando de delete de el dataadapter introducido
        *********************************************************************/
    public static string BorraRowBatch(Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid, Infragistics.WebUI.UltraWebGrid.RowEventArgs e, System.Data.DataSet DS, string Tabla, System.Data.OleDb.OleDbDataAdapter DA)
    {
        string Retorno = "";
        try
        {

            string dataKey = "";
            if (e.Row.DataKey != null)
                dataKey = (string)e.Row.DataKey.ToString();

            if (dataKey.Length > 0)
            {
                // First we locate the row in the DataTable object with the corresponding key.                        

                System.Data.DataRow dr = DS.Tables[Tabla].Rows.Find(new object[] { dataKey });
                System.Data.OleDb.OleDbCommand Borrar = DA.DeleteCommand;
                foreach (OleDbParameter Parametro in Borrar.Parameters)
                {
                    if (Parametro.Direction == System.Data.ParameterDirection.Input || Parametro.Direction == System.Data.ParameterDirection.InputOutput)
                    {
                        Parametro.Value = dr[Parametro.SourceColumn];
                    }

                }

                DS.Tables[Tabla].Rows.Remove(dr);
                Borrar.ExecuteNonQuery();

                //dr.Delete();
            }
        }
        catch (Exception ex)
        {
            Retorno = ex.Message;
            CIsLog2.AgregaError(ex);
        }
        return Retorno;
    }


    /********************************************************************
            Creado:	13:2:2006   19:57
            Nombre de Función: 	ActualizaRowBatch
            Autor:	Moises Trejo
            Parámetros:
            Propósito:	Esta funciona se deberá ejecutar siempre que se quieran guardar registros en forma batch 
        cuado sea una tabla simple, 
        *********************************************************************/
    public static string ActualizaRowBatch(Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid, Infragistics.WebUI.UltraWebGrid.RowEventArgs e, System.Data.DataSet DS, string Tabla, string ID)
    {
        string Retorno = "";
        try
        {
            if (e.Data.GetType().Name != "UltraGridRow")
            {
            }

            // The input argument e.Data contains the data before the modification is made. 
            Infragistics.WebUI.UltraWebGrid.UltraGridRow oldRow = (Infragistics.WebUI.UltraWebGrid.UltraGridRow)e.Data;

            // The update row event argument e.Row contains the value of the row as it was returned to the server.  With this information
            // it would be possible to begin updating the row object in the DataTable

            // The DataKey of a row is designed to hold the primary key information for the row object.  This value is taken from the DataKeyField set in the 
            // InitializeLayout event.
            // In this case, the DataKeyField is the CompanyID, a string value
            string dataKey = "";
            if (e.Row.DataKey != null)
                dataKey = (string)e.Row.DataKey.ToString();


            switch (e.Row.DataChanged)
            {
                // In the case of an added row, no row will be found in the underlying datasource to modify, so a new row object needs to be made to be added to the collection
                case (Infragistics.WebUI.UltraWebGrid.DataChanged.Added):
                    // make a new DataRow object
                    System.Data.DataRow newDataTableRow = DS.Tables[Tabla].NewRow();

                    // populate the DataRow object with values from the UltraGridRow object
                    // When using a bound grid, the UltraGridColumn.Key value is set to the name of the column from the bound source.
                    // Microsoft DataRow objects are indexable by their column name, so we can leverage this to quickly loop through
                    // all the UltraGridCells and move their values into the DataRow object.  There are other techniques and methodologies that
                    // can be followed should this not be robust enough.
                    foreach (Infragistics.WebUI.UltraWebGrid.UltraGridCell c in e.Row.Cells)
                        if (c.Value != null)
                            newDataTableRow[c.Column.Key] = c.Value;
                    if (newDataTableRow[0] == null)
                    {
                        newDataTableRow[0] = CeC_Autonumerico.GeneraAutonumerico(Tabla, ID);
                    }

                    // Once the DataRow object is populated, add the row to the DataTable object
                    DS.Tables[Tabla].Rows.Add(newDataTableRow);

                    // In this case, the CompanyID is a text string that would have to be populated by user.  However there are cases
                    // where the value for the DataKey of a row would not be known at this present moment.  These cases include when the primary key for 
                    // a row would be generated by the database server via an autoincrementing value or some other calculated manner.  In these cases, the following 
                    // code would need to be moved to a point after the DataAdapter.Update (or a corresponding moment if using a custom business object)

                    // Since we know the DataKey at this time, we assign it to the UltraGridRow object.
                    e.Row.DataKey = e.Row.Cells.FromKey(ID).Value;

                    break;
                case (Infragistics.WebUI.UltraWebGrid.DataChanged.Modified):
                    // in the case of an updated row, we find the row in a manner similar to how we want to find a row for deletion,
                    // except instead of removing the row we update the cell values for the row
                    if (dataKey.Length > 0)
                    {
                        // First we locate the row in the DataTable object with the corresponding key.                        
                        System.Data.DataRow dr = DS.Tables[Tabla].Rows.Find(new object[] { dataKey });

                        if (null != dr)
                            // The update code is similar to that used inside of the Add case.
                            foreach (Infragistics.WebUI.UltraWebGrid.UltraGridCell c in e.Row.Cells)
                                if (!dr[c.Column.Key].Equals(c.Value)) // one can check to see if the cell's .DataChanged propery is set to true
                                    if (c.Value != null)
                                        dr[c.Column.Key] = c.Value;

                        // Normally you would not allow modification of the value that is used to populate your DataKey as this most
                        // often corresponds to your primary key.  However, should you allow for the field that is used for the DataKey, you should
                        // update this value.                        
                        e.Row.DataKey = e.Row.Cells.FromKey(ID).Value;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            Retorno = ex.Message;
            CIsLog2.AgregaError(ex);
        }
        return Retorno;
    }
    /// <summary>
    /// Actualiza la Tabla EC_TURNOS_DIA, actualizando las horas de entrada,salida
    /// hora de regreso de comer, salida de comer, hora de entrada/salida permitida, caso de 
    /// no actulizarlar generara un error
    /// </summary>
    /// <param name="ID_Turnos_Dia">ID_Turnos_Dia</param>
    /// <param name="HORAS_ENTRADA">Hora de Entrada</param>
    /// <param name="HORAS_SALIDA">Hora de Salida</param>
    /// <param name="HORAS_HCR">Hora regreso de comer</param>
    /// <param name="HORAS_HCS">Hora salida de comer</param>
    /// <returns></returns>
    public static bool Guardar_Calculos_Flash1(int ID_Turnos_Dia, DateTime HORAS_ENTRADA, DateTime HORAS_SALIDA, DateTime HORAS_HCR, DateTime HORAS_HCS)
    {
        //int  HAYCOMIDA = 1;
        try
        {

            DateTime HE = HORAS_ENTRADA;
            DateTime HS = HORAS_SALIDA;
            DateTime HCR = HORAS_HCR;
            DateTime HCS = HORAS_HCS;
            DateTime HEMIN = HE.AddHours(-3);
            DateTime HEMAX = HE.AddHours(+3);
            DateTime HSMAX = HS.AddHours(+6);
            TimeSpan HCTIEMPO_T = HCR - HCS;
            TimeSpan HTIEMPO_T = HS - HE;
            DateTime HSMIN = HS.AddHours(-3);

            DateTime HCTIEMPO = new DateTime(HCR.Year, HCR.Month, HCR.Day, HCTIEMPO_T.Hours, HCTIEMPO_T.Minutes, 0);
            DateTime HTIEMPO = new DateTime(HE.Year, HE.Month, HE.Day, HTIEMPO_T.Hours, HTIEMPO_T.Minutes, 0);

            /*	string HSMAX_STR = HCTIEMPO.Year+"/"+HCTIEMPO.Month+"/"+HCTIEMPO.Day+" "+HCTIEMPO.Hour+":"+HCTIEMPO.Minute;
                string HEMIN_STR  = HEMIN.Year +"/"+HEMIN.Month+"/"+HEMIN.Day+" "+ HEMIN.Hour+":"+HEMIN.Minute;
                string HCTIEMPO_STR = HCTIEMPO.Year+"/"+HCTIEMPO.Month+"/"+HCTIEMPO.Day+" "+HCTIEMPO.Hour+":"+HCTIEMPO.Minute;
                string HTIEMPO_STR = HTIEMPO.Year +"/"+HTIEMPO.Month+"/"+HTIEMPO.Day+" "+ HTIEMPO.Hour+":"+HTIEMPO.Minute;
                string HSMIN_STR  = HSMIN.Year+"/"+HSMIN.Month+"/"+HSMIN.Day+" "+ HSMIN.Hour+":"+HSMIN.Minute;
        */
            string Query = "Update EC_TURNOS_DIA SET " +
                " EC_TURNOS_DIA.TURNO_DIA_HSMAX  = " + CeC_BD.SqlFechaHora(HSMAX) + "," +
                " EC_TURNOS_DIA.TURNO_DIA_HEMIN = " + CeC_BD.SqlFechaHora(HEMIN) + "," +
                //			" EC_TURNOS_DIA.TURNO_DIA_HEMAX = " + CeC_BD.SqlFechaHora(HEMAX)+ ","+
                " EC_TURNOS_DIA.TURNO_DIA_HAYCOMIDA = 1 ," +
                " EC_TURNOS_DIA.TURNO_DIA_HCTIEMPO = " + CeC_BD.SqlFechaHora(HCTIEMPO) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HTIEMPO = " + CeC_BD.SqlFechaHora(HTIEMPO) + "," +
                " EC_TURNOS_DIA.TURNO_DIA_HSMIN = " + CeC_BD.SqlFechaHora(HSMIN) + "" +
                " WHERE EC_TURNOS_DIA.TURNO_DIA_ID = " + ID_Turnos_Dia;

            CeC_BD.EjecutaComando(Query);
        }
        catch (Exception ex)
        {
            string Err = " Error : " + ex.Message;
            CIsLog2.AgregaError(ex);
            return false;
        }
        return true;
    }
    /// <summary>
    /// Actualiza la Tabla EC_TURNOS_DIA, actualizando las horas de entrada,salida
    /// hora de regreso de comer, salida de comer, hora de entrada/salida permitida, en caso de 
    /// no actulizarlar generara un error
    /// </summary>
    /// <param name="ID_Turnos_Dia">ID_Turnos_Dia</param>
    /// <param name="HE">Horas Extra</param>
    /// <param name="HCS">Hora Salida Comer</param>
    /// <param name="HCR">Hora Regreso Comer</param>
    /// <param name="HORASRETARDO">Horas de Retardo</param>
    /// <param name="HORASBLOQUET">Horas de Tolerancia por Bloque</param>
    /// <param name="HORASBLOQUE">Horas de Bloque Acceso</param>
    /// <param name="HORASTIEMPO"></param>
    /// <returns></returns>
    public static bool Guardar_Calculos_Flash2(int ID_Turnos_Dia, DateTime HE, DateTime HCS, DateTime HCR, DateTime HORASRETARDO, TimeSpan HORASBLOQUET, TimeSpan HORASBLOQUE, TimeSpan HORASTIEMPO)
    {
        try
        {

            DateTime HEMIN = HE.AddHours(-3);
            DateTime HERETARDO = HORASRETARDO;
            DateTime HEMAX = HERETARDO + HORASBLOQUET;
            DateTime HBLOQUE = TimeSpan2DateTime(HORASBLOQUE);
            DateTime HBLOQUET = TimeSpan2DateTime(HORASBLOQUET);

            DateTime HTIEMPO = TimeSpan2DateTime(HORASTIEMPO);
            DateTime HS = HE + HORASTIEMPO;
            DateTime HSMAX = HERETARDO + HORASTIEMPO;
            HSMAX = HSMAX.AddHours(6);

            DateTime HSMIN = HS;
            DateTime HCTIEMPO = TimeSpan2DateTime(HCR - HCS);
            DateTime HCTOLERA = CeC_BD.FechaNula;



            string Query = "Update EC_TURNOS_DIA SET " +

                " EC_TURNOS_DIA.TURNO_DIA_HEMIN  = " + SqlFechaHora(HEMIN) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HE  = " + SqlFechaHora(HE) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HERETARDO  = " + SqlFechaHora(HERETARDO) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HEMAX  = " + SqlFechaHora(HEMAX) + ", " +

                " EC_TURNOS_DIA.TURNO_DIA_HSMIN  = " + SqlFechaHora(HSMIN) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HS  = " + SqlFechaHora(HS) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HSMAX  = " + SqlFechaHora(HSMAX) + ", " +

                " EC_TURNOS_DIA.TURNO_DIA_HBLOQUE  = " + SqlFechaHora(HBLOQUE) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HBLOQUET  = " + SqlFechaHora(HBLOQUET) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HTIEMPO  = " + SqlFechaHora(HTIEMPO) + ", " +

                " EC_TURNOS_DIA.TURNO_DIA_HAYCOMIDA = 1 ," +

                " EC_TURNOS_DIA.TURNO_DIA_HCS  = " + SqlFechaHora(HCS) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HCR  = " + SqlFechaHora(HCR) + ", " +

                " EC_TURNOS_DIA.TURNO_DIA_HCTIEMPO  = " + SqlFechaHora(HCTIEMPO) + ", " +
                " EC_TURNOS_DIA.TURNO_DIA_HCTOLERA  = " + SqlFechaHora(HCTOLERA) + " " +

                " WHERE EC_TURNOS_DIA.TURNO_DIA_ID = " + ID_Turnos_Dia;

            CeC_BD.EjecutaComando(Query);

        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            string error = ex.Message;
        }


        return true;
    }




    #region Parámetros de Terminales
    /// <summary>
    /// Guarda un parámetro de configuración para las terminales en la base de datos del sistema
    /// </summary>
    public static bool GuardaParamTerminal(int Terminal_ID, string Variable, int Valor)
    {
        return GuardaParamTerminal(Terminal_ID, Variable, Valor.ToString());
    }
    /// <summary>
    /// Guarda un parámetro de configuración para las terminales en la base de datos del sistema
    /// </summary>
    public static bool GuardaParamTerminal(int Terminal_ID, string Variable, string Valor)
    {
        try
        {
            int Registros = CeC_BD.EjecutaComando("UPDATE EC_TERMINALES_PARAM SET TERMINALES_PARAM_VALOR = '" + Valor + "' WHERE TERMINAL_ID = " + Terminal_ID.ToString() + " AND TERMINALES_PARAM_NOMBRE = '" + Variable + "'");
            if (Registros <= 0)
                Registros = CeC_BD.EjecutaComando("INSERT INTO EC_TERMINALES_PARAM (TERMINALES_PARAM_ID, TERMINAL_ID, TERMINALES_PARAM_NOMBRE, TERMINALES_PARAM_VALOR) VALUES( " +
                    CeC_Autonumerico.GeneraAutonumerico("EC_TERMINALES_PARAM", "TERMINALES_PARAM_ID").ToString() + ", " + Terminal_ID.ToString() + " , '" + Variable + "', '" + Valor + "')");
            if (Registros > 0)
                return true;
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
        }

        return false;
    }

    /// <summary>
    /// Obtiene un parámetro de configuración de las terminales almacenada en la base de datos 
    /// </summary>
    public static int ObtenParamTerminal(int Terminal_ID, string Variable, int ValorDefecto)
    {
        return Convert.ToInt32(ObtenParamTerminal(Terminal_ID, Variable, ValorDefecto.ToString()));
    }
    /// <summary>
    /// Obtiene un arreglo de bytes de la tabla EC_PERSONAS_TERMINALES en la base de datos, dado el id de persona
    /// y de terminal
    /// </summary>
    /// <param name="Persona_ID">Id de Persona</param>
    /// <param name="Terminal_ID">Id del terminal</param>
    /// <returns></returns>
    public static byte[] ObtenPersonaTerminal(int Persona_ID, int Terminal_ID)
    {

        System.Data.OracleClient.OracleConnection Conexion = new System.Data.OracleClient.OracleConnection();
        Conexion.ConnectionString = CeC_BD.CadenaConexionOracle();
        try
        {
            Conexion.Open();
            System.Data.OracleClient.OracleCommand Selecciona = Conexion.CreateCommand();
            Selecciona.CommandText = "SELECT PERSONAS_TERMINALES_DATO1 FROM EC_PERSONAS_TERMINALES WHERE PERSONA_ID = " + Persona_ID + " AND TERMINAL_ID = " + Terminal_ID;
            object Obj = Selecciona.ExecuteScalar();
            if (Obj != System.DBNull.Value)
            {
                System.Array Arreglo = (System.Array)Obj;
                byte[] Dato = new byte[Arreglo.Length];
                Array.Copy(Arreglo, Dato, Arreglo.Length);
                return Dato;
            }
            Conexion.Dispose();
        }
        catch (Exception ex)
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Dispose();
            CIsLog2.AgregaError(ex);
            string Mensage = ex.Message;

        }

        return null;

    }


    /// <summary>
    /// Obtiene un parámetro de configuración de las terminales almacenada en la base de datos 
    /// </summary>
    public static string ObtenParamTerminal(int Terminal_ID, string Variable, string ValorDefecto)
    {
        try
        {
            object Valor = CeC_BD.EjecutaEscalar("SELECT TERMINALES_PARAM_VALOR FROM EC_TERMINALES_PARAM WHERE TERMINALES_PARAM_NOMBRE = '" + Variable + "' AND TERMINAL_ID = " + Terminal_ID.ToString());
            if (Valor == null)
                return ValorDefecto;
            return Convert.ToString(Valor);
        }
        catch (Exception ex)
        {
            CIsLog2.AgregaError(ex);
            return ValorDefecto;
        }
    }

    /// <summary>
    /// Obtiene el Turno_ID alamcenada en la base de datos
    /// </summary>
    /// <param name="Turno_Nombre">Nombre del Terminal</param>
    /// <returns></returns>
    public static int ObtenTurnoID(string Turno_Nombre)
    {
        string Qry = "SELECT TURNO_ID FROM EC_TURNOS WHERE   TURNO_NOMBRE LIKE '" + Turno_Nombre + "'";
        return EjecutaEscalarInt(Qry);
    }
    /// <summary>
    /// Asigna nombres a Combo de Infragistics
    /// </summary>
    /// <param name="WC">WebControl</param>
    public static void AsignaGruposACombo(Infragistics.WebUI.WebCombo.WebCombo WC)
    {
        DataSet DS = new DataSet();
        DataTable DT = DS.Tables.Add();
        DT.TableName = "Tabla";
        DT.Columns.Add("Grupo_No", typeof(System.Int32));
        DT.Columns.Add("Grupo_Nombre", typeof(System.String));
        if (CeC_Config.CampoGrupoSeleccionado >= 1)
            DT.Rows.Add(new object[] { 1, CeC_Config.NombreGrupo1 });
        if (CeC_Config.CampoGrupoSeleccionado >= 2)
            DT.Rows.Add(new object[] { 2, CeC_Config.NombreGrupo2 });
        if (CeC_Config.CampoGrupoSeleccionado >= 3)
            DT.Rows.Add(new object[] { 3, CeC_Config.NombreGrupo3 });
        WC.DataSource = DS;
        WC.DataTextField = "Grupo_Nombre";
        //    WC.DataMember = DS.Tables[0].Columns[0].ColumnName;

        if (DS.Tables[0].Columns.Count > 1)
        {
            WC.DisplayValue = DS.Tables[0].Columns[1].ColumnName;
            WC.DataTextField = DS.Tables[0].Columns[1].ColumnName;
        }
        else
        {
            WC.DataTextField = DS.Tables[0].Columns[0].ColumnName;
            WC.DisplayValue = DS.Tables[0].Columns[0].ColumnName;
        }
        WC.DataValueField = DS.Tables[0].Columns[0].ColumnName;
        WC.DataBind();
        WC.SelectedRow = WC.Rows[0];

    }
    #endregion

    #region Manejo Binarios
    /// <summary>
    /// Asigna Imagen a CONFIGURACION DE USUARIO
    /// </summary>
    /// <param name="Datos"></param>
    /// <param name="Config_Usuario_ID"></param>
    /// <param name="nomimagen"></param>
    /// <returns></returns>
    public static bool AsignaImagen(byte[] Datos, int Config_Usuario_ID, string nomimagen)
    {
        return AsignaBinario("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID", Config_Usuario_ID, "CONFIG_USUARIO_VALOR_BIN", Datos);
    }
    /// <summary>
    /// Obtiene la imagen de configuracion para login encabezados o reportes
    /// </summary>
    /// <param name="numimagen"></param>
    /// <returns></returns>

    public static byte[] ObtenImagen(string nomimagen)
    {
        int Config_Usuario_ID = EjecutaEscalarInt("SELECT CONFIG_USUARIO_ID FROM EC_CONFIG_USUARIO WHERE CONFIG_USUARIO_VARIABLE = '" + nomimagen + "'  AND USUARIO_ID = 0");

        if (Config_Usuario_ID <= 0)
            return null;
        return ObtenBinario("EC_CONFIG_USUARIO", "CONFIG_USUARIO_ID", Config_Usuario_ID, "CONFIG_USUARIO_VALOR_BIN");
    }

    /// <summary>
    /// Asigan un Binario dependiendo si es Oracle u OLEDB
    /// a los parametros
    /// </summary>
    /// <param name="Tabla">Nombre de Tabala</param>
    /// <param name="CampoLlave1">Nombre de Campo Llave1</param>
    /// <param name="Llave1"></param>
    /// <param name="CampoLlave2"></param>
    /// <param name="Llave2"></param>
    /// <param name="CampoBinario"></param>
    /// <param name="Binario"></param>
    /// <returns></returns>
    public static bool AsignaBinario(string Tabla, string CampoLlave1, int Llave1, string CampoLlave2, int Llave2, string CampoBinario, byte[] Binario)
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
                                               " SET  " + CampoBinario + " = :IMAGEN WHERE " + CampoLlave1 + " = " + Llave1.ToString() + " AND " + CampoLlave2 + " = " + Llave2.ToString();
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
                CIsLog2.AgregaError(ex);
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
                " SET  " + CampoBinario + " = ? WHERE " + CampoLlave1 + " = " + Llave1.ToString() + " AND " + CampoLlave2 + " = " + Llave2.ToString();
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

            Comando.ExecuteNonQuery();
            Conexion.Close();
            return true;
        }
        catch (Exception ex)
        {
            string Error;
            Error = ex.Message;
            CIsLog2.AgregaError(ex);
            //			CIsLog2.AgregaError(ex);
        }

        return false;


    }
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
        if (EsOracle)
        {

            System.Data.OracleClient.OracleConnection Conexion = new System.Data.OracleClient.OracleConnection();
            Conexion.ConnectionString = CeC_BD.CadenaConexionOracle();
            try
            {
                Conexion.Open();
                System.Data.OracleClient.OracleCommand Selecciona = Conexion.CreateCommand();
                Selecciona.CommandText = "UPDATE " + Tabla +
                                               " SET  " + CampoBinario + " = :IMAGEN WHERE " + CampoLlave + " = " + Llave.ToString();
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
                CIsLog2.AgregaError(ex);
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
                " SET  " + CampoBinario + " = ? WHERE " + CampoLlave + " = " + Llave.ToString();
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

            CIsLog2.AgregaError(ex);
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
                CIsLog2.AgregaError(ex);
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
                    CIsLog2.AgregaError(ex);
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




    /// <summary>
    /// Regresa el valor de la etiqueta del campo llave
    /// </summary>
    /// <param name="TerminalID"></param>
    /// <returns></returns>
    public static string EtiquetaLlave(int TerminalID)
    {
        string qry = "SELECT TERMINAL_CAMPO_LLAVE FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + " AND TERMINAL_BORRADO=0";
        string CampoLlave = EjecutaEscalarString(qry);
        return CeC_Campos.ObtenEtiqueta(CampoLlave);
    }
    /// <summary>
    /// Regresa el valor unico del campo adicional de EC_TERMINALES
    /// </summary>
    /// <param name="TerminalID"></param>
    /// <returns></returns>
    public static string EtiquetaLlaveAdicional(int TerminalID)
    {
        string qry = "SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString() + " AND TERMINAL_BORRADO=0";
        string CampoLlave = EjecutaEscalarString(qry);
        return CeC_Campos.ObtenEtiqueta(CampoLlave);
    }
    /// <summary>
    /// Regresa el valor de la PERSONAID
    /// </summary>
    /// <param name="TerminalID"></param>
    /// <param name="ValorCampoAdicional"></param>
    /// <returns></returns>
    public static int PersonaID(int TerminalID, string ValorCampoAdicional)
    {
        string qry = "SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString();
        string CampoAdicional = EjecutaEscalarString(qry);
        qry = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID IN (SELECT " + CeC_Campos.CampoTE_Llave + " AS PERSONA_LINK_ID FROM EC_PERSONAS_DATOS WHERE " + CampoAdicional + " = '" + ValorCampoAdicional + "')";
        return EjecutaEscalarInt(qry);
    }
    /// <summary>
    /// Regresa el valor de PersonaID
    /// </summary>
    /// <param name="TerminalID"></param>
    /// <param name="ValorCampo"></param>
    /// <param name="ValorCampoAdicional"></param>
    /// <returns></returns>
    public static int PersonaID(int TerminalID, string ValorCampo, out string ValorCampoAdicional)
    {
        string qry = "SELECT TERMINAL_CAMPO_LLAVE FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString();
        string CampoLlave = EjecutaEscalarString(qry);
        qry = "SELECT TERMINAL_CAMPO_ADICIONAL FROM EC_TERMINALES WHERE TERMINAL_ID = " + TerminalID.ToString();
        string CampoAdicional = EjecutaEscalarString(qry);
        qry = "SELECT PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_LINK_ID IN (SELECT " + CeC_Campos.CampoTE_Llave + " AS PERSONA_LINK_ID FROM EC_PERSONAS_DATOS WHERE " + CampoLlave + " = '" + ValorCampo + "')";
        int PersonaID = EjecutaEscalarInt(qry);
        if (PersonaID != -9999)
        {
            qry = "SELECT " + CampoAdicional + " FROM EC_PERSONAS_DATOS WHERE " + CampoLlave + " = " + ValorCampo;
            ValorCampoAdicional = EjecutaEscalarString(qry);
            return PersonaID;
        }
        else
        {
            ValorCampoAdicional = "";
            return PersonaID;
        }
    }
    /// <summary>
    /// Regresa el valor de la cadena de Campo Llave
    /// </summary>
    /// <param name="PersonaID"></param>
    /// <returns></returns>
    public static string ValorCampoLlave(int PersonaID)
    {
        string qry = "SELECT PERSONA_LINK_ID FROM EC_PERSONAS WHERE PERSONA_ID = " + PersonaID.ToString();
        int PersonaLinkID = EjecutaEscalarInt(qry);
        qry = "SELECT " + CeC_Campos.CampoTE_Llave + " FROM EC_PERSONAS_DATOS WHERE " + CeC_Campos.CampoTE_Llave + " = '" + PersonaLinkID.ToString() + "'";
        return EjecutaEscalarString(qry);
    }
    /// <summary>
    /// Registra los accesos (persona,terminal,fecha y hora,tipo de acceso)
    /// </summary>
    /// <param name="DataSetAccesos"></param>
    public static void RegistrarAccesos(ref DS_WSAccesos DataSetAccesos)
    {
        string qry;
        int AccesoID;
        int i = 0;
        while (i < DataSetAccesos.DT_Accesos.Rows.Count)
        {
            AccesoID = CeC_Autonumerico.GeneraAutonumerico("EC_ACCESOS", "ACCESO_ID");
            qry = "INSERT INTO EC_ACCESOS(ACCESO_ID,PERSONA_ID,TERMINAL_ID,ACCESO_FECHAHORA,TIPO_ACCESO_ID) VALUES(" + AccesoID.ToString() + "," + DataSetAccesos.DT_Accesos[i][DataSetAccesos.DT_Accesos.PERSONA_IDColumn.Caption].ToString() + "," + DataSetAccesos.DT_Accesos[i][DataSetAccesos.DT_Accesos.TERMINAL_IDColumn.Caption].ToString() + "," + CeC_BD.SqlFechaHora(Convert.ToDateTime(DataSetAccesos.DT_Accesos[i][DataSetAccesos.DT_Accesos.ACCESO_FECHAHORAColumn.Caption])) + "," + DataSetAccesos.DT_Accesos[i][DataSetAccesos.DT_Accesos.TIPO_ACCESO_IDColumn.Caption].ToString() + ")";
            int ret = CeC_BD.EjecutaComando(qry);
            if (ret > 0)
            {
                DataSetAccesos.DT_Accesos.RemoveDT_AccesosRow(DataSetAccesos.DT_Accesos[i]);
            }
            else
            {
                i++;
            }
        }
    }

    /// <summary>
    /// Agrega a la base de datos un mail el cual podra ser enviado por otro proceso
    /// </summary>
    /// <param name="Destinatarios">lista de emails separados por coma a quienes se les enviara el mail</param>
    /// <param name="DestinatariosCopia">lista de emails separados por coma a quienes se les enviara copia del mail</param>
    /// <param name="Titulo">Titulo del mail</param>
    /// <param name="Mensaje">Contenido del Mail</param>
    /// <param name="Adjunto">Archivo ajunto en caso de no tener ser'a nulo</param>
    /// <returns>regresa verdadero si se pudo agregar</returns>
    public static bool EnviarMail(string Destinatarios, string DestinatariosCopia, string Titulo, string Mensaje, byte[] Adjunto)
    {
        try
        {

            int eC_MAIL_ID = CeC_Autonumerico.GeneraAutonumerico("EC_MAILS", "MAIL_ID");
            if (eC_MAIL_ID < 0)
                return false;
            Destinatarios = Destinatarios.Replace("'", "''");
            DestinatariosCopia = DestinatariosCopia.Replace("'", "''");
            Titulo = Titulo.Replace("'", "''");
            Mensaje = Mensaje.Replace("'", "''");
            int No = EjecutaComando("INSERT INTO EC_MAILS (MAIL_ID, MAIL_DESTINOS, " +
                " MAIL_COPIAS, MAIL_TITULO, MAIL_MENSAJE" +
                ", MAIL_FECHAHORA) VALUES( " + eC_MAIL_ID + ", '" + Destinatarios + "', '" + DestinatariosCopia
             + "', '" + Titulo + "', '" + Mensaje + "', " + SqlFechaHora(DateTime.Now) + ")");
            if (No > 0)
            {
                if (Adjunto != null)
                    AsignaBinario("EC_MAILS", "MAIL_ID", eC_MAIL_ID, "MAIL_ADJUNTO", Adjunto);
                CeC_Asistencias.CambioMailsPendientes();
                return true;
            }
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return false;
    }
    /// <summary>
    /// Agrega a la base de datos un mail el cual podra ser enviado por otro porceso
    /// </summary>
    /// <param name="Destinatarios">lista de emails separados por coma a quienes se les enviara el mail</param>
    /// <param name="DestinatariosCopia">lista de emails separados por coma a quienes se les enviara copia del mail</param>
    /// <param name="Titulo">Titulo del mail</param>
    /// <param name="Mensaje">Contenido del Mail</param>
    /// <param name="AdjuntoRutaRelativa">Archivo ajunto en caso de no tener ser'a ""</param>
    /// <returns></returns>
    public static bool EnviarMail(string Destinatarios, string DestinatariosCopia, string Titulo, string Mensaje, string AdjuntoRutaRelativa)
    {
        return EnviarMail(Destinatarios, DestinatariosCopia, Titulo, Mensaje, AdjuntoRutaRelativa, false);
    }
    public static bool EnviarMail(string Destinatarios, string DestinatariosCopia, string Titulo, string Mensaje, string AdjuntoRutaRelativa, bool EsAutomatico)
    {
        try
        {
            byte[] Archivo = null;
            if (AdjuntoRutaRelativa != "")
                if (EsAutomatico)
                    Archivo = System.IO.File.ReadAllBytes(/*HttpRuntime.AppDomainAppPath +*/ AdjuntoRutaRelativa);
                else
                    Archivo = System.IO.File.ReadAllBytes(HttpRuntime.AppDomainAppPath + AdjuntoRutaRelativa);
            return EnviarMail(Destinatarios, DestinatariosCopia, Titulo, Mensaje, Archivo);
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return false;

    }

    public static bool EnviarMailA(string Destinatarios, string DestinatariosCopia, string Titulo, string Mensaje, string AdjuntoRutaRelativa)
    {
        try
        {
            byte[] Archivo = null;
            if (AdjuntoRutaRelativa != "")
                Archivo = System.IO.File.ReadAllBytes(/*HttpRuntime.AppDomainAppPath +*/ AdjuntoRutaRelativa);
            return EnviarMail(Destinatarios, DestinatariosCopia, Titulo, Mensaje, Archivo);
        }
        catch (Exception ex) { CIsLog2.AgregaError(ex); }
        return false;

    }
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
    /// Agrega a la base de datos un mail el cual podra ser enviado por otro proceso
    /// </summary>
    /// <param name="Destinatarios">lista de emails separados por coma a quienes se les enviara el mail</param>
    /// <param name="Titulo">Titulo del mail</param>
    /// <param name="Mensaje">Contenido del Mail</param>
    /// <returns>regresa verdadero si se pudo agregar</returns>
    public static bool EnviarMail(string Destinatarios, string Titulo, string Mensaje)
    {
        byte[] EnAdjunto = null;
        if (Mensaje.Length > 8000)
        {
            EnAdjunto = ObtenArregloBytes(Mensaje);
            Mensaje = "EN_ADJUNTO";
        }
        return EnviarMail(Destinatarios, "", Titulo, Mensaje, EnAdjunto);
    }
    /// <summary>
    /// Asigna una terminal automaticamente
    /// </summary>
    /// <param name="idPersona"></param>
    public static void AsignaTerminalAuto(int idPersona)
    {
        DS_Terminales DSTerminales = new DS_Terminales();
        DS_TerminalesTableAdapters.DTAsignaTerminalAutoTableAdapter DA = new DS_TerminalesTableAdapters.DTAsignaTerminalAutoTableAdapter();
        DA.Fill(DSTerminales.DTAsignaTerminalAuto);
        String Comando;
        for (int i = 0; i < DSTerminales.DTAsignaTerminalAuto.Rows.Count; i++)
        {
            string Qry = DSTerminales.DTAsignaTerminalAuto[i].SITIO_CONSULTA;
            if (Qry.Trim() != "")
            {
                Comando = "INSERT INTO EC_PERSONAS_TERMINALES (TERMINAL_ID, PERSONA_ID) SELECT " + DSTerminales.DTAsignaTerminalAuto[i].TERMINAL_ID.ToString() + " AS TERMINAL_ID, PERSONA_ID FROM EC_PERSONAS WHERE PERSONA_ID = " + idPersona.ToString() + " AND PERSONA_ID IN (" + Qry + ")";
                EjecutaEscalar(Comando);
            }
        }
    }
    /// <summary>
    /// Asigna una Persona a terminal automaticamente
    /// </summary>
    /// <param name="idTerminal"></param>
    public static void AsignaPersonaATerminalAuto(int idTerminal)
    {
        DS_Terminales DSTerminales = new DS_Terminales();
        DS_TerminalesTableAdapters.DTAsignaTerminalAutoTableAdapter DA = new DS_TerminalesTableAdapters.DTAsignaTerminalAutoTableAdapter();
        DA.FillByTerminalId(DSTerminales.DTAsignaTerminalAuto, idTerminal);
        String Comando;
        if (EsOracle)
            Comando = "INSERT INTO EC_PERSONAS_TERMINALES (TERMINAL_ID, PERSONA_ID) SELECT " + idTerminal.ToString() + " AS TERMINAL_ID, PERSONA_ID FROM (" + DSTerminales.DTAsignaTerminalAuto[0].SITIO_CONSULTA.ToString() + ")";
        else
            Comando = "INSERT INTO EC_PERSONAS_TERMINALES (TERMINAL_ID, PERSONA_ID) SELECT " + idTerminal.ToString() + " AS TERMINAL_ID, PERSONA_ID FROM (" + DSTerminales.DTAsignaTerminalAuto[0].SITIO_CONSULTA.ToString() + ") as TABLA";
        EjecutaEscalar(Comando);
    }


    public static bool Activado()
    {
        try
        {
            /*  IsProtectServer.CeT_PS PS = new IsProtectServer.CeT_PS();
              //PS.Activacion = "1234-5678-9101-1213";
              try
              {
                  PS.Directorio = HttpRuntime.AppDomainAppPath;
              }
              catch
              {
                  PS.Directorio = "";
              }


              // Código que se ejecuta al iniciarse la aplicación\

              if (!PS.Coinciden(IsProtectServer.CeT_PS.Productos.eClock))
              {

                  return false;
              }*/
            return true;
        }
        catch (Exception Ex) { CIsLog2.AgregaError(Ex); }
        return false;
    }
    static bool m_IniciadoAplicacion = false;
    /// <summary>
    /// Se manda a llamar desde global.asax y tambien despues de la activacion
    /// </summary>
    public static void IniciaAplicacion()
    {
        if (m_IniciadoAplicacion)
            return;
        m_IniciadoAplicacion = true;


        CIsLog2.S_NombreDestino = HttpRuntime.AppDomainAppPath + CeC_Config.RutaReportesPDF + "eClock" + DateTime.Now.ToString(" yyMMdd HHmmss") + ".log";

        CIsLog2.AgregaLog("Application_Start...");


        if (!Activado())
        {
            CIsLog2.AgregaLog("Preparando...");
            return;
        }

        IniciaBase();
        CreaRelacionesEmpleados();
        CeC_Periodos.GeneraPredeterminados();
        //Inicia el proceso de calculo de asistencias
        CeC_Asistencias.IniciaGeneración();
        CIsLog2.AgregaLog("Fin IniciadoAplicacion...");
        //        m_IniciadoAplicacion = false;

    }
    public static int NumEmpleados()
    {
        return EjecutaEscalarInt("SELECT COUNT (PERSONA_ID) FROM EC_PERSONAS WHERE PERSONA_BORRADO = 0");
    }

    public static bool EstaeClockListo()
    {
        return true;
        if (CeC_Config.MostrarWizardInicio)
            return false;
        if (EjecutaEscalarInt("SELECT COUNT(USUARIO_ID) FROM EC_USUARIOS") <= 0)
            return false;
        return true;
        int Wizard = CeC_BD.EjecutaEscalarInt("SELECT USUARIO_ID FROM EC_USUARIOS where USUARIO_USUARIO = 'admin' AND USUARIO_CLAVE = 'admin'");
        if (Wizard < 0)
            return true;
        return false;
    }

    public static bool GuardarJPG(string nombre, byte[] archivo)
    {
        try
        {
            File.WriteAllBytes(HttpRuntime.AppDomainAppPath + "\\Imagenes\\" + nombre + ".jpg", archivo);
            return true;
        }
        catch { return false; }
    }

    public static byte[] ObtenerJPG(string nombre)
    {
        return File.ReadAllBytes(HttpRuntime.AppDomainAppPath + "\\Imagenes\\" + nombre + ".jpg");
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


    public static string ObtenQryValidaSuscripcion(string Tabla, string Campo, int SuscripcionID)
    {
        string Qry = " " + Campo + " IN ( SELECT AUTONUM_TABLA_ID FROM EC_AUTONUM WHERE AUTONUM_TABLA = '" + Tabla + "' AND AUTONUM_CAMPO_ID = '" +
            Campo + "' AND SUSCRIPCION_ID = " + SuscripcionID + ")";
        return Qry;
    }

 

}
