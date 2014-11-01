/* ----------------------------------------------------------------------
 * File Name: CAT_TECNOLOGIADal.cs
 * 
 * Create Author: Eric
 * 
 * Create DateTime: 2011/7/5
 *
 * Description:   CAT_TECNOLOGIA data access lay
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
    /// Technology
    /// </summary>
    public class CAT_TECNOLOGIADal
    {
        private static readonly CAT_TECNOLOGIADal _classinstance = new CAT_TECNOLOGIADal();
        /// <summary>
        /// Property
        /// </summary>
        public static CAT_TECNOLOGIADal ClassInstance {get{return _classinstance; }}
        
        /// <summary>
        /// Add Record
        /// </summary>
        public int Insert_CAT_TECNOLOGIA(CAT_TECNOLOGIAModel instance)
        {
			try
            {
            	string executesqlstr =  "INSERT INTO CAT_TECNOLOGIA (Dx_Nombre_General, Dx_Nombre_Particular, Dt_Fecha_Tecnologoia ) VALUES (@Dx_Nombre_General, @Dx_Nombre_Particular, @Dt_Fecha_Tecnologoia)";
            
            	SqlParameter[] para = new SqlParameter[] { 
                	new SqlParameter("@Dx_Nombre_General",instance.Dx_Nombre_General),
                	new SqlParameter("@Dx_Nombre_Particular",instance.Dx_Nombre_Particular),
                	new SqlParameter("@Dt_Fecha_Tecnologoia",instance.Dt_Fecha_Tecnologoia)
            	};

            	return SqlHelper.ExecuteNonQuery( ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
			}
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add new CAT_TECNOLOGIA failed", ex, true);
            }
            
        }
        
        /// <summary>
        /// Delete Record
        /// </summary>
        public int Delete_CAT_TECNOLOGIA(String pkid)
        {
			try
            {
            	string executesqlstr = "DELETE CAT_TECNOLOGIA WHERE Cve_Tecnologia = @Cve_Tecnologia";
            
            	SqlParameter[] para = new SqlParameter[] { 
              	  new SqlParameter("@Cve_Tecnologia",pkid)
            	};
            
            	return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
			}
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete CAT_TECNOLOGIA failed", ex, true);
            }
        }
        
        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_CAT_TECNOLOGIA(CAT_TECNOLOGIAModel instance)
		{
			try
            {
				string executesqlstr = "UPDATE CAT_TECNOLOGIA SET Dx_Nombre_General = @Dx_Nombre_General, Dx_Nombre_Particular = @Dx_Nombre_Particular, Dt_Fecha_Tecnologoia = @Dt_Fecha_Tecnologoia WHERE Cve_Tecnologia = @Cve_Tecnologia";
				
				SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@Cve_Tecnologia",instance.Cve_Tecnologia),
					new SqlParameter("@Dx_Nombre_General",instance.Dx_Nombre_General),
					new SqlParameter("@Dx_Nombre_Particular",instance.Dx_Nombre_Particular),
					new SqlParameter("@Dt_Fecha_Tecnologoia",instance.Dt_Fecha_Tecnologoia)
				};
				
				return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
			}
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update CAT_TECNOLOGIA failed", ex, true);
            }
        }
        
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public CAT_TECNOLOGIAModel Get_CAT_TECNOLOGIAByPKID(String pkid)
        {
			try
            {
                string executesqlstr = "SELECT Cve_Tecnologia, Dx_Nombre_General, Dx_Nombre_Particular, Dt_Fecha_Tecnologoia, Cve_Tipo_Tecnologia, Dx_Cve_CC FROM CAT_TECNOLOGIA WHERE Cve_Tecnologia = @Cve_Tecnologia";
				SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@Cve_Tecnologia",pkid)
				};
				CAT_TECNOLOGIAModel modelinstance = new CAT_TECNOLOGIAModel();
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
                throw new LsDAException(this, "Get a CAT_TECNOLOGIA failed", ex, true);
            }
        }
        
        /// <summary>
        /// Get Total Record Number
        /// </summary>
        public Int32 Get_CAT_TECNOLOGIACount(String tablename, String swhere)
        {
			try
            {
				string sqlstr = "SELECT count(1) AS totalNum FROM " + tablename + " WHERE 1 = 1 "+swhere+" ";
				return Convert.ToInt32(SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sqlstr));
			}
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CAT_TECNOLOGIA count failed", ex, true);
            }	
        }
        
        /// <summary>
        /// Get Record With Split Page
        /// </summary>
        public DataTable Get_CAT_TECNOLOGIAListByPager
        (
            String sorder,
            Int32 currentPageIndex,
			Int32 pageSize,
            out Int32 pagecount
        )
        {
			try
            {
				SqlParameter[] para = new SqlParameter[] { 
					new SqlParameter("@Count",SqlDbType.Int),			
					new SqlParameter("@OrderBy",sorder),
					new SqlParameter("@CurrentPageIndex",currentPageIndex),
					new SqlParameter("@PageSize",pageSize)
	
				};
				para[0].Direction = ParameterDirection.Output;
				
				DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "dbo.UP_Pager_GetCAT_TECNOLOGIAList", para);
	
	
				int.TryParse(para[0].Value.ToString(), out pagecount);
				return dt;
			}
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get CAT_TECNOLOGIA list for pager failed", ex, true);
            }	
        }
		
		/// <summary>
        /// Get all Record
        /// </summary>
		public List<CAT_TECNOLOGIAModel> Get_AllCAT_TECNOLOGIA()
        {
			try
            {
				List<CAT_TECNOLOGIAModel> l_instance = new List<CAT_TECNOLOGIAModel>();
				string strSQL = "SELECT Cve_Tecnologia, Dx_Nombre_General, Dx_Nombre_Particular, Dt_Fecha_Tecnologoia FROM CAT_TECNOLOGIA";
				using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text,strSQL))
				{
					while (sdr.Read())
					{
						CAT_TECNOLOGIAModel instance = EvaluateModel(sdr);
						l_instance.Add(instance);
					}
				}
	
				return l_instance;
			}
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all CAT_TECNOLOGIA list failed", ex, true);
            }	
        }
        
        /// <summary>
        /// evaluate model
        /// </summary>
        private CAT_TECNOLOGIAModel EvaluateModel(SqlDataReader sdr)
        {
			try
            {		
				CAT_TECNOLOGIAModel modelinstance = new CAT_TECNOLOGIAModel();
				modelinstance.Cve_Tecnologia = sdr.IsDBNull(sdr.GetOrdinal("Cve_Tecnologia"))?0: sdr.GetInt32(sdr.GetOrdinal("Cve_Tecnologia"));
				modelinstance.Dx_Nombre_General = sdr.IsDBNull(sdr.GetOrdinal("Dx_Nombre_General"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Dx_Nombre_General"));
				modelinstance.Dx_Nombre_Particular = sdr.IsDBNull(sdr.GetOrdinal("Dx_Nombre_Particular"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Dx_Nombre_Particular"));
				modelinstance.Dt_Fecha_Tecnologoia = sdr.IsDBNull(sdr.GetOrdinal("Dt_Fecha_Tecnologoia"))?DateTime.Parse("2011-5-1"): sdr.GetDateTime(sdr.GetOrdinal("Dt_Fecha_Tecnologoia"));
				return modelinstance;
			}
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Evaluate Model:CAT_TECNOLOGIA failed", ex, true);
            }	
        }
        
    }
}



 
 
