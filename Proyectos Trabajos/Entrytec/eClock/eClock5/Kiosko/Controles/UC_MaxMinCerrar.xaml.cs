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

namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_MaxMinCerrar.xaml
    /// </summary>
    public partial class UC_MaxMinCerrar : UserControl
    {
        public bool AutoOcultar { get; set; }
        public UC_MaxMinCerrar()
        {
            InitializeComponent();
        }
        public enum Estado
        {
            Cerrar,
            Maximizar,
            Minimizar,
            Restaurar
        }
        public delegate void EstadoArgs(Estado NuevoEstado);
        public event EstadoArgs EstadoChanged;
        private void Btn_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            if (EstadoChanged != null)
                EstadoChanged(Estado.Cerrar);
        }

        private void Btn_Maximisar_Click(object sender, RoutedEventArgs e)
        {
            Btn_Minipantalla.Visibility = Visibility.Visible;
            Btn_Maximisar.Visibility = Visibility.Hidden;
            if (EstadoChanged != null)
                EstadoChanged(Estado.Maximizar);
        }

        private void Btn_Minipantalla_Click(object sender, RoutedEventArgs e)
        {
            Btn_Maximisar.Visibility = Visibility.Visible;
            Btn_Minipantalla.Visibility = Visibility.Hidden;
            if (EstadoChanged != null)
                EstadoChanged(Estado.Restaurar);
        }

        private void Btn_Minimizar_Click(object sender, RoutedEventArgs e)
        {
            if (EstadoChanged != null)
                EstadoChanged(Estado.Minimizar);
        }
        DateTime FechaMouseDown = new DateTime();
        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FechaMouseDown = DateTime.Now;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan TS = DateTime.Now - FechaMouseDown;
            if (TS.TotalSeconds > 5)
            {
                Grd_Main.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (AutoOcultar)
                Grd_Main.Visibility = System.Windows.Visibility.Hidden;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Grd_Main.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
