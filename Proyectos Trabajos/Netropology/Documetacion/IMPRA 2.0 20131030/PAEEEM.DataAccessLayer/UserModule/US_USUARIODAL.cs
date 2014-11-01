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
    /// User Dal
    /// </summary>
    public class US_USUARIODal
    {
        private static readonly US_USUARIODal _classinstance = new US_USUARIODal();
        /// <summary>
        /// Class Instance
        /// </summary>
        public static US_USUARIODal ClassInstance { get { return _classinstance; } }
        /// <summary>
        /// Add record to User table
        /// </summary>
        /// <param name="instance">User Entity</param>
        /// <returns>int: success-1, failure-0</returns>
        public int Insert_US_USUARIO(US_USUARIOModel instance)
        {
            try
            {
                string executesqlstr = "INSERT INTO US_USUARIO (Id_Rol, Nombre_Usuario, Tipo_Usuario, Contrasena, CorreoElectronico, Numero_Telefono, Nombre_Completo_Usuario, Estatus, Id_Departamento ) VALUES (@Id_Rol, @Nombre_Usuario, @Tipo_Usuario, @Contrasena, @CorreoElectronico, @Numero_Telefono, @Nombre_Completo_Usuario, @Estatus, @Id_Departamento)";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Rol",instance.Id_Rol),
                    new SqlParameter("@Nombre_Usuario",instance.Nombre_Usuario),
                    new SqlParameter("@Tipo_Usuario",instance.Tipo_Usuario),
                    new SqlParameter("@Contrasena",instance.Contrasena),
                    new SqlParameter("@CorreoElectronico",instance.CorreoElectronico),
                    new SqlParameter("@Numero_Telefono",instance.Numero_Telefono),
                    new SqlParameter("@Nombre_Completo_Usuario",instance.Nombre_Completo_Usuario),
                    new SqlParameter("@Estatus",instance.Estatus),
                    new SqlParameter("@Id_Departamento",instance.Id_Departamento)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Add new user failed: Execute method Insert_US_USUARIO in US_USUARIODAL, parameter is US_USUARIOModel entity.", ex, true);
            }
        }
        /// <summary>
        /// Delete exist user by user id
        /// </summary>
        /// <param name="pkid">int user id</param>
        /// <returns>return value: type: int; success-1; failure-0</returns>
        public int Delete_US_USUARIO(String pkid)
        {
            try
            {
                string executesqlstr = "DELETE US_USUARIO WHERE Id_Usuario = @Id_Usuario";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Usuario",pkid)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Delete exist user failed: Execute method Delete_US_USUARIO in US_USUARIODAL, parameter is user id.", ex, true);
            }
        }
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="instance">user entity</param>
        /// <returns>return type:int; success-1; failure-0</returns>
        public int Update_US_USUARIO(US_USUARIOModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE US_USUARIO SET Id_Rol = @Id_Rol, Nombre_Usuario = @Nombre_Usuario, Tipo_Usuario = @Tipo_Usuario, Contrasena = @Contrasena, CorreoElectronico = @CorreoElectronico, Numero_Telefono = @Numero_Telefono, Nombre_Completo_Usuario = @Nombre_Completo_Usuario, Estatus = @Estatus, Id_Departamento = @Id_Departamento WHERE Id_Usuario = @Id_Usuario";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Usuario",instance.Id_Usuario),
                    new SqlParameter("@Id_Rol",instance.Id_Rol),
                    new SqlParameter("@Nombre_Usuario",instance.Nombre_Usuario),
                    new SqlParameter("@Tipo_Usuario",instance.Tipo_Usuario),
                    new SqlParameter("@Contrasena",instance.Contrasena),
                    new SqlParameter("@CorreoElectronico",instance.CorreoElectronico),
                    new SqlParameter("@Numero_Telefono",instance.Numero_Telefono),
                    new SqlParameter("@Nombre_Completo_Usuario",instance.Nombre_Completo_Usuario),
                    new SqlParameter("@Estatus",instance.Estatus),
                    new SqlParameter("@Id_Departamento",instance.Id_Departamento)
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update exist user failed: Execute method Update_US_USUARIO in US_USUARIODAL, parameter is user entity.", ex, true);
            }
        }
        /// <summary>
        /// Get all Record
        /// </summary>
        public List<US_USUARIOModel> Get_AllUS_USUARIO()
        {
            List<US_USUARIOModel> l_instance = new List<US_USUARIOModel>();
            try
            {
                string strSQL = "SELECT Id_Usuario, Id_Rol, Nombre_Usuario, Tipo_Usuario, Contrasena, CorreoElectronico, Numero_Telefono, Nombre_Completo_Usuario, Estatus, Id_Departamento FROM US_USUARIO ORDER BY Nombre_Usuario";
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, strSQL))
                {
                    while (sdr.Read())
                    {
                        US_USUARIOModel instance = EvaluateModel(sdr);
                        l_instance.Add(instance);
                    }
                }

                return l_instance;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get all user failed: Execute method Get_AllUS_USUARIO in US_USUARIODAL.", ex, true);
            }
        }
        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="pkid">id</param>
        /// <returns>user model</returns>
        public US_USUARIOModel Get_US_USUARIOByPKID(String pkid)
        {
            try
            {
                string executesqlstr = "SELECT Id_Usuario, Id_Rol, Nombre_Usuario, Tipo_Usuario, Contrasena, CorreoElectronico, Numero_Telefono, Nombre_Completo_Usuario, Estatus, Id_Departamento FROM US_USUARIO WHERE Id_Usuario = @Id_Usuario";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Id_Usuario",pkid)
                };
                US_USUARIOModel modelinstance = new US_USUARIOModel();
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
                throw new LsDAException(this, "Get exist user failed: Execute method Get_US_USUARIOByPKID in US_USUARIODAL, parameter is user id.", ex, true);
            }
        }
        /// <summary>
        /// Get users by pager size
        /// </summary>
        /// <param name="sorder">Sort BY</param>
        /// <param name="currentPageIndex">Current Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pagecount">Total Page Count</param>
        /// <returns></returns>
        public DataTable Get_US_USUARIOListByPager (String sorder,   Int32 currentPageIndex,  Int32 pageSize,   out Int32 pagecount)
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

                DataTable dt = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "dbo.UP_Pager_GetUS_USUARIOList", para);
                int.TryParse(para[0].Value.ToString(), out pagecount);

                return dt;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get users by pager failed: Execute method Get_US_USUARIOListByPager in US_USUARIODAL.", ex, true);
            }
        }
        /// <summary>
        /// Build user entity
        /// </summary>
        /// <param name="sdr">SqlDataReader</param>
        /// <returns></returns>
        private US_USUARIOModel EvaluateModel(SqlDataReader sdr)
        {
            US_USUARIOModel modelinstance = new US_USUARIOModel();
            modelinstance.Id_Usuario = sdr.IsDBNull(sdr.GetOrdinal("Id_Usuario")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Id_Usuario"));
            modelinstance.Id_Rol = sdr.IsDBNull(sdr.GetOrdinal("Id_Rol")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Id_Rol"));
            modelinstance.Nombre_Usuario = sdr.IsDBNull(sdr.GetOrdinal("Nombre_Usuario")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Nombre_Usuario"));
            modelinstance.Tipo_Usuario = sdr.IsDBNull(sdr.GetOrdinal("Tipo_Usuario")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Tipo_Usuario"));
            modelinstance.Contrasena = sdr.IsDBNull(sdr.GetOrdinal("Contrasena")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Contrasena"));
            modelinstance.CorreoElectronico = sdr.IsDBNull(sdr.GetOrdinal("CorreoElectronico")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("CorreoElectronico"));
            modelinstance.Numero_Telefono = sdr.IsDBNull(sdr.GetOrdinal("Numero_Telefono")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Numero_Telefono"));
            modelinstance.Nombre_Completo_Usuario = sdr.IsDBNull(sdr.GetOrdinal("Nombre_Completo_Usuario")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Nombre_Completo_Usuario"));
            modelinstance.Estatus = sdr.IsDBNull(sdr.GetOrdinal("Estatus")) ? string.Empty : sdr.GetString(sdr.GetOrdinal("Estatus"));
            modelinstance.Id_Departamento = sdr.IsDBNull(sdr.GetOrdinal("Id_Departamento")) ? 0 : sdr.GetInt32(sdr.GetOrdinal("Id_Departamento"));
            return modelinstance;
        }
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="user">user model</param>
        /// <returns>int 1- success, 0-failure</returns>
        public int AuthenticationUser(US_USUARIOModel user)
        {
            int UserID = 0;
            string executesqlstr = "SELECT top 1  Id_Usuario FROM dbo.US_USUARIO  where Nombre_Usuario  = @UserName AND Contrasena =@Password";
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@UserName",user.Nombre_Usuario),
                    new SqlParameter("@Password",user.Contrasena)                    
                };
                object obj = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
                if (obj != null)
                {
                    int.TryParse(obj.ToString(), out UserID);
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Authenticate user failed: Execute method AuthenticationUser in US_USUARIODAL.", ex, true);
            }
            return UserID;
        }
        /// <summary>
        /// Validate user name exists
        /// </summary>
        /// <param name="username"></param>
        /// <param name="address"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidationUserName(string username, out string password, out string address)
        {
            try
            {
                string executesqlstr = "SELECT Contrasena, CorreoElectronico FROM dbo.US_USUARIO  where Nombre_Usuario  = @UserName ";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@UserName",username)                
                };
                
                DataTable User = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
                if (User != null && User.Rows.Count > 0)
                {
                    password = User.Rows[0]["Contrasena"].ToString();
                    address = User.Rows[0]["CorreoElectronico"].ToString();

                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Validate user exist failed: Execute method ValidationUserName in US_USUARIODAL.", ex, true);
            }
            
            password = "";
            address = "";
            return false;
        }
        /// <summary>
        /// Validate user email no duplicate
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsUserNameExist(string userName)
        {
            try
            {
                string executesqlstr = "SELECT top 1  Id_Usuario FROM dbo.US_USUARIO  where Nombre_Usuario = @Nombre_Usuario ";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Nombre_Usuario", userName)              
                };

                object obj = SqlHelper.ExecuteScalar(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);

                return obj == null ? false : true;
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Validate user email exist failed: Execute method IsUserNameExist in US_USUARIODAL.", ex, true);
            }
        }
        /// <summary>
        /// Update password
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int UpdatePassword(US_USUARIOModel instance)
        {
            try
            {
                string executesqlstr = "UPDATE US_USUARIO SET    Contrasena = @Contrasena  WHERE Nombre_Usuario = @Nombre_Usuario";

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Nombre_Usuario",instance.Nombre_Usuario),
                    new SqlParameter("@Contrasena",instance.Contrasena),     
                };

                return SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, executesqlstr, para);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Update user password failed: Execute method UpdatePassword in US_USUARIODAL.", ex, true);
            }
        }
        /// <summary>
        /// get user by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public US_USUARIOModel Get_UserByUserName(string userName)
        {
            try
            {
                string executesqlstr = "SELECT Id_Usuario, Id_Rol, Nombre_Usuario, Tipo_Usuario, Contrasena, CorreoElectronico, Numero_Telefono, Nombre_Completo_Usuario, Estatus, Id_Departamento FROM US_USUARIO WHERE Nombre_Usuario = @Nombre_Usuario";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@Nombre_Usuario",userName)
                };
                US_USUARIOModel modelinstance = new US_USUARIOModel();
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
                throw new LsDAException(this, "Get user by name failed: Execute method Get_UserByUserName in US_USUARIODAL.", ex, true);
            }
        }
        //updated by tina 2012-07-11
        /// <summary>
        /// get users with role
        /// </summary>
        /// <param name="ListTypes"></param>
        /// <param name="SortBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public DataTable GetUsersByUserType(List<string> ListTypes, string selectedRole, int selectedDepartment, int UserID, string SortBy, int PageIndex, int PageSize, out int PageCount)
        {
            DataTable dtResult = null;
            
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Count", SqlDbType.Int),
                    new SqlParameter("@SortBy", SortBy),
                    new SqlParameter("@PageIndex", PageIndex),
                    new SqlParameter("@PageSize", PageSize),
                    new SqlParameter("@Type1", ListTypes[0]),
                    new SqlParameter("@Type2", ListTypes[1]),
                    new SqlParameter("@Type3", ListTypes[2]),
                    new SqlParameter("@Type4", ListTypes[3]),
                    new SqlParameter("@Type5", ListTypes[4]),
                    new SqlParameter("@Type6", ListTypes[5]),
                    new SqlParameter("@Type7", ListTypes[6]),
                    new SqlParameter("@Type8", ListTypes[7]),
                    new SqlParameter("@SelectedRole", selectedRole),
                    new SqlParameter("@SelectedDepartment", selectedDepartment),
                    new SqlParameter("@UserID", UserID)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_users_with_role", paras);
                int.TryParse(paras[0].Value.ToString(), out PageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get users by type failed; Execute method GetUsersByUserType in US_USUARIODAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// get users with regional
        /// </summary>
        /// <param name="regional"></param>
        /// <param name="SortBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public DataTable GetUsersWithRegionalRole(int regional, int zone, int userID, string SortBy, int PageIndex, int PageSize, out int PageCount)
        {
            DataTable dtResult = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Count", SqlDbType.Int),
                    new SqlParameter("@SortBy", SortBy),
                    new SqlParameter("@PageIndex", PageIndex),
                    new SqlParameter("@PageSize", PageSize),
                    new SqlParameter("@Regional", regional),
                    new SqlParameter("@Zone", zone),
                    new SqlParameter("@UserID", userID)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_users_with_regional_role", paras);
                int.TryParse(paras[0].Value.ToString(), out PageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get users by type failed; Execute method GetUsersWithRegionalRole in US_USUARIODAL.", ex, true);
            }
            return dtResult;
        }
        /// <summary>
        /// Get users with zone role
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="SortBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public DataTable GetUsersWithZoneRole(int zone, int userID, string SortBy, int PageIndex, int PageSize, out int PageCount)
        {
            DataTable dtResult = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Count", SqlDbType.Int),
                    new SqlParameter("@SortBy", SortBy),
                    new SqlParameter("@PageIndex", PageIndex),
                    new SqlParameter("@PageSize", PageSize),
                    new SqlParameter("@Zone", zone),
                    new SqlParameter("@UserID", userID)
                };
                paras[0].Direction = ParameterDirection.Output;
                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.StoredProcedure, "sp_get_users_with_zone_role", paras);
                int.TryParse(paras[0].Value.ToString(), out PageCount);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get users by type failed; Execute method GetUsersWithZoneRole in US_USUARIODAL.", ex, true);
            }
            return dtResult;
        }
        //end
        /// <summary>
        /// Activate user with user id
        /// </summary>
        /// <param name="UserID">User Id</param>
        /// <returns></returns>
        public int ActivateUser(string UserID)
        {
            int iResult = 0;
            string SQL = "";
            try
            {
                SQL = "UPDATE [dbo].[US_USUARIO] SET [Estatus] = @Status "+
                            "WHERE [Id_Usuario] = @UserID";
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@Status", GlobalVar.STATUS_USER_ACTIVE)
                };

                iResult = SqlHelper.ExecuteNonQuery(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, SQL, paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Activate user failed;Execute method ActivateUser in US_USUARIODAL.", ex, true);
            }

            return iResult;
        }
        //added by tina 2012-07-10
        public DataTable GetUsersByUserTypeAndDepartment(string logUserRole, string selectedRole, int logUserDepartment, int selectedDepartment)
        {
            DataTable dtResult = null;

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT Id_Usuario,Nombre_Usuario FROM US_USUARIO WHERE 1=1");
                switch (logUserRole)
                {
                    case "C":
                        if (!string.IsNullOrEmpty(selectedRole))
                        {
                            if (selectedRole == "S_ALL")
                            {
                                sql.Append(" AND (Tipo_Usuario='S' OR Tipo_Usuario='S_B')");
                            }
                            else if (selectedRole == "D_ALL")
                            {
                                sql.Append(" AND (Tipo_Usuario='D_C' OR Tipo_Usuario='D_C_B')");
                            }
                            else
                            {
                                sql.Append(" AND Tipo_Usuario=@SelectedRole");
                            }
                        }
                        if (selectedDepartment != 0)
                        {
                            sql.Append(" AND Id_Departamento=@SelectedDepartment");
                        }
                        break;
                    case "R":                        
                        if (selectedDepartment != 0)
                        {
                            //updated by tina 2012-07-17
                            sql.Append(" AND ((Id_Departamento IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Zona = @SelectedDepartment) AND Tipo_Usuario ='S') OR");
                            sql.Append(" (Id_Departamento IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona = @SelectedDepartment) AND Tipo_Usuario ='S_B') OR");
                            sql.Append(" (Id_Departamento IN (SELECT Id_Centro_Disp FROM CAT_CENTRO_DISP WHERE Cve_Zona = @SelectedDepartment) AND Tipo_Usuario ='D_C') OR");
                            sql.Append(" (Id_Departamento IN (SELECT Id_Centro_Disp_Sucursal FROM CAT_CENTRO_DISP_SUCURSAL WHERE Cve_Zona = @SelectedDepartment) AND Tipo_Usuario ='D_C_B') OR");
                            sql.Append(" (Id_Departamento=@SelectedDepartment AND Tipo_Usuario ='Z_O'))");
                            //end
                        }
                        else
                        {
                            sql.Append(" AND ((Id_Departamento IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Region = @LogUserDepartment) AND Tipo_Usuario ='S') OR");
                            sql.Append(" (Id_Departamento IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Region = @LogUserDepartment) AND Tipo_Usuario ='S_B') OR");
                            sql.Append(" (Id_Departamento IN (SELECT Id_Centro_Disp FROM CAT_CENTRO_DISP WHERE Cve_Region = @LogUserDepartment) AND Tipo_Usuario ='D_C') OR");
                            sql.Append(" (Id_Departamento IN (SELECT Id_Centro_Disp_Sucursal FROM CAT_CENTRO_DISP_SUCURSAL WHERE Cve_Region = @LogUserDepartment) AND Tipo_Usuario ='D_C_B') OR");
                            sql.Append(" (Id_Departamento IN (SELECT Cve_Zona FROM CAT_ZONA WHERE Cve_Region = @LogUserDepartment) AND Tipo_Usuario ='Z_O'))");
                        }
                        break;
                    case "Z":
                        sql.Append(" AND ((Id_Departamento IN (SELECT Id_Proveedor FROM CAT_PROVEEDOR WHERE Cve_Zona = @LogUserDepartment) AND Tipo_Usuario ='S') OR");
                        sql.Append(" (Id_Departamento IN (SELECT Id_Branch FROM CAT_PROVEEDORBRANCH WHERE Cve_Zona = @LogUserDepartment) AND Tipo_Usuario ='S_B') OR");
                        sql.Append(" (Id_Departamento IN (SELECT Id_Centro_Disp FROM CAT_CENTRO_DISP WHERE Cve_Zona = @LogUserDepartment) AND Tipo_Usuario ='D_C') OR");
                        sql.Append(" (Id_Departamento IN (SELECT Id_Centro_Disp_Sucursal FROM CAT_CENTRO_DISP_SUCURSAL WHERE Cve_Zona = @LogUserDepartment) AND Tipo_Usuario ='D_C_B'))");
                        break;
                }

                sql.Append(" ORDER BY 2 ASC");
                
                SqlParameter[] paras = new SqlParameter[] {
                    new SqlParameter("@SelectedRole", selectedRole),
                    new SqlParameter("@LogUserDepartment", logUserDepartment),
                    new SqlParameter("@SelectedDepartment", selectedDepartment)
                };

                dtResult = SqlHelper.ExecuteDataTable(ParameterHelper.strCon_DBLsWebApp, CommandType.Text, sql.ToString(), paras);
            }
            catch (SqlException ex)
            {
                throw new LsDAException(this, "Get users by type and department failed; Execute method GetUsersByUserTypeAndDepartment in US_USUARIODAL.", ex, true);
            }
            return dtResult;
        }
        //end
    }
}





