using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6._Access_Modifiers
{
    static class Program
    {
        
        static void Main()
        {
            Console.WriteLine("***** Accesibilidad de struc y clases anidados *****");
            Bicycle A = new Bicycle();
            A.Color = "Rojo";
            A.Pedal();

            Console.WriteLine("***** Operador de referencia");
            Fraction a = new Fraction(1, 2);
            Fraction b = new Fraction(3, 7);
            Fraction c = new Fraction(2, 3);
            Console.WriteLine("El resultado de fracciones (a * b + c) = {0}" , (double)(a * b + c));
            Console.Read();
        }
    }
}
