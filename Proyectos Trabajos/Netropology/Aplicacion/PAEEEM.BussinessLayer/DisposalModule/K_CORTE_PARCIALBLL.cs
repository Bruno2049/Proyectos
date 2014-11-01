/* ----------------------------------------------------------------------
 * File Name: K_CORTE_PARCIALBLL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/10/13
 *
 * Description:   K_CORTE_PARCIAL business logic lay
 *----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;

namespace PAEEEM.BussinessLayer
{
    public class K_CORTE_PARCIALBLL
    {
        private static readonly K_CORTE_PARCIALBLL _classinstance = new K_CORTE_PARCIALBLL();

        public static K_CORTE_PARCIALBLL ClassInstance { get { return _classinstance; } }

        public int Insert_K_CORTE_PARCIAL(List<K_CORTE_PARCIALModel> listProducts)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int i = 0; i < listProducts.Count; i++)
                    {
                        K_CORTE_PARCIALModel instance = listProducts[i];
                        iResult += K_CORTE_PARCIALDAL.ClassInstance.Insert_K_CORTE_PARCIAL(instance);
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
