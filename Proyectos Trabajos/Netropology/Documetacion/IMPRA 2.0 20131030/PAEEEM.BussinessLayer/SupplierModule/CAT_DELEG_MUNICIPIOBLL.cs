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
    public class CAT_DELEG_MUNICIPIOBLL
    {
        private static readonly CAT_DELEG_MUNICIPIOBLL _classinstance = new CAT_DELEG_MUNICIPIOBLL();

        public static CAT_DELEG_MUNICIPIOBLL ClassInstance { get { return _classinstance; } }

        /// <summary>
        /// Get All the DELEG_MUNICIPIO
        /// </summary>
        /// <returns></returns>
        public DataTable Get_All_CAT_DELEG_MUNICIPIO()
        {
            try
            {
                return CAT_DELEG_MUNICIPIODAL.ClassInstance.Get_All_CAT_DELEG_MUNICIPIO();
            }
            catch (LsDAException ex)
            {
                throw new LsBLException(this, ex.Message, ex, true);
            }
       }
         /// <summary>
        /// Get the DELEG_MUNICIPIO By Estado
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CAT_DELEG_MUNICIPIOByEstado(int Cve_Estado)
        {
            return CAT_DELEG_MUNICIPIODAL.ClassInstance.Get_CAT_DELEG_MUNICIPIOByEstado(Cve_Estado);
        }
    }
}
