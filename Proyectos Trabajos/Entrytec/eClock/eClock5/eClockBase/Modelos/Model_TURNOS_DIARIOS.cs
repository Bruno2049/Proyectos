using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_TURNOS_DIARIOS
    {
        //Identificador del horario diario
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_DIARIO_ID { get; set; }
        //Liga hacia el contenedor del turno (padre)
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_ID { get; set; }
        //Fecha en la que se aplicara el turno seleccionado
        [Campo_FechaHora(false, false, "2008-01-01", "2029-12-31", "2010-04-05", Campo_FechaHoraAttribute.Tipo.DatePicker)]
        public DateTime TURNO_DIARIO_FECHA { get; set; }
        //Contiene el una liga hacia la tabla EC_TURNOS que indica que turno usara para este dia, deberá existir una validación para solo permitir que este turno sea de tipo <= 2, ya que el turno hijo no puede ser tipo DIARIO
        [Campo_Int(false, false, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_HIJO_ID { get; set; }

    }
}
