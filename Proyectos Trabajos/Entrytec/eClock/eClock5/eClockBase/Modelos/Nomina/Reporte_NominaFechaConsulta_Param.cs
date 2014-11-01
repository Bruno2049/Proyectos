using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Nomina
{
    public class Reporte_NominaFechaConsulta_Param
    {
        public int PERSONA_ID { get; set; }
        public string AGRUPACION { get; set; }
        public int TIPO_NOMINA_ID { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    }
}
