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
    public partial class MAT_CAT_CREDITOS_POR_HORAS
    
    {
    
    	[DataMember]
        public int IDCREDITOS { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CREDITOSMINIMOS { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CREDITOSMAXIMOS { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> HORASMINIMASPORSEMANA { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> HORASMAXIMASPORSEMANA { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> HORASTOTALESPORSEMESTRE { get; set; }
    
    }
}