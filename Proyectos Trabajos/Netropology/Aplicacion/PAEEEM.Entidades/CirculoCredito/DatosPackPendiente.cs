using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.CirculoCredito
{
    [Serializable]
    public class DatosPackPendiente
    {
        public string Nocredit { get; set; }
        public string folioConsulta { get; set; }
        public DateTime? fechaConsulta { get; set; }
        public string statusPaquete { get; set; }
        public int idstatus { get; set; }
        public string comentarios { get; set; }
        ////
        public int x { get; set; }
        public DateTime? fechaAdicion { get; set; }
    }
}
