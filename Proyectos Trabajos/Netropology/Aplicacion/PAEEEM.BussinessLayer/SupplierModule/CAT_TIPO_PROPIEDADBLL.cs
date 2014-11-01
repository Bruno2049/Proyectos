/* ----------------------------------------------------------------------
 * File Name: CAT_CENTRO_DISPBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/6
 *
 * Description:   CAT_TIPO_INDUSTRIA business logic lay
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
    public class CAT_TIPO_PROPIEDADBLL
    {
        private static readonly CAT_TIPO_PROPIEDADBLL _classinstance = new CAT_TIPO_PROPIEDADBLL();

        public static CAT_TIPO_PROPIEDADBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get all CAT_TIPO_PROPIEDAD
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TIPO_PROPIEDAD()
        {
            try
            {
                return CAT_TIPO_PROPIEDADDal.ClassInstance.Get_All_CAT_TIPO_PROPIEDAD();
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
