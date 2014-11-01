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
    public class US_OPERACIONBll
    {
        private static readonly US_OPERACIONBll _classinstance = new US_OPERACIONBll();
        
        public static US_OPERACIONBll ClassInstance {get{return _classinstance; }}
    
        /// <summary>
        /// Add Record
        /// </summary>
        public int Insert_US_OPERACION(US_OPERACIONModel instance)
        {
            return US_OPERACIONDal.ClassInstance.Insert_US_OPERACION(instance);
        }
        
        /// <summary>
        /// Delete Record
        /// </summary>
        public int Delete_US_OPERACION(String pkid)
        {
            return US_OPERACIONDal.ClassInstance.Delete_US_OPERACION(pkid);
        }
        
        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_US_OPERACION(US_OPERACIONModel instance)
        {
            return US_OPERACIONDal.ClassInstance.Update_US_OPERACION(instance);
        }
        
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public US_OPERACIONModel Get_US_OPERACIONByPKID(String pkid)
        {
            return US_OPERACIONDal.ClassInstance.Get_US_OPERACIONByPKID(pkid);
        }
        
		/// <summary>
        /// Get all Record
        /// </summary>
		public List<US_OPERACIONModel> Get_AllUS_OPERACION()
		{
			return US_OPERACIONDal.ClassInstance.Get_AllUS_OPERACION();
		}

        public DataTable Get_OperationPermissionByNavCode(string navID)
        {
            return US_OPERACIONDal.ClassInstance.Get_OperationPermissionByNavCode(navID);
        }
    }
}

