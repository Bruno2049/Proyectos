using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._Basic_Mechanics
{
    class Car
    {
        public readonly int MaxSpeed;
        public int CurrSpeed;

        public int Speed { 
            get
            {
                return CurrSpeed;
            }
            set 
            {
                CurrSpeed = MaxSpeed;
            }
        }

        public Car(int max)
        {
            MaxSpeed = max;
        }

        public Car ()
        {
            MaxSpeed = 55;
        }
    }
}
