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
    /// Register materials
    /// </summary>
    public class CAT_RESIDUO_MATERIALDal
    {
        private static readonly CAT_RESIDUO_MATERIALDal _classinstance = new CAT_RESIDUO_MATERIALDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_RESIDUO_MATERIALDal ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get gas type
        /// </summary>
        /// <returns></returns>
        public DataTable GetGasTypeByTechnology(string technology)
        {
            try
            {
                string SQL = "select Cve_Residuo_Material,Dx_Residuo_Material_Gral from dbo.View_All_Material where ISNULL(Fg_Residuo_Material,0)=1" +
                                 " and Cve_Tecnologia=@Cve_Tecnologia";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia",technology)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get gas type date failed: Execute method GetGasType in CAT_RESIDUO_MATERIALDal.", ex, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="technology"></param>
        /// <returns></returns>
        public DataTable GetMaterialTypeByTechnology(string technology)
        {
            try
            {
                string SQL = "select distinct case isnull(A.Fg_Residuo_Material,0)" +
                                 " when 0 then A.Cve_Residuo_Material" +
                                 " when 1 then 0 end as Cve_Residuo_Material," +
                                 " case ISNULL(A.Fg_Residuo_Material,0)" +
                                 " when 0 then A.Dx_Residuo_Material_Gral" +
                                 " when 1 then 'Gas Refrigerante' end as Dx_Residuo_Material_Gral" +
                                 " from (select B.* from CAT_RESIDUO_MATERIAL B inner join K_TECNOLOGIA_RESIDUO_MATERIAL C" +
                                 "        on B.Cve_Residuo_Material=C.Cve_Residuo_Material and C.Cve_Tecnologia=@Cve_Tecnologia) A";
                //string SQL = "select * from dbo.View_All_Material where isnull(Fg_Residuo_Material,0)=0 and Cve_Tecnologia=@Cve_Tecnologia";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia",technology)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get material type date failed: Execute method GetMaterialTypeByTechnology in CAT_RESIDUO_MATERIALDal.", ex, true);
            }
        }
        /// <summary>
        /// Get material
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public DataTable GetMaterialByPk(string pk)
        {
            try
            {
                string SQL = "select * from CAT_RESIDUO_MATERIAL where Cve_Residuo_Material=@Cve_Residuo_Material";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Residuo_Material",pk)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get material data date failed: Execute method GetMaterialByPk in CAT_RESIDUO_MATERIALDal.", ex, true);
            }
        }
    }
}
