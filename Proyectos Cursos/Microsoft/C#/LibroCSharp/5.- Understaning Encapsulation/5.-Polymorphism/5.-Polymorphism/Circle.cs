﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._Polymorphism
{
    class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a Circle");
            base.Draw();
        }
    }
}
