using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2._DetailsOfInheritance
{
    public class BenefitPackage
    {
        public enum BenefitPackegeLevel
        {
            Standard, Silver, Gold, Platinium,
        }
        /// <summary>
        /// Assume we have members that represent Dental/Medical Benefits, and so on
        /// </summary>
        public double ComputerPayDeduction()
        {
            return 125.0;
        }
    }    
}
