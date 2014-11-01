using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]    
    public class Campo_Base_Plantilla
    {
        public Campo_Base_Plantilla()
        { }

        public int Cve_Plantilla {get; set;}
        public int Cve_Campo_Base {get; set;}
        public string Dx_Campo_Base {get; set;}
        public string Dx_Nombre_Control {get; set;}
        public string Dx_Tabla_BD {get; set;}
        public string Dx_Campo_BD {get; set;}
        public int? Cve_Agregar_Reporte {get; set;}
        public int? Estatus {get; set;}
        public int? Cve_Adicional1 {get; set;}
        public int? Cve_Adicional2 {get; set;}
        public string Cve_Adicional3 { get; set; }
        public string Dx_Adicional1 {get; set;}
        public string Dx_Adicional2 {get; set;}
    }
}
