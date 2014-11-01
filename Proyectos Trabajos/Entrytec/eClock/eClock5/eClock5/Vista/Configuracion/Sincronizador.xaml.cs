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

using Newtonsoft.Json;
using eClock5.BaseModificada;
using eClock5;
using eClockBase;

namespace eClock5.Vista.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Sincronizador.xaml
    /// </summary>
    public partial class Sincronizador : UserControl
    {
        public Sincronizador()
        {
            InitializeComponent();
        }
        Modelos.eClockSync eClockSyncModelo;


        private void UC_ToolBar_OnEventClickToolBar(UC_ToolBar_Control Control)
        {
            switch (Control.Nombre)
            {
                case "Btn_Guardar":
                    Guardar();
                    break;    
                case "Btn_Sincronizar":

                    break;
            }
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            eClockSyncModelo = Modelos.eClockSync.Carga(); 
            Chb_EjecutarSync.IsChecked = eClockSyncModelo.Ejecutar;
            Cbx_Sitios_ID.SeleccionadoInt = eClockBase.CeC.Convierte2Int(eClockSyncModelo.Sitios);
            Tbx_Usuario_Sync.Text = eClockSyncModelo.Usuario;
            Tbx_ProxyURLSync.Text = eClockSyncModelo.Proxy_URL;
            Tbx_ProxyUSR.Text = eClockSyncModelo.Proxy_Usuario;
            Tbx_ProxyPwdSync.Password = eClockSyncModelo.Password;
            Tbx_ProxyPwdSync_Cnf.Password = eClockSyncModelo.Password;
            Tbx_Sincronizador.Text = eClockSyncModelo.RutaApp;      
        }



        private void Guardar()
        {
            //eClockSyncModelo = new Modelos.eClockSync();
            eClockSyncModelo.Ejecutar =  eClockBase.CeC.Convierte2Bool(Chb_EjecutarSync.IsChecked);
            eClockSyncModelo.Sitios = Cbx_Sitios_ID.SeleccionadoString;
            eClockSyncModelo.Usuario = Tbx_Usuario_Sync.Text;
            eClockSyncModelo.Proxy_URL = Tbx_ProxyURLSync.Text;
            eClockSyncModelo.Proxy_Usuario = Tbx_ProxyUSR.Text;
            eClockSyncModelo.Password = Tbx_ProxyPwdSync.Password;
            eClockSyncModelo.Password = Tbx_ProxyPwdSync_Cnf.Password;
            eClockSyncModelo.Proxy_Clave = Tbx_ProxyPwdSync.Password;
            eClockSyncModelo.RutaApp = Tbx_Sincronizador.Text;
            eClockSyncModelo.Guarda();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Tbx_ProxyPwdSync.Password != Tbx_ProxyPwdSync_Cnf.Password)
                Lbl_ProxyPwdSync_NoCoincide.Visibility = System.Windows.Visibility.Visible;
            else
                Lbl_ProxyPwdSync_NoCoincide.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Btn_Activar_Click(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
