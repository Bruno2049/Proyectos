using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class K_TECNOLOGIA_RESIDUO_MATERIALDal
    {
        private static readonly K_TECNOLOGIA_RESIDUO_MATERIALDal _classinstance = new K_TECNOLOGIA_RESIDUO_MATERIALDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_TECNOLOGIA_RESIDUO_MATERIALDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get materials process order by technology
        /// </summary>
        /// <param name="technology">program</param>
        /// <param name="material">technology</param>
        /// <returns></returns>
        public int GetOrderByTechnologyAndMaterial(string technology,string material)
        {
            int order = 0;
            try
            {
                string SQL = "select Id_Orden from K_TECNOLOGIA_RESIDUO_MATERIAL where Cve_Tecnologia=@Cve_Tecnologia and Cve_Residuo_Material=@Cve_Residuo_Material";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia",technology),
                    new SqlParameter("@Cve_Residuo_Material",material)
                };
                if (SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para) != null)
                {
                    order = Convert.ToInt32(SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para).ToString());
                }
                return order;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get order by technology and material failed: Execute method GetOrderByTechnologyAndMaterial in K_TECNOLOGIA_RESIDUO_MATERIALDal.", ex, true);
            }
        }
    }
}
