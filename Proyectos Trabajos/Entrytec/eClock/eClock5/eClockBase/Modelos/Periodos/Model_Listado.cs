using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Periodos
{
    public class Model_Listado
    {
        public int PERIODO_ID { get; set; }
        public int PERIODO_NO { get; set; }
        public string TIPO_NOMINA_NOMBRE { get; set; }
        public DateTime PERIODO_NOM_INICIO { get; set; }
        public DateTime PERIODO_NOM_FIN { get; set; }
        public DateTime PERIODO_ASIS_INICIO { get; set; }
        public DateTime PERIODO_ASIS_FIN { get; set; }
        public int EDO_PERIODO_ID { get; set; }
        public int PERIODO_N_ID { get; set; }
    }
}
