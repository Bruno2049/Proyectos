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
    public class US_NAVEGACIONBll
    {
        private static readonly US_NAVEGACIONBll _classinstance = new US_NAVEGACIONBll();
        
        public static US_NAVEGACIONBll ClassInstance {get{return _classinstance; }}
        /// <summary>
        /// Add navigation
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_US_NAVEGACION(US_NAVEGACIONModel instance)
        {
            return US_NAVEGACIONDal.ClassInstance.Insert_US_NAVEGACION(instance);
        }
        /// <summary>
        /// Delete navigation
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public int Delete_US_NAVEGACION(String pkid)
        {
            return US_NAVEGACIONDal.ClassInstance.Delete_US_NAVEGACION(pkid);
        }
        /// <summary>
        /// Update navigation
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update_US_NAVEGACION(US_NAVEGACIONModel instance)
        {
            return US_NAVEGACIONDal.ClassInstance.Update_US_NAVEGACION(instance);
        }
        /// <summary>
        /// Get navigation by id
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public US_NAVEGACIONModel Get_US_NAVEGACIONByPKID(String pkid)
        {
            return US_NAVEGACIONDal.ClassInstance.Get_US_NAVEGACIONByPKID(pkid);
        }
        /// <summary>
        /// Get navigation count
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public Int32 Get_US_NAVEGACIONCount(String tablename, String swhere)
        {
            return US_NAVEGACIONDal.ClassInstance.Get_US_NAVEGACIONCount(tablename,swhere);
        }
        
		/// <summary>
        /// Get all Record
        /// </summary>
		public List<US_NAVEGACIONModel> Get_AllUS_NAVEGACION()
		{
			return US_NAVEGACIONDal.ClassInstance.Get_AllUS_NAVEGACION();
		}
        /// <summary>
        /// get root by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataTable Get_RoleNavigationRootByRoleID(int roleID)
        {
            return US_NAVEGACIONDal.ClassInstance.Get_RoleNavigationRootByRoleID(roleID);
        }
        /// <summary>
        /// Get navigations by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rootValue"></param>
        /// <returns></returns>
        public DataTable Get_RoleNavigationByRoleID(int roleID, string rootValue)
        {
            return US_NAVEGACIONDal.ClassInstance.Get_RoleNavigationByRoleID(roleID,rootValue);
        }
        /// <summary>
        /// Get navigation urls by role
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="rootValue"></param>
        /// <returns></returns>
        public string Get_URLNavigationByRoleID(int roleID, string rootValue)
        {
            return US_NAVEGACIONDal.ClassInstance.Get_URLNavigationByRoleID(roleID, rootValue);
        }
        /// <summary>
        /// Get navigations by parent node
        /// </summary>
        /// <param name="navID"></param>
        /// <returns></returns>
        public List<US_NAVEGACIONModel> GetChildrenNavListByParentCode(int navID)
        {
            return US_NAVEGACIONDal.ClassInstance.GetChildrenNavListByParentCode(navID);
        }
        /// <summary>
        /// Check node exist
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool ExistNodeCode(string code)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Check child node exist
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool ExistChildrenNode(string code)
        {
            return US_NAVEGACIONDal.ClassInstance.ExistChildrenNode(code);
        }
    }
}

