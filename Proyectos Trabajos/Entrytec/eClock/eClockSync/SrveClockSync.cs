using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace eClockSync
{
    public partial class SrveClockSync : ServiceBase
    {
        CeClockSync Sync = new CeClockSync();
        public SrveClockSync()
        {
            InitializeComponent();
            this.ServiceName = "Sincronizador de IsTime";
            this.EventLog.Log = "Application";
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = false;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            Sync.Iniciar(args);
        }
        protected override void OnStop()
        {
            Sync.Parar();
        }
    }
}
