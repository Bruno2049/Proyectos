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
    /// Lógica de interacción para UC_Dia.xaml
    /// </summary>
    public partial class UC_Dia : UserControl
    {
        Color m_ColorDia = Colors.White;
        public Color ColorDia
        {
            get { return m_ColorDia; }
            set
            {
                m_ColorDia = value;
                Elipse.Fill = new SolidColorBrush(value);
                Seleccionado = Seleccionado;
            }
        }

        private Color m_ColorSeleccion = Colors.Blue;

        public Color ColorSeleccion
        {
            get { return m_ColorSeleccion; }
            set
            {
                m_ColorSeleccion = value; 
                Seleccionado = Seleccionado;
            }
        }

        private bool m_Seleccionado = false;
        public bool Seleccionado
        {
            get { return m_Seleccionado; }
            set
            {
                m_Seleccionado = value;
                if (m_Seleccionado)
                    Elipse.Stroke = new SolidColorBrush(ColorSeleccion);
                else
                    Elipse.Stroke = Elipse.Fill;
            }
        }

        public bool m_Tenue = false;
        public bool Tenue
        {
            get { return m_Tenue; }
            set
            {
                m_Tenue = value;
                if (value)
                {
                    Foreground = new SolidColorBrush(Colors.DarkGray);
                }
                else
                {
                    Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }
        private DateTime m_Dia;
        public DateTime Dia
        {
            get { return m_Dia; }
            set
            {
                m_Dia = value;
                Lbl_Num_Día.Text = Dia.Day.ToString();
            }
        }
        public UC_Dia()
        {
            
            InitializeComponent();

        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Seleccionado = !Seleccionado;
            if (SeleccionadoChanged != null)
                SeleccionadoChanged(this, Seleccionado);
        }

        public delegate void SeleccionadoChangedArgs(UC_Dia ControlDia, bool Seleccionado);
        public event SeleccionadoChangedArgs SeleccionadoChanged;
    }
}
