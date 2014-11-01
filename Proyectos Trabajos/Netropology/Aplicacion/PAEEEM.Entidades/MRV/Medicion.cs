using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.MRV
{
    [Serializable]
    public class Medicion
    {
        public int IdMedicion { get; set; }
        public DateTime FechaMedicion { get; set; }
        public string DescripcionMedicion { get; set; }
        public bool Estatus { get; set; }
    }
}
