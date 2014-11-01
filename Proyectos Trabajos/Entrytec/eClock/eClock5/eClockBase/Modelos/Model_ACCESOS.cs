using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_ACCESOS
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ACCESO_ID { get; set; }
        //Indica de que persona se refiere el acceso
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID { get; set; }
        //Terminal en la que ocurrio dicho acceso, si es 0 significa que fue una justificacion
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TERMINAL_ID { get; set; }
        //Fecha y hora en la que ocurrio el evento
        //[Campo_FechaHora(false,false,-1,-1,-1,Campo_FechaHoraAttribute.Tipo.DatePicker)]
        //public DateTime ACCESO_FECHAHORA { get; set; }
        //Indica el tipo de acceso
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_ACCESO_ID { get; set; }
        //Indica si el acceso se ha calculado para asistencia
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool ACCESO_CALCULADO { get; set; }

    }
}
