using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Personas
{
    public class Model_PersonasBusqueda
    {
        public int PERSONA_ID { get; set; }
        public string PERSONA_DATO { get; set; }
        public int PERSONA_LINK_ID { get; set; }
        public string PERSONA_NOMBRE { get; set; }
        public string CAMPO_ETIQUETA { get; set; }
        public string CAMPO_NOMBRE { get; set; }
        public override string ToString()
        {
            return PERSONA_NOMBRE.ToString() + "(" + PERSONA_LINK_ID.ToString() + ")";
        }
    }
}
