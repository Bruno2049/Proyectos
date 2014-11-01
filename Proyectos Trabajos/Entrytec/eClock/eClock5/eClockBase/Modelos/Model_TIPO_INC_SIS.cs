using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_TIPO_INC_SIS
    {
        //Identificador del Tipo de incidencias del sistema
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INC_SIS_ID { get; set; }
        //Nombre de la incidencia
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INC_SIS_NOMBRE { get; set; }
        //Abreviatura del tipo de incidencia del sistema
        [Campo_String(false, false, 2, "", Campo_StringAttribute.Tipo.TextBox)]
        public string TIPO_INC_SIS_ABR { get; set; }
        //Color de la incidencia del sistema
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.Color)]
        public int TIPO_INC_SIS_COLOR { get; set; }
    }
}
