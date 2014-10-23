using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Polymorphic_Interface
{
    class Circle : Shape
    {
        public Circle()
        { 
        }

        public Circle(string Name): base(Name)
        { 
        }

        public override void Draw()
        {
            Console.WriteLine("Drawing a Circle {0}",PetName);
        }

        public void Split()
        {
            Console.WriteLine("Split Circle....");
        }
    }
}
