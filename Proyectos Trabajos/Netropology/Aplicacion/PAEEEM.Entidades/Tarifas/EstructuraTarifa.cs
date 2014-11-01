using System;
using System.Collections.Generic;

namespace PAEEEM.Entidades.Tarifas
{
    [Serializable]
    public class CompFacturacion
    {
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public decimal PagoFactBiMen { get; set; }
        public decimal MontoMaxFacturar { get; set; }
        public decimal MontoFactMensualSNIVA { get; set; }

        public List<CompConceptosFacturacion> ConceptosFacturacion { get; set; }
    }

    [Serializable]
    public class CompConceptosFacturacion
    {
        public int IdConcepto { get; set; }
        public string Concepto { get; set; }
        public decimal CPromedioODemMax { get; set; }//es empleado para el consumo promedio o la demanda maxima
        public decimal CargoAdicional { get; set; }//es empleado para el cargo adicional consumo o deman
        public decimal Facturacion { get; set; }
        public decimal FactorPotencia { get; set; }        
    }

    [Serializable]
    public class CompResultado
    {
        public bool ValidacionValue { get; set; }        
        public bool PsrValue { get; set; }       
        public bool CapacidadPagoValue { get; set; }       
        public bool NuevaFacturaNegativaValue { get; set; }       
        public bool ConsumoNegativoValue { get; set; }        
    }

    [Serializable]
    public class CompResultadoValidacion
    {
        public int IdResultado { get; set; }
        public string Nombre { get; set; }
        public string Resultado { get; set; }
    }

    [Serializable]
    public class CompConceptosPsr
    {        
        public decimal MontoFinanciamento { get; set; }
        public decimal AhorroEconomico { get; set; }
        public int Periodo { get; set; }
        public decimal ValorPsr { get; set; }
    }

    [Serializable]
    public class CompConceptosCappago
    {
        public decimal Ventas { get; set; }
        public decimal Gastos { get; set; }
        public decimal Ahorro { get; set; }
        public decimal Flujo { get; set; }
        public decimal Capacidad { get; set; }
    }

}
