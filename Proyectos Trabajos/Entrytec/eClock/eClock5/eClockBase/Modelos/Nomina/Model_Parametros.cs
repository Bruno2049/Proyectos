using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Nomina
{
    public class Model_Parametros
    {
        public int REC_NOMINA_ID { get; set; }
        public int PERSONA_ID { get; set; }
        public string AGRUPACION { get; set; }
        public int TIPO_NOMINA_ID { get; set; }
        public int REC_NOMINA_ANO { get; set; }
        public int REC_NOMINA_NO { get; set; }
        public Model_Parametros()
        {
        }

        public Model_Parametros(int iREC_NOMINA_ID)
        {
            REC_NOMINA_ID = iREC_NOMINA_ID;
        }

    }
}
