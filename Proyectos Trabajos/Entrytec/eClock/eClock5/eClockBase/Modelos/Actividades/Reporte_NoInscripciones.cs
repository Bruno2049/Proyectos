using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Actividades
{
    public class Reporte_NoInscripciones
    {
        public int ACTIVIDAD_ID { get; set; }
        //Terminal en la que ocurrio dicho acceso, si es 0 significa que fue una justificacion
        public string ACTIVIDAD_NOMBRE { get; set; }
        //Indica la descripcion de la actividad.
        public string ACTIVIDAD_DESCRIPCION { get; set; }
        public int NO_INSCRIPCIONES { get; set; }
    }
}
