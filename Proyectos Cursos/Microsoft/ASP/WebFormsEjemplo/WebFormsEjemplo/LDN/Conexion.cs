using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebFormsEjemplo.LDN
{
    public class Conexion
    {
        private SqlConnection Conector;
        private SqlDataReader Lector;
        private SqlDataAdapter Adaptador;
        private DataTable Tabla;
        private SqlCommand Comando;

        public void IniciaConexion()
        {
            try
            {
                Conector = new SqlConnection(@"Data Source=STARKILLER\MSSQLSERVER2012;Initial Catalog=Proyectos;User ID=sa;Password=A@141516182235");
                Conector.Open();
            }
            catch (Exception er)
            {
            }
        }

        public bool GuardaProyecto(string Nom_Proyecto, string COD_Tipo)
        {
            try
            {
                IniciaConexion();
                Comando = new SqlCommand("SP_Guardar", Conector);
                Comando.CommandType = CommandType.StoredProcedure;
                SqlParameter PRM1 = new SqlParameter("@Nom_Proyectos", Nom_Proyecto);
                SqlParameter PRM2 = new SqlParameter("@COD_Tipo", COD_Tipo);
                Comando.Parameters.Add(PRM1);
                Comando.Parameters.Add(PRM2);
                Comando.ExecuteNonQuery();
                return true;
            }

            catch (Exception err)
            {
                return false;
            }
        }

        public DataTable Datos (string Cadena)
        {
            IniciaConexion();
            try
            {
                Adaptador = new SqlDataAdapter(Cadena, Conector);
                Tabla = new DataTable();
                Adaptador.Fill(Tabla);
            }
            catch (Exception e)
            { 
            }

            return Tabla;
        }
    }
}