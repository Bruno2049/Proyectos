/*

Scrambled text generator.
Developed by: Aref Karimi
Email: Arefkr@gmail.com
Last update: 20 October 2009

This code is free to use. 
 
*/
using System;
using System.Web;
using System.Collections.Generic;
using System.Drawing;

namespace PAEEEM.Captcha
{
    public class GetImgText : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";
            var CaptchaText = SecurityHelper.DecryptString(
                Convert.FromBase64String(context.Request.QueryString["CaptchaText"]));
            var rnd = new Random();
            if (CaptchaText != null)
            {
                List<Letter> letter = new List<Letter>();
                int TotalWidth = 0;
                int MaxHeight = 0;
                foreach (char c in CaptchaText)
                {
                    var ltr = new Letter(c);
                    letter.Add(ltr);
                    int space = rnd.Next(5) + 1;
                    ltr.space = space;
                    TotalWidth += ltr.LetterSize.Width + space;
                    if (MaxHeight < ltr.LetterSize.Height)
                        MaxHeight = ltr.LetterSize.Height;
                }
                const int HMargin = 5;
                const int VMargin = 3;

                Bitmap bmp = new Bitmap(TotalWidth + HMargin, MaxHeight + VMargin);
                var Grph = Graphics.FromImage(bmp);
                Grph.FillRectangle(new SolidBrush(Color.Lavender), 0, 0, bmp.Width, bmp.Height);
                Pixelate(ref bmp);
                Grph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                Grph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                int xPos = HMargin;
                foreach (var ltr in letter)
                {
                    Grph.DrawString(ltr.letter.ToString(), ltr.font, new SolidBrush(Color.Navy), xPos, VMargin);
                    xPos += ltr.LetterSize.Width + ltr.space;
                }

                bmp.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private void Pixelate(ref Bitmap bmp)
        {
            Color[] Colors = { Color.Gray, Color.Red, Color.Blue, Color.Olive };
            var grp = Graphics.FromImage(bmp);
            Image background = Image.FromFile(HttpContext.Current.Server.MapPath("images/captcha1.png"));
            grp.DrawImage(background, new Rectangle(0, 0, bmp.Width, bmp.Height));
        }
    }
}