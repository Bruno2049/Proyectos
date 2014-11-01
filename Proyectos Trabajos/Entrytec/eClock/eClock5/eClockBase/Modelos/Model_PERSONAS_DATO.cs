using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_PERSONAS_DATO
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_DATO_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID{ get; set; }
        
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SESION_ID { get; set; }

        //
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_NOMBRE { get; set; }
        //
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERSONA_DATO { get; set; }
        //
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PERSONA_DATO_FECHA { get; set; }

    }
}
