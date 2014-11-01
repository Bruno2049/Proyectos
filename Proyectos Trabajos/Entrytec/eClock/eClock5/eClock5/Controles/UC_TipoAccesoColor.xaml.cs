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
namespace eClock5.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_TipoAccesoColor.xaml
    /// </summary>
    public partial class UC_TipoAccesoColor : UserControl
    {
        public UC_TipoAccesoColor()
        {
            InitializeComponent();
            Loaded += UC_TipoAccesoColor_Loaded;
        }

        void UC_TipoAccesoColor_Loaded(object sender, RoutedEventArgs e)
        {
            int TipoAccesoID = eClockBase.CeC.Convierte2Int(DataContext,0);
            switch(TipoAccesoID)
            {
                case 0:
                    this.Background = Brushes.Black;
                    break;
                case 1:
                    this.Background = Brushes.Green;
                    break;
                case 2:
                    this.Background = Brushes.Blue;
                    break;
                case 3:
                    this.Background = Brushes.Yellow;
                    break;
                case 4:
                    this.Background = Brushes.Pink;
                    break;
                case 5:
                    this.Background = Brushes.RosyBrown;
                    break;
                case 6:
                    this.Background = Brushes.Purple;
                    break;
                case 7:
                    this.Background = Brushes.PeachPuff;
                    break;
                case 8:
                    this.Background = Brushes.Orchid;
                    break;
                default:
                    this.Background = Brushes.Honeydew;
                    break;
            }
            
        }
    }
}
