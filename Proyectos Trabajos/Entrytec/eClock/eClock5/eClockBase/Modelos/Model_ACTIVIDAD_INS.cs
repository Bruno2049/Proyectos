using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_ACTIVIDAD_INS
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ACTIVIDAD_INS_ID { get; set; }
        //Identificador unico de la actividad.
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int ACTIVIDAD_ID { get; set; }
        //Identificador unico de la persona.
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID { get; set; }
        //Identificador unico deL tipo de inscripción.
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INSCRIPCION_ID { get; set; }
        //
        [Campo_FechaHora(true, false, "2006-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime ACTIVIDAD_INS_FECHA { get; set; }
        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ACTIVIDAD_INS_DESCRIPCION { get; set; }
    }
}
