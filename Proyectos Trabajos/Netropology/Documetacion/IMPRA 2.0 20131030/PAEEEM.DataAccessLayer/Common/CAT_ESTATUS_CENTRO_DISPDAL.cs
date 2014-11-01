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
    public class CAT_ESTATUS_CENTRO_DISPDAL
    {
        /// <summary>
        /// Private class instance
        /// </summary>
        private static readonly CAT_ESTATUS_CENTRO_DISPDAL _classInstance = new CAT_ESTATUS_CENTRO_DISPDAL();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static CAT_ESTATUS_CENTRO_DISPDAL ClassInstance { get { return _classInstance; } }

        /// <summary>
        /// Get disposal center status
        /// </summary>
        /// <returns></returns>
        public DataTable GetDisposalCenterEstatus()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Cve_Estatus_Centro_Disp],[Dx_Estatus_Centro_Disp],[Dt_Estatus_Centro_Disp] " +
                            "FROM [dbo].[CAT_ESTATUS_CENTRO_DISP]";

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Disposal Center status failed: Execute method GetDisposalCenterEstatus in CAT_ESTATUS_CENTRO_DISPDAL.", ex, true);
            }
            return dtResult;
        }
    }
}
