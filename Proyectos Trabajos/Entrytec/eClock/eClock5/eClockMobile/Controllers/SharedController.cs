using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eClockMobile.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/

        public ActionResult Index()
        {
            return View();
        }

        public static System.IO.MemoryStream ObtenColor(int iColor)
        {

            int B_MASK = 255;
            int G_MASK = 255 << 8; //65280 
            int R_MASK = 255 << 16; //16711680


            int r = (iColor & R_MASK) >> 16;
            int g = (iColor & G_MASK) >> 8;
            int b = iColor & B_MASK;
            System.Drawing.SolidBrush SB = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb((byte)r, (byte)g, (byte)b));
            int Ancho = 10;
            int Alto = 80;
            System.Drawing.Bitmap Bmp = new System.Drawing.Bitmap(Ancho, Alto);
            System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(Bmp);
            gfx.FillRectangle(SB, 0, 0, Ancho, Alto);
            System.IO.MemoryStream MS = new System.IO.MemoryStream();
            //Bmp = new System.Drawing.Bitmap(Ancho, Alto, gfx);
            Bmp.Save(MS, System.Drawing.Imaging.ImageFormat.Jpeg);
            MS.Seek(0, System.IO.SeekOrigin.Begin);
            return MS;
            //return File(MS, "image/jpeg", id);
        }

        [OutputCache(Duration = 3600000)]
        public FileResult Color(string id)
        {
            if (id == null || id == "")
                return null;

            int iColor = 0;
            id = System.IO.Path.GetFileNameWithoutExtension(id);
            iColor = eClockBase.CeC.Convierte2Int(id);
            return new FileStreamResult(ObtenColor(iColor), "image/jpeg");
            //return File(MS, "image/jpeg", id);
        }
    }
}
