using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.CirculoCredito
{
     [Serializable]
    public class TemporalPDFs
    {   
         public string nocredit { get; set; }
         public bool cheked { get; set; }
         public byte[] carta { get; set; }
         public byte[] acta { get; set; }

    }
}
