using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.Controladores
{
    public class ControladorDB
    {
        private string SqlStringConnetion = @"Data Source=STARKILLER1\MSSQLSERVER2012;Initial Catalog=CRUD(Stored Procedure);Persist Security Info=True;User ID=sa;Password=A@141516182235";
        public SqlConnection Conector;
        public SqlCommand Comando;
        public SqlDataReader DataReader;

        public ControladorDB()
        {
            try
            {
                Conector = new SqlConnection();
                Conector.ConnectionString = SqlStringConnetion;

                if (Conector.State == ConnectionState.Closed)
                    Conector.Open();
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                Console.WriteLine("Fallo en la conexion con la base de datos");
            }
        }

        public bool GuardarRegistro(string SPNombre, string Nombre, string Apellido, int Edad, string Direccion, string Correo, string Telefono, string Celular, string website, string Compania)
        {
            Comando = new SqlCommand(SPNombre,Conector);
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@SPNombre", SPNombre);
            Comando.Parameters.AddWithValue("@Nombre", Nombre);
            Comando.Parameters.AddWithValue("@Apellido", Apellido);
            Comando.Parameters.AddWithValue("@Edad", Edad);
            Comando.Parameters.AddWithValue("@Direccion", Direccion);
            Comando.Parameters.AddWithValue("@Correo", Correo);
            Comando.Parameters.AddWithValue("@Telefono", Telefono);
            Comando.Parameters.AddWithValue("@Celular", Celular);
            Comando.Parameters.AddWithValue("@website", website);
            Comando.Parameters.AddWithValue("@Compania", Compania);

            int Res = Comando.ExecuteNonQuery();
            
            if (Res == 1)
                return true;

            else
                return false;
        }

        public bool ActualizaRegistro(string SPNombre, string Nombre, string Apellido, int Edad, string Direccion, string Correo, string Telefono, string Celular, string website, string Compania,int ID)
        {
            Comando = new SqlCommand(SPNombre,Conector);
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@SPNombre", SPNombre);
            Comando.Parameters.AddWithValue("@Nombre", Nombre);
            Comando.Parameters.AddWithValue("@Apellido", Apellido);
            Comando.Parameters.AddWithValue("@Edad", Edad);
            Comando.Parameters.AddWithValue("@Direccion", Direccion);
            Comando.Parameters.AddWithValue("@Correo", Correo);
            Comando.Parameters.AddWithValue("@Telefono", Telefono);
            Comando.Parameters.AddWithValue("@Celular", Celular);
            Comando.Parameters.AddWithValue("@website", website);
            Comando.Parameters.AddWithValue("@Compania", Compania);
            Comando.Parameters.AddWithValue("@ID", ID);

            int Res = Comando.ExecuteNonQuery();

            if (Res == 1)
                return true;

            else
                return false;
        }

        public bool Busqueda(string proName, string email)
        {
            Comando = new SqlCommand();
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = proName;
            Comando.Connection = Conector;
            Comando.Parameters.AddWithValue("@email", email);
            DataReader = Comando.ExecuteReader();
            if (DataReader.HasRows)
            {
                DataReader.Close();
                return true;
            }
            else
            {
                DataReader.Close();
                return false;
            }
        }

        public bool BorrarRegistro(string PSNombre, string Mail)
        {
            Comando = new SqlCommand(PSNombre, Conector);
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.AddWithValue("@Mail", Mail);
            int res = Comando.ExecuteNonQuery();
            if (res == 1)
                return true;
            else
                return false;
        }
    }
}
