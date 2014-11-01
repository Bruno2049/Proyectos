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
#if !NETFX_CORE
using System.Windows.Media.Animation;
#else
using Windows.UI.Xaml.Media.Animation;
#endif

namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_MessageBox.xaml
    /// </summary>
    public partial class UC_MessageBox : UserControl
    {
        bool m_ControlEdicionAgregado = false;
        public string Mensaje
        {
            get
            {
                return Tbx_Mensaje.Text;
            }
            set
            {
                Tbx_Mensaje.Text = value;
            }
        }
        public string Texto_OK
        {
            get
            {
                return Btn_OK.Content.ToString();
            }
            set
            {
                Btn_OK.Content = value;
            }
        }
        public string Texto_Cancel
        {
            get
            {
                return Btn_Cancel.Content.ToString();
            }
            set
            {
                Btn_Cancel.Content = value;
            }
        }
        public UC_MessageBox()
        {
            InitializeComponent();
        }
        public void ShowDialog(Grid Papa, Color ColorDlg)
        {
            ShowDialog(Papa, new SolidColorBrush(ColorDlg));
        }

        public void ShowDialog(Grid Papa, Brush ColorDlg)
        {
            Margin = new Thickness(0);
            Grd_Main.Background = ColorDlg;
            if (!m_ControlEdicionAgregado)
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Top;
                Width = Double.NaN;
                Height = Double.NaN;
                Papa.Children.Add(this);
                m_ControlEdicionAgregado = true;
                
            }
            else
            {
                this.Visibility = Visibility.Visible;
            }

            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = Papa.ActualHeight;

            da.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(300));

            this.BeginAnimation(FrameworkElement.HeightProperty, da);
            //Grid_Main.RowDefinitions[2].BeginAnimation(System.Windows.Controls.RowDefinition.HeightProperty,da);
            //Grid_M.BeginAnimation(System.Windows.Controls.TextBlock.OpacityProperty, da);
        }
        public void Cerrar()
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        public event RoutedEventHandler Click_OK;
        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            Cerrar();
            if (Click_OK != null)
                Click_OK(this, e);
        }
        public event RoutedEventHandler Click_Cancel;
        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Cerrar();
            if (Click_Cancel != null)
                Click_Cancel(this, e);

        }


    }
}
