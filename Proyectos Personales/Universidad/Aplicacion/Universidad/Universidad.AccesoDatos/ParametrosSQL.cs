using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Universidad.Helpers;

namespace Universidad.AccesoDatos
{
    public class ParametrosSQL
    {
        static LsApplicationState appstate = new LsApplicationState(HttpContext.Current.Application);
        /// <summary>
        /// SQL Connection String 
        /// </summary>
        public static string strCon_DBLsWebApp = appstate.SQLConnString;
    }
}
