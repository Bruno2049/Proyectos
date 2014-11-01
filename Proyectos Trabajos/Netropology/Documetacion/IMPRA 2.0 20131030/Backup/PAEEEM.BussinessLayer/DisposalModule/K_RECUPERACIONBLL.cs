using System;
using System.Collections;
using System.Collections.Generic;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;

namespace PAEEEM.BussinessLayer
{
    public class K_RECUPERACIONBLL
    {
        private static readonly K_RECUPERACIONBLL _classinstance = new K_RECUPERACIONBLL();

        public static K_RECUPERACIONBLL ClassInstance { get { return _classinstance; } }

        public int UpdateFinalActID(ArrayList listRecuperacion, string finalActID)
        {
            int iResult = 0;
            try
            {
                for (int i = 0; i < listRecuperacion.Count; i++)
                {
                    string recuperacionID = listRecuperacion[i].ToString();
                    iResult += K_RECUPERACIONDal.ClassInstance.UpdateFinalActID(recuperacionID, finalActID);
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
