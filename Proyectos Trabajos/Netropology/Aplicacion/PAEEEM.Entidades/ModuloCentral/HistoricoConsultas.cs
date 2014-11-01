using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    public class HistoricoConsultas
    {
        public string No_RPU { get; set; }
        public string No_Solicitud { get; set; }
        public string Folio { get; set; }
        public byte? MOP { get; set; }
        public DateTime? Fecha_Consulta { get; set; }
    }
}
