using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    class Auto
    {
        private Radio MyRadio = new Radio();

        public void TurnOnRadio(bool OnOff)
        {
            MyRadio.Power(OnOff);
        }
    }
}
