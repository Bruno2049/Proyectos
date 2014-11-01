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
    /// Supplier branches
    /// </summary>
    public class SupplierBrancheDal
    {
        /// <summary>
        /// readonly class instance
        /// </summary>
        private static readonly SupplierBrancheDal _classinstance = new SupplierBrancheDal();
        /// <summary>
        /// Property
        /// </summary>
        public static SupplierBrancheDal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Get supplier branches with supplier and zone
        /// </summary>
        /// <param name="SupplierID">Supplier Id</param>
        /// <param name="zoneID">Zone ID</param>
        /// <returns></returns>
        public DataTable GetSupplierBranches(int SupplierID, int zoneID)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Id_Branch],[Id_Proveedor] ,[Cve_Estatus_Proveedor],[Cve_Region] ,[Dx_Razon_Social],[Dx_Nombre_Comercial] ,[Dx_RFC] " +
                            " ,[Dx_Domicilio_Part_Calle],[Dx_Domicilio_Part_Num],[Dx_Domicilio_Part_CP],[Cve_Deleg_Municipio_Part] ,[Cve_Estado_Part] ,[Fg_Mismo_Domicilio] " +
                            ",[Dx_Domicilio_Fiscal_Calle],[Dx_Domicilio_Fiscal_Num] ,[Dx_Domicilio_Fiscal_CP] ,[Cve_Deleg_Municipio_Fisc] ,[Cve_Estado_Fisc],[Dx_Nombre_Repre] ,[Dx_Email_Repre] " +
                            ",[Dx_Telefono_Repre],[Dx_Nombre_Repre_Legal],[Dx_Nombre_Banco],[Dx_Cuenta_Banco],[Binary_Acta_Constitutiva],[Binary_Poder_Notarial] " +
                            " ,[Pct_Tasa_IVA],[Dt_Fecha_Branch], [Cve_Zona] FROM [dbo].[CAT_PROVEEDORBRANCH] WHERE [Id_Proveedor] = @Supplier AND [Cve_Zona] = @Cve_Zona ORDER BY Dx_Razon_Social ASC";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Supplier", SupplierID),
                    new SqlParameter("@Cve_Zona", zoneID)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get supplier branches with supplier failed: Execute method GetSupplierBranches in SupplierBrancheDal.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Get supplier branch
        /// </summary>
        /// <param name="supplierBranchID">Supplier Branch ID</param>
        /// <returns></returns>
        public DataTable GetSupplierBranch(int supplierBranchID)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Id_Branch],[Id_Proveedor],[Cve_Estatus_Proveedor],[Cve_Region],[Dx_Razon_Social],[Dx_Nombre_Comercial] " +
                            " ,[Dx_RFC],[Dx_Domicilio_Part_Calle],[Dx_Domicilio_Part_Num],[Dx_Domicilio_Part_CP],[Cve_Deleg_Municipio_Part],[Cve_Estado_Part] " +
                            ",[Fg_Mismo_Domicilio],[Dx_Domicilio_Fiscal_Calle],[Dx_Domicilio_Fiscal_Num],[Dx_Domicilio_Fiscal_CP],[Cve_Deleg_Municipio_Fisc] " +
                            ",[Cve_Estado_Fisc],[Dx_Nombre_Repre],[Dx_Email_Repre],[Dx_Telefono_Repre],[Dx_Nombre_Repre_Legal],[Dx_Nombre_Banco] " +
                            " ,[Dx_Cuenta_Banco],[Binary_Acta_Constitutiva],[Binary_Poder_Notarial],[Pct_Tasa_IVA],[Dt_Fecha_Branch],[Cve_Zona] " +
                            " FROM [dbo].[CAT_PROVEEDORBRANCH] WHERE [Id_Branch] = @SupplierBranch";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@SupplierBranch", supplierBranchID)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get supplier branch with supplier branch id failed: Execute method GetSupplierBranch in SupplierBrancheDal.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// Get supplier branch
        /// </summary>
        /// <param name="supplierID">Supplier ID</param>
        /// <returns></returns>
        public DataTable GetSupplierBranchWithSupplier( int supplierID)
        {
            if ( supplierID < 0 )
            {
                throw new ArgumentException("Parameter is not valid.", "Supplier ID");
            }

            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Id_Branch],[Id_Proveedor],[Cve_Estatus_Proveedor],[Cve_Region],[Dx_Razon_Social],[Dx_Nombre_Comercial] " +
                            " ,[Dx_RFC],[Dx_Domicilio_Part_Calle],[Dx_Domicilio_Part_Num],[Dx_Domicilio_Part_CP],[Cve_Deleg_Municipio_Part],[Cve_Estado_Part] " +
                            ",[Fg_Mismo_Domicilio],[Dx_Domicilio_Fiscal_Calle],[Dx_Domicilio_Fiscal_Num],[Dx_Domicilio_Fiscal_CP],[Cve_Deleg_Municipio_Fisc] " +
                            ",[Cve_Estado_Fisc],[Dx_Nombre_Repre],[Dx_Email_Repre],[Dx_Telefono_Repre],[Dx_Nombre_Repre_Legal],[Dx_Nombre_Banco] " +
                            " ,[Dx_Cuenta_Banco],[Binary_Acta_Constitutiva],[Binary_Poder_Notarial],[Pct_Tasa_IVA],[Dt_Fecha_Branch],[Cve_Zona] " +
                            " FROM [dbo].[CAT_PROVEEDORBRANCH] WHERE [Id_Proveedor] = @Supplier";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Supplier", supplierID)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch ( SqlException ex )
            {
                throw new LsDAException(this, "Get supplier branch with supplier branch id failed: Execute method GetSupplierBranch in SupplierBrancheDal.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// Get supplier branches with supplier and zone
        /// </summary>
        /// <param name="proveedor"></param>
        /// <param name="zone"></param>
        /// <returns></returns>
        public DataTable GetSupplierBranch(int proveedor, int zone)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Id_Branch],[Id_Proveedor],[Cve_Estatus_Proveedor],[Cve_Region],[Dx_Razon_Social],[Dx_Nombre_Comercial] " +
                            " ,[Dx_RFC],[Dx_Domicilio_Part_Calle],[Dx_Domicilio_Part_Num],[Dx_Domicilio_Part_CP],[Cve_Deleg_Municipio_Part],[Cve_Estado_Part] " +
                            ",[Fg_Mismo_Domicilio],[Dx_Domicilio_Fiscal_Calle],[Dx_Domicilio_Fiscal_Num],[Dx_Domicilio_Fiscal_CP],[Cve_Deleg_Municipio_Fisc] " +
                            ",[Cve_Estado_Fisc],[Dx_Nombre_Repre],[Dx_Email_Repre],[Dx_Telefono_Repre],[Dx_Nombre_Repre_Legal],[Dx_Nombre_Banco] " +
                            " ,[Dx_Cuenta_Banco],[Binary_Acta_Constitutiva],[Binary_Poder_Notarial],[Pct_Tasa_IVA],[Dt_Fecha_Branch], [Cve_Zona] " +
                            " FROM [dbo].[CAT_PROVEEDORBRANCH] WHERE  [Cve_Zona] = @Cve_Zona";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Zona", zone)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get supplier branch with supplier and zone failed: Execute method GetSupplierBranch in SupplierBrancheDal.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Get supplier branches with region
        /// </summary>
        /// <param name="region"></param>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        public DataTable GetSupplierBranchesWithRegion(int region, int proveedor)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Id_Branch],[Id_Proveedor],[Cve_Estatus_Proveedor],PB.[Cve_Region],[Dx_Razon_Social],[Dx_Nombre_Comercial] " +
                            " ,[Dx_RFC],[Dx_Domicilio_Part_Calle],[Dx_Domicilio_Part_Num],[Dx_Domicilio_Part_CP],[Cve_Deleg_Municipio_Part],[Cve_Estado_Part] " +
                            ",[Fg_Mismo_Domicilio],[Dx_Domicilio_Fiscal_Calle],[Dx_Domicilio_Fiscal_Num],[Dx_Domicilio_Fiscal_CP],[Cve_Deleg_Municipio_Fisc] " +
                            ",[Cve_Estado_Fisc],[Dx_Nombre_Repre],[Dx_Email_Repre],[Dx_Telefono_Repre],[Dx_Nombre_Repre_Legal],[Dx_Nombre_Banco] " +
                            " ,[Dx_Cuenta_Banco],[Binary_Acta_Constitutiva],[Binary_Poder_Notarial],[Pct_Tasa_IVA],[Dt_Fecha_Branch], PB.[Cve_Zona] " +
                            " FROM [dbo].[CAT_PROVEEDORBRANCH] AS PB INNER JOIN CAT_ZONA AS Z ON PB.Cve_Zona = Z.Cve_Zona WHERE Z.Cve_Region = @Cve_Region";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Region", region)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get supplier branch with supplier and regional failed: Execute method GetSupplierBranchesWithRegion in SupplierBrancheDal.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Get supplier branches
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        public int GetProveedor(int branch)
        {
            int proveedor=0;
            string SQL = "";
            
            try
            {
                SQL = "SELECT [Id_Proveedor] FROM [dbo].[CAT_PROVEEDORBRANCH] WHERE [Id_Branch] = @Supplier";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Supplier", branch)
                };

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);

                if (o != null)
                {
                    proveedor = int.Parse(o.ToString());
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get supplier failed: Execute method GetProveedor in SupplierBrancheDal.", ex, true);
            }

            return proveedor;
        }
        /// <summary>
        /// Add Record
        /// </summary>
        public int Insert_CAT_PROVEEDORBRANCH(SupplierBranchModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO CAT_PROVEEDORBRANCH (Id_Proveedor,Cve_Estatus_Proveedor, Cve_Region, Dx_Razon_Social, Dx_Nombre_Comercial, Dx_RFC, Dx_Domicilio_Part_Calle, Dx_Domicilio_Part_Num, Dx_Domicilio_Part_CP, Cve_Deleg_Municipio_Part, Cve_Estado_Part, Fg_Mismo_Domicilio, Dx_Domicilio_Fiscal_Calle, Dx_Domicilio_Fiscal_Num, Dx_Domicilio_Fiscal_CP, Cve_Deleg_Municipio_Fisc, Cve_Estado_Fisc, Dx_Nombre_Repre, Dx_Email_Repre, Dx_Telefono_Repre, Dx_Nombre_Repre_Legal, Dx_Nombre_Banco, Dx_Cuenta_Banco, Binary_Acta_Constitutiva, Binary_Poder_Notarial, Pct_Tasa_IVA, Dt_Fecha_Branch,Cve_Zona,Codigo_Branch ) VALUES (@Id_Proveedor,@Cve_Estatus_Proveedor, @Cve_Region, @Dx_Razon_Social, @Dx_Nombre_Comercial, @Dx_RFC, @Dx_Domicilio_Part_Calle, @Dx_Domicilio_Part_Num, @Dx_Domicilio_Part_CP, @Cve_Deleg_Municipio_Part, @Cve_Estado_Part, @Fg_Mismo_Domicilio, @Dx_Domicilio_Fiscal_Calle, @Dx_Domicilio_Fiscal_Num, @Dx_Domicilio_Fiscal_CP, @Cve_Deleg_Municipio_Fisc, @Cve_Estado_Fisc, @Dx_Nombre_Repre, @Dx_Email_Repre, @Dx_Telefono_Repre, @Dx_Nombre_Repre_Legal, @Dx_Nombre_Banco, @Dx_Cuenta_Banco, @Binary_Acta_Constitutiva, @Binary_Poder_Notarial, @Pct_Tasa_IVA, @Dt_Fecha_Branch,@Cve_Zona,@Codigo_Branch)";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Proveedor",instance.Id_Proveedor),
                	new SqlParameter("@Cve_Estatus_Proveedor",instance.Cve_Estatus_Proveedor),
                	new SqlParameter("@Cve_Region",instance.Cve_Region),
                	new SqlParameter("@Dx_Razon_Social",instance.Dx_Razon_Social),
                	new SqlParameter("@Dx_Nombre_Comercial",instance.Dx_Nombre_Comercial),
                	new SqlParameter("@Dx_RFC",instance.Dx_RFC),
                	new SqlParameter("@Dx_Domicilio_Part_Calle",instance.Dx_Domicilio_Part_Calle),
                	new SqlParameter("@Dx_Domicilio_Part_Num",instance.Dx_Domicilio_Part_Num),
                	new SqlParameter("@Dx_Domicilio_Part_CP",instance.Dx_Domicilio_Part_CP),
                	new SqlParameter("@Cve_Deleg_Municipio_Part",instance.Cve_Deleg_Municipio_Part),
                	new SqlParameter("@Cve_Estado_Part",instance.Cve_Estado_Part),
                	new SqlParameter("@Fg_Mismo_Domicilio",instance.Fg_Mismo_Domicilio),
                	new SqlParameter("@Dx_Domicilio_Fiscal_Calle",instance.Dx_Domicilio_Fiscal_Calle),
                	new SqlParameter("@Dx_Domicilio_Fiscal_Num",instance.Dx_Domicilio_Fiscal_Num),
                	new SqlParameter("@Dx_Domicilio_Fiscal_CP",instance.Dx_Domicilio_Fiscal_CP),
                	new SqlParameter("@Cve_Deleg_Municipio_Fisc",instance.Cve_Deleg_Municipio_Fisc),
                	new SqlParameter("@Cve_Estado_Fisc",instance.Cve_Estado_Fisc),
                	new SqlParameter("@Dx_Nombre_Repre",instance.Dx_Nombre_Repre),
                	new SqlParameter("@Dx_Email_Repre",instance.Dx_Email_Repre),
                	new SqlParameter("@Dx_Telefono_Repre",instance.Dx_Telefono_Repre),
                	new SqlParameter("@Dx_Nombre_Repre_Legal",instance.Dx_Nombre_Repre_Legal),
                	new SqlParameter("@Dx_Nombre_Banco",instance.Dx_Nombre_Banco),
                	new SqlParameter("@Dx_Cuenta_Banco",instance.Dx_Cuenta_Banco),
                	new SqlParameter("@Binary_Acta_Constitutiva",instance.Binary_Acta_Constitutiva),
                	new SqlParameter("@Binary_Poder_Notarial",instance.Binary_Poder_Notarial),
                	new SqlParameter("@Pct_Tasa_IVA",instance.Pct_Tasa_IVA),
                	new SqlParameter("@Dt_Fecha_Branch",instance.Dt_Fecha_Branch),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona),
                    new SqlParameter("@Codigo_Branch",instance.Codigo_Branch)
            	};

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add new CAT_PROVEEDORBRANCH failed", ex, true);
            }
        }
        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_CAT_PROVEEDOR(SupplierBranchModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE CAT_PROVEEDORBRANCH SET Id_Proveedor = @Id_Proveedor, Cve_Region = @Cve_Region, Dx_Razon_Social = @Dx_Razon_Social, Dx_Nombre_Comercial = @Dx_Nombre_Comercial, Dx_RFC = @Dx_RFC, Dx_Domicilio_Part_Calle = @Dx_Domicilio_Part_Calle, Dx_Domicilio_Part_Num = @Dx_Domicilio_Part_Num, Dx_Domicilio_Part_CP = @Dx_Domicilio_Part_CP, Cve_Deleg_Municipio_Part = @Cve_Deleg_Municipio_Part, Cve_Estado_Part = @Cve_Estado_Part, Fg_Mismo_Domicilio = @Fg_Mismo_Domicilio, Dx_Domicilio_Fiscal_Calle = @Dx_Domicilio_Fiscal_Calle, Dx_Domicilio_Fiscal_Num = @Dx_Domicilio_Fiscal_Num, Dx_Domicilio_Fiscal_CP = @Dx_Domicilio_Fiscal_CP, Cve_Deleg_Municipio_Fisc = @Cve_Deleg_Municipio_Fisc, Cve_Estado_Fisc = @Cve_Estado_Fisc, Dx_Nombre_Repre = @Dx_Nombre_Repre, Dx_Email_Repre = @Dx_Email_Repre, Dx_Telefono_Repre = @Dx_Telefono_Repre, Dx_Nombre_Repre_Legal = @Dx_Nombre_Repre_Legal, Dx_Nombre_Banco = @Dx_Nombre_Banco, Dx_Cuenta_Banco = @Dx_Cuenta_Banco, Binary_Acta_Constitutiva = @Binary_Acta_Constitutiva, Binary_Poder_Notarial = @Binary_Poder_Notarial, Pct_Tasa_IVA = @Pct_Tasa_IVA, Dt_Fecha_Branch = @Dt_Fecha_Branch,Cve_Zona = @Cve_Zona WHERE Id_Branch = @Id_Branch";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("Id_Proveedor",instance.Id_Proveedor),
					new SqlParameter("@Cve_Region",instance.Cve_Region),
					new SqlParameter("@Dx_Razon_Social",instance.Dx_Razon_Social),
					new SqlParameter("@Dx_Nombre_Comercial",instance.Dx_Nombre_Comercial),
					new SqlParameter("@Dx_RFC",instance.Dx_RFC),
					new SqlParameter("@Dx_Domicilio_Part_Calle",instance.Dx_Domicilio_Part_Calle),
					new SqlParameter("@Dx_Domicilio_Part_Num",instance.Dx_Domicilio_Part_Num),
					new SqlParameter("@Dx_Domicilio_Part_CP",instance.Dx_Domicilio_Part_CP),
					new SqlParameter("@Cve_Deleg_Municipio_Part",instance.Cve_Deleg_Municipio_Part),
					new SqlParameter("@Cve_Estado_Part",instance.Cve_Estado_Part),
					new SqlParameter("@Fg_Mismo_Domicilio",instance.Fg_Mismo_Domicilio),
					new SqlParameter("@Dx_Domicilio_Fiscal_Calle",instance.Dx_Domicilio_Fiscal_Calle),
					new SqlParameter("@Dx_Domicilio_Fiscal_Num",instance.Dx_Domicilio_Fiscal_Num),
					new SqlParameter("@Dx_Domicilio_Fiscal_CP",instance.Dx_Domicilio_Fiscal_CP),
					new SqlParameter("@Cve_Deleg_Municipio_Fisc",instance.Cve_Deleg_Municipio_Fisc),
					new SqlParameter("@Cve_Estado_Fisc",instance.Cve_Estado_Fisc),
					new SqlParameter("@Dx_Nombre_Repre",instance.Dx_Nombre_Repre),
					new SqlParameter("@Dx_Email_Repre",instance.Dx_Email_Repre),
					new SqlParameter("@Dx_Telefono_Repre",instance.Dx_Telefono_Repre),
					new SqlParameter("@Dx_Nombre_Repre_Legal",instance.Dx_Nombre_Repre_Legal),
					new SqlParameter("@Dx_Nombre_Banco",instance.Dx_Nombre_Banco),
					new SqlParameter("@Dx_Cuenta_Banco",instance.Dx_Cuenta_Banco),
					new SqlParameter("@Binary_Acta_Constitutiva",instance.Binary_Acta_Constitutiva),
					new SqlParameter("@Binary_Poder_Notarial",instance.Binary_Poder_Notarial),
					new SqlParameter("@Pct_Tasa_IVA",instance.Pct_Tasa_IVA),
					new SqlParameter("@Dt_Fecha_Branch",instance.Dt_Fecha_Branch),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona),
                    new SqlParameter("@Id_Branch",instance.Id_Branch)
				};

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update CAT_PROVEEDOR failed", ex, true);
            }
        }
        /// <summary>
        /// Update Supplier Branch status
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int UpdateProviderBranchStatus(string BranchID, int Status)
        {
            int Result = 0;
            try
            {
                string Sql = "Update CAT_PROVEEDORBRANCH set Cve_Estatus_Proveedor=@Cve_Estatus_Proveedor where Id_Branch in(" + BranchID + ");";

                if (Status == (int)ProviderStatus.INACTIVO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'I' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + BranchID + ") AND u.Tipo_Usuario = 'S_B'";
                }
                else if (Status == (int)ProviderStatus.CANCELADO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'C' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + BranchID + ") AND u.Tipo_Usuario = 'S_B'";
                }
                else if (Status == (int)ProviderStatus.ACTIVO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'A' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + BranchID + ") AND u.Tipo_Usuario = 'S_B'";
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Estatus_Proveedor", Status)
                  };
                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Change Branch Status failed:Execute method  UpdateProviderBranchStatus in SupplierBrancheDal.", ex, true);
            }
            return Result;
        }     

        /// <summary>
        /// Select Supplier Branch status
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int Select_CAT_PROVEEDOR(SupplierBranchModel instance)
        {
            int count = 0;

            try
            {
                string executesqlstr = "SELECT Count(*) FROM CAT_PROVEEDORBRANCH Where Cve_Region = @Cve_Region AND Dx_Razon_Social = @Dx_Razon_Social AND Cve_Zona = @Cve_Zona";

                SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@Cve_Region",instance.Cve_Region),
					new SqlParameter("@Dx_Razon_Social",instance.Dx_Razon_Social),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona),
				};

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
                if (o != null)
                    count = Convert.ToInt32(o);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update CAT_PROVEEDOR failed", ex, true);
            }

            return count;
        }     
    }
}
