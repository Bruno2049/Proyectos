using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class DatosConsulta
    {
        public string No_Credito { get; set; }
        public string RPU { get; set; }
        public string NombreRazonSocial { get; set; }
        public string RFC { get; set; }
        public decimal? Monto_Solicitado { get; set; }
        public string Dx_Estatus_Credito { get; set; }
        public DateTime? Fecha_Ultmod { get; set; }
        public string Dx_Razon_Social { get; set; }
        public string Dx_Nombre_Comercial { get; set; }
        public string Dx_Nombre_Region { get; set; }
        public string Dx_Nombre_Zona { get; set; }

        //add by @l3x
        public int no_consultasCrediticias { get; set; }
        //<---------
        public int? Rownum { get; set; }
    }
}
