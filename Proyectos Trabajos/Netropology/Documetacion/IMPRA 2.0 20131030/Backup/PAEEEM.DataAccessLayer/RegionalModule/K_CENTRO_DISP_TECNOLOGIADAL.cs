using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;
using PAEEEM.Entities;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// technologies of assigned to disposal
    /// </summary>
    public class K_CENTRO_DISP_TECNOLOGIADAL
    {
        /// <summary>
        /// readonly class instance
        /// </summary>
        private static readonly K_CENTRO_DISP_TECNOLOGIADAL _classinstance = new K_CENTRO_DISP_TECNOLOGIADAL();
        /// <summary>
        /// Property
        /// </summary>
        public static K_CENTRO_DISP_TECNOLOGIADAL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// insert K_CENTRO_DISP_TECNOLOGIA
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_K_CENTRO_DISP_TECNOLOGIA(K_CENTRO_DISP_TECNOLOGIAModel instance)
        {
            try
            {
                string sql = " INSERT INTO K_CENTRO_DISP_TECNOLOGIA(Id_Centro_Disp,Cve_Tecnologia,Cve_Estatus_CD_Tec,Dt_Fecha_CD_Tec,Fg_Tipo_Centro_Disp)" +
                                " VALUES(@Id_Centro_Disp,@Cve_Tecnologia,@Cve_Estatus_CD_Tec,@Dt_Fecha_CD_Tec,@Fg_Tipo_Centro_Disp)";
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@Id_Centro_Disp",instance.Id_Centro_Disp),
                    new SqlParameter("@Cve_Tecnologia",instance.Cve_Tecnologia),
                    new SqlParameter("@Cve_Estatus_CD_Tec",instance.Cve_Estatus_CD_Tec),
                    new SqlParameter("@Dt_Fecha_CD_Tec",instance.Dt_Fecha_CD_Tec),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",instance.Fg_Tipo_Centro_Disp)
                };
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add K_CENTRO_DISP_TECNOLOGIA failed: Execute method Insert_K_CENTRO_DISP_TECNOLOGIA in K_CENTRO_DISP_TECNOLOGIADAL.", ex, true);
            }
        }
        /// <summary>
        /// Delete disposal center related disposal technology
        /// </summary>
        /// <param name="disposal">disposal center</param>
        /// <param name="technology">technology</param>
        /// <param name="type">disposal center type</param>
        /// <returns></returns>
        public int Delete_K_CENTRO_DISP_TECNOLOGIA(int disposal, int technology, string type)
        {
            try
            {
                string sql = "DELETE FROM K_CENTRO_DISP_TECNOLOGIA WHERE Id_Centro_Disp=" + disposal + " AND Cve_Tecnologia=" + technology + " AND Fg_Tipo_Centro_Disp='" + type + "'";
                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "delete K_CENTRO_DISP_TECNOLOGIA failed: Execute method Delete_K_CENTRO_DISP_TECNOLOGIA in K_CENTRO_DISP_TECNOLOGIADAL.", ex, true);
            }
        }
    }
}
