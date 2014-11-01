using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class DatHistoricoProveedores
    {
        public string region { get; set; }
        public string zona { get; set; }
        public int idProveedor { get; set; }
        public string tipo { get; set; }
        public string NomNC { get; set; }
        public string NomRS { get; set; }
        public string Status { get; set; }
        public DateTime? fechaEstatus { get; set; }
        public string motivo { get; set; }
        public string usuario { get; set; }
    }
}
