//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityPrueba.Repositorio
{
    using System;
    using System.Collections.Generic;
    
    public partial class US_CAT_TIPO_USUARIO
    {
        public US_CAT_TIPO_USUARIO()
        {
            this.US_USUARIOS = new HashSet<US_USUARIOS>();
        }
    
        public int ID_TIPO_USUARIO { get; set; }
        public Nullable<int> ID_LOG_REGISTRO { get; set; }
        public string TIPO_USUARIO { get; set; }
        public string DESCRIPCION { get; set; }
    
        public virtual US_LOG_REGISTRO US_LOG_REGISTRO { get; set; }
        public virtual ICollection<US_USUARIOS> US_USUARIOS { get; set; }
    }
}
