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
    public class US_ROL_PERMISOBll
    {
        private static readonly US_ROL_PERMISOBll _classinstance = new US_ROL_PERMISOBll();
        
        public static US_ROL_PERMISOBll ClassInstance {get{return _classinstance; }}
        /// <summary>
        /// Add role-permission
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_US_ROL_PERMISO(US_ROL_PERMISOModel instance)
        {
            return US_ROL_PERMISODal.ClassInstance.Insert_US_ROL_PERMISO(instance);
        }
        
        /// <summary>
        /// Delete Record
        /// </summary>
        public int Delete_US_ROL_PERMISO(String pkid)
        {
            return US_ROL_PERMISODal.ClassInstance.Delete_US_ROL_PERMISO(pkid);
        }
        
        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_US_ROL_PERMISO(US_ROL_PERMISOModel instance)
        {
            return US_ROL_PERMISODal.ClassInstance.Update_US_ROL_PERMISO(instance);
        }
        
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public US_ROL_PERMISOModel Get_US_ROL_PERMISOByPKID(String pkid)
        {
            return US_ROL_PERMISODal.ClassInstance.Get_US_ROL_PERMISOByPKID(pkid);
        }
		/// <summary>
        /// Get all Record
        /// </summary>
		public List<US_ROL_PERMISOModel> Get_AllUS_ROL_PERMISO()
		{
			return US_ROL_PERMISODal.ClassInstance.Get_AllUS_ROL_PERMISO();
		}
        /// <summary>
        /// Get permissions by navigation code
        /// </summary>
        /// <param name="navCode"></param>
        /// <returns></returns>
        public DataTable Get_PermissionByNavCode(string navCode)
        {
            return US_ROL_PERMISODal.ClassInstance.Get_PermissionByNavCode(navCode);
        }
        /// <summary>
        /// Delete permissions by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public int Delete_Role_PermissionByRoleID(int roleID)
        {
            return US_ROL_PERMISODal.ClassInstance.Delete_Role_PermissionByRoleID(roleID);
        }
    }
}

