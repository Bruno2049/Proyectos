using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eClockBase;
using eClockReports.BaseModificada;

namespace eClockReports.BaseModificada
{
    public class CeC_Main
    {
        static bool Iniciado = false;
        public static void Iniciar()
        {
            if (Iniciado)
                return;
            Iniciado = true;
            eClockBase.CeC_Stream.MetodoStream = new CeC_StreamFileLogs();
            CeC_LogDestino.StreamWriter = CeC_StreamFileLogs.sAgregaTexto("eClockReports" + DateTime.Now.ToString(" yyMMdd HHmmss") + ".log");
            eClockBase.CeC_Stream.MetodoStream = new CeC_StreamFile();
            eClockBase.CeC_Log.AgregaLog("Iniciando App...");
        }
    }
}