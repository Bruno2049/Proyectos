using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Alta_Solicitud
{
    [Serializable]
    public class Colonia
    {
        public int CveCp { get; set; }
        public string CodigoPostal { get; set; }
        public string DxColonia { get; set; }
        public string DxTipoColonia { get; set; }
        public int? CveDelegMunicipio { get; set; }
        public string DxDelegacionMunicipio { get; set; }
        public int CveEstado { get; set; }
        public string DxEstado { get; set; }
    }
}
