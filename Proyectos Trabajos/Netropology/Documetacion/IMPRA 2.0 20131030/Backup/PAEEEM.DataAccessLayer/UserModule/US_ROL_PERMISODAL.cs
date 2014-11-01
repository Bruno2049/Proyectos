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
    /// Role Permission Relation
    /// </summary>
    public class US_ROL_PERMISODal
    {
        private static readonly US_ROL_PERMISODal _classinstance = new US_ROL_PERMISODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static US_ROL_PERMISODal ClassInstance {get{return _classinstance; }}
        /// <summary>
        /// Add role-permission
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_US_ROL_PERMISO(US_ROL_PERMISOModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO US_ROL_PERMISO (Id_Rol, Id_Permiso ) VALUES (@Id_Rol, @Id_Permiso)";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Rol",instance.Id_Rol),
                    new SqlParameter("@Id_Permiso",instance.Id_Permiso)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add role_permission relation failed: Execute method Insert_US_ROL_PERMISO in US_ROL_PERMISODal.", ex, true);
            }
        }
        /// <summary>
        /// Delete role permission relation
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public int Delete_US_ROL_PERMISO(String pkid)
        {
            string executesqlstr = "DELETE US_ROL_PERMISO WHERE Id_Rol_Permiso = @Id_Rol_Permiso";
            
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Id_Rol_Permiso",pkid)
            };
            
            return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
        }
        /// <summary>
        /// Update role-permission relation
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_US_ROL_PERMISO(US_ROL_PERMISOModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE US_ROL_PERMISO SET Id_Rol = @Id_Rol, Id_Permiso = @Id_Permiso WHERE Id_Rol_Permiso = @Id_Rol_Permiso";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Rol_Permiso",instance.Id_Rol_Permiso),
                    new SqlParameter("@Id_Rol",instance.Id_Rol),
                    new SqlParameter("@Id_Permiso",instance.Id_Permiso)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update role_permission relation failed: Execute method Update_US_ROL_PERMISO in US_ROL_PERMISODal.", ex, true);
            }
        }
        
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public US_ROL_PERMISOModel Get_US_ROL_PERMISOByPKID(String pkid)
        {
            string executesqlstr = "SELECT Id_Rol_Permiso, Id_Rol, Id_Permiso FROM US_ROL_PERMISO WHERE Id_Rol_Permiso = @Id_Rol_Permiso";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Id_Rol_Permiso",pkid)
            };
            US_ROL_PERMISOModel modelinstance = new US_ROL_PERMISOModel();
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
        /// Get all Record
        /// </summary>
		public List<US_ROL_PERMISOModel> Get_AllUS_ROL_PERMISO()
        {
            List<US_ROL_PERMISOModel> l_instance = new List<US_ROL_PERMISOModel>();
            string strSQL = "SELECT Id_Rol_Permiso, Id_Rol, Id_Permiso FROM US_ROL_PERMISO";
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text,strSQL))
            {
                while (sdr.Read())
                {
                    US_ROL_PERMISOModel instance = EvaluateModel(sdr);
                    l_instance.Add(instance);
                }
            }

            return l_instance;
        }
        
        /// <summary>
        /// evaluate model
        /// </summary>
        private US_ROL_PERMISOModel EvaluateModel(SqlDataReader sdr)
        {
            US_ROL_PERMISOModel modelinstance = new US_ROL_PERMISOModel();
            modelinstance.Id_Rol_Permiso = sdr.IsDBNull(sdr.GetOrdinal("Id_Rol_Permiso"))?0: sdr.GetInt32(sdr.GetOrdinal("Id_Rol_Permiso"));
            modelinstance.Id_Rol = sdr.IsDBNull(sdr.GetOrdinal("Id_Rol"))?0: sdr.GetInt32(sdr.GetOrdinal("Id_Rol"));
            modelinstance.Id_Permiso = sdr.IsDBNull(sdr.GetOrdinal("Id_Permiso"))?0: sdr.GetInt32(sdr.GetOrdinal("Id_Permiso"));
            return modelinstance;
        }
        /// <summary>
        /// Get permissions by navigation code
        /// </summary>
        /// <param name="navCode"></param>
        /// <returns></returns>
        public DataTable Get_PermissionByNavCode(string navCode)
        {
            try
            {
                string SQL = "SELECT [Id_Navegacion],[Nombre_Navegacion],[Url_Navegacion],[Codigo_Padres],[Ruta_Padres],[Estatus],[Nivel_Navegacion],[Secuencia],[Tipo_Permiso],[Id_Permiso] FROM [dbo].[View_Page_Permission] where Tipo_Permiso='P' and Id_Navegacion = @NavCode";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@NavCode",navCode) 
                };
                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, para);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get permissions by navigation code failed: Execute method Get_PermissionByNavCode in US_ROL_PERMISODal.", ex, true);
            }
        }
        /// <summary>
        /// Delete role_permission
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public int Delete_Role_PermissionByRoleID(int roleID)
        {
            try
            {
                string executesqlstr = "DELETE US_ROL_PERMISO WHERE [Id_Rol] = @Id_Rol";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Rol",roleID)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete permissions by role failed: Execute method Delete_Role_PermissionByRoleID in US_ROL_PERMISODal.", ex, true);
            }
        }
    }
}



 
 
