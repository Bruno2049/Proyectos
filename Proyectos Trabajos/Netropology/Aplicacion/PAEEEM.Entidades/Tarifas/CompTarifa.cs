using System;

namespace PAEEEM.Entidades.Tarifas
{
    [Serializable]
    public class CompTarifa
    {
        public int Periodos { get; set; }
        public bool AnioFactValido { get; set; }
        public bool PeriodosValidos { get; set; }
        public bool ValidaFechaTablasTarifas { get; set; }
        public bool CumpleConsumo { get; set; }
        public CompFacturacion FactSinAhorro { get; set; }        
        public CompFacturacion FactConAhorro { get; set; }        
    }
}
