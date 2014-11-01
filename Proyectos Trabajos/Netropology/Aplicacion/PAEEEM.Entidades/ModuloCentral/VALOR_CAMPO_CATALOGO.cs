using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class Valor_Campo_Catalogo
    {
        public Valor_Campo_Catalogo()
        { }

        public int CveCampo { get; set; }
        public int? CveValor { get; set; }
        public string DxValor { get; set; }
        public int? Estatus { get; set; }
        public DateTime? FechaAdicion { get; set; }
        public int? AdicionadoPor { get; set; }
    }
}
