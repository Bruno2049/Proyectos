using System;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Net;
using System.Collections.Generic;


namespace PAEEEM
{
    public partial class PrintForm : System.Web.UI.Page
    {
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
                    var reportName = Request["ReportName"];

                    report.ShowCredentialPrompts = false;
                    report.ShowParameterPrompts = false;

                    var reportServerURL = ConfigurationManager.AppSettings["ReportServerURL"];
                    report.ServerReport.ReportServerUrl = new System.Uri(reportServerURL);
                    
                    var carpetaReportes = ConfigurationManager.AppSettings["CarpetaReportes"];
                    report.ServerReport.ReportPath = "/"+ carpetaReportes + "/" + reportName;

                    //report server credentials
                    var userName = ConfigurationManager.AppSettings["MyReportViewerUser"];
                    var passWord = ConfigurationManager.AppSettings["MyReportViewerPassword"];
                    var domainName = ConfigurationManager.AppSettings["MyReportViewerDomain"];
                    IReportServerCredentials iReportServerCredential = new BasicReportCredentials(userName, passWord, domainName);

                    report.ServerReport.ReportServerCredentials = iReportServerCredential;
                    ReportParameter[] parm = null;

                    // updated by tina 2012-2-16
                    if (reportName == "Recuperados_Preview") // Report of Material
                    {
                        var FI = Request["FI"];
                        var FF = Request["FF"];
                        var CD = Request["CD"];
                        var TEC = Request["TEC"];
                        var Act = Request["Act"];
                        var Prog = Request["Prog"];

                        var paramList = new List<ReportParameter>();
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
                         var FI = Request["FI"];
                         var FF = Request["FF"];
                         var CD = Request["CD"];
                         var TEC = Request["TEC"];
                         var Act = Request["Act"];
                         var Prog = Request["Prog"];

                        var paramList = new List<ReportParameter>();
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
                        var UnableEquipmentActNumber = Request["UnableEquipmentActNumber"];
                        parm = new[]
                            {
                                 new ReportParameter("UnableEquipmentActNumber", UnableEquipmentActNumber)
                            };
                        report.ServerReport.SetParameters(parm);
                    }
                    else if (reportName == "Tabla AmortizacionTemp")
                    {
                        var rpu = Request["RPU"];
                        parm = new []
                            {
                                 new ReportParameter("RPU", rpu)
                            };
                        report.ServerReport.SetParameters(parm);
                    }
                    else if (reportName == "Acuse_Recibo")
                    {
                        string creditNumber = Request["CreditNumber"];
                        parm = new[]
                            {
                                 new ReportParameter("No_Credito", creditNumber)
                            };
                        report.ServerReport.SetParameters(parm);
                    }
                    else if (reportName != "Equipo Baja Eficiencia") // Reports of Validate Credit History , Report "Equipo Baja Eficiencia" have no parameter
                    {                        
                        string creditNumber = Request["CreditNumber"];
                        parm = new []
                            {
                                 new ReportParameter("No_Credito", creditNumber)
                            };
                        report.ServerReport.SetParameters(parm);
                    }
                    
                    report.ServerReport.Refresh();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class BasicReportCredentials : IReportServerCredentials
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
