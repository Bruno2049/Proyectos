using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Conexion_sql
{
    public class PersonaDB
    {
        public static int Insertar(Persona Dato)
        {
            int Retorno = 0;
            using (SqlConnection Conn = Conexion.Conectar())
            {
                SqlCommand comando = new SqlCommand(string.Format("Insert Into Datos (Nombre, Apellido, Edad) Values ('{0}','{1}',{2})", Dato.Nombre, Dato.Apellido, Dato.Edad),Conn);
                Retorno = comando.ExecuteNonQuery();
                Conn.Close();
            }
            return Retorno;
        }

        public static List<Persona> Buscar(Persona Enviado, Int64 Seleccion)
        {            
            string Consulta;
            SqlDataReader Lector;
            List<Persona> Resultado = new List<Persona>();

            using (SqlConnection Conn = Conexion.Conectar())
            {

                if (Seleccion == 1)
                {

                    Consulta = string.Format("Select id,Nombre,Apellido,Edad From Datos where Nombre like '%{0}%'", Enviado.Nombre);
                    SqlCommand Comando = new SqlCommand(Consulta, Conn);
                    Lector = Comando.ExecuteReader();

                    while (Lector.Read())
                    {
                        Persona Integrante = new Persona();
                        Integrante.Id = Lector.GetInt32(0);
                        Integrante.Nombre = Lector.GetString(1);
                        Integrante.Apellido = Lector.GetString(2);
                        Integrante.Edad = Lector.GetInt32(3);

                        Resultado.Add(Integrante);
                    }
                }

                else if (Seleccion == 3)
                {
                    Consulta = string.Format("Select id,Nombre,Apellido,Edad From Datos where Id = {0}", Enviado.Id);
                    SqlCommand Comando = new SqlCommand(Consulta, Conn);
                    Lector = Comando.ExecuteReader();

                    while (Lector.Read())
                    {
                        Persona Integrante = new Persona();
                        Integrante.Id = Lector.GetInt32(0);
                        Integrante.Nombre = Lector.GetString(1);
                        Integrante.Apellido = Lector.GetString(2);
                        Integrante.Edad = Lector.GetInt32(3);

                        Resultado.Add(Integrante);
                    }
                }

                else if (Seleccion == 2)
                {
                    Consulta = string.Format("Select id,Nombre,Apellido,Edad From Datos where Apellido like '%{0}%'", Enviado.Apellido);
                    SqlCommand Comando = new SqlCommand(Consulta, Conn);
                    Lector = Comando.ExecuteReader();

                    while (Lector.Read())
                    {
                        Persona Integrante = new Persona();
                        Integrante.Id = Lector.GetInt32(0);
                        Integrante.Nombre = Lector.GetString(1);
                        Integrante.Apellido = Lector.GetString(2);
                        Integrante.Edad = Lector.GetInt32(3);

                        Resultado.Add(Integrante);
                    }
                }

                else if (Seleccion == 4)
                {
                    Consulta = string.Format("Select * From Datos");
                    SqlCommand Comando = new SqlCommand(Consulta, Conn);
                    Lector = Comando.ExecuteReader();

                    while (Lector.Read())
                    {
                        Persona Integrante = new Persona();
                        Integrante.Id = Lector.GetInt32(0);
                        Integrante.Nombre = Lector.GetString(1);
                        Integrante.Apellido = Lector.GetString(2);
                        Integrante.Edad = Lector.GetInt32(3);

                        Resultado.Add(Integrante);
                    }
                }
                Conn.Close();
                return Resultado;
            }
        }

        public static int Actualizar(Persona Modificar)
        {
            int Retorno;
            using (SqlConnection Conn = Conexion.Conectar())
            {
                string Consulta = string.Format("UPDATE Datos set Nombre = '{0}', Apellido = '{1}',  Edad = {2} WHERE id = {3}", Modificar.Nombre, Modificar.Apellido, Modificar.Edad, Modificar.Id/*"UPDATE Datos SET Nombre = 'Alvaro', Apellido = 'Campos' ,Edad = 12 WHERE id = 2"*/);
                SqlCommand Comando = new SqlCommand(Consulta ,Conn);
                Retorno = Comando.ExecuteNonQuery();
                Conn.Close();
            }
            
            return Retorno; 
        }

        public static int Eliminar (Persona Eliminar)
        {
            int Retorno;
            using(SqlConnection Conn = Conexion.Conectar())
            {
                string Consulta = string.Format("DELETE FROM Datos WHERE id = {0}",Eliminar.Id);
                SqlCommand Comando = new SqlCommand(Consulta,Conn);
                Retorno = Comando.ExecuteNonQuery();
                Conn.Close();
            }
            return Retorno;
        }
    }
}
