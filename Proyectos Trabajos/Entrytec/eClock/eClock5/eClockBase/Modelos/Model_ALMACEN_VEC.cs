using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;
namespace eClockBase.Modelos
{
    public class Model_ALMACEN_VEC
    {
        //Identificador único del registro de almacenes
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int ALMACEN_VEC_ID { get; set; }
        //Nombre del almacen
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string ALMACEN_VEC_NOMBRE { get; set; }
        //
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool ALMACEN_VEC_BORRADO { get; set; }

    }
}
