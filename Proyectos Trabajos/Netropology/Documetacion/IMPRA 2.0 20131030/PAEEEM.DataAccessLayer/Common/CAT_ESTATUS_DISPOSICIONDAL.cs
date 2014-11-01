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
    /// Disposal Center Status
    /// </summary>
    public class CAT_ESTATUS_DISPOSICIONDAL
    {
        /// <summary>
        /// Private class instance
        /// </summary>
        private static readonly CAT_ESTATUS_DISPOSICIONDAL _classInstance = new CAT_ESTATUS_DISPOSICIONDAL();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static CAT_ESTATUS_DISPOSICIONDAL ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// Get credit status
        /// </summary>
        /// <returns></returns>
        public DataTable GetDispositionEstatus()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Cve_Estatus_Disposicion],[Dx_Estatus_Disposicion],[Dt_Estatus_Disposicion] " +
                            "FROM [dbo].[CAT_ESTATUS_DISPOSICION]";

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Disposition status failed: Execute method GetDispositionEstatus in CAT_ESTATUS_DISPOSICIONDAL.", ex, true);
            }
            return dtResult;
        }
    }
}
