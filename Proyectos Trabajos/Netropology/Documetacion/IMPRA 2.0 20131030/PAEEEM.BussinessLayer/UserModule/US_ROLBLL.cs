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
    public class US_ROLBll
    {
        private static readonly US_ROLBll _classinstance = new US_ROLBll();
        
        public static US_ROLBll ClassInstance {get{return _classinstance; }}
        /// <summary>
        /// Add role
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_US_ROL(US_ROLModel instance)
        {
            return US_ROLDal.ClassInstance.Insert_US_ROL(instance);
        }
        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_US_ROL(US_ROLModel instance)
        {
            return US_ROLDal.ClassInstance.Update_US_ROL(instance);
        }
        
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public US_ROLModel Get_US_ROLByPKID(String pkid)
        {
            return US_ROLDal.ClassInstance.Get_US_ROLByPKID(pkid);
        }
		/// <summary>
        /// Get all Record
        /// </summary>
		public List<US_ROLModel> Get_AllUS_ROL()
		{
			return US_ROLDal.ClassInstance.Get_AllUS_ROL();
		}
        /// <summary>
        /// Update role relation
        /// </summary>
        /// <param name="roleEnty"></param>
        /// <returns></returns>
        public int Update_RoleRelation(US_ROLModel roleEnty)
        {
            return US_ROLDal.ClassInstance.Update_RoleRelation(roleEnty);
        }
        /// <summary>
        /// Get role by pager
        /// </summary>
        /// <param name="sorder"></param>
        /// <param name="pagecurrent"></param>
        /// <param name="pageSize"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public DataTable Get_US_ROLListByPager(String sorder, Int32 pagecurrent, Int32 pageSize, out Int32 pagecount)
        {
            return US_ROLDal.ClassInstance.Get_US_ROLListByPager(sorder, pagecurrent, pageSize, out pagecount);
        }
    }
}

