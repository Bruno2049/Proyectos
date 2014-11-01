/* ----------------------------------------------------------------------
 * File Name: CAT_ESTATUS_CREDITODal.cs
 * 
 * Create Author: Eric
 * 
 * Create DateTime: 2011/7/5
 *
 * Description:   CAT_ESTATUS_CREDITO data access lay
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
    /// Credit Status
    /// </summary>
    public class CAT_ESTATUS_CREDITODal
    {
        private static readonly CAT_ESTATUS_CREDITODal _classinstance = new CAT_ESTATUS_CREDITODal();
        /// <summary>
        /// Property
        /// </summary>
        public static CAT_ESTATUS_CREDITODal ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Add Record
        /// </summary>
        public int Insert_CAT_ESTATUS_CREDITO(CAT_ESTATUS_CREDITOModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO CAT_ESTATUS_CREDITO (Dx_Estatus_Credito, Dt_Fecha_Estatus_Credito ) VALUES (@Dx_Estatus_Credito, @Dt_Fecha_Estatus_Credito)";

                SqlParameter[] para = new SqlParameter[] { 
                	new SqlParameter("@Dx_Estatus_Credito",instance.Dx_Estatus_Credito),
                	new SqlParameter("@Dt_Fecha_Estatus_Credito",instance.Dt_Fecha_Estatus_Credito)
            	};

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add new CAT_ESTATUS_CREDITO failed", ex, true);
            }

        }

        /// <summary>
        /// Delete Record
        /// </summary>
        public int Delete_CAT_ESTATUS_CREDITO(String pkid)
        {
            try
            {
                string executesqlstr = "DELETE CAT_ESTATUS_CREDITO WHERE Cve_Estatus_Credito = @Cve_Estatus_Credito";

                SqlParameter[] para = new SqlParameter[] { 
              	  new SqlParameter("@Cve_Estatus_Credito",pkid)
            	};

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete CAT_ESTATUS_CREDITO failed", ex, true);
            }
        }

        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_CAT_ESTATUS_CREDITO(CAT_ESTATUS_CREDITOModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE CAT_ESTATUS_CREDITO SET Dx_Estatus_Credito = @Dx_Estatus_Credito, Dt_Fecha_Estatus_Credito = @Dt_Fecha_Estatus_Credito WHERE Cve_Estatus_Credito = @Cve_Estatus_Credito";

                SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@Cve_Estatus_Credito",instance.Cve_Estatus_Credito),
					new SqlParameter("@Dx_Estatus_Credito",instance.Dx_Estatus_Credito),
					new SqlParameter("@Dt_Fecha_Estatus_Credito",instance.Dt_Fecha_Estatus_Credito)
				};

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update CAT_ESTATUS_CREDITO failed", ex, true);
            }
        }

        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public CAT_ESTATUS_CREDITOModel Get_CAT_ESTATUS_CREDITOByPKID(String pkid)
        {
            try
            {
                string executesqlstr = "SELECT Cve_Estatus_Credito, Dx_Estatus_Credito, Dt_Fecha_Estatus_Credito FROM CAT_ESTATUS_CREDITO WHERE Cve_Estatus_Credito = @Cve_Estatus_Credito";
                SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@Cve_Estatus_Credito",pkid)
				};
                CAT_ESTATUS_CREDITOModel modelinstance = new CAT_ESTATUS_CREDITOModel();
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
                throw new LsDAException(this, "Get a CAT_ESTATUS_CREDITO failed", ex, true);
            }
        }

        /// <summary>
        /// Get Total Record Number
        /// </summary>
        public Int32 Get_CAT_ESTATUS_CREDITOCount(String tablename, String swhere)
        {
            try
            {
                string sqlstr = "SELECT count(1) AS totalNum FROM " + tablename + " WHERE 1 = 1 " + swhere + " ";
                return Convert.ToInt32(SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sqlstr));
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CAT_ESTATUS_CREDITO count failed", ex, true);
            }
        }

        /// <summary>
        /// Get all Record
        /// </summary>
        public List<CAT_ESTATUS_CREDITOModel> Get_AllCAT_ESTATUS_CREDITO()
        {
            try
            {
                List<CAT_ESTATUS_CREDITOModel> l_instance = new List<CAT_ESTATUS_CREDITOModel>();
                string strSQL = "SELECT Cve_Estatus_Credito, Dx_Estatus_Credito, Dt_Fecha_Estatus_Credito FROM CAT_ESTATUS_CREDITO";
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL))
                {
                    while (sdr.Read())
                    {
                        CAT_ESTATUS_CREDITOModel instance = EvaluateModel(sdr);
                        l_instance.Add(instance);
                    }
                }

                return l_instance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all CAT_ESTATUS_CREDITO list failed", ex, true);
            }
        }

        /// <summary>
        /// evaluate model
        /// </summary>
        private CAT_ESTATUS_CREDITOModel EvaluateModel(SqlDataReader sdr)
        {
            try
            {
                CAT_ESTATUS_CREDITOModel modelinstance = new CAT_ESTATUS_CREDITOModel();
                modelinstance.Cve_Estatus_Credito = sdr.IsDBNull(sdr.GetOrdinal("Cve_Estatus_Credito")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Cve_Estatus_Credito"));
                modelinstance.Dx_Estatus_Credito = sdr.IsDBNull(sdr.GetOrdinal("Dx_Estatus_Credito")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Dx_Estatus_Credito"));
                modelinstance.Dt_Fecha_Estatus_Credito = sdr.IsDBNull(sdr.GetOrdinal("Dt_Fecha_Estatus_Credito")) ? DateTime.Parse("2011-5-1") : sdr.GetDateTime(sdr.GetOrdinal("Dt_Fecha_Estatus_Credito"));
                return modelinstance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Evaluate Model:CAT_ESTATUS_CREDITO failed", ex, true);
            }
        }

        /// <summary>
        /// Get status with status name
        /// </summary>
        /// <param name="statusName"></param>
        /// <returns></returns>
        public int Get_CAT_ESTATUS_CREDITOByEstatusName(string statusName)
        {
            try
            {
                int statusID = 0;
                string strSQL = "SELECT TOP 1 [Cve_Estatus_Credito] FROM  [dbo].[CAT_ESTATUS_CREDITO] where [Dx_Estatus_Credito]= @statusName";
                SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@statusName",statusName)
				};
                object obj = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL, para);
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out statusID);
                }
                return obj == null ? 0 : statusID;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get_CAT_ESTATUS_CREDITOByEstatusName method failed", ex, true);
            }
        }
    }
}





