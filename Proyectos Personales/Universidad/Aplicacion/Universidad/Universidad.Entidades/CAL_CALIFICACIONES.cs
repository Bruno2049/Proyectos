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
    public partial class CAL_CALIFICACIONES
    
    {
        public CAL_CALIFICACIONES()
        {
            this.CAL_ALUMNO_KARDEX = new HashSet<CAL_ALUMNO_KARDEX>();
            this.CAL_CALIFICACION_CLASE = new HashSet<CAL_CALIFICACION_CLASE>();
        }
    
    
    	[DataMember]
        public int IDCALIFICACION { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDALUMNOS { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDCALIFICACIONESFECHAS { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDMATERIA { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDTIPOEVALUACION { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONPRIMERPERIODOORDINARIO { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALFIICACIONSEGUNDOPERIODOORDINARIO { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONTERCERPERIODOORDINARIO { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONFINALORDINARIA { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONPRIMERPERIODORECURSAMIENTO { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONSEGUNDOPERIODORECURSAMIENTO { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONTERCERPERIODORECUSAMIENTO { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONFINALRECURSAMIENTO { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONETS1 { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONETS2 { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONETS3 { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CALIFICACIONETS4 { get; set; }
    
    
    	[DataMember]
        public Nullable<int> CREDITOSOBTENIDOS { get; set; }
    
    
    	[DataMember]
        public bool ACREDITADA { get; set; }
    
    
        public virtual ALU_ALUMNOS ALU_ALUMNOS { get; set; }
        public virtual ICollection<CAL_ALUMNO_KARDEX> CAL_ALUMNO_KARDEX { get; set; }
        public virtual ICollection<CAL_CALIFICACION_CLASE> CAL_CALIFICACION_CLASE { get; set; }
        public virtual CAL_CALIFICACIONES_FECHAS CAL_CALIFICACIONES_FECHAS { get; set; }
        public virtual CAL_CAT_TIPO_EVALUACION CAL_CAT_TIPO_EVALUACION { get; set; }
        public virtual MAT_CAT_MATERIAS MAT_CAT_MATERIAS { get; set; }
    }
}
