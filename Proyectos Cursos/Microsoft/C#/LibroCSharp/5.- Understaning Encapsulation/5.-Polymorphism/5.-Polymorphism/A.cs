using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._Polymorphism
{
    public class A
    {
        public virtual void DoWork()
        { 
        }
    }

    public class B : A
    {
        public sealed override void DoWork()
        {
        }
    }

    public class C : B
    {
    }
}
