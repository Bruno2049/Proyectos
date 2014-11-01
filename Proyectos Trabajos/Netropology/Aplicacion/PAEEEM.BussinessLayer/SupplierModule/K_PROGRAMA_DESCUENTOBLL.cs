/* ----------------------------------------------------------------------
 * File Name: K_PROGRAMA_DESCUENTOBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/12
 *
 * Description:   K_PROGRAMA_DESCUENTO business logic lay
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
    public class K_PROGRAMA_DESCUENTOBLL
    {
        private static readonly K_PROGRAMA_DESCUENTOBLL _classinstance = new K_PROGRAMA_DESCUENTOBLL();

        public static K_PROGRAMA_DESCUENTOBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get all CAT_TIPO_ACREDITACION
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_K_PROGRAMA_DESCUENTO(string strPk)
        {
            try
            {
                return K_PROGRAMA_DESCUENTODal.ClassInstance.Get_All_K_PROGRAMA_DESCUENTO(strPk);
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
