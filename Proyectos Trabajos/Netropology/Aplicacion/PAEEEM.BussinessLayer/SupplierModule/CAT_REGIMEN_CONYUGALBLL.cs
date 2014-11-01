/* ----------------------------------------------------------------------
 * File Name: CAT_REGIMEN_CONYUGALBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/6
 *
 * Description:   CAT_REGIMEN_CONYUGAL business logic lay
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
    public class CAT_REGIMEN_CONYUGALBLL
    {
        private static readonly CAT_REGIMEN_CONYUGALBLL _classinstance = new CAT_REGIMEN_CONYUGALBLL();

        public static CAT_REGIMEN_CONYUGALBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get all CAT_REGIMEN_CONYUGAL
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_REGIMEN_CONYUGAL()
        {
            try
            {
                return CAT_REGIMEN_CONYUGALDal.ClassInstance.Get_All_CAT_REGIMEN_CONYUGAL();
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
