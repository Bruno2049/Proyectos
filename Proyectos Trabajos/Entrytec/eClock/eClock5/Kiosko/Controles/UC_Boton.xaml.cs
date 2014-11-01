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
    /// Lógica de interacción para UC_Boton.xaml
    /// </summary>
    public partial class UC_Boton : UserControl
    {
        private Color m_ColorFondo = Colors.White;

        public Color ColorFondo
        {
            get { return m_ColorFondo; }
            set
            {
                m_ColorFondo = value;
                Btn_Main.Background = new SolidColorBrush(value);
            }
        }


        private Color m_ColorFont = Colors.Black;

        public Color ColorFont
        {
            get { return m_ColorFont; }
            set
            {
                m_ColorFont = value;
                Btn_Main.Foreground = new SolidColorBrush(value);
            }
        }


        private string m_Texto = "";

        public string Texto
        {
            get { return m_Texto; }
            set
            {
                m_Texto = value;
                Lbl_Boton.Text = value;
            }
        }
        private int m_NoAlertas = 0;

        public int NoAlertas
        {
            get { return m_NoAlertas; }
            set { m_NoAlertas = value;
            if (m_NoAlertas <= 0)
            {
                Lbl_Num_Día.Visibility = System.Windows.Visibility.Hidden;
                Elipse.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                Lbl_Num_Día.Visibility = System.Windows.Visibility.Visible;
                Elipse.Visibility = System.Windows.Visibility.Visible;
                Lbl_Num_Día.Text = m_NoAlertas.ToString();
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

        public UC_Boton()
        {
            InitializeComponent();
        }
        public event RoutedEventHandler Click;

        private void Btn_Main_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null)
                Click(sender, e);
        }
    }
}
