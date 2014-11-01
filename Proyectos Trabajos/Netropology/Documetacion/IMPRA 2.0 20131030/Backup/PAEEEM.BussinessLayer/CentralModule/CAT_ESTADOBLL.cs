/* ----------------------------------------------------------------------
 * File Name: CAT_ESTADOBLL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description:   CAT_ESTADO business logic lay
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
    public class CAT_ESTADOBLL
    {
        private static readonly CAT_ESTADOBLL _classinstance = new CAT_ESTADOBLL();

        public static CAT_ESTADOBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get All the Estado
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_ESTADO()
        {
            return CAT_ESTADODAL.ClassInstance.Get_All_CAT_ESTADO();
        }
    }
}
