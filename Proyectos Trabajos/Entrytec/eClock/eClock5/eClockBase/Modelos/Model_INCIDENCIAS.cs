using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos
{
    public class Model_INCIDENCIAS
    {
        //Identificador de incidencia
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int INCIDENCIA_ID { get; set; }
        //Indica el tipo de incidencia de la que se trata
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_INCIDENCIA_ID { get; set; }
        //Comentario sobre la incidencia
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string INCIDENCIA_COMENTARIO { get; set; }
        //Fecha y hora en la que se genero la incidencia
        [Campo_FechaHora(false, true, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.TextBox)]
        public DateTime INCIDENCIA_FECHAHORA { get; set; }
        //Informacion adicional sobre la incidencia valida al momento de exportar Campo=Valor|OtroCampo=Valor2
        [Campo_String(false, false, 4000, "", Campo_StringAttribute.Tipo.TextBox)]
        public string INCIDENCIA_EXTRAS { get; set; }
        //Datos de la sesión cuando fue modificado
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SESION_ID { get; set; }

    }
}
