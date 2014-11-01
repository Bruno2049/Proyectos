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
    /// Lógica de interacción para UC_Btn_LiveTile.xaml
    /// </summary>
    public partial class UC_Btn_LiveTile : UserControl
    {
        private string m_Titulo = "Titulo";

        public string Titulo
        {
            get { return m_Titulo; }
            set
            {
                m_Titulo = value;
                Lbl_Titulo.Text = value;
            }
        }
        private string m_Descripcion = "Descripcion";

        public string Descripcion
        {
            get { return m_Descripcion; }
            set
            {
                m_Descripcion = value;
                Lbl_Descripcion.Text = value;
            }
        }
        private string m_Numero = "1";

        public string Numero
        {
            get { return m_Numero; }
            set
            {
                m_Numero = value;
                Lbl_Numero.Text = value;
            }
        }
        public ImageSource Source
        {
            get
            {
                return Img_Icono.Source;
            }
            set
            {
                Img_Icono.Source = value;
            }
        }
        public UC_Btn_LiveTile()
        {
            InitializeComponent();
        }
        public event RoutedEventHandler Click;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Btn_Main.Background = this.Background;
        }
    }
}
