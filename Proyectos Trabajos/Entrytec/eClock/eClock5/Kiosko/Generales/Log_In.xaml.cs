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

using eClock5.BaseModificada;
using eClock5;
using eClockBase;
namespace Kiosko.Generales
{
    /// <summary>
    /// Lógica de interacción para Log_In.xaml
    /// </summary>
    public partial class Log_In : UserControl
    {
        SolidColorBrush Tobrush;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        

        public Log_In()
        { 
            InitializeComponent();            
        }

        eClockBase.CeC_SesionBase m_Sesion = null;
        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {

            Lbl_Error.Visibility = System.Windows.Visibility.Hidden;
            m_Sesion = CeC_Sesion.ObtenSesion(this);

            eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(m_Sesion);
            CSesion.LogeoFinalizado += CSesion_LogeoFinalizado;
            CSesion.CreaSesionAdvSuscripcion(Tbx_Usuario.Text, Tbx_Clave.Password, false);
            Btn_Login.IsEnabled = false;
        }

        private void Btn_Borrar_Click(object sender, RoutedEventArgs e)
        {
            Tbx_Usuario.Text = "";
            Tbx_Clave.Password = "";            
        }

        void CSesion_LogeoFinalizado(object sender, EventArgs e)
        {
            if (m_Sesion.EstaLogeado())
            {
                CeC_Log.AgregaLog("CSesion.CreaSesion_Completado " + m_Sesion.SESION_SEGURIDAD);

                Tbx_Usuario.Text = "";
                Tbx_Clave.Password = "";

                if (LogueoCorrecto != null)
                    LogueoCorrecto(this);
            }
            else
            {
                Tbx_Usuario.Text = "";
                Tbx_Clave.Password = "";
                Lbl_Error.Visibility = System.Windows.Visibility.Visible;

            }
            Btn_Login.IsEnabled = true;
        }

        public delegate void LogueoCorrectoArgs(Log_In Control);
        public event LogueoCorrectoArgs LogueoCorrecto;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Teclado.FocoPrincipal = Tbx_Usuario;
            Teclado.ControlesEnter.Add(Tbx_Clave);

            //SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, 255, 139, 0));
           
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(MainWindow.Config.Color));

            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 6);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            Lbl_Error.Visibility = System.Windows.Visibility.Hidden;
            dispatcherTimer.Stop();
            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void Btn_Registrate_Click(object sender, RoutedEventArgs e)
        {
            UC_ToolBar.MuestraComoDialogo(this, new Generales.Registro());
        }

        private void Btn_OlvideContrasena_Click(object sender, RoutedEventArgs e)
        {
            UC_ToolBar.MuestraComoDialogo(this, new Generales.Olvido_Clave());
        }

        private void Btn_Suscripcion_Click(object sender, RoutedEventArgs e)
        {
            UC_ToolBar.MuestraComoDialogo(this, new Generales.Suscripcion());
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == System.Windows.Visibility.Visible)
            {
                Banner.MuestraFullScreen(this.ActualHeight);
            }
        }

        private void Banner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Teclado.FocoPrincipal = Tbx_Usuario;
            MuestraInactivo();
        }

        public void MuestraInactivo()
        {
            Lbl_Error.Visibility = System.Windows.Visibility.Hidden;
            Tbx_Usuario.Text = "";
            Tbx_Clave.Password = "";
            Keyboard.Focus(Tbx_Usuario);
            if (!Banner.FullScreen)
                Banner.MuestraFullScreen(this.ActualHeight);
        }
        public bool Inactivo
        {
            get { return Banner.FullScreen; }
        }

        private void Tbx_Usuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
