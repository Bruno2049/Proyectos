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
    /// Lógica de interacción para UC_Wizard.xaml
    /// </summary>
    public partial class UC_Wizard : UserControl
    {
        private List<UserControl> m_Pasos = new List<UserControl>();

        public List<UserControl> Pasos
        {
            get { return m_Pasos; }
        }

        private int m_Mostrado = 0;

        public int Mostrado
        {
            get { return m_Mostrado; }
            set { m_Mostrado = value; }
        }

        public UC_Wizard()
        {
            InitializeComponent();
            Loaded += UC_Wizard_Loaded;
        }

        void UC_Wizard_Loaded(object sender, RoutedEventArgs e)
        {
            s_WizardMostrado = this;;
            BaseModificada.Localizaciones.sLocaliza(this);
            if (m_Pasos.Count > 0)
                Mostrar(m_Pasos[0]);
        }
        public delegate void OnFinalizarArgs(int PosActual, string Control);
        public event OnFinalizarArgs OnFinalizar;
        public delegate void OnCancelarArgs(int PosActual, string Control);
        public event OnCancelarArgs OnCancelar;

        public delegate void OnSiguienteArgs(int PosActual, string Control);
        public event OnSiguienteArgs OnSiguiente;
        void Mostrar(UserControl Control)
        {
            XGrid.Children.Clear();
            Control.Width = double.NaN;
            Control.Height = double.NaN;
            Control.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Control.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            if (m_Pasos.IndexOf(Control) == 0)
                XGrid.Children.Remove(XGrid_Titulo);
            else
                if (!XGrid.Children.Contains(XGrid_Titulo))
                    XGrid.Children.Add(XGrid_Titulo);
            BaseModificada.Localizaciones.sLocaliza(Control);
            
            XGrid.Children.Add(Control);
            
        }

        static UC_Wizard s_WizardMostrado;
        public static void sMostrarBotones(bool Atras, bool Siguiente, bool Cancelar, bool Finalizar= false)
        {
            s_WizardMostrado.MostrarBotones(Atras,Siguiente,Cancelar,Finalizar);
        }

        public void MostrarBotones(bool Atras, bool Siguiente, bool Cancelar, bool Finalizar = false)
        {
            Btn_Atras.Visibility = Atras ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            Btn_Cancelar.Visibility = Cancelar ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            
            Btn_Finalizar.Visibility = Finalizar ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            if (!Finalizar)
            {
                Btn_Siguiente.Visibility = System.Windows.Visibility.Visible;
                Btn_Siguiente.IsEnabled = Siguiente;
            }
            else
                Btn_Siguiente.Visibility = Siguiente ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }
        public static void sSiguiente()
        {
            s_WizardMostrado.Siguiente();
        }

        public void Siguiente()
        {
            if (m_Mostrado < m_Pasos.Count - 1)
            {
                Mostrar(m_Pasos[++m_Mostrado]);
            }
        }
        private void Btn_Siguiente_Click(object sender, RoutedEventArgs e)
        {
            if (OnSiguiente != null)
                OnSiguiente(m_Mostrado, m_Pasos[m_Mostrado].GetType().Name);
            else
                Siguiente();
        }
        public UserControl ObtenControl(string Nombre)
        {
            foreach (UserControl Paso in m_Pasos)
            {
                if (Paso.GetType().Name == Nombre)
                    return Paso;                         
            }
            return null;                 
        }

        private void Btn_Atras_Click(object sender, RoutedEventArgs e)
        {
            if (m_Mostrado > 0)
            {
                Mostrar(m_Pasos[--m_Mostrado]);
            }
        }

        private void Btn_Finalizar_Click(object sender, RoutedEventArgs e)
        {
            if (OnFinalizar != null)
                OnFinalizar(m_Mostrado, m_Pasos[m_Mostrado].GetType().Name);
        }

        private void Btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (OnCancelar != null)
                OnCancelar(m_Mostrado, m_Pasos[m_Mostrado].GetType().Name);
        }
        public void Localiza()
        {
            BaseModificada.Localizaciones.sLocaliza(this);
            foreach (UserControl Control in m_Pasos)
            {
                BaseModificada.Localizaciones.sLocaliza(Control);
            }
        }
    }
}
