//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Restaurant
{
    class DatabaseClass
    {
        private string _connectionString = "";

        public DatabaseClass(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Check the connection with the specified connection string
        /// </summary>
        /// <returns>Returns True if the connection can be open else it returns False</returns>
        public bool CheckConnection()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                con.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
    }
}
