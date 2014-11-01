using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_MODELOS_TERMINALES
    {
        //Identificador unico del modelo de terminal
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MODELO_TERMINAL_ID { get; set; }
        //Nombre del modelo de terminal
        [Campo_String(true, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MODELO_TERMINAL_NOMBRE { get; set; }
        //Indica si este modelo esta disponible
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool MODELO_TERMINAL_BORRADO { get; set; }

    }
}
