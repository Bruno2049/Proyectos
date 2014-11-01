using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class MotivosCredito
    {
        public string NoIntentos { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Causa { get; set; }
        public string Motivo { get; set; }
        public string Datos { get; set; }
        public DateTime Fecha { get; set; }
        public string Zona { get; set; }
        public string Region { get; set; }
        public string Distribuidor { get; set; }
        public string IdTrama { get; set; }
        public int Secuencia_E_Alta { get; set; }
        public int Secuencia_E_Baja { get; set; }

        //public string TecnologiaB { get; set; }
        //public string GrupoB { get; set; }
        //public string ProductoB { get; set; }
        //public string CapacidadB { get; set; }
        //public string Cantidad { get; set; }
        //public string ProductoA { get; set; }
        //public string MarcaA { get; set; }
        //public string ModeloA { get; set; }
        //public string CapacidadA { get; set; }
        //public string CantidadA { get; set; }
        //public decimal? PrecioUnitario { get; set; }
        //public decimal? GastosInstalacion { get; set; }
        //public string IdTrama { get; set; }

    }
}
