using System;
using System.Collections.Generic;

namespace PAEEEM.Entidades.Trama
{
    [Serializable]
    public class CompInformacionGeneral
    {
        public List<CompConcepto> Conceptos { get; set; }
        public List<CompConcepto> ConsumoP { get; set; }
        public List<CompConcepto> DemandaP { get; set; }
    }
}
