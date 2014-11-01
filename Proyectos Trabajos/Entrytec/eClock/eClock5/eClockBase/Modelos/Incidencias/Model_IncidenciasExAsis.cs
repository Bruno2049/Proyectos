using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Incidencias
{
    class Model_IncidenciasExAsis
    {
        public int PERSONA_ID { get; set; }
        public int PERSONA_LINK_ID { get; set; }
        public DateTime PERSONA_DIARIO_FECHA { get; set; }
        public DateTime PERSONA_DIARIO_TT { get; set; }
        public DateTime PERSONA_DIARIO_TE { get; set; }
        public DateTime PERSONA_DIARIO_TC { get; set; }
        public DateTime PERSONA_DIARIO_TDE { get; set; }
        public DateTime PERSONA_DIARIO_TES { get; set; }
        public int TIPO_INCIDENCIAS_EX_ID { get; set; }
        public string TIPO_INCIDENCIAS_EX_TXT { get; set; }
        public int TIPO_FALTA_EX_ID { get; set; }
        public string TIPO_INCIDENCIAS_EX_PARAM { get; set; }
        public string INCIDENCIA_COMENTARIO { get; set; }
        public int INCIDENCIA_ID { get; set; }
    }
}
