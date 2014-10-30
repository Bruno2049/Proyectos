using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebasListado.Modelos;

namespace PruebasListado.Clases
{
    public class ListarPersonas
    {
        public List<Personas> Personas
        {
            get { return _personas; }
            set { _personas = value; }
        }


        private  List<Personas> _personas = new List<Personas>();

        public void LlenaLista()
        {
            _personas.Add(new Personas { Nombre = "Esteban", ApellidoP = "Cruz", ApellidoM = "Lagunes", Edad = 27 });
            _personas.Add(new Personas { Nombre = "Ana", ApellidoP = "Martinez", ApellidoM = "Lopez", Edad = 21 });
            _personas.Add(new Personas { Nombre = "Sara", ApellidoP = "Rios", ApellidoM = "velazquez", Edad = 19 });
            _personas.Add(new Personas { Nombre = "Oswaldo", ApellidoP = "Carillo", ApellidoM = "Arellano", Edad = 37 });
        }

        public ListarPersonas()
        {
            LlenaLista();
        }
    }
}
