/* ----------------------------------------------------------------------
 * File Name: CAT_TECNOLOGIABll.cs
 * 
 * Create Author: Eric
 * 
 * Create DateTime: 2011/7/5
 *
 * Description:   CAT_TECNOLOGIA business logic lay
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
    public class CAT_TECNOLOGIABll
    {
        private static readonly CAT_TECNOLOGIABll _classinstance = new CAT_TECNOLOGIABll();
        
        public static CAT_TECNOLOGIABll ClassInstance {get{return _classinstance; }}
    
        /// <summary>
        /// Add Record
        /// </summary>
        public int Insert_CAT_TECNOLOGIA(CAT_TECNOLOGIAModel instance)
        {
            return CAT_TECNOLOGIADal.ClassInstance.Insert_CAT_TECNOLOGIA(instance);
        }
        
        /// <summary>
        /// Delete Record
        /// </summary>
        public int Delete_CAT_TECNOLOGIA(String pkid)
        {
            return CAT_TECNOLOGIADal.ClassInstance.Delete_CAT_TECNOLOGIA(pkid);
        }
        
        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_CAT_TECNOLOGIA(CAT_TECNOLOGIAModel instance)
        {
            return CAT_TECNOLOGIADal.ClassInstance.Update_CAT_TECNOLOGIA(instance);
        }
        
        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public CAT_TECNOLOGIAModel Get_CAT_TECNOLOGIAByPKID(String pkid)
        {
            return CAT_TECNOLOGIADal.ClassInstance.Get_CAT_TECNOLOGIAByPKID(pkid);
        }
        
        /// <summary>
        /// Get Total Record Number
        /// </summary>
        public Int32 Get_CAT_TECNOLOGIACount(String tablename, String swhere)
        {
            return CAT_TECNOLOGIADal.ClassInstance.Get_CAT_TECNOLOGIACount(tablename,swhere);
        }
        
        /// <summary>
        /// Get Record With Split Page
        /// </summary>
        public DataTable Get_CAT_TECNOLOGIAListByPager
        (
            String sorder,
            Int32 pagecurrent,
			Int32 pageSize,
            out Int32 pagecount
        )
        {
            return CAT_TECNOLOGIADal.ClassInstance.Get_CAT_TECNOLOGIAListByPager(sorder,pagecurrent,pageSize,out pagecount);
        }
        
		/// <summary>
        /// Get all Record
        /// </summary>
		public List<CAT_TECNOLOGIAModel> Get_AllCAT_TECNOLOGIA()
		{
			return CAT_TECNOLOGIADal.ClassInstance.Get_AllCAT_TECNOLOGIA();
		}
    }
}

