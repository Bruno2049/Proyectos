using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_CALENDARIOS_DF
    {
        //Identificador de calendario de vacaciones
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CALENDARIO_DF_ID { get; set; }
        //Nombre del calendario de vacaciones
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CALENDARIO_DF_NOMBRE { get; set; }
        //Indica si estará disponible este calendario para edición
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CALENDARIO_DF_BORRADO { get; set; }


    }
}
