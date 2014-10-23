using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2._DetailsOfInheritance
{
    sealed class PTSalesPerson : SalesPerson
    {  
        public sealed override void DisplayStats()
        {
            base.DisplayStats();
        }
    }
}
