/* ----------------------------------------------------------------------
 * File Name: CAT_TIPO_ACREDITACIONBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/6
 *
 * Description:   CAT_TIPO_ACREDITACION business logic lay
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
    public class CAT_TIPO_ACREDITACIONBLL
    {
        private static readonly CAT_TIPO_ACREDITACIONBLL _classinstance = new CAT_TIPO_ACREDITACIONBLL();

        public static CAT_TIPO_ACREDITACIONBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get all CAT_TIPO_ACREDITACION
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TIPO_ACREDITACION()
        {
            try
            {
                return CAT_TIPO_ACREDITACIONDal.ClassInstance.Get_All_CAT_TIPO_ACREDITACION();
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
