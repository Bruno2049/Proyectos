using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RecogSys.RdrAccess;


namespace eClockSync
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string [] Parametros)
        {

            /*CeTerminalSync T = new CeTerminalSync();
            T.CargaCambiosPersonas();
            T.ConfirmaEmpleado(1, "1234");
            T.GuardaCambiosPersonas();*/


       /*     FrmZkSoft m_FrmZK = new FrmZkSoft();
            m_FrmZK.Show();
//            m_FrmZK.axCZKEM1 = m_FrmZK.axCZKEM1;
            m_FrmZK.axCZKEM1.Connect_Net("192.168.17.201", 4370);

*/

            //Application.Run(new FrmZkSoft());
            
            //Thread.CurrentThread.ApartmentState = ApartmentState.STA; 
            /*IsProtectServer.CIs_PS PS = new IsProtectServer.CIs_PS();
            if (!PS.Coinciden(IsProtectServer.CIs_PS.Productos.eClockSync))
            {
                IsProtectServer.F_Activar Act = new IsProtectServer.F_Activar();
                Act.AsignaProducto(IsProtectServer.CIs_PS.Productos.eClockSync);
                if (Act.ShowDialog() != DialogResult.OK)
                    return;
            }*/
#if !SERVICIO
           /* CeZucchetti Zuc = new CeZucchetti();
            CeZucchetti.InitCOMM(Convert.ToInt16(8499), Convert.ToInt16(CeZucchetti.ISZTipoConexion.WSOCK), 0, 0);

            Zuc.ConectaIP("192.168.150.102");
            Zuc.CreaDirectorio("Temporal\\");
            Zuc.PoleoChecadas();
            Zuc.DescargaArchivo("FINGER");
            CeZucchetti.ExitCOMM();*/
            try
            {
                foreach (string Parametro in Parametros)
                {
                    switch (Parametro.ToLower())
                    {
                        case "-i":
                        case "/i":
                            
                            string Ruta = Application.ExecutablePath;
                            Process Pr = Process.Start("\"" + Ruta + "\"", "-e");
                            return;
                            break;
                        case "-e":
                        case "/e":
                            while (true)
                            {
                                try
                                {
                                    eClockSync.WS_eCheck.WS_eCheck WS = new eClockSync.WS_eCheck.WS_eCheck();
                                    DateTime FechaHora = WS.ObtenFechaHora();
                                    break;
                                }
                                catch {}
                                System.Threading.Thread.Sleep(5000);
                            }
//                            return;
                            break;
                    }
                }
                /*
                int R = CeZucchetti.InitCOMM(1,Convert.ToInt16(CeZucchetti.ISZTipoConexion.PROX), 9600, 0);
                R = CeZucchetti.SetTerminal(0);
                //%Kÿü54321ÿÿ4343ÿ
                R = CeZucchetti.PostCommand(new byte[] { (byte)'%', (byte)'K', 255, 129, (byte)'5', 
                    (byte)'4', (byte)'3', (byte)'2', (byte)'1', 255, 255, (byte)'4', (byte)'3', 
                    (byte)'2', (byte)'1', 255 });
                R = CeZucchetti.PostCommand("@MEM 6619 0");
                // %E
                R = CeZucchetti.PostCommand(new byte[] { (byte)'%', (byte)'E', 14 });

                R = CeZucchetti.PostCommand("%S Syncro");
                R = CeZucchetti.ExitCOMM();*/
                
                CeClockSync Sync = new CeClockSync();
                Sync.Sincroniza();
            }
            catch 
            {

            }
#else
            ServiceBase[] ServicesToRun;

            // Se puede ejecutar más de un servicio de usuario dentro del mismo proceso. Para agregar
            // otro servicio a este proceso, cambie la siguiente línea para
            // crear un segundo objeto de servicio. Por ejemplo,
            //
            //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            ServicesToRun = new ServiceBase[] { new SrveClockSync() };

            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}