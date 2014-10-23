using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1._Basic_Mechanics
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("***** Basic Inheritance ******\n");
            Car MyCar = new Car(120);
            MyCar.Speed = 50;
            Console.WriteLine("My car is going {0} MPH", MyCar.Speed);
            MiniVan Van = new MiniVan();
            Van.Speed = 120;
            Console.WriteLine("My Minivan is going {0} MPH", Van.Speed);
        }
    }
}
