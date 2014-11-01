using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_LICENCIAS_ORIGEN
    {
        //Identificador único para la licencia origen.
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int LICENCIA_ORIGEN_ID { get; set; }
        //Nombre del origen de la licencia
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LICENCIA_ORIGEN { get; set; }
        //Indica si esta borrado el origen de la licencia
        [Campo_Int(false, false, -1, -1, 0, Campo_IntAttribute.Tipo.TextBox)]
        public int LICENCIA_ORIGEN_BORRADO { get; set; }

    }
}
