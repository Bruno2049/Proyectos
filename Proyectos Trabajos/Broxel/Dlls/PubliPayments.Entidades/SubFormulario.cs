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
    
    public partial class SubFormulario
    {
        public SubFormulario()
        {
            this.CamposXSubFormularios = new HashSet<CamposXSubFormulario>();
        }
    
        public int idSubFormulario { get; set; }
        public int idFormulario { get; set; }
        public string SubFormulario1 { get; set; }
        public string Clase { get; set; }
    
        public virtual ICollection<CamposXSubFormulario> CamposXSubFormularios { get; set; }
        public virtual Formulario Formulario { get; set; }
    }
}