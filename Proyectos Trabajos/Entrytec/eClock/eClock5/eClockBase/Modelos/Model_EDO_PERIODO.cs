using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_EDO_PERIODO
    {
        //Estado de periodo
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int EDO_PERIODO_ID { get; set; }
        //Periodo Nombre,
        [Campo_String(true, true, 50, "", Campo_StringAttribute.Tipo.TextBox)]
        public string EDO_PERIODO_NOMBRE { get; set; }

    }
}
