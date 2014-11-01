/* ----------------------------------------------------------------------
 * File Name: CAT_PROVEEDORDal.cs
 * 
 * Create Author: Eric
 * 
 * Create DateTime: 2011/7/5
 *
 * Description:   CAT_PROVEEDOR data access lay
 *----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Provider
    /// </summary>
    public class CAT_PROVEEDORDal
    {
        private static readonly CAT_PROVEEDORDal _classinstance = new CAT_PROVEEDORDal();
        /// <summary>
        /// Property
        /// </summary>
        public static CAT_PROVEEDORDal ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Add Record
        /// </summary>
        public int Insert_CAT_PROVEEDOR(CAT_PROVEEDORModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO CAT_PROVEEDOR (Cve_Estatus_Proveedor, Cve_Region, Dx_Razon_Social, Dx_Nombre_Comercial, Dx_RFC, Dx_Domicilio_Part_Calle, Dx_Domicilio_Part_Num, Dx_Domicilio_Part_CP, Cve_Deleg_Municipio_Part, Cve_Estado_Part, Fg_Mismo_Domicilio, Dx_Domicilio_Fiscal_Calle, Dx_Domicilio_Fiscal_Num, Dx_Domicilio_Fiscal_CP, Cve_Deleg_Municipio_Fisc, Cve_Estado_Fisc, Dx_Nombre_Repre, Dx_Email_Repre, Dx_Telefono_Repre, Dx_Nombre_Repre_Legal, Dx_Nombre_Banco, Dx_Cuenta_Banco, Binary_Acta_Constitutiva, Binary_Poder_Notarial, Pct_Tasa_IVA, Dt_Fecha_Proveedor,Cve_Zona,Codigo_Proveedor ) VALUES (@Cve_Estatus_Proveedor, @Cve_Region, @Dx_Razon_Social, @Dx_Nombre_Comercial, @Dx_RFC, @Dx_Domicilio_Part_Calle, @Dx_Domicilio_Part_Num, @Dx_Domicilio_Part_CP, @Cve_Deleg_Municipio_Part, @Cve_Estado_Part, @Fg_Mismo_Domicilio, @Dx_Domicilio_Fiscal_Calle, @Dx_Domicilio_Fiscal_Num, @Dx_Domicilio_Fiscal_CP, @Cve_Deleg_Municipio_Fisc, @Cve_Estado_Fisc, @Dx_Nombre_Repre, @Dx_Email_Repre, @Dx_Telefono_Repre, @Dx_Nombre_Repre_Legal, @Dx_Nombre_Banco, @Dx_Cuenta_Banco, @Binary_Acta_Constitutiva, @Binary_Poder_Notarial, @Pct_Tasa_IVA, @Dt_Fecha_Proveedor,@Cve_Zona,@Codigo_Proveedor)";

                SqlParameter[] para = new SqlParameter[] { 
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
                	new SqlParameter("@Dt_Fecha_Proveedor",instance.Dt_Fecha_Proveedor),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona),
                    new SqlParameter("@Codigo_Proveedor",instance.Codigo_Proveedor)
            	};

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add new CAT_PROVEEDOR failed", ex, true);
            }

        }

        /// <summary>
        /// Delete Record
        /// </summary>
        public int Delete_CAT_PROVEEDOR(String pkid)
        {
            try
            {
                string executesqlstr = "DELETE CAT_PROVEEDOR WHERE Id_Proveedor = @Id_Proveedor";

                SqlParameter[] para = new SqlParameter[] { 
              	  new SqlParameter("@Id_Proveedor",pkid)
            	};

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete CAT_PROVEEDOR failed", ex, true);
            }
        }

        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_CAT_PROVEEDOR(CAT_PROVEEDORModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE CAT_PROVEEDOR SET Cve_Region = @Cve_Region, Dx_Razon_Social = @Dx_Razon_Social, Dx_Nombre_Comercial = @Dx_Nombre_Comercial, Dx_RFC = @Dx_RFC, Dx_Domicilio_Part_Calle = @Dx_Domicilio_Part_Calle, Dx_Domicilio_Part_Num = @Dx_Domicilio_Part_Num, Dx_Domicilio_Part_CP = @Dx_Domicilio_Part_CP, Cve_Deleg_Municipio_Part = @Cve_Deleg_Municipio_Part, Cve_Estado_Part = @Cve_Estado_Part, Fg_Mismo_Domicilio = @Fg_Mismo_Domicilio, Dx_Domicilio_Fiscal_Calle = @Dx_Domicilio_Fiscal_Calle, Dx_Domicilio_Fiscal_Num = @Dx_Domicilio_Fiscal_Num, Dx_Domicilio_Fiscal_CP = @Dx_Domicilio_Fiscal_CP, Cve_Deleg_Municipio_Fisc = @Cve_Deleg_Municipio_Fisc, Cve_Estado_Fisc = @Cve_Estado_Fisc, Dx_Nombre_Repre = @Dx_Nombre_Repre, Dx_Email_Repre = @Dx_Email_Repre, Dx_Telefono_Repre = @Dx_Telefono_Repre, Dx_Nombre_Repre_Legal = @Dx_Nombre_Repre_Legal, Dx_Nombre_Banco = @Dx_Nombre_Banco, Dx_Cuenta_Banco = @Dx_Cuenta_Banco, Binary_Acta_Constitutiva = @Binary_Acta_Constitutiva, Binary_Poder_Notarial = @Binary_Poder_Notarial, Pct_Tasa_IVA = @Pct_Tasa_IVA, Dt_Fecha_Proveedor = @Dt_Fecha_Proveedor,Cve_Zona = @Cve_Zona WHERE Id_Proveedor = @Id_Proveedor";

                SqlParameter[] para = new SqlParameter[] { 
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
					new SqlParameter("@Dt_Fecha_Proveedor",instance.Dt_Fecha_Proveedor),
                    new SqlParameter("@Cve_Zona",instance.Cve_Zona),
                    new SqlParameter("@Id_Proveedor",instance.Id_Proveedor)
				};

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update CAT_PROVEEDOR failed", ex, true);
            }
        }

        /// <summary>
        /// Update Record
        /// </summary>
        public int Select_CAT_PROVEEDOR(CAT_PROVEEDORModel instance)
        {
            int count = 0;
            try
            {
                string executesqlstr = "SELECT Count(*) FROM CAT_PROVEEDOR Where Cve_Region = @Cve_Region AND Dx_Razon_Social = @Dx_Razon_Social AND Cve_Zona = @Cve_Zona";

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
                throw new LsDAException(this, "Select CAT_PROVEEDOR failed", ex, true);
            }

            return count;
        }

        /// <summary>
        /// Update Record
        /// </summary>
        public int Select_CAT_PROVEEDOR_RFC(CAT_PROVEEDORModel instance)
        {
            int count = 0;

            try
            {
                string executesqlstr = "SELECT Count(*) FROM CAT_PROVEEDOR Where Dx_RFC = @RFC ";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@RFC",instance.Dx_RFC)
				};

                object o = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
                if (o != null)
                    count = Convert.ToInt32(o);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Select CAT_PROVEEDOR_RFC failed", ex, true);
            }

            return count;
        }

        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public CAT_PROVEEDORModel Get_CAT_PROVEEDORByPKID(String pkid)
        {
            try
            {
                string executesqlstr = "SELECT Id_Proveedor, Cve_Estatus_Proveedor, Cve_Region, Dx_Razon_Social, Dx_Nombre_Comercial, Dx_RFC, Dx_Domicilio_Part_Calle, Dx_Domicilio_Part_Num, Dx_Domicilio_Part_CP, Cve_Deleg_Municipio_Part, Cve_Estado_Part, Fg_Mismo_Domicilio, Dx_Domicilio_Fiscal_Calle, Dx_Domicilio_Fiscal_Num, Dx_Domicilio_Fiscal_CP, Cve_Deleg_Municipio_Fisc, Cve_Estado_Fisc, Dx_Nombre_Repre, Dx_Email_Repre, Dx_Telefono_Repre, Dx_Nombre_Repre_Legal, Dx_Nombre_Banco, Dx_Cuenta_Banco, Binary_Acta_Constitutiva, Binary_Poder_Notarial, Pct_Tasa_IVA, Dt_Fecha_Proveedor FROM CAT_PROVEEDOR WHERE Id_Proveedor = @Id_Proveedor";
                SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@Id_Proveedor",pkid)
				};
                CAT_PROVEEDORModel modelinstance = new CAT_PROVEEDORModel();
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para))
                {
                    if (sdr.Read())
                    {
                        modelinstance = EvaluateModel(sdr);
                    }
                }
                return modelinstance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get a CAT_PROVEEDOR failed", ex, true);
            }
        }
        /// <summary>
        /// Get proveedor with proveedor branch
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public CAT_PROVEEDORModel Get_CAT_PROVEEDORByBranchID(String BranchID)
        {
            try
            {
                string executesqlstr = "select A.* from CAT_PROVEEDOR A inner join CAT_PROVEEDORBRANCH B on A.Id_Proveedor =B.Id_Proveedor where B.Id_Branch=@Id_Branch";
                SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@Id_Branch",BranchID)
				};
                CAT_PROVEEDORModel modelinstance = new CAT_PROVEEDORModel();
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para))
                {
                    if (sdr.Read())
                    {
                        modelinstance = EvaluateModel(sdr);
                    }
                }
                return modelinstance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get a CAT_PROVEEDOR failed", ex, true);
            }
        }
        /// <summary>
        /// Get Total Record Number
        /// </summary>
        public Int32 Get_CAT_PROVEEDORCount(String tablename, String swhere)
        {
            try
            {
                string sqlstr = "SELECT count(1) AS totalNum FROM " + tablename + " WHERE 1 = 1 " + swhere + " ";
                return Convert.ToInt32(SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sqlstr));
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CAT_PROVEEDOR count failed", ex, true);
            }
        }

        /// <summary>
        /// Get all Record
        /// </summary>
        public List<CAT_PROVEEDORModel> Get_AllCAT_PROVEEDOR()
        {
            try
            {
                List<CAT_PROVEEDORModel> l_instance = new List<CAT_PROVEEDORModel>();
                string strSQL = "SELECT Id_Proveedor, Cve_Estatus_Proveedor, Cve_Region, Dx_Razon_Social, Dx_Nombre_Comercial, Dx_RFC, Dx_Domicilio_Part_Calle, Dx_Domicilio_Part_Num, Dx_Domicilio_Part_CP, Cve_Deleg_Municipio_Part, Cve_Estado_Part, Fg_Mismo_Domicilio, Dx_Domicilio_Fiscal_Calle, Dx_Domicilio_Fiscal_Num, Dx_Domicilio_Fiscal_CP, Cve_Deleg_Municipio_Fisc, Cve_Estado_Fisc, Dx_Nombre_Repre, Dx_Email_Repre, Dx_Telefono_Repre, Dx_Nombre_Repre_Legal, Dx_Nombre_Banco, Dx_Cuenta_Banco, Binary_Acta_Constitutiva, Binary_Poder_Notarial, Pct_Tasa_IVA, Dt_Fecha_Proveedor FROM CAT_PROVEEDOR";
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL))
                {
                    while (sdr.Read())
                    {
                        CAT_PROVEEDORModel instance = EvaluateModel(sdr);
                        l_instance.Add(instance);
                    }
                }

                return l_instance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all CAT_PROVEEDOR list failed", ex, true);
            }
        }
        /// <summary>
        /// Get proveedores with zone
        /// </summary>
        /// <param name="zone"></param>
        /// <returns></returns>
        public DataTable GetProveedorWithZone(int zone)
        {
            DataTable dtProveedors = null;
            try
            {
                string strSQL = "SELECT Id_Proveedor, Cve_Estatus_Proveedor, Cve_Region, Dx_Razon_Social, Dx_Nombre_Comercial, Dx_RFC, Dx_Domicilio_Part_Calle, Dx_Domicilio_Part_Num, Dx_Domicilio_Part_CP, Cve_Deleg_Municipio_Part, Cve_Estado_Part, Fg_Mismo_Domicilio, Dx_Domicilio_Fiscal_Calle, Dx_Domicilio_Fiscal_Num, Dx_Domicilio_Fiscal_CP, Cve_Deleg_Municipio_Fisc, Cve_Estado_Fisc, Dx_Nombre_Repre, Dx_Email_Repre, Dx_Telefono_Repre, Dx_Nombre_Repre_Legal, Dx_Nombre_Banco, Dx_Cuenta_Banco, Binary_Acta_Constitutiva, Binary_Poder_Notarial, Pct_Tasa_IVA, Dt_Fecha_Proveedor, Cve_Zona FROM CAT_PROVEEDOR WHERE [Cve_Zona] = @Cve_Zona";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Zona", zone)
                };
                dtProveedors = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all GetProveedorWithZone list failed", ex, true);
            }

            return dtProveedors;
        }
        /// <summary>
        /// Get proveedores with region
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public DataTable GetProveedorWithRegion(int region)
        {
            DataTable dtProveedors = null;
            try
            {
                string strSQL = "SELECT Id_Proveedor, Cve_Estatus_Proveedor, P.Cve_Region, Dx_Razon_Social, Dx_Nombre_Comercial, Dx_RFC, Dx_Domicilio_Part_Calle, Dx_Domicilio_Part_Num, Dx_Domicilio_Part_CP, Cve_Deleg_Municipio_Part, Cve_Estado_Part, Fg_Mismo_Domicilio, Dx_Domicilio_Fiscal_Calle, Dx_Domicilio_Fiscal_Num, Dx_Domicilio_Fiscal_CP, Cve_Deleg_Municipio_Fisc, Cve_Estado_Fisc, Dx_Nombre_Repre, Dx_Email_Repre, Dx_Telefono_Repre, Dx_Nombre_Repre_Legal, Dx_Nombre_Banco, Dx_Cuenta_Banco, Binary_Acta_Constitutiva, Binary_Poder_Notarial, Pct_Tasa_IVA, Dt_Fecha_Proveedor, P.Cve_Zona FROM CAT_PROVEEDOR AS P " +
                                        "INNER JOIN CAT_ZONA  AS Z ON P.Cve_Zona = Z.Cve_Zona WHERE Z.Cve_Region = @Cve_Region AND Cve_Estatus_Proveedor = 2 ORDER BY Dx_Razon_Social";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Region", region)
                };
                dtProveedors = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all GetProveedorWithRegion list failed", ex, true);
            }

            return dtProveedors;
        }
        /// <summary>
        /// Get proveedores
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public DataTable GetProveedor()
        {
            DataTable dtProveedors = null;
            try
            {
                string strSQL = "SELECT Id_Proveedor, Dx_Razon_Social FROM CAT_PROVEEDOR AS P (nolock) WHERE Cve_Estatus_Proveedor = 2 ORDER BY Dx_Razon_Social";
                dtProveedors = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all GetProveedorWithRegion list failed", ex, true);
            }

            return dtProveedors;
        }
        
        /// <summary>
        /// evaluate model
        /// </summary>
        private CAT_PROVEEDORModel EvaluateModel(SqlDataReader sdr)
        {
            try
            {
                CAT_PROVEEDORModel modelinstance = new CAT_PROVEEDORModel();
                modelinstance.Id_Proveedor = sdr.IsDBNull(sdr.GetOrdinal("Id_Proveedor")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Id_Proveedor"));
                modelinstance.Cve_Estatus_Proveedor = sdr.IsDBNull(sdr.GetOrdinal("Cve_Estatus_Proveedor")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Cve_Estatus_Proveedor"));
                modelinstance.Cve_Region = sdr.IsDBNull(sdr.GetOrdinal("Cve_Region")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Cve_Region"));
                modelinstance.Dx_Razon_Social = sdr.IsDBNull(sdr.GetOrdinal("Dx_Razon_Social")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Razon_Social"));
                modelinstance.Dx_Nombre_Comercial = sdr.IsDBNull(sdr.GetOrdinal("Dx_Nombre_Comercial")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Nombre_Comercial"));
                modelinstance.Dx_RFC = sdr.IsDBNull(sdr.GetOrdinal("Dx_RFC")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_RFC"));
                modelinstance.Dx_Domicilio_Part_Calle = sdr.IsDBNull(sdr.GetOrdinal("Dx_Domicilio_Part_Calle")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Domicilio_Part_Calle"));
                modelinstance.Dx_Domicilio_Part_Num = sdr.IsDBNull(sdr.GetOrdinal("Dx_Domicilio_Part_Num")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Domicilio_Part_Num"));
                modelinstance.Dx_Domicilio_Part_CP = sdr.IsDBNull(sdr.GetOrdinal("Dx_Domicilio_Part_CP")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Domicilio_Part_CP"));
                modelinstance.Cve_Deleg_Municipio_Part = sdr.IsDBNull(sdr.GetOrdinal("Cve_Deleg_Municipio_Part")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Cve_Deleg_Municipio_Part"));
                modelinstance.Cve_Estado_Part = sdr.IsDBNull(sdr.GetOrdinal("Cve_Estado_Part")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Cve_Estado_Part"));
                modelinstance.Fg_Mismo_Domicilio = sdr.IsDBNull(sdr.GetOrdinal("Fg_Mismo_Domicilio")) ? false : sdr.GetBoolean(sdr.GetOrdinal("Fg_Mismo_Domicilio"));
                modelinstance.Dx_Domicilio_Fiscal_Calle = sdr.IsDBNull(sdr.GetOrdinal("Dx_Domicilio_Fiscal_Calle")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Domicilio_Fiscal_Calle"));
                modelinstance.Dx_Domicilio_Fiscal_Num = sdr.IsDBNull(sdr.GetOrdinal("Dx_Domicilio_Fiscal_Num")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Domicilio_Fiscal_Num"));
                modelinstance.Dx_Domicilio_Fiscal_CP = sdr.IsDBNull(sdr.GetOrdinal("Dx_Domicilio_Fiscal_CP")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Domicilio_Fiscal_CP"));
                modelinstance.Cve_Deleg_Municipio_Fisc = sdr.IsDBNull(sdr.GetOrdinal("Cve_Deleg_Municipio_Fisc")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Cve_Deleg_Municipio_Fisc"));
                modelinstance.Cve_Estado_Fisc = sdr.IsDBNull(sdr.GetOrdinal("Cve_Estado_Fisc")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Cve_Estado_Fisc"));
                modelinstance.Dx_Nombre_Repre = sdr.IsDBNull(sdr.GetOrdinal("Dx_Nombre_Repre")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Nombre_Repre"));
                modelinstance.Dx_Email_Repre = sdr.IsDBNull(sdr.GetOrdinal("Dx_Email_Repre")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Email_Repre"));
                modelinstance.Dx_Telefono_Repre = sdr.IsDBNull(sdr.GetOrdinal("Dx_Telefono_Repre")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Telefono_Repre"));
                modelinstance.Dx_Nombre_Repre_Legal = sdr.IsDBNull(sdr.GetOrdinal("Dx_Nombre_Repre_Legal")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Nombre_Repre_Legal"));
                modelinstance.Dx_Nombre_Banco = sdr.IsDBNull(sdr.GetOrdinal("Dx_Nombre_Banco")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Nombre_Banco"));
                modelinstance.Dx_Cuenta_Banco = sdr.IsDBNull(sdr.GetOrdinal("Dx_Cuenta_Banco")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Cuenta_Banco"));
                //modelinstance.Binary_Acta_Constitutiva = sdr.IsDBNull(sdr.GetOrdinal("Binary_Acta_Constitutiva"))?string.Empty: sdr.GetBytes(sdr.GetOrdinal("Binary_Acta_Constitutiva"),);
                //modelinstance.Binary_Poder_Notarial = sdr.IsDBNull(sdr.GetOrdinal("Binary_Poder_Notarial"))?string.Empty: sdr.GetBytes(sdr.GetOrdinal("Binary_Poder_Notarial"));
                modelinstance.Pct_Tasa_IVA = sdr.IsDBNull(sdr.GetOrdinal("Pct_Tasa_IVA")) ? default(float) : sdr.GetDouble(sdr.GetOrdinal("Pct_Tasa_IVA"));
                modelinstance.Dt_Fecha_Proveedor = sdr.IsDBNull(sdr.GetOrdinal("Dt_Fecha_Proveedor")) ? DateTime.Parse("2011-5-1") : sdr.GetDateTime(sdr.GetOrdinal("Dt_Fecha_Proveedor"));
                return modelinstance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Evaluate Model:CAT_PROVEEDOR failed", ex, true);
            }
        }
        //add by coco 2011-10-09
        /// <summary>
        /// get all the suppliers and supplier branches assigned to a specific disposal
        /// </summary>
        /// <param name="DisposaID">disposal center</param>
        /// <param name="UserType">disposal center type: main center or branch</param>
        /// <returns>supplier/branches datatable</returns>
        public DataTable GetDisposalCenterRelatedSuppliers(int DisposaID, string UserType)
        {
            DataTable TableResult = null;
            try
            {
                //string strSQL = "select A.* from CAT_PROVEEDOR A  inner join K_CAT_PROVEEDOR_CAT_CENTRO_DISP B on A.Id_Proveedor=B.Id_Proveedor and B.Id_Centro_Disp=@Id_Centro_Disp and B.Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";
                /*
                string strSQL = " select distinct (convert(varchar(10),A.Id_Proveedor) + '-'  + A.Dx_Razon_Social +'(SUPPLIER)')  as Id_Proveedor ,A.Dx_Razon_Social +'(SUPPLIER)'  as Dx_Razon_Social,A.Dx_Nombre_Comercial +'(SUPPLIER)'  as Dx_Nombre_Comercial from CAT_PROVEEDOR A  inner join K_CAT_PROVEEDOR_CAT_CENTRO_DISP B ";
                strSQL = strSQL + " on A.Id_Proveedor=B.Id_Proveedor and B.Id_Centro_Disp=@Id_Centro_Disp and B.Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";
                strSQL = strSQL + " union all ";
                strSQL = strSQL + " select distinct (Convert(varchar(10),A.Id_Branch) + '-' +A.Dx_Razon_Social +'(BRANCH)' ) AS Id_Proveedor,A.Dx_Razon_Social +'(BRANCH)'  as Dx_Razon_Social,A.Dx_Nombre_Comercial +'(BRANCH)'  as Dx_Nombre_Comercial from CAT_PROVEEDORBRANCH A  inner join K_CAT_PROVEEDOR_CAT_CENTRO_DISP B ";
                strSQL = strSQL + " on A.Id_Branch=B.Id_Proveedor and B.Id_Centro_Disp=@Id_Centro_Disp and B.Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp";
                */

                string strSQL = " select distinct (convert(varchar(10),A.Id_Proveedor) + '-'  + A.Dx_Razon_Social +' (MATRIZ)')  as Id_Proveedor ,A.Dx_Razon_Social +' (MATRIZ)'  as Dx_Razon_Social,A.Dx_Nombre_Comercial +' (MATRIZ)'  as Dx_Nombre_Comercial ";
                strSQL = strSQL + "  from CAT_PROVEEDOR A ";
                strSQL = strSQL + "  inner join K_CAT_PROVEEDOR_CAT_CENTRO_DISP B ";
                strSQL = strSQL + "   on A.Id_Proveedor=B.Id_Proveedor and B.Fg_Tipo_Proveedor = 'M' and B.Id_Centro_Disp=@Id_Centro_Disp and B.Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp ";
                strSQL = strSQL + " union all ";
                strSQL = strSQL + " select distinct (Convert(varchar(10),A.Id_Branch) + '-' +A.Dx_Razon_Social +' (SUCURSAL)' ) AS Id_Proveedor,A.Dx_Razon_Social +' (SUCURSAL)'  as Dx_Razon_Social,A.Dx_Nombre_Comercial +' (SUCURSAL)'  as Dx_Nombre_Comercial ";
                strSQL = strSQL + "  from CAT_PROVEEDORBRANCH A ";
                strSQL = strSQL + "  inner join K_CAT_PROVEEDOR_CAT_CENTRO_DISP B ";
                strSQL = strSQL + "   on A.Id_Branch=B.Id_Proveedor and B.Fg_Tipo_Proveedor = 'B' and B.Id_Centro_Disp=@Id_Centro_Disp and B.Fg_Tipo_Centro_Disp=@Fg_Tipo_Centro_Disp ";

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Id_Centro_Disp", DisposaID),
                    new SqlParameter("@Fg_Tipo_Centro_Disp",UserType)
                };

                TableResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all the suppliers and supplier branches assgined to a specific disposal failed", ex, true);
            }

            return TableResult;
        }
        //end add
        //add by coco 2011-10-25
        /// <summary>
        /// Get providers
        /// </summary>
        /// <param name="Zone"></param>
        /// <param name="Tipo"></param>
        /// <param name="Estatus"></param>
        /// <param name="Regional"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable Get_Provider(string Zone, string Tipo, string Estatus,int Regional, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            try
            {                
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@Zone", Zone),
                    new SqlParameter("@Tipo",Tipo),
                    new SqlParameter("@Estatus", Estatus),
                    new SqlParameter("@Regional",Regional),                  
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_SupplierMonitor", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credits failed: Execute method Get_Provider in CAT_PROVEEDORDal.", ex, true);
            }

            return dtResult;
        }
        //end add
        /// <summary>
        /// Get supplier
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public DataTable GetSupplierByPk(int supplierID)
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT [Id_Proveedor],[Cve_Estatus_Proveedor],[Cve_Region],[Dx_Razon_Social],[Dx_Nombre_Comercial] " +
                            " ,[Dx_RFC],[Dx_Domicilio_Part_Calle],[Dx_Domicilio_Part_Num],[Dx_Domicilio_Part_CP],[Cve_Deleg_Municipio_Part],[Cve_Estado_Part] " +
                            ",[Fg_Mismo_Domicilio],[Dx_Domicilio_Fiscal_Calle],[Dx_Domicilio_Fiscal_Num],[Dx_Domicilio_Fiscal_CP],[Cve_Deleg_Municipio_Fisc] " +
                            ",[Cve_Estado_Fisc],[Dx_Nombre_Repre],[Dx_Email_Repre],[Dx_Telefono_Repre],[Dx_Nombre_Repre_Legal],[Dx_Nombre_Banco] " +
                            " ,[Dx_Cuenta_Banco],[Binary_Acta_Constitutiva],[Binary_Poder_Notarial],[Pct_Tasa_IVA],[Dt_Fecha_Proveedor],[Cve_Zona] " +
                            " FROM [dbo].[CAT_PROVEEDOR] WHERE [Id_Proveedor] = @SupplierID";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@SupplierID", supplierID)
                };
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get branch with supplier id failed: Execute method GetSupplierByPk in CAT_PROVEEDORDal.", ex, true);
            }
            return dtResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetSupplierAndBranch()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT ID,Dx_Nombre_Comercial+'(Supplier)' as Dx_Nombre_Comercial,'Supplier' as Type from dbo.View_Supplier" +
                         " UNION " +
                         " SELECT ID,Dx_Nombre_Comercial+'(Branch)' as Dx_Nombre_Comercial,'Branch' as Type from dbo.View_SupplierBranch" +
                         " ORDER BY Type";
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get supplier branch failed: Execute method GetSupplierAndBranch in CAT_PROVEEDORDal.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Authorization supplier and branch
        /// </summary>
        /// <param name="Zone"></param>
        /// <param name="Tipo"></param>
        /// <param name="Estatus"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable Get_Provider_ForAuthorization(string Zone, string Tipo, string Estatus, string Regional, int pageIndex, int pageSize, out int pageCount)
        {
            DataTable dtResult = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@PageCount", SqlDbType.Int),
                    new SqlParameter("@Zone", Zone),
                    new SqlParameter("@Tipo",Tipo),
                    new SqlParameter("@Estatus", Estatus),                            
                    new SqlParameter("@Regional", Regional),
                    new SqlParameter("@PageIndex", pageIndex),
                    new SqlParameter("@PageSize", pageSize)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_SupplierAuthorization", paras);
                int.TryParse(paras[0].Value.ToString(), out pageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get credits failed: Execute method Get_Provider_ForAuthorization in CAT_PROVEEDORDal.", ex, true);
            }

            return dtResult;
        }
        /// <summary>
        /// Update Supplier Status
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int UpdateProviderStatus(string SupplierID, int Status)
        {
            int Result = 0;
            try
            {
                string Sql = "Update CAT_PROVEEDOR set Cve_Estatus_Proveedor=@Cve_Estatus_Proveedor where Id_Proveedor in(" + SupplierID + ");";

                if (Status == (int)ProviderStatus.INACTIVO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'I' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + SupplierID + ") AND u.Tipo_Usuario = 'S'";
                }
                else if (Status == (int)ProviderStatus.CANCELADO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'C' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + SupplierID + ") AND u.Tipo_Usuario = 'S'";
                }
                else if (Status == (int)ProviderStatus.ACTIVO)
                {
                    Sql += "UPDATE US_USUARIO SET Estatus = 'A' FROM US_USUARIO AS u WITH(NOLOCK) WHERE u.Id_Departamento IN (" + SupplierID + ") AND u.Tipo_Usuario = 'S'";
                }

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Cve_Estatus_Proveedor", Status)
                  };
                Result = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, Sql, paras);
            }
            catch (Exception ex)
            {
                throw new LsDAException(this, "Change Provider Status failed:Execute method  UpdateProviderStatus in CAT_PROVEEDORDal.", ex, true);
            }
            return Result;
        }
      
        //updated by tina 2012-07-18
        public DataTable GetSupplierAndBranchCompanyName()
        {
            DataTable dtResult = null;
            string SQL = "";
            try
            {
                SQL = "SELECT Id_Proveedor, Dx_Nombre_Comercial FROM (" +
                    "SELECT Convert(varchar(10),Id_Proveedor) + '-'  + ISNULL(Dx_Nombre_Comercial,'') +'(SUPPLIER)'  AS Id_Proveedor ,Dx_Nombre_Comercial +'(SUPPLIER)'  AS Dx_Nombre_Comercial FROM CAT_PROVEEDOR" +
                         " UNION " +
                         " SELECT Convert(varchar(10),Id_Branch) + '-' + ISNULL(Dx_Nombre_Comercial,'') +'(BRANCH)'  AS Id_Proveedor,Dx_Nombre_Comercial +'(BRANCH)'  AS Dx_Nombre_Comercial FROM CAT_PROVEEDORBRANCH" +
                         ") f ORDER BY 2";
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get supplier and branch company name failed: Execute method GetSupplierAndBranchCompanyName in CAT_PROVEEDORDal.", ex, true);
            }
            return dtResult;
        }
        //end
    }
}
