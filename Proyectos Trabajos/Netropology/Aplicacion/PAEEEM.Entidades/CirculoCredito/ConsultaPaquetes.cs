using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.CirculoCredito
{
    [Serializable]
    public class ConsultaPaquetes
    {
        public string status { get; set; }
        public string distribuidor { get; set; }
        public string noCredit { get; set; }
        public string folioConsulta { get; set; }
        public DateTime fechaConsulta { get; set; }
        public int noPakete{get;set;}
        public string folioPakete { get; set; }
        public DateTime? fechaRevision { get; set; }
        public int idstatus { get; set; }

        public byte[] carta { get; set; }
        public byte[] acta { get; set; }
    }
}
