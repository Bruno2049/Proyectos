/* ----------------------------------------------------------------------
 * File Name: CAT_TIPO_SOCIEDADBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/6
 *
 * Description:   CAT_TIPO_SOCIEDAD business logic lay
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
    public class CAT_TIPO_SOCIEDADBLL
    {
        private static readonly CAT_TIPO_SOCIEDADBLL _classinstance = new CAT_TIPO_SOCIEDADBLL();

        public static CAT_TIPO_SOCIEDADBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get all CAT_TIPO_SOCIEDAD
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TIPO_SOCIEDAD()
        {
            try
            {
                return CAT_TIPO_SOCIEDADDal.ClassInstance.Get_All_CAT_TIPO_SOCIEDAD();
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
