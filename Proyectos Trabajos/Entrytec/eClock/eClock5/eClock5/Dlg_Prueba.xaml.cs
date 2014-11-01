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

namespace eClock5
{
    /// <summary>
    /// Lógica de interacción para Dlg_Prueba.xaml
    /// </summary>
    public partial class Dlg_Prueba : Window
    {
        public Dlg_Prueba()
        {
            InitializeComponent();
        }

        private void UC_ColorPicker_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
