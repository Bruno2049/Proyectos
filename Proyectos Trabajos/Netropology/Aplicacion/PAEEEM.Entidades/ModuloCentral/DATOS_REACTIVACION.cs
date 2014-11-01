using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class DATOS_REACTIVACION
    {
        public int? Rownum { get; set; }
        public string RPU { get; set; }
        public string NoCredito { get; set; }
        public string Cliente { get; set; }
        public string NomComercial { get; set; }
        public DateTime? FechCancelacion { get; set; }
        public string UltimoStatus { get; set; }
        public string MotivoCancel { get; set; }
        public string Region { get; set; }
        public string Zona { get; set; }
        /////////
        //public int Rol_user { get; set; }
        //public int Id_Proveedor { get; set; }
        //public int Id_Branch { get; set; }
       // public int IdCliente { get; set; }
        public decimal Monto_Solicitado { get; set; }
        public DateTime? Fecha_Consulta { get; set; }
        public string RFC { get; set; }
       //public int NumReactivar { get; set; }
        public int DiasDif { get; set; }
        public DateTime? fechaMotiCancel { get; set; }
        public string NoCreditoMotivoCancel { get; set; }
        public decimal sumaRFC { get; set; }
       
    }
}
