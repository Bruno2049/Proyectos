using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Validacion_RFC_E
{
    public class GridZona
    {
        public int ID_Validacion { get; set; }
        public string Distribuidos_RS { get; set; }
        public string Distribuidor_NC { get; set; }
        public string Tipo_Persona { get; set; }
        public string Nombre_RazonSocial { get; set; }
        public DateTime Fecha_NacRegistro { get; set; }
        public string RFC { get; set; }
        public byte[] Comprobante { get; set; }
    }
}
