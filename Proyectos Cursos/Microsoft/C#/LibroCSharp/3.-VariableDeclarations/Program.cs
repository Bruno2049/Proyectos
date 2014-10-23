using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3._VariableDeclarations
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Declaracion de variables:");
            int myInt = 0;
            string myString;
            myString = "Estos son las variables actuales";
            bool b1 = true, b2 = false, b3 = b1;
            System.Boolean b4 = false;
            DateTime Time = new DateTime();
            Console.WriteLine("Los valores de la varialbes son: {0}, {1}, {2}, {3}, {4}, {5}, {6}",myInt, myString, b1, b2, b3, b4,Time);
            Console.WriteLine();

            Console.WriteLine("=> System.Object Functionality:");
            Console.WriteLine("12.GetHashCode() = {0}", 12.GetHashCode());
            Console.WriteLine("12.Equals(23) = {0}", 12.Equals(23));
            Console.WriteLine("12.ToString() = {0}", 12.ToString());
            Console.WriteLine("12.GetType() = {0}", 12.GetType());
            Console.WriteLine();
        }
    }
}
