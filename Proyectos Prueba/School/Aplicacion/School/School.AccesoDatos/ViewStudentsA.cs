using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.AccesoDatos;

namespace School.AccesoDatos
{
    public class ViewStudentsA
    {
        private static readonly ViewStudentsA _ClassIntance = new ViewStudentsA();

        public static ViewStudentsA ClassIntance
        {
            get { return _ClassIntance; }
        }

        public ViewStudentsA()
        { 
        }

        SchoolDataDataContext _Context = new SchoolDataDataContext();

        public List<STUDENT> GetListStudents()
        {
            var List = (from S in _Context.STUDENT
                        select S).ToList();

            return List;
        }
    }
}
