using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;

using System.Runtime.InteropServices; // DllImport
using SharpSvn;

namespace eUpdater
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        bool is64BitProcess = (IntPtr.Size == 8);
        bool is64BitOperatingSystem = (IntPtr.Size == 8) || InternalCheckIsWow64();

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }

        public void mUpdate(object o)
        {
           /* Process svn = new Process();
            if(InternalCheckIsWow64())
                svn.StartInfo.FileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName),  "svn.exe");
            else
                svn.StartInfo.FileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), "x86\\svn.exe");
            svn.StartInfo.Arguments = "update --trust-server-cert --non-interactive --force --username subversion --password Actualizate-";
            svn.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            svn.Start();
            svn.WaitForExit();*/
            string[] args = Environment.GetCommandLineArgs();
            SvnClient client = new SvnClient();
            SvnUpdateResult result;
            SvnUpdateArgs UpdArgs = new SvnUpdateArgs();
            System.Net.NetworkCredential oCred = new System.Net.NetworkCredential("subversion", " Actualizate-");
            client.Authentication.DefaultCredentials = oCred; 
            client.Update("./", out result);

            this.Dispatcher.Invoke(new System.Action(() =>
            {
                Pgb_Estado.IsIndeterminate = false;
                Lbl_Informacion.Text = "El proceso de actualización del sistema ha terminado satisfactoriamente";
                
                if (args.Length > 1)
                {
                    Process svn = new Process();
                    svn = new Process();
                    svn.StartInfo.FileName = args[1];
                    svn.StartInfo.Arguments = "";
                    svn.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    svn.Start();
                }

            }), null);
            Thread.Sleep(2000);
            this.Dispatcher.Invoke(new System.Action(() =>
            {
                this.Close();
            }), null);
        }

        private void Window_ContentRendered_1(object sender, EventArgs e)
        {
            Pgb_Estado.IsIndeterminate = true;
            Thread svnt = new Thread(new ParameterizedThreadStart(mUpdate));
            svnt.SetApartmentState(ApartmentState.STA);
            svnt.IsBackground = true;
            svnt.Start();
        }
    }
}
