/* ----------------------------------------------------------------------
 * File Name: CAT_ESTATUS_PRODUCTODal.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/11/29
 *
 * Description:   CAT_ESTATUS_PRODUCTODal data access lay
 *----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    public class CAT_ESTATUS_PRODUCTODal
    {
        private static readonly CAT_ESTATUS_PRODUCTODal _classinstance = new CAT_ESTATUS_PRODUCTODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_ESTATUS_PRODUCTODal ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get All the Estado
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_ESTATUS_PRODUCTO()
        {
            try
            {
                string SQL = "select * from CAT_ESTATUS_PRODUCTO";
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all Estado of product failed: Execute method Get_All_CAT_ESTATUS_PRODUCTO in CAT_ESTATUS_PRODUCTODal.", ex, true);
            }
        }
    }
}
