/* ----------------------------------------------------------------------
 * File Name: CAT_FABRICANTEBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/7
 *
 * Description:   CAT_FABRICANTE business logic lay
 *----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.BussinessLayer
{
    public class CAT_FABRICANTEBLL
    {
        private static readonly CAT_FABRICANTEBLL _classinstance = new CAT_FABRICANTEBLL();

        public static CAT_FABRICANTEBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get all CAT_TIPO_INDUSTRIA
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_FABRICANTE()
        {
            try
            {
                return CAT_FABRICANTEDal.ClassInstance.Get_All_CAT_FABRICANTE();
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
