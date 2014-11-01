using System.Collections.Generic;

namespace PAEEEM.Entidades.Alta_Solicitud
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public CLI_Cliente DatosCliente { get; set; }
        public List<DIR_Direcciones> DireccionesCliente { get; set; }
        public CLI_Ref_Cliente RepresentanteLegal { get; set; }
        public CLI_Ref_Cliente ObligadoSolidario { get; set; }
        public CLI_Ref_Cliente RepLegalObligadoSolidario { get; set; }
        public List<CLI_Referencias_Notariales> ReferenciasNotariales { get; set; } 
    }
}
