using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos
{
    public class Model_DIAS_SEMANA
    {
        //Identificador unico de el dia de semana
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int DIA_SEMANA_ID { get; set; }
        //Nombre del dia de la semana
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string DIA_SEMANA_NOMBRE { get; set; }
        //Indica si se trata de un dia Domingo a Lunes
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(true, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool DIA_SEMANA_NORMAL { get; set; }

    }
}
