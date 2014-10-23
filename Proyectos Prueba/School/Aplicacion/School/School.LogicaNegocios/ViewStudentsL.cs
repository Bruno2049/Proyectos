using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.AccesoDatos;
using School.Entidades;

namespace School.LogicaNegocios
{
    public class ViewStudentsL
    {
        private static readonly ViewStudentsL _ClassIntance = new ViewStudentsL();

        public static ViewStudentsL ClassIntance
        {
            get { return _ClassIntance; }
        }

        public ViewStudentsL()
        { 
        }

        public List<> GetListStudents()
        {
            return ViewStudentsA.ClassIntance.GetListStudents();
        }

    }
}
