using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.AltaBajaEquipos
{
    [Serializable]
    public class GrdEquiposAlta
    {
        public int ID_CREDITO_PRODUCTO { get; set; }
        public string Grupo { get; set; }
        public int Cve_Tecnologia { get; set; }
        public string tecnologia { get; set; }
        public string marca { get; set; }
        public string Modelo { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Capacidad { get; set; }
        public decimal Gastos_Instalacion { get; set; }
        public decimal ImporteTotalSinIva { get; set; }
        public int idConsecutivo { get; set; }
    }
}