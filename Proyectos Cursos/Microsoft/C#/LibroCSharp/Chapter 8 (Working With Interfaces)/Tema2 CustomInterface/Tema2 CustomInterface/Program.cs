using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema2_CustomInterface
{
    static class Program
    {
        public static void Main()
        {
            Hexagon Hexa = new Hexagon();
            Console.WriteLine("The Points: {0}\n", Hexa.Points);

            Circle Cir = new Circle("Lisa");
            Console.WriteLine("The name of the Circle is {0}\n", Cir.PetName);
            IPointy Point = null;

            try
            {
                Point = (IPointy)Cir;
                Console.WriteLine("Points: {0} \n", Point.Points);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("\n{0}\n", e.Message);
            }

            Hexagon Hexa2 = new Hexagon("Petter");
            IPointy Point2 = Hexa2 as IPointy;

            if (Point2 != null)
            {
                Console.WriteLine("Points of {0} is {1} \n", Hexa2.PetName, Hexa2.Points);
            }

            else
            {
                Console.WriteLine("No Points ... \n");
            }

            Shape[] Shapes = { new Hexagon(), new Circle(), new Triangle("Joe"), new Circle("JoJo"), Hexa2, new ThreeDCircle() };

            foreach (Shape item in Shapes)
            {
                item.Draw();
                if (item is IPointy)
                {
                    Console.WriteLine("The shape {0} have {1} \n", item.PetName, ((IPointy)item).Points);
                }

                else
                {
                    Console.WriteLine("{0} no points ...\n", item.PetName);
                }

                if (item is IDraw3D)
                {
                    DrawIn3D((IDraw3D)item);
                }
            }

            IPointy FirstPointy = FindFirstPointy(Shapes);

            Console.WriteLine("\n The fist Pointy is a {0}", FirstPointy);


            Console.WriteLine("\n***********************Check Explicit Interface********************************\n");

            Octagon Oct = new Octagon();

            IDrawToForm Itf = (IDrawToForm)Oct;

            Itf.Draw();
            if (Oct is IDrawToMemory)
            {
                ((IDrawToMemory)Oct).Draw();
            }
            ((IDrawToPrinter)Oct).Draw();

            Console.ReadLine();
        }

        public static void DrawIn3D(IDraw3D Interface)
        {
            Console.WriteLine("This shape can draw in 3D");
            Interface.Draw3D();
        }

        public static IPointy FindFirstPointy(Shape[] Shapes)
        {
            foreach (Shape item in Shapes)
            {
                if (item is IPointy)
                {
                    return item as IPointy;
                }

            }
            return null;
        }
    }
}
