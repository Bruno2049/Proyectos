/* ----------------------------------------------------------------------
 * File Name: K_TARIFA_COSTOBLL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/6/27
 *
 * Description:   K_TARIFA_COSTO business logic lay
 *----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.BussinessLayer
{
    public class K_TARIFA_COSTOBLL
    {
        private static readonly K_TARIFA_COSTOBLL _classinstance = new K_TARIFA_COSTOBLL();

        public static K_TARIFA_COSTOBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get Record by Estado
        /// </summary>
        /// <param name="sorder"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Cve_Estado"></param>
        /// <param name="Periodo"></param>
        /// <param name="Cve_Tarifa"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public DataTable Get_K_TARIFA_COSTOListByPagerAndEstadoID(String sorder, Int32 currentPageIndex, Int32 pageSize, int Cve_Estado, String Periodo, int Cve_Tarifa, out Int32 pagecount)
        {
            try
            {
                return  K_TARIFA_COSTODAL.ClassInstance.Get_K_TARIFA_COSTOListByPagerAndEstadoID(sorder, currentPageIndex, pageSize, Cve_Estado, Periodo, Cve_Tarifa, out pagecount);
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Add K_TARIFA_COSTO
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Insert_K_TARIFA_COSTO(K_TARIFA_COSTOModel instance)
        {
            return K_TARIFA_COSTODAL.ClassInstance.Insert_K_TARIFA_COSTO(instance);
        }
    }
}
