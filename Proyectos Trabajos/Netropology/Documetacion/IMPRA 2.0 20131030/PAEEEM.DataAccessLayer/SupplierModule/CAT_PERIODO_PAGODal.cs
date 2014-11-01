/*	
	$Author:     coco,wang
	$Date:       2011-07-20	
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
    /// Data Access Layer for period pago
    /// </summary>
    public class CAT_PERIODO_PAGODal
    {
        /// <summary>
        /// Property field
        /// </summary>
        private static readonly CAT_PERIODO_PAGODal _classinstance = new CAT_PERIODO_PAGODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_PERIODO_PAGODal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get all period pago
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ALL_CAT_PERIODO_PAGO()
        {
            DataTable dt = new DataTable();
            try
            {
                string SQL = "SELECT [Cve_Periodo_Pago],[Dx_Periodo_Pago],[Dx_Etiqueta_Pagos],[Dx_Ciclo],[Dt_Periodo_Pago] FROM [CAT_PERIODO_PAGO]";
                dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp,CommandType.Text,SQL);
              
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return dt;
        }
    }
}
