using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos
{
    public class Model_ACCESOS_JUS
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ACCESO_JUS_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int T_INC_ACCESO_ID { get; set; }
        //Identificador del Acceso creado manualmente (Justificado)
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int ACCESO_ID { get; set; }
        //Contiene la diferencia entre la hora que tenia asignado en el módulo de asistencia y esta utilizando FechaNula como la hora cero/ Puede contener el tiempo de intervalo si asi fuera el caso
        //[Campo_FechaHora(false, false, -1, -1, -1, Campo_FechaHoraAttribute.Tipo.DatePicker)]
        //public DateTime ACCESO_JUS_DIFF { get; set; }
        //Descripcion de la justificacion de accesos
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ACCESO_JUS_DESC { get; set; }
        //Indica que es un intervalo al que fue asignado
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool ACCESO_JUS_INTER { get; set; }

    }
}
