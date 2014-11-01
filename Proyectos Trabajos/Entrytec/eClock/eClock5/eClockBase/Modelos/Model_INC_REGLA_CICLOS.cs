using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_INC_REGLA_CICLOS
    {
        //Identificador de detalle tipo de inventario
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int INC_REGLA_CICLO_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_R_ID { get; set; }
        //Numero de Ciclo
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int INC_REGLA_CICLO_NO { get; set; }
        //Numero de Incidencias permitidas a sumar
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int INC_REGLA_CICLO_INCR { get; set; }

    }
}
