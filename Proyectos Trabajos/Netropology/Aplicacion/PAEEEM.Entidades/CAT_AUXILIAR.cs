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
    
    public partial class CAT_AUXILIAR
    {
        public int Id_Auxiliar { get; set; }
        public string No_Credito { get; set; }
        public string Dx_Nombres { get; set; }
        public string Dx_Apellido_Paterno { get; set; }
        public string Dx_Apellido_Materno { get; set; }
        public Nullable<System.DateTime> Dt_Nacimiento_Fecha { get; set; }
        public string Dx_Numero_Interior { get; set; }
        public string Dx_Ciudad { get; set; }
        public Nullable<int> No_MOP { get; set; }
        public string Ft_Folio { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Consulta { get; set; }
    
        public virtual K_CREDITO K_CREDITO { get; set; }
    }
}