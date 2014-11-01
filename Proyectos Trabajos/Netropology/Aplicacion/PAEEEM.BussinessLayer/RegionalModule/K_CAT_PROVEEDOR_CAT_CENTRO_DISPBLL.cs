using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;
using PAEEEM.Entities;

namespace PAEEEM.BussinessLayer
{
    public class K_CAT_PROVEEDOR_CAT_CENTRO_DISPBLL
    {
        private static readonly K_CAT_PROVEEDOR_CAT_CENTRO_DISPBLL _classinstance = new K_CAT_PROVEEDOR_CAT_CENTRO_DISPBLL();

        public static K_CAT_PROVEEDOR_CAT_CENTRO_DISPBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// add record
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="supplierType"></param>
        /// <param name="listDisposal"></param>
        /// <returns></returns>
        public int Insert_K_CAT_PROVEEDOR_CAT_CENTRO_DISP(int supplierId, string supplierType, List<string> listDisposal)
        {
            int result = 0;
            foreach (string dispoal in listDisposal)
            {
                K_CAT_PROVEEDOR_CAT_CENTRO_DISPModel model = new K_CAT_PROVEEDOR_CAT_CENTRO_DISPModel();
                string[] strDisposal = dispoal.Split(',');
                model.Id_Proveedor = supplierId;
                model.Fg_Tipo_Proveedor = supplierType;
                model.Id_Centro_Disp = Convert.ToInt32(strDisposal[0]);
                model.Fg_Tipo_Centro_Disp = strDisposal[1];

                result += K_CAT_PROVEEDOR_CAT_CENTRO_DISPDal.ClassInstance.Insert_K_CAT_PROVEEDOR_CAT_CENTRO_DISP(model);
            }
            return result;
        }

        /// <summary>
        /// delete record
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="supplierType"></param>
        /// <param name="listDisposal"></param>
        /// <returns></returns>
        public int Delete_K_CAT_PROVEEDOR_CAT_CENTRO_DISP(int supplierId, string supplierType, List<string> listDeleteDisposal)
        {
            int result = 0;
            foreach (string dispoal in listDeleteDisposal)
            {
                string[] strDisposal = dispoal.Split(',');
                string disposalId = strDisposal[0];
                string disposalType = strDisposal[1];

                result += K_CAT_PROVEEDOR_CAT_CENTRO_DISPDal.ClassInstance.Delete_K_CAT_PROVEEDOR_CAT_CENTRO_DISP(supplierId, supplierType, disposalId, disposalType);
            }
            return result;
        }
    }
}
