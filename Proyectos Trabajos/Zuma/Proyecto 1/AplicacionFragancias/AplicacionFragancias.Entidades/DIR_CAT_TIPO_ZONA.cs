//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AplicacionFragancias.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class DIR_CAT_TIPO_ZONA
    {
        public DIR_CAT_TIPO_ZONA()
        {
            this.DIR_CAT_COLONIAS = new HashSet<DIR_CAT_COLONIAS>();
        }
    
        public int IDTIPOZONA { get; set; }
        public string TIPOZONA { get; set; }
    
        public virtual ICollection<DIR_CAT_COLONIAS> DIR_CAT_COLONIAS { get; set; }
    }
}
