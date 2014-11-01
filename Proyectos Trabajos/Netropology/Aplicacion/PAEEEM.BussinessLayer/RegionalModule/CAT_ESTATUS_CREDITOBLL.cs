/* ----------------------------------------------------------------------
 * File Name: CAT_ESTATUS_CREDITOBll.cs
 * 
 * Create Author: Eric
 * 
 * Create DateTime: 2011/7/5
 *
 * Description:   CAT_ESTATUS_CREDITO business logic lay
 *----------------------------------------------------------------------*/

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
    public class CAT_ESTATUS_CREDITOBll
    {
        private static readonly CAT_ESTATUS_CREDITOBll _classinstance = new CAT_ESTATUS_CREDITOBll();
        
        public static CAT_ESTATUS_CREDITOBll ClassInstance {get{return _classinstance; }}
        
		/// <summary>
        /// Get all Record
        /// </summary>
		public List<CAT_ESTATUS_CREDITOModel> Get_AllCAT_ESTATUS_CREDITO()
		{
			return CAT_ESTATUS_CREDITODal.ClassInstance.Get_AllCAT_ESTATUS_CREDITO();
		}
    }
}

