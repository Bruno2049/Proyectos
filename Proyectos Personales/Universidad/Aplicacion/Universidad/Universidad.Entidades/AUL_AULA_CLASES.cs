//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universidad.Entidades
{
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;
    
    
    [DataContract]
    public partial class AUL_AULA_CLASES
    
    {
        public AUL_AULA_CLASES()
        {
            this.MAT_HORARIO_POR_MATERIA = new HashSet<MAT_HORARIO_POR_MATERIA>();
        }
    
    
    	[DataMember]
        public short IDAULACLASES { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDTIPOAULA { get; set; }
    
    
    	[DataMember]
        public string AULA { get; set; }
    
    
    	[DataMember]
        public Nullable<short> MAXLUGARES { get; set; }
    
    
        public virtual AUL_CAT_TIPO_AULA AUL_CAT_TIPO_AULA { get; set; }
        public virtual ICollection<MAT_HORARIO_POR_MATERIA> MAT_HORARIO_POR_MATERIA { get; set; }
    }
}
