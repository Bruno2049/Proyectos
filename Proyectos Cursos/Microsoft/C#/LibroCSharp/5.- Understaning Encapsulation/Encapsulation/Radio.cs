using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    class Radio
    {
        public void Power(bool TurnOn)
        {
            Console.WriteLine("Radio on: {0}",TurnOn);
        }
    }
}
