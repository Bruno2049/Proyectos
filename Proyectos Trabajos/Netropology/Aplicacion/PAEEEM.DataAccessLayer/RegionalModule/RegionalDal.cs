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
    /// Regional Data Access Layer For table CAT_REGION
    /// </summary>
    public class RegionalDal
    {
        /// <summary>
        /// readonly class instance
        /// </summary>
        private static readonly RegionalDal _classinstance = new RegionalDal();
        /// <summary>
        /// Property
        /// </summary>
        public static RegionalDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Add a new regional
        /// </summary>
        /// <param name="RegionalModel"></param>
        /// <returns></returns>
        public int AddRegional(RegionalEntity RegionalModel)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "INSERT INTO [dbo].[CAT_REGION]([Cve_Region],[Dx_Nombre_Region],[Dx_Nombre_Responsable],[Dx_Puesto_Regional],[Dt_Fecha_Region]) " +
                            "VALUES(@RegionalID,@RegionalName,@RegionalResName,@RegionalPuesto,@RegionalDate)";
                SqlParameter[] paras = new SqlParameter[] {
                    new SqlParameter("@RegionalID", RegionalModel.RegionalID),
                    new SqlParameter("@RegionalName", RegionalModel.RegionalNombre),
                    new SqlParameter("@RegionalResName", RegionalModel.ResponsibleNombre),
                    new SqlParameter("@RegionalPuesto", RegionalModel.RegionalPuesto),
                    new SqlParameter("@RegionalDate", RegionalModel.RegionalFetch)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add new regional failed: Execute method Addregional in RegionalDal.", ex, true);
            }

            return iResult;
        }
        /// <summary>
        /// Get regionals without any parameters
        /// </summary>
        /// <returns></returns>
        public DataTable GetRegionals()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Cve_Region],[Dx_Nombre_Region],[Dx_Nombre_Responsable],[Dx_Puesto_Regional],[Dt_Fecha_Region] " +
                            "FROM [dbo].[CAT_REGION] ORDER BY Dx_Nombre_Region asc";                

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get regionals failed: Execute method GetRegionals in RegionalDal.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// Get regionals with regionalID
        /// </summary>
        /// <returns></returns>
        public DataTable GetRegionalsByPK(int regional)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Cve_Region],[Dx_Nombre_Region],[Dx_Nombre_Responsable],[Dx_Puesto_Regional],[Dt_Fecha_Region] " +
                            "FROM [dbo].[CAT_REGION] WHERE Cve_Region=@regional";
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@regional",regional)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL,para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get regionals failed: Execute method GetRegionalsByPK in RegionalDal.", ex, true);
            }

            return dtResult;
        }

        // RSA 20130830 log credit canceled because of service code invalid when authorization was tried
        /// <summary>
        /// Log Credit cancellation
        /// </summary>
        /// <param name="RegionalModel"></param>
        /// <returns></returns>
        public int LogCreditCanceled(string CreditNumber, string user, string Error)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "INSERT INTO [dbo].[Log_Canceled]([Dt_Fecha], [Dx_Usr], [No_Credito], [Dx_Error]) " +
                            "VALUES(@Dt_Fecha, @Dx_Usr, @No_Credito, @Dx_Error)";
                SqlParameter[] paras = new SqlParameter[] {
                    new SqlParameter("@Dt_Fecha", DateTime.Now),
                    new SqlParameter("@Dx_Usr", user),
                    new SqlParameter("@No_Credito", CreditNumber),
                    new SqlParameter("@Dx_Error", Error),
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Log Credito Cancelado: Execute method LogCreditCanceled in RegionalDal.", ex, true);
            }

            return iResult;
        }

    }
}
