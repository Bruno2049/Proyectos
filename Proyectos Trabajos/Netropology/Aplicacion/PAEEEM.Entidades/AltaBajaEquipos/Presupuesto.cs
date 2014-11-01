using System;

namespace PAEEEM.Entidades.AltaBajaEquipos
{
    [Serializable]
    public class Presupuesto
    {
        public int IdPresupuesto { get; set; }
        public int IdOrden { get; set; }
        public string Nombre { get; set; }
        public decimal Resultado { get; set; }
    }
}
