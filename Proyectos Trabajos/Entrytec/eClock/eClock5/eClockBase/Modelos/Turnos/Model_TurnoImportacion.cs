using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Turnos
{
    public class Model_TurnoImportacion
    {
        public int PERSONA_ID { get; set; }
        public int PERSONA_LINK_ID { get; set; }
        public string TURNO_NOMBRE { get; set; }
        public int TURNO_ID { get; set; }
        public DateTime FECHA_INICIAL { get; set; }
        public DateTime FECHA_FINAL { get; set; }
    }
}
