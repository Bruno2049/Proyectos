using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Foto.xaml
    /// </summary>
    public partial class UC_Foto : UserControl
    {
        public UC_Foto()
        {
            InitializeComponent();
        }


        private bool m_Cambio = false;

        public bool Cambio
        {
            get { return m_Cambio; }
            set { m_Cambio = value; }
        }
        private byte[] m_ImagenBytes = null;
        public byte[] ImagenBytes
        {
            get
            {
                try
                {
                    if (Source == null)
                        return null;
                    if (m_ImagenBytes != null && !Cambio)
                        return m_ImagenBytes;
                    byte[] data;
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)Source));
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        encoder.Save(ms);
                        data = ms.ToArray();
                        return data;
                    }
                }
                catch (Exception ex)
                {
                    eClockBase.CeC_Log.AgregaError(ex);
                }
                return null;
            }
            set
            {
                try
                {
                    Source = eClock5.BaseModificada.CeC.Byte2Imagen(value);
                }
                catch (Exception ex)
                {
                    eClockBase.CeC_Log.AgregaError(ex);
                }
            }
        }
        public ImageSource Source
        {
            get
            {
                return Img_Foto.Source;
            }
            set
            {
                Img_Foto.Source = value;
                m_Cambio = false;
            }
        }

        private void UserControl_MouseDown_1(object sender, MouseButtonEventArgs e)
        {


        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Img_Foto.Source = null;
            m_Cambio = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.ShowDialog();

            Cambio = true;
            string ruta = fd.FileName.ToString();

            try
            {
                Img_Foto.Source = new BitmapImage(new Uri(fd.FileName));
                m_Cambio = true;
            }
            catch
            {
            }
        }


    }
}
