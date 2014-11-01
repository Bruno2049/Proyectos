using System;

namespace PAEEEM.Entidades.Trama
{
    [Serializable]
    public class CompConcepto
    {
        public int Id { get; set; }
        public int PuntoInicial { get; set; }
        public int PuntoFinal { get; set; }
        public int PuntoLongitud { get; set; }
        public string Concepto { get; set; }
        public string Dato { get; set; }       
        public DateTime? ValorFecha { get; set; }
        public int ValorEntero { get; set; }
        public decimal ValorDecimal { get; set; }
        public string ValorCadena { get; set; }
    }
}
