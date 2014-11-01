using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Alta_Solicitud
{
    public class Credito
    {
        public string NoCredito { get; set; }
        public decimal MontoSolicitado { get; set; }
        public byte? Estatus { get; set; }
        public string Trama { get; set; }
        ///*
        // * datos del credito
        // */
        //public int IdCliente { get; set; }
        //public CLI_Cliente DatosCliente { get; set; }
        //public List<DIR_Direcciones> DireccionesCliente { get; set; }
        //public CLI_Ref_Cliente RepresentanteLegal { get; set; }
        //public CLI_Ref_Cliente ObligadoSolidario { get; set; }
        //public CLI_Ref_Cliente RepLegalObligadoSolidario { get; set; }
        //public List<CLI_Referencias_Notariales> ReferenciasNotariales { get; set; }
    }
}
