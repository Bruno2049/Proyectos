using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Incidencias
{
    public class Model_Incidencias
    {
        public DateTime FInicio { get; set; }
        public DateTime FFin { get; set; }
        public int Persona_Link_ID { get; set; }
        public int TipoIncidenciaID { get; set; }
        public string Comentario { get; set; }
        public string Abreviatura { get; set; }
        public string Nombre { get; set; }
    }
}
