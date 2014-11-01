using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class EquiposAlta
    {
        public string NoIntentos { get; set; }
        public int ID { get; set; }
        public string Producto { get; set; }
        public string Dx_Marca { get; set; }
        public string Dx_Modelo { get; set; }
        public string Dx_Sistema { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Importe_Total_Sin_IVA { get; set; }
        public decimal? Gasto_Instalacion { get; set; }
        public int Secuencia_E_Alta { get; set; }
    }

    [Serializable]
    public class EquiposBaja
    {
        public string NoIntentos { get; set; }
        public int ID { get; set; }
        public string Dx_Tecnologia { get; set; }
        public string Dx_Grupo { get; set; }
        public string Dx_Tipo_Producto { get; set; }
        public string Dx_Consumo { get; set; }
        public int? Cantidad { get; set; }
        public int Secuencia_E_Baja { get; set; }
    }
}
