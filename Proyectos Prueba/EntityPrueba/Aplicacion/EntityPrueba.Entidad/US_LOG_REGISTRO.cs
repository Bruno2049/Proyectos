//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityPrueba.Entidad
{
    using System;
    using System.Collections.Generic;
    
    public partial class US_LOG_REGISTRO
    {
        public US_LOG_REGISTRO()
        {
            this.US_CAT_ESTATUS_USUARIO = new HashSet<US_CAT_ESTATUS_USUARIO>();
            this.US_CAT_NIVEL_USUARIO = new HashSet<US_CAT_NIVEL_USUARIO>();
            this.US_CAT_TIPO_USUARIO = new HashSet<US_CAT_TIPO_USUARIO>();
            this.US_HISTORIAL = new HashSet<US_HISTORIAL>();
            this.US_USUARIOS = new HashSet<US_USUARIOS>();
        }
    
        public int ID_LOG_REGISTRO { get; set; }
        public string ADICIONADO_POR { get; set; }
        public System.DateTime FECHA_REGISTRO { get; set; }
        public string MODIFICADO_POR { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
        public bool ES_BORRADO { get; set; }
    
        public virtual ICollection<US_CAT_ESTATUS_USUARIO> US_CAT_ESTATUS_USUARIO { get; set; }
        public virtual ICollection<US_CAT_NIVEL_USUARIO> US_CAT_NIVEL_USUARIO { get; set; }
        public virtual ICollection<US_CAT_TIPO_USUARIO> US_CAT_TIPO_USUARIO { get; set; }
        public virtual ICollection<US_HISTORIAL> US_HISTORIAL { get; set; }
        public virtual ICollection<US_USUARIOS> US_USUARIOS { get; set; }
    }
}
