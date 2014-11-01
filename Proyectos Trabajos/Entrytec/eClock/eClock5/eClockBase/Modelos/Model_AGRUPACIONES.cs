using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_AGRUPACIONES
    {
        //
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SUSCRIPCION_ID { get; set; }
        //Agrupacion de la suscripcion
        [Campo_String(false, false, 2000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string AGRUPACION_NOMBRE { get; set; }

    }
}
