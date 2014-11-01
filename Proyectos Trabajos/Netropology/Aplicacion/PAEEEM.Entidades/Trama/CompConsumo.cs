using System;
using System.Collections.Generic;

namespace PAEEEM.Entidades.Trama
{
    [Serializable]
    public class CompConsumo
    {
        public List<CompConcepto> Fechas { get; set; }
        public List<CompConcepto> Consumos { get; set; }
    }
}
