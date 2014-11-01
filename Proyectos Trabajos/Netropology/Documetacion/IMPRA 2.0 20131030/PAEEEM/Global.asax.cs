using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using PAEEEM.Helpers;
using System.IO;
using System.Xml;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Data;
using PAEEEM.DataAccessLayer;
using System.Net.Mail;
using System.Web.Configuration;
using System.Net.Configuration;

namespace PAEEEM
{
    public class Global : System.Web.HttpApplication
    {
        //Added by Jerry 2011/08/08
        public const int PROGRAM = 1;//Identify current program id

        private const string _config_name = "LsWeb.config";
        private const string _elementapplication = "application";
        private const string _elementapp = "configuration/application";
        private const string _serverattribute = "server";
        private const string _databaseattribute = "database";
        private const string _userattribute = "user";
        private const string _passwordattribute = "password";
        private const string _timeoutattribute = "timeout";
        private const string _userpathattribute = "user_path";
        private const string _attributename = "name";
        private const string _attributeversion = "version";
        private const string _linkValue = "value";
        private const string _mailSubject = "subject";
        private const string _reportserverurl = "url";
        private const string _reportfoldername = "name";
        private const string _debugmode = "value";
        private static System.Timers.Timer autoEmailTimer = null;//used for auto-email functions
        private static System.Timers.Timer autoCancelTimer = null;//used to cancel credit automatically
        //Added by Jerry 2012/04/12
        private static System.Timers.Timer notificationTimer = null;
        private static string connString = string.Empty;
        private static string configPath = HttpContext.Current.Request.ApplicationPath;

