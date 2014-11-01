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
    /// Supplier Data Access Layer For Table CAT_PROVEEDOR
    /// </summary>
    public class SupplierDal
    {
        /// <summary>
        /// readonly class instance
        /// </summary>
        private static readonly SupplierDal _classinstance = new SupplierDal();
        /// <summary>
        /// Property
        /// </summary>
        public static SupplierDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get suppliers with zone parameter
        /// </summary>
        /// <param name="zone">zone</param>
        /// <returns></returns>
        public DataTable GetSuppliers(int zone)
        {
            DataTable dtResult = null;
            string SQL = "";
            string WhereClause = "WHERE [Cve_Estatus_Proveedor] = 2";
            try
            {
                if (zone > 0)
                {
                    WhereClause += " AND [Cve_Zona] = "+zone;
                }
                SQL = "SELECT [Id_Proveedor],[Cve_Estatus_Proveedor],[Cve_Region],[Dx_Razon_Social],[Dx_Nombre_Comercial],[Dx_RFC],[Dx_Domicilio_Part_Calle],[Dx_Domicilio_Part_Num] "+
                            " ,[Dx_Domicilio_Part_CP],[Cve_Deleg_Municipio_Part],[Cve_Estado_Part],[Fg_Mismo_Domicilio],[Dx_Domicilio_Fiscal_Calle],[Dx_Domicilio_Fiscal_Num],[Dx_Domicilio_Fiscal_CP] "+
                            ",[Cve_Deleg_Municipio_Fisc],[Cve_Estado_Fisc],[Dx_Nombre_Repre],[Dx_Email_Repre],[Dx_Telefono_Repre],[Dx_Nombre_Repre_Legal],[Dx_Nombre_Banco],[Dx_Cuenta_Banco] "+
                            ",[Binary_Acta_Constitutiva],[Binary_Poder_Notarial],[Pct_Tasa_IVA],[Dt_Fecha_Proveedor], [Cve_Zona] "+
                            " FROM [dbo].[CAT_PROVEEDOR] "+WhereClause+" ORDER BY Dx_Razon_Social ASC";
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get suppliers without any parameters failed: Execute method GetSuppliers in SupplierDal.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// Get suppliers with region parameter
        /// </summary>
        /// <param name="region">region</param>
        /// <returns></returns>
        public DataTable GetRegionSuppliers( int region )
        {
            if ( region < 0 )
            {
                throw new ArgumentException("Parameter is not valid.", "Regional");
            }

            DataTable dtResult = null;
            string SQL = "";
            
            try
            {
                SQL = "SELECT [Id_Proveedor],[Cve_Estatus_Proveedor],[Cve_Region],[Dx_Razon_Social],[Dx_Nombre_Comercial],[Dx_RFC],[Dx_Domicilio_Part_Calle],[Dx_Domicilio_Part_Num] " +
                            " ,[Dx_Domicilio_Part_CP],[Cve_Deleg_Municipio_Part],[Cve_Estado_Part],[Fg_Mismo_Domicilio],[Dx_Domicilio_Fiscal_Calle],[Dx_Domicilio_Fiscal_Num],[Dx_Domicilio_Fiscal_CP] " +
                            ",[Cve_Deleg_Municipio_Fisc],[Cve_Estado_Fisc],[Dx_Nombre_Repre],[Dx_Email_Repre],[Dx_Telefono_Repre],[Dx_Nombre_Repre_Legal],[Dx_Nombre_Banco],[Dx_Cuenta_Banco] " +
                            ",[Binary_Acta_Constitutiva],[Binary_Poder_Notarial],[Pct_Tasa_IVA],[Dt_Fecha_Proveedor], [Cve_Zona] " +
                            " FROM [dbo].[CAT_PROVEEDOR]  WHERE [Cve_Region] = @Cve_Region AND [Cve_Estatus_Proveedor] = 2 ORDER BY Dx_Razon_Social ASC";
                SqlParameter[] paras = new SqlParameter[] {
                    new SqlParameter("@Cve_Region", region)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch ( SqlException ex )
            {
                throw new LsDAException(this, "Get suppliers with region failed: Execute method GetSuppliers in SupplierDal.", ex, true);
            }
            return dtResult;
        }
    }
}
