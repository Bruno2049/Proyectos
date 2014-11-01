using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Alta_Solicitud
{
    [Serializable]
    public class HistoricoCredito
    {
        public int IdSecuencia { get; set; }
        public string Descripcion { get; set; }
        public string Motivo { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreRol { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Observaciones { get; set; }
    }
}
