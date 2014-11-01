using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace eClockSync5
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(eClockSync5.Properties.Settings.Default.lang);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (eClockSync5.Properties.Settings.Default.MostrarAsistente)
                Application.Run(new Frm_Asistente());
            else
            {
                if (eClockSync5.Properties.Settings.Default.ModoFoto)
                {
                    Frm_ModoFoto Dlg = new Frm_ModoFoto();
                    Application.Run(Dlg);
                }
                else
                {
                    Frm_Main Dlg = new Frm_Main();
                    Dlg.args = args;
                    Application.Run(Dlg);
                }
            }
        }
    }
}
