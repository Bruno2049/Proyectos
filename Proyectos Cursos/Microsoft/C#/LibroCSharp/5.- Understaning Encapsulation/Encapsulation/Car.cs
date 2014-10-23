using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    class Car
    {
        public Car()
        {
            petName = "Chuck";
            currSpeed = 10;
        }

        // Here, currSpeed will receive the
        // default value of an int (zero).
        public Car(string pn)
        {
            petName = pn;
        }
        // Let caller set the full state of the Car.
        public Car(string pn, int cs)
        {
            petName = pn;
            currSpeed = cs;
        }

        public string petName;
        public int currSpeed;

        public void PrintState()
        {
            Console.WriteLine("{0} is going {1} MPH.", petName, currSpeed);
        }
        public void SpeedUp(int delta)
        {
            currSpeed += delta;
        }
    }
}
