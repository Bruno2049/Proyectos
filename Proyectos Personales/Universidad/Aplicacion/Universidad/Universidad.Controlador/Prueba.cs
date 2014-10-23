using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Controlador.SVR_Prueba;

namespace Universidad.Controlador
{
    public class Prueba
    {
        private static readonly Prueba _classInstance = new Prueba();

        public static Prueba ClassInstance
        {
            get { return _classInstance; }
        }

        public Prueba()
        {
        }

        public int prueba()
        {
            SVR_Prueba.S_PruebaClient aClient= new S_PruebaClient();
            int A = aClient.Prueba();
            return A;
        }
    }
}
