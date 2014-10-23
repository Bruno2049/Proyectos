using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;

namespace Waiter.Classes
{
    static class DatabaseClass
    {
        #region Global Variables
        static string conStr_Remote = "";
        static string conStr_Local = "Data source=\\My Documents\\Waiter.sdf;Password=P@ssw0rdWaiterP@ssw0rd";
        
        internal static SqlConnection objSqlConnection;
        internal static SqlCeConnection objSqlCeConnection;
        #endregion

        static DatabaseClass()
        {
            objSqlCeConnection = new SqlCeConnection(conStr_Local);
            CheckDataBaseExistance();
            conStr_Remote = GetSQLRemoteConnectionString();
            objSqlConnection = new SqlConnection(conStr_Remote);
        }

        #region SQL_Methods
        public static bool OpenSqlConnection()
        {
            try
            {
                // check if there's any open connection
                if (objSqlConnection.State != System.Data.ConnectionState.Open)
                    objSqlConnection.Open();

                return (true);
            }
            catch (SqlException ex)
            {
                return (false);
            }
        }

        public static bool CloseSqlConnection()
        {
            try
            {
                // check if there's any open connection
                if (objSqlConnection.State != System.Data.ConnectionState.Closed)
                    objSqlConnection.Close();

                return (true);
            }
            catch (SqlException ex)
            {
                return (false);
            }
        }

        public static int ExecuteNonQuary(string commandText, CommandType cmdType, SqlParameter[] spParams)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            if (cmdType == CommandType.StoredProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            if (spParams != null)
            {
                for (int i = 0; i < spParams.Length; i++)
                {
                    cmd.Parameters.Add(spParams[i]);
                }
            }
            cmd.Connection = objSqlConnection;
            try
            {
                OpenSqlConnection();
                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                CloseSqlConnection();
            }
            return result;
        }

        public static int ExecuteNonQuary(string commandText, CommandType cmdType, SqlParameter[] spParams, SqlTransaction tran)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.Connection = objSqlConnection;
            cmd.Transaction = tran;
            if (cmdType == CommandType.StoredProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            if (spParams != null)
            {
                for (int i = 0; i < spParams.Length; i++)
                {
                    cmd.Parameters.Add(spParams[i]);
                }
            }
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

            }
            return result;
        }

