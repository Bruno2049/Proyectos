using System;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using PAEEEM.BussinessLayer;
using PAEEEM.Entities;
using PAEEEM.Helpers;
using PAEEEM.DataAccessLayer;
using System.Configuration;
using System.Security.Principal;
using System.Net;

namespace PAEEEM.CustomReports
{
    public partial class ReportServerForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (null == Session["UserInfo"])
                    {
                        Response.Redirect("../Login/Login.aspx");
                        return;
                    }
                    //Init report data
                    reportServer.Visible = true;
                    reportServer.Reset();

                    /*
                    reportServer.Width = Unit.Parse(ConfigurationManager.AppSettings["ReportViewerWidth"]);
                    reportServer.Height = Unit.Parse(ConfigurationManager.AppSettings["ReportViewerHeight"]);
                    // SizeToReportContent works only when AsyncRendering = false
                    if (ConfigurationManager.AppSettings["SizeToReportContent"] != null)
                        reportServer.SizeToReportContent = Convert.ToBoolean(ConfigurationManager.AppSettings["SizeToReportContent"]);
                    */
                    
                    reportServer.ProcessingMode = ProcessingMode.Remote;
                    ServerReport rep = reportServer.ServerReport;
                    string reportpath = Request["reportpath"];
                    string reportServerURL = ConfigurationManager.AppSettings["ReportServerURL"];
                    rep.ReportServerUrl = new Uri(reportServerURL);
                    rep.ReportPath = reportpath;

                    string usr;
                    string pwd;
                    string dom;
                    IReportServerCredentials irsc;
                    /*
                    if (ConfigurationManager.AppSettings["MyReportViewerUserC"] != null)
                    {
                        usr = HandlingWebCfgCrypt.DecryptSettingsKeys(ConfigurationManager.AppSettings["MyReportViewerUserC"].ToString());
                        pwd = HandlingWebCfgCrypt.DecryptSettingsKeys(ConfigurationManager.AppSettings["MyReportViewerPasswordC"].ToString());
                        irsc = new BasicReportCredentials(((String[])usr.Split('\\'))[1], pwd, ((String[])usr.Split('\\'))[0]);
                    }
                    else
                     */
                    {
                        usr = ConfigurationManager.AppSettings["MyReportViewerUser"].ToString();
                        pwd = ConfigurationManager.AppSettings["MyReportViewerPassword"].ToString();
                        dom = ConfigurationManager.AppSettings["MyReportViewerDomain"].ToString(); ;
                        irsc = new BasicReportCredentials(usr, pwd, dom);
                    }
                    rep.ReportServerCredentials = irsc;

                    List<ReportParameter> paramList = new List<ReportParameter>();
                    /*UserModel user = (UserModel)Session["UserInfo"];*/
                    US_USUARIOModel UserModel = (US_USUARIOModel)Session["UserInfo"];

                    
                    ReportParameterInfoCollection repParameters;
                    repParameters = rep.GetParameters();

                    foreach (ReportParameterInfo repParamInfo in repParameters)
                    {
                        if (repParamInfo.Name == "UserID")
                        {
                            paramList.Add(new ReportParameter("UserID", new string[] { UserModel.Id_Usuario.ToString() }));
                        }
                    }

                    // parámetros del reporte
                    rep.SetParameters(paramList);

                    rep.Refresh();
                }
            }
            catch (Exception ex)
            {
                // throw ex;
                lError.Text = ex.Message;
            }
        }
    }

    public class BasicReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {

        // local variable for network credential.
        private string _UserName;
        private string _PassWord;
        private string _DomainName;
        public BasicReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public BasicReportCredentials()
        {
        }
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // return ((System.Security.Principal.WindowsIdentity)User.Identity).Impersonate();
                return null;  // not use ImpersonationUser
            }
        }
        public ICredentials NetworkCredentials
        {
            get
            {

                // use NetworkCredentials
                return new NetworkCredential(_UserName, _PassWord, _DomainName);
            }
        }
        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {

            // not use FormsCredentials unless you have implements a custom authentication.
            authCookie = null;
            user = password = authority = null;
            return false;
        }

    }

}
