using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tema2_CustomInterface
{
    public class PitchFork : IPointy
    {
        public byte Points
        {
            get { throw new NotImplementedException(); }
        }

        public byte GetNumberOfPoints()
        {
            throw new NotImplementedException();
        }
    }
}
