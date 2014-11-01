using System.Data;
using System.Data.SqlClient;

namespace PAEEEM.AccesoDatos.Tarifas
{
    public class DHistoricoTarifas
    {
        public static DataTable HistoricoTarifaHm(int region, string año, string mes, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {
            pageCount = 0;
            return new DataTable();
            // //try
            // //{

            // var fechaAplicable = "%" + (mes == ""? "": mes) + "-" + (año == "" ? "" : año) + "%";

            //     SqlParameter[] paras =
            //     { 
            //         new SqlParameter("@Count", SqlDbType.Int),
            //         new SqlParameter("@SortBy", sortBy),
            //         new SqlParameter("@PageIndex", pageIndex),
            //         new SqlParameter("@PageSize", pageSize),
            //         new SqlParameter("@Region", region),
            //          new SqlParameter("@FechaAplicable", fechaAplicable)
            //         //new SqlParameter("@Año", año),
            //         //new SqlParameter("@Mes", mes)
            //     };
            //     paras[0].Direction = ParameterDirection.Output;
            //     var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "get_Historico_TarifaHM", paras);
            //     int.TryParse(paras[0].Value.ToString(), out pageCount);
            // //}
            // //catch (SqlException ex)
            // //{
            // //    //throw new LsDAException(this, "Get users by type failed; Execute method HistoricoTarifaHM in DHistoricoTarifas.", ex, true);
            // //}
            //// pageCount = 0;
            // return dtResult;
        }

        public static DataTable HistoricoTarifaOm(int region, string año, string mes, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {
            pageCount = 0;
            return new DataTable();
            ////try
            ////{
            //var fechaAplicable = "%" + (mes == "" ? "" : mes) + "-" + (año == "" ? "" : año) + "%";
            //SqlParameter[] paras =
            //    { 
            //        new SqlParameter("@Count", SqlDbType.Int),
            //        new SqlParameter("@SortBy", sortBy),
            //        new SqlParameter("@PageIndex", pageIndex),
            //        new SqlParameter("@PageSize", pageSize),
            //        new SqlParameter("@Region", region),
            //         new SqlParameter("@FechaAplicable", fechaAplicable)
            //        //new SqlParameter("@Año", año),
            //        //new SqlParameter("@Mes", mes)
            //    };
            //paras[0].Direction = ParameterDirection.Output;
            //var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "get_Historico_TarifaOM", paras);
            //int.TryParse(paras[0].Value.ToString(), out pageCount);
            ////}
            ////catch (SqlException ex)
            ////{
            ////    //throw new LsDAException(this, "Get users by type failed; Execute method HistoricoTarifaHM in DHistoricoTarifas.", ex, true);
            ////}
            //// pageCount = 0;
            //return dtResult;
        }

        public static DataTable HistoricoTarifa03(string año, string mes, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {
            pageCount = 0;
            return  new DataTable();
            ////try
            ////{
            //var fechaAplicable = "%" + (mes == "" ? "" : mes) + "-" + (año == "" ? "" : año) + "%";
            //SqlParameter[] paras =
            //    { 
            //        new SqlParameter("@Count", SqlDbType.Int),
            //        new SqlParameter("@SortBy", sortBy),
            //        new SqlParameter("@PageIndex", pageIndex),
            //        new SqlParameter("@PageSize", pageSize),
            //         new SqlParameter("@FechaAplicable", fechaAplicable)
            //        //new SqlParameter("@Año", año),
            //        //new SqlParameter("@Mes", mes)
            //    };
            //paras[0].Direction = ParameterDirection.Output;
            //var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "get_Historico_Tarifa03", paras);
            //int.TryParse(paras[0].Value.ToString(), out pageCount);
            ////}
            ////catch (SqlException ex)
            ////{
            ////    //throw new LsDAException(this, "Get users by type failed; Execute method HistoricoTarifaHM in DHistoricoTarifas.", ex, true);
            ////}
            //// pageCount = 0;
            //return dtResult;
        }

        public static DataTable HistoricoTarifa02(string año, string mes, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {
            pageCount = 0;
            return new DataTable();
            ////try
            ////{
            //var fechaAplicable = "%" + (mes == "" ? "" : mes) + "-" + (año == "" ? "" : año) + "%";
            //SqlParameter[] paras =
            //    { 
            //        new SqlParameter("@Count", SqlDbType.Int),
            //        new SqlParameter("@SortBy", sortBy),
            //        new SqlParameter("@PageIndex", pageIndex),
            //        new SqlParameter("@PageSize", pageSize),
            //         new SqlParameter("@FechaAplicable", fechaAplicable)
            //        //new SqlParameter("@Año", año),
            //        //new SqlParameter("@Mes", mes)
            //    };
            //paras[0].Direction = ParameterDirection.Output;
            //var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "get_Historico_Tarifa02", paras);
            //int.TryParse(paras[0].Value.ToString(), out pageCount);
            ////}
            ////catch (SqlException ex)
            ////{
            ////    //throw new LsDAException(this, "Get users by type failed; Execute method HistoricoTarifaHM in DHistoricoTarifas.", ex, true);
            ////}
            //// pageCount = 0;
            //return dtResult;
        }
    }
}
