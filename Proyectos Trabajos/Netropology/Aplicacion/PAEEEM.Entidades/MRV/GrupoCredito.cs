using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.MRV
{
    [Serializable]
    public class GrupoCredito
    {
        public int IdGrupo { get; set; }
        public string Grupo { get; set; }
        public bool EsTrifasico { get; set; }
    }
}
