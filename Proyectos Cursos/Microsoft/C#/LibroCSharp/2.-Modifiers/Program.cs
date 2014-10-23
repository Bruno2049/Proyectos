using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2._Modifiers
{
    static class Program
    {
        //Metodo con modificador out
        static void suma (int x, int y, out int Respuesta)
        {
            Respuesta = x + y;
        }

        //Metodo con multiples retornos
        static void Valores (out int Numero, out string Caracter, out bool Boleano)
        {
            Numero = 9;
            Caracter = "Tu cadena";
            Boleano = true;
        }
        //paprametros en ref o referencia

        public static void CambioString(ref string S1, ref string S2)
        {
            string StrgTemp = S1;
            S1 = S2;
            S2 = StrgTemp;
        }

        // paramentros con params

        static double CalculaPromedio(params double[] values)
        {
            Console.WriteLine("Tu enviaste {0} doubles.", values.Length);
            double sum = 0;
            if (values.Length == 0)
                return sum;
            for (int i = 0; i < values.Length; i++)
                sum += values[i];
            return (sum / values.Length);
        }

        //definicion de parametros opcionales
        static void InsertLogData(string Message, string Owner = "Programmer")
        {
            Console.Beep();
            Console.WriteLine("Error: {0}", Message);
            Console.WriteLine("Owner of error: {0}", Owner);
        }

       //Invocando metodos usando Named Parameters
        static void DisplayFancyMessage(ConsoleColor textColor,ConsoleColor backgroundColor, string message)
        {
            // Store old colors to restore after message is printed.
            ConsoleColor oldTextColor = Console.ForegroundColor;
            ConsoleColor oldbackgroundColor = Console.BackgroundColor;
            // Set new colors and print message.
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);
            // Restore previous colors.
            Console.ForegroundColor = oldTextColor;
            Console.BackgroundColor = oldbackgroundColor;
        }
        
        // metodos sobrecargados
        static int Add(int x, int y)
        { 
            return x + y;
        }
        
        static double Add(double x, double y)
        {
            return x + y; 
        }
        static long Add(long x, long y)
        { 
            return x + y;
        }

        //metodo Main
        static void Main()
        {
            Console.WriteLine("Ejemplo con el modificador de parametros out");
            
            int Respuesta; // Se debe declarar antes de usar como out
            suma(90, 80, out Respuesta);
            Console.WriteLine("La suma de 90 y 80 es {0}", Respuesta);

            Console.WriteLine("Otro ejemplo de out ref es el siguiente con multiple retornos");            
            int i; string str; bool b;
            Valores(out i, out str, out b); // ejecuta metodo valores Valores
            Console.WriteLine("Int is: {0}", i);
            Console.WriteLine("String is: {0}", str);
            Console.WriteLine("Boolean is: {0}", b);

            Console.WriteLine("Ejemplo de mofidicador Ref con el cambio de string");
            string S1, S2;
            S1 = "Primera String";
            S2 = "Segunda String";
            Console.WriteLine("S1 = {0} y S2 = {1}",S1,S2);
            CambioString(ref S1, ref S2);
            Console.WriteLine("S1 = {0} y S2 = {1}", S1, S2);

            Console.WriteLine("Ejemplo con el modificador param calculando un promedio ");
            double average;
            // Pasando delimitadores con comas
            average = CalculaPromedio(4.0, 3.2, 5.7, 64.22, 87.2);
            Console.WriteLine("El promedio de el dato es: {0}", average);
            // O pasando un arreglo con doubles.
            double[] data = { 4.0, 3.2, 5.7 };
            average = CalculaPromedio(data);
            Console.WriteLine("El promedio de el dato es: {0}", average);
            // Average of 0 is 0!
            Console.WriteLine("El promedio de el dato es: {0}", CalculaPromedio());
            Console.ReadLine();
            //Ejemplo de definicion de parametros opcionales
            Console.WriteLine("Ejemplo para la definicion de paramentros opcionales");
            InsertLogData("No se pudo resivir el dato");
            InsertLogData("No se pudo ingresar usuario", "UFC");

            //Ejemplo para invocar metodos con mamen parameters
            Console.WriteLine("Ejemplo para invocar metodos con mamen parameters");
            DisplayFancyMessage(message: "Wow! Very Fancy indeed!",
            textColor: ConsoleColor.DarkRed,
            backgroundColor: ConsoleColor.White);
            DisplayFancyMessage(backgroundColor: ConsoleColor.Green,message: "Testing...",textColor: ConsoleColor.DarkBlue);
            Console.ReadLine();

            //metodos sobrecargados o overloaded
            Console.WriteLine("Ejemplo metodos sobrecargados\n");
            // Calls int version of Add()
            Console.WriteLine(Add(10, 10));
            // Calls long version of Add()
            Console.WriteLine(Add(900000000000, 900000000000));
            // Calls double version of Add()
            Console.WriteLine(Add(4.3, 4.4));

        }
    }
}
