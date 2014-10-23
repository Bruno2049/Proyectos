using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Conexion_sql
{
    public class Conexion
    {

        public static SqlConnection Conectar()
        {
            SqlConnection Conn = new SqlConnection(@"Data Source=STARKILLER1\MSSQLSERVER2012;Initial Catalog=Registro;Integrated Security=True");
            Conn.Open();
            return Conn;
        }

    }
}
