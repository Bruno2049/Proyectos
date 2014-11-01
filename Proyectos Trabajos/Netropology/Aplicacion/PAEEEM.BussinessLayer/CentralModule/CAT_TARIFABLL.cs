/* ----------------------------------------------------------------------
 * File Name: CAT_TARIFABLL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description:   CAT_TARIFA business logic lay
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
    public class CAT_TARIFABLL
    {
        private static readonly CAT_TARIFABLL _classinstance = new CAT_TARIFABLL();

        public static CAT_TARIFABLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get All the Tarifa
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TARIFA()
        {
            return CAT_TARIFADAL.ClassInstance.Get_All_CAT_TARIFA();
        }
    }
}
