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
    
    public partial class US_HISTORIAL
    {
        public US_HISTORIAL()
        {
            this.US_USUARIOS = new HashSet<US_USUARIOS>();
        }
    
        public int ID_HISTORIAL { get; set; }
        public Nullable<int> ID_LOG_REGISTRO { get; set; }
        public Nullable<System.DateTime> FECHA_ULTIMA_CESION { get; set; }
        public Nullable<System.DateTime> FECHA_CAMBIOCONTRASENA_ULTIMO { get; set; }
        public string ULTIMA_CONTRASENA { get; set; }
    
        public virtual US_LOG_REGISTRO US_LOG_REGISTRO { get; set; }
        public virtual ICollection<US_USUARIOS> US_USUARIOS { get; set; }
    }
}
