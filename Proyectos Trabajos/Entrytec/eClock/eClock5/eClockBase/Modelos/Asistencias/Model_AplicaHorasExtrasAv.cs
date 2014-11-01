using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Asistencias
{
    public class Model_AplicaHorasExtrasAv
    {
        public int PERSONA_D_HE_ID { get; set; }
        public TimeSpan PERSONA_D_HE_APL { get; set; }
        public int TIPO_INCIDENCIA_ID { get; set; }
        public string PERSONA_D_HE_COMEN { get; set; }
    }
}
