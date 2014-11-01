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
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PAEEEM.Entities;
using PAEEEM.Helpers;
namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Role DAL
    /// </summary>
    public class US_ROLDal
    {
        private static readonly US_ROLDal _classinstance = new US_ROLDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static US_ROLDal ClassInstance {get{return _classinstance; }}
        /// <summary>
        /// Add new role to Role table
        /// </summary>
        /// <param name="instance">Role Model</param>
        /// <returns></returns>
        public int Insert_US_ROL(US_ROLModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO US_ROL (Nombre_Rol, Relacion_Rol ) VALUES (@Nombre_Rol, @Relacion_Rol)";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Nombre_Rol",instance.Nombre_Rol),
                    new SqlParameter("@Relacion_Rol",instance.Relacion_Rol)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add role failure: Execute method Insert_US_ROL in US_ROLDal.", ex, true);
            }
        }
        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_US_ROL(US_ROLModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE US_ROL SET Nombre_Rol = @Nombre_Rol, Relacion_Rol = @Relacion_Rol WHERE Id_Rol = @Id_Rol";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Rol",instance.Id_Rol),
                    new SqlParameter("@Nombre_Rol",instance.Nombre_Rol),
                    new SqlParameter("@Relacion_Rol",instance.Relacion_Rol)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update role failure: Execute method Update_US_ROL in US_ROLDal.", ex, true);
            }
        }
        /// <summary>
        /// Get role by id
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public US_ROLModel Get_US_ROLByPKID(String pkid)
        {
            try
            {
                string executesqlstr = "SELECT Id_Rol, Nombre_Rol, Relacion_Rol FROM US_ROL WHERE Id_Rol = @Id_Rol";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Rol",pkid)
                };
                US_ROLModel modelinstance = new US_ROLModel();
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
                throw new LsDAException(this, "Get role failure: Execute method Get_US_ROLByPKID in US_ROLDal.", ex, true);
            }
        }
        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
		public List<US_ROLModel> Get_AllUS_ROL()
        {
            List<US_ROLModel> l_instance = new List<US_ROLModel>();
            string strSQL = "SELECT Id_Rol, Nombre_Rol, Relacion_Rol FROM US_ROL ORDER BY 2";
            try
            {
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL))
                {
                    while (sdr.Read())
                    {
                        US_ROLModel instance = EvaluateModel(sdr);
                        l_instance.Add(instance);
                    }
                }

                return l_instance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get role failure: Execute method Get_AllUS_ROL in US_ROLDal.", ex, true);
            }
        }
        /// <summary>
        /// Get roles by pager
        /// </summary>
        /// <param name="sorder"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public DataTable Get_US_ROLListByPager( String sorder,  Int32 currentPageIndex,  Int32 pageSize,  out Int32 pagecount )
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
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "dbo.UP_Pager_GetRoleList", para);
                int.TryParse(para[0].Value.ToString(), out pagecount);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get role failure: Execute method Get_US_ROLListByPager in US_ROLDal.", ex, true);
            }
        }
        /// <summary>
        /// Build role entity
        /// </summary>
        /// <param name="sdr"></param>
        /// <returns></returns>
        private US_ROLModel EvaluateModel(SqlDataReader sdr)
        {
            US_ROLModel modelinstance = new US_ROLModel();
            modelinstance.Id_Rol = sdr.IsDBNull(sdr.GetOrdinal("Id_Rol"))?0: sdr.GetInt32(sdr.GetOrdinal("Id_Rol"));
            modelinstance.Nombre_Rol = sdr.IsDBNull(sdr.GetOrdinal("Nombre_Rol"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Nombre_Rol"));
            modelinstance.Relacion_Rol = sdr.IsDBNull(sdr.GetOrdinal("Relacion_Rol"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Relacion_Rol"));
            return modelinstance;
        }
        /// <summary>
        /// Update role relation
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_RoleRelation(US_ROLModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE US_ROL SET  Relacion_Rol = @Relacion_Rol WHERE Id_Rol = @Id_Rol";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Rol",instance.Id_Rol), 
                    new SqlParameter("@Relacion_Rol",instance.Relacion_Rol)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update role failure: Execute method Update_RoleRelation in US_ROLDal.", ex, true);
            }
        }
    }
}



 
 
