//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAEEEM.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class RELACION_VENDEDOR_DISTRIBUIDOR
    {
        public int CVE_RELACION { get; set; }
        public int ID_VENDEDOR { get; set; }
        public int Id_Branch { get; set; }
        public string ADICIONADO_POR { get; set; }
        public System.DateTime FECHA_ADICION { get; set; }
        public string MODIFICADO_POR { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual VENDEDORES VENDEDORES { get; set; }
        public virtual CAT_PROVEEDORBRANCH CAT_PROVEEDORBRANCH { get; set; }
    }
}