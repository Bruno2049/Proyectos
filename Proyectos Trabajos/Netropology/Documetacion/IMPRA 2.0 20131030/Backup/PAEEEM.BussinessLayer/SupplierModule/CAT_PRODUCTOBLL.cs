/* ----------------------------------------------------------------------
 * File Name: CAT_PRODUCTOBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/7
 *
 * Description:   CAT_PRODUCTO business logic lay
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
    public class CAT_PRODUCTOBLL
    {
        private static readonly CAT_PRODUCTOBLL _classinstance = new CAT_PRODUCTOBLL();

        public static CAT_PRODUCTOBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get CAT_PRODUCTO by technology
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CAT_PRODUCTO_ByTechnology(string strTechnology,string strMarca)
        {
            try
            {
                return CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByTechnology(strTechnology,strMarca);
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get product with primary key
        /// </summary>
        /// <param name="strProductID"></param>
        /// <returns></returns>
        public DataTable Get_CAT_PRODUCTO_ByPK(string strProductID)
        {
            try
            {
                return CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByPK(strProductID);
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        /// <summary>
        /// Get products with technology, program and marca
        /// </summary>
        /// <param name="strTechnology"></param>
        /// <param name="strMarca"></param>
        /// <param name="strProgram"></param>
        /// <returns></returns>      
        public DataTable Get_CAT_PRODUCTO_ByTechnology(string strTechnology, string strMarca,string strProgram,string strTypeOfProduct)  //edit by coco 20110823
        {
            try
            {
                return CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ByTechnologyAndProgram(strTechnology, strMarca, strProgram,strTypeOfProduct);  //edit by coco 20110823
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        // RSA 2012-09-11 Start
        /// <summary>
        /// Get products for SE Description
        /// </summary>
        /// <returns></returns>      
        public string Get_CAT_PRODUCTO_ForSE_Description(string productId)
        {
            try
            {
                return CAT_PRODUCTODal.ClassInstance.Get_CAT_PRODUCTO_ForSE_Description(productId).Replace(", ", ",<br/>").Replace("{", "<span class=\"SE\">").Replace("}", "</span>");
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        // RSA 2012-09-11 End
    }
}
