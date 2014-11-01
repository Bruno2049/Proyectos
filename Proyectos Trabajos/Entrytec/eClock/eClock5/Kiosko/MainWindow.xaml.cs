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
using eClock5;
using eClock5.BaseModificada;
using eClockBase;
using System.Windows.Threading;
#if !NETFX_CORE
using System.Windows.Media.Animation;
#else
using Windows.UI.Xaml.Media.Animation;
#endif

namespace Kiosko
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static eClockBase.ControladoresParametros.Kiosco KioscoParametros = new eClockBase.ControladoresParametros.Kiosco();
        public static Configuracion.Configuracion Config;
        int TiempoInactividadLogIn = 10;
        int TiempoInactividadOtro = 15;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            TiempoInactividadLogIn = 125;
            TiempoInactividadOtro = 135;
#endif
        }

        private void Ctr_Log_In_LogueoCorrecto(Generales.Log_In Control)
        {
           /* Inicio Dlg = new Inicio();
            Dlg.ShowDialog();
            return;*/
            KioscoParametros.CargaParametros(m_Sesion, false);
            Ctr_Log_In.Visibility = System.Windows.Visibility.Hidden;
            Ctr_Main.Visibility = System.Windows.Visibility.Visible;
            Ctr_Main.MostrarDatos();
/*            Generales.Main Man = new Generales.Main();
            Man.MostrarDatos();
            Generales.Main.MuestraComoDialogo(this, Man, Colors.White);*/
        }

        private void Ctr_Main_SesionCerrada(Generales.Main Control)
        {
            Ctr_Log_In.Visibility = System.Windows.Visibility.Visible;
            Ctr_Main.Visibility = System.Windows.Visibility.Hidden;
           
        }
        DispatcherTimer Timer;
        eClockBase.CeC_SesionBase m_Sesion;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.ResizeMode = System.Windows.ResizeMode.NoResize;
            eClockBase.CeC_Stream.MetodoStream = new CeC_StreamFile();
            CeC_LogDestino.StreamWriter = System.IO.File.AppendText("eClock5.log");
            Recursos.Inicia(this);
            Config = Configuracion.Configuracion.Carga();
            Config.Guarda();
            m_Sesion = CeC_Sesion.ObtenSesion(this);
            m_Sesion.AsignaControlMensaje(Lbl_Estado);
            if (m_Sesion.Suscripcion == "")
            {
                UC_ToolBar.MuestraComoDialogo(this, new Generales.Suscripcion());
            }
            
                

            Timer = new DispatcherTimer();
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Controles.UC_MessageBox Msg = new Controles.UC_MessageBox();
            Msg.ShowDialog(this.Grd_Main, Colors.Violet);
        }


        
        DateTime FechaHoraUltimoMovimiento;
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            FechaHoraUltimoMovimiento = DateTime.Now;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            FechaHoraUltimoMovimiento = DateTime.Now;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan TS = DateTime.Now - FechaHoraUltimoMovimiento;

            if(m_Sesion.EstaMostrandoMensaje())
                FechaHoraUltimoMovimiento = DateTime.Now;

            if (Ctr_Main.IsVisible)
            {
                if (TS.TotalSeconds >= TiempoInactividadOtro)
                {
                    Lbl_Contador.Visibility = System.Windows.Visibility.Hidden;
                    Ctr_Main.CerrarSesion();
                   
                }
                else
                {
                    if (TS.TotalSeconds >= TiempoInactividadOtro / 2)
                    {
                        Lbl_Contador.Visibility = System.Windows.Visibility.Visible;
                        Lbl_Contador.Texto = CeC.Convierte2String(TiempoInactividadOtro - TS.Seconds);
                    }
                    else
                        Lbl_Contador.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            else
                if (!Ctr_Log_In.Inactivo)
                {
                    if (TS.TotalSeconds >= TiempoInactividadLogIn)
                    {
                        Lbl_Contador.Visibility = System.Windows.Visibility.Hidden;
                        Ctr_Log_In.MuestraInactivo();                        
                    }
                    else
                    {
                        if (TS.TotalSeconds >= TiempoInactividadLogIn / 2)
                        {
                            Lbl_Contador.Visibility = System.Windows.Visibility.Visible;
                            Lbl_Contador.Texto = CeC.Convierte2String(TiempoInactividadLogIn - TS.Seconds);
                        }
                        else
                            Lbl_Contador.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
                else
                    Lbl_Contador.Visibility = System.Windows.Visibility.Hidden;
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
        }

        private void UC_MaxMinCerrar_EstadoChanged(Controles.UC_MaxMinCerrar.Estado NuevoEstado)
        {
            switch (NuevoEstado)
            {
                case Controles.UC_MaxMinCerrar.Estado.Minimizar:
                    this.WindowState = System.Windows.WindowState.Minimized;
                    break;
                case Controles.UC_MaxMinCerrar.Estado.Restaurar:
                    this.WindowState = System.Windows.WindowState.Normal;
                    break;
                case Controles.UC_MaxMinCerrar.Estado.Maximizar:
                    this.WindowState = System.Windows.WindowState.Maximized;
                    break;
                case Controles.UC_MaxMinCerrar.Estado.Cerrar:
                    this.Close();
                    break;

            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
