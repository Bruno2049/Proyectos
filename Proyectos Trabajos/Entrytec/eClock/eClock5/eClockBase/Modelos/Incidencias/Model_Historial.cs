using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Incidencias
{
    public class Model_Historial
    {
        /// <summary>
        /// Numero de empleado
        /// </summary>
        public int PERSONA_LINK_ID {get;set;}
        /// <summary>
        /// Nombre del empleado
        /// </summary>
        public string PERSONA_NOMBRE { get; set; }
        /// <summary>
        /// Fecha en la que se registro la incidencia
        /// </summary>
        public DateTime ALMACEN_INC_FECHA { get; set; }
        /// <summary>
        /// Nombre de la incidencia
        /// </summary>
        public string TIPO_INCIDENCIA_NOMBRE { get; set; }
        /// <summary>
        /// ID del Incidencia almacenada *
        /// </summary>
        public int ALMACEN_INC_NO { get; set; }
        /// <summary>
        /// Se refiere al saldo de la incidencia
        /// </summary>
        public double ALMACEN_INC_SALDO { get; set; }
        /// <summary>
        /// Se refiere al tipo de incidencia
        /// </summary>
        public string TIPO_ALMACEN_INC_NOMBRE { get; set; }
        /// <summary>
        /// Es el comentario con respecto a la incidencia
        /// </summary>
        public string ALMACEN_INC_COMEN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ALMACEN_INC_EXTRAS { get; set; }
        /// <summary>
        /// Es el id que se asigna a la incidencia almacenada
        /// </summary>
        public int ALMACEN_INC_ID { get; set; }
    }
}
