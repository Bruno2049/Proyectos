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
    /// CAT_DELEG_MUNICIPIO data access lay
    /// </summary>
    public class CAT_DELEG_MUNICIPIODAL
    {
        private static readonly CAT_DELEG_MUNICIPIODAL _classinstance = new CAT_DELEG_MUNICIPIODAL();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static CAT_DELEG_MUNICIPIODAL ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get All the DELEG_MUNICIPIODAL
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_DELEG_MUNICIPIO()
        {
            try
            {
                string SQL = "SELECT * FROM CAT_DELEG_MUNICIPIO ORDER BY 2";//edit by coco 20110811
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all Estado failed: Execute method Get_All_CAT_DELEG_MUNICIPIO in CAT_DELEG_MUNICIPIO.", ex, true);
            }
        }
        /// <summary>
        /// Get the DELEG_MUNICIPIODAL By Estado
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CAT_DELEG_MUNICIPIOByEstado(int Cve_Estado)
        {
            try
            {
                string SQL = "SELECT Cve_Deleg_Municipio,Dx_Deleg_Municipio,Dt_Deleg_Municipio FROM CAT_DELEG_MUNICIPIO WHERE @Cve_Estado=-1 OR Cve_Estado=@Cve_Estado ORDER BY 2";
                SqlParameter[] para = new SqlParameter[]
                    {
                        new SqlParameter ("@Cve_Estado", Cve_Estado)
                    };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all Estado failed: Execute method Get_All_CAT_DELEG_MUNICIPIO in CAT_DELEG_MUNICIPIO.", ex, true);
            }
        }
    }
}
