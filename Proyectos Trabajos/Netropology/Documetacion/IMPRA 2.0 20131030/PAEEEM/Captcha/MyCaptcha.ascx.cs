/*

Captcha asp.net control.
Developed by: Aref Karimi
Email: Arefkr@gmail.com
Last update: 20 October 2009

This code is free to use. 
 
*/
using System;
using System.Linq;

namespace PAEEEM.Captcha
{
    public partial class MyCaptcha : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GeneratedText == null)
                    TryNew();
            }
        }

        public void TryNew()
        {
            char[] Valichars = {'1','2','3','4','5','6','7','8','9','0','a','b','c','d','e','f','g','h','i',
                           'j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };
            Random rnd = new Random();
            string Captcha = "";
            int LetterCount = MaxLetterCount > 5 ? MaxLetterCount : 5;
            char newChar = (char)0;

            for (int i = 0; i < LetterCount; i++)
            {
                newChar = Char.ToUpper(Valichars[rnd.Next(Valichars.Count())]);
                Captcha += newChar;
            }
            GeneratedText = Captcha;

            ImgCaptcha.ImageUrl = "GetImgText.ashx?CaptchaText=" +
                Server.UrlEncode(Convert.ToBase64String(SecurityHelper.EncryptString(Captcha)));
            TxtCpatcha.Text = string.Empty;
        }
        public int MaxLetterCount { get; set; }
        private string GeneratedText
        {
            get
            {
                return ViewState[this.ClientID + "text"] != null ?
                    ViewState[this.ClientID + "text"].ToString() : null;
            }
            set
            {
                // Encrypt the value before storing it in viewstate.
                ViewState[this.ClientID + "text"] = value;
            }
        }
        public bool IsValid
        {
            get
            {
                bool result = GeneratedText.ToUpper() == TxtCpatcha.Text.Trim().ToUpper();
                if (!result)
                    TryNew();
                return result;
            }
        }
        protected void btnTryNewWords_Click(object sender, EventArgs e)
        {
            TryNew();
        }
    }
}
