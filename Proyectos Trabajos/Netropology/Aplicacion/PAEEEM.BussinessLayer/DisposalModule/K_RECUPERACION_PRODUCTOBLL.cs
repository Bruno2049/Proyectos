using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.BussinessLayer
{
    public class K_RECUPERACION_PRODUCTOBLL
    {
        private static readonly K_RECUPERACION_PRODUCTOBLL _classinstance = new K_RECUPERACION_PRODUCTOBLL();

        public static K_RECUPERACION_PRODUCTOBLL ClassInstance { get { return _classinstance; } }

        public int Insert_K_RECUPERACION_PRODUCTO(int Id_Recuperacion, ArrayList listProducts)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int i = 0; i < listProducts.Count; i++)
                    {
                        int Id_Credito_Sustitucion = Convert.ToInt32(listProducts[i].ToString());
                        iResult += K_RECUPERACION_PRODUCTODal.ClassInstance.Insert_K_RECUPERACION_PRODUCTO(Id_Recuperacion, Id_Credito_Sustitucion);
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

        public int DeleteDataById_Recuperacion(string Id_Recuperacion)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    iResult += K_RECUPERACION_PRODUCTODal.ClassInstance.Delete_K_RECUPERACION_PRODUCTOById_Recuperacion(Id_Recuperacion);
                    iResult += K_RECUPERACIONDal.ClassInstance.Delete_K_RECUPERACION(Id_Recuperacion);
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