        protected void LoadAppConfig()
        {
            try
            {
                string configname = GetConfigFile();
                using (StreamReader sr = new StreamReader(configname))
                {
                    string configxml = sr.ReadToEnd();
                    sr.Close();

                    XmlDocument document = new XmlDocument();
                    document.LoadXml(configxml);
                    XmlNodeList nodes = document.GetElementsByTagName(_elementapplication);
                    XmlNode appnode = document.SelectSingleNode(_elementapp);
                    LsApplicationState ApplicationState = new LsApplicationState(HttpContext.Current.Application);
                    ApplicationState.AppName = appnode.Attributes.GetNamedItem(_attributename).Value;
                    ApplicationState.AppVersion = appnode.Attributes.GetNamedItem(_attributeversion).Value;
                    MailHostModle mailModel = new MailHostModle(HttpContext.Current.Application);
                    if (nodes.Count > 0)
                    {
                        //parse
                        foreach (XmlNode node in nodes)
                        {
                            if (node.HasChildNodes)
                            {
                                foreach (XmlNode n in node.ChildNodes)
                                {
                                    switch (n.Name)
                                    {
                                        case "database":
                                            ApplicationState.SQLConnString = "Data Source = " + n.Attributes[_serverattribute].Value + "; " +
                                                                                                        "Initial Catalog = " + n.Attributes[_databaseattribute].Value + "; " +
                                                                                                        "User Id = " + n.Attributes[_userattribute].Value + "; " +
                                                                                                        "Password = " + n.Attributes[_passwordattribute].Value;

                                            connString = ApplicationState.SQLConnString;//Added by Jerry 2012/04/13
                                            break;
                                        case "logging":
                                            ApplicationState.ErrorLogPath = n.Attributes[_userpathattribute].Value.ToString();//, DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString()+".txt");
                                            break;
                                        case "link":
                                            ApplicationState.Link = n.Attributes[_linkValue].Value;
                                            break;
                                        case "activemail":
                                            ApplicationState.MailActive = n.Attributes[_mailSubject].Value;
                                            break;
                                        case "notactivemail":
                                            ApplicationState.MailNotActive = n.Attributes[_mailSubject].Value;
                                            break;
                                        case "forgetpwdmail":
                                            ApplicationState.MailForgetPwd = n.Attributes[_mailSubject].Value;
                                            break;
                                        case "reportserverurl":
                                            ApplicationState.ReportServerURL = n.Attributes[_reportserverurl].Value;
                                            break;
                                        case "reportfolder":
                                            ApplicationState.ReportFolder = n.Attributes[_reportfoldername].Value;
                                            break;
                                        case "debugmode":
                                            ApplicationState.DebugMode = n.Attributes[_debugmode].Value;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            // else: No database and error log configuration
                        }
                    }
                    //else: nothing
                }
            }
            catch (Exception ex)
            {
                LsLog.LogToFile(ex.Message);
            }
        }

        private string GetConfigFile()
        {
            return Path.Combine(Server.MapPath("~"), _config_name);
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            LoadAppConfig();
            // RSA 20130619 These are not needed anymore (Credit auto cancel and its notification)
            //autoEmailTimer = new System.Timers.Timer();
            //autoEmailTimer.AutoReset = true;
            //autoEmailTimer.Interval = 43200000;//12 hours
            //autoEmailTimer.Elapsed += new System.Timers.ElapsedEventHandler(autoEmailTimer_Elapsed);
            //autoEmailTimer.Start();
            ////auto-cancel credit timer
            //autoCancelTimer = new System.Timers.Timer();
            //autoCancelTimer.AutoReset = true;
            //autoCancelTimer.Interval = 43200000;//12 hours
            //autoCancelTimer.Elapsed += new System.Timers.ElapsedEventHandler(autoCancelTimer_Elapsed);
            //autoCancelTimer.Start();
            ////Added by Jerry 2012/04/12
            //notificationTimer = new System.Timers.Timer();
            //notificationTimer.AutoReset = true;
            //notificationTimer.Interval = 3600000;//1 hour
            //notificationTimer.Elapsed += new System.Timers.ElapsedEventHandler(notificationTimer_Elapsed);
            //notificationTimer.Start();
        }

        void notificationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<PAEEEM.Entities.ScheduledNotificationEntity> ScheduledNotificationCollection = ScheduledNotificationDal.ClassInstance.GetScheduledNotifications(connString);
            foreach (PAEEEM.Entities.ScheduledNotificationEntity ScheduledNotificationEntity in ScheduledNotificationCollection)
            {
                TimeSpan DayDiff = default(TimeSpan);
                if (ScheduledNotificationEntity.LastSent != null)
                {
                    DayDiff = DateTime.Parse(ScheduledNotificationEntity.LastSent.Value.ToShortDateString()) - DateTime.Parse(ScheduledNotificationEntity.CreateDate.ToShortDateString());
                }

                ScheduledNotificationEntity.Body = ScheduledNotificationEntity.Body.Replace(GlobalVar.REPLACED_DAY_OF_NUMBER, (3 - DayDiff.Days).ToString());
                ScheduledNotificationEntity.Body = ScheduledNotificationEntity.Body.Replace(GlobalVar.REPLACED_CREDIT_NUMBER, ScheduledNotificationEntity.CreditNumber);
                ScheduledNotificationEntity.Body = ScheduledNotificationEntity.Body.Replace(GlobalVar.REPLACED_OLD_EQUIPMENT_NUMBER, ScheduledNotificationEntity.FolioID);
                // SendNotificationEmail(ScheduledNotificationEntity.ToEmail, ScheduledNotificationEntity.CCEmail, ScheduledNotificationEntity.Subject, ScheduledNotificationEntity.Body);

                //Update LastSent time
                ScheduledNotificationDal.ClassInstance.UpdateLastSentTime(ScheduledNotificationEntity.NotificationId, connString);
            }
        }

        private static void SendNotificationEmail(string toAddress, string ccAddress, string subject, string body)
        {
            if (!string.IsNullOrEmpty(toAddress))
            {
                string From = "", UserName = "", Password = "";

                Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration(configPath);

                MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

                if (mailSettings != null)
                {
                    Password = mailSettings.Smtp.Network.Password;
                    UserName = mailSettings.Smtp.Network.UserName;
                    From = mailSettings.Smtp.From;
                }
                
                MailMessage Mail = new MailMessage(From, toAddress, subject, body);
                string[] ccAddressArray = ccAddress.Split(new char[] { ';' });
                foreach (string ccEmailAddress in ccAddressArray)
                {
                    MailAddress mailCCAdrress = new MailAddress(ccEmailAddress);
                    Mail.CC.Add(mailCCAdrress);
                }

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential(UserName, Password);
                client.SendAsync(Mail, string.Empty);
            }
        }

        void autoCancelTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DataTable dtScheduleJobsWithCancel = ScheduleJobsDal.ClassInstance.GetScheduleJobsWithCanceled(connString);

            foreach (DataRow row in dtScheduleJobsWithCancel.Rows)
            {
                int iCount = 0;
                //Changed by Jerry 2012/04/13
                iCount = K_CREDITODal.ClassInstance.CancelCredit(row["Credit_No"].ToString(), (int)CreditStatus.CANCELADO, DateTime.Now.Date, "System", DateTime.Now.Date, connString);
                if (iCount > 0)
                {
                    ScheduleJobsDal.ClassInstance.CanceledScheduleJob(row["Credit_No"].ToString(), connString);//Changed by Jerry 2012/04/13
                }
            }
        }

        void autoEmailTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DataTable dtScheduleJobs = ScheduleJobsDal.ClassInstance.GetScheduleJobsWithDateandStatus(connString);
            //Changed by Jerry 2012/04/13
            string From = "", UserName = "", Password = "";
            Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration(configPath);

            MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            if (mailSettings != null)
            {
                Password = mailSettings.Smtp.Network.Password;
                UserName = mailSettings.Smtp.Network.UserName;
                From = mailSettings.Smtp.From;
            }

            foreach (DataRow row in dtScheduleJobs.Rows)
            {
                MailMessage mailMessage = new MailMessage(From, row["Supplier_Email"].ToString());
                mailMessage.Subject = row["Email_Title"].ToString();
                mailMessage.Body = row["Email_Body"].ToString();

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential(UserName, Password);
                client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
                client.SendAsync(mailMessage, row["Credit_No"].ToString());
            }
        }

        void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //throw new SmtpException("Warning Email Send Error.");
                LsLog.LogToFile("Warning Email Send Error: " + e.Error.Message);
                return;
            }
            //do related update logic
            string creditNum = e.UserState.ToString();//Changed by Jerry 2011/08/12
            ScheduleJobsDal.ClassInstance.ProcessScheduleJob(creditNum);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //: Initialize session associated variables            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            if (Context.Session != null && Context.Session["UserName"] != null)
            {
                string sKey = (string)Session["UserName"];
                // Accessing the Cache Item extends the Sliding Expiration automatically
                string sUser = (string)HttpContext.Current.Cache[sKey];
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                if (ex.GetType() == typeof(HttpException))
                {
                    string Message = "";
                    Message += "Unhandled Exceiption Occured:/r/n"
                                        + "The method name: " + ex.TargetSite.Name + "/r/n"
                                        + "The class name: " + ex.TargetSite.DeclaringType.FullName + "/r/n"
                                        + "The stack trace: " + ex.StackTrace;

                    LsLog.LogToFile(Message);
                }
                Server.ClearError();
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            if (Session["UserName"] != null && Session["UserName"].ToString() != string.Empty)
            {
                HttpContext.Current.Cache.Remove(Session["UserName"].ToString());
            }
            Session["UserName"] = null;
            Session.Clear();
            Session.Abandon();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            autoEmailTimer.Stop();
            autoCancelTimer.Stop();
            notificationTimer.Stop();
        }
    }
}
