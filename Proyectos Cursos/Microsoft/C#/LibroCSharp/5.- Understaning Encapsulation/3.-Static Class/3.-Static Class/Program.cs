using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3._Static_Class
{
    static class Program
    {
        static void Main()
        {
            TimeUtilClass.PrintTime();
            TimeUtilClass.PrintDate();
            
            //ERROR:
            // No puedes crear una classe estatica
            
            //TimeUtilClass u = new TimeUtilClass();
            Console.ReadLine();

        }
    }
}
