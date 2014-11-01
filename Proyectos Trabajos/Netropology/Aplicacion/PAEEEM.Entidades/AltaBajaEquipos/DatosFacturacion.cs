using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.AltaBajaEquipos
{
    [Serializable]
    public class DatosFacturacion
    {
        public decimal ValorAmortizacion { get; set; }
        public decimal CapacidadPago { get; set; }
        public decimal ValorIva { get; set; }
        public decimal MontoTotalPagar { get; set; }
        public int Plazo { get; set; }
        public int PeriodoPago { get; set; }
        public decimal TotalAhorrosKw { get; set; }
        public decimal TotalAhorrosKwH { get; set; }
        public decimal KwConAhorro { get; set; }
        public decimal KwHConAhorro { get; set; }

        public decimal ConsumoPromedio { get; set; }
        public decimal DemandaMaxima { get; set; }
        public decimal FactorPotencia { get; set; }
        public decimal AhorroEconomico { get; set; }

        public decimal MontoMaximoFacturar20 { get; set; }
        public decimal ProximaFacturacionMensual { get; set; }

        public bool EsCombinacionTecnologias { get; set; }
        public decimal PorcentageMaximoInstalacion { get; set; }

        public decimal KvAR { get; set; }
        public bool AplicaPeridosBC { get; set; }
        public bool AplicaFPBc { get; set; }
    }
}
