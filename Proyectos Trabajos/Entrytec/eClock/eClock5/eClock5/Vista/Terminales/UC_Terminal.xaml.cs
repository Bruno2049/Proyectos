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

using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Threading;

namespace eClock5.Vista.Terminales
{
    /// <summary>
    /// Lógica de interacción para UC_Terminal.xaml
    /// </summary>
    public partial class UC_Terminal : UserControl
    {

        public UC_Terminal()
        {
            InitializeComponent();
        }
        string DireccionIP = "";
        public bool ActualizaDatos()
        {
            try
            {
                if (ListadoModelos != null)
                {
                    ActualizaMarcas();
                    return false;
                }
                //Lst_Datos.Items.Clear();
                eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);
                eClockBase.Controladores.Sesion CSesion = new eClockBase.Controladores.Sesion(Sesion);
                CSesion.CambioListadoEvent += CSesion_CambioListadoEvent;
                CSesion.ObtenListado("EC_MODELOS", "MODELO_ID", "MODELO_MARCA", "MODELO_MODELO", "MODELO_COMENTARIO", "", false, "", "");
                return true;
            }
            catch (Exception ex)
            {
                eClockBase.CeC_Log.AgregaError(ex);
            }
            return false;
        }
        static List<eClockBase.Controladores.ListadoJson> ListadoModelos = null;
        void CSesion_CambioListadoEvent(string Listado)
        {
            try
            {
                ListadoModelos = eClockBase.Controladores.CeC_ZLib.Json2Object<List<eClockBase.Controladores.ListadoJson>>(Listado);
                ActualizaMarcas();
            }
            catch { }

        }

        void ActualizaMarcas()
        {

            var results = from c in ListadoModelos
                          group c by c.Nombre.ToString() into g
                          select new
                          {
                              Marca = g.Key
                          };
            Cmb_Marca.DisplayMemberPath = "Marca";
            Cmb_Marca.SelectedValuePath = "Marca";
            Cmb_Modelo.DisplayMemberPath = "Adicional";
            Cmb_Modelo.SelectedValuePath = "Llave";
            Cmb_Marca.ItemsSource = results;
            if (Modelo_ID > 0)
                Cmb_Marca.SelectedValue = Modelo().Nombre;
            ActualizaModelos();
        }

        eClockBase.Controladores.ListadoJson Modelo()
        {
            var results = from c in ListadoModelos
                          where eClockBase.CeC.Convierte2Int(c.Llave) == Modelo_ID
                          select c;
            return results.Min();
        }

