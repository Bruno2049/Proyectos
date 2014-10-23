using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3._Polymorphic_Interface
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("****** Polymorphic Interface ******\n");
            Shape[] MyShapes = { new Circle("Beth"), new Hexagon(), new Circle(), new Hexagon("Hexa") };

            foreach (Shape Shapes in MyShapes)
            {
                Shapes.Draw();
            }

            ThreeDCircle C = new ThreeDCircle();
            C.Split();
            ((Circle)C).Split();
        }
    }
}
