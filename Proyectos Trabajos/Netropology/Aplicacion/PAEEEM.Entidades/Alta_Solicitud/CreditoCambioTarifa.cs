using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Alta_Solicitud
{
    public class CreditoCambioTarifa
    {
        public string IdCredito { get; set; }
        public string RpuActual { get; set; }
        public string RpuNuevo { get; set; }
        public string UsuarioDistribuidor { get; set; }
    }
}
