/*	
	$Author:     coco,wang
	$Date:       2011-07-06	
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
    /// CAT_REGIMEN_CONYUGALDal data access lay
    /// </summary>
    public class CAT_REGIMEN_CONYUGALDal
    {
        private static readonly CAT_REGIMEN_CONYUGALDal _classinstance = new CAT_REGIMEN_CONYUGALDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_REGIMEN_CONYUGALDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get All CAT_REGIMEN_CONYUGAL
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_REGIMEN_CONYUGAL()
        {
            try
            {
                string SQL = "SELECT Cve_Reg_Conyugal_Repre_legal,Dx_Reg_Conyugal_Repre_legal,Dt_Reg_Conyugal_Repre_legal FROM CAT_REGIMEN_CONYUGAL";
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
    }
}
