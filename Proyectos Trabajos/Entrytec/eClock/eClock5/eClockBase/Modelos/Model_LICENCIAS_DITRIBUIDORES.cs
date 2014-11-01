using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_LICENCIAS_DITRIBUIDORES
    {
        //Identificador único para el distribuidor de la licencia
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int LICENCIA_DISTRIBUIDOR_ID { get; set; }
        //Nombre del distribuidor.
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LICENCIA_DISTRIBUIDOR { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int SUSCRIPCION_ID { get; set; }
        //Indica si el distribuidor esta borrado
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, true, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool LICENCIA_DISTRIBUIDOR_BORRADO { get; set; }

    }
}
