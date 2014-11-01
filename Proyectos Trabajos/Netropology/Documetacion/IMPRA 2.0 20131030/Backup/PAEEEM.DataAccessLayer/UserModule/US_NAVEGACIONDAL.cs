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
    /// navigation DAL
    /// </summary>
    public class US_NAVEGACIONDal
    {
        private static readonly US_NAVEGACIONDal _classinstance = new US_NAVEGACIONDal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static US_NAVEGACIONDal ClassInstance {get{return _classinstance; }}
        /// <summary>
        /// Add navigation
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_US_NAVEGACION(US_NAVEGACIONModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO US_NAVEGACION (Nombre_Navegacion, Url_Navegacion, Codigo_Padres, Ruta_Padres, Estatus, Nivel_Navegacion, Secuencia ) VALUES (@Nombre_Navegacion, @Url_Navegacion, @Codigo_Padres, @Ruta_Padres, @Estatus, @Nivel_Navegacion, @Secuencia)";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Nombre_Navegacion",instance.Nombre_Navegacion),
                    new SqlParameter("@Url_Navegacion",instance.Url_Navegacion),
                    new SqlParameter("@Codigo_Padres",instance.Codigo_Padres),
                    new SqlParameter("@Ruta_Padres",instance.Ruta_Padres),
                    new SqlParameter("@Estatus",instance.Estatus),
                    new SqlParameter("@Nivel_Navegacion",instance.Nivel_Navegacion),
                    new SqlParameter("@Secuencia",instance.Secuencia)
                };

                return SqlHelper.ExecuteNonQueryGetID(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add navigation failed: Execute method Insert_US_NAVEGACION in US_NAVEGACIONDal.", ex, true);
            }
        }
        /// <summary>
        /// Delete navigation
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public int Delete_US_NAVEGACION(String pkid)
        {
            try
            {
                string executesqlstr = "DELETE US_NAVEGACION WHERE Id_Navegacion = @Id_Navegacion";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Navegacion",pkid)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete navigation failed: Execute method Delete_US_NAVEGACION in US_NAVEGACIONDal.", ex, true);
            }
        }
        /// <summary>
        /// Update navigation
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_US_NAVEGACION(US_NAVEGACIONModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE US_NAVEGACION SET Nombre_Navegacion = @Nombre_Navegacion, Url_Navegacion = @Url_Navegacion, Codigo_Padres = @Codigo_Padres, Ruta_Padres = @Ruta_Padres, Estatus = @Estatus, Nivel_Navegacion = @Nivel_Navegacion, Secuencia = @Secuencia WHERE Id_Navegacion = @Id_Navegacion";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Navegacion",instance.Id_Navegacion),
                    new SqlParameter("@Nombre_Navegacion",instance.Nombre_Navegacion),
                    new SqlParameter("@Url_Navegacion",instance.Url_Navegacion),
                    new SqlParameter("@Codigo_Padres",instance.Codigo_Padres),
                    new SqlParameter("@Ruta_Padres",instance.Ruta_Padres),
                    new SqlParameter("@Estatus",instance.Estatus),
                    new SqlParameter("@Nivel_Navegacion",instance.Nivel_Navegacion),
                    new SqlParameter("@Secuencia",instance.Secuencia)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update navigation failed: Execute method Update_US_NAVEGACION in US_NAVEGACIONDal.", ex, true);
            }
        }
        
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public US_NAVEGACIONModel Get_US_NAVEGACIONByPKID(String pkid)
        {
            string executesqlstr = "SELECT Id_Navegacion, Nombre_Navegacion, Url_Navegacion, Codigo_Padres, Ruta_Padres, Estatus, Nivel_Navegacion, Secuencia FROM US_NAVEGACION WHERE Id_Navegacion = @Id_Navegacion";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Id_Navegacion",pkid)
            };
            US_NAVEGACIONModel modelinstance = new US_NAVEGACIONModel();
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
        /// Get Total Record Number
        /// </summary>
        public Int32 Get_US_NAVEGACIONCount(String tablename, String swhere)
        {
            string sqlstr = "SELECT count(1) AS totalNum FROM " + tablename + " WHERE 1 = 1 "+swhere+" ";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sqlstr));
        }
        
        /// <summary>
        /// Get Record With Split Page
        /// </summary>
        public DataTable Get_US_NAVEGACIONListByPager
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
			
			DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "dbo.UP_Pager_GetUS_NAVEGACIONList", para);


  			int.TryParse(para[0].Value.ToString(), out pagecount);
            return dt;
        }
		/// <summary>
		/// Get All navigations
		/// </summary>
		/// <returns></returns>
		public List<US_NAVEGACIONModel> Get_AllUS_NAVEGACION()
        {
            List<US_NAVEGACIONModel> l_instance = new List<US_NAVEGACIONModel>();
            string strSQL = "SELECT Id_Navegacion, Nombre_Navegacion, Url_Navegacion, Codigo_Padres, Ruta_Padres, Estatus, Nivel_Navegacion, Secuencia FROM US_NAVEGACION";
            try
            {
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL))
                {
                    while (sdr.Read())
                    {
                        US_NAVEGACIONModel instance = EvaluateModel(sdr);
                        l_instance.Add(instance);
                    }
                }

                return l_instance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all navigations failed: Execute method Get_AllUS_NAVEGACION in US_NAVEGACIONDal.", ex, true);
            }
        }
        
        /// <summary>
        /// evaluate model
        /// </summary>
        private US_NAVEGACIONModel EvaluateModel(SqlDataReader sdr)
        {
            US_NAVEGACIONModel modelinstance = new US_NAVEGACIONModel();
            modelinstance.Id_Navegacion = sdr.IsDBNull(sdr.GetOrdinal("Id_Navegacion"))?0: sdr.GetInt32(sdr.GetOrdinal("Id_Navegacion"));
            modelinstance.Nombre_Navegacion = sdr.IsDBNull(sdr.GetOrdinal("Nombre_Navegacion"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Nombre_Navegacion"));
            modelinstance.Url_Navegacion = sdr.IsDBNull(sdr.GetOrdinal("Url_Navegacion"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Url_Navegacion"));
            modelinstance.Codigo_Padres = sdr.IsDBNull(sdr.GetOrdinal("Codigo_Padres"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Codigo_Padres"));
            modelinstance.Ruta_Padres = sdr.IsDBNull(sdr.GetOrdinal("Ruta_Padres"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Ruta_Padres"));
            modelinstance.Estatus = sdr.IsDBNull(sdr.GetOrdinal("Estatus"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Estatus"));
            modelinstance.Nivel_Navegacion = sdr.IsDBNull(sdr.GetOrdinal("Nivel_Navegacion"))?0: sdr.GetInt32(sdr.GetOrdinal("Nivel_Navegacion"));
            modelinstance.Secuencia = sdr.IsDBNull(sdr.GetOrdinal("Secuencia"))?string.Empty: sdr.GetString(sdr.GetOrdinal("Secuencia"));
            return modelinstance;
        }
        /// <summary>
        /// Get navigations by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataTable Get_RoleNavigationRootByRoleID(int roleID)
        {
            try
            {
                string SQL = "SELECT TOP 1000 [Id_Rol],[Nombre_Rol],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Secuencia],[Nivel_Navegacion],Id_Navegacion,Id_Permiso FROM [dbo].[View_Role_Navigation] WHERE [Nivel_Navegacion]=0 AND [Id_Rol]=@RoleID";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@RoleID",roleID)		                
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all navigations by role failed: Execute method Get_RoleNavigationRootByRoleID in US_NAVEGACIONDal.", ex, true);
            }
        }
        /// <summary>
        /// get navigations by role and root value
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rootValue"></param>
        /// <returns></returns>
        public DataTable Get_RoleNavigationByRoleID(int roleID, string rootValue)
        {
            try
            {
                string SQL = "SELECT TOP 1000 [Id_Rol],[Nombre_Rol],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Secuencia],[Nivel_Navegacion],Id_Navegacion,Id_Permiso FROM [dbo].[View_Role_Navigation] WHERE Codigo_Padres =@RootValue AND [Id_Rol]=@RoleID";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@RoleID",roleID),
                    new SqlParameter("@RootValue",rootValue)		
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all navigations by role failed: Execute method Get_RoleNavigationByRoleID in US_NAVEGACIONDal.", ex, true);
            }
        }
        /// <summary>
        /// Get url by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rootValue"></param>
        /// <returns></returns>
        public string Get_URLNavigationByRoleID(int roleID, string rootValue)
        {
            try
            {
                string SQL = "SELECT TOP 1 [Url_Navegacion]  FROM [dbo].[View_Role_Navigation] WHERE Id_Navegacion =@RootValue AND [Id_Rol]=@RoleID";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@RoleID",roleID),
                    new SqlParameter("@RootValue",rootValue)		
                };
                object obj = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

                return obj == null ? string.Empty : obj.ToString();
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get navigation urls by role failed: Execute method Get_URLNavigationByRoleID in US_NAVEGACIONDal.", ex, true);
            }
        }
        /// <summary>
        /// Get child nodes by parent node
        /// </summary>
        /// <param name="navID"></param>
        /// <returns></returns>
        public List<US_NAVEGACIONModel> GetChildrenNavListByParentCode(int navID)
        {
            List<US_NAVEGACIONModel> l_instance = new List<US_NAVEGACIONModel>();
            string strSQL = "SELECT [Id_Navegacion],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Estatus],[Nivel_Navegacion],[Secuencia],[Tipo_Permiso],[Id_Permiso] FROM [dbo].[View_Page_Permission] where Tipo_Permiso='P' and Codigo_Padres = @navID";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@navID",navID)                
            };
            try
            {
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL,para))
                {
                    while (sdr.Read())
                    {
                        US_NAVEGACIONModel instance = EvaluateModel(sdr);
                        l_instance.Add(instance);
                    }
                }

                return l_instance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get child navigation by parent failed: Execute method GetChildrenNavListByParentCode in US_NAVEGACIONDal.", ex, true);
            }
        }
        /// <summary>
        /// Check if child node exist
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool ExistChildrenNode(string code)
        {
            try
            {
                string SQL = "SELECT TOP 1 1 FROM [dbo].[View_Page_Permission] WHERE Codigo_Padres =@NavID ";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@NavID",code) 	
                };
                object obj = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);

                return obj == null ? false : true;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Check node failed: Execute method ExistChildrenNode in US_NAVEGACIONDal.", ex, true);
            }
        }
    }
}



 
 
