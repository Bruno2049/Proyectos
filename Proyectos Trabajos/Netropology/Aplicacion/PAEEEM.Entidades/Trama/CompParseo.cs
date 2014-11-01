using System;
using System.Collections.Generic;

namespace PAEEEM.Entidades.Trama
{
    [Serializable]
    public class CompParseo
    {
        public int Periodos { get; set; }       
        public string Trama { get; set; }
        public CompInformacionGeneral InformacionGeneral { get; set; }
        public CompConsumo Consumo { get; set; }
        public CompDetalleConsumo DetalleConsumos { get; set; }
        public List<CompHistorialConsumo> HistorialConsumo { get; set; }
        public List<CompHistorialDetconsumo> HistorialDetconsumos { get; set; }
        public CompInfoConsDeman InfoDemanda { get; set; }
        public CompInfoConsDeman InfoConsumo { get; set; }
    }
}
