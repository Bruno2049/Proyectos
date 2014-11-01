using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
#if !NETFX_CORE
using System.Windows;
#else
using Windows.UI.Xaml;
#endif
using eClockBase;

namespace eClock5
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()            
        {
            eClockBase.CeC_Stream.MetodoStream = new CeC_StreamFile();
            CeC_LogDestino.StreamWriter = CeC_StreamFile.sAgregaTexto("eClock5.log");
            eClockBase.CeC_SesionBase Sesion = CeC_Sesion.ObtenSesion(this);

            /*Diseno MW = new eClock5.Diseno();
            MW.ShowDialog();

            /*
            */
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            // hook on error before app really starts
           // AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            base.OnStartup(e);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // put your tracing or logging code here (I put a message box as an example)
            MessageBox.Show(e.ExceptionObject.ToString());
        }
    }
}
