using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_MOD_TERM_T_TECNO
    {
        //Identificador del modelo de la terminal
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MODELO_TERMINAL_ID { get; set; }
        //Identificador del tipo de tecnología soportada
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_TECNOLOGIA_ID { get; set; }

    }
}
