using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eClockBase.Modelos.Nomina
{
    public class Model_Interfaz
    {
        public int SISTEMA_NOMINA_ID { get; set; }
        public string CadenaConexion { get; set; }
        public bool ActualizarEmpleados { get; set; }
        public bool ImportarTiposIncidencias { get; set; }
        public bool ImportarIncidencias { get; set; }
        public bool ExportarIncidencias { get; set; }
        public bool ImportarNomina { get; set; }
    }
}
