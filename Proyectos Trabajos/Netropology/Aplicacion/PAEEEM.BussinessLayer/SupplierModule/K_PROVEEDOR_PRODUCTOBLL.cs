/* ----------------------------------------------------------------------
 * File Name: K_PROVEEDOR_PRODUCTOBLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/6
 *
 * Description:   K_PROVEEDOR_PRODUCTO business logic lay
 *----------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;
using PAEEEM.Entities;

namespace PAEEEM.BussinessLayer
{
    public class K_PROVEEDOR_PRODUCTOBLL
    {
        private static readonly K_PROVEEDOR_PRODUCTOBLL _classinstance = new K_PROVEEDOR_PRODUCTOBLL();

        public static K_PROVEEDOR_PRODUCTOBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// get all CAT_TIPO_PROPIEDAD
        /// </summary>
        /// <returns></returns>
        public DataTable Get_K_PROVEEDOR_PRODUCTO_ByPK(string strProductID, string strProviderID)
        {
            try
            {
                return K_PROVEEDOR_PRODUCTODal.ClassInstance.Get_K_PROVEEDOR_PRODUCTO_ByPK(strProductID,strProviderID);
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }

        /// <summary>
        /// add record
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="arrListProduct"></param>
        /// <param name="estatus"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        //edit by coco 2010-05-30
        public int Insert_K_PROVEEDOR_PRODUCTO(int supplierId, ArrayList arrListProduct,int estatus,DateTime createDate,ArrayList arrlistUnitPrice)
        {
            int result = 0;
           for(int i=0;i<arrListProduct.Count ;i++)
            {
                K_PROVEEDOR_PRODUCTOEntity model = new K_PROVEEDOR_PRODUCTOEntity();
                model.Id_Proveedor = supplierId;
                model.Cve_Producto = Convert.ToInt32(arrListProduct[i]);
                model.Cve_Estatus_Prov_Prod = estatus;
                model.Dt_Fecha_Prov_Prod = createDate;
                model.Mt_Precio_Unitario = Convert.ToDecimal(arrlistUnitPrice[i]);
                result += K_PROVEEDOR_PRODUCTODal.ClassInstance.Insert_K_PROVEEDOR_PRODUCTO(model);
            }
            return result;
        }
        //end edit
    }
}
