/* ----------------------------------------------------------------------
 * File Name: CAT_ESTADODAL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description:   CAT_ESTADO data access lay
 *----------------------------------------------------------------------*/

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
    /// CAT_ESTADO data access lay
    /// </summary>
    public class CAT_ESTADODAL
    {
        private static readonly CAT_ESTADODAL _classinstance = new CAT_ESTADODAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_ESTADODAL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get All the Estado
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_ESTADO()
        {
            try
            {
                string SQL = "SELECT Cve_Estado,Dx_Nombre_Estado,Dt_Estado FROM CAT_ESTADO order by 2";
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all Estado failed: Execute method Get_All_CAT_ESTADO in CAT_ESTADO.", ex, true);
            }
        }
    }
}