        void ActualizaModelos()
        {
            try
            {
                Cmb_Modelo.ItemsSource = null;
                string Marca = Cmb_Marca.SelectedValue.ToString();
                var results = from c in ListadoModelos
                              where c.Nombre.ToString() == Marca
                              select c;
                Cmb_Modelo.ItemsSource = results;
                if (Modelo_ID > 0)
                    Cmb_Modelo.SelectedValue = Modelo().Llave;

            }
            catch { }
            
        }
        eClockBase.Modelos.Model_TERMINALES Terminal = null;
        eClockBase.Modelos.Terminales.TerminalDir TerminalDir = null;
        int Modelo_ID = -1;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                TerminalDir = new eClockBase.Modelos.Terminales.TerminalDir();
                Terminal = (eClockBase.Modelos.Model_TERMINALES)DataContext;
                Modelo_ID = eClockBase.CeC.Convierte2Int(Terminal.TERMINAL_MODELO, -1);
                TerminalDir.CargarCadenaConexion(Terminal.TERMINAL_DIR);
                Tbx_IP.Text = eClockBase.CeC.Convierte2String(TerminalDir.Direccion);
                Tbx_Nombre.Text = eClockBase.CeC.Convierte2String(Terminal.TERMINAL_NOMBRE);
                ActualizaDatos();
            }
            catch
            {
            }
        }
        Thread workerThread = null;
        int ThreadIDActual = -1;
        private void Tbx_IP_TextChanged(object sender, TextChangedEventArgs e)
        {
            TerminalDir.Direccion = DireccionIP = Tbx_IP.Text;
            Terminal.TERMINAL_DIR = TerminalDir.ObtenCadenaConexion();
            if (workerThread != null)
                workerThread.Abort();
            workerThread = new Thread(Ping);
            ThreadIDActual = workerThread.ManagedThreadId;
            workerThread.Start();
            /*
            TimerEjecutaSincronizador = new DispatcherTimer();
            TimerEjecutaSincronizador.Tick += TimerEjecutaSincronizador_Tick;
            TimerEjecutaSincronizador.Interval = new TimeSpan(0, 1, 0);
            TimerEjecutaSincronizador.Start();

            TimerEjecutaSincronizador_Tick(this, null);*/
        }
        public void CambiaTextoPing(string Texto)
        {
           /* Lbl_PingEdo.Dispatcher.BeginInvoke( () =>
            {
                Lbl_PingEdo.Text = Texto;
            });*/
            int Thread = System.Threading.Thread.CurrentThread.ManagedThreadId;
            if (Thread != ThreadIDActual)
                return;
            Dispatcher.Invoke(new Action(() => {
                SolidColorBrush SB = new SolidColorBrush(Colors.Red);
                switch (Texto)
                {
                    case "C":
                        SB = new SolidColorBrush(Colors.Green);
                        break;
                    case "I":
                        SB = new SolidColorBrush(Colors.Yellow);
                        break;
                }
                Lbl_PingEdo.Background = SB;
                Lbl_PingEdo.Text = Texto; 

            }));
        }
        public void Ping()
        {
            try
            {
                
                CambiaTextoPing("I");

                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(DireccionIP);
                if (reply.Status == IPStatus.Success)
                    CambiaTextoPing("C");
                else
                    CambiaTextoPing("E");
            }
            catch
            {
                CambiaTextoPing("ED");
            }
            //Ping(DireccionIP);
        }

        public void Ping(string DireccionIP)
        {
            try
            {

                string who = DireccionIP;
                AutoResetEvent waiter = new AutoResetEvent(false);

                Ping pingSender = new Ping();

                // When the PingCompleted event is raised,
                // the PingCompletedCallback method is called.
                pingSender.PingCompleted += pingSender_PingCompleted;
                //pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);

                // Wait 12 seconds for a reply.
                int timeout = 12000;

                // Set options for transmission:
                // The data can go through 64 gateways or routers
                // before it is destroyed, and the data packet
                // cannot be fragmented.
                PingOptions options = new PingOptions(64, true);

                Console.WriteLine("Time to live: {0}", options.Ttl);
                Console.WriteLine("Don't fragment: {0}", options.DontFragment);

                // Send the ping asynchronously.
                // Use the waiter as the user token.
                // When the callback completes, it can wake up this thread.
                pingSender.SendAsync(who, timeout, buffer, options, waiter);

                // Prevent this example application from ending.
                // A real application should do something useful
                // when possible.
                waiter.WaitOne();
                Console.WriteLine("Ping example completed.");
            }
            catch { }

        }


        void pingSender_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            // If the operation was canceled, display a message to the user.
            if (e.Cancelled)
            {

                Console.WriteLine("Ping canceled.");
                Lbl_PingEdo.Text = "Cancelado";
                // Let the main thread resume. 
                // UserToken is the AutoResetEvent object that the main thread 
                // is waiting for.
                ((AutoResetEvent)e.UserState).Set();
            }

            // If an error occurred, display the exception to the user.
            if (e.Error != null)
            {
                Lbl_PingEdo.Text = "Falló";
                Console.WriteLine("Ping failed:");
                Console.WriteLine(e.Error.ToString());

                // Let the main thread resume. 
                ((AutoResetEvent)e.UserState).Set();
            }

            PingReply reply = e.Reply;
            if (reply.Status == IPStatus.Success)
            {
                Lbl_PingEdo.Text = "Responde";
            }


            // Let the main thread resume.
            ((AutoResetEvent)e.UserState).Set();
        }

        private static void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            // If the operation was canceled, display a message to the user.
            if (e.Cancelled)
            {

                Console.WriteLine("Ping canceled.");

                // Let the main thread resume. 
                // UserToken is the AutoResetEvent object that the main thread 
                // is waiting for.
                ((AutoResetEvent)e.UserState).Set();
            }

            // If an error occurred, display the exception to the user.
            if (e.Error != null)
            {
                Console.WriteLine("Ping failed:");
                Console.WriteLine(e.Error.ToString());

                // Let the main thread resume. 
                ((AutoResetEvent)e.UserState).Set();
            }

            PingReply reply = e.Reply;

            DisplayReply(reply);

            // Let the main thread resume.
            ((AutoResetEvent)e.UserState).Set();
        }

        public static void DisplayReply(PingReply reply)
        {
            if (reply == null)
                return;

            Console.WriteLine("ping status: {0}", reply.Status);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Address: {0}", reply.Address.ToString());
                Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
            }
        }

        private void Cmb_Marca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualizaModelos();
        }

        private void Cmb_Modelo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cmb_Modelo.SelectedIndex >= 0)
                Terminal.TERMINAL_MODELO = eClockBase.CeC.Convierte2Int(Cmb_Modelo.SelectedValue).ToString();
        }

        private void Chb_Usb_Unchecked(object sender, RoutedEventArgs e)
        {
            Tbx_IP.IsEnabled = true;
            TerminalDir.TipoConexion = eClockBase.Modelos.Terminales.TerminalDir.tipo.Red;
            Terminal.TERMINAL_DIR = TerminalDir.ObtenCadenaConexion();
        }

        private void Chb_Usb_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void Chb_Usb_Checked(object sender, RoutedEventArgs e)
        {
            Tbx_IP.IsEnabled = false;
            TerminalDir.TipoConexion = eClockBase.Modelos.Terminales.TerminalDir.tipo.USB;
            Terminal.TERMINAL_DIR = TerminalDir.ObtenCadenaConexion();
        }

        private void Tbx_Nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            Terminal.TERMINAL_NOMBRE = Tbx_Nombre.Text;
        }

    }
}
