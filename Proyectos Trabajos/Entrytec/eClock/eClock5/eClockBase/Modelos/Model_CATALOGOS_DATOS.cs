using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_CATALOGOS_DATOS
    {
        //Identificador del catalogo, con tados
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CATALOGO_DATO_ID { get; set; }
        //Valor de campo llave
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CATALOGO_DATO_LLAVE { get; set; }
        //Descripcion o nombre del valor del registro
        [Campo_String(false, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CATALOGO_DATO_DESC { get; set; }

    }
}
