using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Incidencias
{
    public class Model_Horas
    {
        /// <summary>
        /// Contiene la fecha y la hora del inicio de la justificación por horas
        /// </summary>
        public DateTime Inicio { set; get; }
        /// <summary>
        /// Contiene la fecha y la hora del fin de la justificación por horas
        /// </summary>
        public DateTime Fin { set; get; }

        /// <summary>
        /// Duración del permiso
        /// </summary>
        public TimeSpan Tiempo { set; get; }
    }
}
