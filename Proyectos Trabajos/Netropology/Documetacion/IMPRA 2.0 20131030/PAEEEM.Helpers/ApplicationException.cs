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
using System.Diagnostics;
using System.Text;

namespace PAEEEM.Helpers
{
    /// <summary>
    /// Base exception class for this application
    /// </summary>
    /// <remarks>
    /// Used to wrap all exceptions generated at application running
    /// </remarks>
    public class LsApplicationException:Exception
    {
        public LsApplicationException() { }
        
        public LsApplicationException(string Message, bool bLog):this(Message, null, bLog)
        {}

        public LsApplicationException(string Message, Exception oInnerException, bool bLog):this(null, Message, oInnerException, bLog)
        {            
        }

        public LsApplicationException(object oSource, string Message, Exception oInnerException, bool bLog)
        {
            if (oSource != null)
            {
                base.Source = oSource.ToString();
            }
            //
            if (bLog)
            { 
                Dump(Format(oSource, Message, oInnerException));
            }
        }

        /// <summary>
        /// Writes the entire message to trace listener and log file
        /// </summary>
        /// <param name="sMessage"></param>
        private void Dump(string sMessage)
        {
            // write to trace listener
            Trace.Write(sMessage);
            //write to file
            try
            {
                LsLog.LogToFile(sMessage);
            }
            catch (Exception)
            {
                throw new LsApplicationException("Write error log failed.", false);
            }
        }
        /// <summary>
        /// Format the error message, and the inner exception message
        /// </summary>
        /// <param name="oSource"></param>
        /// <param name="sMessage"></param>
        /// <param name="oInnerException"></param>
        /// <returns></returns>
        public static string Format(object oSource, string sMessage, Exception oInnerException)
        {
            StringBuilder sNewMessage = new StringBuilder();
            string sErrorStack = null;
            // get the error stack, if InnerException is null, sErrorStack will be "exception was not chained" and
            // should never be null
            sErrorStack = BuildErrorStack(oInnerException);

            Trace.AutoFlush = true;
            sNewMessage.Append("Exception Summary \n")
            .Append("-------------------------------\r\n")
            .Append(DateTime.Now.ToShortDateString())
            .Append(":")
            .Append(DateTime.Now.ToShortTimeString())
            .Append(" - ")
            .Append(sMessage)
            .Append("\r\n")
            .Append(sErrorStack);

            return sNewMessage.ToString();
        }

        /// <summary>
        /// Takes a first nested exception object and builds a error
        /// stack from its chained contents
        /// </summary>
        /// <param name="oChainedException"></param>
        /// <returns></returns>
        private static string BuildErrorStack(Exception oChainedException)
        {
            string sErrorStack = null;
            StringBuilder sbErrorStack = new StringBuilder();
            int nErrStackNum = 1;
            Exception oInnerException = null;
            if (oChainedException != null)
            {
                sbErrorStack.Append("Error Stack \n")
                .Append("------------------------\n");
                oInnerException = oChainedException;
                while (oInnerException != null)
                {
                    sbErrorStack.Append(nErrStackNum)
                    .Append(") ")
                    .Append(oInnerException.Message)
                    .Append("\n");
                    oInnerException =
                    oInnerException.InnerException;
                    nErrStackNum++;
                }
                sbErrorStack.Append("\n----------------------\n")
                .Append("Call Stack\n")
                .Append(oChainedException.StackTrace);
                sErrorStack = sbErrorStack.ToString();
            }
            else
            {
                sErrorStack = "exception was not chained";
            }

            return sErrorStack;
        }
    }
}
