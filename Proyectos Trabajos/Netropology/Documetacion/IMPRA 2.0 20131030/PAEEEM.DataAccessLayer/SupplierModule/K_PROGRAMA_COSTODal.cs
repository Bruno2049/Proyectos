/*	
	$Author:     coco,wang
	$Date:       2011-07-12
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Data Access Layer for program related cost
    /// </summary>
    public class K_PROGRAMA_COSTODal
    {
        private static readonly K_PROGRAMA_COSTODal _classinstance = new K_PROGRAMA_COSTODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static K_PROGRAMA_COSTODal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get program related cost
        /// </summary>
        /// <param name="strPk"></param>
        /// <returns></returns>
        public DataTable Get_All_K_PROGRAMA_COSTO(string strPk)
        {
            try
            {
                string SQL = "select * from K_PROGRAMA_COSTO  where ID_Prog_Proy=@ID_Prog_Proy";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@ID_Prog_Proy", strPk)  
                 };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
    }
}
