using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Tarifas
{
    public class CAT_Tecnologia_FD
    {
        public int? IDREGION_BIOCLIMA { get; set; }
        public string REGION { get; set; }
        public int Cve_Tecnologia { get; set; }
        public string Dx_Nombre_General { get; set; }        
        public decimal FACTOR_DEGRADACION { get; set; }
        public string USUARIO { get; set; }
    }
}