        public static DataSet ExecuteDataSet(string spName, SqlParameter[] spParams)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = objSqlConnection;
                da.SelectCommand = cmd;
                for (int i = 0; i < spParams.Length; i++)
                {
                    cmd.Parameters.Add(spParams[i]);
                }
                da.Fill(ds);
            }
            catch (SqlException ex)
            { 
                //
            }
            return ds;
        }

        public static DataSet ExecuteDataSet(string commandText, CommandType cmdType)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = commandText;
                if (cmdType == CommandType.StoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else if (cmdType == CommandType.TableDirect)
                {
                    cmd.CommandType = CommandType.TableDirect;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }
                cmd.Connection = objSqlConnection;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (SqlException)
            { }
            return ds;
        }

        public static SqlDataReader ExecuteReader(string commandText, CommandType cmdType)
        {
            SqlDataReader objReader = null; 
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = commandText;
                if (cmdType == CommandType.StoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else if (cmdType == CommandType.TableDirect)
                {
                    cmd.CommandType = CommandType.TableDirect;
                }
                else
                {
                    cmd.CommandType = CommandType.Text;
                }
                cmd.Connection = objSqlConnection;
                objReader = cmd.ExecuteReader();
            }
            catch (SqlException)
            { }
            return objReader;
        }

        public static object ExecuteScaler(string commandText, CommandType cmdType)
        {
            object result = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objSqlConnection;
                cmd.CommandText = commandText;
                if (cmdType == CommandType.StoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                OpenSqlConnection();
                result = (object)cmd.ExecuteScalar();
            }
            catch (SqlException)
            {
                
            }
            finally
            {
                CloseSqlConnection();
            }
            return result;
        }

        public static object ExecuteScaler(string commandText, CommandType cmdType, SqlParameter[] spParams)
        {
            object result = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objSqlConnection;
                cmd.CommandText = commandText;
                if (cmdType == CommandType.StoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                if (spParams != null)
                {
                    for (int i = 0; i < spParams.Length; i++)
                    {
                        cmd.Parameters.Add(spParams[i]);
                    }
                }
                OpenSqlConnection();
                result = (object)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
            }
            finally
            {
                CloseSqlConnection();
            }
            return result;
        }

        public static object ExecuteScaler(string commandText, CommandType cmdType, SqlParameter[] spParams, SqlTransaction tran)
        {
            object result = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objSqlConnection;
                cmd.Transaction = tran;
                cmd.CommandText = commandText;
                if (cmdType == CommandType.StoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                if (spParams != null)
                {
                    for (int i = 0; i < spParams.Length; i++)
                    {
                        cmd.Parameters.Add(spParams[i]);
                    }
                }
                result = (object)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
            }
            return result;
        }

        public static bool CheckConnectionToServer()
        {
            bool result = false;
            try
            {
                objSqlConnection.Open();
                objSqlConnection.Close();
                result = true;
            }
            catch(SqlException ex)
            {
                //
            }
            return result;
        }

        public static void RefreshConnectionString()
        {
            objSqlConnection.ConnectionString = GetSQLRemoteConnectionString();
        }

        public static DateTime GetServerDateTime()
        {
            DateTime result = DateTime.MinValue;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = objSqlConnection;
                cmd.CommandText = "SELECT GETDATE() AS ServerDateTime";
                OpenSqlConnection();
                result = (DateTime)cmd.ExecuteScalar();
                CloseSqlConnection();
            }
            catch (SqlException)
            {
                CloseSqlConnection();
            }
            return result;
        }
        #endregion

        #region SQL_CE_Methods
        public static bool CheckDataBaseExistance()
        {
            if (File.Exists("\\My Documents\\Waiter.sdf"))
            {
                return true;
            }
            else
            {
                try
                {
                    SqlCeEngine objSqlCeEngine = new SqlCeEngine(conStr_Local);
                    objSqlCeEngine.CreateDatabase();
                    CreateConfigTable();
                    CreateDataHasChangedTable();
                    CreateGroupsTable();
                    CreateProductsTable();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private static void CreateConfigTable()
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandText = "CREATE TABLE Config(IDN INT Primary Key,ServerName NVARCHAR(50),DatabaseName NVARCHAR(30),UserID NVARCHAR(30),Password NVARCHAR(30), ConnectionTimeout INT)";
            cmd.Connection = objSqlCeConnection;
            try
            {
                objSqlCeConnection.Open();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Insert Config(IDN,ServerName,DatabaseName,UserID,Password,ConnectionTimeout) Values(1,'ali-notebook\\MSSQL2005,1301','Restaurant','sa','sapass',10)";
                cmd.Connection = objSqlCeConnection;
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
            }
            finally
            {
                objSqlCeConnection.Close();
            }
        }

        private static void CreateDataHasChangedTable()
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandText = "CREATE TABLE DataHasChanged(ID INT Primary Key,GroupsLastChangedDate DATETIME,ProductsLastChangedDate DATETIME)";
            cmd.Connection = objSqlCeConnection;
            try
            {
                objSqlCeConnection.Open();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Insert DataHasChanged(ID,GroupsLastChangedDate,ProductsLastChangedDate) Values(1,GETDATE(),GETDATE())";
                cmd.Connection = objSqlCeConnection;
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
            }
            finally
            {
                objSqlCeConnection.Close();
            }
        }

        private static void CreateGroupsTable()
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandText = "CREATE TABLE Groups(GroupID INT Primary Key,GroupName NVARCHAR(50))";
            cmd.Connection = objSqlCeConnection;
            try
            {
                objSqlCeConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
            }
            finally
            {
                objSqlCeConnection.Close();
            }
        }

        private static void CreateProductsTable()
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandText = "CREATE TABLE Products(ProductID INT Primary Key,ProductName NVARCHAR(100),UnitName NVARCHAR(50),GroupID INT,Price DECIMAL(18,2))";
            cmd.Connection = objSqlCeConnection;
            try
            {
                objSqlCeConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
            }
            finally
            {
                objSqlCeConnection.Close();
            }
        }

        private static string GetSQLRemoteConnectionString()
        {
            string strRemoteConStr = "";
            string serverName = "";
            string database = "";
            string userId = "";
            string password = "";
            string connectionTimeout = "";
            try
            {
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandText = "SELECT ServerName,DatabaseName,UserID,Password,ConnectionTimeout FROM Config";
                cmd.Connection = objSqlCeConnection;
                if (OpenSqlCeConnection())
                {
                    SqlCeDataReader objReader = cmd.ExecuteReader();
                    if (objReader.Read())
                    {
                        serverName = objReader["ServerName"].ToString();
                        database = objReader["DatabaseName"].ToString();
                        userId = objReader["UserID"].ToString();
                        password = objReader["Password"].ToString();
                        connectionTimeout = objReader["ConnectionTimeout"].ToString();
                    }
                    objReader.Close();
                    strRemoteConStr = "Data Source=" + serverName + "; Initial Catalog=" + database + "; User ID=" + userId + "; Password=" + password + "; Connection Timeout=" + connectionTimeout + "";
                }
            }
            catch (SqlCeException ex)
            { }
            finally
            {
                CloseSqlCeConnection();
            }
            return strRemoteConStr;
        }

        public static DataSet ExecuteCeDataSet(string commandText)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCeDataAdapter da = new SqlCeDataAdapter();
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandText = commandText;
                cmd.Connection = objSqlCeConnection;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (SqlCeException ex)
            {
                //
            }
            return ds;
        }

        public static int ExecuteCeNonQuary(string _strCommandText)
        {
            int result = 0;
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandText = _strCommandText;
            cmd.Connection = objSqlCeConnection;
            try
            {
                OpenSqlCeConnection();
                result = cmd.ExecuteNonQuery();
                CloseSqlCeConnection();
            }
            catch (SqlCeException ex)
            {
                CloseSqlCeConnection();
            }
            return result;
        }

        public static object ExecuteCeScaler(string _strCommandText)
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandText = _strCommandText;
            cmd.Connection = objSqlCeConnection;
            try
            {
                OpenSqlCeConnection();
                object result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
            catch (SqlCeException ex)
            {
                CloseSqlCeConnection();
                return null;
            }
            finally
            {
                CloseSqlCeConnection();
            }
        }

        public static bool OpenSqlCeConnection()
        {
            try
            {
                if (objSqlCeConnection.State == System.Data.ConnectionState.Closed)
                {
                    objSqlCeConnection.Open();
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlCeException)
            {
                return false;
            }
        }

        public static bool CloseSqlCeConnection()
        {
            try
            {
                if (objSqlCeConnection.State == System.Data.ConnectionState.Open)
                {
                    objSqlCeConnection.Close();
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlCeException)
            {
                return false;
            }
        }

        public static bool DeleteTableRows(string tableName)
        {
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandText = "DELETE FROM "+tableName+"";
            cmd.Connection = objSqlCeConnection;
            try
            {
                objSqlCeConnection.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlCeException ex)
            {
                return false;
            }
            finally
            {
                objSqlCeConnection.Close();
            }
        }

        public static SqlCeDataReader ExecuteCeReader(string _strCommand)
        {
            SqlCeDataReader objReader=null ;
            SqlCeCommand cmd = new SqlCeCommand();
            cmd.CommandText = _strCommand;
            cmd.Connection = objSqlCeConnection;
            try
            {
                objReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (SqlCeException)
            { }
            return (objReader);
        }

        public static bool CheckCeTableExistance(string tableName)
        {
            string commandText = "Select * from " + tableName;
            SqlCeCommand objSqlCeCommand = new SqlCeCommand(commandText, objSqlCeConnection);
            try
            {
                OpenSqlCeConnection();
                objSqlCeCommand.ExecuteScalar();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseSqlCeConnection();
            }
        }

        #endregion
    }
}
