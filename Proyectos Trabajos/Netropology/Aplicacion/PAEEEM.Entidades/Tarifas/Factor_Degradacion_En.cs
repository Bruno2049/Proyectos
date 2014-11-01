using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.Tarifas
{
    public class Factor_Degradacion_En
    {
        public int Cve_Deleg_Municipio { get; set; }
        public int Cve_Tecnologia { get; set; }
        public decimal FACTOR_DEGRADACION1 { get; set; }
        public bool ESTATUS { get; set; }
        public string ADICIONADO_POR { get; set; }
        public System.DateTime FECHA_ADICION { get; set; }
        public string MODIFICADO_POR { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }


        public virtual CAT_DELEG_MUNICIPIO CAT_DELEG_MUNICIPIO { get; set; }
        public virtual CAT_TECNOLOGIA CAT_TECNOLOGIA { get; set; }
    }
}
