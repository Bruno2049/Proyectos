/* ----------------------------------------------------------------------
 * File Name: K_RECUP_RESIDUOSBLL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/10/12
 *
 * Description:   K_RECUP_RESIDUOS business logic lay
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
    public class K_RECUP_RESIDUOSBLL
    {
        private static readonly K_RECUP_RESIDUOSBLL _classinstance = new K_RECUP_RESIDUOSBLL();

        public static K_RECUP_RESIDUOSBLL ClassInstance { get { return _classinstance; } }

        public int Insert_K_RECUP_RESIDUOS(List<K_RECUP_RESIDUOSModel> listMaterial)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int i = 0; i < listMaterial.Count; i++)
                    {
                        K_RECUP_RESIDUOSModel instance = listMaterial[i];
                        iResult += K_RECUP_RESIDUOSDAL.ClassInstance.Insert_K_RECUP_RESIDUOS(instance);
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

        public int UpdateK_RECUP_RESIDUOSStatus(List<string[]> listUpdate)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int i = 0; i < listUpdate.Count; i++)
                    {
                        string[] update = listUpdate[i];
                        iResult += K_RECUP_RESIDUOSDAL.ClassInstance.UpdateK_RECUP_RESIDUOSStatus("Y", (int)DisposalStatus.INHABILITADO, update[0], update[1]);
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
