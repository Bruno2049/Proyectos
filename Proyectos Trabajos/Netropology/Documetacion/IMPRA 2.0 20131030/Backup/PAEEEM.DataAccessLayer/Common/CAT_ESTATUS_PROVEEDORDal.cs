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
    /// Provider status
    /// </summary>
    public class CAT_ESTATUS_PROVEEDORDal
    {
        /// <summary>
        /// Private class instance
        /// </summary>
        private static readonly CAT_ESTATUS_PROVEEDORDal _classInstance = new CAT_ESTATUS_PROVEEDORDal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static CAT_ESTATUS_PROVEEDORDal ClassInstance { get { return _classInstance; } }

        /// <summary>
        /// Get disposal center status
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Provider_Estatus()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "select * from CAT_ESTATUS_PROVEEDOR";

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Provider status failed: Execute method Get_Provider_Estatus in CAT_ESTATUS_PROVEEDORDal.", ex, true);
            }
            return dtResult;
        }
    }
}
