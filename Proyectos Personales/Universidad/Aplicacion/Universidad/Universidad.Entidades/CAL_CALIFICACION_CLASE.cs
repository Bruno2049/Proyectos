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
    public partial class CAL_CALIFICACION_CLASE
    
    {
    
    	[DataMember]
        public int IDCALIFICACIONCLASE { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDCLASE { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDCALIFICACION { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDTIPOEVALUACION { get; set; }
    
    
        public virtual CAL_CAT_TIPO_EVALUACION CAL_CAT_TIPO_EVALUACION { get; set; }
        public virtual CAL_CALIFICACIONES CAL_CALIFICACIONES { get; set; }
        public virtual CLA_CLASE CLA_CLASE { get; set; }
    }
}