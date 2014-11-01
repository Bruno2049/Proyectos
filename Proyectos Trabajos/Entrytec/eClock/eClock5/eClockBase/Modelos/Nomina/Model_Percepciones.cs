using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Nomina
{
    public class Model_Percepciones
    {
        public int PersonaID { get; set; }
        public string Clave { get; set; }
        public string Concepto { get; set; }
        public double Horas_Dias { get; set; }
        public double Importe { get; set; }
    }
}
