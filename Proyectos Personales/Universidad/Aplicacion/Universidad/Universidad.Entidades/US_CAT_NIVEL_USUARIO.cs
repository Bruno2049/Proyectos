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
    
    public partial class US_CAT_NIVEL_USUARIO
    {
        public US_CAT_NIVEL_USUARIO()
        {
            this.US_USUARIOS = new HashSet<US_USUARIOS>();
        }
    
        public int ID_NIVEL_USUARIO { get; set; }
        public string NIVEL_USUARIO { get; set; }
        public string DESCRIPCION { get; set; }
    
        public virtual ICollection<US_USUARIOS> US_USUARIOS { get; set; }
    }
}
