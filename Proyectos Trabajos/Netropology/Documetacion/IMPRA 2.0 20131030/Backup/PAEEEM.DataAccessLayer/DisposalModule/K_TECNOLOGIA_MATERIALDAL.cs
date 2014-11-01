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
    /// K_TECNOLOGIA_MATERIALDAL data access lay
    /// </summary>
    public class K_TECNOLOGIA_MATERIALDAL
    {
        private static readonly K_TECNOLOGIA_MATERIALDAL _classinstance = new K_TECNOLOGIA_MATERIALDAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_TECNOLOGIA_MATERIALDAL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get material with technology
        /// </summary>
        /// <param name="Technology"></param>
        /// <returns></returns>
        public DataTable GetMaterialByTechnology(int Technology)
        {
            try
            {
                string SQL = "select * from dbo.View_Technology_Material where Cve_Tecnologia=@Technology";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Technology",Technology)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get K_CREDITO_SUSTITUCION failed: Execute method Get_K_CREDITO_SUSTITUCIONByNo_Credito in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }

        // added by tina 2012-03-13 //updated by tina 2012-08-10
        public int GetMaterialMaxOrderByTechnology(int technology)
        {
            int maxOrder = 0;
            try
            {
                string SQL = "SELECT MAX(Id_Orden) FROM K_TECNOLOGIA_RESIDUO_MATERIAL WHERE Cve_Tecnologia=@Technology";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Technology",technology)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                if (o != null)
                {
                    int.TryParse(o.ToString(), out maxOrder);
                }
                return maxOrder;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get K_CREDITO_SUSTITUCION failed: Execute method Get_K_CREDITO_SUSTITUCIONByNo_Credito in K_CREDITO_SUSTITUCION.", ex, true);
            }
        }
    }
}
