using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PAEEEM.DataAccessLayer.CentralModule
{
    public class Log
    {
        public DataTable DBConsulta_Log(string fechaInicio, string fechaFin, string nomUser, int? idRol, int? idProceso,
          string descTarea)
        {
            //return new DataTable();
            var para = new[]
            {
                new SqlParameter("@FECHA_INICIO", fechaInicio),
                new SqlParameter("@FECHA_FIN", fechaFin),
                new SqlParameter("@IDROL", idRol),
                new SqlParameter("@NAMEUSER", nomUser),
                new SqlParameter("@DESCRIPCIONTAREA", descTarea),
                new SqlParameter("@IDPROCESO", idProceso)
            };
            var dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure,
                "dbo.CONSULTA_LOG", para);
            return dt;
        }

        public static DataTable DBConsulta_Log(string fechaInicio, string fechaFin, int idRol, string nameUser,
            int idTarea, int idProceso, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {
            pageCount = 0;
            //return new DataTable();
            nameUser = "%" + (nameUser == "" ? null : nameUser) + "%";

            SqlParameter[] paras =
            {
                new SqlParameter("@Count", SqlDbType.Int),
                new SqlParameter("@SortBy", sortBy),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@FECHA_INICIO", fechaInicio),
                new SqlParameter("@FECHA_FIN", fechaFin),
                new SqlParameter("@ID_ROL", idRol),
                new SqlParameter("@NAMEUSER", nameUser),
                new SqlParameter("@ID_TAREA", idTarea),
                new SqlParameter("@IDPROCESO", idProceso),
            };
            paras[0].Direction = ParameterDirection.Output;
            var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure,
                "get_Historico_LOG", paras);
            int.TryParse(paras[0].Value.ToString(), out pageCount);
            return dtResult;
        }
    

    }
}
