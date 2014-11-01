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
using System.Windows.Controls.Primitives;
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
    /// Lógica de interacción para UC_Pestanas.xaml
    /// </summary>
    public partial class UC_Pestanas : UserControl
    {
        public delegate void EventClickPestana(UserControl Control);
        public event EventClickPestana OnEventClickPestana;

        public UC_Pestanas()
        {
            InitializeComponent();
        }
        public ToggleButton m_Seleccionado = null;
        private UC_Acordion_Herencia m_Elemento = null;
        private List<UC_Acordion_Pestanas> m_PestanasAnteriores = null;
        public bool CreaPestanas(UC_Acordion_Herencia Elemento, string RestriccionesPermitidas)
        {
            if (Elemento == m_Elemento)
                return false;
            m_PestanasAnteriores = null;
            try
            {
                m_Elemento = Elemento;
                Pnl.Children.Clear();
                List<UC_Acordion_Pestanas> Pestanas = Elemento.Pestanas;
                if (m_PestanasAnteriores == Pestanas)
                    return true;
                
                m_PestanasAnteriores = Pestanas;

                int Cont = -1;
                foreach (UC_Acordion_Pestanas Pestana in Pestanas)
                {
                    Cont++;
                    if (!UC_Acordion.ValidaRestricciones(Pestana.Permisos, RestriccionesPermitidas))
                        continue;
                    if (Elemento.PensenaSeleccionada < 0)
                        Elemento.PensenaSeleccionada = Cont;
                    Pestana.Padre = Elemento;
                    ToggleButton Boton = new ToggleButton();
                    Boton.Style = (Style)FindResource("ToggleButtonPestanasStyle");    
                    Boton.Name = Pestana.Nombre;
                    Boton.Content = Pestana.Etiqueta;
                    /*
                    Boton.Foreground = Recursos.Blanco_Brush;
                    Boton.FontFamily = Recursos.EntryTecFont;
                    Boton.FontSize = Recursos.FontSizeTitulo;
                    Boton.BorderBrush = Boton.Background = Brushes.Transparent;                    
                    Boton.BorderThickness = new Thickness(0,0,20,0);*/
                    Boton.Click += Boton_Click;
                    Boton.Tag = Pestana;
                    if (Cont == Elemento.PensenaSeleccionada)
                        Boton_Click(Boton, null);
                    Pnl.Children.Add(Boton);
                }
                
                return true;
            }
            catch (Exception ex)
            { }
            return false;
        }

        public static bool TienePestanas(UC_Acordion_Herencia Elemento, string RestriccionesPermitidas)
        {

            int NoPestanas = 0;
            try
            {
                List<UC_Acordion_Pestanas> Pestanas = Elemento.Pestanas;
                int Cont = 0;
                foreach (UC_Acordion_Pestanas Pestana in Pestanas)
                {
                    if (!UC_Acordion.ValidaRestricciones(Pestana.Permisos, RestriccionesPermitidas))
                        continue;
                    NoPestanas++;
                }

                
            }
            catch (Exception ex)
            { }
            if (NoPestanas > 0)
                return true;
            return false;
        }

        void Boton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton Boton = (ToggleButton)sender;
            if (Boton == m_Seleccionado)
                return;
            if (m_Seleccionado != null)
                m_Seleccionado.IsChecked = false;
                //m_Seleccionado.Background = Brushes.Transparent;
            UC_Acordion_Pestanas Pestana = ((UC_Acordion_Pestanas)Boton.Tag);
            if (m_PestanasAnteriores != null)
                m_Elemento.PensenaSeleccionada = m_PestanasAnteriores.IndexOf(Pestana);

//            Boton.Background = (ImageBrush)FindResource("FlechaArriba_Brush");
            Boton.IsChecked = true;
            m_Seleccionado = Boton;
            if (OnEventClickPestana != null)
            {
                Clases.Parametros Parametros = new Clases.Parametros(false,Pestana.Padre.Parametro);
                if (Pestana.Vista != null)
                {
                    Pestana.Vista.Width = double.NaN;
                    Pestana.Vista.Height = double.NaN;
                    Pestana.Vista.Tag = Parametros;
                    OnEventClickPestana(Pestana.Vista);
                }
            }
            
        }
    }
}
