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
    public partial class HOR_CAT_DIAS_FESTIVOS
    
    {
    
    	[DataMember]
        public int IDDIASFESTIVOS { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDTIPOFERIADO { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDDIA { get; set; }
    
    
    	[DataMember]
        public string NOMBREDIAFESTIVO { get; set; }
    
    
    	[DataMember]
        public Nullable<System.DateTime> FECHADIAFESTIVO { get; set; }
    
    
        public virtual HOR_CAT_DIAS_SEMANA HOR_CAT_DIAS_SEMANA { get; set; }
        public virtual HOR_CAT_TIPO_DIA_FERIADO HOR_CAT_TIPO_DIA_FERIADO { get; set; }
    }
}