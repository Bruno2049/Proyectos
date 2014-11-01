using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_PERFILES
    {
        //Identificador de perfil
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERFIL_ID { get; set; }
        //Nombre del perfil
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERFIL_NOMBRE { get; set; }
        //Indica si se ha borrado el registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool PERFIL_BORRADO { get; set; }

    }
}
