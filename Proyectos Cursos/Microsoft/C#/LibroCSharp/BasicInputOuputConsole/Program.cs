using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicInputOuputConsole
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Ingresa tu nombre: ");
            string Nom = Console.ReadLine();
            Console.WriteLine("Ingresa tu Edad: ");
            int Edad = Convert.ToInt32(Console.ReadLine());

            ConsoleColor Previo = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tu nombre es {0} y tu edad es {1}", Nom, Edad);
            Console.ForegroundColor = Previo;
        }
    }
}
