using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Polymorphic_Interface
{
    class Hexagon : Shape
    {

        public Hexagon(string Name)
            : base(Name)
        { 
        }

        public Hexagon():base()
        {
            PetName = "Hexa no Name";
        }

        public override void Draw()
        {
            Console.WriteLine("Drawing {0} the hexagon", PetName);
        }
    }
}
