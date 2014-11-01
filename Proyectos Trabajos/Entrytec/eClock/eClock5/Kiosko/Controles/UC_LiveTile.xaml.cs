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
using System.Windows.Threading;

namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_LiveTile.xaml
    /// </summary>
    public partial class UC_LiveTile : UserControl
    {
        private int m_TimeOutCambio = 5;

        public int TimeOutCambio
        {
            get { return m_TimeOutCambio; }
            set { m_TimeOutCambio = value; }
        }

        private Color m_ColorFondo = Colors.White;

        public Color ColorFondo
        {
            get { return m_ColorFondo; }
            set
            {
                m_ColorFondo = value;
                Background = new SolidColorBrush(value);
            }
        }
        private string m_Texto = "";

        public string Texto
        {
            get { return m_Texto; }
            set
            {
                m_Texto = value;
                Lbl_Texto.Text = value;
            }
        }

        private string m_Descripcion = "";

        public string Descripcion
        {
            get { return m_Descripcion; }
            set
            {
                m_Descripcion = value;
                Lbl_Descripcion.Text = value;
            }
        }

        private bool m_ImagenMostrada = false;

        public bool ImagenMostrada
        {
            get { return m_ImagenMostrada; }
            set
            {
                m_ImagenMostrada = value;
                if (m_ImagenMostrada)
                {
                    Lbl_Descripcion.Visibility = System.Windows.Visibility.Collapsed;
                    Stk_Panel.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                    Img_Boton.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    Lbl_Descripcion.Visibility = System.Windows.Visibility.Visible;
                    Stk_Panel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    Img_Boton.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        public ImageSource Imagen
        {
            get
            {
                return Img_Boton.Source;
            }
            set
            {
                Img_Boton.Source = value;
            }
        }

        public UC_LiveTile()
        {
            InitializeComponent();
        }

        DispatcherTimer Timer = new DispatcherTimer();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void IniciaCambio()
        {
            Timer.Tick -= Timer_Tick;
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, m_TimeOutCambio);
            Timer.Start();
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            ImagenMostrada = !ImagenMostrada;
        }

        private void Img_Boton_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Timer != null)
            {
                Timer.Stop();
                Timer = null;
            }
        }

        
    }
}
