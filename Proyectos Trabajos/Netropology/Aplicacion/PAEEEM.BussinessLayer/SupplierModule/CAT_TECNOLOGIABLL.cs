/*
	Copyright IMPRA, Inc. 2010
	All rights are reserved. Reproduction or transmission in whole or in part,
      in any form or by any means, electronic, mechanical or otherwise, is 
prohibited without the prior written consent of the copyright owner.

	$Archive:    $
	$Revision:   $
	$Author:     $
	$Date:       $
	Log at end of file
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PAEEEM.DataAccessLayer;
using PAEEEM.Helpers;

namespace PAEEEM.BussinessLayer
{
    public class CAT_TECNOLOGIABLL
    {
        private static readonly CAT_TECNOLOGIABLL _classinstance = new CAT_TECNOLOGIABLL();

        public static CAT_TECNOLOGIABLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get All the TECNOLOGIA
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_TECNOLOGIA()
        {
            return CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIA();
        }
        // Add by coco 2011/08/03
        public DataTable Get_All_CAT_TECNOLOGIAByProgram(string strProgram)
        {            
            try
            {
                return  CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgram(strProgram);
            }
            catch (Exception ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        // End
        // Add by Tina 2011/08/03
        public DataTable Get_CAT_TECNOLOGIAByProgramAndDisposal(int Program,int Disposal)
        {
            try
            {
                return CAT_TECNOLOGIADAL.ClassInstance.Get_CAT_TECNOLOGIAByProgramAndDisposal(Program,Disposal);
            }
            catch (Exception ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        // End
        // Add by coco 2011/08/10
        public DataTable Get_All_CAT_TECNOLOGIAByProgramAndProductID(string strProgram, string strProductID, string strTypeOfProduct, int idUsuario) // edit by coco 20110823
        {
            try
            {
                return CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgramAndProductID(strProgram, strProductID, strTypeOfProduct, idUsuario);  //edit by coco 20110823 
            }
            catch (Exception ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
        // End
        public DataTable Get_All_CAT_TECNOLOGIAByProgramAndDxCveCC(string strProgram, int idUsuario, string Dx_Cve_CC)
        {
            try
            {
                return CAT_TECNOLOGIADAL.ClassInstance.Get_All_CAT_TECNOLOGIAByProgramAndDxCveCC(strProgram, idUsuario, Dx_Cve_CC);  //edit by coco 20110823 
            }
            catch (Exception ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
        }
    }
}
