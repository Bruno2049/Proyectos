using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.MRV
{
    [Serializable]
    public class MedicionDetalle
    {
        public int IdMedicionDetalle { get; set; }
        public int IdMedicion { get; set; }
        public int IdCampoMedicion { get; set; }
        public string CampoMedicion { get; set; }
        public int IdGrupo { get; set; }
        public string Grupo { get; set; }
        public decimal EquipoIneficiente { get; set; }
        public decimal EquipoEficiente { get; set; }
        public string Unidad { get; set; }
        public decimal Valor { get; set; }
        public decimal Porcentaje { get; set; }
        public string Observaciones { get; set; }
        public bool Estatus { get; set; }
        public DateTime FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
    }
}
