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
using eClockBase;
namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_Direccion.xaml
    /// </summary>
    public partial class UC_Direccion : UserControl
    {
        private eClockBase.Modelos.Terminales.TerminalDir TerminalDir = new eClockBase.Modelos.Terminales.TerminalDir();

        public string Valor
        {
            get
            {
                Vista2TerminalDir();
                return TerminalDir.ObtenCadenaConexion();
            }
            set
            {                
                TerminalDir.CargarCadenaConexion(value);
                TerminalDir2Vista(); 
            }
        }

        public void TerminalDir2Vista()
        {
            switch (TerminalDir.TipoConexion)
            {
                case eClockBase.Modelos.Terminales.TerminalDir.tipo.Red:
                    Tbx_Red_Dir.Text = TerminalDir.Direccion;
                    Tbx_Red_Puerto.Text = TerminalDir.Puerto.ToString();             
                    Tbx_Red_ID.Text = TerminalDir.NoTerminal;
                    Tbx_Red_Clave.Text = TerminalDir.Clave;
                    Rbt_Red.IsChecked = true;
                    break;
                case eClockBase.Modelos.Terminales.TerminalDir.tipo.USB:
                    Rbt_Usb.IsChecked = true;
                    break;
                case eClockBase.Modelos.Terminales.TerminalDir.tipo.Serial:
                    Rbt_Serial.IsChecked = true;
                    break;
                case eClockBase.Modelos.Terminales.TerminalDir.tipo.Modem:
                    Rbt_Modem.IsChecked = true;
                    break;
                case eClockBase.Modelos.Terminales.TerminalDir.tipo.RS485:
                    Rbt_RS485.IsChecked = true;
                    break;
            }
        }

        public void Vista2TerminalDir()
        {
            if (CeC.Convierte2Bool(Rbt_Red.IsChecked))
            {
                TerminalDir.TipoConexion = eClockBase.Modelos.Terminales.TerminalDir.tipo.Red;
                TerminalDir.Direccion = Tbx_Red_Dir.Text;
                TerminalDir.Puerto = CeC.Convierte2Int(Tbx_Red_Puerto.Text);
                TerminalDir.NoTerminal = Tbx_Red_ID.Text;
                TerminalDir.Clave = Tbx_Red_Clave.Text;
            }

            if (CeC.Convierte2Bool(Rbt_Usb.IsChecked))
            {
                Spn_Serial.Visibility = System.Windows.Visibility.Visible;
            }

            if (CeC.Convierte2Bool(Rbt_Serial.IsChecked))
            {
                Spn_USB.Visibility = System.Windows.Visibility.Visible;
            }

            if (CeC.Convierte2Bool(Rbt_Modem.IsChecked))
            {
                TerminalDir.TipoConexion = eClockBase.Modelos.Terminales.TerminalDir.tipo.Modem;
                TerminalDir.Direccion = Tbx_Modem_Telefono.Text;
                TerminalDir.Puerto = CeC.Convierte2Int(Tbx_Modem_Puerto.Text);                
                TerminalDir.Velocidad = CeC.Convierte2Int(Tbx_485_Velocidad.Text);
                TerminalDir.Clave = Tbx_Modem_Clave.Text;
            }

            if (CeC.Convierte2Bool(Rbt_RS485.IsChecked))
            {
                Spn_485.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public UC_Direccion()
        {
            InitializeComponent();
        }

        private void CambioRbt(object sender, RoutedEventArgs e)
        {
            Spn_Red.Visibility = System.Windows.Visibility.Collapsed;
            Spn_Serial.Visibility = System.Windows.Visibility.Collapsed;
            Spn_USB.Visibility = System.Windows.Visibility.Collapsed;
            Spn_Modem.Visibility = System.Windows.Visibility.Collapsed;
            Spn_485.Visibility = System.Windows.Visibility.Collapsed;

            if (CeC.Convierte2Bool(Rbt_Red.IsChecked))
            {
                Spn_Red.Visibility = System.Windows.Visibility.Visible;
            }
            if (CeC.Convierte2Bool(Rbt_Usb.IsChecked))
            {
                Spn_Serial.Visibility = System.Windows.Visibility.Visible;
            }
            if (CeC.Convierte2Bool(Rbt_Serial.IsChecked))
            {
                Spn_USB.Visibility = System.Windows.Visibility.Visible;
            }
            if (CeC.Convierte2Bool(Rbt_Modem.IsChecked))
            {
                Spn_Modem.Visibility = System.Windows.Visibility.Visible;
            }
            if (CeC.Convierte2Bool(Rbt_RS485.IsChecked))
            {
                Spn_485.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
