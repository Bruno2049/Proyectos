using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2._KeywordThis
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("***** This *****");
            // Make a Motorcycle with a rider named Tiny?
            Motorcycle c = new Motorcycle(5);
            c.SetDriverName("Tiny");
            c.PopAWheely();
            Console.WriteLine("Rider name is {0}", c.driverName);
        }
    }
}