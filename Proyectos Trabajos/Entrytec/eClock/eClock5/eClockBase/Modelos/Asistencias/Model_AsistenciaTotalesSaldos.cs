using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Asistencias
{
    public class Model_AsistenciaTotalesSaldos
    {
        public string AGRUPACION_NOMBRE { get; set; }
        public int PERSONA_LINK_ID { get; set; }
        public string PERSONA_NOMBRE { get; set; }
        public string TIPO_INCIDENCIA_NOMBRE { get; set; }
        public decimal ALMACEN_INC_SALDO { get; set; }
        public override string ToString()
        {
            try
            {
                return PERSONA_NOMBRE + " - " + TIPO_INCIDENCIA_NOMBRE + " = " + CeC.Convierte2String(ALMACEN_INC_SALDO);
            }
            catch { }
            return "-";
        }
    }
}
