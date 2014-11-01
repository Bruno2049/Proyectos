using System.Collections.Generic;
using System.Data;
using PAEEEM.Entidades;

namespace PAEEEM.AccesoDatos.Log
{
    public class DbMonitorLog
    {
        public List<CAT_TIPO_PROCESOS> ObtieneTiposProcesos()
        {
            List<CAT_TIPO_PROCESOS> listTipoProc;

            using (var r = new Repositorio<CAT_TIPO_PROCESOS>())
            {
                listTipoProc = r.Filtro(c => true);
            }
            return listTipoProc;
        }

        public List<CAT_TAREAS_PROCESOS> ObtieneTareasProcesos(int idTipoProceso)
        {
            List<CAT_TAREAS_PROCESOS> listTareaProc;

            using (var r = new Repositorio<CAT_TAREAS_PROCESOS>())
            {
                listTareaProc = r.Filtro(c => c.IDTIPOPROCESO == idTipoProceso);
            }
            return listTareaProc;
        }

        public List<US_ROL> ObtieneRoles()
        {
            List<US_ROL> listRoles;

            using (var r = new Repositorio<US_ROL>())
            {
                listRoles = r.Filtro(c => true);
            }
            return listRoles;
        }

        public DataTable DBConsulta_Log(string fechaInicio, string fechaFin, string nomUser, int? idRol, int? idProceso,
            string descTarea)
        {
            return new DataTable();
            //var para = new[]
            //{
            //    new SqlParameter("@FECHA_INICIO", fechaInicio),
            //    new SqlParameter("@FECHA_FIN", fechaFin),
            //    new SqlParameter("@IDROL", idRol),
            //    new SqlParameter("@NAMEUSER", nomUser),
            //    new SqlParameter("@DESCRIPCIONTAREA", descTarea),
            //    new SqlParameter("@IDPROCESO", idProceso)
            //};
            //var dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure,
            //    "dbo.CONSULTA_LOG", para);
            //return dt;
        }

        public static DataTable DBConsulta_Log(string fechaInicio, string fechaFin, int idRol, string nameUser,
            int idTarea, int idProceso, string sortBy, int pageIndex, int pageSize, out int pageCount)
        {
            pageCount = 0;
            return new DataTable();
            //nameUser = "%" + (nameUser == "" ? null : nameUser) + "%";

            //SqlParameter[] paras =
            //{
            //    new SqlParameter("@Count", SqlDbType.Int),
            //    new SqlParameter("@SortBy", sortBy),
            //    new SqlParameter("@PageIndex", pageIndex),
            //    new SqlParameter("@PageSize", pageSize),
            //    new SqlParameter("@FECHA_INICIO", fechaInicio),
            //    new SqlParameter("@FECHA_FIN", fechaFin),
            //    new SqlParameter("@ID_ROL", idRol),
            //    new SqlParameter("@NAMEUSER", nameUser),
            //    new SqlParameter("@ID_TAREA", idTarea),
            //    new SqlParameter("@IDPROCESO", idProceso),
            //};
            //paras[0].Direction = ParameterDirection.Output;
            //var dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure,
            //    "get_Historico_LOG", paras);
            //int.TryParse(paras[0].Value.ToString(), out pageCount);
            //return dtResult;
        }
    }
}




