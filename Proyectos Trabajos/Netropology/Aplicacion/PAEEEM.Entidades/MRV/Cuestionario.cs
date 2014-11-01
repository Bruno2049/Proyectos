using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.MRV
{
    [Serializable]
    public class Cuestionario
    {
        public int IdCuestionario { get; set; }
        public int IdMedicion { get; set; }
        public DateTime FechaCuestionario { get; set; }
        public string DescripcionCuestionario { get; set; }
        public bool Estatus { get; set; }
    }
}
