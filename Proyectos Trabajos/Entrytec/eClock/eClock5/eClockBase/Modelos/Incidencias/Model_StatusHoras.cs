using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Incidencias
{
    public class Model_StatusHoras
    {
        public int PersonaDiarioID { get; set; }
        public DateTime UltimaChecada { get; set; }
        public DateTime Salida { get; set; }
        public DateTime PrimerChecada { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Turno_Entrada { get; set; }
        public DateTime Turno_Salida { get; set; }
    }
}
