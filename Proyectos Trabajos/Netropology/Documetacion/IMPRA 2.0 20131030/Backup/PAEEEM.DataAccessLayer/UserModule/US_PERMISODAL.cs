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
    /// Permission DAL
    /// </summary>
    public class US_PERMISODal
    {
        private static readonly US_PERMISODal _classinstance = new US_PERMISODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static US_PERMISODal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Add permission
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_US_PERMISO(US_PERMISOModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO US_PERMISO (Estatus_Permiso, Valor_Permiso, Tipo_Permiso ) VALUES (@Estatus_Permiso, @Valor_Permiso, @Tipo_Permiso)";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Estatus_Permiso",instance.Estatus_Permiso),
                    new SqlParameter("@Valor_Permiso",instance.Valor_Permiso),
                    new SqlParameter("@Tipo_Permiso",instance.Tipo_Permiso)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add permission failed: Execute method Insert_US_PERMISO in US_PERMISODal.", ex, true);
            }
        }
        /// <summary>
        /// Delete permission
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public int Delete_US_PERMISO(String pkid)
        {
            try
            {
                string executesqlstr = "DELETE US_PERMISO WHERE Id_Permiso = @Id_Permiso";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Permiso",pkid)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete permission failed: Execute method Delete_US_PERMISO in US_PERMISODal.", ex, true);
            }
        }
        /// <summary>
        /// Update permission
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_US_PERMISO(US_PERMISOModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE US_PERMISO SET Estatus_Permiso = @Estatus_Permiso, Valor_Permiso = @Valor_Permiso, Tipo_Permiso = @Tipo_Permiso WHERE Id_Permiso = @Id_Permiso";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Permiso",instance.Id_Permiso),
                    new SqlParameter("@Estatus_Permiso",instance.Estatus_Permiso),
                    new SqlParameter("@Valor_Permiso",instance.Valor_Permiso),
                    new SqlParameter("@Tipo_Permiso",instance.Tipo_Permiso)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update permission failed: Execute method Update_US_PERMISO in US_PERMISODal.", ex, true);
            }
        }

        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public US_PERMISOModel Get_US_PERMISOByPKID(String pkid)
        {
            string executesqlstr = "SELECT Id_Permiso, Estatus_Permiso, Valor_Permiso, Tipo_Permiso FROM US_PERMISO WHERE Id_Permiso = @Id_Permiso";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Id_Permiso",pkid)
            };
            US_PERMISOModel modelinstance = new US_PERMISOModel();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para))
            {
                if (sdr.Read())
                {
                    modelinstance = EvaluateModel(sdr);
                }
            }
            return modelinstance;
        }
        /// <summary>
        /// Get Record With Split Page
        /// </summary>
        public DataTable Get_US_PERMISOListByPager
        (
            String sorder,
            Int32 currentPageIndex,
            Int32 pageSize,
            out Int32 pagecount
        )
        {
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Count",SqlDbType.Int),			
                new SqlParameter("@OrderBy",sorder),
                new SqlParameter("@CurrentPageIndex",currentPageIndex),
				new SqlParameter("@PageSize",pageSize)

            };
            para[0].Direction = ParameterDirection.Output;

            DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "dbo.UP_Pager_GetUS_PERMISOList", para);

            int.TryParse(para[0].Value.ToString(), out pagecount);
            return dt;
        }
        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <returns></returns>
        public List<US_PERMISOModel> Get_AllUS_PERMISO()
        {
            List<US_PERMISOModel> l_instance = new List<US_PERMISOModel>();
            string strSQL = "SELECT Id_Permiso, Estatus_Permiso, Valor_Permiso, Tipo_Permiso FROM US_PERMISO";
            try
            {
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL))
                {
                    while (sdr.Read())
                    {
                        US_PERMISOModel instance = EvaluateModel(sdr);
                        l_instance.Add(instance);
                    }
                }

                return l_instance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all permissions failed: Execute method Get_AllUS_PERMISO in US_PERMISODal.", ex, true);
            }
        }

        /// <summary>
        /// evaluate model
        /// </summary>
        private US_PERMISOModel EvaluateModel(SqlDataReader sdr)
        {
            US_PERMISOModel modelinstance = new US_PERMISOModel();
            modelinstance.Id_Permiso = sdr.IsDBNull(sdr.GetOrdinal("Id_Permiso")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Id_Permiso"));
            modelinstance.Estatus_Permiso = sdr.IsDBNull(sdr.GetOrdinal("Estatus_Permiso")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Estatus_Permiso"));
            modelinstance.Valor_Permiso = sdr.IsDBNull(sdr.GetOrdinal("Valor_Permiso")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Valor_Permiso"));
            modelinstance.Tipo_Permiso = sdr.IsDBNull(sdr.GetOrdinal("Tipo_Permiso")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Tipo_Permiso"));
            return modelinstance;
        }
        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <returns></returns>
        public DataTable Get_AllPagePermission()
        {
            string SQL = "SELECT [Id_Navegacion],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Estatus],[Nivel_Navegacion],[Secuencia],[Tipo_Permiso],[Id_Permiso] FROM [dbo].[View_Page_Permission] where Tipo_Permiso='P'";
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text,  SQL);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all permissions failed: Execute method Get_AllPagePermission in US_PERMISODal.", ex, true);
            }
        }
        /// <summary>
        /// Get root permission
        /// </summary>
        /// <returns></returns>
        public DataTable Get_RootPermission()
        {
            string SQL = "SELECT top 1  [Id_Navegacion],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Estatus],[Nivel_Navegacion],[Secuencia],[Tipo_Permiso],[Id_Permiso] FROM [dbo].[View_Page_Permission] where Nivel_Navegacion=0 ";
            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get root permission failed: Execute method Get_RootPermission in US_PERMISODal.", ex, true);
            }
        }
        /// <summary>
        /// Get permissions by root
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public DataTable Get_AllPermissionByParentID(string parentID)
        {
            try
            {
                string SQL = "SELECT [Id_Navegacion],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Estatus],[Nivel_Navegacion],[Secuencia],[Tipo_Permiso],[Id_Permiso] FROM [dbo].[View_Page_Permission] where Tipo_Permiso='P' and Codigo_Padres = @ParentID";
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@ParentID",parentID) 
            };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get  permission by root failed: Execute method Get_AllPermissionByParentID in US_PERMISODal.", ex, true);
            }
        }
        /// <summary>
        /// Delete permission
        /// </summary>
        /// <param name="navID"></param>
        /// <param name="pertype"></param>
        public void Delete_Permission(string navID, string pertype)
        {
            try
            {
                string executesqlstr = "DELETE US_PERMISO WHERE Valor_Permiso = @Id_Permiso and Tipo_Permiso=@Tipo_Permiso";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Permiso",navID),
                    new SqlParameter("@Tipo_Permiso",pertype)
                };
                
                SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete  permission failed: Execute method Delete_Permission in US_PERMISODal.", ex, true);
            }
        }
        /// <summary>
        /// Get page permissions by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataTable Get_RolePagePermissionByRoleID(int roleID)
        {
            try
            {
                string SQL = "SELECT [Id_Navegacion],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Nivel_Navegacion],[Secuencia],[Id_Rol],[Nombre_Rol],[Relacion_Rol],[Id_Permiso],[Tipo_Permiso]FROM  [dbo].[View_Role_Navigation] where Tipo_Permiso='P' and Id_Rol =  @RoleID";
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@RoleID",roleID) 
            };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get page permissions by role failed: Execute method Get_RolePagePermissionByRoleID in US_PERMISODal.", ex, true);
            }
        }
        /// <summary>
        /// Get operation permissions by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataTable Get_RoleOperationPermissionByRoleID(int roleID)
        {
            try
            {
                string SQL = "SELECT [Id_Navegacion],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Nivel_Navegacion],[Secuencia],[Id_Rol],[Nombre_Rol],[Relacion_Rol],[Id_Permiso],[Tipo_Permiso]FROM  [dbo].[View_Role_Navigation] where Tipo_Permiso='O' and Id_Rol =  @RoleID";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@RoleID",roleID) 
                };

                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get operation permissions by role failed: Execute method Get_RoleOperationPermissionByRoleID in US_PERMISODal.", ex, true);
            }
        }
    }
}





