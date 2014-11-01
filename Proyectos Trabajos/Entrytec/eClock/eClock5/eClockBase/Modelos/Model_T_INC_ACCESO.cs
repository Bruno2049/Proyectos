using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_T_INC_ACCESO
    {
        //Identificador unico de registro
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int T_INC_ACCESO_ID { get; set; }

        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string T_INC_ACCESO_NOMBRE { get; set; }

        [Campo_String(false, false, 20, "", Campo_StringAttribute.Tipo.TextBox)]
        public string T_INC_ACCESO_ABR { get; set; }

        //Identificador unico de registro
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.Color)]
        public int T_INC_ACCESO_COLOR { get; set; }

        //Identificador unico de registro
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_INCIDENCIA_ID { get; set; }

        //Identificador unico de registro
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TIPO_INCIDENCIA_EX_ID { get; set; }

        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INCIDENCIA_EX_REGLA { get; set; }

        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string T_INC_ACCESO_COMEN { get; set; }

        //
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool T_INC_ACCESO_ENTRADA { get; set; }

        //
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool T_INC_ACCESO_SALIDA { get; set; }

        //
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool T_INC_ACCESO_INTERVALO { get; set; }

        //Identificador unico de registro
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int T_INC_ACCESO_ASIGNAR { get; set; }

        //Identificador unico de registro
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int T_INC_ACCESO_MEDICION { get; set; }

        //
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool T_INC_ACCESO_BORRADO { get; set; }
        
    }
}
