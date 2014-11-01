/* ----------------------------------------------------------------------
 * File Name: CAT_PROGRAMABLL.cs
 * 
 * Create Author: coco
 * 
 * Create DateTime: 2011/7/11
 *
 * Description:   CAT_PROGRAMA business logic lay
 *----------------------------------------------------------------------*/

using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;
namespace PAEEEM.BussinessLayer
{
    public class CAT_PROGRAMABLL
    {
        private static readonly CAT_PROGRAMABLL _classinstance = new CAT_PROGRAMABLL();

        public static CAT_PROGRAMABLL ClassInstance { get { return _classinstance; } }

        public DataTable Get_CAT_PROGRAMAbyPk(string strPk)
        {
            try
            {
                return CAT_PROGRAMADal.ClassInstance.Get_All_CAT_PROGRAMAByPK(strPk);
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
