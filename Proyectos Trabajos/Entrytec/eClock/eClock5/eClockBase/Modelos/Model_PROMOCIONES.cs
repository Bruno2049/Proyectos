using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_PROMOCIONES
    {
        // 
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PROMOCION_ID { get; set; }
        //
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int RECOMPENSA_ID { get; set; }
        //No de Movimientos
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PROMOCION_NO_MOV { get; set; }
        //
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PROMOCION_NOMBRE { get; set; }
        //
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PROMOCION_COMENTARIO { get; set; }
        //Fecha y hora hasta desde donde será valida la promocion
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PROMOCION_DESDE { get; set; }
        //Fecha y hora hasta cuando dejara de ser valida la promocion
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime PROMOCION_HASTA { get; set; }
        //
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, true, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool PROMOCION_BORRADO { get; set; }

    }
}
