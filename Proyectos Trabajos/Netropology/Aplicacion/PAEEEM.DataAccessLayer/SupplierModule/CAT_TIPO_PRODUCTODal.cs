/*
	Copyright 2011
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     coco
	$Date:       2011-08-23
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
    /// Product type
    /// </summary>
    public class CAT_TIPO_PRODUCTODal
    {
        private static readonly CAT_TIPO_PRODUCTODal _classinstance = new CAT_TIPO_PRODUCTODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_TIPO_PRODUCTODal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get All CAT_TIPO_INDUSTRIA//get activity type of the enterprise
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CAT_TIPO_PRODUCTOByTechnology(string strTechnology)
        {
            try
            {
                string SQL = "select * from CAT_TIPO_PRODUCTO";
                if (!strTechnology.Equals(""))
                {
                    SQL = SQL + " where Cve_Tecnologia in (" + strTechnology + ")";
                }
                SQL = SQL + " ORDER BY Dx_Tipo_Producto";
                //SqlParameter[] paras = new SqlParameter[] {                     
                //    new SqlParameter("@Cve_Tecnologia",strTechnology)
                //};
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get product type
        /// </summary>
        /// <param name="strPk">product type id</param>
        /// <returns></returns>
        public DataTable Get_CAT_TIPO_PRODUCTOByPk(string strPk)
        {
            try
            {
                string SQL = "select * from CAT_TIPO_PRODUCTO where Ft_Tipo_Producto =@Ft_Tipo_Producto";
                SqlParameter[] paras = new SqlParameter[] {                     
                    new SqlParameter("@Ft_Tipo_Producto",strPk)
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
        }
        public string Get_TechnologyByPk(string strPk)
        {
            string Result = "";
            try
            {
                string SQL = "select Cve_Tecnologia from CAT_TIPO_PRODUCTO where Ft_Tipo_Producto =@Ft_Tipo_Producto";
                SqlParameter[] paras = new SqlParameter[] {                     
                    new SqlParameter("@Ft_Tipo_Producto",strPk)
                };
                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                if (o != null)
                {
                    Result = o.ToString();
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, ex.Message, ex, true);
            }
            return Result;
        }
    }
}
