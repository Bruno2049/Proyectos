using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class Demos_ExceptionHandling : BasePage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    MailMessage myMessage = new MailMessage();
    myMessage.Subject = "Exception Handling Test";
    myMessage.Body = "Test message body";

    try
    {
      myMessage.From = new MailAddress("you@example.com");
      myMessage.To.Add(new MailAddress("you@example.com"));

      SmtpClient mySmtpClient = new SmtpClient();
      mySmtpClient.Send(myMessage);
      Message.Text = "Message sent";
    }
    catch (SmtpException)
    {
      Message.Text = "An error occurred while sending your e-mail. Please try again.";
    }
  }
}