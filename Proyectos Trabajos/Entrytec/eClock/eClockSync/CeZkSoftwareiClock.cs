using System;
using System.Collections.Generic;
using System.Text;

namespace eClockSync
{
    class CeZkSoftwareiClock : CeZkSoftware
    {
        public override bool Conecta()
        {
            AgregaLog("Conectando iClock");
            EsiClock = true;
            return base.Conecta();
        }
    }
}
