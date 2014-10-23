using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Polymorphic_Interface
{
    class ThreeDCircle : Circle
    {
        protected new string S_PetName;

        public new string PetName
        {
            get { return S_PetName; }
            set { S_PetName = value; }
        }        

         ///<summary>
         ///Con el modificardor new el metodo split se esconde de la clase base y con esto se puede implemetar el metodo de nuevo
         ///</summary>
        public new void Split()
        {
            Console.WriteLine("Split 3D Circle...");
        }
    }
}
