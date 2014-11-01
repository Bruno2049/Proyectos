using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;

namespace PAEEEM.Captcha
{
    /// <summary>
    /// Summary description for Letter
    /// </summary>
    public class Letter
    {
        string[] ValidFonts = { "Segoe Script", "Century", "Eccentric Std", "Freestyle Script", "Viner Hand ITC" };
        static Random rnd = new Random();

        public Letter(char c)
        {
            font = new Font(ValidFonts[rnd.Next(ValidFonts.Count())], rnd.Next(20) + 20, GraphicsUnit.Pixel);
            letter = c;
        }
        public Font font
        {
            get;
            private set;
        }
        public Size LetterSize
        {
            get
            {
                var Bmp = new Bitmap(1, 1);
                var Grph = Graphics.FromImage(Bmp);
                return Grph.MeasureString(letter.ToString(), font).ToSize();
            }
        }
        public char letter
        {
            get;
            private set;
        }
        public int space
        {
            get;
            set;
        }
    }
}