using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Nomina
{
    /// <summary>
    /// Se usa para mandar informacion a la funcion de importación de recibos de nomina en el 
    /// servicio S_Nominas
    /// </summary>
    public class Model_RecNominasPDFyXML
    {
        public int PERSONA_LINK_ID { get; set; }
        public int NOMINA_ID { get; set; }
        public byte[] REC_NOMINA_PDF { get; set; }
        public byte[] REC_NOMINA_XML { get; set; }
    }
}
