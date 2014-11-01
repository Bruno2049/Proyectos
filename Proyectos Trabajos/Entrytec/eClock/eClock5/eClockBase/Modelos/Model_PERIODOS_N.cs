using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_PERIODOS_N
    {
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERIODO_N_ID { get; set; }
        //Nombre del periodo
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERIODO_N_NOM { get; set; }
        //Descripcion del periodo
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERIODO_N_DESC { get; set; }
        //Intervalo de tiempo que se usará para crear los periodos
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIMESPAN_ID { get; set; }
        //Query de seleccion SELECT PERSONA_ID FROM EC_PERSONAS
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string PERIODO_N_PERSONAS { get; set; }
        //Dias a restar para obtener el periodo de asistencia
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PERIODO_N_DASIS { get; set; }
        //Indica si el periodo ha sido borrado
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool PERIODO_N_BORRADO { get; set; }

    }
}
