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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Threading;
namespace Kiosko.Controles
{
    /// <summary>
    /// Lógica de interacción para UC_MessageBoxFullScreen.xaml
    /// </summary>
    public partial class UC_MessageBoxFullScreen : UserControl
    {
        private Color m_Color;

        public Color Color
        {
            get { return m_Color; }
            set
            {
                m_Color = value;
                this.Background = new SolidColorBrush(value);
            }
        }

        private int m_TimeOutSegundos = 5;

        public int TimeOutSegundos
        {
            get { return m_TimeOutSegundos; }
            set { m_TimeOutSegundos = value; }
        }

        public string Mensaje
        {
            get { return Lbl_Mensaje.Text; }
            set { Lbl_Mensaje.Text = value; }
        }
        public ImageSource Imagen
        {
            get { return Img_Imagen.Source; }
            set { Img_Imagen.Source = value; }
        }
        public UC_MessageBoxFullScreen()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cerrar();
        }
        void Cerrar()
        {
            Timer.Tick -= Timer_Tick;
            Timer.Stop();
            Visibility = System.Windows.Visibility.Hidden;
            if (Cerrado != null)
                Cerrado();
        }
        DispatcherTimer Timer = new DispatcherTimer();
        public void Mostrar(UserControl Padre)
        {
            Width = double.NaN;
            Height = double.NaN;
            Margin = new Thickness(0);
            ((Panel)Padre.Content).Children.Add(this);

            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, m_TimeOutSegundos);
            Timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            Cerrar();
        }

        public delegate void CerradoArgs();
        public event CerradoArgs Cerrado;


    }
}
