using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_CustomException
{
    public class CrearLog
    {
        public StreamReader Lector;
        public StreamWriter Escritor;
        public List<string> ContenidoLog = null;
        public TipoMensage TipoMensage;

        public CrearLog()
        {
            try
            {
                Lector = new StreamReader("Log.txt");
                Lector.Close();
            }
            catch (Exception)
            {
                Escritor = new StreamWriter("Log.txt");
                Escritor.WriteLine("Este Log esta creado para el registro de errores, informacion y debug de el software" + System.Environment.NewLine);
                Escritor.WriteLine("Fecha de creacion: " + DateTime.Now.ToString() + System.Environment.NewLine);
                Escritor.WriteLine(System.Environment.NewLine + "****************************************************************************************************************************************" + System.Environment.NewLine + System.Environment.NewLine);
                Escritor.Close();
                Escritor = null;
                Lector = null;
            }
        }

        public void GuardaError(TipoMensage Tipo, string Mensage, Exception e)
        {
            ContenidoLog = new List<string>();
            Lector = new StreamReader("Log.txt");
            if (Lector != null)
            {
                while (!Lector.EndOfStream)
                {
                    string Linea = Lector.ReadLine();                    
                    ContenidoLog.Add(Linea);
                }
                Lector.Close();
            }

            if (TipoMensage.Excepcion == Tipo)
            {
                Escritor = new StreamWriter("Log.txt");

                foreach (string Lineas in ContenidoLog)
                {
                    Escritor.WriteLine(Lineas);
                }

                Escritor.WriteLine(System.Environment.NewLine + "Excepcion => " + Mensage + " Fecha: " + DateTime.Now.ToString() + System.Environment.NewLine + ", Informacion de Excepcion:" + System.Environment.NewLine +
                    "Member name: " + e.TargetSite + System.Environment.NewLine + " Class defining member: " + e.TargetSite.DeclaringType + System.Environment.NewLine + "Member type: "
                    + e.TargetSite.MemberType + System.Environment.NewLine + "Message: " + e.Message + System.Environment.NewLine + "Source: " + e.Source + System.Environment.NewLine + "Stack: " + e.StackTrace
                     );
                foreach (System.Collections.DictionaryEntry de in e.Data)
                {
                    Escritor.WriteLine(System.Environment.NewLine + "Data.Key: " + de.Key.ToString() + "Data.Value: " + de.Value.ToString());
                }
                Escritor.Close();
            }
        }

    }

    public enum TipoMensage
    {
        Error, Informacion, Excepcion
    }
}
