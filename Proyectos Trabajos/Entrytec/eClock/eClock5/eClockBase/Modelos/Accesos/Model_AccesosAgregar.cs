using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Accesos
{
    /// <summary>
    /// Este es un modelos para poder agragar Accesos y posee los atributos de los Campos de dos tablas EC_Personas y todos los campos de EC_Acceso
    /// </summary>
    public class Model_AccesosAgregar
    {

        public int PERSONA_LINK_ID { get; set; }
        /// <summary>
        /// La propiedad Terminal_ID se Almacenara el Terminal_ID de la tabla EC_Accesos
        /// </summary>
        public int TERMINAL_ID { get; set; }
        /// <summary>
        /// Indica la fecha y hora en que se realizo la checada
        /// </summary>
        public DateTime ACCESO_FECHAHORA { get; set; }
        /// <summary>
        /// Indica el ID de el tipo de acceso del, que resulto la checada
        /// </summary>
        public int TIPO_ACCESO_ID { get; set; }

    }
}