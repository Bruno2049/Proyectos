using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_LOG_AUDITORIA
    {
        //Identificador unico de cada registro del log de Auditoria
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int LOG_AUDITORIA_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int SESION_ID { get; set; }
        //Fecha y hora en la que sucedio el evento
        [Campo_FechaHora(true, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime LOG_AUDITORIA_FECHAHORA { get; set; }
        //Identificador del tipo de auditoria de la que se trata RFU Ver 2.0
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TIPO_AUDITORIA_ID { get; set; }
        //Descripcion del evento de auditoria
        [Campo_String(true, false, 255, "", Campo_StringAttribute.Tipo.TextBox)]
        public string LOG_AUDITORIA_DESCRIPCION { get; set; }

    }
}
