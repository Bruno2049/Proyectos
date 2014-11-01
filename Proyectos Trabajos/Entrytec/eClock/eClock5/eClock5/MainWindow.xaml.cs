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
using Newtonsoft.Json;
using eClockBase;
namespace eClock5
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void UC_Campos_Loaded_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
            
            eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
            CSesion.LogeoFinalizado += CSesion_LogeoFinalizado;
            CSesion.CreaSesionCompleted += delegate(object CSsender, eClockBase.ES_Sesion.CreaSesionCompletedEventArgs CSe)
            {
                CeC_Log.AgregaLog("CSesion.CreaSesion_Completado ");
                Tbk_Resultado.Text = Sesion.SESION_SEGURIDAD;

            };
            //CSesion.CreaSesion_Inicio("admin", "admin");
            //CSesion.CreaSesion_Inicio("EntryTec", "1");


            //eClockBase.CeC_SesionBase
            /*Cliente.CreaSesionAsync("admin", "admin");
            Cliente.CreaSesionCompleted += delegate (object CSsender, eClockBase.ES_Sesion.CreaSesionCompletedEventArgs CSe)
            {
                Cliente.
            };*/
            
            /*Cliente.ObtenListadoAsync("DQAAAA8AAAA=",CeC_Sesion
                Cliente*/
            


        }

        void CSesion_LogeoFinalizado(object sender, EventArgs e)
        {
            Dlg_Prueba PRueba = new Dlg_Prueba();
            PRueba.ShowDialog();
        }

        void Cliente_CreaSesionCompleted(object sender, eClockBase.ES_Sesion.CreaSesionCompletedEventArgs e)
        {

        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            eClockBase.CeC_LogDestino.StreamWriter.Close();
        }
    }
}
