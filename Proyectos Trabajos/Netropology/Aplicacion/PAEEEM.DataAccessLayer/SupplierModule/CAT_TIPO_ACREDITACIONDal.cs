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
    /// Data Access Layer for accreditation type
    /// </summary>
    public class CAT_TIPO_ACREDITACIONDal
    {
        /// <summary>
        /// Class Instance field
        /// </summary>
        private static readonly CAT_TIPO_ACREDITACIONDal _classinstance = new CAT_TIPO_ACREDITACIONDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_TIPO_ACREDITACIONDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get All CAT_TIPO_ACREDITACION
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TIPO_ACREDITACION()
        {
            try
            {
                string SQL = "SELECT Cve_Acreditacion_Repre_legal,Dx_Acreditacion_Repre_Legal,Dt_Fecha_Acreditacion FROM CAT_TIPO_ACREDITACION";
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
