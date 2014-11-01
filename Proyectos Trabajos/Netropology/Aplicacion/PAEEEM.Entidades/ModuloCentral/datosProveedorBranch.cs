using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class datosProveedorBranch
    {
        public int Id_Branch { get; set; }
        public string Dx_Nombre_Comercial { get; set; }
        public string Codigo_Postal { get; set; }
        public string Dx_Nombre_Estado { get; set; }
        public string Dx_Deleg_Municipio { get; set; }
        public string Dx_Colonia { get; set; }
        public string Dx_Domicilio_Part_Calle { get; set; }
        public string Dx_Domicilio_Part_Num { get; set; }
        public string Dx_Nombre_Repre { get; set; }
        public string Dx_Email_Repre { get; set; }
        public string Dx_Telefono_Repre { get; set; }
        public string Dx_Nombre_Repre_Legal { get; set; }
        public byte[] Acta_Constitutiva { get; set; }
        public string Dx_Nombre_Zona { get; set; }
        public string Dx_Domicilio_Part_CP { get; set; }
        public int? Cve_Deleg_Municipio_Part { get; set; }
        public int? Cve_Estado_Part { get; set; }
        public int? Cve_Zona { get; set; }

        //

        public string Apellido_Paterno_Resp { get; set; }
        public string Apellido_Materno_Resp { get; set; }

        public string Apellido_Paterno_Rep_Legal { get; set; }
        public string Apellido_Materno_Rep_Legal { get; set; }

        
    }
}
