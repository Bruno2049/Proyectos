using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7._EqualityAndRelationalOperators
{
    static class Program
    {
        static void Main()
        {
            /* This is illegal, given that Length returns an int, not a bool.
            string stringData = "My textual data";
            
            if(stringData.Length)
            {
                Console.WriteLine("string is greater than 0 characters");
            }
             * 
             * Esto ya no se puede realizar en C#
            */
            // Legal, as this resolves to either true or false.
            Console.WriteLine("Ejemplo con if");
            string Consulta = "Tiene caracteres";
            if(Consulta.Length > 0)
            {
                Console.WriteLine("Esta string tiene mas de 0 caracteres");
            }

            Console.WriteLine("Ejemplo con switch");
            Console.WriteLine("1 [C#], 2 [VB]");
            Console.Write("Escoje el lenguaje que te gusta: ");
            string langChoice = Console.ReadLine();
            int n = int.Parse(langChoice);
            switch (n)
            {
                case 1:
                    Console.WriteLine("Buena decicion C# es un gran lenguaje.");
                    break;
                case 2:
                    Console.WriteLine("VB Bien ahora es POO y multihilo !");
                    break;
                default:
                    Console.WriteLine("Bueno.. mucha suerte!");
                    break;
            }

            Console.WriteLine("Otro ejemplo con switch");

            Console.Write("Enter your favorite day of the week: ");
            DayOfWeek favDay;
            try
            {
                favDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Bad input!");
                return;
            }
            switch (favDay)
            {
                case DayOfWeek.Friday:
                    Console.WriteLine("Yes, Friday rules!");
                    break;
                case DayOfWeek.Monday:
                    Console.WriteLine("Another day, another dollar");
                    break;
                case DayOfWeek.Saturday:
                    Console.WriteLine("Great day indeed.");
                    break;
                case DayOfWeek.Sunday:
                    Console.WriteLine("Football!!");
                    break;
                case DayOfWeek.Thursday:
                    Console.WriteLine("Almost Friday...");
                    break;
                case DayOfWeek.Tuesday:
                    Console.WriteLine("At least it is not Monday");
                    break;
                case DayOfWeek.Wednesday:
                    Console.WriteLine("A fine day.");
                    break;
            }
        }
    }
}
