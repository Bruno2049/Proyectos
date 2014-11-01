using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_CAMPOS_INC_R
    {
        //Nombre que se usará para controlar los campos. @RI+ id de regla de incidencia + _ + Campo
        [Campo_String(true, true, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_NOMBRE { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_R_ID { get; set; }
        //Indica que el campo es obligatorio
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool CAMPO_INC_R_OBL { get; set; }
        //Contiene el nombre del campo donde se guardará en caso de exportar la informacion
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_INC_R_DEST { get; set; }
        //Contiene datos extras para la exportación
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CAMPO_INC_R_EXP { get; set; }
        //Orden en el que se mostraran los campos
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int CAMPO_INC_R_ORD { get; set; }

    }
}
