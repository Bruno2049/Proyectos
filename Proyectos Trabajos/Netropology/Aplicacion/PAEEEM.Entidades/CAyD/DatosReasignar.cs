using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.CAyD
{
    [Serializable]
    public class DatosReasignar
    {
        public string NoSolicitud { get; set; }
        public string DistrNC { get; set; }
        public string DistrRS { get; set; }
        public string Folio { get; set; }
        public string CAyD { get; set; }
    }
}
