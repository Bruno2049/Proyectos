using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;


namespace PAEEEM.Helpers
{
    public class MailUtility
    {
        static LsApplicationState AppState = new LsApplicationState(HttpContext.Current.Application);
        //public static event SendCompletedEventHandler SendCompleted;

        private static string GetEmailTemplateBody(string templatename)
        {
            string TempaltePath = AppDomain.CurrentDomain.BaseDirectory + @"EmailTemplate";
            string FilePath = Path.Combine(TempaltePath, templatename);
            string Result=string.Empty;

            try
            {
                if (File.Exists(FilePath))
                {
                    Result = File.ReadAllText(FilePath);
                }
            }
            catch (Exception ex)
            {
                LsLog.LogToFile(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
            return Result;
        }

        public static void RegisterEmail(string username, string address,  string password)
        {
            if (!string.IsNullOrEmpty(address))
            {
                string Body = GetEmailTemplateBody("ActivateTemplate.html");
                Body = Body.Replace("#USERNAME#", username);
                Body = Body.Replace("#EMAIL#", address);
                Body = Body.Replace("#PASSWORD#", password);
                Body = Body.Replace("#LINK# ", AppState.Link);

                string[] settings = GetMailSettings();
                string From = settings[4];
                MailMessage Mail = new MailMessage(From, address);

                Mail.Subject = AppState.MailActive;
                Mail.Body = Body;
                Mail.IsBodyHtml = true;

                SendEmail(Mail, "Register", settings[3], settings[2]);
            }
        }

        public static void PasswordChangeEmail(string username,  string address, string password)
        {
            if (!string.IsNullOrEmpty(address))
            {
                string Body = GetEmailTemplateBody("ForgetPasswordTemplate.html");
                Body = Body.Replace("#USERNAME#", username);
                Body = Body.Replace("#PASSWORD#", password);
                string[] settings = GetMailSettings();
                string From = settings[4];
                
                MailMessage Mail = new MailMessage(From, address);
                Mail.Subject = AppState.MailNotActive;
                Mail.Body = Body;
                Mail.IsBodyHtml = true;

                SendEmail(Mail, "PasswordChanged", settings[3], settings[2]);
            }
        }

        private static string[] GetMailSettings()
        {
            string[] settingsArray = new string[5] { "","","","",""};
            string ConfigPath = HttpContext.Current.Request.ApplicationPath;
            Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration(ConfigPath);

            MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            if (mailSettings != null)
            {
                string port = mailSettings.Smtp.Network.Port.ToString();
                string host = mailSettings.Smtp.Network.Host;
                string password = mailSettings.Smtp.Network.Password;
                string username = mailSettings.Smtp.Network.UserName;
                string address = mailSettings.Smtp.From;

                settingsArray.SetValue(port, 0);
                settingsArray.SetValue(host, 1);
                settingsArray.SetValue(password, 2);
                settingsArray.SetValue(username, 3);
                settingsArray.SetValue(address, 4);
            }

            return settingsArray;
        }

        public static void StatusChangeEmail(string username, string address, string status)
        {
            if (!string.IsNullOrEmpty(address))
            {
                string Body = GetEmailTemplateBody("De-activateTemplate.html");
                Body = Body.Replace("#USERNAME#", username);
                Body = Body.Replace("#STATUS#", status);
                string[] settings = GetMailSettings();
                string From = settings[4];
                MailMessage Mail = new MailMessage(From, address);

                Mail.Subject = AppState.MailNotActive;
                Mail.Body = Body;
                Mail.IsBodyHtml = true;

                SendEmail(Mail, status, settings[3], settings[2]);
            }
        }

        private static void SendEmail(MailMessage message, string usertoken, string username, string password)
        {
            try
            {
                SmtpClient Client = new SmtpClient();
                Client.EnableSsl = false;
                Client.UseDefaultCredentials = true;
                Client.Credentials = new System.Net.NetworkCredential(username, password);
                Client.SendCompleted += new SendCompletedEventHandler(Client_SendCompleted);

                Client.SendAsync(message, usertoken);
            }
            catch (Exception ex)
            {
                LsLog.LogToFile(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        static void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (e.UserState.ToString() != "Register" && e.UserState.ToString() != "PasswordChanged")
                {
                    //OnSendCompleted(sender, e);
                }
            }
        }

        //protected static void OnSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    if (sender != null)
        //    {
        //        //SendCompleted(sender, e);
        //    }
        //}
    }
}
