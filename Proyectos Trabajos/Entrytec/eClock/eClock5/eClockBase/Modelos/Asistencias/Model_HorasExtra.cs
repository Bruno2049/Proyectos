using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Asistencias
{
    public class Model_HorasExtra
    {
        public int PERSONA_D_HE_ID { get; set; }
        public string AGRUPACION_NOMBRE { get; set; }
        public int PERSONA_LINK_ID { get; set; }
        public string PERSONA_NOMBRE { get; set; }
        public DateTime PERSONA_DIARIO_FECHA { get; set; }

        public DateTime PERSONA_D_HE_SIS { get; set; }
        public DateTime PERSONA_D_HE_CAL { get; set; }
        public DateTime PERSONA_D_HE_APL { get; set; }

        public DateTime ACCESO_E { get; set; }
        public DateTime ACCESO_S { get; set; }

        public DateTime ACCESO_CS { get; set; }
        public DateTime ACCESO_CR { get; set; }

        public string TURNO { get; set; }
        public string INCIDENCIA_NOMBRE { get; set; }
        public string INCIDENCIA_ABR { get; set; }
        public string TIPO_INC_C_SIS_NOMBRE { get; set; }

        public DateTime PERSONA_DIARIO_TT { get; set; }
        public DateTime PERSONA_DIARIO_TDE { get; set; }
        public DateTime PERSONA_DIARIO_TE { get; set; }
        public DateTime PERSONA_DIARIO_TC { get; set; }
        public DateTime PERSONA_DIARIO_TES { get; set; }
        
        public decimal PERSONA_D_HE_SIMPLE { get; set; }
        public decimal PERSONA_D_HE_DOBLE { get; set; }
        public decimal PERSONA_D_HE_TRIPLE { get; set; }
        public string PERSONA_D_HE_COMEN { get; set; }

        public bool Seleccionado { get; set; }
        public override string ToString()
        {
            return PERSONA_NOMBRE + "[" + PERSONA_DIARIO_FECHA.ToString("dd/MM/YYYY") + "]" + (INCIDENCIA_ABR != null ? INCIDENCIA_ABR : "");
        }

    }
}
