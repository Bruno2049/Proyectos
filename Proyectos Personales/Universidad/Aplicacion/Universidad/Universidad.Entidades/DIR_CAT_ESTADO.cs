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
    
    public partial class DIR_CAT_ESTADO
    {
        public DIR_CAT_ESTADO()
        {
            this.DIR_CAT_CODIGO_POSTAL = new HashSet<DIR_CAT_CODIGO_POSTAL>();
            this.DIR_CAT_DELG_MUNICIPIO = new HashSet<DIR_CAT_DELG_MUNICIPIO>();
            this.DIR_DIRECCIONES = new HashSet<DIR_DIRECCIONES>();
        }
    
        public int ID_ESTADO { get; set; }
        public string NOMBRE_ESTADO { get; set; }
    
        public virtual ICollection<DIR_CAT_CODIGO_POSTAL> DIR_CAT_CODIGO_POSTAL { get; set; }
        public virtual ICollection<DIR_CAT_DELG_MUNICIPIO> DIR_CAT_DELG_MUNICIPIO { get; set; }
        public virtual ICollection<DIR_DIRECCIONES> DIR_DIRECCIONES { get; set; }
    }
}
