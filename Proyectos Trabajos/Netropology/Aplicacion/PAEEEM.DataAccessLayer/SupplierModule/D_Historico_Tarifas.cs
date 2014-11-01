using System.Data;
using System.Data.SqlClient;
using PAEEEM.DataAccessLayer;

namespace PAEEEM.DataAccessLayer.Tarifas
{
    public class DHistoricoTarifas
    {
        public static DataTable HistoricoTarifaHm(int region, string año, string mes, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {

            var fechaAplicable = "%" + (mes == "" ? "" : mes) + "" + (año == "" ? "" :"-" + año) + "%";

            SqlParameter[] paras =
                 { 
                     new SqlParameter("@Count", SqlDbType.Int),
                     new SqlParameter("@SortBy", sortBy),
                     new SqlParameter("@PageIndex", pageIndex),
                     new SqlParameter("@PageSize", pageSize),
                     new SqlParameter("@Region", region),
                      new SqlParameter("@FechaAplicable", fechaAplicable)
                 };
            paras[0].Direction = ParameterDirection.Output;
            var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "get_Historico_TarifaHM", paras);
            int.TryParse(paras[0].Value.ToString(), out pageCount);
           
            return dtResult;
        }

        public static DataTable HistoricoTarifaOm(int region, string año, string mes, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {
            var fechaAplicable = "%" + (mes == "" ? "" : mes) + "" + (año == "" ? "" : "-" + año) + "%";
            SqlParameter[] paras =
                { 
                    new SqlParameter("@Count", SqlDbType.Int),
                    new SqlParameter("@SortBy", sortBy),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                    new SqlParameter("@Region", region),
                     new SqlParameter("@FechaAplicable", fechaAplicable)
                };
            paras[0].Direction = ParameterDirection.Output;
            var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "get_Historico_TarifaOM", paras);
            int.TryParse(paras[0].Value.ToString(), out pageCount);
            return dtResult;
        }

        public static DataTable HistoricoTarifa03(string año, string mes, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {
            var fechaAplicable = "%" + (mes == "" ? "" : mes) + "" + (año == "" ? "" : "-" + año) + "%";
            SqlParameter[] paras =
                { 
                    new SqlParameter("@Count", SqlDbType.Int),
                    new SqlParameter("@SortBy", sortBy),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                     new SqlParameter("@FechaAplicable", fechaAplicable)
                };
            paras[0].Direction = ParameterDirection.Output;
            var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "get_Historico_Tarifa03", paras);
            int.TryParse(paras[0].Value.ToString(), out pageCount);
            
            return dtResult;
        }

        public static DataTable HistoricoTarifa02(string año, string mes, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {
          
            var fechaAplicable = "%" + (mes == "" ? "" : mes) + "" + (año == "" ? "" : "-" + año) + "%";
            SqlParameter[] paras =
                { 
                    new SqlParameter("@Count", SqlDbType.Int),
                    new SqlParameter("@SortBy", sortBy),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize),
                     new SqlParameter("@FechaAplicable", fechaAplicable)
                };
            paras[0].Direction = ParameterDirection.Output;
            var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "get_Historico_Tarifa02", paras);
            int.TryParse(paras[0].Value.ToString(), out pageCount);
           
            return dtResult;
        }
    }
}
