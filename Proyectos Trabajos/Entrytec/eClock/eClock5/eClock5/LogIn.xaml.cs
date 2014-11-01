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
using eClockBase;

namespace eClock5
{
    /// <summary>
    /// Lógica de interacción para LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        eClockBase.CeC_SesionBase Sesion;
        public LogIn()
        {
            InitializeComponent();
            Loaded += LogIn_Loaded;
        }
         
        void LogIn_Loaded(object sender, RoutedEventArgs e)
        {
            eClock.AsignaIcono(this);
            Grid_Error.Visibility = System.Windows.Visibility.Collapsed;
            BaseModificada.Localizaciones.sLocaliza(this);
            Sesion = CeC_Sesion.ObtenSesion(this);
            Btn_Maximisar.Visibility = Visibility.Hidden;
            if (!Sesion.MantenerSesion) 
                Chk_Recordar.IsChecked = false;
            else
                Chk_Recordar.IsChecked = true;
            Tbx_Usuario.Focus();
        }


        private void Btn_LogIn_Click(object sender, RoutedEventArgs e)
        {
            Grid_Error.Visibility = Visibility.Hidden;
            

            eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
            
            CSesion.LogeoFinalizado += CSesion_LogeoFinalizado;
            /*CSesion.CreaSesionCompleted += delegate(object CSsender, eClockBase.ES_Sesion.CreaSesionCompletedEventArgs CSe)
            {
                CeC_Log.AgregaLog("CSesion.CreaSesion_Completado " );

                if (!Sesion.EstaLogeado())
                    Grid_Error.Visibility = Visibility.Visible;

            };
            
            CSesion.CreaSesion_Inicio(Tbx_Usuario.Text, Tbx_Clave.Password, (bool)Chk_Recordar.IsChecked);
            */
            
            CSesion.CreaSesion_InicioAdv(Tbx_Usuario.Text, Tbx_Clave.Password, (bool)Chk_Recordar.IsChecked);
        }

        void CSesion_LogeoFinalizado(object sender, EventArgs e)
        {
            if (!Sesion.EstaLogeado())
                Grid_Error.Visibility = Visibility.Visible;
            else
                this.Close();
            /*eClock Dlg = new eClock();
            Dlg.ShowDialog();*/
            //throw new NotImplementedException();
        }

        private void Btn_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Btn_Maximisar_Click(object sender, RoutedEventArgs e)
        {
            Btn_Mini_Pantalla.Visibility = Visibility.Visible;
            Btn_Maximisar.Visibility = Visibility.Hidden;
            this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void Btn_Mini_Pantalla_Click(object sender, RoutedEventArgs e)
        {
            Btn_Maximisar.Visibility = Visibility.Visible;
            Btn_Mini_Pantalla.Visibility = Visibility.Hidden;
            this.WindowState = System.Windows.WindowState.Normal;
        }

        private void Rectangle_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                Btn_Maximisar.Visibility = Visibility.Hidden;
                Btn_Mini_Pantalla.Visibility = Visibility.Visible;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Vista.Wizard.Instalacion Dlg = new Vista.Wizard.Instalacion();
            if (Dlg.ShowDialog() != true)
            {
                this.Close();
            }
        }








    }
}
