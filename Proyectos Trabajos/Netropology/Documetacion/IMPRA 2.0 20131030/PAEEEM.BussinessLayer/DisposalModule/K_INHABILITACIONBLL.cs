using System;
using System.Collections;
using System.Collections.Generic;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;

namespace PAEEEM.BussinessLayer
{
    public class K_INHABILITACIONBLL
    {
        private static readonly K_INHABILITACIONBLL _classinstance = new K_INHABILITACIONBLL();

        public static K_INHABILITACIONBLL ClassInstance { get { return _classinstance; } }

        public int UpdateFinalActID(ArrayList listInhabilitacion, string finalActID)
        {
            int iResult = 0;
            try
            {
                for (int i = 0; i < listInhabilitacion.Count; i++)
                {
                    string inhabilitacionID = listInhabilitacion[i].ToString();
                    iResult += K_INHABILITACIONDal.ClassInstance.UpdateFinalActID(inhabilitacionID, finalActID);
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
