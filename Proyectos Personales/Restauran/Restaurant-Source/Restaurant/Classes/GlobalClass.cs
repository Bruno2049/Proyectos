using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

namespace Restaurant
{
    static class GlobalClass
    {
        public static string _UserName = "";
        public static string _Password = "";
        public static string _FullName = "";
        public static Int32 _PermissionID = -1;
        public static Int32 _UserID = -1;
        public static DateTime _Today = DateTime.Now;

        public static string _ConStr = Restaurant.Properties.Settings.Default.ConnectionStr;

        public static DateTime GetCurrentDateTime()
        {
            DateTime dt = DateTime.MinValue;
            SqlDatabase objSqlDatabase = new SqlDatabase(_ConStr);
            try
            {
                string commandText = "SELECT GETDATE() AS Today ";
                dt = (DateTime)objSqlDatabase.ExecuteScalar(System.Data.CommandType.Text, commandText);

            }
            catch (SqlException)
            { }
            return dt;
        }

        public static string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static DataTable GetRestaurantInfo()
        {
            SqlDatabase objSqlDatabase = new SqlDatabase(_ConStr);
            try
            {
                string commandText = "SELECT * FROM Settings";
                DataSet ds = objSqlDatabase.ExecuteDataSet(System.Data.CommandType.Text, commandText);
                if (ds != null)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }
    }
}
