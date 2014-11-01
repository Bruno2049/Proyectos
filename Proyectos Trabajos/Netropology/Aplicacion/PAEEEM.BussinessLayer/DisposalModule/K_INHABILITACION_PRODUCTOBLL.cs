using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;

namespace PAEEEM.BussinessLayer
{
    public class K_INHABILITACION_PRODUCTOBLL
    {
        private static readonly K_INHABILITACION_PRODUCTOBLL _classinstance = new K_INHABILITACION_PRODUCTOBLL();

        public static K_INHABILITACION_PRODUCTOBLL ClassInstance { get { return _classinstance; } }

        public int Insert_K_INHABILITACION_PRODUCTO(int Id_Inhabilitacion,ArrayList listProducts)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int i = 0; i < listProducts.Count; i++)
                    {
                        int Id_Credito_Sustitucion = Convert.ToInt32(listProducts[i].ToString());
                        iResult += K_INHABILITACION_PRODUCTODal.ClassInstance.Insert_K_INHABILITACION_PRODUCTO(Id_Inhabilitacion, Id_Credito_Sustitucion);
                    }
                    scope.Complete();
                }
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, false);
            }
            return iResult;
        }
    }
}
