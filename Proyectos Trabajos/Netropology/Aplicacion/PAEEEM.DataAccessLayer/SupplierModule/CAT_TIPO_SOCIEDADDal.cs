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
    /// Data Access Layer for sociedad type
    /// </summary>
     public class CAT_TIPO_SOCIEDADDal
    {
         private static readonly CAT_TIPO_SOCIEDADDal _classinstance = new CAT_TIPO_SOCIEDADDal();
        /// <summary>
        /// Class Instance
        /// </summary>
         public static CAT_TIPO_SOCIEDADDal ClassInstance { get { return _classinstance; } }
         /// <summary>
         /// Get all sociedad types
         /// </summary>
         /// <returns></returns>
         public DataTable Get_All_CAT_TIPO_SOCIEDAD()
         {
             try
             {
                 string SQL = "SELECT Cve_Tipo_Sociedad, Dx_Tipo_Sociedad FROM CAT_TIPO_SOCIEDAD";
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
