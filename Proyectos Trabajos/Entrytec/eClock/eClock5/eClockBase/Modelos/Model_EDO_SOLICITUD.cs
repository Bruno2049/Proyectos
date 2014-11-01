using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_EDO_SOLICITUD
    {
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int EDO_SOLICITUD_ID { get; set; }
        //Nombre del estado de la solicitud
        [Campo_String(true, true, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string EDO_SOLICITUD_NOMBRE { get; set; }

    }
}
