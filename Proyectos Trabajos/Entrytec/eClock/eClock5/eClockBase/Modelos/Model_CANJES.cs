using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_CANJES
    {
        //Identificador de canje 0 significa no canjeado
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int CANJE_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int PROMOCION_ID { get; set; }
        //Indica la fecha en la que se genero el canje de Promoción
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime CANJE_FECHAHORA { get; set; }
        //Comentario sobre el canje de Promoción
        [Campo_String(false, false, 45, "", Campo_StringAttribute.Tipo.TextBox)]
        public string CANJE_COMENTARIO { get; set; }

    }
}
