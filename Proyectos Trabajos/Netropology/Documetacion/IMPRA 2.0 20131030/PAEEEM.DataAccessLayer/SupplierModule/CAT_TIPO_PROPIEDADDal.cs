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
    /// Data Access Layer for propiedad type
    /// </summary>
    public class CAT_TIPO_PROPIEDADDal
    {
        private static readonly CAT_TIPO_PROPIEDADDal _classinstance = new CAT_TIPO_PROPIEDADDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_TIPO_PROPIEDADDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get all propiedad types
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TIPO_PROPIEDAD()
        {
            try
            {
                string SQL = "SELECT Cve_Tipo_Propiedad,Dx_Tipo_Propiedad,Dt_Tipo_Propiedad FROM CAT_TIPO_PROPIEDAD";
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
