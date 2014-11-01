using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_TURNOS
    {
        //Identificador unico de registro de turno
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_ID { get; set; }
        //Tipo de Turno (Semanal, Flexible, etc.)
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_TURNO_ID { get; set; }
        //Nombre del Turno
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TURNO_NOMBRE { get; set; }
        //Indica si las personas que esten asignadas a este turno se les generara el cálculo de prenomina (asistencia)
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TURNO_ASISTENCIA { get; set; }
        //Indica si se permitirán horas extras
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TURNO_PHEXTRAS { get; set; }
        //Indica si el turno actual solo aplica a una persona. el nombre del turno deberá ser el persona_Link_ID
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TURNO_INDIVIDUAL { get; set; }
        //Si este campo contiene datos indicará los grupos (grupo_1_ID separado por comas) que podrán ver el turno actual , el administrador
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TURNO_GRUPOS { get; set; }
        //Color que se usará para dibujar el turno
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.Color)]
        public int TURNO_COLOR { get; set; }
        //Indica el status de este registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TURNO_BORRADO { get; set; }

    }
}
