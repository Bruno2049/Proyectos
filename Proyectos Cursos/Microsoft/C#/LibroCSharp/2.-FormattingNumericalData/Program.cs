using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2._FormattingNumericalData
{
    static class Program
    {
        public static void Main()
        {
            Console.WriteLine("El valor 99999 en varios Formatos:");
            Console.WriteLine("Cantidad monetaria en dolares c format: {0:c}", 99999);
            Console.WriteLine("Cantidad en 9 espacios d9 format: {0:d9}", 99999);
            Console.WriteLine("Cantidad Float con tres decimales f3 format: {0:f3}", 99999);
            Console.WriteLine("Formato numero estandar n format: {0:n}", 99999);
            Console.WriteLine("Formato en exponecial E format: {0:E}", 99999);
            Console.WriteLine("e format: {0:e}", 99999);
            Console.WriteLine("Formato Hexagesimal X format: {0:X}", 99999);
            Console.WriteLine("x format: {0:x}", 99999);

            string userMessage = string.Format("100000 En Hexagesimaa es {0:X}",100000);
            MessageBox.Show(userMessage);
        }
    }
}
