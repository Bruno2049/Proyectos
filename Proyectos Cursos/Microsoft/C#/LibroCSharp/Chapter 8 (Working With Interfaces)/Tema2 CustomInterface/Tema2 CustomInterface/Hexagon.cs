using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_CustomInterface
{
    class Hexagon : Shape , IPointy , IDraw3D
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

        public byte Points
        {
            get { return 6; }
        }

        public byte GetNumberOfPoints()
        {
            return Points;
        }

        public void Draw3D()
        {
            Console.WriteLine("Drawing Hexagon in 3D!");
        }
    }
}
