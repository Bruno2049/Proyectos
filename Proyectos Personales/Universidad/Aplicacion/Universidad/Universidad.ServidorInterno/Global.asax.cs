using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml;
using Universidad.Helpers;

namespace Universidad.ServidorInterno
{
    public class Global : System.Web.HttpApplication
    {
        //private static string ConnectionSql = "";

        protected void Application_Start(object sender, EventArgs e)
        {
            var applicationState = new LsApplicationState(HttpContext.Current.Application);
            //ConnectionSql = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //applicationState.SQLConnString = ConnectionSql;

            //applicationState.SQLConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}