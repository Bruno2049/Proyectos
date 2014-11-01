using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;

namespace eClockBase.Modelos
{
    public class Model_TURNOS_SEMANAL_DIA
    {
        //Identificador del turno semanal
        [Campo_Int(true, true, -1, -1, -1, Campo_IntAttribute.Tipo.TextBox)]
        public int TURNO_SEMANAL_DIA_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TURNO_DIA_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int TURNO_ID { get; set; }
        //
        [Campo_Int(true, false, -1, -1, -1, Campo_IntAttribute.Tipo.ComboBox)]
        public int DIA_SEMANA_ID { get; set; }

        public Model_TURNOS_SEMANAL_DIA()
        {
        }

        public Model_TURNOS_SEMANAL_DIA(int iTURNO_ID, int iTURNO_DIA_ID, int iDIA_SEMANA_ID)
        {
            TURNO_ID = iTURNO_ID;
            TURNO_DIA_ID = iTURNO_DIA_ID;
            DIA_SEMANA_ID = iDIA_SEMANA_ID;
        }
    }
}
