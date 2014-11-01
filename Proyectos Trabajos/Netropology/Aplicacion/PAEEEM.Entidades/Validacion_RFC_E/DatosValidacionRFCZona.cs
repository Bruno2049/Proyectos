using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Validacion_RFC_E
{
    public class DatosValidacionRFCZona
    {
        public string DistribuidorRS { get; set; }
        public string DirtribuidorNC { get; set; }
        public string TipoPersona { get; set; }
        public string NombreORZ { get; set; }
        public DateTime FechaNacReg { get; set; }
        public string RFC { get; set; }
        public byte[] PDF { get; set; }
    }
}
