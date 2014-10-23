using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_CustomInterface
{
    class Triangle : Shape , IPointy
    {
        public Triangle()
        { }

        public Triangle(string Name) : base  (Name)
        { }

        public override void Draw()
        {
            Console.WriteLine("The name of Triangle is {0}",PetName);
        }

        public byte Points
        {
            get { return 3; }
        }

        public byte GetNumberOfPoints()
        {
            return Points;
        }
    }
}
