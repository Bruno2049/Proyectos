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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.BussinessLayer
{
    public class US_PERMISOBll
    {
        private static readonly US_PERMISOBll _classinstance = new US_PERMISOBll();
        
        public static US_PERMISOBll ClassInstance {get{return _classinstance; }}
        /// <summary>
        /// Add permission
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_US_PERMISO(US_PERMISOModel instance)
        {
            return US_PERMISODal.ClassInstance.Insert_US_PERMISO(instance);
        }
        
        /// <summary>
        /// Delete Record
        /// </summary>
        public int Delete_US_PERMISO(String pkid)
        {
            return US_PERMISODal.ClassInstance.Delete_US_PERMISO(pkid);
        }
        
        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_US_PERMISO(US_PERMISOModel instance)
        {
            return US_PERMISODal.ClassInstance.Update_US_PERMISO(instance);
        }
        
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public US_PERMISOModel Get_US_PERMISOByPKID(String pkid)
        {
            return US_PERMISODal.ClassInstance.Get_US_PERMISOByPKID(pkid);
        }
        
		/// <summary>
        /// Get all Record
        /// </summary>
		public List<US_PERMISOModel> Get_AllUS_PERMISO()
		{
			return US_PERMISODal.ClassInstance.Get_AllUS_PERMISO();
		}
        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <returns></returns>
        public DataTable Get_AllPagePermission()
        {
            return US_PERMISODal.ClassInstance.Get_AllPagePermission();
        }
        /// <summary>
        /// Get root permission
        /// </summary>
        /// <returns></returns>
        public DataTable Get_RootPermission()
        {
            return US_PERMISODal.ClassInstance.Get_RootPermission();
        }
        /// <summary>
        /// Get permissions by parent
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public DataTable Get_AllPermissionByParentID(string parentID)
        {
            return US_PERMISODal.ClassInstance.Get_AllPermissionByParentID(parentID);
        }
        /// <summary>
        /// Delete permissions
        /// </summary>
        /// <param name="navID"></param>
        /// <param name="pertype"></param>
        public void Delete_Permission(string navID, string pertype)
        {
            US_PERMISODal.ClassInstance.Delete_Permission(navID, pertype);
        }
        /// <summary>
        /// Get page permissions by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataTable Get_RolePagePermissionByRoleID(int roleID)
        {
           return US_PERMISODal.ClassInstance.Get_RolePagePermissionByRoleID(roleID);
        }
        /// <summary>
        /// Get operation permissions by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataTable Get_RoleOperationPermissionByRoleID(int roleID)
        {
            return US_PERMISODal.ClassInstance.Get_RoleOperationPermissionByRoleID(roleID);
        }
    }
}

