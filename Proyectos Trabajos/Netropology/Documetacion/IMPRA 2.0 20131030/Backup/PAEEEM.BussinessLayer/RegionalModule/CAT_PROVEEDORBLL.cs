/* ----------------------------------------------------------------------
 * File Name: CAT_PROVEEDORBll.cs
 * 
 * Create Author: Eric
 * 
 * Create DateTime: 2011/7/5
 *
 * Description:   CAT_PROVEEDOR business logic lay
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
    public class CAT_PROVEEDORBll
    {
        private static readonly CAT_PROVEEDORBll _classinstance = new CAT_PROVEEDORBll();

        public static CAT_PROVEEDORBll ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Add Record
        /// </summary>
        public int Insert_CAT_PROVEEDOR(CAT_PROVEEDORModel instance)
        {
            return CAT_PROVEEDORDal.ClassInstance.Insert_CAT_PROVEEDOR(instance);
        }

        /// <summary>
        /// Delete Record
        /// </summary>
        public int Delete_CAT_PROVEEDOR(String pkid)
        {
            return CAT_PROVEEDORDal.ClassInstance.Delete_CAT_PROVEEDOR(pkid);
        }

        /// <summary>
        /// Update Record
        /// </summary>
        public int Update_CAT_PROVEEDOR(CAT_PROVEEDORModel instance)
        {
            return CAT_PROVEEDORDal.ClassInstance.Update_CAT_PROVEEDOR(instance);
        }

        /// <summary>
        /// Get Record By Primary Key
        /// </summary>
        public CAT_PROVEEDORModel Get_CAT_PROVEEDORByPKID(String pkid)
        {
            try
            {
                return CAT_PROVEEDORDal.ClassInstance.Get_CAT_PROVEEDORByPKID(pkid);
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get all Record
        /// </summary>
        public List<CAT_PROVEEDORModel> Get_AllCAT_PROVEEDOR()
        {
            return CAT_PROVEEDORDal.ClassInstance.Get_AllCAT_PROVEEDOR();
        }
    }
}

