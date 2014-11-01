using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PAEEEM.AccesoDatos.Operacion_Datos;
using PAEEEM.Entidades;
using PAEEEM.Entidades.Alta_Solicitud;

namespace PAEEEM.AccesoDatos.Log
{
    public class ConsultaHistoricos
    {
        private PAEEEM_DESAEntidades contextModel;

        public List<HistoricoCredito> ObtenHistoricoCredito(string idCredito)
        {
            var lstHistoricoCredito = new List<HistoricoCredito>();

            contextModel = new PAEEEM_DESAEntidades();
            var sqlConn = new SqlConnection(contextModel.Database.Connection.ConnectionString);
            SqlDataReader sqldr;
            var cont = 1;

            try
            {
                using (var cmd = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "SP_ConsultaHistoricoCredito",
                        Connection = sqlConn
                    })
                {
                    ConfiguracionComando.AgregaParametro(cmd, "@No_Credito", SqlDbType.NVarChar, idCredito,
                                                         ParameterDirection.Input);

                    sqlConn.Open();
                    sqldr = cmd.ExecuteReader();

                    while (sqldr.Read())
                    {
                        var historico = new HistoricoCredito
                            {
                                IdSecuencia = cont,
                                Descripcion = sqldr["DESCRIPCION_DISPLAY"].ToString(),
                                Motivo = sqldr["MOTIVO"].ToString(),
                                NombreUsuario = sqldr["Nombre_Usuario"].ToString(),
                                NombreRol = sqldr["Nombre_Rol"].ToString(),
                                Fecha = Convert.ToDateTime(sqldr["FECHA_ADICION"].ToString()),
                                Hora = sqldr["HORA"].ToString(),
                                Observaciones = sqldr["OBSERVACIONES"].ToString()
                            };

                        lstHistoricoCredito.Add(historico);
                        cont++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open) sqlConn.Close();
                sqlConn.Dispose();  
            }
            

            return lstHistoricoCredito;
        }
    }
}
