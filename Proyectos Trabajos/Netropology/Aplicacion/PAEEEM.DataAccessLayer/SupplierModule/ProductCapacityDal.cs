/*
	Copyright IMPRA, Inc. 2010
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     $
	$Date:       $
	Log at end of file
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
    /// Data Access Layer For Product Capacity
    /// </summary>
    public class ProductCapacityDal
    {
        /// <summary>
        /// Readonly variable for class instance
        /// </summary>
        private static readonly ProductCapacityDal _classInstance = new ProductCapacityDal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static ProductCapacityDal ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// Get product capacity
        /// </summary>
        /// <param name="strTechnologyID">technology</param>
        /// <returns></returns>
        public DataTable Get_ProductCapacity(string strTechnologyID)
        {
            try
            {

                //string SQL = "select * from CAT_PRODUCTO_CAPACIDAD  where Cve_Tecnologia in ("+ strTechnologyID+")";
                StringBuilder SQL = new StringBuilder();

                SQL.Append("SELECT CS.Cve_Capacidad_Sust Ft_Capacidad,CASE WHEN CT.Cve_Esquema = 1 THEN");
                SQL.Append("       CS.Dx_Capacidad ELSE  CAST(CS.No_Capacidad AS VARCHAR) END AS Cve_Producto_Capacidad ");
                SQL.Append("  FROM CAT_TECNOLOGIA CT, CAT_CAPACIDAD_SUSTITUCION CS");
                SQL.Append(" WHERE CT.Cve_Tecnologia = CS.Cve_Tecnologia");
                SQL.Append("   AND CS.Cve_Tecnologia in (" + strTechnologyID + ")");

                //string SQL = "select * from CAT_CAPACIDAD_SUSTITUCION  where Cve_Tecnologia in (" + strTechnologyID + ")";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Tecnologia", strTechnologyID)  
                 };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL.ToString(), paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
    }
}
