using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace eClockMobile.BaseModificada
{
    public class CeC : eClockBase.CeC
    {
        public static System.Drawing.Bitmap Byte2Imagen(byte[] ArregloImagen)
        {
            try
            {
                System.IO.MemoryStream MS = new System.IO.MemoryStream(ArregloImagen);
                System.Drawing.Bitmap bmpStoredImage = new Bitmap(MS);
                return bmpStoredImage;
            }
            catch { }
            return null;
        }

        public static bool ThumbnailCallback()
        {
            return false;
        }

        public static byte[] Imagen2Thumbnail(byte[] Imagen, int Ancho, int Alto)
        {
            try
            {
                if (Imagen == null)
                    return null;
                MemoryStream stream = new MemoryStream();
                Bitmap ImagenOrigen = new System.Drawing.Bitmap(new MemoryStream(Imagen));

                int AnchoFinal = Ancho;
                int AltoFinal = ImagenOrigen.Height * Ancho / ImagenOrigen.Width;
                if (AltoFinal > Alto)
                {
                    AltoFinal = Alto;
                    AnchoFinal = ImagenOrigen.Width * Alto / ImagenOrigen.Height;
                }


                System.Drawing.Image.GetThumbnailImageAbort Callback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap Thumbnail = (Bitmap)ImagenOrigen.GetThumbnailImage(AnchoFinal, AltoFinal, Callback, IntPtr.Zero);

                Thumbnail.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] Datos = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(Datos, 0, (int)Datos.Length);
                return Datos;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return null;
        }

        public static string HtmlAgrega(string Url)
        {
            if (Url == null || Url == "")
                return "";
            return Url += ".html";
        }
        public static string HtmlQuita(string URLParametroID)
        {
            if (URLParametroID == null || URLParametroID == "")
                return "";
            return URLParametroID.Replace(".html", "");
        }

    }
}
