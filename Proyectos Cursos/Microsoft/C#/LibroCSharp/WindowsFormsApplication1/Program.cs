using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        public static void Main()
        {
            Console.WriteLine("=> Convercion de datos:");
            bool b = bool.Parse("True");
            Console.WriteLine("Valor de b: {0}", b);
            double d = double.Parse("99.884");
            Console.WriteLine("Valor of d: {0}", d);
            int i = int.Parse("8");
            Console.WriteLine("Valor de i: {0}", i);
            char c = Char.Parse("w");
            Console.WriteLine("Valor de  c: {0}", c);
            Console.WriteLine();

            Console.WriteLine("=> Fecha y Tiempos:");
            // Este Constructor es (Año, Mes, Dia).
            DateTime dt = new DateTime(2011, 10, 17);
            // Que mes es este?
            Console.WriteLine("El dia de hoy {0} es {1}", dt.Date, dt.DayOfWeek);
            // Este mes es Diciembre.
            dt = dt.AddMonths(2);
            Console.WriteLine("Es Horario de verano: {0}", dt.IsDaylightSavingTime());
            // Este Constructor es (hours, minutes, seconds).
            TimeSpan ts = new TimeSpan(4, 30, 0);
            Console.WriteLine(ts);
            // Resta 15 minutos al TimeSpan y
            // imprime el resultado.
            Console.WriteLine(ts.Subtract(new TimeSpan(0, 15, 0)));
        }
    }
}
