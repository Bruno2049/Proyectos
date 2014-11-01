using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Asistencias
{
    public class Model_Lineal
    {
        public int Persona_ID { get; set; }
        public string Agrupacion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Campos { get; set; }
        public string OrdenarPor { get; set; }
        public DateTime TiposIncidenciasSistemaIDs { get; set; }
        public string TiposIncidenciasIDs { get; set; }
        public string INCIDENCIA_NOMBRE { get; set; }
        public string INCIDENCIA_ABR { get; set; }
        public string TIPO_INC_C_SIS_NOMBRE { get; set; }
        public DateTime PERSONA_DIARIO_TT { get; set; }
        public DateTime PERSONA_DIARIO_TDE { get; set; }
        public DateTime PERSONA_DIARIO_TE { get; set; }
        public DateTime PERSONA_DIARIO_TC { get; set; }
        public DateTime PERSONA_D_HE_SIS { get; set; }
        public DateTime PERSONA_D_HE_CAL { get; set; }

        public override string ToString()
        {
            return "";// PERSONA_NOMBRE + "[" + PERSONA_DIARIO_FECHA.ToString("dd/MM/YYYY") + "]" + (INCIDENCIA_ABR != null ? INCIDENCIA_ABR : "");
        }
    }
}
