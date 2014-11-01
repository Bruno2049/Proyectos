using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_PERSONAS_S_HUELLA
    {
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID { get; set; }
        //Fecha en la que la persona se agrego al listado sin huella
        //[Campo_FechaHora(false, false, -1, -1, -1, Campo_FechaHoraAttribute.Tipo.TextBox)]
        //public DateTime PERSONA_S_HUELLA_FECHA { get; set; }
        //Contraseña que se tecleara para entrar sin huella solo esta habilitada en algunos equipos
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERSONA_S_HUELLA_CLAVE { get; set; }

    }
}
