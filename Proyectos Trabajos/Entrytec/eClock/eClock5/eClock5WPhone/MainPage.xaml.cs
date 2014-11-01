using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace eClock5WPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                eClockBase.ES_Sesion.S_SesionClient Cliente = new eClockBase.ES_Sesion.S_SesionClient(eClockBase.ES_Sesion.S_SesionClient.EndpointConfiguration.BasicHttpBinding_S_Sesion,
                    "http://192.168.17.125:50723/S_Sesion.svc");
                Cliente.CreaSesionCompleted += Cliente_CreaSesionCompleted;
                //Cliente.DoWorkAsync();
                Cliente.CreaSesionAsync("admin", "admin");
                Boton1.Content = "Inicio";
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
        }

        void Cliente_CreaSesionCompleted(object sender, eClockBase.ES_Sesion.CreaSesionCompletedEventArgs e)
        {
            //Boton1.Content = "Fin";
            Boton1.Content = e.Result;
        }
    }
}