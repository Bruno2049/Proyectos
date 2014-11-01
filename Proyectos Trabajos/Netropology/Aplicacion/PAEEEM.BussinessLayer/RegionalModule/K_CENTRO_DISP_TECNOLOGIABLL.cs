/* ----------------------------------------------------------------------
 * File Name: K_CENTRO_DISP_TECNOLOGIABLL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/10/27
 *
 * Description:   K_CENTRO_DISP_TECNOLOGIA business logic lay
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
    public class K_CENTRO_DISP_TECNOLOGIABLL
    {
        private static readonly K_CENTRO_DISP_TECNOLOGIABLL _classinstance = new K_CENTRO_DISP_TECNOLOGIABLL();

        public static K_CENTRO_DISP_TECNOLOGIABLL ClassInstance { get { return _classinstance; } }

        public int Insert_K_CENTRO_DISP_TECNOLOGIA(List<K_CENTRO_DISP_TECNOLOGIAModel> listAssignedTechnology)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int i = 0; i < listAssignedTechnology.Count; i++)
                    {
                        K_CENTRO_DISP_TECNOLOGIAModel instance = listAssignedTechnology[i];
                        iResult += K_CENTRO_DISP_TECNOLOGIADAL.ClassInstance.Insert_K_CENTRO_DISP_TECNOLOGIA(instance);
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
        public int Delete_K_CENTRO_DISP_TECNOLOGIA(List<string[]> listDelete)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int i = 0; i < listDelete.Count; i++)
                    {
                        string[] paras = listDelete[i];
                        iResult += K_CENTRO_DISP_TECNOLOGIADAL.ClassInstance.Delete_K_CENTRO_DISP_TECNOLOGIA(Convert.ToInt32(paras[0]), Convert.ToInt32(paras[1]), paras[2]);
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
