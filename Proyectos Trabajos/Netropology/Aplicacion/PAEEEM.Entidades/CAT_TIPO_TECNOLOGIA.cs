//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAEEEM.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAT_TIPO_TECNOLOGIA
    {
        public CAT_TIPO_TECNOLOGIA()
        {
            this.CAT_TECNOLOGIA = new HashSet<CAT_TECNOLOGIA>();
        }
    
        public int Cve_Tipo_Tecnologia { get; set; }
        public string Dx_Nombre { get; set; }
        public string Atributo1 { get; set; }
        public string Atributo2 { get; set; }
        public string Atributo3 { get; set; }
        public string Atributo4 { get; set; }
        public string Atributo5 { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Tipo_Tecnologia { get; set; }
    
        public virtual ICollection<CAT_TECNOLOGIA> CAT_TECNOLOGIA { get; set; }
    }
}