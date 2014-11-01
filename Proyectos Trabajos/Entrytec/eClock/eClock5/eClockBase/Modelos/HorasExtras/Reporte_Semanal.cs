using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.HorasExtras
{
    public class Reporte_Semanal
    {

        /// <summary>
        /// Persona Diario ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// PErsona Link_ID
        /// </summary>
        public int LINK { get; set; }
        /// <summary>
        /// Agrupacion
        /// </summary>
        public string AGR { get; set; }
        /// <summary>
        /// Persona Nombre
        /// </summary>
        public string NOMBRE { get; set; }
        /// <summary>
        /// Turno perdeterminado
        /// </summary>
        public string TURNO { get; set; }
        /// <summary>
        /// Horas Extras
        /// </summary>
        public DateTime HEA0 { get; set; }
        /// <summary>
        /// Horas Extras
        /// </summary>
        public DateTime HEA1 { get; set; }
        /// <summary>
        /// Horas Extras
        /// </summary>
        public DateTime HEA2 { get; set; }
        /// <summary>
        /// Horas Extras
        /// </summary>
        public DateTime HEA3 { get; set; }
        /// <summary>
        /// Horas Extras
        /// </summary>
        public DateTime HEA4 { get; set; }        
        /// <summary>
        /// Horas Extras
        /// </summary>
        public DateTime HEA5 { get; set; }
        /// <summary>
        /// Horas Extras
        /// </summary>
        public DateTime HEA6 { get; set; }


        public DateTime PERSONA_D_HE_APL { get; set; }
        //        
        public DateTime PERSONA_D_HE_SIMPLE { get; set; }
        //        
        public DateTime PERSONA_D_HE_DOBLE { get; set; }
        //        
        public DateTime PERSONA_D_HE_TRIPLE { get; set; }


    }
}
