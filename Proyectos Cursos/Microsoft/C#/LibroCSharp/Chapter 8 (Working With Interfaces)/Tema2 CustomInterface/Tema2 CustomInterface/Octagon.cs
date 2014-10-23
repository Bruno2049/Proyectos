using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tema2_CustomInterface
{
    public class Octagon : IDrawToPrinter, IDrawToForm, IDrawToMemory
    {
        void IDrawToPrinter.Draw()
        {
            Console.WriteLine("Drawing in Printer!");
        }

        void IDrawToForm.Draw()
        {
            Console.WriteLine("Drawing in From!");
        }

        void IDrawToMemory.Draw()
        {
            Console.WriteLine("Drawing in Memory!");
        }
    }
}
