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
using System.Windows.Threading;
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
using eClockBase;
using Newtonsoft.Json;

namespace eClock5
{
    /// <summary>
    /// Lógica de interacción para eClock.xaml
    /// </summary>
    public partial class eClock : Window
    {
        eClockBase.CeC_SesionBase Sesion;
        Clases.CeC_eClockSync eClockSync;
        public bool Ejecuta(string ArchivoNombre)
        {
            try
            {
                eClockBase.CeC_Log.AgregaDebug("Ejecuta(ArchivoNombre);" + ArchivoNombre);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = ArchivoNombre;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.Start();
                return true;
            }
            catch(Exception ex) 
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }

        private DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }

        public static void AsignaIcono(Window Ventana)
        {
            //return;
            try
            {

                Ventana.Icon = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/eClock.ico", UriKind.Absolute));
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
                try
                {

                    Ventana.Icon = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/eClockXP.ico", UriKind.Absolute));
                }
                catch (Exception exX)
                {
                    CeC_Log.AgregaError(exX);
                }
            }
        }

        public eClock()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            eClockBase.CeC_Log.AgregaLog("Fecha y hora de compilacion " + RetrieveLinkerTimestamp().ToUniversalTime());
            eClockBase.CeC_Log.AgregaDebug("Recursos.Inicia(this);");
            Recursos.Inicia(this);
            AsignaIcono(this);
            /*
            CeC_BD BD = new CeC_BD("Provider=MSDAORA;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=190.190.1.2)(PORT=1526)))(CONNECT_DATA=(SERVICE_NAME=test)));User Id=apps;Password=apps;");
            string Datos = CeC_BD.DataSet2JsonList(BD.lEjecutaDataSet("select BALANCE_TYPE_ID,BALANCE_NAME,ATTRIBUTE1,BASE_BALANCE_TYPE_ID from pay_balance_types"));
            */
            //Ejecuta("icacls ./ /grant todos:(OI)(CI)(F)");
            
            /*  eClockBase.Modelos.Model_PUBLICIDAD Pub = new eClockBase.Modelos.Model_PUBLICIDAD();
              Pub.PUBLICIDAD = new byte[10];
              string Res = JsonConvert.SerializeObject(Pub);
              *//*
            Clases.Interfaces.Interfaz Interfaz = new Clases.Interfaces.Interfaz();
            Interfaz.Parametros = new eClockBase.Modelos.Nomina.Model_Interfaz();
            Interfaz.Parametros.SISTEMA_NOMINA_ID = 1;
            Interfaz.Parametros.CadenaConexion = "Provider=MSDAORA;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=190.190.1.2)(PORT=1526)))(CONNECT_DATA=(SERVICE_NAME=test)));User Id=apps;Password=apps;";
            eClockBase.Modelos.Nomina.Model_RecNominasImportar Imp = Interfaz.ObtenRecibosNomina("66", 2013, 30);*/
            eClockBase.CeC_Log.AgregaDebug("Sesion = CeC_Sesion.ObtenSesion(this);");
            Sesion = CeC_Sesion.ObtenSesion(this);
            eClockBase.CeC_Log.AgregaDebug("eClockBase.Controladores.Localizaciones.sObtenEtiquetasAyuda(Sesion);");
            eClockBase.Controladores.Localizaciones.sObtenEtiquetasAyuda(Sesion);
            if (Sesion.Maquina == "" || Sesion.Licencia == "")
            {

                Ejecuta("PermisosDa.bat");
                Vista.Wizard.Instalacion Dlg = new Vista.Wizard.Instalacion();
                if (Dlg.ShowDialog() != true)
                {
                    this.Close();
                }
            }
            if (!Sesion.EstaLogeado())
            {
                LogIn Dlg = new LogIn();
                Dlg.ShowDialog();
            }
            if (!Sesion.EstaLogeado())
            {
                this.Close();
            }
            if (Sesion.Maquina != "" && Sesion.Licencia != "")
            {
                eClockBase.Controladores.Licencias Lic = new eClockBase.Controladores.Licencias(Sesion);
                Lic.LicenciaCambio += Lic_LicenciaCambio;
                Lic.ValidaLicencia();
            }
            eClockSync = new Clases.CeC_eClockSync();
            eClockSync.EjecutaSincronizador(Sesion);
            InitializeComponent();
            Sesion.AsignaControlMensaje(Lbl_Estado);
            BaseModificada.Localizaciones.sLocaliza(this);
            Loaded += eClock_Loaded;

        }


        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string ss = e.ToString();
            eClockBase.CeC_Log.AgregaError(e.ExceptionObject.ToString());
        }

        void Lic_LicenciaCambio(bool Correcta)
        {
            if (!Correcta)
            {
                Vista.Licencias.NoValida Dlg = new Vista.Licencias.NoValida();
                BaseModificada.Localizaciones.sLocaliza(Dlg);
                this.Visibility = System.Windows.Visibility.Collapsed;

                if (Dlg.ShowDialog() == true)
                {
                }
                else
                    Application.Current.Shutdown();
            }
        }

        void eClock_Loaded(object sender, RoutedEventArgs e)
        {

            Btn_Maximisar.Visibility = Visibility.Hidden;
            Acordion.ControlPestanas = Pestanas;
            PantallaPrincipal();
        }
        public void MuestraControl(UserControl Control)
        {
            Pnl.Children.Clear();
            Pnl.Children.Add(Control);
            if (!Control.IsLoaded)
                BaseModificada.Localizaciones.sLocaliza(Control);
        }
        private void UC_Pestanas_OnEventClickPestana_1(UserControl Control)
        {
            MuestraControl(Control);
        }

        private void Acordion_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Maximisar_Click(object sender, RoutedEventArgs e)
        {
            Btn_Mini_Pantalla.Visibility = Visibility.Visible;
            Btn_Maximisar.Visibility = Visibility.Hidden;
            this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void Btn_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            eClockSync.PararSincronizador();
            this.Close();
        }

        private void Btn_Mini_Pantalla_Click(object sender, RoutedEventArgs e)
        {
            Btn_Maximisar.Visibility = Visibility.Visible;
            Btn_Mini_Pantalla.Visibility = Visibility.Hidden;
            this.WindowState = System.Windows.WindowState.Normal;
        }

        private void Btn_Minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Rectangle_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                Btn_Maximisar.Visibility = Visibility.Hidden;
                Btn_Mini_Pantalla.Visibility = Visibility.Visible;
            }
        }

        private void Btn_Logo_Click(object sender, RoutedEventArgs e)
        {
            
            Pestanas.CreaPestanas(null, "");
            MuestraControl(new eClock5.Vista.eClock.LiveTiles());
            PantallaPrincipal();
        }

        private void PantallaPrincipal()
        {
            eClockBase.Controladores.Sesion Sesion = new eClockBase.Controladores.Sesion(CeC_Sesion.ObtenSesion(this));
            Sesion.Inicia_eClock5();
        }




    }
}
