//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universidad.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class PER_CAT_TIPO_PERSONA
    {
        public PER_CAT_TIPO_PERSONA()
        {
            this.PER_PERSONAS = new HashSet<PER_PERSONAS>();
        }
    
        public int ID_TIPO_PERSONA { get; set; }
        public string TIPO_PERSONA { get; set; }
        public string DESCRIPCION { get; set; }
    
        public virtual ICollection<PER_PERSONAS> PER_PERSONAS { get; set; }
    }
}
