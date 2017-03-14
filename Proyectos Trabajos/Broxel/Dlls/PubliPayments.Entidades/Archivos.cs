//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PubliPayments.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Archivos
    {
        public Archivos()
        {
            this.ArchivosError = new HashSet<ArchivosError>();
        }
    
        public int id { get; set; }
        public string Archivo { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> Registros { get; set; }
        public Nullable<int> Tiempo { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Estatus { get; set; }
        public System.DateTime FechaAlta { get; set; }
    
        public virtual ICollection<ArchivosError> ArchivosError { get; set; }
    }
}