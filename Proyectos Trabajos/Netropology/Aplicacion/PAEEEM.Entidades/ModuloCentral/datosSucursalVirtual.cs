using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class datosSucursalVirtual
    {
        public int Id_Branch { get; set; }

        public int? Cve_Region { get; set; }
        public string Dx_Nombre_Region { get; set; }

        public int? Cve_Zona { get; set; }
        public string Dx_Nombre_Zona { get; set; }

        public int? Cve_Estado_Part { get; set; }
        public string Dx_Nombre_Estado { get; set; }

        public string Dx_Deleg_Municipio { get; set; }
        public int? Cve_Deleg_Municipio_Part { get; set; }
        
        public string Dx_Domicilio_Part_CP { get; set; }
        public string Dx_Colonia { get; set; } 

        public string Dx_Nombre_Comercial { get; set; }

        public int? id_Dependencia { get; set; }
        public string Dx_Nombre_Dependencia { get; set; }

        public int? id_S_FIS { get; set; }

        public string DX_Nombre_Vinculado { get; set; }
     
       
      
 
       

    }
}
