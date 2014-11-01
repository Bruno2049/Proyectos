using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.SubEstaciones
{
    public class NuevoRPU
    {
        public string No_Credito { get; set; }
        public string RPU_Distribuidor { get; set; }
        public DateTime Fecha_Captura_RPU_Disct { get; set; }
        public string Usuario_Distribuidor { get; set; }
        public string RPU_Jefe_Zona { get; set; }
        public DateTime Fecha_Captura_RPU_Zona { get; set; }
        public string Usuario_Jefe_zona { get; set; }
        public int Proceso_Iniciado { get; set; }        
    }
}
