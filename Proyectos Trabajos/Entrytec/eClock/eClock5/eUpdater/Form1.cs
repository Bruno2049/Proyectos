using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SharpSvn;
using System.Diagnostics;
using System.Runtime.InteropServices; // DllImport

namespace eUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        string Directorio;
        private void Form1_Load(object sender, EventArgs e)
        {
            Directorio = System.IO.Directory.GetCurrentDirectory();
           // MessageBox.Show(Directorio);
            backgroundWorker1.RunWorkerAsync();
            //backgroundWorker1_DoWork(null, null);
        } 

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            string[] args = Environment.GetCommandLineArgs();
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
            this.Close();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }  

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string[] args = Environment.GetCommandLineArgs();
                SvnClient client = new SvnClient();
                SvnUpdateResult result;
                SvnUpdateArgs UpdArgs = new SvnUpdateArgs();
                System.Net.NetworkCredential oCred = new System.Net.NetworkCredential("subversion", "Actualizate-");
                client.Authentication.DefaultCredentials = oCred;
                client.Progress += client_Progress;

                client.Update(Directorio, out result);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        void client_Progress(object sender, SvnProgressEventArgs e)
        {
            try
            {
                if (e.Progress > e.TotalProgress)
                    Pbar.Maximum = Convert.ToInt32(e.Progress + 1);
                //Pbar.Maximum = Convert.ToInt32(e.TotalProgress);
                Pbar.Value = Convert.ToInt32(e.Progress);
            }
            catch { }
        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
