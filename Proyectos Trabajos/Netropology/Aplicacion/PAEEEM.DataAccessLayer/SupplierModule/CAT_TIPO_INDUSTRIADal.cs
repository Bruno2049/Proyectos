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
    /// CAT_TIPO_INDUSTRIADal data access lay
    /// </summary>
    public class CAT_TIPO_INDUSTRIADal
    {
        private static readonly CAT_TIPO_INDUSTRIADal _classinstance = new CAT_TIPO_INDUSTRIADal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_TIPO_INDUSTRIADal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get All CAT_TIPO_INDUSTRIA//get activity type of the enterprise
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TIPO_INDUSTRIA()
        {
            try
            {
                string SQL = "SELECT Cve_Tipo_Industria ,DESCRIPCION_SCIAN FROM CAT_TIPO_INDUSTRIA";
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }


        public DataRow Get_CAT_TIPO_INDUSTRIAByID(int Cve_Tipo_Industria)
        {
            try
            {
                string SQL = "SELECT Cve_Tipo_Industria, Dx_Tipo_Industria, Dt_Fecha_Industria FROM CAT_TIPO_INDUSTRIA where Cve_Tipo_Industria =" + Cve_Tipo_Industria.ToString();
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt.Rows[0];
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        
    }
}
