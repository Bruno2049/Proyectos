using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForAndForech
{
    static class Program
    {       
        static void Main()
        {
            Console.WriteLine("Este ejemplo te mostrara el comportamiento de for");
            for (int i = 0; i < 4; i++)
            {
                // Nota i es solo visible en el ambito de el loop for
                Console.WriteLine("El numero es : {0} ", i);
            }
            // "i" no es visible en esta parte.

            Console.ReadLine();
            Console.WriteLine("Este ejemplo te mostrara el funcionaminto de foreach");

            string[] Modelos = { "Ford", "BMW", "Yugo", "Honda" };
            foreach (string c in Modelos)
            {
                Console.WriteLine(c);
            }
                
            int[] myInts = { 10, 20, 30, 40 };
            foreach (int i in myInts)
                Console.WriteLine(i);

            Console.WriteLine("Ejemplo con Forech y Link Query");
            int[] numbers = { 10, 20, 30, 40, 1, 2, 3, 8 };
            // LINQ query!
            var subset = from i in numbers where i < 10 select i;
            Console.Write("Values in subset: ");
            foreach (var i in subset)
            {
                Console.Write("{0} ", i);
            }

            Console.WriteLine("Ejemplos con while");

            string Respuesta = "";

            while (Respuesta.ToUpper() != "SI")
            {
                Console.WriteLine(@"Deceas salir del Loop [Si\No]: ");
                Respuesta = Console.ReadLine();
            }

            Console.WriteLine("Ejemplo con do while");
            do
            {
                Console.WriteLine(@"Deceas salir del Loop [Si\No]: ");
                Respuesta = Console.ReadLine();
            }
            while (Respuesta.ToUpper() != "SI");

            Console.WriteLine("");
        }
    }
}
