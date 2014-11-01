using System;

namespace PAEEEM.Entidades.Trama
{
    [Serializable]
    public class CompHistorialConsumo
    {
        public int IdHistorial { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal Consumo { get; set; }
        public int IdConsumo { get; set; }
    }

    [Serializable]
    public class CompHistorialDetconsumo
    {
        public int IdHistorial { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal Consumo { get; set; }
        public int Demanda { get; set; }
        public decimal FactorPotencia { get; set; }
        public int Id { get; set; }
    }

    [Serializable]
    public class CompInfoConsDeman
    {               
        public decimal Suma { get; set; }
        public decimal DemandaMax { get; set; }
        public CompDetalleFechas Detalle { get; set; }       
    }

    [Serializable]
    public class CompDetalleFechas
    {
        public DateTime? FechaMin { get; set; }
        public DateTime? FechaMax { get; set; }
        public decimal Promedio { get; set; }
        public decimal DemandaMax { get; set; }        
    }

}
