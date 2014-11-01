using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase
{
    public class CeC_Log
    {
        public static string S_NombreDestino
        {
            get { return CeC_Log.LogIO.NombreDestino; }
            set { CeC_Log.LogIO.NombreDestino = value; }
        }



        public enum CeC_Log_TiposMens
        {
            Informacion,
            Atencion,
            Error
        }


        public static CeC_LogDestino LogIO = new CeC_LogDestino();
        public static void AgregaDebugF(string Funcion, string Mensaje, params object[] Parametros)
        {
            AgregaDebugF(Funcion, string.Format(Mensaje, Parametros));
        }
        public static void AgregaDebugF(string Funcion, string Mensaje)
        {

            AgregaDebug("Funcion = " + Funcion + " ; " + Mensaje);
        }

        public static void AgregaDebug(string Mensaje, params object[] Parametros)
        {
            AgregaDebug(string.Format(Mensaje, Parametros));
        }
        public static void AgregaDebug(string Mensaje)
        {
            LogIO.Agrega(CeC_LogDestino.Tipo.Debug, "", Mensaje);

        }
        public static void AgregaLogMsg(string Mensaje)
        {
            AgregaLogMsg(Mensaje, "Atención");
        }
        public static void AgregaLogMsg(string Mensaje, string Titulo)
        {
            LogIO.Agrega(CeC_LogDestino.Tipo.LogMsg, Titulo, Mensaje);
        }
        public static void AgregaLog(string Mensaje)
        {
            LogIO.Agrega(CeC_LogDestino.Tipo.LogMsg, "", Mensaje);
        }
        
        public static void AgregaError(string Mensaje)
        {
            LogIO.Agrega(CeC_LogDestino.Tipo.Error, "", Mensaje);
        }
        public static void AgregaError(Exception ex)
        {
            AgregaError("", "", ex);
        }
        public static void AgregaError(string Funcion, Exception ex)
        {
            AgregaError(Funcion, "", ex);
        }
        
        public static void AgregaError(string Funcion, string Mensaje, Exception ex)
        {
            string MensajeExtra = "";
            if (Funcion.Length > 0)
                MensajeExtra = "Funcion '" + Funcion + "' ";
            if (Mensaje.Length > 0)
                MensajeExtra += "Mensaje" + Mensaje;


            AgregaError(MensajeExtra + " \n Msg = " + ex.Message + "\n StackTrace = " + ex.StackTrace);
        }
        public static void AgregaErrorMsg(Exception ex)
        {
            LogIO.Agrega(CeC_LogDestino.Tipo.ErrorMsg, "", "Mensaje = " + ex.Message + "\n StackTrace = " + ex.StackTrace);

        }
        public static void AgregaErrorMsg(string Mensaje)
        {
            AgregaErrorMsg(Mensaje, "Error");
        }
        public static void AgregaErrorMsg(string Mensaje, string Titulo)
        {
            LogIO.Agrega(CeC_LogDestino.Tipo.ErrorMsg, Titulo, Mensaje);
        }

    }
}
