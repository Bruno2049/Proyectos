using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;


namespace eClockBase.Modelos.HorasExtras
{
    public class Reporte_Semanal_HET
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
        /// EntradaSalida del Día
        /// </summary>
        public string IO0 { get; set; }
        /// <summary>
        /// EntradaSalida del Día
        /// </summary>
        public string IO1 { get; set; }
        /// <summary>
        /// EntradaSalida del Día
        /// </summary>
        public string IO2 { get; set; }
        /// <summary>
        /// EntradaSalida del Día
        /// </summary>
        public string IO3 { get; set; }
        /// <summary>
        /// EntradaSalida del Día
        /// </summary>
        public string IO4 { get; set; }
        /// <summary>
        /// EntradaSalida del Día
        /// </summary>
        public string IO5 { get; set; }
        /// <summary>
        /// EntradaSalida del Día
        /// </summary>
        public string IO6 { get; set; }

        //******************************************************************************************************
        /// <summary>
        /// Abreviatura de la incidencia
        /// </summary>
        public string ABR0 { get; set; }
        /// <summary>
        /// Abreviatura de la incidencia
        /// </summary>
        public string ABR1 { get; set; }
        /// <summary>
        /// Abreviatura de la incidencia
        /// </summary>
        public string ABR2 { get; set; }
        /// <summary>
        /// Abreviatura de la incidencia
        /// </summary>
        public string ABR3 { get; set; }
        /// <summary>
        /// Abreviatura de la incidencia
        /// </summary>
        public string ABR4 { get; set; }
        /// <summary>
        /// Abreviatura de la incidencia
        /// </summary>
        public string ABR5 { get; set; }
        /// <summary>
        /// Abreviatura de la incidencia
        /// </summary>
        public string ABR6 { get; set; }

        //*******************************************************************************************************
        /// <summary>
        /// Color de la incidencia
        /// </summary>
        public int IC0 { get; set; }
        /// <summary>
        /// Color de la incidencia
        /// </summary>
        public int IC1 { get; set; }
        /// <summary>
        /// Color de la incidencia
        /// </summary>
        public int IC2 { get; set; }
        /// <summary>
        /// Color de la incidencia
        /// </summary>
        public int IC3 { get; set; }
        /// <summary>
        /// Color de la incidencia
        /// </summary>
        public int IC4 { get; set; }
        /// <summary>
        /// Color de la incidencia
        /// </summary>
        public int IC5 { get; set; }
        /// <summary>
        /// Color de la incidencia
        /// </summary>
        public int IC6 { get; set; }


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
