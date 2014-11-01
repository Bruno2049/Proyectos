using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Asistencias
{
    public class Model_AsistenciaTotales
    {
        public string AGRUPACION_NOMBRE { get; set; }
        public int PERSONA_LINK_ID { get; set; }
        public string PERSONA_NOMBRE { get; set; }
        public string INCIDENCIA_NOMBRE { get; set; }
        public int No { get; set; }
        public decimal Saldo { get; set; }
        public override string ToString()
        {
            try
            {
                return PERSONA_NOMBRE + " - " + INCIDENCIA_NOMBRE + " (" + No + ") = " + CeC.Convierte2String(Saldo);
            }
            catch { }
            return "-";
        }
    }
}
