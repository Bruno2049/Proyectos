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
    /// Zona Data Access Layer
    /// </summary>
    public class CatZonaDal
    {
        /// <summary>
        /// Private class instance
        /// </summary>
        private static readonly CatZonaDal _classInstance = new CatZonaDal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static CatZonaDal ClassInstance { get { return _classInstance; } }
        /// <summary>
        /// Get zones
        /// </summary>
        /// <param name="regional"></param>
        /// <returns></returns>
        public DataTable GetZonaWithRegional(int regional)
        {
            DataTable dtZone = null;
            string SQL = "";
            try
            {
                SQL = @"SELECT [Cve_Zona],[Cve_Region],[Dx_Nombre_Zona],[Dx_Nombre_Responsable],[Dx_Puesto_Zona],[Dt_Fecha_Zona]
                                FROM [dbo].[CAT_ZONA] WHERE [Cve_Region] = @Cve_Region";
                SqlParameter[] Paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Region", regional)
                };

                dtZone = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, Paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get zones failed in method GetZonaWithRegional in module:CatZonaDal.", ex, true);
            }

            return dtZone;
        }
        public DataTable GetAllZone()
        {
            DataTable dtZone = null;
            string SQL = "";
            try
            {
                SQL = @"SELECT [Cve_Zona],[Cve_Region],[Dx_Nombre_Zona],[Dx_Nombre_Responsable],[Dx_Puesto_Zona],[Dt_Fecha_Zona]
                                FROM [dbo].[CAT_ZONA]";               

                dtZone = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get zones failed in method GetAllZone in module:CatZonaDal.", ex, true);
            }

            return dtZone;
        }
        public DataTable GetAllRegion()
        {
            DataTable dtRegional = null;
            string SQL = "";
            try
            {
                SQL = @"SELECT [Cve_Region],[Dx_Nombre_Region]
                                FROM [dbo].[CAT_Region] (nolock)";

                dtRegional = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get Regions failed in method GetAllZone in module:CatZonaDal.", ex, true);
            }

            return dtRegional;
        }
    }
}
