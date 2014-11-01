using System;
using System.Collections.Generic;

namespace PAEEEM.Entidades.Trama
{
    [Serializable]
    public class CompDetalleConsumo
    {
        public List<CompConcepto> PeriodoMes { get; set; }
        public List<CompConcepto> PeriodoAnio { get; set; }
        public List<CompConcepto> Consumo { get; set; }
        public List<CompConcepto> Demanda { get; set; }
        public List<CompConcepto> FactorPotencia { get; set; }
    }
}
