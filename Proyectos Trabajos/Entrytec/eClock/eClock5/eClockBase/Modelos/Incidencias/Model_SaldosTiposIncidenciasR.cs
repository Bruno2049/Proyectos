using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Incidencias
{
    public class Model_SaldosTiposIncidenciasR
    {
        public int PERSONA_ID { get; set; }
        public DateTime ALMACEN_INC_FECHA { get; set; }
        public decimal ALMACEN_INC_SALDO { get; set; }
        public DateTime ALMACEN_INC_SIGUIENTE { get; set; }
        public string TIPO_INCIDENCIA_NOMBRE { get; set; }
        public string TIPO_INCIDENCIA_R_DESC { get; set; }
        public int TIPO_INCIDENCIA_ID { get; set; }
        public int TIPO_INCIDENCIA_R_ID { get; set; }
    }
}
