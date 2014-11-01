using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase
{
    public class CeC_LogDestino
    {
        public static System.IO.StreamWriter StreamWriter = null;

        public enum CeC_Log_Almacenamiento
        {
            Archivo,
            EventLog,
            BaseDeDatos
        }

        public enum Tipo
        {
            Debug,
            Log,
            LogMsg,
            Error,
            ErrorMsg
        }

        public bool AutoNombreArchivo = false;

        public bool GuardarDebug = false;
        public bool GuardarLog = false;
        public bool GuardarError = true;

        /// <summary>
        /// Tiempo en dias que los archivos de log permaneceran
        /// </summary>
        public static int VigenciaLog = 15;
        public string NombreDestino = "CeC_Log.log";


        private CeC_Log_Almacenamiento m_TipoAlmacenamiento = CeC_Log_Almacenamiento.Archivo;

        public CeC_Log_Almacenamiento TipoAlmacenamiento
        {
            set
            {
                m_TipoAlmacenamiento = value;
                switch (m_TipoAlmacenamiento)
                {
                    case CeC_Log_Almacenamiento.Archivo: NombreDestino = "CeC_Log.log"; break;
                    case CeC_Log_Almacenamiento.EventLog: NombreDestino = "CeC_Log"; break;

                }

            }
            get { return m_TipoAlmacenamiento; }
        }


        private string ObtenNombreArchivoDin()
        {
            return "NombreArchivoDinamico.log";
            /*
            string Nombre = Application.ExecutablePath + " " + DateTime.Today.ToString("yy-MM-dd") + ".log";
            if (NombreDestino != Nombre)
            {
                NombreDestino = Nombre;
                BorraLogViejos();
            }
            return NombreDestino;*/
        }

        public virtual bool EscribeArchivo(string Mensaje)
        {
            if (StreamWriter != null)
            {
                StreamWriter.WriteLine(Mensaje);
                StreamWriter.Flush();
            }
            /*
            if (AutoNombreArchivo)
            {
                NombreDestino = ObtenNombreArchivoDin();
            }
            System.IO.StreamWriter sw;
            if (!System.IO.File.Exists(NombreDestino))
                sw = System.IO.File.CreateText(NombreDestino);
            else
                sw = System.IO.File.AppendText(NombreDestino);
            sw.WriteLine(MensajeFinal);
            sw.Close();*/
            return true;
        }
        public virtual bool EscribeBDatos(Tipo Tipo,  string Titulo, string Mensaje)
        {

            return true;
        }
        public virtual bool EscribeEventLog(Tipo Tipo,  string Titulo, string Mensaje)
        {
            return true;
        }
        public virtual bool MuestraMsg(Tipo Tipo, string Titulo, string Mensaje)
        {
            return true;
        }
        public bool Agrega(Tipo Tipo,  string Titulo, string Mensaje)
        {

            //try
            //{
            switch (m_TipoAlmacenamiento)
            {
                case CeC_Log_Almacenamiento.Archivo:
                    {
                        string TMens = "";
                        switch (Tipo)
                        {
                            case CeC_LogDestino.Tipo.Log: TMens += "Log->"; break;
                            case CeC_LogDestino.Tipo.Error: TMens += "Error->"; break;
                            case CeC_LogDestino.Tipo.Debug: TMens += "Debug->"; break;
                            case CeC_LogDestino.Tipo.ErrorMsg: TMens += "ErrorMsg->"; break;
                            case CeC_LogDestino.Tipo.LogMsg: TMens += "LogMsg->"; break;
                        }

                        string MensajeFinal = "";
                        MensajeFinal = TMens + "\"" + Titulo + "\" " + Mensaje + " " + DateTime.Now.ToString();
                        //System.Windows.Forms.MessageBox.Show(MensajeFinal);
                        {
                            EscribeArchivo(MensajeFinal);
                            //System.Windows.Forms.MessageBox.Show(MensajeFinal,NombreDestino);
                        }

                    }

                    break;

                case CeC_Log_Almacenamiento.EventLog:
                    EscribeEventLog(Tipo, Titulo, Mensaje);
                    break;
                case CeC_Log_Almacenamiento.BaseDeDatos:
                    EscribeBDatos(Tipo, Titulo, Mensaje);
                    break;


            }
            switch (Tipo)
            {
                case CeC_LogDestino.Tipo.ErrorMsg:
                case CeC_LogDestino.Tipo.LogMsg:
                    MuestraMsg(Tipo, Titulo, Mensaje);
                    break;
            }
            return true;
        }

        public virtual bool BorraLogViejos()
        {
/*
            try
            {
                string[] STemp = Application.ExecutablePath.Split('\\');
                string NArchivo = STemp[STemp.Length - 1] + " *.log";


                string[] Archivos = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), NArchivo);
                foreach (string RutaArchivo in Archivos)
                {
                    try
                    {
                        string Archivo = RutaArchivo.Substring(RutaArchivo.LastIndexOf('\\') + 1);
                        int Pos = Archivo.IndexOf(".log");//01-02-08.log
                        DateTime FechaArchivo = new DateTime(Convert.ToInt32(Archivo.Substring(Pos - 8, 2)), Convert.ToInt32(Archivo.Substring(Pos - 5, 2)), Convert.ToInt32(Archivo.Substring(Pos - 2, 2)));
                        if (FechaArchivo < DateTime.Today.AddDays(-VigenciaLog))
                        {
                            System.IO.File.Delete(RutaArchivo);
                        }
                    }
                    catch (Exception ex)
                    {
                        CeC_Log.AgregaError(ex);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                CeC_Log.AgregaError(ex);
            }
            return false;
 * */
            return true;
        }


    }
}
