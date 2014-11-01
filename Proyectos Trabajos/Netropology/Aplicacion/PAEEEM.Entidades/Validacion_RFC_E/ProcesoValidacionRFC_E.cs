using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Validacion_RFC_E
{
    public class ProcesoValidacionRFC_E
    {
        public int Id_Validacion { get; set; }
        public int Id_Distribuidor { get; set; }
        public Nullable<int> Id_Proveedor { get; set; }
        public Nullable<int> Id_Branch { get; set; }
        public Nullable<int> Id_JefeZona { get; set; }
        public string Nombre_RZ { get; set; }
        public System.DateTime Fecha_Nac_Reg { get; set; }
        public string Tipo_Persona { get; set; }
        public string RFC { get; set; }
        public byte[] Comprobante { get; set; }
        public byte Estatus_Validacion { get; set; }
        public string Comentarios_Validacion { get; set; }
        public System.DateTime Fecha_Solicitud { get; set; }
        public Nullable<System.DateTime> Fecha_Validacion { get; set; }
    }
}
