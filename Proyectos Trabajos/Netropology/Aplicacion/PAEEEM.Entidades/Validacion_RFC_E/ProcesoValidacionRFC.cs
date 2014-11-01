using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Validacion_RFC_E
{
    public class ProcesoValidacionRFC
    {
        public string RFC { get; set; }
        public int Estatus_Validacion { get; set; }
        public string Comentarios { get; set; }
        public DateTime? Fecha_Solicitud { get; set; }
        public DateTime? Fecha_Validacion { get; set; }
    }
}
