using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_EDO_SUSCRIPCION
    {
        //Identificador del estado de la suscripcion
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int EDO_SUSCRIPCION_ID { get; set; }
        //Estado de la suscripcion
        [Campo_String(true, true, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string EDO_SUSCRIPCION { get; set; }

    }
}
