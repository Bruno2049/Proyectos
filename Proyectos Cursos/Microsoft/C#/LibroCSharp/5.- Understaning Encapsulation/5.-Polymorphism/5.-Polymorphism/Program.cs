using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5._Polymorphism
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("***** Creacion de herencias *****\n");
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new Rectangle());
            shapes.Add(new Triangle());
            shapes.Add(new Circle());

            foreach (Shape Form in shapes)
            {
                Form.Draw();                
            }

            Console.WriteLine("***** Miebros Virtuales");

            DerivadeClass B = new DerivadeClass();
            //llama el metodo de una clase heredada

            BaseClass A = new BaseClass();
            //Llama el metodo de la clase base
            
        }
    }
}
