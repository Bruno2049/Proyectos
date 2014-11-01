using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_TIPO_NOMINA
    {
        //Identificador de tipo de nomina
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_NOMINA_ID { get; set; }
        //Calendario que se usará para el calculo de dias festivos
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int CALENDARIO_DF_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int PERIODO_N_ID { get; set; }
        //Nombre del tipo de nomina
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_NOMINA_NOMBRE { get; set; }
        //Identificador de tipo de nomina en sistemas externos
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_NOMINA_IDEX { get; set; }
        //Indica si se ha borrado el registro
        [JsonConverter(typeof(BoolConverter))]
        [Campo_Bool(false, false, false, Campo_BoolAttribute.Tipo.CheckBox)]
        public bool TIPO_NOMINA_BORRADO { get; set; }
        //-1 = Nunca calcular Horas Extras; 0= Calcular horas extras cuando este habilitado en el turno; 1=Siempre calcular horas extras;2=Calcular Horas Extras como simples
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_NOMINA_HE { get; set; }
        //Color del Tipo de Nomina
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.Color)]
        public int TIPO_NOMINA_COLOR { get; set; }

    }
}
