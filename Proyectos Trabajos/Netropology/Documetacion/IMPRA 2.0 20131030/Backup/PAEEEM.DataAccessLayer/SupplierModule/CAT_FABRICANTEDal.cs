/*	
	$Author:     coco,wang
	$Date:       2011-07-07	
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
    /// Data Access Layer for fabricator
    /// </summary>
    public class CAT_FABRICANTEDal
    {
        private static readonly CAT_FABRICANTEDal _classinstance = new CAT_FABRICANTEDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_FABRICANTEDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get all fabricator
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_FABRICANTE()
        {
            try
            {
                string SQL = "SELECT Cve_Fabricante,Dx_Nombre_Fabricante,Dt_Fecha_Fabricante FROM CAT_FABRICANTE ORDER BY 2";
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
