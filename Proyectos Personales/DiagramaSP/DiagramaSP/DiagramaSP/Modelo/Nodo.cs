using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramaSP.Modelo
{
    public class Nodo
    {
        public int IdFuncion { get; set; }
        public int IdFuncionPadre { get; set; }
        public string NombreFuncion { get; set; }
        public int NoLineaFuncion { get; set; }
        public string Codigo { get; set; }
        public string CodigoInterno { get; set; }
    }
}
