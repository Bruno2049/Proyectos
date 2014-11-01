using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Sesion
{
    public class Model_DatosPersona
    {

        public int PERSONA_ID { get; set; }
        public int PERSONA_LINK_ID { get; set; }
        public string PERSONA_NOMBRE { get; set; }
        public string PERSONA_EMAIL { get; set; }
        public string AGRUPACION_NOMBRE { get; set; }
       // public byte[] PERSONA_IMA { get; set; }
        public DateTime UltimaSesion { get; set; }
        public override string ToString()
        {
            return PERSONA_NOMBRE.ToString() + "(" + PERSONA_LINK_ID.ToString() + ")";
        }

    }
}
