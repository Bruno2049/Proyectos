using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Importar
{
    public class Model_Resultado
    {
        /// <summary>
        /// Listado de posiciones de los elementos que se pudieron importar, separados por comas
        /// </summary>
        public string CorrectosPoss { get; set; }
        /// <summary>
        /// Listado de posiciones de los elementos que no se pudieron importar, separados por comas
        /// </summary>
        public string ErroneosPoss { get; set; }

        public string Error { get; set; }
    }
}
