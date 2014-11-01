using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Asistencias
{
    /// <summary>
    /// Este metodo sera utilizado para el control de asistencias de los empleados
    /// </summary>
    public class Model_Asistencia
    {
        /// <summary>
        /// Este es el ID de las checadas y se puede ubicar en EC_Personas_Diario
        /// </summary>
        public int PERSONA_DIARIO_ID { get; set; }
        /// <summary>
        /// Es la agrupacion de el empleado se puede obtener en EC_Personas 
        /// </summary>
        public string AGRUPACION_NOMBRE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PERSONA_LINK_ID { get; set; }
        public string PERSONA_NOMBRE { get; set; }
        public DateTime PERSONA_DIARIO_FECHA { get; set; }
        public DateTime ACCESO_E { get; set; }
        public DateTime ACCESO_S { get; set; }
        public string TURNO { get; set; }
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
            return PERSONA_NOMBRE + "[" + PERSONA_DIARIO_FECHA.ToString("dd/MM/YYYY") + "]" + (INCIDENCIA_ABR != null ? INCIDENCIA_ABR : "");
        }
    }
}
