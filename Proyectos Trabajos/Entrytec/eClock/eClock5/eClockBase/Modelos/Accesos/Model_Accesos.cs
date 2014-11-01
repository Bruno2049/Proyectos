using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eClockBase.Atributos_Campos;
using Newtonsoft.Json;

namespace eClockBase.Modelos.Accesos
{
    /// <summary>
    /// Modelo en el cual se Almacena las propiedades de los campos de las tablas de EC_Personas y EC_ Accesos
    /// </summary>
    public class Model_Accesos
    {
        

        /// <summary>
        /// Indica el Acceso_ID y es un campo de EC_Accesos
        /// </summary>
        public int ACCESO_ID { get; set; }        
        /// <summary>
        /// Indica el nombre de la agrupacion y es un campo de EC_Personas
        /// </summary>
        public string AGRUPACION_NOMBRE { get; set; }
        //Indica el nombre de la Persona y es un campo de EC_Personas
        public string PERSONA_NOMBRE { get; set; }
        //Indica el numero de PERSONA_LINK_ID y es un campo de EC_Persona
        public int PERSONA_LINK_ID { get; set; }
        //Indica la fecha y hora en que se realizo la checada y es un campo de EC_Accesos
        public DateTime ACCESO_FECHAHORA { get; set; }
        //Indica el Nombre de tipo de acceso y es un campo de EC_Acceso
        public string TIPO_ACCESO_NOMBRE { get; set; }
        //Indica el El numbre de la terminal y es un campo de EC_Accesos
        public string TERMINAL_NOMBRE { get; set; }
        //Indica el Acceso que fue calculado y es un campo de EC_Accesos
        public int ACCESO_CALCULADO { get; set; }
        //Indica el ID de el tipo de acceso y es Campo de EC_Accesos
        public int TIPO_ACCESO_ID { get; set; }
        
        //Metodo sebrecargado para la convercion a string de Acceso_ID
        public override string ToString()
        {
            return ACCESO_ID.ToString();
        }   
    }
    
}
