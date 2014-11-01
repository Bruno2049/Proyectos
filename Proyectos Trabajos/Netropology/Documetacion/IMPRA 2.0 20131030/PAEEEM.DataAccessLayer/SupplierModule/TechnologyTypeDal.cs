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
using System.Data.SqlClient;
using PAEEEM.Entities;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// Data Access Layer For Technology Type
    /// </summary>
    public class TechnologyTypeDal
    {
        /// <summary>
        /// Readonly variable for class instance
        /// </summary>
        private static readonly TechnologyTypeDal _classInstance = new TechnologyTypeDal();
        /// <summary>
        /// Property for class instance
        /// </summary>
        public static TechnologyTypeDal ClassInstance { get { return _classInstance; } }
    }
}
