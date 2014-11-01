/* ----------------------------------------------------------------------
 * File Name: CAT_CENTRO_DISPBLL.cs
 * 
 * Create Author: Tina
 * 
 * Create DateTime: 2011/7/4
 *
 * Description:   CAT_CENTRO_DISP business logic lay
 *----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.BussinessLayer
{
    public class CAT_CENTRO_DISPBLL
    {
        private static readonly CAT_CENTRO_DISPBLL _classinstance = new CAT_CENTRO_DISPBLL();

        public static CAT_CENTRO_DISPBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get All the CAT_CENTRO_DISP
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_CENTRO_DISP(int Cve_Estatus_Centro_Disp)
        {
            return CAT_CENTRO_DISPDAL.ClassInstance.Get_All_CAT_CENTRO_DISPDAL(Cve_Estatus_Centro_Disp);
        }
        // Add by Tina 2011/08/03
        /// <summary>
        /// Get  CAT_CENTRO_DISP by Cve_Tecnologia
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CAT_CENTRO_DISPByTECHNOLOGY(int Cve_Estatus_Centro_Disp,string Cve_Tecnologia, int Id_usuario)// Update by Tina 2011/08/24
        {
            return CAT_CENTRO_DISPDAL.ClassInstance.Get_CAT_CENTRO_DISPByTECHNOLOGY(Cve_Estatus_Centro_Disp,Cve_Tecnologia,Id_usuario);
        }
        // End
    }
}
