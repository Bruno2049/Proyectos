using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._KeywordThis
{
    class Motorcycle
    {
        public string driverName;
        public int driverIntensity;
        public void PopAWheely()
        {
            for (int i = 0; i <= driverIntensity; i++)
            {
                Console.WriteLine("Yeeeeeee Haaaaaeewww!");
            }
        }

        public void SetDriverName(string name)
        {
            this.driverName = name;
        }
        
        public Motorcycle() { }
        
        public Motorcycle(int intensity)
        {
            if (intensity > 10)
            {
                intensity = 10;
            }
            driverIntensity = intensity;
        }

        public Motorcycle(int intensity, string name)
        {
            if (intensity > 10)
            {
                intensity = 10;
            }
            driverIntensity = intensity;
            driverName = name;
        }

        //public Motorcycle(int intensity) : this(intensity, "") { }
        //public Motorcycle(string name) : this(0, name) { }
    }
}

