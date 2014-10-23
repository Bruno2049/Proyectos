using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._Polymorphism
{
    public class BaseClass
    {
        public virtual int WorkProperty
        {
            get { return 0; }
        }
    }

    public class DerivadeClass : BaseClass
    {
    }
}
