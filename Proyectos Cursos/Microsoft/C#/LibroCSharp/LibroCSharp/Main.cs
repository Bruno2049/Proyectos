using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibroCSharp
{
    static class AplicacionesConsola
    {     
        static void Main()
        {
            Console.WriteLine("Revicion de detalles del equipo");
            foreach (string Drive in Environment.GetLogicalDrives())
            {
                Console.WriteLine("Diver {0}\n", Drive);
            }

            Console.WriteLine("La vercion de el sistema operativo: {0}\n",Environment.OSVersion);
            Console.WriteLine("Numero de procesadores: {0}\n", Environment.ProcessorCount);
            Console.WriteLine("La version de el .NET: {0}", Environment.Version);
        }
    }
}
