/*
	Copyright IMPRA, Inc. 2010
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     $
	$Date:       $
	Log at end of file
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PAEEEM.Helpers
{
    /// <summary>
    ///  Base database operation class for this application
    /// </summary>
    /// <remarks>
    /// Used to access background database for whole modules
    /// </remarks>
    public class LsDatabase
    {
        private string _connectionString;
        private int _commandTimeout = 60;
        private int _connectTimeout = 60;
        private SqlConnection _conn = null;
        private int _connectionCounter = 0;

        public LsDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Connection
        {
            get
            {
                return _conn = new SqlConnection(_connectionString);
            }
        }

        public int CommandTimeout
        {
            get { return _commandTimeout; }
            set { _commandTimeout = value; }
        }

        public int ConnectTimeout
        {
            get { return _connectTimeout; }
            set { _connectTimeout = value; }
        }

        public SqlConnection OpenConnection()
        {
            if (null == _conn)
            {
                _conn = new SqlConnection(_connectionString);
            }
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }
            _connectionCounter++;

            return _conn;
        }

        public void CloseConnection()
        {
            if (_connectionCounter >= 1)
            {
                _connectionCounter--;
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
                if (null != _conn)
                {
                    _conn = null;
                }
            }
        }
        /// <summary>
        /// Execute SQL query, and return a Datatable object
        /// </summary>
        /// <param name="SQL">SQL sentence</param>
        /// <param name="data">Returned data table object</param>
        public void ExecuteSQLDataTable(string SQL, out DataTable data)
        {
            data = new DataTable();
            SqlConnection conn = null;
            SqlDataAdapter adapter = null;

            try
            {
                conn = OpenConnection();
                adapter = new SqlDataAdapter(SQL, conn);
                adapter.Fill(data);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(null, "Error in ExecuteSQLDataTable: " + ex.Message, ex, false);
            }
            finally
            {
                CloseConnection();
            }
        }
        /// <summary>
        /// Execute SQL and return the first row first column
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public object ExecuteSQLScalar(string SQL)
        {
            object Result = null;
            SqlConnection conn = null;

            try
            {
                conn = OpenConnection();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.CommandTimeout = CommandTimeout;
                Result = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new LsDAException("Error in ExecuteSQLScalar: " + ex.Message, ex, false);
            }
            finally
            {
                CloseConnection();
            }

            return Result;
        }
        /// <summary>
        /// Execute SQL query without command parameters
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int ExecuteSQLNonQuery(string SQL)
        {
            int Result = -1;
            SqlConnection conn = null;

            try
            {
                conn = OpenConnection();

                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.CommandTimeout = CommandTimeout;
                Result = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new LsDAException("Error in ExecuteSQLNonQuery: " + ex.Message, ex, false);
            }
            finally
            {
                CloseConnection();
            }

            return Result;
        }
        /// <summary>
        /// Execute SQL List
        /// </summary>
        /// <param name="SQLList">SQL List</param>
        /// <returns>int count records affected</returns>
        public int ExecuteSQLNonQuery(List<string> SQLList)
        {
            int Result = -1;

            SqlConnection conn = null;

            try
            {
                conn = OpenConnection();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                for (int i = 0; i < SQLList.Count; i++)
                {
                    cmd.CommandText = SQLList[i];
                    Result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw new LsDAException("Error in ExecuteSQLNonQuery: " + e.Message, e, false);
            }
            finally
            {
                CloseConnection();
            }
            return Result;
        }
        /// <summary>
        /// Execute SQL query with parameters
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSQLNonQuery(string SQL, Dictionary<string, string> parameters)
        {
            int Result = -1;
            SqlConnection conn = null;

            try
            {
                conn = OpenConnection();

                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.CommandTimeout = CommandTimeout;

                foreach (string key in parameters.Keys)
                {
                    cmd.Parameters.AddWithValue(key, parameters[key]);
                }

                Result = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new LsDAException("Error in ExecuteSQLNonQuery: " + e.Message, e, false);
            }
            finally
            {
                CloseConnection();
            }

            return Result;
        }

        public int ExecuteSQLNonQuery(string SQL, Dictionary<string, object> parameters)
        {
            int Result = -1;
            SqlConnection conn = null;

            try
            {
                conn = OpenConnection();

                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.CommandTimeout = CommandTimeout;

                foreach (string key in parameters.Keys)
                {
                    cmd.Parameters.AddWithValue(key, parameters[key]);
                }

                Result = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new LsDAException("Error in ExecuteSQLNonQuery: " + e.Message, e, false);
            }
            finally
            {
                CloseConnection();
            }

            return Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSQLNonQuery(string SQL, SqlParameter[] parameters)
        {
            int Result = -1;
            SqlConnection conn = null;

            try
            {
                conn = OpenConnection();

                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.CommandTimeout = CommandTimeout;

                foreach (SqlParameter p in parameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(p);
                    }
                }

                Result = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new LsDAException("Error in ExecuteSQLNonQuery: " + e.Message, e, false);
            }
            finally
            {
                CloseConnection();
            }

            return Result;
        }
        /// <summary>
        /// Execute SQL store procedure
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public int ExecuteSQLStoredProcedure(string Procedure, params object[] Parameters)
        {
            int SQLResult = -1;
            SqlConnection conn = null;

            try
            {
                conn = OpenConnection();

                SqlCommand command = new SqlCommand(Procedure);
                command.CommandType = CommandType.StoredProcedure;

                for (int ParamIndex = 0; ParamIndex < Parameters.Length; ParamIndex += 2)
                {
                    command.Parameters.AddWithValue(Parameters[ParamIndex].ToString(), Parameters[ParamIndex + 1]);
                }

                command.Connection = conn;
                command.CommandTimeout = CommandTimeout;
                SQLResult = command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new LsDAException("Error in ExecuteSQLStoredProcedure: " + e.Message, e, false);
            }
            finally
            {
                CloseConnection();
            }

            return SQLResult;
        }        
    }
}
