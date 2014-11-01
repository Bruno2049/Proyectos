/* ----------------------------------------------------------------------
 * File Name: CAT_IDENTIFICACIONBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/6
 *
 * Description: CAT_IDENTIFICACION business logic lay
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
    public class CAT_IDENTIFICACIONBLL
    {
        private static readonly CAT_IDENTIFICACIONBLL _classinstance = new CAT_IDENTIFICACIONBLL();

        public static CAT_IDENTIFICACIONBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get all CAT_TIPO_INDUSTRIA
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_IDENTIFICACION()
        {
            try
            {
                return CAT_IDENTIFICACIONDal.ClassInstance.Get_All_CAT_IDENTIFICACION();
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
