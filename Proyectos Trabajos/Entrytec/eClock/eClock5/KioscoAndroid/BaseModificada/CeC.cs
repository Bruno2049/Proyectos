using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;

namespace KioscoAndroid.BaseModificada
{
    class CeC
    {
        public static BitmapImage Byte2Imagen(byte[] ArregloImagen)
        {
            try
            {
                System.IO.MemoryStream MS = new System.IO.MemoryStream(ArregloImagen);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = MS;
                bi.EndInit();
                return bi;
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
    }
}
