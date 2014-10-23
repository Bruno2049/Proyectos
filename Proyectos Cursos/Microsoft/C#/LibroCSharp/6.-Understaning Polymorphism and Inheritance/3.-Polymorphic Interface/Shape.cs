using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3._Polymorphic_Interface
{
    public abstract class Shape
    {
        #region Atributos y campos

        protected string S_PetName;

        public string PetName
        {
            get { return S_PetName; }
            set { S_PetName = value; }
        }

        #endregion

        #region Constructores

        public Shape()
        {
            PetName = "No name";
        }

        public Shape(string Name)
        {
            PetName = Name;
        }

        #endregion

        #region Metodos
        
        public abstract void Draw();
        
        #endregion
    }
}
