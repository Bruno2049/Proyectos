using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Campos
{
    public class Model_CampoTexto
    {
        public string Nombre { get; set;}
        public string Etiqueta { get; set; }
        public int TIPO_DATO_ID { get; set; }
        public bool Requerido { get; set; }
    }
}
