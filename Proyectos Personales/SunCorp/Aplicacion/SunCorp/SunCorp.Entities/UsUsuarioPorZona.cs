//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SunCorp.Entities
{
    
    using System;
    using System.Collections.Generic;
    
    using System.Runtime.Serialization;
    
    [DataContract]
    public partial class UsUsuarioPorZona
    
    {
    
    	[DataMember]
        public int IdUsuarioZona { get; set; }
    
    	[DataMember]
        public Nullable<int> IdZona { get; set; }
    
    	[DataMember]
        public Nullable<int> IdUsuarios { get; set; }
    
    
        public virtual UsUsuarios UsUsuarios { get; set; }
        public virtual UsZona UsZona { get; set; }
    }
}
