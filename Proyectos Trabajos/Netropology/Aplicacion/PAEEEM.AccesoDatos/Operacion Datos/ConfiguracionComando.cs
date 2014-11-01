using System.Data;
using System.Data.SqlClient;

namespace PAEEEM.AccesoDatos.Operacion_Datos
{
    public static class ConfiguracionComando
    {

        public static void AgregaParametro(SqlCommand cmd, string nombreParametro, SqlDbType tipoDato, object value , int longitud, ParameterDirection direccion)
        {
            var parametro = new SqlParameter(nombreParametro, tipoDato)
                {
                    Value = value,
                    Direction = direccion,
                    Size = longitud,                    
                };
            cmd.Parameters.Add(parametro);
        }

        public static void AgregaParametro(SqlCommand cmd, string nombreParametro, SqlDbType tipoDato, object value, ParameterDirection direccion)
        {
            var parametro = new SqlParameter(nombreParametro, tipoDato)
                {
                    Value = value,
                    Direction = direccion
                };
            cmd.Parameters.Add(parametro);
        }

    }
}
