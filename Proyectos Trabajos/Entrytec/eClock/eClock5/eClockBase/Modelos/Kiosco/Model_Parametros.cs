using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Kiosco
{
    public class Model_Parametros
    {
        public int REPORTE_ID_Asistencia { get; set; }
        public int REPORTE_ID_Nomina { get; set; }
        public int FORMATO_REP_ID_Asistencia { get; set; }
        public int FORMATO_REP_ID_Nomina { get; set; }
        public int REPORTE_ID_RecNomina { get; set; }
        /// <summary>
        /// Identificador unico del tipo de incidencia que se usará como vacaciones
        /// </summary>
        public int TIPO_INCIDENCIA_ID_Vaca { get; set; }
        private string m_Color = "#FF003250";
        public string Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }
    }
}
