using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !NETFX_CORE
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
#endif

namespace eClock5.Vista.Wizard
{
    /// <summary>
    /// Lógica de interacción para Licencia.xaml
    /// </summary>
    public partial class Licencia : UserControl
    {
        public string Llave = "";
        public Licencia()
        {
            InitializeComponent();
            Loaded += Licencia_Loaded;
        }
        void ActualizaBotones()
        {

            //Lbl_NoValida.Visibility
            Lbl_EscribaSuClave.IsEnabled = Lbl_NoValida.IsEnabled = Tbx_Licencia.IsEnabled = Chb_NoTengoClave.IsChecked == true ? false : true;
            if (Lbl_NoValida.IsEnabled)
                Lbl_NoValida.Visibility = System.Windows.Visibility.Visible;
            else
                Lbl_NoValida.Visibility = System.Windows.Visibility.Collapsed;
            Controles.UC_Wizard.sMostrarBotones(false, Chb_NoTengoClave.IsChecked == true ? true : false || ClaveValida(), true);
            if (Chb_NoTengoClave.IsChecked == true)
                Llave = "";
        }
        void Licencia_Loaded(object sender, RoutedEventArgs e)
        {
            ActualizaBotones();
        }
        public bool ClaveValida()
        {
            Lbl_NoValida.Visibility = System.Windows.Visibility.Visible;
            if (Tbx_Licencia.Text.Length <= 20)
                return false;
            Llave = Tbx_Licencia.Text.Replace("-", "");
            string Licencia = Llave.Substring(0, 16);
            byte[] Arreglo = eClockBase.CeC.ObtenArregloBytes(Licencia);
            int Chk = 0;
            foreach (byte Caracter in Arreglo)
            {
                Chk = Chk + Caracter * 7 + 52;
            }
            Chk = Chk % 100;
            string sChk = Llave.Substring(Llave.Length - 2, 2);
            if (Chk.ToString() != sChk)
                return false;

            Lbl_NoValida.Visibility = System.Windows.Visibility.Hidden;
            return true;
        }

        private void Tbx_Licencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualizaBotones();
        }

        private void Chb_NoTengoClave_Checked(object sender, RoutedEventArgs e)
        {
            ActualizaBotones();
        }

        private void Chb_NoTengoClave_Unchecked(object sender, RoutedEventArgs e)
        {
            ActualizaBotones();
        }
    }
}
