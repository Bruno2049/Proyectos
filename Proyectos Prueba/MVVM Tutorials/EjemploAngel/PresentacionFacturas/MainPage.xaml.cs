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

namespace PresentacionFacturas
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsertarCabecera cabecera = new InsertarCabecera();
            CanvasPrincipalv.Children.Clear();
            CanvasPrincipalv.Children.Add(cabecera);
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListarFacturas cabecera = new ListarFacturas();
            CanvasPrincipalv.Children.Clear();
            CanvasPrincipalv.Children.Add(cabecera);
        }
    }
}
