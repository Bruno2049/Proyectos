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

        public MailUtility()
        {}

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

        public static void RegisterEmail(string username, string address, string password)
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

        public static void NuevoMailRpuDist(string usuarioDist, string usuarioJefeZona, string noCredito, string correoZona)
        {
            var cuerpo = GetEmailTemplateBody("MensajeRPUDis.html");
            cuerpo = cuerpo.Replace("#USUARIO_ZONA#", usuarioJefeZona);
            cuerpo = cuerpo.Replace("#USUARIO_DIST#", usuarioDist);
            cuerpo = cuerpo.Replace("#NO_CREDITO#", noCredito);

            var settings = GetMailSettings();

            var mail = new MailMessage(settings[4], correoZona)
            {
                Subject = AppState.MailNotActive,
                Body = cuerpo,
                IsBodyHtml = true
            };

            SendEmail(mail, null, settings[3], settings[2]);
        }

        public static void MailValidacionRFC(string Template, string Usuario_Dist, string Usuario_Jefe_Zona, string RFC, string Correo, string Motivos_rechazo)
        {
            string Cuerpo = GetEmailTemplateBody(Template);
            Cuerpo = Cuerpo.Replace("#USUARIO_ZONA#", Usuario_Jefe_Zona);
            Cuerpo = Cuerpo.Replace("#USUARIO_DIST#", Usuario_Dist);
            Cuerpo = Cuerpo.Replace("#MOTIVOS_RECHAZO#", Motivos_rechazo);
            Cuerpo = Cuerpo.Replace("#RFC#", RFC);

            string[] settings = GetMailSettings();

            var mail = new MailMessage(settings[4], Correo);
            mail.Subject = AppState.MailNotActive;
            mail.Body = Cuerpo;
            mail.IsBodyHtml = true;

            SendEmail(mail, null, settings[3], settings[2]);
        }

        public static void MailNuevoRpu(string template,string usuarioDist, string usuarioJefeZona,string nombreZona, string noCredito, string enviarAlCorreo)
        {
            var cuerpo = GetEmailTemplateBody(template);
            cuerpo = cuerpo.Replace("#NOMBRE_ZONA#", nombreZona);
            cuerpo = cuerpo.Replace("#USUARIO_ZONA#", usuarioJefeZona);
            cuerpo = cuerpo.Replace("#USUARIO_DIST#", usuarioDist);
            cuerpo = cuerpo.Replace("#NO_CREDITO#", noCredito);

            var settings = GetMailSettings();

            var mail = new MailMessage(settings[4], enviarAlCorreo)
            {
                Subject = AppState.MailNotActive,
                Body = cuerpo,
                IsBodyHtml = true
            };

            SendEmail(mail, null, settings[3], settings[2]);
        }

        public static void MailRpuNoCoincide(string template, string usuarioDist, string usuarioJefeZona, string nombreZona, string noCredito, string enviarAlCorreo)
        {
            var cuerpo = GetEmailTemplateBody(template);
            //cuerpo = cuerpo.Replace("#NOMBRE_ZONA#", nombreZona);
            //cuerpo = cuerpo.Replace("#USUARIO_ZONA#", usuarioJefeZona);
            cuerpo = cuerpo.Replace("#USUARIO_DIST#", usuarioDist);
            cuerpo = cuerpo.Replace("#NO_CREDITO#", noCredito);

            var settings = GetMailSettings();

            var mail = new MailMessage(settings[4], enviarAlCorreo)
            {
                Subject = AppState.MailNotActive,
                Body = cuerpo,
                IsBodyHtml = true
            };

            SendEmail(mail, null, settings[3], settings[2]);
        }

        public void ValiacionRpuSicom(string template, string usuarioDist, string noCredito, string correoDist, string rpuAnterior, string rpuNuevo)
        {
            var cuerpo = GetEmailTemplateBody(template);
            cuerpo = cuerpo.Replace("#USUARIO_DIST#", usuarioDist);
            cuerpo = cuerpo.Replace("#NO_CREDITO#", noCredito);
            cuerpo = cuerpo.Replace("#RPU_ANTERIOR#", rpuAnterior);
            cuerpo = cuerpo.Replace("#RPU_NUEVO#", rpuNuevo);

            string[] settings = GetMailSettings();

            var mail = new MailMessage(settings[4], correoDist)
                {
                    Subject = "Cambio de Tarifa Crédito: " + noCredito,
                    Body = cuerpo,
                    IsBodyHtml = true
                };

            SendEmail(mail, null, settings[3], settings[2]);
        }

        private static void SendEmail(MailMessage message, string usertoken, string username, string password)
        {
            var settings = GetMailSettings();
            
            var client = new SmtpClient();
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Host = settings[1];
            client.Port = int.Parse(settings[0]);
            client.Credentials = new NetworkCredential(username, password);
            
            client.Send(message);
            client.Dispose();

            //try
            //{
            
            //client.SendCompleted += new SendCompletedEventHandler(Client_SendCompleted);

            //client.SendAsync(message, usertoken);
            
            //}
            //catch (Exception ex)
            //{
                //LsLog.LogToFile(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                //client.Dispose();
                //throw ex;

            //}
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



        ////add by @l3x////
        public static void MotivoCanceAdistribuidor(string template,string address, string nomusu, string noCredito, string rpu, string nomRazon, string motivo)
        {
            var cuerpo = GetEmailTemplateBody(template);
            cuerpo = cuerpo.Replace("#USUARIO_DIST#", nomusu);
            cuerpo = cuerpo.Replace("#NOSOLICITUD#", noCredito);
            cuerpo = cuerpo.Replace("#RPU#", rpu);
            cuerpo = cuerpo.Replace("#NOMBREORAZONSOCIAL#", nomRazon);
            cuerpo = cuerpo.Replace("#MOTIVO#", motivo);

            string[] settings = GetMailSettings();

            var mail = new MailMessage(settings[4], address)
            {
                Subject = "Credito cancelado: " + noCredito,
                Body = cuerpo,
                IsBodyHtml = true
            };

            SendEmail(mail, null, settings[3], settings[2]);
        }

        public static void ProductModifcado(string template,string address, string fecha, string repre, string marca,string modelo)
        {
            var cuerpo = GetEmailTemplateBody(template);
            cuerpo = cuerpo.Replace("#FECHA#", fecha);
            cuerpo = cuerpo.Replace("#REPRESENTANTE#", repre);
            cuerpo = cuerpo.Replace("#MARCA#", marca);
            cuerpo = cuerpo.Replace("#MODELO#", modelo);
            string[] settings = GetMailSettings();
            
            var mail = new MailMessage(settings[4], address)
            {
                Subject = "Pruebas del Modulo 4 Producto inhabilitado: " + marca + " " + modelo,
                Body = cuerpo,
                IsBodyHtml = true
            };

            SendEmail(mail, null, settings[3], settings[2]);
        }

        ////<---------////


        public static void MetasMontoTotal(string template, string address, string fecha, string tipoMonto, string monto )
        {
            var cuerpo = GetEmailTemplateBody(template);
            cuerpo = cuerpo.Replace("#FECHA#", fecha);
            cuerpo = cuerpo.Replace("#TIPO_MONTO#", tipoMonto);
            cuerpo = cuerpo.Replace("#MONTO#", monto);

            string[] settings = GetMailSettings();

            var mail = new MailMessage(settings[4], address)
            {
                Subject = "Recursos del programa se encuentran comprometidos",
                Body = cuerpo,
                IsBodyHtml = true
            };

            SendEmail(mail, null, settings[3], settings[2]);
        }
    }
}
