using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using PAEEEM.Helpers;
using System.Security.Principal;
using System.Net;
using System.Collections.Generic;


namespace PAEEEM
{
    public partial class PrintForm : System.Web.UI.Page
    {
        static LsApplicationState appstate = new LsApplicationState(HttpContext.Current.Application);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //setup report viewer settings
                    report.Visible = true;
                    report.Reset();
                    report.ProcessingMode = ProcessingMode.Remote;

                    //get report parameters
                    string reportName = Request["ReportName"];
                   // string creditNumber = Request["CreditNumber"];

                    report.ShowCredentialPrompts = false;
                    report.ShowParameterPrompts = false;

                    report.ServerReport.ReportServerUrl = new System.Uri(appstate.ReportServerURL);
                    report.ServerReport.ReportPath = "/" + appstate.ReportFolder + "/" + reportName;
                    
                    //report server credentials
                    string userName = ConfigurationManager.AppSettings["MyReportViewerUser"].ToString();
                    string passWord = ConfigurationManager.AppSettings["MyReportViewerPassword"].ToString();
                    string domainName = ConfigurationManager.AppSettings["MyReportViewerDomain"].ToString();
                    IReportServerCredentials iReportServerCredential = new BasicReportCredentials(userName, passWord, domainName);

                    report.ServerReport.ReportServerCredentials = iReportServerCredential;
                    ReportParameter[] parm = null;

                    // updated by tina 2012-2-16
                    if (reportName == "Recuperados_Preview") // Report of Material
                    {
                        //string MaterialRecoveryActNumber = Request["MaterialRecoveryActNumber"];
                        string FI = Request["FI"];
                        string FF = Request["FF"];
                        string CD = Request["CD"];
                        string TEC = Request["TEC"];
                        string Act = Request["Act"];
                        string Prog = Request["Prog"];

                        //parm = new ReportParameter[]
                        //    {
                        //        new ReportParameter("RecuperacionID", MaterialRecoveryActNumber)
                        //    };

                        List<ReportParameter> paramList = new List<ReportParameter>();
                        // paramList.Add(new ReportParameter("RecuperacionID", MaterialRecoveryActNumber));
                         paramList.Add(new ReportParameter("FI", FI));
                         paramList.Add(new ReportParameter("FF", FF));
                         paramList.Add(new ReportParameter("CD", CD));
                         paramList.Add(new ReportParameter("Tec", TEC));
                         paramList.Add(new ReportParameter("Act", Act));
                         paramList.Add(new ReportParameter("Prog", Prog));


                        
                        report.ServerReport.SetParameters(paramList);
                    }
                    
                    else if (reportName == "ActaCircunst_InhabyDestruccion_Preview") // Report of Material
                     {
                         //string MaterialRecoveryActNumber = Request["MaterialRecoveryActNumber"];
                        string FI = Request["FI"];
                        string FF = Request["FF"];
                        string CD = Request["CD"];
                        string TEC = Request["TEC"];
                        string Act = Request["Act"];
                        string Prog = Request["Prog"];

                        //parm = new ReportParameter[]
                        //    {
                        //        new ReportParameter("RecuperacionID", MaterialRecoveryActNumber)
                        //    };

                        List<ReportParameter> paramList = new List<ReportParameter>();
                        // paramList.Add(new ReportParameter("RecuperacionID", MaterialRecoveryActNumber));
                         paramList.Add(new ReportParameter("FI", FI));
                         paramList.Add(new ReportParameter("FF", FF));
                         paramList.Add(new ReportParameter("CD", CD));
                         paramList.Add(new ReportParameter("Tec", TEC));
                         paramList.Add(new ReportParameter("Act", Act));
                         paramList.Add(new ReportParameter("Prog", Prog));


                        
                        report.ServerReport.SetParameters(paramList);
                      
                     }
                    else if (reportName == "Report_UnenableEquipment") // Report of Old Equipment
                    {
                        string UnableEquipmentActNumber = Request["UnableEquipmentActNumber"];
                        parm = new ReportParameter[]
                            {
                                 new ReportParameter("UnableEquipmentActNumber", UnableEquipmentActNumber)
                            };
                        report.ServerReport.SetParameters(parm);
                    }
                    else if (reportName != "Equipo Baja Eficiencia") // Reports of Validate Credit History , Report "Equipo Baja Eficiencia" have no parameter
                    {
                        string creditNumber = Request["CreditNumber"];
                        parm = new ReportParameter[]
                            {
                                 new ReportParameter("No_Credito", creditNumber)
                            };
                        report.ServerReport.SetParameters(parm);
                    }
                    //refresh report render
                    report.ServerReport.Refresh();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
