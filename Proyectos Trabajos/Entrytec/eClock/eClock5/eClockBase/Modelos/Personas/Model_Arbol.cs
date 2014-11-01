using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Personas
{
    public class Model_Arbol
    {
        public string Agrupacion { get; set; }
        public string AGRUPACION_NOMBRE { get; set; }
        public List<Model_ArbolPersona> Personas { get; set; }
        public List<Model_Arbol> Agrupaciones { get; set; }
        public Model_Arbol()
        {
            Personas = new List<Model_ArbolPersona>();
            Agrupaciones = new List<Model_Arbol>();
        }
    }
    public class Model_ArbolPersona
    {
        public int PERSONA_ID { get; set; }
        public string PERSONA_NOMBRE { get; set; }
        public int PERSONA_LINK_ID { get; set; }
        public string AGRUPACION_NOMBRE { get; set; }
    }

}
