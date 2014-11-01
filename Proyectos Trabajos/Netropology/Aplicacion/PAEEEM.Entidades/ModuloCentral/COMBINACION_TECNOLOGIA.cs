using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class Combinacion_Tecnologia
    {
        public Combinacion_Tecnologia()
        { }

        public int CveTecnologia { get; set; }
        public int CveTecnologiaCombinada { get; set; }
        public string TecnologíaCombinada { get; set; }
        public int? Estatus { get; set; }
        public DateTime? FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
        public int EstatusInt { get; set; }
    }
}
