using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class DetalleVariablesGlobales
    {
        public int IdParametro { get; set; }
        public int IdSeccion { get; set; }
        public string Descripcion { get; set; }
        public string valor { get; set; }
        public bool? estatus { get; set; }
        public DateTime fecha_adiccion { get; set; }
        public string adicionadoPor { get; set; }
        public bool? parametro_modificable { get; set; }

        public byte ID_Prog_Proy { get; set; }
        //public byte total_incentivo { get; set; }//0-PG 1-TF 2-TI
        public int? Rownum { get; set; }
    }
}
