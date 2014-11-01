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
    /// CAT_IDENTIFICACION data access lay
    /// </summary>
    public class CAT_IDENTIFICACIONDal
    {
        private static readonly CAT_IDENTIFICACIONDal _classinstance = new CAT_IDENTIFICACIONDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_IDENTIFICACIONDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get All CAT_IDENTIFICACION
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_IDENTIFICACION()
        {
            try
            {
                string SQL = "SELECT Cve_Identificacion_Repre_legal,Dx_Identificacion_Repre_legal,Dt_Identificacion_Repre_legal FROM  CAT_IDENTIFICACION";
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
