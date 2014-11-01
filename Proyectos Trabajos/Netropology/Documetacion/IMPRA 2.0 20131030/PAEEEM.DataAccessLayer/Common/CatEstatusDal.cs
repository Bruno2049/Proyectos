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
    /// Data Access Layer model for table CAT_ESTATUS
    /// </summary>
    public class CatEstatusDal
    {
        /// <summary>
        /// Private class instance
        /// </summary>
        private static readonly CatEstatusDal _classInstance = new CatEstatusDal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static CatEstatusDal ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// Get credit status
        /// </summary>
        /// <returns></returns>
        public DataTable GetCreditEstatus()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Cve_Estatus_Credito],[Dx_Estatus_Credito],[Dt_Fecha_Estatus_Credito] "+
                            "FROM [dbo].[CAT_ESTATUS_CREDITO]";

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credit status failed: Execute method GetCreditEstatus in CatEstatusDal.", ex, true);
            }
            return dtResult;
        }
    }
}
