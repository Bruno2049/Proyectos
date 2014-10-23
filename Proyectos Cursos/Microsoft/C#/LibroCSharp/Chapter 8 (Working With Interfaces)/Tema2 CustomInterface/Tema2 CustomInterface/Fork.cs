using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_CustomInterface
{
    class Fork : IPointy
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
