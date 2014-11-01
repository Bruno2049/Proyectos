using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_PERSONAS
    {
        ///Identificador unico por persona
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.PersonaLinkID)]
        public int PERSONA_ID { get; set; }
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }
        //Liga al numero de empleado, en el caso de merck es TRACVE
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERSONA_LINK_ID { get; set; }
        //Muestra el tipo de persona de la que se trata, para empleado se usara 0
        [Campo_Int(false, true, -1, -1, 1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_PERSONA_ID { get; set; }
        //Nombre de la persona
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERSONA_NOMBRE { get; set; }
        //Indicara el turno predeterminado al que estara asignado dicha persona (el default es 0 ""Ningun Turno"")
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_ID { get; set; }
        //
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int CALENDARIO_DF_ID { get; set; }
        //Contiene el email de la persona
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERSONA_EMAIL { get; set; }
        //Identificador de diseño
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int DISENO_ID { get; set; }
        //Indica la agrupación de la persona
        [Campo_String(false, false, 2000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string AGRUPACION_NOMBRE { get; set; }
        //Indica si se ha borrado el registro (Alta o Baja)
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, true, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool PERSONA_BORRADO { get; set; }

    }
}
