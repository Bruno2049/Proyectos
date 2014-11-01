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
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace PAEEEM.Helpers
{
    /// <summary>
    /// Log info at application running
    /// </summary>
    /// <remarks>
    /// Used to record process info at application runtime
    /// </remarks>
    public class LsLog
    {
        public LsLog()
        { }

        /// <summary>
		/// Write the specified "Message" to the "FileName" file in the "BasePath" directory
		/// </summary>
		/// <param name="Message">Log message</param>
		static public void LogToFile(string Message)
		{
            string BasePath = "";
            string FileName = "";
            string FolderName = "";

			try
			{
                LsApplicationState ApplicationState = new LsApplicationState(HttpContext.Current.Application);
                BasePath = ApplicationState.ErrorLogPath;
                if (HttpContext.Current.Session != null)
                {
                    FolderName = HttpContext.Current.Session["UserName"] as string;
                    if (null != FolderName)
                    {
                        BasePath = Path.Combine(BasePath, FolderName);
                    }
                }
                FileName = DateTime.Now.Year.ToString() + string.Format("{0:00}", DateTime.Now.Month) + string.Format("{0:00}", DateTime.Now.Day) + ".txt";

				if (!Directory.Exists(BasePath))
				{
					//Directory.CreateDirectory(BasePath);
				}

				string FullFileName = Path.Combine(BasePath, FileName);

				if (FileName.Length > 0)
				{
					DateTime date = DateTime.Now;

                    Message = "----------" + date.ToShortDateString() + " " + date.ToLongTimeString() + "----------\r\n" + Message;

                    //using (StreamWriter sw = new StreamWriter(FullFileName, true)) 
                    //{
                    //    sw.AutoFlush = true;
                    //    sw.WriteLine(Message);
                    //}
				}
			}
			catch(Exception e)
			{
                throw e;
			}
		}
    }
}
