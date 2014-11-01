using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_MASCARAS
    {
        //Identificador unico de la mascara
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MASCARA_ID { get; set; }
        //Nombre de la mascara
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MASCARA_NOMBRE { get; set; }
        //Mascara
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string MASCARA { get; set; }
        //Ancho Recomendado en pixeles
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int MASCARA_ANCHO { get; set; }

    }
}
