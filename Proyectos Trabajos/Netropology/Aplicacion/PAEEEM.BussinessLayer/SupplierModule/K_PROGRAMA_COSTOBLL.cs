/* ----------------------------------------------------------------------
 * File Name: K_PROGRAMA_COSTOBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/12
 *
 * Description:   K_PROGRAMA_COSTO business logic lay
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
    public class K_PROGRAMA_COSTOBLL
    {
        private static readonly K_PROGRAMA_COSTOBLL _classinstance = new K_PROGRAMA_COSTOBLL();

        public static K_PROGRAMA_COSTOBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get all CAT_TIPO_ACREDITACION
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_K_PROGRAMA_COSTO(string strPk)
        {
            try
            {
                return K_PROGRAMA_COSTODal.ClassInstance.Get_All_K_PROGRAMA_COSTO(strPk);
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
