using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.MRV
{
    [Serializable]
    public class EquipoAltaEficienciaMrv
    {
        public int IdEquipo { get; set; }
        public int IdCuestionario { get; set; }
        public int IdCreditoProducto { get; set; }
        public int IdTecnologia { get; set; }
        public string NombreEquipo { get; set; }
        public bool EnOperacion { get; set; }
        public bool Estatus { get; set; }
        public DateTime FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }

        public void MoverEnOperacion(bool valor)
        {
            this.EnOperacion = valor;
        }
    }
}
