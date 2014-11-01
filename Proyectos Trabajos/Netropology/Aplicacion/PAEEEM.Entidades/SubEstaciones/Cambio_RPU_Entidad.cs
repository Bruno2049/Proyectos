using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.SubEstaciones
{
    public class Cambio_RPU_Entidad
    {
        public string No_Credito { get; set; }
        public string RPU_Distribuidor { get; set; }
        public string Usuario_Distribuidor { get; set; }
        public Nullable<System.DateTime> Fecha_Captura_Dist { get; set; }
        public string RPU_Jefe_Zona { get; set; }
        public string Usuario_Jefe_Zona { get; set; }
        public Nullable<System.DateTime> Fecha_Captura_Zona { get; set; }
        public Nullable<int> Proceso_Iniciado { get; set; }
    }
}
