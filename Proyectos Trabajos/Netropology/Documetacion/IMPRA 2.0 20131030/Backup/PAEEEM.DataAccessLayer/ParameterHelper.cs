using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PAEEEM.Helpers;

namespace PAEEEM.DataAccessLayer
{
    /// <summary>
    /// SQL Connection Helper Class
    /// </summary>
    public class ParameterHelper
    {
       static LsApplicationState appstate = new LsApplicationState(HttpContext.Current.Application);
        /// <summary>
        /// SQL Connection String 
        /// </summary>
       public static string strCon_DBLsWebApp = appstate.SQLConnString;
       //public static string strCon_DBLsWebApp = @"Data Source=192.168.1.90\VIRTUALSERVER01; database=PAEEEM; user=administrator; password=admin_0;timeout = 3600";
    }
}
