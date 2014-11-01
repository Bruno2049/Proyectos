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
using System.Web;

namespace PAEEEM.Helpers
{
    public class LsBLException:LsApplicationException
    {
        public LsBLException() : base() { }

        public LsBLException(string message, bool bLog) : base(message, bLog) { }

        public LsBLException(string message, Exception oInnerException, bool bLog) : base(message, oInnerException, bLog) { }

        public LsBLException(object oSource, string message, Exception oInnerException, bool bLog) : base(oSource, message, oInnerException, bLog) { }
    }
}
